// Decompiled with JetBrains decompiler
// Type: System.Collections.Stack
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections
{
  /// <summary>表示对象的简单后进先出 (LIFO) 非泛型集合。</summary>
  /// <filterpriority>1</filterpriority>
  [DebuggerTypeProxy(typeof (Stack.StackDebugView))]
  [DebuggerDisplay("Count = {Count}")]
  [ComVisible(true)]
  [Serializable]
  public class Stack : ICollection, IEnumerable, ICloneable
  {
    private object[] _array;
    private int _size;
    private int _version;
    [NonSerialized]
    private object _syncRoot;
    private const int _defaultCapacity = 10;

    /// <summary>获取 <see cref="T:System.Collections.Stack" /> 中包含的元素数。</summary>
    /// <returns>
    /// <see cref="T:System.Collections.Stack" /> 中包含的元素个数。</returns>
    /// <filterpriority>2</filterpriority>
    public virtual int Count
    {
      get
      {
        return this._size;
      }
    }

    /// <summary>获取一个值，该值指示是否同步对 <see cref="T:System.Collections.Stack" /> 的访问（线程安全）。</summary>
    /// <returns>如果对 <see cref="T:System.Collections.Stack" /> 的访问是同步的（线程安全），则为 true；否则为 false。默认值为 false。</returns>
    /// <filterpriority>2</filterpriority>
    public virtual bool IsSynchronized
    {
      get
      {
        return false;
      }
    }

    /// <summary>获取可用于同步对 <see cref="T:System.Collections.Stack" /> 的访问的对象。</summary>
    /// <returns>可用于同步对 <see cref="T:System.Collections.Stack" /> 的访问的 <see cref="T:System.Object" />。</returns>
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

    /// <summary>初始化 <see cref="T:System.Collections.Stack" /> 类的新实例，该实例为空并且具有默认初始容量。</summary>
    public Stack()
    {
      this._array = new object[10];
      this._size = 0;
      this._version = 0;
    }

    /// <summary>初始化 <see cref="T:System.Collections.Stack" /> 类的新实例，该实例为空并且具有指定的初始容量或默认初始容量（这两个容量中的较大者）。</summary>
    /// <param name="initialCapacity">
    /// <see cref="T:System.Collections.Stack" /> 可包含的初始元素数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="initialCapacity" /> 小于零。</exception>
    public Stack(int initialCapacity)
    {
      if (initialCapacity < 0)
        throw new ArgumentOutOfRangeException("initialCapacity", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (initialCapacity < 10)
        initialCapacity = 10;
      this._array = new object[initialCapacity];
      this._size = 0;
      this._version = 0;
    }

    /// <summary>初始化 <see cref="T:System.Collections.Stack" /> 类的新实例，该实例包含从指定集合复制的元素并且具有与所复制的元素数相同的初始容量。</summary>
    /// <param name="col">
    /// <see cref="T:System.Collections.ICollection" />，从其中复制元素。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="col" /> 为 null。</exception>
    public Stack(ICollection col)
      : this(col == null ? 32 : col.Count)
    {
      if (col == null)
        throw new ArgumentNullException("col");
      foreach (object obj in (IEnumerable) col)
        this.Push(obj);
    }

    /// <summary>从 <see cref="T:System.Collections.Stack" /> 中移除所有对象。</summary>
    /// <filterpriority>2</filterpriority>
    public virtual void Clear()
    {
      Array.Clear((Array) this._array, 0, this._size);
      this._size = 0;
      this._version = this._version + 1;
    }

    /// <summary>创建 <see cref="T:System.Collections.Stack" /> 的浅表副本。</summary>
    /// <returns>
    /// <see cref="T:System.Collections.Stack" /> 的浅表副本。</returns>
    /// <filterpriority>2</filterpriority>
    public virtual object Clone()
    {
      Stack stack = new Stack(this._size);
      stack._size = this._size;
      Array.Copy((Array) this._array, 0, (Array) stack._array, 0, this._size);
      stack._version = this._version;
      return (object) stack;
    }

    /// <summary>确定某元素是否在 <see cref="T:System.Collections.Stack" /> 中。</summary>
    /// <returns>如果在 <see cref="T:System.Collections.Stack" /> 中找到 <paramref name="obj" />，则为 true；否则为 false。</returns>
    /// <param name="obj">要在 <see cref="T:System.Collections.Stack" /> 中查找的 <see cref="T:System.Object" />。该值可以为 null。</param>
    /// <filterpriority>2</filterpriority>
    public virtual bool Contains(object obj)
    {
      int index = this._size;
      while (index-- > 0)
      {
        if (obj == null)
        {
          if (this._array[index] == null)
            return true;
        }
        else if (this._array[index] != null && this._array[index].Equals(obj))
          return true;
      }
      return false;
    }

    /// <summary>从指定数组索引开始将 <see cref="T:System.Collections.Stack" /> 复制到现有一维 <see cref="T:System.Array" /> 中。</summary>
    /// <param name="array">作为从 <see cref="T:System.Collections.Stack" /> 复制的元素的目标的一维 <see cref="T:System.Array" />。<see cref="T:System.Array" /> 必须具有从零开始的索引。</param>
    /// <param name="index">
    /// <paramref name="array" /> 中从零开始的索引，从此索引处开始进行复制。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于零。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="array" /> 是多维的。- 或 -源 <see cref="T:System.Collections.Stack" /> 中的元素数目大于从 <paramref name="index" /> 到目标 <paramref name="array" /> 末尾之间的可用空间。</exception>
    /// <exception cref="T:System.InvalidCastException">源 <see cref="T:System.Collections.Stack" /> 的类型无法自动转换为目标 <paramref name="array" /> 的类型。</exception>
    /// <filterpriority>2</filterpriority>
    public virtual void CopyTo(Array array, int index)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      if (array.Rank != 1)
        throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
      if (index < 0)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (array.Length - index < this._size)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      int num = 0;
      if (array is object[])
      {
        object[] objArray = (object[]) array;
        for (; num < this._size; ++num)
          objArray[num + index] = this._array[this._size - num - 1];
      }
      else
      {
        for (; num < this._size; ++num)
          array.SetValue(this._array[this._size - num - 1], num + index);
      }
    }

    /// <summary>返回 <see cref="T:System.Collections.Stack" /> 的 <see cref="T:System.Collections.IEnumerator" />。</summary>
    /// <returns>用于 <see cref="T:System.Collections.Stack" /> 的 <see cref="T:System.Collections.IEnumerator" />。</returns>
    /// <filterpriority>2</filterpriority>
    public virtual IEnumerator GetEnumerator()
    {
      return (IEnumerator) new Stack.StackEnumerator(this);
    }

    /// <summary>返回位于 <see cref="T:System.Collections.Stack" /> 顶部的对象但不将其移除。</summary>
    /// <returns>位于 <see cref="T:System.Collections.Stack" /> 顶部的 <see cref="T:System.Object" />。</returns>
    /// <exception cref="T:System.InvalidOperationException">
    /// <see cref="T:System.Collections.Stack" /> 为空。</exception>
    /// <filterpriority>2</filterpriority>
    public virtual object Peek()
    {
      if (this._size == 0)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EmptyStack"));
      return this._array[this._size - 1];
    }

    /// <summary>移除并返回位于 <see cref="T:System.Collections.Stack" /> 顶部的对象。</summary>
    /// <returns>从 <see cref="T:System.Collections.Stack" /> 的顶部移除的 <see cref="T:System.Object" />。</returns>
    /// <exception cref="T:System.InvalidOperationException">
    /// <see cref="T:System.Collections.Stack" /> 为空。</exception>
    /// <filterpriority>2</filterpriority>
    public virtual object Pop()
    {
      if (this._size == 0)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EmptyStack"));
      this._version = this._version + 1;
      object[] objArray = this._array;
      int num = this._size - 1;
      this._size = num;
      int index = num;
      object obj = objArray[index];
      this._array[this._size] = (object) null;
      return obj;
    }

    /// <summary>将对象插入 <see cref="T:System.Collections.Stack" /> 的顶部。</summary>
    /// <param name="obj">要推入到 <see cref="T:System.Collections.Stack" /> 中的 <see cref="T:System.Object" />。该值可以为 null。</param>
    /// <filterpriority>2</filterpriority>
    public virtual void Push(object obj)
    {
      if (this._size == this._array.Length)
      {
        object[] objArray = new object[2 * this._array.Length];
        Array.Copy((Array) this._array, 0, (Array) objArray, 0, this._size);
        this._array = objArray;
      }
      object[] objArray1 = this._array;
      int num = this._size;
      this._size = num + 1;
      int index = num;
      object obj1 = obj;
      objArray1[index] = obj1;
      this._version = this._version + 1;
    }

    /// <summary>返回 <see cref="T:System.Collections.Stack" /> 的同步（线程安全）包装。</summary>
    /// <returns>
    /// <see cref="T:System.Collections.Stack" /> 周围的同步包装。</returns>
    /// <param name="stack">要同步的 <see cref="T:System.Collections.Stack" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="stack" /> 为 null。</exception>
    /// <filterpriority>2</filterpriority>
    [HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
    public static Stack Synchronized(Stack stack)
    {
      if (stack == null)
        throw new ArgumentNullException("stack");
      return (Stack) new Stack.SyncStack(stack);
    }

    /// <summary>将 <see cref="T:System.Collections.Stack" /> 复制到新数组中。</summary>
    /// <returns>新数组，包含 <see cref="T:System.Collections.Stack" /> 的元素的副本。</returns>
    /// <filterpriority>2</filterpriority>
    public virtual object[] ToArray()
    {
      object[] objArray = new object[this._size];
      for (int index = 0; index < this._size; ++index)
        objArray[index] = this._array[this._size - index - 1];
      return objArray;
    }

    [Serializable]
    private class SyncStack : Stack
    {
      private Stack _s;
      private object _root;

      public override bool IsSynchronized
      {
        get
        {
          return true;
        }
      }

      public override object SyncRoot
      {
        get
        {
          return this._root;
        }
      }

      public override int Count
      {
        get
        {
          lock (this._root)
            return this._s.Count;
        }
      }

      internal SyncStack(Stack stack)
      {
        this._s = stack;
        this._root = stack.SyncRoot;
      }

      public override bool Contains(object obj)
      {
        lock (this._root)
          return this._s.Contains(obj);
      }

      public override object Clone()
      {
        lock (this._root)
          return (object) new Stack.SyncStack((Stack) this._s.Clone());
      }

      public override void Clear()
      {
        lock (this._root)
          this._s.Clear();
      }

      public override void CopyTo(Array array, int arrayIndex)
      {
        lock (this._root)
          this._s.CopyTo(array, arrayIndex);
      }

      public override void Push(object value)
      {
        lock (this._root)
          this._s.Push(value);
      }

      public override object Pop()
      {
        lock (this._root)
          return this._s.Pop();
      }

      public override IEnumerator GetEnumerator()
      {
        lock (this._root)
          return this._s.GetEnumerator();
      }

      public override object Peek()
      {
        lock (this._root)
          return this._s.Peek();
      }

      public override object[] ToArray()
      {
        lock (this._root)
          return this._s.ToArray();
      }
    }

    [Serializable]
    private class StackEnumerator : IEnumerator, ICloneable
    {
      private Stack _stack;
      private int _index;
      private int _version;
      private object currentElement;

      public virtual object Current
      {
        get
        {
          if (this._index == -2)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
          if (this._index == -1)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
          return this.currentElement;
        }
      }

      internal StackEnumerator(Stack stack)
      {
        this._stack = stack;
        this._version = this._stack._version;
        this._index = -2;
        this.currentElement = (object) null;
      }

      public object Clone()
      {
        return this.MemberwiseClone();
      }

      public virtual bool MoveNext()
      {
        if (this._version != this._stack._version)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
        if (this._index == -2)
        {
          this._index = this._stack._size - 1;
          int num = this._index >= 0 ? 1 : 0;
          if (num == 0)
            return num != 0;
          this.currentElement = this._stack._array[this._index];
          return num != 0;
        }
        if (this._index == -1)
          return false;
        int num1 = this._index - 1;
        this._index = num1;
        int num2 = num1 >= 0 ? 1 : 0;
        if (num2 != 0)
        {
          this.currentElement = this._stack._array[this._index];
          return num2 != 0;
        }
        this.currentElement = (object) null;
        return num2 != 0;
      }

      public virtual void Reset()
      {
        if (this._version != this._stack._version)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
        this._index = -2;
        this.currentElement = (object) null;
      }
    }

    internal class StackDebugView
    {
      private Stack stack;

      [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
      public object[] Items
      {
        get
        {
          return this.stack.ToArray();
        }
      }

      public StackDebugView(Stack stack)
      {
        if (stack == null)
          throw new ArgumentNullException("stack");
        this.stack = stack;
      }
    }
  }
}
