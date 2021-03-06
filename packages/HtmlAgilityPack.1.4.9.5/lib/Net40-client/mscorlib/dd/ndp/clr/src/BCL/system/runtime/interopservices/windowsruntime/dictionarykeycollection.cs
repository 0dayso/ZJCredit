﻿// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.DictionaryKeyCollection`2
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  [DebuggerDisplay("Count = {Count}")]
  [Serializable]
  internal sealed class DictionaryKeyCollection<TKey, TValue> : ICollection<TKey>, IEnumerable<TKey>, IEnumerable
  {
    private readonly IDictionary<TKey, TValue> dictionary;

    public int Count
    {
      get
      {
        return this.dictionary.Count;
      }
    }

    bool ICollection<TKey>.IsReadOnly
    {
      get
      {
        return true;
      }
    }

    public DictionaryKeyCollection(IDictionary<TKey, TValue> dictionary)
    {
      if (dictionary == null)
        throw new ArgumentNullException("dictionary");
      this.dictionary = dictionary;
    }

    public void CopyTo(TKey[] array, int index)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      if (index < 0)
        throw new ArgumentOutOfRangeException("index");
      if (array.Length <= index && this.Count > 0)
        throw new ArgumentException(Environment.GetResourceString("Arg_IndexOutOfRangeException"));
      if (array.Length - index < this.dictionary.Count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InsufficientSpaceToCopyCollection"));
      int num = index;
      foreach (KeyValuePair<TKey, TValue> keyValuePair in (IEnumerable<KeyValuePair<TKey, TValue>>) this.dictionary)
        array[num++] = keyValuePair.Key;
    }

    void ICollection<TKey>.Add(TKey item)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_KeyCollectionSet"));
    }

    void ICollection<TKey>.Clear()
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_KeyCollectionSet"));
    }

    public bool Contains(TKey item)
    {
      return this.dictionary.ContainsKey(item);
    }

    bool ICollection<TKey>.Remove(TKey item)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_KeyCollectionSet"));
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    public IEnumerator<TKey> GetEnumerator()
    {
      return (IEnumerator<TKey>) new DictionaryKeyEnumerator<TKey, TValue>(this.dictionary);
    }
  }
}
