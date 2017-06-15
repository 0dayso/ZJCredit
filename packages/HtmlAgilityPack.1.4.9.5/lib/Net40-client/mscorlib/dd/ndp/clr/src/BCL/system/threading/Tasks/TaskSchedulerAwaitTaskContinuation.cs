// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.TaskSchedulerAwaitTaskContinuation
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Threading.Tasks
{
  internal sealed class TaskSchedulerAwaitTaskContinuation : AwaitTaskContinuation
  {
    private readonly TaskScheduler m_scheduler;

    [SecurityCritical]
    internal TaskSchedulerAwaitTaskContinuation(TaskScheduler scheduler, Action action, bool flowExecutionContext, ref StackCrawlMark stackMark)
      : base(action, flowExecutionContext, ref stackMark)
    {
      this.m_scheduler = scheduler;
    }

    internal override sealed void Run(Task ignored, bool canInlineContinuationTask)
    {
      if (this.m_scheduler == TaskScheduler.Default)
      {
        base.Run(ignored, canInlineContinuationTask);
      }
      else
      {
        int num = !canInlineContinuationTask ? 0 : (TaskScheduler.InternalCurrent == this.m_scheduler ? 1 : (Thread.CurrentThread.IsThreadPoolThread ? 1 : 0));
        Task task = this.CreateTask((Action<object>) (state =>
        {
          try
          {
            ((Action) state)();
          }
          catch (Exception ex)
          {
            AwaitTaskContinuation.ThrowAsyncIfNecessary(ex);
          }
        }), (object) this.m_action, this.m_scheduler);
        if (num != 0)
        {
          TaskContinuation.InlineIfPossibleOrElseQueue(task, false);
        }
        else
        {
          try
          {
            task.ScheduleAndStart(false);
          }
          catch (TaskSchedulerException ex)
          {
          }
        }
      }
    }
  }
}
