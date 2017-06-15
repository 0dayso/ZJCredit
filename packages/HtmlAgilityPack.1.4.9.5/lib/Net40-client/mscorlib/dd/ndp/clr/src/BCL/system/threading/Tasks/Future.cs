// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.Task`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Permissions;

namespace System.Threading.Tasks
{
  /// <summary>表示一个可以返回值的异步操作。</summary>
  /// <typeparam name="TResult">此 <see cref="T:System.Threading.Tasks.Task`1" /> 生成的结果的类型。</typeparam>
  [DebuggerTypeProxy(typeof (SystemThreadingTasks_FutureDebugView<>))]
  [DebuggerDisplay("Id = {Id}, Status = {Status}, Method = {DebuggerDisplayMethodDescription}, Result = {DebuggerDisplayResultDescription}")]
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public class Task<TResult> : Task
  {
    private static readonly TaskFactory<TResult> s_Factory = new TaskFactory<TResult>();
    internal static readonly Func<Task<Task>, Task<TResult>> TaskWhenAnyCast = new Func<Task<Task>, Task<TResult>>(Task<TResult>.\u003C\u003Ec.\u003C\u003E9.\u003C\u002Ecctor\u003Eb__64_0);
    internal TResult m_result;

    private string DebuggerDisplayResultDescription
    {
      get
      {
        if (!this.IsRanToCompletion)
          return Environment.GetResourceString("TaskT_DebuggerNoResult");
        return string.Concat((object) this.m_result);
      }
    }

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

    /// <summary>获取此 <see cref="T:System.Threading.Tasks.Task`1" /> 的结果值。</summary>
    /// <returns>此 <see cref="T:System.Threading.Tasks.Task`1" /> 的结果值，该值类型与任务参数类型相同。</returns>
    /// <exception cref="T:System.AggregateException">任务已取消。<see cref="P:System.AggregateException.InnerExceptions" /> 集合包含 <see cref="T:System.Threading.Tasks.TaskCanceledException" /> 对象。- 或 -在任务执行过程中引发异常。<see cref="P:System.AggregateException.InnerExceptions" /> 集合包含有关异常或异常的信息。</exception>
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    [__DynamicallyInvokable]
    public TResult Result
    {
      [__DynamicallyInvokable] get
      {
        if (!this.IsWaitNotificationEnabledOrNotRanToCompletion)
          return this.m_result;
        return this.GetResultCore(true);
      }
    }

    internal TResult ResultOnSuccess
    {
      get
      {
        return this.m_result;
      }
    }

    /// <summary>提供对用于创建和配置 <see cref="T:System.Threading.Tasks.Task`1" /> 实例的工厂方法的访问。</summary>
    /// <returns>一个工厂对象，可创建多种 <see cref="T:System.Threading.Tasks.Task`1" /> 对象。</returns>
    [__DynamicallyInvokable]
    public static TaskFactory<TResult> Factory
    {
      [__DynamicallyInvokable] get
      {
        return Task<TResult>.s_Factory;
      }
    }

    internal Task()
    {
    }

    internal Task(object state, TaskCreationOptions options)
      : base(state, options, true)
    {
    }

    internal Task(TResult result)
      : base(false, TaskCreationOptions.None, new CancellationToken())
    {
      this.m_result = result;
    }

    internal Task(bool canceled, TResult result, TaskCreationOptions creationOptions, CancellationToken ct)
      : base(canceled, creationOptions, ct)
    {
      if (canceled)
        return;
      this.m_result = result;
    }

    /// <summary>使用指定的函数初始化新的 <see cref="T:System.Threading.Tasks.Task`1" />。</summary>
    /// <param name="function">表示要在任务中执行的代码的委托。在完成此函数后，该任务的 <see cref="P:System.Threading.Tasks.Task`1.Result" /> 属性将设置为返回此函数的结果值。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="function" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task(Func<TResult> function)
      : this(function, (Task) null, new CancellationToken(), TaskCreationOptions.None, InternalTaskOptions.None, (TaskScheduler) null)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.PossiblyCaptureContext(ref stackMark);
    }

