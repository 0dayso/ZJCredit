// Decompiled with JetBrains decompiler
// Type: System.Collections.Concurrent.ConcurrentStack`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections.Concurrent
{
  /// <summary>表示线程安全的后进先出 (LIFO) 集合。</summary>
  /// <typeparam name="T">堆栈中包含的元素的类型。</typeparam>
  [DebuggerDisplay("Count = {Count}")]
  [DebuggerTypeProxy(typeof (SystemCollectionsConcurrent_ProducerConsumerCollectionDebugView<>))]
  [__DynamicallyInvokable]
  [Serializable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public class ConcurrentStack<T> : IProducerConsumerCollection<T>, IEnumerable<T>, IEnumerable, ICollection, IReadOnlyCollection<T>
  {
    [NonSerialized]
    private volatile ConcurrentStack<T>.Node m_head;
    private T[] m_serializationArray;
    private const int BACKOFF_MAX_YIELDS = 8;

    /// <summary>获取一个值，该值指示 <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> 是否为空。</summary>
    /// <returns>如果 <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> 为空，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool IsEmpty
    {
      [__DynamicallyInvokable] get
      {
        return this.m_head == null;
      }
    }

    /// <summary>获取 <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> 中包含的元素数。</summary>
    /// <returns>
    /// <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> 中包含的元素数。</returns>
    [__DynamicallyInvokable]
    public int Count
    {
      [__DynamicallyInvokable] get
      {
        int num = 0;
        for (ConcurrentStack<T>.Node node = this.m_head; node != null; node = node.m_next)
          ++num;
        return num;
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
        throw new NotSupportedException(Environment.GetResourceString("ConcurrentCollection_SyncRoot_NotSupported"));
      }
    }

    /// <summary>初始化 <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public ConcurrentStack()
    {
    }

    /// <summary>初始化 <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> 类的新实例，该类包含从指定集合中复制的元素</summary>
    /// <param name="collection">其元素被复制到新的 <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> 中的集合。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="collection" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    public ConcurrentStack(IEnumerable<T> collection)
    {
      if (collection == null)
        throw new ArgumentNullException("collection");
      this.InitializeFromCollection(collection);
    }

    private void InitializeFromCollection(IEnumerable<T> collection)
    {
      ConcurrentStack<T>.Node node = (ConcurrentStack<T>.Node) null;
      foreach (T obj in collection)
        node = new ConcurrentStack<T>.Node(obj)
        {
          m_next = node
        };
      this.m_head = node;
    }

    [OnSerializing]
    private void OnSerializing(StreamingContext context)
    {
      this.m_serializationArray = this.ToArray();
    }

    [OnDeserialized]
    private void OnDeserialized(StreamingContext context)
    {
      ConcurrentStack<T>.Node node1 = (ConcurrentStack<T>.Node) null;
      ConcurrentStack<T>.Node node2 = (ConcurrentStack<T>.Node) null;
      for (int index = 0; index < this.m_serializationArray.Length; ++index)
      {
        ConcurrentStack<T>.Node node3 = new ConcurrentStack<T>.Node(this.m_serializationArray[index]);
        if (node1 == null)
          node2 = node3;
        else
          node1.m_next = node3;
        node1 = node3;
      }
      this.m_head = node2;
      this.m_serializationArray = (T[]) null;
    }

    /// <summary>从 <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> 中移除所有对象。</summary>
    [__DynamicallyInvokable]
    public void Clear()
    {
      this.m_head = (ConcurrentStack<T>.Node) null;
    }

    [__DynamicallyInvokable]
    void ICollection.CopyTo(Array array, int index)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      ((ICollection) this.ToList()).CopyTo(array, index);
    }

    /// <summary>从指定数组索引开始将 <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> 元素复制到现有一维 <see cref="T:System.Array" /> 中。</summary>
    /// <param name="array">一维 <see cref="T:System.Array" />，用作从 <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> 所复制的元素的目标数组。<see cref="T:System.Array" /> 必须具有从零开始的索引。</param>
    /// <param name="index">
    /// <paramref name="array" /> 中从零开始的索引，从此处开始复制。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null 引用（在 Visual Basic 中为 Nothing）。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于零。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="index" /> 等于或大于的长度 <paramref name="array" /> -在源中的元素数目 <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> 大于从可用空间 <paramref name="index" /> 到目标的末尾 <paramref name="array" />。</exception>
    [__DynamicallyInvokable]
    public void CopyTo(T[] array, int index)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      this.ToList().CopyTo(array, index);
    }

    /// <summary>在 <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> 的顶部插入一个对象。</summary>
    /// <param name="item">要推入到 <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> 中的对象。该值对于引用类型可以是空引用（在 Visual Basic 中为 Nothing）。</param>
    [__DynamicallyInvokable]
    public void Push(T item)
    {
      ConcurrentStack<T>.Node node1 = new ConcurrentStack<T>.Node(item);
      node1.m_next = this.m_head;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      ConcurrentStack<T>.Node& location1 = @this.m_head;
      ConcurrentStack<T>.Node node2 = node1;
      ConcurrentStack<T>.Node comparand = node2.m_next;
      if (Interlocked.CompareExchange<ConcurrentStack<T>.Node>(location1, node2, comparand) == node1.m_next)
        return;
      ConcurrentStack<T>.Node node3 = node1;
      this.PushCore(node3, node3);
    }

    /// <summary>自动将多个对象插入 <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> 的顶部。</summary>
    /// <param name="items">要推入到 <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> 中的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="items" /> 为 null 引用（在 Visual Basic 中为 Nothing）。</exception>
    [__DynamicallyInvokable]
    public void PushRange(T[] items)
    {
      if (items == null)
        throw new ArgumentNullException("items");
      this.PushRange(items, 0, items.Length);
    }

    /// <summary>自动将多个对象插入 <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> 的顶部。</summary>
    /// <param name="items">要推入到 <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> 中的对象。</param>
    /// <param name="startIndex">
    /// <paramref name="items" /> 中从零开始的偏移量，在此开始将元素插入到 <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> 的顶部。</param>
    /// <param name="count">要插入到 <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> 顶部的元素数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="items" /> 为 null 引用（在 Visual Basic 中为 Nothing）。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 或 <paramref name="count" /> 为负。或者 <paramref name="startIndex" /> 大于或等于的长度 <paramref name="items" />。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="startIndex" /> + <paramref name="count" /> 的长度大于 <paramref name="items" />。</exception>
    [__DynamicallyInvokable]
    public void PushRange(T[] items, int startIndex, int count)
    {
      this.ValidatePushPopRangeInput(items, startIndex, count);
      if (count == 0)
        return;
      ConcurrentStack<T>.Node tail;
      ConcurrentStack<T>.Node head = tail = new ConcurrentStack<T>.Node(items[startIndex]);
      for (int index = startIndex + 1; index < startIndex + count; ++index)
        head = new ConcurrentStack<T>.Node(items[index])
        {
          m_next = head
        };
      tail.m_next = this.m_head;
      if (Interlocked.CompareExchange<ConcurrentStack<T>.Node>(ref this.m_head, head, tail.m_next) == tail.m_next)
        return;
      this.PushCore(head, tail);
    }

    private void PushCore(ConcurrentStack<T>.Node head, ConcurrentStack<T>.Node tail)
    {
      SpinWait spinWait = new SpinWait();
      do
      {
        spinWait.SpinOnce();
        tail.m_next = this.m_head;
      }
      while (Interlocked.CompareExchange<ConcurrentStack<T>.Node>(ref this.m_head, head, tail.m_next) != tail.m_next);
      if (!CDSCollectionETWBCLProvider.Log.IsEnabled())
        return;
      CDSCollectionETWBCLProvider.Log.ConcurrentStack_FastPushFailed(spinWait.Count);
    }

    private void ValidatePushPopRangeInput(T[] items, int startIndex, int count)
    {
      if (items == null)
        throw new ArgumentNullException("items");
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ConcurrentStack_PushPopRange_CountOutOfRange"));
      int length = items.Length;
      if (startIndex >= length || startIndex < 0)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ConcurrentStack_PushPopRange_StartOutOfRange"));
      if (length - count < startIndex)
        throw new ArgumentException(Environment.GetResourceString("ConcurrentStack_PushPopRange_InvalidCount"));
    }

    [__DynamicallyInvokable]
    bool IProducerConsumerCollection<T>.TryAdd(T item)
    {
      this.Push(item);
      return true;
    }

    /// <summary>尝试从 <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> 的顶部返回一个对象而无需移除它。</summary>
    /// <returns>如果成功返回了对象，则为 true；否则为 false。</returns>
    /// <param name="result">当此方法返回时，<paramref name="result" /> 包含来自 <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> 顶部的一个对象，或如果操作失败，则包含未指定的值。</param>
    [__DynamicallyInvokable]
    public bool TryPeek(out T result)
    {
      ConcurrentStack<T>.Node node = this.m_head;
      if (node == null)
      {
        result = default (T);
        return false;
      }
      result = node.m_value;
      return true;
    }

    /// <summary>尝试弹出并返回 <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> 顶部的对象。</summary>
    /// <returns>如果从 <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> 的顶部成功移除并返回了元素，则为 true；否则为 false。</returns>
    /// <param name="result">如果操作成功，则此方法返回时，<paramref name="result" /> 包含所移除的对象。如果没有可供移除的对象，则不指定该值。</param>
    [__DynamicallyInvokable]
    public bool TryPop(out T result)
    {
      ConcurrentStack<T>.Node comparand = this.m_head;
      if (comparand == null)
      {
        result = default (T);
        return false;
      }
      if (Interlocked.CompareExchange<ConcurrentStack<T>.Node>(ref this.m_head, comparand.m_next, comparand) != comparand)
        return this.TryPopCore(out result);
      result = comparand.m_value;
      return true;
    }

    /// <summary>尝试自动弹出并返回 <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> 顶部的多个对象。</summary>
    /// <returns>成功从 <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> 顶部弹出并插入 <paramref name="items" /> 中的对象数。</returns>
    /// <param name="items">要将从 <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> 顶部弹出的对象添加到的 <see cref="T:System.Array" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="items" /> 是 null 的参数 （在 Visual Basic 中为 Nothing）。</exception>
    [__DynamicallyInvokable]
    public int TryPopRange(T[] items)
    {
      if (items == null)
        throw new ArgumentNullException("items");
      return this.TryPopRange(items, 0, items.Length);
    }

    /// <summary>尝试自动弹出并返回 <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> 顶部的多个对象。</summary>
    /// <returns>成功从堆栈顶部弹出并插入 <paramref name="items" /> 中的对象数。</returns>
    /// <param name="items">要将从 <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> 顶部弹出的对象添加到的 <see cref="T:System.Array" />。</param>
    /// <param name="startIndex">
    /// <paramref name="items" /> 中从零开始的偏移量，在此开始从 <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> 顶部插入元素。</param>
    /// <param name="count">从 <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> 顶部弹出并插入 <paramref name="items" /> 中的元素数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="items" /> 为 null 引用（在 Visual Basic 中为 Nothing）。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 或 <paramref name="count" /> 为负。或者 <paramref name="startIndex" /> 大于或等于的长度 <paramref name="items" />。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="startIndex" /> + <paramref name="count" /> 的长度大于 <paramref name="items" />。</exception>
    [__DynamicallyInvokable]
    public int TryPopRange(T[] items, int startIndex, int count)
    {
      this.ValidatePushPopRangeInput(items, startIndex, count);
      if (count == 0)
        return 0;
      ConcurrentStack<T>.Node poppedHead;
      int nodesCount = this.TryPopCore(count, out poppedHead);
      if (nodesCount > 0)
        this.CopyRemovedItems(poppedHead, items, startIndex, nodesCount);
      return nodesCount;
    }

    private bool TryPopCore(out T result)
    {
      ConcurrentStack<T>.Node poppedHead;
      if (this.TryPopCore(1, out poppedHead) == 1)
      {
        result = poppedHead.m_value;
        return true;
      }
      result = default (T);
      return false;
    }

    private int TryPopCore(int count, out ConcurrentStack<T>.Node poppedHead)
    {
      SpinWait spinWait = new SpinWait();
      int num1 = 1;
      Random random = new Random(Environment.TickCount & int.MaxValue);
      ConcurrentStack<T>.Node comparand;
      int num2;
      while (true)
      {
        comparand = this.m_head;
        if (comparand != null)
        {
          ConcurrentStack<T>.Node node = comparand;
          for (num2 = 1; num2 < count && node.m_next != null; ++num2)
            node = node.m_next;
          if (Interlocked.CompareExchange<ConcurrentStack<T>.Node>(ref this.m_head, node.m_next, comparand) != comparand)
          {
            for (int index = 0; index < num1; ++index)
              spinWait.SpinOnce();
            num1 = spinWait.NextSpinWillYield ? random.Next(1, 8) : num1 * 2;
          }
          else
            goto label_9;
        }
        else
          break;
      }
      if (count == 1 && CDSCollectionETWBCLProvider.Log.IsEnabled())
        CDSCollectionETWBCLProvider.Log.ConcurrentStack_FastPopFailed(spinWait.Count);
      poppedHead = (ConcurrentStack<T>.Node) null;
      return 0;
label_9:
      if (count == 1 && CDSCollectionETWBCLProvider.Log.IsEnabled())
        CDSCollectionETWBCLProvider.Log.ConcurrentStack_FastPopFailed(spinWait.Count);
      poppedHead = comparand;
      return num2;
    }

    private void CopyRemovedItems(ConcurrentStack<T>.Node head, T[] collection, int startIndex, int nodesCount)
    {
      ConcurrentStack<T>.Node node = head;
      for (int index = startIndex; index < startIndex + nodesCount; ++index)
      {
        collection[index] = node.m_value;
        node = node.m_next;
      }
    }

    [__DynamicallyInvokable]
    bool IProducerConsumerCollection<T>.TryTake(out T item)
    {
      return this.TryPop(out item);
    }

    /// <summary>将 <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> 中存储的项复制到一个新的数组。</summary>
    /// <returns>新数组包含从 <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> 复制的元素的快照。</returns>
    [__DynamicallyInvokable]
    public T[] ToArray()
    {
      return this.ToList().ToArray();
    }

    private List<T> ToList()
    {
      List<T> objList = new List<T>();
      for (ConcurrentStack<T>.Node node = this.m_head; node != null; node = node.m_next)
        objList.Add(node.m_value);
      return objList;
    }

    /// <summary>返回循环访问 <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> 的枚举数。</summary>
    /// <returns>用于 <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> 的枚举数。</returns>
    [__DynamicallyInvokable]
    public IEnumerator<T> GetEnumerator()
    {
      return this.GetEnumerator(this.m_head);
    }

    private IEnumerator<T> GetEnumerator(ConcurrentStack<T>.Node head)
    {
      for (ConcurrentStack<T>.Node current = head; current != null; current = current.m_next)
        yield return current.m_value;
    }

    [__DynamicallyInvokable]
    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    private class Node
    {
      internal readonly T m_value;
      internal ConcurrentStack<T>.Node m_next;

      internal Node(T value)
      {
        this.m_value = value;
        this.m_next = (ConcurrentStack<T>.Node) null;
      }
    }
  }
}
