// Decompiled with JetBrains decompiler
// Type: System.Resources.ResourceSet
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Resources
{
  /// <summary>存储已针对某个特定区域性进行了本地化的所有资源，忽略所有其他区域性（包括任何代用规则）。安全说明：在此类不受信任的数据中调用方法存在安全风险。仅在受信任的数据类中调用方法。有关详细信息，请参阅 Untrusted Data Security Risks。</summary>
  [ComVisible(true)]
  [Serializable]
  public class ResourceSet : IDisposable, IEnumerable
  {
    /// <summary>指示用于读取资源的 <see cref="T:System.Resources.IResourceReader" />。</summary>
    [NonSerialized]
    protected IResourceReader Reader;
    /// <summary>存储资源的 <see cref="T:System.Collections.Hashtable" />。</summary>
    protected Hashtable Table;
    private Hashtable _caseInsensitiveTable;

    /// <summary>使用默认属性初始化 <see cref="T:System.Resources.ResourceSet" /> 类的新实例。</summary>
    protected ResourceSet()
    {
      this.CommonInit();
    }

    internal ResourceSet(bool junk)
    {
    }

    /// <summary>使用从给定文件打开并读取资源的系统默认的 <see cref="T:System.Resources.ResourceReader" /> 来创建 <see cref="T:System.Resources.ResourceSet" /> 类的新实例。</summary>
    /// <param name="fileName">要读取的资源文件。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="fileName" /> 参数为 null。</exception>
    public ResourceSet(string fileName)
    {
      this.Reader = (IResourceReader) new ResourceReader(fileName);
      this.CommonInit();
      this.ReadResources();
    }

    /// <summary>使用从给定流中读取资源的系统默认的 <see cref="T:System.Resources.ResourceReader" /> 来创建 <see cref="T:System.Resources.ResourceSet" /> 类的新实例。</summary>
    /// <param name="stream">要读取的资源的 <see cref="T:System.IO.Stream" />。流应引用现有的资源文件。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="stream" /> 不可读。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="stream" /> 参数为 null。</exception>
    [SecurityCritical]
    public ResourceSet(Stream stream)
    {
      this.Reader = (IResourceReader) new ResourceReader(stream);
      this.CommonInit();
      this.ReadResources();
    }

    /// <summary>使用指定的资源阅读器创建 <see cref="T:System.Resources.ResourceSet" /> 类的新实例。</summary>
    /// <param name="reader">将使用的读取器。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="reader" /> 参数为 null。</exception>
    public ResourceSet(IResourceReader reader)
    {
      if (reader == null)
        throw new ArgumentNullException("reader");
      this.Reader = reader;
      this.CommonInit();
      this.ReadResources();
    }

    private void CommonInit()
    {
      this.Table = new Hashtable();
    }

    /// <summary>通过此 <see cref="T:System.Resources.ResourceSet" /> 来关闭和释放所有资源。</summary>
    public virtual void Close()
    {
      this.Dispose(true);
    }

    /// <summary>释放与当前实例关联的资源（内存除外），并关闭内部托管对象（如果请求这样做）。</summary>
    /// <param name="disposing">指示是否应显式关闭当前实例中包含的对象。</param>
    protected virtual void Dispose(bool disposing)
    {
      if (disposing)
      {
        IResourceReader resourceReader = this.Reader;
        this.Reader = (IResourceReader) null;
        if (resourceReader != null)
          resourceReader.Close();
      }
      this.Reader = (IResourceReader) null;
      this._caseInsensitiveTable = (Hashtable) null;
      this.Table = (Hashtable) null;
    }

    /// <summary>处置由 <see cref="T:System.Resources.ResourceSet" /> 的当前实例使用的资源（内存除外）。</summary>
    public void Dispose()
    {
      this.Dispose(true);
    }

    /// <summary>返回此类 <see cref="T:System.Resources.ResourceSet" /> 的首选资源读取器类。</summary>
    /// <returns>返回这类 <see cref="T:System.Resources.ResourceSet" /> 的首选资源阅读器的 <see cref="T:System.Type" />。</returns>
    public virtual Type GetDefaultReader()
    {
      return typeof (ResourceReader);
    }

    /// <summary>返回此类 <see cref="T:System.Resources.ResourceSet" /> 的首选资源编写器类。</summary>
    /// <returns>返回这类 <see cref="T:System.Resources.ResourceSet" /> 的首选资源编写器的 <see cref="T:System.Type" />。</returns>
    public virtual Type GetDefaultWriter()
    {
      return typeof (ResourceWriter);
    }

    /// <summary>返回 <see cref="T:System.Collections.IDictionaryEnumerator" />，它可以循环访问 <see cref="T:System.Resources.ResourceSet" />。</summary>
    /// <returns>该 <see cref="T:System.Resources.ResourceSet" /> 的 <see cref="T:System.Collections.IDictionaryEnumerator" />。</returns>
    /// <exception cref="T:System.ObjectDisposedException">资源集已关闭或已释放。</exception>
    [ComVisible(false)]
    public virtual IDictionaryEnumerator GetEnumerator()
    {
      return this.GetEnumeratorHelper();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumeratorHelper();
    }

    private IDictionaryEnumerator GetEnumeratorHelper()
    {
      Hashtable hashtable = this.Table;
      if (hashtable == null)
        throw new ObjectDisposedException((string) null, Environment.GetResourceString("ObjectDisposed_ResourceSet"));
      return hashtable.GetEnumerator();
    }

    /// <summary>搜索具有指定名称的 <see cref="T:System.String" /> 资源。</summary>
    /// <returns>当值是 <see cref="T:System.String" /> 时为资源的值。</returns>
    /// <param name="name">要搜索的资源的名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="name" /> 指定的资源不是 <see cref="T:System.String" />。</exception>
    /// <exception cref="T:System.ObjectDisposedException">对象已关闭或已释放。</exception>
    public virtual string GetString(string name)
    {
      object objectInternal = this.GetObjectInternal(name);
      try
      {
        return (string) objectInternal;
      }
      catch (InvalidCastException ex)
      {
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ResourceNotString_Name", (object) name));
      }
    }

    /// <summary>如果请求的话，按照不区分大小写的方式搜索具有指定名称的 <see cref="T:System.String" /> 资源。</summary>
    /// <returns>当值是 <see cref="T:System.String" /> 时为资源的值。</returns>
    /// <param name="name">要搜索的资源的名称。</param>
    /// <param name="ignoreCase">指示是否应忽略指定名称的大小写。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="name" /> 指定的资源不是 <see cref="T:System.String" />。</exception>
    /// <exception cref="T:System.ObjectDisposedException">对象已关闭或已释放。</exception>
    public virtual string GetString(string name, bool ignoreCase)
    {
      object objectInternal = this.GetObjectInternal(name);
      string str;
      try
      {
        str = (string) objectInternal;
      }
      catch (InvalidCastException ex)
      {
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ResourceNotString_Name", (object) name));
      }
      if (str != null || !ignoreCase)
        return str;
      object insensitiveObjectInternal = this.GetCaseInsensitiveObjectInternal(name);
      try
      {
        return (string) insensitiveObjectInternal;
      }
      catch (InvalidCastException ex)
      {
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ResourceNotString_Name", (object) name));
      }
    }

    /// <summary>搜索具有指定名称的资源对象。</summary>
    /// <returns>所请求的资源。</returns>
    /// <param name="name">要搜索的资源的区分大小写的名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数为 null。</exception>
    /// <exception cref="T:System.ObjectDisposedException">对象已关闭或已释放。</exception>
    public virtual object GetObject(string name)
    {
      return this.GetObjectInternal(name);
    }

    /// <summary>如果请求的话，按照不区分大小写的方式搜索具有指定名称的资源对象。</summary>
    /// <returns>所请求的资源。</returns>
    /// <param name="name">要搜索的资源的名称。</param>
    /// <param name="ignoreCase">指示是否应忽略指定名称的大小写。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数为 null。</exception>
    /// <exception cref="T:System.ObjectDisposedException">对象已关闭或已释放。</exception>
    public virtual object GetObject(string name, bool ignoreCase)
    {
      object objectInternal = this.GetObjectInternal(name);
      if (objectInternal != null || !ignoreCase)
        return objectInternal;
      return this.GetCaseInsensitiveObjectInternal(name);
    }

    /// <summary>读取所有资源，并将它们存储在 <see cref="F:System.Resources.ResourceSet.Table" /> 属性中指示的 <see cref="T:System.Collections.Hashtable" /> 中。</summary>
    protected virtual void ReadResources()
    {
      IDictionaryEnumerator enumerator = this.Reader.GetEnumerator();
      while (enumerator.MoveNext())
      {
        object obj = enumerator.Value;
        this.Table.Add(enumerator.Key, obj);
      }
    }

    private object GetObjectInternal(string name)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      Hashtable hashtable = this.Table;
      if (hashtable == null)
        throw new ObjectDisposedException((string) null, Environment.GetResourceString("ObjectDisposed_ResourceSet"));
      string str = name;
      return hashtable[(object) str];
    }

    private object GetCaseInsensitiveObjectInternal(string name)
    {
      Hashtable hashtable1 = this.Table;
      if (hashtable1 == null)
        throw new ObjectDisposedException((string) null, Environment.GetResourceString("ObjectDisposed_ResourceSet"));
      Hashtable hashtable2 = this._caseInsensitiveTable;
      if (hashtable2 == null)
      {
        hashtable2 = new Hashtable((IEqualityComparer) StringComparer.OrdinalIgnoreCase);
        IDictionaryEnumerator enumerator = hashtable1.GetEnumerator();
        while (enumerator.MoveNext())
          hashtable2.Add(enumerator.Key, enumerator.Value);
        this._caseInsensitiveTable = hashtable2;
      }
      return hashtable2[(object) name];
    }
  }
}