    /// <summary>使用指定的函数初始化新的 <see cref="T:System.Threading.Tasks.Task`1" />。</summary>
    /// <param name="function">表示要在任务中执行的代码的委托。在完成此函数后，该任务的 <see cref="P:System.Threading.Tasks.Task`1.Result" /> 属性将设置为返回此函数的结果值。</param>
    /// <param name="cancellationToken">将指派给此任务的 <see cref="T:System.Threading.CancellationToken" />。</param>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.Threading.CancellationTokenSource" /> 创建<paramref name=" cancellationToken" /> 已释放。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="function" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task(Func<TResult> function, CancellationToken cancellationToken)
      : this(function, (Task) null, cancellationToken, TaskCreationOptions.None, InternalTaskOptions.None, (TaskScheduler) null)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.PossiblyCaptureContext(ref stackMark);
    }

    /// <summary>使用指定的函数和创建选项初始化新的 <see cref="T:System.Threading.Tasks.Task`1" />。</summary>
    /// <param name="function">表示要在任务中执行的代码的委托。在完成此函数后，该任务的 <see cref="P:System.Threading.Tasks.Task`1.Result" /> 属性将设置为返回此函数的结果值。</param>
    /// <param name="creationOptions">用于自定义任务的行为的 <see cref="T:System.Threading.Tasks.TaskCreationOptions" />。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="creationOptions" /> 参数指定的无效值 <see cref="T:System.Threading.Tasks.TaskCreationOptions" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="function" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task(Func<TResult> function, TaskCreationOptions creationOptions)
      : this(function, Task.InternalCurrentIfAttached(creationOptions), new CancellationToken(), creationOptions, InternalTaskOptions.None, (TaskScheduler) null)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.PossiblyCaptureContext(ref stackMark);
    }

    /// <summary>使用指定的函数和创建选项初始化新的 <see cref="T:System.Threading.Tasks.Task`1" />。</summary>
    /// <param name="function">表示要在任务中执行的代码的委托。在完成此函数后，该任务的 <see cref="P:System.Threading.Tasks.Task`1.Result" /> 属性将设置为返回此函数的结果值。</param>
    /// <param name="cancellationToken">将指派给新任务的 <see cref="T:System.Threading.CancellationToken" />。</param>
    /// <param name="creationOptions">用于自定义任务的行为的 <see cref="T:System.Threading.Tasks.TaskCreationOptions" />。</param>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.Threading.CancellationTokenSource" /> 创建<paramref name=" cancellationToken" /> 已释放。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="creationOptions" /> 参数指定的无效值 <see cref="T:System.Threading.Tasks.TaskCreationOptions" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="function" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task(Func<TResult> function, CancellationToken cancellationToken, TaskCreationOptions creationOptions)
      : this(function, Task.InternalCurrentIfAttached(creationOptions), cancellationToken, creationOptions, InternalTaskOptions.None, (TaskScheduler) null)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.PossiblyCaptureContext(ref stackMark);
    }

    /// <summary>使用指定的函数和状态初始化新的 <see cref="T:System.Threading.Tasks.Task`1" />。</summary>
    /// <param name="function">表示要在任务中执行的代码的委托。在完成此函数后，该任务的 <see cref="P:System.Threading.Tasks.Task`1.Result" /> 属性将设置为返回此函数的结果值。</param>
    /// <param name="state">一个表示由该操作使用的数据的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="function" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task(Func<object, TResult> function, object state)
      : this((Delegate) function, state, (Task) null, new CancellationToken(), TaskCreationOptions.None, InternalTaskOptions.None, (TaskScheduler) null)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.PossiblyCaptureContext(ref stackMark);
    }

    /// <summary>使用指定的操作、状态和选项初始化新的 <see cref="T:System.Threading.Tasks.Task`1" />。</summary>
    /// <param name="function">表示要在任务中执行的代码的委托。在完成此函数后，该任务的 <see cref="P:System.Threading.Tasks.Task`1.Result" /> 属性将设置为返回此函数的结果值。</param>
    /// <param name="state">一个表示将由此函数使用的数据的对象。</param>
    /// <param name="cancellationToken">将指派给此新任务的 <see cref="T:System.Threading.CancellationToken" />。</param>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.Threading.CancellationTokenSource" /> 创建<paramref name=" cancellationToken" /> 已释放。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="function" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task(Func<object, TResult> function, object state, CancellationToken cancellationToken)
      : this((Delegate) function, state, (Task) null, cancellationToken, TaskCreationOptions.None, InternalTaskOptions.None, (TaskScheduler) null)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.PossiblyCaptureContext(ref stackMark);
    }

    /// <summary>使用指定的操作、状态和选项初始化新的 <see cref="T:System.Threading.Tasks.Task`1" />。</summary>
    /// <param name="function">表示要在任务中执行的代码的委托。在完成此函数后，该任务的 <see cref="P:System.Threading.Tasks.Task`1.Result" /> 属性将设置为返回此函数的结果值。</param>
    /// <param name="state">一个表示将由此函数使用的数据的对象。</param>
    /// <param name="creationOptions">用于自定义任务的行为的 <see cref="T:System.Threading.Tasks.TaskCreationOptions" />。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="creationOptions" /> 参数指定的无效值 <see cref="T:System.Threading.Tasks.TaskCreationOptions" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="function" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task(Func<object, TResult> function, object state, TaskCreationOptions creationOptions)
      : this((Delegate) function, state, Task.InternalCurrentIfAttached(creationOptions), new CancellationToken(), creationOptions, InternalTaskOptions.None, (TaskScheduler) null)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.PossiblyCaptureContext(ref stackMark);
    }

    /// <summary>使用指定的操作、状态和选项初始化新的 <see cref="T:System.Threading.Tasks.Task`1" />。</summary>
    /// <param name="function">表示要在任务中执行的代码的委托。在完成此函数后，该任务的 <see cref="P:System.Threading.Tasks.Task`1.Result" /> 属性将设置为返回此函数的结果值。</param>
    /// <param name="state">一个表示将由此函数使用的数据的对象。</param>
    /// <param name="cancellationToken">将指派给此新任务的 <see cref="T:System.Threading.CancellationToken" />。</param>
    /// <param name="creationOptions">用于自定义任务的行为的 <see cref="T:System.Threading.Tasks.TaskCreationOptions" />。</param>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.Threading.CancellationTokenSource" /> 创建<paramref name=" cancellationToken" /> 已释放。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="creationOptions" /> 参数指定的无效值 <see cref="T:System.Threading.Tasks.TaskCreationOptions" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="function" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task(Func<object, TResult> function, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions)
      : this((Delegate) function, state, Task.InternalCurrentIfAttached(creationOptions), cancellationToken, creationOptions, InternalTaskOptions.None, (TaskScheduler) null)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.PossiblyCaptureContext(ref stackMark);
    }

    internal Task(Func<TResult> valueSelector, Task parent, CancellationToken cancellationToken, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, TaskScheduler scheduler, ref StackCrawlMark stackMark)
      : this(valueSelector, parent, cancellationToken, creationOptions, internalOptions, scheduler)
    {
      this.PossiblyCaptureContext(ref stackMark);
    }

    internal Task(Func<TResult> valueSelector, Task parent, CancellationToken cancellationToken, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, TaskScheduler scheduler)
      : base((Delegate) valueSelector, (object) null, parent, cancellationToken, creationOptions, internalOptions, scheduler)
    {
      if ((internalOptions & InternalTaskOptions.SelfReplicating) != InternalTaskOptions.None)
        throw new ArgumentOutOfRangeException("creationOptions", Environment.GetResourceString("TaskT_ctor_SelfReplicating"));
    }

    internal Task(Func<object, TResult> valueSelector, object state, Task parent, CancellationToken cancellationToken, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, TaskScheduler scheduler, ref StackCrawlMark stackMark)
      : this((Delegate) valueSelector, state, parent, cancellationToken, creationOptions, internalOptions, scheduler)
    {
      this.PossiblyCaptureContext(ref stackMark);
    }

    internal Task(Delegate valueSelector, object state, Task parent, CancellationToken cancellationToken, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, TaskScheduler scheduler)
      : base(valueSelector, state, parent, cancellationToken, creationOptions, internalOptions, scheduler)
    {
      if ((internalOptions & InternalTaskOptions.SelfReplicating) != InternalTaskOptions.None)
        throw new ArgumentOutOfRangeException("creationOptions", Environment.GetResourceString("TaskT_ctor_SelfReplicating"));
    }

    internal static Task<TResult> StartNew(Task parent, Func<TResult> function, CancellationToken cancellationToken, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, TaskScheduler scheduler, ref StackCrawlMark stackMark)
    {
      if (function == null)
        throw new ArgumentNullException("function");
      if (scheduler == null)
        throw new ArgumentNullException("scheduler");
      if ((internalOptions & InternalTaskOptions.SelfReplicating) != InternalTaskOptions.None)
        throw new ArgumentOutOfRangeException("creationOptions", Environment.GetResourceString("TaskT_ctor_SelfReplicating"));
      Task<TResult> task = new Task<TResult>(function, parent, cancellationToken, creationOptions, internalOptions | InternalTaskOptions.QueuedByRuntime, scheduler, ref stackMark);
      int num = 0;
      task.ScheduleAndStart(num != 0);
      return task;
    }

    internal static Task<TResult> StartNew(Task parent, Func<object, TResult> function, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, TaskScheduler scheduler, ref StackCrawlMark stackMark)
    {
      if (function == null)
        throw new ArgumentNullException("function");
      if (scheduler == null)
        throw new ArgumentNullException("scheduler");
      if ((internalOptions & InternalTaskOptions.SelfReplicating) != InternalTaskOptions.None)
        throw new ArgumentOutOfRangeException("creationOptions", Environment.GetResourceString("TaskT_ctor_SelfReplicating"));
      Task<TResult> task = new Task<TResult>(function, state, parent, cancellationToken, creationOptions, internalOptions | InternalTaskOptions.QueuedByRuntime, scheduler, ref stackMark);
      int num = 0;
      task.ScheduleAndStart(num != 0);
      return task;
    }

    internal bool TrySetResult(TResult result)
    {
      if (this.IsCompleted || !this.AtomicStateUpdate(67108864, 90177536))
        return false;
      this.m_result = result;
      Interlocked.Exchange(ref this.m_stateFlags, this.m_stateFlags | 16777216);
      Task.ContingentProperties contingentProperties = this.m_contingentProperties;
      if (contingentProperties != null)
        contingentProperties.SetCompleted();
      this.FinishStageThree();
      return true;
    }

    internal void DangerousSetResult(TResult result)
    {
      if (this.m_parent != null)
      {
        this.TrySetResult(result);
      }
      else
      {
        this.m_result = result;
        this.m_stateFlags = this.m_stateFlags | 16777216;
      }
    }

    internal TResult GetResultCore(bool waitCompletionNotification)
    {
      if (!this.IsCompleted)
        this.InternalWait(-1, new CancellationToken());
      if (waitCompletionNotification)
        this.NotifyDebuggerOfWaitCompletionIfNecessary();
      if (!this.IsRanToCompletion)
        this.ThrowIfExceptional(true);
      return this.m_result;
    }

    internal bool TrySetException(object exceptionObject)
    {
      bool flag = false;
      this.EnsureContingentPropertiesInitialized(true);
      if (this.AtomicStateUpdate(67108864, 90177536))
      {
        this.AddException(exceptionObject);
        this.Finish(false);
        flag = true;
      }
      return flag;
    }

    internal bool TrySetCanceled(CancellationToken tokenToRecord)
    {
      return this.TrySetCanceled(tokenToRecord, (object) null);
    }

    internal bool TrySetCanceled(CancellationToken tokenToRecord, object cancellationException)
    {
      bool flag = false;
      if (this.AtomicStateUpdate(67108864, 90177536))
      {
        this.RecordInternalCancellationRequest(tokenToRecord, cancellationException);
        this.CancellationCleanupLogic();
        flag = true;
      }
      return flag;
    }

    internal override void InnerInvoke()
    {
      Func<TResult> func1 = this.m_action as Func<TResult>;
      if (func1 != null)
      {
        this.m_result = func1();
      }
      else
      {
        Func<object, TResult> func2 = this.m_action as Func<object, TResult>;
        if (func2 == null)
          return;
        this.m_result = func2(this.m_stateObject);
      }
    }

    /// <summary>获取用于等待此 <see cref="T:System.Threading.Tasks.Task`1" /> 的 awaiter。</summary>
    /// <returns>一个 awaiter 实例。</returns>
    [__DynamicallyInvokable]
    public TaskAwaiter<TResult> GetAwaiter()
    {
      return new TaskAwaiter<TResult>(this);
    }

    /// <summary>配置用于等待此 <see cref="T:System.Threading.Tasks.Task`1" />的 awaiter。</summary>
    /// <returns>用于的等待此任务的对象。</returns>
    /// <param name="continueOnCapturedContext">尝试将延续任务封送回原始上下文，则为 true；否则为 false。</param>
    [__DynamicallyInvokable]
    public ConfiguredTaskAwaitable<TResult> ConfigureAwait(bool continueOnCapturedContext)
    {
      return new ConfiguredTaskAwaitable<TResult>(this, continueOnCapturedContext);
    }

    /// <summary>创建一个在目标任务完成时异步执行的延续任务。</summary>
    /// <returns>一个新的延续任务。</returns>
    /// <param name="continuationAction">在 <see cref="T:System.Threading.Tasks.Task`1" /> 完成时要运行的操作。在运行时，委托将作为一个参数传递给完成的任务。</param>
    /// <exception cref="T:System.ObjectDisposedException">已释放了 <see cref="T:System.Threading.Tasks.Task`1" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="continuationAction" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWith(Action<Task<TResult>> continuationAction)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith(continuationAction, TaskScheduler.Current, new CancellationToken(), TaskContinuationOptions.None, ref stackMark);
    }

    /// <summary>创建一个在目标 <see cref="T:System.Threading.Tasks.Task`1" /> 完成时异步执行的可取消延续任务。</summary>
    /// <returns>一个新的延续任务。</returns>
    /// <param name="continuationAction">在 <see cref="T:System.Threading.Tasks.Task`1" /> 完成时要运行的操作。在运行时，委托作为一个参数传递给完成的任务。</param>
    /// <param name="cancellationToken">传递给新的延续任务的取消标记。</param>
    /// <exception cref="T:System.ObjectDisposedException">已释放了 <see cref="T:System.Threading.Tasks.Task`1" />。- 或 -<see cref="T:System.Threading.CancellationTokenSource" /> 创建 <paramref name="cancellationToken" /> 已被释放。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="continuationAction" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWith(Action<Task<TResult>> continuationAction, CancellationToken cancellationToken)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith(continuationAction, TaskScheduler.Current, cancellationToken, TaskContinuationOptions.None, ref stackMark);
    }

    /// <summary>创建一个在目标 <see cref="T:System.Threading.Tasks.Task`1" /> 完成时异步执行的延续任务。</summary>
    /// <returns>一个新的延续 <see cref="T:System.Threading.Tasks.Task" />。</returns>
    /// <param name="continuationAction">在 <see cref="T:System.Threading.Tasks.Task`1" /> 完成时要运行的操作。在运行时，委托将作为一个参数传递给完成的任务。</param>
    /// <param name="scheduler">要与延续任务关联并用于其执行过程的 <see cref="T:System.Threading.Tasks.TaskScheduler" />。</param>
    /// <exception cref="T:System.ObjectDisposedException">已释放了 <see cref="T:System.Threading.Tasks.Task`1" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="continuationAction" /> 参数为 null。- 或 -<paramref name="scheduler" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWith(Action<Task<TResult>> continuationAction, TaskScheduler scheduler)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith(continuationAction, scheduler, new CancellationToken(), TaskContinuationOptions.None, ref stackMark);
    }

    /// <summary>创建根据 <paramref name="continuationOptions" /> 中指定的条件加以执行的延续任务。</summary>
    /// <returns>一个新的延续 <see cref="T:System.Threading.Tasks.Task" />。</returns>
    /// <param name="continuationAction">在 <paramref name="continuationOptions" /> 中指定的条件的操作。在运行时，委托将作为一个参数传递给完成的任务。</param>
    /// <param name="continuationOptions">用于设置计划延续任务的时间以及延续任务的工作方式的选项。这包括条件（如 <see cref="F:System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled" />）和执行选项（如 <see cref="F:System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously" />）。</param>
    /// <exception cref="T:System.ObjectDisposedException">已释放了 <see cref="T:System.Threading.Tasks.Task`1" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="continuationAction" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="continuationOptions" /> 参数指定的无效值 <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWith(Action<Task<TResult>> continuationAction, TaskContinuationOptions continuationOptions)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith(continuationAction, TaskScheduler.Current, new CancellationToken(), continuationOptions, ref stackMark);
    }

    /// <summary>创建根据 <paramref name="continuationOptions" /> 中指定的条件加以执行的延续任务。</summary>
    /// <returns>一个新的延续 <see cref="T:System.Threading.Tasks.Task" />。</returns>
    /// <param name="continuationAction">根据在 <paramref name="continuationOptions" /> 中指定的条件运行的操作。在运行时，委托将作为一个参数传递给完成的任务。</param>
    /// <param name="cancellationToken">将指派给新的延续任务的 <see cref="T:System.Threading.CancellationToken" />。</param>
    /// <param name="continuationOptions">用于设置计划延续任务的时间以及延续任务的工作方式的选项。这包括条件（如 <see cref="F:System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled" />）和执行选项（如 <see cref="F:System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously" />）。</param>
    /// <param name="scheduler">要与延续任务关联并用于其执行过程的 <see cref="T:System.Threading.Tasks.TaskScheduler" />。</param>
    /// <exception cref="T:System.ObjectDisposedException">已释放了 <see cref="T:System.Threading.Tasks.Task`1" />。- 或 -<see cref="T:System.Threading.CancellationTokenSource" /> 创建 <paramref name="cancellationToken" /> 已释放。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="continuationAction" /> 参数为 null。- 或 -<paramref name="scheduler" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="continuationOptions" /> 参数指定的无效值 <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWith(Action<Task<TResult>> continuationAction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith(continuationAction, scheduler, cancellationToken, continuationOptions, ref stackMark);
    }

    internal Task ContinueWith(Action<Task<TResult>> continuationAction, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, ref StackCrawlMark stackMark)
    {
      if (continuationAction == null)
        throw new ArgumentNullException("continuationAction");
      if (scheduler == null)
        throw new ArgumentNullException("scheduler");
      TaskCreationOptions creationOptions;
      InternalTaskOptions internalOptions;
      Task.CreationOptionsFromContinuationOptions(continuationOptions, out creationOptions, out internalOptions);
      Task continuationTask = (Task) new ContinuationTaskFromResultTask<TResult>(this, (Delegate) continuationAction, (object) null, creationOptions, internalOptions, ref stackMark);
      this.ContinueWithCore(continuationTask, scheduler, cancellationToken, continuationOptions);
      return continuationTask;
    }

    /// <summary>创建一个传递了状态信息并在目标 <see cref="T:System.Threading.Tasks.Task`1" /> 完成时执行的延续任务。</summary>
    /// <returns>一个新的延续 <see cref="T:System.Threading.Tasks.Task" />。</returns>
    /// <param name="continuationAction">在 <see cref="T:System.Threading.Tasks.Task`1" /> 完成时要运行的操作。运行时，委托作为一个参数传递给完成的任务和调用方提供的状态对象。</param>
    /// <param name="state">一个表示由该延续操作使用的数据的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="continuationAction" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWith(Action<Task<TResult>, object> continuationAction, object state)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith(continuationAction, state, TaskScheduler.Current, new CancellationToken(), TaskContinuationOptions.None, ref stackMark);
    }

    /// <summary>创建一个在目标 <see cref="T:System.Threading.Tasks.Task`1" /> 完成时执行的延续任务。</summary>
    /// <returns>一个新的延续 <see cref="T:System.Threading.Tasks.Task" />。</returns>
    /// <param name="continuationAction">在 <see cref="T:System.Threading.Tasks.Task`1" /> 完成时要运行的操作。运行时，将传递委托，如完成的任务一样，调用方提供的状态对象（如参数）。</param>
    /// <param name="state">一个表示由该延续操作使用的数据的对象。</param>
    /// <param name="cancellationToken">将指派给新的延续任务的 <see cref="T:System.Threading.CancellationToken" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="continuationAction" /> 参数为 null。</exception>
    /// <exception cref="T:System.ObjectDisposedException">所提供 <see cref="T:System.Threading.CancellationToken" /> 已释放。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWith(Action<Task<TResult>, object> continuationAction, object state, CancellationToken cancellationToken)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith(continuationAction, state, TaskScheduler.Current, cancellationToken, TaskContinuationOptions.None, ref stackMark);
    }

    /// <summary>创建一个在目标 <see cref="T:System.Threading.Tasks.Task`1" /> 完成时执行的延续任务。</summary>
    /// <returns>一个新的延续 <see cref="T:System.Threading.Tasks.Task" />。</returns>
    /// <param name="continuationAction">在 <see cref="T:System.Threading.Tasks.Task`1" /> 完成时要运行的操作。运行时，将传递委托，如完成的任务一样，调用方提供的状态对象（如参数）。</param>
    /// <param name="state">一个表示由该延续操作使用的数据的对象。</param>
    /// <param name="scheduler">要与延续任务关联并用于其执行过程的 <see cref="T:System.Threading.Tasks.TaskScheduler" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="continuationAction" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="scheduler" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWith(Action<Task<TResult>, object> continuationAction, object state, TaskScheduler scheduler)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith(continuationAction, state, scheduler, new CancellationToken(), TaskContinuationOptions.None, ref stackMark);
    }

    /// <summary>创建一个在目标 <see cref="T:System.Threading.Tasks.Task`1" /> 完成时执行的延续任务。</summary>
    /// <returns>一个新的延续 <see cref="T:System.Threading.Tasks.Task" />。</returns>
    /// <param name="continuationAction">在 <see cref="T:System.Threading.Tasks.Task`1" /> 完成时要运行的操作。运行时，将传递委托，如完成的任务一样，调用方提供的状态对象（如参数）。</param>
    /// <param name="state">一个表示由该延续操作使用的数据的对象。</param>
    /// <param name="continuationOptions">用于设置计划延续任务的时间以及延续任务的工作方式的选项。这包括条件（如 <see cref="F:System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled" />）和执行选项（如 <see cref="F:System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously" />）。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="continuationAction" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="continuationOptions" /> 参数指定的无效值 <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWith(Action<Task<TResult>, object> continuationAction, object state, TaskContinuationOptions continuationOptions)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith(continuationAction, state, TaskScheduler.Current, new CancellationToken(), continuationOptions, ref stackMark);
    }

    /// <summary>创建一个在目标 <see cref="T:System.Threading.Tasks.Task`1" /> 完成时执行的延续任务。</summary>
    /// <returns>一个新的延续 <see cref="T:System.Threading.Tasks.Task" />。</returns>
    /// <param name="continuationAction">在 <see cref="T:System.Threading.Tasks.Task`1" /> 完成时要运行的操作。运行时，将传递委托，如完成的任务一样，调用方提供的状态对象（如参数）。</param>
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
    public Task ContinueWith(Action<Task<TResult>, object> continuationAction, object state, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith(continuationAction, state, scheduler, cancellationToken, continuationOptions, ref stackMark);
    }

    internal Task ContinueWith(Action<Task<TResult>, object> continuationAction, object state, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, ref StackCrawlMark stackMark)
    {
      if (continuationAction == null)
        throw new ArgumentNullException("continuationAction");
      if (scheduler == null)
        throw new ArgumentNullException("scheduler");
      TaskCreationOptions creationOptions;
      InternalTaskOptions internalOptions;
      Task.CreationOptionsFromContinuationOptions(continuationOptions, out creationOptions, out internalOptions);
      Task continuationTask = (Task) new ContinuationTaskFromResultTask<TResult>(this, (Delegate) continuationAction, state, creationOptions, internalOptions, ref stackMark);
      this.ContinueWithCore(continuationTask, scheduler, cancellationToken, continuationOptions);
      return continuationTask;
    }

    /// <summary>创建一个在目标 <see cref="T:System.Threading.Tasks.Task`1" /> 完成时异步执行的延续任务。</summary>
    /// <returns>一个新的延续 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    /// <param name="continuationFunction">在 <see cref="T:System.Threading.Tasks.Task`1" /> 完成时要运行的函数。在运行时，委托将作为一个参数传递给完成的任务。</param>
    /// <typeparam name="TNewResult"> 延续任务生成的结果的类型。</typeparam>
    /// <exception cref="T:System.ObjectDisposedException">已释放了 <see cref="T:System.Threading.Tasks.Task`1" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="continuationFunction" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, TNewResult> continuationFunction)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith<TNewResult>(continuationFunction, TaskScheduler.Current, new CancellationToken(), TaskContinuationOptions.None, ref stackMark);
    }

    /// <summary>创建一个在目标 <see cref="T:System.Threading.Tasks.Task`1" /> 完成时异步执行的延续任务。</summary>
    /// <returns>一个新的延续 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    /// <param name="continuationFunction">在 <see cref="T:System.Threading.Tasks.Task`1" /> 完成时要运行的函数。在运行时，委托将作为一个参数传递给完成的任务。</param>
    /// <param name="cancellationToken">将指派给新任务的 <see cref="T:System.Threading.CancellationToken" />。</param>
    /// <typeparam name="TNewResult"> 延续任务生成的结果的类型。</typeparam>
    /// <exception cref="T:System.ObjectDisposedException">已释放了 <see cref="T:System.Threading.Tasks.Task`1" />。- 或 -<see cref="T:System.Threading.CancellationTokenSource" /> 创建<paramref name=" cancellationToken" /> 已释放。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="continuationFunction" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, TNewResult> continuationFunction, CancellationToken cancellationToken)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith<TNewResult>(continuationFunction, TaskScheduler.Current, cancellationToken, TaskContinuationOptions.None, ref stackMark);
    }

    /// <summary>创建一个在目标 <see cref="T:System.Threading.Tasks.Task`1" /> 完成时异步执行的延续任务。</summary>
    /// <returns>一个新的延续 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    /// <param name="continuationFunction">在 <see cref="T:System.Threading.Tasks.Task`1" /> 完成时要运行的函数。在运行时，委托将作为一个参数传递给完成的任务。</param>
    /// <param name="scheduler">要与延续任务关联并用于其执行过程的 <see cref="T:System.Threading.Tasks.TaskScheduler" />。</param>
    /// <typeparam name="TNewResult"> 延续任务生成的结果的类型。</typeparam>
    /// <exception cref="T:System.ObjectDisposedException">已释放了 <see cref="T:System.Threading.Tasks.Task`1" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="continuationFunction" /> 参数为 null。- 或 -<paramref name="scheduler" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, TNewResult> continuationFunction, TaskScheduler scheduler)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith<TNewResult>(continuationFunction, scheduler, new CancellationToken(), TaskContinuationOptions.None, ref stackMark);
    }

    /// <summary>创建根据 <paramref name="continuationOptions" /> 中指定的条件加以执行的延续任务。</summary>
    /// <returns>一个新的延续 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    /// <param name="continuationFunction">根据 <paramref name="continuationOptions" /> 中指定的条件运行函数。在运行时，委托将作为一个参数传递给完成的任务。</param>
    /// <param name="continuationOptions">用于设置计划延续任务的时间以及延续任务的工作方式的选项。这包括条件（如 <see cref="F:System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled" />）和执行选项（如 <see cref="F:System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously" />）。</param>
    /// <typeparam name="TNewResult"> 延续任务生成的结果的类型。</typeparam>
    /// <exception cref="T:System.ObjectDisposedException">已释放了 <see cref="T:System.Threading.Tasks.Task`1" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="continuationFunction" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="continuationOptions" /> 参数指定的无效值 <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, TNewResult> continuationFunction, TaskContinuationOptions continuationOptions)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith<TNewResult>(continuationFunction, TaskScheduler.Current, new CancellationToken(), continuationOptions, ref stackMark);
    }

    /// <summary>创建根据 <paramref name="continuationOptions" /> 中指定的条件加以执行的延续任务。</summary>
    /// <returns>一个新的延续 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    /// <param name="continuationFunction">根据 <paramref name="continuationOptions" /> 中指定的条件运行函数。在运行时，委托将作为一个参数传递给此完成的任务。</param>
    /// <param name="cancellationToken">将指派给新任务的 <see cref="T:System.Threading.CancellationToken" />。</param>
    /// <param name="continuationOptions">用于设置计划延续任务的时间以及延续任务的工作方式的选项。这包括条件（如 <see cref="F:System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled" />）和执行选项（如 <see cref="F:System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously" />）。</param>
    /// <param name="scheduler">要与延续任务关联并用于其执行过程的 <see cref="T:System.Threading.Tasks.TaskScheduler" />。</param>
    /// <typeparam name="TNewResult"> 延续任务生成的结果的类型。</typeparam>
    /// <exception cref="T:System.ObjectDisposedException">已释放了 <see cref="T:System.Threading.Tasks.Task`1" />。- 或 -<see cref="T:System.Threading.CancellationTokenSource" /> 创建<paramref name=" cancellationToken" /> 已释放。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="continuationFunction" /> 参数为 null。- 或 -<paramref name="scheduler" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="continuationOptions" /> 参数指定的无效值 <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, TNewResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith<TNewResult>(continuationFunction, scheduler, cancellationToken, continuationOptions, ref stackMark);
    }

    internal Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, TNewResult> continuationFunction, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, ref StackCrawlMark stackMark)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException("continuationFunction");
      if (scheduler == null)
        throw new ArgumentNullException("scheduler");
      TaskCreationOptions creationOptions;
      InternalTaskOptions internalOptions;
      Task.CreationOptionsFromContinuationOptions(continuationOptions, out creationOptions, out internalOptions);
      Task<TNewResult> task = (Task<TNewResult>) new ContinuationResultTaskFromResultTask<TResult, TNewResult>(this, (Delegate) continuationFunction, (object) null, creationOptions, internalOptions, ref stackMark);
      this.ContinueWithCore((Task) task, scheduler, cancellationToken, continuationOptions);
      return task;
    }

    /// <summary>创建一个在目标 <see cref="T:System.Threading.Tasks.Task`1" /> 完成时执行的延续任务。</summary>
    /// <returns>一个新的延续 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    /// <param name="continuationFunction">在 <see cref="T:System.Threading.Tasks.Task`1" /> 完成时要运行的函数。运行时，将传递委托，如完成的任务一样，调用方提供的状态对象（如参数）。</param>
    /// <param name="state">一个表示由该延续功能使用的数据的对象。</param>
    /// <typeparam name="TNewResult">延续任务生成的结果的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="continuationFunction" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, object, TNewResult> continuationFunction, object state)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith<TNewResult>(continuationFunction, state, TaskScheduler.Current, new CancellationToken(), TaskContinuationOptions.None, ref stackMark);
    }

    /// <summary>创建一个在目标 <see cref="T:System.Threading.Tasks.Task`1" /> 完成时执行的延续任务。</summary>
    /// <returns>一个新的延续 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    /// <param name="continuationFunction">在 <see cref="T:System.Threading.Tasks.Task`1" /> 完成时要运行的函数。运行时，将传递委托，如完成的任务一样，调用方提供的状态对象（如参数）。</param>
    /// <param name="state">一个表示由该延续功能使用的数据的对象。</param>
    /// <param name="cancellationToken">将指派给新任务的 <see cref="T:System.Threading.CancellationToken" />。</param>
    /// <typeparam name="TNewResult">延续任务生成的结果的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="continuationFunction" /> 参数为 null。</exception>
    /// <exception cref="T:System.ObjectDisposedException">所提供 <see cref="T:System.Threading.CancellationToken" /> 已释放。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, object, TNewResult> continuationFunction, object state, CancellationToken cancellationToken)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith<TNewResult>(continuationFunction, state, TaskScheduler.Current, cancellationToken, TaskContinuationOptions.None, ref stackMark);
    }

    /// <summary>创建一个在目标 <see cref="T:System.Threading.Tasks.Task`1" /> 完成时执行的延续任务。</summary>
    /// <returns>一个新的延续 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    /// <param name="continuationFunction">在 <see cref="T:System.Threading.Tasks.Task`1" /> 完成时要运行的函数。运行时，将传递委托，如完成的任务一样，调用方提供的状态对象（如参数）。</param>
    /// <param name="state">一个表示由该延续功能使用的数据的对象。</param>
    /// <param name="scheduler">要与延续任务关联并用于其执行过程的 <see cref="T:System.Threading.Tasks.TaskScheduler" />。</param>
    /// <typeparam name="TNewResult">延续任务生成的结果的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="continuationFunction" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="scheduler" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, object, TNewResult> continuationFunction, object state, TaskScheduler scheduler)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith<TNewResult>(continuationFunction, state, scheduler, new CancellationToken(), TaskContinuationOptions.None, ref stackMark);
    }

    /// <summary>创建一个在目标 <see cref="T:System.Threading.Tasks.Task`1" /> 完成时执行的延续任务。</summary>
    /// <returns>一个新的延续 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    /// <param name="continuationFunction">在 <see cref="T:System.Threading.Tasks.Task`1" /> 完成时要运行的函数。运行时，将传递委托，如完成的任务一样，调用方提供的状态对象（如参数）。</param>
    /// <param name="state">一个表示由该延续功能使用的数据的对象。</param>
    /// <param name="continuationOptions">用于设置计划延续任务的时间以及延续任务的工作方式的选项。这包括条件（如 <see cref="F:System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled" />）和执行选项（如 <see cref="F:System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously" />）。</param>
    /// <typeparam name="TNewResult">延续任务生成的结果的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="continuationFunction" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="continuationOptions" /> 参数指定的无效值 <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, object, TNewResult> continuationFunction, object state, TaskContinuationOptions continuationOptions)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith<TNewResult>(continuationFunction, state, TaskScheduler.Current, new CancellationToken(), continuationOptions, ref stackMark);
    }

    /// <summary>创建一个在目标 <see cref="T:System.Threading.Tasks.Task`1" /> 完成时执行的延续任务。</summary>
    /// <returns>一个新的延续 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    /// <param name="continuationFunction">在 <see cref="T:System.Threading.Tasks.Task`1" /> 完成时要运行的函数。运行时，将传递委托，如完成的任务一样，调用方提供的状态对象（如参数）。</param>
    /// <param name="state">一个表示由该延续功能使用的数据的对象。</param>
    /// <param name="cancellationToken">将指派给新任务的 <see cref="T:System.Threading.CancellationToken" />。</param>
    /// <param name="continuationOptions">用于设置计划延续任务的时间以及延续任务的工作方式的选项。这包括条件（如 <see cref="F:System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled" />）和执行选项（如 <see cref="F:System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously" />）。</param>
    /// <param name="scheduler">要与延续任务关联并用于其执行过程的 <see cref="T:System.Threading.Tasks.TaskScheduler" />。</param>
    /// <typeparam name="TNewResult">延续任务生成的结果的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="continuationFunction" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="continuationOptions" />  参数指定的无效值 <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="scheduler" /> 参数为 null。</exception>
    /// <exception cref="T:System.ObjectDisposedException">所提供 <see cref="T:System.Threading.CancellationToken" /> 已释放。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, object, TNewResult> continuationFunction, object state, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.ContinueWith<TNewResult>(continuationFunction, state, scheduler, cancellationToken, continuationOptions, ref stackMark);
    }

    internal Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, object, TNewResult> continuationFunction, object state, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, ref StackCrawlMark stackMark)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException("continuationFunction");
      if (scheduler == null)
        throw new ArgumentNullException("scheduler");
      TaskCreationOptions creationOptions;
      InternalTaskOptions internalOptions;
      Task.CreationOptionsFromContinuationOptions(continuationOptions, out creationOptions, out internalOptions);
      Task<TNewResult> task = (Task<TNewResult>) new ContinuationResultTaskFromResultTask<TResult, TNewResult>(this, (Delegate) continuationFunction, state, creationOptions, internalOptions, ref stackMark);
      this.ContinueWithCore((Task) task, scheduler, cancellationToken, continuationOptions);
      return task;
    }
  }
}
