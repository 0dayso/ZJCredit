// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.Dictionary`2
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System.Collections.Generic
{
  /// <summary>表示键和值的集合。若要浏览此类型的.NET Framework 源代码，请参阅 Reference Source。</summary>
  /// <typeparam name="TKey">字典中的键的类型。</typeparam>
  /// <typeparam name="TValue">字典中的值的类型。</typeparam>
  /// <filterpriority>1</filterpriority>
  [DebuggerTypeProxy(typeof (Mscorlib_DictionaryDebugView<,>))]
  [DebuggerDisplay("Count = {Count}")]
  [ComVisible(false)]
  [__DynamicallyInvokable]
  [Serializable]
  public class Dictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary, ICollection, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, ISerializable, IDeserializationCallback
  {
    private int[] buckets;
    private Dictionary<TKey, TValue>.Entry[] entries;
    private int count;
    private int version;
    private int freeList;
    private int freeCount;
    private IEqualityComparer<TKey> comparer;
    private Dictionary<TKey, TValue>.KeyCollection keys;
    private Dictionary<TKey, TValue>.ValueCollection values;
    private object _syncRoot;
    private const string VersionName = "Version";
    private const string HashSizeName = "HashSize";
    private const string KeyValuePairsName = "KeyValuePairs";
    private const string ComparerName = "Comparer";

    /// <summary>获取用于确定字典中的键是否相等的 <see cref="T:System.Collections.Generic.IEqualityComparer`1" />。</summary>
    /// <returns>
    /// <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> 泛型接口实现，它用于确定当前 <see cref="T:System.Collections.Generic.Dictionary`2" /> 中的键是否相等并为键提供哈希值。</returns>
    [__DynamicallyInvokable]
    public IEqualityComparer<TKey> Comparer
    {
      [__DynamicallyInvokable] get
      {
        return this.comparer;
      }
    }

    /// <summary>获取包含在 <see cref="T:System.Collections.Generic.Dictionary`2" /> 中的键/值对的数目。</summary>
    /// <returns>包含在 <see cref="T:System.Collections.Generic.Dictionary`2" /> 中的键/值对的数目。</returns>
    [__DynamicallyInvokable]
    public int Count
    {
      [__DynamicallyInvokable] get
      {
        return this.count - this.freeCount;
      }
    }

    /// <summary>获得一个包含 <see cref="T:System.Collections.Generic.Dictionary`2" /> 中的键的集合。</summary>
    /// <returns>一个 <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />，包含 <see cref="T:System.Collections.Generic.Dictionary`2" /> 中的键。</returns>
    [__DynamicallyInvokable]
    public Dictionary<TKey, TValue>.KeyCollection Keys
    {
      [__DynamicallyInvokable] get
      {
        if (this.keys == null)
          this.keys = new Dictionary<TKey, TValue>.KeyCollection(this);
        return this.keys;
      }
    }

    [__DynamicallyInvokable]
    ICollection<TKey> IDictionary<TKey, TValue>.Keys
    {
      [__DynamicallyInvokable] get
      {
        if (this.keys == null)
          this.keys = new Dictionary<TKey, TValue>.KeyCollection(this);
        return (ICollection<TKey>) this.keys;
      }
    }

    [__DynamicallyInvokable]
    IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
    {
      [__DynamicallyInvokable] get
      {
        if (this.keys == null)
          this.keys = new Dictionary<TKey, TValue>.KeyCollection(this);
        return (IEnumerable<TKey>) this.keys;
      }
    }

    /// <summary>获得一个包含 <see cref="T:System.Collections.Generic.Dictionary`2" /> 中的值的集合。</summary>
    /// <returns>一个 <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" />，包含 <see cref="T:System.Collections.Generic.Dictionary`2" /> 中的值。</returns>
    [__DynamicallyInvokable]
    public Dictionary<TKey, TValue>.ValueCollection Values
    {
      [__DynamicallyInvokable] get
      {
        if (this.values == null)
          this.values = new Dictionary<TKey, TValue>.ValueCollection(this);
        return this.values;
      }
    }

    [__DynamicallyInvokable]
    ICollection<TValue> IDictionary<TKey, TValue>.Values
    {
      [__DynamicallyInvokable] get
      {
        if (this.values == null)
          this.values = new Dictionary<TKey, TValue>.ValueCollection(this);
        return (ICollection<TValue>) this.values;
      }
    }

    [__DynamicallyInvokable]
    IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
    {
      [__DynamicallyInvokable] get
      {
        if (this.values == null)
          this.values = new Dictionary<TKey, TValue>.ValueCollection(this);
        return (IEnumerable<TValue>) this.values;
      }
    }

    /// <summary>获取或设置与指定的键关联的值。</summary>
    /// <returns>与指定的键相关联的值。如果指定键未找到，则 Get 操作引发 <see cref="T:System.Collections.Generic.KeyNotFoundException" />，而 Set 操作创建一个带指定键的新元素。</returns>
    /// <param name="key">要获取或设置的值的键。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> 为 null。</exception>
    /// <exception cref="T:System.Collections.Generic.KeyNotFoundException">已检索该属性，并且集合中不存在 <paramref name="key" />。</exception>
    [__DynamicallyInvokable]
    public TValue this[TKey key]
    {
      [__DynamicallyInvokable] get
      {
        int entry = this.FindEntry(key);
        if (entry >= 0)
          return this.entries[entry].value;
        ThrowHelper.ThrowKeyNotFoundException();
        return default (TValue);
      }
      [__DynamicallyInvokable] set
      {
        this.Insert(key, value, false);
      }
    }

    [__DynamicallyInvokable]
    bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
    {
      [__DynamicallyInvokable] get
      {
        return false;
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
        if (this._syncRoot == null)
          Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), (object) null);
        return this._syncRoot;
      }
    }

    [__DynamicallyInvokable]
    bool IDictionary.IsFixedSize
    {
      [__DynamicallyInvokable] get
      {
        return false;
      }
    }

    [__DynamicallyInvokable]
    bool IDictionary.IsReadOnly
    {
      [__DynamicallyInvokable] get
      {
        return false;
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
        if (Dictionary<TKey, TValue>.IsCompatibleKey(key))
        {
          int entry = this.FindEntry((TKey) key);
          if (entry >= 0)
            return (object) this.entries[entry].value;
        }
        return (object) null;
      }
      [__DynamicallyInvokable] set
      {
        if (key == null)
          ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
        ThrowHelper.IfNullAndNullsAreIllegalThenThrow<TValue>(value, ExceptionArgument.value);
        try
        {
          TKey index = (TKey) key;
          try
          {
            this[index] = (TValue) value;
          }
          catch (InvalidCastException ex)
          {
            ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof (TValue));
          }
        }
        catch (InvalidCastException ex)
        {
          ThrowHelper.ThrowWrongKeyTypeArgumentException(key, typeof (TKey));
        }
      }
    }

    /// <summary>初始化 <see cref="T:System.Collections.Generic.Dictionary`2" /> 类的新实例，该实例为空，具有默认的初始容量并为键类型使用默认的相等比较器。</summary>
    [__DynamicallyInvokable]
    public Dictionary()
      : this(0, (IEqualityComparer<TKey>) null)
    {
    }

    /// <summary>初始化 <see cref="T:System.Collections.Generic.Dictionary`2" /> 类的新实例，该实例为空，具有指定的初始容量并为键类型使用默认的相等比较器。</summary>
    /// <param name="capacity">
    /// <see cref="T:System.Collections.Generic.Dictionary`2" /> 可包含的初始元素数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="capacity" /> 小于 0。</exception>
    [__DynamicallyInvokable]
    public Dictionary(int capacity)
      : this(capacity, (IEqualityComparer<TKey>) null)
    {
    }

    /// <summary>初始化 <see cref="T:System.Collections.Generic.Dictionary`2" /> 类的新实例，该实例为空，具有默认的初始容量并使用指定的 <see cref="T:System.Collections.Generic.IEqualityComparer`1" />。</summary>
    /// <param name="comparer">比较键时要使用的 <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> 实现，或者为 null，以便为键类型使用默认的 <see cref="T:System.Collections.Generic.EqualityComparer`1" />。</param>
    [__DynamicallyInvokable]
    public Dictionary(IEqualityComparer<TKey> comparer)
      : this(0, comparer)
    {
    }

    /// <summary>初始化 <see cref="T:System.Collections.Generic.Dictionary`2" /> 类的新实例，该实例为空，具有指定的初始容量并使用指定的 <see cref="T:System.Collections.Generic.IEqualityComparer`1" />。</summary>
    /// <param name="capacity">
    /// <see cref="T:System.Collections.Generic.Dictionary`2" /> 可包含的初始元素数。</param>
    /// <param name="comparer">比较键时要使用的 <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> 实现，或者为 null，以便为键类型使用默认的 <see cref="T:System.Collections.Generic.EqualityComparer`1" />。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="capacity" /> 小于 0。</exception>
    [__DynamicallyInvokable]
    public Dictionary(int capacity, IEqualityComparer<TKey> comparer)
    {
      if (capacity < 0)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.capacity);
      if (capacity > 0)
        this.Initialize(capacity);
      this.comparer = comparer ?? (IEqualityComparer<TKey>) EqualityComparer<TKey>.Default;
    }

    /// <summary>初始化 <see cref="T:System.Collections.Generic.Dictionary`2" /> 类的新实例，该实例包含从指定的 <see cref="T:System.Collections.Generic.IDictionary`2" /> 复制的元素并为键类型使用默认的相等比较器。</summary>
    /// <param name="dictionary">
    /// <see cref="T:System.Collections.Generic.IDictionary`2" />，它的元素被复制到新 <see cref="T:System.Collections.Generic.Dictionary`2" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="dictionary" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="dictionary" /> 包含一个或多个重复键。</exception>
    [__DynamicallyInvokable]
    public Dictionary(IDictionary<TKey, TValue> dictionary)
      : this(dictionary, (IEqualityComparer<TKey>) null)
    {
    }

    /// <summary>初始化 <see cref="T:System.Collections.Generic.Dictionary`2" /> 类的新实例，该实例包含从指定的 <see cref="T:System.Collections.Generic.IDictionary`2" /> 中复制的元素并使用指定的 <see cref="T:System.Collections.Generic.IEqualityComparer`1" />。</summary>
    /// <param name="dictionary">
    /// <see cref="T:System.Collections.Generic.IDictionary`2" />，它的元素被复制到新 <see cref="T:System.Collections.Generic.Dictionary`2" />。</param>
    /// <param name="comparer">比较键时要使用的 <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> 实现，或者为 null，以便为键类型使用默认的 <see cref="T:System.Collections.Generic.EqualityComparer`1" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="dictionary" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="dictionary" /> 包含一个或多个重复键。</exception>
    [__DynamicallyInvokable]
    public Dictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
      : this(dictionary != null ? dictionary.Count : 0, comparer)
    {
      if (dictionary == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.dictionary);
      foreach (KeyValuePair<TKey, TValue> keyValuePair in (IEnumerable<KeyValuePair<TKey, TValue>>) dictionary)
        this.Add(keyValuePair.Key, keyValuePair.Value);
    }

    /// <summary>用序列化数据初始化 <see cref="T:System.Collections.Generic.Dictionary`2" /> 类的新实例。</summary>
    /// <param name="info">一个 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 对象包含序列化 <see cref="T:System.Collections.Generic.Dictionary`2" /> 所需的信息。</param>
    /// <param name="context">一个 <see cref="T:System.Runtime.Serialization.StreamingContext" /> 结构包含与 <see cref="T:System.Collections.Generic.Dictionary`2" /> 关联的序列化流的源和目标。</param>
    protected Dictionary(SerializationInfo info, StreamingContext context)
    {
      HashHelpers.SerializationInfoTable.Add((object) this, info);
    }

    /// <summary>将指定的键和值添加到字典中。</summary>
    /// <param name="key">要添加的元素的键。</param>
    /// <param name="value">要添加的元素的值。对于引用类型，该值可以为 null。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <see cref="T:System.Collections.Generic.Dictionary`2" /> 中已存在具有相同键的元素。</exception>
    [__DynamicallyInvokable]
    public void Add(TKey key, TValue value)
    {
      this.Insert(key, value, true);
    }

    [__DynamicallyInvokable]
    void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> keyValuePair)
    {
      this.Add(keyValuePair.Key, keyValuePair.Value);
    }

    [__DynamicallyInvokable]
    bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> keyValuePair)
    {
      int entry = this.FindEntry(keyValuePair.Key);
      return entry >= 0 && EqualityComparer<TValue>.Default.Equals(this.entries[entry].value, keyValuePair.Value);
    }

    [__DynamicallyInvokable]
    bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> keyValuePair)
    {
      int entry = this.FindEntry(keyValuePair.Key);
      if (entry < 0 || !EqualityComparer<TValue>.Default.Equals(this.entries[entry].value, keyValuePair.Value))
        return false;
      this.Remove(keyValuePair.Key);
      return true;
    }

    /// <summary>将所有键和值从 <see cref="T:System.Collections.Generic.Dictionary`2" /> 中移除。</summary>
    [__DynamicallyInvokable]
    public void Clear()
    {
      if (this.count <= 0)
        return;
      for (int index = 0; index < this.buckets.Length; ++index)
        this.buckets[index] = -1;
      Array.Clear((Array) this.entries, 0, this.count);
      this.freeList = -1;
      this.count = 0;
      this.freeCount = 0;
      this.version = this.version + 1;
    }

    /// <summary>确定是否 <see cref="T:System.Collections.Generic.Dictionary`2" /> 包含指定键。</summary>
    /// <returns>如果 true 包含具有指定键的元素，则为 <see cref="T:System.Collections.Generic.Dictionary`2" />；否则为 false。</returns>
    /// <param name="key">要在 <see cref="T:System.Collections.Generic.Dictionary`2" /> 中定位的键。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public bool ContainsKey(TKey key)
    {
      return this.FindEntry(key) >= 0;
    }

    /// <summary>确定 <see cref="T:System.Collections.Generic.Dictionary`2" /> 是否包含特定值。</summary>
    /// <returns>如果 true 包含具有指定值的元素，则为 <see cref="T:System.Collections.Generic.Dictionary`2" />；否则为 false。</returns>
    /// <param name="value">要在 <see cref="T:System.Collections.Generic.Dictionary`2" /> 中定位的值。对于引用类型，该值可以为 null。</param>
    [__DynamicallyInvokable]
    public bool ContainsValue(TValue value)
    {
      if ((object) value == null)
      {
        for (int index = 0; index < this.count; ++index)
        {
          if (this.entries[index].hashCode >= 0 && (object) this.entries[index].value == null)
            return true;
        }
      }
      else
      {
        EqualityComparer<TValue> @default = EqualityComparer<TValue>.Default;
        for (int index = 0; index < this.count; ++index)
        {
          if (this.entries[index].hashCode >= 0 && @default.Equals(this.entries[index].value, value))
            return true;
        }
      }
      return false;
    }

    private void CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
    {
      if (array == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
      if (index < 0 || index > array.Length)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
      if (array.Length - index < this.Count)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
      int num = this.count;
      Dictionary<TKey, TValue>.Entry[] entryArray = this.entries;
      for (int index1 = 0; index1 < num; ++index1)
      {
        if (entryArray[index1].hashCode >= 0)
          array[index++] = new KeyValuePair<TKey, TValue>(entryArray[index1].key, entryArray[index1].value);
      }
    }

    /// <summary>返回循环访问 <see cref="T:System.Collections.Generic.Dictionary`2" /> 的枚举数。</summary>
    /// <returns>用于 <see cref="T:System.Collections.Generic.Dictionary`2" /> 的 <see cref="T:System.Collections.Generic.Dictionary`2.Enumerator" /> 结构。</returns>
    [__DynamicallyInvokable]
    public Dictionary<TKey, TValue>.Enumerator GetEnumerator()
    {
      return new Dictionary<TKey, TValue>.Enumerator(this, 2);
    }

    [__DynamicallyInvokable]
    IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
    {
      return (IEnumerator<KeyValuePair<TKey, TValue>>) new Dictionary<TKey, TValue>.Enumerator(this, 2);
    }

    /// <summary>实现 <see cref="T:System.Runtime.Serialization.ISerializable" /> 接口，并返回序列化 <see cref="T:System.Collections.Generic.Dictionary`2" /> 实例所需的数据。</summary>
    /// <param name="info">
    /// <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 对象，该对象包含序列化 <see cref="T:System.Collections.Generic.Dictionary`2" /> 实例所需的信息。</param>
    /// <param name="context">一个 <see cref="T:System.Runtime.Serialization.StreamingContext" /> 结构，它包含与 <see cref="T:System.Collections.Generic.Dictionary`2" /> 实例关联的序列化流的源和目标。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="info" /> 为 null。</exception>
    [SecurityCritical]
    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.info);
      info.AddValue("Version", this.version);
      info.AddValue("Comparer", HashHelpers.GetEqualityComparerForSerialization((object) this.comparer), typeof (IEqualityComparer<TKey>));
      info.AddValue("HashSize", this.buckets == null ? 0 : this.buckets.Length);
      if (this.buckets == null)
        return;
      KeyValuePair<TKey, TValue>[] array = new KeyValuePair<TKey, TValue>[this.Count];
      this.CopyTo(array, 0);
      info.AddValue("KeyValuePairs", (object) array, typeof (KeyValuePair<TKey, TValue>[]));
    }

    private int FindEntry(TKey key)
    {
      if ((object) key == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
      if (this.buckets != null)
      {
        int num = this.comparer.GetHashCode(key) & int.MaxValue;
        for (int index = this.buckets[num % this.buckets.Length]; index >= 0; index = this.entries[index].next)
        {
          if (this.entries[index].hashCode == num && this.comparer.Equals(this.entries[index].key, key))
            return index;
        }
      }
      return -1;
    }

    private void Initialize(int capacity)
    {
      int prime = HashHelpers.GetPrime(capacity);
      this.buckets = new int[prime];
      for (int index = 0; index < this.buckets.Length; ++index)
        this.buckets[index] = -1;
      this.entries = new Dictionary<TKey, TValue>.Entry[prime];
      this.freeList = -1;
    }

    private void Insert(TKey key, TValue value, bool add)
    {
      if ((object) key == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
      if (this.buckets == null)
        this.Initialize(0);
      int num1 = this.comparer.GetHashCode(key) & int.MaxValue;
      int index1 = num1 % this.buckets.Length;
      int num2 = 0;
      for (int index2 = this.buckets[index1]; index2 >= 0; index2 = this.entries[index2].next)
      {
        if (this.entries[index2].hashCode == num1 && this.comparer.Equals(this.entries[index2].key, key))
        {
          if (add)
            ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_AddingDuplicate);
          this.entries[index2].value = value;
          this.version = this.version + 1;
          return;
        }
        ++num2;
      }
      int index3;
      if (this.freeCount > 0)
      {
        index3 = this.freeList;
        this.freeList = this.entries[index3].next;
        this.freeCount = this.freeCount - 1;
      }
      else
      {
        if (this.count == this.entries.Length)
        {
          this.Resize();
          index1 = num1 % this.buckets.Length;
        }
        index3 = this.count;
        this.count = this.count + 1;
      }
      this.entries[index3].hashCode = num1;
      this.entries[index3].next = this.buckets[index1];
      this.entries[index3].key = key;
      this.entries[index3].value = value;
      this.buckets[index1] = index3;
      this.version = this.version + 1;
      if (num2 <= 100 || !HashHelpers.IsWellKnownEqualityComparer((object) this.comparer))
        return;
      this.comparer = (IEqualityComparer<TKey>) HashHelpers.GetRandomizedEqualityComparer((object) this.comparer);
      this.Resize(this.entries.Length, true);
    }

    /// <summary>实现 <see cref="T:System.Runtime.Serialization.ISerializable" /> 接口，并在完成反序列化之后引发反序列化事件。</summary>
    /// <param name="sender">反序列化事件源。</param>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">与当前 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 实例关联的 <see cref="T:System.Collections.Generic.Dictionary`2" /> 对象无效。</exception>
    public virtual void OnDeserialization(object sender)
    {
      SerializationInfo serializationInfo;
      HashHelpers.SerializationInfoTable.TryGetValue((object) this, out serializationInfo);
      if (serializationInfo == null)
        return;
      int int32_1 = serializationInfo.GetInt32("Version");
      int int32_2 = serializationInfo.GetInt32("HashSize");
      this.comparer = (IEqualityComparer<TKey>) serializationInfo.GetValue("Comparer", typeof (IEqualityComparer<TKey>));
      if (int32_2 != 0)
      {
        this.buckets = new int[int32_2];
        for (int index = 0; index < this.buckets.Length; ++index)
          this.buckets[index] = -1;
        this.entries = new Dictionary<TKey, TValue>.Entry[int32_2];
        this.freeList = -1;
        KeyValuePair<TKey, TValue>[] keyValuePairArray = (KeyValuePair<TKey, TValue>[]) serializationInfo.GetValue("KeyValuePairs", typeof (KeyValuePair<TKey, TValue>[]));
        if (keyValuePairArray == null)
          ThrowHelper.ThrowSerializationException(ExceptionResource.Serialization_MissingKeys);
        for (int index = 0; index < keyValuePairArray.Length; ++index)
        {
          if ((object) keyValuePairArray[index].Key == null)
            ThrowHelper.ThrowSerializationException(ExceptionResource.Serialization_NullKey);
          this.Insert(keyValuePairArray[index].Key, keyValuePairArray[index].Value, true);
        }
      }
      else
        this.buckets = (int[]) null;
      this.version = int32_1;
      HashHelpers.SerializationInfoTable.Remove((object) this);
    }

    private void Resize()
    {
      this.Resize(HashHelpers.ExpandPrime(this.count), false);
    }

    private void Resize(int newSize, bool forceNewHashCodes)
    {
      int[] numArray = new int[newSize];
      for (int index = 0; index < numArray.Length; ++index)
        numArray[index] = -1;
      Dictionary<TKey, TValue>.Entry[] entryArray = new Dictionary<TKey, TValue>.Entry[newSize];
      Array.Copy((Array) this.entries, 0, (Array) entryArray, 0, this.count);
      if (forceNewHashCodes)
      {
        for (int index = 0; index < this.count; ++index)
        {
          if (entryArray[index].hashCode != -1)
            entryArray[index].hashCode = this.comparer.GetHashCode(entryArray[index].key) & int.MaxValue;
        }
      }
      for (int index1 = 0; index1 < this.count; ++index1)
      {
        if (entryArray[index1].hashCode >= 0)
        {
          int index2 = entryArray[index1].hashCode % newSize;
          entryArray[index1].next = numArray[index2];
          numArray[index2] = index1;
        }
      }
      this.buckets = numArray;
      this.entries = entryArray;
    }

    /// <summary>将带有指定键的值从 <see cref="T:System.Collections.Generic.Dictionary`2" /> 中移除。</summary>
    /// <returns>如果成功找到并移除该元素，则为 true；否则为 false。如果在 <see cref="T:System.Collections.Generic.Dictionary`2" /> 中没有找到 <paramref name="key" />，则此方法返回 false。</returns>
    /// <param name="key">要移除的元素的键。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public bool Remove(TKey key)
    {
      if ((object) key == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
      if (this.buckets != null)
      {
        int num = this.comparer.GetHashCode(key) & int.MaxValue;
        int index1 = num % this.buckets.Length;
        int index2 = -1;
        for (int index3 = this.buckets[index1]; index3 >= 0; index3 = this.entries[index3].next)
        {
          if (this.entries[index3].hashCode == num && this.comparer.Equals(this.entries[index3].key, key))
          {
            if (index2 < 0)
              this.buckets[index1] = this.entries[index3].next;
            else
              this.entries[index2].next = this.entries[index3].next;
            this.entries[index3].hashCode = -1;
            this.entries[index3].next = this.freeList;
            this.entries[index3].key = default (TKey);
            this.entries[index3].value = default (TValue);
            this.freeList = index3;
            this.freeCount = this.freeCount + 1;
            this.version = this.version + 1;
            return true;
          }
          index2 = index3;
        }
      }
      return false;
    }

    /// <summary>获取与指定键关联的值。</summary>
    /// <returns>如果 true 包含具有指定键的元素，则为 <see cref="T:System.Collections.Generic.Dictionary`2" />；否则为 false。</returns>
    /// <param name="key">要获取的值的键。</param>
    /// <param name="value">当此方法返回时，如果找到指定键，则包含与该键相关的值；否则包含 <paramref name="value" /> 参数类型的默认值。此参数未经初始化即被传递。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public bool TryGetValue(TKey key, out TValue value)
    {
      int entry = this.FindEntry(key);
      if (entry >= 0)
      {
        value = this.entries[entry].value;
        return true;
      }
      value = default (TValue);
      return false;
    }

    internal TValue GetValueOrDefault(TKey key)
    {
      int entry = this.FindEntry(key);
      if (entry >= 0)
        return this.entries[entry].value;
      return default (TValue);
    }

    [__DynamicallyInvokable]
    void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
    {
      this.CopyTo(array, index);
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
        this.CopyTo(array1, index);
      else if (array is DictionaryEntry[])
      {
        DictionaryEntry[] dictionaryEntryArray = array as DictionaryEntry[];
        Dictionary<TKey, TValue>.Entry[] entryArray = this.entries;
        for (int index1 = 0; index1 < this.count; ++index1)
        {
          if (entryArray[index1].hashCode >= 0)
            dictionaryEntryArray[index++] = new DictionaryEntry((object) entryArray[index1].key, (object) entryArray[index1].value);
        }
      }
      else
      {
        object[] objArray = array as object[];
        if (objArray == null)
          ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
        try
        {
          int num = this.count;
          Dictionary<TKey, TValue>.Entry[] entryArray = this.entries;
          for (int index1 = 0; index1 < num; ++index1)
          {
            if (entryArray[index1].hashCode >= 0)
              objArray[index++] = (object) new KeyValuePair<TKey, TValue>(entryArray[index1].key, entryArray[index1].value);
          }
        }
        catch (ArrayTypeMismatchException ex)
        {
          ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
        }
      }
    }

    [__DynamicallyInvokable]
    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) new Dictionary<TKey, TValue>.Enumerator(this, 2);
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
      if (key == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
      ThrowHelper.IfNullAndNullsAreIllegalThenThrow<TValue>(value, ExceptionArgument.value);
      try
      {
        TKey key1 = (TKey) key;
        try
        {
          this.Add(key1, (TValue) value);
        }
        catch (InvalidCastException ex)
        {
          ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof (TValue));
        }
      }
      catch (InvalidCastException ex)
      {
        ThrowHelper.ThrowWrongKeyTypeArgumentException(key, typeof (TKey));
      }
    }

    [__DynamicallyInvokable]
    bool IDictionary.Contains(object key)
    {
      if (Dictionary<TKey, TValue>.IsCompatibleKey(key))
        return this.ContainsKey((TKey) key);
      return false;
    }

    [__DynamicallyInvokable]
    IDictionaryEnumerator IDictionary.GetEnumerator()
    {
      return (IDictionaryEnumerator) new Dictionary<TKey, TValue>.Enumerator(this, 1);
    }

    [__DynamicallyInvokable]
    void IDictionary.Remove(object key)
    {
      if (!Dictionary<TKey, TValue>.IsCompatibleKey(key))
        return;
      this.Remove((TKey) key);
    }

    private struct Entry
    {
      public int hashCode;
      public int next;
      public TKey key;
      public TValue value;
    }

    /// <summary>枚举 <see cref="T:System.Collections.Generic.Dictionary`2" /> 的元素。</summary>
    [__DynamicallyInvokable]
    [Serializable]
    public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable, IEnumerator, IDictionaryEnumerator
    {
      private Dictionary<TKey, TValue> dictionary;
      private int version;
      private int index;
      private KeyValuePair<TKey, TValue> current;
      private int getEnumeratorRetType;
      internal const int DictEntry = 1;
      internal const int KeyValuePair = 2;

      /// <summary>获取枚举数当前位置的元素。</summary>
      /// <returns>
      /// <see cref="T:System.Collections.Generic.Dictionary`2" /> 中位于枚举数当前位置的元素。</returns>
      [__DynamicallyInvokable]
      public KeyValuePair<TKey, TValue> Current
      {
        [__DynamicallyInvokable] get
        {
          return this.current;
        }
      }

      [__DynamicallyInvokable]
      object IEnumerator.Current
      {
        [__DynamicallyInvokable] get
        {
          if (this.index == 0 || this.index == this.dictionary.count + 1)
            ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
          if (this.getEnumeratorRetType == 1)
            return (object) new DictionaryEntry((object) this.current.Key, (object) this.current.Value);
          return (object) new KeyValuePair<TKey, TValue>(this.current.Key, this.current.Value);
        }
      }

      [__DynamicallyInvokable]
      DictionaryEntry IDictionaryEnumerator.Entry
      {
        [__DynamicallyInvokable] get
        {
          if (this.index == 0 || this.index == this.dictionary.count + 1)
            ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
          return new DictionaryEntry((object) this.current.Key, (object) this.current.Value);
        }
      }

      [__DynamicallyInvokable]
      object IDictionaryEnumerator.Key
      {
        [__DynamicallyInvokable] get
        {
          if (this.index == 0 || this.index == this.dictionary.count + 1)
            ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
          return (object) this.current.Key;
        }
      }

      [__DynamicallyInvokable]
      object IDictionaryEnumerator.Value
      {
        [__DynamicallyInvokable] get
        {
          if (this.index == 0 || this.index == this.dictionary.count + 1)
            ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
          return (object) this.current.Value;
        }
      }

      internal Enumerator(Dictionary<TKey, TValue> dictionary, int getEnumeratorRetType)
      {
        this.dictionary = dictionary;
        this.version = dictionary.version;
        this.index = 0;
        this.getEnumeratorRetType = getEnumeratorRetType;
        this.current = new KeyValuePair<TKey, TValue>();
      }

      /// <summary>使枚举数前进到 <see cref="T:System.Collections.Generic.Dictionary`2" /> 的下一个元素。</summary>
      /// <returns>如果枚举数成功地推进到下一个元素，则为 true；如果枚举数越过集合的结尾，则为 false。</returns>
      /// <exception cref="T:System.InvalidOperationException">在创建了枚举数后集合被修改了。</exception>
      [__DynamicallyInvokable]
      public bool MoveNext()
      {
        if (this.version != this.dictionary.version)
          ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
        for (; (uint) this.index < (uint) this.dictionary.count; this.index = this.index + 1)
        {
          if (this.dictionary.entries[this.index].hashCode >= 0)
          {
            this.current = new KeyValuePair<TKey, TValue>(this.dictionary.entries[this.index].key, this.dictionary.entries[this.index].value);
            this.index = this.index + 1;
            return true;
          }
        }
        this.index = this.dictionary.count + 1;
        this.current = new KeyValuePair<TKey, TValue>();
        return false;
      }

      /// <summary>释放由 <see cref="T:System.Collections.Generic.Dictionary`2.Enumerator" /> 使用的所有资源。</summary>
      [__DynamicallyInvokable]
      public void Dispose()
      {
      }

      [__DynamicallyInvokable]
      void IEnumerator.Reset()
      {
        if (this.version != this.dictionary.version)
          ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
        this.index = 0;
        this.current = new KeyValuePair<TKey, TValue>();
      }
    }

    /// <summary>表示 <see cref="T:System.Collections.Generic.Dictionary`2" /> 中键的集合。此类不能被继承。</summary>
    [DebuggerTypeProxy(typeof (Mscorlib_DictionaryKeyCollectionDebugView<,>))]
    [DebuggerDisplay("Count = {Count}")]
    [__DynamicallyInvokable]
    [Serializable]
    public sealed class KeyCollection : ICollection<TKey>, IEnumerable<TKey>, IEnumerable, ICollection, IReadOnlyCollection<TKey>
    {
      private Dictionary<TKey, TValue> dictionary;

      /// <summary>获取 <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" /> 中包含的元素数。</summary>
      /// <returns>
      /// <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" /> 中包含的元素个数。检索此属性的值的运算复杂度为 O(1)。</returns>
      [__DynamicallyInvokable]
      public int Count
      {
        [__DynamicallyInvokable] get
        {
          return this.dictionary.Count;
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
          return ((ICollection) this.dictionary).SyncRoot;
        }
      }

      /// <summary>初始化 <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" /> 类的新实例，该实例反映指定的 <see cref="T:System.Collections.Generic.Dictionary`2" /> 中的键。</summary>
      /// <param name="dictionary">
      /// <see cref="T:System.Collections.Generic.Dictionary`2" />，其键反映在新的 <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" /> 中。</param>
      /// <exception cref="T:System.ArgumentNullException">
      /// <paramref name="dictionary" /> 为 null。</exception>
      [__DynamicallyInvokable]
      public KeyCollection(Dictionary<TKey, TValue> dictionary)
      {
        if (dictionary == null)
          ThrowHelper.ThrowArgumentNullException(ExceptionArgument.dictionary);
        this.dictionary = dictionary;
      }

      /// <summary>返回循环访问 <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" /> 的枚举数。</summary>
      /// <returns>用于 <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" /> 的 <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection.Enumerator" />。</returns>
      [__DynamicallyInvokable]
      public Dictionary<TKey, TValue>.KeyCollection.Enumerator GetEnumerator()
      {
        return new Dictionary<TKey, TValue>.KeyCollection.Enumerator(this.dictionary);
      }

      /// <summary>从指定数组索引开始将 <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" /> 元素复制到现有一维 <see cref="T:System.Array" /> 中。</summary>
      /// <param name="array">作为从 <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" /> 复制的元素的目标位置的一维 <see cref="T:System.Array" />。<see cref="T:System.Array" /> 必须具有从零开始的索引。</param>
      /// <param name="index">
      /// <paramref name="array" /> 中从零开始的索引，从此索引处开始进行复制。</param>
      /// <exception cref="T:System.ArgumentNullException">
      /// <paramref name="array" /> 为 null。</exception>
      /// <exception cref="T:System.ArgumentOutOfRangeException">
      /// <paramref name="index" /> 小于零。</exception>
      /// <exception cref="T:System.ArgumentException">源 <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" /> 中的元素数目大于从 <paramref name="index" /> 到目标 <paramref name="array" /> 末尾之间的可用空间。</exception>
      [__DynamicallyInvokable]
      public void CopyTo(TKey[] array, int index)
      {
        if (array == null)
          ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
        if (index < 0 || index > array.Length)
          ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
        if (array.Length - index < this.dictionary.Count)
          ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
        int num = this.dictionary.count;
        Dictionary<TKey, TValue>.Entry[] entryArray = this.dictionary.entries;
        for (int index1 = 0; index1 < num; ++index1)
        {
          if (entryArray[index1].hashCode >= 0)
            array[index++] = entryArray[index1].key;
        }
      }

      [__DynamicallyInvokable]
      void ICollection<TKey>.Add(TKey item)
      {
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_KeyCollectionSet);
      }

      [__DynamicallyInvokable]
      void ICollection<TKey>.Clear()
      {
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_KeyCollectionSet);
      }

      [__DynamicallyInvokable]
      bool ICollection<TKey>.Contains(TKey item)
      {
        return this.dictionary.ContainsKey(item);
      }

      [__DynamicallyInvokable]
      bool ICollection<TKey>.Remove(TKey item)
      {
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_KeyCollectionSet);
        return false;
      }

      [__DynamicallyInvokable]
      IEnumerator<TKey> IEnumerable<TKey>.GetEnumerator()
      {
        return (IEnumerator<TKey>) new Dictionary<TKey, TValue>.KeyCollection.Enumerator(this.dictionary);
      }

      [__DynamicallyInvokable]
      IEnumerator IEnumerable.GetEnumerator()
      {
        return (IEnumerator) new Dictionary<TKey, TValue>.KeyCollection.Enumerator(this.dictionary);
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
        if (array.Length - index < this.dictionary.Count)
          ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
        TKey[] array1 = array as TKey[];
        if (array1 != null)
        {
          this.CopyTo(array1, index);
        }
        else
        {
          object[] objArray = array as object[];
          if (objArray == null)
            ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
          int num = this.dictionary.count;
          Dictionary<TKey, TValue>.Entry[] entryArray = this.dictionary.entries;
          try
          {
            for (int index1 = 0; index1 < num; ++index1)
            {
              if (entryArray[index1].hashCode >= 0)
                objArray[index++] = (object) entryArray[index1].key;
            }
          }
          catch (ArrayTypeMismatchException ex)
          {
            ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
          }
        }
      }

      /// <summary>枚举 <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" /> 的元素。</summary>
      [__DynamicallyInvokable]
      [Serializable]
      public struct Enumerator : IEnumerator<TKey>, IDisposable, IEnumerator
      {
        private Dictionary<TKey, TValue> dictionary;
        private int index;
        private int version;
        private TKey currentKey;

        /// <summary>获取枚举数当前位置的元素。</summary>
        /// <returns>
        /// <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" /> 中位于该枚举数当前位置的元素。</returns>
        [__DynamicallyInvokable]
        public TKey Current
        {
          [__DynamicallyInvokable] get
          {
            return this.currentKey;
          }
        }

        [__DynamicallyInvokable]
        object IEnumerator.Current
        {
          [__DynamicallyInvokable] get
          {
            if (this.index == 0 || this.index == this.dictionary.count + 1)
              ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
            return (object) this.currentKey;
          }
        }

        internal Enumerator(Dictionary<TKey, TValue> dictionary)
        {
          this.dictionary = dictionary;
          this.version = dictionary.version;
          this.index = 0;
          this.currentKey = default (TKey);
        }

        /// <summary>释放由 <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection.Enumerator" /> 使用的所有资源。</summary>
        [__DynamicallyInvokable]
        public void Dispose()
        {
        }

        /// <summary>使枚举数前进到 <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" /> 的下一个元素。</summary>
        /// <returns>如果枚举数已成功地推进到下一个元素，则为 true；如果枚举数传递到集合的末尾，则为 false。</returns>
        /// <exception cref="T:System.InvalidOperationException">在创建了枚举数后集合被修改了。</exception>
        [__DynamicallyInvokable]
        public bool MoveNext()
        {
          if (this.version != this.dictionary.version)
            ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
          for (; (uint) this.index < (uint) this.dictionary.count; this.index = this.index + 1)
          {
            if (this.dictionary.entries[this.index].hashCode >= 0)
            {
              this.currentKey = this.dictionary.entries[this.index].key;
              this.index = this.index + 1;
              return true;
            }
          }
          this.index = this.dictionary.count + 1;
          this.currentKey = default (TKey);
          return false;
        }

        [__DynamicallyInvokable]
        void IEnumerator.Reset()
        {
          if (this.version != this.dictionary.version)
            ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
          this.index = 0;
          this.currentKey = default (TKey);
        }
      }
    }

    /// <summary>表示 <see cref="T:System.Collections.Generic.Dictionary`2" /> 中值的集合。此类不能被继承。</summary>
    [DebuggerTypeProxy(typeof (Mscorlib_DictionaryValueCollectionDebugView<,>))]
    [DebuggerDisplay("Count = {Count}")]
    [__DynamicallyInvokable]
    [Serializable]
    public sealed class ValueCollection : ICollection<TValue>, IEnumerable<TValue>, IEnumerable, ICollection, IReadOnlyCollection<TValue>
    {
      private Dictionary<TKey, TValue> dictionary;

      /// <summary>获取 <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" /> 中包含的元素数。</summary>
      /// <returns>
      /// <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" /> 中包含的元素个数。</returns>
      [__DynamicallyInvokable]
      public int Count
      {
        [__DynamicallyInvokable] get
        {
          return this.dictionary.Count;
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
          return ((ICollection) this.dictionary).SyncRoot;
        }
      }

      /// <summary>初始化 <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" /> 类的新实例，该实例反映指定的 <see cref="T:System.Collections.Generic.Dictionary`2" /> 中的值。</summary>
      /// <param name="dictionary">
      /// <see cref="T:System.Collections.Generic.Dictionary`2" />，其值反映在新的 <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" /> 中。</param>
      /// <exception cref="T:System.ArgumentNullException">
      /// <paramref name="dictionary" /> 为 null。</exception>
      [__DynamicallyInvokable]
      public ValueCollection(Dictionary<TKey, TValue> dictionary)
      {
        if (dictionary == null)
          ThrowHelper.ThrowArgumentNullException(ExceptionArgument.dictionary);
        this.dictionary = dictionary;
      }

      /// <summary>返回循环访问 <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" /> 的枚举数。</summary>
      /// <returns>用于 <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" /> 的 <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection.Enumerator" />。</returns>
      [__DynamicallyInvokable]
      public Dictionary<TKey, TValue>.ValueCollection.Enumerator GetEnumerator()
      {
        return new Dictionary<TKey, TValue>.ValueCollection.Enumerator(this.dictionary);
      }

      /// <summary>从指定数组索引开始将 <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" /> 元素复制到现有一维 <see cref="T:System.Array" /> 中。</summary>
      /// <param name="array">作为从 <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" /> 复制的元素的目标位置的一维 <see cref="T:System.Array" />。<see cref="T:System.Array" /> 必须具有从零开始的索引。</param>
      /// <param name="index">
      /// <paramref name="array" /> 中从零开始的索引，从此索引处开始进行复制。</param>
      /// <exception cref="T:System.ArgumentNullException">
      /// <paramref name="array" /> 为 null。</exception>
      /// <exception cref="T:System.ArgumentOutOfRangeException">
      /// <paramref name="index" /> 小于零。</exception>
      /// <exception cref="T:System.ArgumentException">源 <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" /> 中的元素数目大于从 <paramref name="index" /> 到目标 <paramref name="array" /> 末尾之间的可用空间。</exception>
      [__DynamicallyInvokable]
      public void CopyTo(TValue[] array, int index)
      {
        if (array == null)
          ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
        if (index < 0 || index > array.Length)
          ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
        if (array.Length - index < this.dictionary.Count)
          ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
        int num = this.dictionary.count;
        Dictionary<TKey, TValue>.Entry[] entryArray = this.dictionary.entries;
        for (int index1 = 0; index1 < num; ++index1)
        {
          if (entryArray[index1].hashCode >= 0)
            array[index++] = entryArray[index1].value;
        }
      }

      [__DynamicallyInvokable]
      void ICollection<TValue>.Add(TValue item)
      {
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ValueCollectionSet);
      }

      [__DynamicallyInvokable]
      bool ICollection<TValue>.Remove(TValue item)
      {
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ValueCollectionSet);
        return false;
      }

      [__DynamicallyInvokable]
      void ICollection<TValue>.Clear()
      {
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ValueCollectionSet);
      }

      [__DynamicallyInvokable]
      bool ICollection<TValue>.Contains(TValue item)
      {
        return this.dictionary.ContainsValue(item);
      }

      [__DynamicallyInvokable]
      IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
      {
        return (IEnumerator<TValue>) new Dictionary<TKey, TValue>.ValueCollection.Enumerator(this.dictionary);
      }

      [__DynamicallyInvokable]
      IEnumerator IEnumerable.GetEnumerator()
      {
        return (IEnumerator) new Dictionary<TKey, TValue>.ValueCollection.Enumerator(this.dictionary);
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
        if (array.Length - index < this.dictionary.Count)
          ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
        TValue[] array1 = array as TValue[];
        if (array1 != null)
        {
          this.CopyTo(array1, index);
        }
        else
        {
          object[] objArray = array as object[];
          if (objArray == null)
            ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
          int num = this.dictionary.count;
          Dictionary<TKey, TValue>.Entry[] entryArray = this.dictionary.entries;
          try
          {
            for (int index1 = 0; index1 < num; ++index1)
            {
              if (entryArray[index1].hashCode >= 0)
                objArray[index++] = (object) entryArray[index1].value;
            }
          }
          catch (ArrayTypeMismatchException ex)
          {
            ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
          }
        }
      }

      /// <summary>枚举 <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" /> 的元素。</summary>
      [__DynamicallyInvokable]
      [Serializable]
      public struct Enumerator : IEnumerator<TValue>, IDisposable, IEnumerator
      {
        private Dictionary<TKey, TValue> dictionary;
        private int index;
        private int version;
        private TValue currentValue;

        /// <summary>获取枚举数当前位置的元素。</summary>
        /// <returns>
        /// <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" /> 中位于枚举数当前位置的元素。</returns>
        [__DynamicallyInvokable]
        public TValue Current
        {
          [__DynamicallyInvokable] get
          {
            return this.currentValue;
          }
        }

        [__DynamicallyInvokable]
        object IEnumerator.Current
        {
          [__DynamicallyInvokable] get
          {
            if (this.index == 0 || this.index == this.dictionary.count + 1)
              ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
            return (object) this.currentValue;
          }
        }

        internal Enumerator(Dictionary<TKey, TValue> dictionary)
        {
          this.dictionary = dictionary;
          this.version = dictionary.version;
          this.index = 0;
          this.currentValue = default (TValue);
        }

        /// <summary>释放由 <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection.Enumerator" /> 使用的所有资源。</summary>
        [__DynamicallyInvokable]
        public void Dispose()
        {
        }

        /// <summary>使枚举器前进到 <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" /> 的下一个元素。</summary>
        /// <returns>如果枚举数成功地推进到下一个元素，则为 true；如果枚举数越过集合的结尾，则为 false。</returns>
        /// <exception cref="T:System.InvalidOperationException">在创建了枚举数后集合被修改了。</exception>
        [__DynamicallyInvokable]
        public bool MoveNext()
        {
          if (this.version != this.dictionary.version)
            ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
          for (; (uint) this.index < (uint) this.dictionary.count; this.index = this.index + 1)
          {
            if (this.dictionary.entries[this.index].hashCode >= 0)
            {
              this.currentValue = this.dictionary.entries[this.index].value;
              this.index = this.index + 1;
              return true;
            }
          }
          this.index = this.dictionary.count + 1;
          this.currentValue = default (TValue);
          return false;
        }

        [__DynamicallyInvokable]
        void IEnumerator.Reset()
        {
          if (this.version != this.dictionary.version)
            ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
          this.index = 0;
          this.currentValue = default (TValue);
        }
      }
    }
  }
}
