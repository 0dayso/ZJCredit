// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.Mscorlib_DictionaryDebugView`2
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;

namespace System.Collections.Generic
{
  internal sealed class Mscorlib_DictionaryDebugView<K, V>
  {
    private IDictionary<K, V> dict;

    [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
    public KeyValuePair<K, V>[] Items
    {
      get
      {
        KeyValuePair<K, V>[] array = new KeyValuePair<K, V>[this.dict.Count];
        this.dict.CopyTo(array, 0);
        return array;
      }
    }

    public Mscorlib_DictionaryDebugView(IDictionary<K, V> dictionary)
    {
      if (dictionary == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.dictionary);
      this.dict = dictionary;
    }
  }
}
