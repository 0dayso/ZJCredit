// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.SynchronizationContextAwaitTaskContinuation
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Threading.Tasks
{
  internal sealed class SynchronizationContextAwaitTaskContinuation : AwaitTaskContinuation
  {
    private static readonly SendOrPostCallback s_postCallback = new SendOrPostCallback(SynchronizationContextAwaitTaskContinuation.\u003C\u003Ec.\u003C\u003E9.\u003C\u002Ecctor\u003Eb__8_0);
    [SecurityCritical]
    private static ContextCallback s_postActionCallback;
    private readonly SynchronizationContext m_syncContext;

    [SecurityCritical]
    internal SynchronizationContextAwaitTaskContinuation(SynchronizationContext context, Action action, bool flowExecutionContext, ref StackCrawlMark stackMark)
      : base(action, flowExecutionContext, ref stackMark)
    {
      this.m_syncContext = context;
    }

    [SecuritySafeCritical]
    internal override sealed void Run(Task task, bool canInlineContinuationTask)
    {
      if (canInlineContinuationTask && this.m_syncContext == SynchronizationContext.CurrentNoFlow)
      {
        this.RunCallback(AwaitTaskContinuation.GetInvokeActionCallback(), (object) this.m_action, ref Task.t_currentTask);
      }
      else
      {
        TplEtwProvider tplEtwProvider = TplEtwProvider.Log;
        if (tplEtwProvider.IsEnabled())
        {
          this.m_continuationId = Task.NewId();
          tplEtwProvider.AwaitTaskContinuationScheduled((task.ExecutingTaskScheduler ?? TaskScheduler.Default).Id, task.Id, this.m_continuationId);
        }
        this.RunCallback(SynchronizationContextAwaitTaskContinuation.GetPostActionCallback(), (object) this, ref Task.t_currentTask);
      }
    }

    [SecurityCritical]
    private static void PostAction(object state)
    {
      SynchronizationContextAwaitTaskContinuation taskContinuation = (SynchronizationContextAwaitTaskContinuation) state;
      if (TplEtwProvider.Log.TasksSetActivityIds && taskContinuation.m_continuationId != 0)
        taskContinuation.m_syncContext.Post(SynchronizationContextAwaitTaskContinuation.s_postCallback, (object) SynchronizationContextAwaitTaskContinuation.GetActionLogDelegate(taskContinuation.m_continuationId, taskContinuation.m_action));
      else
        taskContinuation.m_syncContext.Post(SynchronizationContextAwaitTaskContinuation.s_postCallback, (object) taskContinuation.m_action);
    }

    private static Action GetActionLogDelegate(int continuationId, Action action)
    {
      return (Action) (() =>
      {
        Guid oldActivityThatWillContinue;
        EventSource.SetCurrentThreadActivityId(TplEtwProvider.CreateGuidForTaskID(continuationId), out oldActivityThatWillContinue);
        try
        {
          action();
        }
        finally
        {
          EventSource.SetCurrentThreadActivityId(oldActivityThatWillContinue);
        }
      });
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static ContextCallback GetPostActionCallback()
    {
      ContextCallback contextCallback = SynchronizationContextAwaitTaskContinuation.s_postActionCallback;
      if (contextCallback == null)
        SynchronizationContextAwaitTaskContinuation.s_postActionCallback = contextCallback = new ContextCallback(SynchronizationContextAwaitTaskContinuation.PostAction);
      return contextCallback;
    }
  }
}
