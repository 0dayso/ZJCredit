// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.AsyncTaskMethodBuilder
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
  /// <summary>表示生成器，用于返回任务的异步方法。</summary>
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public struct AsyncTaskMethodBuilder
  {
    private static readonly Task<VoidTaskResult> s_cachedCompleted = AsyncTaskMethodBuilder<VoidTaskResult>.s_defaultResultTask;
    private AsyncTaskMethodBuilder<VoidTaskResult> m_builder;

    /// <summary>获取此生成器的任务。</summary>
    /// <returns>此生成器的任务。</returns>
    /// <exception cref="T:System.InvalidOperationException">该生成程序未初始化。</exception>
    [__DynamicallyInvokable]
    public Task Task
    {
      [__DynamicallyInvokable] get
      {
        return (Task) this.m_builder.Task;
      }
    }

    private object ObjectIdForDebugger
    {
      get
      {
        return (object) this.Task;
      }
    }

    /// <summary>创建 <see cref="T:System.Runtime.CompilerServices.AsyncTaskMethodBuilder" /> 类的实例。</summary>
    /// <returns>生成器的新实例。</returns>
    [__DynamicallyInvokable]
    public static AsyncTaskMethodBuilder Create()
    {
      return new AsyncTaskMethodBuilder();
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
      this.m_builder.SetStateMachine(stateMachine);
    }

    /// <summary>指定的 awaiter 完成时，安排状态机，以继续下一操作。</summary>
    /// <param name="awaiter">awaiter。</param>
    /// <param name="stateMachine">状态机。</param>
    /// <typeparam name="TAwaiter">Awaiter 的类型。</typeparam>
    /// <typeparam name="TStateMachine">状态机的类型。</typeparam>
    [__DynamicallyInvokable]
    public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
    {
      this.m_builder.AwaitOnCompleted<TAwaiter, TStateMachine>(ref awaiter, ref stateMachine);
    }

    /// <summary>指定的 awaiter 完成时，安排状态机，以继续下一操作。此方法可从部分受信任的代码调用。</summary>
    /// <param name="awaiter">awaiter。</param>
    /// <param name="stateMachine">状态机。</param>
    /// <typeparam name="TAwaiter">Awaiter 的类型。</typeparam>
    /// <typeparam name="TStateMachine">状态机的类型。</typeparam>
    [__DynamicallyInvokable]
    public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
    {
      this.m_builder.AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref awaiter, ref stateMachine);
    }

    /// <summary>将任务标记为已成功完成。</summary>
    /// <exception cref="T:System.InvalidOperationException">任务已完成。- 或 -该生成程序未初始化。</exception>
    [__DynamicallyInvokable]
    public void SetResult()
    {
      this.m_builder.SetResult(AsyncTaskMethodBuilder.s_cachedCompleted);
    }

    /// <summary>标记此任务为失败并绑定指定的异常至此任务。</summary>
    /// <param name="exception">要绑定到任务的异常。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="exception" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">任务已完成。- 或 -该生成程序未初始化。</exception>
    [__DynamicallyInvokable]
    public void SetException(Exception exception)
    {
      this.m_builder.SetException(exception);
    }

    internal void SetNotificationForWaitCompletion(bool enabled)
    {
      this.m_builder.SetNotificationForWaitCompletion(enabled);
    }
  }
}
