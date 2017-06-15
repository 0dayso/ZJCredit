// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.DictionaryToMapAdapter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  internal sealed class DictionaryToMapAdapter
  {
    private DictionaryToMapAdapter()
    {
    }

    [SecurityCritical]
    internal V Lookup<K, V>(K key)
    {
      V v;
      if (!JitHelpers.UnsafeCast<IDictionary<K, V>>((object) this).TryGetValue(key, out v))
      {
        KeyNotFoundException notFoundException = new KeyNotFoundException(Environment.GetResourceString("Arg_KeyNotFound"));
        int hr = -2147483637;
        notFoundException.SetErrorCode(hr);
        throw notFoundException;
      }
      return v;
    }

    [SecurityCritical]
    internal uint Size<K, V>()
    {
      return (uint) JitHelpers.UnsafeCast<IDictionary<K, V>>((object) this).Count;
    }

    [SecurityCritical]
    internal bool HasKey<K, V>(K key)
    {
      return JitHelpers.UnsafeCast<IDictionary<K, V>>((object) this).ContainsKey(key);
    }

    [SecurityCritical]
    internal IReadOnlyDictionary<K, V> GetView<K, V>()
    {
      IDictionary<K, V> dictionary = JitHelpers.UnsafeCast<IDictionary<K, V>>((object) this);
      return dictionary as IReadOnlyDictionary<K, V> ?? (IReadOnlyDictionary<K, V>) new ReadOnlyDictionary<K, V>(dictionary);
    }

    [SecurityCritical]
    internal bool Insert<K, V>(K key, V value)
    {
      IDictionary<K, V> dictionary = JitHelpers.UnsafeCast<IDictionary<K, V>>((object) this);
      K key1 = key;
      bool flag = dictionary.ContainsKey(key1);
      K index = key;
      V v = value;
      dictionary[index] = v;
      return flag;
    }

    [SecurityCritical]
    internal void Remove<K, V>(K key)
    {
      if (!JitHelpers.UnsafeCast<IDictionary<K, V>>((object) this).Remove(key))
      {
        KeyNotFoundException notFoundException = new KeyNotFoundException(Environment.GetResourceString("Arg_KeyNotFound"));
        int hr = -2147483637;
        notFoundException.SetErrorCode(hr);
        throw notFoundException;
      }
    }

    [SecurityCritical]
    internal void Clear<K, V>()
    {
      JitHelpers.UnsafeCast<IDictionary<K, V>>((object) this).Clear();
    }
  }
}
