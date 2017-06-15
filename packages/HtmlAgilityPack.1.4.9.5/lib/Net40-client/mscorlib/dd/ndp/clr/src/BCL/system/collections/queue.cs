// Decompiled with JetBrains decompiler
// Type: System.Collections.Queue
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections
{
  /// <summary>表示对象的先进先出集合。</summary>
  /// <filterpriority>1</filterpriority>
  [DebuggerTypeProxy(typeof (Queue.QueueDebugView))]
  [DebuggerDisplay("Count = {Count}")]
  [ComVisible(true)]
  [Serializable]
  public class Queue : ICollection, IEnumerable, ICloneable
  {
    private object[] _array;
    private int _head;
    private int _tail;
    private int _size;
    private int _growFactor;
    private int _version;
    [NonSerialized]
    private object _syncRoot;
    private const int _MinimumGrow = 4;
    private const int _ShrinkThreshold = 32;

    /// <summary>获取 <see cref="T:System.Collections.Queue" /> 中包含的元素数。</summary>
    /// <returns>
    /// <see cref="T:System.Collections.Queue" /> 中包含的元素数。</returns>
    /// <filterpriority>2</filterpriority>
    public virtual int Count
    {
      get
      {
        return this._size;
      }
    }

    /// <summary>获取一个值，该值指示是否同步对 <see cref="T:System.Collections.Queue" /> 的访问（线程安全）。</summary>
    /// <returns>如果对 <see cref="T:System.Collections.Queue" /> 的访问是同步的（线程安全），则为 true；否则为 false。默认值为 false。</returns>
    /// <filterpriority>2</filterpriority>
    public virtual bool IsSynchronized
    {
      get
      {
        return false;
      }
    }

    /// <summary>获取可用于同步对 <see cref="T:System.Collections.Queue" /> 的访问的对象。</summary>
    /// <returns>可用于同步对 <see cref="T:System.Collections.Queue" /> 的访问的对象。</returns>
    /// <filterpriority>2</filterpriority>
    public virtual object SyncRoot
    {
      get
      {
        if (this._syncRoot == null)
          Interlocked.CompareExchange(ref this._syncRoot, new object(), (object) null);
        return this._syncRoot;
      }
    }

    /// <summary>初始化 <see cref="T:System.Collections.Queue" /> 类的新实例，该实例为空，具有默认的初始容量并使用默认的增长因子。</summary>
    public Queue()
      : this(32, 2f)
    {
    }

    /// <summary>初始化 <see cref="T:System.Collections.Queue" /> 类的新实例，该实例为空，具有指定的初始容量并使用默认的增长因子。</summary>
    /// <param name="capacity">
    /// <see cref="T:System.Collections.Queue" /> 可包含的初始元素数目。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="capacity" /> is less than zero. </exception>
    public Queue(int capacity)
      : this(capacity, 2f)
    {
    }

    /// <summary>初始化 <see cref="T:System.Collections.Queue" /> 类的新实例，该实例为空，具有指定的初始容量并使用指定的增长因子。</summary>
    /// <param name="capacity">
    /// <see cref="T:System.Collections.Queue" /> 可包含的初始元素数目。</param>
    /// <param name="growFactor">扩展 <see cref="T:System.Collections.Queue" /> 容量要使用的因子。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="capacity" /> is less than zero.-or- <paramref name="growFactor" /> is less than 1.0 or greater than 10.0. </exception>
    public Queue(int capacity, float growFactor)
    {
      if (capacity < 0)
        throw new ArgumentOutOfRangeException("capacity", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if ((double) growFactor < 1.0 || (double) growFactor > 10.0)
        throw new ArgumentOutOfRangeException("growFactor", Environment.GetResourceString("ArgumentOutOfRange_QueueGrowFactor", (object) 1, (object) 10));
      this._array = new object[capacity];
      this._head = 0;
      this._tail = 0;
      this._size = 0;
      this._growFactor = (int) ((double) growFactor * 100.0);
    }

    /// <summary>初始化 <see cref="T:System.Collections.Queue" /> 类的新实例，该实例包含从指定集合复制的元素，具有与复制的元素数相同的初始容量并使用默认的增长因子。</summary>
    /// <param name="col">要从中复制元素的 <see cref="T:System.Collections.ICollection" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="col" /> is null. </exception>
    public Queue(ICollection col)
      : this(col == null ? 32 : col.Count)
    {
      if (col == null)
        throw new ArgumentNullException("col");
      foreach (object obj in (IEnumerable) col)
        this.Enqueue(obj);
    }

    /// <summary>创建 <see cref="T:System.Collections.Queue" /> 的浅表副本。</summary>
    /// <returns>
    /// <see cref="T:System.Collections.Queue" /> 的浅表复制。</returns>
    /// <filterpriority>2</filterpriority>
    public virtual object Clone()
    {
      Queue queue = new Queue(this._size);
      queue._size = this._size;
      int num = this._size;
      int length1 = this._array.Length - this._head < num ? this._array.Length - this._head : num;
      Array.Copy((Array) this._array, this._head, (Array) queue._array, 0, length1);
      int length2 = num - length1;
      if (length2 > 0)
        Array.Copy((Array) this._array, 0, (Array) queue._array, this._array.Length - this._head, length2);
      queue._version = this._version;
      return (object) queue;
    }

    /// <summary>从 <see cref="T:System.Collections.Queue" /> 中移除所有对象。</summary>
    /// <filterpriority>2</filterpriority>
    public virtual void Clear()
    {
      if (this._head < this._tail)
      {
        Array.Clear((Array) this._array, this._head, this._size);
      }
      else
      {
        Array.Clear((Array) this._array, this._head, this._array.Length - this._head);
        Array.Clear((Array) this._array, 0, this._tail);
      }
      this._head = 0;
      this._tail = 0;
      this._size = 0;
      this._version = this._version + 1;
    }

    /// <summary>从指定数组索引开始将 <see cref="T:System.Collections.Queue" /> 元素复制到现有一维 <see cref="T:System.Array" /> 中。</summary>
    /// <param name="array">一维 <see cref="T:System.Array" />，它是从 <see cref="T:System.Collections.Queue" /> 复制的元素的目标。<see cref="T:System.Array" /> 必须具有从零开始的索引。</param>
    /// <param name="index">
    /// <paramref name="array" /> 中从零开始的索引，从此索引处开始进行复制。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> is null. </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> is less than zero. </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="array" /> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.Queue" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />. </exception>
    /// <exception cref="T:System.ArrayTypeMismatchException">The type of the source <see cref="T:System.Collections.Queue" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
    /// <filterpriority>2</filterpriority>
    public virtual void CopyTo(Array array, int index)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      if (array.Rank != 1)
        throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
      if (index < 0)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (array.Length - index < this._size)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      int num = this._size;
      if (num == 0)
        return;
      int length1 = this._array.Length - this._head < num ? this._array.Length - this._head : num;
      Array.Copy((Array) this._array, this._head, array, index, length1);
      int length2 = num - length1;
      if (length2 <= 0)
        return;
      Array.Copy((Array) this._array, 0, array, index + this._array.Length - this._head, length2);
    }

    /// <summary>将对象添加到 <see cref="T:System.Collections.Queue" /> 的结尾处。</summary>
    /// <param name="obj">要添加到 <see cref="T:System.Collections.Queue" /> 的对象。该值可以为 null。</param>
    /// <filterpriority>2</filterpriority>
    public virtual void Enqueue(object obj)
    {
      if (this._size == this._array.Length)
      {
        int capacity = (int) ((long) this._array.Length * (long) this._growFactor / 100L);
        if (capacity < this._array.Length + 4)
          capacity = this._array.Length + 4;
        this.SetCapacity(capacity);
      }
      this._array[this._tail] = obj;
      this._tail = (this._tail + 1) % this._array.Length;
      this._size = this._size + 1;
      this._version = this._version + 1;
    }

    /// <summary>返回循环访问 <see cref="T:System.Collections.Queue" /> 的枚举数。</summary>
    /// <returns>用于 <see cref="T:System.Collections.Queue" /> 的 <see cref="T:System.Collections.IEnumerator" />。</returns>
    /// <filterpriority>2</filterpriority>
    public virtual IEnumerator GetEnumerator()
    {
      return (IEnumerator) new Queue.QueueEnumerator(this);
    }

    /// <summary>移除并返回位于 <see cref="T:System.Collections.Queue" /> 开始处的对象。</summary>
    /// <returns>从 <see cref="T:System.Collections.Queue" /> 的开头移除的对象。</returns>
    /// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.Queue" /> is empty. </exception>
    /// <filterpriority>2</filterpriority>
    public virtual object Dequeue()
    {
      if (this.Count == 0)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EmptyQueue"));
      object obj = this._array[this._head];
      this._array[this._head] = (object) null;
      this._head = (this._head + 1) % this._array.Length;
      this._size = this._size - 1;
      this._version = this._version + 1;
      return obj;
    }

    /// <summary>返回位于 <see cref="T:System.Collections.Queue" /> 开始处的对象但不将其移除。</summary>
    /// <returns>位于 <see cref="T:System.Collections.Queue" /> 的开头的对象。</returns>
    /// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.Queue" /> is empty. </exception>
    /// <filterpriority>2</filterpriority>
    public virtual object Peek()
    {
      if (this.Count == 0)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EmptyQueue"));
      return this._array[this._head];
    }

    /// <summary>返回一个新的 <see cref="T:System.Collections.Queue" />，它将包装原始队列，并且是线程安全的。</summary>
    /// <returns>一个同步（线程安全）的 <see cref="T:System.Collections.Queue" /> 包装。</returns>
    /// <param name="queue">要同步的 <see cref="T:System.Collections.Queue" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="queue" /> is null. </exception>
    /// <filterpriority>2</filterpriority>
    [HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
    public static Queue Synchronized(Queue queue)
    {
      if (queue == null)
        throw new ArgumentNullException("queue");
      return (Queue) new Queue.SynchronizedQueue(queue);
    }

    /// <summary>确定某元素是否在 <see cref="T:System.Collections.Queue" /> 中。</summary>
    /// <returns>如果在 <see cref="T:System.Collections.Queue" /> 中找到了 <paramref name="obj" />，则为 true；否则为 false。</returns>
    /// <param name="obj">要在 <see cref="T:System.Collections.Queue" /> 中定位的 <see cref="T:System.Object" />。该值可以为 null。</param>
    /// <filterpriority>2</filterpriority>
    public virtual bool Contains(object obj)
    {
      int index = this._head;
      int num = this._size;
      while (num-- > 0)
      {
        if (obj == null)
        {
          if (this._array[index] == null)
            return true;
        }
        else if (this._array[index] != null && this._array[index].Equals(obj))
          return true;
        index = (index + 1) % this._array.Length;
      }
      return false;
    }

    internal object GetElement(int i)
    {
      return this._array[(this._head + i) % this._array.Length];
    }

    /// <summary>将 <see cref="T:System.Collections.Queue" /> 元素复制到新数组。</summary>
    /// <returns>包含从 <see cref="T:System.Collections.Queue" /> 复制的元素的新数组。</returns>
    /// <filterpriority>2</filterpriority>
    public virtual object[] ToArray()
    {
      object[] objArray = new object[this._size];
      if (this._size == 0)
        return objArray;
      if (this._head < this._tail)
      {
        Array.Copy((Array) this._array, this._head, (Array) objArray, 0, this._size);
      }
      else
      {
        Array.Copy((Array) this._array, this._head, (Array) objArray, 0, this._array.Length - this._head);
        Array.Copy((Array) this._array, 0, (Array) objArray, this._array.Length - this._head, this._tail);
      }
      return objArray;
    }

    private void SetCapacity(int capacity)
    {
      object[] objArray = new object[capacity];
      if (this._size > 0)
      {
        if (this._head < this._tail)
        {
          Array.Copy((Array) this._array, this._head, (Array) objArray, 0, this._size);
        }
        else
        {
          Array.Copy((Array) this._array, this._head, (Array) objArray, 0, this._array.Length - this._head);
          Array.Copy((Array) this._array, 0, (Array) objArray, this._array.Length - this._head, this._tail);
        }
      }
      this._array = objArray;
      this._head = 0;
      this._tail = this._size == capacity ? 0 : this._size;
      this._version = this._version + 1;
    }

    /// <summary>将容量设置为 <see cref="T:System.Collections.Queue" /> 中元素的实际数目。</summary>
    /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Queue" /> is read-only.</exception>
    /// <filterpriority>2</filterpriority>
    public virtual void TrimToSize()
    {
      this.SetCapacity(this._size);
    }

    [Serializable]
    private class SynchronizedQueue : Queue
    {
      private Queue _q;
      private object root;

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
          return this.root;
        }
      }

      public override int Count
      {
        get
        {
          lock (this.root)
            return this._q.Count;
        }
      }

      internal SynchronizedQueue(Queue q)
      {
        this._q = q;
        this.root = this._q.SyncRoot;
      }

      public override void Clear()
      {
        lock (this.root)
          this._q.Clear();
      }

      public override object Clone()
      {
        lock (this.root)
          return (object) new Queue.SynchronizedQueue((Queue) this._q.Clone());
      }

      public override bool Contains(object obj)
      {
        lock (this.root)
          return this._q.Contains(obj);
      }

      public override void CopyTo(Array array, int arrayIndex)
      {
        lock (this.root)
          this._q.CopyTo(array, arrayIndex);
      }

      public override void Enqueue(object value)
      {
        lock (this.root)
          this._q.Enqueue(value);
      }

      public override object Dequeue()
      {
        lock (this.root)
          return this._q.Dequeue();
      }

      public override IEnumerator GetEnumerator()
      {
        lock (this.root)
          return this._q.GetEnumerator();
      }

      public override object Peek()
      {
        lock (this.root)
          return this._q.Peek();
      }

      public override object[] ToArray()
      {
        lock (this.root)
          return this._q.ToArray();
      }

      public override void TrimToSize()
      {
        lock (this.root)
          this._q.TrimToSize();
      }
    }

    [Serializable]
    private class QueueEnumerator : IEnumerator, ICloneable
    {
      private Queue _q;
      private int _index;
      private int _version;
      private object currentElement;

      public virtual object Current
      {
        get
        {
          if (this.currentElement != this._q._array)
            return this.currentElement;
          if (this._index == 0)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
        }
      }

      internal QueueEnumerator(Queue q)
      {
        this._q = q;
        this._version = this._q._version;
        this._index = 0;
        this.currentElement = (object) this._q._array;
        if (this._q._size != 0)
          return;
        this._index = -1;
      }

      public object Clone()
      {
        return this.MemberwiseClone();
      }

      public virtual bool MoveNext()
      {
        if (this._version != this._q._version)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
        if (this._index < 0)
        {
          this.currentElement = (object) this._q._array;
          return false;
        }
        this.currentElement = this._q.GetElement(this._index);
        this._index = this._index + 1;
        if (this._index == this._q._size)
          this._index = -1;
        return true;
      }

      public virtual void Reset()
      {
        if (this._version != this._q._version)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
        this._index = this._q._size != 0 ? 0 : -1;
        this.currentElement = (object) this._q._array;
      }
    }

    internal class QueueDebugView
    {
      private Queue queue;

      [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
      public object[] Items
      {
        get
        {
          return this.queue.ToArray();
        }
      }

      public QueueDebugView(Queue queue)
      {
        if (queue == null)
          throw new ArgumentNullException("queue");
        this.queue = queue;
      }
    }
  }
}
