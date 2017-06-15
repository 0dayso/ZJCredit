// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.TaskAwaiter`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
  /// <summary>表示等待完成的异步任务的对象，并提供结果的参数。</summary>
  /// <typeparam name="TResult">任务的结果。</typeparam>
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public struct TaskAwaiter<TResult> : ICriticalNotifyCompletion, INotifyCompletion
  {
    private readonly Task<TResult> m_task;

    /// <summary>获取一个值，该值指示异步任务是否已完成。</summary>
    /// <returns>如果该任务已完成，则为 true；否则为 false。</returns>
    /// <exception cref="T:System.NullReferenceException">
    /// <see cref="T:System.Runtime.CompilerServices.TaskAwaiter`1" /> 对象未正确地初始化。</exception>
    [__DynamicallyInvokable]
    public bool IsCompleted
    {
      [__DynamicallyInvokable] get
      {
        return this.m_task.IsCompleted;
      }
    }

    internal TaskAwaiter(Task<TResult> task)
    {
      this.m_task = task;
    }

    /// <summary>将操作设置为当 <see cref="T:System.Runtime.CompilerServices.TaskAwaiter`1" /> 对象停止等待异步任务完成时执行。</summary>
    /// <param name="continuation">在等待操作完成时要执行的操作。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="continuation" /> 为 null。</exception>
    /// <exception cref="T:System.NullReferenceException">
    /// <see cref="T:System.Runtime.CompilerServices.TaskAwaiter`1" /> 对象未正确地初始化。</exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public void OnCompleted(Action continuation)
    {
      TaskAwaiter.OnCompletedInternal((Task) this.m_task, continuation, true, true);
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
      TaskAwaiter.OnCompletedInternal((Task) this.m_task, continuation, true, false);
    }

    /// <summary>异步任务完成后关闭等待任务。</summary>
    /// <returns>已完成任务的结果。</returns>
    /// <exception cref="T:System.NullReferenceException">
    /// <see cref="T:System.Runtime.CompilerServices.TaskAwaiter`1" /> 对象未正确地初始化。</exception>
    /// <exception cref="T:System.Threading.Tasks.TaskCanceledException">已取消的任务。</exception>
    /// <exception cref="T:System.Exception">在 <see cref="F:System.Threading.Tasks.TaskStatus.Faulted" /> 状态中完成的任务。</exception>
    [__DynamicallyInvokable]
    public TResult GetResult()
    {
      TaskAwaiter.ValidateEnd((Task) this.m_task);
      return this.m_task.ResultOnSuccess;
    }
  }
}
