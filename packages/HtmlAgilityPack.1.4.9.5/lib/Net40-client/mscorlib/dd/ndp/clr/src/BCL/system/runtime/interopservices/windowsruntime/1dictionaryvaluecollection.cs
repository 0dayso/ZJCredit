// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.DictionaryValueEnumerator`2
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  [Serializable]
  internal sealed class DictionaryValueEnumerator<TKey, TValue> : IEnumerator<TValue>, IDisposable, IEnumerator
  {
    private readonly IDictionary<TKey, TValue> dictionary;
    private IEnumerator<KeyValuePair<TKey, TValue>> enumeration;

    object IEnumerator.Current
    {
      get
      {
        return (object) this.Current;
      }
    }

    public TValue Current
    {
      get
      {
        return this.enumeration.Current.Value;
      }
    }

    public DictionaryValueEnumerator(IDictionary<TKey, TValue> dictionary)
    {
      if (dictionary == null)
        throw new ArgumentNullException("dictionary");
      this.dictionary = dictionary;
      this.enumeration = dictionary.GetEnumerator();
    }

    void IDisposable.Dispose()
    {
      this.enumeration.Dispose();
    }

    public bool MoveNext()
    {
      return this.enumeration.MoveNext();
    }

    public void Reset()
    {
      this.enumeration = this.dictionary.GetEnumerator();
    }
  }
}
