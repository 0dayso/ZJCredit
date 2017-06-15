// Decompiled with JetBrains decompiler
// Type: System.Collections.ArrayList
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections
{
  /// <summary>使用大小会根据需要动态增加的数组来实现 <see cref="T:System.Collections.IList" /> 接口。若要浏览此类型的 .NET Framework 源代码，请参阅引用源。</summary>
  /// <filterpriority>1</filterpriority>
  [DebuggerTypeProxy(typeof (ArrayList.ArrayListDebugView))]
  [DebuggerDisplay("Count = {Count}")]
  [ComVisible(true)]
  [Serializable]
  public class ArrayList : IList, ICollection, IEnumerable, ICloneable
  {
    private static readonly object[] emptyArray = EmptyArray<object>.Value;
    private object[] _items;
    private int _size;
    private int _version;
    [NonSerialized]
    private object _syncRoot;
    private const int _defaultCapacity = 4;

    /// <summary>获取或设置 <see cref="T:System.Collections.ArrayList" /> 可包含的元素数。</summary>
    /// <returns>
    /// <see cref="T:System.Collections.ArrayList" /> 可包含的元素数目。</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <see cref="P:System.Collections.ArrayList.Capacity" /> is set to a value that is less than <see cref="P:System.Collections.ArrayList.Count" />.</exception>
    /// <exception cref="T:System.OutOfMemoryException">There is not enough memory available on the system.</exception>
    /// <filterpriority>1</filterpriority>
    public virtual int Capacity
    {
      get
      {
        return this._items.Length;
      }
      set
      {
        if (value < this._size)
          throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_SmallCapacity"));
        if (value == this._items.Length)
          return;
        if (value > 0)
        {
          object[] objArray = new object[value];
          if (this._size > 0)
            Array.Copy((Array) this._items, 0, (Array) objArray, 0, this._size);
          this._items = objArray;
        }
        else
          this._items = new object[4];
      }
    }

    /// <summary>获取 <see cref="T:System.Collections.ArrayList" /> 中实际包含的元素数。</summary>
    /// <returns>
    /// <see cref="T:System.Collections.ArrayList" /> 中实际包含的元素数。</returns>
    /// <filterpriority>1</filterpriority>
    public virtual int Count
    {
      get
      {
        return this._size;
      }
    }

    /// <summary>获取一个值，该值指示 <see cref="T:System.Collections.ArrayList" /> 是否具有固定大小。</summary>
    /// <returns>如果 <see cref="T:System.Collections.ArrayList" /> 具有固定大小，则为 true；否则为 false。默认值为 false。</returns>
    /// <filterpriority>2</filterpriority>
    public virtual bool IsFixedSize
    {
      get
      {
        return false;
      }
    }

    /// <summary>获取一个值，该值指示 <see cref="T:System.Collections.ArrayList" /> 是否为只读。</summary>
    /// <returns>如果 <see cref="T:System.Collections.ArrayList" /> 为只读，则为 true；否则为 false。默认值为 false。</returns>
    /// <filterpriority>2</filterpriority>
    public virtual bool IsReadOnly
    {
      get
      {
        return false;
      }
    }

    /// <summary>获取一个值，该值指示是否同步对 <see cref="T:System.Collections.ArrayList" /> 的访问（线程安全）。</summary>
    /// <returns>如果对 <see cref="T:System.Collections.ArrayList" /> 的访问是同步的（线程安全），则为 true；否则为 false。默认值为 false。</returns>
    /// <filterpriority>2</filterpriority>
    public virtual bool IsSynchronized
    {
      get
      {
        return false;
      }
    }

    /// <summary>获取可用于同步对 <see cref="T:System.Collections.ArrayList" /> 的访问的对象。</summary>
    /// <returns>可用于同步对 <see cref="T:System.Collections.ArrayList" /> 的访问的对象。</returns>
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

    /// <summary>获取或设置指定索引处的元素。</summary>
    /// <returns>指定索引处的元素。</returns>
    /// <param name="index">要获取或设置的元素的从零开始的索引。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> is less than zero.-or- <paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.ArrayList.Count" />. </exception>
    /// <filterpriority>1</filterpriority>
    public virtual object this[int index]
    {
      get
      {
        if (index < 0 || index >= this._size)
          throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
        return this._items[index];
      }
      set
      {
        if (index < 0 || index >= this._size)
          throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
        this._items[index] = value;
        this._version = this._version + 1;
      }
    }

    internal ArrayList(bool trash)
    {
    }

    /// <summary>初始化 <see cref="T:System.Collections.ArrayList" /> 类的新实例，该实例为空并且具有默认初始容量。</summary>
    public ArrayList()
    {
      this._items = ArrayList.emptyArray;
    }

    /// <summary>初始化 <see cref="T:System.Collections.ArrayList" /> 类的新实例，该实例为空并且具有指定的初始容量。</summary>
    /// <param name="capacity">新列表最初可以存储的元素数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="capacity" /> is less than zero. </exception>
    public ArrayList(int capacity)
    {
      if (capacity < 0)
        throw new ArgumentOutOfRangeException("capacity", Environment.GetResourceString("ArgumentOutOfRange_MustBeNonNegNum", (object) "capacity"));
      if (capacity == 0)
        this._items = ArrayList.emptyArray;
      else
        this._items = new object[capacity];
    }

    /// <summary>初始化 <see cref="T:System.Collections.ArrayList" /> 类的新实例，该实例包含从指定集合复制的元素，具有与复制的元素数相同的初始容量。</summary>
    /// <param name="c">
    /// <see cref="T:System.Collections.ICollection" />，它的元素已复制到新列表。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="c" /> is null. </exception>
    public ArrayList(ICollection c)
    {
      if (c == null)
        throw new ArgumentNullException("c", Environment.GetResourceString("ArgumentNull_Collection"));
      int count = c.Count;
      if (count == 0)
      {
        this._items = ArrayList.emptyArray;
      }
      else
      {
        this._items = new object[count];
        this.AddRange(c);
      }
    }

    /// <summary>为一个特定 <see cref="T:System.Collections.IList" /> 创建一个 <see cref="T:System.Collections.ArrayList" /> 包装。</summary>
    /// <returns>在 <see cref="T:System.Collections.IList" /> 周围的 <see cref="T:System.Collections.ArrayList" /> 包装。</returns>
    /// <param name="list">要包装的 <see cref="T:System.Collections.IList" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="list" /> is null.</exception>
    /// <filterpriority>2</filterpriority>
    public static ArrayList Adapter(IList list)
    {
      if (list == null)
        throw new ArgumentNullException("list");
      return (ArrayList) new ArrayList.IListWrapper(list);
    }

    /// <summary>将对象添加到 <see cref="T:System.Collections.ArrayList" /> 的结尾处。</summary>
    /// <returns>
    /// <paramref name="value" /> 已添加的 <see cref="T:System.Collections.ArrayList" /> 索引。</returns>
    /// <param name="value">要添加到 <see cref="T:System.Collections.ArrayList" /> 末尾的 <see cref="T:System.Object" />。该值可以为 null。</param>
    /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.-or- The <see cref="T:System.Collections.ArrayList" /> has a fixed size. </exception>
    /// <filterpriority>1</filterpriority>
    public virtual int Add(object value)
    {
      if (this._size == this._items.Length)
        this.EnsureCapacity(this._size + 1);
      this._items[this._size] = value;
      this._version = this._version + 1;
      int num = this._size;
      this._size = num + 1;
      return num;
    }

    /// <summary>添加 <see cref="T:System.Collections.ICollection" /> 的元素到 <see cref="T:System.Collections.ArrayList" /> 的末尾。</summary>
    /// <param name="c">
    /// <see cref="T:System.Collections.ICollection" />，其元素应添加到 <see cref="T:System.Collections.ArrayList" /> 的末尾。集合本身不能为 null，但它可以包含为 null 的元素。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="c" /> is null. </exception>
    /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.-or- The <see cref="T:System.Collections.ArrayList" /> has a fixed size. </exception>
    /// <filterpriority>1</filterpriority>
    public virtual void AddRange(ICollection c)
    {
      this.InsertRange(this._size, c);
    }

    /// <summary>使用指定的比较器在已排序 <see cref="T:System.Collections.ArrayList" /> 的某个元素范围中搜索元素，并返回该元素从零开始的索引。</summary>
    /// <returns>如果找到 <paramref name="value" />，则为已排序的 <see cref="T:System.Collections.ArrayList" /> 中从零开始的 <paramref name="value" /> 索引；否则为一个负数，它是大于 <paramref name="value" /> 的下一个元素索引的按位求补，如果没有更大的元素，则为 <see cref="P:System.Collections.ArrayList.Count" /> 的按位求补。</returns>
    /// <param name="index">要搜索范围的从零开始的起始索引。</param>
    /// <param name="count">要搜索的范围的长度。</param>
    /// <param name="value">要定位的 <see cref="T:System.Object" />。该值可以为 null。</param>
    /// <param name="comparer">比较元素时要使用的 <see cref="T:System.Collections.IComparer" /> 实现。- 或 - null 使用默认比较器，该比较器是每一个元素的 <see cref="T:System.IComparable" /> 实现。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="index" /> and <paramref name="count" /> do not denote a valid range in the <see cref="T:System.Collections.ArrayList" />.-or- <paramref name="comparer" /> is null and neither <paramref name="value" /> nor the elements of <see cref="T:System.Collections.ArrayList" /> implement the <see cref="T:System.IComparable" /> interface. </exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="comparer" /> is null and <paramref name="value" /> is not of the same type as the elements of the <see cref="T:System.Collections.ArrayList" />. </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> is less than zero.-or- <paramref name="count" /> is less than zero. </exception>
    /// <filterpriority>1</filterpriority>
    public virtual int BinarySearch(int index, int count, object value, IComparer comparer)
    {
      if (index < 0)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (this._size - index < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      return Array.BinarySearch((Array) this._items, index, count, value, comparer);
    }

    /// <summary>使用默认的比较器在整个已排序的 <see cref="T:System.Collections.ArrayList" /> 中搜索元素，并返回该元素从零开始的索引。</summary>
    /// <returns>如果找到 <paramref name="value" />，则为已排序的 <see cref="T:System.Collections.ArrayList" /> 中从零开始的 <paramref name="value" /> 索引；否则为一个负数，它是大于 <paramref name="value" /> 的下一个元素索引的按位求补，如果没有更大的元素，则为 <see cref="P:System.Collections.ArrayList.Count" /> 的按位求补。</returns>
    /// <param name="value">要定位的 <see cref="T:System.Object" />。该值可以为 null。</param>
    /// <exception cref="T:System.ArgumentException">Neither <paramref name="value" /> nor the elements of <see cref="T:System.Collections.ArrayList" /> implement the <see cref="T:System.IComparable" /> interface. </exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="value" /> is not of the same type as the elements of the <see cref="T:System.Collections.ArrayList" />. </exception>
    /// <filterpriority>1</filterpriority>
    public virtual int BinarySearch(object value)
    {
      return this.BinarySearch(0, this.Count, value, (IComparer) null);
    }

    /// <summary>使用指定的比较器在整个已排序的 <see cref="T:System.Collections.ArrayList" /> 中搜索元素，并返回该元素从零开始的索引。</summary>
    /// <returns>如果找到 <paramref name="value" />，则为已排序的 <see cref="T:System.Collections.ArrayList" /> 中从零开始的 <paramref name="value" /> 索引；否则为一个负数，它是大于 <paramref name="value" /> 的下一个元素索引的按位求补，如果没有更大的元素，则为 <see cref="P:System.Collections.ArrayList.Count" /> 的按位求补。</returns>
    /// <param name="value">要定位的 <see cref="T:System.Object" />。该值可以为 null。</param>
    /// <param name="comparer">比较元素时要使用的 <see cref="T:System.Collections.IComparer" /> 实现。- 或 - null 使用默认比较器，该比较器是每一个元素的 <see cref="T:System.IComparable" /> 实现。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="comparer" /> is null and neither <paramref name="value" /> nor the elements of <see cref="T:System.Collections.ArrayList" /> implement the <see cref="T:System.IComparable" /> interface. </exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="comparer" /> is null and <paramref name="value" /> is not of the same type as the elements of the <see cref="T:System.Collections.ArrayList" />. </exception>
    /// <filterpriority>1</filterpriority>
    public virtual int BinarySearch(object value, IComparer comparer)
    {
      return this.BinarySearch(0, this.Count, value, comparer);
    }

    /// <summary>从 <see cref="T:System.Collections.ArrayList" /> 中移除所有元素。</summary>
    /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.-or- The <see cref="T:System.Collections.ArrayList" /> has a fixed size. </exception>
    /// <filterpriority>1</filterpriority>
    public virtual void Clear()
    {
      if (this._size > 0)
      {
        Array.Clear((Array) this._items, 0, this._size);
        this._size = 0;
      }
      this._version = this._version + 1;
    }

    /// <summary>创建 <see cref="T:System.Collections.ArrayList" /> 的浅表副本。</summary>
    /// <returns>
    /// <see cref="T:System.Collections.ArrayList" /> 的浅表副本。</returns>
    /// <filterpriority>2</filterpriority>
    public virtual object Clone()
    {
      ArrayList arrayList = new ArrayList(this._size);
      arrayList._size = this._size;
      arrayList._version = this._version;
      Array.Copy((Array) this._items, 0, (Array) arrayList._items, 0, this._size);
      return (object) arrayList;
    }

    /// <summary>确定某元素是否在 <see cref="T:System.Collections.ArrayList" /> 中。</summary>
    /// <returns>如果在 <see cref="T:System.Collections.ArrayList" /> 中找到了 <paramref name="item" />，则为 true；否则为 false。</returns>
    /// <param name="item">要在 <see cref="T:System.Collections.ArrayList" /> 中定位的 <see cref="T:System.Object" />。该值可以为 null。</param>
    /// <filterpriority>1</filterpriority>
    public virtual bool Contains(object item)
    {
      if (item == null)
      {
        for (int index = 0; index < this._size; ++index)
        {
          if (this._items[index] == null)
            return true;
        }
        return false;
      }
      for (int index = 0; index < this._size; ++index)
      {
        if (this._items[index] != null && this._items[index].Equals(item))
          return true;
      }
      return false;
    }

    /// <summary>从目标数组的开头开始，将整个 <see cref="T:System.Collections.ArrayList" /> 复制到一维兼容 <see cref="T:System.Array" />。</summary>
    /// <param name="array">一维 <see cref="T:System.Array" />，它是从 <see cref="T:System.Collections.ArrayList" /> 复制的元素的目标。<see cref="T:System.Array" /> 必须具有从零开始的索引。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> is null. </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="array" /> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.ArrayList" /> is greater than the number of elements that the destination <paramref name="array" /> can contain. </exception>
    /// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.ArrayList" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
    /// <filterpriority>2</filterpriority>
    public virtual void CopyTo(Array array)
    {
      this.CopyTo(array, 0);
    }

    /// <summary>从目标数组的指定索引处开始将整个 <see cref="T:System.Collections.ArrayList" /> 复制到兼容的一维 <see cref="T:System.Array" />。</summary>
    /// <param name="array">一维 <see cref="T:System.Array" />，它是从 <see cref="T:System.Collections.ArrayList" /> 复制的元素的目标。<see cref="T:System.Array" /> 必须具有从零开始的索引。</param>
    /// <param name="arrayIndex">
    /// <paramref name="array" /> 中从零开始的索引，从此索引处开始进行复制。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> is null. </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="arrayIndex" /> is less than zero. </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="array" /> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.ArrayList" /> is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />. </exception>
    /// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.ArrayList" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
    /// <filterpriority>2</filterpriority>
    public virtual void CopyTo(Array array, int arrayIndex)
    {
      if (array != null && array.Rank != 1)
        throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
      Array.Copy((Array) this._items, 0, array, arrayIndex, this._size);
    }

    /// <summary>从目标数组的指定索引处开始，将元素的范围从 <see cref="T:System.Collections.ArrayList" /> 复制到一维兼容 <see cref="T:System.Array" />。</summary>
    /// <param name="index">复制即从源 <see cref="T:System.Collections.ArrayList" /> 中从零开始的索引开始。</param>
    /// <param name="array">一维 <see cref="T:System.Array" />，它是从 <see cref="T:System.Collections.ArrayList" /> 复制的元素的目标。<see cref="T:System.Array" /> 必须具有从零开始的索引。</param>
    /// <param name="arrayIndex">
    /// <paramref name="array" /> 中从零开始的索引，从此索引处开始进行复制。</param>
    /// <param name="count">要复制的元素数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> is null. </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> is less than zero.-or- <paramref name="arrayIndex" /> is less than zero.-or- <paramref name="count" /> is less than zero. </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="array" /> is multidimensional.-or- <paramref name="index" /> is equal to or greater than the <see cref="P:System.Collections.ArrayList.Count" /> of the source <see cref="T:System.Collections.ArrayList" />.-or- The number of elements from <paramref name="index" /> to the end of the source <see cref="T:System.Collections.ArrayList" /> is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />. </exception>
    /// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.ArrayList" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
    /// <filterpriority>2</filterpriority>
    public virtual void CopyTo(int index, Array array, int arrayIndex, int count)
    {
      if (this._size - index < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (array != null && array.Rank != 1)
        throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
      Array.Copy((Array) this._items, index, array, arrayIndex, count);
    }

    private void EnsureCapacity(int min)
    {
      if (this._items.Length >= min)
        return;
      int num = this._items.Length == 0 ? 4 : this._items.Length * 2;
      if ((uint) num > 2146435071U)
        num = 2146435071;
      if (num < min)
        num = min;
      this.Capacity = num;
    }

    /// <summary>返回具有固定大小的 <see cref="T:System.Collections.IList" /> 包装。</summary>
    /// <returns>具有固定大小的 <see cref="T:System.Collections.IList" /> 包装。</returns>
    /// <param name="list">要包装的 <see cref="T:System.Collections.IList" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="list" /> is null. </exception>
    /// <filterpriority>2</filterpriority>
    public static IList FixedSize(IList list)
    {
      if (list == null)
        throw new ArgumentNullException("list");
      return (IList) new ArrayList.FixedSizeList(list);
    }

    /// <summary>返回具有固定大小的 <see cref="T:System.Collections.ArrayList" /> 包装。</summary>
    /// <returns>具有固定大小的 <see cref="T:System.Collections.ArrayList" /> 包装。</returns>
    /// <param name="list">要包装的 <see cref="T:System.Collections.ArrayList" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="list" /> is null. </exception>
    /// <filterpriority>2</filterpriority>
    public static ArrayList FixedSize(ArrayList list)
    {
      if (list == null)
        throw new ArgumentNullException("list");
      return (ArrayList) new ArrayList.FixedSizeArrayList(list);
    }

    /// <summary>返回用于整个 <see cref="T:System.Collections.ArrayList" /> 的枚举数。</summary>
    /// <returns>用于整个 <see cref="T:System.Collections.ArrayList" /> 的 <see cref="T:System.Collections.IEnumerator" />。</returns>
    /// <filterpriority>2</filterpriority>
    public virtual IEnumerator GetEnumerator()
    {
      return (IEnumerator) new ArrayList.ArrayListEnumeratorSimple(this);
    }

    /// <summary>返回用于 <see cref="T:System.Collections.ArrayList" /> 中元素的范围的枚举数。</summary>
    /// <returns>一个 <see cref="T:System.Collections.IEnumerator" />，用于 <see cref="T:System.Collections.ArrayList" /> 中指定的元素范围。</returns>
    /// <param name="index">枚举数应引用的 <see cref="T:System.Collections.ArrayList" /> 部分从零开始的起始索引。</param>
    /// <param name="count">枚举数应引用的 <see cref="T:System.Collections.ArrayList" /> 部分中的元素数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> is less than zero.-or- <paramref name="count" /> is less than zero. </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="index" /> and <paramref name="count" /> do not specify a valid range in the <see cref="T:System.Collections.ArrayList" />. </exception>
    /// <filterpriority>2</filterpriority>
    public virtual IEnumerator GetEnumerator(int index, int count)
    {
      if (index < 0)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (this._size - index < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      return (IEnumerator) new ArrayList.ArrayListEnumerator(this, index, count);
    }

    /// <summary>搜索指定的 <see cref="T:System.Object" />，并返回整个 <see cref="T:System.Collections.ArrayList" /> 中第一个匹配项的从零开始索引。</summary>
    /// <returns>如果在整个 <see cref="T:System.Collections.ArrayList" /> 中找到 <paramref name="value" /> 的第一个匹配项，则为该项的从零开始的索引；否则为 -1。</returns>
    /// <param name="value">要在 <see cref="T:System.Collections.ArrayList" /> 中定位的 <see cref="T:System.Object" />。该值可以为 null。</param>
    /// <filterpriority>1</filterpriority>
    public virtual int IndexOf(object value)
    {
      return Array.IndexOf((Array) this._items, value, 0, this._size);
    }

    /// <summary>搜索指定的 <see cref="T:System.Object" />，并返回 <see cref="T:System.Collections.ArrayList" /> 中从指定索引到最后一个元素这部分元素中第一个匹配项的从零开始索引。</summary>
    /// <returns>如果在 <see cref="T:System.Collections.ArrayList" /> 中从 <paramref name="startIndex" /> 到最后一个元素的元素范围内找到 <paramref name="value" /> 的第一个匹配项，则为该项的从零开始的索引；否则为 -1。</returns>
    /// <param name="value">要在 <see cref="T:System.Collections.ArrayList" /> 中定位的 <see cref="T:System.Object" />。该值可以为 null。</param>
    /// <param name="startIndex">从零开始的搜索的起始索引。空列表中 0（零）为有效值。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> is outside the range of valid indexes for the <see cref="T:System.Collections.ArrayList" />. </exception>
    /// <filterpriority>1</filterpriority>
    public virtual int IndexOf(object value, int startIndex)
    {
      if (startIndex > this._size)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      return Array.IndexOf((Array) this._items, value, startIndex, this._size - startIndex);
    }

    /// <summary>搜索指定的 <see cref="T:System.Object" />，并返回 <see cref="T:System.Collections.ArrayList" /> 中从指定索引开始并包含指定元素数的这部分元素中第一个匹配项的从零开始索引。</summary>
    /// <returns>如果在 <see cref="T:System.Collections.ArrayList" /> 中从 <paramref name="startIndex" /> 开始并包含 <paramref name="count" /> 个元素的元素范围内找到 <paramref name="value" /> 的第一个匹配项，则为该项的从零开始的索引；否则为 -1。</returns>
    /// <param name="value">要在 <see cref="T:System.Collections.ArrayList" /> 中定位的 <see cref="T:System.Object" />。该值可以为 null。</param>
    /// <param name="startIndex">从零开始的搜索的起始索引。空列表中 0（零）为有效值。</param>
    /// <param name="count">要搜索的部分中的元素数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> is outside the range of valid indexes for the <see cref="T:System.Collections.ArrayList" />.-or- <paramref name="count" /> is less than zero.-or- <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in the <see cref="T:System.Collections.ArrayList" />. </exception>
    /// <filterpriority>1</filterpriority>
    public virtual int IndexOf(object value, int startIndex, int count)
    {
      if (startIndex > this._size)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (count < 0 || startIndex > this._size - count)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
      return Array.IndexOf((Array) this._items, value, startIndex, count);
    }

    /// <summary>将元素插入 <see cref="T:System.Collections.ArrayList" /> 的指定索引处。</summary>
    /// <param name="index">从零开始的索引，应在该位置插入 <paramref name="value" />。</param>
    /// <param name="value">要插入的 <see cref="T:System.Object" />。该值可以为 null。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> is less than zero.-or- <paramref name="index" /> is greater than <see cref="P:System.Collections.ArrayList.Count" />. </exception>
    /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.-or- The <see cref="T:System.Collections.ArrayList" /> has a fixed size. </exception>
    /// <filterpriority>1</filterpriority>
    public virtual void Insert(int index, object value)
    {
      if (index < 0 || index > this._size)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_ArrayListInsert"));
      if (this._size == this._items.Length)
        this.EnsureCapacity(this._size + 1);
      if (index < this._size)
        Array.Copy((Array) this._items, index, (Array) this._items, index + 1, this._size - index);
      this._items[index] = value;
      this._size = this._size + 1;
      this._version = this._version + 1;
    }

    /// <summary>将集合中的某个元素插入 <see cref="T:System.Collections.ArrayList" /> 的指定索引处。</summary>
    /// <param name="index">应在此处插入新元素的从零开始的索引。</param>
    /// <param name="c">
    /// <see cref="T:System.Collections.ICollection" />，应将其元素插入到 <see cref="T:System.Collections.ArrayList" /> 中。集合本身不能为 null，但它可以包含为 null 的元素。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="c" /> is null. </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> is less than zero.-or- <paramref name="index" /> is greater than <see cref="P:System.Collections.ArrayList.Count" />. </exception>
    /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.-or- The <see cref="T:System.Collections.ArrayList" /> has a fixed size. </exception>
    /// <filterpriority>2</filterpriority>
    public virtual void InsertRange(int index, ICollection c)
    {
      if (c == null)
        throw new ArgumentNullException("c", Environment.GetResourceString("ArgumentNull_Collection"));
      if (index < 0 || index > this._size)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      int count = c.Count;
      if (count <= 0)
        return;
      this.EnsureCapacity(this._size + count);
      if (index < this._size)
        Array.Copy((Array) this._items, index, (Array) this._items, index + count, this._size - index);
      object[] objArray = new object[count];
      c.CopyTo((Array) objArray, 0);
      objArray.CopyTo((Array) this._items, index);
      this._size = this._size + count;
      this._version = this._version + 1;
    }

    /// <summary>搜索指定的 <see cref="T:System.Object" />，并返回整个 <see cref="T:System.Collections.ArrayList" /> 中最后一个匹配项的从零开始索引。</summary>
    /// <returns>如果在整个 <see cref="T:System.Collections.ArrayList" /> 中找到 <paramref name="value" /> 的最后一个匹配项，则为该项的从零开始的索引；否则为 -1。</returns>
    /// <param name="value">要在 <see cref="T:System.Collections.ArrayList" /> 中定位的 <see cref="T:System.Object" />。该值可以为 null。</param>
    /// <filterpriority>2</filterpriority>
    public virtual int LastIndexOf(object value)
    {
      return this.LastIndexOf(value, this._size - 1, this._size);
    }

    /// <summary>搜索指定的 <see cref="T:System.Object" />，并返回 <see cref="T:System.Collections.ArrayList" /> 中从第一个元素到指定索引这部分元素中最后一个匹配项的从零开始索引。</summary>
    /// <returns>如果在 <see cref="T:System.Collections.ArrayList" /> 中从第一个元素到 <paramref name="startIndex" /> 的元素范围内找到 <paramref name="value" /> 的最后一个匹配项，则为该项的从零开始的索引；否则为 -1。</returns>
    /// <param name="value">要在 <see cref="T:System.Collections.ArrayList" /> 中定位的 <see cref="T:System.Object" />。该值可以为 null。</param>
    /// <param name="startIndex">向后搜索的从零开始的起始索引。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> is outside the range of valid indexes for the <see cref="T:System.Collections.ArrayList" />. </exception>
    /// <filterpriority>2</filterpriority>
    public virtual int LastIndexOf(object value, int startIndex)
    {
      if (startIndex >= this._size)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      object obj = value;
      int startIndex1 = startIndex;
      int num = 1;
      int count = startIndex1 + num;
      return this.LastIndexOf(obj, startIndex1, count);
    }

    /// <summary>搜索指定的 <see cref="T:System.Object" />，并返回 <see cref="T:System.Collections.ArrayList" /> 中到指定索引为止包含指定元素数的这部分元素中最后一个匹配项的从零开始索引。</summary>
    /// <returns>如果找到包含 <paramref name="count" /> 元素数和到 <paramref name="startIndex" /> 为止的索引，则为 <see cref="T:System.Collections.ArrayList" /> 中元素范围内 <paramref name="value" /> 最后一个匹配项的从零开始索引；否则为 -1。</returns>
    /// <param name="value">要在 <see cref="T:System.Collections.ArrayList" /> 中定位的 <see cref="T:System.Object" />。该值可以为 null。</param>
    /// <param name="startIndex">向后搜索的从零开始的起始索引。</param>
    /// <param name="count">要搜索的部分中的元素数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> is outside the range of valid indexes for the <see cref="T:System.Collections.ArrayList" />.-or- <paramref name="count" /> is less than zero.-or- <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in the <see cref="T:System.Collections.ArrayList" />. </exception>
    /// <filterpriority>2</filterpriority>
    public virtual int LastIndexOf(object value, int startIndex, int count)
    {
      if (this.Count != 0 && (startIndex < 0 || count < 0))
        throw new ArgumentOutOfRangeException(startIndex < 0 ? "startIndex" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (this._size == 0)
        return -1;
      if (startIndex >= this._size || count > startIndex + 1)
        throw new ArgumentOutOfRangeException(startIndex >= this._size ? "startIndex" : "count", Environment.GetResourceString("ArgumentOutOfRange_BiggerThanCollection"));
      return Array.LastIndexOf((Array) this._items, value, startIndex, count);
    }

    /// <summary>返回只读的 <see cref="T:System.Collections.IList" /> 包装。</summary>
    /// <returns>一个围绕 <paramref name="list" /> 的只读 <see cref="T:System.Collections.IList" /> 包装。</returns>
    /// <param name="list">要包装的 <see cref="T:System.Collections.IList" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="list" /> is null. </exception>
    /// <filterpriority>2</filterpriority>
    public static IList ReadOnly(IList list)
    {
      if (list == null)
        throw new ArgumentNullException("list");
      return (IList) new ArrayList.ReadOnlyList(list);
    }

    /// <summary>返回只读的 <see cref="T:System.Collections.ArrayList" /> 包装。</summary>
    /// <returns>一个围绕 <paramref name="list" /> 的只读 <see cref="T:System.Collections.ArrayList" /> 包装。</returns>
    /// <param name="list">要包装的 <see cref="T:System.Collections.ArrayList" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="list" /> is null. </exception>
    /// <filterpriority>2</filterpriority>
    public static ArrayList ReadOnly(ArrayList list)
    {
      if (list == null)
        throw new ArgumentNullException("list");
      return (ArrayList) new ArrayList.ReadOnlyArrayList(list);
    }

    /// <summary>从 <see cref="T:System.Collections.ArrayList" /> 中移除特定对象的第一个匹配项。</summary>
    /// <param name="obj">要从 <see cref="T:System.Collections.ArrayList" /> 中移除的 <see cref="T:System.Object" />。该值可以为 null。</param>
    /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.-or- The <see cref="T:System.Collections.ArrayList" /> has a fixed size. </exception>
    /// <filterpriority>1</filterpriority>
    public virtual void Remove(object obj)
    {
      int index = this.IndexOf(obj);
      if (index < 0)
        return;
      this.RemoveAt(index);
    }

    /// <summary>移除 <see cref="T:System.Collections.ArrayList" /> 的指定索引处的元素。</summary>
    /// <param name="index">要移除的元素的从零开始的索引。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> is less than zero.-or- <paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.ArrayList.Count" />. </exception>
    /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.-or- The <see cref="T:System.Collections.ArrayList" /> has a fixed size. </exception>
    /// <filterpriority>1</filterpriority>
    public virtual void RemoveAt(int index)
    {
      if (index < 0 || index >= this._size)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      this._size = this._size - 1;
      if (index < this._size)
        Array.Copy((Array) this._items, index + 1, (Array) this._items, index, this._size - index);
      this._items[this._size] = (object) null;
      this._version = this._version + 1;
    }

    /// <summary>从 <see cref="T:System.Collections.ArrayList" /> 中移除一定范围的元素。</summary>
    /// <param name="index">要移除的元素范围的从零开始的起始索引。</param>
    /// <param name="count">要移除的元素数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> is less than zero.-or- <paramref name="count" /> is less than zero. </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="index" /> and <paramref name="count" /> do not denote a valid range of elements in the <see cref="T:System.Collections.ArrayList" />. </exception>
    /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.-or- The <see cref="T:System.Collections.ArrayList" /> has a fixed size. </exception>
    /// <filterpriority>2</filterpriority>
    public virtual void RemoveRange(int index, int count)
    {
      if (index < 0)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (this._size - index < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (count <= 0)
        return;
      int num = this._size;
      this._size = this._size - count;
      if (index < this._size)
        Array.Copy((Array) this._items, index + count, (Array) this._items, index, this._size - index);
      while (num > this._size)
        this._items[--num] = (object) null;
      this._version = this._version + 1;
    }

    /// <summary>返回一个 <see cref="T:System.Collections.ArrayList" />，它的元素是指定值的副本。</summary>
    /// <returns>一个带 <paramref name="count" /> 元素数的 <see cref="T:System.Collections.ArrayList" />，所有这些都是 <paramref name="value" /> 的副本。</returns>
    /// <param name="value">要在新 <see cref="T:System.Collections.ArrayList" /> 中对其进行多次复制的 <see cref="T:System.Object" />。该值可以为 null。</param>
    /// <param name="count">
    /// <paramref name="value" /> 应复制的次数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="count" /> is less than zero. </exception>
    /// <filterpriority>2</filterpriority>
    public static ArrayList Repeat(object value, int count)
    {
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      ArrayList arrayList = new ArrayList(count > 4 ? count : 4);
      for (int index = 0; index < count; ++index)
        arrayList.Add(value);
      return arrayList;
    }

    /// <summary>将整个 <see cref="T:System.Collections.ArrayList" /> 中元素的顺序反转。</summary>
    /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only. </exception>
    /// <filterpriority>2</filterpriority>
    public virtual void Reverse()
    {
      this.Reverse(0, this.Count);
    }

    /// <summary>将指定范围中元素的顺序反转。</summary>
    /// <param name="index">要反转的范围的从零开始的起始索引。</param>
    /// <param name="count">要反转的范围内的元素数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> is less than zero.-or- <paramref name="count" /> is less than zero. </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="index" /> and <paramref name="count" /> do not denote a valid range of elements in the <see cref="T:System.Collections.ArrayList" />. </exception>
    /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only. </exception>
    /// <filterpriority>2</filterpriority>
    public virtual void Reverse(int index, int count)
    {
      if (index < 0)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (this._size - index < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      Array.Reverse((Array) this._items, index, count);
      this._version = this._version + 1;
    }

    /// <summary>在 <see cref="T:System.Collections.ArrayList" /> 中的元素范围内复制集合的元素。</summary>
    /// <param name="index">从零开始的 <see cref="T:System.Collections.ArrayList" /> 索引，从此开始复制 <paramref name="c" /> 的元素。</param>
    /// <param name="c">
    /// <see cref="T:System.Collections.ICollection" />，它的元素要复制到 <see cref="T:System.Collections.ArrayList" />。集合本身不能为 null，但它可以包含为 null 的元素。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> is less than zero.-or- <paramref name="index" /> plus the number of elements in <paramref name="c" /> is greater than <see cref="P:System.Collections.ArrayList.Count" />. </exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="c" /> is null. </exception>
    /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only. </exception>
    /// <filterpriority>2</filterpriority>
    public virtual void SetRange(int index, ICollection c)
    {
      if (c == null)
        throw new ArgumentNullException("c", Environment.GetResourceString("ArgumentNull_Collection"));
      int count = c.Count;
      if (index < 0 || index > this._size - count)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (count <= 0)
        return;
      c.CopyTo((Array) this._items, index);
      this._version = this._version + 1;
    }

    /// <summary>返回一个 <see cref="T:System.Collections.ArrayList" />，它代表源 <see cref="T:System.Collections.ArrayList" /> 中的元素子集。</summary>
    /// <returns>一个 <see cref="T:System.Collections.ArrayList" />，它代表源 <see cref="T:System.Collections.ArrayList" /> 中的元素子集。</returns>
    /// <param name="index">范围开始处的从零开始的 <see cref="T:System.Collections.ArrayList" /> 索引。</param>
    /// <param name="count">范围中的元素数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> is less than zero.-or- <paramref name="count" /> is less than zero. </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="index" /> and <paramref name="count" /> do not denote a valid range of elements in the <see cref="T:System.Collections.ArrayList" />. </exception>
    /// <filterpriority>2</filterpriority>
    public virtual ArrayList GetRange(int index, int count)
    {
      if (index < 0 || count < 0)
        throw new ArgumentOutOfRangeException(index < 0 ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (this._size - index < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      return (ArrayList) new ArrayList.Range(this, index, count);
    }

    /// <summary>对整个 <see cref="T:System.Collections.ArrayList" /> 中的元素进行排序。</summary>
    /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only. </exception>
    /// <filterpriority>1</filterpriority>
    public virtual void Sort()
    {
      this.Sort(0, this.Count, (IComparer) Comparer.Default);
    }

    /// <summary>使用指定的比较器对整个 <see cref="T:System.Collections.ArrayList" /> 中的元素进行排序。</summary>
    /// <param name="comparer">比较元素时要使用的 <see cref="T:System.Collections.IComparer" /> 实现。- 或 -一个空引用（在 Visual Basic 中为 Nothing），将使用每个元素的 <see cref="T:System.IComparable" /> 实现。</param>
    /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only. </exception>
    /// <exception cref="T:System.InvalidOperationException">An error occurred while comparing two elements.</exception>
    /// <exception cref="T:System.ArgumentException">null is passed for <paramref name="comparer" />, and the elements in the list do not implement <see cref="T:System.IComparable" />.</exception>
    /// <filterpriority>1</filterpriority>
    public virtual void Sort(IComparer comparer)
    {
      this.Sort(0, this.Count, comparer);
    }

    /// <summary>使用指定的比较器对 <see cref="T:System.Collections.ArrayList" /> 中某个范围内的元素进行排序。</summary>
    /// <param name="index">要排序范围的从零开始的起始索引。</param>
    /// <param name="count">要排序的范围的长度。</param>
    /// <param name="comparer">比较元素时要使用的 <see cref="T:System.Collections.IComparer" /> 实现。- 或 -一个空引用（在 Visual Basic 中为 Nothing），将使用每个元素的 <see cref="T:System.IComparable" /> 实现。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> is less than zero.-or- <paramref name="count" /> is less than zero. </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="index" /> and <paramref name="count" /> do not specify a valid range in the <see cref="T:System.Collections.ArrayList" />. </exception>
    /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only. </exception>
    /// <exception cref="T:System.InvalidOperationException">An error occurred while comparing two elements.</exception>
    /// <filterpriority>1</filterpriority>
    public virtual void Sort(int index, int count, IComparer comparer)
    {
      if (index < 0)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (this._size - index < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      Array.Sort((Array) this._items, index, count, comparer);
      this._version = this._version + 1;
    }

    /// <summary>返回同步的（线程安全）<see cref="T:System.Collections.IList" /> 包装。</summary>
    /// <returns>同步的（线程安全）<see cref="T:System.Collections.IList" /> 包装。</returns>
    /// <param name="list">要同步的 <see cref="T:System.Collections.IList" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="list" /> is null. </exception>
    /// <filterpriority>2</filterpriority>
    [HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
    public static IList Synchronized(IList list)
    {
      if (list == null)
        throw new ArgumentNullException("list");
      return (IList) new ArrayList.SyncIList(list);
    }

    /// <summary>返回同步的（线程安全）<see cref="T:System.Collections.ArrayList" /> 包装。</summary>
    /// <returns>同步的（线程安全）<see cref="T:System.Collections.ArrayList" /> 包装。</returns>
    /// <param name="list">要同步的 <see cref="T:System.Collections.ArrayList" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="list" /> is null. </exception>
    /// <filterpriority>2</filterpriority>
    [HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
    public static ArrayList Synchronized(ArrayList list)
    {
      if (list == null)
        throw new ArgumentNullException("list");
      return (ArrayList) new ArrayList.SyncArrayList(list);
    }

    /// <summary>将 <see cref="T:System.Collections.ArrayList" /> 的元素复制到一个新的 <see cref="T:System.Object" /> 数组。</summary>
    /// <returns>一个 <see cref="T:System.Object" /> 数组，包含 <see cref="T:System.Collections.ArrayList" /> 的元素的副本。</returns>
    /// <filterpriority>1</filterpriority>
    public virtual object[] ToArray()
    {
      object[] objArray = new object[this._size];
      Array.Copy((Array) this._items, 0, (Array) objArray, 0, this._size);
      return objArray;
    }

    /// <summary>将 <see cref="T:System.Collections.ArrayList" /> 的元素复制到一个指定元素类型的新数组。</summary>
    /// <returns>特定元素类型的数组包含 <see cref="T:System.Collections.ArrayList" /> 的元素的副本。</returns>
    /// <param name="type">要创建和复制元素的目标数组的元素 <see cref="T:System.Type" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="type" /> is null. </exception>
    /// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.ArrayList" /> cannot be cast automatically to the specified type. </exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    public virtual Array ToArray(Type type)
    {
      if (type == (Type) null)
        throw new ArgumentNullException("type");
      Array instance = Array.UnsafeCreateInstance(type, this._size);
      Array.Copy((Array) this._items, 0, instance, 0, this._size);
      return instance;
    }

    /// <summary>将容量设置为 <see cref="T:System.Collections.ArrayList" /> 中元素的实际数目。</summary>
    /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.-or- The <see cref="T:System.Collections.ArrayList" /> has a fixed size. </exception>
    /// <filterpriority>2</filterpriority>
    public virtual void TrimToSize()
    {
      this.Capacity = this._size;
    }

    [Serializable]
    private class IListWrapper : ArrayList
    {
      private IList _list;

      public override int Capacity
      {
        get
        {
          return this._list.Count;
        }
        set
        {
          if (value < this.Count)
            throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_SmallCapacity"));
        }
      }

      public override int Count
      {
        get
        {
          return this._list.Count;
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
          return this._list.IsSynchronized;
        }
      }

      public override object this[int index]
      {
        get
        {
          return this._list[index];
        }
        set
        {
          this._list[index] = value;
          this._version = this._version + 1;
        }
      }

      public override object SyncRoot
      {
        get
        {
          return this._list.SyncRoot;
        }
      }

      internal IListWrapper(IList list)
      {
        this._list = list;
        this._version = 0;
      }

      public override int Add(object obj)
      {
        int num = this._list.Add(obj);
        this._version = this._version + 1;
        return num;
      }

      public override void AddRange(ICollection c)
      {
        this.InsertRange(this.Count, c);
      }

      public override int BinarySearch(int index, int count, object value, IComparer comparer)
      {
        if (index < 0 || count < 0)
          throw new ArgumentOutOfRangeException(index < 0 ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        if (this.Count - index < count)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
        if (comparer == null)
          comparer = (IComparer) Comparer.Default;
        int num1 = index;
        int num2 = index + count - 1;
        while (num1 <= num2)
        {
          int index1 = (num1 + num2) / 2;
          int num3 = comparer.Compare(value, this._list[index1]);
          if (num3 == 0)
            return index1;
          if (num3 < 0)
            num2 = index1 - 1;
          else
            num1 = index1 + 1;
        }
        return ~num1;
      }

      public override void Clear()
      {
        if (this._list.IsFixedSize)
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
        this._list.Clear();
        this._version = this._version + 1;
      }

      public override object Clone()
      {
        return (object) new ArrayList.IListWrapper(this._list);
      }

      public override bool Contains(object obj)
      {
        return this._list.Contains(obj);
      }

      public override void CopyTo(Array array, int index)
      {
        this._list.CopyTo(array, index);
      }

      public override void CopyTo(int index, Array array, int arrayIndex, int count)
      {
        if (array == null)
          throw new ArgumentNullException("array");
        if (index < 0 || arrayIndex < 0)
          throw new ArgumentOutOfRangeException(index < 0 ? "index" : "arrayIndex", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        if (count < 0)
          throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        if (array.Length - arrayIndex < count)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
        if (array.Rank != 1)
          throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
        if (this._list.Count - index < count)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
        for (int index1 = index; index1 < index + count; ++index1)
          array.SetValue(this._list[index1], arrayIndex++);
      }

      public override IEnumerator GetEnumerator()
      {
        return this._list.GetEnumerator();
      }

      public override IEnumerator GetEnumerator(int index, int count)
      {
        if (index < 0 || count < 0)
          throw new ArgumentOutOfRangeException(index < 0 ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        if (this._list.Count - index < count)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
        return (IEnumerator) new ArrayList.IListWrapper.IListWrapperEnumWrapper(this, index, count);
      }

      public override int IndexOf(object value)
      {
        return this._list.IndexOf(value);
      }

      public override int IndexOf(object value, int startIndex)
      {
        return this.IndexOf(value, startIndex, this._list.Count - startIndex);
      }

      public override int IndexOf(object value, int startIndex, int count)
      {
        if (startIndex < 0 || startIndex > this.Count)
          throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
        if (count < 0 || startIndex > this.Count - count)
          throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
        int num = startIndex + count;
        if (value == null)
        {
          for (int index = startIndex; index < num; ++index)
          {
            if (this._list[index] == null)
              return index;
          }
          return -1;
        }
        for (int index = startIndex; index < num; ++index)
        {
          if (this._list[index] != null && this._list[index].Equals(value))
            return index;
        }
        return -1;
      }

      public override void Insert(int index, object obj)
      {
        this._list.Insert(index, obj);
        this._version = this._version + 1;
      }

      public override void InsertRange(int index, ICollection c)
      {
        if (c == null)
          throw new ArgumentNullException("c", Environment.GetResourceString("ArgumentNull_Collection"));
        if (index < 0 || index > this.Count)
          throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
        if (c.Count <= 0)
          return;
        ArrayList arrayList = this._list as ArrayList;
        if (arrayList != null)
        {
          arrayList.InsertRange(index, c);
        }
        else
        {
          foreach (object obj in (IEnumerable) c)
            this._list.Insert(index++, obj);
        }
        this._version = this._version + 1;
      }

      public override int LastIndexOf(object value)
      {
        return this.LastIndexOf(value, this._list.Count - 1, this._list.Count);
      }

      public override int LastIndexOf(object value, int startIndex)
      {
        object obj = value;
        int startIndex1 = startIndex;
        int num = 1;
        int count = startIndex1 + num;
        return this.LastIndexOf(obj, startIndex1, count);
      }

      public override int LastIndexOf(object value, int startIndex, int count)
      {
        if (this._list.Count == 0)
          return -1;
        if (startIndex < 0 || startIndex >= this._list.Count)
          throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
        if (count < 0 || count > startIndex + 1)
          throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
        int num = startIndex - count + 1;
        if (value == null)
        {
          for (int index = startIndex; index >= num; --index)
          {
            if (this._list[index] == null)
              return index;
          }
          return -1;
        }
        for (int index = startIndex; index >= num; --index)
        {
          if (this._list[index] != null && this._list[index].Equals(value))
            return index;
        }
        return -1;
      }

      public override void Remove(object value)
      {
        int index = this.IndexOf(value);
        if (index < 0)
          return;
        this.RemoveAt(index);
      }

      public override void RemoveAt(int index)
      {
        this._list.RemoveAt(index);
        this._version = this._version + 1;
      }

      public override void RemoveRange(int index, int count)
      {
        if (index < 0 || count < 0)
          throw new ArgumentOutOfRangeException(index < 0 ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        if (this._list.Count - index < count)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
        if (count > 0)
          this._version = this._version + 1;
        for (; count > 0; --count)
          this._list.RemoveAt(index);
      }

      public override void Reverse(int index, int count)
      {
        if (index < 0 || count < 0)
          throw new ArgumentOutOfRangeException(index < 0 ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        if (this._list.Count - index < count)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
        int index1 = index;
        object obj;
        for (int index2 = index + count - 1; index1 < index2; this._list[index2--] = obj)
        {
          obj = this._list[index1];
          this._list[index1++] = this._list[index2];
        }
        this._version = this._version + 1;
      }

      public override void SetRange(int index, ICollection c)
      {
        if (c == null)
          throw new ArgumentNullException("c", Environment.GetResourceString("ArgumentNull_Collection"));
        if (index < 0 || index > this._list.Count - c.Count)
          throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
        if (c.Count <= 0)
          return;
        foreach (object obj in (IEnumerable) c)
          this._list[index++] = obj;
        this._version = this._version + 1;
      }

      public override ArrayList GetRange(int index, int count)
      {
        if (index < 0 || count < 0)
          throw new ArgumentOutOfRangeException(index < 0 ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        if (this._list.Count - index < count)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
        return (ArrayList) new ArrayList.Range((ArrayList) this, index, count);
      }

      public override void Sort(int index, int count, IComparer comparer)
      {
        if (index < 0 || count < 0)
          throw new ArgumentOutOfRangeException(index < 0 ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        if (this._list.Count - index < count)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
        object[] objArray = new object[count];
        this.CopyTo(index, (Array) objArray, 0, count);
        Array.Sort((Array) objArray, 0, count, comparer);
        for (int index1 = 0; index1 < count; ++index1)
          this._list[index1 + index] = objArray[index1];
        this._version = this._version + 1;
      }

      public override object[] ToArray()
      {
        object[] objArray = new object[this.Count];
        this._list.CopyTo((Array) objArray, 0);
        return objArray;
      }

      [SecuritySafeCritical]
      public override Array ToArray(Type type)
      {
        if (type == (Type) null)
          throw new ArgumentNullException("type");
        Array instance = Array.UnsafeCreateInstance(type, this._list.Count);
        this._list.CopyTo(instance, 0);
        return instance;
      }

      public override void TrimToSize()
      {
      }

      [Serializable]
      private sealed class IListWrapperEnumWrapper : IEnumerator, ICloneable
      {
        private IEnumerator _en;
        private int _remaining;
        private int _initialStartIndex;
        private int _initialCount;
        private bool _firstCall;

        public object Current
        {
          get
          {
            if (this._firstCall)
              throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
            if (this._remaining < 0)
              throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
            return this._en.Current;
          }
        }

        private IListWrapperEnumWrapper()
        {
        }

        internal IListWrapperEnumWrapper(ArrayList.IListWrapper listWrapper, int startIndex, int count)
        {
          this._en = listWrapper.GetEnumerator();
          this._initialStartIndex = startIndex;
          this._initialCount = count;
          do
            ;
          while (startIndex-- > 0 && this._en.MoveNext());
          this._remaining = count;
          this._firstCall = true;
        }

        public object Clone()
        {
          ArrayList.IListWrapper.IListWrapperEnumWrapper wrapperEnumWrapper = new ArrayList.IListWrapper.IListWrapperEnumWrapper();
          wrapperEnumWrapper._en = (IEnumerator) ((ICloneable) this._en).Clone();
          wrapperEnumWrapper._initialStartIndex = this._initialStartIndex;
          wrapperEnumWrapper._initialCount = this._initialCount;
          wrapperEnumWrapper._remaining = this._remaining;
          int num = this._firstCall ? 1 : 0;
          wrapperEnumWrapper._firstCall = num != 0;
          return (object) wrapperEnumWrapper;
        }

        public bool MoveNext()
        {
          if (this._firstCall)
          {
            this._firstCall = false;
            int num = this._remaining;
            this._remaining = num - 1;
            if (num > 0)
              return this._en.MoveNext();
            return false;
          }
          if (this._remaining < 0 || !this._en.MoveNext())
            return false;
          int num1 = this._remaining;
          this._remaining = num1 - 1;
          return num1 > 0;
        }

        public void Reset()
        {
          this._en.Reset();
          int num = this._initialStartIndex;
          do
            ;
          while (num-- > 0 && this._en.MoveNext());
          this._remaining = this._initialCount;
          this._firstCall = true;
        }
      }
    }

    [Serializable]
    private class SyncArrayList : ArrayList
    {
      private ArrayList _list;
      private object _root;

      public override int Capacity
      {
        get
        {
          lock (this._root)
            return this._list.Capacity;
        }
        set
        {
          lock (this._root)
            this._list.Capacity = value;
        }
      }

      public override int Count
      {
        get
        {
          lock (this._root)
            return this._list.Count;
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

      public override object this[int index]
      {
        get
        {
          lock (this._root)
            return this._list[index];
        }
        set
        {
          lock (this._root)
            this._list[index] = value;
        }
      }

      public override object SyncRoot
      {
        get
        {
          return this._root;
        }
      }

      internal SyncArrayList(ArrayList list)
        : base(false)
      {
        this._list = list;
        this._root = list.SyncRoot;
      }

      public override int Add(object value)
      {
        lock (this._root)
          return this._list.Add(value);
      }

      public override void AddRange(ICollection c)
      {
        lock (this._root)
          this._list.AddRange(c);
      }

      public override int BinarySearch(object value)
      {
        lock (this._root)
          return this._list.BinarySearch(value);
      }

      public override int BinarySearch(object value, IComparer comparer)
      {
        lock (this._root)
          return this._list.BinarySearch(value, comparer);
      }

      public override int BinarySearch(int index, int count, object value, IComparer comparer)
      {
        lock (this._root)
          return this._list.BinarySearch(index, count, value, comparer);
      }

      public override void Clear()
      {
        lock (this._root)
          this._list.Clear();
      }

      public override object Clone()
      {
        lock (this._root)
          return (object) new ArrayList.SyncArrayList((ArrayList) this._list.Clone());
      }

      public override bool Contains(object item)
      {
        lock (this._root)
          return this._list.Contains(item);
      }

      public override void CopyTo(Array array)
      {
        lock (this._root)
          this._list.CopyTo(array);
      }

      public override void CopyTo(Array array, int index)
      {
        lock (this._root)
          this._list.CopyTo(array, index);
      }

      public override void CopyTo(int index, Array array, int arrayIndex, int count)
      {
        lock (this._root)
          this._list.CopyTo(index, array, arrayIndex, count);
      }

      public override IEnumerator GetEnumerator()
      {
        lock (this._root)
          return this._list.GetEnumerator();
      }

      public override IEnumerator GetEnumerator(int index, int count)
      {
        lock (this._root)
          return this._list.GetEnumerator(index, count);
      }

      public override int IndexOf(object value)
      {
        lock (this._root)
          return this._list.IndexOf(value);
      }

      public override int IndexOf(object value, int startIndex)
      {
        lock (this._root)
          return this._list.IndexOf(value, startIndex);
      }

      public override int IndexOf(object value, int startIndex, int count)
      {
        lock (this._root)
          return this._list.IndexOf(value, startIndex, count);
      }

      public override void Insert(int index, object value)
      {
        lock (this._root)
          this._list.Insert(index, value);
      }

      public override void InsertRange(int index, ICollection c)
      {
        lock (this._root)
          this._list.InsertRange(index, c);
      }

      public override int LastIndexOf(object value)
      {
        lock (this._root)
          return this._list.LastIndexOf(value);
      }

      public override int LastIndexOf(object value, int startIndex)
      {
        lock (this._root)
          return this._list.LastIndexOf(value, startIndex);
      }

      public override int LastIndexOf(object value, int startIndex, int count)
      {
        lock (this._root)
          return this._list.LastIndexOf(value, startIndex, count);
      }

      public override void Remove(object value)
      {
        lock (this._root)
          this._list.Remove(value);
      }

      public override void RemoveAt(int index)
      {
        lock (this._root)
          this._list.RemoveAt(index);
      }

      public override void RemoveRange(int index, int count)
      {
        lock (this._root)
          this._list.RemoveRange(index, count);
      }

      public override void Reverse(int index, int count)
      {
        lock (this._root)
          this._list.Reverse(index, count);
      }

      public override void SetRange(int index, ICollection c)
      {
        lock (this._root)
          this._list.SetRange(index, c);
      }

      public override ArrayList GetRange(int index, int count)
      {
        lock (this._root)
          return this._list.GetRange(index, count);
      }

      public override void Sort()
      {
        lock (this._root)
          this._list.Sort();
      }

      public override void Sort(IComparer comparer)
      {
        lock (this._root)
          this._list.Sort(comparer);
      }

      public override void Sort(int index, int count, IComparer comparer)
      {
        lock (this._root)
          this._list.Sort(index, count, comparer);
      }

      public override object[] ToArray()
      {
        lock (this._root)
          return this._list.ToArray();
      }

      public override Array ToArray(Type type)
      {
        lock (this._root)
          return this._list.ToArray(type);
      }

      public override void TrimToSize()
      {
        lock (this._root)
          this._list.TrimToSize();
      }
    }

    [Serializable]
    private class SyncIList : IList, ICollection, IEnumerable
    {
      private IList _list;
      private object _root;

      public virtual int Count
      {
        get
        {
          lock (this._root)
            return this._list.Count;
        }
      }

      public virtual bool IsReadOnly
      {
        get
        {
          return this._list.IsReadOnly;
        }
      }

      public virtual bool IsFixedSize
      {
        get
        {
          return this._list.IsFixedSize;
        }
      }

      public virtual bool IsSynchronized
      {
        get
        {
          return true;
        }
      }

      public virtual object this[int index]
      {
        get
        {
          lock (this._root)
            return this._list[index];
        }
        set
        {
          lock (this._root)
            this._list[index] = value;
        }
      }

      public virtual object SyncRoot
      {
        get
        {
          return this._root;
        }
      }

      internal SyncIList(IList list)
      {
        this._list = list;
        this._root = list.SyncRoot;
      }

      public virtual int Add(object value)
      {
        lock (this._root)
          return this._list.Add(value);
      }

      public virtual void Clear()
      {
        lock (this._root)
          this._list.Clear();
      }

      public virtual bool Contains(object item)
      {
        lock (this._root)
          return this._list.Contains(item);
      }

      public virtual void CopyTo(Array array, int index)
      {
        lock (this._root)
          this._list.CopyTo(array, index);
      }

      public virtual IEnumerator GetEnumerator()
      {
        lock (this._root)
          return this._list.GetEnumerator();
      }

      public virtual int IndexOf(object value)
      {
        lock (this._root)
          return this._list.IndexOf(value);
      }

      public virtual void Insert(int index, object value)
      {
        lock (this._root)
          this._list.Insert(index, value);
      }

      public virtual void Remove(object value)
      {
        lock (this._root)
          this._list.Remove(value);
      }

      public virtual void RemoveAt(int index)
      {
        lock (this._root)
          this._list.RemoveAt(index);
      }
    }

    [Serializable]
    private class FixedSizeList : IList, ICollection, IEnumerable
    {
      private IList _list;

      public virtual int Count
      {
        get
        {
          return this._list.Count;
        }
      }

      public virtual bool IsReadOnly
      {
        get
        {
          return this._list.IsReadOnly;
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
          return this._list.IsSynchronized;
        }
      }

      public virtual object this[int index]
      {
        get
        {
          return this._list[index];
        }
        set
        {
          this._list[index] = value;
        }
      }

      public virtual object SyncRoot
      {
        get
        {
          return this._list.SyncRoot;
        }
      }

      internal FixedSizeList(IList l)
      {
        this._list = l;
      }

      public virtual int Add(object obj)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
      }

      public virtual void Clear()
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
      }

      public virtual bool Contains(object obj)
      {
        return this._list.Contains(obj);
      }

      public virtual void CopyTo(Array array, int index)
      {
        this._list.CopyTo(array, index);
      }

      public virtual IEnumerator GetEnumerator()
      {
        return this._list.GetEnumerator();
      }

      public virtual int IndexOf(object value)
      {
        return this._list.IndexOf(value);
      }

      public virtual void Insert(int index, object obj)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
      }

      public virtual void Remove(object value)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
      }

      public virtual void RemoveAt(int index)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
      }
    }

    [Serializable]
    private class FixedSizeArrayList : ArrayList
    {
      private ArrayList _list;

      public override int Count
      {
        get
        {
          return this._list.Count;
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
          return true;
        }
      }

      public override bool IsSynchronized
      {
        get
        {
          return this._list.IsSynchronized;
        }
      }

      public override object this[int index]
      {
        get
        {
          return this._list[index];
        }
        set
        {
          this._list[index] = value;
          this._version = this._list._version;
        }
      }

      public override object SyncRoot
      {
        get
        {
          return this._list.SyncRoot;
        }
      }

      public override int Capacity
      {
        get
        {
          return this._list.Capacity;
        }
        set
        {
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
        }
      }

      internal FixedSizeArrayList(ArrayList l)
      {
        this._list = l;
        this._version = this._list._version;
      }

      public override int Add(object obj)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
      }

      public override void AddRange(ICollection c)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
      }

      public override int BinarySearch(int index, int count, object value, IComparer comparer)
      {
        return this._list.BinarySearch(index, count, value, comparer);
      }

      public override void Clear()
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
      }

      public override object Clone()
      {
        return (object) new ArrayList.FixedSizeArrayList(this._list) { _list = (ArrayList) this._list.Clone() };
      }

      public override bool Contains(object obj)
      {
        return this._list.Contains(obj);
      }

      public override void CopyTo(Array array, int index)
      {
        this._list.CopyTo(array, index);
      }

      public override void CopyTo(int index, Array array, int arrayIndex, int count)
      {
        this._list.CopyTo(index, array, arrayIndex, count);
      }

      public override IEnumerator GetEnumerator()
      {
        return this._list.GetEnumerator();
      }

      public override IEnumerator GetEnumerator(int index, int count)
      {
        return this._list.GetEnumerator(index, count);
      }

      public override int IndexOf(object value)
      {
        return this._list.IndexOf(value);
      }

      public override int IndexOf(object value, int startIndex)
      {
        return this._list.IndexOf(value, startIndex);
      }

      public override int IndexOf(object value, int startIndex, int count)
      {
        return this._list.IndexOf(value, startIndex, count);
      }

      public override void Insert(int index, object obj)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
      }

      public override void InsertRange(int index, ICollection c)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
      }

      public override int LastIndexOf(object value)
      {
        return this._list.LastIndexOf(value);
      }

      public override int LastIndexOf(object value, int startIndex)
      {
        return this._list.LastIndexOf(value, startIndex);
      }

      public override int LastIndexOf(object value, int startIndex, int count)
      {
        return this._list.LastIndexOf(value, startIndex, count);
      }

      public override void Remove(object value)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
      }

      public override void RemoveAt(int index)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
      }

      public override void RemoveRange(int index, int count)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
      }

      public override void SetRange(int index, ICollection c)
      {
        this._list.SetRange(index, c);
        this._version = this._list._version;
      }

      public override ArrayList GetRange(int index, int count)
      {
        if (index < 0 || count < 0)
          throw new ArgumentOutOfRangeException(index < 0 ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        if (this.Count - index < count)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
        return (ArrayList) new ArrayList.Range((ArrayList) this, index, count);
      }

      public override void Reverse(int index, int count)
      {
        this._list.Reverse(index, count);
        this._version = this._list._version;
      }

      public override void Sort(int index, int count, IComparer comparer)
      {
        this._list.Sort(index, count, comparer);
        this._version = this._list._version;
      }

      public override object[] ToArray()
      {
        return this._list.ToArray();
      }

      public override Array ToArray(Type type)
      {
        return this._list.ToArray(type);
      }

      public override void TrimToSize()
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
      }
    }

    [Serializable]
    private class ReadOnlyList : IList, ICollection, IEnumerable
    {
      private IList _list;

      public virtual int Count
      {
        get
        {
          return this._list.Count;
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
          return this._list.IsSynchronized;
        }
      }

      public virtual object this[int index]
      {
        get
        {
          return this._list[index];
        }
        set
        {
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
        }
      }

      public virtual object SyncRoot
      {
        get
        {
          return this._list.SyncRoot;
        }
      }

      internal ReadOnlyList(IList l)
      {
        this._list = l;
      }

      public virtual int Add(object obj)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
      }

      public virtual void Clear()
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
      }

      public virtual bool Contains(object obj)
      {
        return this._list.Contains(obj);
      }

      public virtual void CopyTo(Array array, int index)
      {
        this._list.CopyTo(array, index);
      }

      public virtual IEnumerator GetEnumerator()
      {
        return this._list.GetEnumerator();
      }

      public virtual int IndexOf(object value)
      {
        return this._list.IndexOf(value);
      }

      public virtual void Insert(int index, object obj)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
      }

      public virtual void Remove(object value)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
      }

      public virtual void RemoveAt(int index)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
      }
    }

    [Serializable]
    private class ReadOnlyArrayList : ArrayList
    {
      private ArrayList _list;

      public override int Count
      {
        get
        {
          return this._list.Count;
        }
      }

      public override bool IsReadOnly
      {
        get
        {
          return true;
        }
      }

      public override bool IsFixedSize
      {
        get
        {
          return true;
        }
      }

      public override bool IsSynchronized
      {
        get
        {
          return this._list.IsSynchronized;
        }
      }

      public override object this[int index]
      {
        get
        {
          return this._list[index];
        }
        set
        {
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
        }
      }

      public override object SyncRoot
      {
        get
        {
          return this._list.SyncRoot;
        }
      }

      public override int Capacity
      {
        get
        {
          return this._list.Capacity;
        }
        set
        {
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
        }
      }

      internal ReadOnlyArrayList(ArrayList l)
      {
        this._list = l;
      }

      public override int Add(object obj)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
      }

      public override void AddRange(ICollection c)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
      }

      public override int BinarySearch(int index, int count, object value, IComparer comparer)
      {
        return this._list.BinarySearch(index, count, value, comparer);
      }

      public override void Clear()
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
      }

      public override object Clone()
      {
        return (object) new ArrayList.ReadOnlyArrayList(this._list) { _list = (ArrayList) this._list.Clone() };
      }

      public override bool Contains(object obj)
      {
        return this._list.Contains(obj);
      }

      public override void CopyTo(Array array, int index)
      {
        this._list.CopyTo(array, index);
      }

      public override void CopyTo(int index, Array array, int arrayIndex, int count)
      {
        this._list.CopyTo(index, array, arrayIndex, count);
      }

      public override IEnumerator GetEnumerator()
      {
        return this._list.GetEnumerator();
      }

      public override IEnumerator GetEnumerator(int index, int count)
      {
        return this._list.GetEnumerator(index, count);
      }

      public override int IndexOf(object value)
      {
        return this._list.IndexOf(value);
      }

      public override int IndexOf(object value, int startIndex)
      {
        return this._list.IndexOf(value, startIndex);
      }

      public override int IndexOf(object value, int startIndex, int count)
      {
        return this._list.IndexOf(value, startIndex, count);
      }

      public override void Insert(int index, object obj)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
      }

      public override void InsertRange(int index, ICollection c)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
      }

      public override int LastIndexOf(object value)
      {
        return this._list.LastIndexOf(value);
      }

      public override int LastIndexOf(object value, int startIndex)
      {
        return this._list.LastIndexOf(value, startIndex);
      }

      public override int LastIndexOf(object value, int startIndex, int count)
      {
        return this._list.LastIndexOf(value, startIndex, count);
      }

      public override void Remove(object value)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
      }

      public override void RemoveAt(int index)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
      }

      public override void RemoveRange(int index, int count)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
      }

      public override void SetRange(int index, ICollection c)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
      }

      public override ArrayList GetRange(int index, int count)
      {
        if (index < 0 || count < 0)
          throw new ArgumentOutOfRangeException(index < 0 ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        if (this.Count - index < count)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
        return (ArrayList) new ArrayList.Range((ArrayList) this, index, count);
      }

      public override void Reverse(int index, int count)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
      }

      public override void Sort(int index, int count, IComparer comparer)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
      }

      public override object[] ToArray()
      {
        return this._list.ToArray();
      }

      public override Array ToArray(Type type)
      {
        return this._list.ToArray(type);
      }

      public override void TrimToSize()
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
      }
    }

    [Serializable]
    private sealed class ArrayListEnumerator : IEnumerator, ICloneable
    {
      private ArrayList list;
      private int index;
      private int endIndex;
      private int version;
      private object currentElement;
      private int startIndex;

      public object Current
      {
        get
        {
          if (this.index < this.startIndex)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
          if (this.index > this.endIndex)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
          return this.currentElement;
        }
      }

      internal ArrayListEnumerator(ArrayList list, int index, int count)
      {
        this.list = list;
        this.startIndex = index;
        this.index = index - 1;
        this.endIndex = this.index + count;
        this.version = list._version;
        this.currentElement = (object) null;
      }

      public object Clone()
      {
        return this.MemberwiseClone();
      }

      public bool MoveNext()
      {
        if (this.version != this.list._version)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
        if (this.index < this.endIndex)
        {
          ArrayList arrayList = this.list;
          int num = this.index + 1;
          this.index = num;
          int index = num;
          this.currentElement = arrayList[index];
          return true;
        }
        this.index = this.endIndex + 1;
        return false;
      }

      public void Reset()
      {
        if (this.version != this.list._version)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
        this.index = this.startIndex - 1;
      }
    }

    [Serializable]
    private class Range : ArrayList
    {
      private ArrayList _baseList;
      private int _baseIndex;
      private int _baseSize;
      private int _baseVersion;

      public override int Capacity
      {
        get
        {
          return this._baseList.Capacity;
        }
        set
        {
          if (value < this.Count)
            throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_SmallCapacity"));
        }
      }

      public override int Count
      {
        get
        {
          this.InternalUpdateRange();
          return this._baseSize;
        }
      }

      public override bool IsReadOnly
      {
        get
        {
          return this._baseList.IsReadOnly;
        }
      }

      public override bool IsFixedSize
      {
        get
        {
          return this._baseList.IsFixedSize;
        }
      }

      public override bool IsSynchronized
      {
        get
        {
          return this._baseList.IsSynchronized;
        }
      }

      public override object SyncRoot
      {
        get
        {
          return this._baseList.SyncRoot;
        }
      }

      public override object this[int index]
      {
        get
        {
          this.InternalUpdateRange();
          if (index < 0 || index >= this._baseSize)
            throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
          return this._baseList[this._baseIndex + index];
        }
        set
        {
          this.InternalUpdateRange();
          if (index < 0 || index >= this._baseSize)
            throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
          this._baseList[this._baseIndex + index] = value;
          this.InternalUpdateVersion();
        }
      }

      internal Range(ArrayList list, int index, int count)
        : base(false)
      {
        this._baseList = list;
        this._baseIndex = index;
        this._baseSize = count;
        this._baseVersion = list._version;
        this._version = list._version;
      }

      private void InternalUpdateRange()
      {
        if (this._baseVersion != this._baseList._version)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_UnderlyingArrayListChanged"));
      }

      private void InternalUpdateVersion()
      {
        this._baseVersion = this._baseVersion + 1;
        this._version = this._version + 1;
      }

      public override int Add(object value)
      {
        this.InternalUpdateRange();
        this._baseList.Insert(this._baseIndex + this._baseSize, value);
        this.InternalUpdateVersion();
        int num = this._baseSize;
        this._baseSize = num + 1;
        return num;
      }

      public override void AddRange(ICollection c)
      {
        if (c == null)
          throw new ArgumentNullException("c");
        this.InternalUpdateRange();
        int count = c.Count;
        if (count <= 0)
          return;
        this._baseList.InsertRange(this._baseIndex + this._baseSize, c);
        this.InternalUpdateVersion();
        this._baseSize = this._baseSize + count;
      }

      public override int BinarySearch(int index, int count, object value, IComparer comparer)
      {
        if (index < 0 || count < 0)
          throw new ArgumentOutOfRangeException(index < 0 ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        if (this._baseSize - index < count)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
        this.InternalUpdateRange();
        int num = this._baseList.BinarySearch(this._baseIndex + index, count, value, comparer);
        if (num >= 0)
          return num - this._baseIndex;
        return num + this._baseIndex;
      }

      public override void Clear()
      {
        this.InternalUpdateRange();
        if (this._baseSize == 0)
          return;
        this._baseList.RemoveRange(this._baseIndex, this._baseSize);
        this.InternalUpdateVersion();
        this._baseSize = 0;
      }

      public override object Clone()
      {
        this.InternalUpdateRange();
        return (object) new ArrayList.Range(this._baseList, this._baseIndex, this._baseSize) { _baseList = (ArrayList) this._baseList.Clone() };
      }

      public override bool Contains(object item)
      {
        this.InternalUpdateRange();
        if (item == null)
        {
          for (int index = 0; index < this._baseSize; ++index)
          {
            if (this._baseList[this._baseIndex + index] == null)
              return true;
          }
          return false;
        }
        for (int index = 0; index < this._baseSize; ++index)
        {
          if (this._baseList[this._baseIndex + index] != null && this._baseList[this._baseIndex + index].Equals(item))
            return true;
        }
        return false;
      }

      public override void CopyTo(Array array, int index)
      {
        if (array == null)
          throw new ArgumentNullException("array");
        if (array.Rank != 1)
          throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
        if (index < 0)
          throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        if (array.Length - index < this._baseSize)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
        this.InternalUpdateRange();
        this._baseList.CopyTo(this._baseIndex, array, index, this._baseSize);
      }

      public override void CopyTo(int index, Array array, int arrayIndex, int count)
      {
        if (array == null)
          throw new ArgumentNullException("array");
        if (array.Rank != 1)
          throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
        if (index < 0 || count < 0)
          throw new ArgumentOutOfRangeException(index < 0 ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        if (array.Length - arrayIndex < count)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
        if (this._baseSize - index < count)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
        this.InternalUpdateRange();
        this._baseList.CopyTo(this._baseIndex + index, array, arrayIndex, count);
      }

      public override IEnumerator GetEnumerator()
      {
        return this.GetEnumerator(0, this._baseSize);
      }

      public override IEnumerator GetEnumerator(int index, int count)
      {
        if (index < 0 || count < 0)
          throw new ArgumentOutOfRangeException(index < 0 ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        if (this._baseSize - index < count)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
        this.InternalUpdateRange();
        return this._baseList.GetEnumerator(this._baseIndex + index, count);
      }

      public override ArrayList GetRange(int index, int count)
      {
        if (index < 0 || count < 0)
          throw new ArgumentOutOfRangeException(index < 0 ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        if (this._baseSize - index < count)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
        this.InternalUpdateRange();
        return (ArrayList) new ArrayList.Range((ArrayList) this, index, count);
      }

      public override int IndexOf(object value)
      {
        this.InternalUpdateRange();
        int num = this._baseList.IndexOf(value, this._baseIndex, this._baseSize);
        if (num >= 0)
          return num - this._baseIndex;
        return -1;
      }

      public override int IndexOf(object value, int startIndex)
      {
        if (startIndex < 0)
          throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        if (startIndex > this._baseSize)
          throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
        this.InternalUpdateRange();
        int num = this._baseList.IndexOf(value, this._baseIndex + startIndex, this._baseSize - startIndex);
        if (num >= 0)
          return num - this._baseIndex;
        return -1;
      }

      public override int IndexOf(object value, int startIndex, int count)
      {
        if (startIndex < 0 || startIndex > this._baseSize)
          throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
        if (count < 0 || startIndex > this._baseSize - count)
          throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
        this.InternalUpdateRange();
        int num = this._baseList.IndexOf(value, this._baseIndex + startIndex, count);
        if (num >= 0)
          return num - this._baseIndex;
        return -1;
      }

      public override void Insert(int index, object value)
      {
        if (index < 0 || index > this._baseSize)
          throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
        this.InternalUpdateRange();
        this._baseList.Insert(this._baseIndex + index, value);
        this.InternalUpdateVersion();
        this._baseSize = this._baseSize + 1;
      }

      public override void InsertRange(int index, ICollection c)
      {
        if (index < 0 || index > this._baseSize)
          throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
        if (c == null)
          throw new ArgumentNullException("c");
        this.InternalUpdateRange();
        int count = c.Count;
        if (count <= 0)
          return;
        this._baseList.InsertRange(this._baseIndex + index, c);
        this._baseSize = this._baseSize + count;
        this.InternalUpdateVersion();
      }

      public override int LastIndexOf(object value)
      {
        this.InternalUpdateRange();
        int num = this._baseList.LastIndexOf(value, this._baseIndex + this._baseSize - 1, this._baseSize);
        if (num >= 0)
          return num - this._baseIndex;
        return -1;
      }

      public override int LastIndexOf(object value, int startIndex)
      {
        object obj = value;
        int startIndex1 = startIndex;
        int num = 1;
        int count = startIndex1 + num;
        return this.LastIndexOf(obj, startIndex1, count);
      }

      public override int LastIndexOf(object value, int startIndex, int count)
      {
        this.InternalUpdateRange();
        if (this._baseSize == 0)
          return -1;
        if (startIndex >= this._baseSize)
          throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
        if (startIndex < 0)
          throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        int num = this._baseList.LastIndexOf(value, this._baseIndex + startIndex, count);
        if (num >= 0)
          return num - this._baseIndex;
        return -1;
      }

      public override void RemoveAt(int index)
      {
        if (index < 0 || index >= this._baseSize)
          throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
        this.InternalUpdateRange();
        this._baseList.RemoveAt(this._baseIndex + index);
        this.InternalUpdateVersion();
        this._baseSize = this._baseSize - 1;
      }

      public override void RemoveRange(int index, int count)
      {
        if (index < 0 || count < 0)
          throw new ArgumentOutOfRangeException(index < 0 ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        if (this._baseSize - index < count)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
        this.InternalUpdateRange();
        if (count <= 0)
          return;
        this._baseList.RemoveRange(this._baseIndex + index, count);
        this.InternalUpdateVersion();
        this._baseSize = this._baseSize - count;
      }

      public override void Reverse(int index, int count)
      {
        if (index < 0 || count < 0)
          throw new ArgumentOutOfRangeException(index < 0 ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        if (this._baseSize - index < count)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
        this.InternalUpdateRange();
        this._baseList.Reverse(this._baseIndex + index, count);
        this.InternalUpdateVersion();
      }

      public override void SetRange(int index, ICollection c)
      {
        this.InternalUpdateRange();
        if (index < 0 || index >= this._baseSize)
          throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
        this._baseList.SetRange(this._baseIndex + index, c);
        if (c.Count <= 0)
          return;
        this.InternalUpdateVersion();
      }

      public override void Sort(int index, int count, IComparer comparer)
      {
        if (index < 0 || count < 0)
          throw new ArgumentOutOfRangeException(index < 0 ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        if (this._baseSize - index < count)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
        this.InternalUpdateRange();
        this._baseList.Sort(this._baseIndex + index, count, comparer);
        this.InternalUpdateVersion();
      }

      public override object[] ToArray()
      {
        this.InternalUpdateRange();
        object[] objArray = new object[this._baseSize];
        Array.Copy((Array) this._baseList._items, this._baseIndex, (Array) objArray, 0, this._baseSize);
        return objArray;
      }

      [SecuritySafeCritical]
      public override Array ToArray(Type type)
      {
        if (type == (Type) null)
          throw new ArgumentNullException("type");
        this.InternalUpdateRange();
        Array instance = Array.UnsafeCreateInstance(type, this._baseSize);
        this._baseList.CopyTo(this._baseIndex, instance, 0, this._baseSize);
        return instance;
      }

      public override void TrimToSize()
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_RangeCollection"));
      }
    }

    [Serializable]
    private sealed class ArrayListEnumeratorSimple : IEnumerator, ICloneable
    {
      private static object dummyObject = new object();
      private ArrayList list;
      private int index;
      private int version;
      private object currentElement;
      [NonSerialized]
      private bool isArrayList;

      public object Current
      {
        get
        {
          object obj = this.currentElement;
          if (ArrayList.ArrayListEnumeratorSimple.dummyObject != obj)
            return obj;
          if (this.index == -1)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
        }
      }

      internal ArrayListEnumeratorSimple(ArrayList list)
      {
        this.list = list;
        this.index = -1;
        this.version = list._version;
        this.isArrayList = list.GetType() == typeof (ArrayList);
        this.currentElement = ArrayList.ArrayListEnumeratorSimple.dummyObject;
      }

      public object Clone()
      {
        return this.MemberwiseClone();
      }

      public bool MoveNext()
      {
        if (this.version != this.list._version)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
        if (this.isArrayList)
        {
          if (this.index < this.list._size - 1)
          {
            object[] objArray = this.list._items;
            int num = this.index + 1;
            this.index = num;
            int index = num;
            this.currentElement = objArray[index];
            return true;
          }
          this.currentElement = ArrayList.ArrayListEnumeratorSimple.dummyObject;
          this.index = this.list._size;
          return false;
        }
        if (this.index < this.list.Count - 1)
        {
          ArrayList arrayList = this.list;
          int num = this.index + 1;
          this.index = num;
          int index = num;
          this.currentElement = arrayList[index];
          return true;
        }
        this.index = this.list.Count;
        this.currentElement = ArrayList.ArrayListEnumeratorSimple.dummyObject;
        return false;
      }

      public void Reset()
      {
        if (this.version != this.list._version)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
        this.currentElement = ArrayList.ArrayListEnumeratorSimple.dummyObject;
        this.index = -1;
      }
    }

    internal class ArrayListDebugView
    {
      private ArrayList arrayList;

      [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
      public object[] Items
      {
        get
        {
          return this.arrayList.ToArray();
        }
      }

      public ArrayListDebugView(ArrayList arrayList)
      {
        if (arrayList == null)
          throw new ArgumentNullException("arrayList");
        this.arrayList = arrayList;
      }
    }
  }
}
