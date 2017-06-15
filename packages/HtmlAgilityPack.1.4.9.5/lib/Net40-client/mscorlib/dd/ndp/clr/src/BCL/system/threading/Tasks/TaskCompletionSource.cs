// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.TaskCompletionSource`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Security.Permissions;

namespace System.Threading.Tasks
{
  /// <summary>表示未绑定到委托的 <see cref="T:System.Threading.Tasks.Task`1" /> 的制造者方，并通过 <see cref="P:System.Threading.Tasks.TaskCompletionSource`1.Task" /> 属性提供对使用者方的访问。</summary>
  /// <typeparam name="TResult">与此 <see cref="T:System.Threading.Tasks.TaskCompletionSource`1" /> 关联的结果值的类型。</typeparam>
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public class TaskCompletionSource<TResult>
  {
    private readonly System.Threading.Tasks.Task<TResult> m_task;

    /// <summary>获取由此 <see cref="T:System.Threading.Tasks.TaskCompletionSource`1" /> 创建的 <see cref="T:System.Threading.Tasks.Task`1" />。</summary>
    /// <returns>返回由此 <see cref="T:System.Threading.Tasks.TaskCompletionSource`1" /> 创建的 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    [__DynamicallyInvokable]
    public System.Threading.Tasks.Task<TResult> Task
    {
      [__DynamicallyInvokable] get
      {
        return this.m_task;
      }
    }

    /// <summary>创建一个 <see cref="T:System.Threading.Tasks.TaskCompletionSource`1" />。</summary>
    [__DynamicallyInvokable]
    public TaskCompletionSource()
    {
      this.m_task = new System.Threading.Tasks.Task<TResult>();
    }

    /// <summary>使用指定的选项创建一个 <see cref="T:System.Threading.Tasks.TaskCompletionSource`1" />。</summary>
    /// <param name="creationOptions">创建基础 <see cref="T:System.Threading.Tasks.Task`1" /> 时要使用的选项。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="creationOptions" /> 表示与 <see cref="T:System.Threading.Tasks.TaskCompletionSource`1" /> 一起使用时无效的选项。</exception>
    [__DynamicallyInvokable]
    public TaskCompletionSource(TaskCreationOptions creationOptions)
      : this((object) null, creationOptions)
    {
    }

    /// <summary>使用指定的状态创建一个 <see cref="T:System.Threading.Tasks.TaskCompletionSource`1" />。</summary>
    /// <param name="state">要用作基础 <see cref="T:System.Threading.Tasks.Task`1" /> 的 AsyncState 的状态。</param>
    [__DynamicallyInvokable]
    public TaskCompletionSource(object state)
      : this(state, TaskCreationOptions.None)
    {
    }

    /// <summary>使用指定的状态和选项创建一个 <see cref="T:System.Threading.Tasks.TaskCompletionSource`1" />。</summary>
    /// <param name="state">要用作基础 <see cref="T:System.Threading.Tasks.Task`1" /> 的 AsyncState 的状态。</param>
    /// <param name="creationOptions">创建基础 <see cref="T:System.Threading.Tasks.Task`1" /> 时要使用的选项。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="creationOptions" /> 表示与 <see cref="T:System.Threading.Tasks.TaskCompletionSource`1" /> 一起使用时无效的选项。</exception>
    [__DynamicallyInvokable]
    public TaskCompletionSource(object state, TaskCreationOptions creationOptions)
    {
      this.m_task = new System.Threading.Tasks.Task<TResult>(state, creationOptions);
    }

    private void SpinUntilCompleted()
    {
      SpinWait spinWait = new SpinWait();
      while (!this.m_task.IsCompleted)
        spinWait.SpinOnce();
    }

    /// <summary>尝试将基础 <see cref="T:System.Threading.Tasks.Task`1" /> 转换为 <see cref="F:System.Threading.Tasks.TaskStatus.Faulted" /> 状态。</summary>
    /// <returns>如果操作成功，则为 true；否则为 false。</returns>
    /// <param name="exception">要绑定到此 <see cref="T:System.Threading.Tasks.Task`1" /> 的异常。</param>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="P:System.Threading.Tasks.TaskCompletionSource`1.Task" /> 已处理。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="exception" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    public bool TrySetException(Exception exception)
    {
      if (exception == null)
        throw new ArgumentNullException("exception");
      int num = this.m_task.TrySetException((object) exception) ? 1 : 0;
      if (num != 0)
        return num != 0;
      if (this.m_task.IsCompleted)
        return num != 0;
      this.SpinUntilCompleted();
      return num != 0;
    }

    /// <summary>尝试将基础 <see cref="T:System.Threading.Tasks.Task`1" /> 转换为 <see cref="F:System.Threading.Tasks.TaskStatus.Faulted" /> 状态。</summary>
    /// <returns>如果操作成功，则为 true；否则为 false。</returns>
    /// <param name="exceptions">要绑定到此 <see cref="T:System.Threading.Tasks.Task`1" /> 的异常的集合。</param>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="P:System.Threading.Tasks.TaskCompletionSource`1.Task" /> 已处理。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="exceptions" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">在 <paramref name="exceptions" /> 中有一个或多个 null 元素。- 或 -<paramref name="exceptions" /> 集合是空的。</exception>
    [__DynamicallyInvokable]
    public bool TrySetException(IEnumerable<Exception> exceptions)
    {
      if (exceptions == null)
        throw new ArgumentNullException("exceptions");
      List<Exception> exceptionList = new List<Exception>();
      foreach (Exception exception in exceptions)
      {
        if (exception == null)
          throw new ArgumentException(Environment.GetResourceString("TaskCompletionSourceT_TrySetException_NullException"), "exceptions");
        exceptionList.Add(exception);
      }
      if (exceptionList.Count == 0)
        throw new ArgumentException(Environment.GetResourceString("TaskCompletionSourceT_TrySetException_NoExceptions"), "exceptions");
      int num = this.m_task.TrySetException((object) exceptionList) ? 1 : 0;
      if (num != 0)
        return num != 0;
      if (this.m_task.IsCompleted)
        return num != 0;
      this.SpinUntilCompleted();
      return num != 0;
    }

    internal bool TrySetException(IEnumerable<ExceptionDispatchInfo> exceptions)
    {
      int num = this.m_task.TrySetException((object) exceptions) ? 1 : 0;
      if (num != 0)
        return num != 0;
      if (this.m_task.IsCompleted)
        return num != 0;
      this.SpinUntilCompleted();
      return num != 0;
    }

    /// <summary>将基础 <see cref="T:System.Threading.Tasks.Task`1" /> 转换为 <see cref="F:System.Threading.Tasks.TaskStatus.Faulted" /> 状态。</summary>
    /// <param name="exception">要绑定到此 <see cref="T:System.Threading.Tasks.Task`1" /> 的异常。</param>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="P:System.Threading.Tasks.TaskCompletionSource`1.Task" /> 已处理。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="exception" /> 参数为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">基础 <see cref="T:System.Threading.Tasks.Task`1" /> 已经处于三个最终状态之一： <see cref="F:System.Threading.Tasks.TaskStatus.RanToCompletion" />、<see cref="F:System.Threading.Tasks.TaskStatus.Faulted" /> 或 <see cref="F:System.Threading.Tasks.TaskStatus.Canceled" />。</exception>
    [__DynamicallyInvokable]
    public void SetException(Exception exception)
    {
      if (exception == null)
        throw new ArgumentNullException("exception");
      if (!this.TrySetException(exception))
        throw new InvalidOperationException(Environment.GetResourceString("TaskT_TransitionToFinal_AlreadyCompleted"));
    }

    /// <summary>将基础 <see cref="T:System.Threading.Tasks.Task`1" /> 转换为 <see cref="F:System.Threading.Tasks.TaskStatus.Faulted" /> 状态。</summary>
    /// <param name="exceptions">要绑定到此 <see cref="T:System.Threading.Tasks.Task`1" /> 的异常的集合。</param>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="P:System.Threading.Tasks.TaskCompletionSource`1.Task" /> 已处理。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="exceptions" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">在 <paramref name="exceptions" /> 中有一个或多个 null 元素。</exception>
    /// <exception cref="T:System.InvalidOperationException">基础 <see cref="T:System.Threading.Tasks.Task`1" /> 已经处于三个最终状态之一： <see cref="F:System.Threading.Tasks.TaskStatus.RanToCompletion" />、<see cref="F:System.Threading.Tasks.TaskStatus.Faulted" /> 或 <see cref="F:System.Threading.Tasks.TaskStatus.Canceled" />。</exception>
    [__DynamicallyInvokable]
    public void SetException(IEnumerable<Exception> exceptions)
    {
      if (!this.TrySetException(exceptions))
        throw new InvalidOperationException(Environment.GetResourceString("TaskT_TransitionToFinal_AlreadyCompleted"));
    }

    /// <summary>尝试将基础 <see cref="T:System.Threading.Tasks.Task`1" /> 转换为 <see cref="F:System.Threading.Tasks.TaskStatus.RanToCompletion" /> 状态。</summary>
    /// <returns>如果操作成功，则为 true；否则为 false。</returns>
    /// <param name="result">要绑定到此 <see cref="T:System.Threading.Tasks.Task`1" /> 的结果值。</param>
    [__DynamicallyInvokable]
    public bool TrySetResult(TResult result)
    {
      int num = this.m_task.TrySetResult(result) ? 1 : 0;
      if (num != 0)
        return num != 0;
      if (this.m_task.IsCompleted)
        return num != 0;
      this.SpinUntilCompleted();
      return num != 0;
    }

    /// <summary>将基础 <see cref="T:System.Threading.Tasks.Task`1" /> 转换为 <see cref="F:System.Threading.Tasks.TaskStatus.RanToCompletion" /> 状态。</summary>
    /// <param name="result">要绑定到此 <see cref="T:System.Threading.Tasks.Task`1" /> 的结果值。</param>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="P:System.Threading.Tasks.TaskCompletionSource`1.Task" /> 已处理。</exception>
    /// <exception cref="T:System.InvalidOperationException">基础 <see cref="T:System.Threading.Tasks.Task`1" /> 已经处于三个最终状态之一： <see cref="F:System.Threading.Tasks.TaskStatus.RanToCompletion" />、<see cref="F:System.Threading.Tasks.TaskStatus.Faulted" /> 或 <see cref="F:System.Threading.Tasks.TaskStatus.Canceled" />。</exception>
    [__DynamicallyInvokable]
    public void SetResult(TResult result)
    {
      if (!this.TrySetResult(result))
        throw new InvalidOperationException(Environment.GetResourceString("TaskT_TransitionToFinal_AlreadyCompleted"));
    }

    /// <summary>尝试将基础 <see cref="T:System.Threading.Tasks.Task`1" /> 转换为 <see cref="F:System.Threading.Tasks.TaskStatus.Canceled" /> 状态。</summary>
    /// <returns>如果操作成功，则为 true；如果操作失败或对象已被释放，则为 false。</returns>
    [__DynamicallyInvokable]
    public bool TrySetCanceled()
    {
      return this.TrySetCanceled(new CancellationToken());
    }

    /// <summary>尝试，则过渡基础<see cref="T:System.Threading.Tasks.Task`1" />到<see cref="F:System.Threading.Tasks.TaskStatus.Canceled" />状态，并使用于存储在已取消的任务的取消标记。</summary>
    /// <returns>如果操作成功，则为 true；否则为 false。</returns>
    /// <param name="cancellationToken">取消标记。 </param>
    [__DynamicallyInvokable]
    public bool TrySetCanceled(CancellationToken cancellationToken)
    {
      int num = this.m_task.TrySetCanceled(cancellationToken) ? 1 : 0;
      if (num != 0)
        return num != 0;
      if (this.m_task.IsCompleted)
        return num != 0;
      this.SpinUntilCompleted();
      return num != 0;
    }

    /// <summary>将基础 <see cref="T:System.Threading.Tasks.Task`1" /> 转换为 <see cref="F:System.Threading.Tasks.TaskStatus.Canceled" /> 状态。</summary>
    /// <exception cref="T:System.InvalidOperationException">基础 <see cref="T:System.Threading.Tasks.Task`1" /> 已经处于三个最终状态之一： <see cref="F:System.Threading.Tasks.TaskStatus.RanToCompletion" />、 <see cref="F:System.Threading.Tasks.TaskStatus.Faulted" /> 或 <see cref="F:System.Threading.Tasks.TaskStatus.Canceled" />；或者基础 <see cref="T:System.Threading.Tasks.Task`1" /> 已被释放。</exception>
    [__DynamicallyInvokable]
    public void SetCanceled()
    {
      if (!this.TrySetCanceled())
        throw new InvalidOperationException(Environment.GetResourceString("TaskT_TransitionToFinal_AlreadyCompleted"));
    }
  }
}
