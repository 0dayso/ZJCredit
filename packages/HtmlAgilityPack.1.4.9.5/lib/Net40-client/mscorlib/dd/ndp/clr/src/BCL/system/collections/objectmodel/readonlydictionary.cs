// Decompiled with JetBrains decompiler
// Type: System.Collections.ObjectModel.ReadOnlyDictionary`2
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace System.Collections.ObjectModel
{
  /// <summary>表示键/值对的只读泛型集合。</summary>
  /// <typeparam name="TKey">字典中键的类型。</typeparam>
  /// <typeparam name="TValue">字典中值的类型。</typeparam>
  [DebuggerTypeProxy(typeof (Mscorlib_DictionaryDebugView<,>))]
  [DebuggerDisplay("Count = {Count}")]
  [__DynamicallyInvokable]
  [Serializable]
  public class ReadOnlyDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary, ICollection, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>
  {
    private readonly IDictionary<TKey, TValue> m_dictionary;
    [NonSerialized]
    private object m_syncRoot;
    [NonSerialized]
    private ReadOnlyDictionary<TKey, TValue>.KeyCollection m_keys;
    [NonSerialized]
    private ReadOnlyDictionary<TKey, TValue>.ValueCollection m_values;

    /// <summary>获取由  <see cref="T:System.Collections.ObjectModel.ReadOnlyDictionary`2" /> 对象包装的字典。</summary>
    /// <returns>由此对象包装的字典。</returns>
    [__DynamicallyInvokable]
    protected IDictionary<TKey, TValue> Dictionary
    {
      [__DynamicallyInvokable] get
      {
        return this.m_dictionary;
      }
    }

    /// <summary>获取包含字典中的键的键集合。</summary>
    /// <returns>包含字典中的键的键集合。</returns>
    [__DynamicallyInvokable]
    public ReadOnlyDictionary<TKey, TValue>.KeyCollection Keys
    {
      [__DynamicallyInvokable] get
      {
        if (this.m_keys == null)
          this.m_keys = new ReadOnlyDictionary<TKey, TValue>.KeyCollection(this.m_dictionary.Keys);
        return this.m_keys;
      }
    }

    /// <summary>获取包含词典中的值的集合。</summary>
    /// <returns>一个集合，其中包含实现 <see cref="T:System.Collections.ObjectModel.ReadOnlyDictionary`2" /> 的对象的值。</returns>
    [__DynamicallyInvokable]
    public ReadOnlyDictionary<TKey, TValue>.ValueCollection Values
    {
      [__DynamicallyInvokable] get
      {
        if (this.m_values == null)
          this.m_values = new ReadOnlyDictionary<TKey, TValue>.ValueCollection(this.m_dictionary.Values);
        return this.m_values;
      }
    }

    [__DynamicallyInvokable]
    ICollection<TKey> IDictionary<TKey, TValue>.Keys
    {
      [__DynamicallyInvokable] get
      {
        return (ICollection<TKey>) this.Keys;
      }
    }

    [__DynamicallyInvokable]
    ICollection<TValue> IDictionary<TKey, TValue>.Values
    {
      [__DynamicallyInvokable] get
      {
        return (ICollection<TValue>) this.Values;
      }
    }

    /// <summary>获取具有指定键的元素。</summary>
    /// <returns>具有指定键的元素。</returns>
    /// <param name="key">要获取的元素的键。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> 为 null。</exception>
    /// <exception cref="T:System.Collections.Generic.KeyNotFoundException">检索了属性但没有找到 <paramref name="key" />。</exception>
    [__DynamicallyInvokable]
    public TValue this[TKey key]
    {
      [__DynamicallyInvokable] get
      {
        return this.m_dictionary[key];
      }
    }

    [__DynamicallyInvokable]
    TValue IDictionary<TKey, TValue>.this[TKey key]
    {
      [__DynamicallyInvokable] get
      {
        return this.m_dictionary[key];
      }
      [__DynamicallyInvokable] set
      {
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
      }
    }

    /// <summary>获取字典中项的数目。</summary>
    /// <returns>字典中的项数。</returns>
    [__DynamicallyInvokable]
    public int Count
    {
      [__DynamicallyInvokable] get
      {
        return this.m_dictionary.Count;
      }
    }

    [__DynamicallyInvokable]
    bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
    {
      [__DynamicallyInvokable] get
      {
        return true;
      }
    }

    [__DynamicallyInvokable]
    bool IDictionary.IsFixedSize
    {
      [__DynamicallyInvokable] get
      {
        return true;
      }
    }

    [__DynamicallyInvokable]
    bool IDictionary.IsReadOnly
    {
      [__DynamicallyInvokable] get
      {
        return true;
      }
    }

    [__DynamicallyInvokable]
    ICollection IDictionary.Keys
    {
      [__DynamicallyInvokable] get
      {
        return (ICollection) this.Keys;
      }
    }

    [__DynamicallyInvokable]
    ICollection IDictionary.Values
    {
      [__DynamicallyInvokable] get
      {
        return (ICollection) this.Values;
      }
    }

    [__DynamicallyInvokable]
    object IDictionary.this[object key]
    {
      [__DynamicallyInvokable] get
      {
        if (ReadOnlyDictionary<TKey, TValue>.IsCompatibleKey(key))
          return (object) this[(TKey) key];
        return (object) null;
      }
      [__DynamicallyInvokable] set
      {
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
      }
    }

    [__DynamicallyInvokable]
    bool ICollection.IsSynchronized
    {
      [__DynamicallyInvokable] get
      {
        return false;
      }
    }

    [__DynamicallyInvokable]
    object ICollection.SyncRoot
    {
      [__DynamicallyInvokable] get
      {
        if (this.m_syncRoot == null)
        {
          ICollection collection = this.m_dictionary as ICollection;
          if (collection != null)
            this.m_syncRoot = collection.SyncRoot;
          else
            Interlocked.CompareExchange<object>(ref this.m_syncRoot, new object(), (object) null);
        }
        return this.m_syncRoot;
      }
    }

    [__DynamicallyInvokable]
    IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
    {
      [__DynamicallyInvokable] get
      {
        return (IEnumerable<TKey>) this.Keys;
      }
    }

    [__DynamicallyInvokable]
    IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
    {
      [__DynamicallyInvokable] get
      {
        return (IEnumerable<TValue>) this.Values;
      }
    }

    /// <summary>初始化 <see cref="T:System.Collections.ObjectModel.ReadOnlyDictionary`2" /> 类的新实例，该实例是指定字典周围的包装。</summary>
    /// <param name="dictionary">要包装的字典。</param>
    [__DynamicallyInvokable]
    public ReadOnlyDictionary(IDictionary<TKey, TValue> dictionary)
    {
      if (dictionary == null)
        throw new ArgumentNullException("dictionary");
      this.m_dictionary = dictionary;
    }

    /// <summary>确定字典是否包含具有指定键的元素。</summary>
    /// <returns>如果词典包含具有指定键的元素，则为 true；否则为 false。</returns>
    /// <param name="key">要在字典中定位的键。</param>
    [__DynamicallyInvokable]
    public bool ContainsKey(TKey key)
    {
      return this.m_dictionary.ContainsKey(key);
    }

    /// <summary>检索与指定键关联的值。</summary>
    /// <returns>如果实现 <see cref="T:System.Collections.ObjectModel.ReadOnlyDictionary`2" /> 的对象包含具有指定键的元素，则为 true；否则，为 false。</returns>
    /// <param name="key">将检索其值的键。</param>
    /// <param name="value">当此方法返回时，如果找到指定键，则返回与该键相关联的值；否则，将返回 <paramref name="value" /> 参数的类型的默认值。该参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    public bool TryGetValue(TKey key, out TValue value)
    {
      return this.m_dictionary.TryGetValue(key, out value);
    }

    [__DynamicallyInvokable]
    void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
    {
      ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
    }

    [__DynamicallyInvokable]
    bool IDictionary<TKey, TValue>.Remove(TKey key)
    {
      ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
      return false;
    }

    [__DynamicallyInvokable]
    bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
    {
      return this.m_dictionary.Contains(item);
    }

    [__DynamicallyInvokable]
    void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
      this.m_dictionary.CopyTo(array, arrayIndex);
    }

    [__DynamicallyInvokable]
    void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
    {
      ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
    }

    [__DynamicallyInvokable]
    void ICollection<KeyValuePair<TKey, TValue>>.Clear()
    {
      ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
    }

    [__DynamicallyInvokable]
    bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
    {
      ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
      return false;
    }

    /// <summary>返回循环访问 <see cref="T:System.Collections.ObjectModel.ReadOnlyDictionary`2" /> 的枚举数。</summary>
    /// <returns>一个可用于循环访问集合的枚举器。</returns>
    [__DynamicallyInvokable]
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
      return this.m_dictionary.GetEnumerator();
    }

    [__DynamicallyInvokable]
    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.m_dictionary.GetEnumerator();
    }

    private static bool IsCompatibleKey(object key)
    {
      if (key == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
      return key is TKey;
    }

    [__DynamicallyInvokable]
    void IDictionary.Add(object key, object value)
    {
      ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
    }

    [__DynamicallyInvokable]
    void IDictionary.Clear()
    {
      ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
    }

    [__DynamicallyInvokable]
    bool IDictionary.Contains(object key)
    {
      if (ReadOnlyDictionary<TKey, TValue>.IsCompatibleKey(key))
        return this.ContainsKey((TKey) key);
      return false;
    }

    [__DynamicallyInvokable]
    IDictionaryEnumerator IDictionary.GetEnumerator()
    {
      IDictionary dictionary = this.m_dictionary as IDictionary;
      if (dictionary != null)
        return dictionary.GetEnumerator();
      return (IDictionaryEnumerator) new ReadOnlyDictionary<TKey, TValue>.DictionaryEnumerator(this.m_dictionary);
    }

    [__DynamicallyInvokable]
    void IDictionary.Remove(object key)
    {
      ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
    }

    [__DynamicallyInvokable]
    void ICollection.CopyTo(Array array, int index)
    {
      if (array == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
      if (array.Rank != 1)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
      if (array.GetLowerBound(0) != 0)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_NonZeroLowerBound);
      if (index < 0 || index > array.Length)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
      if (array.Length - index < this.Count)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
      KeyValuePair<TKey, TValue>[] array1 = array as KeyValuePair<TKey, TValue>[];
      if (array1 != null)
      {
        this.m_dictionary.CopyTo(array1, index);
      }
      else
      {
        DictionaryEntry[] dictionaryEntryArray = array as DictionaryEntry[];
        if (dictionaryEntryArray != null)
        {
          foreach (KeyValuePair<TKey, TValue> m in (IEnumerable<KeyValuePair<TKey, TValue>>) this.m_dictionary)
            dictionaryEntryArray[index++] = new DictionaryEntry((object) m.Key, (object) m.Value);
        }
        else
        {
          object[] objArray = array as object[];
          if (objArray == null)
            ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
          try
          {
            foreach (KeyValuePair<TKey, TValue> m in (IEnumerable<KeyValuePair<TKey, TValue>>) this.m_dictionary)
              objArray[index++] = (object) new KeyValuePair<TKey, TValue>(m.Key, m.Value);
          }
          catch (ArrayTypeMismatchException ex)
          {
            ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
          }
        }
      }
    }

    [Serializable]
    private struct DictionaryEnumerator : IDictionaryEnumerator, IEnumerator
    {
      private readonly IDictionary<TKey, TValue> m_dictionary;
      private IEnumerator<KeyValuePair<TKey, TValue>> m_enumerator;

      public DictionaryEntry Entry
      {
        get
        {
          KeyValuePair<TKey, TValue> current = this.m_enumerator.Current;
          __Boxed<TKey> local1 = (object) current.Key;
          current = this.m_enumerator.Current;
          __Boxed<TValue> local2 = (object) current.Value;
          return new DictionaryEntry((object) local1, (object) local2);
        }
      }

      public object Key
      {
        get
        {
          return (object) this.m_enumerator.Current.Key;
        }
      }

      public object Value
      {
        get
        {
          return (object) this.m_enumerator.Current.Value;
        }
      }

      public object Current
      {
        get
        {
          return (object) this.Entry;
        }
      }

      public DictionaryEnumerator(IDictionary<TKey, TValue> dictionary)
      {
        this.m_dictionary = dictionary;
        this.m_enumerator = this.m_dictionary.GetEnumerator();
      }

      public bool MoveNext()
      {
        return this.m_enumerator.MoveNext();
      }

      public void Reset()
      {
        this.m_enumerator.Reset();
      }
    }

    /// <summary>表示 <see cref="T:System.Collections.ObjectModel.ReadOnlyDictionary`2" /> 对象的键的只读集合。</summary>
    [DebuggerTypeProxy(typeof (Mscorlib_CollectionDebugView<>))]
    [DebuggerDisplay("Count = {Count}")]
    [__DynamicallyInvokable]
    [Serializable]
    public sealed class KeyCollection : ICollection<TKey>, IEnumerable<TKey>, IEnumerable, ICollection, IReadOnlyCollection<TKey>
    {
      private readonly ICollection<TKey> m_collection;
      [NonSerialized]
      private object m_syncRoot;

      /// <summary>获取集合中的元素数。</summary>
      /// <returns>集合中的元素数。</returns>
      [__DynamicallyInvokable]
      public int Count
      {
        [__DynamicallyInvokable] get
        {
          return this.m_collection.Count;
        }
      }

      [__DynamicallyInvokable]
      bool ICollection<TKey>.IsReadOnly
      {
        [__DynamicallyInvokable] get
        {
          return true;
        }
      }

      [__DynamicallyInvokable]
      bool ICollection.IsSynchronized
      {
        [__DynamicallyInvokable] get
        {
          return false;
        }
      }

      [__DynamicallyInvokable]
      object ICollection.SyncRoot
      {
        [__DynamicallyInvokable] get
        {
          if (this.m_syncRoot == null)
          {
            ICollection collection = this.m_collection as ICollection;
            if (collection != null)
              this.m_syncRoot = collection.SyncRoot;
            else
              Interlocked.CompareExchange<object>(ref this.m_syncRoot, new object(), (object) null);
          }
          return this.m_syncRoot;
        }
      }

      internal KeyCollection(ICollection<TKey> collection)
      {
        if (collection == null)
          ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
        this.m_collection = collection;
      }

      [__DynamicallyInvokable]
      void ICollection<TKey>.Add(TKey item)
      {
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
      }

      [__DynamicallyInvokable]
      void ICollection<TKey>.Clear()
      {
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
      }

      [__DynamicallyInvokable]
      bool ICollection<TKey>.Contains(TKey item)
      {
        return this.m_collection.Contains(item);
      }

      /// <summary>从特定的数组索引开始，将集合的元素复制到一个数组中。</summary>
      /// <param name="array">作为集合中元素的复制目标位置的一维数组。该数组的索引必须从零开始。</param>
      /// <param name="arrayIndex">
      /// <paramref name="array" /> 中从零开始的索引，从此索引处开始进行复制。</param>
      /// <exception cref="T:System.ArgumentNullException">
      /// <paramref name="array" /> 为 null。</exception>
      /// <exception cref="T:System.ArgumentOutOfRangeException">
      /// <paramref name="arrayIndex" /> 小于 0。</exception>
      /// <exception cref="T:System.ArgumentException">
      /// <paramref name="array" /> 是多维的。- 或 -源集合中的元素数大于从 <paramref name="arrayIndex" /> 到目标 <paramref name="array" /> 的末尾的可用空间。- 或 -无法自动将类型 <paramref name="T" /> 强制转换为目标 <paramref name="array" /> 的类型。</exception>
      [__DynamicallyInvokable]
      public void CopyTo(TKey[] array, int arrayIndex)
      {
        this.m_collection.CopyTo(array, arrayIndex);
      }

      [__DynamicallyInvokable]
      bool ICollection<TKey>.Remove(TKey item)
      {
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
        return false;
      }

      /// <summary>返回一个循环访问集合的枚举器。</summary>
      /// <returns>一个可用于循环访问集合的枚举器。</returns>
      [__DynamicallyInvokable]
      public IEnumerator<TKey> GetEnumerator()
      {
        return this.m_collection.GetEnumerator();
      }

      [__DynamicallyInvokable]
      IEnumerator IEnumerable.GetEnumerator()
      {
        return this.m_collection.GetEnumerator();
      }

      [__DynamicallyInvokable]
      void ICollection.CopyTo(Array array, int index)
      {
        ReadOnlyDictionaryHelpers.CopyToNonGenericICollectionHelper<TKey>(this.m_collection, array, index);
      }
    }

    /// <summary>表示 <see cref="T:System.Collections.ObjectModel.ReadOnlyDictionary`2" /> 对象的值的只读集合。</summary>
    [DebuggerTypeProxy(typeof (Mscorlib_CollectionDebugView<>))]
    [DebuggerDisplay("Count = {Count}")]
    [__DynamicallyInvokable]
    [Serializable]
    public sealed class ValueCollection : ICollection<TValue>, IEnumerable<TValue>, IEnumerable, ICollection, IReadOnlyCollection<TValue>
    {
      private readonly ICollection<TValue> m_collection;
      [NonSerialized]
      private object m_syncRoot;

      /// <summary>获取集合中的元素数。</summary>
      /// <returns>集合中的元素数。</returns>
      [__DynamicallyInvokable]
      public int Count
      {
        [__DynamicallyInvokable] get
        {
          return this.m_collection.Count;
        }
      }

      [__DynamicallyInvokable]
      bool ICollection<TValue>.IsReadOnly
      {
        [__DynamicallyInvokable] get
        {
          return true;
        }
      }

      [__DynamicallyInvokable]
      bool ICollection.IsSynchronized
      {
        [__DynamicallyInvokable] get
        {
          return false;
        }
      }

      [__DynamicallyInvokable]
      object ICollection.SyncRoot
      {
        [__DynamicallyInvokable] get
        {
          if (this.m_syncRoot == null)
          {
            ICollection collection = this.m_collection as ICollection;
            if (collection != null)
              this.m_syncRoot = collection.SyncRoot;
            else
              Interlocked.CompareExchange<object>(ref this.m_syncRoot, new object(), (object) null);
          }
          return this.m_syncRoot;
        }
      }

      internal ValueCollection(ICollection<TValue> collection)
      {
        if (collection == null)
          ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
        this.m_collection = collection;
      }

      [__DynamicallyInvokable]
      void ICollection<TValue>.Add(TValue item)
      {
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
      }

      [__DynamicallyInvokable]
      void ICollection<TValue>.Clear()
      {
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
      }

      [__DynamicallyInvokable]
      bool ICollection<TValue>.Contains(TValue item)
      {
        return this.m_collection.Contains(item);
      }

      /// <summary>从特定的数组索引开始，将集合的元素复制到一个数组中。</summary>
      /// <param name="array">作为集合中元素的复制目标位置的一维数组。该数组的索引必须从零开始。</param>
      /// <param name="arrayIndex">
      /// <paramref name="array" /> 中从零开始的索引，从此索引处开始进行复制。</param>
      /// <exception cref="T:System.ArgumentNullException">
      /// <paramref name="array" /> 为 null。</exception>
      /// <exception cref="T:System.ArgumentOutOfRangeException">
      /// <paramref name="arrayIndex" /> 小于 0。</exception>
      /// <exception cref="T:System.ArgumentException">
      /// <paramref name="array" /> 是多维的。- 或 -源集合中的元素数大于从 <paramref name="arrayIndex" /> 到目标 <paramref name="array" /> 的末尾的可用空间。- 或 -无法自动将类型 <paramref name="T" /> 强制转换为目标 <paramref name="array" /> 的类型。</exception>
      [__DynamicallyInvokable]
      public void CopyTo(TValue[] array, int arrayIndex)
      {
        this.m_collection.CopyTo(array, arrayIndex);
      }

      [__DynamicallyInvokable]
      bool ICollection<TValue>.Remove(TValue item)
      {
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
        return false;
      }

      /// <summary>返回一个循环访问集合的枚举器。</summary>
      /// <returns>一个可用于循环访问集合的枚举器。</returns>
      [__DynamicallyInvokable]
      public IEnumerator<TValue> GetEnumerator()
      {
        return this.m_collection.GetEnumerator();
      }

      [__DynamicallyInvokable]
      IEnumerator IEnumerable.GetEnumerator()
      {
        return this.m_collection.GetEnumerator();
      }

      [__DynamicallyInvokable]
      void ICollection.CopyTo(Array array, int index)
      {
        ReadOnlyDictionaryHelpers.CopyToNonGenericICollectionHelper<TValue>(this.m_collection, array, index);
      }
    }
  }
}
