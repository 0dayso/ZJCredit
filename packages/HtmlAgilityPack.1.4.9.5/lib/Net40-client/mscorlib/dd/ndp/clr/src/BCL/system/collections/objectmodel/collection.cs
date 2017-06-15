// Decompiled with JetBrains decompiler
// Type: System.Collections.ObjectModel.Collection`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Collections.ObjectModel
{
  /// <summary>提供泛型集合的基类。</summary>
  /// <typeparam name="T">集合中的元素类型。</typeparam>
  [ComVisible(false)]
  [DebuggerTypeProxy(typeof (Mscorlib_CollectionDebugView<>))]
  [DebuggerDisplay("Count = {Count}")]
  [__DynamicallyInvokable]
  [Serializable]
  public class Collection<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection, IReadOnlyList<T>, IReadOnlyCollection<T>
  {
    private IList<T> items;
    [NonSerialized]
    private object _syncRoot;

    /// <summary>获取 <see cref="T:System.Collections.ObjectModel.Collection`1" /> 中实际包含的元素数。</summary>
    /// <returns>
    /// <see cref="T:System.Collections.ObjectModel.Collection`1" /> 中实际包含的元素数。</returns>
    [__DynamicallyInvokable]
    public int Count
    {
      [__DynamicallyInvokable] get
      {
        return this.items.Count;
      }
    }

    /// <summary>获取 <see cref="T:System.Collections.ObjectModel.Collection`1" /> 周围的 <see cref="T:System.Collections.Generic.IList`1" /> 包装。</summary>
    /// <returns>
    /// <see cref="T:System.Collections.ObjectModel.Collection`1" /> 周围的 <see cref="T:System.Collections.Generic.IList`1" /> 包装。</returns>
    [__DynamicallyInvokable]
    protected IList<T> Items
    {
      [__DynamicallyInvokable] get
      {
        return this.items;
      }
    }

    /// <summary>获取或设置位于指定索引处的元素。</summary>
    /// <returns>位于指定索引处的元素。</returns>
    /// <param name="index">要获得或设置的元素从零开始的索引。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于零。- 或 -<paramref name="index" /> 等于或大于 <see cref="P:System.Collections.ObjectModel.Collection`1.Count" />。</exception>
    [__DynamicallyInvokable]
    public T this[int index]
    {
      [__DynamicallyInvokable] get
      {
        return this.items[index];
      }
      [__DynamicallyInvokable] set
      {
        if (this.items.IsReadOnly)
          ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
        if (index < 0 || index >= this.items.Count)
          ThrowHelper.ThrowArgumentOutOfRangeException();
        this.SetItem(index, value);
      }
    }

    [__DynamicallyInvokable]
    bool ICollection<T>.IsReadOnly
    {
      [__DynamicallyInvokable] get
      {
        return this.items.IsReadOnly;
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
        {
          ICollection collection = this.items as ICollection;
          if (collection != null)
            this._syncRoot = collection.SyncRoot;
          else
            Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), (object) null);
        }
        return this._syncRoot;
      }
    }

    [__DynamicallyInvokable]
    object IList.this[int index]
    {
      [__DynamicallyInvokable] get
      {
        return (object) this.items[index];
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

    [__DynamicallyInvokable]
    bool IList.IsReadOnly
    {
      [__DynamicallyInvokable] get
      {
        return this.items.IsReadOnly;
      }
    }

    [__DynamicallyInvokable]
    bool IList.IsFixedSize
    {
      [__DynamicallyInvokable] get
      {
        IList list = this.items as IList;
        if (list != null)
          return list.IsFixedSize;
        return this.items.IsReadOnly;
      }
    }

    /// <summary>初始化为空的 <see cref="T:System.Collections.ObjectModel.Collection`1" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public Collection()
    {
      this.items = (IList<T>) new List<T>();
    }

    /// <summary>将 <see cref="T:System.Collections.ObjectModel.Collection`1" /> 类的新实例初始化为指定列表的包装。</summary>
    /// <param name="list">由新的集合包装的列表。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="list" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public Collection(IList<T> list)
    {
      if (list == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.list);
      this.items = list;
    }

    /// <summary>将对象添加到 <see cref="T:System.Collections.ObjectModel.Collection`1" /> 的结尾处。</summary>
    /// <param name="item">要添加到 <see cref="T:System.Collections.ObjectModel.Collection`1" /> 的末尾处的对象。对于引用类型，该值可以为 null。</param>
    [__DynamicallyInvokable]
    public void Add(T item)
    {
      if (this.items.IsReadOnly)
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
      this.InsertItem(this.items.Count, item);
    }

    /// <summary>从 <see cref="T:System.Collections.ObjectModel.Collection`1" /> 中移除所有元素。</summary>
    [__DynamicallyInvokable]
    public void Clear()
    {
      if (this.items.IsReadOnly)
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
      this.ClearItems();
    }

    /// <summary>从目标数组的指定索引处开始将整个 <see cref="T:System.Collections.ObjectModel.Collection`1" /> 复制到兼容的一维 <see cref="T:System.Array" />。</summary>
    /// <param name="array">作为从 <see cref="T:System.Collections.ObjectModel.Collection`1" /> 复制的元素的目标位置的一维 <see cref="T:System.Array" />。<see cref="T:System.Array" /> 必须具有从零开始的索引。</param>
    /// <param name="index">
    /// <paramref name="array" /> 中从零开始的索引，从此索引处开始进行复制。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于零。</exception>
    /// <exception cref="T:System.ArgumentException">源 <see cref="T:System.Collections.ObjectModel.Collection`1" /> 中的元素数目大于从 <paramref name="index" /> 到目标 <paramref name="array" /> 末尾之间的可用空间。</exception>
    [__DynamicallyInvokable]
    public void CopyTo(T[] array, int index)
    {
      this.items.CopyTo(array, index);
    }

    /// <summary>确定某元素是否在 <see cref="T:System.Collections.ObjectModel.Collection`1" /> 中。</summary>
    /// <returns>如果在 <see cref="T:System.Collections.ObjectModel.Collection`1" /> 中找到 <paramref name="item" />，则为 true；否则为 false。</returns>
    /// <param name="item">要在 <see cref="T:System.Collections.ObjectModel.Collection`1" /> 中定位的对象。对于引用类型，该值可以为 null。</param>
    [__DynamicallyInvokable]
    public bool Contains(T item)
    {
      return this.items.Contains(item);
    }

    /// <summary>返回循环访问 <see cref="T:System.Collections.ObjectModel.Collection`1" /> 的枚举数。</summary>
    /// <returns>用于 <see cref="T:System.Collections.ObjectModel.Collection`1" /> 的 <see cref="T:System.Collections.Generic.IEnumerator`1" />。</returns>
    [__DynamicallyInvokable]
    public IEnumerator<T> GetEnumerator()
    {
      return this.items.GetEnumerator();
    }

    /// <summary>搜索指定的对象，并返回整个 <see cref="T:System.Collections.ObjectModel.Collection`1" /> 中第一个匹配项的从零开始的索引。</summary>
    /// <returns>如果在整个 <see cref="T:System.Collections.ObjectModel.Collection`1" /> 中找到 <paramref name="item" /> 的第一个匹配项，则为该项的从零开始的索引；否则为 -1。</returns>
    /// <param name="item">要在 <see cref="T:System.Collections.Generic.List`1" /> 中定位的对象。对于引用类型，该值可以为 null。</param>
    [__DynamicallyInvokable]
    public int IndexOf(T item)
    {
      return this.items.IndexOf(item);
    }

    /// <summary>将元素插入 <see cref="T:System.Collections.ObjectModel.Collection`1" /> 的指定索引处。</summary>
    /// <param name="index">从零开始的索引，应在该位置插入 <paramref name="item" />。</param>
    /// <param name="item">要插入的对象。对于引用类型，该值可以为 null。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于零。- 或 -<paramref name="index" /> 大于 <see cref="P:System.Collections.ObjectModel.Collection`1.Count" />。</exception>
    [__DynamicallyInvokable]
    public void Insert(int index, T item)
    {
      if (this.items.IsReadOnly)
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
      if (index < 0 || index > this.items.Count)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_ListInsert);
      this.InsertItem(index, item);
    }

    /// <summary>从 <see cref="T:System.Collections.ObjectModel.Collection`1" /> 中移除特定对象的第一个匹配项。</summary>
    /// <returns>如果成功移除 <paramref name="item" />，则为 true；否则为 false。如果在原始 <see cref="T:System.Collections.ObjectModel.Collection`1" /> 中没有找到 <paramref name="item" />，此方法也会返回 false。</returns>
    /// <param name="item">要从 <see cref="T:System.Collections.ObjectModel.Collection`1" /> 中移除的对象。对于引用类型，该值可以为 null。</param>
    [__DynamicallyInvokable]
    public bool Remove(T item)
    {
      if (this.items.IsReadOnly)
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
      int index = this.items.IndexOf(item);
      if (index < 0)
        return false;
      this.RemoveItem(index);
      return true;
    }

    /// <summary>移除 <see cref="T:System.Collections.ObjectModel.Collection`1" /> 的指定索引处的元素。</summary>
    /// <param name="index">要移除的元素的从零开始的索引。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于零。- 或 -<paramref name="index" /> 等于或大于 <see cref="P:System.Collections.ObjectModel.Collection`1.Count" />。</exception>
    [__DynamicallyInvokable]
    public void RemoveAt(int index)
    {
      if (this.items.IsReadOnly)
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
      if (index < 0 || index >= this.items.Count)
        ThrowHelper.ThrowArgumentOutOfRangeException();
      this.RemoveItem(index);
    }

    /// <summary>从 <see cref="T:System.Collections.ObjectModel.Collection`1" /> 中移除所有元素。</summary>
    [__DynamicallyInvokable]
    protected virtual void ClearItems()
    {
      this.items.Clear();
    }

    /// <summary>将元素插入 <see cref="T:System.Collections.ObjectModel.Collection`1" /> 的指定索引处。</summary>
    /// <param name="index">从零开始的索引，应在该位置插入 <paramref name="item" />。</param>
    /// <param name="item">要插入的对象。对于引用类型，该值可以为 null。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于零。- 或 -<paramref name="index" /> 大于 <see cref="P:System.Collections.ObjectModel.Collection`1.Count" />。</exception>
    [__DynamicallyInvokable]
    protected virtual void InsertItem(int index, T item)
    {
      this.items.Insert(index, item);
    }

    /// <summary>移除 <see cref="T:System.Collections.ObjectModel.Collection`1" /> 的指定索引处的元素。</summary>
    /// <param name="index">要移除的元素的从零开始的索引。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于零。- 或 -<paramref name="index" /> 等于或大于 <see cref="P:System.Collections.ObjectModel.Collection`1.Count" />。</exception>
    [__DynamicallyInvokable]
    protected virtual void RemoveItem(int index)
    {
      this.items.RemoveAt(index);
    }

    /// <summary>替换指定索引处的元素。</summary>
    /// <param name="index">待替换元素的从零开始的索引。</param>
    /// <param name="item">位于指定索引处的元素的新值。对于引用类型，该值可以为 null。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于零。- 或 -<paramref name="index" /> 大于 <see cref="P:System.Collections.ObjectModel.Collection`1.Count" />。</exception>
    [__DynamicallyInvokable]
    protected virtual void SetItem(int index, T item)
    {
      this.items[index] = item;
    }

    [__DynamicallyInvokable]
    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.items.GetEnumerator();
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
      if (index < 0)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
      if (array.Length - index < this.Count)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
      T[] array1 = array as T[];
      if (array1 != null)
      {
        this.items.CopyTo(array1, index);
      }
      else
      {
        Type elementType = array.GetType().GetElementType();
        Type c = typeof (T);
        if (!elementType.IsAssignableFrom(c) && !c.IsAssignableFrom(elementType))
          ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
        object[] objArray = array as object[];
        if (objArray == null)
          ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
        int count = this.items.Count;
        try
        {
          for (int index1 = 0; index1 < count; ++index1)
            objArray[index++] = (object) this.items[index1];
        }
        catch (ArrayTypeMismatchException ex)
        {
          ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
        }
      }
    }

    [__DynamicallyInvokable]
    int IList.Add(object value)
    {
      if (this.items.IsReadOnly)
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
      ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(value, ExceptionArgument.value);
      try
      {
        this.Add((T) value);
      }
      catch (InvalidCastException ex)
      {
        ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof (T));
      }
      return this.Count - 1;
    }

    [__DynamicallyInvokable]
    bool IList.Contains(object value)
    {
      if (Collection<T>.IsCompatibleObject(value))
        return this.Contains((T) value);
      return false;
    }

    [__DynamicallyInvokable]
    int IList.IndexOf(object value)
    {
      if (Collection<T>.IsCompatibleObject(value))
        return this.IndexOf((T) value);
      return -1;
    }

    [__DynamicallyInvokable]
    void IList.Insert(int index, object value)
    {
      if (this.items.IsReadOnly)
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
      ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(value, ExceptionArgument.value);
      try
      {
        this.Insert(index, (T) value);
      }
      catch (InvalidCastException ex)
      {
        ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof (T));
      }
    }

    [__DynamicallyInvokable]
    void IList.Remove(object value)
    {
      if (this.items.IsReadOnly)
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
      if (!Collection<T>.IsCompatibleObject(value))
        return;
      this.Remove((T) value);
    }

    private static bool IsCompatibleObject(object value)
    {
      if (value is T)
        return true;
      if (value == null)
        return (object) default (T) == null;
      return false;
    }
  }
}
