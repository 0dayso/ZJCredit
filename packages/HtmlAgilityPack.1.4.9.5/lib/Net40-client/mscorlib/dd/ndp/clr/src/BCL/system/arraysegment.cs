// Decompiled with JetBrains decompiler
// Type: System.ArraySegment`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;

namespace System
{
  /// <summary>分隔一维数组的一部分。</summary>
  /// <typeparam name="T">数组段中元素的类型。</typeparam>
  /// <filterpriority>2</filterpriority>
  [__DynamicallyInvokable]
  [Serializable]
  public struct ArraySegment<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IReadOnlyList<T>, IReadOnlyCollection<T>
  {
    private T[] _array;
    private int _offset;
    private int _count;

    /// <summary>获取原始数组，其中包含数组段分隔的元素范围。</summary>
    /// <returns>传递到构造函数并且包含由 <see cref="T:System.ArraySegment`1" /> 分隔的范围的原始数组。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public T[] Array
    {
      [__DynamicallyInvokable] get
      {
        return this._array;
      }
    }

    /// <summary>获取由数组段分隔的范围中的第一个元素的位置（相对于原始数组的开始位置）。</summary>
    /// <returns>由 <see cref="T:System.ArraySegment`1" /> 分隔的范围中的第一个元素的位置（相对于原始数组的开始位置）。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public int Offset
    {
      [__DynamicallyInvokable] get
      {
        return this._offset;
      }
    }

    /// <summary>获取由数组段分隔的范围中的元素个数。</summary>
    /// <returns>由 <see cref="T:System.ArraySegment`1" /> 分隔的范围中的元素个数。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public int Count
    {
      [__DynamicallyInvokable] get
      {
        return this._count;
      }
    }

    [__DynamicallyInvokable]
    T IList<T>.this[int index]
    {
      [__DynamicallyInvokable] get
      {
        if (this._array == null)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullArray"));
        if (index < 0 || index >= this._count)
          throw new ArgumentOutOfRangeException("index");
        return this._array[this._offset + index];
      }
      [__DynamicallyInvokable] set
      {
        if (this._array == null)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullArray"));
        if (index < 0 || index >= this._count)
          throw new ArgumentOutOfRangeException("index");
        this._array[this._offset + index] = value;
      }
    }

    [__DynamicallyInvokable]
    T IReadOnlyList<T>.this[int index]
    {
      [__DynamicallyInvokable] get
      {
        if (this._array == null)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullArray"));
        if (index < 0 || index >= this._count)
          throw new ArgumentOutOfRangeException("index");
        return this._array[this._offset + index];
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

    /// <summary>初始化 <see cref="T:System.ArraySegment`1" /> 结构的新实例，该结构用于分隔指定数组中的所有元素。</summary>
    /// <param name="array">要包装的数组。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public ArraySegment(T[] array)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      this._array = array;
      this._offset = 0;
      this._count = array.Length;
    }

    /// <summary>初始化 <see cref="T:System.ArraySegment`1" /> 结构的新结构，该结构用于分隔指定数组中指定的元素范围。</summary>
    /// <param name="array">包含要分隔的元素范围的数组。</param>
    /// <param name="offset">范围中第一个元素的从零开始的索引。</param>
    /// <param name="count">范围中的元素数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="offset" /> 或 <paramref name="count" /> 小于 0。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="offset" /> 和 <paramref name="count" /> 不指定 <paramref name="array" /> 中的有效范围。</exception>
    [__DynamicallyInvokable]
    public ArraySegment(T[] array, int offset, int count)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      if (offset < 0)
        throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (array.Length - offset < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      this._array = array;
      this._offset = offset;
      this._count = count;
    }

    /// <summary>指示两个 <see cref="T:System.ArraySegment`1" /> 结构是否等同。</summary>
    /// <returns>如果 <paramref name="a" /> 等于 <paramref name="b" />，则为 true；否则为 false。</returns>
    /// <param name="a">相等运算符左侧的结构。</param>
    /// <param name="b">相等运算符右侧的结构。</param>
    [__DynamicallyInvokable]
    public static bool operator ==(ArraySegment<T> a, ArraySegment<T> b)
    {
      return a.Equals(b);
    }

    /// <summary>指示两个 <see cref="T:System.ArraySegment`1" /> 结构是否不相等。</summary>
    /// <returns>如果 <paramref name="a" /> 不等于 <paramref name="b" />，则为 true；否则为 false。</returns>
    /// <param name="a">不等运算符左侧的结构。</param>
    /// <param name="b">不等运算符右侧的结构。</param>
    [__DynamicallyInvokable]
    public static bool operator !=(ArraySegment<T> a, ArraySegment<T> b)
    {
      return !(a == b);
    }

    /// <summary>返回当前实例的哈希代码。</summary>
    /// <returns>32 位有符号整数哈希代码。</returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      if (this._array != null)
        return this._array.GetHashCode() ^ this._offset ^ this._count;
      return 0;
    }

