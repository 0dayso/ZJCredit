// Decompiled with JetBrains decompiler
// Type: System.Collections.Concurrent.IProducerConsumerCollection`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;

namespace System.Collections.Concurrent
{
  /// <summary>定义供制造者/使用者用来操作线程安全集合的方法。此接口提供一个统一的表示（为生产者/消费者集合），从而更高级别抽象如 <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> 可以使用集合作为基础的存储机制。</summary>
  /// <typeparam name="T">指定集合中的元素的类型。</typeparam>
  [__DynamicallyInvokable]
  public interface IProducerConsumerCollection<T> : IEnumerable<T>, IEnumerable, ICollection
  {
    /// <summary>从指定的索引位置开始，将 <see cref="T:System.Collections.Concurrent.IProducerConsumerCollection`1" /> 的元素复制到 <see cref="T:System.Array" /> 中。</summary>
    /// <param name="array">一维 <see cref="T:System.Array" />，用作从 <see cref="T:System.Collections.Concurrent.IProducerConsumerCollection`1" /> 所复制的元素的目标数组。 该数组的索引必须从零开始。</param>
    /// <param name="index">
    /// <paramref name="array" /> 中从零开始的索引，从此索引处开始进行复制。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 是 null 引用（在 Visual Basic 中为 Nothing）。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于零。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="index" /> 等于或大于 <paramref name="array" /> 的长度 - 或 - 集合中的元素数大于从 <paramref name="index" /> 到目标 <paramref name="array" /> 结尾的可用空间。</exception>
    [__DynamicallyInvokable]
    void CopyTo(T[] array, int index);

    /// <summary>尝试将一个对象添加到 <see cref="T:System.Collections.Concurrent.IProducerConsumerCollection`1" /> 中。</summary>
    /// <returns>如果成功添加了对象，则为 true；否则为 false。</returns>
    /// <param name="item">要添加到 <see cref="T:System.Collections.Concurrent.IProducerConsumerCollection`1" /> 的对象。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="item" /> 对于此集合无效。</exception>
    [__DynamicallyInvokable]
    bool TryAdd(T item);

    /// <summary>尝试从 <see cref="T:System.Collections.Concurrent.IProducerConsumerCollection`1" /> 中移除并返回一个对象。</summary>
    /// <returns>如果成功移除并返回了对象，则为 true；否则为 false。</returns>
    /// <param name="item">此方法返回时，如果成功移除并返回了对象，则 <paramref name="item" /> 包含所移除的对象。如果没有可供移除的对象，则不指定该值。</param>
    [__DynamicallyInvokable]
    bool TryTake(out T item);

    /// <summary>将 <see cref="T:System.Collections.Concurrent.IProducerConsumerCollection`1" /> 中包含的元素复制到新数组中。</summary>
    /// <returns>一个新数组，其中包含从 <see cref="T:System.Collections.Concurrent.IProducerConsumerCollection`1" /> 复制的元素。</returns>
    [__DynamicallyInvokable]
    T[] ToArray();
  }
}
