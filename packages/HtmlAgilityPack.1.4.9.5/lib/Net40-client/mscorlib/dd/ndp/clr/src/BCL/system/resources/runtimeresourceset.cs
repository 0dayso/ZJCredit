// Decompiled with JetBrains decompiler
// Type: System.Resources.RuntimeResourceSet
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security;

namespace System.Resources
{
  internal sealed class RuntimeResourceSet : ResourceSet, IEnumerable
  {
    internal const int Version = 2;
    private Dictionary<string, ResourceLocator> _resCache;
    private ResourceReader _defaultReader;
    private Dictionary<string, ResourceLocator> _caseInsensitiveTable;
    private bool _haveReadFromReader;

    [SecurityCritical]
    internal RuntimeResourceSet(string fileName)
      : base(false)
    {
      this._resCache = new Dictionary<string, ResourceLocator>((IEqualityComparer<string>) FastResourceComparer.Default);
      this._defaultReader = new ResourceReader((Stream) new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read), this._resCache);
      this.Reader = (IResourceReader) this._defaultReader;
    }

    [SecurityCritical]
    internal RuntimeResourceSet(Stream stream)
      : base(false)
    {
      this._resCache = new Dictionary<string, ResourceLocator>((IEqualityComparer<string>) FastResourceComparer.Default);
      this._defaultReader = new ResourceReader(stream, this._resCache);
      this.Reader = (IResourceReader) this._defaultReader;
    }

    protected override void Dispose(bool disposing)
    {
      if (this.Reader == null)
        return;
      if (disposing)
      {
        lock (this.Reader)
        {
          this._resCache = (Dictionary<string, ResourceLocator>) null;
          if (this._defaultReader != null)
          {
            this._defaultReader.Close();
            this._defaultReader = (ResourceReader) null;
          }
          this._caseInsensitiveTable = (Dictionary<string, ResourceLocator>) null;
          base.Dispose(disposing);
        }
      }
      else
      {
        this._resCache = (Dictionary<string, ResourceLocator>) null;
        this._caseInsensitiveTable = (Dictionary<string, ResourceLocator>) null;
        this._defaultReader = (ResourceReader) null;
        base.Dispose(disposing);
      }
    }

    public override IDictionaryEnumerator GetEnumerator()
    {
      return this.GetEnumeratorHelper();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumeratorHelper();
    }

    private IDictionaryEnumerator GetEnumeratorHelper()
    {
      IResourceReader resourceReader = this.Reader;
      if (resourceReader == null || this._resCache == null)
        throw new ObjectDisposedException((string) null, Environment.GetResourceString("ObjectDisposed_ResourceSet"));
      return resourceReader.GetEnumerator();
    }

    public override string GetString(string key)
    {
      return (string) this.GetObject(key, false, true);
    }

    public override string GetString(string key, bool ignoreCase)
    {
      return (string) this.GetObject(key, ignoreCase, true);
    }

    public override object GetObject(string key)
    {
      return this.GetObject(key, false, false);
    }

    public override object GetObject(string key, bool ignoreCase)
    {
      return this.GetObject(key, ignoreCase, false);
    }

    private object GetObject(string key, bool ignoreCase, bool isString)
    {
      if (key == null)
        throw new ArgumentNullException("key");
      if (this.Reader == null || this._resCache == null)
        throw new ObjectDisposedException((string) null, Environment.GetResourceString("ObjectDisposed_ResourceSet"));
      object obj = (object) null;
      lock (this.Reader)
      {
        if (this.Reader == null)
          throw new ObjectDisposedException((string) null, Environment.GetResourceString("ObjectDisposed_ResourceSet"));
        ResourceLocator local_1;
        if (this._defaultReader != null)
        {
          int local_7 = -1;
          if (this._resCache.TryGetValue(key, out local_1))
          {
            obj = local_1.Value;
            local_7 = local_1.DataPosition;
          }
          if (local_7 == -1 && obj == null)
            local_7 = this._defaultReader.FindPosForResource(key);
          if (local_7 != -1 && obj == null)
          {
            ResourceTypeCode local_8;
            if (isString)
            {
              obj = (object) this._defaultReader.LoadString(local_7);
              local_8 = ResourceTypeCode.String;
            }
            else
              obj = this._defaultReader.LoadObject(local_7, out local_8);
            local_1 = new ResourceLocator(local_7, ResourceLocator.CanCache(local_8) ? obj : (object) null);
            lock (this._resCache)
              this._resCache[key] = local_1;
          }
          if (obj != null || !ignoreCase)
            return obj;
        }
        if (!this._haveReadFromReader)
        {
          if (ignoreCase && this._caseInsensitiveTable == null)
            this._caseInsensitiveTable = new Dictionary<string, ResourceLocator>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
          if (this._defaultReader == null)
          {
            IDictionaryEnumerator local_12 = this.Reader.GetEnumerator();
            while (local_12.MoveNext())
            {
              DictionaryEntry local_13 = local_12.Entry;
              string local_14 = (string) local_13.Key;
              ResourceLocator local_15 = new ResourceLocator(-1, local_13.Value);
              this._resCache.Add(local_14, local_15);
              if (ignoreCase)
                this._caseInsensitiveTable.Add(local_14, local_15);
            }
            if (!ignoreCase)
              this.Reader.Close();
          }
          else
          {
            ResourceReader.ResourceEnumerator local_16 = this._defaultReader.GetEnumeratorInternal();
            while (local_16.MoveNext())
              this._caseInsensitiveTable.Add((string) local_16.Key, new ResourceLocator(local_16.DataPosition, (object) null));
          }
          this._haveReadFromReader = true;
        }
        object local_4 = (object) null;
        bool local_5 = false;
        bool local_6 = false;
        if (this._defaultReader != null && this._resCache.TryGetValue(key, out local_1))
        {
          local_5 = true;
          local_4 = this.ResolveResourceLocator(local_1, key, this._resCache, local_6);
        }
        if (!local_5 & ignoreCase && this._caseInsensitiveTable.TryGetValue(key, out local_1))
        {
          bool local_6_1 = true;
          local_4 = this.ResolveResourceLocator(local_1, key, this._resCache, local_6_1);
        }
        return local_4;
      }
    }

    private object ResolveResourceLocator(ResourceLocator resLocation, string key, Dictionary<string, ResourceLocator> copyOfCache, bool keyInWrongCase)
    {
      object obj = resLocation.Value;
      if (obj == null)
      {
        ResourceTypeCode typeCode;
        lock (this.Reader)
          obj = this._defaultReader.LoadObject(resLocation.DataPosition, out typeCode);
        if (!keyInWrongCase && ResourceLocator.CanCache(typeCode))
        {
          resLocation.Value = obj;
          copyOfCache[key] = resLocation;
        }
      }
      return obj;
    }
  }
}
