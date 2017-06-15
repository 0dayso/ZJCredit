// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.List`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.Versioning;
using System.Threading;

namespace System.Collections.Generic
{
  /// <summary>表示可通过索引访问的对象的强类型列表。提供用于对列表进行搜索、排序和操作的方法。若要浏览此类型的.NET Framework 源代码，请参阅 Reference Source。</summary>
  /// <typeparam name="T">列表中元素的类型。</typeparam>
  /// <filterpriority>1</filterpriority>
  [DebuggerTypeProxy(typeof (Mscorlib_CollectionDebugView<>))]
  [DebuggerDisplay("Count = {Count}")]
  [__DynamicallyInvokable]
  [Serializable]
  public class List<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection, IReadOnlyList<T>, IReadOnlyCollection<T>
  {
    private static readonly T[] _emptyArray = new T[0];
    private const int _defaultCapacity = 4;
    private T[] _items;
    private int _size;
    private int _version;
    [NonSerialized]
    private object _syncRoot;

    /// <summary>获取或设置该内部数据结构在不调整大小的情况下能够容纳的元素总数。</summary>
    /// <returns>在需要调整大小之前 <see cref="T:System.Collections.Generic.List`1" /> 可包含的元素数目。</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <see cref="P:System.Collections.Generic.List`1.Capacity" /> 设置一个值，则该值为小于 <see cref="P:System.Collections.Generic.List`1.Count" />。</exception>
    /// <exception cref="T:System.OutOfMemoryException">没有足够的内存可用系统上。</exception>
    [__DynamicallyInvokable]
    public int Capacity
    {
      [__DynamicallyInvokable] get
      {
        return this._items.Length;
      }
      [__DynamicallyInvokable] set
      {
        if (value < this._size)
          ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.value, ExceptionResource.ArgumentOutOfRange_SmallCapacity);
        if (value == this._items.Length)
          return;
        if (value > 0)
        {
          T[] objArray = new T[value];
          if (this._size > 0)
            Array.Copy((Array) this._items, 0, (Array) objArray, 0, this._size);
          this._items = objArray;
        }
        else
          this._items = List<T>._emptyArray;
      }
    }

    /// <summary>获取 <see cref="T:System.Collections.Generic.List`1" /> 中包含的元素数。</summary>
    /// <returns>
    /// <see cref="T:System.Collections.Generic.List`1" /> 中包含的元素数。</returns>
    [__DynamicallyInvokable]
    public int Count
    {
      [__DynamicallyInvokable] get
      {
        return this._size;
      }
    }

    [__DynamicallyInvokable]
    bool IList.IsFixedSize
    {
      [__DynamicallyInvokable] get
      {
        return false;
      }
    }

    [__DynamicallyInvokable]
    bool ICollection<T>.IsReadOnly
    {
      [__DynamicallyInvokable] get
      {
        return false;
      }
    }

    [__DynamicallyInvokable]
    bool IList.IsReadOnly
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

    /// <summary>获取或设置指定索引处的元素。</summary>
    /// <returns>指定索引处的元素。</returns>
    /// <param name="index">要获取或设置的元素的从零开始的索引。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于 0。- 或 -<paramref name="index" /> 等于或大于 <see cref="P:System.Collections.Generic.List`1.Count" />。</exception>
    [__DynamicallyInvokable]
    public T this[int index]
    {
      [__DynamicallyInvokable] get
      {
        if ((uint) index >= (uint) this._size)
          ThrowHelper.ThrowArgumentOutOfRangeException();
        return this._items[index];
      }
      [__DynamicallyInvokable] set
      {
        if ((uint) index >= (uint) this._size)
          ThrowHelper.ThrowArgumentOutOfRangeException();
        this._items[index] = value;
        this._version = this._version + 1;
      }
    }

