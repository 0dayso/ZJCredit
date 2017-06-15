// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.AsyncVoidMethodBuilder
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
  /// <summary>表示生成器，用于不返回值的异步方法。</summary>
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public struct AsyncVoidMethodBuilder
  {
    private SynchronizationContext m_synchronizationContext;
    private AsyncMethodBuilderCore m_coreState;
    private Task m_task;

    private Task Task
    {
      get
      {
        if (this.m_task == null)
          this.m_task = new Task();
        return this.m_task;
      }
    }

    private object ObjectIdForDebugger
    {
      get
      {
        return (object) this.Task;
      }
    }

    /// <summary>创建 <see cref="T:System.Runtime.CompilerServices.AsyncVoidMethodBuilder" /> 类的实例。</summary>
    /// <returns>生成器的新实例。</returns>
    [__DynamicallyInvokable]
    public static AsyncVoidMethodBuilder Create()
    {
      SynchronizationContext currentNoFlow = SynchronizationContext.CurrentNoFlow;
      if (currentNoFlow != null)
        currentNoFlow.OperationStarted();
      return new AsyncVoidMethodBuilder() { m_synchronizationContext = currentNoFlow };
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
        Action completionAction = this.m_coreState.GetCompletionAction(AsyncCausalityTracer.LoggingOn ? this.Task : (Task) null, ref runnerToInitialize);
        if (this.m_coreState.m_stateMachine == null)
        {
          if (AsyncCausalityTracer.LoggingOn)
            AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, this.Task.Id, "Async: " + stateMachine.GetType().Name, 0UL);
          this.m_coreState.PostBoxInitialization((IAsyncStateMachine) stateMachine, runnerToInitialize, (Task) null);
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
        Action completionAction = this.m_coreState.GetCompletionAction(AsyncCausalityTracer.LoggingOn ? this.Task : (Task) null, ref runnerToInitialize);
        if (this.m_coreState.m_stateMachine == null)
        {
          if (AsyncCausalityTracer.LoggingOn)
            AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, this.Task.Id, "Async: " + stateMachine.GetType().Name, 0UL);
          this.m_coreState.PostBoxInitialization((IAsyncStateMachine) stateMachine, runnerToInitialize, (Task) null);
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

    /// <summary>标记此方法生成器为成功完成。</summary>
    /// <exception cref="T:System.InvalidOperationException">该生成程序未初始化。</exception>
    [__DynamicallyInvokable]
    public void SetResult()
    {
      if (AsyncCausalityTracer.LoggingOn)
        AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, this.Task.Id, AsyncCausalityStatus.Completed);
      if (this.m_synchronizationContext == null)
        return;
      this.NotifySynchronizationContextOfCompletion();
    }

    /// <summary>将一个异常绑定到该方法生成器。</summary>
    /// <param name="exception">要绑定的异常。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="exception" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">该生成程序未初始化。</exception>
    [__DynamicallyInvokable]
    public void SetException(Exception exception)
    {
      if (exception == null)
        throw new ArgumentNullException("exception");
      if (AsyncCausalityTracer.LoggingOn)
        AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, this.Task.Id, AsyncCausalityStatus.Error);
      if (this.m_synchronizationContext != null)
      {
        try
        {
          AsyncMethodBuilderCore.ThrowAsync(exception, this.m_synchronizationContext);
        }
        finally
        {
          this.NotifySynchronizationContextOfCompletion();
        }
      }
      else
        AsyncMethodBuilderCore.ThrowAsync(exception, (SynchronizationContext) null);
    }

    private void NotifySynchronizationContextOfCompletion()
    {
      try
      {
        this.m_synchronizationContext.OperationCompleted();
      }
      catch (Exception ex)
      {
        // ISSUE: variable of the null type
        __Null local = null;
        AsyncMethodBuilderCore.ThrowAsync(ex, (SynchronizationContext) local);
      }
    }
  }
}
