// Decompiled with JetBrains decompiler
// Type: System.Collections.ObjectModel.ReadOnlyCollection`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Collections.ObjectModel
{
  /// <summary>提供泛型只读集合的基类。</summary>
  /// <typeparam name="T">集合中的元素类型。</typeparam>
  [ComVisible(false)]
  [DebuggerTypeProxy(typeof (Mscorlib_CollectionDebugView<>))]
  [DebuggerDisplay("Count = {Count}")]
  [__DynamicallyInvokable]
  [Serializable]
  public class ReadOnlyCollection<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection, IReadOnlyList<T>, IReadOnlyCollection<T>
  {
    private IList<T> list;
    [NonSerialized]
    private object _syncRoot;

    /// <summary>获取 <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> 实例中包含的元素数。</summary>
    /// <returns>
    /// <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> 实例中包含的元素数。</returns>
    [__DynamicallyInvokable]
    public int Count
    {
      [__DynamicallyInvokable] get
      {
        return this.list.Count;
      }
    }

    /// <summary>获取指定索引处的元素。</summary>
    /// <returns>指定索引处的元素。</returns>
    /// <param name="index">要获取的元素的索引（索引从零开始）。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> is less than zero.-or-<paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.ObjectModel.ReadOnlyCollection`1.Count" />. </exception>
    [__DynamicallyInvokable]
    public T this[int index]
    {
      [__DynamicallyInvokable] get
      {
        return this.list[index];
      }
    }

    /// <summary>返回 <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> 包装的 <see cref="T:System.Collections.Generic.IList`1" />。</summary>
    /// <returns>
    /// <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> 包装的 <see cref="T:System.Collections.Generic.IList`1" />。</returns>
    [__DynamicallyInvokable]
    protected IList<T> Items
    {
      [__DynamicallyInvokable] get
      {
        return this.list;
      }
    }

    [__DynamicallyInvokable]
    bool ICollection<T>.IsReadOnly
    {
      [__DynamicallyInvokable] get
      {
        return true;
      }
    }

    [__DynamicallyInvokable]
    T IList<T>.this[int index]
    {
      [__DynamicallyInvokable] get
      {
        return this.list[index];
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
        if (this._syncRoot == null)
        {
          ICollection collection = this.list as ICollection;
          if (collection != null)
            this._syncRoot = collection.SyncRoot;
          else
            Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), (object) null);
        }
        return this._syncRoot;
      }
    }

    [__DynamicallyInvokable]
    bool IList.IsFixedSize
    {
      [__DynamicallyInvokable] get
      {
        return true;
      }
    }

    [__DynamicallyInvokable]
    bool IList.IsReadOnly
    {
      [__DynamicallyInvokable] get
      {
        return true;
      }
    }

    [__DynamicallyInvokable]
    object IList.this[int index]
    {
      [__DynamicallyInvokable] get
      {
        return (object) this.list[index];
      }
      [__DynamicallyInvokable] set
      {
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
      }
    }

    /// <summary>初始化 <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> 类的新实例，该实例是围绕指定列表的只读包装。</summary>
    /// <param name="list">要包装的列表。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="list" /> is null.</exception>
    [__DynamicallyInvokable]
    public ReadOnlyCollection(IList<T> list)
    {
      if (list == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.list);
      this.list = list;
    }

    /// <summary>确定某元素是否在 <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> 中。</summary>
    /// <returns>如果在 <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> 中找到 <paramref name="value" />，则为 true；否则为 false。</returns>
    /// <param name="value">要在 <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> 中定位的对象。对于引用类型，该值可以为 null。</param>
    [__DynamicallyInvokable]
    public bool Contains(T value)
    {
      return this.list.Contains(value);
    }

    /// <summary>从目标数组的指定索引处开始将整个 <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> 复制到兼容的一维 <see cref="T:System.Array" />。</summary>
    /// <param name="array">作为从 <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> 复制的元素的目标的一维 <see cref="T:System.Array" />。<see cref="T:System.Array" /> 必须具有从零开始的索引。</param>
    /// <param name="index">
    /// <paramref name="array" /> 中从零开始的索引，从此索引处开始进行复制。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> is null.</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> is less than zero.</exception>
    /// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
    [__DynamicallyInvokable]
    public void CopyTo(T[] array, int index)
    {
      this.list.CopyTo(array, index);
    }

    /// <summary>返回循环访问 <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> 的枚举数。</summary>
    /// <returns>用于 <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> 的 <see cref="T:System.Collections.Generic.IEnumerator`1" />。</returns>
    [__DynamicallyInvokable]
    public IEnumerator<T> GetEnumerator()
    {
      return this.list.GetEnumerator();
    }

    /// <summary>搜索指定的对象，并返回整个 <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> 中第一个匹配项的从零开始的索引。</summary>
    /// <returns>如果在整个 <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> 中找到 <paramref name="item" /> 的第一个匹配项，则为该项的从零开始的索引；否则为 -1。</returns>
    /// <param name="value">要在 <see cref="T:System.Collections.Generic.List`1" /> 中定位的对象。对于引用类型，该值可以为 null。</param>
    [__DynamicallyInvokable]
    public int IndexOf(T value)
    {
      return this.list.IndexOf(value);
    }

    [__DynamicallyInvokable]
    void ICollection<T>.Add(T value)
    {
      ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
    }

    [__DynamicallyInvokable]
    void ICollection<T>.Clear()
    {
      ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
    }

    [__DynamicallyInvokable]
    void IList<T>.Insert(int index, T value)
    {
      ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
    }

    [__DynamicallyInvokable]
    bool ICollection<T>.Remove(T value)
    {
      ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
      return false;
    }

    [__DynamicallyInvokable]
    void IList<T>.RemoveAt(int index)
    {
      ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
    }

    [__DynamicallyInvokable]
    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.list.GetEnumerator();
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
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.arrayIndex, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
      if (array.Length - index < this.Count)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
      T[] array1 = array as T[];
      if (array1 != null)
      {
        this.list.CopyTo(array1, index);
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
        int count = this.list.Count;
        try
        {
          for (int index1 = 0; index1 < count; ++index1)
            objArray[index++] = (object) this.list[index1];
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
      ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
      return -1;
    }

    [__DynamicallyInvokable]
    void IList.Clear()
    {
      ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
    }

    private static bool IsCompatibleObject(object value)
    {
      if (value is T)
        return true;
      if (value == null)
        return (object) default (T) == null;
      return false;
    }

    [__DynamicallyInvokable]
    bool IList.Contains(object value)
    {
      if (ReadOnlyCollection<T>.IsCompatibleObject(value))
        return this.Contains((T) value);
      return false;
    }

    [__DynamicallyInvokable]
    int IList.IndexOf(object value)
    {
      if (ReadOnlyCollection<T>.IsCompatibleObject(value))
        return this.IndexOf((T) value);
      return -1;
    }

    [__DynamicallyInvokable]
    void IList.Insert(int index, object value)
    {
      ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
    }

    [__DynamicallyInvokable]
    void IList.Remove(object value)
    {
      ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
    }

    [__DynamicallyInvokable]
    void IList.RemoveAt(int index)
    {
      ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
    }
  }
}
