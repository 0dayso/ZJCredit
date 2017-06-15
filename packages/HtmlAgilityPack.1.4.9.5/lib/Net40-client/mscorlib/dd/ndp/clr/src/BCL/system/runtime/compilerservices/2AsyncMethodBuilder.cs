// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
  /// <summary>表示异步方法的生成器，该生成器将返回任务并提供结果的参数。</summary>
  /// <typeparam name="TResult">用来完成任务的结果。</typeparam>
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public struct AsyncTaskMethodBuilder<TResult>
  {
    internal static readonly System.Threading.Tasks.Task<TResult> s_defaultResultTask = AsyncTaskCache.CreateCacheableTask<TResult>(default (TResult));
    private AsyncMethodBuilderCore m_coreState;
    private System.Threading.Tasks.Task<TResult> m_task;

    /// <summary>获取此生成器的任务。</summary>
    /// <returns>此生成器的任务。</returns>
    [__DynamicallyInvokable]
    public System.Threading.Tasks.Task<TResult> Task
    {
      [__DynamicallyInvokable] get
      {
        System.Threading.Tasks.Task<TResult> task = this.m_task;
        if (task == null)
          this.m_task = task = new System.Threading.Tasks.Task<TResult>();
        return task;
      }
    }

    private object ObjectIdForDebugger
    {
      get
      {
        return (object) this.Task;
      }
    }

    /// <summary>创建 <see cref="T:System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1" /> 类的实例。</summary>
    /// <returns>生成器的新实例。</returns>
    [__DynamicallyInvokable]
    public static AsyncTaskMethodBuilder<TResult> Create()
    {
      return new AsyncTaskMethodBuilder<TResult>();
    }

    /// <summary>开始运行有关联状态机的生成器。</summary>
    /// <param name="stateMachine">由引用传递的状态器实例。</param>
    /// <typeparam name="TStateMachine">状态机的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="stateMachine" /> 为 null。</exception>
    [SecuritySafeCritical]
    [DebuggerStepThrough]
    [__DynamicallyInvokable]
    public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
    {
      if ((object) stateMachine == null)
        throw new ArgumentNullException("stateMachine");
      ExecutionContextSwitcher ecsw = new ExecutionContextSwitcher();
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        ExecutionContext.EstablishCopyOnWriteScope(ref ecsw);
        stateMachine.MoveNext();
      }
      finally
      {
        ecsw.Undo();
      }
    }

    /// <summary>一个生成器与指定的状态机关联。</summary>
    /// <param name="stateMachine">要与生成器关联的状态机实例。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="stateMachine" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">预设置状态机。</exception>
    [__DynamicallyInvokable]
    public void SetStateMachine(IAsyncStateMachine stateMachine)
    {
      this.m_coreState.SetStateMachine(stateMachine);
    }

    /// <summary>指定的 awaiter 完成时，安排状态机，以继续下一操作。</summary>
    /// <param name="awaiter">awaiter。</param>
    /// <param name="stateMachine">状态机。</param>
    /// <typeparam name="TAwaiter">Awaiter 的类型。</typeparam>
    /// <typeparam name="TStateMachine">状态机的类型。</typeparam>
    [__DynamicallyInvokable]
    public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
    {
      try
      {
        AsyncMethodBuilderCore.MoveNextRunner runnerToInitialize = (AsyncMethodBuilderCore.MoveNextRunner) null;
        Action completionAction = this.m_coreState.GetCompletionAction(AsyncCausalityTracer.LoggingOn ? (System.Threading.Tasks.Task) this.Task : (System.Threading.Tasks.Task) null, ref runnerToInitialize);
        if (this.m_coreState.m_stateMachine == null)
        {
          System.Threading.Tasks.Task<TResult> task = this.Task;
          this.m_coreState.PostBoxInitialization((IAsyncStateMachine) stateMachine, runnerToInitialize, (System.Threading.Tasks.Task) task);
        }
        awaiter.OnCompleted(completionAction);
      }
      catch (Exception ex)
      {
        // ISSUE: variable of the null type
        __Null local = null;
        AsyncMethodBuilderCore.ThrowAsync(ex, (SynchronizationContext) local);
      }
    }

    /// <summary>指定的 awaiter 完成时，安排状态机，以继续下一操作。此方法可从部分受信任的代码调用。</summary>
    /// <param name="awaiter">awaiter。</param>
    /// <param name="stateMachine">状态机。</param>
    /// <typeparam name="TAwaiter">Awaiter 的类型。</typeparam>
    /// <typeparam name="TStateMachine">状态机的类型。</typeparam>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
    {
      try
      {
        AsyncMethodBuilderCore.MoveNextRunner runnerToInitialize = (AsyncMethodBuilderCore.MoveNextRunner) null;
        Action completionAction = this.m_coreState.GetCompletionAction(AsyncCausalityTracer.LoggingOn ? (System.Threading.Tasks.Task) this.Task : (System.Threading.Tasks.Task) null, ref runnerToInitialize);
        if (this.m_coreState.m_stateMachine == null)
        {
          System.Threading.Tasks.Task<TResult> task = this.Task;
          this.m_coreState.PostBoxInitialization((IAsyncStateMachine) stateMachine, runnerToInitialize, (System.Threading.Tasks.Task) task);
        }
        awaiter.UnsafeOnCompleted(completionAction);
      }
      catch (Exception ex)
      {
        // ISSUE: variable of the null type
        __Null local = null;
        AsyncMethodBuilderCore.ThrowAsync(ex, (SynchronizationContext) local);
      }
    }

    /// <summary>将任务标记为已成功完成。</summary>
    /// <param name="result">用来完成任务的结果。</param>
    /// <exception cref="T:System.InvalidOperationException">任务已完成。</exception>
    [__DynamicallyInvokable]
    public void SetResult(TResult result)
    {
      System.Threading.Tasks.Task<TResult> task = this.m_task;
      if (task == null)
      {
        this.m_task = this.GetTaskForResult(result);
      }
      else
      {
        if (AsyncCausalityTracer.LoggingOn)
          AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, task.Id, AsyncCausalityStatus.Completed);
        if (System.Threading.Tasks.Task.s_asyncDebuggingEnabled)
          System.Threading.Tasks.Task.RemoveFromActiveTasks(task.Id);
        if (!task.TrySetResult(result))
          throw new InvalidOperationException(Environment.GetResourceString("TaskT_TransitionToFinal_AlreadyCompleted"));
      }
    }

    internal void SetResult(System.Threading.Tasks.Task<TResult> completedTask)
    {
      if (this.m_task == null)
        this.m_task = completedTask;
      else
        this.SetResult(default (TResult));
    }

    /// <summary>标记此任务为失败并绑定指定的异常至此任务。</summary>
    /// <param name="exception">要绑定到任务的异常。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="exception" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">任务已完成。</exception>
    [__DynamicallyInvokable]
    public void SetException(Exception exception)
    {
      if (exception == null)
        throw new ArgumentNullException("exception");
      System.Threading.Tasks.Task<TResult> task = this.m_task ?? this.Task;
      OperationCanceledException canceledException = exception as OperationCanceledException;
      if ((canceledException != null ? (task.TrySetCanceled(canceledException.CancellationToken, (object) canceledException) ? 1 : 0) : (task.TrySetException((object) exception) ? 1 : 0)) == 0)
        throw new InvalidOperationException(Environment.GetResourceString("TaskT_TransitionToFinal_AlreadyCompleted"));
    }

    internal void SetNotificationForWaitCompletion(bool enabled)
    {
      this.Task.SetNotificationForWaitCompletion(enabled);
    }

    [SecuritySafeCritical]
    private System.Threading.Tasks.Task<TResult> GetTaskForResult(TResult result)
    {
      if ((object) default (TResult) != null)
      {
        if (typeof (TResult) == typeof (bool))
          return JitHelpers.UnsafeCast<System.Threading.Tasks.Task<TResult>>((bool) (object) result ? (object) AsyncTaskCache.TrueTask : (object) AsyncTaskCache.FalseTask);
        if (typeof (TResult) == typeof (int))
        {
          int num = (int) (object) result;
          if (num < 9 && num >= -1)
            return JitHelpers.UnsafeCast<System.Threading.Tasks.Task<TResult>>((object) AsyncTaskCache.Int32Tasks[num - -1]);
        }
        else if (typeof (TResult) == typeof (uint) && (int) (uint) (object) result == 0 || typeof (TResult) == typeof (byte) && (int) (byte) (object) result == 0 || (typeof (TResult) == typeof (sbyte) && (int) (sbyte) (object) result == 0 || typeof (TResult) == typeof (char) && (int) (char) (object) result == 0) || (typeof (TResult) == typeof (Decimal) && Decimal.Zero == (Decimal) (object) result || typeof (TResult) == typeof (long) && (long) (object) result == 0L || (typeof (TResult) == typeof (ulong) && (long) (ulong) (object) result == 0L || typeof (TResult) == typeof (short) && (int) (short) (object) result == 0)) || (typeof (TResult) == typeof (ushort) && (int) (ushort) (object) result == 0 || typeof (TResult) == typeof (IntPtr) && IntPtr.Zero == (IntPtr) (object) result || typeof (TResult) == typeof (UIntPtr) && UIntPtr.Zero == (UIntPtr) (object) result))
          return AsyncTaskMethodBuilder<TResult>.s_defaultResultTask;
      }
      else if ((object) result == null)
        return AsyncTaskMethodBuilder<TResult>.s_defaultResultTask;
      return new System.Threading.Tasks.Task<TResult>(result);
    }
  }
}
