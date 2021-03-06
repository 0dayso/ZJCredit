﻿// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.EventPayload
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;

namespace System.Diagnostics.Tracing
{
  internal class EventPayload : IDictionary<string, object>, ICollection<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable
  {
    private List<string> m_names;
    private List<object> m_values;

    public ICollection<string> Keys
    {
      get
      {
        return (ICollection<string>) this.m_names;
      }
    }

    public ICollection<object> Values
    {
      get
      {
        return (ICollection<object>) this.m_values;
      }
    }

    public object this[string key]
    {
      get
      {
        if (key == null)
          throw new ArgumentNullException("key");
        int index = 0;
        foreach (string mName in this.m_names)
        {
          if (mName == key)
            return this.m_values[index];
          ++index;
        }
        throw new KeyNotFoundException();
      }
      set
      {
        throw new NotSupportedException();
      }
    }

    public int Count
    {
      get
      {
        return this.m_names.Count;
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return true;
      }
    }

    internal EventPayload(List<string> payloadNames, List<object> payloadValues)
    {
      this.m_names = payloadNames;
      this.m_values = payloadValues;
    }

    public void Add(string key, object value)
    {
      throw new NotSupportedException();
    }

    public void Add(KeyValuePair<string, object> payloadEntry)
    {
      throw new NotSupportedException();
    }

    public void Clear()
    {
      throw new NotSupportedException();
    }

    public bool Contains(KeyValuePair<string, object> entry)
    {
      return this.ContainsKey(entry.Key);
    }

    public bool ContainsKey(string key)
    {
      if (key == null)
        throw new ArgumentNullException("key");
      foreach (string mName in this.m_names)
      {
        if (mName == key)
          return true;
      }
      return false;
    }

    public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
    {
      throw new NotSupportedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    public void CopyTo(KeyValuePair<string, object>[] payloadEntries, int count)
    {
      throw new NotSupportedException();
    }

    public bool Remove(string key)
    {
      throw new NotSupportedException();
    }

    public bool Remove(KeyValuePair<string, object> entry)
    {
      throw new NotSupportedException();
    }

    public bool TryGetValue(string key, out object value)
    {
      if (key == null)
        throw new ArgumentNullException("key");
      int index = 0;
      foreach (string mName in this.m_names)
      {
        if (mName == key)
        {
          value = this.m_values[index];
          return true;
        }
        ++index;
      }
      value = (object) null;
      return false;
    }
  }
}
