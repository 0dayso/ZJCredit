// Decompiled with JetBrains decompiler
// Type: System.Collections.Concurrent.ConcurrentDictionary`2
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections.Concurrent
{
  /// <summary>表示可由多个线程同时访问的键/值对的线程安全集合。</summary>
  /// <typeparam name="TKey">字典中的键的类型。</typeparam>
  /// <typeparam name="TValue">字典中的值的类型。</typeparam>
  [ComVisible(false)]
  [DebuggerTypeProxy(typeof (Mscorlib_DictionaryDebugView<,>))]
  [DebuggerDisplay("Count = {Count}")]
  [__DynamicallyInvokable]
  [Serializable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public class ConcurrentDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary, ICollection, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>
  {
    private static readonly bool s_isValueWriteAtomic = ConcurrentDictionary<TKey, TValue>.IsValueWriteAtomic();
    [NonSerialized]
    private volatile ConcurrentDictionary<TKey, TValue>.Tables m_tables;
    internal IEqualityComparer<TKey> m_comparer;
    [NonSerialized]
    private readonly bool m_growLockArray;
    [OptionalField]
    private int m_keyRehashCount;
    [NonSerialized]
    private int m_budget;
    private KeyValuePair<TKey, TValue>[] m_serializationArray;
    private int m_serializationConcurrencyLevel;
    private int m_serializationCapacity;
    private const int DEFAULT_CONCURRENCY_MULTIPLIER = 4;
    private const int DEFAULT_CAPACITY = 31;
    private const int MAX_LOCK_NUMBER = 1024;

    /// <summary>获取或设置与指定的键关联的值。</summary>
    /// <returns>位于指定索引处的键/值对的值。</returns>
    /// <param name="key">要获取或设置的值的键。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> is  null.</exception>
    /// <exception cref="T:System.Collections.Generic.KeyNotFoundException">已检索该属性和 <paramref name="key" /> 集合中不存在。</exception>
    [__DynamicallyInvokable]
    public TValue this[TKey key]
    {
      [__DynamicallyInvokable] get
      {
        TValue obj;
        if (!this.TryGetValue(key, out obj))
          throw new KeyNotFoundException();
        return obj;
      }
      [__DynamicallyInvokable] set
      {
        if ((object) key == null)
          throw new ArgumentNullException("key");
        TValue resultingValue;
        this.TryAddInternal(key, value, true, true, out resultingValue);
      }
    }

    /// <summary>获取包含在 <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> 中的键/值对的数目。</summary>
    /// <returns>包含在 <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> 中的键/值对的数目。</returns>
    /// <exception cref="T:System.OverflowException">字典中已包含元素的最大数目 (<see cref="F:System.Int32.MaxValue" />)。</exception>
    [__DynamicallyInvokable]
    public int Count
    {
      [__DynamicallyInvokable] get
      {
        int num = 0;
        int locksAcquired = 0;
        try
        {
          this.AcquireAllLocks(ref locksAcquired);
          for (int index = 0; index < this.m_tables.m_countPerLock.Length; ++index)
            num += this.m_tables.m_countPerLock[index];
        }
        finally
        {
          this.ReleaseLocks(0, locksAcquired);
        }
        return num;
      }
    }

    /// <summary>获取一个值，该值指示 <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> 是否为空。</summary>
    /// <returns>如果 true 为空，则为 <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool IsEmpty
    {
      [__DynamicallyInvokable] get
      {
        int locksAcquired = 0;
        try
        {
          this.AcquireAllLocks(ref locksAcquired);
          for (int index = 0; index < this.m_tables.m_countPerLock.Length; ++index)
          {
            if (this.m_tables.m_countPerLock[index] != 0)
              return false;
          }
        }
        finally
        {
          this.ReleaseLocks(0, locksAcquired);
        }
        return true;
      }
    }

    /// <summary>获得一个包含 <see cref="T:System.Collections.Generic.Dictionary`2" /> 中的键的集合。</summary>
    /// <returns>
    /// <see cref="T:System.Collections.Generic.Dictionary`2" /> 中的键的集合。</returns>
    [__DynamicallyInvokable]
    public ICollection<TKey> Keys
    {
      [__DynamicallyInvokable] get
      {
        return (ICollection<TKey>) this.GetKeys();
      }
    }

    [__DynamicallyInvokable]
    IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
    {
      [__DynamicallyInvokable] get
      {
        return (IEnumerable<TKey>) this.GetKeys();
      }
    }

    /// <summary>获取包含 <see cref="T:System.Collections.Generic.Dictionary`2" /> 中的值的集合。</summary>
    /// <returns>包含 <see cref="T:System.Collections.Generic.Dictionary`2" /> 中的值的集合。</returns>
    [__DynamicallyInvokable]
    public ICollection<TValue> Values
    {
      [__DynamicallyInvokable] get
      {
        return (ICollection<TValue>) this.GetValues();
      }
    }

    [__DynamicallyInvokable]
    IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
    {
      [__DynamicallyInvokable] get
      {
        return (IEnumerable<TValue>) this.GetValues();
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
        return (ICollection) this.GetKeys();
      }
    }

    [__DynamicallyInvokable]
    ICollection IDictionary.Values
    {
      [__DynamicallyInvokable] get
      {
        return (ICollection) this.GetValues();
      }
    }

    [__DynamicallyInvokable]
    object IDictionary.this[object key]
    {
      [__DynamicallyInvokable] get
      {
        if (key == null)
          throw new ArgumentNullException("key");
        TValue obj;
        if (key is TKey && this.TryGetValue((TKey) key, out obj))
          return (object) obj;
        return (object) null;
      }
      [__DynamicallyInvokable] set
      {
        if (key == null)
          throw new ArgumentNullException("key");
        if (!(key is TKey))
          throw new ArgumentException(this.GetResource("ConcurrentDictionary_TypeOfKeyIncorrect"));
        if (!(value is TValue))
          throw new ArgumentException(this.GetResource("ConcurrentDictionary_TypeOfValueIncorrect"));
        this[(TKey) key] = (TValue) value;
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
        throw new NotSupportedException(Environment.GetResourceString("ConcurrentCollection_SyncRoot_NotSupported"));
      }
    }

    private static int DefaultConcurrencyLevel
    {
      get
      {
        return 4 * PlatformHelper.ProcessorCount;
      }
    }

    /// <summary>初始化 <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> 类的新实例，该实例为空，具有默认的并发级别和默认的初始容量，并为键类型使用默认比较器。</summary>
    [__DynamicallyInvokable]
    public ConcurrentDictionary()
      : this(ConcurrentDictionary<TKey, TValue>.DefaultConcurrencyLevel, 31, true, (IEqualityComparer<TKey>) EqualityComparer<TKey>.Default)
    {
    }

    /// <summary>初始化 <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> 类的新实例，该实例为空，具有指定的并发级别和容量，并为键类型使用默认比较器。</summary>
    /// <param name="concurrencyLevel">将同时更新 <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> 的线程的估计数量。</param>
    /// <param name="capacity">
    /// <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> 可包含的初始元素数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="concurrencyLevel" /> 为小于 1。- 或 -<paramref name="capacity" /> 小于 0。</exception>
    [__DynamicallyInvokable]
    public ConcurrentDictionary(int concurrencyLevel, int capacity)
      : this(concurrencyLevel, capacity, false, (IEqualityComparer<TKey>) EqualityComparer<TKey>.Default)
    {
    }

    /// <summary>初始化 <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> 类的新实例，该实例包含从指定的 <see cref="T:System.Collections.Generic.IEnumerable`1" /> 中复制的元素，具有默认的并发级别和默认的初始容量，并为键类型使用默认比较器。</summary>
    /// <param name="collection">
    /// <see cref="T:System.Collections.Generic.IEnumerable`1" />，它的元素被复制到新 <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="collection" /> 或任何其密钥为  null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="collection" /> 包含一个或多个重复的键。</exception>
    [__DynamicallyInvokable]
    public ConcurrentDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection)
      : this(collection, (IEqualityComparer<TKey>) EqualityComparer<TKey>.Default)
    {
    }

    /// <summary>初始化 <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> 类的新实例，该实例为空，具有默认的并发级别和容量，并使用指定的 <see cref="T:System.Collections.Generic.IEqualityComparer`1" />。</summary>
    /// <param name="comparer">在对键进行比较时使用的相等比较实现。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="comparer" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public ConcurrentDictionary(IEqualityComparer<TKey> comparer)
      : this(ConcurrentDictionary<TKey, TValue>.DefaultConcurrencyLevel, 31, true, comparer)
    {
    }

    /// <summary>初始化 <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> 类的新实例，该实例包含从指定的 <see cref="T:System.Collections.IEnumerable" /> 中复制的元素，具有默认的并发级别和默认的初始容量，并使用指定的 <see cref="T:System.Collections.Generic.IEqualityComparer`1" />。</summary>
    /// <param name="collection">
    /// <see cref="T:System.Collections.Generic.IEnumerable`1" />，它的元素被复制到新 <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />。</param>
    /// <param name="comparer">在对键进行比较时使用的 <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> 实现。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="collection" /> 或 <paramref name="comparer" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public ConcurrentDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey> comparer)
      : this(comparer)
    {
      if (collection == null)
        throw new ArgumentNullException("collection");
      this.InitializeFromCollection(collection);
    }

    /// <summary>初始化 <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> 类的新实例，该实例包含从指定的 <see cref="T:System.Collections.IEnumerable" /> 中复制的元素并使用指定的 <see cref="T:System.Collections.Generic.IEqualityComparer`1" />。</summary>
    /// <param name="concurrencyLevel">将同时更新 <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> 的线程的估计数量。</param>
    /// <param name="collection">
    /// <see cref="T:System.Collections.Generic.IEnumerable`1" />，它的元素被复制到新 <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />。</param>
    /// <param name="comparer">在对键进行比较时使用的 <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> 实现。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="collection" /> 或 <paramref name="comparer" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="concurrencyLevel" /> 为小于 1。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="collection" /> 包含一个或多个重复的键。</exception>
    [__DynamicallyInvokable]
    public ConcurrentDictionary(int concurrencyLevel, IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey> comparer)
      : this(concurrencyLevel, 31, false, comparer)
    {
      if (collection == null)
        throw new ArgumentNullException("collection");
      if (comparer == null)
        throw new ArgumentNullException("comparer");
      this.InitializeFromCollection(collection);
    }

    /// <summary>初始化 <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> 类的新实例，该实例为空，具有指定的并发级别和指定的初始容量，并使用指定的 <see cref="T:System.Collections.Generic.IEqualityComparer`1" />。</summary>
    /// <param name="concurrencyLevel">将同时更新 <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> 的线程的估计数量。</param>
    /// <param name="capacity">
    /// <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> 可包含的初始元素数。</param>
    /// <param name="comparer">在对键进行比较时使用的 <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> 实现。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="comparer" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="concurrencyLevel" /> 或 <paramref name="capacity" /> 小于 1。</exception>
    [__DynamicallyInvokable]
    public ConcurrentDictionary(int concurrencyLevel, int capacity, IEqualityComparer<TKey> comparer)
      : this(concurrencyLevel, capacity, false, comparer)
    {
    }

    internal ConcurrentDictionary(int concurrencyLevel, int capacity, bool growLockArray, IEqualityComparer<TKey> comparer)
    {
      if (concurrencyLevel < 1)
        throw new ArgumentOutOfRangeException("concurrencyLevel", this.GetResource("ConcurrentDictionary_ConcurrencyLevelMustBePositive"));
      if (capacity < 0)
        throw new ArgumentOutOfRangeException("capacity", this.GetResource("ConcurrentDictionary_CapacityMustNotBeNegative"));
      if (comparer == null)
        throw new ArgumentNullException("comparer");
      if (capacity < concurrencyLevel)
        capacity = concurrencyLevel;
      object[] locks = new object[concurrencyLevel];
      for (int index = 0; index < locks.Length; ++index)
        locks[index] = new object();
      int[] countPerLock = new int[locks.Length];
      ConcurrentDictionary<TKey, TValue>.Node[] buckets = new ConcurrentDictionary<TKey, TValue>.Node[capacity];
      this.m_tables = new ConcurrentDictionary<TKey, TValue>.Tables(buckets, locks, countPerLock, comparer);
      this.m_growLockArray = growLockArray;
      this.m_budget = buckets.Length / locks.Length;
    }

    private static bool IsValueWriteAtomic()
    {
      Type type = typeof (TValue);
      bool flag = type.IsClass || type == typeof (bool) || (type == typeof (char) || type == typeof (byte)) || (type == typeof (sbyte) || type == typeof (short) || (type == typeof (ushort) || type == typeof (int))) || type == typeof (uint) || type == typeof (float);
      if (!flag && IntPtr.Size == 8)
        flag = ((flag ? 1 : 0) | (type == typeof (double) ? 1 : (type == typeof (long) ? 1 : 0))) != 0;
      return flag;
    }

    private void InitializeFromCollection(IEnumerable<KeyValuePair<TKey, TValue>> collection)
    {
      foreach (KeyValuePair<TKey, TValue> keyValuePair in collection)
      {
        if ((object) keyValuePair.Key == null)
          throw new ArgumentNullException("key");
        TValue resultingValue;
        if (!this.TryAddInternal(keyValuePair.Key, keyValuePair.Value, false, false, out resultingValue))
          throw new ArgumentException(this.GetResource("ConcurrentDictionary_SourceContainsDuplicateKeys"));
      }
      if (this.m_budget != 0)
        return;
      this.m_budget = this.m_tables.m_buckets.Length / this.m_tables.m_locks.Length;
    }

    /// <summary>尝试将指定的键和值添加到 <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> 中。</summary>
    /// <returns>如果成功地将键/值对添加到 true，则为 <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />；如果该键已存在，则为 false。</returns>
    /// <param name="key">要添加的元素的键。</param>
    /// <param name="value">要添加的元素的值。对于引用类型，该值可以为 null。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> is  null.</exception>
    /// <exception cref="T:System.OverflowException">字典中已包含元素的最大数目 (<see cref="F:System.Int32.MaxValue" />)。</exception>
    [__DynamicallyInvokable]
    public bool TryAdd(TKey key, TValue value)
    {
      if ((object) key == null)
        throw new ArgumentNullException("key");
      TValue resultingValue;
      return this.TryAddInternal(key, value, false, true, out resultingValue);
    }

    /// <summary>确定是否 <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> 包含指定键。</summary>
    /// <returns>如果 true 包含具有指定键的元素，则为 <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />；否则为 false。</returns>
    /// <param name="key">要在 <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> 中定位的键。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public bool ContainsKey(TKey key)
    {
      if ((object) key == null)
        throw new ArgumentNullException("key");
      TValue obj;
      return this.TryGetValue(key, out obj);
    }

    /// <summary>尝试从 <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> 中移除并返回具有指定键的值。</summary>
    /// <returns>如果成功地移除了对象，则为 true；否则为 false。</returns>
    /// <param name="key">要移除并返回的元素的键。</param>
    /// <param name="value">当此方法返回时，将包含从 <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> 中移除的对象；如果 TValue 不存在，则包含 <paramref name="key" /> 类型的默认值。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> is  null.</exception>
    [__DynamicallyInvokable]
    public bool TryRemove(TKey key, out TValue value)
    {
      if ((object) key == null)
        throw new ArgumentNullException("key");
      return this.TryRemoveInternal(key, out value, false, default (TValue));
    }

    private bool TryRemoveInternal(TKey key, out TValue value, bool matchValue, TValue oldValue)
    {
label_0:
      ConcurrentDictionary<TKey, TValue>.Tables tables = this.m_tables;
      IEqualityComparer<TKey> equalityComparer = tables.m_comparer;
      int bucketNo;
      int lockNo;
      this.GetBucketAndLockNo(equalityComparer.GetHashCode(key), out bucketNo, out lockNo, tables.m_buckets.Length, tables.m_locks.Length);
      lock (tables.m_locks[lockNo])
      {
        if (tables == this.m_tables)
        {
          ConcurrentDictionary<TKey, TValue>.Node local_6 = (ConcurrentDictionary<TKey, TValue>.Node) null;
          for (ConcurrentDictionary<TKey, TValue>.Node local_7 = tables.m_buckets[bucketNo]; local_7 != null; local_7 = local_7.m_next)
          {
            if (equalityComparer.Equals(local_7.m_key, key))
            {
              if (matchValue && !EqualityComparer<TValue>.Default.Equals(oldValue, local_7.m_value))
              {
                value = default (TValue);
                return false;
              }
              if (local_6 == null)
                Volatile.Write<ConcurrentDictionary<TKey, TValue>.Node>(ref tables.m_buckets[bucketNo], local_7.m_next);
              else
                local_6.m_next = local_7.m_next;
              value = local_7.m_value;
              --tables.m_countPerLock[lockNo];
              return true;
            }
            local_6 = local_7;
          }
        }
        else
          goto label_0;
      }
      value = default (TValue);
      return false;
    }

    /// <summary>尝试从 <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> 获取与指定的键关联的值。</summary>
    /// <returns>如果在 true 中找到该键，则为 <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />；否则为 false。</returns>
    /// <param name="key">要获取的值的键。</param>
    /// <param name="value">当此方法返回时，将包含 <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> 中具有指定键的对象；如果操作失败，则包含类型的默认值。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> is  null.</exception>
    [__DynamicallyInvokable]
    public bool TryGetValue(TKey key, out TValue value)
    {
      if ((object) key == null)
        throw new ArgumentNullException("key");
      ConcurrentDictionary<TKey, TValue>.Tables tables = this.m_tables;
      IEqualityComparer<TKey> equalityComparer = tables.m_comparer;
      int bucketNo;
      int lockNo;
      this.GetBucketAndLockNo(equalityComparer.GetHashCode(key), out bucketNo, out lockNo, tables.m_buckets.Length, tables.m_locks.Length);
      for (ConcurrentDictionary<TKey, TValue>.Node node = Volatile.Read<ConcurrentDictionary<TKey, TValue>.Node>(ref tables.m_buckets[bucketNo]); node != null; node = node.m_next)
      {
        if (equalityComparer.Equals(node.m_key, key))
        {
          value = node.m_value;
          return true;
        }
      }
      value = default (TValue);
      return false;
    }

    /// <summary>将指定键的现有值与指定值进行比较，如果相等，则用第三个值更新该键。</summary>
    /// <returns>如果具有 true 的值与 <paramref name="key" /> 相等且被替换为 <paramref name="comparisonValue" />，则为 <paramref name="newValue" />；否则为 false。</returns>
    /// <param name="key">其值将与 <paramref name="comparisonValue" /> 进行比较并且可能被替换的键。</param>
    /// <param name="newValue">当比较结果相等时，该值将替换具有指定 <paramref name="key" /> 的元素的值。</param>
    /// <param name="comparisonValue">与具有指定 <paramref name="key" /> 的元素的值进行比较的值。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public bool TryUpdate(TKey key, TValue newValue, TValue comparisonValue)
    {
      if ((object) key == null)
        throw new ArgumentNullException("key");
      IEqualityComparer<TValue> equalityComparer1 = (IEqualityComparer<TValue>) EqualityComparer<TValue>.Default;
label_3:
      ConcurrentDictionary<TKey, TValue>.Tables tables = this.m_tables;
      IEqualityComparer<TKey> equalityComparer2 = tables.m_comparer;
      int hashCode = equalityComparer2.GetHashCode(key);
      int bucketNo;
      int lockNo;
      this.GetBucketAndLockNo(hashCode, out bucketNo, out lockNo, tables.m_buckets.Length, tables.m_locks.Length);
      lock (tables.m_locks[lockNo])
      {
        if (tables == this.m_tables)
        {
          ConcurrentDictionary<TKey, TValue>.Node local_8 = (ConcurrentDictionary<TKey, TValue>.Node) null;
          for (ConcurrentDictionary<TKey, TValue>.Node local_9 = tables.m_buckets[bucketNo]; local_9 != null; local_9 = local_9.m_next)
          {
            if (equalityComparer2.Equals(local_9.m_key, key))
            {
              if (!equalityComparer1.Equals(local_9.m_value, comparisonValue))
                return false;
              if (ConcurrentDictionary<TKey, TValue>.s_isValueWriteAtomic)
              {
                local_9.m_value = newValue;
              }
              else
              {
                ConcurrentDictionary<TKey, TValue>.Node local_10 = new ConcurrentDictionary<TKey, TValue>.Node(local_9.m_key, newValue, hashCode, local_9.m_next);
                if (local_8 == null)
                  tables.m_buckets[bucketNo] = local_10;
                else
                  local_8.m_next = local_10;
              }
              return true;
            }
            local_8 = local_9;
          }
          return false;
        }
        goto label_3;
      }
    }

    /// <summary>将所有键和值从 <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> 中移除。</summary>
    [__DynamicallyInvokable]
    public void Clear()
    {
      int locksAcquired = 0;
      try
      {
        this.AcquireAllLocks(ref locksAcquired);
        ConcurrentDictionary<TKey, TValue>.Tables tables = new ConcurrentDictionary<TKey, TValue>.Tables(new ConcurrentDictionary<TKey, TValue>.Node[31], this.m_tables.m_locks, new int[this.m_tables.m_countPerLock.Length], this.m_tables.m_comparer);
        this.m_tables = tables;
        this.m_budget = Math.Max(1, tables.m_buckets.Length / tables.m_locks.Length);
      }
      finally
      {
        this.ReleaseLocks(0, locksAcquired);
      }
    }

    [__DynamicallyInvokable]
    void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      if (index < 0)
        throw new ArgumentOutOfRangeException("index", this.GetResource("ConcurrentDictionary_IndexIsNegative"));
      int locksAcquired = 0;
      try
      {
        this.AcquireAllLocks(ref locksAcquired);
        int num = 0;
        for (int index1 = 0; index1 < this.m_tables.m_locks.Length && num >= 0; ++index1)
          num += this.m_tables.m_countPerLock[index1];
        if (array.Length - num < index || num < 0)
          throw new ArgumentException(this.GetResource("ConcurrentDictionary_ArrayNotLargeEnough"));
        this.CopyToPairs(array, index);
      }
      finally
      {
        this.ReleaseLocks(0, locksAcquired);
      }
    }

    /// <summary>将 <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> 中存储的键和值对复制到新数组中。</summary>
    /// <returns>一个新数组，其中包含从 <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> 复制的键和值对的快照。</returns>
    [__DynamicallyInvokable]
    public KeyValuePair<TKey, TValue>[] ToArray()
    {
      int locksAcquired = 0;
      try
      {
        this.AcquireAllLocks(ref locksAcquired);
        int length = 0;
        int index = 0;
        while (index < this.m_tables.m_locks.Length)
        {
          checked { length += this.m_tables.m_countPerLock[index]; }
          checked { ++index; }
        }
        KeyValuePair<TKey, TValue>[] array = new KeyValuePair<TKey, TValue>[length];
        this.CopyToPairs(array, 0);
        return array;
      }
      finally
      {
        this.ReleaseLocks(0, locksAcquired);
      }
    }

    private void CopyToPairs(KeyValuePair<TKey, TValue>[] array, int index)
    {
      foreach (ConcurrentDictionary<TKey, TValue>.Node mBucket in this.m_tables.m_buckets)
      {
        for (ConcurrentDictionary<TKey, TValue>.Node node = mBucket; node != null; node = node.m_next)
        {
          array[index] = new KeyValuePair<TKey, TValue>(node.m_key, node.m_value);
          ++index;
        }
      }
    }

    private void CopyToEntries(DictionaryEntry[] array, int index)
    {
      foreach (ConcurrentDictionary<TKey, TValue>.Node mBucket in this.m_tables.m_buckets)
      {
        for (ConcurrentDictionary<TKey, TValue>.Node node = mBucket; node != null; node = node.m_next)
        {
          array[index] = new DictionaryEntry((object) node.m_key, (object) node.m_value);
          ++index;
        }
      }
    }

    private void CopyToObjects(object[] array, int index)
    {
      foreach (ConcurrentDictionary<TKey, TValue>.Node mBucket in this.m_tables.m_buckets)
      {
        for (ConcurrentDictionary<TKey, TValue>.Node node = mBucket; node != null; node = node.m_next)
        {
          array[index] = (object) new KeyValuePair<TKey, TValue>(node.m_key, node.m_value);
          ++index;
        }
      }
    }

    /// <summary>返回循环访问 <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> 的枚举数。</summary>
    /// <returns>用于 <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> 的枚举数。</returns>
    [__DynamicallyInvokable]
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
      foreach (ConcurrentDictionary<TKey, TValue>.Node mBucket in this.m_tables.m_buckets)
      {
        ConcurrentDictionary<TKey, TValue>.Node current;
        for (current = Volatile.Read<ConcurrentDictionary<TKey, TValue>.Node>(ref mBucket); current != null; current = current.m_next)
          yield return new KeyValuePair<TKey, TValue>(current.m_key, current.m_value);
        current = (ConcurrentDictionary<TKey, TValue>.Node) null;
      }
    }

    private bool TryAddInternal(TKey key, TValue value, bool updateIfExists, bool acquireLock, out TValue resultingValue)
    {
label_0:
      ConcurrentDictionary<TKey, TValue>.Tables tables1 = this.m_tables;
      IEqualityComparer<TKey> equalityComparer = tables1.m_comparer;
      int hashCode = equalityComparer.GetHashCode(key);
      int bucketNo;
      int lockNo;
      this.GetBucketAndLockNo(hashCode, out bucketNo, out lockNo, tables1.m_buckets.Length, tables1.m_locks.Length);
      bool flag1 = false;
      bool lockTaken = false;
      bool flag2 = false;
      try
      {
        if (acquireLock)
          Monitor.Enter(tables1.m_locks[lockNo], ref lockTaken);
        if (tables1 == this.m_tables)
        {
          int num = 0;
          ConcurrentDictionary<TKey, TValue>.Node node1 = (ConcurrentDictionary<TKey, TValue>.Node) null;
          for (ConcurrentDictionary<TKey, TValue>.Node node2 = tables1.m_buckets[bucketNo]; node2 != null; node2 = node2.m_next)
          {
            if (equalityComparer.Equals(node2.m_key, key))
            {
              if (updateIfExists)
              {
                if (ConcurrentDictionary<TKey, TValue>.s_isValueWriteAtomic)
                {
                  node2.m_value = value;
                }
                else
                {
                  ConcurrentDictionary<TKey, TValue>.Node node3 = new ConcurrentDictionary<TKey, TValue>.Node(node2.m_key, value, hashCode, node2.m_next);
                  if (node1 == null)
                    tables1.m_buckets[bucketNo] = node3;
                  else
                    node1.m_next = node3;
                }
                resultingValue = value;
              }
              else
                resultingValue = node2.m_value;
              return false;
            }
            node1 = node2;
            ++num;
          }
          if (num > 100 && HashHelpers.IsWellKnownEqualityComparer((object) equalityComparer))
          {
            flag1 = true;
            flag2 = true;
          }
          Volatile.Write<ConcurrentDictionary<TKey, TValue>.Node>(ref tables1.m_buckets[bucketNo], new ConcurrentDictionary<TKey, TValue>.Node(key, value, hashCode, tables1.m_buckets[bucketNo]));
          checked { ++tables1.m_countPerLock[lockNo]; }
          if (tables1.m_countPerLock[lockNo] > this.m_budget)
            flag1 = true;
        }
        else
          goto label_0;
      }
      finally
      {
        if (lockTaken)
          Monitor.Exit(tables1.m_locks[lockNo]);
      }
      if (flag1)
      {
        if (flag2)
        {
          this.GrowTable(tables1, (IEqualityComparer<TKey>) HashHelpers.GetRandomizedEqualityComparer((object) equalityComparer), true, this.m_keyRehashCount);
        }
        else
        {
          ConcurrentDictionary<TKey, TValue>.Tables tables2 = tables1;
          IEqualityComparer<TKey> newComparer = tables2.m_comparer;
          int num = 0;
          int rehashCount = this.m_keyRehashCount;
          this.GrowTable(tables2, newComparer, num != 0, rehashCount);
        }
      }
      resultingValue = value;
      return true;
    }

    /// <summary>如果该键不存在，则通过使用指定的函数将键/值对添加到 <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> 中。</summary>
    /// <returns>键的值。如果字典中已存在该键，则为该键的现有值；如果字典中不存在该键，则为由 valueFactory 返回的键的新值。</returns>
    /// <param name="key">要添加的元素的键。</param>
    /// <param name="valueFactory">用于为键生成值的函数</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> 或 <paramref name="valueFactory" /> 为 null。</exception>
    /// <exception cref="T:System.OverflowException">字典中已包含元素的最大数目 (<see cref="F:System.Int32.MaxValue" />)。</exception>
    [__DynamicallyInvokable]
    public TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory)
    {
      if ((object) key == null)
        throw new ArgumentNullException("key");
      if (valueFactory == null)
        throw new ArgumentNullException("valueFactory");
      TValue resultingValue;
      if (this.TryGetValue(key, out resultingValue))
        return resultingValue;
      this.TryAddInternal(key, valueFactory(key), false, true, out resultingValue);
      return resultingValue;
    }

    /// <summary>如果该键不存在，则将键/值对添加到 <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> 中。</summary>
    /// <returns>键的值。如果字典中已存在该键，则为该键的现有值；如果字典中不存在该键，则为新值。</returns>
    /// <param name="key">要添加的元素的键。</param>
    /// <param name="value">当键不存在时要添加的值</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> 为 null。</exception>
    /// <exception cref="T:System.OverflowException">字典中已包含元素的最大数目 (<see cref="F:System.Int32.MaxValue" />)。</exception>
    [__DynamicallyInvokable]
    public TValue GetOrAdd(TKey key, TValue value)
    {
      if ((object) key == null)
        throw new ArgumentNullException("key");
      TValue resultingValue;
      this.TryAddInternal(key, value, false, true, out resultingValue);
      return resultingValue;
    }

    /// <summary>如果该键不存在，则使用指定函数将键/值对添加到 <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />；如果该键已存在，则使用该函数更新 <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> 中的键/值对。</summary>
    /// <returns>键的新值。这将是 addValueFactory 的结果（如果缺少键）或 updateValueFactory 的结果（如果存在键）。</returns>
    /// <param name="key">要添加的键或应更新其值的键</param>
    /// <param name="addValueFactory">用于为空缺键生成值的函数</param>
    /// <param name="updateValueFactory">用于基于键的现有值为现有键生成新值的函数</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" />, <paramref name="addValueFactory" />, or <paramref name="updateValueFactory" /> is null.</exception>
    /// <exception cref="T:System.OverflowException">字典中已包含元素的最大数目 (<see cref="F:System.Int32.MaxValue" />)。</exception>
    [__DynamicallyInvokable]
    public TValue AddOrUpdate(TKey key, Func<TKey, TValue> addValueFactory, Func<TKey, TValue, TValue> updateValueFactory)
    {
      if ((object) key == null)
        throw new ArgumentNullException("key");
      if (addValueFactory == null)
        throw new ArgumentNullException("addValueFactory");
      if (updateValueFactory == null)
        throw new ArgumentNullException("updateValueFactory");
      TValue comparisonValue;
      TValue newValue;
      do
      {
        while (!this.TryGetValue(key, out comparisonValue))
        {
          TValue obj = addValueFactory(key);
          TValue resultingValue;
          if (this.TryAddInternal(key, obj, false, true, out resultingValue))
            return resultingValue;
        }
        newValue = updateValueFactory(key, comparisonValue);
      }
      while (!this.TryUpdate(key, newValue, comparisonValue));
      return newValue;
    }

    /// <summary>如果该键不存在，则将键/值对添加到 <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> 中；如果该键已经存在，则通过使用指定的函数更新 <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> 中的键/值对。</summary>
    /// <returns>键的新值。这将是 addValue（如果缺少键）或 updateValueFactory 的结果（如果存在键）。</returns>
    /// <param name="key">要添加的键或应更新其值的键</param>
    /// <param name="addValue">要为空缺键添加的值</param>
    /// <param name="updateValueFactory">用于基于键的现有值为现有键生成新值的函数</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> 或 <paramref name="updateValueFactory" /> 为 null。</exception>
    /// <exception cref="T:System.OverflowException">字典中已包含元素的最大数目 (<see cref="F:System.Int32.MaxValue" />)。</exception>
    [__DynamicallyInvokable]
    public TValue AddOrUpdate(TKey key, TValue addValue, Func<TKey, TValue, TValue> updateValueFactory)
    {
      if ((object) key == null)
        throw new ArgumentNullException("key");
      if (updateValueFactory == null)
        throw new ArgumentNullException("updateValueFactory");
      TValue comparisonValue;
      TValue newValue;
      do
      {
        while (!this.TryGetValue(key, out comparisonValue))
        {
          TValue resultingValue;
          if (this.TryAddInternal(key, addValue, false, true, out resultingValue))
            return resultingValue;
        }
        newValue = updateValueFactory(key, comparisonValue);
      }
      while (!this.TryUpdate(key, newValue, comparisonValue));
      return newValue;
    }

    [__DynamicallyInvokable]
    void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
    {
      if (!this.TryAdd(key, value))
        throw new ArgumentException(this.GetResource("ConcurrentDictionary_KeyAlreadyExisted"));
    }

    [__DynamicallyInvokable]
    bool IDictionary<TKey, TValue>.Remove(TKey key)
    {
      TValue obj;
      return this.TryRemove(key, out obj);
    }

    [__DynamicallyInvokable]
    void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> keyValuePair)
    {
      ((IDictionary<TKey, TValue>) this).Add(keyValuePair.Key, keyValuePair.Value);
    }

    [__DynamicallyInvokable]
    bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> keyValuePair)
    {
      TValue x;
      if (!this.TryGetValue(keyValuePair.Key, out x))
        return false;
      return EqualityComparer<TValue>.Default.Equals(x, keyValuePair.Value);
    }

    [__DynamicallyInvokable]
    bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> keyValuePair)
    {
      if ((object) keyValuePair.Key == null)
        throw new ArgumentNullException(this.GetResource("ConcurrentDictionary_ItemKeyIsNull"));
      TValue obj;
      return this.TryRemoveInternal(keyValuePair.Key, out obj, true, keyValuePair.Value);
    }

    [__DynamicallyInvokable]
    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    [__DynamicallyInvokable]
    void IDictionary.Add(object key, object value)
    {
      if (key == null)
        throw new ArgumentNullException("key");
      if (!(key is TKey))
        throw new ArgumentException(this.GetResource("ConcurrentDictionary_TypeOfKeyIncorrect"));
      TValue obj;
      try
      {
        obj = (TValue) value;
      }
      catch (InvalidCastException ex)
      {
        throw new ArgumentException(this.GetResource("ConcurrentDictionary_TypeOfValueIncorrect"));
      }
      ((IDictionary<TKey, TValue>) this).Add((TKey) key, obj);
    }

    [__DynamicallyInvokable]
    bool IDictionary.Contains(object key)
    {
      if (key == null)
        throw new ArgumentNullException("key");
      if (key is TKey)
        return this.ContainsKey((TKey) key);
      return false;
    }

    [__DynamicallyInvokable]
    IDictionaryEnumerator IDictionary.GetEnumerator()
    {
      return (IDictionaryEnumerator) new ConcurrentDictionary<TKey, TValue>.DictionaryEnumerator(this);
    }

    [__DynamicallyInvokable]
    void IDictionary.Remove(object key)
    {
      if (key == null)
        throw new ArgumentNullException("key");
      if (!(key is TKey))
        return;
      TValue obj;
      this.TryRemove((TKey) key, out obj);
    }

    [__DynamicallyInvokable]
    void ICollection.CopyTo(Array array, int index)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      if (index < 0)
        throw new ArgumentOutOfRangeException("index", this.GetResource("ConcurrentDictionary_IndexIsNegative"));
      int locksAcquired = 0;
      try
      {
        this.AcquireAllLocks(ref locksAcquired);
        ConcurrentDictionary<TKey, TValue>.Tables tables = this.m_tables;
        int num = 0;
        for (int index1 = 0; index1 < tables.m_locks.Length && num >= 0; ++index1)
          num += tables.m_countPerLock[index1];
        if (array.Length - num < index || num < 0)
          throw new ArgumentException(this.GetResource("ConcurrentDictionary_ArrayNotLargeEnough"));
        KeyValuePair<TKey, TValue>[] array1 = array as KeyValuePair<TKey, TValue>[];
        if (array1 != null)
        {
          this.CopyToPairs(array1, index);
        }
        else
        {
          DictionaryEntry[] array2 = array as DictionaryEntry[];
          if (array2 != null)
          {
            this.CopyToEntries(array2, index);
          }
          else
          {
            object[] array3 = array as object[];
            if (array3 == null)
              throw new ArgumentException(this.GetResource("ConcurrentDictionary_ArrayIncorrectType"), "array");
            this.CopyToObjects(array3, index);
          }
        }
      }
      finally
      {
        this.ReleaseLocks(0, locksAcquired);
      }
    }

    private void GrowTable(ConcurrentDictionary<TKey, TValue>.Tables tables, IEqualityComparer<TKey> newComparer, bool regenerateHashKeys, int rehashCount)
    {
      int locksAcquired = 0;
      try
      {
        this.AcquireLocks(0, 1, ref locksAcquired);
        if (regenerateHashKeys && rehashCount == this.m_keyRehashCount)
        {
          tables = this.m_tables;
        }
        else
        {
          if (tables != this.m_tables)
            return;
          long num = 0;
          for (int index = 0; index < tables.m_countPerLock.Length; ++index)
            num += (long) tables.m_countPerLock[index];
          if (num < (long) (tables.m_buckets.Length / 4))
          {
            this.m_budget = 2 * this.m_budget;
            if (this.m_budget >= 0)
              return;
            this.m_budget = int.MaxValue;
            return;
          }
        }
        int length1 = 0;
        bool flag = false;
        try
        {
          length1 = checked (tables.m_buckets.Length * 2 + 1);
          while (length1 % 3 == 0 || length1 % 5 == 0 || length1 % 7 == 0)
            checked { length1 += 2; }
          if (length1 > 2146435071)
            flag = true;
        }
        catch (OverflowException ex)
        {
          flag = true;
        }
        if (flag)
        {
          length1 = 2146435071;
          this.m_budget = int.MaxValue;
        }
        this.AcquireLocks(1, tables.m_locks.Length, ref locksAcquired);
        object[] locks = tables.m_locks;
        if (this.m_growLockArray && tables.m_locks.Length < 1024)
        {
          locks = new object[tables.m_locks.Length * 2];
          Array.Copy((Array) tables.m_locks, (Array) locks, tables.m_locks.Length);
          for (int length2 = tables.m_locks.Length; length2 < locks.Length; ++length2)
            locks[length2] = new object();
        }
        ConcurrentDictionary<TKey, TValue>.Node[] buckets = new ConcurrentDictionary<TKey, TValue>.Node[length1];
        int[] countPerLock = new int[locks.Length];
        ConcurrentDictionary<TKey, TValue>.Node node1;
        for (int index = 0; index < tables.m_buckets.Length; ++index)
        {
          for (ConcurrentDictionary<TKey, TValue>.Node node2 = tables.m_buckets[index]; node2 != null; node2 = node1)
          {
            node1 = node2.m_next;
            int hashcode = node2.m_hashcode;
            if (regenerateHashKeys)
              hashcode = newComparer.GetHashCode(node2.m_key);
            int bucketNo;
            int lockNo;
            this.GetBucketAndLockNo(hashcode, out bucketNo, out lockNo, buckets.Length, locks.Length);
            buckets[bucketNo] = new ConcurrentDictionary<TKey, TValue>.Node(node2.m_key, node2.m_value, hashcode, buckets[bucketNo]);
            checked { ++countPerLock[lockNo]; }
          }
        }
        if (regenerateHashKeys)
          this.m_keyRehashCount = this.m_keyRehashCount + 1;
        this.m_budget = Math.Max(1, buckets.Length / locks.Length);
        this.m_tables = new ConcurrentDictionary<TKey, TValue>.Tables(buckets, locks, countPerLock, newComparer);
      }
      finally
      {
        this.ReleaseLocks(0, locksAcquired);
      }
    }

    private void GetBucketAndLockNo(int hashcode, out int bucketNo, out int lockNo, int bucketCount, int lockCount)
    {
      bucketNo = (hashcode & int.MaxValue) % bucketCount;
      lockNo = bucketNo % lockCount;
    }

    private void AcquireAllLocks(ref int locksAcquired)
    {
      if (CDSCollectionETWBCLProvider.Log.IsEnabled())
        CDSCollectionETWBCLProvider.Log.ConcurrentDictionary_AcquiringAllLocks(this.m_tables.m_buckets.Length);
      this.AcquireLocks(0, 1, ref locksAcquired);
      this.AcquireLocks(1, this.m_tables.m_locks.Length, ref locksAcquired);
    }

    private void AcquireLocks(int fromInclusive, int toExclusive, ref int locksAcquired)
    {
      object[] objArray = this.m_tables.m_locks;
      for (int index = fromInclusive; index < toExclusive; ++index)
      {
        bool lockTaken = false;
        try
        {
          Monitor.Enter(objArray[index], ref lockTaken);
        }
        finally
        {
          if (lockTaken)
            ++locksAcquired;
        }
      }
    }

    private void ReleaseLocks(int fromInclusive, int toExclusive)
    {
      for (int index = fromInclusive; index < toExclusive; ++index)
        Monitor.Exit(this.m_tables.m_locks[index]);
    }

    private ReadOnlyCollection<TKey> GetKeys()
    {
      int locksAcquired = 0;
      try
      {
        this.AcquireAllLocks(ref locksAcquired);
        List<TKey> keyList = new List<TKey>();
        for (int index = 0; index < this.m_tables.m_buckets.Length; ++index)
        {
          for (ConcurrentDictionary<TKey, TValue>.Node node = this.m_tables.m_buckets[index]; node != null; node = node.m_next)
            keyList.Add(node.m_key);
        }
        return new ReadOnlyCollection<TKey>((IList<TKey>) keyList);
      }
      finally
      {
        this.ReleaseLocks(0, locksAcquired);
      }
    }

    private ReadOnlyCollection<TValue> GetValues()
    {
      int locksAcquired = 0;
      try
      {
        this.AcquireAllLocks(ref locksAcquired);
        List<TValue> objList = new List<TValue>();
        for (int index = 0; index < this.m_tables.m_buckets.Length; ++index)
        {
          for (ConcurrentDictionary<TKey, TValue>.Node node = this.m_tables.m_buckets[index]; node != null; node = node.m_next)
            objList.Add(node.m_value);
        }
        return new ReadOnlyCollection<TValue>((IList<TValue>) objList);
      }
      finally
      {
        this.ReleaseLocks(0, locksAcquired);
      }
    }

    [Conditional("DEBUG")]
    private void Assert(bool condition)
    {
    }

    private string GetResource(string key)
    {
      return Environment.GetResourceString(key);
    }

    [OnSerializing]
    private void OnSerializing(StreamingContext context)
    {
      ConcurrentDictionary<TKey, TValue>.Tables tables = this.m_tables;
      this.m_serializationArray = this.ToArray();
      this.m_serializationConcurrencyLevel = tables.m_locks.Length;
      this.m_serializationCapacity = tables.m_buckets.Length;
      this.m_comparer = (IEqualityComparer<TKey>) HashHelpers.GetEqualityComparerForSerialization((object) tables.m_comparer);
    }

    [OnDeserialized]
    private void OnDeserialized(StreamingContext context)
    {
      KeyValuePair<TKey, TValue>[] keyValuePairArray = this.m_serializationArray;
      ConcurrentDictionary<TKey, TValue>.Node[] buckets = new ConcurrentDictionary<TKey, TValue>.Node[this.m_serializationCapacity];
      int[] countPerLock = new int[this.m_serializationConcurrencyLevel];
      object[] locks = new object[this.m_serializationConcurrencyLevel];
      for (int index = 0; index < locks.Length; ++index)
        locks[index] = new object();
      this.m_tables = new ConcurrentDictionary<TKey, TValue>.Tables(buckets, locks, countPerLock, this.m_comparer);
      this.InitializeFromCollection((IEnumerable<KeyValuePair<TKey, TValue>>) keyValuePairArray);
      this.m_serializationArray = (KeyValuePair<TKey, TValue>[]) null;
    }

    private class Tables
    {
      internal readonly ConcurrentDictionary<TKey, TValue>.Node[] m_buckets;
      internal readonly object[] m_locks;
      internal volatile int[] m_countPerLock;
      internal readonly IEqualityComparer<TKey> m_comparer;

      internal Tables(ConcurrentDictionary<TKey, TValue>.Node[] buckets, object[] locks, int[] countPerLock, IEqualityComparer<TKey> comparer)
      {
        this.m_buckets = buckets;
        this.m_locks = locks;
        this.m_countPerLock = countPerLock;
        this.m_comparer = comparer;
      }
    }

    private class Node
    {
      internal TKey m_key;
      internal TValue m_value;
      internal volatile ConcurrentDictionary<TKey, TValue>.Node m_next;
      internal int m_hashcode;

      internal Node(TKey key, TValue value, int hashcode, ConcurrentDictionary<TKey, TValue>.Node next)
      {
        this.m_key = key;
        this.m_value = value;
        this.m_next = next;
        this.m_hashcode = hashcode;
      }
    }

    private class DictionaryEnumerator : IDictionaryEnumerator, IEnumerator
    {
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

      internal DictionaryEnumerator(ConcurrentDictionary<TKey, TValue> dictionary)
      {
        this.m_enumerator = dictionary.GetEnumerator();
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
  }
}
