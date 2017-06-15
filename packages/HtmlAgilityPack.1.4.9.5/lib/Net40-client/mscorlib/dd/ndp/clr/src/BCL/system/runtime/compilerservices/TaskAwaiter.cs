// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.TaskAwaiter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.ObjectModel;
using System.Diagnostics.Tracing;
using System.Runtime.ExceptionServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
  /// <summary>提供对象，其等待异步任务的完成。</summary>
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public struct TaskAwaiter : ICriticalNotifyCompletion, INotifyCompletion
  {
    private readonly Task m_task;

    /// <summary>获取一个值，该值指示异步任务是否已完成。</summary>
    /// <returns>如果该任务已完成，则为 true；否则为 false。</returns>
    /// <exception cref="T:System.NullReferenceException">
    /// <see cref="T:System.Runtime.CompilerServices.TaskAwaiter" /> 对象未正确地初始化。</exception>
    [__DynamicallyInvokable]
    public bool IsCompleted
    {
      [__DynamicallyInvokable] get
      {
        return this.m_task.IsCompleted;
      }
    }

    internal TaskAwaiter(Task task)
    {
      this.m_task = task;
    }

    /// <summary>将操作设置为当 <see cref="T:System.Runtime.CompilerServices.TaskAwaiter" /> 对象停止等待异步任务完成时执行。</summary>
    /// <param name="continuation">在等待操作完成时要执行的操作。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="continuation" /> 为 null。</exception>
    /// <exception cref="T:System.NullReferenceException">
    /// <see cref="T:System.Runtime.CompilerServices.TaskAwaiter" /> 对象未正确地初始化。</exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public void OnCompleted(Action continuation)
    {
      TaskAwaiter.OnCompletedInternal(this.m_task, continuation, true, true);
    }

    /// <summary>计划与此 awaiter 相关异步任务的延续操作。</summary>
    /// <param name="continuation">在等待操作完成时要调用的操作。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="continuation" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">等待未正确地初始化。</exception>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public void UnsafeOnCompleted(Action continuation)
    {
      TaskAwaiter.OnCompletedInternal(this.m_task, continuation, true, false);
    }

    /// <summary>异步任务完成后关闭等待任务。</summary>
    /// <exception cref="T:System.NullReferenceException">
    /// <see cref="T:System.Runtime.CompilerServices.TaskAwaiter" /> 对象未正确地初始化。</exception>
    /// <exception cref="T:System.Threading.Tasks.TaskCanceledException">已取消的任务。</exception>
    /// <exception cref="T:System.Exception">在 <see cref="F:System.Threading.Tasks.TaskStatus.Faulted" /> 状态中完成的任务。</exception>
    [__DynamicallyInvokable]
    public void GetResult()
    {
      TaskAwaiter.ValidateEnd(this.m_task);
    }

    internal static void ValidateEnd(Task task)
    {
      if (!task.IsWaitNotificationEnabledOrNotRanToCompletion)
        return;
      TaskAwaiter.HandleNonSuccessAndDebuggerNotification(task);
    }

    private static void HandleNonSuccessAndDebuggerNotification(Task task)
    {
      if (!task.IsCompleted)
        task.InternalWait(-1, new CancellationToken());
      task.NotifyDebuggerOfWaitCompletionIfNecessary();
      if (task.IsRanToCompletion)
        return;
      TaskAwaiter.ThrowForNonSuccess(task);
    }

    private static void ThrowForNonSuccess(Task task)
    {
      switch (task.Status)
      {
        case TaskStatus.Canceled:
          ExceptionDispatchInfo exceptionDispatchInfo = task.GetCancellationExceptionDispatchInfo();
          if (exceptionDispatchInfo != null)
            exceptionDispatchInfo.Throw();
          throw new TaskCanceledException(task);
        case TaskStatus.Faulted:
          ReadOnlyCollection<ExceptionDispatchInfo> exceptionDispatchInfos = task.GetExceptionDispatchInfos();
          if (exceptionDispatchInfos.Count <= 0)
            throw task.Exception;
          exceptionDispatchInfos[0].Throw();
          break;
      }
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static void OnCompletedInternal(Task task, Action continuation, bool continueOnCapturedContext, bool flowExecutionContext)
    {
      if (continuation == null)
        throw new ArgumentNullException("continuation");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      if (TplEtwProvider.Log.IsEnabled() || Task.s_asyncDebuggingEnabled)
        continuation = TaskAwaiter.OutputWaitEtwEvents(task, continuation);
      task.SetContinuationForAwait(continuation, continueOnCapturedContext, flowExecutionContext, ref stackMark);
    }

    private static Action OutputWaitEtwEvents(Task task, Action continuation)
    {
      if (Task.s_asyncDebuggingEnabled)
        Task.AddToActiveTasks(task);
      TplEtwProvider etwLog = TplEtwProvider.Log;
      if (etwLog.IsEnabled())
      {
        Task internalCurrent = Task.InternalCurrent;
        Task continuationTask = AsyncMethodBuilderCore.TryGetContinuationTask(continuation);
        etwLog.TaskWaitBegin(internalCurrent != null ? internalCurrent.m_taskScheduler.Id : TaskScheduler.Default.Id, internalCurrent != null ? internalCurrent.Id : 0, task.Id, TplEtwProvider.TaskWaitBehavior.Asynchronous, continuationTask != null ? continuationTask.Id : 0, Thread.GetDomainID());
      }
      return AsyncMethodBuilderCore.CreateContinuationWrapper(continuation, (Action) (() =>
      {
        if (Task.s_asyncDebuggingEnabled)
          Task.RemoveFromActiveTasks(task.Id);
        Guid oldActivityThatWillContinue = new Guid();
        int num = etwLog.IsEnabled() ? 1 : 0;
        if (num != 0)
        {
          Task internalCurrent = Task.InternalCurrent;
          etwLog.TaskWaitEnd(internalCurrent != null ? internalCurrent.m_taskScheduler.Id : TaskScheduler.Default.Id, internalCurrent != null ? internalCurrent.Id : 0, task.Id);
          if (etwLog.TasksSetActivityIds && (task.Options & (TaskCreationOptions) 1024) != TaskCreationOptions.None)
            EventSource.SetCurrentThreadActivityId(TplEtwProvider.CreateGuidForTaskID(task.Id), out oldActivityThatWillContinue);
        }
        continuation();
        if (num == 0)
          return;
        etwLog.TaskWaitContinuationComplete(task.Id);
        if (!etwLog.TasksSetActivityIds || (task.Options & (TaskCreationOptions) 1024) == TaskCreationOptions.None)
          return;
        EventSource.SetCurrentThreadActivityId(oldActivityThatWillContinue);
      }), (Task) null);
    }
  }
}
