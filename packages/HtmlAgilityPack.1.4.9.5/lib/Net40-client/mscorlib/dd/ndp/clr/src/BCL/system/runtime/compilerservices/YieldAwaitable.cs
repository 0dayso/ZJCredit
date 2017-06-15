// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.YieldAwaitable
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics.Tracing;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
  /// <summary>提供上下文，用于在异步切换到目标环境时等待。</summary>
  [__DynamicallyInvokable]
  [StructLayout(LayoutKind.Sequential, Size = 1)]
  public struct YieldAwaitable
  {
    /// <summary>为此类的实例检索 <see cref="T:System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter" /> 对象。</summary>
    /// <returns>用于监视异步操作是否完成的对象。</returns>
    [__DynamicallyInvokable]
    public YieldAwaitable.YieldAwaiter GetAwaiter()
    {
      return new YieldAwaitable.YieldAwaiter();
    }

    /// <summary>提供等待器，用于切换至目标环境。</summary>
    [__DynamicallyInvokable]
    [StructLayout(LayoutKind.Sequential, Size = 1)]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
    public struct YieldAwaiter : ICriticalNotifyCompletion, INotifyCompletion
    {
      private static readonly WaitCallback s_waitCallbackRunAction = new WaitCallback(YieldAwaitable.YieldAwaiter.RunAction);
      private static readonly SendOrPostCallback s_sendOrPostCallbackRunAction = new SendOrPostCallback(YieldAwaitable.YieldAwaiter.RunAction);

      /// <summary>获取一个值，该值指示是否需要一个 yield。</summary>
      /// <returns>始终 false，指示 yield 始终是 <see cref="T:System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter" /> 所必需的。</returns>
      [__DynamicallyInvokable]
      public bool IsCompleted
      {
        [__DynamicallyInvokable] get
        {
          return false;
        }
      }

      /// <summary>设置延续以调用。</summary>
      /// <param name="continuation">要异步调用的调用。</param>
      /// <exception cref="T:System.ArgumentNullException">
      /// <paramref name="continuation" /> 为 null。</exception>
      [SecuritySafeCritical]
      [__DynamicallyInvokable]
      public void OnCompleted(Action continuation)
      {
        YieldAwaitable.YieldAwaiter.QueueContinuation(continuation, true);
      }

      /// <summary>发送 <paramref name="continuation" /> 回到当前上下文。</summary>
      /// <param name="continuation">要异步调用的调用。</param>
      /// <exception cref="T:System.ArgumentNullException">
      /// <paramref name="continuation" /> 参数为 null。</exception>
      [SecurityCritical]
      [__DynamicallyInvokable]
      public void UnsafeOnCompleted(Action continuation)
      {
        YieldAwaitable.YieldAwaiter.QueueContinuation(continuation, false);
      }

      [SecurityCritical]
      private static void QueueContinuation(Action continuation, bool flowContext)
      {
        if (continuation == null)
          throw new ArgumentNullException("continuation");
        if (TplEtwProvider.Log.IsEnabled())
          continuation = YieldAwaitable.YieldAwaiter.OutputCorrelationEtwEvent(continuation);
        SynchronizationContext currentNoFlow = SynchronizationContext.CurrentNoFlow;
        if (currentNoFlow != null && currentNoFlow.GetType() != typeof (SynchronizationContext))
        {
          currentNoFlow.Post(YieldAwaitable.YieldAwaiter.s_sendOrPostCallbackRunAction, (object) continuation);
        }
        else
        {
          TaskScheduler current = TaskScheduler.Current;
          if (current == TaskScheduler.Default)
          {
            if (flowContext)
              ThreadPool.QueueUserWorkItem(YieldAwaitable.YieldAwaiter.s_waitCallbackRunAction, (object) continuation);
            else
              ThreadPool.UnsafeQueueUserWorkItem(YieldAwaitable.YieldAwaiter.s_waitCallbackRunAction, (object) continuation);
          }
          else
            Task.Factory.StartNew(continuation, new CancellationToken(), TaskCreationOptions.PreferFairness, current);
        }
      }

      private static Action OutputCorrelationEtwEvent(Action continuation)
      {
        int continuationId = Task.NewId();
        Task internalCurrent = Task.InternalCurrent;
        TplEtwProvider.Log.AwaitTaskContinuationScheduled(TaskScheduler.Current.Id, internalCurrent != null ? internalCurrent.Id : 0, continuationId);
        return AsyncMethodBuilderCore.CreateContinuationWrapper(continuation, (Action) (() =>
        {
          TplEtwProvider tplEtwProvider = TplEtwProvider.Log;
          int TaskID1 = continuationId;
          tplEtwProvider.TaskWaitContinuationStarted(TaskID1);
          Guid oldActivityThatWillContinue = new Guid();
          if (tplEtwProvider.TasksSetActivityIds)
            EventSource.SetCurrentThreadActivityId(TplEtwProvider.CreateGuidForTaskID(continuationId), out oldActivityThatWillContinue);
          continuation();
          if (tplEtwProvider.TasksSetActivityIds)
            EventSource.SetCurrentThreadActivityId(oldActivityThatWillContinue);
          int TaskID2 = continuationId;
          tplEtwProvider.TaskWaitContinuationComplete(TaskID2);
        }), (Task) null);
      }

      private static void RunAction(object state)
      {
        ((Action) state)();
      }

      /// <summary>结束等待操作。</summary>
      [__DynamicallyInvokable]
      public void GetResult()
      {
      }
    }
  }
}
