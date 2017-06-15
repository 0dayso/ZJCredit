// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.ConfiguredTaskAwaitable
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
  /// <summary>提供用于启用对任务的已配置等待的可等待对象。</summary>
  [__DynamicallyInvokable]
  public struct ConfiguredTaskAwaitable
  {
    private readonly ConfiguredTaskAwaitable.ConfiguredTaskAwaiter m_configuredTaskAwaiter;

    internal ConfiguredTaskAwaitable(Task task, bool continueOnCapturedContext)
    {
      this.m_configuredTaskAwaiter = new ConfiguredTaskAwaitable.ConfiguredTaskAwaiter(task, continueOnCapturedContext);
    }

    /// <summary>返回此可等待对象的 Awaiter。</summary>
    /// <returns>awaiter。</returns>
    [__DynamicallyInvokable]
    public ConfiguredTaskAwaitable.ConfiguredTaskAwaiter GetAwaiter()
    {
      return this.m_configuredTaskAwaiter;
    }

    /// <summary>提供可等待 (<see cref="T:System.Runtime.CompilerServices.ConfiguredTaskAwaitable" />) 对象的 Awaiter。</summary>
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
    public struct ConfiguredTaskAwaiter : ICriticalNotifyCompletion, INotifyCompletion
    {
      private readonly Task m_task;
      private readonly bool m_continueOnCapturedContext;

      /// <summary>获取指定长期任务是否复杂的值。</summary>
      /// <returns>如果已成功完成等待任务，则为 true；否则为 false。</returns>
      /// <exception cref="T:System.NullReferenceException">等待未正确地初始化。</exception>
      [__DynamicallyInvokable]
      public bool IsCompleted
      {
        [__DynamicallyInvokable] get
        {
          return this.m_task.IsCompleted;
        }
      }

      internal ConfiguredTaskAwaiter(Task task, bool continueOnCapturedContext)
      {
        this.m_task = task;
        this.m_continueOnCapturedContext = continueOnCapturedContext;
      }

      /// <summary>为与此 awaiter 关联的任务计划延续操作。</summary>
      /// <param name="continuation">在等待操作完成时要调用的操作。</param>
      /// <exception cref="T:System.ArgumentNullException">
      /// <paramref name="continuation" /> 参数为 null。</exception>
      /// <exception cref="T:System.NullReferenceException">等待未正确地初始化。</exception>
      [SecuritySafeCritical]
      [__DynamicallyInvokable]
      public void OnCompleted(Action continuation)
      {
        TaskAwaiter.OnCompletedInternal(this.m_task, continuation, this.m_continueOnCapturedContext, true);
      }

      /// <summary>为与此 awaiter 关联的任务计划延续操作。</summary>
      /// <param name="continuation">在等待操作完成时要调用的操作。</param>
      /// <exception cref="T:System.ArgumentNullException">
      /// <paramref name="continuation" /> 参数为 null。</exception>
      /// <exception cref="T:System.NullReferenceException">等待未正确地初始化。</exception>
      [SecurityCritical]
      [__DynamicallyInvokable]
      public void UnsafeOnCompleted(Action continuation)
      {
        TaskAwaiter.OnCompletedInternal(this.m_task, continuation, this.m_continueOnCapturedContext, false);
      }

      /// <summary>结束对已完成任务的等待。</summary>
      /// <exception cref="T:System.NullReferenceException">等待未正确地初始化。</exception>
      /// <exception cref="T:System.Threading.Tasks.TaskCanceledException">已取消的任务。</exception>
      /// <exception cref="T:System.Exception">该任务在出错状态下完成。</exception>
      [__DynamicallyInvokable]
      public void GetResult()
      {
        TaskAwaiter.ValidateEnd(this.m_task);
      }
    }
  }
}
