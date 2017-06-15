// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.TaskFactory
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Permissions;

namespace System.Threading.Tasks
{
  /// <summary>提供对创建和计划 <see cref="T:System.Threading.Tasks.Task" /> 对象的支持。</summary>
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public class TaskFactory
  {
    private CancellationToken m_defaultCancellationToken;
    private TaskScheduler m_defaultScheduler;
    private TaskCreationOptions m_defaultCreationOptions;
    private TaskContinuationOptions m_defaultContinuationOptions;

    private TaskScheduler DefaultScheduler
    {
      get
      {
        if (this.m_defaultScheduler == null)
          return TaskScheduler.Current;
        return this.m_defaultScheduler;
      }
    }

    /// <summary>获取此任务工厂的默认取消标记。</summary>
    /// <returns>此任务工厂的默认任务取消标记。</returns>
    [__DynamicallyInvokable]
    public CancellationToken CancellationToken
    {
      [__DynamicallyInvokable] get
      {
        return this.m_defaultCancellationToken;
      }
    }

    /// <summary>获取此任务工厂的默认任务计划程序。</summary>
    /// <returns>此任务工厂的默认任务计划程序。</returns>
    [__DynamicallyInvokable]
    public TaskScheduler Scheduler
    {
      [__DynamicallyInvokable] get
      {
        return this.m_defaultScheduler;
      }
    }

    /// <summary>获取此任务工厂的默认任务创建选项。</summary>
    /// <returns>此任务工厂的默认任务创建选项。</returns>
    [__DynamicallyInvokable]
    public TaskCreationOptions CreationOptions
    {
      [__DynamicallyInvokable] get
      {
        return this.m_defaultCreationOptions;
      }
    }

    /// <summary>获取此任务工厂的默认任务继续选项。</summary>
    /// <returns>此任务工厂的默认任务继续选项。</returns>
    [__DynamicallyInvokable]
    public TaskContinuationOptions ContinuationOptions
    {
      [__DynamicallyInvokable] get
      {
        return this.m_defaultContinuationOptions;
      }
    }

    /// <summary>使用默认配置初始化 <see cref="T:System.Threading.Tasks.TaskFactory" /> 实例。</summary>
    [__DynamicallyInvokable]
    public TaskFactory()
      : this(new CancellationToken(), TaskCreationOptions.None, TaskContinuationOptions.None, (TaskScheduler) null)
    {
    }

    /// <summary>使用指定配置初始化 <see cref="T:System.Threading.Tasks.TaskFactory" /> 实例。</summary>
    /// <param name="cancellationToken">将指派给由此 <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> 创建的任务的 <see cref="T:System.Threading.Tasks.TaskFactory" />，除非在调用工厂方法时显式指定另一个 CancellationToken。</param>
    [__DynamicallyInvokable]
    public TaskFactory(CancellationToken cancellationToken)
      : this(cancellationToken, TaskCreationOptions.None, TaskContinuationOptions.None, (TaskScheduler) null)
    {
    }

    /// <summary>使用指定配置初始化 <see cref="T:System.Threading.Tasks.TaskFactory" /> 实例。</summary>
    /// <param name="scheduler">要用于计划使用此 TaskFactory 创建的任何任务的 <see cref="T:System.Threading.Tasks.TaskScheduler" />。一个 null 值，该值指示应使用当前的 TaskScheduler。</param>
    [__DynamicallyInvokable]
    public TaskFactory(TaskScheduler scheduler)
      : this(new CancellationToken(), TaskCreationOptions.None, TaskContinuationOptions.None, scheduler)
    {
    }

    /// <summary>使用指定配置初始化 <see cref="T:System.Threading.Tasks.TaskFactory" /> 实例。</summary>
    /// <param name="creationOptions">在使用此 TaskFactory 创建任务时要使用的默认 <see cref="T:System.Threading.Tasks.TaskCreationOptions" />。</param>
    /// <param name="continuationOptions">在使用此 TaskFactory 创建延续任务时要使用的默认 <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="creationOptions" /> 参数指定的 <see cref="T:System.Threading.Tasks.TaskCreationOptions" /> 值无效。有关详细信息，请参阅的备注部分 <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" />。- 或 -<paramref name="continuationOptions" /> 参数指定的值无效。 </exception>
    [__DynamicallyInvokable]
    public TaskFactory(TaskCreationOptions creationOptions, TaskContinuationOptions continuationOptions)
      : this(new CancellationToken(), creationOptions, continuationOptions, (TaskScheduler) null)
    {
    }

    /// <summary>使用指定配置初始化 <see cref="T:System.Threading.Tasks.TaskFactory" /> 实例。</summary>
    /// <param name="cancellationToken">将指派给由此 <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> 创建的任务的 <see cref="T:System.Threading.Tasks.TaskFactory" />，除非在调用工厂方法时显式指定另一个 CancellationToken。</param>
    /// <param name="creationOptions">在使用此 TaskFactory 创建任务时要使用的默认 <see cref="T:System.Threading.Tasks.TaskCreationOptions" />。</param>
    /// <param name="continuationOptions">在使用此 TaskFactory 创建延续任务时要使用的默认 <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />。</param>
    /// <param name="scheduler">要用于计划使用此 TaskFactory 创建的任何任务的默认 <see cref="T:System.Threading.Tasks.TaskScheduler" />。一个 null 值，该值指示应使用 TaskScheduler.Current。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="creationOptions" /> 参数指定的 <see cref="T:System.Threading.Tasks.TaskCreationOptions" /> 值无效。有关详细信息，请参阅的备注部分 <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" />。- 或 -<paramref name="continuationOptions" /> 参数指定的值无效。 </exception>
    [__DynamicallyInvokable]
    public TaskFactory(CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
      TaskFactory.CheckCreationOptions(creationOptions);
      this.m_defaultCancellationToken = cancellationToken;
      this.m_defaultScheduler = scheduler;
      this.m_defaultCreationOptions = creationOptions;
      this.m_defaultContinuationOptions = continuationOptions;
    }

    private TaskScheduler GetDefaultScheduler(Task currTask)
    {
      if (this.m_defaultScheduler != null)
        return this.m_defaultScheduler;
      if (currTask != null && (currTask.CreationOptions & TaskCreationOptions.HideScheduler) == TaskCreationOptions.None)
        return currTask.ExecutingTaskScheduler;
      return TaskScheduler.Default;
    }

    internal static void CheckCreationOptions(TaskCreationOptions creationOptions)
    {
      if ((creationOptions & ~(TaskCreationOptions.PreferFairness | TaskCreationOptions.LongRunning | TaskCreationOptions.AttachedToParent | TaskCreationOptions.DenyChildAttach | TaskCreationOptions.HideScheduler | TaskCreationOptions.RunContinuationsAsynchronously)) != TaskCreationOptions.None)
        throw new ArgumentOutOfRangeException("creationOptions");
    }

    /// <summary>创建并启动 任务。</summary>
    /// <returns>已启动的任务。</returns>
    /// <param name="action">要异步执行的操作委托。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="action" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task StartNew(Action action)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      Task internalCurrent = Task.InternalCurrent;
      return Task.InternalStartNew(internalCurrent, (Delegate) action, (object) null, this.m_defaultCancellationToken, this.GetDefaultScheduler(internalCurrent), this.m_defaultCreationOptions, InternalTaskOptions.None, ref stackMark);
    }

