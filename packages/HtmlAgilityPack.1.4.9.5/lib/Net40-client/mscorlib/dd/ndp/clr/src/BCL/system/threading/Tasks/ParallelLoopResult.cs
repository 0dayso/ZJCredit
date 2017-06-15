// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.ParallelLoopResult
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Threading.Tasks
{
  /// <summary>提供执行 <see cref="T:System.Threading.Tasks.Parallel" /> 循环的完成状态。</summary>
  [__DynamicallyInvokable]
  public struct ParallelLoopResult
  {
    internal bool m_completed;
    internal long? m_lowestBreakIteration;

    /// <summary>获取该循环是否已运行完成（即，该循环的所有迭代均已执行，并且该循环没有收到提前结束的请求）。</summary>
    /// <returns>如果该循环已运行完成，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool IsCompleted
    {
      [__DynamicallyInvokable] get
      {
        return this.m_completed;
      }
    }

    /// <summary>获取从中调用 <see cref="M:System.Threading.Tasks.ParallelLoopState.Break" /> 的最低迭代的索引。</summary>
    /// <returns>返回一个表示从中调用 Break 语句的最低迭代的整数。</returns>
    [__DynamicallyInvokable]
    public long? LowestBreakIteration
    {
      [__DynamicallyInvokable] get
      {
        return this.m_lowestBreakIteration;
      }
    }
  }
}
