// Decompiled with JetBrains decompiler
// Type: System.Collections.Hashtable
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections
{
  /// <summary>表示根据键的哈希代码进行组织的键/值对的集合。若要浏览此类型的.NET Framework 源代码，请参阅参考源。</summary>
  /// <filterpriority>1</filterpriority>
  [DebuggerTypeProxy(typeof (Hashtable.HashtableDebugView))]
  [DebuggerDisplay("Count = {Count}")]
  [ComVisible(true)]
  [Serializable]
  public class Hashtable : IDictionary, ICollection, IEnumerable, ISerializable, IDeserializationCallback, ICloneable
  {
    internal const int HashPrime = 101;
    private const int InitialSize = 3;
    private const string LoadFactorName = "LoadFactor";
    private const string VersionName = "Version";
    private const string ComparerName = "Comparer";
    private const string HashCodeProviderName = "HashCodeProvider";
    private const string HashSizeName = "HashSize";
    private const string KeysName = "Keys";
    private const string ValuesName = "Values";
    private const string KeyComparerName = "KeyComparer";
    private Hashtable.bucket[] buckets;
    private int count;
    private int occupancy;
    private int loadsize;
    private float loadFactor;
    private volatile int version;
    private volatile bool isWriterInProgress;
    private ICollection keys;
    private ICollection values;
    private IEqualityComparer _keycomparer;
    private object _syncRoot;

    /// <summary>获取或设置可分配哈希代码的对象。</summary>
    /// <returns>可分配哈希代码的对象。</returns>
    /// <exception cref="T:System.ArgumentException">该属性被设置为某个值，但哈希表是使用 <see cref="T:System.Collections.IEqualityComparer" /> 创建的。</exception>
    [Obsolete("Please use EqualityComparer property.")]
    protected IHashCodeProvider hcp
    {
      get
      {
        if (this._keycomparer is CompatibleComparer)
          return ((CompatibleComparer) this._keycomparer).HashCodeProvider;
        if (this._keycomparer == null)
          return (IHashCodeProvider) null;
        throw new ArgumentException(Environment.GetResourceString("Arg_CannotMixComparisonInfrastructure"));
      }
      set
      {
        if (this._keycomparer is CompatibleComparer)
        {
          this._keycomparer = (IEqualityComparer) new CompatibleComparer(((CompatibleComparer) this._keycomparer).Comparer, value);
        }
        else
        {
          if (this._keycomparer != null)
            throw new ArgumentException(Environment.GetResourceString("Arg_CannotMixComparisonInfrastructure"));
          this._keycomparer = (IEqualityComparer) new CompatibleComparer((IComparer) null, value);
        }
      }
    }

    /// <summary>获取或设置要用于 <see cref="T:System.Collections.Hashtable" /> 的 <see cref="T:System.Collections.IComparer" />。</summary>
    /// <returns>要用于 <see cref="T:System.Collections.Hashtable" /> 的 <see cref="T:System.Collections.IComparer" />。</returns>
    /// <exception cref="T:System.ArgumentException">该属性被设置为某个值，但哈希表是使用 <see cref="T:System.Collections.IEqualityComparer" /> 创建的。</exception>
    [Obsolete("Please use KeyComparer properties.")]
    protected IComparer comparer
    {
      get
      {
        if (this._keycomparer is CompatibleComparer)
          return ((CompatibleComparer) this._keycomparer).Comparer;
        if (this._keycomparer == null)
          return (IComparer) null;
        throw new ArgumentException(Environment.GetResourceString("Arg_CannotMixComparisonInfrastructure"));
      }
      set
      {
        if (this._keycomparer is CompatibleComparer)
        {
          CompatibleComparer compatibleComparer = (CompatibleComparer) this._keycomparer;
          this._keycomparer = (IEqualityComparer) new CompatibleComparer(value, compatibleComparer.HashCodeProvider);
        }
        else
        {
          if (this._keycomparer != null)
            throw new ArgumentException(Environment.GetResourceString("Arg_CannotMixComparisonInfrastructure"));
          this._keycomparer = (IEqualityComparer) new CompatibleComparer(value, (IHashCodeProvider) null);
        }
      }
    }

    /// <summary>获取要用于 <see cref="T:System.Collections.Hashtable" /> 的 <see cref="T:System.Collections.IEqualityComparer" />。</summary>
    /// <returns>要用于 <see cref="T:System.Collections.Hashtable" /> 的 <see cref="T:System.Collections.IEqualityComparer" />。</returns>
    /// <exception cref="T:System.ArgumentException">该属性被设置为某个值，但哈希表是使用 <see cref="T:System.Collections.IHashCodeProvider" /> 和 <see cref="T:System.Collections.IComparer" /> 创建的。</exception>
    protected IEqualityComparer EqualityComparer
    {
      get
      {
        return this._keycomparer;
      }
    }

    /// <summary>获取或设置与指定的键关联的值。</summary>
    /// <returns>与指定的键相关联的值。如果未找到指定的键，尝试获取它将返回 null，尝试设置它将使用指定的键创建新元素。</returns>
    /// <param name="key">要获取或设置其值的键。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> 为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">设置该属性，而且 <see cref="T:System.Collections.Hashtable" /> 为只读。- 或 -设置该属性，集合中不存在 <paramref name="key" />，而且 <see cref="T:System.Collections.Hashtable" /> 具有固定大小。</exception>
    /// <filterpriority>2</filterpriority>
    public virtual object this[object key]
    {
      get
      {
        if (key == null)
          throw new ArgumentNullException("key", Environment.GetResourceString("ArgumentNull_Key"));
        Hashtable.bucket[] bucketArray = this.buckets;
        uint seed;
        uint incr;
        uint num1 = this.InitHash(key, bucketArray.Length, out seed, out incr);
        int num2 = 0;
        int index = (int) (seed % (uint) bucketArray.Length);
        Hashtable.bucket bucket;
        do
        {
          int num3 = 0;
          int num4;
          do
          {
            num4 = this.version;
            bucket = bucketArray[index];
            if (++num3 % 8 == 0)
              Thread.Sleep(1);
          }
          while (this.isWriterInProgress || num4 != this.version);
          if (bucket.key == null)
            return (object) null;
          if ((long) (bucket.hash_coll & int.MaxValue) == (long) num1 && this.KeyEquals(bucket.key, key))
            return bucket.val;
          index = (int) (((long) index + (long) incr) % (long) (uint) bucketArray.Length);
        }
        while (bucket.hash_coll < 0 && ++num2 < bucketArray.Length);
        return (object) null;
      }
      set
      {
        this.Insert(key, value, false);
      }
    }

    /// <summary>获取一个值，该值指示 <see cref="T:System.Collections.Hashtable" /> 是否为只读。</summary>
    /// <returns>true if the <see cref="T:System.Collections.Hashtable" /> is read-only; otherwise, false.默认值为 false。</returns>
    /// <filterpriority>2</filterpriority>
    public virtual bool IsReadOnly
    {
      get
      {
        return false;
      }
    }

    /// <summary>获取一个值，该值指示 <see cref="T:System.Collections.Hashtable" /> 是否具有固定大小。</summary>
    /// <returns>true if the <see cref="T:System.Collections.Hashtable" /> has a fixed size; otherwise, false.默认值为 false。</returns>
    /// <filterpriority>2</filterpriority>
    public virtual bool IsFixedSize
    {
      get
      {
        return false;
      }
    }

    /// <summary>获取一个值，该值指示是否同步对 <see cref="T:System.Collections.Hashtable" /> 的访问（线程安全）。</summary>
    /// <returns>true if access to the <see cref="T:System.Collections.Hashtable" /> is synchronized (thread safe); otherwise, false.默认值为 false。</returns>
    /// <filterpriority>2</filterpriority>
    public virtual bool IsSynchronized
    {
      get
      {
        return false;
      }
    }

    /// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the keys in the <see cref="T:System.Collections.Hashtable" />.</summary>
    /// <returns>An <see cref="T:System.Collections.ICollection" /> containing the keys in the <see cref="T:System.Collections.Hashtable" />.</returns>
    /// <filterpriority>1</filterpriority>
    public virtual ICollection Keys
    {
      get
      {
        if (this.keys == null)
          this.keys = (ICollection) new Hashtable.KeyCollection(this);
        return this.keys;
      }
    }

    /// <summary>获取包含 <see cref="T:System.Collections.Hashtable" /> 中的值的 <see cref="T:System.Collections.ICollection" />。</summary>
    /// <returns>一个 <see cref="T:System.Collections.ICollection" />，它包含 <see cref="T:System.Collections.Hashtable" /> 中的值。</returns>
    /// <filterpriority>1</filterpriority>
    public virtual ICollection Values
    {
      get
      {
        if (this.values == null)
          this.values = (ICollection) new Hashtable.ValueCollection(this);
        return this.values;
      }
    }

    /// <summary>获取可用于同步对 <see cref="T:System.Collections.Hashtable" /> 的访问的对象。</summary>
    /// <returns>可用于同步对 <see cref="T:System.Collections.Hashtable" /> 的访问的对象。</returns>
    /// <filterpriority>2</filterpriority>
    public virtual object SyncRoot
    {
      get
      {
        if (this._syncRoot == null)
          Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), (object) null);
        return this._syncRoot;
      }
    }

    /// <summary>获取包含在 <see cref="T:System.Collections.Hashtable" /> 中的键/值对的数目。</summary>
    /// <returns>包含在 <see cref="T:System.Collections.Hashtable" /> 中的键/值对的数目。</returns>
    /// <filterpriority>1</filterpriority>
    public virtual int Count
    {
      get
      {
        return this.count;
      }
    }

    internal Hashtable(bool trash)
    {
    }

    /// <summary>使用默认的初始容量、加载因子、哈希代码提供程序和比较器来初始化 <see cref="T:System.Collections.Hashtable" /> 类的新的空实例。</summary>
    public Hashtable()
      : this(0, 1f)
    {
    }

    /// <summary>使用指定的初始容量、默认加载因子、默认哈希代码提供程序和默认比较器来初始化 <see cref="T:System.Collections.Hashtable" /> 类的新的空实例。</summary>
    /// <param name="capacity">
    /// <see cref="T:System.Collections.Hashtable" /> 对象最初可包含的元素的近似数目。 </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="capacity" /> 小于零。</exception>
    public Hashtable(int capacity)
      : this(capacity, 1f)
    {
    }

    /// <summary>使用指定的初始容量、指定的加载因子、默认的哈希代码提供程序和默认比较器来初始化 <see cref="T:System.Collections.Hashtable" /> 类的新的空实例。</summary>
    /// <param name="capacity">
    /// <see cref="T:System.Collections.Hashtable" /> 对象最初可包含的元素的近似数目。</param>
    /// <param name="loadFactor">0.1 到 1.0 范围内的数字，再乘以提供最佳性能的默认值。结果是元素与存储桶的最大比率。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="capacity" /> 小于零。- 或 - <paramref name="loadFactor" /> 小于 0.1。- 或 - <paramref name="loadFactor" /> 大于 1.0。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="capacity" /> 导致溢出。</exception>
    public Hashtable(int capacity, float loadFactor)
    {
      if (capacity < 0)
        throw new ArgumentOutOfRangeException("capacity", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if ((double) loadFactor < 0.100000001490116 || (double) loadFactor > 1.0)
        throw new ArgumentOutOfRangeException("loadFactor", Environment.GetResourceString("ArgumentOutOfRange_HashtableLoadFactor", (object) 0.1, (object) 1.0));
      this.loadFactor = 0.72f * loadFactor;
      double num = (double) capacity / (double) this.loadFactor;
      if (num > (double) int.MaxValue)
        throw new ArgumentException(Environment.GetResourceString("Arg_HTCapacityOverflow"));
      int length = num > 3.0 ? HashHelpers.GetPrime((int) num) : 3;
      this.buckets = new Hashtable.bucket[length];
      this.loadsize = (int) ((double) this.loadFactor * (double) length);
      this.isWriterInProgress = false;
    }

    /// <summary>使用指定的初始容量、加载因子、哈希代码提供程序和比较器来初始化 <see cref="T:System.Collections.Hashtable" /> 类的新的空实例。</summary>
    /// <param name="capacity">
    /// <see cref="T:System.Collections.Hashtable" /> 对象最初可包含的元素的近似数目。</param>
    /// <param name="loadFactor">0.1 到 1.0 范围内的数字，再乘以提供最佳性能的默认值。结果是元素与存储桶的最大比率。</param>
    /// <param name="hcp">
    /// <see cref="T:System.Collections.IHashCodeProvider" /> 对象，用于为 <see cref="T:System.Collections.Hashtable" /> 中的所有键提供哈希代码。- 或 - null 使用默认哈希代码提供程序，该提供程序是每一个键的 <see cref="M:System.Object.GetHashCode" /> 实现。</param>
    /// <param name="comparer">
    /// <see cref="T:System.Collections.IComparer" /> 对象，用于确定两个键是否相等。- 或 - null 使用默认比较器，该比较器是每一个键的 <see cref="M:System.Object.Equals(System.Object)" /> 实现。 </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="capacity" /> 小于零。- 或 - <paramref name="loadFactor" /> 小于 0.1。- 或 - <paramref name="loadFactor" /> 大于 1.0。</exception>
    [Obsolete("Please use Hashtable(int, float, IEqualityComparer) instead.")]
    public Hashtable(int capacity, float loadFactor, IHashCodeProvider hcp, IComparer comparer)
      : this(capacity, loadFactor)
    {
      if (hcp == null && comparer == null)
        this._keycomparer = (IEqualityComparer) null;
      else
        this._keycomparer = (IEqualityComparer) new CompatibleComparer(comparer, hcp);
    }

    /// <summary>使用指定的初始容量、加载因子和 <see cref="T:System.Collections.IEqualityComparer" /> 对象来初始化 <see cref="T:System.Collections.Hashtable" /> 类的新的空实例。</summary>
    /// <param name="capacity">
    /// <see cref="T:System.Collections.Hashtable" /> 对象最初可包含的元素的近似数目。</param>
    /// <param name="loadFactor">0.1 到 1.0 范围内的数字，再乘以提供最佳性能的默认值。结果是元素与存储桶的最大比率。</param>
    /// <param name="equalityComparer">
    /// <see cref="T:System.Collections.IEqualityComparer" /> 对象，用于定义要用来处理 <see cref="T:System.Collections.Hashtable" /> 的哈希代码提供程序和比较器。- 或 - null，则使用默认哈希代码提供程序和默认比较器。默认哈希代码提供程序是各个键的 <see cref="M:System.Object.GetHashCode" /> 实现，而默认比较器是各个键的 <see cref="M:System.Object.Equals(System.Object)" /> 实现。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="capacity" /> 小于零。- 或 - <paramref name="loadFactor" /> 小于 0.1。- 或 - <paramref name="loadFactor" /> 大于 1.0。</exception>
    public Hashtable(int capacity, float loadFactor, IEqualityComparer equalityComparer)
      : this(capacity, loadFactor)
    {
      this._keycomparer = equalityComparer;
    }

    /// <summary>使用默认初始容量、默认加载因子、指定的哈希代码提供程序和指定的比较器来初始化 <see cref="T:System.Collections.Hashtable" /> 类的新的空实例。</summary>
    /// <param name="hcp">
    /// <see cref="T:System.Collections.IHashCodeProvider" /> 对象，用于为 <see cref="T:System.Collections.Hashtable" /> 对象中的所有键提供哈希代码。- 或 - null 使用默认哈希代码提供程序，该提供程序是每一个键的 <see cref="M:System.Object.GetHashCode" /> 实现。</param>
    /// <param name="comparer">
    /// <see cref="T:System.Collections.IComparer" /> 对象，用于确定两个键是否相等。- 或 - null 使用默认比较器，该比较器是每一个键的 <see cref="M:System.Object.Equals(System.Object)" /> 实现。</param>
    [Obsolete("Please use Hashtable(IEqualityComparer) instead.")]
    public Hashtable(IHashCodeProvider hcp, IComparer comparer)
      : this(0, 1f, hcp, comparer)
    {
    }

    /// <summary>使用默认的初始容量、默认加载因子和指定的 <see cref="T:System.Collections.IEqualityComparer" /> 对象来初始化 <see cref="T:System.Collections.Hashtable" /> 类的新的空实例。</summary>
    /// <param name="equalityComparer">
    /// <see cref="T:System.Collections.IEqualityComparer" /> 对象，用于定义要与 <see cref="T:System.Collections.Hashtable" /> 对象一起使用的哈希代码提供程序和比较器。- 或 - null，则使用默认哈希代码提供程序和默认比较器。默认哈希代码提供程序是各个键的 <see cref="M:System.Object.GetHashCode" /> 实现，而默认比较器是各个键的 <see cref="M:System.Object.Equals(System.Object)" /> 实现。</param>
    public Hashtable(IEqualityComparer equalityComparer)
      : this(0, 1f, equalityComparer)
    {
    }

    /// <summary>使用指定的初始容量、哈希代码提供程序、比较器和默认加载因子来初始化 <see cref="T:System.Collections.Hashtable" /> 类的新的空实例。</summary>
    /// <param name="capacity">
    /// <see cref="T:System.Collections.Hashtable" /> 对象最初可包含的元素的近似数目。</param>
    /// <param name="hcp">
    /// <see cref="T:System.Collections.IHashCodeProvider" /> 对象，用于为 <see cref="T:System.Collections.Hashtable" /> 中的所有键提供哈希代码。- 或 - null 使用默认哈希代码提供程序，该提供程序是每一个键的 <see cref="M:System.Object.GetHashCode" /> 实现。</param>
    /// <param name="comparer">
    /// <see cref="T:System.Collections.IComparer" /> 对象，用于确定两个键是否相等。- 或 - null 使用默认比较器，该比较器是每一个键的 <see cref="M:System.Object.Equals(System.Object)" /> 实现。 </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="capacity" /> 小于零。</exception>
    [Obsolete("Please use Hashtable(int, IEqualityComparer) instead.")]
    public Hashtable(int capacity, IHashCodeProvider hcp, IComparer comparer)
      : this(capacity, 1f, hcp, comparer)
    {
    }

    /// <summary>使用指定的初始容量和 <see cref="T:System.Collections.IEqualityComparer" /> 以及默认的加载因子来初始化 <see cref="T:System.Collections.Hashtable" /> 类的新的空实例。</summary>
    /// <param name="capacity">
    /// <see cref="T:System.Collections.Hashtable" /> 对象最初可包含的元素的近似数目。</param>
    /// <param name="equalityComparer">
    /// <see cref="T:System.Collections.IEqualityComparer" /> 对象，用于定义要用来处理 <see cref="T:System.Collections.Hashtable" /> 的哈希代码提供程序和比较器。- 或 - null，则使用默认哈希代码提供程序和默认比较器。默认哈希代码提供程序是各个键的 <see cref="M:System.Object.GetHashCode" /> 实现，而默认比较器是各个键的 <see cref="M:System.Object.Equals(System.Object)" /> 实现。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="capacity" /> 小于零。</exception>
    public Hashtable(int capacity, IEqualityComparer equalityComparer)
      : this(capacity, 1f, equalityComparer)
    {
    }

    /// <summary>通过将指定字典中的元素复制到新的 <see cref="T:System.Collections.Hashtable" /> 对象中，初始化 <see cref="T:System.Collections.Hashtable" /> 类的一个新实例。新 <see cref="T:System.Collections.Hashtable" /> 对象的初始容量等于复制的元素数，并且使用默认的加载因子、哈希代码提供程序和比较器。</summary>
    /// <param name="d">要复制到新 <see cref="T:System.Collections.Hashtable" /> 对象的 <see cref="T:System.Collections.IDictionary" /> 对象。 </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="d" /> 为 null。</exception>
    public Hashtable(IDictionary d)
      : this(d, 1f)
    {
    }

    /// <summary>通过将指定字典中的元素复制到新的 <see cref="T:System.Collections.Hashtable" /> 对象中，初始化 <see cref="T:System.Collections.Hashtable" /> 类的一个新实例。新 <see cref="T:System.Collections.Hashtable" /> 对象的初始容量等于复制的元素数，并且使用指定的加载因子、默认哈希代码提供程序和默认比较器。</summary>
    /// <param name="d">要复制到新 <see cref="T:System.Collections.Hashtable" /> 对象的 <see cref="T:System.Collections.IDictionary" /> 对象。</param>
    /// <param name="loadFactor">0.1 到 1.0 范围内的数字，再乘以提供最佳性能的默认值。结果是元素与存储桶的最大比率。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="d" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="loadFactor" /> 小于 0.1。- 或 - <paramref name="loadFactor" /> 大于 1.0。</exception>
    public Hashtable(IDictionary d, float loadFactor)
      : this(d, loadFactor, (IEqualityComparer) null)
    {
    }

    /// <summary>通过将指定字典中的元素复制到新的 <see cref="T:System.Collections.Hashtable" /> 对象中，初始化 <see cref="T:System.Collections.Hashtable" /> 类的一个新实例。新 <see cref="T:System.Collections.Hashtable" /> 对象的初始容量等于复制的元素数，并且使用默认的加载因子、指定的哈希代码提供程序和指定的比较器。此 API 已废弃不用。有关另类，请参见 <see cref="M:System.Collections.Hashtable.#ctor(System.Collections.IDictionary,System.Collections.IEqualityComparer)" />。</summary>
    /// <param name="d">要复制到新 <see cref="T:System.Collections.Hashtable" /> 对象的 <see cref="T:System.Collections.IDictionary" /> 对象。</param>
    /// <param name="hcp">
    /// <see cref="T:System.Collections.IHashCodeProvider" /> 对象，用于为 <see cref="T:System.Collections.Hashtable" /> 中的所有键提供哈希代码。- 或 - null 使用默认哈希代码提供程序，该提供程序是每一个键的 <see cref="M:System.Object.GetHashCode" /> 实现。</param>
    /// <param name="comparer">
    /// <see cref="T:System.Collections.IComparer" /> 对象，用于确定两个键是否相等。- 或 - null 使用默认比较器，该比较器是每一个键的 <see cref="M:System.Object.Equals(System.Object)" /> 实现。 </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="d" /> 为 null。</exception>
    [Obsolete("Please use Hashtable(IDictionary, IEqualityComparer) instead.")]
    public Hashtable(IDictionary d, IHashCodeProvider hcp, IComparer comparer)
      : this(d, 1f, hcp, comparer)
    {
    }

    /// <summary>通过将指定字典中的元素复制到新的 <see cref="T:System.Collections.Hashtable" /> 对象中，初始化 <see cref="T:System.Collections.Hashtable" /> 类的一个新实例。新 <see cref="T:System.Collections.Hashtable" /> 对象的初始容量等于复制的元素数，并且使用默认加载因子和指定的 <see cref="T:System.Collections.IEqualityComparer" /> 对象。</summary>
    /// <param name="d">要复制到新 <see cref="T:System.Collections.Hashtable" /> 对象的 <see cref="T:System.Collections.IDictionary" /> 对象。</param>
    /// <param name="equalityComparer">
    /// <see cref="T:System.Collections.IEqualityComparer" /> 对象，用于定义要用来处理 <see cref="T:System.Collections.Hashtable" /> 的哈希代码提供程序和比较器。- 或 - null，则使用默认哈希代码提供程序和默认比较器。默认哈希代码提供程序是各个键的 <see cref="M:System.Object.GetHashCode" /> 实现，而默认比较器是各个键的 <see cref="M:System.Object.Equals(System.Object)" /> 实现。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="d" /> 为 null。</exception>
    public Hashtable(IDictionary d, IEqualityComparer equalityComparer)
      : this(d, 1f, equalityComparer)
    {
    }

    /// <summary>通过将指定字典中的元素复制到新的 <see cref="T:System.Collections.Hashtable" /> 对象中，初始化 <see cref="T:System.Collections.Hashtable" /> 类的一个新实例。新 <see cref="T:System.Collections.Hashtable" /> 对象的初始容量等于复制的元素数，并且使用指定的加载因子、哈希代码提供程序和比较器。</summary>
    /// <param name="d">要复制到新 <see cref="T:System.Collections.Hashtable" /> 对象的 <see cref="T:System.Collections.IDictionary" /> 对象。</param>
    /// <param name="loadFactor">0.1 到 1.0 范围内的数字，再乘以提供最佳性能的默认值。结果是元素与存储桶的最大比率。</param>
    /// <param name="hcp">
    /// <see cref="T:System.Collections.IHashCodeProvider" /> 对象，用于为 <see cref="T:System.Collections.Hashtable" /> 中的所有键提供哈希代码。- 或 - null 使用默认哈希代码提供程序，该提供程序是每一个键的 <see cref="M:System.Object.GetHashCode" /> 实现。</param>
    /// <param name="comparer">
    /// <see cref="T:System.Collections.IComparer" /> 对象，用于确定两个键是否相等。- 或 - null 使用默认比较器，该比较器是每一个键的 <see cref="M:System.Object.Equals(System.Object)" /> 实现。 </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="d" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="loadFactor" /> 小于 0.1。- 或 - <paramref name="loadFactor" /> 大于 1.0。</exception>
    [Obsolete("Please use Hashtable(IDictionary, float, IEqualityComparer) instead.")]
    public Hashtable(IDictionary d, float loadFactor, IHashCodeProvider hcp, IComparer comparer)
      : this(d != null ? d.Count : 0, loadFactor, hcp, comparer)
    {
      if (d == null)
        throw new ArgumentNullException("d", Environment.GetResourceString("ArgumentNull_Dictionary"));
      IDictionaryEnumerator enumerator = d.GetEnumerator();
      while (enumerator.MoveNext())
        this.Add(enumerator.Key, enumerator.Value);
    }

    /// <summary>通过将指定字典中的元素复制到新的 <see cref="T:System.Collections.Hashtable" /> 对象中，初始化 <see cref="T:System.Collections.Hashtable" /> 类的一个新实例。新 <see cref="T:System.Collections.Hashtable" /> 对象的初始容量等于复制的元素数，并且使用指定的加载因子和 <see cref="T:System.Collections.IEqualityComparer" /> 对象。</summary>
    /// <param name="d">要复制到新 <see cref="T:System.Collections.Hashtable" /> 对象的 <see cref="T:System.Collections.IDictionary" /> 对象。</param>
    /// <param name="loadFactor">0.1 到 1.0 范围内的数字，再乘以提供最佳性能的默认值。结果是元素与存储桶的最大比率。</param>
    /// <param name="equalityComparer">
    /// <see cref="T:System.Collections.IEqualityComparer" /> 对象，用于定义要用来处理 <see cref="T:System.Collections.Hashtable" /> 的哈希代码提供程序和比较器。- 或 - null，则使用默认哈希代码提供程序和默认比较器。默认哈希代码提供程序是各个键的 <see cref="M:System.Object.GetHashCode" /> 实现，而默认比较器是各个键的 <see cref="M:System.Object.Equals(System.Object)" /> 实现。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="d" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="loadFactor" /> 小于 0.1。- 或 - <paramref name="loadFactor" /> 大于 1.0。</exception>
    public Hashtable(IDictionary d, float loadFactor, IEqualityComparer equalityComparer)
      : this(d != null ? d.Count : 0, loadFactor, equalityComparer)
    {
      if (d == null)
        throw new ArgumentNullException("d", Environment.GetResourceString("ArgumentNull_Dictionary"));
      IDictionaryEnumerator enumerator = d.GetEnumerator();
      while (enumerator.MoveNext())
        this.Add(enumerator.Key, enumerator.Value);
    }

    /// <summary>初始化 <see cref="T:System.Collections.Hashtable" /> 类的新的空实例，该实例可序列化且使用指定的 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 和 <see cref="T:System.Runtime.Serialization.StreamingContext" />。</summary>
    /// <param name="info">
    /// <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 对象，包含序列化 <see cref="T:System.Collections.Hashtable" /> 所需的信息。</param>
    /// <param name="context">
    /// <see cref="T:System.Runtime.Serialization.StreamingContext" /> 对象，该对象包含与 <see cref="T:System.Collections.Hashtable" /> 相关联的序列化流的源和目标。 </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="info" /> 为 null。</exception>
    protected Hashtable(SerializationInfo info, StreamingContext context)
    {
      HashHelpers.SerializationInfoTable.Add((object) this, info);
    }

    private uint InitHash(object key, int hashsize, out uint seed, out uint incr)
    {
      uint num = (uint) (this.GetHash(key) & int.MaxValue);
      seed = num;
      incr = 1U + seed * 101U % (uint) (hashsize - 1);
      return num;
    }

    /// <summary>将带有指定键和值的元素添加到 <see cref="T:System.Collections.Hashtable" /> 中。</summary>
    /// <param name="key">要添加的元素的键。</param>
    /// <param name="value">要添加的元素的值。该值可以为 null。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <see cref="T:System.Collections.Hashtable" /> 中已存在具有相同键的元素。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <see cref="T:System.Collections.Hashtable" /> 为只读。- 或 -<see cref="T:System.Collections.Hashtable" /> 具有固定大小。</exception>
    /// <filterpriority>1</filterpriority>
    public virtual void Add(object key, object value)
    {
      this.Insert(key, value, true);
    }

    /// <summary>从 <see cref="T:System.Collections.Hashtable" /> 中移除所有元素。</summary>
    /// <exception cref="T:System.NotSupportedException">
    /// <see cref="T:System.Collections.Hashtable" /> 为只读。 </exception>
    /// <filterpriority>1</filterpriority>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    public virtual void Clear()
    {
      if (this.count == 0 && this.occupancy == 0)
        return;
      Thread.BeginCriticalRegion();
      this.isWriterInProgress = true;
      for (int index = 0; index < this.buckets.Length; ++index)
      {
        this.buckets[index].hash_coll = 0;
        this.buckets[index].key = (object) null;
        this.buckets[index].val = (object) null;
      }
      this.count = 0;
      this.occupancy = 0;
      this.UpdateVersion();
      this.isWriterInProgress = false;
      Thread.EndCriticalRegion();
    }

    /// <summary>创建 <see cref="T:System.Collections.Hashtable" /> 的浅表副本。</summary>
    /// <returns>
    /// <see cref="T:System.Collections.Hashtable" /> 的浅表副本。</returns>
    /// <filterpriority>1</filterpriority>
    public virtual object Clone()
    {
      Hashtable.bucket[] bucketArray = this.buckets;
      Hashtable hashtable = new Hashtable(this.count, this._keycomparer);
      hashtable.version = this.version;
      hashtable.loadFactor = this.loadFactor;
      hashtable.count = 0;
      int length = bucketArray.Length;
      while (length > 0)
      {
        --length;
        object index = bucketArray[length].key;
        if (index != null && index != bucketArray)
          hashtable[index] = bucketArray[length].val;
      }
      return (object) hashtable;
    }

    /// <summary>确定 <see cref="T:System.Collections.Hashtable" /> 是否包含特定键。</summary>
    /// <returns>true if the <see cref="T:System.Collections.Hashtable" /> contains an element with the specified key; otherwise, false.</returns>
    /// <param name="key">要在 <see cref="T:System.Collections.Hashtable" /> 中定位的键。 </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> 为 null。 </exception>
    /// <filterpriority>1</filterpriority>
    public virtual bool Contains(object key)
    {
      return this.ContainsKey(key);
    }

    /// <summary>确定 <see cref="T:System.Collections.Hashtable" /> 是否包含特定键。</summary>
    /// <returns>true if the <see cref="T:System.Collections.Hashtable" /> contains an element with the specified key; otherwise, false.</returns>
    /// <param name="key">要在 <see cref="T:System.Collections.Hashtable" /> 中定位的键。 </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> 为 null。</exception>
    /// <filterpriority>1</filterpriority>
    public virtual bool ContainsKey(object key)
    {
      if (key == null)
        throw new ArgumentNullException("key", Environment.GetResourceString("ArgumentNull_Key"));
      Hashtable.bucket[] bucketArray = this.buckets;
      uint seed;
      uint incr;
      uint num1 = this.InitHash(key, bucketArray.Length, out seed, out incr);
      int num2 = 0;
      int index = (int) (seed % (uint) bucketArray.Length);
      Hashtable.bucket bucket;
      do
      {
        bucket = bucketArray[index];
        if (bucket.key == null)
          return false;
        if ((long) (bucket.hash_coll & int.MaxValue) == (long) num1 && this.KeyEquals(bucket.key, key))
          return true;
        index = (int) (((long) index + (long) incr) % (long) (uint) bucketArray.Length);
      }
      while (bucket.hash_coll < 0 && ++num2 < bucketArray.Length);
      return false;
    }

    /// <summary>确定 <see cref="T:System.Collections.Hashtable" /> 是否包含特定值。</summary>
    /// <returns>如果 <see cref="T:System.Collections.Hashtable" /> 包含带有指定的 <paramref name="value" /> 的元素，则为 true；否则为 false。</returns>
    /// <param name="value">要在 <see cref="T:System.Collections.Hashtable" /> 中定位的值。该值可以为 null。</param>
    /// <filterpriority>1</filterpriority>
    public virtual bool ContainsValue(object value)
    {
      if (value == null)
      {
        int length = this.buckets.Length;
        while (--length >= 0)
        {
          if (this.buckets[length].key != null && this.buckets[length].key != this.buckets && this.buckets[length].val == null)
            return true;
        }
      }
      else
      {
        int length = this.buckets.Length;
        while (--length >= 0)
        {
          object obj = this.buckets[length].val;
          if (obj != null && obj.Equals(value))
            return true;
        }
      }
      return false;
    }

    private void CopyKeys(Array array, int arrayIndex)
    {
      Hashtable.bucket[] bucketArray = this.buckets;
      int length = bucketArray.Length;
      while (--length >= 0)
      {
        object obj = bucketArray[length].key;
        if (obj != null && obj != this.buckets)
          array.SetValue(obj, arrayIndex++);
      }
    }

    private void CopyEntries(Array array, int arrayIndex)
    {
      Hashtable.bucket[] bucketArray = this.buckets;
      int length = bucketArray.Length;
      while (--length >= 0)
      {
        object key = bucketArray[length].key;
        if (key != null && key != this.buckets)
        {
          DictionaryEntry dictionaryEntry = new DictionaryEntry(key, bucketArray[length].val);
          array.SetValue((object) dictionaryEntry, arrayIndex++);
        }
      }
    }

    /// <summary>将 <see cref="T:System.Collections.Hashtable" /> 元素复制到一维 <see cref="T:System.Array" /> 实例中的指定索引位置。</summary>
    /// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the <see cref="T:System.Collections.DictionaryEntry" /> objects copied from <see cref="T:System.Collections.Hashtable" />.<see cref="T:System.Array" /> 必须具有从零开始的索引。</param>
    /// <param name="arrayIndex">
    /// <paramref name="array" /> 中从零开始的索引，从此索引处开始进行复制。 </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="arrayIndex" /> 小于零。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="array" /> 是多维的。- 或 -源 <see cref="T:System.Collections.Hashtable" /> 中的元素数目大于从 <paramref name="arrayIndex" /> 到目标 <paramref name="array" /> 末尾之间的可用空间。</exception>
    /// <exception cref="T:System.InvalidCastException">源 <see cref="T:System.Collections.Hashtable" /> 的类型无法自动转换为目标 <paramref name="array" /> 的类型。</exception>
    /// <filterpriority>2</filterpriority>
    public virtual void CopyTo(Array array, int arrayIndex)
    {
      if (array == null)
        throw new ArgumentNullException("array", Environment.GetResourceString("ArgumentNull_Array"));
      if (array.Rank != 1)
        throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
      if (arrayIndex < 0)
        throw new ArgumentOutOfRangeException("arrayIndex", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (array.Length - arrayIndex < this.Count)
        throw new ArgumentException(Environment.GetResourceString("Arg_ArrayPlusOffTooSmall"));
      this.CopyEntries(array, arrayIndex);
    }

    internal virtual KeyValuePairs[] ToKeyValuePairsArray()
    {
      KeyValuePairs[] keyValuePairsArray = new KeyValuePairs[this.count];
      int num = 0;
      Hashtable.bucket[] bucketArray = this.buckets;
      int length = bucketArray.Length;
      while (--length >= 0)
      {
        object key = bucketArray[length].key;
        if (key != null && key != this.buckets)
          keyValuePairsArray[num++] = new KeyValuePairs(key, bucketArray[length].val);
      }
      return keyValuePairsArray;
    }

    private void CopyValues(Array array, int arrayIndex)
    {
      Hashtable.bucket[] bucketArray = this.buckets;
      int length = bucketArray.Length;
      while (--length >= 0)
      {
        object obj = bucketArray[length].key;
        if (obj != null && obj != this.buckets)
          array.SetValue(bucketArray[length].val, arrayIndex++);
      }
    }

    private void expand()
    {
      this.rehash(HashHelpers.ExpandPrime(this.buckets.Length), false);
    }

    private void rehash()
    {
      this.rehash(this.buckets.Length, false);
    }

    private void UpdateVersion()
    {
      this.version = this.version + 1;
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    private void rehash(int newsize, bool forceNewHashCode)
    {
      this.occupancy = 0;
      Hashtable.bucket[] newBuckets = new Hashtable.bucket[newsize];
      for (int index = 0; index < this.buckets.Length; ++index)
      {
        Hashtable.bucket bucket = this.buckets[index];
        if (bucket.key != null && bucket.key != this.buckets)
        {
          int hashcode = (forceNewHashCode ? this.GetHash(bucket.key) : bucket.hash_coll) & int.MaxValue;
          this.putEntry(newBuckets, bucket.key, bucket.val, hashcode);
        }
      }
      Thread.BeginCriticalRegion();
      this.isWriterInProgress = true;
      this.buckets = newBuckets;
      this.loadsize = (int) ((double) this.loadFactor * (double) newsize);
      this.UpdateVersion();
      this.isWriterInProgress = false;
      Thread.EndCriticalRegion();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) new Hashtable.HashtableEnumerator(this, 3);
    }

    /// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> that iterates through the <see cref="T:System.Collections.Hashtable" />.</summary>
    /// <returns>用于 <see cref="T:System.Collections.Hashtable" /> 的 <see cref="T:System.Collections.IDictionaryEnumerator" />。</returns>
    /// <filterpriority>2</filterpriority>
    public virtual IDictionaryEnumerator GetEnumerator()
    {
      return (IDictionaryEnumerator) new Hashtable.HashtableEnumerator(this, 3);
    }

    /// <summary>返回指定键的哈希代码。</summary>
    /// <returns>
    /// <paramref name="key" /> 的哈希代码。</returns>
    /// <param name="key">
    /// <see cref="T:System.Object" />，将为其返回哈希代码。</param>
    /// <exception cref="T:System.NullReferenceException">
    /// <paramref name="key" /> 为 null。</exception>
    protected virtual int GetHash(object key)
    {
      if (this._keycomparer != null)
        return this._keycomparer.GetHashCode(key);
      return key.GetHashCode();
    }

    /// <summary>将特定 <see cref="T:System.Object" /> 与 <see cref="T:System.Collections.Hashtable" /> 中的特定键进行比较。</summary>
    /// <returns>如果 <paramref name="item" /> 和 <paramref name="key" /> 相等，则为 true；否则为 false。</returns>
    /// <param name="item">要与 <paramref name="key" /> 进行比较的 <see cref="T:System.Object" />。</param>
    /// <param name="key">要与 <paramref name="item" /> 进行比较的 <see cref="T:System.Collections.Hashtable" /> 中的键。 </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="item" /> 为 null。- 或 - <paramref name="key" /> 为 null。</exception>
    protected virtual bool KeyEquals(object item, object key)
    {
      if (this.buckets == item)
        return false;
      if (item == key)
        return true;
      if (this._keycomparer != null)
        return this._keycomparer.Equals(item, key);
      if (item != null)
        return item.Equals(key);
      return false;
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    private void Insert(object key, object nvalue, bool add)
    {
      if (key == null)
        throw new ArgumentNullException("key", Environment.GetResourceString("ArgumentNull_Key"));
      if (this.count >= this.loadsize)
        this.expand();
      else if (this.occupancy > this.loadsize && this.count > 100)
        this.rehash();
      uint seed;
      uint incr;
      uint num1 = this.InitHash(key, this.buckets.Length, out seed, out incr);
      int num2 = 0;
      int index1 = -1;
      int index2 = (int) (seed % (uint) this.buckets.Length);
      do
      {
        if (index1 == -1 && this.buckets[index2].key == this.buckets && this.buckets[index2].hash_coll < 0)
          index1 = index2;
        if (this.buckets[index2].key == null || this.buckets[index2].key == this.buckets && ((long) this.buckets[index2].hash_coll & 2147483648L) == 0L)
        {
          if (index1 != -1)
            index2 = index1;
          Thread.BeginCriticalRegion();
          this.isWriterInProgress = true;
          this.buckets[index2].val = nvalue;
          this.buckets[index2].key = key;
          this.buckets[index2].hash_coll |= (int) num1;
          this.count = this.count + 1;
          this.UpdateVersion();
          this.isWriterInProgress = false;
          Thread.EndCriticalRegion();
          if (num2 <= 100 || !HashHelpers.IsWellKnownEqualityComparer((object) this._keycomparer) || this._keycomparer != null && this._keycomparer is RandomizedObjectEqualityComparer)
            return;
          this._keycomparer = HashHelpers.GetRandomizedEqualityComparer((object) this._keycomparer);
          this.rehash(this.buckets.Length, true);
          return;
        }
        if ((long) (this.buckets[index2].hash_coll & int.MaxValue) == (long) num1 && this.KeyEquals(this.buckets[index2].key, key))
        {
          if (add)
            throw new ArgumentException(Environment.GetResourceString("Argument_AddingDuplicate__", this.buckets[index2].key, key));
          Thread.BeginCriticalRegion();
          this.isWriterInProgress = true;
          this.buckets[index2].val = nvalue;
          this.UpdateVersion();
          this.isWriterInProgress = false;
          Thread.EndCriticalRegion();
          if (num2 <= 100 || !HashHelpers.IsWellKnownEqualityComparer((object) this._keycomparer) || this._keycomparer != null && this._keycomparer is RandomizedObjectEqualityComparer)
            return;
          this._keycomparer = HashHelpers.GetRandomizedEqualityComparer((object) this._keycomparer);
          this.rehash(this.buckets.Length, true);
          return;
        }
        if (index1 == -1 && this.buckets[index2].hash_coll >= 0)
        {
          this.buckets[index2].hash_coll |= int.MinValue;
          this.occupancy = this.occupancy + 1;
        }
        index2 = (int) (((long) index2 + (long) incr) % (long) (uint) this.buckets.Length);
      }
      while (++num2 < this.buckets.Length);
      if (index1 == -1)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_HashInsertFailed"));
      Thread.BeginCriticalRegion();
      this.isWriterInProgress = true;
      this.buckets[index1].val = nvalue;
      this.buckets[index1].key = key;
      this.buckets[index1].hash_coll |= (int) num1;
      this.count = this.count + 1;
      this.UpdateVersion();
      this.isWriterInProgress = false;
      Thread.EndCriticalRegion();
      if (this.buckets.Length <= 100 || !HashHelpers.IsWellKnownEqualityComparer((object) this._keycomparer) || this._keycomparer != null && this._keycomparer is RandomizedObjectEqualityComparer)
        return;
      this._keycomparer = HashHelpers.GetRandomizedEqualityComparer((object) this._keycomparer);
      this.rehash(this.buckets.Length, true);
    }

    private void putEntry(Hashtable.bucket[] newBuckets, object key, object nvalue, int hashcode)
    {
      uint num1 = (uint) hashcode;
      uint num2 = 1U + num1 * 101U % (uint) (newBuckets.Length - 1);
      int index;
      for (index = (int) (num1 % (uint) newBuckets.Length); newBuckets[index].key != null && newBuckets[index].key != this.buckets; index = (int) (((long) index + (long) num2) % (long) (uint) newBuckets.Length))
      {
        if (newBuckets[index].hash_coll >= 0)
        {
          newBuckets[index].hash_coll |= int.MinValue;
          this.occupancy = this.occupancy + 1;
        }
      }
      newBuckets[index].val = nvalue;
      newBuckets[index].key = key;
      newBuckets[index].hash_coll |= hashcode;
    }

    /// <summary>从 <see cref="T:System.Collections.Hashtable" /> 中移除带有指定键的元素。</summary>
    /// <param name="key">要移除的元素的键。 </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> 为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <see cref="T:System.Collections.Hashtable" /> 为只读。- 或 -<see cref="T:System.Collections.Hashtable" /> 具有固定大小。</exception>
    /// <filterpriority>1</filterpriority>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    public virtual void Remove(object key)
    {
      if (key == null)
        throw new ArgumentNullException("key", Environment.GetResourceString("ArgumentNull_Key"));
      uint seed;
      uint incr;
      uint num1 = this.InitHash(key, this.buckets.Length, out seed, out incr);
      int num2 = 0;
      int index = (int) (seed % (uint) this.buckets.Length);
      Hashtable.bucket bucket;
      do
      {
        bucket = this.buckets[index];
        if ((long) (bucket.hash_coll & int.MaxValue) == (long) num1 && this.KeyEquals(bucket.key, key))
        {
          Thread.BeginCriticalRegion();
          this.isWriterInProgress = true;
          this.buckets[index].hash_coll &= int.MinValue;
          this.buckets[index].key = this.buckets[index].hash_coll == 0 ? (object) null : (object) this.buckets;
          this.buckets[index].val = (object) null;
          this.count = this.count - 1;
          this.UpdateVersion();
          this.isWriterInProgress = false;
          Thread.EndCriticalRegion();
          break;
        }
        index = (int) (((long) index + (long) incr) % (long) (uint) this.buckets.Length);
      }
      while (bucket.hash_coll < 0 && ++num2 < this.buckets.Length);
    }

    /// <summary>返回 <see cref="T:System.Collections.Hashtable" /> 的同步（线程安全）包装。</summary>
    /// <returns>
    /// <see cref="T:System.Collections.Hashtable" /> 的同步（线程安全）包装。</returns>
    /// <param name="table">要同步的 <see cref="T:System.Collections.Hashtable" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="table" /> 为 null。</exception>
    /// <filterpriority>1</filterpriority>
    [HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
    public static Hashtable Synchronized(Hashtable table)
    {
      if (table == null)
        throw new ArgumentNullException("table");
      return (Hashtable) new Hashtable.SyncHashtable(table);
    }

    /// <summary>实现 <see cref="T:System.Runtime.Serialization.ISerializable" /> 接口，并返回序列化 <see cref="T:System.Collections.Hashtable" /> 所需的数据。</summary>
    /// <param name="info">一个 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 对象，它包含序列化 <see cref="T:System.Collections.Hashtable" /> 所需的信息。</param>
    /// <param name="context">
    /// <see cref="T:System.Runtime.Serialization.StreamingContext" /> 对象，该对象包含与 <see cref="T:System.Collections.Hashtable" /> 相关联的序列化流的源和目标。 </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="info" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">已修改集合。</exception>
    /// <filterpriority>2</filterpriority>
    [SecurityCritical]
    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      lock (this.SyncRoot)
      {
        int local_2 = this.version;
        info.AddValue("LoadFactor", this.loadFactor);
        info.AddValue("Version", this.version);
        IEqualityComparer local_3 = (IEqualityComparer) HashHelpers.GetEqualityComparerForSerialization((object) this._keycomparer);
        if (local_3 == null)
        {
          info.AddValue("Comparer", (object) null, typeof (IComparer));
          info.AddValue("HashCodeProvider", (object) null, typeof (IHashCodeProvider));
        }
        else if (local_3 is CompatibleComparer)
        {
          CompatibleComparer local_6 = local_3 as CompatibleComparer;
          info.AddValue("Comparer", (object) local_6.Comparer, typeof (IComparer));
          info.AddValue("HashCodeProvider", (object) local_6.HashCodeProvider, typeof (IHashCodeProvider));
        }
        else
          info.AddValue("KeyComparer", (object) local_3, typeof (IEqualityComparer));
        info.AddValue("HashSize", this.buckets.Length);
        object[] local_4 = new object[this.count];
        object[] local_5 = new object[this.count];
        this.CopyKeys((Array) local_4, 0);
        this.CopyValues((Array) local_5, 0);
        info.AddValue("Keys", (object) local_4, typeof (object[]));
        info.AddValue("Values", (object) local_5, typeof (object[]));
        if (this.version != local_2)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
      }
    }

    /// <summary>实现 <see cref="T:System.Runtime.Serialization.ISerializable" /> 接口，并在完成反序列化之后引发反序列化事件。</summary>
    /// <param name="sender">反序列化事件源。</param>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">与当前 <see cref="T:System.Collections.Hashtable" /> 相关联的 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 对象无效。</exception>
    /// <filterpriority>2</filterpriority>
    public virtual void OnDeserialization(object sender)
    {
      if (this.buckets != null)
        return;
      SerializationInfo serializationInfo;
      HashHelpers.SerializationInfoTable.TryGetValue((object) this, out serializationInfo);
      if (serializationInfo == null)
        throw new SerializationException(Environment.GetResourceString("Serialization_InvalidOnDeser"));
      int length = 0;
      IComparer comparer = (IComparer) null;
      IHashCodeProvider hashCodeProvider = (IHashCodeProvider) null;
      object[] objArray1 = (object[]) null;
      object[] objArray2 = (object[]) null;
      SerializationInfoEnumerator enumerator = serializationInfo.GetEnumerator();
      while (enumerator.MoveNext())
      {
        string name = enumerator.Name;
        // ISSUE: reference to a compiler-generated method
        uint stringHash = \u003CPrivateImplementationDetails\u003E.ComputeStringHash(name);
        if (stringHash <= 1613443821U)
        {
          if ((int) stringHash != 891156946)
          {
            if ((int) stringHash != 1228509323)
            {
              if ((int) stringHash == 1613443821 && name == "Keys")
                objArray1 = (object[]) serializationInfo.GetValue("Keys", typeof (object[]));
            }
            else if (name == "KeyComparer")
              this._keycomparer = (IEqualityComparer) serializationInfo.GetValue("KeyComparer", typeof (IEqualityComparer));
          }
          else if (name == "Comparer")
            comparer = (IComparer) serializationInfo.GetValue("Comparer", typeof (IComparer));
        }
        else if (stringHash <= 2484309429U)
        {
          if ((int) stringHash != -1924324773)
          {
            if ((int) stringHash == -1810657867 && name == "HashCodeProvider")
              hashCodeProvider = (IHashCodeProvider) serializationInfo.GetValue("HashCodeProvider", typeof (IHashCodeProvider));
          }
          else if (name == "Values")
            objArray2 = (object[]) serializationInfo.GetValue("Values", typeof (object[]));
        }
        else if ((int) stringHash != -938822048)
        {
          if ((int) stringHash == -811751054 && name == "LoadFactor")
            this.loadFactor = serializationInfo.GetSingle("LoadFactor");
        }
        else if (name == "HashSize")
          length = serializationInfo.GetInt32("HashSize");
      }
      this.loadsize = (int) ((double) this.loadFactor * (double) length);
      if (this._keycomparer == null && (comparer != null || hashCodeProvider != null))
        this._keycomparer = (IEqualityComparer) new CompatibleComparer(comparer, hashCodeProvider);
      this.buckets = new Hashtable.bucket[length];
      if (objArray1 == null)
        throw new SerializationException(Environment.GetResourceString("Serialization_MissingKeys"));
      if (objArray2 == null)
        throw new SerializationException(Environment.GetResourceString("Serialization_MissingValues"));
      if (objArray1.Length != objArray2.Length)
        throw new SerializationException(Environment.GetResourceString("Serialization_KeyValueDifferentSizes"));
      for (int index = 0; index < objArray1.Length; ++index)
      {
        if (objArray1[index] == null)
          throw new SerializationException(Environment.GetResourceString("Serialization_NullKey"));
        this.Insert(objArray1[index], objArray2[index], true);
      }
      this.version = serializationInfo.GetInt32("Version");
      HashHelpers.SerializationInfoTable.Remove((object) this);
    }

    private struct bucket
    {
      public object key;
      public object val;
      public int hash_coll;
    }

    [Serializable]
    private class KeyCollection : ICollection, IEnumerable
    {
      private Hashtable _hashtable;

      public virtual bool IsSynchronized
      {
        get
        {
          return this._hashtable.IsSynchronized;
        }
      }

      public virtual object SyncRoot
      {
        get
        {
          return this._hashtable.SyncRoot;
        }
      }

      public virtual int Count
      {
        get
        {
          return this._hashtable.count;
        }
      }

      internal KeyCollection(Hashtable hashtable)
      {
        this._hashtable = hashtable;
      }

      public virtual void CopyTo(Array array, int arrayIndex)
      {
        if (array == null)
          throw new ArgumentNullException("array");
        if (array.Rank != 1)
          throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
        if (arrayIndex < 0)
          throw new ArgumentOutOfRangeException("arrayIndex", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        if (array.Length - arrayIndex < this._hashtable.count)
          throw new ArgumentException(Environment.GetResourceString("Arg_ArrayPlusOffTooSmall"));
        this._hashtable.CopyKeys(array, arrayIndex);
      }

      public virtual IEnumerator GetEnumerator()
      {
        return (IEnumerator) new Hashtable.HashtableEnumerator(this._hashtable, 1);
      }
    }

    [Serializable]
    private class ValueCollection : ICollection, IEnumerable
    {
      private Hashtable _hashtable;

      public virtual bool IsSynchronized
      {
        get
        {
          return this._hashtable.IsSynchronized;
        }
      }

      public virtual object SyncRoot
      {
        get
        {
          return this._hashtable.SyncRoot;
        }
      }

      public virtual int Count
      {
        get
        {
          return this._hashtable.count;
        }
      }

      internal ValueCollection(Hashtable hashtable)
      {
        this._hashtable = hashtable;
      }

      public virtual void CopyTo(Array array, int arrayIndex)
      {
        if (array == null)
          throw new ArgumentNullException("array");
        if (array.Rank != 1)
          throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
        if (arrayIndex < 0)
          throw new ArgumentOutOfRangeException("arrayIndex", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        if (array.Length - arrayIndex < this._hashtable.count)
          throw new ArgumentException(Environment.GetResourceString("Arg_ArrayPlusOffTooSmall"));
        this._hashtable.CopyValues(array, arrayIndex);
      }

      public virtual IEnumerator GetEnumerator()
      {
        return (IEnumerator) new Hashtable.HashtableEnumerator(this._hashtable, 2);
      }
    }

    [Serializable]
    private class SyncHashtable : Hashtable, IEnumerable
    {
      protected Hashtable _table;

      public override int Count
      {
        get
        {
          return this._table.Count;
        }
      }

      public override bool IsReadOnly
      {
        get
        {
          return this._table.IsReadOnly;
        }
      }

      public override bool IsFixedSize
      {
        get
        {
          return this._table.IsFixedSize;
        }
      }

      public override bool IsSynchronized
      {
        get
        {
          return true;
        }
      }

      public override object this[object key]
      {
        get
        {
          return this._table[key];
        }
        set
        {
          lock (this._table.SyncRoot)
            this._table[key] = value;
        }
      }

      public override object SyncRoot
      {
        get
        {
          return this._table.SyncRoot;
        }
      }

      public override ICollection Keys
      {
        get
        {
          lock (this._table.SyncRoot)
            return this._table.Keys;
        }
      }

      public override ICollection Values
      {
        get
        {
          lock (this._table.SyncRoot)
            return this._table.Values;
        }
      }

      internal SyncHashtable(Hashtable table)
        : base(false)
      {
        this._table = table;
      }

      internal SyncHashtable(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this._table = (Hashtable) info.GetValue("ParentTable", typeof (Hashtable));
        if (this._table == null)
          throw new SerializationException(Environment.GetResourceString("Serialization_InsufficientState"));
      }

      [SecurityCritical]
      public override void GetObjectData(SerializationInfo info, StreamingContext context)
      {
        if (info == null)
          throw new ArgumentNullException("info");
        lock (this._table.SyncRoot)
          info.AddValue("ParentTable", (object) this._table, typeof (Hashtable));
      }

      public override void Add(object key, object value)
      {
        lock (this._table.SyncRoot)
          this._table.Add(key, value);
      }

      public override void Clear()
      {
        lock (this._table.SyncRoot)
          this._table.Clear();
      }

      public override bool Contains(object key)
      {
        return this._table.Contains(key);
      }

      public override bool ContainsKey(object key)
      {
        if (key == null)
          throw new ArgumentNullException("key", Environment.GetResourceString("ArgumentNull_Key"));
        return this._table.ContainsKey(key);
      }

      public override bool ContainsValue(object key)
      {
        lock (this._table.SyncRoot)
          return this._table.ContainsValue(key);
      }

      public override void CopyTo(Array array, int arrayIndex)
      {
        lock (this._table.SyncRoot)
          this._table.CopyTo(array, arrayIndex);
      }

      public override object Clone()
      {
        lock (this._table.SyncRoot)
          return (object) Hashtable.Synchronized((Hashtable) this._table.Clone());
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
        return (IEnumerator) this._table.GetEnumerator();
      }

      public override IDictionaryEnumerator GetEnumerator()
      {
        return this._table.GetEnumerator();
      }

      public override void Remove(object key)
      {
        lock (this._table.SyncRoot)
          this._table.Remove(key);
      }

      public override void OnDeserialization(object sender)
      {
      }

      internal override KeyValuePairs[] ToKeyValuePairsArray()
      {
        return this._table.ToKeyValuePairsArray();
      }
    }

    [Serializable]
    private class HashtableEnumerator : IDictionaryEnumerator, IEnumerator, ICloneable
    {
      private Hashtable hashtable;
      private int bucket;
      private int version;
      private bool current;
      private int getObjectRetType;
      private object currentKey;
      private object currentValue;
      internal const int Keys = 1;
      internal const int Values = 2;
      internal const int DictEntry = 3;

      public virtual object Key
      {
        get
        {
          if (!this.current)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
          return this.currentKey;
        }
      }

      public virtual DictionaryEntry Entry
      {
        get
        {
          if (!this.current)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
          return new DictionaryEntry(this.currentKey, this.currentValue);
        }
      }

      public virtual object Current
      {
        get
        {
          if (!this.current)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
          if (this.getObjectRetType == 1)
            return this.currentKey;
          if (this.getObjectRetType == 2)
            return this.currentValue;
          return (object) new DictionaryEntry(this.currentKey, this.currentValue);
        }
      }

      public virtual object Value
      {
        get
        {
          if (!this.current)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
          return this.currentValue;
        }
      }

      internal HashtableEnumerator(Hashtable hashtable, int getObjRetType)
      {
        this.hashtable = hashtable;
        this.bucket = hashtable.buckets.Length;
        this.version = hashtable.version;
        this.current = false;
        this.getObjectRetType = getObjRetType;
      }

      public object Clone()
      {
        return this.MemberwiseClone();
      }

      public virtual bool MoveNext()
      {
        if (this.version != this.hashtable.version)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
        while (this.bucket > 0)
        {
          this.bucket = this.bucket - 1;
          object obj = this.hashtable.buckets[this.bucket].key;
          if (obj != null && obj != this.hashtable.buckets)
          {
            this.currentKey = obj;
            this.currentValue = this.hashtable.buckets[this.bucket].val;
            this.current = true;
            return true;
          }
        }
        this.current = false;
        return false;
      }

      public virtual void Reset()
      {
        if (this.version != this.hashtable.version)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
        this.current = false;
        this.bucket = this.hashtable.buckets.Length;
        this.currentKey = (object) null;
        this.currentValue = (object) null;
      }
    }

    internal class HashtableDebugView
    {
      private Hashtable hashtable;

      [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
      public KeyValuePairs[] Items
      {
        get
        {
          return this.hashtable.ToKeyValuePairsArray();
        }
      }

      public HashtableDebugView(Hashtable hashtable)
      {
        if (hashtable == null)
          throw new ArgumentNullException("hashtable");
        this.hashtable = hashtable;
      }
    }
  }
}