    /// <summary>创建并启动 <see cref="T:System.Threading.Tasks.Task" />。</summary>
    /// <returns>已启动的 <see cref="T:System.Threading.Tasks.Task" />。</returns>
    /// <param name="action">要异步执行的操作委托。</param>
    /// <param name="cancellationToken">将指派给新任务的 <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" />。</param>
    /// <exception cref="T:System.ObjectDisposedException">提供的 <see cref="T:System.Threading.CancellationToken" /> 已被释放。</exception>
    /// <exception cref="T:System.ArgumentNullException">当 <paramref name="action" /> 参数为 null 时引发的异常。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task StartNew(Action action, CancellationToken cancellationToken)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      Task internalCurrent = Task.InternalCurrent;
      return Task.InternalStartNew(internalCurrent, (Delegate) action, (object) null, cancellationToken, this.GetDefaultScheduler(internalCurrent), this.m_defaultCreationOptions, InternalTaskOptions.None, ref stackMark);
    }

    /// <summary>创建并启动 <see cref="T:System.Threading.Tasks.Task" />。</summary>
    /// <returns>已启动的 <see cref="T:System.Threading.Tasks.Task" />。</returns>
    /// <param name="action">要异步执行的操作委托。</param>
    /// <param name="creationOptions">一个 TaskCreationOptions 值，用于控制创建的 <see cref="T:System.Threading.Tasks.Task" /> 的行为。</param>
    /// <exception cref="T:System.ArgumentNullException">当 <paramref name="action" /> 参数为 null 时引发的异常。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="creationOptions" /> 参数指定无效 TaskCreationOptions 值时引发的异常。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task StartNew(Action action, TaskCreationOptions creationOptions)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      Task internalCurrent = Task.InternalCurrent;
      return Task.InternalStartNew(internalCurrent, (Delegate) action, (object) null, this.m_defaultCancellationToken, this.GetDefaultScheduler(internalCurrent), creationOptions, InternalTaskOptions.None, ref stackMark);
    }

    /// <summary>创建并启动 <see cref="T:System.Threading.Tasks.Task" />。</summary>
    /// <returns>已启动的 <see cref="T:System.Threading.Tasks.Task" />。</returns>
    /// <param name="action">要异步执行的操作委托。</param>
    /// <param name="cancellationToken">将指派给新 <see cref="T:System.Threading.Tasks.Task" /> 的 <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /></param>
    /// <param name="creationOptions">一个 TaskCreationOptions 值，用于控制创建的 <see cref="T:System.Threading.Tasks.Task" /> 的行为。</param>
    /// <param name="scheduler">用于计划所创建的 <see cref="T:System.Threading.Tasks.TaskScheduler" /> 的 <see cref="T:System.Threading.Tasks.Task" />。</param>
    /// <exception cref="T:System.ObjectDisposedException">提供的 <see cref="T:System.Threading.CancellationToken" /> 已被释放。</exception>
    /// <exception cref="T:System.ArgumentNullException">当 <paramref name="action" /> 参数为 null 时引发的异常。- 或 -当 <paramref name="scheduler" /> 参数为 null 时引发的异常。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="creationOptions" /> 参数指定无效 TaskCreationOptions 值时引发的异常。<paramref name="creationOptions" /> 参数指定无效 TaskCreationOptions 值时引发的异常。有关更多信息，请参见 <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /> 的备注</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task StartNew(Action action, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return Task.InternalStartNew(Task.InternalCurrentIfAttached(creationOptions), (Delegate) action, (object) null, cancellationToken, scheduler, creationOptions, InternalTaskOptions.None, ref stackMark);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal Task StartNew(Action action, CancellationToken cancellationToken, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, TaskScheduler scheduler)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return Task.InternalStartNew(Task.InternalCurrentIfAttached(creationOptions), (Delegate) action, (object) null, cancellationToken, scheduler, creationOptions, internalOptions, ref stackMark);
    }

    /// <summary>创建并启动 <see cref="T:System.Threading.Tasks.Task" />。</summary>
    /// <returns>已启动的 <see cref="T:System.Threading.Tasks.Task" />。</returns>
    /// <param name="action">要异步执行的操作委托。</param>
    /// <param name="state">一个包含由 <paramref name="action" /> 委托使用的数据的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="action" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task StartNew(Action<object> action, object state)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      Task internalCurrent = Task.InternalCurrent;
      return Task.InternalStartNew(internalCurrent, (Delegate) action, state, this.m_defaultCancellationToken, this.GetDefaultScheduler(internalCurrent), this.m_defaultCreationOptions, InternalTaskOptions.None, ref stackMark);
    }

    /// <summary>创建并启动 <see cref="T:System.Threading.Tasks.Task" />。</summary>
    /// <returns>已启动的 <see cref="T:System.Threading.Tasks.Task" />。</returns>
    /// <param name="action">要异步执行的操作委托。</param>
    /// <param name="state">一个包含由 <paramref name="action" /> 委托使用的数据的对象。</param>
    /// <param name="cancellationToken">将指派给新 <see cref="T:System.Threading.Tasks.Task" /> 的 <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /></param>
    /// <exception cref="T:System.ObjectDisposedException">提供的 <see cref="T:System.Threading.CancellationToken" /> 已被释放。</exception>
    /// <exception cref="T:System.ArgumentNullException">当 <paramref name="action" /> 参数为 null 时引发的异常。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task StartNew(Action<object> action, object state, CancellationToken cancellationToken)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      Task internalCurrent = Task.InternalCurrent;
      return Task.InternalStartNew(internalCurrent, (Delegate) action, state, cancellationToken, this.GetDefaultScheduler(internalCurrent), this.m_defaultCreationOptions, InternalTaskOptions.None, ref stackMark);
    }

    /// <summary>创建并启动 <see cref="T:System.Threading.Tasks.Task" />。</summary>
    /// <returns>已启动的 <see cref="T:System.Threading.Tasks.Task" />。</returns>
    /// <param name="action">要异步执行的操作委托。</param>
    /// <param name="state">一个包含由 <paramref name="action" /> 委托使用的数据的对象。</param>
    /// <param name="creationOptions">一个 TaskCreationOptions 值，用于控制创建的 <see cref="T:System.Threading.Tasks.Task" /> 的行为。</param>
    /// <exception cref="T:System.ArgumentNullException">当 <paramref name="action" /> 参数为 null 时引发的异常。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="creationOptions" /> 参数指定无效 TaskCreationOptions 值时引发的异常。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task StartNew(Action<object> action, object state, TaskCreationOptions creationOptions)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      Task internalCurrent = Task.InternalCurrent;
      return Task.InternalStartNew(internalCurrent, (Delegate) action, state, this.m_defaultCancellationToken, this.GetDefaultScheduler(internalCurrent), creationOptions, InternalTaskOptions.None, ref stackMark);
    }

    /// <summary>创建并启动 <see cref="T:System.Threading.Tasks.Task" />。</summary>
    /// <returns>已启动的 <see cref="T:System.Threading.Tasks.Task" />。</returns>
    /// <param name="action">要异步执行的操作委托。</param>
    /// <param name="state">一个包含由 <paramref name="action" /> 委托使用的数据的对象。</param>
    /// <param name="cancellationToken">将指派给新任务的 <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" />。</param>
    /// <param name="creationOptions">一个 TaskCreationOptions 值，用于控制创建的 <see cref="T:System.Threading.Tasks.Task" /> 的行为。</param>
    /// <param name="scheduler">用于计划所创建的 <see cref="T:System.Threading.Tasks.TaskScheduler" /> 的 <see cref="T:System.Threading.Tasks.Task" />。</param>
    /// <exception cref="T:System.ObjectDisposedException">提供的 <see cref="T:System.Threading.CancellationToken" /> 已被释放。</exception>
    /// <exception cref="T:System.ArgumentNullException">当 <paramref name="action" /> 参数为 null 时引发的异常。- 或 -当 <paramref name="scheduler" /> 参数为 null 时引发的异常。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="creationOptions" /> 参数指定无效 TaskCreationOptions 值时引发的异常。<paramref name="creationOptions" /> 参数指定无效 TaskCreationOptions 值时引发的异常。有关更多信息，请参见 <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /> 的备注</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task StartNew(Action<object> action, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return Task.InternalStartNew(Task.InternalCurrentIfAttached(creationOptions), (Delegate) action, state, cancellationToken, scheduler, creationOptions, InternalTaskOptions.None, ref stackMark);
    }

    /// <summary>创建并启动 <see cref="T:System.Threading.Tasks.Task`1" />。</summary>
    /// <returns>已启动的 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    /// <param name="function">一个函数委托，可返回能够通过 <see cref="T:System.Threading.Tasks.Task`1" /> 获得的将来结果。</param>
    /// <typeparam name="TResult">可通过 <see cref="T:System.Threading.Tasks.Task`1" /> 获得的结果的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="function" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> StartNew<TResult>(Func<TResult> function)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      Task internalCurrent = Task.InternalCurrent;
      return Task<TResult>.StartNew(internalCurrent, function, this.m_defaultCancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackMark);
    }

    /// <summary>创建并启动 <see cref="T:System.Threading.Tasks.Task`1" />。</summary>
    /// <returns>已启动的 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    /// <param name="function">一个函数委托，可返回能够通过 <see cref="T:System.Threading.Tasks.Task`1" /> 获得的将来结果。</param>
    /// <param name="cancellationToken">将指派给新 <see cref="T:System.Threading.Tasks.Task" /> 的 <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /></param>
    /// <typeparam name="TResult">可通过 <see cref="T:System.Threading.Tasks.Task`1" /> 获得的结果的类型。</typeparam>
    /// <exception cref="T:System.ObjectDisposedException">提供的 <see cref="T:System.Threading.CancellationToken" /> 已被释放。</exception>
    /// <exception cref="T:System.ArgumentNullException">当 <paramref name="function" /> 参数为 null 时引发的异常。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> StartNew<TResult>(Func<TResult> function, CancellationToken cancellationToken)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      Task internalCurrent = Task.InternalCurrent;
      return Task<TResult>.StartNew(internalCurrent, function, cancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackMark);
    }

    /// <summary>创建并启动 <see cref="T:System.Threading.Tasks.Task`1" />。</summary>
    /// <returns>已启动的 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    /// <param name="function">一个函数委托，可返回能够通过 <see cref="T:System.Threading.Tasks.Task`1" /> 获得的将来结果。</param>
    /// <param name="creationOptions">一个 TaskCreationOptions 值，用于控制创建的 <see cref="T:System.Threading.Tasks.Task`1" /> 的行为。</param>
    /// <typeparam name="TResult">可通过 <see cref="T:System.Threading.Tasks.Task`1" /> 获得的结果的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">当 <paramref name="function" /> 参数为 null 时引发的异常。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="creationOptions" /> 参数指定无效 TaskCreationOptions 值时引发的异常。<paramref name="creationOptions" /> 参数指定无效 TaskCreationOptions 值时引发的异常。有关更多信息，请参见 <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /> 的备注</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> StartNew<TResult>(Func<TResult> function, TaskCreationOptions creationOptions)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      Task internalCurrent = Task.InternalCurrent;
      return Task<TResult>.StartNew(internalCurrent, function, this.m_defaultCancellationToken, creationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackMark);
    }

    /// <summary>创建并启动 <see cref="T:System.Threading.Tasks.Task`1" />。</summary>
    /// <returns>已启动的 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    /// <param name="function">一个函数委托，可返回能够通过 <see cref="T:System.Threading.Tasks.Task`1" /> 获得的将来结果。</param>
    /// <param name="cancellationToken">将指派给新任务的 <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" />。</param>
    /// <param name="creationOptions">一个 TaskCreationOptions 值，用于控制创建的 <see cref="T:System.Threading.Tasks.Task`1" /> 的行为。</param>
    /// <param name="scheduler">用于计划所创建的 <see cref="T:System.Threading.Tasks.TaskScheduler" /> 的 <see cref="T:System.Threading.Tasks.Task`1" />。</param>
    /// <typeparam name="TResult">可通过 <see cref="T:System.Threading.Tasks.Task`1" /> 获得的结果的类型。</typeparam>
    /// <exception cref="T:System.ObjectDisposedException">提供的 <see cref="T:System.Threading.CancellationToken" /> 已被释放。</exception>
    /// <exception cref="T:System.ArgumentNullException">当 <paramref name="function" /> 参数为 null 时引发的异常。- 或 -当 <paramref name="scheduler" /> 参数为 null 时引发的异常。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="creationOptions" /> 参数指定无效 TaskCreationOptions 值时引发的异常。<paramref name="creationOptions" /> 参数指定无效 TaskCreationOptions 值时引发的异常。有关更多信息，请参见 <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /> 的备注</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> StartNew<TResult>(Func<TResult> function, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return Task<TResult>.StartNew(Task.InternalCurrentIfAttached(creationOptions), function, cancellationToken, creationOptions, InternalTaskOptions.None, scheduler, ref stackMark);
    }

    /// <summary>创建并启动 <see cref="T:System.Threading.Tasks.Task`1" />。</summary>
    /// <returns>已启动的 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    /// <param name="function">一个函数委托，可返回能够通过 <see cref="T:System.Threading.Tasks.Task`1" /> 获得的将来结果。</param>
    /// <param name="state">一个包含由 <paramref name="function" /> 委托使用的数据的对象。</param>
    /// <typeparam name="TResult">可通过 <see cref="T:System.Threading.Tasks.Task`1" /> 获得的结果的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">当 <paramref name="function" /> 参数为 null 时引发的异常。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> StartNew<TResult>(Func<object, TResult> function, object state)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      Task internalCurrent = Task.InternalCurrent;
      return Task<TResult>.StartNew(internalCurrent, function, state, this.m_defaultCancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackMark);
    }

    /// <summary>创建并启动 <see cref="T:System.Threading.Tasks.Task`1" />。</summary>
    /// <returns>已启动的 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    /// <param name="function">一个函数委托，可返回能够通过 <see cref="T:System.Threading.Tasks.Task`1" /> 获得的将来结果。</param>
    /// <param name="state">一个包含由 <paramref name="function" /> 委托使用的数据的对象。</param>
    /// <param name="cancellationToken">将指派给新 <see cref="T:System.Threading.Tasks.Task" /> 的 <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /></param>
    /// <typeparam name="TResult">可通过 <see cref="T:System.Threading.Tasks.Task`1" /> 获得的结果的类型。</typeparam>
    /// <exception cref="T:System.ObjectDisposedException">提供的 <see cref="T:System.Threading.CancellationToken" /> 已被释放。</exception>
    /// <exception cref="T:System.ArgumentNullException">当 <paramref name="function" /> 参数为 null 时引发的异常。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> StartNew<TResult>(Func<object, TResult> function, object state, CancellationToken cancellationToken)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      Task internalCurrent = Task.InternalCurrent;
      return Task<TResult>.StartNew(internalCurrent, function, state, cancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackMark);
    }

    /// <summary>创建并启动 <see cref="T:System.Threading.Tasks.Task`1" />。</summary>
    /// <returns>已启动的 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    /// <param name="function">一个函数委托，可返回能够通过 <see cref="T:System.Threading.Tasks.Task`1" /> 获得的将来结果。</param>
    /// <param name="state">一个包含由 <paramref name="function" /> 委托使用的数据的对象。</param>
    /// <param name="creationOptions">一个 TaskCreationOptions 值，用于控制创建的 <see cref="T:System.Threading.Tasks.Task`1" /> 的行为。</param>
    /// <typeparam name="TResult">可通过 <see cref="T:System.Threading.Tasks.Task`1" /> 获得的结果的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">当 <paramref name="function" /> 参数为 null 时引发的异常。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="creationOptions" /> 参数指定无效 TaskCreationOptions 值时引发的异常。<paramref name="creationOptions" /> 参数指定无效 TaskCreationOptions 值时引发的异常。有关更多信息，请参见 <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /> 的备注</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> StartNew<TResult>(Func<object, TResult> function, object state, TaskCreationOptions creationOptions)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      Task internalCurrent = Task.InternalCurrent;
      return Task<TResult>.StartNew(internalCurrent, function, state, this.m_defaultCancellationToken, creationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackMark);
    }

    /// <summary>创建并启动 <see cref="T:System.Threading.Tasks.Task`1" />。</summary>
    /// <returns>已启动的 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    /// <param name="function">一个函数委托，可返回能够通过 <see cref="T:System.Threading.Tasks.Task`1" /> 获得的将来结果。</param>
    /// <param name="state">一个包含由 <paramref name="function" /> 委托使用的数据的对象。</param>
    /// <param name="cancellationToken">将指派给新任务的 <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" />。</param>
    /// <param name="creationOptions">一个 TaskCreationOptions 值，用于控制创建的 <see cref="T:System.Threading.Tasks.Task`1" /> 的行为。</param>
    /// <param name="scheduler">用于计划所创建的 <see cref="T:System.Threading.Tasks.TaskScheduler" /> 的 <see cref="T:System.Threading.Tasks.Task`1" />。</param>
    /// <typeparam name="TResult">可通过 <see cref="T:System.Threading.Tasks.Task`1" /> 获得的结果的类型。</typeparam>
    /// <exception cref="T:System.ObjectDisposedException">提供的 <see cref="T:System.Threading.CancellationToken" /> 已被释放。</exception>
    /// <exception cref="T:System.ArgumentNullException">当 <paramref name="function" /> 参数为 null 时引发的异常。- 或 -当 <paramref name="scheduler" /> 参数为 null 时引发的异常。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="creationOptions" /> 参数指定无效 TaskCreationOptions 值时引发的异常。<paramref name="creationOptions" /> 参数指定无效 TaskCreationOptions 值时引发的异常。有关更多信息，请参见 <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /> 的备注</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> StartNew<TResult>(Func<object, TResult> function, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return Task<TResult>.StartNew(Task.InternalCurrentIfAttached(creationOptions), function, state, cancellationToken, creationOptions, InternalTaskOptions.None, scheduler, ref stackMark);
    }

    /// <summary>创建一个 <see cref="T:System.Threading.Tasks.Task" />，它在指定的 <see cref="T:System.IAsyncResult" /> 完成时执行一个结束方法操作。</summary>
    /// <returns>一个表示异步操作的 <see cref="T:System.Threading.Tasks.Task" />。</returns>
    /// <param name="asyncResult">IAsyncResult，完成它时将触发对 <paramref name="endMethod" /> 的处理。</param>
    /// <param name="endMethod">用于处理完成的 <paramref name="asyncResult" /> 的操作委托。</param>
    /// <exception cref="T:System.ArgumentNullException">当 <paramref name="asyncResult" /> 参数为 null 时引发的异常。- 或 -当 <paramref name="endMethod" /> 参数为 null 时引发的异常。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task FromAsync(IAsyncResult asyncResult, Action<IAsyncResult> endMethod)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.FromAsync(asyncResult, endMethod, this.m_defaultCreationOptions, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>创建一个 <see cref="T:System.Threading.Tasks.Task" />，它在指定的 <see cref="T:System.IAsyncResult" /> 完成时执行一个结束方法操作。</summary>
    /// <returns>一个表示异步操作的 <see cref="T:System.Threading.Tasks.Task" />。</returns>
    /// <param name="asyncResult">IAsyncResult，完成它时将触发对 <paramref name="endMethod" /> 的处理。</param>
    /// <param name="endMethod">用于处理完成的 <paramref name="asyncResult" /> 的操作委托。</param>
    /// <param name="creationOptions">TaskCreationOptions 值，用于控制创建的 <see cref="T:System.Threading.Tasks.Task" /> 的行为。</param>
    /// <exception cref="T:System.ArgumentNullException">当 <paramref name="asyncResult" /> 参数为 null 时引发的异常。- 或 -当 <paramref name="endMethod" /> 参数为 null 时引发的异常。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="creationOptions" /> 参数指定无效 TaskCreationOptions 值时引发的异常。有关更多信息，请参见 <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /> 的备注</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task FromAsync(IAsyncResult asyncResult, Action<IAsyncResult> endMethod, TaskCreationOptions creationOptions)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.FromAsync(asyncResult, endMethod, creationOptions, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>创建一个 <see cref="T:System.Threading.Tasks.Task" />，它在指定的 <see cref="T:System.IAsyncResult" /> 完成时执行一个结束方法操作。</summary>
    /// <returns>创建的表示异步操作的 <see cref="T:System.Threading.Tasks.Task" />。</returns>
    /// <param name="asyncResult">IAsyncResult，完成它时将触发对 <paramref name="endMethod" /> 的处理。</param>
    /// <param name="endMethod">用于处理完成的 <paramref name="asyncResult" /> 的操作委托。</param>
    /// <param name="creationOptions">TaskCreationOptions 值，用于控制创建的 <see cref="T:System.Threading.Tasks.Task" /> 的行为。</param>
    /// <param name="scheduler">用于计划将执行结束方法的任务的 <see cref="T:System.Threading.Tasks.TaskScheduler" />。</param>
    /// <exception cref="T:System.ArgumentNullException">当 <paramref name="asyncResult" /> 参数为 null 时引发的异常。- 或 -当 <paramref name="endMethod" /> 参数为 null 时引发的异常。- 或 -当 <paramref name="scheduler" /> 参数为 null 时引发的异常。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="creationOptions" /> 参数指定无效 TaskCreationOptions 值时引发的异常。<paramref name="creationOptions" /> 参数指定无效 TaskCreationOptions 值时引发的异常。有关更多信息，请参见 <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /> 的备注</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task FromAsync(IAsyncResult asyncResult, Action<IAsyncResult> endMethod, TaskCreationOptions creationOptions, TaskScheduler scheduler)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.FromAsync(asyncResult, endMethod, creationOptions, scheduler, ref stackMark);
    }

    private Task FromAsync(IAsyncResult asyncResult, Action<IAsyncResult> endMethod, TaskCreationOptions creationOptions, TaskScheduler scheduler, ref StackCrawlMark stackMark)
    {
      return (Task) TaskFactory<VoidTaskResult>.FromAsyncImpl(asyncResult, (Func<IAsyncResult, VoidTaskResult>) null, endMethod, creationOptions, scheduler, ref stackMark);
    }

    /// <summary>创建一个 <see cref="T:System.Threading.Tasks.Task" />，表示符合异步编程模型模式的成对的开始和结束方法。</summary>
    /// <returns>创建的表示异步操作的 <see cref="T:System.Threading.Tasks.Task" />。</returns>
    /// <param name="beginMethod">用于启动异步操作的委托。</param>
    /// <param name="endMethod">用于结束异步操作的委托。</param>
    /// <param name="state">一个包含由 <paramref name="beginMethod" /> 委托使用的数据的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">当 <paramref name="beginMethod" /> 参数为 null 时引发的异常。- 或 -当 <paramref name="endMethod" /> 参数为 null 时引发的异常。</exception>
    [__DynamicallyInvokable]
    public Task FromAsync(Func<AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, object state)
    {
      return this.FromAsync(beginMethod, endMethod, state, this.m_defaultCreationOptions);
    }

    /// <summary>创建一个 <see cref="T:System.Threading.Tasks.Task" />，表示符合异步编程模型模式的成对的开始和结束方法。</summary>
    /// <returns>创建的表示异步操作的 <see cref="T:System.Threading.Tasks.Task" />。</returns>
    /// <param name="beginMethod">用于启动异步操作的委托。</param>
    /// <param name="endMethod">用于结束异步操作的委托。</param>
    /// <param name="state">一个包含由 <paramref name="beginMethod" /> 委托使用的数据的对象。</param>
    /// <param name="creationOptions">TaskCreationOptions 值，用于控制创建的 <see cref="T:System.Threading.Tasks.Task" /> 的行为。</param>
    /// <exception cref="T:System.ArgumentNullException">当 <paramref name="beginMethod" /> 参数为 null 时引发的异常。- 或 -当 <paramref name="endMethod" /> 参数为 null 时引发的异常。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="creationOptions" /> 参数指定无效 TaskCreationOptions 值时引发的异常。</exception>
    [__DynamicallyInvokable]
    public Task FromAsync(Func<AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, object state, TaskCreationOptions creationOptions)
    {
      return (Task) TaskFactory<VoidTaskResult>.FromAsyncImpl(beginMethod, (Func<IAsyncResult, VoidTaskResult>) null, endMethod, state, creationOptions);
    }

    /// <summary>创建一个 <see cref="T:System.Threading.Tasks.Task" />，表示符合异步编程模型模式的成对的开始和结束方法。</summary>
    /// <returns>创建的表示异步操作的 <see cref="T:System.Threading.Tasks.Task" />。</returns>
    /// <param name="beginMethod">用于启动异步操作的委托。</param>
    /// <param name="endMethod">用于结束异步操作的委托。</param>
    /// <param name="arg1">传递给 <paramref name="beginMethod" /> 委托的第一个参数。</param>
    /// <param name="state">一个包含由 <paramref name="beginMethod" /> 委托使用的数据的对象。</param>
    /// <typeparam name="TArg1">传递给 <paramref name="beginMethod" /> 委托的第一个参数的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">当 <paramref name="beginMethod" /> 参数为 null 时引发的异常。- 或 -当 <paramref name="endMethod" /> 参数为 null 时引发的异常。</exception>
    [__DynamicallyInvokable]
    public Task FromAsync<TArg1>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, object state)
    {
      return this.FromAsync<TArg1>(beginMethod, endMethod, arg1, state, this.m_defaultCreationOptions);
    }

    /// <summary>创建一个 <see cref="T:System.Threading.Tasks.Task" />，表示符合异步编程模型模式的成对的开始和结束方法。</summary>
    /// <returns>创建的表示异步操作的 <see cref="T:System.Threading.Tasks.Task" />。</returns>
    /// <param name="beginMethod">用于启动异步操作的委托。</param>
    /// <param name="endMethod">用于结束异步操作的委托。</param>
    /// <param name="arg1">传递给 <paramref name="beginMethod" /> 委托的第一个参数。</param>
    /// <param name="state">一个包含由 <paramref name="beginMethod" /> 委托使用的数据的对象。</param>
    /// <param name="creationOptions">TaskCreationOptions 值，用于控制创建的 <see cref="T:System.Threading.Tasks.Task" /> 的行为。</param>
    /// <typeparam name="TArg1">传递给 <paramref name="beginMethod" /> 委托的第一个参数的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">当 <paramref name="beginMethod" /> 参数为 null 时引发的异常。- 或 -当 <paramref name="endMethod" /> 参数为 null 时引发的异常。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="creationOptions" /> 参数指定无效 TaskCreationOptions 值时引发的异常。<paramref name="creationOptions" /> 参数指定无效 TaskCreationOptions 值时引发的异常。有关更多信息，请参见 <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /> 的备注</exception>
    [__DynamicallyInvokable]
    public Task FromAsync<TArg1>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, object state, TaskCreationOptions creationOptions)
    {
      return (Task) TaskFactory<VoidTaskResult>.FromAsyncImpl<TArg1>(beginMethod, (Func<IAsyncResult, VoidTaskResult>) null, endMethod, arg1, state, creationOptions);
    }

    /// <summary>创建一个 <see cref="T:System.Threading.Tasks.Task" />，表示符合异步编程模型模式的成对的开始和结束方法。</summary>
    /// <returns>创建的表示异步操作的 <see cref="T:System.Threading.Tasks.Task" />。</returns>
    /// <param name="beginMethod">用于启动异步操作的委托。</param>
    /// <param name="endMethod">用于结束异步操作的委托。</param>
    /// <param name="arg1">传递给 <paramref name="beginMethod" /> 委托的第一个参数。</param>
    /// <param name="arg2">传递给 <paramref name="beginMethod" /> 委托的第二个参数。</param>
    /// <param name="state">一个包含由 <paramref name="beginMethod" /> 委托使用的数据的对象。</param>
    /// <typeparam name="TArg1">传递给 <paramref name="beginMethod" /> 委托的第二个参数的类型。</typeparam>
    /// <typeparam name="TArg2">传递给 <paramref name="beginMethod" /> 委托的第一个参数的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">当 <paramref name="beginMethod" /> 参数为 null 时引发的异常。- 或 -当 <paramref name="endMethod" /> 参数为 null 时引发的异常。</exception>
    [__DynamicallyInvokable]
    public Task FromAsync<TArg1, TArg2>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, TArg2 arg2, object state)
    {
      return this.FromAsync<TArg1, TArg2>(beginMethod, endMethod, arg1, arg2, state, this.m_defaultCreationOptions);
    }

    /// <summary>创建一个 <see cref="T:System.Threading.Tasks.Task" />，表示符合异步编程模型模式的成对的开始和结束方法。</summary>
    /// <returns>创建的表示异步操作的 <see cref="T:System.Threading.Tasks.Task" />。</returns>
    /// <param name="beginMethod">用于启动异步操作的委托。</param>
    /// <param name="endMethod">用于结束异步操作的委托。</param>
    /// <param name="arg1">传递给 <paramref name="beginMethod" /> 委托的第一个参数。</param>
    /// <param name="arg2">传递给 <paramref name="beginMethod" /> 委托的第二个参数。</param>
    /// <param name="state">一个包含由 <paramref name="beginMethod" /> 委托使用的数据的对象。</param>
    /// <param name="creationOptions">TaskCreationOptions 值，用于控制创建的 <see cref="T:System.Threading.Tasks.Task" /> 的行为。</param>
    /// <typeparam name="TArg1">传递给 <paramref name="beginMethod" /> 委托的第二个参数的类型。</typeparam>
    /// <typeparam name="TArg2">传递给 <paramref name="beginMethod" /> 委托的第一个参数的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">当 <paramref name="beginMethod" /> 参数为 null 时引发的异常。- 或 -当 <paramref name="endMethod" /> 参数为 null 时引发的异常。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="creationOptions" /> 参数指定无效 TaskCreationOptions 值时引发的异常。<paramref name="creationOptions" /> 参数指定无效 TaskCreationOptions 值时引发的异常。有关更多信息，请参见 <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /> 的备注</exception>
    [__DynamicallyInvokable]
    public Task FromAsync<TArg1, TArg2>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, TArg2 arg2, object state, TaskCreationOptions creationOptions)
    {
      return (Task) TaskFactory<VoidTaskResult>.FromAsyncImpl<TArg1, TArg2>(beginMethod, (Func<IAsyncResult, VoidTaskResult>) null, endMethod, arg1, arg2, state, creationOptions);
    }

    /// <summary>创建一个 <see cref="T:System.Threading.Tasks.Task" />，表示符合异步编程模型模式的成对的开始和结束方法。</summary>
    /// <returns>创建的表示异步操作的 <see cref="T:System.Threading.Tasks.Task" />。</returns>
    /// <param name="beginMethod">用于启动异步操作的委托。</param>
    /// <param name="endMethod">用于结束异步操作的委托。</param>
    /// <param name="arg1">传递给 <paramref name="beginMethod" /> 委托的第一个参数。</param>
    /// <param name="arg2">传递给 <paramref name="beginMethod" /> 委托的第二个参数。</param>
    /// <param name="arg3">传递给 <paramref name="beginMethod" /> 委托的第三个参数。</param>
    /// <param name="state">一个包含由 <paramref name="beginMethod" /> 委托使用的数据的对象。</param>
    /// <typeparam name="TArg1">传递给 <paramref name="beginMethod" /> 委托的第二个参数的类型。</typeparam>
    /// <typeparam name="TArg2">传递给 <paramref name="beginMethod" /> 委托的第三个参数的类型。</typeparam>
    /// <typeparam name="TArg3">传递给 <paramref name="beginMethod" /> 委托的第一个参数的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">当 <paramref name="beginMethod" /> 参数为 null 时引发的异常。- 或 -当 <paramref name="endMethod" /> 参数为 null 时引发的异常。</exception>
    [__DynamicallyInvokable]
    public Task FromAsync<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state)
    {
      return this.FromAsync<TArg1, TArg2, TArg3>(beginMethod, endMethod, arg1, arg2, arg3, state, this.m_defaultCreationOptions);
    }

    /// <summary>创建一个 <see cref="T:System.Threading.Tasks.Task" />，表示符合异步编程模型模式的成对的开始和结束方法。</summary>
    /// <returns>创建的表示异步操作的 <see cref="T:System.Threading.Tasks.Task" />。</returns>
    /// <param name="beginMethod">用于启动异步操作的委托。</param>
    /// <param name="endMethod">用于结束异步操作的委托。</param>
    /// <param name="arg1">传递给 <paramref name="beginMethod" /> 委托的第一个参数。</param>
    /// <param name="arg2">传递给 <paramref name="beginMethod" /> 委托的第二个参数。</param>
    /// <param name="arg3">传递给 <paramref name="beginMethod" /> 委托的第三个参数。</param>
    /// <param name="state">一个包含由 <paramref name="beginMethod" /> 委托使用的数据的对象。</param>
    /// <param name="creationOptions">TaskCreationOptions 值，用于控制创建的 <see cref="T:System.Threading.Tasks.Task" /> 的行为。</param>
    /// <typeparam name="TArg1">传递给 <paramref name="beginMethod" /> 委托的第二个参数的类型。</typeparam>
    /// <typeparam name="TArg2">传递给 <paramref name="beginMethod" /> 委托的第三个参数的类型。</typeparam>
    /// <typeparam name="TArg3">传递给 <paramref name="beginMethod" /> 委托的第一个参数的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">当 <paramref name="beginMethod" /> 参数为 null 时引发的异常。- 或 -当 <paramref name="endMethod" /> 参数为 null 时引发的异常。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="creationOptions" /> 参数指定无效 TaskCreationOptions 值时引发的异常。<paramref name="creationOptions" /> 参数指定无效 TaskCreationOptions 值时引发的异常。有关更多信息，请参见 <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /> 的备注</exception>
    [__DynamicallyInvokable]
    public Task FromAsync<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state, TaskCreationOptions creationOptions)
    {
      return (Task) TaskFactory<VoidTaskResult>.FromAsyncImpl<TArg1, TArg2, TArg3>(beginMethod, (Func<IAsyncResult, VoidTaskResult>) null, endMethod, arg1, arg2, arg3, state, creationOptions);
    }

    /// <summary>创建一个 <see cref="T:System.Threading.Tasks.Task`1" />，它在指定的 <see cref="T:System.IAsyncResult" /> 完成时执行一个结束方法函数。</summary>
    /// <returns>一个表示异步操作的 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    /// <param name="asyncResult">IAsyncResult，完成它时将触发对 <paramref name="endMethod" /> 的处理。</param>
    /// <param name="endMethod">用于处理完成的 <paramref name="asyncResult" /> 的函数委托。</param>
    /// <typeparam name="TResult">可通过 <see cref="T:System.Threading.Tasks.Task`1" /> 获得的结果的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">当 <paramref name="asyncResult" /> 参数为 null 时引发的异常。- 或 -当 <paramref name="endMethod" /> 参数为 null 时引发的异常。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> FromAsync<TResult>(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.FromAsyncImpl(asyncResult, endMethod, (Action<IAsyncResult>) null, this.m_defaultCreationOptions, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>创建一个 <see cref="T:System.Threading.Tasks.Task`1" />，它在指定的 <see cref="T:System.IAsyncResult" /> 完成时执行一个结束方法函数。</summary>
    /// <returns>一个表示异步操作的 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    /// <param name="asyncResult">IAsyncResult，完成它时将触发对 <paramref name="endMethod" /> 的处理。</param>
    /// <param name="endMethod">用于处理完成的 <paramref name="asyncResult" /> 的函数委托。</param>
    /// <param name="creationOptions">TaskCreationOptions 值，用于控制创建的 <see cref="T:System.Threading.Tasks.Task`1" /> 的行为。</param>
    /// <typeparam name="TResult">可通过 <see cref="T:System.Threading.Tasks.Task`1" /> 获得的结果的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">当 <paramref name="asyncResult" /> 参数为 null 时引发的异常。- 或 -当 <paramref name="endMethod" /> 参数为 null 时引发的异常。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="creationOptions" /> 参数指定无效 TaskCreationOptions 值时引发的异常。<paramref name="creationOptions" /> 参数指定无效 TaskCreationOptions 值时引发的异常。有关更多信息，请参见 <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /> 的备注</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> FromAsync<TResult>(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod, TaskCreationOptions creationOptions)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.FromAsyncImpl(asyncResult, endMethod, (Action<IAsyncResult>) null, creationOptions, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>创建一个 <see cref="T:System.Threading.Tasks.Task`1" />，它在指定的 <see cref="T:System.IAsyncResult" /> 完成时执行一个结束方法函数。</summary>
    /// <returns>一个表示异步操作的 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    /// <param name="asyncResult">IAsyncResult，完成它时将触发对 <paramref name="endMethod" /> 的处理。</param>
    /// <param name="endMethod">用于处理完成的 <paramref name="asyncResult" /> 的函数委托。</param>
    /// <param name="creationOptions">TaskCreationOptions 值，用于控制创建的 <see cref="T:System.Threading.Tasks.Task`1" /> 的行为。</param>
    /// <param name="scheduler">用于计划将执行结束方法的任务的 <see cref="T:System.Threading.Tasks.TaskScheduler" />。</param>
    /// <typeparam name="TResult">可通过 <see cref="T:System.Threading.Tasks.Task`1" /> 获得的结果的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">当 <paramref name="asyncResult" /> 参数为 null 时引发的异常。- 或 -当 <paramref name="endMethod" /> 参数为 null 时引发的异常。- 或 -当 <paramref name="scheduler" /> 参数为 null 时引发的异常。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="creationOptions" /> 参数指定无效 TaskCreationOptions 值时引发的异常。<paramref name="creationOptions" /> 参数指定无效 TaskCreationOptions 值时引发的异常。有关更多信息，请参见 <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /> 的备注</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> FromAsync<TResult>(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod, TaskCreationOptions creationOptions, TaskScheduler scheduler)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.FromAsyncImpl(asyncResult, endMethod, (Action<IAsyncResult>) null, creationOptions, scheduler, ref stackMark);
    }

    /// <summary>创建一个 <see cref="T:System.Threading.Tasks.Task`1" />，表示符合异步编程模型模式的成对的开始和结束方法。</summary>
    /// <returns>创建的表示异步操作的 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    /// <param name="beginMethod">用于启动异步操作的委托。</param>
    /// <param name="endMethod">用于结束异步操作的委托。</param>
    /// <param name="state">一个包含由 <paramref name="beginMethod" /> 委托使用的数据的对象。</param>
    /// <typeparam name="TResult">可通过 <see cref="T:System.Threading.Tasks.Task`1" /> 获得的结果的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">当 <paramref name="beginMethod" /> 参数为 null 时引发的异常。- 或 -当 <paramref name="endMethod" /> 参数为 null 时引发的异常。</exception>
    [__DynamicallyInvokable]
    public Task<TResult> FromAsync<TResult>(Func<AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, object state)
    {
      return TaskFactory<TResult>.FromAsyncImpl(beginMethod, endMethod, (Action<IAsyncResult>) null, state, this.m_defaultCreationOptions);
    }

    /// <summary>创建一个 <see cref="T:System.Threading.Tasks.Task`1" />，表示符合异步编程模型模式的成对的开始和结束方法。</summary>
    /// <returns>创建的表示异步操作的 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    /// <param name="beginMethod">用于启动异步操作的委托。</param>
    /// <param name="endMethod">用于结束异步操作的委托。</param>
    /// <param name="state">一个包含由 <paramref name="beginMethod" /> 委托使用的数据的对象。</param>
    /// <param name="creationOptions">TaskCreationOptions 值，用于控制创建的 <see cref="T:System.Threading.Tasks.Task`1" /> 的行为。</param>
    /// <typeparam name="TResult">可通过 <see cref="T:System.Threading.Tasks.Task`1" /> 获得的结果的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">当 <paramref name="beginMethod" /> 参数为 null 时引发的异常。- 或 -当 <paramref name="endMethod" /> 参数为 null 时引发的异常。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="creationOptions" /> 参数指定无效 TaskCreationOptions 值时引发的异常。<paramref name="creationOptions" /> 参数指定无效 TaskCreationOptions 值时引发的异常。有关更多信息，请参见 <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /> 的备注</exception>
    [__DynamicallyInvokable]
    public Task<TResult> FromAsync<TResult>(Func<AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, object state, TaskCreationOptions creationOptions)
    {
      return TaskFactory<TResult>.FromAsyncImpl(beginMethod, endMethod, (Action<IAsyncResult>) null, state, creationOptions);
    }

    /// <summary>创建一个 <see cref="T:System.Threading.Tasks.Task`1" />，表示符合异步编程模型模式的成对的开始和结束方法。</summary>
    /// <returns>创建的表示异步操作的 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    /// <param name="beginMethod">用于启动异步操作的委托。</param>
    /// <param name="endMethod">用于结束异步操作的委托。</param>
    /// <param name="arg1">传递给 <paramref name="beginMethod" /> 委托的第一个参数。</param>
    /// <param name="state">一个包含由 <paramref name="beginMethod" /> 委托使用的数据的对象。</param>
    /// <typeparam name="TArg1">传递给 <paramref name="beginMethod" /> 委托的第一个参数的类型。</typeparam>
    /// <typeparam name="TResult">可通过 <see cref="T:System.Threading.Tasks.Task`1" /> 获得的结果的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">当 <paramref name="beginMethod" /> 参数为 null 时引发的异常。- 或 -当 <paramref name="endMethod" /> 参数为 null 时引发的异常。</exception>
    [__DynamicallyInvokable]
    public Task<TResult> FromAsync<TArg1, TResult>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, object state)
    {
      return TaskFactory<TResult>.FromAsyncImpl<TArg1>(beginMethod, endMethod, (Action<IAsyncResult>) null, arg1, state, this.m_defaultCreationOptions);
    }

    /// <summary>创建一个 <see cref="T:System.Threading.Tasks.Task`1" />，表示符合异步编程模型模式的成对的开始和结束方法。</summary>
    /// <returns>创建的表示异步操作的 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    /// <param name="beginMethod">用于启动异步操作的委托。</param>
    /// <param name="endMethod">用于结束异步操作的委托。</param>
    /// <param name="arg1">传递给 <paramref name="beginMethod" /> 委托的第一个参数。</param>
    /// <param name="state">一个包含由 <paramref name="beginMethod" /> 委托使用的数据的对象。</param>
    /// <param name="creationOptions">TaskCreationOptions 值，用于控制创建的 <see cref="T:System.Threading.Tasks.Task`1" /> 的行为。</param>
    /// <typeparam name="TArg1">传递给 <paramref name="beginMethod" /> 委托的第一个参数的类型。</typeparam>
    /// <typeparam name="TResult">可通过 <see cref="T:System.Threading.Tasks.Task`1" /> 获得的结果的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">当 <paramref name="beginMethod" /> 参数为 null 时引发的异常。- 或 -当 <paramref name="endMethod" /> 参数为 null 时引发的异常。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="creationOptions" /> 参数指定无效 TaskCreationOptions 值时引发的异常。<paramref name="creationOptions" /> 参数指定无效 TaskCreationOptions 值时引发的异常。有关更多信息，请参见 <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /> 的备注</exception>
    [__DynamicallyInvokable]
    public Task<TResult> FromAsync<TArg1, TResult>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, object state, TaskCreationOptions creationOptions)
    {
      return TaskFactory<TResult>.FromAsyncImpl<TArg1>(beginMethod, endMethod, (Action<IAsyncResult>) null, arg1, state, creationOptions);
    }

    /// <summary>创建一个 <see cref="T:System.Threading.Tasks.Task`1" />，表示符合异步编程模型模式的成对的开始和结束方法。</summary>
    /// <returns>创建的表示异步操作的 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    /// <param name="beginMethod">用于启动异步操作的委托。</param>
    /// <param name="endMethod">用于结束异步操作的委托。</param>
    /// <param name="arg1">传递给 <paramref name="beginMethod" /> 委托的第一个参数。</param>
    /// <param name="arg2">传递给 <paramref name="beginMethod" /> 委托的第二个参数。</param>
    /// <param name="state">一个包含由 <paramref name="beginMethod" /> 委托使用的数据的对象。</param>
    /// <typeparam name="TArg1">传递给 <paramref name="beginMethod" /> 委托的第二个参数的类型。</typeparam>
    /// <typeparam name="TArg2">传递给 <paramref name="beginMethod" /> 委托的第一个参数的类型。</typeparam>
    /// <typeparam name="TResult">可通过 <see cref="T:System.Threading.Tasks.Task`1" /> 获得的结果的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">当 <paramref name="beginMethod" /> 参数为 null 时引发的异常。- 或 -当 <paramref name="endMethod" /> 参数为 null 时引发的异常。</exception>
    [__DynamicallyInvokable]
    public Task<TResult> FromAsync<TArg1, TArg2, TResult>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, object state)
    {
      return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2>(beginMethod, endMethod, (Action<IAsyncResult>) null, arg1, arg2, state, this.m_defaultCreationOptions);
    }

    /// <summary>创建一个 <see cref="T:System.Threading.Tasks.Task`1" />，表示符合异步编程模型模式的成对的开始和结束方法。</summary>
    /// <returns>创建的表示异步操作的 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    /// <param name="beginMethod">用于启动异步操作的委托。</param>
    /// <param name="endMethod">用于结束异步操作的委托。</param>
    /// <param name="arg1">传递给 <paramref name="beginMethod" /> 委托的第一个参数。</param>
    /// <param name="arg2">传递给 <paramref name="beginMethod" /> 委托的第二个参数。</param>
    /// <param name="state">一个包含由 <paramref name="beginMethod" /> 委托使用的数据的对象。</param>
    /// <param name="creationOptions">TaskCreationOptions 值，用于控制创建的 <see cref="T:System.Threading.Tasks.Task`1" /> 的行为。</param>
    /// <typeparam name="TArg1">传递给 <paramref name="beginMethod" /> 委托的第二个参数的类型。</typeparam>
    /// <typeparam name="TArg2">传递给 <paramref name="beginMethod" /> 委托的第一个参数的类型。</typeparam>
    /// <typeparam name="TResult">可通过 <see cref="T:System.Threading.Tasks.Task`1" /> 获得的结果的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">当 <paramref name="beginMethod" /> 参数为 null 时引发的异常。- 或 -当 <paramref name="endMethod" /> 参数为 null 时引发的异常。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="creationOptions" /> 参数指定无效 TaskCreationOptions 值时引发的异常。<paramref name="creationOptions" /> 参数指定无效 TaskCreationOptions 值时引发的异常。有关更多信息，请参见 <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /> 的备注</exception>
    [__DynamicallyInvokable]
    public Task<TResult> FromAsync<TArg1, TArg2, TResult>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, object state, TaskCreationOptions creationOptions)
    {
      return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2>(beginMethod, endMethod, (Action<IAsyncResult>) null, arg1, arg2, state, creationOptions);
    }

    /// <summary>创建一个 <see cref="T:System.Threading.Tasks.Task`1" />，表示符合异步编程模型模式的成对的开始和结束方法。</summary>
    /// <returns>创建的表示异步操作的 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    /// <param name="beginMethod">用于启动异步操作的委托。</param>
    /// <param name="endMethod">用于结束异步操作的委托。</param>
    /// <param name="arg1">传递给 <paramref name="beginMethod" /> 委托的第一个参数。</param>
    /// <param name="arg2">传递给 <paramref name="beginMethod" /> 委托的第二个参数。</param>
    /// <param name="arg3">传递给 <paramref name="beginMethod" /> 委托的第三个参数。</param>
    /// <param name="state">一个包含由 <paramref name="beginMethod" /> 委托使用的数据的对象。</param>
    /// <typeparam name="TArg1">传递给 <paramref name="beginMethod" /> 委托的第二个参数的类型。</typeparam>
    /// <typeparam name="TArg2">传递给 <paramref name="beginMethod" /> 委托的第三个参数的类型。</typeparam>
    /// <typeparam name="TArg3">传递给 <paramref name="beginMethod" /> 委托的第一个参数的类型。</typeparam>
    /// <typeparam name="TResult">可通过 <see cref="T:System.Threading.Tasks.Task`1" /> 获得的结果的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">当 <paramref name="beginMethod" /> 参数为 null 时引发的异常。- 或 -当 <paramref name="endMethod" /> 参数为 null 时引发的异常。</exception>
    [__DynamicallyInvokable]
    public Task<TResult> FromAsync<TArg1, TArg2, TArg3, TResult>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state)
    {
      return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2, TArg3>(beginMethod, endMethod, (Action<IAsyncResult>) null, arg1, arg2, arg3, state, this.m_defaultCreationOptions);
    }

    /// <summary>创建一个 <see cref="T:System.Threading.Tasks.Task`1" />，表示符合异步编程模型模式的成对的开始和结束方法。</summary>
    /// <returns>创建的表示异步操作的 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    /// <param name="beginMethod">用于启动异步操作的委托。</param>
    /// <param name="endMethod">用于结束异步操作的委托。</param>
    /// <param name="arg1">传递给 <paramref name="beginMethod" /> 委托的第一个参数。</param>
    /// <param name="arg2">传递给 <paramref name="beginMethod" /> 委托的第二个参数。</param>
    /// <param name="arg3">传递给 <paramref name="beginMethod" /> 委托的第三个参数。</param>
    /// <param name="state">一个包含由 <paramref name="beginMethod" /> 委托使用的数据的对象。</param>
    /// <param name="creationOptions">TaskCreationOptions 值，用于控制创建的 <see cref="T:System.Threading.Tasks.Task`1" /> 的行为。</param>
    /// <typeparam name="TArg1">传递给 <paramref name="beginMethod" /> 委托的第二个参数的类型。</typeparam>
    /// <typeparam name="TArg2">传递给 <paramref name="beginMethod" /> 委托的第三个参数的类型。</typeparam>
    /// <typeparam name="TArg3">传递给 <paramref name="beginMethod" /> 委托的第一个参数的类型。</typeparam>
    /// <typeparam name="TResult">可通过 <see cref="T:System.Threading.Tasks.Task`1" /> 获得的结果的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">当 <paramref name="beginMethod" /> 参数为 null 时引发的异常。- 或 -当 <paramref name="endMethod" /> 参数为 null 时引发的异常。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="creationOptions" /> 参数指定无效 TaskCreationOptions 值时引发的异常。<paramref name="creationOptions" /> 参数指定无效 TaskCreationOptions 值时引发的异常。有关更多信息，请参见 <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /> 的备注</exception>
    [__DynamicallyInvokable]
    public Task<TResult> FromAsync<TArg1, TArg2, TArg3, TResult>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state, TaskCreationOptions creationOptions)
    {
      return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2, TArg3>(beginMethod, endMethod, (Action<IAsyncResult>) null, arg1, arg2, arg3, state, creationOptions);
    }

    internal static void CheckFromAsyncOptions(TaskCreationOptions creationOptions, bool hasBeginMethod)
    {
      if (hasBeginMethod)
      {
        if ((creationOptions & TaskCreationOptions.LongRunning) != TaskCreationOptions.None)
          throw new ArgumentOutOfRangeException("creationOptions", Environment.GetResourceString("Task_FromAsync_LongRunning"));
        if ((creationOptions & TaskCreationOptions.PreferFairness) != TaskCreationOptions.None)
          throw new ArgumentOutOfRangeException("creationOptions", Environment.GetResourceString("Task_FromAsync_PreferFairness"));
      }
      if ((creationOptions & ~(TaskCreationOptions.PreferFairness | TaskCreationOptions.LongRunning | TaskCreationOptions.AttachedToParent | TaskCreationOptions.DenyChildAttach | TaskCreationOptions.HideScheduler)) != TaskCreationOptions.None)
        throw new ArgumentOutOfRangeException("creationOptions");
    }

    internal static Task<Task[]> CommonCWAllLogic(Task[] tasksCopy)
    {
      TaskFactory.CompleteOnCountdownPromise countdownPromise = new TaskFactory.CompleteOnCountdownPromise(tasksCopy);
      for (int index = 0; index < tasksCopy.Length; ++index)
      {
        if (tasksCopy[index].IsCompleted)
          countdownPromise.Invoke(tasksCopy[index]);
        else
          tasksCopy[index].AddCompletionAction((ITaskCompletionAction) countdownPromise);
      }
      return (Task<Task[]>) countdownPromise;
    }

    internal static Task<Task<T>[]> CommonCWAllLogic<T>(Task<T>[] tasksCopy)
    {
      TaskFactory.CompleteOnCountdownPromise<T> countdownPromise = new TaskFactory.CompleteOnCountdownPromise<T>(tasksCopy);
      for (int index = 0; index < tasksCopy.Length; ++index)
      {
        if (tasksCopy[index].IsCompleted)
          countdownPromise.Invoke((Task) tasksCopy[index]);
        else
          tasksCopy[index].AddCompletionAction((ITaskCompletionAction) countdownPromise);
      }
      return (Task<Task<T>[]>) countdownPromise;
    }

    /// <summary>创建一个延续任务，该任务在一组指定的任务完成后开始。</summary>
    /// <returns>新的延续任务。</returns>
    /// <param name="tasks">继续执行的任务所在的数组。</param>
    /// <param name="continuationAction">在 <paramref name="tasks" /> 数组中的所有任务完成时要执行的操作委托。</param>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <paramref name="tasks" /> 数组中的一个元素已被释放。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tasks" /> 数组为 null。- 或 -<paramref name="continuationAction" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tasks" /> 数组为空或包含 null 值。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWhenAll(Task[] tasks, Action<Task[]> continuationAction)
    {
      if (continuationAction == null)
        throw new ArgumentNullException("continuationAction");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Task) TaskFactory<VoidTaskResult>.ContinueWhenAllImpl(tasks, (Func<Task[], VoidTaskResult>) null, continuationAction, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>创建一个延续任务，该任务在一组指定的任务完成后开始。</summary>
    /// <returns>新的延续任务。</returns>
    /// <param name="tasks">继续执行的任务所在的数组。</param>
    /// <param name="continuationAction">在 <paramref name="tasks" /> 数组中的所有任务完成时要执行的操作委托。</param>
    /// <param name="cancellationToken">将指派给新的延续任务的取消标记。</param>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <paramref name="tasks" /> 数组中的一个元素已被释放。- 或 -创建了 <see cref="T:System.Threading.CancellationTokenSource" /> 的 <paramref name="cancellationToken" /> 已经被释放。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tasks" /> 数组为 null。- 或 -<paramref name="continuationAction" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tasks" /> 数组为空或包含 null 值。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWhenAll(Task[] tasks, Action<Task[]> continuationAction, CancellationToken cancellationToken)
    {
      if (continuationAction == null)
        throw new ArgumentNullException("continuationAction");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Task) TaskFactory<VoidTaskResult>.ContinueWhenAllImpl(tasks, (Func<Task[], VoidTaskResult>) null, continuationAction, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>创建一个延续任务，该任务在一组指定的任务完成后开始。</summary>
    /// <returns>新的延续任务。</returns>
    /// <param name="tasks">继续执行的任务所在的数组。</param>
    /// <param name="continuationAction">在 <paramref name="tasks" /> 数组中的所有任务完成时要执行的操作委托。</param>
    /// <param name="continuationOptions">枚举值的按位组合，这些枚举值控制新的延续任务的行为。NotOn* 和 OnlyOn* 成员不受支持。</param>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <paramref name="tasks" /> 数组中的一个元素已被释放。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tasks" /> 数组为 null。- 或 -<paramref name="continuationAction" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="continuationOptions" /> 参数指定的值无效。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tasks" /> 数组为空或包含 null 值。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWhenAll(Task[] tasks, Action<Task[]> continuationAction, TaskContinuationOptions continuationOptions)
    {
      if (continuationAction == null)
        throw new ArgumentNullException("continuationAction");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Task) TaskFactory<VoidTaskResult>.ContinueWhenAllImpl(tasks, (Func<Task[], VoidTaskResult>) null, continuationAction, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>创建一个延续任务，该任务在一组指定的任务完成后开始。</summary>
    /// <returns>新的延续任务。</returns>
    /// <param name="tasks">继续执行的任务所在的数组。</param>
    /// <param name="continuationAction">在 <paramref name="tasks" /> 数组中的所有任务完成时要执行的操作委托。</param>
    /// <param name="cancellationToken">将指派给新的延续任务的取消标记。</param>
    /// <param name="continuationOptions">枚举值的按位组合，这些枚举值控制新的延续任务的行为。</param>
    /// <param name="scheduler">用于计划新的延续任务的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tasks" /> 数组为 null。- 或 -<paramref name="continuationAction" /> 参数为 null。- 或 -<paramref name="scheduler" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tasks" /> 数组为空或包含 null 值。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWhenAll(Task[] tasks, Action<Task[]> continuationAction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      if (continuationAction == null)
        throw new ArgumentNullException("continuationAction");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Task) TaskFactory<VoidTaskResult>.ContinueWhenAllImpl(tasks, (Func<Task[], VoidTaskResult>) null, continuationAction, continuationOptions, cancellationToken, scheduler, ref stackMark);
    }

    /// <summary>创建一个延续任务，该任务在一组指定的任务完成后开始。</summary>
    /// <returns>新的延续任务。</returns>
    /// <param name="tasks">继续执行的任务所在的数组。</param>
    /// <param name="continuationAction">在 <paramref name="tasks" /> 数组中的所有任务完成时要执行的操作委托。</param>
    /// <typeparam name="TAntecedentResult">以前的 <paramref name="tasks" /> 结果的类型。</typeparam>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <paramref name="tasks" /> 数组中的一个元素已被释放。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tasks" /> 数组为 null。- 或 -<paramref name="continuationAction" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tasks" /> 数组为空或包含 null 值。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>[]> continuationAction)
    {
      if (continuationAction == null)
        throw new ArgumentNullException("continuationAction");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Task) TaskFactory<VoidTaskResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, (Func<Task<TAntecedentResult>[], VoidTaskResult>) null, continuationAction, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>创建一个延续任务，该任务在一组指定的任务完成后开始。</summary>
    /// <returns>新的延续任务。</returns>
    /// <param name="tasks">继续执行的任务所在的数组。</param>
    /// <param name="continuationAction">在 <paramref name="tasks" /> 数组中的所有任务完成时要执行的操作委托。</param>
    /// <param name="cancellationToken">将指派给新的延续任务的取消标记。</param>
    /// <typeparam name="TAntecedentResult">以前的 <paramref name="tasks" /> 结果的类型。</typeparam>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <paramref name="tasks" /> 数组中的一个元素已被释放。- 或 -创建了 <see cref="T:System.Threading.CancellationTokenSource" /> 的 <paramref name="cancellationToken" /> 已经被释放。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tasks" /> 数组为 null。- 或 -<paramref name="continuationAction" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tasks" /> 数组为空或包含 null 值。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>[]> continuationAction, CancellationToken cancellationToken)
    {
      if (continuationAction == null)
        throw new ArgumentNullException("continuationAction");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Task) TaskFactory<VoidTaskResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, (Func<Task<TAntecedentResult>[], VoidTaskResult>) null, continuationAction, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>创建一个延续任务，该任务在一组指定的任务完成后开始。</summary>
    /// <returns>新的延续任务。</returns>
    /// <param name="tasks">继续执行的任务所在的数组。</param>
    /// <param name="continuationAction">在 <paramref name="tasks" /> 数组中的所有任务完成时要执行的操作委托。</param>
    /// <param name="continuationOptions">枚举值的按位组合，这些枚举值控制新的延续任务的行为。NotOn* 和 OnlyOn* 成员不受支持。</param>
    /// <typeparam name="TAntecedentResult">以前的 <paramref name="tasks" /> 结果的类型。</typeparam>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <paramref name="tasks" /> 数组中的一个元素已被释放。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tasks" /> 数组为 null。- 或 -<paramref name="continuationAction" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="continuationOptions" /> 参数指定的值无效。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tasks" /> 数组为空或包含 null 值。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>[]> continuationAction, TaskContinuationOptions continuationOptions)
    {
      if (continuationAction == null)
        throw new ArgumentNullException("continuationAction");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Task) TaskFactory<VoidTaskResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, (Func<Task<TAntecedentResult>[], VoidTaskResult>) null, continuationAction, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>创建一个延续任务，该任务在一组指定的任务完成后开始。</summary>
    /// <returns>新的延续任务。</returns>
    /// <param name="tasks">继续执行的任务所在的数组。</param>
    /// <param name="continuationAction">在 <paramref name="tasks" /> 数组中的所有任务完成时要执行的操作委托。</param>
    /// <param name="cancellationToken">将指派给新的延续任务的取消标记。</param>
    /// <param name="continuationOptions">枚举值的按位组合，这些枚举值控制新的延续任务的行为。NotOn* 和 OnlyOn* 成员不受支持。</param>
    /// <param name="scheduler">用于计划新的延续任务的对象。</param>
    /// <typeparam name="TAntecedentResult">以前的 <paramref name="tasks" /> 结果的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tasks" /> 数组为 null。- 或 -<paramref name="continuationAction" /> 参数为 null。- 或 -<paramref name="scheduler" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tasks" /> 数组为空或包含 null 值。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>[]> continuationAction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      if (continuationAction == null)
        throw new ArgumentNullException("continuationAction");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Task) TaskFactory<VoidTaskResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, (Func<Task<TAntecedentResult>[], VoidTaskResult>) null, continuationAction, continuationOptions, cancellationToken, scheduler, ref stackMark);
    }

    /// <summary>创建一个延续任务，该任务在一组指定的任务完成后开始。</summary>
    /// <returns>新的延续任务。</returns>
    /// <param name="tasks">继续执行的任务所在的数组。</param>
    /// <param name="continuationFunction">在 <paramref name="tasks" /> 数组中的所有任务完成时要异步执行的函数委托。</param>
    /// <typeparam name="TResult">由 <paramref name="continuationFunction" /> 委托返回并与创建的任务关联的结果的类型。</typeparam>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <paramref name="tasks" /> 数组中的一个元素已被释放。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tasks" /> 数组为 null。- 或 -<paramref name="continuationFunction" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tasks" /> 数组为空或包含 null 值。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAll<TResult>(Task[] tasks, Func<Task[], TResult> continuationFunction)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException("continuationFunction");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, (Action<Task[]>) null, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>创建一个延续任务，该任务在一组指定的任务完成后开始。</summary>
    /// <returns>新的延续任务。</returns>
    /// <param name="tasks">继续执行的任务所在的数组。</param>
    /// <param name="continuationFunction">在 <paramref name="tasks" /> 数组中的所有任务完成时要异步执行的函数委托。</param>
    /// <param name="cancellationToken">将指派给新的延续任务的取消标记。</param>
    /// <typeparam name="TResult">由 <paramref name="continuationFunction" /> 委托返回并与创建的任务关联的结果的类型。</typeparam>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <paramref name="tasks" /> 数组中的一个元素已被释放。- 或 -创建了 <see cref="T:System.Threading.CancellationTokenSource" /> 的 <paramref name="cancellationToken" /> 已经被释放。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tasks" /> 数组为 null。- 或 -<paramref name="continuationFunction" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tasks" /> 数组为空或包含 null 值。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAll<TResult>(Task[] tasks, Func<Task[], TResult> continuationFunction, CancellationToken cancellationToken)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException("continuationFunction");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, (Action<Task[]>) null, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>创建一个延续任务，该任务在一组指定的任务完成后开始。</summary>
    /// <returns>新的延续任务。</returns>
    /// <param name="tasks">继续执行的任务所在的数组。</param>
    /// <param name="continuationFunction">在 <paramref name="tasks" /> 数组中的所有任务完成时要异步执行的函数委托。</param>
    /// <param name="continuationOptions">枚举值的按位组合，这些枚举值控制新的延续任务的行为。NotOn* 和 OnlyOn* 成员不受支持。</param>
    /// <typeparam name="TResult">由 <paramref name="continuationFunction" /> 委托返回并与创建的任务关联的结果的类型。</typeparam>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <paramref name="tasks" /> 数组中的一个元素已被释放。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tasks" /> 数组为 null。- 或 -<paramref name="continuationFunction" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="continuationOptions" /> 参数指定的值无效。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tasks" /> 数组为空或包含 null 值。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAll<TResult>(Task[] tasks, Func<Task[], TResult> continuationFunction, TaskContinuationOptions continuationOptions)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException("continuationFunction");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, (Action<Task[]>) null, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>创建一个延续任务，该任务在一组指定的任务完成后开始。</summary>
    /// <returns>新的延续任务。</returns>
    /// <param name="tasks">继续执行的任务所在的数组。</param>
    /// <param name="continuationFunction">在 <paramref name="tasks" /> 数组中的所有任务完成时要异步执行的函数委托。</param>
    /// <param name="cancellationToken">将指派给新的延续任务的取消标记。</param>
    /// <param name="continuationOptions">枚举值的按位组合，这些枚举值控制新的延续任务的行为。NotOn* 和 OnlyOn* 成员不受支持。</param>
    /// <param name="scheduler">用于计划新的延续任务的对象。</param>
    /// <typeparam name="TResult">由 <paramref name="continuationFunction" /> 委托返回并与创建的任务关联的结果的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tasks" /> 数组为 null。- 或 -<paramref name="continuationFunction" /> 参数为 null。- 或 -<paramref name="scheduler" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tasks" /> 数组为空或包含 null 值。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAll<TResult>(Task[] tasks, Func<Task[], TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException("continuationFunction");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, (Action<Task[]>) null, continuationOptions, cancellationToken, scheduler, ref stackMark);
    }

    /// <summary>创建一个延续任务，该任务在一组指定的任务完成后开始。</summary>
    /// <returns>新的延续任务。</returns>
    /// <param name="tasks">继续执行的任务所在的数组。</param>
    /// <param name="continuationFunction">在 <paramref name="tasks" /> 数组中的所有任务完成时要异步执行的函数委托。</param>
    /// <typeparam name="TAntecedentResult">以前的 <paramref name="tasks" /> 结果的类型。</typeparam>
    /// <typeparam name="TResult">由 <paramref name="continuationFunction" /> 委托返回并与创建的任务关联的结果的类型。</typeparam>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <paramref name="tasks" /> 数组中的一个元素已被释放。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tasks" /> 数组为 null。- 或 -<paramref name="continuationFunction" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tasks" /> 数组为空或包含 null 值。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAll<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException("continuationFunction");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, (Action<Task<TAntecedentResult>[]>) null, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>创建一个延续任务，该任务在一组指定的任务完成后开始。</summary>
    /// <returns>新的延续任务。</returns>
    /// <param name="tasks">继续执行的任务所在的数组。</param>
    /// <param name="continuationFunction">在 <paramref name="tasks" /> 数组中的所有任务完成时要异步执行的函数委托。</param>
    /// <param name="cancellationToken">将指派给新的延续任务的取消标记。</param>
    /// <typeparam name="TAntecedentResult">以前的 <paramref name="tasks" /> 结果的类型。</typeparam>
    /// <typeparam name="TResult">由 <paramref name="continuationFunction" /> 委托返回并与创建的任务关联的结果的类型。</typeparam>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <paramref name="tasks" /> 数组中的一个元素已被释放。- 或 -<see cref="T:System.Threading.CancellationTokenSource" /> 创建<paramref name=" cancellationToken" /> 已释放。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tasks" /> 数组为 null。- 或 -<paramref name="continuationFunction" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tasks" /> 数组为空或包含 null 值。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAll<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, CancellationToken cancellationToken)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException("continuationFunction");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, (Action<Task<TAntecedentResult>[]>) null, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>创建一个延续任务，该任务在一组指定的任务完成后开始。</summary>
    /// <returns>新的延续任务。</returns>
    /// <param name="tasks">继续执行的任务所在的数组。</param>
    /// <param name="continuationFunction">在 <paramref name="tasks" /> 数组中的所有任务完成时要异步执行的函数委托。</param>
    /// <param name="continuationOptions">枚举值的按位组合，这些枚举值控制新的延续任务的行为。NotOn* 和 OnlyOn* 成员不受支持。</param>
    /// <typeparam name="TAntecedentResult">以前的 <paramref name="tasks" /> 结果的类型。</typeparam>
    /// <typeparam name="TResult">由 <paramref name="continuationFunction" /> 委托返回并与创建的任务关联的结果的类型。</typeparam>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <paramref name="tasks" /> 数组中的一个元素已被释放。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tasks" /> 数组为 null。- 或 -<paramref name="continuationFunction" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="continuationOptions" /> 参数指定的值无效。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tasks" /> 数组为空或包含 null 值。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAll<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, TaskContinuationOptions continuationOptions)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException("continuationFunction");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, (Action<Task<TAntecedentResult>[]>) null, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>创建一个延续任务，该任务在一组指定的任务完成后开始。</summary>
    /// <returns>新的延续任务。</returns>
    /// <param name="tasks">继续执行的任务所在的数组。</param>
    /// <param name="continuationFunction">在 <paramref name="tasks" /> 数组中的所有任务完成时要异步执行的函数委托。</param>
    /// <param name="cancellationToken">将指派给新的延续任务的取消标记。</param>
    /// <param name="continuationOptions">枚举值的按位组合，这些枚举值控制新的延续任务的行为。NotOn* 和 OnlyOn* 成员不受支持。</param>
    /// <param name="scheduler">用于计划新的延续任务的对象。</param>
    /// <typeparam name="TAntecedentResult">以前的 <paramref name="tasks" /> 结果的类型。</typeparam>
    /// <typeparam name="TResult">由 <paramref name="continuationFunction" /> 委托返回并与创建的任务关联的结果的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tasks" /> 数组为 null。- 或 -<paramref name="continuationFunction" /> 参数为 null。- 或 -<paramref name="scheduler" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tasks" /> 数组为空或包含 null 值。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="continuationOptions" /> 参数指定的值无效。</exception>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <paramref name="tasks" /> 数组中的一个元素已被释放。- 或 -创建了 <see cref="T:System.Threading.CancellationTokenSource" /> 的 <paramref name="cancellationToken" /> 已经被释放。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAll<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException("continuationFunction");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, (Action<Task<TAntecedentResult>[]>) null, continuationOptions, cancellationToken, scheduler, ref stackMark);
    }

    internal static Task<Task> CommonCWAnyLogic(IList<Task> tasks)
    {
      TaskFactory.CompleteOnInvokePromise completeOnInvokePromise = new TaskFactory.CompleteOnInvokePromise(tasks);
      bool flag = false;
      int count = tasks.Count;
      for (int index = 0; index < count; ++index)
      {
        Task completingTask = tasks[index];
        if (completingTask == null)
          throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_NullTask"), "tasks");
        if (!flag)
        {
          if (completeOnInvokePromise.IsCompleted)
            flag = true;
          else if (completingTask.IsCompleted)
          {
            completeOnInvokePromise.Invoke(completingTask);
            flag = true;
          }
          else
            completingTask.AddCompletionAction((ITaskCompletionAction) completeOnInvokePromise);
        }
      }
      return (Task<Task>) completeOnInvokePromise;
    }

    /// <summary>创建一个延续 <see cref="T:System.Threading.Tasks.Task" />，它将在提供的组中的任何任务完成后马上开始。</summary>
    /// <returns>新的延续 <see cref="T:System.Threading.Tasks.Task" />。</returns>
    /// <param name="tasks">在一个任务完成时继续执行的任务所在的数组。</param>
    /// <param name="continuationAction">在 <paramref name="tasks" /> 数组中的一个任务完成时要执行的操作委托。</param>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <paramref name="tasks" /> 数组中的某个元素已被释放。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tasks" /> 数组为 null。- 或 -<paramref name="continuationAction" /> 参数是 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tasks" /> 数组包含 null 值。- 或 -<paramref name="tasks" /> 数组为空。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWhenAny(Task[] tasks, Action<Task> continuationAction)
    {
      if (continuationAction == null)
        throw new ArgumentNullException("continuationAction");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Task) TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl(tasks, (Func<Task, VoidTaskResult>) null, continuationAction, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>创建一个延续 <see cref="T:System.Threading.Tasks.Task" />，它将在提供的组中的任何任务完成后马上开始。</summary>
    /// <returns>新的延续 <see cref="T:System.Threading.Tasks.Task" />。</returns>
    /// <param name="tasks">在一个任务完成时继续执行的任务所在的数组。</param>
    /// <param name="continuationAction">在 <paramref name="tasks" /> 数组中的一个任务完成时要执行的操作委托。</param>
    /// <param name="cancellationToken">将指派给新的延续任务的 <see cref="T:System.Threading.CancellationToken" />。</param>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <paramref name="tasks" /> 数组中的某个元素已被释放。- 或 -<paramref name="cancellationToken" /> 已释放。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tasks" /> 数组为 null。- 或 -<paramref name="continuationAction" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tasks" /> 数组包含 null 值。- 或 -<paramref name="tasks" /> 数组为空。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWhenAny(Task[] tasks, Action<Task> continuationAction, CancellationToken cancellationToken)
    {
      if (continuationAction == null)
        throw new ArgumentNullException("continuationAction");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Task) TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl(tasks, (Func<Task, VoidTaskResult>) null, continuationAction, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>创建一个延续 <see cref="T:System.Threading.Tasks.Task" />，它将在提供的组中的任何任务完成后马上开始。</summary>
    /// <returns>新的延续 <see cref="T:System.Threading.Tasks.Task" />。</returns>
    /// <param name="tasks">在一个任务完成时继续执行的任务所在的数组。</param>
    /// <param name="continuationAction">在 <paramref name="tasks" /> 数组中的一个任务完成时要执行的操作委托。</param>
    /// <param name="continuationOptions">
    /// <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> 值，用于控制所创建的延续 <see cref="T:System.Threading.Tasks.Task" /> 的行为。</param>
    /// <exception cref="T:System.ObjectDisposedException">在 <paramref name="tasks" /> 数组中的元素之一已经被释放时引发的异常。</exception>
    /// <exception cref="T:System.ArgumentNullException">当 <paramref name="tasks" /> 数组为 null 时引发的异常。- 或 -当 <paramref name="continuationAction" /> 参数为 null 时引发的异常。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">当 <paramref name="continuationOptions" /> 参数指定无效 TaskContinuationOptions 值时引发的异常。</exception>
    /// <exception cref="T:System.ArgumentException">当 <paramref name="tasks" /> 数组包含 null 值时引发的异常。- 或 -当 <paramref name="tasks" /> 数组为空时引发的异常。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWhenAny(Task[] tasks, Action<Task> continuationAction, TaskContinuationOptions continuationOptions)
    {
      if (continuationAction == null)
        throw new ArgumentNullException("continuationAction");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Task) TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl(tasks, (Func<Task, VoidTaskResult>) null, continuationAction, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>创建一个延续 <see cref="T:System.Threading.Tasks.Task" />，它将在提供的组中的任何任务完成后马上开始。</summary>
    /// <returns>新的延续 <see cref="T:System.Threading.Tasks.Task" />。</returns>
    /// <param name="tasks">在一个任务完成时继续执行的任务所在的数组。</param>
    /// <param name="continuationAction">在 <paramref name="tasks" /> 数组中的一个任务完成时要执行的操作委托。</param>
    /// <param name="cancellationToken">将指派给新的延续任务的 <see cref="T:System.Threading.CancellationToken" />。</param>
    /// <param name="continuationOptions">
    /// <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> 值，用于控制所创建的延续 <see cref="T:System.Threading.Tasks.Task" /> 的行为。</param>
    /// <param name="scheduler">用于计划所创建的延续 <see cref="T:System.Threading.Tasks.TaskScheduler" /> 的 <see cref="T:System.Threading.Tasks.Task" />。</param>
    /// <exception cref="T:System.ArgumentNullException">当 <paramref name="tasks" /> 数组为 null 时引发的异常。- 或 -当 <paramref name="continuationAction" /> 参数为 null 时引发的异常。- 或 -当 <paramref name="scheduler" /> 参数为 null 时引发的异常。</exception>
    /// <exception cref="T:System.ArgumentException">当 <paramref name="tasks" /> 数组包含 null 值时引发的异常。- 或 -当 <paramref name="tasks" /> 数组为空时引发的异常。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWhenAny(Task[] tasks, Action<Task> continuationAction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      if (continuationAction == null)
        throw new ArgumentNullException("continuationAction");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Task) TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl(tasks, (Func<Task, VoidTaskResult>) null, continuationAction, continuationOptions, cancellationToken, scheduler, ref stackMark);
    }

    /// <summary>创建一个延续 <see cref="T:System.Threading.Tasks.Task`1" />，它将在提供的组中的任何任务完成后马上开始。</summary>
    /// <returns>新的延续 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    /// <param name="tasks">在一个任务完成时继续执行的任务所在的数组。</param>
    /// <param name="continuationFunction">在 <paramref name="tasks" /> 数组中的一个任务完成时要异步执行的函数委托。</param>
    /// <typeparam name="TResult">由 <paramref name="continuationFunction" /> 委托返回并与创建的 <see cref="T:System.Threading.Tasks.Task`1" /> 关联的结果的类型。</typeparam>
    /// <exception cref="T:System.ObjectDisposedException">在 <paramref name="tasks" /> 数组中的元素之一已经被释放时引发的异常。</exception>
    /// <exception cref="T:System.ArgumentNullException">当 <paramref name="tasks" /> 数组为 null 时引发的异常。- 或 -当 <paramref name="continuationFunction" /> 参数为 null 时引发的异常。</exception>
    /// <exception cref="T:System.ArgumentException">当 <paramref name="tasks" /> 数组包含 null 值时引发的异常。- 或 -当 <paramref name="tasks" /> 数组为空时引发的异常。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAny<TResult>(Task[] tasks, Func<Task, TResult> continuationFunction)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException("continuationFunction");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, (Action<Task>) null, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>创建一个延续 <see cref="T:System.Threading.Tasks.Task`1" />，它将在提供的组中的任何任务完成后马上开始。</summary>
    /// <returns>新的延续 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    /// <param name="tasks">在一个任务完成时继续执行的任务所在的数组。</param>
    /// <param name="continuationFunction">在 <paramref name="tasks" /> 数组中的一个任务完成时要异步执行的函数委托。</param>
    /// <param name="cancellationToken">将指派给新的延续任务的 <see cref="T:System.Threading.CancellationToken" />。</param>
    /// <typeparam name="TResult">由 <paramref name="continuationFunction" /> 委托返回并与创建的 <see cref="T:System.Threading.Tasks.Task`1" /> 关联的结果的类型。</typeparam>
    /// <exception cref="T:System.ObjectDisposedException">在 <paramref name="tasks" /> 数组中的元素之一已经被释放时引发的异常。- 或 -提供的 <see cref="T:System.Threading.CancellationToken" /> 已被释放。</exception>
    /// <exception cref="T:System.ArgumentNullException">当 <paramref name="tasks" /> 数组为 null 时引发的异常。- 或 -当 <paramref name="continuationFunction" /> 参数为 null 时引发的异常。</exception>
    /// <exception cref="T:System.ArgumentException">当 <paramref name="tasks" /> 数组包含 null 值时引发的异常。- 或 -当 <paramref name="tasks" /> 数组为空时引发的异常。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAny<TResult>(Task[] tasks, Func<Task, TResult> continuationFunction, CancellationToken cancellationToken)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException("continuationFunction");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, (Action<Task>) null, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>创建一个延续 <see cref="T:System.Threading.Tasks.Task`1" />，它将在提供的组中的任何任务完成后马上开始。</summary>
    /// <returns>新的延续 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    /// <param name="tasks">在一个任务完成时继续执行的任务所在的数组。</param>
    /// <param name="continuationFunction">在 <paramref name="tasks" /> 数组中的一个任务完成时要异步执行的函数委托。</param>
    /// <param name="continuationOptions">
    /// <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> 值，用于控制所创建的延续 <see cref="T:System.Threading.Tasks.Task`1" /> 的行为。</param>
    /// <typeparam name="TResult">由 <paramref name="continuationFunction" /> 委托返回并与创建的 <see cref="T:System.Threading.Tasks.Task`1" /> 关联的结果的类型。</typeparam>
    /// <exception cref="T:System.ObjectDisposedException">在 <paramref name="tasks" /> 数组中的元素之一已经被释放时引发的异常。</exception>
    /// <exception cref="T:System.ArgumentNullException">当 <paramref name="tasks" /> 数组为 null 时引发的异常。- 或 -当 <paramref name="continuationFunction" /> 参数为 null 时引发的异常。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">当 <paramref name="continuationOptions" /> 参数指定无效 TaskContinuationOptions 值时引发的异常。</exception>
    /// <exception cref="T:System.ArgumentException">当 <paramref name="tasks" /> 数组包含 null 值时引发的异常。- 或 -当 <paramref name="tasks" /> 数组为空时引发的异常。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAny<TResult>(Task[] tasks, Func<Task, TResult> continuationFunction, TaskContinuationOptions continuationOptions)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException("continuationFunction");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, (Action<Task>) null, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>创建一个延续 <see cref="T:System.Threading.Tasks.Task`1" />，它将在提供的组中的任何任务完成后马上开始。</summary>
    /// <returns>新的延续 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    /// <param name="tasks">在一个任务完成时继续执行的任务所在的数组。</param>
    /// <param name="continuationFunction">在 <paramref name="tasks" /> 数组中的一个任务完成时要异步执行的函数委托。</param>
    /// <param name="cancellationToken">将指派给新的延续任务的 <see cref="T:System.Threading.CancellationToken" />。</param>
    /// <param name="continuationOptions">
    /// <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> 值，用于控制所创建的延续 <see cref="T:System.Threading.Tasks.Task`1" /> 的行为。</param>
    /// <param name="scheduler">用于计划所创建的延续 <see cref="T:System.Threading.Tasks.TaskScheduler" /> 的 <see cref="T:System.Threading.Tasks.Task`1" />。</param>
    /// <typeparam name="TResult">由 <paramref name="continuationFunction" /> 委托返回并与创建的 <see cref="T:System.Threading.Tasks.Task`1" /> 关联的结果的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">当 <paramref name="tasks" /> 数组为 null 时引发的异常。- 或 -当 <paramref name="continuationFunction" /> 参数为 null 时引发的异常。- 或 -当 <paramref name="scheduler" /> 参数为 null 时引发的异常。</exception>
    /// <exception cref="T:System.ArgumentException">当 <paramref name="tasks" /> 数组包含 null 值时引发的异常。- 或 -当 <paramref name="tasks" /> 数组为空时引发的异常。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAny<TResult>(Task[] tasks, Func<Task, TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException("continuationFunction");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, (Action<Task>) null, continuationOptions, cancellationToken, scheduler, ref stackMark);
    }

    /// <summary>创建一个延续 <see cref="T:System.Threading.Tasks.Task`1" />，它将在提供的组中的任何任务完成后马上开始。</summary>
    /// <returns>新的延续 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    /// <param name="tasks">在一个任务完成时继续执行的任务所在的数组。</param>
    /// <param name="continuationFunction">在 <paramref name="tasks" /> 数组中的一个任务完成时要异步执行的函数委托。</param>
    /// <typeparam name="TAntecedentResult">以前的 <paramref name="tasks" /> 结果的类型。</typeparam>
    /// <typeparam name="TResult">由 <paramref name="continuationFunction" /> 委托返回并与创建的 <see cref="T:System.Threading.Tasks.Task`1" /> 关联的结果的类型。</typeparam>
    /// <exception cref="T:System.ObjectDisposedException">在 <paramref name="tasks" /> 数组中的元素之一已经被释放时引发的异常。</exception>
    /// <exception cref="T:System.ArgumentNullException">当 <paramref name="tasks" /> 数组为 null 时引发的异常。- 或 -当 <paramref name="continuationFunction" /> 参数为 null 时引发的异常。</exception>
    /// <exception cref="T:System.ArgumentException">当 <paramref name="tasks" /> 数组包含 null 值时引发的异常。- 或 -当 <paramref name="tasks" /> 数组为空时引发的异常。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAny<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      if (continuationFunction == null)
        throw new ArgumentNullException("continuationFunction");
      return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, (Action<Task<TAntecedentResult>>) null, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>创建一个延续 <see cref="T:System.Threading.Tasks.Task`1" />，它将在提供的组中的任何任务完成后马上开始。</summary>
    /// <returns>新的延续 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    /// <param name="tasks">在一个任务完成时继续执行的任务所在的数组。</param>
    /// <param name="continuationFunction">在 <paramref name="tasks" /> 数组中的一个任务完成时要异步执行的函数委托。</param>
    /// <param name="cancellationToken">将指派给新的延续任务的 <see cref="T:System.Threading.CancellationToken" />。</param>
    /// <typeparam name="TAntecedentResult">以前的 <paramref name="tasks" /> 结果的类型。</typeparam>
    /// <typeparam name="TResult">由 <paramref name="continuationFunction" /> 委托返回并与创建的 <see cref="T:System.Threading.Tasks.Task`1" /> 关联的结果的类型。</typeparam>
    /// <exception cref="T:System.ObjectDisposedException">在 <paramref name="tasks" /> 数组中的元素之一已经被释放时引发的异常。- 或 -提供的 <see cref="T:System.Threading.CancellationToken" /> 已被释放。</exception>
    /// <exception cref="T:System.ArgumentNullException">当 <paramref name="tasks" /> 数组为 null 时引发的异常。- 或 -当 <paramref name="continuationFunction" /> 参数为 null 时引发的异常。</exception>
    /// <exception cref="T:System.ArgumentException">当 <paramref name="tasks" /> 数组包含 null 值时引发的异常。- 或 -当 <paramref name="tasks" /> 数组为空时引发的异常。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAny<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, CancellationToken cancellationToken)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException("continuationFunction");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, (Action<Task<TAntecedentResult>>) null, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>创建一个延续 <see cref="T:System.Threading.Tasks.Task`1" />，它将在提供的组中的任何任务完成后马上开始。</summary>
    /// <returns>新的延续 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    /// <param name="tasks">在一个任务完成时继续执行的任务所在的数组。</param>
    /// <param name="continuationFunction">在 <paramref name="tasks" /> 数组中的一个任务完成时要异步执行的函数委托。</param>
    /// <param name="continuationOptions">
    /// <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> 值，用于控制所创建的延续 <see cref="T:System.Threading.Tasks.Task`1" /> 的行为。</param>
    /// <typeparam name="TAntecedentResult">以前的 <paramref name="tasks" /> 结果的类型。</typeparam>
    /// <typeparam name="TResult">由 <paramref name="continuationFunction" /> 委托返回并与创建的 <see cref="T:System.Threading.Tasks.Task`1" /> 关联的结果的类型。</typeparam>
    /// <exception cref="T:System.ObjectDisposedException">在 <paramref name="tasks" /> 数组中的元素之一已经被释放时引发的异常。</exception>
    /// <exception cref="T:System.ArgumentNullException">当 <paramref name="tasks" /> 数组为 null 时引发的异常。- 或 -当 <paramref name="continuationFunction" /> 参数为 null 时引发的异常。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">当 <paramref name="continuationOptions" /> 参数指定无效 TaskContinuationOptions 值时引发的异常。</exception>
    /// <exception cref="T:System.ArgumentException">当 <paramref name="tasks" /> 数组包含 null 值时引发的异常。- 或 -当 <paramref name="tasks" /> 数组为空时引发的异常。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAny<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, TaskContinuationOptions continuationOptions)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException("continuationFunction");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, (Action<Task<TAntecedentResult>>) null, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>创建一个延续 <see cref="T:System.Threading.Tasks.Task`1" />，它将在提供的组中的任何任务完成后马上开始。</summary>
    /// <returns>新的延续 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    /// <param name="tasks">在一个任务完成时继续执行的任务所在的数组。</param>
    /// <param name="continuationFunction">在 <paramref name="tasks" /> 数组中的一个任务完成时要异步执行的函数委托。</param>
    /// <param name="cancellationToken">将指派给新的延续任务的 <see cref="T:System.Threading.CancellationToken" />。</param>
    /// <param name="continuationOptions">
    /// <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> 值，用于控制所创建的延续 <see cref="T:System.Threading.Tasks.Task`1" /> 的行为。</param>
    /// <param name="scheduler">用于计划所创建的延续 <see cref="T:System.Threading.Tasks.TaskScheduler" /> 的 <see cref="T:System.Threading.Tasks.Task`1" />。</param>
    /// <typeparam name="TAntecedentResult">以前的 <paramref name="tasks" /> 结果的类型。</typeparam>
    /// <typeparam name="TResult">由 <paramref name="continuationFunction" /> 委托返回并与创建的 <see cref="T:System.Threading.Tasks.Task`1" /> 关联的结果的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">当 <paramref name="tasks" /> 数组为 null 时引发的异常。- 或 -当 <paramref name="continuationFunction" /> 参数为 null 时引发的异常。- 或 -当 <paramref name="scheduler" /> 参数为 null 时引发的异常。</exception>
    /// <exception cref="T:System.ArgumentException">当 <paramref name="tasks" /> 数组包含 null 值时引发的异常。- 或 -当 <paramref name="tasks" /> 数组为空时引发的异常。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAny<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException("continuationFunction");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, (Action<Task<TAntecedentResult>>) null, continuationOptions, cancellationToken, scheduler, ref stackMark);
    }

    /// <summary>创建一个延续 <see cref="T:System.Threading.Tasks.Task" />，它将在提供的组中的任何任务完成后马上开始。</summary>
    /// <returns>新的延续 <see cref="T:System.Threading.Tasks.Task" />。</returns>
    /// <param name="tasks">在一个任务完成时继续执行的任务所在的数组。</param>
    /// <param name="continuationAction">在 <paramref name="tasks" /> 数组中的一个任务完成时要执行的操作委托。</param>
    /// <typeparam name="TAntecedentResult">以前的 <paramref name="tasks" /> 结果的类型。</typeparam>
    /// <exception cref="T:System.ObjectDisposedException">在 <paramref name="tasks" /> 数组中的元素之一已经被释放时引发的异常。</exception>
    /// <exception cref="T:System.ArgumentNullException">当 <paramref name="tasks" /> 数组为 null 时引发的异常。- 或 -当 <paramref name="continuationAction" /> 参数为 null 时引发的异常。</exception>
    /// <exception cref="T:System.ArgumentException">当 <paramref name="tasks" /> 数组包含 null 值时引发的异常。- 或 -当 <paramref name="tasks" /> 数组为空时引发的异常。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>> continuationAction)
    {
      if (continuationAction == null)
        throw new ArgumentNullException("continuationAction");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Task) TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, (Func<Task<TAntecedentResult>, VoidTaskResult>) null, continuationAction, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>创建一个延续 <see cref="T:System.Threading.Tasks.Task" />，它将在提供的组中的任何任务完成后马上开始。</summary>
    /// <returns>新的延续 <see cref="T:System.Threading.Tasks.Task" />。</returns>
    /// <param name="tasks">在一个任务完成时继续执行的任务所在的数组。</param>
    /// <param name="continuationAction">在 <paramref name="tasks" /> 数组中的一个任务完成时要执行的操作委托。</param>
    /// <param name="cancellationToken">将指派给新的延续任务的 <see cref="T:System.Threading.CancellationToken" />。</param>
    /// <typeparam name="TAntecedentResult">以前的 <paramref name="tasks" /> 结果的类型。</typeparam>
    /// <exception cref="T:System.ObjectDisposedException">在 <paramref name="tasks" /> 数组中的元素之一已经被释放时引发的异常。- 或 -提供的 <see cref="T:System.Threading.CancellationToken" /> 已被释放。</exception>
    /// <exception cref="T:System.ArgumentNullException">当 <paramref name="tasks" /> 数组为 null 时引发的异常。- 或 -当 <paramref name="continuationAction" /> 参数为 null 时引发的异常。</exception>
    /// <exception cref="T:System.ArgumentException">当 <paramref name="tasks" /> 数组包含 null 值时引发的异常。- 或 -当 <paramref name="tasks" /> 数组为空时引发的异常。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>> continuationAction, CancellationToken cancellationToken)
    {
      if (continuationAction == null)
        throw new ArgumentNullException("continuationAction");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Task) TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, (Func<Task<TAntecedentResult>, VoidTaskResult>) null, continuationAction, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>创建一个延续 <see cref="T:System.Threading.Tasks.Task" />，它将在提供的组中的任何任务完成后马上开始。</summary>
    /// <returns>新的延续 <see cref="T:System.Threading.Tasks.Task" />。</returns>
    /// <param name="tasks">在一个任务完成时继续执行的任务所在的数组。</param>
    /// <param name="continuationAction">在 <paramref name="tasks" /> 数组中的一个任务完成时要执行的操作委托。</param>
    /// <param name="continuationOptions">
    /// <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> 值，用于控制所创建的延续 <see cref="T:System.Threading.Tasks.Task" /> 的行为。</param>
    /// <typeparam name="TAntecedentResult">以前的 <paramref name="tasks" /> 结果的类型。</typeparam>
    /// <exception cref="T:System.ObjectDisposedException">在 <paramref name="tasks" /> 数组中的元素之一已经被释放时引发的异常。</exception>
    /// <exception cref="T:System.ArgumentNullException">当 <paramref name="tasks" /> 数组为 null 时引发的异常。- 或 -当 <paramref name="continuationAction" /> 参数为 null 时引发的异常。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">当 <paramref name="continuationOptions" /> 参数指定无效 TaskContinuationOptions 值时引发的异常。</exception>
    /// <exception cref="T:System.ArgumentException">当 <paramref name="tasks" /> 数组包含 null 值时引发的异常。- 或 -当 <paramref name="tasks" /> 数组为空时引发的异常。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>> continuationAction, TaskContinuationOptions continuationOptions)
    {
      if (continuationAction == null)
        throw new ArgumentNullException("continuationAction");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Task) TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, (Func<Task<TAntecedentResult>, VoidTaskResult>) null, continuationAction, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>创建一个延续 <see cref="T:System.Threading.Tasks.Task" />，它将在提供的组中的任何任务完成后马上开始。</summary>
    /// <returns>新的延续 <see cref="T:System.Threading.Tasks.Task" />。</returns>
    /// <param name="tasks">在一个任务完成时继续执行的任务所在的数组。</param>
    /// <param name="continuationAction">在 <paramref name="tasks" /> 数组中的一个任务完成时要执行的操作委托。</param>
    /// <param name="cancellationToken">将指派给新的延续任务的 <see cref="T:System.Threading.CancellationToken" />。</param>
    /// <param name="continuationOptions">
    /// <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> 值，用于控制所创建的延续 <see cref="T:System.Threading.Tasks.Task" /> 的行为。</param>
    /// <param name="scheduler">用于计划所创建的延续 <see cref="T:System.Threading.Tasks.TaskScheduler" /> 的 <see cref="T:System.Threading.Tasks.Task`1" />。</param>
    /// <typeparam name="TAntecedentResult">以前的 <paramref name="tasks" /> 结果的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">当 <paramref name="tasks" /> 数组为 null 时引发的异常。- 或 -当 <paramref name="continuationAction" /> 参数为 null 时引发的异常。- 或 -当 <paramref name="scheduler" /> 参数为 null 时引发的异常。</exception>
    /// <exception cref="T:System.ArgumentException">当 <paramref name="tasks" /> 数组包含 null 值时引发的异常。- 或 -当 <paramref name="tasks" /> 数组为空时引发的异常。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>> continuationAction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      if (continuationAction == null)
        throw new ArgumentNullException("continuationAction");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Task) TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, (Func<Task<TAntecedentResult>, VoidTaskResult>) null, continuationAction, continuationOptions, cancellationToken, scheduler, ref stackMark);
    }

    internal static Task[] CheckMultiContinuationTasksAndCopy(Task[] tasks)
    {
      if (tasks == null)
        throw new ArgumentNullException("tasks");
      if (tasks.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_EmptyTaskList"), "tasks");
      Task[] taskArray = new Task[tasks.Length];
      for (int index = 0; index < tasks.Length; ++index)
      {
        taskArray[index] = tasks[index];
        if (taskArray[index] == null)
          throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_NullTask"), "tasks");
      }
      return taskArray;
    }

    internal static Task<TResult>[] CheckMultiContinuationTasksAndCopy<TResult>(Task<TResult>[] tasks)
    {
      if (tasks == null)
        throw new ArgumentNullException("tasks");
      if (tasks.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_EmptyTaskList"), "tasks");
      Task<TResult>[] taskArray = new Task<TResult>[tasks.Length];
      for (int index = 0; index < tasks.Length; ++index)
      {
        taskArray[index] = tasks[index];
        if (taskArray[index] == null)
          throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_NullTask"), "tasks");
      }
      return taskArray;
    }

    internal static void CheckMultiTaskContinuationOptions(TaskContinuationOptions continuationOptions)
    {
      if ((continuationOptions & (TaskContinuationOptions.LongRunning | TaskContinuationOptions.ExecuteSynchronously)) == (TaskContinuationOptions.LongRunning | TaskContinuationOptions.ExecuteSynchronously))
        throw new ArgumentOutOfRangeException("continuationOptions", Environment.GetResourceString("Task_ContinueWith_ESandLR"));
      if ((continuationOptions & ~(TaskContinuationOptions.OnlyOnRanToCompletion | TaskContinuationOptions.PreferFairness | TaskContinuationOptions.LongRunning | TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.DenyChildAttach | TaskContinuationOptions.HideScheduler | TaskContinuationOptions.LazyCancellation | TaskContinuationOptions.NotOnRanToCompletion | TaskContinuationOptions.ExecuteSynchronously)) != TaskContinuationOptions.None)
        throw new ArgumentOutOfRangeException("continuationOptions");
      if ((continuationOptions & (TaskContinuationOptions.OnlyOnRanToCompletion | TaskContinuationOptions.NotOnRanToCompletion)) != TaskContinuationOptions.None)
        throw new ArgumentOutOfRangeException("continuationOptions", Environment.GetResourceString("Task_MultiTaskContinuation_FireOptions"));
    }

    private sealed class CompleteOnCountdownPromise : Task<Task[]>, ITaskCompletionAction
    {
      private readonly Task[] _tasks;
      private int _count;

      internal override bool ShouldNotifyDebuggerOfWaitCompletion
      {
        get
        {
          if (base.ShouldNotifyDebuggerOfWaitCompletion)
            return Task.AnyTaskRequiresNotifyDebuggerOfWaitCompletion(this._tasks);
          return false;
        }
      }

      internal CompleteOnCountdownPromise(Task[] tasksCopy)
      {
        this._tasks = tasksCopy;
        this._count = tasksCopy.Length;
        if (AsyncCausalityTracer.LoggingOn)
          AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, this.Id, "TaskFactory.ContinueWhenAll", 0UL);
        if (!Task.s_asyncDebuggingEnabled)
          return;
        Task.AddToActiveTasks((Task) this);
      }

      public void Invoke(Task completingTask)
      {
        if (AsyncCausalityTracer.LoggingOn)
          AsyncCausalityTracer.TraceOperationRelation(CausalityTraceLevel.Important, this.Id, CausalityRelation.Join);
        if (completingTask.IsWaitNotificationEnabled)
          this.SetNotificationForWaitCompletion(true);
        if (Interlocked.Decrement(ref this._count) != 0)
          return;
        if (AsyncCausalityTracer.LoggingOn)
          AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, this.Id, AsyncCausalityStatus.Completed);
        if (Task.s_asyncDebuggingEnabled)
          Task.RemoveFromActiveTasks(this.Id);
        this.TrySetResult(this._tasks);
      }
    }

    private sealed class CompleteOnCountdownPromise<T> : Task<Task<T>[]>, ITaskCompletionAction
    {
      private readonly Task<T>[] _tasks;
      private int _count;

      internal override bool ShouldNotifyDebuggerOfWaitCompletion
      {
        get
        {
          if (base.ShouldNotifyDebuggerOfWaitCompletion)
            return Task.AnyTaskRequiresNotifyDebuggerOfWaitCompletion((Task[]) this._tasks);
          return false;
        }
      }

      internal CompleteOnCountdownPromise(Task<T>[] tasksCopy)
      {
        this._tasks = tasksCopy;
        this._count = tasksCopy.Length;
        if (AsyncCausalityTracer.LoggingOn)
          AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, this.Id, "TaskFactory.ContinueWhenAll<>", 0UL);
        if (!Task.s_asyncDebuggingEnabled)
          return;
        Task.AddToActiveTasks((Task) this);
      }

      public void Invoke(Task completingTask)
      {
        if (AsyncCausalityTracer.LoggingOn)
          AsyncCausalityTracer.TraceOperationRelation(CausalityTraceLevel.Important, this.Id, CausalityRelation.Join);
        if (completingTask.IsWaitNotificationEnabled)
          this.SetNotificationForWaitCompletion(true);
        if (Interlocked.Decrement(ref this._count) != 0)
          return;
        if (AsyncCausalityTracer.LoggingOn)
          AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, this.Id, AsyncCausalityStatus.Completed);
        if (Task.s_asyncDebuggingEnabled)
          Task.RemoveFromActiveTasks(this.Id);
        this.TrySetResult(this._tasks);
      }
    }

    internal sealed class CompleteOnInvokePromise : Task<Task>, ITaskCompletionAction
    {
      private IList<Task> _tasks;
      private int m_firstTaskAlreadyCompleted;

      public CompleteOnInvokePromise(IList<Task> tasks)
      {
        this._tasks = tasks;
        if (AsyncCausalityTracer.LoggingOn)
          AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, this.Id, "TaskFactory.ContinueWhenAny", 0UL);
        if (!Task.s_asyncDebuggingEnabled)
          return;
        Task.AddToActiveTasks((Task) this);
      }

      public void Invoke(Task completingTask)
      {
        if (Interlocked.CompareExchange(ref this.m_firstTaskAlreadyCompleted, 1, 0) != 0)
          return;
        if (AsyncCausalityTracer.LoggingOn)
        {
          AsyncCausalityTracer.TraceOperationRelation(CausalityTraceLevel.Important, this.Id, CausalityRelation.Choice);
          AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, this.Id, AsyncCausalityStatus.Completed);
        }
        if (Task.s_asyncDebuggingEnabled)
          Task.RemoveFromActiveTasks(this.Id);
        this.TrySetResult(completingTask);
        IList<Task> taskList = this._tasks;
        int count = taskList.Count;
        for (int index = 0; index < count; ++index)
        {
          Task task = taskList[index];
          if (task != null && !task.IsCompleted)
            task.RemoveContinuation((object) this);
        }
        this._tasks = (IList<Task>) null;
      }
    }
  }
}
