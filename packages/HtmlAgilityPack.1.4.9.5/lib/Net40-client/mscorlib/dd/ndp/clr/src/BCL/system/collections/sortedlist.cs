// Decompiled with JetBrains decompiler
// Type: System.Collections.SortedList
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections
{
  /// <summary>表示键/值对的集合，这些键值对按键排序并可按照键和索引访问。</summary>
  /// <filterpriority>1</filterpriority>
  [DebuggerTypeProxy(typeof (SortedList.SortedListDebugView))]
  [DebuggerDisplay("Count = {Count}")]
  [ComVisible(true)]
  [Serializable]
  public class SortedList : IDictionary, ICollection, IEnumerable, ICloneable
  {
    private static object[] emptyArray = EmptyArray<object>.Value;
    private object[] keys;
    private object[] values;
    private int _size;
    private int version;
    private IComparer comparer;
    private SortedList.KeyList keyList;
    private SortedList.ValueList valueList;
    [NonSerialized]
    private object _syncRoot;
    private const int _defaultCapacity = 16;

    /// <summary>获取或设置 <see cref="T:System.Collections.SortedList" /> 对象的容量。</summary>
    /// <returns>
    /// <see cref="T:System.Collections.SortedList" /> 对象可包含的元素数。</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">分配的值小于 <see cref="T:System.Collections.SortedList" /> 对象中的当前元素数。</exception>
    /// <exception cref="T:System.OutOfMemoryException">系统中没有足够的可用内存。</exception>
    /// <filterpriority>2</filterpriority>
    public virtual int Capacity
    {
      get
      {
        return this.keys.Length;
      }
      set
      {
        if (value < this.Count)
          throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_SmallCapacity"));
        if (value == this.keys.Length)
          return;
        if (value > 0)
        {
          object[] objArray1 = new object[value];
          object[] objArray2 = new object[value];
          if (this._size > 0)
          {
            Array.Copy((Array) this.keys, 0, (Array) objArray1, 0, this._size);
            Array.Copy((Array) this.values, 0, (Array) objArray2, 0, this._size);
          }
          this.keys = objArray1;
          this.values = objArray2;
        }
        else
        {
          this.keys = SortedList.emptyArray;
          this.values = SortedList.emptyArray;
        }
      }
    }

    /// <summary>获取 <see cref="T:System.Collections.SortedList" /> 对象中包含的元素数。</summary>
    /// <returns>
    /// <see cref="T:System.Collections.SortedList" /> 对象中包含的元素数。</returns>
    /// <filterpriority>1</filterpriority>
    public virtual int Count
    {
      get
      {
        return this._size;
      }
    }

    /// <summary>获取 <see cref="T:System.Collections.SortedList" /> 对象中的键。</summary>
    /// <returns>包含 <see cref="T:System.Collections.SortedList" /> 对象中的键的 <see cref="T:System.Collections.ICollection" /> 对象。</returns>
    /// <filterpriority>1</filterpriority>
    public virtual ICollection Keys
    {
      get
      {
        return (ICollection) this.GetKeyList();
      }
    }

    /// <summary>获取 <see cref="T:System.Collections.SortedList" /> 对象中的值。</summary>
    /// <returns>
    /// <see cref="T:System.Collections.ICollection" /> 对象，它包含 <see cref="T:System.Collections.SortedList" /> 对象中的值。</returns>
    /// <filterpriority>1</filterpriority>
    public virtual ICollection Values
    {
      get
      {
        return (ICollection) this.GetValueList();
      }
    }

    /// <summary>获取一个值，该值指示 <see cref="T:System.Collections.SortedList" /> 对象是否为只读。</summary>
    /// <returns>true if the <see cref="T:System.Collections.SortedList" /> object is read-only; otherwise, false.默认值为 false。</returns>
    /// <filterpriority>2</filterpriority>
    public virtual bool IsReadOnly
    {
      get
      {
        return false;
      }
    }

    /// <summary>获取一个值，该值指示 <see cref="T:System.Collections.SortedList" /> 对象是否具有固定大小。</summary>
    /// <returns>true if the <see cref="T:System.Collections.SortedList" /> object has a fixed size; otherwise, false.默认值为 false。</returns>
    /// <filterpriority>2</filterpriority>
    public virtual bool IsFixedSize
    {
      get
      {
        return false;
      }
    }

    /// <summary>获取一个值，该值指示对 <see cref="T:System.Collections.SortedList" /> 对象的访问是否同步（线程安全）。</summary>
    /// <returns>true if access to the <see cref="T:System.Collections.SortedList" /> object is synchronized (thread safe); otherwise, false.默认值为 false。</returns>
    /// <filterpriority>2</filterpriority>
    public virtual bool IsSynchronized
    {
      get
      {
        return false;
      }
    }

    /// <summary>获取一个对象，该对象可用于同步对 <see cref="T:System.Collections.SortedList" /> 对象的访问。</summary>
    /// <returns>一个对象，该对象可用于同步对 <see cref="T:System.Collections.SortedList" /> 对象的访问。</returns>
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

    /// <summary>获取并设置与 <see cref="T:System.Collections.SortedList" /> 对象中的特定键相关联的值。</summary>
    /// <returns>如果找到 <paramref name="key" />，则为与 <see cref="T:System.Collections.SortedList" /> 对象中的 <paramref name="key" /> 参数相关联的值；否则为 null。</returns>
    /// <param name="key">与要获取或设置的值相关联的键。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> 为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">设置该属性，而且 <see cref="T:System.Collections.SortedList" /> 对象为只读。- 或 -设置该属性，集合中不存在 <paramref name="key" />，而且 <see cref="T:System.Collections.SortedList" /> 具有固定大小。</exception>
    /// <exception cref="T:System.OutOfMemoryException">没有足够的可用内存来将元素添加到 <see cref="T:System.Collections.SortedList" />。</exception>
    /// <exception cref="T:System.InvalidOperationException">比较器引发异常。</exception>
    /// <filterpriority>1</filterpriority>
    public virtual object this[object key]
    {
      get
      {
        int index = this.IndexOfKey(key);
        if (index >= 0)
          return this.values[index];
        return (object) null;
      }
      set
      {
        if (key == null)
          throw new ArgumentNullException("key", Environment.GetResourceString("ArgumentNull_Key"));
        int index = Array.BinarySearch((Array) this.keys, 0, this._size, key, this.comparer);
        if (index >= 0)
        {
          this.values[index] = value;
          this.version = this.version + 1;
        }
        else
          this.Insert(~index, key, value);
      }
    }

    /// <summary>初始化 <see cref="T:System.Collections.SortedList" /> 类的新实例，该实例为空、具有默认初始容量并根据 <see cref="T:System.IComparable" /> 接口（此接口由添加到 <see cref="T:System.Collections.SortedList" /> 对象中的每个键实现）进行排序。</summary>
    public SortedList()
    {
      this.Init();
    }

    /// <summary>初始化 <see cref="T:System.Collections.SortedList" /> 类的新实例，该实例为空、具有指定的初始容量并且根据 <see cref="T:System.IComparable" /> 接口（此接口由添加到 <see cref="T:System.Collections.SortedList" /> 对象的每个键实现）进行排序。</summary>
    /// <param name="initialCapacity">
    /// <see cref="T:System.Collections.SortedList" /> 对象可包含的初始元素数。 </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="initialCapacity" /> 小于零。 </exception>
    /// <exception cref="T:System.OutOfMemoryException">There is not enough available memory to create a <see cref="T:System.Collections.SortedList" /> object with the specified <paramref name="initialCapacity" />.</exception>
    public SortedList(int initialCapacity)
    {
      if (initialCapacity < 0)
        throw new ArgumentOutOfRangeException("initialCapacity", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      this.keys = new object[initialCapacity];
      this.values = new object[initialCapacity];
      this.comparer = (IComparer) new Comparer(CultureInfo.CurrentCulture);
    }

    /// <summary>初始化 <see cref="T:System.Collections.SortedList" /> 类的新实例，该实例为空、具有默认初始容量并根据指定的 <see cref="T:System.Collections.IComparer" /> 接口进行排序。</summary>
    /// <param name="comparer">在比较键时要使用的 <see cref="T:System.Collections.IComparer" /> 实现。- 或 - null，使用每个键的 <see cref="T:System.IComparable" /> 实现。</param>
    public SortedList(IComparer comparer)
      : this()
    {
      if (comparer == null)
        return;
      this.comparer = comparer;
    }

    /// <summary>初始化 <see cref="T:System.Collections.SortedList" /> 类的新实例，该实例为空、具有指定的初始容量并根据指定的 <see cref="T:System.Collections.IComparer" /> 接口排序。</summary>
    /// <param name="comparer">在比较键时要使用的 <see cref="T:System.Collections.IComparer" /> 实现。- 或 - null，使用每个键的 <see cref="T:System.IComparable" /> 实现。</param>
    /// <param name="capacity">
    /// <see cref="T:System.Collections.SortedList" /> 对象可包含的初始元素数。 </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="capacity" /> 小于零。</exception>
    /// <exception cref="T:System.OutOfMemoryException">没有足够的可用内存来创建具有指定 <paramref name="capacity" /> 的 <see cref="T:System.Collections.SortedList" /> 对象。</exception>
    public SortedList(IComparer comparer, int capacity)
      : this(comparer)
    {
      this.Capacity = capacity;
    }

    /// <summary>初始化 <see cref="T:System.Collections.SortedList" /> 类的新实例，该实例包含从指定字典复制的元素、具有与所复制的元素数相同的初始容量并根据由每个键实现的 <see cref="T:System.IComparable" /> 接口排序。</summary>
    /// <param name="d">要复制到新 <see cref="T:System.Collections.SortedList" /> 对象的 <see cref="T:System.Collections.IDictionary" /> 实现。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="d" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="d" /> 中的一个或多个元素未实现 <see cref="T:System.IComparable" /> 接口。</exception>
    public SortedList(IDictionary d)
      : this(d, (IComparer) null)
    {
    }

    /// <summary>初始化 <see cref="T:System.Collections.SortedList" /> 类的新实例，该实例包含从指定字典复制的元素、具有与所复制的元素数相同的初始容量并根据指定的 <see cref="T:System.Collections.IComparer" /> 接口排序。</summary>
    /// <param name="d">要复制到新 <see cref="T:System.Collections.SortedList" /> 对象的 <see cref="T:System.Collections.IDictionary" /> 实现。</param>
    /// <param name="comparer">在比较键时要使用的 <see cref="T:System.Collections.IComparer" /> 实现。- 或 - null，使用每个键的 <see cref="T:System.IComparable" /> 实现。 </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="d" /> 为 null。 </exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="comparer" /> 为 null，<paramref name="d" /> 中的一个或多个元素不实现 <see cref="T:System.IComparable" /> 接口。</exception>
    public SortedList(IDictionary d, IComparer comparer)
      : this(comparer, d != null ? d.Count : 0)
    {
      if (d == null)
        throw new ArgumentNullException("d", Environment.GetResourceString("ArgumentNull_Dictionary"));
      d.Keys.CopyTo((Array) this.keys, 0);
      d.Values.CopyTo((Array) this.values, 0);
      Array.Sort((Array) this.keys, (Array) this.values, comparer);
      this._size = d.Count;
    }

    private void Init()
    {
      this.keys = SortedList.emptyArray;
      this.values = SortedList.emptyArray;
      this._size = 0;
      this.comparer = (IComparer) new Comparer(CultureInfo.CurrentCulture);
    }

    /// <summary>将带有指定键和值的元素添加到 <see cref="T:System.Collections.SortedList" /> 对象。</summary>
    /// <param name="key">要添加的元素的键。</param>
    /// <param name="value">要添加的元素的值。该值可以为 null。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">带有指定 <paramref name="key" /> 的元素已经存在于 <see cref="T:System.Collections.SortedList" /> 对象中。- 或 -<see cref="T:System.Collections.SortedList" /> 设置为使用 <see cref="T:System.IComparable" /> 接口，并且 <paramref name="key" /> 不实现 <see cref="T:System.IComparable" /> 接口。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <see cref="T:System.Collections.SortedList" /> 为只读。- 或 -<see cref="T:System.Collections.SortedList" /> 具有固定大小。</exception>
    /// <exception cref="T:System.OutOfMemoryException">没有足够的可用内存来将元素添加到 <see cref="T:System.Collections.SortedList" />。</exception>
    /// <exception cref="T:System.InvalidOperationException">比较器引发异常。</exception>
    /// <filterpriority>2</filterpriority>
    public virtual void Add(object key, object value)
    {
      if (key == null)
        throw new ArgumentNullException("key", Environment.GetResourceString("ArgumentNull_Key"));
      int index = Array.BinarySearch((Array) this.keys, 0, this._size, key, this.comparer);
      if (index >= 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_AddingDuplicate__", this.GetKey(index), key));
      this.Insert(~index, key, value);
    }

    /// <summary>从 <see cref="T:System.Collections.SortedList" /> 对象中移除所有元素。</summary>
    /// <exception cref="T:System.NotSupportedException">
    /// <see cref="T:System.Collections.SortedList" /> 对象是只读的。- 或 -<see cref="T:System.Collections.SortedList" /> 具有固定大小。 </exception>
    /// <filterpriority>1</filterpriority>
    public virtual void Clear()
    {
      this.version = this.version + 1;
      Array.Clear((Array) this.keys, 0, this._size);
      Array.Clear((Array) this.values, 0, this._size);
      this._size = 0;
    }

    /// <summary>创建 <see cref="T:System.Collections.SortedList" /> 对象的浅表副本。</summary>
    /// <returns>
    /// <see cref="T:System.Collections.SortedList" /> 对象的浅表副本。</returns>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public virtual object Clone()
    {
      SortedList sortedList = new SortedList(this._size);
      Array.Copy((Array) this.keys, 0, (Array) sortedList.keys, 0, this._size);
      Array.Copy((Array) this.values, 0, (Array) sortedList.values, 0, this._size);
      sortedList._size = this._size;
      sortedList.version = this.version;
      sortedList.comparer = this.comparer;
      return (object) sortedList;
    }

    /// <summary>确定 <see cref="T:System.Collections.SortedList" /> 对象是否包含特定键。</summary>
    /// <returns>如果 <see cref="T:System.Collections.SortedList" /> 对象包含带有指定 <paramref name="key" /> 的元素，则为 true；否则为 false。</returns>
    /// <param name="key">要在 <see cref="T:System.Collections.SortedList" /> 对象中定位的键。 </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">比较器引发异常。</exception>
    /// <filterpriority>1</filterpriority>
    public virtual bool Contains(object key)
    {
      return this.IndexOfKey(key) >= 0;
    }

    /// <summary>确定 <see cref="T:System.Collections.SortedList" /> 对象是否包含特定键。</summary>
    /// <returns>如果 <see cref="T:System.Collections.SortedList" /> 对象包含带有指定 <paramref name="key" /> 的元素，则为 true；否则为 false。</returns>
    /// <param name="key">要在 <see cref="T:System.Collections.SortedList" /> 对象中定位的键。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">比较器引发异常。</exception>
    /// <filterpriority>1</filterpriority>
    public virtual bool ContainsKey(object key)
    {
      return this.IndexOfKey(key) >= 0;
    }

    /// <summary>确定 <see cref="T:System.Collections.SortedList" /> 对象是否包含特定值。</summary>
    /// <returns>true if the <see cref="T:System.Collections.SortedList" /> object contains an element with the specified <paramref name="value" />; otherwise, false.</returns>
    /// <param name="value">要在 <see cref="T:System.Collections.SortedList" /> 对象中定位的值。该值可以为 null。</param>
    /// <filterpriority>2</filterpriority>
    public virtual bool ContainsValue(object value)
    {
      return this.IndexOfValue(value) >= 0;
    }

    /// <summary>从指定数组索引开始将 <see cref="T:System.Collections.SortedList" /> 元素复制到一维 <see cref="T:System.Array" /> 对象中。</summary>
    /// <param name="array">一维 <see cref="T:System.Array" /> 对象，它是从 <see cref="T:System.Collections.SortedList" /> 复制的 <see cref="T:System.Collections.DictionaryEntry" /> 对象的目标位置。<see cref="T:System.Array" /> 必须具有从零开始的索引。</param>
    /// <param name="arrayIndex">
    /// <paramref name="array" /> 中从零开始的索引，从此索引处开始进行复制。 </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="arrayIndex" /> 小于零。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="array" /> 是多维的。- 或 -源 <see cref="T:System.Collections.SortedList" /> 对象中的元素数目大于从 <paramref name="arrayIndex" /> 到目标 <paramref name="array" /> 末尾之间的可用空间。</exception>
    /// <exception cref="T:System.InvalidCastException">源 <see cref="T:System.Collections.SortedList" /> 的类型无法自动转换为目标 <paramref name="array" /> 的类型。</exception>
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
      for (int index = 0; index < this.Count; ++index)
      {
        DictionaryEntry dictionaryEntry = new DictionaryEntry(this.keys[index], this.values[index]);
        array.SetValue((object) dictionaryEntry, index + arrayIndex);
      }
    }

    internal virtual KeyValuePairs[] ToKeyValuePairsArray()
    {
      KeyValuePairs[] keyValuePairsArray = new KeyValuePairs[this.Count];
      for (int index = 0; index < this.Count; ++index)
        keyValuePairsArray[index] = new KeyValuePairs(this.keys[index], this.values[index]);
      return keyValuePairsArray;
    }

    private void EnsureCapacity(int min)
    {
      int num = this.keys.Length == 0 ? 16 : this.keys.Length * 2;
      if ((uint) num > 2146435071U)
        num = 2146435071;
      if (num < min)
        num = min;
      this.Capacity = num;
    }

    /// <summary>获取 <see cref="T:System.Collections.SortedList" /> 对象的指定索引处的值。</summary>
    /// <returns>位于 <see cref="T:System.Collections.SortedList" /> 对象的指定索引处的值。</returns>
    /// <param name="index">要获取的值的从零开始的索引。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 不在 <see cref="T:System.Collections.SortedList" /> 对象的有效索引范围内。</exception>
    /// <filterpriority>2</filterpriority>
    public virtual object GetByIndex(int index)
    {
      if (index < 0 || index >= this.Count)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      return this.values[index];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) new SortedList.SortedListEnumerator(this, 0, this._size, 3);
    }

    /// <summary>返回一个循环访问 <see cref="T:System.Collections.SortedList" /> 对象的 <see cref="T:System.Collections.IDictionaryEnumerator" /> 对象。</summary>
    /// <returns>一个用于 <see cref="T:System.Collections.SortedList" /> 对象的 <see cref="T:System.Collections.IDictionaryEnumerator" /> 对象。</returns>
    /// <filterpriority>2</filterpriority>
    public virtual IDictionaryEnumerator GetEnumerator()
    {
      return (IDictionaryEnumerator) new SortedList.SortedListEnumerator(this, 0, this._size, 3);
    }

    /// <summary>获取 <see cref="T:System.Collections.SortedList" /> 对象的指定索引处的键。</summary>
    /// <returns>
    /// <see cref="T:System.Collections.SortedList" /> 对象的指定索引处的键。</returns>
    /// <param name="index">要获取的键的从零开始的索引。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 不在 <see cref="T:System.Collections.SortedList" /> 对象的有效索引范围内。</exception>
    /// <filterpriority>2</filterpriority>
    public virtual object GetKey(int index)
    {
      if (index < 0 || index >= this.Count)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      return this.keys[index];
    }

    /// <summary>获取 <see cref="T:System.Collections.SortedList" /> 对象中的键。</summary>
    /// <returns>包含 <see cref="T:System.Collections.SortedList" /> 对象中的键的 <see cref="T:System.Collections.IList" /> 对象。</returns>
    /// <filterpriority>2</filterpriority>
    public virtual IList GetKeyList()
    {
      if (this.keyList == null)
        this.keyList = new SortedList.KeyList(this);
      return (IList) this.keyList;
    }

    /// <summary>获取 <see cref="T:System.Collections.SortedList" /> 对象中的值。</summary>
    /// <returns>
    /// <see cref="T:System.Collections.IList" /> 对象，它包含 <see cref="T:System.Collections.SortedList" /> 对象中的值。</returns>
    /// <filterpriority>2</filterpriority>
    public virtual IList GetValueList()
    {
      if (this.valueList == null)
        this.valueList = new SortedList.ValueList(this);
      return (IList) this.valueList;
    }

    /// <summary>返回 <see cref="T:System.Collections.SortedList" /> 对象中指定键的从零开始的索引。</summary>
    /// <returns>如果在 <see cref="T:System.Collections.SortedList" /> 对象中找到 <paramref name="key" />，则为 <paramref name="key" /> 参数的从零开始的索引；否则为 -1。</returns>
    /// <param name="key">要在 <see cref="T:System.Collections.SortedList" /> 对象中定位的键。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">比较器引发异常。</exception>
    /// <filterpriority>1</filterpriority>
    public virtual int IndexOfKey(object key)
    {
      if (key == null)
        throw new ArgumentNullException("key", Environment.GetResourceString("ArgumentNull_Key"));
      int num = Array.BinarySearch((Array) this.keys, 0, this._size, key, this.comparer);
      if (num < 0)
        return -1;
      return num;
    }

    /// <summary>返回指定的值在 <see cref="T:System.Collections.SortedList" /> 对象中第一个匹配项的从零开始的索引。</summary>
    /// <returns>如果在 <see cref="T:System.Collections.SortedList" /> 对象中找到 <paramref name="value" />，则为 <paramref name="value" /> 参数的第一个匹配项的从零开始的索引；否则为 -1。</returns>
    /// <param name="value">要在 <see cref="T:System.Collections.SortedList" /> 对象中定位的值。该值可以为 null。</param>
    /// <filterpriority>1</filterpriority>
    public virtual int IndexOfValue(object value)
    {
      return Array.IndexOf<object>(this.values, value, 0, this._size);
    }

    private void Insert(int index, object key, object value)
    {
      if (this._size == this.keys.Length)
        this.EnsureCapacity(this._size + 1);
      if (index < this._size)
      {
        Array.Copy((Array) this.keys, index, (Array) this.keys, index + 1, this._size - index);
        Array.Copy((Array) this.values, index, (Array) this.values, index + 1, this._size - index);
      }
      this.keys[index] = key;
      this.values[index] = value;
      this._size = this._size + 1;
      this.version = this.version + 1;
    }

    /// <summary>移除 <see cref="T:System.Collections.SortedList" /> 对象的指定索引处的元素。</summary>
    /// <param name="index">要移除的元素的从零开始的索引。 </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 不在 <see cref="T:System.Collections.SortedList" /> 对象的有效索引范围内。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <see cref="T:System.Collections.SortedList" /> 为只读。- 或 -<see cref="T:System.Collections.SortedList" /> 具有固定大小。</exception>
    /// <filterpriority>2</filterpriority>
    public virtual void RemoveAt(int index)
    {
      if (index < 0 || index >= this.Count)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      this._size = this._size - 1;
      if (index < this._size)
      {
        Array.Copy((Array) this.keys, index + 1, (Array) this.keys, index, this._size - index);
        Array.Copy((Array) this.values, index + 1, (Array) this.values, index, this._size - index);
      }
      this.keys[this._size] = (object) null;
      this.values[this._size] = (object) null;
      this.version = this.version + 1;
    }

    /// <summary>从 <see cref="T:System.Collections.SortedList" /> 对象中移除带有指定键的元素。</summary>
    /// <param name="key">要移除的元素的键。 </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> 为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <see cref="T:System.Collections.SortedList" /> 对象是只读的。- 或 -<see cref="T:System.Collections.SortedList" /> 具有固定大小。</exception>
    /// <filterpriority>1</filterpriority>
    public virtual void Remove(object key)
    {
      int index = this.IndexOfKey(key);
      if (index < 0)
        return;
      this.RemoveAt(index);
    }

    /// <summary>替换 <see cref="T:System.Collections.SortedList" /> 对象中指定索引处的值。</summary>
    /// <param name="index">从零开始的索引，在该位置保存 <paramref name="value" />。</param>
    /// <param name="value">要保存到 <see cref="T:System.Collections.SortedList" /> 对象中的 <see cref="T:System.Object" />。该值可以为 null。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 不在 <see cref="T:System.Collections.SortedList" /> 对象的有效索引范围内。</exception>
    /// <filterpriority>2</filterpriority>
    public virtual void SetByIndex(int index, object value)
    {
      if (index < 0 || index >= this.Count)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      this.values[index] = value;
      this.version = this.version + 1;
    }

    /// <summary>返回 <see cref="T:System.Collections.SortedList" /> 对象的同步（线程安全）包装。</summary>
    /// <returns>
    /// <see cref="T:System.Collections.SortedList" /> 对象的同步（线程安全）包装。</returns>
    /// <param name="list">要同步的 <see cref="T:System.Collections.SortedList" /> 对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="list" /> 为 null。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
    public static SortedList Synchronized(SortedList list)
    {
      if (list == null)
        throw new ArgumentNullException("list");
      return (SortedList) new SortedList.SyncSortedList(list);
    }

    /// <summary>将容量设置为 <see cref="T:System.Collections.SortedList" /> 对象中元素的实际数目。</summary>
    /// <exception cref="T:System.NotSupportedException">
    /// <see cref="T:System.Collections.SortedList" /> 对象是只读的。- 或 -<see cref="T:System.Collections.SortedList" /> 具有固定大小。</exception>
    /// <filterpriority>2</filterpriority>
    public virtual void TrimToSize()
    {
      this.Capacity = this._size;
    }

    [Serializable]
    private class SyncSortedList : SortedList
    {
      private SortedList _list;
      private object _root;

      public override int Count
      {
        get
        {
          lock (this._root)
            return this._list.Count;
        }
      }

      public override object SyncRoot
      {
        get
        {
          return this._root;
        }
      }

      public override bool IsReadOnly
      {
        get
        {
          return this._list.IsReadOnly;
        }
      }

      public override bool IsFixedSize
      {
        get
        {
          return this._list.IsFixedSize;
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
          lock (this._root)
            return this._list[key];
        }
        set
        {
          lock (this._root)
            this._list[key] = value;
        }
      }

      public override int Capacity
      {
        get
        {
          lock (this._root)
            return this._list.Capacity;
        }
      }

      internal SyncSortedList(SortedList list)
      {
        this._list = list;
        this._root = list.SyncRoot;
      }

      public override void Add(object key, object value)
      {
        lock (this._root)
          this._list.Add(key, value);
      }

      public override void Clear()
      {
        lock (this._root)
          this._list.Clear();
      }

      public override object Clone()
      {
        lock (this._root)
          return this._list.Clone();
      }

      public override bool Contains(object key)
      {
        lock (this._root)
          return this._list.Contains(key);
      }

      public override bool ContainsKey(object key)
      {
        lock (this._root)
          return this._list.ContainsKey(key);
      }

      public override bool ContainsValue(object key)
      {
        lock (this._root)
          return this._list.ContainsValue(key);
      }

      public override void CopyTo(Array array, int index)
      {
        lock (this._root)
          this._list.CopyTo(array, index);
      }

      public override object GetByIndex(int index)
      {
        lock (this._root)
          return this._list.GetByIndex(index);
      }

      public override IDictionaryEnumerator GetEnumerator()
      {
        lock (this._root)
          return this._list.GetEnumerator();
      }

      public override object GetKey(int index)
      {
        lock (this._root)
          return this._list.GetKey(index);
      }

      public override IList GetKeyList()
      {
        lock (this._root)
          return this._list.GetKeyList();
      }

      public override IList GetValueList()
      {
        lock (this._root)
          return this._list.GetValueList();
      }

      public override int IndexOfKey(object key)
      {
        if (key == null)
          throw new ArgumentNullException("key", Environment.GetResourceString("ArgumentNull_Key"));
        lock (this._root)
          return this._list.IndexOfKey(key);
      }

      public override int IndexOfValue(object value)
      {
        lock (this._root)
          return this._list.IndexOfValue(value);
      }

      public override void RemoveAt(int index)
      {
        lock (this._root)
          this._list.RemoveAt(index);
      }

      public override void Remove(object key)
      {
        lock (this._root)
          this._list.Remove(key);
      }

      public override void SetByIndex(int index, object value)
      {
        lock (this._root)
          this._list.SetByIndex(index, value);
      }

      internal override KeyValuePairs[] ToKeyValuePairsArray()
      {
        return this._list.ToKeyValuePairsArray();
      }

      public override void TrimToSize()
      {
        lock (this._root)
          this._list.TrimToSize();
      }
    }

    [Serializable]
    private class SortedListEnumerator : IDictionaryEnumerator, IEnumerator, ICloneable
    {
      private SortedList sortedList;
      private object key;
      private object value;
      private int index;
      private int startIndex;
      private int endIndex;
      private int version;
      private bool current;
      private int getObjectRetType;
      internal const int Keys = 1;
      internal const int Values = 2;
      internal const int DictEntry = 3;

      public virtual object Key
      {
        get
        {
          if (this.version != this.sortedList.version)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
          if (!this.current)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
          return this.key;
        }
      }

      public virtual DictionaryEntry Entry
      {
        get
        {
          if (this.version != this.sortedList.version)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
          if (!this.current)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
          return new DictionaryEntry(this.key, this.value);
        }
      }

      public virtual object Current
      {
        get
        {
          if (!this.current)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
          if (this.getObjectRetType == 1)
            return this.key;
          if (this.getObjectRetType == 2)
            return this.value;
          return (object) new DictionaryEntry(this.key, this.value);
        }
      }

      public virtual object Value
      {
        get
        {
          if (this.version != this.sortedList.version)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
          if (!this.current)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
          return this.value;
        }
      }

      internal SortedListEnumerator(SortedList sortedList, int index, int count, int getObjRetType)
      {
        this.sortedList = sortedList;
        this.index = index;
        this.startIndex = index;
        this.endIndex = index + count;
        this.version = sortedList.version;
        this.getObjectRetType = getObjRetType;
        this.current = false;
      }

      public object Clone()
      {
        return this.MemberwiseClone();
      }

      public virtual bool MoveNext()
      {
        if (this.version != this.sortedList.version)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
        if (this.index < this.endIndex)
        {
          this.key = this.sortedList.keys[this.index];
          this.value = this.sortedList.values[this.index];
          this.index = this.index + 1;
          this.current = true;
          return true;
        }
        this.key = (object) null;
        this.value = (object) null;
        this.current = false;
        return false;
      }

      public virtual void Reset()
      {
        if (this.version != this.sortedList.version)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
        this.index = this.startIndex;
        this.current = false;
        this.key = (object) null;
        this.value = (object) null;
      }
    }

    [Serializable]
    private class KeyList : IList, ICollection, IEnumerable
    {
      private SortedList sortedList;

      public virtual int Count
      {
        get
        {
          return this.sortedList._size;
        }
      }

      public virtual bool IsReadOnly
      {
        get
        {
          return true;
        }
      }

      public virtual bool IsFixedSize
      {
        get
        {
          return true;
        }
      }

      public virtual bool IsSynchronized
      {
        get
        {
          return this.sortedList.IsSynchronized;
        }
      }

      public virtual object SyncRoot
      {
        get
        {
          return this.sortedList.SyncRoot;
        }
      }

      public virtual object this[int index]
      {
        get
        {
          return this.sortedList.GetKey(index);
        }
        set
        {
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_KeyCollectionSet"));
        }
      }

      internal KeyList(SortedList sortedList)
      {
        this.sortedList = sortedList;
      }

      public virtual int Add(object key)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
      }

      public virtual void Clear()
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
      }

      public virtual bool Contains(object key)
      {
        return this.sortedList.Contains(key);
      }

      public virtual void CopyTo(Array array, int arrayIndex)
      {
        if (array != null && array.Rank != 1)
          throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
        Array.Copy((Array) this.sortedList.keys, 0, array, arrayIndex, this.sortedList.Count);
      }

      public virtual void Insert(int index, object value)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
      }

      public virtual IEnumerator GetEnumerator()
      {
        return (IEnumerator) new SortedList.SortedListEnumerator(this.sortedList, 0, this.sortedList.Count, 1);
      }

      public virtual int IndexOf(object key)
      {
        if (key == null)
          throw new ArgumentNullException("key", Environment.GetResourceString("ArgumentNull_Key"));
        int num = Array.BinarySearch((Array) this.sortedList.keys, 0, this.sortedList.Count, key, this.sortedList.comparer);
        if (num >= 0)
          return num;
        return -1;
      }

      public virtual void Remove(object key)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
      }

      public virtual void RemoveAt(int index)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
      }
    }

    [Serializable]
    private class ValueList : IList, ICollection, IEnumerable
    {
      private SortedList sortedList;

      public virtual int Count
      {
        get
        {
          return this.sortedList._size;
        }
      }

      public virtual bool IsReadOnly
      {
        get
        {
          return true;
        }
      }

      public virtual bool IsFixedSize
      {
        get
        {
          return true;
        }
      }

      public virtual bool IsSynchronized
      {
        get
        {
          return this.sortedList.IsSynchronized;
        }
      }

      public virtual object SyncRoot
      {
        get
        {
          return this.sortedList.SyncRoot;
        }
      }

      public virtual object this[int index]
      {
        get
        {
          return this.sortedList.GetByIndex(index);
        }
        set
        {
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
        }
      }

      internal ValueList(SortedList sortedList)
      {
        this.sortedList = sortedList;
      }

      public virtual int Add(object key)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
      }

      public virtual void Clear()
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
      }

      public virtual bool Contains(object value)
      {
        return this.sortedList.ContainsValue(value);
      }

      public virtual void CopyTo(Array array, int arrayIndex)
      {
        if (array != null && array.Rank != 1)
          throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
        Array.Copy((Array) this.sortedList.values, 0, array, arrayIndex, this.sortedList.Count);
      }

      public virtual void Insert(int index, object value)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
      }

      public virtual IEnumerator GetEnumerator()
      {
        return (IEnumerator) new SortedList.SortedListEnumerator(this.sortedList, 0, this.sortedList.Count, 2);
      }

      public virtual int IndexOf(object value)
      {
        return Array.IndexOf<object>(this.sortedList.values, value, 0, this.sortedList.Count);
      }

      public virtual void Remove(object value)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
      }

      public virtual void RemoveAt(int index)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
      }
    }

    internal class SortedListDebugView
    {
      private SortedList sortedList;

      [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
      public KeyValuePairs[] Items
      {
        get
        {
          return this.sortedList.ToKeyValuePairsArray();
        }
      }

      public SortedListDebugView(SortedList sortedList)
      {
        if (sortedList == null)
          throw new ArgumentNullException("sortedList");
        this.sortedList = sortedList;
      }
    }
  }
}