    [__DynamicallyInvokable]
    object IList.this[int index]
    {
      [__DynamicallyInvokable] get
      {
        return (object) this[index];
      }
      [__DynamicallyInvokable] set
      {
        ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(value, ExceptionArgument.value);
        try
        {
          this[index] = (T) value;
        }
        catch (InvalidCastException ex)
        {
          ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof (T));
        }
      }
    }

    /// <summary>初始化 <see cref="T:System.Collections.Generic.List`1" /> 类的新实例，该实例为空并且具有默认初始容量。</summary>
    [__DynamicallyInvokable]
    public List()
    {
      this._items = List<T>._emptyArray;
    }

    /// <summary>初始化 <see cref="T:System.Collections.Generic.List`1" /> 类的新实例，该实例为空并且具有指定的初始容量。</summary>
    /// <param name="capacity">新列表最初可以存储的元素数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="capacity" /> 小于 0。</exception>
    [__DynamicallyInvokable]
    public List(int capacity)
    {
      if (capacity < 0)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.capacity, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
      if (capacity == 0)
        this._items = List<T>._emptyArray;
      else
        this._items = new T[capacity];
    }

    /// <summary>初始化 <see cref="T:System.Collections.Generic.List`1" /> 类的新实例，该实例包含从指定集合复制的元素并且具有足够的容量来容纳所复制的元素。</summary>
    /// <param name="collection">一个集合，其元素被复制到新列表中。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="collection" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public List(IEnumerable<T> collection)
    {
      if (collection == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
      ICollection<T> objs = collection as ICollection<T>;
      if (objs != null)
      {
        int count = objs.Count;
        if (count == 0)
        {
          this._items = List<T>._emptyArray;
        }
        else
        {
          this._items = new T[count];
          objs.CopyTo(this._items, 0);
          this._size = count;
        }
      }
      else
      {
        this._size = 0;
        this._items = List<T>._emptyArray;
        foreach (T obj in collection)
          this.Add(obj);
      }
    }

    private static bool IsCompatibleObject(object value)
    {
      if (value is T)
        return true;
      if (value == null)
        return (object) default (T) == null;
      return false;
    }

    /// <summary>将对象添加到 <see cref="T:System.Collections.Generic.List`1" /> 的结尾处。</summary>
    /// <param name="item">要添加到 <see cref="T:System.Collections.Generic.List`1" /> 末尾的对象。对于引用类型，该值可以为 null。</param>
    [__DynamicallyInvokable]
    public void Add(T item)
    {
      if (this._size == this._items.Length)
        this.EnsureCapacity(this._size + 1);
      T[] objArray = this._items;
      int num = this._size;
      this._size = num + 1;
      int index = num;
      T obj = item;
      objArray[index] = obj;
      this._version = this._version + 1;
    }

    [__DynamicallyInvokable]
    int IList.Add(object item)
    {
      ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(item, ExceptionArgument.item);
      try
      {
        this.Add((T) item);
      }
      catch (InvalidCastException ex)
      {
        ThrowHelper.ThrowWrongValueTypeArgumentException(item, typeof (T));
      }
      return this.Count - 1;
    }

    /// <summary>将指定集合的元素添加到 <see cref="T:System.Collections.Generic.List`1" /> 的末尾。</summary>
    /// <param name="collection">应将其元素添加到 <see cref="T:System.Collections.Generic.List`1" /> 的末尾的集合。集合自身不能为 null，但它可以包含为 null 的元素（如果类型 <paramref name="T" /> 为引用类型）。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="collection" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public void AddRange(IEnumerable<T> collection)
    {
      this.InsertRange(this._size, collection);
    }

    /// <summary>返回当前集合的只读 <see cref="T:System.Collections.Generic.IList`1" /> 包装器。</summary>
    /// <returns>一个 <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" />，作为围绕当前 <see cref="T:System.Collections.Generic.List`1" /> 的只读包装。</returns>
    [__DynamicallyInvokable]
    public ReadOnlyCollection<T> AsReadOnly()
    {
      return new ReadOnlyCollection<T>((IList<T>) this);
    }

    /// <summary>使用指定的比较器在已排序 <see cref="T:System.Collections.Generic.List`1" /> 的某个元素范围中搜索元素，并返回该元素从零开始的索引。</summary>
    /// <returns>如果找到 <paramref name="item" />，则为已排序的 <see cref="T:System.Collections.Generic.List`1" /> 中 <paramref name="item" /> 的从零开始的索引；否则为一个负数，该负数是大于 <paramref name="item" /> 的下一个元素的索引的按位求补。如果没有更大的元素，则为 <see cref="P:System.Collections.Generic.List`1.Count" /> 的按位求补。</returns>
    /// <param name="index">要搜索范围的从零开始的起始索引。</param>
    /// <param name="count">要搜索的范围的长度。</param>
    /// <param name="item">要定位的对象。对于引用类型，该值可以为 null。</param>
    /// <param name="comparer">比较元素时要使用的 <see cref="T:System.Collections.Generic.IComparer`1" /> 实现，若要使用默认比较器 <see cref="P:System.Collections.Generic.Comparer`1.Default" />，则为 null。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于 0。- 或 -<paramref name="count" /> 小于 0。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="index" /> 和 <paramref name="count" /> 不表示中的有效范围 <see cref="T:System.Collections.Generic.List`1" />。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="comparer" /> 是 null, ，和默认比较器 <see cref="P:System.Collections.Generic.Comparer`1.Default" /> 找不到的一种实现 <see cref="T:System.IComparable`1" /> 泛型接口或 <see cref="T:System.IComparable" /> 类型的接口 <paramref name="T" />。</exception>
    [__DynamicallyInvokable]
    public int BinarySearch(int index, int count, T item, IComparer<T> comparer)
    {
      if (index < 0)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
      if (count < 0)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
      if (this._size - index < count)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
      return Array.BinarySearch<T>(this._items, index, count, item, comparer);
    }

    /// <summary>使用默认的比较器在整个已排序的 <see cref="T:System.Collections.Generic.List`1" /> 中搜索元素，并返回该元素从零开始的索引。</summary>
    /// <returns>如果找到 <paramref name="item" />，则为已排序的 <see cref="T:System.Collections.Generic.List`1" /> 中 <paramref name="item" /> 的从零开始的索引；否则为一个负数，该负数是大于 <paramref name="item" /> 的下一个元素的索引的按位求补。如果没有更大的元素，则为 <see cref="P:System.Collections.Generic.List`1.Count" /> 的按位求补。</returns>
    /// <param name="item">要定位的对象。对于引用类型，该值可以为 null。</param>
    /// <exception cref="T:System.InvalidOperationException">默认比较器 <see cref="P:System.Collections.Generic.Comparer`1.Default" /> 找不到的一种实现 <see cref="T:System.IComparable`1" /> 泛型接口或 <see cref="T:System.IComparable" /> 类型的接口 <paramref name="T" />。</exception>
    [__DynamicallyInvokable]
    public int BinarySearch(T item)
    {
      return this.BinarySearch(0, this.Count, item, (IComparer<T>) null);
    }

    /// <summary>使用指定的比较器在整个已排序的 <see cref="T:System.Collections.Generic.List`1" /> 中搜索元素，并返回该元素从零开始的索引。</summary>
    /// <returns>如果找到 <paramref name="item" />，则为已排序的 <see cref="T:System.Collections.Generic.List`1" /> 中 <paramref name="item" /> 的从零开始的索引；否则为一个负数，该负数是大于 <paramref name="item" /> 的下一个元素的索引的按位求补。如果没有更大的元素，则为 <see cref="P:System.Collections.Generic.List`1.Count" /> 的按位求补。</returns>
    /// <param name="item">要定位的对象。对于引用类型，该值可以为 null。</param>
    /// <param name="comparer">比较元素时要使用的 <see cref="T:System.Collections.Generic.IComparer`1" /> 实现。- 或 -如果使用默认比较器 <see cref="P:System.Collections.Generic.Comparer`1.Default" />，则为 null。</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="comparer" /> 是 null, ，和默认比较器 <see cref="P:System.Collections.Generic.Comparer`1.Default" /> 找不到的一种实现 <see cref="T:System.IComparable`1" /> 泛型接口或 <see cref="T:System.IComparable" /> 类型的接口 <paramref name="T" />。</exception>
    [__DynamicallyInvokable]
    public int BinarySearch(T item, IComparer<T> comparer)
    {
      return this.BinarySearch(0, this.Count, item, comparer);
    }

    /// <summary>从 <see cref="T:System.Collections.Generic.List`1" /> 中移除所有元素。</summary>
    [__DynamicallyInvokable]
    public void Clear()
    {
      if (this._size > 0)
      {
        Array.Clear((Array) this._items, 0, this._size);
        this._size = 0;
      }
      this._version = this._version + 1;
    }

    /// <summary>确定某元素是否在 <see cref="T:System.Collections.Generic.List`1" /> 中。</summary>
    /// <returns>如果在 true 中找到 <paramref name="item" />，则为 <see cref="T:System.Collections.Generic.List`1" />；否则为 false。</returns>
    /// <param name="item">要在 <see cref="T:System.Collections.Generic.List`1" /> 中定位的对象。对于引用类型，该值可以为 null。</param>
    [__DynamicallyInvokable]
    public bool Contains(T item)
    {
      if ((object) item == null)
      {
        for (int index = 0; index < this._size; ++index)
        {
          if ((object) this._items[index] == null)
            return true;
        }
        return false;
      }
      EqualityComparer<T> @default = EqualityComparer<T>.Default;
      for (int index = 0; index < this._size; ++index)
      {
        if (@default.Equals(this._items[index], item))
          return true;
      }
      return false;
    }

    [__DynamicallyInvokable]
    bool IList.Contains(object item)
    {
      if (List<T>.IsCompatibleObject(item))
        return this.Contains((T) item);
      return false;
    }

    /// <summary>将当前 <see cref="T:System.Collections.Generic.List`1" /> 中的元素转换为另一种类型，并返回包含已转换元素的列表。</summary>
    /// <returns>目标类型的 <see cref="T:System.Collections.Generic.List`1" />，包含当前 <see cref="T:System.Collections.Generic.List`1" /> 中转换后的元素。</returns>
    /// <param name="converter">一个 <see cref="T:System.Converter`2" /> 委托，可将每个元素从一种类型转换为另一种类型。</param>
    /// <typeparam name="TOutput">目标数组元素的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="converter" /> 为 null。</exception>
    public List<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter)
    {
      if (converter == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.converter);
      List<TOutput> outputList = new List<TOutput>(this._size);
      for (int index = 0; index < this._size; ++index)
        outputList._items[index] = converter(this._items[index]);
      outputList._size = this._size;
      return outputList;
    }

    /// <summary>从目标数组的开头开始，将整个 <see cref="T:System.Collections.Generic.List`1" /> 复制到兼容的一维数组。</summary>
    /// <param name="array">一维 <see cref="T:System.Array" />，它是从 <see cref="T:System.Collections.Generic.List`1" /> 复制的元素的目标。<see cref="T:System.Array" /> 必须具有从零开始的索引。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">在源中的元素数目 <see cref="T:System.Collections.Generic.List`1" /> 大于元素的数目，目标 <paramref name="array" /> 可以包含。</exception>
    [__DynamicallyInvokable]
    public void CopyTo(T[] array)
    {
      this.CopyTo(array, 0);
    }

    [__DynamicallyInvokable]
    void ICollection.CopyTo(Array array, int arrayIndex)
    {
      if (array != null && array.Rank != 1)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
      try
      {
        Array.Copy((Array) this._items, 0, array, arrayIndex, this._size);
      }
      catch (ArrayTypeMismatchException ex)
      {
        ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
      }
    }

    /// <summary>从目标数组的指定索引处开始，将元素的范围从 <see cref="T:System.Collections.Generic.List`1" /> 复制到兼容的一维数组。</summary>
    /// <param name="index">复制即从源 <see cref="T:System.Collections.Generic.List`1" /> 中从零开始的索引开始。</param>
    /// <param name="array">一维 <see cref="T:System.Array" />，它是从 <see cref="T:System.Collections.Generic.List`1" /> 复制的元素的目标。<see cref="T:System.Array" /> 必须具有从零开始的索引。</param>
    /// <param name="arrayIndex">
    /// <paramref name="array" /> 中从零开始的索引，从此处开始复制。</param>
    /// <param name="count">要复制的元素数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于 0。- 或 -<paramref name="arrayIndex" /> 小于 0。- 或 -<paramref name="count" /> 小于 0。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="index" /> 等于或大于 <see cref="P:System.Collections.Generic.List`1.Count" /> 源的 <see cref="T:System.Collections.Generic.List`1" />。- 或 -中的元素数 <paramref name="index" /> 到源末尾 <see cref="T:System.Collections.Generic.List`1" /> 大于从可用空间 <paramref name="arrayIndex" /> 到目标的末尾 <paramref name="array" />。</exception>
    [__DynamicallyInvokable]
    public void CopyTo(int index, T[] array, int arrayIndex, int count)
    {
      if (this._size - index < count)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
      Array.Copy((Array) this._items, index, (Array) array, arrayIndex, count);
    }

    /// <summary>从目标数组的指定索引处开始，将整个 <see cref="T:System.Collections.Generic.List`1" /> 复制到兼容的一维数组。</summary>
    /// <param name="array">一维 <see cref="T:System.Array" />，它是从 <see cref="T:System.Collections.Generic.List`1" /> 复制的元素的目标。<see cref="T:System.Array" /> 必须具有从零开始的索引。</param>
    /// <param name="arrayIndex">
    /// <paramref name="array" /> 中从零开始的索引，从此处开始复制。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="arrayIndex" /> 小于 0。</exception>
    /// <exception cref="T:System.ArgumentException">在源中的元素数目 <see cref="T:System.Collections.Generic.List`1" /> 大于从可用空间 <paramref name="arrayIndex" /> 到目标的末尾 <paramref name="array" />。</exception>
    [__DynamicallyInvokable]
    public void CopyTo(T[] array, int arrayIndex)
    {
      Array.Copy((Array) this._items, 0, (Array) array, arrayIndex, this._size);
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

    /// <summary>确定 <see cref="T:System.Collections.Generic.List`1" /> 是否包含与指定谓词定义的条件匹配的元素。</summary>
    /// <returns>如果 <see cref="T:System.Collections.Generic.List`1" /> 包含一个或多个元素与指定谓词定义的条件匹配，则为 true；否则为 false。</returns>
    /// <param name="match">
    /// <see cref="T:System.Predicate`1" /> 委托，用于定义要搜索的元素应满足的条件。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="match" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public bool Exists(Predicate<T> match)
    {
      return this.FindIndex(match) != -1;
    }

    /// <summary>搜索与指定谓词所定义的条件相匹配的元素，并返回整个 <see cref="T:System.Collections.Generic.List`1" /> 中的第一个匹配元素。</summary>
    /// <returns>如果找到与指定谓词定义的条件匹配的第一个元素，则为该元素；否则为类型 <paramref name="T" /> 的默认值。</returns>
    /// <param name="match">
    /// <see cref="T:System.Predicate`1" /> 委托，用于定义要搜索的元素的条件。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="match" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public T Find(Predicate<T> match)
    {
      if (match == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
      for (int index = 0; index < this._size; ++index)
      {
        if (match(this._items[index]))
          return this._items[index];
      }
      return default (T);
    }

    /// <summary>检索与指定谓词定义的条件匹配的所有元素。</summary>
    /// <returns>如果找到一个 <see cref="T:System.Collections.Generic.List`1" />，其中所有元素均与指定谓词定义的条件匹配，则为该数组；否则为一个空 <see cref="T:System.Collections.Generic.List`1" />。</returns>
    /// <param name="match">
    /// <see cref="T:System.Predicate`1" /> 委托，用于定义要搜索的元素应满足的条件。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="match" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public List<T> FindAll(Predicate<T> match)
    {
      if (match == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
      List<T> objList = new List<T>();
      for (int index = 0; index < this._size; ++index)
      {
        if (match(this._items[index]))
          objList.Add(this._items[index]);
      }
      return objList;
    }

    /// <summary>搜索与指定谓词所定义的条件相匹配的元素，并返回整个 <see cref="T:System.Collections.Generic.List`1" /> 中第一个匹配元素的从零开始的索引。</summary>
    /// <returns>如果找到与 <paramref name="match" /> 定义的条件相匹配的第一个元素，则为该元素的从零开始的索引；否则为 -1。</returns>
    /// <param name="match">
    /// <see cref="T:System.Predicate`1" /> 委托，用于定义要搜索的元素的条件。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="match" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public int FindIndex(Predicate<T> match)
    {
      return this.FindIndex(0, this._size, match);
    }

    /// <summary>搜索与指定谓词所定义的条件相匹配的元素，并返回 <see cref="T:System.Collections.Generic.List`1" /> 中从指定索引到最后一个元素的元素范围内第一个匹配项的从零开始的索引。</summary>
    /// <returns>如果找到与 <paramref name="match" /> 定义的条件相匹配的第一个元素，则为该元素的从零开始的索引；否则为 -1。</returns>
    /// <param name="startIndex">从零开始的搜索的起始索引。</param>
    /// <param name="match">
    /// <see cref="T:System.Predicate`1" /> 委托，用于定义要搜索的元素的条件。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="match" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 超出的有效索引范围 <see cref="T:System.Collections.Generic.List`1" />。</exception>
    [__DynamicallyInvokable]
    public int FindIndex(int startIndex, Predicate<T> match)
    {
      return this.FindIndex(startIndex, this._size - startIndex, match);
    }

    /// <summary>搜索与指定谓词所定义的条件相匹配的一个元素，并返回 <see cref="T:System.Collections.Generic.List`1" /> 中从指定的索引开始、包含指定元素个数的元素范围内第一个匹配项的从零开始的索引。</summary>
    /// <returns>如果找到与 <paramref name="match" /> 定义的条件相匹配的第一个元素，则为该元素的从零开始的索引；否则为 -1。</returns>
    /// <param name="startIndex">从零开始的搜索的起始索引。</param>
    /// <param name="count">要搜索的部分中的元素数。</param>
    /// <param name="match">
    /// <see cref="T:System.Predicate`1" /> 委托，用于定义要搜索的元素的条件。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="match" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 超出的有效索引范围 <see cref="T:System.Collections.Generic.List`1" />。- 或 -<paramref name="count" /> 小于 0。- 或 -<paramref name="startIndex" /> 和 <paramref name="count" /> 未指定中的有效部分 <see cref="T:System.Collections.Generic.List`1" />。</exception>
    [__DynamicallyInvokable]
    public int FindIndex(int startIndex, int count, Predicate<T> match)
    {
      if ((uint) startIndex > (uint) this._size)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
      if (count < 0 || startIndex > this._size - count)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_Count);
      if (match == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
      int num = startIndex + count;
      for (int index = startIndex; index < num; ++index)
      {
        if (match(this._items[index]))
          return index;
      }
      return -1;
    }

    /// <summary>搜索与指定谓词所定义的条件相匹配的元素，并返回整个 <see cref="T:System.Collections.Generic.List`1" /> 中的最后一个匹配元素。</summary>
    /// <returns>如果找到，则为与指定谓词所定义的条件相匹配的最后一个元素；否则为类型 <paramref name="T" /> 的默认值。</returns>
    /// <param name="match">
    /// <see cref="T:System.Predicate`1" /> 委托，用于定义要搜索的元素的条件。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="match" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public T FindLast(Predicate<T> match)
    {
      if (match == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
      for (int index = this._size - 1; index >= 0; --index)
      {
        if (match(this._items[index]))
          return this._items[index];
      }
      return default (T);
    }

    /// <summary>搜索与指定谓词所定义的条件相匹配的元素，并返回整个 <see cref="T:System.Collections.Generic.List`1" /> 中最后一个匹配元素的从零开始的索引。</summary>
    /// <returns>如果找到与 <paramref name="match" /> 定义的条件相匹配的最后一个元素，则为该元素的从零开始的索引；否则为 -1。</returns>
    /// <param name="match">
    /// <see cref="T:System.Predicate`1" /> 委托，用于定义要搜索的元素的条件。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="match" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public int FindLastIndex(Predicate<T> match)
    {
      return this.FindLastIndex(this._size - 1, this._size, match);
    }

    /// <summary>搜索与由指定谓词定义的条件相匹配的元素，并返回 <see cref="T:System.Collections.Generic.List`1" /> 中从第一个元素到指定索引的元素范围内最后一个匹配项的从零开始的索引。</summary>
    /// <returns>如果找到与 <paramref name="match" /> 定义的条件相匹配的最后一个元素，则为该元素的从零开始的索引；否则为 -1。</returns>
    /// <param name="startIndex">向后搜索的从零开始的起始索引。</param>
    /// <param name="match">
    /// <see cref="T:System.Predicate`1" /> 委托，用于定义要搜索的元素的条件。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="match" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 超出的有效索引范围 <see cref="T:System.Collections.Generic.List`1" />。</exception>
    [__DynamicallyInvokable]
    public int FindLastIndex(int startIndex, Predicate<T> match)
    {
      int startIndex1 = startIndex;
      int num = 1;
      int count = startIndex1 + num;
      Predicate<T> match1 = match;
      return this.FindLastIndex(startIndex1, count, match1);
    }

    /// <summary>搜索与指定谓词所定义的条件相匹配的元素，并返回 <see cref="T:System.Collections.Generic.List`1" /> 中包含指定元素个数、到指定索引结束的元素范围内最后一个匹配项的从零开始的索引。</summary>
    /// <returns>如果找到与 <paramref name="match" /> 定义的条件相匹配的最后一个元素，则为该元素的从零开始的索引；否则为 -1。</returns>
    /// <param name="startIndex">向后搜索的从零开始的起始索引。</param>
    /// <param name="count">要搜索的部分中的元素数。</param>
    /// <param name="match">
    /// <see cref="T:System.Predicate`1" /> 委托，用于定义要搜索的元素的条件。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="match" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 超出的有效索引范围 <see cref="T:System.Collections.Generic.List`1" />。- 或 -<paramref name="count" /> 小于 0。- 或 -<paramref name="startIndex" /> 和 <paramref name="count" /> 未指定中的有效部分 <see cref="T:System.Collections.Generic.List`1" />。</exception>
    [__DynamicallyInvokable]
    public int FindLastIndex(int startIndex, int count, Predicate<T> match)
    {
      if (match == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
      if (this._size == 0)
      {
        if (startIndex != -1)
          ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
      }
      else if ((uint) startIndex >= (uint) this._size)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
      if (count < 0 || startIndex - count + 1 < 0)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_Count);
      int num = startIndex - count;
      for (int index = startIndex; index > num; --index)
      {
        if (match(this._items[index]))
          return index;
      }
      return -1;
    }

    /// <summary>对 <see cref="T:System.Collections.Generic.List`1" /> 的每个元素执行指定操作。</summary>
    /// <param name="action">要对 <see cref="T:System.Collections.Generic.List`1" /> 的每个元素执行的 <see cref="T:System.Action`1" /> 委托。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="action" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">在集合中的元素已被修改。从.NET Framework 4.5 开始，将引发此异常。</exception>
    [__DynamicallyInvokable]
    public void ForEach(Action<T> action)
    {
      if (action == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
      int num = this._version;
      for (int index = 0; index < this._size && (num == this._version || !BinaryCompatibility.TargetsAtLeast_Desktop_V4_5); ++index)
        action(this._items[index]);
      if (num == this._version || !BinaryCompatibility.TargetsAtLeast_Desktop_V4_5)
        return;
      ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
    }

    /// <summary>返回循环访问 <see cref="T:System.Collections.Generic.List`1" /> 的枚举数。</summary>
    /// <returns>用于 <see cref="T:System.Collections.Generic.List`1.Enumerator" /> 的 <see cref="T:System.Collections.Generic.List`1" />。</returns>
    [__DynamicallyInvokable]
    public List<T>.Enumerator GetEnumerator()
    {
      return new List<T>.Enumerator(this);
    }

    [__DynamicallyInvokable]
    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
      return (IEnumerator<T>) new List<T>.Enumerator(this);
    }

    [__DynamicallyInvokable]
    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) new List<T>.Enumerator(this);
    }

    /// <summary>在源 <see cref="T:System.Collections.Generic.List`1" /> 中创建元素范围的浅表复制。</summary>
    /// <returns>源 <see cref="T:System.Collections.Generic.List`1" /> 中的元素范围的浅表副本复制。</returns>
    /// <param name="index">范围开始处的从零开始的 <see cref="T:System.Collections.Generic.List`1" /> 索引。</param>
    /// <param name="count">范围中的元素数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于 0。- 或 -<paramref name="count" /> 小于 0。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="index" /> 和 <paramref name="count" /> 不表示有效范围中的元素为 <see cref="T:System.Collections.Generic.List`1" />。</exception>
    [__DynamicallyInvokable]
    public List<T> GetRange(int index, int count)
    {
      if (index < 0)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
      if (count < 0)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
      if (this._size - index < count)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
      List<T> objList = new List<T>(count);
      Array.Copy((Array) this._items, index, (Array) objList._items, 0, count);
      objList._size = count;
      return objList;
    }

    /// <summary>搜索指定的对象，并返回整个 <see cref="T:System.Collections.Generic.List`1" /> 中第一个匹配项的从零开始的索引。</summary>
    /// <returns>如果在整个 <paramref name="item" /> 中找到 <see cref="T:System.Collections.Generic.List`1" /> 的匹配项，则为第一个匹配项的从零开始的索引；否则为 -1。</returns>
    /// <param name="item">要在 <see cref="T:System.Collections.Generic.List`1" /> 中定位的对象。对于引用类型，该值可以为 null。</param>
    [__DynamicallyInvokable]
    public int IndexOf(T item)
    {
      return Array.IndexOf<T>(this._items, item, 0, this._size);
    }

    [__DynamicallyInvokable]
    int IList.IndexOf(object item)
    {
      if (List<T>.IsCompatibleObject(item))
        return this.IndexOf((T) item);
      return -1;
    }

    /// <summary>搜索指定对象并返回 <see cref="T:System.Collections.Generic.List`1" /> 中从指定索引到最后一个元素这部分元素中第一个匹配项的从零开始索引。</summary>
    /// <returns>如果在 <see cref="T:System.Collections.Generic.List`1" /> 中从 <paramref name="index" /> 到最后一个元素的元素范围内找到 <paramref name="item" /> 的第一个匹配项，则为该项的从零开始的索引；否则为 -1。</returns>
    /// <param name="item">要在 <see cref="T:System.Collections.Generic.List`1" /> 中定位的对象。对于引用类型，该值可以为 null。</param>
    /// <param name="index">从零开始的搜索的起始索引。空列表中 0（零）为有效值。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 超出的有效索引范围 <see cref="T:System.Collections.Generic.List`1" />。</exception>
    [__DynamicallyInvokable]
    public int IndexOf(T item, int index)
    {
      if (index > this._size)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_Index);
      return Array.IndexOf<T>(this._items, item, index, this._size - index);
    }

    /// <summary>搜索指定对象并返回 <see cref="T:System.Collections.Generic.List`1" /> 中从指定索引开始并包含指定元素数的这部分元素中第一个匹配项的从零开始索引。</summary>
    /// <returns>如果在 <see cref="T:System.Collections.Generic.List`1" /> 中从 <paramref name="index" /> 开始并包含 <paramref name="count" /> 个元素的元素范围内找到 <paramref name="item" /> 的第一个匹配项，则为该项的从零开始的索引；否则为 -1。</returns>
    /// <param name="item">要在 <see cref="T:System.Collections.Generic.List`1" /> 中定位的对象。对于引用类型，该值可以为 null。</param>
    /// <param name="index">从零开始的搜索的起始索引。空列表中 0（零）为有效值。</param>
    /// <param name="count">要搜索的部分中的元素数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 超出的有效索引范围 <see cref="T:System.Collections.Generic.List`1" />。- 或 -<paramref name="count" /> 小于 0。- 或 -<paramref name="index" /> 和 <paramref name="count" /> 未指定中的有效部分 <see cref="T:System.Collections.Generic.List`1" />。</exception>
    [__DynamicallyInvokable]
    public int IndexOf(T item, int index, int count)
    {
      if (index > this._size)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_Index);
      if (count < 0 || index > this._size - count)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_Count);
      return Array.IndexOf<T>(this._items, item, index, count);
    }

    /// <summary>将元素插入 <see cref="T:System.Collections.Generic.List`1" /> 的指定索引处。</summary>
    /// <param name="index">应插入 <paramref name="item" /> 的从零开始的索引。</param>
    /// <param name="item">要插入的对象。对于引用类型，该值可以为 null。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于 0。- 或 -<paramref name="index" /> 大于 <see cref="P:System.Collections.Generic.List`1.Count" />。</exception>
    [__DynamicallyInvokable]
    public void Insert(int index, T item)
    {
      if ((uint) index > (uint) this._size)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_ListInsert);
      if (this._size == this._items.Length)
        this.EnsureCapacity(this._size + 1);
      if (index < this._size)
        Array.Copy((Array) this._items, index, (Array) this._items, index + 1, this._size - index);
      this._items[index] = item;
      this._size = this._size + 1;
      this._version = this._version + 1;
    }

    [__DynamicallyInvokable]
    void IList.Insert(int index, object item)
    {
      ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(item, ExceptionArgument.item);
      try
      {
        this.Insert(index, (T) item);
      }
      catch (InvalidCastException ex)
      {
        ThrowHelper.ThrowWrongValueTypeArgumentException(item, typeof (T));
      }
    }

    /// <summary>将集合中的元素插入 <see cref="T:System.Collections.Generic.List`1" /> 的指定索引处。</summary>
    /// <param name="index">应在此处插入新元素的从零开始的索引。</param>
    /// <param name="collection">应将其元素插入到 <see cref="T:System.Collections.Generic.List`1" /> 中的集合。集合自身不能为 null，但它可以包含为 null 的元素（如果类型 <paramref name="T" /> 为引用类型）。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="collection" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于 0。- 或 -<paramref name="index" /> 大于 <see cref="P:System.Collections.Generic.List`1.Count" />。</exception>
    [__DynamicallyInvokable]
    public void InsertRange(int index, IEnumerable<T> collection)
    {
      if (collection == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
      if ((uint) index > (uint) this._size)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_Index);
      ICollection<T> objs = collection as ICollection<T>;
      if (objs != null)
      {
        int count = objs.Count;
        if (count > 0)
        {
          this.EnsureCapacity(this._size + count);
          if (index < this._size)
            Array.Copy((Array) this._items, index, (Array) this._items, index + count, this._size - index);
          if (this == objs)
          {
            T[] objArray1 = this._items;
            int sourceIndex = 0;
            T[] objArray2 = this._items;
            int num = index;
            Array.Copy((Array) objArray1, sourceIndex, (Array) objArray2, num, num);
            Array.Copy((Array) this._items, index + count, (Array) this._items, index * 2, this._size - index);
          }
          else
          {
            T[] array = new T[count];
            objs.CopyTo(array, 0);
            array.CopyTo((Array) this._items, index);
          }
          this._size = this._size + count;
        }
      }
      else
      {
        foreach (T obj in collection)
          this.Insert(index++, obj);
      }
      this._version = this._version + 1;
    }

    /// <summary>搜索指定对象并返回整个 <see cref="T:System.Collections.Generic.List`1" /> 中最后一个匹配项的从零开始索引。</summary>
    /// <returns>如果在整个 <see cref="T:System.Collections.Generic.List`1" /> 中找到 <paramref name="item" /> 的最后一个匹配项，则为该项的从零开始的索引；否则为 -1。</returns>
    /// <param name="item">要在 <see cref="T:System.Collections.Generic.List`1" /> 中定位的对象。对于引用类型，该值可以为 null。</param>
    [__DynamicallyInvokable]
    public int LastIndexOf(T item)
    {
      if (this._size == 0)
        return -1;
      return this.LastIndexOf(item, this._size - 1, this._size);
    }

    /// <summary>搜索指定对象并返回 <see cref="T:System.Collections.Generic.List`1" /> 中从第一个元素到指定索引这部分元素中最后一个匹配项的从零开始的索引。</summary>
    /// <returns>如果在 <see cref="T:System.Collections.Generic.List`1" /> 中从第一个元素到 <paramref name="index" /> 的元素范围内找到 <paramref name="item" /> 的最后一个匹配项，则为该项的从零开始的索引；否则为 -1。</returns>
    /// <param name="item">要在 <see cref="T:System.Collections.Generic.List`1" /> 中定位的对象。对于引用类型，该值可以为 null。</param>
    /// <param name="index">向后搜索的从零开始的起始索引。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 超出的有效索引范围 <see cref="T:System.Collections.Generic.List`1" />。</exception>
    [__DynamicallyInvokable]
    public int LastIndexOf(T item, int index)
    {
      if (index >= this._size)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_Index);
      T obj = item;
      int index1 = index;
      int num = 1;
      int count = index1 + num;
      return this.LastIndexOf(obj, index1, count);
    }

    /// <summary>搜索指定对象并返回 <see cref="T:System.Collections.Generic.List`1" /> 中到指定索引为止包含指定元素数的这部分元素中最后一个匹配项的从零开始索引。</summary>
    /// <returns>如果找到包含 <paramref name="count" /> 个元素、到 <paramref name="index" /> 为止的索引，则为 <see cref="T:System.Collections.Generic.List`1" /> 中元素范围内 <paramref name="item" /> 的最后一个匹配项的从零开始的索引；否则为 -1。</returns>
    /// <param name="item">要在 <see cref="T:System.Collections.Generic.List`1" /> 中定位的对象。对于引用类型，该值可以为 null。</param>
    /// <param name="index">向后搜索的从零开始的起始索引。</param>
    /// <param name="count">要搜索的部分中的元素数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 超出的有效索引范围 <see cref="T:System.Collections.Generic.List`1" />。- 或 -<paramref name="count" /> 小于 0。- 或 -<paramref name="index" /> 和 <paramref name="count" /> 未指定中的有效部分 <see cref="T:System.Collections.Generic.List`1" />。</exception>
    [__DynamicallyInvokable]
    public int LastIndexOf(T item, int index, int count)
    {
      if (this.Count != 0 && index < 0)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
      if (this.Count != 0 && count < 0)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
      if (this._size == 0)
        return -1;
      if (index >= this._size)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_BiggerThanCollection);
      if (count > index + 1)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_BiggerThanCollection);
      return Array.LastIndexOf<T>(this._items, item, index, count);
    }

    /// <summary>从 <see cref="T:System.Collections.Generic.List`1" /> 中移除特定对象的第一个匹配项。</summary>
    /// <returns>如果成功移除了 <paramref name="item" />，则为 true；否则为 false。如果在 false 中没有找到 <paramref name="item" />，则此方法也会返回 <see cref="T:System.Collections.Generic.List`1" />。</returns>
    /// <param name="item">要从 <see cref="T:System.Collections.Generic.List`1" /> 中删除的对象。对于引用类型，该值可以为 null。</param>
    [__DynamicallyInvokable]
    public bool Remove(T item)
    {
      int index = this.IndexOf(item);
      if (index < 0)
        return false;
      this.RemoveAt(index);
      return true;
    }

    [__DynamicallyInvokable]
    void IList.Remove(object item)
    {
      if (!List<T>.IsCompatibleObject(item))
        return;
      this.Remove((T) item);
    }

    /// <summary>移除与指定的谓词所定义的条件相匹配的所有元素。</summary>
    /// <returns>从 <see cref="T:System.Collections.Generic.List`1" /> 中移除的元素数。</returns>
    /// <param name="match">
    /// <see cref="T:System.Predicate`1" /> 委托，用于定义要移除的元素应满足的条件。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="match" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public int RemoveAll(Predicate<T> match)
    {
      if (match == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
      int index1 = 0;
      while (index1 < this._size && !match(this._items[index1]))
        ++index1;
      if (index1 >= this._size)
        return 0;
      int index2 = index1 + 1;
      while (index2 < this._size)
      {
        while (index2 < this._size && match(this._items[index2]))
          ++index2;
        if (index2 < this._size)
          this._items[index1++] = this._items[index2++];
      }
      Array.Clear((Array) this._items, index1, this._size - index1);
      int num = this._size - index1;
      this._size = index1;
      this._version = this._version + 1;
      return num;
    }

    /// <summary>移除 <see cref="T:System.Collections.Generic.List`1" /> 的指定索引处的元素。</summary>
    /// <param name="index">要移除的元素的从零开始的索引。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于 0。- 或 -<paramref name="index" /> 等于或大于 <see cref="P:System.Collections.Generic.List`1.Count" />。</exception>
    [__DynamicallyInvokable]
    public void RemoveAt(int index)
    {
      if ((uint) index >= (uint) this._size)
        ThrowHelper.ThrowArgumentOutOfRangeException();
      this._size = this._size - 1;
      if (index < this._size)
        Array.Copy((Array) this._items, index + 1, (Array) this._items, index, this._size - index);
      this._items[this._size] = default (T);
      this._version = this._version + 1;
    }

    /// <summary>从 <see cref="T:System.Collections.Generic.List`1" /> 中移除一系列元素。</summary>
    /// <param name="index">要移除的元素范围的从零开始的起始索引。</param>
    /// <param name="count">要移除的元素数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于 0。- 或 -<paramref name="count" /> 小于 0。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="index" /> 和 <paramref name="count" /> 不表示有效范围中的元素为 <see cref="T:System.Collections.Generic.List`1" />。</exception>
    [__DynamicallyInvokable]
    public void RemoveRange(int index, int count)
    {
      if (index < 0)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
      if (count < 0)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
      if (this._size - index < count)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
      if (count <= 0)
        return;
      int num = this._size;
      this._size = this._size - count;
      if (index < this._size)
        Array.Copy((Array) this._items, index + count, (Array) this._items, index, this._size - index);
      Array.Clear((Array) this._items, this._size, count);
      this._version = this._version + 1;
    }

    /// <summary>将整个 <see cref="T:System.Collections.Generic.List`1" /> 中元素的顺序反转。</summary>
    [__DynamicallyInvokable]
    public void Reverse()
    {
      this.Reverse(0, this.Count);
    }

    /// <summary>将指定范围中元素的顺序反转。</summary>
    /// <param name="index">要反转的范围的从零开始的起始索引。</param>
    /// <param name="count">要反转的范围内的元素数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于 0。- 或 -<paramref name="count" /> 小于 0。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="index" /> 和 <paramref name="count" /> 不表示有效范围中的元素为 <see cref="T:System.Collections.Generic.List`1" />。</exception>
    [__DynamicallyInvokable]
    public void Reverse(int index, int count)
    {
      if (index < 0)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
      if (count < 0)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
      if (this._size - index < count)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
      Array.Reverse((Array) this._items, index, count);
      this._version = this._version + 1;
    }

    /// <summary>使用默认比较器对整个 <see cref="T:System.Collections.Generic.List`1" /> 中的元素进行排序。</summary>
    /// <exception cref="T:System.InvalidOperationException">默认比较器 <see cref="P:System.Collections.Generic.Comparer`1.Default" /> 找不到的一种实现 <see cref="T:System.IComparable`1" /> 泛型接口或 <see cref="T:System.IComparable" /> 类型的接口 <paramref name="T" />。</exception>
    [__DynamicallyInvokable]
    public void Sort()
    {
      this.Sort(0, this.Count, (IComparer<T>) null);
    }

    /// <summary>使用指定的比较器对整个 <see cref="T:System.Collections.Generic.List`1" /> 中的元素进行排序。</summary>
    /// <param name="comparer">比较元素时要使用的 <see cref="T:System.Collections.Generic.IComparer`1" /> 实现，若要使用默认比较器 <see cref="P:System.Collections.Generic.Comparer`1.Default" />，则为 null。</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="comparer" /> 是 null, ，和默认比较器 <see cref="P:System.Collections.Generic.Comparer`1.Default" /> 找不到实现 <see cref="T:System.IComparable`1" /> 泛型接口或 <see cref="T:System.IComparable" /> 类型的接口 <paramref name="T" />。</exception>
    /// <exception cref="T:System.ArgumentException">实现 <paramref name="comparer" /> 在排序期间导致了错误。例如， <paramref name="comparer" /> 比较某个项与其自身时可能不会返回 0。</exception>
    [__DynamicallyInvokable]
    public void Sort(IComparer<T> comparer)
    {
      this.Sort(0, this.Count, comparer);
    }

    /// <summary>使用指定的比较器对 <see cref="T:System.Collections.Generic.List`1" /> 中某个范围内的元素进行排序。</summary>
    /// <param name="index">要排序范围的从零开始的起始索引。</param>
    /// <param name="count">要排序的范围的长度。</param>
    /// <param name="comparer">比较元素时要使用的 <see cref="T:System.Collections.Generic.IComparer`1" /> 实现，若要使用默认比较器 <see cref="P:System.Collections.Generic.Comparer`1.Default" />，则为 null。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于 0。- 或 -<paramref name="count" /> 小于 0。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="index" /> 和 <paramref name="count" /> 未指定中的有效范围 <see cref="T:System.Collections.Generic.List`1" />。- 或 -实现 <paramref name="comparer" /> 在排序期间导致了错误。例如， <paramref name="comparer" /> 比较某个项与其自身时可能不会返回 0。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="comparer" /> 是 null, ，和默认比较器 <see cref="P:System.Collections.Generic.Comparer`1.Default" /> 找不到实现 <see cref="T:System.IComparable`1" /> 泛型接口或 <see cref="T:System.IComparable" /> 类型的接口 <paramref name="T" />。</exception>
    [__DynamicallyInvokable]
    public void Sort(int index, int count, IComparer<T> comparer)
    {
      if (index < 0)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
      if (count < 0)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
      if (this._size - index < count)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
      Array.Sort<T>(this._items, index, count, comparer);
      this._version = this._version + 1;
    }

    /// <summary>使用指定的 <see cref="T:System.Comparison`1" />，对整个 <see cref="T:System.Collections.Generic.List`1" /> 中的元素进行排序。</summary>
    /// <param name="comparison">比较元素时要使用的 <see cref="T:System.Comparison`1" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="comparison" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">实现 <paramref name="comparison" /> 在排序期间导致了错误。例如， <paramref name="comparison" /> 比较某个项与其自身时可能不会返回 0。</exception>
    [__DynamicallyInvokable]
    public void Sort(Comparison<T> comparison)
    {
      if (comparison == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
      if (this._size <= 0)
        return;
      Array.Sort<T>(this._items, 0, this._size, (IComparer<T>) new Array.FunctorComparer<T>(comparison));
    }

    /// <summary>将 <see cref="T:System.Collections.Generic.List`1" /> 的元素复制到新数组中。</summary>
    /// <returns>一个包含 <see cref="T:System.Collections.Generic.List`1" /> 的元素副本的数组。</returns>
    [__DynamicallyInvokable]
    public T[] ToArray()
    {
      T[] objArray = new T[this._size];
      Array.Copy((Array) this._items, 0, (Array) objArray, 0, this._size);
      return objArray;
    }

    /// <summary>将容量设置为 <see cref="T:System.Collections.Generic.List`1" /> 中元素的实际数目（如果该数目小于某个阈值）。</summary>
    [__DynamicallyInvokable]
    public void TrimExcess()
    {
      if (this._size >= (int) ((double) this._items.Length * 0.9))
        return;
      this.Capacity = this._size;
    }

    /// <summary>确定 <see cref="T:System.Collections.Generic.List`1" /> 中的每个元素是否都与指定谓词定义的条件匹配。</summary>
    /// <returns>如果 <see cref="T:System.Collections.Generic.List`1" /> 中的每个元素都与指定的谓词所定义的条件相匹配，则为 true；否则为 false。如果列表没有元素，则返回值为 true。</returns>
    /// <param name="match">用于定义检查元素时要对照条件的 <see cref="T:System.Predicate`1" /> 委托。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="match" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public bool TrueForAll(Predicate<T> match)
    {
      if (match == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
      for (int index = 0; index < this._size; ++index)
      {
        if (!match(this._items[index]))
          return false;
      }
      return true;
    }

    internal static IList<T> Synchronized(List<T> list)
    {
      return (IList<T>) new List<T>.SynchronizedList(list);
    }

    [Serializable]
    internal class SynchronizedList : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
    {
      private List<T> _list;
      private object _root;

      public int Count
      {
        get
        {
          lock (this._root)
            return this._list.Count;
        }
      }

      public bool IsReadOnly
      {
        get
        {
          return ((ICollection<T>) this._list).IsReadOnly;
        }
      }

      public T this[int index]
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

      internal SynchronizedList(List<T> list)
      {
        this._list = list;
        this._root = ((ICollection) list).SyncRoot;
      }

      public void Add(T item)
      {
        lock (this._root)
          this._list.Add(item);
      }

      public void Clear()
      {
        lock (this._root)
          this._list.Clear();
      }

      public bool Contains(T item)
      {
        lock (this._root)
          return this._list.Contains(item);
      }

      public void CopyTo(T[] array, int arrayIndex)
      {
        lock (this._root)
          this._list.CopyTo(array, arrayIndex);
      }

      public bool Remove(T item)
      {
        lock (this._root)
          return this._list.Remove(item);
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
        lock (this._root)
          return (IEnumerator) this._list.GetEnumerator();
      }

      IEnumerator<T> IEnumerable<T>.GetEnumerator()
      {
        lock (this._root)
          return ((IEnumerable<T>) this._list).GetEnumerator();
      }

      public int IndexOf(T item)
      {
        lock (this._root)
          return this._list.IndexOf(item);
      }

      public void Insert(int index, T item)
      {
        lock (this._root)
          this._list.Insert(index, item);
      }

      public void RemoveAt(int index)
      {
        lock (this._root)
          this._list.RemoveAt(index);
      }
    }

    /// <summary>枚举 <see cref="T:System.Collections.Generic.List`1" /> 的元素。</summary>
    [__DynamicallyInvokable]
    [Serializable]
    public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
    {
      private List<T> list;
      private int index;
      private int version;
      private T current;

      /// <summary>获取枚举数当前位置的元素。</summary>
      /// <returns>
      /// <see cref="T:System.Collections.Generic.List`1" /> 中位于该枚举数当前位置的元素。</returns>
      [__DynamicallyInvokable]
      public T Current
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
          if (this.index == 0 || this.index == this.list._size + 1)
            ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
          return (object) this.Current;
        }
      }

      internal Enumerator(List<T> list)
      {
        this.list = list;
        this.index = 0;
        this.version = list._version;
        this.current = default (T);
      }

      /// <summary>释放由 <see cref="T:System.Collections.Generic.List`1.Enumerator" /> 使用的所有资源。</summary>
      [__DynamicallyInvokable]
      public void Dispose()
      {
      }

      /// <summary>使枚举数前进到 <see cref="T:System.Collections.Generic.List`1" /> 的下一个元素。</summary>
      /// <returns>如果枚举数成功地推进到下一个元素，则为 true；如果枚举数越过集合的结尾，则为 false。</returns>
      /// <exception cref="T:System.InvalidOperationException">在创建了枚举数后集合被修改了。</exception>
      [__DynamicallyInvokable]
      public bool MoveNext()
      {
        List<T> objList = this.list;
        if (this.version != objList._version || (uint) this.index >= (uint) objList._size)
          return this.MoveNextRare();
        this.current = objList._items[this.index];
        this.index = this.index + 1;
        return true;
      }

      private bool MoveNextRare()
      {
        if (this.version != this.list._version)
          ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
        this.index = this.list._size + 1;
        this.current = default (T);
        return false;
      }

      [__DynamicallyInvokable]
      void IEnumerator.Reset()
      {
        if (this.version != this.list._version)
          ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
        this.index = 0;
        this.current = default (T);
      }
    }
  }
}
