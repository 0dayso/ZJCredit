// Decompiled with JetBrains decompiler
// Type: System.Collections.Concurrent.Partitioner`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Security.Permissions;

namespace System.Collections.Concurrent
{
  /// <summary>表示将一个数据源拆分成多个分区的特定方式。</summary>
  /// <typeparam name="TSource">集合中的元素的类型。</typeparam>
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public abstract class Partitioner<TSource>
  {
    /// <summary>获取是否可以动态创建附加分区。</summary>
    /// <returns>如果 <see cref="T:System.Collections.Concurrent.Partitioner`1" /> 可以根据分区请求动态创建分区，则为 true；如果 <see cref="T:System.Collections.Concurrent.Partitioner`1" /> 只能以静态方式分配分区，则为 false。</returns>
    [__DynamicallyInvokable]
    public virtual bool SupportsDynamicPartitions
    {
      [__DynamicallyInvokable] get
      {
        return false;
      }
    }

    /// <summary>创建新的分区程序实例。</summary>
    [__DynamicallyInvokable]
    protected Partitioner()
    {
    }

    /// <summary>将基础集合分区成给定数目的分区。</summary>
    /// <returns>一个包含 <paramref name="partitionCount" /> 枚举器的列表。</returns>
    /// <param name="partitionCount">要创建的分区数。</param>
    [__DynamicallyInvokable]
    public abstract IList<IEnumerator<TSource>> GetPartitions(int partitionCount);

    /// <summary>创建一个可将基础集合分区成可变数目的分区的对象。</summary>
    /// <returns>一个可针对基础数据源创建分区的对象。</returns>
    /// <exception cref="T:System.NotSupportedException">该基类不支持动态分区。必须在派生类中实现它。</exception>
    [__DynamicallyInvokable]
    public virtual IEnumerable<TSource> GetDynamicPartitions()
    {
      throw new NotSupportedException(Environment.GetResourceString("Partitioner_DynamicPartitionsNotSupported"));
    }
  }
}
