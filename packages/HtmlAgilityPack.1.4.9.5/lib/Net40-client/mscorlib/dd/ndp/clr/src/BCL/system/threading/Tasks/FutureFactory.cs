// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.TaskFactory`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Security.Permissions;

namespace System.Threading.Tasks
{
  /// <summary>提供对创建和计划 <see cref="T:System.Threading.Tasks.Task`1" /> 对象的支持。</summary>
  /// <typeparam name="TResult">此类的方法创建的 <see cref="T:System.Threading.Tasks.Task`1" /> 对象的返回值。</typeparam>
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public class TaskFactory<TResult>
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
    /// <returns>此任务工厂的默认取消标记。</returns>
    [__DynamicallyInvokable]
    public CancellationToken CancellationToken
    {
      [__DynamicallyInvokable] get
      {
        return this.m_defaultCancellationToken;
      }
    }

    /// <summary>获取此任务工厂的任务计划程序。</summary>
    /// <returns>此任务工厂的任务计划程序。</returns>
    [__DynamicallyInvokable]
    public TaskScheduler Scheduler
    {
      [__DynamicallyInvokable] get
      {
        return this.m_defaultScheduler;
      }
    }

    /// <summary>获取此任务工厂的 <see cref="T:System.Threading.Tasks.TaskCreationOptions" /> 枚举值。</summary>
    /// <returns>指定此任务工厂的默认创建选项的枚举值之一。</returns>
    [__DynamicallyInvokable]
    public TaskCreationOptions CreationOptions
    {
      [__DynamicallyInvokable] get
      {
        return this.m_defaultCreationOptions;
      }
    }

    /// <summary>获取此任务工厂的 <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> 枚举值。</summary>
    /// <returns>指定此任务工厂的默认延续选项的枚举值之一。</returns>
    [__DynamicallyInvokable]
    public TaskContinuationOptions ContinuationOptions
    {
      [__DynamicallyInvokable] get
      {
        return this.m_defaultContinuationOptions;
      }
    }

    /// <summary>使用默认配置初始化 <see cref="T:System.Threading.Tasks.TaskFactory`1" /> 实例。</summary>
    [__DynamicallyInvokable]
    public TaskFactory()
      : this(new CancellationToken(), TaskCreationOptions.None, TaskContinuationOptions.None, (TaskScheduler) null)
    {
    }

    /// <summary>使用默认配置初始化 <see cref="T:System.Threading.Tasks.TaskFactory`1" /> 实例。</summary>
    /// <param name="cancellationToken">将指派给由此 <see cref="T:System.Threading.Tasks.TaskFactory" /> 创建的任务的默认取消标记（除非在调用工厂方法时显式指定另一个取消标记）。</param>
    [__DynamicallyInvokable]
    public TaskFactory(CancellationToken cancellationToken)
      : this(cancellationToken, TaskCreationOptions.None, TaskContinuationOptions.None, (TaskScheduler) null)
    {
    }

    /// <summary>使用指定配置初始化 <see cref="T:System.Threading.Tasks.TaskFactory`1" /> 实例。</summary>
    /// <param name="scheduler">要用于计划使用此 <see cref="T:System.Threading.Tasks.TaskFactory`1" /> 创建的任何任务的计划程序。一个 null 值，该值指示应使用当前 <see cref="T:System.Threading.Tasks.TaskScheduler" />。</param>
    [__DynamicallyInvokable]
    public TaskFactory(TaskScheduler scheduler)
      : this(new CancellationToken(), TaskCreationOptions.None, TaskContinuationOptions.None, scheduler)
    {
    }

    /// <summary>使用指定配置初始化 <see cref="T:System.Threading.Tasks.TaskFactory`1" /> 实例。</summary>
    /// <param name="creationOptions">在使用此 <see cref="T:System.Threading.Tasks.TaskFactory`1" /> 创建任务时要使用的默认选项。</param>
    /// <param name="continuationOptions">在使用此 <see cref="T:System.Threading.Tasks.TaskFactory`1" /> 创建任务时要使用的默认选项。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="creationOptions" /> 或 <paramref name="continuationOptions" /> 指定了一个无效值。</exception>
    [__DynamicallyInvokable]
    public TaskFactory(TaskCreationOptions creationOptions, TaskContinuationOptions continuationOptions)
      : this(new CancellationToken(), creationOptions, continuationOptions, (TaskScheduler) null)
    {
    }

    /// <summary>使用指定配置初始化 <see cref="T:System.Threading.Tasks.TaskFactory`1" /> 实例。</summary>
    /// <param name="cancellationToken">将指派给由此 <see cref="T:System.Threading.Tasks.TaskFactory" /> 创建的任务的默认取消标记（除非在调用工厂方法时显式指定另一个取消标记）。</param>
    /// <param name="creationOptions">在使用此 <see cref="T:System.Threading.Tasks.TaskFactory`1" /> 创建任务时要使用的默认选项。</param>
    /// <param name="continuationOptions">在使用此 <see cref="T:System.Threading.Tasks.TaskFactory`1" /> 创建任务时要使用的默认选项。</param>
    /// <param name="scheduler">要用于计划使用此 <see cref="T:System.Threading.Tasks.TaskFactory`1" /> 创建的任何任务的默认计划程序。null 值指示应使用 <see cref="P:System.Threading.Tasks.TaskScheduler.Current" />。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="creationOptions" /> 或 <paramref name="continuationOptions" /> 指定了一个无效值。</exception>
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

