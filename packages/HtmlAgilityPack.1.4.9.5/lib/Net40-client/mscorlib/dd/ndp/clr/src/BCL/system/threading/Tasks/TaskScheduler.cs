// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.TaskScheduler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;

namespace System.Threading.Tasks
{
  /// <summary>表示一个处理将任务排队到线程中的低级工作的对象。</summary>
  [DebuggerDisplay("Id={Id}")]
  [DebuggerTypeProxy(typeof (TaskScheduler.SystemThreadingTasks_TaskSchedulerDebugView))]
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  [PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
  public abstract class TaskScheduler
  {
    private static readonly ConditionalWeakTable<TaskScheduler, object> s_activeTaskSchedulers = new ConditionalWeakTable<TaskScheduler, object>();
    private static readonly TaskScheduler s_defaultTaskScheduler = (TaskScheduler) new ThreadPoolTaskScheduler();
    private static readonly object _unobservedTaskExceptionLockObject = new object();
    internal static int s_taskSchedulerIdCounter;
    private volatile int m_taskSchedulerId;
    private static EventHandler<UnobservedTaskExceptionEventArgs> _unobservedTaskException;

    /// <summary>指示此 <see cref="T:System.Threading.Tasks.TaskScheduler" /> 能够支持的最大并发级别。</summary>
    /// <returns>返回表示最大并发级别的一个整数。默认计划程序返回 <see cref="F:System.Int32.MaxValue" />。</returns>
    [__DynamicallyInvokable]
    public virtual int MaximumConcurrencyLevel
    {
      [__DynamicallyInvokable] get
      {
        return int.MaxValue;
      }
    }

    internal virtual bool RequiresAtomicStartTransition
    {
      get
      {
        return true;
      }
    }

    /// <summary>获取由 .NET Framework 提供的默认 <see cref="T:System.Threading.Tasks.TaskScheduler" /> 实例。</summary>
    /// <returns>返回默认的 <see cref="T:System.Threading.Tasks.TaskScheduler" /> 实例。</returns>
    [__DynamicallyInvokable]
    public static TaskScheduler Default
    {
      [__DynamicallyInvokable] get
      {
        return TaskScheduler.s_defaultTaskScheduler;
      }
    }

    /// <summary>获取与当前正在执行的任务关联的 <see cref="T:System.Threading.Tasks.TaskScheduler" />。</summary>
    /// <returns>返回与当前正在执行的任务关联的 <see cref="T:System.Threading.Tasks.TaskScheduler" />。</returns>
    [__DynamicallyInvokable]
    public static TaskScheduler Current
    {
      [__DynamicallyInvokable] get
      {
        return TaskScheduler.InternalCurrent ?? TaskScheduler.Default;
      }
    }

    internal static TaskScheduler InternalCurrent
    {
      get
      {
        Task internalCurrent = Task.InternalCurrent;
        if (internalCurrent == null || (internalCurrent.CreationOptions & TaskCreationOptions.HideScheduler) != TaskCreationOptions.None)
          return (TaskScheduler) null;
        return internalCurrent.ExecutingTaskScheduler;
      }
    }

    /// <summary>获取此 <see cref="T:System.Threading.Tasks.TaskScheduler" /> 的唯一 ID。</summary>
    /// <returns>返回此 <see cref="T:System.Threading.Tasks.TaskScheduler" /> 的唯一 ID。</returns>
    [__DynamicallyInvokable]
    public int Id
    {
      [__DynamicallyInvokable] get
      {
        if (this.m_taskSchedulerId == 0)
        {
          int num;
          do
          {
            num = Interlocked.Increment(ref TaskScheduler.s_taskSchedulerIdCounter);
          }
          while (num == 0);
          Interlocked.CompareExchange(ref this.m_taskSchedulerId, num, 0);
        }
        return this.m_taskSchedulerId;
      }
    }

    /// <summary>当出错的任务的未观察到的异常将要触发异常升级策略时发生，默认情况下，这将终止进程。</summary>
    [__DynamicallyInvokable]
    public static event EventHandler<UnobservedTaskExceptionEventArgs> UnobservedTaskException
    {
      [SecurityCritical, __DynamicallyInvokable] add
      {
        if (value == null)
          return;
        RuntimeHelpers.PrepareContractedDelegate((Delegate) value);
        lock (TaskScheduler._unobservedTaskExceptionLockObject)
          TaskScheduler._unobservedTaskException += value;
      }
      [SecurityCritical, __DynamicallyInvokable] remove
      {
        lock (TaskScheduler._unobservedTaskExceptionLockObject)
          TaskScheduler._unobservedTaskException -= value;
      }
    }

    /// <summary>初始化 <see cref="T:System.Threading.Tasks.TaskScheduler" />。</summary>
    [__DynamicallyInvokable]
    protected TaskScheduler()
    {
      TaskScheduler.s_activeTaskSchedulers.Add(this, (object) null);
    }

    /// <summary>将 <see cref="T:System.Threading.Tasks.Task" /> 排队到计划程序中。</summary>
    /// <param name="task">要排队的 <see cref="T:System.Threading.Tasks.Task" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="task" /> 参数为 null。</exception>
    [SecurityCritical]
    [__DynamicallyInvokable]
    protected internal abstract void QueueTask(Task task);

    /// <summary>确定是否可以在此调用中同步执行提供的 <see cref="T:System.Threading.Tasks.Task" />，如果可以，将执行该任务。</summary>
    /// <returns>一个布尔值，该值指示是否已以内联方式执行该任务。</returns>
    /// <param name="task">要执行的 <see cref="T:System.Threading.Tasks.Task" />。</param>
    /// <param name="taskWasPreviouslyQueued">一个布尔值，该值指示任务之前是否已排队。如果此参数为 True，则该任务以前可能已排队（已计划）；如果为 False，则已知该任务尚未排队，此时将执行此调用，以便以内联方式执行该任务，而不用将其排队。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="task" /> 参数为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">已执行的 <paramref name="task" />。</exception>
    [SecurityCritical]
    [__DynamicallyInvokable]
    protected abstract bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued);

    /// <summary>仅对于调试器支持，生成当前排队到计划程序中等待执行的 <see cref="T:System.Threading.Tasks.Task" /> 实例的枚举。</summary>
    /// <returns>一个允许调试器遍历当前排队到此计划程序中的任务的枚举。</returns>
    /// <exception cref="T:System.NotSupportedException">此计划程序无法在此时生成排队任务的列表。</exception>
    [SecurityCritical]
    [__DynamicallyInvokable]
    protected abstract IEnumerable<Task> GetScheduledTasks();

    [SecuritySafeCritical]
    internal bool TryRunInline(Task task, bool taskWasPreviouslyQueued)
    {
      TaskScheduler executingTaskScheduler = task.ExecutingTaskScheduler;
      if (executingTaskScheduler != this && executingTaskScheduler != null)
        return executingTaskScheduler.TryRunInline(task, taskWasPreviouslyQueued);
      StackGuard currentStackGuard;
      if (executingTaskScheduler == null || task.m_action == null || (task.IsDelegateInvoked || task.IsCanceled) || !(currentStackGuard = Task.CurrentStackGuard).TryBeginInliningScope())
        return false;
      bool flag = false;
      try
      {
        task.FireTaskScheduledIfNeeded(this);
        flag = this.TryExecuteTaskInline(task, taskWasPreviouslyQueued);
      }
      finally
      {
        currentStackGuard.EndInliningScope();
      }
      if (flag && !task.IsDelegateInvoked && !task.IsCanceled)
        throw new InvalidOperationException(Environment.GetResourceString("TaskScheduler_InconsistentStateAfterTryExecuteTaskInline"));
      return flag;
    }

    /// <summary>尝试将以前排队到此计划程序中的 <see cref="T:System.Threading.Tasks.Task" /> 取消排队。</summary>
    /// <returns>一个布尔值，该值指示是否已成功地将 <paramref name="task" /> 参数取消排队。</returns>
    /// <param name="task">要取消排队的 <see cref="T:System.Threading.Tasks.Task" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="task" /> 参数为 null。</exception>
    [SecurityCritical]
    [__DynamicallyInvokable]
    protected internal virtual bool TryDequeue(Task task)
    {
      return false;
    }

    internal virtual void NotifyWorkItemProgress()
    {
    }

    [SecurityCritical]
    internal void InternalQueueTask(Task task)
    {
      task.FireTaskScheduledIfNeeded(this);
      this.QueueTask(task);
    }

    /// <summary>创建一个与当前 <see cref="T:System.Threading.SynchronizationContext" /> 关联的 <see cref="T:System.Threading.Tasks.TaskScheduler" />。</summary>
    /// <returns>与由 <see cref="P:System.Threading.SynchronizationContext.Current" /> 确定的当前 <see cref="T:System.Threading.SynchronizationContext" /> 关联的 <see cref="T:System.Threading.Tasks.TaskScheduler" />。</returns>
    /// <exception cref="T:System.InvalidOperationException">当前的 SynchronizationContext 不能用作 TaskScheduler。</exception>
    [__DynamicallyInvokable]
    public static TaskScheduler FromCurrentSynchronizationContext()
    {
      return (TaskScheduler) new SynchronizationContextTaskScheduler();
    }

    /// <summary>尝试在此计划程序上执行提供的 <see cref="T:System.Threading.Tasks.Task" />。</summary>
    /// <returns>一个布尔值，如果成功执行了 <paramref name="task" />，则该值为 true；如果未成功执行，则该值为 false。执行失败的常见原因是，该任务先前已经执行或者位于正在由另一个线程执行的进程中。</returns>
    /// <param name="task">要执行的 <see cref="T:System.Threading.Tasks.Task" /> 对象。</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="task" /> 与此计划程序无关联。</exception>
    [SecurityCritical]
    [__DynamicallyInvokable]
    protected bool TryExecuteTask(Task task)
    {
      if (task.ExecutingTaskScheduler != this)
        throw new InvalidOperationException(Environment.GetResourceString("TaskScheduler_ExecuteTask_WrongTaskScheduler"));
      return task.ExecuteEntry(true);
    }

    internal static void PublishUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs ueea)
    {
      lock (TaskScheduler._unobservedTaskExceptionLockObject)
      {
        EventHandler<UnobservedTaskExceptionEventArgs> local_2 = TaskScheduler._unobservedTaskException;
        if (local_2 == null)
          return;
        local_2(sender, ueea);
      }
    }