    /// <summary>确定指定的对象是否等于当前实例。</summary>
    /// <returns>如果指定的对象为 <see cref="T:System.ArraySegment`1" /> 结构并且等于当前实例，则为 true；否则为 false。</returns>
    /// <param name="obj">要与当前实例进行比较的对象。</param>
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      if (obj is ArraySegment<T>)
        return this.Equals((ArraySegment<T>) obj);
      return false;
    }

    /// <summary>确定指定的 <see cref="T:System.ArraySegment`1" /> 结构是否等于当前实例。</summary>
    /// <returns>如果指定的 <see cref="T:System.ArraySegment`1" /> 结构等于当前实例，则为 true；否则为 false。</returns>
    /// <param name="obj">要与当前实例进行比较的结构。</param>
    [__DynamicallyInvokable]
    public bool Equals(ArraySegment<T> obj)
    {
      if (obj._array == this._array && obj._offset == this._offset)
        return obj._count == this._count;
      return false;
    }

    [__DynamicallyInvokable]
    int IList<T>.IndexOf(T item)
    {
      if (this._array == null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullArray"));
      int num = System.Array.IndexOf<T>(this._array, item, this._offset, this._count);
      if (num < 0)
        return -1;
      return num - this._offset;
    }

    [__DynamicallyInvokable]
    void IList<T>.Insert(int index, T item)
    {
      throw new NotSupportedException();
    }

    [__DynamicallyInvokable]
    void IList<T>.RemoveAt(int index)
    {
      throw new NotSupportedException();
    }

    [__DynamicallyInvokable]
    void ICollection<T>.Add(T item)
    {
      throw new NotSupportedException();
    }

    [__DynamicallyInvokable]
    void ICollection<T>.Clear()
    {
      throw new NotSupportedException();
    }

    [__DynamicallyInvokable]
    bool ICollection<T>.Contains(T item)
    {
      if (this._array == null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullArray"));
      return System.Array.IndexOf<T>(this._array, item, this._offset, this._count) >= 0;
    }

    [__DynamicallyInvokable]
    void ICollection<T>.CopyTo(T[] array, int arrayIndex)
    {
      if (this._array == null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullArray"));
      System.Array.Copy((System.Array) this._array, this._offset, (System.Array) array, arrayIndex, this._count);
    }

    [__DynamicallyInvokable]
    bool ICollection<T>.Remove(T item)
    {
      throw new NotSupportedException();
    }

    [__DynamicallyInvokable]
    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
      if (this._array == null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullArray"));
      return (IEnumerator<T>) new ArraySegment<T>.ArraySegmentEnumerator(this);
    }

    [__DynamicallyInvokable]
    IEnumerator IEnumerable.GetEnumerator()
    {
      if (this._array == null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullArray"));
      return (IEnumerator) new ArraySegment<T>.ArraySegmentEnumerator(this);
    }

    [Serializable]
    private sealed class ArraySegmentEnumerator : IEnumerator<T>, IDisposable, IEnumerator
    {
      private T[] _array;
      private int _start;
      private int _end;
      private int _current;

      public T Current
      {
        get
        {
          if (this._current < this._start)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
          if (this._current >= this._end)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
          return this._array[this._current];
        }
      }

      object IEnumerator.Current
      {
        get
        {
          return (object) this.Current;
        }
      }

      internal ArraySegmentEnumerator(ArraySegment<T> arraySegment)
      {
        this._array = arraySegment._array;
        this._start = arraySegment._offset;
        this._end = this._start + arraySegment._count;
        this._current = this._start - 1;
      }

      public bool MoveNext()
      {
        if (this._current >= this._end)
          return false;
        this._current = this._current + 1;
        return this._current < this._end;
      }

      void IEnumerator.Reset()
      {
        this._current = this._start - 1;
      }

      public void Dispose()
      {
      }
    }
  }
}
