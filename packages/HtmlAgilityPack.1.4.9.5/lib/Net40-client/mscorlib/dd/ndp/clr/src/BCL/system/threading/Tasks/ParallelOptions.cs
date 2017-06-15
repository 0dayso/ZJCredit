// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.ParallelOptions
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Threading.Tasks
{
  /// <summary>存储用于配置 <see cref="T:System.Threading.Tasks.Parallel" /> 类的方法的操作的选项。</summary>
  [__DynamicallyInvokable]
  public class ParallelOptions
  {
    private TaskScheduler m_scheduler;
    private int m_maxDegreeOfParallelism;
    private CancellationToken m_cancellationToken;

    /// <summary>获取或设置与此 <see cref="T:System.Threading.Tasks.ParallelOptions" /> 实例关联的 <see cref="T:System.Threading.Tasks.TaskScheduler" />。将此属性设置为 null，以指示应使用当前计划程序。</summary>
    /// <returns>与此实例关联的任务计划程序。</returns>
    [__DynamicallyInvokable]
    public TaskScheduler TaskScheduler
    {
      [__DynamicallyInvokable] get
      {
        return this.m_scheduler;
      }
      [__DynamicallyInvokable] set
      {
        this.m_scheduler = value;
      }
    }

    internal TaskScheduler EffectiveTaskScheduler
    {
      get
      {
        if (this.m_scheduler == null)
          return TaskScheduler.Current;
        return this.m_scheduler;
      }
    }

    /// <summary>获取或设置此 <see cref="T:System.Threading.Tasks.ParallelOptions" /> 实例所允许的并发任务的最大数目。</summary>
    /// <returns>一个表示最大并行度的整数。</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">该属性被设置为 0 或小于 1- 的值。</exception>
    [__DynamicallyInvokable]
    public int MaxDegreeOfParallelism
    {
      [__DynamicallyInvokable] get
      {
        return this.m_maxDegreeOfParallelism;
      }
      [__DynamicallyInvokable] set
      {
        if (value == 0 || value < -1)
          throw new ArgumentOutOfRangeException("MaxDegreeOfParallelism");
        this.m_maxDegreeOfParallelism = value;
      }
    }

    /// <summary>获取或设置与此 <see cref="T:System.Threading.Tasks.ParallelOptions" /> 实例关联的 <see cref="T:System.Threading.CancellationToken" />。</summary>
    /// <returns>与此实例关联的标记。</returns>
    [__DynamicallyInvokable]
    public CancellationToken CancellationToken
    {
      [__DynamicallyInvokable] get
      {
        return this.m_cancellationToken;
      }
      [__DynamicallyInvokable] set
      {
        this.m_cancellationToken = value;
      }
    }

    internal int EffectiveMaxConcurrencyLevel
    {
      get
      {
        int val2 = this.MaxDegreeOfParallelism;
        int concurrencyLevel = this.EffectiveTaskScheduler.MaximumConcurrencyLevel;
        if (concurrencyLevel > 0 && concurrencyLevel != int.MaxValue)
          val2 = val2 == -1 ? concurrencyLevel : Math.Min(concurrencyLevel, val2);
        return val2;
      }
    }

    /// <summary>初始化 <see cref="T:System.Threading.Tasks.ParallelOptions" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public ParallelOptions()
    {
      this.m_scheduler = TaskScheduler.Default;
      this.m_maxDegreeOfParallelism = -1;
      this.m_cancellationToken = CancellationToken.None;
    }
  }
}