    [SecurityCritical]
    internal Task[] GetScheduledTasksForDebugger()
    {
      IEnumerable<Task> scheduledTasks = this.GetScheduledTasks();
      if (scheduledTasks == null)
        return (Task[]) null;
      Task[] taskArray = scheduledTasks as Task[] ?? new List<Task>(scheduledTasks).ToArray();
      foreach (Task task in taskArray)
      {
        int id = task.Id;
      }
      return taskArray;
    }

    [SecurityCritical]
    internal static TaskScheduler[] GetTaskSchedulersForDebugger()
    {
      ICollection<TaskScheduler> keys = TaskScheduler.s_activeTaskSchedulers.Keys;
      TaskScheduler[] taskSchedulerArray = new TaskScheduler[keys.Count];
      TaskScheduler[] array = taskSchedulerArray;
      int arrayIndex = 0;
      keys.CopyTo(array, arrayIndex);
      foreach (TaskScheduler taskScheduler in taskSchedulerArray)
      {
        int id = taskScheduler.Id;
      }
      return taskSchedulerArray;
    }

    internal sealed class SystemThreadingTasks_TaskSchedulerDebugView
    {
      private readonly TaskScheduler m_taskScheduler;

      public int Id
      {
        get
        {
          return this.m_taskScheduler.Id;
        }
      }

      public IEnumerable<Task> ScheduledTasks
      {
        [SecurityCritical] get
        {
          return this.m_taskScheduler.GetScheduledTasks();
        }
      }

      public SystemThreadingTasks_TaskSchedulerDebugView(TaskScheduler scheduler)
      {
        this.m_taskScheduler = scheduler;
      }
    }
  }
}
