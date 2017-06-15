// Decompiled with JetBrains decompiler
// Type: System.Collections.Concurrent.ConcurrentQueue`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections.Concurrent
{
  /// <summary>表示线程安全的先进先出 (FIFO) 集合。</summary>
  /// <typeparam name="T">队列中包含的元素的类型。</typeparam>
  [ComVisible(false)]
  [DebuggerDisplay("Count = {Count}")]
  [DebuggerTypeProxy(typeof (SystemCollectionsConcurrent_ProducerConsumerCollectionDebugView<>))]
  [__DynamicallyInvokable]
  [Serializable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public class ConcurrentQueue<T> : IProducerConsumerCollection<T>, IEnumerable<T>, IEnumerable, ICollection, IReadOnlyCollection<T>
  {
    [NonSerialized]
    private volatile ConcurrentQueue<T>.Segment m_head;
    [NonSerialized]
    private volatile ConcurrentQueue<T>.Segment m_tail;
    private T[] m_serializationArray;
    private const int SEGMENT_SIZE = 32;
    [NonSerialized]
    internal volatile int m_numSnapshotTakers;

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

    /// <summary>获取一个值，该值指示 <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" /> 是否为空。</summary>
    /// <returns>如果 <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" /> 为空，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool IsEmpty
    {
      [__DynamicallyInvokable] get
      {
        ConcurrentQueue<T>.Segment segment = this.m_head;
        if (!segment.IsEmpty)
          return false;
        if (segment.Next == null)
          return true;
        SpinWait spinWait = new SpinWait();
        for (; segment.IsEmpty; segment = this.m_head)
        {
          if (segment.Next == null)
            return true;
          spinWait.SpinOnce();
        }
        return false;
      }
    }

    /// <summary>获取 <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" /> 中包含的元素数。</summary>
    /// <returns>
    /// <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" /> 中包含的元素数。</returns>
    [__DynamicallyInvokable]
    public int Count
    {
      [__DynamicallyInvokable] get
      {
        ConcurrentQueue<T>.Segment head;
        ConcurrentQueue<T>.Segment tail;
        int headLow;
        int tailHigh;
        this.GetHeadTailPositions(out head, out tail, out headLow, out tailHigh);
        if (head == tail)
          return tailHigh - headLow + 1;
        return 32 - headLow + 32 * (int) (tail.m_index - head.m_index - 1L) + (tailHigh + 1);
      }
    }

    /// <summary>初始化 <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public ConcurrentQueue()
    {
      this.m_head = this.m_tail = new ConcurrentQueue<T>.Segment(0L, this);
    }

    /// <summary>初始化 <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" /> 类的新实例，该类包含从指定集合中复制的元素</summary>
    /// <param name="collection">其元素被复制到新的 <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" /> 中的集合。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="collection" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    public ConcurrentQueue(IEnumerable<T> collection)
    {
      if (collection == null)
        throw new ArgumentNullException("collection");
      this.InitializeFromCollection(collection);
    }

    private void InitializeFromCollection(IEnumerable<T> collection)
    {
      ConcurrentQueue<T>.Segment segment = new ConcurrentQueue<T>.Segment(0L, this);
      this.m_head = segment;
      int num = 0;
      foreach (T obj in collection)
      {
        segment.UnsafeAdd(obj);
        ++num;
        if (num >= 32)
        {
          segment = segment.UnsafeGrow();
          num = 0;
        }
      }
      this.m_tail = segment;
    }

    [OnSerializing]
    private void OnSerializing(StreamingContext context)
    {
      this.m_serializationArray = this.ToArray();
    }

    [OnDeserialized]
    private void OnDeserialized(StreamingContext context)
    {
      this.InitializeFromCollection((IEnumerable<T>) this.m_serializationArray);
      this.m_serializationArray = (T[]) null;
    }

    [__DynamicallyInvokable]
    void ICollection.CopyTo(Array array, int index)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      ((ICollection) this.ToList()).CopyTo(array, index);
    }

    [__DynamicallyInvokable]
    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    [__DynamicallyInvokable]
    bool IProducerConsumerCollection<T>.TryAdd(T item)
    {
      this.Enqueue(item);
      return true;
    }

    [__DynamicallyInvokable]
    bool IProducerConsumerCollection<T>.TryTake(out T item)
    {
      return this.TryDequeue(out item);
    }

    /// <summary>将 <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" /> 中存储的元素复制到新数组中。</summary>
    /// <returns>新数组包含从 <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" /> 复制的元素的快照。</returns>
    [__DynamicallyInvokable]
    public T[] ToArray()
    {
      return this.ToList().ToArray();
    }

    private List<T> ToList()
    {
      Interlocked.Increment(ref this.m_numSnapshotTakers);
      List<T> list = new List<T>();
      try
      {
        ConcurrentQueue<T>.Segment head;
        ConcurrentQueue<T>.Segment tail;
        int headLow;
        int tailHigh;
        this.GetHeadTailPositions(out head, out tail, out headLow, out tailHigh);
        if (head == tail)
        {
          head.AddToList(list, headLow, tailHigh);
        }
        else
        {
          head.AddToList(list, headLow, 31);
          for (ConcurrentQueue<T>.Segment next = head.Next; next != tail; next = next.Next)
            next.AddToList(list, 0, 31);
          tail.AddToList(list, 0, tailHigh);
        }
      }
      finally
      {
        Interlocked.Decrement(ref this.m_numSnapshotTakers);
      }
      return list;
    }

    private void GetHeadTailPositions(out ConcurrentQueue<T>.Segment head, out ConcurrentQueue<T>.Segment tail, out int headLow, out int tailHigh)
    {
      head = this.m_head;
      tail = this.m_tail;
      headLow = head.Low;
      tailHigh = tail.High;
      SpinWait spinWait = new SpinWait();
      while (head != this.m_head || tail != this.m_tail || (headLow != head.Low || tailHigh != tail.High) || head.m_index > tail.m_index)
      {
        spinWait.SpinOnce();
        head = this.m_head;
        tail = this.m_tail;
        headLow = head.Low;
        tailHigh = tail.High;
      }
    }

    /// <summary>从指定数组索引开始将 <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" /> 元素复制到现有一维 <see cref="T:System.Array" /> 中。</summary>
    /// <param name="array">一维 <see cref="T:System.Array" />，用作从 <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" /> 所复制的元素的目标数组。<see cref="T:System.Array" /> 必须具有从零开始的索引。</param>
    /// <param name="index">
    /// <paramref name="array" /> 中从零开始的索引，从此处开始复制。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null 引用（在 Visual Basic 中为 Nothing）。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于零。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="index" /> 等于或大于的长度 <paramref name="array" /> -在源中的元素数目 <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" /> 大于从可用空间 <paramref name="index" /> 到目标的末尾 <paramref name="array" />。</exception>
    [__DynamicallyInvokable]
    public void CopyTo(T[] array, int index)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      this.ToList().CopyTo(array, index);
    }

    /// <summary>返回循环访问 <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" /> 的枚举数。</summary>
    /// <returns>
    /// <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" /> 的内容的枚举器。</returns>
    [__DynamicallyInvokable]
    public IEnumerator<T> GetEnumerator()
    {
      Interlocked.Increment(ref this.m_numSnapshotTakers);
      ConcurrentQueue<T>.Segment head;
      ConcurrentQueue<T>.Segment tail;
      int headLow;
      int tailHigh;
      this.GetHeadTailPositions(out head, out tail, out headLow, out tailHigh);
      return this.GetEnumerator(head, tail, headLow, tailHigh);
    }

    private IEnumerator<T> GetEnumerator(ConcurrentQueue<T>.Segment head, ConcurrentQueue<T>.Segment tail, int headLow, int tailHigh)
    {
      try
      {
        SpinWait spin = new SpinWait();
        if (head == tail)
        {
          for (int i = headLow; i <= tailHigh; ++i)
          {
            spin.Reset();
            while (!head.m_state[i].m_value)
              spin.SpinOnce();
            yield return head.m_array[i];
          }
        }
        else
        {
          for (int i = headLow; i < 32; ++i)
          {
            spin.Reset();
            while (!head.m_state[i].m_value)
              spin.SpinOnce();
            yield return head.m_array[i];
          }
          ConcurrentQueue<T>.Segment curr;
          for (curr = head.Next; curr != tail; curr = curr.Next)
          {
            for (int i = 0; i < 32; ++i)
            {
              spin.Reset();
              while (!curr.m_state[i].m_value)
                spin.SpinOnce();
              yield return curr.m_array[i];
            }
          }
          for (int i = 0; i <= tailHigh; ++i)
          {
            spin.Reset();
            while (!tail.m_state[i].m_value)
              spin.SpinOnce();
            yield return tail.m_array[i];
          }
          curr = (ConcurrentQueue<T>.Segment) null;
        }
      }
      finally
      {
        Interlocked.Decrement(ref this.m_numSnapshotTakers);
      }
    }

    /// <summary>将对象添加到 <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" /> 的结尾处。</summary>
    /// <param name="item">要添加到 <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" /> 末尾的对象。该值对于引用类型可以是空引用（在 Visual Basic 中为 Nothing）。</param>
    [__DynamicallyInvokable]
    public void Enqueue(T item)
    {
      SpinWait spinWait = new SpinWait();
      while (!this.m_tail.TryAppend(item))
        spinWait.SpinOnce();
    }

    /// <summary>尝试移除并返回并发队列开头处的对象。</summary>
    /// <returns>如果成功在 <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" /> 开头处移除并返回了元素，则为 true；否则为 false。</returns>
    /// <param name="result">如果操作成功，则此方法返回时，<paramref name="result" /> 包含所移除的对象。如果没有可供移除的对象，则不指定该值。</param>
    [__DynamicallyInvokable]
    public bool TryDequeue(out T result)
    {
      while (!this.IsEmpty)
      {
        if (this.m_head.TryRemove(out result))
          return true;
      }
      result = default (T);
      return false;
    }

    /// <summary>尝试返回 <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" /> 开头处的对象但不将其移除。</summary>
    /// <returns>如果成功返回了对象，则为 true；否则为 false。</returns>
    /// <param name="result">此方法返回时，<paramref name="result" /> 包含 <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" /> 开头处的对象；如果操作失败，则包含未指定的值。</param>
    [__DynamicallyInvokable]
    public bool TryPeek(out T result)
    {
      Interlocked.Increment(ref this.m_numSnapshotTakers);
      while (!this.IsEmpty)
      {
        if (this.m_head.TryPeek(out result))
        {
          Interlocked.Decrement(ref this.m_numSnapshotTakers);
          return true;
        }
      }
      result = default (T);
      Interlocked.Decrement(ref this.m_numSnapshotTakers);
      return false;
    }

    private class Segment
    {
      internal volatile T[] m_array;
      internal volatile VolatileBool[] m_state;
      private volatile ConcurrentQueue<T>.Segment m_next;
      internal readonly long m_index;
      private volatile int m_low;
      private volatile int m_high;
      private volatile ConcurrentQueue<T> m_source;

      internal ConcurrentQueue<T>.Segment Next
      {
        get
        {
          return this.m_next;
        }
      }

      internal bool IsEmpty
      {
        get
        {
          return this.Low > this.High;
        }
      }

      internal int Low
      {
        get
        {
          return Math.Min(this.m_low, 32);
        }
      }

      internal int High
      {
        get
        {
          return Math.Min(this.m_high, 31);
        }
      }

      internal Segment(long index, ConcurrentQueue<T> source)
      {
        this.m_array = new T[32];
        this.m_state = new VolatileBool[32];
        this.m_high = -1;
        this.m_index = index;
        this.m_source = source;
      }

      internal void UnsafeAdd(T value)
      {
        this.m_high = this.m_high + 1;
        this.m_array[this.m_high] = value;
        this.m_state[this.m_high].m_value = true;
      }

      internal ConcurrentQueue<T>.Segment UnsafeGrow()
      {
        ConcurrentQueue<T>.Segment segment = new ConcurrentQueue<T>.Segment(this.m_index + 1L, this.m_source);
        this.m_next = segment;
        return segment;
      }

      internal void Grow()
      {
        this.m_next = new ConcurrentQueue<T>.Segment(this.m_index + 1L, this.m_source);
        this.m_source.m_tail = this.m_next;
      }

      internal bool TryAppend(T value)
      {
        if (this.m_high >= 31)
          return false;
        int index = 32;
        try
        {
        }
        finally
        {
          index = Interlocked.Increment(ref this.m_high);
          if (index <= 31)
          {
            this.m_array[index] = value;
            this.m_state[index].m_value = true;
          }
          if (index == 31)
            this.Grow();
        }
        return index <= 31;
      }

      internal bool TryRemove(out T result)
      {
        SpinWait spinWait1 = new SpinWait();
        int low = this.Low;
        for (int high = this.High; low <= high; high = this.High)
        {
          if (Interlocked.CompareExchange(ref this.m_low, low + 1, low) == low)
          {
            SpinWait spinWait2 = new SpinWait();
            while (!this.m_state[low].m_value)
              spinWait2.SpinOnce();
            result = this.m_array[low];
            if (this.m_source.m_numSnapshotTakers <= 0)
              this.m_array[low] = default (T);
            if (low + 1 >= 32)
            {
              SpinWait spinWait3 = new SpinWait();
              while (this.m_next == null)
                spinWait3.SpinOnce();
              this.m_source.m_head = this.m_next;
            }
            return true;
          }
          spinWait1.SpinOnce();
          low = this.Low;
        }
        result = default (T);
        return false;
      }

      internal bool TryPeek(out T result)
      {
        result = default (T);
        int low = this.Low;
        if (low > this.High)
          return false;
        SpinWait spinWait = new SpinWait();
        while (!this.m_state[low].m_value)
          spinWait.SpinOnce();
        result = this.m_array[low];
        return true;
      }

      internal void AddToList(List<T> list, int start, int end)
      {
        for (int index = start; index <= end; ++index)
        {
          SpinWait spinWait = new SpinWait();
          while (!this.m_state[index].m_value)
            spinWait.SpinOnce();
          list.Add(this.m_array[index]);
        }
      }
    }
  }
}
