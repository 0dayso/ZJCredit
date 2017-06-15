// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.ParallelLoopState
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Security.Permissions;

namespace System.Threading.Tasks
{
  /// <summary>可使并行循环迭代与其他迭代交互。此类的实例由 <see cref="T:System.Threading.Tasks.Parallel" /> 类提供给每个循环；不能在您的用户代码中创建实例。</summary>
  [DebuggerDisplay("ShouldExitCurrentIteration = {ShouldExitCurrentIteration}")]
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public class ParallelLoopState
  {
    private ParallelLoopStateFlags m_flagsBase;

    internal virtual bool InternalShouldExitCurrentIteration
    {
      get
      {
        throw new NotSupportedException(Environment.GetResourceString("ParallelState_NotSupportedException_UnsupportedMethod"));
      }
    }

    /// <summary>获取循环的当前迭代是否应基于此迭代或其他迭代发出的请求退出。</summary>
    /// <returns>如果当前迭代应退出，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool ShouldExitCurrentIteration
    {
      [__DynamicallyInvokable] get
      {
        return this.InternalShouldExitCurrentIteration;
      }
    }

    /// <summary>获取循环的任何迭代是否已调用 <see cref="M:System.Threading.Tasks.ParallelLoopState.Stop" /> 方法。</summary>
    /// <returns>如果任何迭代通过调用 <see cref="M:System.Threading.Tasks.ParallelLoopState.Stop" /> 方法已停止循环，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool IsStopped
    {
      [__DynamicallyInvokable] get
      {
        return (uint) (this.m_flagsBase.LoopStateFlags & ParallelLoopStateFlags.PLS_STOPPED) > 0U;
      }
    }

    /// <summary>获取循环的任何迭代是否已引发相应迭代未处理的异常。</summary>
    /// <returns>如果引发了未经处理的异常，则为 true；否则为 false。 </returns>
    [__DynamicallyInvokable]
    public bool IsExceptional
    {
      [__DynamicallyInvokable] get
      {
        return (uint) (this.m_flagsBase.LoopStateFlags & ParallelLoopStateFlags.PLS_EXCEPTIONAL) > 0U;
      }
    }

    internal virtual long? InternalLowestBreakIteration
    {
      get
      {
        throw new NotSupportedException(Environment.GetResourceString("ParallelState_NotSupportedException_UnsupportedMethod"));
      }
    }

    /// <summary>获取从中调用 <see cref="M:System.Threading.Tasks.ParallelLoopState.Break" /> 的最低循环迭代。</summary>
    /// <returns>从中调用 <see cref="M:System.Threading.Tasks.ParallelLoopState.Break" /> 的最低迭代。如果是 <see cref="M:System.Threading.Tasks.Parallel.ForEach``1(System.Collections.Concurrent.Partitioner{``0},System.Action{``0})" /> 循环，该值会基于内部生成的索引。</returns>
    [__DynamicallyInvokable]
    public long? LowestBreakIteration
    {
      [__DynamicallyInvokable] get
      {
        return this.InternalLowestBreakIteration;
      }
    }

    internal ParallelLoopState(ParallelLoopStateFlags fbase)
    {
      this.m_flagsBase = fbase;
    }

    /// <summary>告知 <see cref="T:System.Threading.Tasks.Parallel" /> 循环应在系统方便的时候尽早停止执行。</summary>
    /// <exception cref="T:System.InvalidOperationException">
    /// <see cref="M:System.Threading.Tasks.ParallelLoopState.Break" /> 以前调用了方法。<see cref="M:System.Threading.Tasks.ParallelLoopState.Break" /> 和 <see cref="M:System.Threading.Tasks.ParallelLoopState.Stop" /> 不能被相同循环的迭代组合使用。</exception>
    [__DynamicallyInvokable]
    public void Stop()
    {
      this.m_flagsBase.Stop();
    }

    internal virtual void InternalBreak()
    {
      throw new NotSupportedException(Environment.GetResourceString("ParallelState_NotSupportedException_UnsupportedMethod"));
    }

    /// <summary>告知 <see cref="T:System.Threading.Tasks.Parallel" /> 循环应在系统方便的时候尽早停止执行当前迭代之外的迭代。</summary>
    /// <exception cref="T:System.InvalidOperationException">
    /// <see cref="M:System.Threading.Tasks.ParallelLoopState.Stop" /> 方法以前被调用过。<see cref="M:System.Threading.Tasks.ParallelLoopState.Break" /> 和 <see cref="M:System.Threading.Tasks.ParallelLoopState.Stop" /> 不能被相同循环的迭代组合使用。</exception>
    [__DynamicallyInvokable]
    public void Break()
    {
      this.InternalBreak();
    }

    internal static void Break(int iteration, ParallelLoopStateFlags32 pflags)
    {
      int oldState = ParallelLoopStateFlags.PLS_NONE;
      if (!pflags.AtomicLoopStateUpdate(ParallelLoopStateFlags.PLS_BROKEN, ParallelLoopStateFlags.PLS_STOPPED | ParallelLoopStateFlags.PLS_EXCEPTIONAL | ParallelLoopStateFlags.PLS_CANCELED, ref oldState))
      {
        if ((oldState & ParallelLoopStateFlags.PLS_STOPPED) != 0)
          throw new InvalidOperationException(Environment.GetResourceString("ParallelState_Break_InvalidOperationException_BreakAfterStop"));
      }
      else
      {
        int comparand = pflags.m_lowestBreakIteration;
        if (iteration >= comparand)
          return;
        SpinWait spinWait = new SpinWait();
        while (Interlocked.CompareExchange(ref pflags.m_lowestBreakIteration, iteration, comparand) != comparand)
        {
          spinWait.SpinOnce();
          comparand = pflags.m_lowestBreakIteration;
          if (iteration > comparand)
            break;
        }
      }
    }

    internal static void Break(long iteration, ParallelLoopStateFlags64 pflags)
    {
      int oldState = ParallelLoopStateFlags.PLS_NONE;
      if (!pflags.AtomicLoopStateUpdate(ParallelLoopStateFlags.PLS_BROKEN, ParallelLoopStateFlags.PLS_STOPPED | ParallelLoopStateFlags.PLS_EXCEPTIONAL | ParallelLoopStateFlags.PLS_CANCELED, ref oldState))
      {
        if ((oldState & ParallelLoopStateFlags.PLS_STOPPED) != 0)
          throw new InvalidOperationException(Environment.GetResourceString("ParallelState_Break_InvalidOperationException_BreakAfterStop"));
      }
      else
      {
        long lowestBreakIteration = pflags.LowestBreakIteration;
        if (iteration >= lowestBreakIteration)
          return;
        SpinWait spinWait = new SpinWait();
        while (Interlocked.CompareExchange(ref pflags.m_lowestBreakIteration, iteration, lowestBreakIteration) != lowestBreakIteration)
        {
          spinWait.SpinOnce();
          lowestBreakIteration = pflags.LowestBreakIteration;
          if (iteration > lowestBreakIteration)
            break;
        }
      }
    }
  }
}
