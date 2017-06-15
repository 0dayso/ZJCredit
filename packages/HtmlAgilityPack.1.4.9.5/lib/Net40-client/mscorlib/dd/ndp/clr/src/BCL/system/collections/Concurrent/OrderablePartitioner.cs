// Decompiled with JetBrains decompiler
// Type: System.Collections.Concurrent.OrderablePartitioner`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Security.Permissions;

namespace System.Collections.Concurrent
{
  /// <summary>表示将一个可排序数据源拆分成多个分区的特定方式。</summary>
  /// <typeparam name="TSource">集合中的元素的类型。</typeparam>
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public abstract class OrderablePartitioner<TSource> : Partitioner<TSource>
  {
    /// <summary>获取是否按键增加的顺序生成每个分区中的元素。</summary>
    /// <returns>如果按键增加的顺序生成每个分区中的元素，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool KeysOrderedInEachPartition { [__DynamicallyInvokable] get; private set; }

    /// <summary>获取前一分区中的元素是否始终排在后一分区中的元素之前。</summary>
    /// <returns>如果前一分区中的元素始终排在后一分区中的元素之前，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool KeysOrderedAcrossPartitions { [__DynamicallyInvokable] get; private set; }

    /// <summary>获取是否规范化顺序键。</summary>
    /// <returns>如果规范化键，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool KeysNormalized { [__DynamicallyInvokable] get; private set; }

    /// <summary>从派生类中的构造函数进行调用以便使用索引键上指定的约束初始化 <see cref="T:System.Collections.Concurrent.OrderablePartitioner`1" /> 类。</summary>
    /// <param name="keysOrderedInEachPartition">指示是否按键增加的顺序生成每个分区中的元素。</param>
    /// <param name="keysOrderedAcrossPartitions">指示前一分区中的元素是否始终排在后一分区中的元素之前。如果为 true，则分区 0 中的每个元素的顺序键比分区 1 中的任何元素都要小，分区 1 中的每个元素的顺序键比分区 2 中的任何元素都要小，依次类推。</param>
    /// <param name="keysNormalized">指示是否规范化键。如果为 true，所有顺序键均为范围 [0 .. numberOfElements-1] 中的不同整数。如果为 false，顺序键仍必须互不相同，但只考虑其相对顺序，而不考虑其绝对值。</param>
    [__DynamicallyInvokable]
    protected OrderablePartitioner(bool keysOrderedInEachPartition, bool keysOrderedAcrossPartitions, bool keysNormalized)
    {
      this.KeysOrderedInEachPartition = keysOrderedInEachPartition;
      this.KeysOrderedAcrossPartitions = keysOrderedAcrossPartitions;
      this.KeysNormalized = keysNormalized;
    }

    /// <summary>将基础集合分区成指定数目的可排序分区。</summary>
    /// <returns>一个包含 <paramref name="partitionCount" /> 枚举器的列表。</returns>
    /// <param name="partitionCount">要创建的分区数。</param>
    [__DynamicallyInvokable]
    public abstract IList<IEnumerator<KeyValuePair<long, TSource>>> GetOrderablePartitions(int partitionCount);

    /// <summary>创建一个可将基础集合分区成可变数目的分区的对象。</summary>
    /// <returns>一个可针对基础数据源创建分区的对象。</returns>
    /// <exception cref="T:System.NotSupportedException">此分区程序不支持动态分区。</exception>
    [__DynamicallyInvokable]
    public virtual IEnumerable<KeyValuePair<long, TSource>> GetOrderableDynamicPartitions()
    {
      throw new NotSupportedException(Environment.GetResourceString("Partitioner_DynamicPartitionsNotSupported"));
    }

    /// <summary>将基础集合分区成给定数目的可排序分区。</summary>
    /// <returns>一个包含 <paramref name="partitionCount" /> 枚举器的列表。</returns>
    /// <param name="partitionCount">要创建的分区数。</param>
    [__DynamicallyInvokable]
    public override IList<IEnumerator<TSource>> GetPartitions(int partitionCount)
    {
      IList<IEnumerator<KeyValuePair<long, TSource>>> orderablePartitions = this.GetOrderablePartitions(partitionCount);
      if (orderablePartitions.Count != partitionCount)
        throw new InvalidOperationException("OrderablePartitioner_GetPartitions_WrongNumberOfPartitions");
      IEnumerator<TSource>[] enumeratorArray = new IEnumerator<TSource>[partitionCount];
      for (int index = 0; index < partitionCount; ++index)
        enumeratorArray[index] = (IEnumerator<TSource>) new OrderablePartitioner<TSource>.EnumeratorDropIndices(orderablePartitions[index]);
      return (IList<IEnumerator<TSource>>) enumeratorArray;
    }

    /// <summary>创建一个可将基础集合分区成可变数目的分区的对象。</summary>
    /// <returns>一个可针对基础数据源创建分区的对象。</returns>
    /// <exception cref="T:System.NotSupportedException">该基类不支持动态分区。它必须在派生类中实现。</exception>
    [__DynamicallyInvokable]
    public override IEnumerable<TSource> GetDynamicPartitions()
    {
      return (IEnumerable<TSource>) new OrderablePartitioner<TSource>.EnumerableDropIndices(this.GetOrderableDynamicPartitions());
    }

    private class EnumerableDropIndices : IEnumerable<TSource>, IEnumerable, IDisposable
    {
      private readonly IEnumerable<KeyValuePair<long, TSource>> m_source;

      public EnumerableDropIndices(IEnumerable<KeyValuePair<long, TSource>> source)
      {
        this.m_source = source;
      }

      public IEnumerator<TSource> GetEnumerator()
      {
        return (IEnumerator<TSource>) new OrderablePartitioner<TSource>.EnumeratorDropIndices(this.m_source.GetEnumerator());
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
        return (IEnumerator) this.GetEnumerator();
      }

      public void Dispose()
      {
        IDisposable disposable = this.m_source as IDisposable;
        if (disposable == null)
          return;
        disposable.Dispose();
      }
    }

    private class EnumeratorDropIndices : IEnumerator<TSource>, IDisposable, IEnumerator
    {
      private readonly IEnumerator<KeyValuePair<long, TSource>> m_source;

      public TSource Current
      {
        get
        {
          return this.m_source.Current.Value;
        }
      }

      object IEnumerator.Current
      {
        get
        {
          return (object) this.Current;
        }
      }

      public EnumeratorDropIndices(IEnumerator<KeyValuePair<long, TSource>> source)
      {
        this.m_source = source;
      }

      public bool MoveNext()
      {
        return this.m_source.MoveNext();
      }

      public void Dispose()
      {
        this.m_source.Dispose();
      }

      public void Reset()
      {
        this.m_source.Reset();
      }
    }
  }
}