    /// <summary>创建并启动 任务。</summary>
    /// <returns>已启动的任务。</returns>
    /// <param name="function">一个函数委托，可返回能够通过任务获得的将来结果。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="function" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> StartNew(Func<TResult> function)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      Task internalCurrent = Task.InternalCurrent;
      return Task<TResult>.StartNew(internalCurrent, function, this.m_defaultCancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackMark);
    }

    /// <summary>创建并启动 任务。</summary>
    /// <returns>已启动的任务。</returns>
    /// <param name="function">一个函数委托，可返回能够通过任务获得的将来结果。</param>
    /// <param name="cancellationToken">将指派给新的任务的取消标记。</param>
    /// <exception cref="T:System.ObjectDisposedException">已处理创建的 <paramref name="cancellationToken" /> 的取消标记源。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="function" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> StartNew(Func<TResult> function, CancellationToken cancellationToken)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      Task internalCurrent = Task.InternalCurrent;
      return Task<TResult>.StartNew(internalCurrent, function, cancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackMark);
    }

    /// <summary>创建并启动 任务。</summary>
    /// <returns>已启动的 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    /// <param name="function">一个函数委托，可返回能够通过任务获得的将来结果。</param>
    /// <param name="creationOptions">控制所创建的任务的行为的枚举值之一。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="function" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="creationOptions" /> 参数指定的值无效。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> StartNew(Func<TResult> function, TaskCreationOptions creationOptions)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      Task internalCurrent = Task.InternalCurrent;
      return Task<TResult>.StartNew(internalCurrent, function, this.m_defaultCancellationToken, creationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackMark);
    }

    /// <summary>创建并启动 任务。</summary>
    /// <returns>已启动的任务。</returns>
    /// <param name="function">一个函数委托，可返回能够通过任务获得的将来结果。</param>
    /// <param name="cancellationToken">将指派给新的任务的取消标记。</param>
    /// <param name="creationOptions">控制所创建的任务的行为的枚举值之一。</param>
    /// <param name="scheduler">用于计划所创建的任务的任务计划程序。</param>
    /// <exception cref="T:System.ObjectDisposedException">已处理创建的 <paramref name="cancellationToken" /> 的取消标记源。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="function" /> 参数为 null。- 或 -<paramref name="scheduler" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="creationOptions" /> 参数指定的值无效。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> StartNew(Func<TResult> function, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return Task<TResult>.StartNew(Task.InternalCurrentIfAttached(creationOptions), function, cancellationToken, creationOptions, InternalTaskOptions.None, scheduler, ref stackMark);
    }

    /// <summary>创建并启动 任务。</summary>
    /// <returns>已启动的任务。</returns>
    /// <param name="function">一个函数委托，可返回能够通过任务获得的将来结果。</param>
    /// <param name="state">包含 <paramref name="function" /> 委托所用数据的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="function" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> StartNew(Func<object, TResult> function, object state)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      Task internalCurrent = Task.InternalCurrent;
      return Task<TResult>.StartNew(internalCurrent, function, state, this.m_defaultCancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackMark);
    }

    /// <summary>创建并启动 任务。</summary>
    /// <returns>已启动的任务。</returns>
    /// <param name="function">一个函数委托，可返回能够通过任务获得的将来结果。</param>
    /// <param name="state">包含 <paramref name="function" /> 委托所用数据的对象。</param>
    /// <param name="cancellationToken">将指派给新的任务的取消标记。</param>
    /// <exception cref="T:System.ObjectDisposedException">已处理创建的 <paramref name="cancellationToken" /> 的取消标记源。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="function" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> StartNew(Func<object, TResult> function, object state, CancellationToken cancellationToken)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      Task internalCurrent = Task.InternalCurrent;
      return Task<TResult>.StartNew(internalCurrent, function, state, cancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackMark);
    }

    /// <summary>创建并启动 任务。</summary>
    /// <returns>已启动的任务。</returns>
    /// <param name="function">一个函数委托，可返回能够通过任务获得的将来结果。</param>
    /// <param name="state">包含 <paramref name="function" /> 委托所用数据的对象。</param>
    /// <param name="creationOptions">控制所创建的任务的行为的枚举值之一。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="function" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="creationOptions" /> 参数指定的值无效。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> StartNew(Func<object, TResult> function, object state, TaskCreationOptions creationOptions)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      Task internalCurrent = Task.InternalCurrent;
      return Task<TResult>.StartNew(internalCurrent, function, state, this.m_defaultCancellationToken, creationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackMark);
    }

    /// <summary>创建并启动 任务。</summary>
    /// <returns>已启动的任务。</returns>
    /// <param name="function">一个函数委托，可返回能够通过任务获得的将来结果。</param>
    /// <param name="state">包含 <paramref name="function" /> 委托所用数据的对象。</param>
    /// <param name="cancellationToken">将指派给新的任务的取消标记。</param>
    /// <param name="creationOptions">控制所创建的任务的行为的枚举值之一。</param>
    /// <param name="scheduler">用于计划所创建的任务的任务计划程序。</param>
    /// <exception cref="T:System.ObjectDisposedException">已处理创建的 <paramref name="cancellationToken" /> 的取消标记源。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="function" /> 参数为 null。- 或 -<paramref name="scheduler" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="creationOptions" /> 参数指定的值无效。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> StartNew(Func<object, TResult> function, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return Task<TResult>.StartNew(Task.InternalCurrentIfAttached(creationOptions), function, state, cancellationToken, creationOptions, InternalTaskOptions.None, scheduler, ref stackMark);
    }

    private static void FromAsyncCoreLogic(IAsyncResult iar, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, Task<TResult> promise, bool requiresSynchronization)
    {
      Exception exception = (Exception) null;
      OperationCanceledException canceledException = (OperationCanceledException) null;
      TResult result = default (TResult);
      try
      {
        if (endFunction != null)
          result = endFunction(iar);
        else
          endAction(iar);
      }
      catch (OperationCanceledException ex)
      {
        canceledException = ex;
      }
      catch (Exception ex)
      {
        exception = ex;
      }
      finally
      {
        if (canceledException != null)
          promise.TrySetCanceled(canceledException.CancellationToken, (object) canceledException);
        else if (exception != null)
        {
          if (promise.TrySetException((object) exception) && exception is ThreadAbortException)
            promise.m_contingentProperties.m_exceptionsHolder.MarkAsHandled(false);
        }
        else
        {
          if (AsyncCausalityTracer.LoggingOn)
            AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, promise.Id, AsyncCausalityStatus.Completed);
          if (Task.s_asyncDebuggingEnabled)
            Task.RemoveFromActiveTasks(promise.Id);
          if (requiresSynchronization)
            promise.TrySetResult(result);
          else
            promise.DangerousSetResult(result);
        }
      }
    }

    /// <summary>创建一个任务，它在指定的 <see cref="T:System.IAsyncResult" /> 完成时执行一个结束方法函数。</summary>
    /// <returns>一个表示异步操作的 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    /// <param name="asyncResult">
    /// <see cref="T:System.IAsyncResult" />，完成它时将触发对 <paramref name="endMethod" /> 的处理。</param>
    /// <param name="endMethod">用于处理完成的 <paramref name="asyncResult" /> 的函数委托。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="asyncResult" /> 参数为 null。- 或 -<paramref name="endMethod" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> FromAsync(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.FromAsyncImpl(asyncResult, endMethod, (Action<IAsyncResult>) null, this.m_defaultCreationOptions, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>创建一个任务，它在指定的 <see cref="T:System.IAsyncResult" /> 完成时执行一个结束方法函数。</summary>
    /// <returns>表示异步操作的任务。</returns>
    /// <param name="asyncResult">
    /// <see cref="T:System.IAsyncResult" />，完成它时将触发对 <paramref name="endMethod" /> 的处理。</param>
    /// <param name="endMethod">用于处理完成的 <paramref name="asyncResult" /> 的函数委托。</param>
    /// <param name="creationOptions">控制所创建的任务的行为的枚举值之一。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="asyncResult" /> 参数为 null。- 或 -<paramref name="endMethod" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="creationOptions" /> 参数指定的值无效。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> FromAsync(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod, TaskCreationOptions creationOptions)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.FromAsyncImpl(asyncResult, endMethod, (Action<IAsyncResult>) null, creationOptions, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>创建一个任务，它在指定的 <see cref="T:System.IAsyncResult" /> 完成时执行一个结束方法函数。</summary>
    /// <returns>创建的表示异步操作的任务。</returns>
    /// <param name="asyncResult">
    /// <see cref="T:System.IAsyncResult" />，完成它时将触发对 <paramref name="endMethod" /> 的处理。</param>
    /// <param name="endMethod">用于处理完成的 <paramref name="asyncResult" /> 的函数委托。</param>
    /// <param name="creationOptions">控制所创建的任务的行为的枚举值之一。</param>
    /// <param name="scheduler">用于计划将执行结束方法的任务计划程序。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="asyncResult" /> 参数为 null。- 或 -<paramref name="endMethod" /> 参数为 null。- 或 -<paramref name="scheduler" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="creationOptions" /> 参数指定的值无效。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> FromAsync(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod, TaskCreationOptions creationOptions, TaskScheduler scheduler)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.FromAsyncImpl(asyncResult, endMethod, (Action<IAsyncResult>) null, creationOptions, scheduler, ref stackMark);
    }

    internal static Task<TResult> FromAsyncImpl(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, TaskCreationOptions creationOptions, TaskScheduler scheduler, ref StackCrawlMark stackMark)
    {
      if (asyncResult == null)
        throw new ArgumentNullException("asyncResult");
      if (endFunction == null && endAction == null)
        throw new ArgumentNullException("endMethod");
      if (scheduler == null)
        throw new ArgumentNullException("scheduler");
      TaskFactory.CheckFromAsyncOptions(creationOptions, false);
      Task<TResult> promise = new Task<TResult>((object) null, creationOptions);
      if (AsyncCausalityTracer.LoggingOn)
        AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, promise.Id, "TaskFactory.FromAsync", 0UL);
      if (Task.s_asyncDebuggingEnabled)
        Task.AddToActiveTasks((Task) promise);
      Task t = new Task((Action<object>) (param0 => TaskFactory<TResult>.FromAsyncCoreLogic(asyncResult, endFunction, endAction, promise, true)), (object) null, (Task) null, new CancellationToken(), TaskCreationOptions.None, InternalTaskOptions.None, (TaskScheduler) null, ref stackMark);
      if (AsyncCausalityTracer.LoggingOn)
        AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Verbose, t.Id, "TaskFactory.FromAsync Callback", 0UL);
      if (Task.s_asyncDebuggingEnabled)
        Task.AddToActiveTasks(t);
      if (asyncResult.IsCompleted)
      {
        try
        {
          t.InternalRunSynchronously(scheduler, false);
        }
        catch (Exception ex)
        {
          promise.TrySetException((object) ex);
        }
      }
      else
        ThreadPool.RegisterWaitForSingleObject(asyncResult.AsyncWaitHandle, (WaitOrTimerCallback) ((param0, param1) =>
        {
          try
          {
            t.InternalRunSynchronously(scheduler, false);
          }
          catch (Exception ex)
          {
            promise.TrySetException((object) ex);
          }
        }), (object) null, -1, true);
      return promise;
    }

    /// <summary>创建一个任务，它表示符合异步编程模型模式的成对的开始和结束方法。</summary>
    /// <returns>创建的表示异步操作的任务。</returns>
    /// <param name="beginMethod">用于启动异步操作的委托。</param>
    /// <param name="endMethod">用于结束异步操作的委托。</param>
    /// <param name="state">一个包含由 <paramref name="beginMethod" /> 委托使用的数据的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="beginMethod" /> 参数为 null。- 或 -<paramref name="endMethod" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    public Task<TResult> FromAsync(Func<AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, object state)
    {
      return TaskFactory<TResult>.FromAsyncImpl(beginMethod, endMethod, (Action<IAsyncResult>) null, state, this.m_defaultCreationOptions);
    }

    /// <summary>创建一个任务，它表示符合异步编程模型模式的成对的开始和结束方法。</summary>
    /// <returns>创建的表示异步操作的 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    /// <param name="beginMethod">用于启动异步操作的委托。</param>
    /// <param name="endMethod">用于结束异步操作的委托。</param>
    /// <param name="state">一个包含由 <paramref name="beginMethod" /> 委托使用的数据的对象。</param>
    /// <param name="creationOptions">控制所创建的任务的行为的枚举值之一。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="beginMethod" /> 参数为 null。- 或 -<paramref name="endMethod" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="creationOptions" /> 参数指定的值无效。</exception>
    [__DynamicallyInvokable]
    public Task<TResult> FromAsync(Func<AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, object state, TaskCreationOptions creationOptions)
    {
      return TaskFactory<TResult>.FromAsyncImpl(beginMethod, endMethod, (Action<IAsyncResult>) null, state, creationOptions);
    }

    internal static Task<TResult> FromAsyncImpl(Func<AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, object state, TaskCreationOptions creationOptions)
    {
      if (beginMethod == null)
        throw new ArgumentNullException("beginMethod");
      if (endFunction == null && endAction == null)
        throw new ArgumentNullException("endMethod");
      TaskFactory.CheckFromAsyncOptions(creationOptions, true);
      Task<TResult> promise = new Task<TResult>(state, creationOptions);
      if (AsyncCausalityTracer.LoggingOn)
        AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, promise.Id, "TaskFactory.FromAsync: " + beginMethod.Method.Name, 0UL);
      if (Task.s_asyncDebuggingEnabled)
        Task.AddToActiveTasks((Task) promise);
      try
      {
        if (BinaryCompatibility.TargetsAtLeast_Desktop_V4_5)
        {
          IAsyncResult iar1 = beginMethod((AsyncCallback) (iar =>
          {
            if (iar.CompletedSynchronously)
              return;
            TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true);
          }), state);
          if (iar1.CompletedSynchronously)
            TaskFactory<TResult>.FromAsyncCoreLogic(iar1, endFunction, endAction, promise, false);
        }
        else
        {
          IAsyncResult asyncResult = beginMethod((AsyncCallback) (iar => TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true)), state);
        }
      }
      catch
      {
        if (AsyncCausalityTracer.LoggingOn)
          AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, promise.Id, AsyncCausalityStatus.Error);
        if (Task.s_asyncDebuggingEnabled)
          Task.RemoveFromActiveTasks(promise.Id);
        promise.TrySetResult(default (TResult));
        throw;
      }
      return promise;
    }

    /// <summary>创建一个任务，它表示符合异步编程模型模式的成对的开始和结束方法。</summary>
    /// <returns>创建的表示异步操作的任务。</returns>
    /// <param name="beginMethod">用于启动异步操作的委托。</param>
    /// <param name="endMethod">用于结束异步操作的委托。</param>
    /// <param name="arg1">传递给 <paramref name="beginMethod" /> 委托的第一个参数。</param>
    /// <param name="state">一个包含由 <paramref name="beginMethod" /> 委托使用的数据的对象。</param>
    /// <typeparam name="TArg1">传递给 <paramref name="beginMethod" /> 委托的第一个参数的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="beginMethod" /> 参数为 null。- 或 -<paramref name="endMethod" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    public Task<TResult> FromAsync<TArg1>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, object state)
    {
      return TaskFactory<TResult>.FromAsyncImpl<TArg1>(beginMethod, endMethod, (Action<IAsyncResult>) null, arg1, state, this.m_defaultCreationOptions);
    }

    /// <summary>创建一个任务，它表示符合异步编程模型模式的成对的开始和结束方法。</summary>
    /// <returns>创建的表示异步操作的任务。</returns>
    /// <param name="beginMethod">用于启动异步操作的委托。</param>
    /// <param name="endMethod">用于结束异步操作的委托。</param>
    /// <param name="arg1">传递给 <paramref name="beginMethod" /> 委托的第一个参数。</param>
    /// <param name="state">一个包含由 <paramref name="beginMethod" /> 委托使用的数据的对象。</param>
    /// <param name="creationOptions">控制所创建的任务的行为的枚举值之一。</param>
    /// <typeparam name="TArg1">传递给 <paramref name="beginMethod" /> 委托的第一个参数的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="beginMethod" /> 参数为 null。- 或 -<paramref name="endMethod" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="creationOptions" /> 参数指定的值无效。</exception>
    [__DynamicallyInvokable]
    public Task<TResult> FromAsync<TArg1>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, object state, TaskCreationOptions creationOptions)
    {
      return TaskFactory<TResult>.FromAsyncImpl<TArg1>(beginMethod, endMethod, (Action<IAsyncResult>) null, arg1, state, creationOptions);
    }

    internal static Task<TResult> FromAsyncImpl<TArg1>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, TArg1 arg1, object state, TaskCreationOptions creationOptions)
    {
      if (beginMethod == null)
        throw new ArgumentNullException("beginMethod");
      if (endFunction == null && endAction == null)
        throw new ArgumentNullException("endFunction");
      TaskFactory.CheckFromAsyncOptions(creationOptions, true);
      Task<TResult> promise = new Task<TResult>(state, creationOptions);
      if (AsyncCausalityTracer.LoggingOn)
        AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, promise.Id, "TaskFactory.FromAsync: " + beginMethod.Method.Name, 0UL);
      if (Task.s_asyncDebuggingEnabled)
        Task.AddToActiveTasks((Task) promise);
      try
      {
        if (BinaryCompatibility.TargetsAtLeast_Desktop_V4_5)
        {
          IAsyncResult iar1 = beginMethod(arg1, (AsyncCallback) (iar =>
          {
            if (iar.CompletedSynchronously)
              return;
            TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true);
          }), state);
          if (iar1.CompletedSynchronously)
            TaskFactory<TResult>.FromAsyncCoreLogic(iar1, endFunction, endAction, promise, false);
        }
        else
        {
          IAsyncResult asyncResult = beginMethod(arg1, (AsyncCallback) (iar => TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true)), state);
        }
      }
      catch
      {
        if (AsyncCausalityTracer.LoggingOn)
          AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, promise.Id, AsyncCausalityStatus.Error);
        if (Task.s_asyncDebuggingEnabled)
          Task.RemoveFromActiveTasks(promise.Id);
        promise.TrySetResult(default (TResult));
        throw;
      }
      return promise;
    }

    /// <summary>创建一个任务，它表示符合异步编程模型模式的成对的开始和结束方法。</summary>
    /// <returns>创建的表示异步操作的任务。</returns>
    /// <param name="beginMethod">用于启动异步操作的委托。</param>
    /// <param name="endMethod">用于结束异步操作的委托。</param>
    /// <param name="arg1">传递给 <paramref name="beginMethod" /> 委托的第一个参数。</param>
    /// <param name="arg2">传递给 <paramref name="beginMethod" /> 委托的第二个参数。</param>
    /// <param name="state">一个包含由 <paramref name="beginMethod" /> 委托使用的数据的对象。</param>
    /// <typeparam name="TArg1">传递给 <paramref name="beginMethod" /> 委托的第二个参数的类型。</typeparam>
    /// <typeparam name="TArg2">传递给 <paramref name="beginMethod" /> 委托的第一个参数的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="beginMethod" /> 参数为 null。- 或 -<paramref name="endMethod" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    public Task<TResult> FromAsync<TArg1, TArg2>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, object state)
    {
      return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2>(beginMethod, endMethod, (Action<IAsyncResult>) null, arg1, arg2, state, this.m_defaultCreationOptions);
    }

    /// <summary>创建一个任务，它表示符合异步编程模型模式的成对的开始和结束方法。</summary>
    /// <returns>创建的表示异步操作的任务。</returns>
    /// <param name="beginMethod">用于启动异步操作的委托。</param>
    /// <param name="endMethod">用于结束异步操作的委托。</param>
    /// <param name="arg1">传递给 <paramref name="beginMethod" /> 委托的第一个参数。</param>
    /// <param name="arg2">传递给 <paramref name="beginMethod" /> 委托的第二个参数。</param>
    /// <param name="state">一个包含由 <paramref name="beginMethod" /> 委托使用的数据的对象。</param>
    /// <param name="creationOptions">一个对象，用于控制所创建的 <see cref="T:System.Threading.Tasks.Task`1" /> 的行为。</param>
    /// <typeparam name="TArg1">传递给 <paramref name="beginMethod" /> 委托的第二个参数的类型。</typeparam>
    /// <typeparam name="TArg2">传递给 <paramref name="beginMethod" /> 委托的第一个参数的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="beginMethod" /> 参数为 null。- 或 -<paramref name="endMethod" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="creationOptions" /> 参数指定的值无效。</exception>
    [__DynamicallyInvokable]
    public Task<TResult> FromAsync<TArg1, TArg2>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, object state, TaskCreationOptions creationOptions)
    {
      return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2>(beginMethod, endMethod, (Action<IAsyncResult>) null, arg1, arg2, state, creationOptions);
    }

    internal static Task<TResult> FromAsyncImpl<TArg1, TArg2>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, TArg1 arg1, TArg2 arg2, object state, TaskCreationOptions creationOptions)
    {
      if (beginMethod == null)
        throw new ArgumentNullException("beginMethod");
      if (endFunction == null && endAction == null)
        throw new ArgumentNullException("endMethod");
      TaskFactory.CheckFromAsyncOptions(creationOptions, true);
      Task<TResult> promise = new Task<TResult>(state, creationOptions);
      if (AsyncCausalityTracer.LoggingOn)
        AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, promise.Id, "TaskFactory.FromAsync: " + beginMethod.Method.Name, 0UL);
      if (Task.s_asyncDebuggingEnabled)
        Task.AddToActiveTasks((Task) promise);
      try
      {
        if (BinaryCompatibility.TargetsAtLeast_Desktop_V4_5)
        {
          IAsyncResult iar1 = beginMethod(arg1, arg2, (AsyncCallback) (iar =>
          {
            if (iar.CompletedSynchronously)
              return;
            TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true);
          }), state);
          if (iar1.CompletedSynchronously)
            TaskFactory<TResult>.FromAsyncCoreLogic(iar1, endFunction, endAction, promise, false);
        }
        else
        {
          IAsyncResult asyncResult = beginMethod(arg1, arg2, (AsyncCallback) (iar => TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true)), state);
        }
      }
      catch
      {
        if (AsyncCausalityTracer.LoggingOn)
          AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, promise.Id, AsyncCausalityStatus.Error);
        if (Task.s_asyncDebuggingEnabled)
          Task.RemoveFromActiveTasks(promise.Id);
        promise.TrySetResult(default (TResult));
        throw;
      }
      return promise;
    }

    /// <summary>创建一个任务，它表示符合异步编程模型模式的成对的开始和结束方法。</summary>
    /// <returns>创建的表示异步操作的任务。</returns>
    /// <param name="beginMethod">用于启动异步操作的委托。</param>
    /// <param name="endMethod">用于结束异步操作的委托。</param>
    /// <param name="arg1">传递给 <paramref name="beginMethod" /> 委托的第一个参数。</param>
    /// <param name="arg2">传递给 <paramref name="beginMethod" /> 委托的第二个参数。</param>
    /// <param name="arg3">传递给 <paramref name="beginMethod" /> 委托的第三个参数。</param>
    /// <param name="state">一个包含由 <paramref name="beginMethod" /> 委托使用的数据的对象。</param>
    /// <typeparam name="TArg1">传递给 <paramref name="beginMethod" /> 委托的第二个参数的类型。</typeparam>
    /// <typeparam name="TArg2">传递给 <paramref name="beginMethod" /> 委托的第三个参数的类型。</typeparam>
    /// <typeparam name="TArg3">传递给 <paramref name="beginMethod" /> 委托的第一个参数的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="beginMethod" /> 参数为 null。- 或 -<paramref name="endMethod" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    public Task<TResult> FromAsync<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state)
    {
      return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2, TArg3>(beginMethod, endMethod, (Action<IAsyncResult>) null, arg1, arg2, arg3, state, this.m_defaultCreationOptions);
    }

    /// <summary>创建一个任务，它表示符合异步编程模型模式的成对的开始和结束方法。</summary>
    /// <returns>创建的表示异步操作的任务。</returns>
    /// <param name="beginMethod">用于启动异步操作的委托。</param>
    /// <param name="endMethod">用于结束异步操作的委托。</param>
    /// <param name="arg1">传递给 <paramref name="beginMethod" /> 委托的第一个参数。</param>
    /// <param name="arg2">传递给 <paramref name="beginMethod" /> 委托的第二个参数。</param>
    /// <param name="arg3">传递给 <paramref name="beginMethod" /> 委托的第三个参数。</param>
    /// <param name="state">一个包含由 <paramref name="beginMethod" /> 委托使用的数据的对象。</param>
    /// <param name="creationOptions">一个对象，用于控制所创建的任务的行为。</param>
    /// <typeparam name="TArg1">传递给 <paramref name="beginMethod" /> 委托的第二个参数的类型。</typeparam>
    /// <typeparam name="TArg2">传递给 <paramref name="beginMethod" /> 委托的第三个参数的类型。</typeparam>
    /// <typeparam name="TArg3">传递给 <paramref name="beginMethod" /> 委托的第一个参数的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="beginMethod" /> 参数为 null。- 或 -<paramref name="endMethod" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="creationOptions" /> 参数指定的值无效。</exception>
    [__DynamicallyInvokable]
    public Task<TResult> FromAsync<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state, TaskCreationOptions creationOptions)
    {
      return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2, TArg3>(beginMethod, endMethod, (Action<IAsyncResult>) null, arg1, arg2, arg3, state, creationOptions);
    }

    internal static Task<TResult> FromAsyncImpl<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state, TaskCreationOptions creationOptions)
    {
      if (beginMethod == null)
        throw new ArgumentNullException("beginMethod");
      if (endFunction == null && endAction == null)
        throw new ArgumentNullException("endMethod");
      TaskFactory.CheckFromAsyncOptions(creationOptions, true);
      Task<TResult> promise = new Task<TResult>(state, creationOptions);
      if (AsyncCausalityTracer.LoggingOn)
        AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, promise.Id, "TaskFactory.FromAsync: " + beginMethod.Method.Name, 0UL);
      if (Task.s_asyncDebuggingEnabled)
        Task.AddToActiveTasks((Task) promise);
      try
      {
        if (BinaryCompatibility.TargetsAtLeast_Desktop_V4_5)
        {
          IAsyncResult iar1 = beginMethod(arg1, arg2, arg3, (AsyncCallback) (iar =>
          {
            if (iar.CompletedSynchronously)
              return;
            TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true);
          }), state);
          if (iar1.CompletedSynchronously)
            TaskFactory<TResult>.FromAsyncCoreLogic(iar1, endFunction, endAction, promise, false);
        }
        else
        {
          IAsyncResult asyncResult = beginMethod(arg1, arg2, arg3, (AsyncCallback) (iar => TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true)), state);
        }
      }
      catch
      {
        if (AsyncCausalityTracer.LoggingOn)
          AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, promise.Id, AsyncCausalityStatus.Error);
        if (Task.s_asyncDebuggingEnabled)
          Task.RemoveFromActiveTasks(promise.Id);
        promise.TrySetResult(default (TResult));
        throw;
      }
      return promise;
    }

    internal static Task<TResult> FromAsyncTrim<TInstance, TArgs>(TInstance thisRef, TArgs args, Func<TInstance, TArgs, AsyncCallback, object, IAsyncResult> beginMethod, Func<TInstance, IAsyncResult, TResult> endMethod) where TInstance : class
    {
      TaskFactory<TResult>.FromAsyncTrimPromise<TInstance> asyncTrimPromise = new TaskFactory<TResult>.FromAsyncTrimPromise<TInstance>(thisRef, endMethod);
      IAsyncResult asyncResult = beginMethod(thisRef, args, TaskFactory<TResult>.FromAsyncTrimPromise<TInstance>.s_completeFromAsyncResult, (object) asyncTrimPromise);
      if (asyncResult.CompletedSynchronously)
        asyncTrimPromise.Complete(thisRef, endMethod, asyncResult, false);
      return (Task<TResult>) asyncTrimPromise;
    }

    private static Task<TResult> CreateCanceledTask(TaskContinuationOptions continuationOptions, CancellationToken ct)
    {
      TaskCreationOptions creationOptions;
      InternalTaskOptions internalOptions;
      Task.CreationOptionsFromContinuationOptions(continuationOptions, out creationOptions, out internalOptions);
      return new Task<TResult>(true, default (TResult), creationOptions, ct);
    }

    /// <summary>创建一个延续任务，它将在提供的一组任务完成后马上开始。</summary>
    /// <returns>新的延续任务。</returns>
    /// <param name="tasks">继续执行的任务所在的数组。</param>
    /// <param name="continuationFunction">在 <paramref name="tasks" /> 数组中的所有任务完成时要异步执行的函数委托。</param>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <paramref name="tasks" /> 数组中的某个元素已被释放。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tasks" /> 数组为 null。- 或 -<paramref name="continuationFunction" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tasks" /> 数组包含 null 值或为空。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAll(Task[] tasks, Func<Task[], TResult> continuationFunction)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException("continuationFunction");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, (Action<Task[]>) null, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>创建一个延续任务，它将在提供的一组任务完成后马上开始。</summary>
    /// <returns>新的延续任务。</returns>
    /// <param name="tasks">继续执行的任务所在的数组。</param>
    /// <param name="continuationFunction">在 <paramref name="tasks" /> 数组中的所有任务完成时要异步执行的函数委托。</param>
    /// <param name="cancellationToken">将指派给新的延续任务的取消标记。</param>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <paramref name="tasks" /> 数组中的某个元素已被释放。- 或 -<see cref="T:System.Threading.CancellationTokenSource" /> 创建<paramref name=" cancellationToken" /> 已释放。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tasks" /> 数组为 null。- 或 -<paramref name="continuationFunction" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tasks" /> 数组包含 null 值或为空。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAll(Task[] tasks, Func<Task[], TResult> continuationFunction, CancellationToken cancellationToken)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException("continuationFunction");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, (Action<Task[]>) null, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>创建一个延续任务，它将在提供的一组任务完成后马上开始。</summary>
    /// <returns>新的延续任务。</returns>
    /// <param name="tasks">继续执行的任务所在的数组。</param>
    /// <param name="continuationFunction">在 <paramref name="tasks" /> 数组中的所有任务完成时要异步执行的函数委托。</param>
    /// <param name="continuationOptions">控制所创建的延续任务的行为的枚举值之一。NotOn* 或 OnlyOn* 值均无效。</param>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <paramref name="tasks" /> 数组中的某个元素已被释放。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tasks" /> 数组为 null。- 或 -<paramref name="continuationFunction" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="continuationOptions" /> 参数指定的值无效。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tasks" /> 数组包含 null 值或为空。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAll(Task[] tasks, Func<Task[], TResult> continuationFunction, TaskContinuationOptions continuationOptions)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException("continuationFunction");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, (Action<Task[]>) null, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>创建一个延续任务，它将在提供的一组任务完成后马上开始。</summary>
    /// <returns>新的延续任务。</returns>
    /// <param name="tasks">继续执行的任务所在的数组。</param>
    /// <param name="continuationFunction">在 <paramref name="tasks" /> 数组中的所有任务完成时要异步执行的函数委托。</param>
    /// <param name="cancellationToken">将指派给新的延续任务的取消标记。</param>
    /// <param name="continuationOptions">控制所创建的延续任务的行为的枚举值之一。NotOn* 或 OnlyOn* 值均无效。</param>
    /// <param name="scheduler">用于计划所创建的延续任务的计划程序。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tasks" /> 数组为 null。- 或 -<paramref name="continuationFunction" /> 参数为 null。- 或 -<paramref name="scheduler" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tasks" /> 数组包含 null 值或为空。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="continuationOptions" /> 指定了一个无效值。</exception>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <paramref name="tasks" /> 数组中的某个元素已被释放。- 或 -<see cref="T:System.Threading.CancellationTokenSource" /> 创建<paramref name=" cancellationToken" /> 已释放。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAll(Task[] tasks, Func<Task[], TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException("continuationFunction");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, (Action<Task[]>) null, continuationOptions, cancellationToken, scheduler, ref stackMark);
    }

    /// <summary>创建一个延续任务，它将在提供的一组任务完成后马上开始。</summary>
    /// <returns>新的延续任务。</returns>
    /// <param name="tasks">继续执行的任务所在的数组。</param>
    /// <param name="continuationFunction">在 <paramref name="tasks" /> 数组中的所有任务完成时要异步执行的函数委托。</param>
    /// <typeparam name="TAntecedentResult">以前的 <paramref name="tasks" /> 结果的类型。</typeparam>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <paramref name="tasks" /> 数组中的某个元素已被释放。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tasks" /> 数组为 null。- 或 -<paramref name="continuationFunction" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tasks" /> 数组包含 null 值或为空。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException("continuationFunction");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, (Action<Task<TAntecedentResult>[]>) null, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>创建一个延续任务，它将在提供的一组任务完成后马上开始。</summary>
    /// <returns>新的延续任务。</returns>
    /// <param name="tasks">继续执行的任务所在的数组。</param>
    /// <param name="continuationFunction">在 <paramref name="tasks" /> 数组中的所有任务完成时要异步执行的函数委托。</param>
    /// <param name="cancellationToken">将指派给新的延续任务的取消标记。</param>
    /// <typeparam name="TAntecedentResult">以前的 <paramref name="tasks" /> 结果的类型。</typeparam>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <paramref name="tasks" /> 数组中的某个元素已被释放。- 或 -<see cref="T:System.Threading.CancellationTokenSource" /> 创建<paramref name=" cancellationToken" /> 已释放。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tasks" /> 数组为 null。- 或 -<paramref name="continuationFunction" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tasks" /> 数组包含 null 值或为空。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, CancellationToken cancellationToken)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException("continuationFunction");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, (Action<Task<TAntecedentResult>[]>) null, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>创建一个延续任务，它将在提供的一组任务完成后马上开始。</summary>
    /// <returns>新的延续任务。</returns>
    /// <param name="tasks">继续执行的任务所在的数组。</param>
    /// <param name="continuationFunction">在 <paramref name="tasks" /> 数组中的所有任务完成时要异步执行的函数委托。</param>
    /// <param name="continuationOptions">控制所创建的延续任务的行为的枚举值之一。NotOn* 或 OnlyOn* 值均无效。</param>
    /// <typeparam name="TAntecedentResult">以前的 <paramref name="tasks" /> 结果的类型。</typeparam>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <paramref name="tasks" /> 数组中的某个元素已被释放。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tasks" /> 数组为 null。- 或 -<paramref name="continuationFunction" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="continuationOptions" /> 参数指定的值无效。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tasks" /> 数组包含 null 值或为空。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, TaskContinuationOptions continuationOptions)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException("continuationFunction");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, (Action<Task<TAntecedentResult>[]>) null, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>创建一个延续任务，它将在提供的一组任务完成后马上开始。</summary>
    /// <returns>新的延续任务。</returns>
    /// <param name="tasks">继续执行的任务所在的数组。</param>
    /// <param name="continuationFunction">在 <paramref name="tasks" /> 数组中的所有任务完成时要异步执行的函数委托。</param>
    /// <param name="cancellationToken">将指派给新的延续任务的取消标记。</param>
    /// <param name="continuationOptions">控制所创建的延续任务的行为的枚举值之一。NotOn* 或 OnlyOn* 值均无效。</param>
    /// <param name="scheduler">用于计划所创建的延续任务的计划程序。</param>
    /// <typeparam name="TAntecedentResult">以前的 <paramref name="tasks" /> 结果的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tasks" /> 数组为 null。- 或 -<paramref name="continuationFunction" /> 参数为 null。- 或 -<paramref name="scheduler" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tasks" /> 数组包含 null 值或为空。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="continuationOptions" /> 参数指定的值无效。</exception>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <paramref name="tasks" /> 数组中的某个元素已被释放。- 或 -<see cref="T:System.Threading.CancellationTokenSource" /> 创建<paramref name=" cancellationToken" /> 已释放。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException("continuationFunction");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, (Action<Task<TAntecedentResult>[]>) null, continuationOptions, cancellationToken, scheduler, ref stackMark);
    }

    internal static Task<TResult> ContinueWhenAllImpl<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, Action<Task<TAntecedentResult>[]> continuationAction, TaskContinuationOptions continuationOptions, CancellationToken cancellationToken, TaskScheduler scheduler, ref StackCrawlMark stackMark)
    {
      TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
      if (tasks == null)
        throw new ArgumentNullException("tasks");
      if (scheduler == null)
        throw new ArgumentNullException("scheduler");
      Task<TAntecedentResult>[] tasksCopy = TaskFactory.CheckMultiContinuationTasksAndCopy<TAntecedentResult>(tasks);
      if (cancellationToken.IsCancellationRequested && (continuationOptions & TaskContinuationOptions.LazyCancellation) == TaskContinuationOptions.None)
        return TaskFactory<TResult>.CreateCanceledTask(continuationOptions, cancellationToken);
      Task<Task<TAntecedentResult>[]> task = TaskFactory.CommonCWAllLogic<TAntecedentResult>(tasksCopy);
      if (continuationFunction != null)
        return task.ContinueWith<TResult>(GenericDelegateCache<TAntecedentResult, TResult>.CWAllFuncDelegate, (object) continuationFunction, scheduler, cancellationToken, continuationOptions, ref stackMark);
      return task.ContinueWith<TResult>(GenericDelegateCache<TAntecedentResult, TResult>.CWAllActionDelegate, (object) continuationAction, scheduler, cancellationToken, continuationOptions, ref stackMark);
    }

    internal static Task<TResult> ContinueWhenAllImpl(Task[] tasks, Func<Task[], TResult> continuationFunction, Action<Task[]> continuationAction, TaskContinuationOptions continuationOptions, CancellationToken cancellationToken, TaskScheduler scheduler, ref StackCrawlMark stackMark)
    {
      TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
      if (tasks == null)
        throw new ArgumentNullException("tasks");
      if (scheduler == null)
        throw new ArgumentNullException("scheduler");
      Task[] tasksCopy = TaskFactory.CheckMultiContinuationTasksAndCopy(tasks);
      if (cancellationToken.IsCancellationRequested && (continuationOptions & TaskContinuationOptions.LazyCancellation) == TaskContinuationOptions.None)
        return TaskFactory<TResult>.CreateCanceledTask(continuationOptions, cancellationToken);
      Task<Task[]> task = TaskFactory.CommonCWAllLogic(tasksCopy);
      if (continuationFunction != null)
        return task.ContinueWith<TResult>((Func<Task<Task[]>, object, TResult>) ((completedTasks, state) =>
        {
          completedTasks.NotifyDebuggerOfWaitCompletionIfNecessary();
          return ((Func<Task[], TResult>) state)(completedTasks.Result);
        }), (object) continuationFunction, scheduler, cancellationToken, continuationOptions, ref stackMark);
      return task.ContinueWith<TResult>((Func<Task<Task[]>, object, TResult>) ((completedTasks, state) =>
      {
        completedTasks.NotifyDebuggerOfWaitCompletionIfNecessary();
        ((Action<Task[]>) state)(completedTasks.Result);
        return default (TResult);
      }), (object) continuationAction, scheduler, cancellationToken, continuationOptions, ref stackMark);
    }

    /// <summary>创建一个延续任务，它将在提供的组中的任何任务完成后马上开始。</summary>
    /// <returns>新的延续任务。</returns>
    /// <param name="tasks">在一个任务完成时继续执行的任务所在的数组。</param>
    /// <param name="continuationFunction">在 <paramref name="tasks" /> 数组中的一个任务完成时要异步执行的函数委托。</param>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <paramref name="tasks" /> 数组中的某个元素已被释放。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tasks" /> 数组为 null。- 或 -<paramref name="continuationFunction" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tasks" /> 数组包含 null 值或为空。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAny(Task[] tasks, Func<Task, TResult> continuationFunction)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException("continuationFunction");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, (Action<Task>) null, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>创建一个延续任务，它将在提供的组中的任何任务完成后马上开始。</summary>
    /// <returns>新的延续任务。</returns>
    /// <param name="tasks">在一个任务完成时继续执行的任务所在的数组。</param>
    /// <param name="continuationFunction">在 <paramref name="tasks" /> 数组中的一个任务完成时要异步执行的函数委托。</param>
    /// <param name="cancellationToken">将指派给新的延续任务的取消标记。</param>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <paramref name="tasks" /> 数组中的某个元素已被释放。- 或 -<see cref="T:System.Threading.CancellationTokenSource" /> 创建<paramref name=" cancellationToken" /> 已释放。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tasks" /> 数组参数为 null。- 或 -<paramref name="continuationFunction" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tasks" /> 数组包含 null 值。- 或 -<paramref name="tasks" /> 数组为空。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAny(Task[] tasks, Func<Task, TResult> continuationFunction, CancellationToken cancellationToken)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException("continuationFunction");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, (Action<Task>) null, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>创建一个延续任务，它将在提供的组中的任何任务完成后马上开始。</summary>
    /// <returns>新的延续任务。</returns>
    /// <param name="tasks">在一个任务完成时继续执行的任务所在的数组。</param>
    /// <param name="continuationFunction">在 <paramref name="tasks" /> 数组中的一个任务完成时要异步执行的函数委托。</param>
    /// <param name="continuationOptions">控制所创建的延续任务的行为的枚举值之一。NotOn* 或 OnlyOn* 值均无效。</param>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <paramref name="tasks" /> 数组中的某个元素已被释放。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tasks" /> 数组为 null。- 或 -<paramref name="continuationFunction" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="continuationOptions" /> 参数指定的枚举值无效。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tasks" /> 数组包含 null 值。- 或 -<paramref name="tasks" /> 数组为空。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAny(Task[] tasks, Func<Task, TResult> continuationFunction, TaskContinuationOptions continuationOptions)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException("continuationFunction");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, (Action<Task>) null, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>创建一个延续任务，它将在提供的组中的任何任务完成后马上开始。</summary>
    /// <returns>新的延续任务。</returns>
    /// <param name="tasks">在一个任务完成时继续执行的任务所在的数组。</param>
    /// <param name="continuationFunction">在 <paramref name="tasks" /> 数组中的一个任务完成时要异步执行的函数委托。</param>
    /// <param name="cancellationToken">将指派给新的延续任务的取消标记。</param>
    /// <param name="continuationOptions">控制所创建的延续任务的行为的枚举值之一。NotOn* 或 OnlyOn* 值均无效。</param>
    /// <param name="scheduler">用于计划所创建的延续任务的任务计划程序。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tasks" /> 数组为 null。- 或 -<paramref name="continuationFunction" /> 参数为 null。- 或 -<paramref name="scheduler" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tasks" /> 数组包含 null 值。- 或 -<paramref name="tasks" /> 数组为空。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="continuationOptions" /> 参数指定的 <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> 值无效。</exception>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <paramref name="tasks" /> 数组中的某个元素已被释放。- 或 -<see cref="T:System.Threading.CancellationTokenSource" /> 创建<paramref name=" cancellationToken" /> 已释放。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAny(Task[] tasks, Func<Task, TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException("continuationFunction");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, (Action<Task>) null, continuationOptions, cancellationToken, scheduler, ref stackMark);
    }

    /// <summary>创建一个延续任务，它将在提供的组中的任何任务完成后马上开始。</summary>
    /// <returns>新的延续 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    /// <param name="tasks">在一个任务完成时继续执行的任务所在的数组。</param>
    /// <param name="continuationFunction">在 <paramref name="tasks" /> 数组中的一个任务完成时要异步执行的函数委托。</param>
    /// <typeparam name="TAntecedentResult">以前的 <paramref name="tasks" /> 结果的类型。</typeparam>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <paramref name="tasks" /> 数组中的某个元素已被释放。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tasks" /> 数组为 null。- 或 -<paramref name="continuationFunction" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tasks" /> 数组包含 null 值。- 或 -<paramref name="tasks" /> 数组为空。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException("continuationFunction");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, (Action<Task<TAntecedentResult>>) null, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>创建一个延续任务，它将在提供的组中的任何任务完成后马上开始。</summary>
    /// <returns>新的延续任务。</returns>
    /// <param name="tasks">在一个任务完成时继续执行的任务所在的数组。</param>
    /// <param name="continuationFunction">在 <paramref name="tasks" /> 数组中的一个任务完成时要异步执行的函数委托。</param>
    /// <param name="cancellationToken">将指派给新的延续任务的取消标记。</param>
    /// <typeparam name="TAntecedentResult">以前的 <paramref name="tasks" /> 结果的类型。</typeparam>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <paramref name="tasks" /> 数组中的某个元素已被释放。- 或 -<see cref="T:System.Threading.CancellationTokenSource" /> 创建<paramref name=" cancellationToken" /> 已释放。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tasks" /> 数组为 null。- 或 -<paramref name="continuationFunction" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tasks" /> 数组包含 null 值。- 或 -<paramref name="tasks" /> 数组为空。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, CancellationToken cancellationToken)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException("continuationFunction");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, (Action<Task<TAntecedentResult>>) null, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>创建一个延续任务，它将在提供的组中的任何任务完成后马上开始。</summary>
    /// <returns>新的延续 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    /// <param name="tasks">在一个任务完成时继续执行的任务所在的数组。</param>
    /// <param name="continuationFunction">在 <paramref name="tasks" /> 数组中的一个任务完成时要异步执行的函数委托。</param>
    /// <param name="continuationOptions">控制所创建的延续任务的行为的枚举值之一。NotOn* 或 OnlyOn* 值均无效。</param>
    /// <typeparam name="TAntecedentResult">以前的 <paramref name="tasks" /> 结果的类型。</typeparam>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <paramref name="tasks" /> 数组中的某个元素已被释放。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tasks" /> 数组为 null。- 或 -<paramref name="continuationFunction" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="continuationOptions" /> 参数指定的枚举值无效。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tasks" /> 数组包含 null 值。- 或 -<paramref name="tasks" /> 数组为空。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, TaskContinuationOptions continuationOptions)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException("continuationFunction");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, (Action<Task<TAntecedentResult>>) null, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackMark);
    }

    /// <summary>创建一个延续任务，它将在提供的组中的任何任务完成后马上开始。</summary>
    /// <returns>新的延续 <see cref="T:System.Threading.Tasks.Task`1" />。</returns>
    /// <param name="tasks">在一个任务完成时继续执行的任务所在的数组。</param>
    /// <param name="continuationFunction">在 <paramref name="tasks" /> 数组中的一个任务完成时要异步执行的函数委托。</param>
    /// <param name="cancellationToken">将指派给新的延续任务的取消标记。</param>
    /// <param name="continuationOptions">控制所创建的延续任务的行为的枚举值之一。NotOn* 或 OnlyOn* 值均无效。</param>
    /// <param name="scheduler">用于计划所创建的延续 <see cref="T:System.Threading.Tasks.TaskScheduler" /> 的 <see cref="T:System.Threading.Tasks.Task`1" />。</param>
    /// <typeparam name="TAntecedentResult">以前的 <paramref name="tasks" /> 结果的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tasks" /> 数组为 null。- 或 -<paramref name="continuationFunction" /> 参数为 null。- 或 -<paramref name="scheduler" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tasks" /> 数组包含 null 值。- 或 -<paramref name="tasks" /> 数组为空。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="continuationOptions" /> 参数指定的 TaskContinuationOptions 值无效。</exception>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <paramref name="tasks" /> 数组中的某个元素已被释放。- 或 -<see cref="T:System.Threading.CancellationTokenSource" /> 创建<paramref name=" cancellationToken" /> 已释放。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Task<TResult> ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
    {
      if (continuationFunction == null)
        throw new ArgumentNullException("continuationFunction");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, (Action<Task<TAntecedentResult>>) null, continuationOptions, cancellationToken, scheduler, ref stackMark);
    }

    internal static Task<TResult> ContinueWhenAnyImpl(Task[] tasks, Func<Task, TResult> continuationFunction, Action<Task> continuationAction, TaskContinuationOptions continuationOptions, CancellationToken cancellationToken, TaskScheduler scheduler, ref StackCrawlMark stackMark)
    {
      TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
      if (tasks == null)
        throw new ArgumentNullException("tasks");
      if (tasks.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_EmptyTaskList"), "tasks");
      if (scheduler == null)
        throw new ArgumentNullException("scheduler");
      Task<Task> task1 = TaskFactory.CommonCWAnyLogic((IList<Task>) tasks);
      if (cancellationToken.IsCancellationRequested && (continuationOptions & TaskContinuationOptions.LazyCancellation) == TaskContinuationOptions.None)
        return TaskFactory<TResult>.CreateCanceledTask(continuationOptions, cancellationToken);
      if (continuationFunction == null)
        return task1.ContinueWith<TResult>((Func<Task<Task>, object, TResult>) ((completedTask, state) =>
        {
          ((Action<Task>) state)(completedTask.Result);
          return default (TResult);
        }), (object) continuationAction, scheduler, cancellationToken, continuationOptions, ref stackMark);
      Task<Task> task2 = task1;
      Func<Task, TResult> func = continuationFunction;
      TaskScheduler scheduler1 = scheduler;
      CancellationToken cancellationToken1 = cancellationToken;
      int num = (int) continuationOptions;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      StackCrawlMark& stackMark1 = @stackMark;
      return task2.ContinueWith<TResult>((Func<Task<Task>, object, TResult>) ((completedTask, state) => ((Func<Task, TResult>) state)(completedTask.Result)), (object) func, scheduler1, cancellationToken1, (TaskContinuationOptions) num, stackMark1);
    }

    internal static Task<TResult> ContinueWhenAnyImpl<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, Action<Task<TAntecedentResult>> continuationAction, TaskContinuationOptions continuationOptions, CancellationToken cancellationToken, TaskScheduler scheduler, ref StackCrawlMark stackMark)
    {
      TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
      if (tasks == null)
        throw new ArgumentNullException("tasks");
      if (tasks.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_EmptyTaskList"), "tasks");
      if (scheduler == null)
        throw new ArgumentNullException("scheduler");
      Task<Task> task = TaskFactory.CommonCWAnyLogic((IList<Task>) tasks);
      if (cancellationToken.IsCancellationRequested && (continuationOptions & TaskContinuationOptions.LazyCancellation) == TaskContinuationOptions.None)
        return TaskFactory<TResult>.CreateCanceledTask(continuationOptions, cancellationToken);
      if (continuationFunction != null)
        return task.ContinueWith<TResult>(GenericDelegateCache<TAntecedentResult, TResult>.CWAnyFuncDelegate, (object) continuationFunction, scheduler, cancellationToken, continuationOptions, ref stackMark);
      return task.ContinueWith<TResult>(GenericDelegateCache<TAntecedentResult, TResult>.CWAnyActionDelegate, (object) continuationAction, scheduler, cancellationToken, continuationOptions, ref stackMark);
    }

    private sealed class FromAsyncTrimPromise<TInstance> : Task<TResult> where TInstance : class
    {
      internal static readonly AsyncCallback s_completeFromAsyncResult = new AsyncCallback(TaskFactory<TResult>.FromAsyncTrimPromise<TInstance>.CompleteFromAsyncResult);
      private TInstance m_thisRef;
      private Func<TInstance, IAsyncResult, TResult> m_endMethod;

      internal FromAsyncTrimPromise(TInstance thisRef, Func<TInstance, IAsyncResult, TResult> endMethod)
      {
        this.m_thisRef = thisRef;
        this.m_endMethod = endMethod;
      }

      internal static void CompleteFromAsyncResult(IAsyncResult asyncResult)
      {
        if (asyncResult == null)
          throw new ArgumentNullException("asyncResult");
        TaskFactory<TResult>.FromAsyncTrimPromise<TInstance> asyncTrimPromise = asyncResult.AsyncState as TaskFactory<TResult>.FromAsyncTrimPromise<TInstance>;
        if (asyncTrimPromise == null)
          throw new ArgumentException(Environment.GetResourceString("InvalidOperation_WrongAsyncResultOrEndCalledMultiple"), "asyncResult");
        TInstance thisRef = asyncTrimPromise.m_thisRef;
        Func<TInstance, IAsyncResult, TResult> endMethod = asyncTrimPromise.m_endMethod;
        asyncTrimPromise.m_thisRef = default (TInstance);
        asyncTrimPromise.m_endMethod = (Func<TInstance, IAsyncResult, TResult>) null;
        if (endMethod == null)
          throw new ArgumentException(Environment.GetResourceString("InvalidOperation_WrongAsyncResultOrEndCalledMultiple"), "asyncResult");
        if (asyncResult.CompletedSynchronously)
          return;
        asyncTrimPromise.Complete(thisRef, endMethod, asyncResult, true);
      }

      internal void Complete(TInstance thisRef, Func<TInstance, IAsyncResult, TResult> endMethod, IAsyncResult asyncResult, bool requiresSynchronization)
      {
        try
        {
          TResult result = endMethod(thisRef, asyncResult);
          if (requiresSynchronization)
            this.TrySetResult(result);
          else
            this.DangerousSetResult(result);
        }
        catch (OperationCanceledException ex)
        {
          this.TrySetCanceled(ex.CancellationToken, (object) ex);
        }
        catch (Exception ex)
        {
          this.TrySetException((object) ex);
        }
      }
    }
  }
}
