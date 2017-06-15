// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.Task
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Security;
using System.Security.Permissions;

namespace System.Threading.Tasks
{
  /// <summary>表示一个异步操作。若要浏览此类型的.NET Framework 源代码，请参阅 Reference Source。</summary>
  [DebuggerTypeProxy(typeof (SystemThreadingTasks_TaskDebugView))]
  [DebuggerDisplay("Id = {Id}, Status = {Status}, Method = {DebuggerDisplayMethodDescription}")]
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public class Task : IThreadPoolWorkItem, IAsyncResult, IDisposable
  {
    private static readonly TaskFactory s_factory = new TaskFactory();
    private static readonly object s_taskCompletionSentinel = new object();
    private static readonly Dictionary<int, Task> s_currentActiveTasks = new Dictionary<int, Task>();
    private static readonly object s_activeTasksLock = new object();
    private static readonly Action<object> s_taskCancelCallback = new Action<object>(Task.TaskCancelCallback);
    private static readonly Func<Task.ContingentProperties> s_createContingentProperties = new Func<Task.ContingentProperties>(Task.\u003C\u003Ec.\u003C\u003E9.\u003C\u002Ecctor\u003Eb__293_0);
    private static readonly Predicate<Task> s_IsExceptionObservedByParentPredicate = new Predicate<Task>(Task.\u003C\u003Ec.\u003C\u003E9.\u003C\u002Ecctor\u003Eb__293_1);
    private static readonly Predicate<object> s_IsTaskContinuationNullPredicate = new Predicate<object>(Task.\u003C\u003Ec.\u003C\u003E9.\u003C\u002Ecctor\u003Eb__293_2);
    [ThreadStatic]
    internal static Task t_currentTask;
    [ThreadStatic]
    private static StackGuard t_stackGuard;
    internal static int s_taskIdCounter;
    private volatile int m_taskId;
    internal object m_action;
    internal object m_stateObject;
    internal TaskScheduler m_taskScheduler;
    internal readonly Task m_parent;
    internal volatile int m_stateFlags;
    private const int OptionsMask = 65535;
    internal const int TASK_STATE_STARTED = 65536;
    internal const int TASK_STATE_DELEGATE_INVOKED = 131072;
    internal const int TASK_STATE_DISPOSED = 262144;
    internal const int TASK_STATE_EXCEPTIONOBSERVEDBYPARENT = 524288;
    internal const int TASK_STATE_CANCELLATIONACKNOWLEDGED = 1048576;
    internal const int TASK_STATE_FAULTED = 2097152;
    internal const int TASK_STATE_CANCELED = 4194304;
    internal const int TASK_STATE_WAITING_ON_CHILDREN = 8388608;
    internal const int TASK_STATE_RAN_TO_COMPLETION = 16777216;
    internal const int TASK_STATE_WAITINGFORACTIVATION = 33554432;
    internal const int TASK_STATE_COMPLETION_RESERVED = 67108864;
    internal const int TASK_STATE_THREAD_WAS_ABORTED = 134217728;
    internal const int TASK_STATE_WAIT_COMPLETION_NOTIFICATION = 268435456;
    internal const int TASK_STATE_EXECUTIONCONTEXT_IS_NULL = 536870912;
    internal const int TASK_STATE_TASKSCHEDULED_WAS_FIRED = 1073741824;
    private const int TASK_STATE_COMPLETED_MASK = 23068672;
    private const int CANCELLATION_REQUESTED = 1;
    private volatile object m_continuationObject;
    [FriendAccessAllowed]
    internal static bool s_asyncDebuggingEnabled;
    internal volatile Task.ContingentProperties m_contingentProperties;
    private static Task s_completedTask;
    [SecurityCritical]
    private static ContextCallback s_ecCallback;

    private string DebuggerDisplayMethodDescription
    {
      get
      {
        Delegate @delegate = (Delegate) this.m_action;
        if (@delegate == null)
          return "{null}";
        return @delegate.Method.ToString();
      }
    }

    internal TaskCreationOptions Options
    {
      get
      {
        return Task.OptionsMethod(this.m_stateFlags);
      }
    }

    internal bool IsWaitNotificationEnabledOrNotRanToCompletion
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        return (this.m_stateFlags & 285212672) != 16777216;
      }
    }

    internal virtual bool ShouldNotifyDebuggerOfWaitCompletion
    {
      get
      {
        return this.IsWaitNotificationEnabled;
      }
    }

    internal bool IsWaitNotificationEnabled
    {
      get
      {
        return (uint) (this.m_stateFlags & 268435456) > 0U;
      }
    }

    /// <summary>获取此 <see cref="T:System.Threading.Tasks.Task" /> 实例的 ID。</summary>
    /// <returns>系统分配到此 <see cref="T:System.Threading.Tasks.Task" /> 实例的标识符。</returns>
    [__DynamicallyInvokable]
    public int Id
    {
      [__DynamicallyInvokable] get
      {
        if (this.m_taskId == 0)
          Interlocked.CompareExchange(ref this.m_taskId, Task.NewId(), 0);
        return this.m_taskId;
      }
    }

    /// <summary>返回当前正在执行 <see cref="T:System.Threading.Tasks.Task" /> 的 ID。</summary>
    /// <returns>系统分配给当前正在执行的任务的一个整数。</returns>
    [__DynamicallyInvokable]
    public static int? CurrentId
    {
      [__DynamicallyInvokable] get
      {
        Task internalCurrent = Task.InternalCurrent;
        if (internalCurrent != null)
          return new int?(internalCurrent.Id);
        return new int?();
      }
    }

    internal static Task InternalCurrent
    {
      get
      {
        return Task.t_currentTask;
      }
    }

    internal static StackGuard CurrentStackGuard
    {
      get
      {
        StackGuard stackGuard = Task.t_stackGuard;
        if (stackGuard == null)
          Task.t_stackGuard = stackGuard = new StackGuard();
        return stackGuard;
      }
    }

    /// <summary>获取导致 <see cref="T:System.AggregateException" /> 提前结束的 <see cref="T:System.Threading.Tasks.Task" />。如果 <see cref="T:System.Threading.Tasks.Task" /> 成功完成或尚未引发任何异常，这将返回 null。</summary>
    /// <returns>导致 <see cref="T:System.AggregateException" /> 提前结束的 <see cref="T:System.Threading.Tasks.Task" />。</returns>
    [__DynamicallyInvokable]
    public AggregateException Exception
    {
      [__DynamicallyInvokable] get
      {
        AggregateException aggregateException = (AggregateException) null;
        if (this.IsFaulted)
          aggregateException = this.GetExceptions(false);
        return aggregateException;
      }
    }

    /// <summary>获取此任务的 <see cref="T:System.Threading.Tasks.TaskStatus" />。</summary>
    /// <returns>此任务实例的当前 <see cref="T:System.Threading.Tasks.TaskStatus" />。</returns>
    [__DynamicallyInvokable]
    public TaskStatus Status
    {
      [__DynamicallyInvokable] get
      {
        int num = this.m_stateFlags;
        return (num & 2097152) == 0 ? ((num & 4194304) == 0 ? ((num & 16777216) == 0 ? ((num & 8388608) == 0 ? ((num & 131072) == 0 ? ((num & 65536) == 0 ? ((num & 33554432) == 0 ? TaskStatus.Created : TaskStatus.WaitingForActivation) : TaskStatus.WaitingToRun) : TaskStatus.Running) : TaskStatus.WaitingForChildrenToComplete) : TaskStatus.RanToCompletion) : TaskStatus.Canceled) : TaskStatus.Faulted;
      }
    }

    /// <summary>获取此 <see cref="T:System.Threading.Tasks.Task" /> 实例是否由于被取消的原因而已完成执行。</summary>
    /// <returns>如果任务由于被取消而完成，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool IsCanceled
    {
      [__DynamicallyInvokable] get
      {
        return (this.m_stateFlags & 6291456) == 4194304;
      }
    }

    internal bool IsCancellationRequested
    {
      get
      {
        Task.ContingentProperties contingentProperties = this.m_contingentProperties;
        if (contingentProperties == null)
          return false;
        if (contingentProperties.m_internalCancellationRequested != 1)
          return contingentProperties.m_cancellationToken.IsCancellationRequested;
        return true;
      }
    }

    internal CancellationToken CancellationToken
    {
      get
      {
        Task.ContingentProperties contingentProperties = this.m_contingentProperties;
        if (contingentProperties != null)
          return contingentProperties.m_cancellationToken;
        return new CancellationToken();
      }
    }

    internal bool IsCancellationAcknowledged
    {
      get
      {
        return (uint) (this.m_stateFlags & 1048576) > 0U;
      }
    }

    /// <summary>获取此 <see cref="T:System.Threading.Tasks.Task" /> 是否已完成。</summary>
    /// <returns>如果任务已完成，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool IsCompleted
    {
      [__DynamicallyInvokable] get
      {
        return Task.IsCompletedMethod(this.m_stateFlags);
      }
    }

    internal bool IsRanToCompletion
    {
      get
      {
        return (this.m_stateFlags & 23068672) == 16777216;
      }
    }

    /// <summary>获取用于创建此任务的 <see cref="T:System.Threading.Tasks.TaskCreationOptions" />。</summary>
    /// <returns>用于创建此任务的 <see cref="T:System.Threading.Tasks.TaskCreationOptions" />。</returns>
    [__DynamicallyInvokable]
    public TaskCreationOptions CreationOptions
    {
      [__DynamicallyInvokable] get
      {
        return this.Options & (TaskCreationOptions) -65281;
      }
    }

    [__DynamicallyInvokable]
    WaitHandle IAsyncResult.AsyncWaitHandle
    {
      [__DynamicallyInvokable] get
      {
        if ((uint) (this.m_stateFlags & 262144) > 0U)
          throw new ObjectDisposedException((string) null, Environment.GetResourceString("Task_ThrowIfDisposed"));
        return this.CompletedEvent.WaitHandle;
      }
    }

    /// <summary>获取在创建 <see cref="T:System.Threading.Tasks.Task" /> 时提供的状态对象，如果未提供，则为 null。</summary>
    /// <returns>一个 <see cref="T:System.Object" />，表示在创建任务时传递给该任务的状态数据。</returns>
    [__DynamicallyInvokable]
    public object AsyncState
    {
      [__DynamicallyInvokable] get
      {
        return this.m_stateObject;
      }
    }

    [__DynamicallyInvokable]
    bool IAsyncResult.CompletedSynchronously
    {
      [__DynamicallyInvokable] get
      {
        return false;
      }
    }

    internal TaskScheduler ExecutingTaskScheduler
    {
      get
      {
        return this.m_taskScheduler;
      }
    }

    /// <summary>提供对用于创建 <see cref="T:System.Threading.Tasks.Task" /> 和 <see cref="T:System.Threading.Tasks.Task`1" /> 的工厂方法的访问。</summary>
    /// <returns>一个工厂对象，可创建多种 <see cref="T:System.Threading.Tasks.Task" /> 和 <see cref="T:System.Threading.Tasks.Task`1" /> 对象。</returns>
    [__DynamicallyInvokable]
    public static TaskFactory Factory
    {
      [__DynamicallyInvokable] get
      {
        return Task.s_factory;
      }
    }

    /// <summary>获取一个已成功完成的任务。</summary>
    /// <returns>已成功完成的任务。</returns>
    [__DynamicallyInvokable]
    public static Task CompletedTask
    {
      [__DynamicallyInvokable] get
      {
        Task task = Task.s_completedTask;
        if (task == null)
        {
          int num1 = 0;
          int num2 = 16384;
          CancellationToken ct = new CancellationToken();
          Task.s_completedTask = task = new Task(num1 != 0, (TaskCreationOptions) num2, ct);
        }
        return task;
      }
    }

    internal ManualResetEventSlim CompletedEvent
    {
      get
      {
        Task.ContingentProperties contingentProperties = this.EnsureContingentPropertiesInitialized(true);
        if (contingentProperties.m_completionEvent == null)
        {
          bool isCompleted = this.IsCompleted;
          ManualResetEventSlim manualResetEventSlim = new ManualResetEventSlim(isCompleted);
          if (Interlocked.CompareExchange<ManualResetEventSlim>(ref contingentProperties.m_completionEvent, manualResetEventSlim, (ManualResetEventSlim) null) != null)
            manualResetEventSlim.Dispose();
          else if (!isCompleted && this.IsCompleted)
            manualResetEventSlim.Set();
        }
        return contingentProperties.m_completionEvent;
      }
    }

    internal bool IsSelfReplicatingRoot
    {
      get
      {
        return (this.Options & (TaskCreationOptions) 2304) == (TaskCreationOptions) 2048;
      }
    }

    internal bool IsChildReplica
    {
      get
      {
        return (uint) (this.Options & (TaskCreationOptions) 256) > 0U;
      }
    }

    internal int ActiveChildCount
    {
      get
      {
        Task.ContingentProperties contingentProperties = this.m_contingentProperties;
        if (contingentProperties == null)
          return 0;
        return contingentProperties.m_completionCountdown - 1;
      }
    }

    internal bool ExceptionRecorded
    {
      get
      {
        Task.ContingentProperties contingentProperties = this.m_contingentProperties;
        if (contingentProperties != null && contingentProperties.m_exceptionsHolder != null)
          return contingentProperties.m_exceptionsHolder.ContainsFaultList;
        return false;
      }
    }

    /// <summary>获取 <see cref="T:System.Threading.Tasks.Task" /> 是否由于未经处理异常的原因而完成。</summary>
    /// <returns>如果任务引发了未经处理的异常，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool IsFaulted
    {
      [__DynamicallyInvokable] get
      {
        return (uint) (this.m_stateFlags & 2097152) > 0U;
      }
    }

    internal ExecutionContext CapturedContext
    {
      get
      {
        if ((this.m_stateFlags & 536870912) == 536870912)
          return (ExecutionContext) null;
        Task.ContingentProperties contingentProperties = this.m_contingentProperties;
        if (contingentProperties != null && contingentProperties.m_capturedContext != null)
          return contingentProperties.m_capturedContext;
        return ExecutionContext.PreAllocatedDefault;
      }
      set
      {
        if (value == null)
        {
          this.m_stateFlags = this.m_stateFlags | 536870912;
        }
        else
        {
          if (value.IsPreAllocatedDefault)
            return;
          this.EnsureContingentPropertiesInitialized(false).m_capturedContext = value;
        }
      }
    }

    internal bool IsExceptionObservedByParent
    {
      get
      {
        return (uint) (this.m_stateFlags & 524288) > 0U;
      }
    }

    internal bool IsDelegateInvoked
    {
      get
      {
        return (uint) (this.m_stateFlags & 131072) > 0U;
      }
    }

    internal virtual object SavedStateForNextReplica
    {
      get
      {
        return (object) null;
      }
      set
      {
      }
    }

    internal virtual object SavedStateFromPreviousReplica
    {
      get
      {
        return (object) null;
      }
      set
      {
      }
    }

    internal virtual Task HandedOverChildReplica
    {
      get
      {
        return (Task) null;
      }
      set
      {
      }
    }

    internal Task(bool canceled, TaskCreationOptions creationOptions, CancellationToken ct)
    {
      int num = (int) creationOptions;
      if (canceled)
      {
        this.m_stateFlags = 5242880 | num;
        Task.ContingentProperties contingentProperties;
        this.m_contingentProperties = contingentProperties = new Task.ContingentProperties();
        contingentProperties.m_cancellationToken = ct;
        contingentProperties.m_internalCancellationRequested = 1;
      }
      else
        this.m_stateFlags = 16777216 | num;
    }

    internal Task()
    {
      this.m_stateFlags = 33555456;
    }

    internal Task(object state, TaskCreationOptions creationOptions, bool promiseStyle)
    {
      if ((creationOptions & ~(TaskCreationOptions.AttachedToParent | TaskCreationOptions.RunContinuationsAsynchronously)) != TaskCreationOptions.None)
        throw new ArgumentOutOfRangeException("creationOptions");
      if ((creationOptions & TaskCreationOptions.AttachedToParent) != TaskCreationOptions.None)
        this.m_parent = Task.InternalCurrent;
      this.TaskConstructorCore((object) null, state, new CancellationToken(), creationOptions, InternalTaskOptions.PromiseTask, (TaskScheduler) null);
    }

    /// <summary>使用指定的操作初始化新的 <see cref="T:System.Threading.Tasks.Task" />。</summary>
    /// <param name="action">表示要在任务中执行的代码的委托。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="action" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task(Action action)
      : this((Delegate) action, (object) null, (Task) null, new CancellationToken(), TaskCreationOptions.None, InternalTaskOptions.None, (TaskScheduler) null)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.PossiblyCaptureContext(ref stackMark);
    }

    /// <summary>使用指定的操作和 <see cref="T:System.Threading.Tasks.Task" /> 初始化新的 <see cref="T:System.Threading.CancellationToken" />。</summary>
    /// <param name="action">表示要在任务中执行的代码的委托。</param>
    /// <param name="cancellationToken">新任务将观察的 <see cref="T:System.Threading.CancellationToken" />。</param>
    /// <exception cref="T:System.ObjectDisposedException">所提供 <see cref="T:System.Threading.CancellationToken" /> 已释放。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="action" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task(Action action, CancellationToken cancellationToken)
      : this((Delegate) action, (object) null, (Task) null, cancellationToken, TaskCreationOptions.None, InternalTaskOptions.None, (TaskScheduler) null)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.PossiblyCaptureContext(ref stackMark);
    }

    /// <summary>使用指定的操作和创建选项初始化新的 <see cref="T:System.Threading.Tasks.Task" />。</summary>
    /// <param name="action">表示要在任务中执行的代码的委托。</param>
    /// <param name="creationOptions">用于自定义任务的行为的 <see cref="T:System.Threading.Tasks.TaskCreationOptions" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="action" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="creationOptions" /> 参数指定的无效值 <see cref="T:System.Threading.Tasks.TaskCreationOptions" />。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task(Action action, TaskCreationOptions creationOptions)
      : this((Delegate) action, (object) null, Task.InternalCurrentIfAttached(creationOptions), new CancellationToken(), creationOptions, InternalTaskOptions.None, (TaskScheduler) null)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.PossiblyCaptureContext(ref stackMark);
    }

    /// <summary>使用指定的操作和创建选项初始化新的 <see cref="T:System.Threading.Tasks.Task" />。</summary>
    /// <param name="action">表示要在任务中执行的代码的委托。</param>
    /// <param name="cancellationToken">新任务将观察的 <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" />。</param>
    /// <param name="creationOptions">用于自定义任务的行为的 <see cref="T:System.Threading.Tasks.TaskCreationOptions" />。</param>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.Threading.CancellationTokenSource" /> 创建 <paramref name="cancellationToken" /> 已释放。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="action" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="creationOptions" /> 参数指定的无效值 <see cref="T:System.Threading.Tasks.TaskCreationOptions" />。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task(Action action, CancellationToken cancellationToken, TaskCreationOptions creationOptions)
      : this((Delegate) action, (object) null, Task.InternalCurrentIfAttached(creationOptions), cancellationToken, creationOptions, InternalTaskOptions.None, (TaskScheduler) null)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.PossiblyCaptureContext(ref stackMark);
    }

    /// <summary>使用指定的操作和状态初始化新的 <see cref="T:System.Threading.Tasks.Task" />。</summary>
    /// <param name="action">表示要在任务中执行的代码的委托。</param>
    /// <param name="state">一个表示由该操作使用的数据的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="action" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task(Action<object> action, object state)
      : this((Delegate) action, state, (Task) null, new CancellationToken(), TaskCreationOptions.None, InternalTaskOptions.None, (TaskScheduler) null)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.PossiblyCaptureContext(ref stackMark);
    }

    /// <summary>使用指定的操作、状态和选项初始化新的 <see cref="T:System.Threading.Tasks.Task" />。</summary>
    /// <param name="action">表示要在任务中执行的代码的委托。</param>
    /// <param name="state">一个表示由该操作使用的数据的对象。</param>
    /// <param name="cancellationToken">新任务将观察的 <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" />。</param>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.Threading.CancellationTokenSource" /> 创建 <paramref name="cancellationToken" /> 已释放。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="action" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task(Action<object> action, object state, CancellationToken cancellationToken)
      : this((Delegate) action, state, (Task) null, cancellationToken, TaskCreationOptions.None, InternalTaskOptions.None, (TaskScheduler) null)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.PossiblyCaptureContext(ref stackMark);
    }

    /// <summary>使用指定的操作、状态和选项初始化新的 <see cref="T:System.Threading.Tasks.Task" />。</summary>
    /// <param name="action">表示要在任务中执行的代码的委托。</param>
    /// <param name="state">一个表示由该操作使用的数据的对象。</param>
    /// <param name="creationOptions">用于自定义任务的行为的 <see cref="T:System.Threading.Tasks.TaskCreationOptions" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="action" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="creationOptions" /> 参数指定的无效值 <see cref="T:System.Threading.Tasks.TaskCreationOptions" />。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task(Action<object> action, object state, TaskCreationOptions creationOptions)
      : this((Delegate) action, state, Task.InternalCurrentIfAttached(creationOptions), new CancellationToken(), creationOptions, InternalTaskOptions.None, (TaskScheduler) null)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.PossiblyCaptureContext(ref stackMark);
    }

    /// <summary>使用指定的操作、状态和选项初始化新的 <see cref="T:System.Threading.Tasks.Task" />。</summary>
    /// <param name="action">表示要在任务中执行的代码的委托。</param>
    /// <param name="state">一个表示由该操作使用的数据的对象。</param>
    /// <param name="cancellationToken">新任务将观察的 <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" />。</param>
    /// <param name="creationOptions">用于自定义任务的行为的 <see cref="T:System.Threading.Tasks.TaskCreationOptions" />。</param>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.Threading.CancellationTokenSource" /> 创建 <paramref name="cancellationToken" /> 已释放。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="action" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="creationOptions" /> 参数指定的无效值 <see cref="T:System.Threading.Tasks.TaskCreationOptions" />。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task(Action<object> action, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions)
      : this((Delegate) action, state, Task.InternalCurrentIfAttached(creationOptions), cancellationToken, creationOptions, InternalTaskOptions.None, (TaskScheduler) null)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.PossiblyCaptureContext(ref stackMark);
    }

    internal Task(Action<object> action, object state, Task parent, CancellationToken cancellationToken, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, TaskScheduler scheduler, ref StackCrawlMark stackMark)
      : this((Delegate) action, state, parent, cancellationToken, creationOptions, internalOptions, scheduler)
    {
      this.PossiblyCaptureContext(ref stackMark);
    }

    internal Task(Delegate action, object state, Task parent, CancellationToken cancellationToken, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, TaskScheduler scheduler)
    {
      if (action == null)
        throw new ArgumentNullException("action");
      if ((creationOptions & TaskCreationOptions.AttachedToParent) != TaskCreationOptions.None || (internalOptions & InternalTaskOptions.SelfReplicating) != InternalTaskOptions.None)
        this.m_parent = parent;
      this.TaskConstructorCore((object) action, state, cancellationToken, creationOptions, internalOptions, scheduler);
    }

    [FriendAccessAllowed]
    internal static bool AddToActiveTasks(Task task)
    {
      lock (Task.s_activeTasksLock)
        Task.s_currentActiveTasks[task.Id] = task;
      return true;
    }

    [FriendAccessAllowed]
    internal static void RemoveFromActiveTasks(int taskId)
    {
      lock (Task.s_activeTasksLock)
        Task.s_currentActiveTasks.Remove(taskId);
    }

    internal void TaskConstructorCore(object action, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, TaskScheduler scheduler)
    {
      this.m_action = action;
      this.m_stateObject = state;
      this.m_taskScheduler = scheduler;
      if ((creationOptions & ~(TaskCreationOptions.PreferFairness | TaskCreationOptions.LongRunning | TaskCreationOptions.AttachedToParent | TaskCreationOptions.DenyChildAttach | TaskCreationOptions.HideScheduler | TaskCreationOptions.RunContinuationsAsynchronously)) != TaskCreationOptions.None)
        throw new ArgumentOutOfRangeException("creationOptions");
      if ((creationOptions & TaskCreationOptions.LongRunning) != TaskCreationOptions.None && (internalOptions & InternalTaskOptions.SelfReplicating) != InternalTaskOptions.None)
        throw new InvalidOperationException(Environment.GetResourceString("Task_ctor_LRandSR"));
      int num = (int) (creationOptions | (TaskCreationOptions) internalOptions);
      if (this.m_action == null || (internalOptions & InternalTaskOptions.ContinuationTask) != InternalTaskOptions.None)
        num |= 33554432;
      this.m_stateFlags = num;
      if (this.m_parent != null && (creationOptions & TaskCreationOptions.AttachedToParent) != TaskCreationOptions.None && (this.m_parent.CreationOptions & TaskCreationOptions.DenyChildAttach) == TaskCreationOptions.None)
        this.m_parent.AddNewChild();
      if (!cancellationToken.CanBeCanceled)
        return;
      this.AssignCancellationToken(cancellationToken, (Task) null, (TaskContinuation) null);
    }

    private void AssignCancellationToken(CancellationToken cancellationToken, Task antecedent, TaskContinuation continuation)
    {
      Task.ContingentProperties contingentProperties = this.EnsureContingentPropertiesInitialized(false);
      contingentProperties.m_cancellationToken = cancellationToken;
      try
      {
        if (AppContextSwitches.ThrowExceptionIfDisposedCancellationTokenSource)
          cancellationToken.ThrowIfSourceDisposed();
        if ((this.Options & (TaskCreationOptions) 13312) != TaskCreationOptions.None)
          return;
        if (cancellationToken.IsCancellationRequested)
        {
          this.InternalCancel(false);
        }
        else
        {
          CancellationTokenRegistration tokenRegistration = antecedent != null ? cancellationToken.InternalRegisterWithoutEC(Task.s_taskCancelCallback, (object) new Tuple<Task, Task, TaskContinuation>(this, antecedent, continuation)) : cancellationToken.InternalRegisterWithoutEC(Task.s_taskCancelCallback, (object) this);
          contingentProperties.m_cancellationRegistration = new Shared<CancellationTokenRegistration>(tokenRegistration);
        }
      }
      catch
      {
        if (this.m_parent != null && (this.Options & TaskCreationOptions.AttachedToParent) != TaskCreationOptions.None && (this.m_parent.Options & TaskCreationOptions.DenyChildAttach) == TaskCreationOptions.None)
          this.m_parent.DisregardChild();
        throw;
      }
    }

    private static void TaskCancelCallback(object o)
    {
      Task task = o as Task;
      if (task == null)
      {
        Tuple<Task, Task, TaskContinuation> tuple = o as Tuple<Task, Task, TaskContinuation>;
        if (tuple != null)
        {
          task = tuple.Item1;
          tuple.Item2.RemoveContinuation((object) tuple.Item3);
        }
      }
      task.InternalCancel(false);
    }

    [SecuritySafeCritical]
    internal void PossiblyCaptureContext(ref StackCrawlMark stackMark)
    {
      this.CapturedContext = ExecutionContext.Capture(ref stackMark, ExecutionContext.CaptureOptions.IgnoreSyncCtx | ExecutionContext.CaptureOptions.OptimizeDefaultCase);
    }

    internal static TaskCreationOptions OptionsMethod(int flags)
    {
      return (TaskCreationOptions) (flags & (int) ushort.MaxValue);
    }

    internal bool AtomicStateUpdate(int newBits, int illegalBits)
    {
      SpinWait spinWait = new SpinWait();
      while (true)
      {
        int comparand = this.m_stateFlags;
        if ((comparand & illegalBits) == 0)
        {
          if (Interlocked.CompareExchange(ref this.m_stateFlags, comparand | newBits, comparand) != comparand)
            spinWait.SpinOnce();
          else
            goto label_4;
        }
        else
          break;
      }
      return false;
label_4:
      return true;
    }

    internal bool AtomicStateUpdate(int newBits, int illegalBits, ref int oldFlags)
    {
      SpinWait spinWait = new SpinWait();
      while (true)
      {
        oldFlags = this.m_stateFlags;
        if ((oldFlags & illegalBits) == 0)
        {
          if (Interlocked.CompareExchange(ref this.m_stateFlags, oldFlags | newBits, oldFlags) != oldFlags)
            spinWait.SpinOnce();
          else
            goto label_4;
        }
        else
          break;
      }
      return false;
label_4:
      return true;
    }

    internal void SetNotificationForWaitCompletion(bool enabled)
    {
      if (enabled)
      {
        this.AtomicStateUpdate(268435456, 90177536);
      }
      else
      {
        SpinWait spinWait = new SpinWait();
        while (true)
        {
          int comparand = this.m_stateFlags;
          if (Interlocked.CompareExchange(ref this.m_stateFlags, comparand & -268435457, comparand) != comparand)
            spinWait.SpinOnce();
          else
            break;
        }
      }
    }

    internal bool NotifyDebuggerOfWaitCompletionIfNecessary()
    {
      if (!this.IsWaitNotificationEnabled || !this.ShouldNotifyDebuggerOfWaitCompletion)
        return false;
      this.NotifyDebuggerOfWaitCompletion();
      return true;
    }

    internal static bool AnyTaskRequiresNotifyDebuggerOfWaitCompletion(Task[] tasks)
    {
      foreach (Task task in tasks)
      {
        if (task != null && task.IsWaitNotificationEnabled && task.ShouldNotifyDebuggerOfWaitCompletion)
          return true;
      }
      return false;
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    private void NotifyDebuggerOfWaitCompletion()
    {
      this.SetNotificationForWaitCompletion(false);
    }

    internal bool MarkStarted()
    {
      return this.AtomicStateUpdate(65536, 4259840);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal bool FireTaskScheduledIfNeeded(TaskScheduler ts)
    {
      TplEtwProvider tplEtwProvider = TplEtwProvider.Log;
      if (!tplEtwProvider.IsEnabled() || (this.m_stateFlags & 1073741824) != 0)
        return false;
      this.m_stateFlags = this.m_stateFlags | 1073741824;
      Task internalCurrent = Task.InternalCurrent;
      Task task = this.m_parent;
      tplEtwProvider.TaskScheduled(ts.Id, internalCurrent == null ? 0 : internalCurrent.Id, this.Id, task == null ? 0 : task.Id, (int) this.Options, Thread.GetDomainID());
      return true;
    }

    internal void AddNewChild()
    {
      Task.ContingentProperties contingentProperties = this.EnsureContingentPropertiesInitialized(true);
      if (contingentProperties.m_completionCountdown == 1 && !this.IsSelfReplicatingRoot)
        ++contingentProperties.m_completionCountdown;
      else
        Interlocked.Increment(ref contingentProperties.m_completionCountdown);
    }

    internal void DisregardChild()
    {
      Interlocked.Decrement(ref this.EnsureContingentPropertiesInitialized(true).m_completionCountdown);
    }

    /// <summary>启动 <see cref="T:System.Threading.Tasks.Task" />，并将它安排到当前的 <see cref="T:System.Threading.Tasks.TaskScheduler" /> 中执行。</summary>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.Threading.Tasks.Task" /> 释放实例。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <see cref="T:System.Threading.Tasks.Task" /> 未处于有效的状态，无法启动。它可能已启动、 执行，或取消，或它可能具有不支持直接计划的方式创建。</exception>
    [__DynamicallyInvokable]
    public void Start()
    {
      this.Start(TaskScheduler.Current);
    }

    /// <summary>启动 <see cref="T:System.Threading.Tasks.Task" />，并将它安排到指定的 <see cref="T:System.Threading.Tasks.TaskScheduler" /> 中执行。</summary>
    /// <param name="scheduler">要与之关联并执行此任务的 <see cref="T:System.Threading.Tasks.TaskScheduler" />。</param>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.Threading.Tasks.Task" /> 释放实例。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="scheduler" /> 参数为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <see cref="T:System.Threading.Tasks.Task" /> 未处于有效的状态，无法启动。它可能已启动、 执行，或取消，或它可能具有不支持直接计划的方式创建。</exception>
    [__DynamicallyInvokable]
    public void Start(TaskScheduler scheduler)
    {
      int flags = this.m_stateFlags;
      if (Task.IsCompletedMethod(flags))
        throw new InvalidOperationException(Environment.GetResourceString("Task_Start_TaskCompleted"));
      if (scheduler == null)
        throw new ArgumentNullException("scheduler");
      int num1 = (int) Task.OptionsMethod(flags);
      int num2 = 1024;
      if ((num1 & num2) != 0)
        throw new InvalidOperationException(Environment.GetResourceString("Task_Start_Promise"));
      int num3 = 512;
      if ((num1 & num3) != 0)
        throw new InvalidOperationException(Environment.GetResourceString("Task_Start_ContinuationTask"));
      if (Interlocked.CompareExchange<TaskScheduler>(ref this.m_taskScheduler, scheduler, (TaskScheduler) null) != null)
        throw new InvalidOperationException(Environment.GetResourceString("Task_Start_AlreadyStarted"));
      this.ScheduleAndStart(true);
    }

    /// <summary>对当前的 <see cref="T:System.Threading.Tasks.Task" /> 同步运行 <see cref="T:System.Threading.Tasks.TaskScheduler" />。</summary>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.Threading.Tasks.Task" /> 释放实例。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <see cref="T:System.Threading.Tasks.Task" /> 未处于有效的状态，无法启动。它可能已启动、 执行，或取消，或它可能具有不支持直接计划的方式创建。</exception>
    [__DynamicallyInvokable]
    public void RunSynchronously()
    {
      this.InternalRunSynchronously(TaskScheduler.Current, true);
    }

    /// <summary>对提供的 <see cref="T:System.Threading.Tasks.Task" /> 同步运行 <see cref="T:System.Threading.Tasks.TaskScheduler" />。</summary>
    /// <param name="scheduler">尝试对其以内联方式运行此任务的计划程序。</param>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.Threading.Tasks.Task" /> 释放实例。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="scheduler" /> 参数为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <see cref="T:System.Threading.Tasks.Task" /> 未处于有效的状态，无法启动。它可能已启动、 执行，或取消，或它可能具有不支持直接计划的方式创建。</exception>
    [__DynamicallyInvokable]
    public void RunSynchronously(TaskScheduler scheduler)
    {
      if (scheduler == null)
        throw new ArgumentNullException("scheduler");
      this.InternalRunSynchronously(scheduler, true);
    }

    [SecuritySafeCritical]
    internal void InternalRunSynchronously(TaskScheduler scheduler, bool waitForCompletion)
    {
      int flags = this.m_stateFlags;
      int num1 = (int) Task.OptionsMethod(flags);
      int num2 = 512;
      if ((num1 & num2) != 0)
        throw new InvalidOperationException(Environment.GetResourceString("Task_RunSynchronously_Continuation"));
      int num3 = 1024;
      if ((num1 & num3) != 0)
        throw new InvalidOperationException(Environment.GetResourceString("Task_RunSynchronously_Promise"));
      if (Task.IsCompletedMethod(flags))
        throw new InvalidOperationException(Environment.GetResourceString("Task_RunSynchronously_TaskCompleted"));
      if (Interlocked.CompareExchange<TaskScheduler>(ref this.m_taskScheduler, scheduler, (TaskScheduler) null) != null)
        throw new InvalidOperationException(Environment.GetResourceString("Task_RunSynchronously_AlreadyStarted"));
      if (!this.MarkStarted())
        throw new InvalidOperationException(Environment.GetResourceString("Task_RunSynchronously_TaskCompleted"));
      bool flag = false;
      try
      {
        if (!scheduler.TryRunInline(this, false))
        {
          scheduler.InternalQueueTask(this);
          flag = true;
        }
        if (!waitForCompletion || this.IsCompleted)
          return;
        this.SpinThenBlockingWait(-1, new CancellationToken());
      }
      catch (System.Exception ex)
      {
        if (!flag && !(ex is ThreadAbortException))
        {
          TaskSchedulerException schedulerException = new TaskSchedulerException(ex);
          this.AddException((object) schedulerException);
          this.Finish(false);
          this.m_contingentProperties.m_exceptionsHolder.MarkAsHandled(false);
          throw schedulerException;
        }
        throw;
      }
    }

    internal static Task InternalStartNew(Task creatingTask, Delegate action, object state, CancellationToken cancellationToken, TaskScheduler scheduler, TaskCreationOptions options, InternalTaskOptions internalOptions, ref StackCrawlMark stackMark)
    {
      if (scheduler == null)
        throw new ArgumentNullException("scheduler");
      Task task = new Task(action, state, creatingTask, cancellationToken, options, internalOptions | InternalTaskOptions.QueuedByRuntime, scheduler);
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      StackCrawlMark& stackMark1 = @stackMark;
      task.PossiblyCaptureContext(stackMark1);
      int num = 0;
      task.ScheduleAndStart(num != 0);
      return task;
    }

    internal static int NewId()
    {
      int TaskID;
      do
      {
        TaskID = Interlocked.Increment(ref Task.s_taskIdCounter);
      }
      while (TaskID == 0);
      TplEtwProvider.Log.NewID(TaskID);
      return TaskID;
    }

    internal static Task InternalCurrentIfAttached(TaskCreationOptions creationOptions)
    {
      if ((creationOptions & TaskCreationOptions.AttachedToParent) == TaskCreationOptions.None)
        return (Task) null;
      return Task.InternalCurrent;
    }

    internal Task.ContingentProperties EnsureContingentPropertiesInitialized(bool needsProtection)
    {
      return this.m_contingentProperties ?? this.EnsureContingentPropertiesInitializedCore(needsProtection);
    }

    private Task.ContingentProperties EnsureContingentPropertiesInitializedCore(bool needsProtection)
    {
      if (needsProtection)
        return LazyInitializer.EnsureInitialized<Task.ContingentProperties>(ref this.m_contingentProperties, Task.s_createContingentProperties);
      return this.m_contingentProperties = new Task.ContingentProperties();
    }

    private static bool IsCompletedMethod(int flags)
    {
      return (uint) (flags & 23068672) > 0U;
    }

    private static ExecutionContext CopyExecutionContext(ExecutionContext capturedContext)
    {
      if (capturedContext == null)
        return (ExecutionContext) null;
      if (capturedContext.IsPreAllocatedDefault)
        return ExecutionContext.PreAllocatedDefault;
      return capturedContext.CreateCopy();
    }

    /// <summary>释放 <see cref="T:System.Threading.Tasks.Task" /> 类的当前实例所使用的所有资源。</summary>
    /// <exception cref="T:System.InvalidOperationException">该任务不在最终状态之一： <see cref="F:System.Threading.Tasks.TaskStatus.RanToCompletion" />, ，<see cref="F:System.Threading.Tasks.TaskStatus.Faulted" />, ，或 <see cref="F:System.Threading.Tasks.TaskStatus.Canceled" />。</exception>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>释放 <see cref="T:System.Threading.Tasks.Task" />，同时释放其所有非托管资源。</summary>
    /// <param name="disposing">一个布尔值，该值指示是否由于调用 <see cref="M:System.Threading.Tasks.Task.Dispose" /> 的原因而调用此方法。</param>
    /// <exception cref="T:System.InvalidOperationException">该任务不在最终状态之一： <see cref="F:System.Threading.Tasks.TaskStatus.RanToCompletion" />, ，<see cref="F:System.Threading.Tasks.TaskStatus.Faulted" />, ，或 <see cref="F:System.Threading.Tasks.TaskStatus.Canceled" />。</exception>
    protected virtual void Dispose(bool disposing)
    {
      if (disposing)
      {
        if ((this.Options & (TaskCreationOptions) 16384) != TaskCreationOptions.None)
          return;
        if (!this.IsCompleted)
          throw new InvalidOperationException(Environment.GetResourceString("Task_Dispose_NotCompleted"));
        Task.ContingentProperties contingentProperties = this.m_contingentProperties;
        if (contingentProperties != null)
        {
          ManualResetEventSlim manualResetEventSlim = contingentProperties.m_completionEvent;
          if (manualResetEventSlim != null)
          {
            contingentProperties.m_completionEvent = (ManualResetEventSlim) null;
            if (!manualResetEventSlim.IsSet)
              manualResetEventSlim.Set();
            manualResetEventSlim.Dispose();
          }
        }
      }
      this.m_stateFlags = this.m_stateFlags | 262144;
    }

    [SecuritySafeCritical]
    internal void ScheduleAndStart(bool needsProtection)
    {
      if (needsProtection)
      {
        if (!this.MarkStarted())
          return;
      }
      else
        this.m_stateFlags = this.m_stateFlags | 65536;
      if (Task.s_asyncDebuggingEnabled)
        Task.AddToActiveTasks(this);
      if (AsyncCausalityTracer.LoggingOn && (this.Options & (TaskCreationOptions) 512) == TaskCreationOptions.None)
        AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, this.Id, "Task: " + ((Delegate) this.m_action).Method.Name, 0UL);
      try
      {
        this.m_taskScheduler.InternalQueueTask(this);
      }
      catch (ThreadAbortException ex)
      {
        this.AddException((object) ex);
        this.FinishThreadAbortedTask(true, false);
      }
      catch (System.Exception ex)
      {
        TaskSchedulerException schedulerException = new TaskSchedulerException(ex);
        this.AddException((object) schedulerException);
        this.Finish(false);
        if ((this.Options & (TaskCreationOptions) 512) == TaskCreationOptions.None)
          this.m_contingentProperties.m_exceptionsHolder.MarkAsHandled(false);
        throw schedulerException;
      }
    }

    internal void AddException(object exceptionObject)
    {
      this.AddException(exceptionObject, false);
    }

    internal void AddException(object exceptionObject, bool representsCancellation)
    {
      Task.ContingentProperties contingentProperties = this.EnsureContingentPropertiesInitialized(true);
      if (contingentProperties.m_exceptionsHolder == null)
      {
        TaskExceptionHolder taskExceptionHolder = new TaskExceptionHolder(this);
        if (Interlocked.CompareExchange<TaskExceptionHolder>(ref contingentProperties.m_exceptionsHolder, taskExceptionHolder, (TaskExceptionHolder) null) != null)
          taskExceptionHolder.MarkAsHandled(false);
      }
      lock (contingentProperties)
        contingentProperties.m_exceptionsHolder.Add(exceptionObject, representsCancellation);
    }

    private AggregateException GetExceptions(bool includeTaskCanceledExceptions)
    {
      System.Exception includeThisException = (System.Exception) null;
      if (includeTaskCanceledExceptions && this.IsCanceled)
        includeThisException = (System.Exception) new TaskCanceledException(this);
      if (this.ExceptionRecorded)
        return this.m_contingentProperties.m_exceptionsHolder.CreateExceptionObject(false, includeThisException);
      if (includeThisException == null)
        return (AggregateException) null;
      return new AggregateException(new System.Exception[1]{ includeThisException });
    }

    internal ReadOnlyCollection<ExceptionDispatchInfo> GetExceptionDispatchInfos()
    {
      if ((!this.IsFaulted ? 0 : (this.ExceptionRecorded ? 1 : 0)) == 0)
        return new ReadOnlyCollection<ExceptionDispatchInfo>((IList<ExceptionDispatchInfo>) new ExceptionDispatchInfo[0]);
      return this.m_contingentProperties.m_exceptionsHolder.GetExceptionDispatchInfos();
    }

    internal ExceptionDispatchInfo GetCancellationExceptionDispatchInfo()
    {
      Task.ContingentProperties contingentProperties = this.m_contingentProperties;
      if (contingentProperties == null)
        return (ExceptionDispatchInfo) null;
      TaskExceptionHolder taskExceptionHolder = contingentProperties.m_exceptionsHolder;
      if (taskExceptionHolder == null)
        return (ExceptionDispatchInfo) null;
      return taskExceptionHolder.GetCancellationExceptionDispatchInfo();
    }

    internal void ThrowIfExceptional(bool includeTaskCanceledExceptions)
    {
      System.Exception exception = (System.Exception) this.GetExceptions(includeTaskCanceledExceptions);
      if (exception != null)
      {
        this.UpdateExceptionObservedStatus();
        throw exception;
      }
    }

    internal void UpdateExceptionObservedStatus()
    {
      if (this.m_parent == null || (this.Options & TaskCreationOptions.AttachedToParent) == TaskCreationOptions.None || ((this.m_parent.CreationOptions & TaskCreationOptions.DenyChildAttach) != TaskCreationOptions.None || Task.InternalCurrent != this.m_parent))
        return;
      this.m_stateFlags = this.m_stateFlags | 524288;
    }

    internal void Finish(bool bUserDelegateExecuted)
    {
      if (!bUserDelegateExecuted)
      {
        this.FinishStageTwo();
      }
      else
      {
        Task.ContingentProperties contingentProperties = this.m_contingentProperties;
        if (contingentProperties == null || contingentProperties.m_completionCountdown == 1 && !this.IsSelfReplicatingRoot || Interlocked.Decrement(ref contingentProperties.m_completionCountdown) == 0)
          this.FinishStageTwo();
        else
          this.AtomicStateUpdate(8388608, 23068672);
        List<Task> taskList = contingentProperties != null ? contingentProperties.m_exceptionalChildren : (List<Task>) null;
        if (taskList == null)
          return;
        lock (taskList)
          taskList.RemoveAll(Task.s_IsExceptionObservedByParentPredicate);
      }
    }

    internal void FinishStageTwo()
    {
      this.AddExceptionsFromChildren();
      int num;
      if (this.ExceptionRecorded)
      {
        num = 2097152;
        if (AsyncCausalityTracer.LoggingOn)
          AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, this.Id, AsyncCausalityStatus.Error);
        if (Task.s_asyncDebuggingEnabled)
          Task.RemoveFromActiveTasks(this.Id);
      }
      else if (this.IsCancellationRequested && this.IsCancellationAcknowledged)
      {
        num = 4194304;
        if (AsyncCausalityTracer.LoggingOn)
          AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, this.Id, AsyncCausalityStatus.Canceled);
        if (Task.s_asyncDebuggingEnabled)
          Task.RemoveFromActiveTasks(this.Id);
      }
      else
      {
        num = 16777216;
        if (AsyncCausalityTracer.LoggingOn)
          AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, this.Id, AsyncCausalityStatus.Completed);
        if (Task.s_asyncDebuggingEnabled)
          Task.RemoveFromActiveTasks(this.Id);
      }
      Interlocked.Exchange(ref this.m_stateFlags, this.m_stateFlags | num);
      Task.ContingentProperties contingentProperties = this.m_contingentProperties;
      if (contingentProperties != null)
      {
        contingentProperties.SetCompleted();
        contingentProperties.DeregisterCancellationCallback();
      }
      this.FinishStageThree();
    }

    internal void FinishStageThree()
    {
      this.m_action = (object) null;
      if (this.m_parent != null && (this.m_parent.CreationOptions & TaskCreationOptions.DenyChildAttach) == TaskCreationOptions.None && (this.m_stateFlags & (int) ushort.MaxValue & 4) != 0)
        this.m_parent.ProcessChildCompletion(this);
      this.FinishContinuations();
    }

    internal void ProcessChildCompletion(Task childTask)
    {
      Task.ContingentProperties contingentProperties = this.m_contingentProperties;
      if (childTask.IsFaulted && !childTask.IsExceptionObservedByParent)
      {
        if (contingentProperties.m_exceptionalChildren == null)
          Interlocked.CompareExchange<List<Task>>(ref contingentProperties.m_exceptionalChildren, new List<Task>(), (List<Task>) null);
        List<Task> taskList = contingentProperties.m_exceptionalChildren;
        if (taskList != null)
        {
          lock (taskList)
            taskList.Add(childTask);
        }
      }
      if (Interlocked.Decrement(ref contingentProperties.m_completionCountdown) != 0)
        return;
      this.FinishStageTwo();
    }

    internal void AddExceptionsFromChildren()
    {
      Task.ContingentProperties contingentProperties = this.m_contingentProperties;
      List<Task> taskList = contingentProperties != null ? contingentProperties.m_exceptionalChildren : (List<Task>) null;
      if (taskList == null)
        return;
      lock (taskList)
      {
        foreach (Task item_0 in taskList)
        {
          if (item_0.IsFaulted && !item_0.IsExceptionObservedByParent)
            this.AddException((object) item_0.m_contingentProperties.m_exceptionsHolder.CreateExceptionObject(false, (System.Exception) null));
        }
      }
      contingentProperties.m_exceptionalChildren = (List<Task>) null;
    }

    internal void FinishThreadAbortedTask(bool bTAEAddedToExceptionHolder, bool delegateRan)
    {
      if (bTAEAddedToExceptionHolder)
        this.m_contingentProperties.m_exceptionsHolder.MarkAsHandled(false);
      if (!this.AtomicStateUpdate(134217728, 157286400))
        return;
      this.Finish(delegateRan);
    }

    private void Execute()
    {
      if (this.IsSelfReplicatingRoot)
      {
        Task.ExecuteSelfReplicating(this);
      }
      else
      {
        try
        {
          this.InnerInvoke();
        }
        catch (ThreadAbortException ex)
        {
          if (this.IsChildReplica)
            return;
          this.HandleException((System.Exception) ex);
          this.FinishThreadAbortedTask(true, true);
        }
        catch (System.Exception ex)
        {
          this.HandleException(ex);
        }
      }
    }

    internal virtual bool ShouldReplicate()
    {
      return true;
    }

    internal virtual Task CreateReplicaTask(Action<object> taskReplicaDelegate, object stateObject, Task parentTask, TaskScheduler taskScheduler, TaskCreationOptions creationOptionsForReplica, InternalTaskOptions internalOptionsForReplica)
    {
      return new Task((Delegate) taskReplicaDelegate, stateObject, parentTask, new CancellationToken(), creationOptionsForReplica, internalOptionsForReplica, parentTask.ExecutingTaskScheduler);
    }

    private static void ExecuteSelfReplicating(Task root)
    {
      TaskCreationOptions creationOptionsForReplicas = root.CreationOptions | TaskCreationOptions.AttachedToParent;
      InternalTaskOptions internalOptionsForReplicas = InternalTaskOptions.ChildReplica | InternalTaskOptions.SelfReplicating | InternalTaskOptions.QueuedByRuntime;
      bool replicasAreQuitting = 0 != 0;
      Action<object> taskReplicaDelegate = (Action<object>) null;
      taskReplicaDelegate = (Action<object>) (param0 =>
      {
        Task internalCurrent = Task.InternalCurrent;
        Task task1 = internalCurrent.HandedOverChildReplica;
        if (task1 == null)
        {
          if (!root.ShouldReplicate() || Volatile.Read(ref replicasAreQuitting))
            return;
          ExecutionContext capturedContext = root.CapturedContext;
          task1 = root.CreateReplicaTask(taskReplicaDelegate, root.m_stateObject, root, root.ExecutingTaskScheduler, creationOptionsForReplicas, internalOptionsForReplicas);
          task1.CapturedContext = Task.CopyExecutionContext(capturedContext);
          task1.ScheduleAndStart(false);
        }
        try
        {
          root.InnerInvokeWithArg(internalCurrent);
        }
        catch (System.Exception ex)
        {
          root.HandleException(ex);
          if (ex is ThreadAbortException)
            internalCurrent.FinishThreadAbortedTask(false, true);
        }
        object stateForNextReplica = internalCurrent.SavedStateForNextReplica;
        if (stateForNextReplica != null)
        {
          Task replicaTask = root.CreateReplicaTask(taskReplicaDelegate, root.m_stateObject, root, root.ExecutingTaskScheduler, creationOptionsForReplicas, internalOptionsForReplicas);
          ExecutionContext executionContext = Task.CopyExecutionContext(root.CapturedContext);
          replicaTask.CapturedContext = executionContext;
          Task task2 = task1;
          replicaTask.HandedOverChildReplica = task2;
          object obj = stateForNextReplica;
          replicaTask.SavedStateFromPreviousReplica = obj;
          int num = 0;
          replicaTask.ScheduleAndStart(num != 0);
        }
        else
        {
          replicasAreQuitting = true;
          try
          {
            task1.InternalCancel(true);
          }
          catch (System.Exception ex)
          {
            root.HandleException(ex);
          }
        }
      });
      taskReplicaDelegate((object) null);
    }

    [SecurityCritical]
    void IThreadPoolWorkItem.ExecuteWorkItem()
    {
      this.ExecuteEntry(false);
    }

    [SecurityCritical]
    void IThreadPoolWorkItem.MarkAborted(ThreadAbortException tae)
    {
      if (this.IsCompleted)
        return;
      this.HandleException((System.Exception) tae);
      this.FinishThreadAbortedTask(true, false);
    }

    [SecuritySafeCritical]
    internal bool ExecuteEntry(bool bPreventDoubleExecution)
    {
      if (bPreventDoubleExecution || (this.Options & (TaskCreationOptions) 2048) != TaskCreationOptions.None)
      {
        int oldFlags = 0;
        if (!this.AtomicStateUpdate(131072, 23199744, ref oldFlags) && (oldFlags & 4194304) == 0)
          return false;
      }
      else
        this.m_stateFlags = this.m_stateFlags | 131072;
      if (!this.IsCancellationRequested && !this.IsCanceled)
        this.ExecuteWithThreadLocal(ref Task.t_currentTask);
      else if (!this.IsCanceled && (Interlocked.Exchange(ref this.m_stateFlags, this.m_stateFlags | 4194304) & 4194304) == 0)
        this.CancellationCleanupLogic();
      return true;
    }

    [SecurityCritical]
    private void ExecuteWithThreadLocal(ref Task currentTaskSlot)
    {
      Task task = currentTaskSlot;
      TplEtwProvider tplEtwProvider = TplEtwProvider.Log;
      Guid oldActivityThatWillContinue = new Guid();
      bool flag = tplEtwProvider.IsEnabled();
      if (flag)
      {
        if (tplEtwProvider.TasksSetActivityIds)
          EventSource.SetCurrentThreadActivityId(TplEtwProvider.CreateGuidForTaskID(this.Id), out oldActivityThatWillContinue);
        if (task != null)
          tplEtwProvider.TaskStarted(task.m_taskScheduler.Id, task.Id, this.Id);
        else
          tplEtwProvider.TaskStarted(TaskScheduler.Current.Id, 0, this.Id);
      }
      if (AsyncCausalityTracer.LoggingOn)
        AsyncCausalityTracer.TraceSynchronousWorkStart(CausalityTraceLevel.Required, this.Id, CausalitySynchronousWork.Execution);
      try
      {
        currentTaskSlot = this;
        ExecutionContext capturedContext = this.CapturedContext;
        if (capturedContext == null)
        {
          this.Execute();
        }
        else
        {
          if (this.IsSelfReplicatingRoot || this.IsChildReplica)
            this.CapturedContext = Task.CopyExecutionContext(capturedContext);
          ContextCallback callback = Task.s_ecCallback;
          if (callback == null)
            Task.s_ecCallback = callback = new ContextCallback(Task.ExecutionContextCallback);
          ExecutionContext.Run(capturedContext, callback, (object) this, true);
        }
        if (AsyncCausalityTracer.LoggingOn)
          AsyncCausalityTracer.TraceSynchronousWorkCompletion(CausalityTraceLevel.Required, CausalitySynchronousWork.Execution);
        this.Finish(true);
      }
      finally
      {
        currentTaskSlot = task;
        if (flag)
        {
          if (task != null)
            tplEtwProvider.TaskCompleted(task.m_taskScheduler.Id, task.Id, this.Id, this.IsFaulted);
          else
            tplEtwProvider.TaskCompleted(TaskScheduler.Current.Id, 0, this.Id, this.IsFaulted);
          if (tplEtwProvider.TasksSetActivityIds)
            EventSource.SetCurrentThreadActivityId(oldActivityThatWillContinue);
        }
      }
    }

    [SecurityCritical]
    private static void ExecutionContextCallback(object obj)
    {
      (obj as Task).Execute();
    }

    internal virtual void InnerInvoke()
    {
      Action action1 = this.m_action as Action;
      if (action1 != null)
      {
        action1();
      }
      else
      {
        Action<object> action2 = this.m_action as Action<object>;
        if (action2 == null)
          return;
        action2(this.m_stateObject);
      }
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    internal void InnerInvokeWithArg(Task childTask)
    {
      this.InnerInvoke();
    }

    private void HandleException(System.Exception unhandledException)
    {
      OperationCanceledException canceledException = unhandledException as OperationCanceledException;
      if (canceledException != null && this.IsCancellationRequested && this.m_contingentProperties.m_cancellationToken == canceledException.CancellationToken)
      {
        this.SetCancellationAcknowledged();
        this.AddException((object) canceledException, true);
      }
      else
        this.AddException((object) unhandledException);
    }

    /// <summary>获取用于等待此 <see cref="T:System.Threading.Tasks.Task" /> 的 awaiter。</summary>
    /// <returns>一个 awaiter 实例。</returns>
    [__DynamicallyInvokable]
    public TaskAwaiter GetAwaiter()
    {
      return new TaskAwaiter(this);
    }

    /// <summary>配置用于等待此 <see cref="T:System.Threading.Tasks.Task" />的 awaiter。</summary>
    /// <returns>用于的等待此任务的对象。</returns>
    /// <param name="continueOnCapturedContext">尝试将延续任务封送回原始上下文，则为 true；否则为 false。</param>
    [__DynamicallyInvokable]
    public ConfiguredTaskAwaitable ConfigureAwait(bool continueOnCapturedContext)
    {
      return new ConfiguredTaskAwaitable(this, continueOnCapturedContext);
    }

    [SecurityCritical]
    internal void SetContinuationForAwait(Action continuationAction, bool continueOnCapturedContext, bool flowExecutionContext, ref StackCrawlMark stackMark)
    {
      TaskContinuation taskContinuation = (TaskContinuation) null;
      if (continueOnCapturedContext)
      {
        SynchronizationContext currentNoFlow = SynchronizationContext.CurrentNoFlow;
        if (currentNoFlow != null && currentNoFlow.GetType() != typeof (SynchronizationContext))
        {
          taskContinuation = (TaskContinuation) new SynchronizationContextAwaitTaskContinuation(currentNoFlow, continuationAction, flowExecutionContext, ref stackMark);
        }
        else
        {
          TaskScheduler internalCurrent = TaskScheduler.InternalCurrent;
          if (internalCurrent != null && internalCurrent != TaskScheduler.Default)
            taskContinuation = (TaskContinuation) new TaskSchedulerAwaitTaskContinuation(internalCurrent, continuationAction, flowExecutionContext, ref stackMark);
        }
      }
      if (taskContinuation == null & flowExecutionContext)
        taskContinuation = (TaskContinuation) new AwaitTaskContinuation(continuationAction, true, ref stackMark);
      if (taskContinuation != null)
      {
        if (this.AddTaskContinuation((object) taskContinuation, false))
          return;
        taskContinuation.Run(this, false);
      }
      else
      {
        if (this.AddTaskContinuation((object) continuationAction, false))
          return;
        AwaitTaskContinuation.UnsafeScheduleAction(continuationAction, this);
      }
    }

    /// <summary>创建异步产生当前上下文的等待任务。</summary>
    /// <returns>等待时，上下文将异步转换回等待时的当前上下文。如果当前 <see cref="T:System.Threading.SynchronizationContext" /> 不为 null，则将其视为当前上下文。否则，与当前执行任务关联的任务计划程序将视为当前上下文。</returns>
    [__DynamicallyInvokable]
    public static YieldAwaitable Yield()
    {
      return new YieldAwaitable();
    }

    /// <summary>等待 <see cref="T:System.Threading.Tasks.Task" /> 完成执行过程。</summary>
    /// <exception cref="T:System.ObjectDisposedException">已释放了 <see cref="T:System.Threading.Tasks.Task" />。</exception>
    /// <exception cref="T:System.AggregateException">任务已取消。<see cref="P:System.AggregateException.InnerExceptions" /> 集合包含 <see cref="T:System.Threading.Tasks.TaskCanceledException" /> 对象。- 或 -在任务执行过程中引发异常。<see cref="P:System.AggregateException.InnerExceptions" /> 集合包含有关异常或异常的信息。</exception>
    [__DynamicallyInvokable]
    public void Wait()
    {
      this.Wait(-1, new CancellationToken());
    }

    /// <summary>等待 <see cref="T:System.Threading.Tasks.Task" /> 在指定的时间间隔内完成执行。</summary>
    /// <returns>如果在分配的时间内 true 完成执行，则为 <see cref="T:System.Threading.Tasks.Task" />；否则为 false。</returns>
    /// <param name="timeout">表示等待毫秒数的 <see cref="T:System.TimeSpan" />，或表示 -1 毫秒（无限期等待）的 <see cref="T:System.TimeSpan" />。</param>
    /// <exception cref="T:System.ObjectDisposedException">已释放了 <see cref="T:System.Threading.Tasks.Task" />。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="timeout" /> 是-1 毫秒以外的负数字表示无限超时。- 或 -<paramref name="timeout" /> 大于 <see cref="F:System.Int32.MaxValue" />。</exception>
    /// <exception cref="T:System.AggregateException">任务已取消。<see cref="P:System.AggregateException.InnerExceptions" /> 集合包含 <see cref="T:System.Threading.Tasks.TaskCanceledException" /> 对象。- 或 -在任务执行过程中引发异常。<see cref="P:System.AggregateException.InnerExceptions" /> 集合包含有关异常或异常的信息。</exception>
    [__DynamicallyInvokable]
    public bool Wait(TimeSpan timeout)
    {
      long num = (long) timeout.TotalMilliseconds;
      if (num < -1L || num > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException("timeout");
      return this.Wait((int) num, new CancellationToken());
    }

    /// <summary>等待 <see cref="T:System.Threading.Tasks.Task" /> 完成执行过程。如果在任务完成之前取消标记已取消，等待将终止。</summary>
    /// <param name="cancellationToken">等待任务完成期间要观察的取消标记。</param>
    /// <exception cref="T:System.OperationCanceledException">
    /// <paramref name="cancellationToken" /> 已取消。</exception>
    /// <exception cref="T:System.ObjectDisposedException">已释放该任务。</exception>
    /// <exception cref="T:System.AggregateException">任务已取消。<see cref="P:System.AggregateException.InnerExceptions" /> 集合包含 <see cref="T:System.Threading.Tasks.TaskCanceledException" /> 对象。- 或 -在任务执行过程中引发异常。<see cref="P:System.AggregateException.InnerExceptions" /> 集合包含有关异常或异常的信息。</exception>
    [__DynamicallyInvokable]
    public void Wait(CancellationToken cancellationToken)
    {
      this.Wait(-1, cancellationToken);
    }

    /// <summary>等待 <see cref="T:System.Threading.Tasks.Task" /> 在指定的毫秒数内完成执行。</summary>
    /// <returns>如果在分配的时间内 true 完成执行，则为 <see cref="T:System.Threading.Tasks.Task" />；否则为 false。</returns>
    /// <param name="millisecondsTimeout">等待的毫秒数，或为 <see cref="F:System.Threading.Timeout.Infinite" /> (-1)，表示无限期等待。</param>
    /// <exception cref="T:System.ObjectDisposedException">已释放了 <see cref="T:System.Threading.Tasks.Task" />。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="millisecondsTimeout" /> 是-1，表示无限超时值之外的负数。</exception>
    /// <exception cref="T:System.AggregateException">任务已取消。<see cref="P:System.AggregateException.InnerExceptions" /> 集合包含 <see cref="T:System.Threading.Tasks.TaskCanceledException" /> 对象。- 或 -在任务执行过程中引发异常。<see cref="P:System.AggregateException.InnerExceptions" /> 集合包含有关异常或异常的信息。</exception>
    [__DynamicallyInvokable]
    public bool Wait(int millisecondsTimeout)
    {
      return this.Wait(millisecondsTimeout, new CancellationToken());
    }

    /// <summary>等待 <see cref="T:System.Threading.Tasks.Task" /> 完成执行过程。如果在任务完成之前超时间隔结束或取消标记已取消，等待将终止。</summary>
    /// <returns>如果在分配的时间内 true 完成执行，则为 <see cref="T:System.Threading.Tasks.Task" />；否则为 false。</returns>
    /// <param name="millisecondsTimeout">等待的毫秒数，或为 <see cref="F:System.Threading.Timeout.Infinite" /> (-1)，表示无限期等待。</param>
    /// <param name="cancellationToken">等待任务完成期间要观察的取消标记。</param>
    /// <exception cref="T:System.OperationCanceledException">
    /// <paramref name="cancellationToken" /> 已取消。</exception>
    /// <exception cref="T:System.ObjectDisposedException">已释放了 <see cref="T:System.Threading.Tasks.Task" />。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="millisecondsTimeout" /> 是-1，表示无限超时值之外的负数。</exception>
    /// <exception cref="T:System.AggregateException">任务已取消。<see cref="P:System.AggregateException.InnerExceptions" /> 集合包含 <see cref="T:System.Threading.Tasks.TaskCanceledException" /> 对象。- 或 -在任务执行过程中引发异常。<see cref="P:System.AggregateException.InnerExceptions" /> 集合包含有关异常或异常的信息。</exception>
    [__DynamicallyInvokable]
    public bool Wait(int millisecondsTimeout, CancellationToken cancellationToken)
    {
      if (millisecondsTimeout < -1)
        throw new ArgumentOutOfRangeException("millisecondsTimeout");
      if (!this.IsWaitNotificationEnabledOrNotRanToCompletion)
        return true;
      if (!this.InternalWait(millisecondsTimeout, cancellationToken))
        return false;
      if (this.IsWaitNotificationEnabledOrNotRanToCompletion)
      {
        this.NotifyDebuggerOfWaitCompletionIfNecessary();
        if (this.IsCanceled)
          cancellationToken.ThrowIfCancellationRequested();
        this.ThrowIfExceptional(true);
      }
      return true;
    }

    private bool WrappedTryRunInline()
    {
      if (this.m_taskScheduler == null)
        return false;
      try
      {
        return this.m_taskScheduler.TryRunInline(this, true);
      }
      catch (System.Exception ex)
      {
        if (!(ex is ThreadAbortException))
          throw new TaskSchedulerException(ex);
        throw;
      }
    }

    [MethodImpl(MethodImplOptions.NoOptimization)]
    internal bool InternalWait(int millisecondsTimeout, CancellationToken cancellationToken)
    {
      TplEtwProvider tplEtwProvider = TplEtwProvider.Log;
      int num = tplEtwProvider.IsEnabled() ? 1 : 0;
      if (num != 0)
      {
        Task internalCurrent = Task.InternalCurrent;
        tplEtwProvider.TaskWaitBegin(internalCurrent != null ? internalCurrent.m_taskScheduler.Id : TaskScheduler.Default.Id, internalCurrent != null ? internalCurrent.Id : 0, this.Id, TplEtwProvider.TaskWaitBehavior.Synchronous, 0, Thread.GetDomainID());
      }
      bool flag = this.IsCompleted;
      if (!flag)
      {
        Debugger.NotifyOfCrossThreadDependency();
        flag = millisecondsTimeout == -1 && !cancellationToken.CanBeCanceled && (this.WrappedTryRunInline() && this.IsCompleted) || this.SpinThenBlockingWait(millisecondsTimeout, cancellationToken);
      }
      if (num != 0)
      {
        Task internalCurrent = Task.InternalCurrent;
        if (internalCurrent != null)
          tplEtwProvider.TaskWaitEnd(internalCurrent.m_taskScheduler.Id, internalCurrent.Id, this.Id);
        else
          tplEtwProvider.TaskWaitEnd(TaskScheduler.Default.Id, 0, this.Id);
        tplEtwProvider.TaskWaitContinuationComplete(this.Id);
      }
      return flag;
    }

    private bool SpinThenBlockingWait(int millisecondsTimeout, CancellationToken cancellationToken)
    {
      bool flag1 = millisecondsTimeout == -1;
      uint num1 = flag1 ? 0U : (uint) Environment.TickCount;
      bool flag2 = this.SpinWait(millisecondsTimeout);
      if (!flag2)
      {
        Task.SetOnInvokeMres setOnInvokeMres = new Task.SetOnInvokeMres();
        try
        {
          this.AddCompletionAction((ITaskCompletionAction) setOnInvokeMres, true);
          if (flag1)
          {
            flag2 = setOnInvokeMres.Wait(-1, cancellationToken);
          }
          else
          {
            uint num2 = (uint) Environment.TickCount - num1;
            if ((long) num2 < (long) millisecondsTimeout)
              flag2 = setOnInvokeMres.Wait((int) ((long) millisecondsTimeout - (long) num2), cancellationToken);
          }
        }
        finally
        {
          if (!this.IsCompleted)
            this.RemoveContinuation((object) setOnInvokeMres);
        }
      }
      return flag2;
    }

    private bool SpinWait(int millisecondsTimeout)
    {
      if (this.IsCompleted)
        return true;
      if (millisecondsTimeout == 0)
        return false;
      int num = PlatformHelper.IsSingleProcessor ? 1 : 10;
      for (int index = 0; index < num; ++index)
      {
        if (this.IsCompleted)
          return true;
        if (index == num / 2)
          Thread.Yield();
        else
          Thread.SpinWait(PlatformHelper.ProcessorCount * (4 << index));
      }
      return this.IsCompleted;
    }

    [SecuritySafeCritical]
    internal bool InternalCancel(bool bCancelNonExecutingOnly)
    {
      bool flag1 = false;
      bool flag2 = false;
      TaskSchedulerException schedulerException = (TaskSchedulerException) null;
      if ((this.m_stateFlags & 65536) != 0)
      {
        TaskScheduler taskScheduler = this.m_taskScheduler;
        try
        {
          flag1 = taskScheduler != null && taskScheduler.TryDequeue(this);
        }
        catch (System.Exception ex)
        {
          if (!(ex is ThreadAbortException))
            schedulerException = new TaskSchedulerException(ex);
        }
        bool flag3 = taskScheduler != null && taskScheduler.RequiresAtomicStartTransition || (uint) (this.Options & (TaskCreationOptions) 2048) > 0U;
        if (!flag1 & bCancelNonExecutingOnly & flag3)
          flag2 = this.AtomicStateUpdate(4194304, 4325376);
      }
      if (!bCancelNonExecutingOnly | flag1 | flag2)
      {
        this.RecordInternalCancellationRequest();
        if (flag1)
          flag2 = this.AtomicStateUpdate(4194304, 4325376);
        else if (!flag2 && (this.m_stateFlags & 65536) == 0)
          flag2 = this.AtomicStateUpdate(4194304, 23265280);
        if (flag2)
          this.CancellationCleanupLogic();
      }
      if (schedulerException != null)
        throw schedulerException;
      return flag2;
    }

    internal void RecordInternalCancellationRequest()
    {
      this.EnsureContingentPropertiesInitialized(true).m_internalCancellationRequested = 1;
    }

    internal void RecordInternalCancellationRequest(CancellationToken tokenToRecord)
    {
      this.RecordInternalCancellationRequest();
      if (!(tokenToRecord != new CancellationToken()))
        return;
      this.m_contingentProperties.m_cancellationToken = tokenToRecord;
    }

    internal void RecordInternalCancellationRequest(CancellationToken tokenToRecord, object cancellationException)
    {
      this.RecordInternalCancellationRequest(tokenToRecord);
      if (cancellationException == null)
        return;
      this.AddException(cancellationException, true);
    }

    internal void CancellationCleanupLogic()
    {
      Interlocked.Exchange(ref this.m_stateFlags, this.m_stateFlags | 4194304);
      Task.ContingentProperties contingentProperties = this.m_contingentProperties;
      if (contingentProperties != null)
      {
        contingentProperties.SetCompleted();
        contingentProperties.DeregisterCancellationCallback();
      }
      if (AsyncCausalityTracer.LoggingOn)
        AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, this.Id, AsyncCausalityStatus.Canceled);
      if (Task.s_asyncDebuggingEnabled)
        Task.RemoveFromActiveTasks(this.Id);
      this.FinishStageThree();
    }

    private void SetCancellationAcknowledged()
    {
      this.m_stateFlags = this.m_stateFlags | 1048576;
    }

    [SecuritySafeCritical]
    internal void FinishContinuations()
    {
      object Object1 = Interlocked.Exchange(ref this.m_continuationObject, Task.s_taskCompletionSentinel);
      TplEtwProvider.Log.RunningContinuation(this.Id, Object1);
      if (Object1 == null)
        return;
      if (AsyncCausalityTracer.LoggingOn)
        AsyncCausalityTracer.TraceSynchronousWorkStart(CausalityTraceLevel.Required, this.Id, CausalitySynchronousWork.CompletionNotification);
      bool flag = (this.m_stateFlags & 134217728) == 0 && Thread.CurrentThread.ThreadState != ThreadState.AbortRequested && (this.m_stateFlags & 64) == 0;
      Action action1 = Object1 as Action;
      if (action1 != null)
      {
        AwaitTaskContinuation.RunOrScheduleAction(action1, flag, ref Task.t_currentTask);
        this.LogFinishCompletionNotification();
      }
      else
      {
        ITaskCompletionAction completionAction = Object1 as ITaskCompletionAction;
        if (completionAction != null)
        {
          completionAction.Invoke(this);
          this.LogFinishCompletionNotification();
        }
        else
        {
          TaskContinuation taskContinuation1 = Object1 as TaskContinuation;
          if (taskContinuation1 != null)
          {
            taskContinuation1.Run(this, flag);
            this.LogFinishCompletionNotification();
          }
          else
          {
            List<object> objectList = Object1 as List<object>;
            if (objectList == null)
            {
              this.LogFinishCompletionNotification();
            }
            else
            {
              lock (objectList)
                ;
              int count = objectList.Count;
              for (int Index = 0; Index < count; ++Index)
              {
                StandardTaskContinuation taskContinuation2 = objectList[Index] as StandardTaskContinuation;
                if (taskContinuation2 != null && (taskContinuation2.m_options & TaskContinuationOptions.ExecuteSynchronously) == TaskContinuationOptions.None)
                {
                  TplEtwProvider.Log.RunningContinuationList(this.Id, Index, (object) taskContinuation2);
                  objectList[Index] = (object) null;
                  taskContinuation2.Run(this, flag);
                }
              }
              for (int Index = 0; Index < count; ++Index)
              {
                object Object2 = objectList[Index];
                if (Object2 != null)
                {
                  objectList[Index] = (object) null;
                  TplEtwProvider.Log.RunningContinuationList(this.Id, Index, Object2);
                  Action action2 = Object2 as Action;
                  if (action2 != null)
                  {
                    AwaitTaskContinuation.RunOrScheduleAction(action2, flag, ref Task.t_currentTask);
                  }
                  else
                  {
                    TaskContinuation taskContinuation2 = Object2 as TaskContinuation;
                    if (taskContinuation2 != null)
                      taskContinuation2.Run(this, flag);
                    else
                      ((ITaskCompletionAction) Object2).Invoke(this);
                  }
                }
              }
              this.LogFinishCompletionNotification();
            }
          }
        }
      }
    }

    private void LogFinishCompletionNotification()
    {
      if (!AsyncCausalityTracer.LoggingOn)
        return;
      AsyncCausalityTracer.TraceSynchronousWorkCompletion(CausalityTraceLevel.Required, CausalitySynchronousWork.CompletionNotification);
    }

    /// <summary>创建一个在目标 <see cref="T:System.Threading.Tasks.Task" /> 完成时异步执行的延续任务。</summary>
    /// <returns>一个新的延续 <see cref="T:System.Threading.Tasks.Task" />。</returns>
    /// <param name="continuationAction">在 <see cref="T:System.Threading.Tasks.Task" /> 完成时要运行的操作。在运行时，委托将作为一个参数传递给完成的任务。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="continuationAction" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWith(Action<Task> continuationAction)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith(continuationAction, TaskScheduler.Current, new CancellationToken(), TaskContinuationOptions.None, ref stackMark);
    }

    /// <summary>创建一个在目标 <see cref="T:System.Threading.Tasks.Task" /> 完成时可接收取消标记并以异步方式执行的延续任务。</summary>
    /// <returns>一个新的延续 <see cref="T:System.Threading.Tasks.Task" />。</returns>
    /// <param name="continuationAction">在 <see cref="T:System.Threading.Tasks.Task" /> 完成时要运行的操作。在运行时，委托将作为一个参数传递给完成的任务。</param>
    /// <param name="cancellationToken">将指派给新的延续任务的 <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" />。</param>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.Threading.CancellationTokenSource" /> 已释放，创建该标记。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="continuationAction" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWith(Action<Task> continuationAction, CancellationToken cancellationToken)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith(continuationAction, TaskScheduler.Current, cancellationToken, TaskContinuationOptions.None, ref stackMark);
    }

    /// <summary>创建一个在目标 <see cref="T:System.Threading.Tasks.Task" /> 完成时异步执行的延续任务。延续任务使用指定计划程序。</summary>
    /// <returns>一个新的延续 <see cref="T:System.Threading.Tasks.Task" />。</returns>
    /// <param name="continuationAction">在 <see cref="T:System.Threading.Tasks.Task" /> 完成时要运行的操作。在运行时，委托将作为一个参数传递给完成的任务。</param>
    /// <param name="scheduler">要与延续任务关联并用于其执行过程的 <see cref="T:System.Threading.Tasks.TaskScheduler" />。</param>
    /// <exception cref="T:System.ObjectDisposedException">已释放了 <see cref="T:System.Threading.Tasks.Task" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="continuationAction" /> 参数为 null。- 或 -<paramref name="scheduler" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWith(Action<Task> continuationAction, TaskScheduler scheduler)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith(continuationAction, scheduler, new CancellationToken(), TaskContinuationOptions.None, ref stackMark);
    }

    /// <summary>创建一个在目标任务完成时按照指定的 <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> 执行的延续任务。</summary>
    /// <returns>一个新的延续 <see cref="T:System.Threading.Tasks.Task" />。</returns>
    /// <param name="continuationAction">根据在 <paramref name="continuationOptions" /> 中指定的条件运行的操作。在运行时，委托将作为一个参数传递给完成的任务。</param>
    /// <param name="continuationOptions">用于设置计划延续任务的时间以及延续任务的工作方式的选项。这包括条件（如 <see cref="F:System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled" />）和执行选项（如 <see cref="F:System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously" />）。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="continuationAction" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="continuationOptions" /> 参数指定的无效值 <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWith(Action<Task> continuationAction, TaskContinuationOptions continuationOptions)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith(continuationAction, TaskScheduler.Current, new CancellationToken(), continuationOptions, ref stackMark);
    }

    /// <summary>创建一个在目标任务完成时按照指定的 <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> 执行的延续任务。延续任务会收到一个取消标记，并使用指定计划程序。</summary>
    /// <returns>一个新的延续 <see cref="T:System.Threading.Tasks.Task" />。</returns>
    /// <param name="continuationAction">根据在 <paramref name="continuationOptions" /> 中指定的条件运行的操作。在运行时，委托将作为一个参数传递给完成的任务。</param>
    /// <param name="cancellationToken">将指派给新的延续任务的 <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" />。</param>
    /// <param name="continuationOptions">用于设置计划延续任务的时间以及延续任务的工作方式的选项。这包括条件（如 <see cref="F:System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled" />）和执行选项（如 <see cref="F:System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously" />）。</param>
    /// <param name="scheduler">要与延续任务关联并用于其执行过程的 <see cref="T:System.Threading.Tasks.TaskScheduler" />。</param>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.Threading.CancellationTokenSource" /> 已释放，创建该标记。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="continuationAction" /> 参数为 null。- 或 -<paramref name="scheduler" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="continuationOptions" /> 参数指定的无效值 <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWith(Action<Task> continuationAction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith(continuationAction, scheduler, cancellationToken, continuationOptions, ref stackMark);
    }

    private Task ContinueWith(Action<Task> continuationAction, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, ref StackCrawlMark stackMark)
    {
      if (continuationAction == null)
        throw new ArgumentNullException("continuationAction");
      if (scheduler == null)
        throw new ArgumentNullException("scheduler");
      TaskCreationOptions creationOptions;
      InternalTaskOptions internalOptions;
      Task.CreationOptionsFromContinuationOptions(continuationOptions, out creationOptions, out internalOptions);
      Task continuationTask = (Task) new ContinuationTaskFromTask(this, (Delegate) continuationAction, (object) null, creationOptions, internalOptions, ref stackMark);
      this.ContinueWithCore(continuationTask, scheduler, cancellationToken, continuationOptions);
      return continuationTask;
    }

    /// <summary>创建一个在目标 <see cref="T:System.Threading.Tasks.Task" /> 完成时接收调用方提供的状态信息并执行的延续任务。</summary>
    /// <returns>一个新的延续任务。</returns>
    /// <param name="continuationAction">在任务完成时要运行的操作。运行时，委托作为一个参数传递给完成的任务和调用方提供的状态对象。</param>
    /// <param name="state">一个表示由该延续操作使用的数据的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="continuationAction" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWith(Action<Task, object> continuationAction, object state)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith(continuationAction, state, TaskScheduler.Current, new CancellationToken(), TaskContinuationOptions.None, ref stackMark);
    }

    /// <summary>创建一个在目标 <see cref="T:System.Threading.Tasks.Task" /> 完成时接收调用方提供的状态信息和取消标记，并以异步方式执行的延续任务。</summary>
    /// <returns>一个新的延续 <see cref="T:System.Threading.Tasks.Task" />。</returns>
    /// <param name="continuationAction">在 <see cref="T:System.Threading.Tasks.Task" /> 完成时要运行的操作。运行时，将传递委托，如完成的任务一样，调用方提供的状态对象（如参数）。</param>
    /// <param name="state">一个表示由该延续操作使用的数据的对象。</param>
    /// <param name="cancellationToken">将指派给新的延续任务的 <see cref="T:System.Threading.CancellationToken" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="continuationAction" /> 参数为 null。</exception>
    /// <exception cref="T:System.ObjectDisposedException">所提供 <see cref="T:System.Threading.CancellationToken" /> 已释放。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWith(Action<Task, object> continuationAction, object state, CancellationToken cancellationToken)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith(continuationAction, state, TaskScheduler.Current, cancellationToken, TaskContinuationOptions.None, ref stackMark);
    }

    /// <summary>创建一个在目标 <see cref="T:System.Threading.Tasks.Task" /> 完成时接收调用方提供的状态信息并以异步方式执行的延续任务。延续任务使用指定计划程序。</summary>
    /// <returns>一个新的延续 <see cref="T:System.Threading.Tasks.Task" />。</returns>
    /// <param name="continuationAction">在 <see cref="T:System.Threading.Tasks.Task" /> 完成时要运行的操作。运行时，将传递委托，如完成的任务一样，调用方提供的状态对象（如参数）。</param>
    /// <param name="state">一个表示由该延续操作使用的数据的对象。</param>
    /// <param name="scheduler">要与延续任务关联并用于其执行过程的 <see cref="T:System.Threading.Tasks.TaskScheduler" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="continuationAction" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="scheduler" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWith(Action<Task, object> continuationAction, object state, TaskScheduler scheduler)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith(continuationAction, state, scheduler, new CancellationToken(), TaskContinuationOptions.None, ref stackMark);
    }

    /// <summary>创建一个在目标 <see cref="T:System.Threading.Tasks.Task" /> 完成时接收调用方提供的状态信息并执行的延续任务。延续任务根据一组指定的条件执行。</summary>
    /// <returns>一个新的延续 <see cref="T:System.Threading.Tasks.Task" />。</returns>
    /// <param name="continuationAction">在 <see cref="T:System.Threading.Tasks.Task" /> 完成时要运行的操作。运行时，将传递委托，如完成的任务一样，调用方提供的状态对象（如参数）。</param>
    /// <param name="state">一个表示由该延续操作使用的数据的对象。</param>
    /// <param name="continuationOptions">用于设置计划延续任务的时间以及延续任务的工作方式的选项。这包括条件（如 <see cref="F:System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled" />）和执行选项（如 <see cref="F:System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously" />）。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="continuationAction" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="continuationOptions" /> 参数指定的无效值 <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWith(Action<Task, object> continuationAction, object state, TaskContinuationOptions continuationOptions)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith(continuationAction, state, TaskScheduler.Current, new CancellationToken(), continuationOptions, ref stackMark);
    }

    /// <summary>创建一个在目标 <see cref="T:System.Threading.Tasks.Task" /> 完成时接收调用方提供的状态信息和取消标记并执行的延续任务。延续任务根据一组指定的条件执行，并使用指定的计划程序。</summary>
    /// <returns>一个新的延续 <see cref="T:System.Threading.Tasks.Task" />。</returns>
    /// <param name="continuationAction">在 <see cref="T:System.Threading.Tasks.Task" /> 完成时要运行的操作。运行时，将传递委托，如完成的任务一样，调用方提供的状态对象（如参数）。</param>
    /// <param name="state">一个表示由该延续操作使用的数据的对象。</param>
    /// <param name="cancellationToken">将指派给新的延续任务的 <see cref="T:System.Threading.CancellationToken" />。</param>
    /// <param name="continuationOptions">用于设置计划延续任务的时间以及延续任务的工作方式的选项。这包括条件（如 <see cref="F:System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled" />）和执行选项（如 <see cref="F:System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously" />）。</param>
    /// <param name="scheduler">要与延续任务关联并用于其执行过程的 <see cref="T:System.Threading.Tasks.TaskScheduler" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="continuationAction" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="continuationOptions" /> 参数指定的无效值 <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="scheduler" /> 参数为 null。</exception>
    /// <exception cref="T:System.ObjectDisposedException">所提供 <see cref="T:System.Threading.CancellationToken" /> 已释放。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWith(Action<Task, object> continuationAction, object state, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith(continuationAction, state, scheduler, cancellationToken, continuationOptions, ref stackMark);
    }

    private Task ContinueWith(Action<Task, object> continuationAction, object state, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, ref StackCrawlMark stackMark)
    {
      if (continuationAction == null)
        throw new ArgumentNullException("continuationAction");
      if (scheduler == null)
        throw new ArgumentNullException("scheduler");
      TaskCreationOptions creationOptions;
      InternalTaskOptions internalOptions;
      Task.CreationOptionsFromContinuationOptions(continuationOptions, out creationOptions, out internalOptions);
      Task continuationTask = (Task) new ContinuationTaskFromTask(this, (Delegate) continuationAction, state, creationOptions, internalOptions, ref stackMark);
      this.ContinueWithCore(continuationTask, scheduler, cancellationToken, continuationOptions);
      return continuationTask;
    }

    /// <summary>创建一个在目标 <see cref="T:System.Threading.Tasks.Task`1" /> 完成并返回一个值时异步执行的延续任务。</summary>
    /// <returns>一个新的延续任务。</returns>
    /// <param name="continuationFunction">在 <see cref="T:System.Threading.Tasks.Task`1" /> 完成时要运行的函数。在运行时，委托将作为一个参数传递给完成的任务。</param>
    /// <typeparam name="TResult"> 延续任务生成的结果的类型。</typeparam>
    /// <exception cref="T:System.ObjectDisposedException">已释放了 <see cref="T:System.Threading.Tasks.Task" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="continuationFunction" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWith<TResult>(Func<Task, TResult> continuationFunction)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith<TResult>(continuationFunction, TaskScheduler.Current, new CancellationToken(), TaskContinuationOptions.None, ref stackMark);
    }

    /// <summary>创建一个在目标 <see cref="T:System.Threading.Tasks.Task" /> 完成并返回一个值时异步执行的延续任务。延续任务收到取消标记。</summary>
    /// <returns>一个新的延续 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    /// <param name="continuationFunction">在 <see cref="T:System.Threading.Tasks.Task" /> 完成时要运行的函数。在运行时，委托将作为一个参数传递给完成的任务。</param>
    /// <param name="cancellationToken">将指派给新的延续任务的 <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" />。</param>
    /// <typeparam name="TResult"> 延续任务生成的结果的类型。</typeparam>
    /// <exception cref="T:System.ObjectDisposedException">已释放了 <see cref="T:System.Threading.Tasks.Task" />。- 或 -<see cref="T:System.Threading.CancellationTokenSource" /> 已释放，创建该标记。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="continuationFunction" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWith<TResult>(Func<Task, TResult> continuationFunction, CancellationToken cancellationToken)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith<TResult>(continuationFunction, TaskScheduler.Current, cancellationToken, TaskContinuationOptions.None, ref stackMark);
    }

    /// <summary>创建一个在目标 <see cref="T:System.Threading.Tasks.Task" /> 完成并返回一个值时异步执行的延续任务。延续任务使用指定计划程序。</summary>
    /// <returns>一个新的延续 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    /// <param name="continuationFunction">在 <see cref="T:System.Threading.Tasks.Task" /> 完成时要运行的函数。在运行时，委托将作为一个参数传递给完成的任务。</param>
    /// <param name="scheduler">要与延续任务关联并用于其执行过程的 <see cref="T:System.Threading.Tasks.TaskScheduler" />。</param>
    /// <typeparam name="TResult"> 延续任务生成的结果的类型。</typeparam>
    /// <exception cref="T:System.ObjectDisposedException">已释放了 <see cref="T:System.Threading.Tasks.Task" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="continuationFunction" /> 参数为 null。- 或 -<paramref name="scheduler" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWith<TResult>(Func<Task, TResult> continuationFunction, TaskScheduler scheduler)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith<TResult>(continuationFunction, scheduler, new CancellationToken(), TaskContinuationOptions.None, ref stackMark);
    }

    /// <summary>创建一个按照指定延续任务选项执行并返回一个值的延续任务。</summary>
    /// <returns>一个新的延续 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    /// <param name="continuationFunction">根据在 <paramref name="continuationOptions" /> 中指定的条件运行的函数。在运行时，委托将作为一个参数传递给完成的任务。</param>
    /// <param name="continuationOptions">用于设置计划延续任务的时间以及延续任务的工作方式的选项。这包括条件（如 <see cref="F:System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled" />）和执行选项（如 <see cref="F:System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously" />）。</param>
    /// <typeparam name="TResult"> 延续任务生成的结果的类型。</typeparam>
    /// <exception cref="T:System.ObjectDisposedException">已释放了 <see cref="T:System.Threading.Tasks.Task" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="continuationFunction" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="continuationOptions" /> 参数指定的无效值 <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWith<TResult>(Func<Task, TResult> continuationFunction, TaskContinuationOptions continuationOptions)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith<TResult>(continuationFunction, TaskScheduler.Current, new CancellationToken(), continuationOptions, ref stackMark);
    }

    /// <summary>创建一个按照指定延续任务选项执行并返回一个值的延续任务。延续任务被传入一个取消标记，并使用指定计划程序。</summary>
    /// <returns>一个新的延续 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    /// <param name="continuationFunction">根据 <paramref name="continuationOptions." /> 中指定的条件运行函数。在运行时，委托将作为一个自变量传递给完成的任务。</param>
    /// <param name="cancellationToken">将指派给新的延续任务的 <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" />。</param>
    /// <param name="continuationOptions">用于设置计划延续任务的时间以及延续任务的工作方式的选项。这包括条件（如 <see cref="F:System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled" />）和执行选项（如 <see cref="F:System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously" />）。</param>
    /// <param name="scheduler">要与延续任务关联并用于其执行过程的 <see cref="T:System.Threading.Tasks.TaskScheduler" />。</param>
    /// <typeparam name="TResult"> 延续任务生成的结果的类型。</typeparam>
    /// <exception cref="T:System.ObjectDisposedException">已释放了 <see cref="T:System.Threading.Tasks.Task" />。- 或 -<see cref="T:System.Threading.CancellationTokenSource" /> 已释放，创建该标记。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="continuationFunction" /> 参数为 null。- 或 -<paramref name="scheduler" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="continuationOptions" /> 参数指定的无效值 <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWith<TResult>(Func<Task, TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith<TResult>(continuationFunction, scheduler, cancellationToken, continuationOptions, ref stackMark);
    }

    private Task<TResult> ContinueWith<TResult>(Func<Task, TResult> continuationFunction, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, ref StackCrawlMark stackMark)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException("continuationFunction");
      if (scheduler == null)
        throw new ArgumentNullException("scheduler");
      TaskCreationOptions creationOptions;
      InternalTaskOptions internalOptions;
      Task.CreationOptionsFromContinuationOptions(continuationOptions, out creationOptions, out internalOptions);
      Task<TResult> task = (Task<TResult>) new ContinuationResultTaskFromTask<TResult>(this, (Delegate) continuationFunction, (object) null, creationOptions, internalOptions, ref stackMark);
      this.ContinueWithCore((Task) task, scheduler, cancellationToken, continuationOptions);
      return task;
    }

    /// <summary>创建一个在目标 <see cref="T:System.Threading.Tasks.Task" /> 完成并返回一个值时接收调用方提供的状态信息并以异步方式执行的延续任务。</summary>
    /// <returns>一个新的延续 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    /// <param name="continuationFunction">在 <see cref="T:System.Threading.Tasks.Task" /> 完成时要运行的函数。运行时，将传递委托，如完成的任务一样，调用方提供的状态对象（如参数）。</param>
    /// <param name="state">一个表示由该延续功能使用的数据的对象。</param>
    /// <typeparam name="TResult">延续任务生成的结果的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="continuationFunction" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWith<TResult>(Func<Task, object, TResult> continuationFunction, object state)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith<TResult>(continuationFunction, state, TaskScheduler.Current, new CancellationToken(), TaskContinuationOptions.None, ref stackMark);
    }

    /// <summary>创建一个在目标 <see cref="T:System.Threading.Tasks.Task" /> 完成并返回一个值时异步执行的延续任务。延续任务接收调用方提供的状态信息和取消标记。</summary>
    /// <returns>一个新的延续 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    /// <param name="continuationFunction">在 <see cref="T:System.Threading.Tasks.Task" /> 完成时要运行的函数。运行时，将传递委托，如完成的任务一样，调用方提供的状态对象（如参数）。</param>
    /// <param name="state">一个表示由该延续功能使用的数据的对象。</param>
    /// <param name="cancellationToken">将指派给新的延续任务的 <see cref="T:System.Threading.CancellationToken" />。</param>
    /// <typeparam name="TResult">延续任务生成的结果的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="continuationFunction" /> 参数为 null。</exception>
    /// <exception cref="T:System.ObjectDisposedException">所提供 <see cref="T:System.Threading.CancellationToken" /> 已释放。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWith<TResult>(Func<Task, object, TResult> continuationFunction, object state, CancellationToken cancellationToken)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith<TResult>(continuationFunction, state, TaskScheduler.Current, cancellationToken, TaskContinuationOptions.None, ref stackMark);
    }

    /// <summary>创建一个在目标 <see cref="T:System.Threading.Tasks.Task" /> 完成时异步执行的延续任务。延续任务接收调用方提供的状态信息，并使用指定计划程序。</summary>
    /// <returns>一个新的延续 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    /// <param name="continuationFunction">在 <see cref="T:System.Threading.Tasks.Task" /> 完成时要运行的函数。运行时，将传递委托，如完成的任务一样，调用方提供的状态对象（如参数）。</param>
    /// <param name="state">一个表示由该延续功能使用的数据的对象。</param>
    /// <param name="scheduler">要与延续任务关联并用于其执行过程的 <see cref="T:System.Threading.Tasks.TaskScheduler" />。</param>
    /// <typeparam name="TResult">延续任务生成的结果的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="continuationFunction" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="scheduler" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWith<TResult>(Func<Task, object, TResult> continuationFunction, object state, TaskScheduler scheduler)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith<TResult>(continuationFunction, state, scheduler, new CancellationToken(), TaskContinuationOptions.None, ref stackMark);
    }

    /// <summary>创建一个在目标 <see cref="T:System.Threading.Tasks.Task" /> 完成时根据指定的任务延续选项执行的延续任务。延续任务接收调用方提供的状态信息。</summary>
    /// <returns>一个新的延续 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    /// <param name="continuationFunction">在 <see cref="T:System.Threading.Tasks.Task" /> 完成时要运行的函数。运行时，将传递委托，如完成的任务一样，调用方提供的状态对象（如参数）。</param>
    /// <param name="state">一个表示由该延续功能使用的数据的对象。</param>
    /// <param name="continuationOptions">用于设置计划延续任务的时间以及延续任务的工作方式的选项。这包括条件（如 <see cref="F:System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled" />）和执行选项（如 <see cref="F:System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously" />）。</param>
    /// <typeparam name="TResult">延续任务生成的结果的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="continuationFunction" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="continuationOptions" /> 参数指定的无效值 <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWith<TResult>(Func<Task, object, TResult> continuationFunction, object state, TaskContinuationOptions continuationOptions)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith<TResult>(continuationFunction, state, TaskScheduler.Current, new CancellationToken(), continuationOptions, ref stackMark);
    }

    /// <summary>创建一个在目标 <see cref="T:System.Threading.Tasks.Task" /> 完成并返回一个值时根据指定的任务延续选项执行的延续任务。延续任务接收调用方提供的状态信息和取消标记，并使用指定计划程序。</summary>
    /// <returns>一个新的延续 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    /// <param name="continuationFunction">在 <see cref="T:System.Threading.Tasks.Task" /> 完成时要运行的函数。运行时，将传递委托，如完成的任务一样，调用方提供的状态对象（如参数）。</param>
    /// <param name="state">一个表示由该延续功能使用的数据的对象。</param>
    /// <param name="cancellationToken">将指派给新的延续任务的 <see cref="T:System.Threading.CancellationToken" />。</param>
    /// <param name="continuationOptions">用于设置计划延续任务的时间以及延续任务的工作方式的选项。这包括条件（如 <see cref="F:System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled" />）和执行选项（如 <see cref="F:System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously" />）。</param>
    /// <param name="scheduler">要与延续任务关联并用于其执行过程的 <see cref="T:System.Threading.Tasks.TaskScheduler" />。</param>
    /// <typeparam name="TResult">延续任务生成的结果的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="continuationFunction" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="continuationOptions" /> 参数指定的无效值 <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="scheduler" /> 参数为 null。</exception>
    /// <exception cref="T:System.ObjectDisposedException">所提供 <see cref="T:System.Threading.CancellationToken" /> 已释放。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWith<TResult>(Func<Task, object, TResult> continuationFunction, object state, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith<TResult>(continuationFunction, state, scheduler, cancellationToken, continuationOptions, ref stackMark);
    }

    private Task<TResult> ContinueWith<TResult>(Func<Task, object, TResult> continuationFunction, object state, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, ref StackCrawlMark stackMark)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException("continuationFunction");
      if (scheduler == null)
        throw new ArgumentNullException("scheduler");
      TaskCreationOptions creationOptions;
      InternalTaskOptions internalOptions;
      Task.CreationOptionsFromContinuationOptions(continuationOptions, out creationOptions, out internalOptions);
      Task<TResult> task = (Task<TResult>) new ContinuationResultTaskFromTask<TResult>(this, (Delegate) continuationFunction, state, creationOptions, internalOptions, ref stackMark);
      this.ContinueWithCore((Task) task, scheduler, cancellationToken, continuationOptions);
      return task;
    }

    internal static void CreationOptionsFromContinuationOptions(TaskContinuationOptions continuationOptions, out TaskCreationOptions creationOptions, out InternalTaskOptions internalOptions)
    {
      TaskContinuationOptions continuationOptions1 = TaskContinuationOptions.OnlyOnRanToCompletion | TaskContinuationOptions.NotOnRanToCompletion;
      TaskContinuationOptions continuationOptions2 = TaskContinuationOptions.PreferFairness | TaskContinuationOptions.LongRunning | TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.DenyChildAttach | TaskContinuationOptions.HideScheduler | TaskContinuationOptions.RunContinuationsAsynchronously;
      TaskContinuationOptions continuationOptions3 = TaskContinuationOptions.LongRunning | TaskContinuationOptions.ExecuteSynchronously;
      if ((continuationOptions & continuationOptions3) == continuationOptions3)
        throw new ArgumentOutOfRangeException("continuationOptions", Environment.GetResourceString("Task_ContinueWith_ESandLR"));
      if ((continuationOptions & ~(continuationOptions2 | continuationOptions1 | TaskContinuationOptions.LazyCancellation | TaskContinuationOptions.ExecuteSynchronously)) != TaskContinuationOptions.None)
        throw new ArgumentOutOfRangeException("continuationOptions");
      if ((continuationOptions & continuationOptions1) == continuationOptions1)
        throw new ArgumentOutOfRangeException("continuationOptions", Environment.GetResourceString("Task_ContinueWith_NotOnAnything"));
      creationOptions = (TaskCreationOptions) (continuationOptions & continuationOptions2);
      internalOptions = InternalTaskOptions.ContinuationTask;
      if ((continuationOptions & TaskContinuationOptions.LazyCancellation) == TaskContinuationOptions.None)
        return;
      internalOptions |= InternalTaskOptions.LazyCancellation;
    }

    internal void ContinueWithCore(Task continuationTask, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions options)
    {
      TaskContinuation continuation = (TaskContinuation) new StandardTaskContinuation(continuationTask, options, scheduler);
      if (cancellationToken.CanBeCanceled)
      {
        if (this.IsCompleted || cancellationToken.IsCancellationRequested)
          continuationTask.AssignCancellationToken(cancellationToken, (Task) null, (TaskContinuation) null);
        else
          continuationTask.AssignCancellationToken(cancellationToken, this, continuation);
      }
      if (continuationTask.IsCompleted)
        return;
      if ((this.Options & (TaskCreationOptions) 1024) != TaskCreationOptions.None && !(this is ITaskCompletionAction))
      {
        TplEtwProvider tplEtwProvider = TplEtwProvider.Log;
        if (tplEtwProvider.IsEnabled())
          tplEtwProvider.AwaitTaskContinuationScheduled(TaskScheduler.Current.Id, Task.CurrentId ?? 0, continuationTask.Id);
      }
      if (this.AddTaskContinuation((object) continuation, false))
        return;
      continuation.Run(this, true);
    }

    internal void AddCompletionAction(ITaskCompletionAction action)
    {
      this.AddCompletionAction(action, false);
    }

    private void AddCompletionAction(ITaskCompletionAction action, bool addBeforeOthers)
    {
      if (this.AddTaskContinuation((object) action, addBeforeOthers))
        return;
      action.Invoke(this);
    }

    private bool AddTaskContinuationComplex(object tc, bool addBeforeOthers)
    {
      object comparand = this.m_continuationObject;
      if (comparand != Task.s_taskCompletionSentinel && !(comparand is List<object>))
        Interlocked.CompareExchange(ref this.m_continuationObject, (object) new List<object>()
        {
          comparand
        }, comparand);
      List<object> objectList = this.m_continuationObject as List<object>;
      if (objectList != null)
      {
        lock (objectList)
        {
          if (this.m_continuationObject != Task.s_taskCompletionSentinel)
          {
            if (objectList.Count == objectList.Capacity)
              objectList.RemoveAll(Task.s_IsTaskContinuationNullPredicate);
            if (addBeforeOthers)
              objectList.Insert(0, tc);
            else
              objectList.Add(tc);
            return true;
          }
        }
      }
      return false;
    }

    private bool AddTaskContinuation(object tc, bool addBeforeOthers)
    {
      if (this.IsCompleted)
        return false;
      if (this.m_continuationObject != null || Interlocked.CompareExchange(ref this.m_continuationObject, tc, (object) null) != null)
        return this.AddTaskContinuationComplex(tc, addBeforeOthers);
      return true;
    }

    internal void RemoveContinuation(object continuationObject)
    {
      object obj = this.m_continuationObject;
      if (obj == Task.s_taskCompletionSentinel)
        return;
      List<object> objectList = obj as List<object>;
      if (objectList == null)
      {
        if (Interlocked.CompareExchange(ref this.m_continuationObject, (object) new List<object>(), continuationObject) == continuationObject)
          return;
        objectList = this.m_continuationObject as List<object>;
      }
      if (objectList == null)
        return;
      lock (objectList)
      {
        if (this.m_continuationObject == Task.s_taskCompletionSentinel)
          return;
        int local_4 = objectList.IndexOf(continuationObject);
        if (local_4 == -1)
          return;
        objectList[local_4] = (object) null;
      }
    }

    /// <summary>等待提供的所有 <see cref="T:System.Threading.Tasks.Task" /> 对象完成执行过程。</summary>
    /// <param name="tasks">要等待的 <see cref="T:System.Threading.Tasks.Task" /> 实例的数组。</param>
    /// <exception cref="T:System.ObjectDisposedException">一个或多个 <see cref="T:System.Threading.Tasks.Task" /> 中的对象 <paramref name="tasks" /> 已被释放。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tasks" /> 参数为 null。- 或 -<paramref name="tasks" /> 参数包含 null 元素。</exception>
    /// <exception cref="T:System.AggregateException">在至少一个 <see cref="T:System.Threading.Tasks.Task" /> 实例已取消。如果任务已被取消， <see cref="T:System.AggregateException" /> 异常包含 <see cref="T:System.OperationCanceledException" /> 中的异常其 <see cref="P:System.AggregateException.InnerExceptions" /> 集合。- 或 -在至少一个执行期间引发了异常 <see cref="T:System.Threading.Tasks.Task" /> 实例。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoOptimization)]
    public static void WaitAll(params Task[] tasks)
    {
      Task.WaitAll(tasks, -1);
    }

    /// <summary>等待所有提供的可取消 <see cref="T:System.Threading.Tasks.Task" /> 对象在指定的时间间隔内完成执行。</summary>
    /// <returns>如果在分配的时间内所有 true 实例都已完成执行，则为 <see cref="T:System.Threading.Tasks.Task" />；否则为 false。</returns>
    /// <param name="tasks">要等待的 <see cref="T:System.Threading.Tasks.Task" /> 实例的数组。</param>
    /// <param name="timeout">表示等待毫秒数的 <see cref="T:System.TimeSpan" />，或表示 -1 毫秒（无限期等待）的 <see cref="T:System.TimeSpan" />。</param>
    /// <exception cref="T:System.ObjectDisposedException">一个或多个 <see cref="T:System.Threading.Tasks.Task" /> 中的对象 <paramref name="tasks" /> 已被释放。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tasks" /> 参数为 null。</exception>
    /// <exception cref="T:System.AggregateException">在至少一个 <see cref="T:System.Threading.Tasks.Task" /> 实例已取消。如果任务已被取消， <see cref="T:System.AggregateException" /> 包含 <see cref="T:System.OperationCanceledException" /> 中其 <see cref="P:System.AggregateException.InnerExceptions" /> 集合。- 或 -在至少一个执行期间引发了异常 <see cref="T:System.Threading.Tasks.Task" /> 实例。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="timeout" /> 是-1 毫秒以外的负数字表示无限超时。- 或 -<paramref name="timeout" /> 大于 <see cref="F:System.Int32.MaxValue" />。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tasks" /> 参数包含 null 元素。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoOptimization)]
    public static bool WaitAll(Task[] tasks, TimeSpan timeout)
    {
      long num = (long) timeout.TotalMilliseconds;
      if (num < -1L || num > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException("timeout");
      return Task.WaitAll(tasks, (int) num);
    }

    /// <summary>等待所有提供的 <see cref="T:System.Threading.Tasks.Task" /> 在指定的毫秒数内完成执行。</summary>
    /// <returns>如果在分配的时间内所有 true 实例都已完成执行，则为 <see cref="T:System.Threading.Tasks.Task" />；否则为 false。</returns>
    /// <param name="tasks">要等待的 <see cref="T:System.Threading.Tasks.Task" /> 实例的数组。</param>
    /// <param name="millisecondsTimeout">等待的毫秒数，或为 <see cref="F:System.Threading.Timeout.Infinite" /> (-1)，表示无限期等待。</param>
    /// <exception cref="T:System.ObjectDisposedException">一个或多个 <see cref="T:System.Threading.Tasks.Task" /> 中的对象 <paramref name="tasks" /> 已被释放。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tasks" /> 参数为 null。</exception>
    /// <exception cref="T:System.AggregateException">在至少一个 <see cref="T:System.Threading.Tasks.Task" /> 实例已取消。如果任务已被取消， <see cref="T:System.AggregateException" /> 包含 <see cref="T:System.OperationCanceledException" /> 中其 <see cref="P:System.AggregateException.InnerExceptions" /> 集合。- 或 -在至少一个执行期间引发了异常 <see cref="T:System.Threading.Tasks.Task" /> 实例。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="millisecondsTimeout" /> 是-1，表示无限超时值之外的负数。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tasks" /> 参数包含 null 元素。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoOptimization)]
    public static bool WaitAll(Task[] tasks, int millisecondsTimeout)
    {
      return Task.WaitAll(tasks, millisecondsTimeout, new CancellationToken());
    }

    /// <summary>等待提供的所有 <see cref="T:System.Threading.Tasks.Task" /> 对象完成执行过程（除非取消等待）。</summary>
    /// <param name="tasks">要等待的 <see cref="T:System.Threading.Tasks.Task" /> 实例的数组。</param>
    /// <param name="cancellationToken">等待任务完成期间要观察的 <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" />。</param>
    /// <exception cref="T:System.OperationCanceledException">
    /// <paramref name="cancellationToken" /> 已取消。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tasks" /> 参数为 null。</exception>
    /// <exception cref="T:System.AggregateException">在至少一个 <see cref="T:System.Threading.Tasks.Task" /> 实例已取消。如果任务已被取消， <see cref="T:System.AggregateException" /> 包含 <see cref="T:System.OperationCanceledException" /> 中其 <see cref="P:System.AggregateException.InnerExceptions" /> 集合。- 或 -在至少一个执行期间引发了异常 <see cref="T:System.Threading.Tasks.Task" /> 实例。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tasks" /> 参数包含 null 元素。</exception>
    /// <exception cref="T:System.ObjectDisposedException">一个或多个 <see cref="T:System.Threading.Tasks.Task" /> 中的对象 <paramref name="tasks" /> 已被释放。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoOptimization)]
    public static void WaitAll(Task[] tasks, CancellationToken cancellationToken)
    {
      Task.WaitAll(tasks, -1, cancellationToken);
    }

    /// <summary>等待提供的所有 <see cref="T:System.Threading.Tasks.Task" /> 对象在指定的毫秒数内完成执行，或等到取消等待。</summary>
    /// <returns>如果在分配的时间内所有 true 实例都已完成执行，则为 <see cref="T:System.Threading.Tasks.Task" />；否则为 false。</returns>
    /// <param name="tasks">要等待的 <see cref="T:System.Threading.Tasks.Task" /> 实例的数组。</param>
    /// <param name="millisecondsTimeout">等待的毫秒数，或为 <see cref="F:System.Threading.Timeout.Infinite" /> (-1)，表示无限期等待。</param>
    /// <param name="cancellationToken">等待任务完成期间要观察的 <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" />。</param>
    /// <exception cref="T:System.ObjectDisposedException">一个或多个 <see cref="T:System.Threading.Tasks.Task" /> 中的对象 <paramref name="tasks" /> 已被释放。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tasks" /> 参数为 null。</exception>
    /// <exception cref="T:System.AggregateException">在至少一个 <see cref="T:System.Threading.Tasks.Task" /> 实例已取消。如果任务已被取消， <see cref="T:System.AggregateException" /> 包含 <see cref="T:System.OperationCanceledException" /> 中其 <see cref="P:System.AggregateException.InnerExceptions" /> 集合。- 或 -在至少一个执行期间引发了异常 <see cref="T:System.Threading.Tasks.Task" /> 实例。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="millisecondsTimeout" /> 是-1，表示无限超时值之外的负数。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tasks" /> 参数包含 null 元素。</exception>
    /// <exception cref="T:System.OperationCanceledException">
    /// <paramref name="cancellationToken" /> 已取消。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoOptimization)]
    public static bool WaitAll(Task[] tasks, int millisecondsTimeout, CancellationToken cancellationToken)
    {
      if (tasks == null)
        throw new ArgumentNullException("tasks");
      if (millisecondsTimeout < -1)
        throw new ArgumentOutOfRangeException("millisecondsTimeout");
      cancellationToken.ThrowIfCancellationRequested();
      List<System.Exception> exceptions = (List<System.Exception>) null;
      List<Task> list1 = (List<Task>) null;
      List<Task> list2 = (List<Task>) null;
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = true;
      for (int index = tasks.Length - 1; index >= 0; --index)
      {
        Task task = tasks[index];
        if (task == null)
          throw new ArgumentException(Environment.GetResourceString("Task_WaitMulti_NullTask"), "tasks");
        bool flag4 = task.IsCompleted;
        if (!flag4)
        {
          if (millisecondsTimeout != -1 || cancellationToken.CanBeCanceled)
          {
            Task.AddToList<Task>(task, ref list1, tasks.Length);
          }
          else
          {
            flag4 = task.WrappedTryRunInline() && task.IsCompleted;
            if (!flag4)
              Task.AddToList<Task>(task, ref list1, tasks.Length);
          }
        }
        if (flag4)
        {
          if (task.IsFaulted)
            flag1 = true;
          else if (task.IsCanceled)
            flag2 = true;
          if (task.IsWaitNotificationEnabled)
            Task.AddToList<Task>(task, ref list2, 1);
        }
      }
      if (list1 != null)
      {
        flag3 = Task.WaitAllBlockingCore(list1, millisecondsTimeout, cancellationToken);
        if (flag3)
        {
          foreach (Task task in list1)
          {
            if (task.IsFaulted)
              flag1 = true;
            else if (task.IsCanceled)
              flag2 = true;
            if (task.IsWaitNotificationEnabled)
              Task.AddToList<Task>(task, ref list2, 1);
          }
        }
        GC.KeepAlive((object) tasks);
      }
      if (flag3 && list2 != null)
      {
        foreach (Task task in list2)
        {
          if (task.NotifyDebuggerOfWaitCompletionIfNecessary())
            break;
        }
      }
      if (flag3 && flag1 | flag2)
      {
        if (!flag1)
          cancellationToken.ThrowIfCancellationRequested();
        foreach (Task task in tasks)
          Task.AddExceptionsForCompletedTask(ref exceptions, task);
        throw new AggregateException((IEnumerable<System.Exception>) exceptions);
      }
      return flag3;
    }

    private static void AddToList<T>(T item, ref List<T> list, int initSize)
    {
      if (list == null)
        list = new List<T>(initSize);
      list.Add(item);
    }

    private static bool WaitAllBlockingCore(List<Task> tasks, int millisecondsTimeout, CancellationToken cancellationToken)
    {
      bool flag = false;
      Task.SetOnCountdownMres setOnCountdownMres = new Task.SetOnCountdownMres(tasks.Count);
      try
      {
        foreach (Task task in tasks)
          task.AddCompletionAction((ITaskCompletionAction) setOnCountdownMres, true);
        flag = setOnCountdownMres.Wait(millisecondsTimeout, cancellationToken);
        return flag;
      }
      finally
      {
        if (!flag)
        {
          foreach (Task task in tasks)
          {
            if (!task.IsCompleted)
              task.RemoveContinuation((object) setOnCountdownMres);
          }
        }
      }
    }

    internal static void FastWaitAll(Task[] tasks)
    {
      List<System.Exception> exceptions = (List<System.Exception>) null;
      for (int index = tasks.Length - 1; index >= 0; --index)
      {
        if (!tasks[index].IsCompleted)
          tasks[index].WrappedTryRunInline();
      }
      for (int index = tasks.Length - 1; index >= 0; --index)
      {
        Task t = tasks[index];
        t.SpinThenBlockingWait(-1, new CancellationToken());
        Task.AddExceptionsForCompletedTask(ref exceptions, t);
      }
      if (exceptions != null)
        throw new AggregateException((IEnumerable<System.Exception>) exceptions);
    }

    internal static void AddExceptionsForCompletedTask(ref List<System.Exception> exceptions, Task t)
    {
      AggregateException exceptions1 = t.GetExceptions(true);
      if (exceptions1 == null)
        return;
      t.UpdateExceptionObservedStatus();
      if (exceptions == null)
        exceptions = new List<System.Exception>(exceptions1.InnerExceptions.Count);
      exceptions.AddRange((IEnumerable<System.Exception>) exceptions1.InnerExceptions);
    }

    /// <summary>等待提供的任一 <see cref="T:System.Threading.Tasks.Task" /> 对象完成执行过程。</summary>
    /// <returns>已完成的任务在 <paramref name="tasks" /> 数组参数中的索引。</returns>
    /// <param name="tasks">要等待的 <see cref="T:System.Threading.Tasks.Task" /> 实例的数组。</param>
    /// <exception cref="T:System.ObjectDisposedException">已释放了 <see cref="T:System.Threading.Tasks.Task" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tasks" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tasks" /> 参数包含 null 元素。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoOptimization)]
    public static int WaitAny(params Task[] tasks)
    {
      return Task.WaitAny(tasks, -1);
    }

    /// <summary>等待任何提供的 <see cref="T:System.Threading.Tasks.Task" /> 对象在指定的时间间隔内完成执行。</summary>
    /// <returns>已完成的任务在 <paramref name="tasks" /> 数组参数中的索引，如果发生超时，则为 -1。</returns>
    /// <param name="tasks">要等待的 <see cref="T:System.Threading.Tasks.Task" /> 实例的数组。</param>
    /// <param name="timeout">表示等待毫秒数的 <see cref="T:System.TimeSpan" />，或表示 -1 毫秒（无限期等待）的 <see cref="T:System.TimeSpan" />。</param>
    /// <exception cref="T:System.ObjectDisposedException">已释放了 <see cref="T:System.Threading.Tasks.Task" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tasks" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="timeout" /> 是-1 毫秒以外的负数字表示无限超时。- 或 -<paramref name="timeout" /> 大于 <see cref="F:System.Int32.MaxValue" />。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tasks" /> 参数包含 null 元素。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoOptimization)]
    public static int WaitAny(Task[] tasks, TimeSpan timeout)
    {
      long num = (long) timeout.TotalMilliseconds;
      if (num < -1L || num > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException("timeout");
      return Task.WaitAny(tasks, (int) num);
    }

    /// <summary>等待提供的任何 <see cref="T:System.Threading.Tasks.Task" /> 对象完成执行过程（除非取消等待）。</summary>
    /// <returns>已完成的任务在 <paramref name="tasks" /> 数组参数中的索引。</returns>
    /// <param name="tasks">要等待的 <see cref="T:System.Threading.Tasks.Task" /> 实例的数组。</param>
    /// <param name="cancellationToken">等待任务完成期间要观察的 <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" />。</param>
    /// <exception cref="T:System.ObjectDisposedException">已释放了 <see cref="T:System.Threading.Tasks.Task" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tasks" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tasks" /> 参数包含 null 元素。</exception>
    /// <exception cref="T:System.OperationCanceledException">
    /// <paramref name="cancellationToken" /> 已取消。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoOptimization)]
    public static int WaitAny(Task[] tasks, CancellationToken cancellationToken)
    {
      return Task.WaitAny(tasks, -1, cancellationToken);
    }

    /// <summary>等待任何提供的 <see cref="T:System.Threading.Tasks.Task" /> 对象在指定的毫秒数内完成执行。</summary>
    /// <returns>已完成的任务在 <paramref name="tasks" /> 数组参数中的索引，如果发生超时，则为 -1。</returns>
    /// <param name="tasks">要等待的 <see cref="T:System.Threading.Tasks.Task" /> 实例的数组。</param>
    /// <param name="millisecondsTimeout">等待的毫秒数，或为 <see cref="F:System.Threading.Timeout.Infinite" /> (-1)，表示无限期等待。</param>
    /// <exception cref="T:System.ObjectDisposedException">已释放了 <see cref="T:System.Threading.Tasks.Task" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tasks" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="millisecondsTimeout" /> 是-1，表示无限超时值之外的负数。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tasks" /> 参数包含 null 元素。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoOptimization)]
    public static int WaitAny(Task[] tasks, int millisecondsTimeout)
    {
      return Task.WaitAny(tasks, millisecondsTimeout, new CancellationToken());
    }

    /// <summary>等待提供的任何 <see cref="T:System.Threading.Tasks.Task" /> 对象在指定的毫秒数内完成执行，或等到取消标记取消。</summary>
    /// <returns>已完成的任务在 <paramref name="tasks" /> 数组参数中的索引，如果发生超时，则为 -1。</returns>
    /// <param name="tasks">要等待的 <see cref="T:System.Threading.Tasks.Task" /> 实例的数组。</param>
    /// <param name="millisecondsTimeout">等待的毫秒数，或为 <see cref="F:System.Threading.Timeout.Infinite" /> (-1)，表示无限期等待。</param>
    /// <param name="cancellationToken">等待任务完成期间要观察的 <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" />。</param>
    /// <exception cref="T:System.ObjectDisposedException">已释放了 <see cref="T:System.Threading.Tasks.Task" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tasks" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="millisecondsTimeout" /> 是-1，表示无限超时值之外的负数。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tasks" /> 参数包含 null 元素。</exception>
    /// <exception cref="T:System.OperationCanceledException">
    /// <paramref name="cancellationToken" /> 已取消。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoOptimization)]
    public static int WaitAny(Task[] tasks, int millisecondsTimeout, CancellationToken cancellationToken)
    {
      if (tasks == null)
        throw new ArgumentNullException("tasks");
      if (millisecondsTimeout < -1)
        throw new ArgumentOutOfRangeException("millisecondsTimeout");
      cancellationToken.ThrowIfCancellationRequested();
      int num = -1;
      for (int index = 0; index < tasks.Length; ++index)
      {
        Task task = tasks[index];
        if (task == null)
          throw new ArgumentException(Environment.GetResourceString("Task_WaitMulti_NullTask"), "tasks");
        if (num == -1 && task.IsCompleted)
          num = index;
      }
      if (num == -1 && tasks.Length != 0)
      {
        Task<Task> task = TaskFactory.CommonCWAnyLogic((IList<Task>) tasks);
        if (task.Wait(millisecondsTimeout, cancellationToken))
          num = Array.IndexOf<Task>(tasks, task.Result);
      }
      GC.KeepAlive((object) tasks);
      return num;
    }

    /// <summary>创建指定结果的、成功完成的 <see cref="T:System.Threading.Tasks.Task`1" />。</summary>
    /// <returns>已成功完成的任务。</returns>
    /// <param name="result">存储入已完成任务的结果。</param>
    /// <typeparam name="TResult">任务返回的结果的类型。</typeparam>
    [__DynamicallyInvokable]
    public static Task<TResult> FromResult<TResult>(TResult result)
    {
      return new Task<TResult>(result);
    }

    /// <summary>创建 <see cref="T:System.Threading.Tasks.Task" />，它是以指定的异常来完成的。</summary>
    /// <returns>出错的任务。</returns>
    /// <param name="exception">完成任务的异常。</param>
    [__DynamicallyInvokable]
    public static Task FromException(System.Exception exception)
    {
      return (Task) Task.FromException<VoidTaskResult>(exception);
    }

    /// <summary>创建 <see cref="T:System.Threading.Tasks.Task`1" />，它是以指定的异常来完成的。</summary>
    /// <returns>出错的任务。</returns>
    /// <param name="exception">完成任务的异常。</param>
    /// <typeparam name="TResult">任务返回的结果的类型。</typeparam>
    [__DynamicallyInvokable]
    public static Task<TResult> FromException<TResult>(System.Exception exception)
    {
      if (exception == null)
        throw new ArgumentNullException("exception");
      Task<TResult> task = new Task<TResult>();
      System.Exception exception1 = exception;
      task.TrySetException((object) exception1);
      return task;
    }

    [FriendAccessAllowed]
    internal static Task FromCancellation(CancellationToken cancellationToken)
    {
      if (!cancellationToken.IsCancellationRequested)
        throw new ArgumentOutOfRangeException("cancellationToken");
      return new Task(true, TaskCreationOptions.None, cancellationToken);
    }

    /// <summary>创建 <see cref="T:System.Threading.Tasks.Task" />，它因指定的取消标记进行的取消操作而完成。</summary>
    /// <returns>取消的任务。</returns>
    /// <param name="cancellationToken">完成任务的取消标记。</param>
    [__DynamicallyInvokable]
    public static Task FromCanceled(CancellationToken cancellationToken)
    {
      return Task.FromCancellation(cancellationToken);
    }

    [FriendAccessAllowed]
    internal static Task<TResult> FromCancellation<TResult>(CancellationToken cancellationToken)
    {
      if (!cancellationToken.IsCancellationRequested)
        throw new ArgumentOutOfRangeException("cancellationToken");
      return new Task<TResult>(true, default (TResult), TaskCreationOptions.None, cancellationToken);
    }

    /// <summary>创建 <see cref="T:System.Threading.Tasks.Task`1" />，它因指定的取消标记进行的取消操作而完成。</summary>
    /// <returns>取消的任务。</returns>
    /// <param name="cancellationToken">完成任务的取消标记。</param>
    /// <typeparam name="TResult">任务返回的结果的类型。</typeparam>
    [__DynamicallyInvokable]
    public static Task<TResult> FromCanceled<TResult>(CancellationToken cancellationToken)
    {
      return Task.FromCancellation<TResult>(cancellationToken);
    }

    internal static Task<TResult> FromCancellation<TResult>(OperationCanceledException exception)
    {
      if (exception == null)
        throw new ArgumentNullException("exception");
      Task<TResult> task = new Task<TResult>();
      CancellationToken cancellationToken = exception.CancellationToken;
      OperationCanceledException canceledException = exception;
      task.TrySetCanceled(cancellationToken, (object) canceledException);
      return task;
    }

    /// <summary>将在线程池上运行的指定工作排队，并返回该工作的任务句柄。</summary>
    /// <returns>表示在线程池执行的队列的任务。</returns>
    /// <param name="action">以异步方式执行的工作量。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="action" /> 参数是 null。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Task Run(Action action)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return Task.InternalStartNew((Task) null, (Delegate) action, (object) null, new CancellationToken(), TaskScheduler.Default, TaskCreationOptions.DenyChildAttach, InternalTaskOptions.None, ref stackMark);
    }

    /// <summary>将在线程池上运行的指定工作排队，并返回该工作的任务句柄。</summary>
    /// <returns>表示在线程池执行的队列的任务。</returns>
    /// <param name="action">以异步方式执行的工作量。</param>
    /// <param name="cancellationToken">应用以取消工作的取消标记</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="action" /> 参数是 null。</exception>
    /// <exception cref="T:System.Threading.Tasks.TaskCanceledException">已取消该任务。</exception>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.Threading.CancellationTokenSource" /> 与关联 <paramref name="cancellationToken" /> 已释放。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Task Run(Action action, CancellationToken cancellationToken)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return Task.InternalStartNew((Task) null, (Delegate) action, (object) null, cancellationToken, TaskScheduler.Default, TaskCreationOptions.DenyChildAttach, InternalTaskOptions.None, ref stackMark);
    }

    /// <summary>将在线程池上运行的指定工作排队，并返回代表该工作的 <see cref="T:System.Threading.Tasks.Task`1" /> 对象。</summary>
    /// <returns>表示在线程池中排队执行的工作的任务对象。</returns>
    /// <param name="function">以异步方式执行的工作。</param>
    /// <typeparam name="TResult">任务的返回类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="function" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Task<TResult> Run<TResult>(Func<TResult> function)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return Task<TResult>.StartNew((Task) null, function, new CancellationToken(), TaskCreationOptions.DenyChildAttach, InternalTaskOptions.None, TaskScheduler.Default, ref stackMark);
    }

    /// <summary>将在线程池上运行的指定工作排队，并返回该工作的 Task(TResult) 句柄。</summary>
    /// <returns>表示在线程池执行的队列的 Task(TResult)。</returns>
    /// <param name="function">以异步方式执行的工作量。</param>
    /// <param name="cancellationToken">应用以取消工作的取消标记</param>
    /// <typeparam name="TResult">任务的结果类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="function" /> 参数是 null。</exception>
    /// <exception cref="T:System.Threading.Tasks.TaskCanceledException">已取消该任务。</exception>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.Threading.CancellationTokenSource" /> 与关联 <paramref name="cancellationToken" /> 已释放。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Task<TResult> Run<TResult>(Func<TResult> function, CancellationToken cancellationToken)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return Task<TResult>.StartNew((Task) null, function, cancellationToken, TaskCreationOptions.DenyChildAttach, InternalTaskOptions.None, TaskScheduler.Default, ref stackMark);
    }

    /// <summary>将在线程池上运行的指定工作排队，并返回 <paramref name="function" /> 返回的任务的代理项。</summary>
    /// <returns>表示由 <paramref name="function" /> 返回的任务代理的任务。</returns>
    /// <param name="function">以异步方式执行的工作量。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="function" /> 参数是 null。</exception>
    [__DynamicallyInvokable]
    public static Task Run(Func<Task> function)
    {
      return Task.Run(function, new CancellationToken());
    }

    /// <summary>将在线程池上运行的指定工作排队，并返回 <paramref name="function" /> 返回的任务的代理项。</summary>
    /// <returns>表示由 <paramref name="function" /> 返回的任务代理的任务。</returns>
    /// <param name="function">以异步方式执行的工作。</param>
    /// <param name="cancellationToken">应用以取消工作的取消标记。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="function" /> 参数是 null。</exception>
    /// <exception cref="T:System.Threading.Tasks.TaskCanceledException">已取消该任务。</exception>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.Threading.CancellationTokenSource" /> 与关联 <paramref name="cancellationToken" /> 已释放。</exception>
    [__DynamicallyInvokable]
    public static Task Run(Func<Task> function, CancellationToken cancellationToken)
    {
      if (function == null)
        throw new ArgumentNullException("function");
      if (AppContextSwitches.ThrowExceptionIfDisposedCancellationTokenSource)
        cancellationToken.ThrowIfSourceDisposed();
      if (cancellationToken.IsCancellationRequested)
        return Task.FromCancellation(cancellationToken);
      return (Task) new UnwrapPromise<VoidTaskResult>((Task) Task<Task>.Factory.StartNew(function, cancellationToken, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default), true);
    }

    /// <summary>将在线程池上运行的指定工作排队，并返回 Task(TResult) 返回的 <paramref name="function" /> 的代理项。</summary>
    /// <returns>表示由 Task(TResult) 返回的 Task(TResult) 的代理的 <paramref name="function" />。</returns>
    /// <param name="function">以异步方式执行的工作量。</param>
    /// <typeparam name="TResult">代理任务返回的结果的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="function" /> 参数是 null。</exception>
    [__DynamicallyInvokable]
    public static Task<TResult> Run<TResult>(Func<Task<TResult>> function)
    {
      return Task.Run<TResult>(function, new CancellationToken());
    }

    /// <summary>将在线程池上运行的指定工作排队，并返回 Task(TResult) 返回的 <paramref name="function" /> 的代理项。</summary>
    /// <returns>表示由 Task(TResult) 返回的 Task(TResult) 的代理的 <paramref name="function" />。</returns>
    /// <param name="function">以异步方式执行的工作量。</param>
    /// <param name="cancellationToken">应用以取消工作的取消标记</param>
    /// <typeparam name="TResult">代理任务返回的结果的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="function" /> 参数是 null。</exception>
    /// <exception cref="T:System.Threading.Tasks.TaskCanceledException">已取消该任务。</exception>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.Threading.CancellationTokenSource" /> 与关联 <paramref name="cancellationToken" /> 已释放。</exception>
    [__DynamicallyInvokable]
    public static Task<TResult> Run<TResult>(Func<Task<TResult>> function, CancellationToken cancellationToken)
    {
      if (function == null)
        throw new ArgumentNullException("function");
      if (AppContextSwitches.ThrowExceptionIfDisposedCancellationTokenSource)
        cancellationToken.ThrowIfSourceDisposed();
      if (cancellationToken.IsCancellationRequested)
        return Task.FromCancellation<TResult>(cancellationToken);
      return (Task<TResult>) new UnwrapPromise<TResult>((Task) Task<Task<TResult>>.Factory.StartNew(function, cancellationToken, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default), true);
    }

    /// <summary>创建一个在指定的时间间隔后完成的任务。</summary>
    /// <returns>表示时间延迟的任务。</returns>
    /// <param name="delay">在完成返回的任务前等待的时间跨度；如果无限期等待，则为 TimeSpan.FromMilliseconds(-1)。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="delay" /> 表示一个负时间间隔以外 TimeSpan.FromMillseconds(-1)。- 或 -<paramref name="delay" /> 参数的 <see cref="P:System.TimeSpan.TotalMilliseconds" /> 属性大于 <see cref="F:System.Int32.MaxValue" />。</exception>
    [__DynamicallyInvokable]
    public static Task Delay(TimeSpan delay)
    {
      return Task.Delay(delay, new CancellationToken());
    }

    /// <summary>创建一个在指定的时间间隔后完成的可取消任务。</summary>
    /// <returns>表示时间延迟的任务。</returns>
    /// <param name="delay">在完成返回的任务前等待的时间跨度；如果无限期等待，则为 TimeSpan.FromMilliseconds(-1)。</param>
    /// <param name="cancellationToken">将在完成返回的任务之前选中的取消标记。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="delay" /> 表示一个负时间间隔以外 TimeSpan.FromMillseconds(-1)。- 或 -<paramref name="delay" /> 参数的 <see cref="P:System.TimeSpan.TotalMilliseconds" /> 属性大于 <see cref="F:System.Int32.MaxValue" />。</exception>
    /// <exception cref="T:System.Threading.Tasks.TaskCanceledException">已取消该任务。</exception>
    /// <exception cref="T:System.ObjectDisposedException">所提供 <paramref name="cancellationToken" /> 已释放。</exception>
    [__DynamicallyInvokable]
    public static Task Delay(TimeSpan delay, CancellationToken cancellationToken)
    {
      long num = (long) delay.TotalMilliseconds;
      if (num < -1L || num > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException("delay", Environment.GetResourceString("Task_Delay_InvalidDelay"));
      return Task.Delay((int) num, cancellationToken);
    }

    /// <summary>创建将在时间延迟后完成的任务。</summary>
    /// <returns>表示时间延迟的任务。</returns>
    /// <param name="millisecondsDelay">在完成返回的任务前要等待的毫秒数；如果无限期等待，则为 -1。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="millisecondsDelay" /> 参数是小于-1。</exception>
    [__DynamicallyInvokable]
    public static Task Delay(int millisecondsDelay)
    {
      return Task.Delay(millisecondsDelay, new CancellationToken());
    }

    /// <summary>创建将在时间延迟后完成的可取消任务。</summary>
    /// <returns>表示时间延迟的任务。</returns>
    /// <param name="millisecondsDelay">在完成返回的任务前要等待的毫秒数；如果无限期等待，则为 -1。</param>
    /// <param name="cancellationToken">将在完成返回的任务之前选中的取消标记。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="millisecondsDelay" /> 参数是小于-1。</exception>
    /// <exception cref="T:System.Threading.Tasks.TaskCanceledException">已取消该任务。</exception>
    /// <exception cref="T:System.ObjectDisposedException">所提供 <paramref name="cancellationToken" /> 已释放。</exception>
    [__DynamicallyInvokable]
    public static Task Delay(int millisecondsDelay, CancellationToken cancellationToken)
    {
      if (millisecondsDelay < -1)
        throw new ArgumentOutOfRangeException("millisecondsDelay", Environment.GetResourceString("Task_Delay_InvalidMillisecondsDelay"));
      if (cancellationToken.IsCancellationRequested)
        return Task.FromCancellation(cancellationToken);
      if (millisecondsDelay == 0)
        return Task.CompletedTask;
      Task.DelayPromise delayPromise1 = new Task.DelayPromise(cancellationToken);
      if (cancellationToken.CanBeCanceled)
      {
        Task.DelayPromise delayPromise2 = delayPromise1;
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        CancellationToken& local = @cancellationToken;
        Task.DelayPromise delayPromise3 = delayPromise1;
        // ISSUE: explicit reference operation
        CancellationTokenRegistration tokenRegistration = (^local).InternalRegisterWithoutEC((Action<object>) (state => ((Task.DelayPromise) state).Complete()), (object) delayPromise3);
        delayPromise2.Registration = tokenRegistration;
      }
      if (millisecondsDelay != -1)
      {
        Task.DelayPromise delayPromise2 = delayPromise1;
        Task.DelayPromise delayPromise3 = delayPromise1;
        int dueTime = millisecondsDelay;
        int period = -1;
        Timer timer = new Timer((TimerCallback) (state => ((Task.DelayPromise) state).Complete()), (object) delayPromise3, dueTime, period);
        delayPromise2.Timer = timer;
        delayPromise1.Timer.KeepRootedWhileScheduled();
      }
      return (Task) delayPromise1;
    }

    /// <summary>创建一个任务，该任务将在可枚举集合中的所有 <see cref="T:System.Threading.Tasks.Task" /> 对象都完成时完成。</summary>
    /// <returns>表示所有提供的任务的完成情况的任务。</returns>
    /// <param name="tasks">等待完成的任务。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tasks" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tasks" /> 包含集合 null 任务。</exception>
    [__DynamicallyInvokable]
    public static Task WhenAll(IEnumerable<Task> tasks)
    {
      Task[] taskArray = tasks as Task[];
      if (taskArray != null)
        return Task.WhenAll(taskArray);
      ICollection<Task> tasks1 = tasks as ICollection<Task>;
      if (tasks1 != null)
      {
        int num = 0;
        Task[] tasks2 = new Task[tasks1.Count];
        foreach (Task task in tasks)
        {
          if (task == null)
            throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_NullTask"), "tasks");
          tasks2[num++] = task;
        }
        return Task.InternalWhenAll(tasks2);
      }
      if (tasks == null)
        throw new ArgumentNullException("tasks");
      List<Task> taskList = new List<Task>();
      foreach (Task task in tasks)
      {
        if (task == null)
          throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_NullTask"), "tasks");
        taskList.Add(task);
      }
      return Task.InternalWhenAll(taskList.ToArray());
    }

    /// <summary>创建一个任务，该任务将在数组中的所有 <see cref="T:System.Threading.Tasks.Task" /> 对象都完成时完成。</summary>
    /// <returns>表示所有提供的任务的完成情况的任务。</returns>
    /// <param name="tasks">等待完成的任务。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tasks" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tasks" /> 包含数组 null 任务。</exception>
    [__DynamicallyInvokable]
    public static Task WhenAll(params Task[] tasks)
    {
      if (tasks == null)
        throw new ArgumentNullException("tasks");
      int length = tasks.Length;
      if (length == 0)
        return Task.InternalWhenAll(tasks);
      Task[] tasks1 = new Task[length];
      for (int index = 0; index < length; ++index)
      {
        Task task = tasks[index];
        if (task == null)
          throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_NullTask"), "tasks");
        tasks1[index] = task;
      }
      return Task.InternalWhenAll(tasks1);
    }

    private static Task InternalWhenAll(Task[] tasks)
    {
      if (tasks.Length != 0)
        return (Task) new Task.WhenAllPromise(tasks);
      return Task.CompletedTask;
    }

    /// <summary>创建一个任务，该任务将在可枚举集合中的所有 <see cref="T:System.Threading.Tasks.Task`1" /> 对象都完成时完成。</summary>
    /// <returns>表示所有提供的任务的完成情况的任务。</returns>
    /// <param name="tasks">等待完成的任务。</param>
    /// <typeparam name="TResult">已完成任务的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tasks" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tasks" /> 包含集合 null 任务。</exception>
    [__DynamicallyInvokable]
    public static Task<TResult[]> WhenAll<TResult>(IEnumerable<Task<TResult>> tasks)
    {
      Task<TResult>[] taskArray = tasks as Task<TResult>[];
      if (taskArray != null)
        return Task.WhenAll<TResult>(taskArray);
      ICollection<Task<TResult>> tasks1 = tasks as ICollection<Task<TResult>>;
      if (tasks1 != null)
      {
        int num = 0;
        Task<TResult>[] tasks2 = new Task<TResult>[tasks1.Count];
        foreach (Task<TResult> task in tasks)
        {
          if (task == null)
            throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_NullTask"), "tasks");
          tasks2[num++] = task;
        }
        return Task.InternalWhenAll<TResult>(tasks2);
      }
      if (tasks == null)
        throw new ArgumentNullException("tasks");
      List<Task<TResult>> taskList = new List<Task<TResult>>();
      foreach (Task<TResult> task in tasks)
      {
        if (task == null)
          throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_NullTask"), "tasks");
        taskList.Add(task);
      }
      return Task.InternalWhenAll<TResult>(taskList.ToArray());
    }

    /// <summary>创建一个任务，该任务将在数组中的所有 <see cref="T:System.Threading.Tasks.Task`1" /> 对象都完成时完成。</summary>
    /// <returns>表示所有提供的任务的完成情况的任务。</returns>
    /// <param name="tasks">等待完成的任务。</param>
    /// <typeparam name="TResult">已完成任务的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tasks" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tasks" /> 包含数组 null 任务。</exception>
    [__DynamicallyInvokable]
    public static Task<TResult[]> WhenAll<TResult>(params Task<TResult>[] tasks)
    {
      if (tasks == null)
        throw new ArgumentNullException("tasks");
      int length = tasks.Length;
      if (length == 0)
        return Task.InternalWhenAll<TResult>(tasks);
      Task<TResult>[] tasks1 = new Task<TResult>[length];
      for (int index = 0; index < length; ++index)
      {
        Task<TResult> task = tasks[index];
        if (task == null)
          throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_NullTask"), "tasks");
        tasks1[index] = task;
      }
      return Task.InternalWhenAll<TResult>(tasks1);
    }

    private static Task<TResult[]> InternalWhenAll<TResult>(Task<TResult>[] tasks)
    {
      if (tasks.Length != 0)
        return (Task<TResult[]>) new Task.WhenAllPromise<TResult>(tasks);
      return new Task<TResult[]>(false, new TResult[0], TaskCreationOptions.None, new CancellationToken());
    }

    /// <summary>任何提供的任务已完成时，创建将完成的任务。</summary>
    /// <returns>表示提供的任务之一已完成的任务。返回任务的结果是完成的任务。</returns>
    /// <param name="tasks">等待完成的任务。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tasks" /> 参数为空。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tasks" /> 数组包含 null 的任务中，或为空。</exception>
    [__DynamicallyInvokable]
    public static Task<Task> WhenAny(params Task[] tasks)
    {
      if (tasks == null)
        throw new ArgumentNullException("tasks");
      if (tasks.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_EmptyTaskList"), "tasks");
      int length = tasks.Length;
      Task[] taskArray = new Task[length];
      for (int index = 0; index < length; ++index)
      {
        Task task = tasks[index];
        if (task == null)
          throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_NullTask"), "tasks");
        taskArray[index] = task;
      }
      return TaskFactory.CommonCWAnyLogic((IList<Task>) taskArray);
    }

    /// <summary>任何提供的任务已完成时，创建将完成的任务。</summary>
    /// <returns>表示提供的任务之一已完成的任务。返回任务的结果是完成的任务。</returns>
    /// <param name="tasks">等待完成的任务。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tasks" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tasks" /> 数组包含 null 的任务中，或为空。</exception>
    [__DynamicallyInvokable]
    public static Task<Task> WhenAny(IEnumerable<Task> tasks)
    {
      if (tasks == null)
        throw new ArgumentNullException("tasks");
      List<Task> taskList = new List<Task>();
      foreach (Task task in tasks)
      {
        if (task == null)
          throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_NullTask"), "tasks");
        taskList.Add(task);
      }
      if (taskList.Count == 0)
        throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_EmptyTaskList"), "tasks");
      return TaskFactory.CommonCWAnyLogic((IList<Task>) taskList);
    }

    /// <summary>任何提供的任务已完成时，创建将完成的任务。</summary>
    /// <returns>表示提供的任务之一已完成的任务。返回任务的结果是完成的任务。</returns>
    /// <param name="tasks">等待完成的任务。</param>
    /// <typeparam name="TResult">已完成任务的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tasks" /> 参数为空。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tasks" /> 数组包含 null 的任务中，或为空。</exception>
    [__DynamicallyInvokable]
    public static Task<Task<TResult>> WhenAny<TResult>(params Task<TResult>[] tasks)
    {
      return Task.WhenAny((Task[]) tasks).ContinueWith<Task<TResult>>(Task<TResult>.TaskWhenAnyCast, new CancellationToken(), TaskContinuationOptions.DenyChildAttach | TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
    }

    /// <summary>任何提供的任务已完成时，创建将完成的任务。</summary>
    /// <returns>表示提供的任务之一已完成的任务。返回任务的结果是完成的任务。</returns>
    /// <param name="tasks">等待完成的任务。</param>
    /// <typeparam name="TResult">已完成任务的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tasks" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tasks" /> 数组包含 null 的任务中，或为空。</exception>
    [__DynamicallyInvokable]
    public static Task<Task<TResult>> WhenAny<TResult>(IEnumerable<Task<TResult>> tasks)
    {
      return Task.WhenAny((IEnumerable<Task>) tasks).ContinueWith<Task<TResult>>(Task<TResult>.TaskWhenAnyCast, new CancellationToken(), TaskContinuationOptions.DenyChildAttach | TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
    }

    [FriendAccessAllowed]
    internal static Task<TResult> CreateUnwrapPromise<TResult>(Task outerTask, bool lookForOce)
    {
      return (Task<TResult>) new UnwrapPromise<TResult>(outerTask, lookForOce);
    }

    internal virtual Delegate[] GetDelegateContinuationsForDebugger()
    {
      if (this.m_continuationObject != this)
        return Task.GetDelegatesFromContinuationObject(this.m_continuationObject);
      return (Delegate[]) null;
    }

    internal static Delegate[] GetDelegatesFromContinuationObject(object continuationObject)
    {
      if (continuationObject != null)
      {
        Action action = continuationObject as Action;
        if (action != null)
          return new Delegate[1]{ (Delegate) AsyncMethodBuilderCore.TryGetStateMachineForDebugger(action) };
        TaskContinuation taskContinuation = continuationObject as TaskContinuation;
        if (taskContinuation != null)
          return taskContinuation.GetDelegateContinuationsForDebugger();
        Task task = continuationObject as Task;
        if (task != null)
        {
          Delegate[] continuationsForDebugger = task.GetDelegateContinuationsForDebugger();
          if (continuationsForDebugger != null)
            return continuationsForDebugger;
        }
        ITaskCompletionAction completionAction = continuationObject as ITaskCompletionAction;
        if (completionAction != null)
          return new Delegate[1]{ (Delegate) new Action<Task>(completionAction.Invoke) };
        List<object> objectList = continuationObject as List<object>;
        if (objectList != null)
        {
          List<Delegate> delegateList = new List<Delegate>();
          foreach (object continuationObject1 in objectList)
          {
            Delegate[] continuationObject2 = Task.GetDelegatesFromContinuationObject(continuationObject1);
            if (continuationObject2 != null)
            {
              foreach (Delegate @delegate in continuationObject2)
              {
                if (@delegate != null)
                  delegateList.Add(@delegate);
              }
            }
          }
          return delegateList.ToArray();
        }
      }
      return (Delegate[]) null;
    }

    private static Task GetActiveTaskFromId(int taskId)
    {
      Task task = (Task) null;
      Task.s_currentActiveTasks.TryGetValue(taskId, out task);
      return task;
    }

    private static Task[] GetActiveTasks()
    {
      return new List<Task>((IEnumerable<Task>) Task.s_currentActiveTasks.Values).ToArray();
    }

    internal class ContingentProperties
    {
      internal volatile int m_completionCountdown = 1;
      internal ExecutionContext m_capturedContext;
      internal volatile ManualResetEventSlim m_completionEvent;
      internal volatile TaskExceptionHolder m_exceptionsHolder;
      internal CancellationToken m_cancellationToken;
      internal Shared<CancellationTokenRegistration> m_cancellationRegistration;
      internal volatile int m_internalCancellationRequested;
      internal volatile List<Task> m_exceptionalChildren;

      internal void SetCompleted()
      {
        ManualResetEventSlim manualResetEventSlim = this.m_completionEvent;
        if (manualResetEventSlim == null)
          return;
        manualResetEventSlim.Set();
      }

      internal void DeregisterCancellationCallback()
      {
        if (this.m_cancellationRegistration == null)
          return;
        try
        {
          this.m_cancellationRegistration.Value.Dispose();
        }
        catch (ObjectDisposedException ex)
        {
        }
        this.m_cancellationRegistration = (Shared<CancellationTokenRegistration>) null;
      }
    }

    private sealed class SetOnInvokeMres : ManualResetEventSlim, ITaskCompletionAction
    {
      internal SetOnInvokeMres()
        : base(false, 0)
      {
      }

      public void Invoke(Task completingTask)
      {
        this.Set();
      }
    }

    private sealed class SetOnCountdownMres : ManualResetEventSlim, ITaskCompletionAction
    {
      private int _count;

      internal SetOnCountdownMres(int count)
      {
        this._count = count;
      }

      public void Invoke(Task completingTask)
      {
        if (Interlocked.Decrement(ref this._count) != 0)
          return;
        this.Set();
      }
    }

    private sealed class DelayPromise : Task<VoidTaskResult>
    {
      internal readonly CancellationToken Token;
      internal CancellationTokenRegistration Registration;
      internal Timer Timer;

      internal DelayPromise(CancellationToken token)
      {
        this.Token = token;
        if (AsyncCausalityTracer.LoggingOn)
          AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, this.Id, "Task.Delay", 0UL);
        if (!Task.s_asyncDebuggingEnabled)
          return;
        Task.AddToActiveTasks((Task) this);
      }

      internal void Complete()
      {
        bool flag;
        if (this.Token.IsCancellationRequested)
        {
          flag = this.TrySetCanceled(this.Token);
        }
        else
        {
          if (AsyncCausalityTracer.LoggingOn)
            AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, this.Id, AsyncCausalityStatus.Completed);
          if (Task.s_asyncDebuggingEnabled)
            Task.RemoveFromActiveTasks(this.Id);
          flag = this.TrySetResult(new VoidTaskResult());
        }
        if (!flag)
          return;
        if (this.Timer != null)
          this.Timer.Dispose();
        this.Registration.Dispose();
      }
    }

    private sealed class WhenAllPromise : Task<VoidTaskResult>, ITaskCompletionAction
    {
      private readonly Task[] m_tasks;
      private int m_count;

      internal override bool ShouldNotifyDebuggerOfWaitCompletion
      {
        get
        {
          if (base.ShouldNotifyDebuggerOfWaitCompletion)
            return Task.AnyTaskRequiresNotifyDebuggerOfWaitCompletion(this.m_tasks);
          return false;
        }
      }

      internal WhenAllPromise(Task[] tasks)
      {
        if (AsyncCausalityTracer.LoggingOn)
          AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, this.Id, "Task.WhenAll", 0UL);
        if (Task.s_asyncDebuggingEnabled)
          Task.AddToActiveTasks((Task) this);
        this.m_tasks = tasks;
        this.m_count = tasks.Length;
        foreach (Task task in tasks)
        {
          if (task.IsCompleted)
            this.Invoke(task);
          else
            task.AddCompletionAction((ITaskCompletionAction) this);
        }
      }

      public void Invoke(Task completedTask)
      {
        if (AsyncCausalityTracer.LoggingOn)
          AsyncCausalityTracer.TraceOperationRelation(CausalityTraceLevel.Important, this.Id, CausalityRelation.Join);
        if (Interlocked.Decrement(ref this.m_count) != 0)
          return;
        List<ExceptionDispatchInfo> exceptionDispatchInfoList = (List<ExceptionDispatchInfo>) null;
        Task task1 = (Task) null;
        for (int index = 0; index < this.m_tasks.Length; ++index)
        {
          Task task2 = this.m_tasks[index];
          if (task2.IsFaulted)
          {
            if (exceptionDispatchInfoList == null)
              exceptionDispatchInfoList = new List<ExceptionDispatchInfo>();
            exceptionDispatchInfoList.AddRange((IEnumerable<ExceptionDispatchInfo>) task2.GetExceptionDispatchInfos());
          }
          else if (task2.IsCanceled && task1 == null)
            task1 = task2;
          if (task2.IsWaitNotificationEnabled)
            this.SetNotificationForWaitCompletion(true);
          else
            this.m_tasks[index] = (Task) null;
        }
        if (exceptionDispatchInfoList != null)
          this.TrySetException((object) exceptionDispatchInfoList);
        else if (task1 != null)
        {
          this.TrySetCanceled(task1.CancellationToken, (object) task1.GetCancellationExceptionDispatchInfo());
        }
        else
        {
          if (AsyncCausalityTracer.LoggingOn)
            AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, this.Id, AsyncCausalityStatus.Completed);
          if (Task.s_asyncDebuggingEnabled)
            Task.RemoveFromActiveTasks(this.Id);
          this.TrySetResult(new VoidTaskResult());
        }
      }
    }

    private sealed class WhenAllPromise<T> : Task<T[]>, ITaskCompletionAction
    {
      private readonly Task<T>[] m_tasks;
      private int m_count;

      internal override bool ShouldNotifyDebuggerOfWaitCompletion
      {
        get
        {
          if (base.ShouldNotifyDebuggerOfWaitCompletion)
            return Task.AnyTaskRequiresNotifyDebuggerOfWaitCompletion((Task[]) this.m_tasks);
          return false;
        }
      }

      internal WhenAllPromise(Task<T>[] tasks)
      {
        this.m_tasks = tasks;
        this.m_count = tasks.Length;
        if (AsyncCausalityTracer.LoggingOn)
          AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, this.Id, "Task.WhenAll", 0UL);
        if (Task.s_asyncDebuggingEnabled)
          Task.AddToActiveTasks((Task) this);
        foreach (Task<T> task in tasks)
        {
          if (task.IsCompleted)
            this.Invoke((Task) task);
          else
            task.AddCompletionAction((ITaskCompletionAction) this);
        }
      }

      public void Invoke(Task ignored)
      {
        if (AsyncCausalityTracer.LoggingOn)
          AsyncCausalityTracer.TraceOperationRelation(CausalityTraceLevel.Important, this.Id, CausalityRelation.Join);
        if (Interlocked.Decrement(ref this.m_count) != 0)
          return;
        T[] result = new T[this.m_tasks.Length];
        List<ExceptionDispatchInfo> exceptionDispatchInfoList = (List<ExceptionDispatchInfo>) null;
        Task task1 = (Task) null;
        for (int index = 0; index < this.m_tasks.Length; ++index)
        {
          Task<T> task2 = this.m_tasks[index];
          if (task2.IsFaulted)
          {
            if (exceptionDispatchInfoList == null)
              exceptionDispatchInfoList = new List<ExceptionDispatchInfo>();
            exceptionDispatchInfoList.AddRange((IEnumerable<ExceptionDispatchInfo>) task2.GetExceptionDispatchInfos());
          }
          else if (task2.IsCanceled)
          {
            if (task1 == null)
              task1 = (Task) task2;
          }
          else
            result[index] = task2.GetResultCore(false);
          if (task2.IsWaitNotificationEnabled)
            this.SetNotificationForWaitCompletion(true);
          else
            this.m_tasks[index] = (Task<T>) null;
        }
        if (exceptionDispatchInfoList != null)
          this.TrySetException((object) exceptionDispatchInfoList);
        else if (task1 != null)
        {
          this.TrySetCanceled(task1.CancellationToken, (object) task1.GetCancellationExceptionDispatchInfo());
        }
        else
        {
          if (AsyncCausalityTracer.LoggingOn)
            AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, this.Id, AsyncCausalityStatus.Completed);
          if (Task.s_asyncDebuggingEnabled)
            Task.RemoveFromActiveTasks(this.Id);
          this.TrySetResult(result);
        }
      }
    }
  }
}
