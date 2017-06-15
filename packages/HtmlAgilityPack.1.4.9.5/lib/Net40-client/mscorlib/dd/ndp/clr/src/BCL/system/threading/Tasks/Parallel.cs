// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.Parallel
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Security.Permissions;

namespace System.Threading.Tasks
{
  /// <summary>提供对并行循环和区域的支持。</summary>
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public static class Parallel
  {
    internal static ParallelOptions s_defaultParallelOptions = new ParallelOptions();
    internal static int s_forkJoinContextID;
    internal const int DEFAULT_LOOP_STRIDE = 16;

    /// <summary>尽可能并行执行提供的每个操作。</summary>
    /// <param name="actions">要执行的 <see cref="T:System.Action" /> 数组。</param>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="actions" /> argument is null.</exception>
    /// <exception cref="T:System.AggregateException">The exception that is thrown when any action in the <paramref name="actions" /> array throws an exception.</exception>
    /// <exception cref="T:System.ArgumentException">The <paramref name="actions" /> array contains a null element.</exception>
    [__DynamicallyInvokable]
    public static void Invoke(params Action[] actions)
    {
      Parallel.Invoke(Parallel.s_defaultParallelOptions, actions);
    }

    /// <summary>执行所提供的每个操作，而且尽可能并行运行，除非用户取消了操作。</summary>
    /// <param name="parallelOptions">一个对象，用于配置此操作的行为。</param>
    /// <param name="actions">要执行的操作数组。</param>
    /// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> is set.</exception>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="actions" /> argument is null.-or-The <paramref name="parallelOptions" /> argument is null.</exception>
    /// <exception cref="T:System.AggregateException">The exception that is thrown when any action in the <paramref name="actions" /> array throws an exception.</exception>
    /// <exception cref="T:System.ArgumentException">The <paramref name="actions" /> array contains a null element.</exception>
    /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
    [__DynamicallyInvokable]
    public static void Invoke(ParallelOptions parallelOptions, params Action[] actions)
    {
      if (actions == null)
        throw new ArgumentNullException("actions");
      if (parallelOptions == null)
        throw new ArgumentNullException("parallelOptions");
      CancellationToken cancellationToken = parallelOptions.CancellationToken;
      if (cancellationToken.CanBeCanceled && AppContextSwitches.ThrowExceptionIfDisposedCancellationTokenSource)
      {
        cancellationToken = parallelOptions.CancellationToken;
        cancellationToken.ThrowIfSourceDisposed();
      }
      cancellationToken = parallelOptions.CancellationToken;
      if (cancellationToken.IsCancellationRequested)
        throw new OperationCanceledException(parallelOptions.CancellationToken);
      Action[] actionsCopy = new Action[actions.Length];
      for (int index = 0; index < actionsCopy.Length; ++index)
      {
        actionsCopy[index] = actions[index];
        if (actionsCopy[index] == null)
          throw new ArgumentException(Environment.GetResourceString("Parallel_Invoke_ActionNull"));
      }
      int ForkJoinContextID = 0;
      Task task = (Task) null;
      if (TplEtwProvider.Log.IsEnabled())
      {
        ForkJoinContextID = Interlocked.Increment(ref Parallel.s_forkJoinContextID);
        task = Task.InternalCurrent;
        TplEtwProvider.Log.ParallelInvokeBegin(task != null ? task.m_taskScheduler.Id : TaskScheduler.Current.Id, task != null ? task.Id : 0, ForkJoinContextID, TplEtwProvider.ForkJoinOperationType.ParallelInvoke, actionsCopy.Length);
      }
      if (actionsCopy.Length < 1)
        return;
      try
      {
        if (actionsCopy.Length > 10 || parallelOptions.MaxDegreeOfParallelism != -1 && parallelOptions.MaxDegreeOfParallelism < actionsCopy.Length)
        {
          ConcurrentQueue<Exception> exceptionQ = (ConcurrentQueue<Exception>) null;
          try
          {
            int actionIndex = 0;
            ParallelForReplicatingTask forReplicatingTask = new ParallelForReplicatingTask(parallelOptions, (Action) (() =>
            {
              for (int index = Interlocked.Increment(ref actionIndex); index <= actionsCopy.Length; index = Interlocked.Increment(ref actionIndex))
              {
                try
                {
                  actionsCopy[index - 1]();
                }
                catch (Exception ex)
                {
                  LazyInitializer.EnsureInitialized<ConcurrentQueue<Exception>>(ref exceptionQ, (Func<ConcurrentQueue<Exception>>) (() => new ConcurrentQueue<Exception>()));
                  exceptionQ.Enqueue(ex);
                }
                if (parallelOptions.CancellationToken.IsCancellationRequested)
                  throw new OperationCanceledException(parallelOptions.CancellationToken);
              }
            }), TaskCreationOptions.None, InternalTaskOptions.SelfReplicating);
            TaskScheduler effectiveTaskScheduler = parallelOptions.EffectiveTaskScheduler;
            forReplicatingTask.RunSynchronously(effectiveTaskScheduler);
            forReplicatingTask.Wait();
          }
          catch (Exception ex)
          {
            LazyInitializer.EnsureInitialized<ConcurrentQueue<Exception>>(ref exceptionQ, (Func<ConcurrentQueue<Exception>>) (() => new ConcurrentQueue<Exception>()));
            AggregateException aggregateException = ex as AggregateException;
            if (aggregateException != null)
            {
              foreach (Exception innerException in aggregateException.InnerExceptions)
                exceptionQ.Enqueue(innerException);
            }
            else
              exceptionQ.Enqueue(ex);
          }
          if (exceptionQ != null && exceptionQ.Count > 0)
          {
            Parallel.ThrowIfReducableToSingleOCE((IEnumerable<Exception>) exceptionQ, parallelOptions.CancellationToken);
            throw new AggregateException((IEnumerable<Exception>) exceptionQ);
          }
        }
        else
        {
          Task[] tasks = new Task[actionsCopy.Length];
          cancellationToken = parallelOptions.CancellationToken;
          if (cancellationToken.IsCancellationRequested)
            throw new OperationCanceledException(parallelOptions.CancellationToken);
          for (int index = 1; index < tasks.Length; ++index)
            tasks[index] = Task.Factory.StartNew(actionsCopy[index], parallelOptions.CancellationToken, TaskCreationOptions.None, InternalTaskOptions.None, parallelOptions.EffectiveTaskScheduler);
          tasks[0] = new Task(actionsCopy[0]);
          tasks[0].RunSynchronously(parallelOptions.EffectiveTaskScheduler);
          try
          {
            if (tasks.Length <= 4)
              Task.FastWaitAll(tasks);
            else
              Task.WaitAll(tasks);
          }
          catch (AggregateException ex)
          {
            Parallel.ThrowIfReducableToSingleOCE((IEnumerable<Exception>) ex.InnerExceptions, parallelOptions.CancellationToken);
            throw;
          }
          finally
          {
            for (int index = 0; index < tasks.Length; ++index)
            {
              if (tasks[index].IsCompleted)
                tasks[index].Dispose();
            }
          }
        }
      }
      finally
      {
        if (TplEtwProvider.Log.IsEnabled())
          TplEtwProvider.Log.ParallelInvokeEnd(task != null ? task.m_taskScheduler.Id : TaskScheduler.Current.Id, task != null ? task.Id : 0, ForkJoinContextID);
      }
    }

    /// <summary>执行 for（在 Visual Basic 中为 For）循环，其中可能会并行运行迭代。</summary>
    /// <returns>包含有关已完成的循环部分的信息的结构。</returns>
    /// <param name="fromInclusive">开始索引（含）。</param>
    /// <param name="toExclusive">结束索引（不含）。</param>
    /// <param name="body">将为每个迭代调用一次的委托。</param>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="body" /> argument is null.</exception>
    /// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
    [__DynamicallyInvokable]
    public static ParallelLoopResult For(int fromInclusive, int toExclusive, Action<int> body)
    {
      if (body == null)
        throw new ArgumentNullException("body");
      return Parallel.ForWorker<object>(fromInclusive, toExclusive, Parallel.s_defaultParallelOptions, body, (Action<int, ParallelLoopState>) null, (Func<int, ParallelLoopState, object, object>) null, (Func<object>) null, (Action<object>) null);
    }

    /// <summary>执行具有 64 位索引的 for（在 Visual Basic 中为 For）循环，其中可能会并行运行迭代。</summary>
    /// <returns>包含有关已完成的循环部分的信息的结构。</returns>
    /// <param name="fromInclusive">开始索引（含）。</param>
    /// <param name="toExclusive">结束索引（不含）。</param>
    /// <param name="body">将为每个迭代调用一次的委托。</param>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="body" /> argument is null.</exception>
    /// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
    [__DynamicallyInvokable]
    public static ParallelLoopResult For(long fromInclusive, long toExclusive, Action<long> body)
    {
      if (body == null)
        throw new ArgumentNullException("body");
      return Parallel.ForWorker64<object>(fromInclusive, toExclusive, Parallel.s_defaultParallelOptions, body, (Action<long, ParallelLoopState>) null, (Func<long, ParallelLoopState, object, object>) null, (Func<object>) null, (Action<object>) null);
    }

    /// <summary>执行 for（在 Visual Basic 中为 For）循环，其中可能会并行运行迭代，而且可以配置循环选项。</summary>
    /// <returns>包含有关已完成的循环部分的信息的结构。</returns>
    /// <param name="fromInclusive">开始索引（含）。</param>
    /// <param name="toExclusive">结束索引（不含）。</param>
    /// <param name="parallelOptions">一个对象，用于配置此操作的行为。</param>
    /// <param name="body">将为每个迭代调用一次的委托。</param>
    /// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled.</exception>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="body" /> argument is null.-or-The <paramref name="parallelOptions" /> argument is null.</exception>
    /// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
    /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
    [__DynamicallyInvokable]
    public static ParallelLoopResult For(int fromInclusive, int toExclusive, ParallelOptions parallelOptions, Action<int> body)
    {
      if (body == null)
        throw new ArgumentNullException("body");
      if (parallelOptions == null)
        throw new ArgumentNullException("parallelOptions");
      return Parallel.ForWorker<object>(fromInclusive, toExclusive, parallelOptions, body, (Action<int, ParallelLoopState>) null, (Func<int, ParallelLoopState, object, object>) null, (Func<object>) null, (Action<object>) null);
    }

    /// <summary>执行具有 64 位索引的 for（在 Visual Basic 中为 For）循环，其中可能会并行运行迭代，而且可以配置循环选项。</summary>
    /// <returns>包含有关已完成的循环部分的信息的结构。</returns>
    /// <param name="fromInclusive">开始索引（含）。</param>
    /// <param name="toExclusive">结束索引（不含）。</param>
    /// <param name="parallelOptions">一个对象，用于配置此操作的行为。</param>
    /// <param name="body">将为每个迭代调用一次的委托。</param>
    /// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled.</exception>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="body" /> argument is null.-or-The <paramref name="parallelOptions" /> argument is null.</exception>
    /// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
    /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
    [__DynamicallyInvokable]
    public static ParallelLoopResult For(long fromInclusive, long toExclusive, ParallelOptions parallelOptions, Action<long> body)
    {
      if (body == null)
        throw new ArgumentNullException("body");
      if (parallelOptions == null)
        throw new ArgumentNullException("parallelOptions");
      return Parallel.ForWorker64<object>(fromInclusive, toExclusive, parallelOptions, body, (Action<long, ParallelLoopState>) null, (Func<long, ParallelLoopState, object, object>) null, (Func<object>) null, (Action<object>) null);
    }

    /// <summary>执行 for（在 Visual Basic 中为 For）循环，其中可能会并行运行迭代，而且可以监视和操作循环的状态。 </summary>
    /// <returns>包含有关已完成的循环部分的信息的结构。</returns>
    /// <param name="fromInclusive">开始索引（含）。</param>
    /// <param name="toExclusive">结束索引（不含）。</param>
    /// <param name="body">将为每个迭代调用一次的委托。</param>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="body" /> argument is null.</exception>
    /// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
    [__DynamicallyInvokable]
    public static ParallelLoopResult For(int fromInclusive, int toExclusive, Action<int, ParallelLoopState> body)
    {
      if (body == null)
        throw new ArgumentNullException("body");
      return Parallel.ForWorker<object>(fromInclusive, toExclusive, Parallel.s_defaultParallelOptions, (Action<int>) null, body, (Func<int, ParallelLoopState, object, object>) null, (Func<object>) null, (Action<object>) null);
    }

    /// <summary>执行具有 64 位索引的 for（在 Visual Basic 中为 For）循环，其中可能会并行运行迭代，而且可以监视和操作循环的状态。</summary>
    /// <returns>一个 <see cref="T:System.Threading.Tasks.ParallelLoopResult" /> 结构，其中包含有关已完成的循环部分的信息。</returns>
    /// <param name="fromInclusive">开始索引（含）。</param>
    /// <param name="toExclusive">结束索引（不含）。</param>
    /// <param name="body">将为每个迭代调用一次的委托。</param>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="body" /> argument is null.</exception>
    /// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
    [__DynamicallyInvokable]
    public static ParallelLoopResult For(long fromInclusive, long toExclusive, Action<long, ParallelLoopState> body)
    {
      if (body == null)
        throw new ArgumentNullException("body");
      return Parallel.ForWorker64<object>(fromInclusive, toExclusive, Parallel.s_defaultParallelOptions, (Action<long>) null, body, (Func<long, ParallelLoopState, object, object>) null, (Func<object>) null, (Action<object>) null);
    }

    /// <summary>执行 for（在 Visual Basic 中为 For）循环，其中可能会并行运行迭代，而且可以配置循环选项，可以监视和操作循环的状态。</summary>
    /// <returns>包含有关已完成的循环部分的信息的结构。</returns>
    /// <param name="fromInclusive">开始索引（含）。</param>
    /// <param name="toExclusive">结束索引（不含）。</param>
    /// <param name="parallelOptions">一个对象，用于配置此操作的行为。</param>
    /// <param name="body">将为每个迭代调用一次的委托。</param>
    /// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled.</exception>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="body" /> argument is null.-or-The <paramref name="parallelOptions" /> argument is null.</exception>
    /// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
    /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
    [__DynamicallyInvokable]
    public static ParallelLoopResult For(int fromInclusive, int toExclusive, ParallelOptions parallelOptions, Action<int, ParallelLoopState> body)
    {
      if (body == null)
        throw new ArgumentNullException("body");
      if (parallelOptions == null)
        throw new ArgumentNullException("parallelOptions");
      return Parallel.ForWorker<object>(fromInclusive, toExclusive, parallelOptions, (Action<int>) null, body, (Func<int, ParallelLoopState, object, object>) null, (Func<object>) null, (Action<object>) null);
    }

    /// <summary>执行具有 64 位索引的 for（在 Visual Basic 中为 For）循环，其中可能会并行运行迭代，而且可以配置循环选项，可以监视和操作循环的状态。</summary>
    /// <returns>包含有关已完成的循环部分的信息的结构。</returns>
    /// <param name="fromInclusive">开始索引（含）。</param>
    /// <param name="toExclusive">结束索引（不含）。</param>
    /// <param name="parallelOptions">一个对象，用于配置此操作的行为。</param>
    /// <param name="body">将为每个迭代调用一次的委托。</param>
    /// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled.</exception>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="body" /> argument is null.-or-The <paramref name="parallelOptions" /> argument is null.</exception>
    /// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
    /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
    [__DynamicallyInvokable]
    public static ParallelLoopResult For(long fromInclusive, long toExclusive, ParallelOptions parallelOptions, Action<long, ParallelLoopState> body)
    {
      if (body == null)
        throw new ArgumentNullException("body");
      if (parallelOptions == null)
        throw new ArgumentNullException("parallelOptions");
      return Parallel.ForWorker64<object>(fromInclusive, toExclusive, parallelOptions, (Action<long>) null, body, (Func<long, ParallelLoopState, object, object>) null, (Func<object>) null, (Action<object>) null);
    }

    /// <summary>执行具有线程本地数据的 for（在 Visual Basic 中为 For）循环，其中可能会并行运行迭代，而且可以监视和操作循环的状态。</summary>
    /// <returns>包含有关已完成的循环部分的信息的结构。</returns>
    /// <param name="fromInclusive">开始索引（含）。</param>
    /// <param name="toExclusive">结束索引（不含）。</param>
    /// <param name="localInit">用于返回每个任务的本地数据的初始状态的函数委托。</param>
    /// <param name="body">将为每个迭代调用一次的委托。</param>
    /// <param name="localFinally">用于对每个任务的本地状态执行一个最终操作的委托。</param>
    /// <typeparam name="TLocal">线程本地数据的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="body" /> argument is null.-or-The <paramref name="localInit" /> argument is null.-or-The <paramref name="localFinally" /> argument is null.</exception>
    /// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
    [__DynamicallyInvokable]
    public static ParallelLoopResult For<TLocal>(int fromInclusive, int toExclusive, Func<TLocal> localInit, Func<int, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
    {
      if (body == null)
        throw new ArgumentNullException("body");
      if (localInit == null)
        throw new ArgumentNullException("localInit");
      if (localFinally == null)
        throw new ArgumentNullException("localFinally");
      return Parallel.ForWorker<TLocal>(fromInclusive, toExclusive, Parallel.s_defaultParallelOptions, (Action<int>) null, (Action<int, ParallelLoopState>) null, body, localInit, localFinally);
    }

    /// <summary>执行具有 64 位索引和线程本地数据的 for（在 Visual Basic 中为 For）循环，其中可能会并行运行迭代，而且可以监视和操作循环的状态。</summary>
    /// <returns>包含有关已完成的循环部分的信息的结构。</returns>
    /// <param name="fromInclusive">开始索引（含）。</param>
    /// <param name="toExclusive">结束索引（不含）。</param>
    /// <param name="localInit">用于返回每个任务的本地数据的初始状态的函数委托。</param>
    /// <param name="body">将为每个迭代调用一次的委托。</param>
    /// <param name="localFinally">用于对每个任务的本地状态执行一个最终操作的委托。</param>
    /// <typeparam name="TLocal">线程本地数据的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="body" /> argument is null.-or-The <paramref name="localInit" /> argument is null.-or-The <paramref name="localFinally" /> argument is null.</exception>
    /// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
    [__DynamicallyInvokable]
    public static ParallelLoopResult For<TLocal>(long fromInclusive, long toExclusive, Func<TLocal> localInit, Func<long, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
    {
      if (body == null)
        throw new ArgumentNullException("body");
      if (localInit == null)
        throw new ArgumentNullException("localInit");
      if (localFinally == null)
        throw new ArgumentNullException("localFinally");
      return Parallel.ForWorker64<TLocal>(fromInclusive, toExclusive, Parallel.s_defaultParallelOptions, (Action<long>) null, (Action<long, ParallelLoopState>) null, body, localInit, localFinally);
    }

    /// <summary>执行具有线程本地数据的 for（在 Visual Basic 中为 For）循环，其中可能会并行运行迭代，而且可以配置循环选项，可以监视和操作循环的状态。</summary>
    /// <returns>包含有关已完成的循环部分的信息的结构。</returns>
    /// <param name="fromInclusive">开始索引（含）。</param>
    /// <param name="toExclusive">结束索引（不含）。</param>
    /// <param name="parallelOptions">一个对象，用于配置此操作的行为。</param>
    /// <param name="localInit">用于返回每个任务的本地数据的初始状态的函数委托。</param>
    /// <param name="body">将为每个迭代调用一次的委托。</param>
    /// <param name="localFinally">用于对每个任务的本地状态执行一个最终操作的委托。</param>
    /// <typeparam name="TLocal">线程本地数据的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="body" /> argument is null.-or-The <paramref name="localInit" /> argument is null.-or-The <paramref name="localFinally" /> argument is null.-or-The <paramref name="parallelOptions" /> argument is null.</exception>
    /// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled.</exception>
    /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
    /// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
    [__DynamicallyInvokable]
    public static ParallelLoopResult For<TLocal>(int fromInclusive, int toExclusive, ParallelOptions parallelOptions, Func<TLocal> localInit, Func<int, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
    {
      if (body == null)
        throw new ArgumentNullException("body");
      if (localInit == null)
        throw new ArgumentNullException("localInit");
      if (localFinally == null)
        throw new ArgumentNullException("localFinally");
      if (parallelOptions == null)
        throw new ArgumentNullException("parallelOptions");
      return Parallel.ForWorker<TLocal>(fromInclusive, toExclusive, parallelOptions, (Action<int>) null, (Action<int, ParallelLoopState>) null, body, localInit, localFinally);
    }

    /// <summary>执行具有 64 位索引和线程本地数据的 for（在 Visual Basic 中为 For）循环，其中可能会并行运行迭代，而且可以配置循环选项，可以监视和操作循环的状态。</summary>
    /// <returns>包含有关已完成的循环部分的信息的结构。</returns>
    /// <param name="fromInclusive">开始索引（含）。</param>
    /// <param name="toExclusive">结束索引（不含）。</param>
    /// <param name="parallelOptions">一个对象，用于配置此操作的行为。</param>
    /// <param name="localInit">用于返回每个线程的本地数据的初始状态的函数委托。</param>
    /// <param name="body">将为每个迭代调用一次的委托。</param>
    /// <param name="localFinally">用于对每个线程的本地状态执行一个最终操作的委托。</param>
    /// <typeparam name="TLocal">线程本地数据的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="body" /> argument is null.-or-The <paramref name="localInit" /> argument is null.-or-The <paramref name="localFinally" /> argument is null.-or-The <paramref name="parallelOptions" /> argument is null.</exception>
    /// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled.</exception>
    /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
    /// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
    [__DynamicallyInvokable]
    public static ParallelLoopResult For<TLocal>(long fromInclusive, long toExclusive, ParallelOptions parallelOptions, Func<TLocal> localInit, Func<long, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
    {
      if (body == null)
        throw new ArgumentNullException("body");
      if (localInit == null)
        throw new ArgumentNullException("localInit");
      if (localFinally == null)
        throw new ArgumentNullException("localFinally");
      if (parallelOptions == null)
        throw new ArgumentNullException("parallelOptions");
      return Parallel.ForWorker64<TLocal>(fromInclusive, toExclusive, parallelOptions, (Action<long>) null, (Action<long, ParallelLoopState>) null, body, localInit, localFinally);
    }

    private static ParallelLoopResult ForWorker<TLocal>(int fromInclusive, int toExclusive, ParallelOptions parallelOptions, Action<int> body, Action<int, ParallelLoopState> bodyWithState, Func<int, ParallelLoopState, TLocal, TLocal> bodyWithLocal, Func<TLocal> localInit, Action<TLocal> localFinally)
    {
      ParallelLoopResult parallelLoopResult = new ParallelLoopResult();
      if (toExclusive <= fromInclusive)
      {
        parallelLoopResult.m_completed = true;
        return parallelLoopResult;
      }
      ParallelLoopStateFlags32 sharedPStateFlags = new ParallelLoopStateFlags32();
      TaskCreationOptions creationOptions = TaskCreationOptions.None;
      InternalTaskOptions internalOptions = InternalTaskOptions.SelfReplicating;
      if (parallelOptions.CancellationToken.IsCancellationRequested)
        throw new OperationCanceledException(parallelOptions.CancellationToken);
      int nNumExpectedWorkers = parallelOptions.EffectiveMaxConcurrencyLevel == -1 ? PlatformHelper.ProcessorCount : parallelOptions.EffectiveMaxConcurrencyLevel;
      RangeManager rangeManager = new RangeManager((long) fromInclusive, (long) toExclusive, 1L, nNumExpectedWorkers);
      OperationCanceledException oce = (OperationCanceledException) null;
      CancellationTokenRegistration tokenRegistration = new CancellationTokenRegistration();
      CancellationToken cancellationToken;
      if (parallelOptions.CancellationToken.CanBeCanceled)
      {
        cancellationToken = parallelOptions.CancellationToken;
        tokenRegistration = cancellationToken.InternalRegisterWithoutEC((Action<object>) (o =>
        {
          sharedPStateFlags.Cancel();
          oce = new OperationCanceledException(parallelOptions.CancellationToken);
        }), (object) null);
      }
      int forkJoinContextID = 0;
      Task task = (Task) null;
      if (TplEtwProvider.Log.IsEnabled())
      {
        forkJoinContextID = Interlocked.Increment(ref Parallel.s_forkJoinContextID);
        task = Task.InternalCurrent;
        TplEtwProvider.Log.ParallelLoopBegin(task != null ? task.m_taskScheduler.Id : TaskScheduler.Current.Id, task != null ? task.Id : 0, forkJoinContextID, TplEtwProvider.ForkJoinOperationType.ParallelFor, (long) fromInclusive, (long) toExclusive);
      }
      ParallelForReplicatingTask rootTask = (ParallelForReplicatingTask) null;
      try
      {
        rootTask = new ParallelForReplicatingTask(parallelOptions, (Action) (() =>
        {
          Task internalCurrent = Task.InternalCurrent;
          bool flag1 = internalCurrent == rootTask;
          RangeWorker rangeWorker1 = new RangeWorker();
          object fromPreviousReplica = internalCurrent.SavedStateFromPreviousReplica;
          RangeWorker rangeWorker2 = !(fromPreviousReplica is RangeWorker) ? rangeManager.RegisterNewWorker() : (RangeWorker) fromPreviousReplica;
          int nFromInclusiveLocal32;
          int nToExclusiveLocal32;
          if (!rangeWorker2.FindNewWork32(out nFromInclusiveLocal32, out nToExclusiveLocal32) || sharedPStateFlags.ShouldExitLoop(nFromInclusiveLocal32))
            return;
          if (TplEtwProvider.Log.IsEnabled())
            TplEtwProvider.Log.ParallelFork(internalCurrent != null ? internalCurrent.m_taskScheduler.Id : TaskScheduler.Current.Id, internalCurrent != null ? internalCurrent.Id : 0, forkJoinContextID);
          TLocal local = default (TLocal);
          bool flag2 = false;
          try
          {
            ParallelLoopState32 parallelLoopState32 = (ParallelLoopState32) null;
            if (bodyWithState != null)
              parallelLoopState32 = new ParallelLoopState32(sharedPStateFlags);
            else if (bodyWithLocal != null)
            {
              parallelLoopState32 = new ParallelLoopState32(sharedPStateFlags);
              if (localInit != null)
              {
                local = localInit();
                flag2 = true;
              }
            }
            Parallel.LoopTimer loopTimer = new Parallel.LoopTimer(rootTask.ActiveChildCount);
            do
            {
              if (body != null)
              {
                for (int index = nFromInclusiveLocal32; index < nToExclusiveLocal32 && (sharedPStateFlags.LoopStateFlags == ParallelLoopStateFlags.PLS_NONE || !sharedPStateFlags.ShouldExitLoop()); ++index)
                  body(index);
              }
              else if (bodyWithState != null)
              {
                for (int CallerIteration = nFromInclusiveLocal32; CallerIteration < nToExclusiveLocal32 && (sharedPStateFlags.LoopStateFlags == ParallelLoopStateFlags.PLS_NONE || !sharedPStateFlags.ShouldExitLoop(CallerIteration)); ++CallerIteration)
                {
                  parallelLoopState32.CurrentIteration = CallerIteration;
                  bodyWithState(CallerIteration, (ParallelLoopState) parallelLoopState32);
                }
              }
              else
              {
                for (int CallerIteration = nFromInclusiveLocal32; CallerIteration < nToExclusiveLocal32 && (sharedPStateFlags.LoopStateFlags == ParallelLoopStateFlags.PLS_NONE || !sharedPStateFlags.ShouldExitLoop(CallerIteration)); ++CallerIteration)
                {
                  parallelLoopState32.CurrentIteration = CallerIteration;
                  local = bodyWithLocal(CallerIteration, (ParallelLoopState) parallelLoopState32, local);
                }
              }
              if (!flag1 && loopTimer.LimitExceeded())
              {
                internalCurrent.SavedStateForNextReplica = (object) rangeWorker2;
                break;
              }
            }
            while (rangeWorker2.FindNewWork32(out nFromInclusiveLocal32, out nToExclusiveLocal32) && (sharedPStateFlags.LoopStateFlags == ParallelLoopStateFlags.PLS_NONE || !sharedPStateFlags.ShouldExitLoop(nFromInclusiveLocal32)));
          }
          catch
          {
            sharedPStateFlags.SetExceptional();
            throw;
          }
          finally
          {
            if (localFinally != null & flag2)
              localFinally(local);
            if (TplEtwProvider.Log.IsEnabled())
              TplEtwProvider.Log.ParallelJoin(internalCurrent != null ? internalCurrent.m_taskScheduler.Id : TaskScheduler.Current.Id, internalCurrent != null ? internalCurrent.Id : 0, forkJoinContextID);
          }
        }), creationOptions, internalOptions);
        rootTask.RunSynchronously(parallelOptions.EffectiveTaskScheduler);
        rootTask.Wait();
        cancellationToken = parallelOptions.CancellationToken;
        if (cancellationToken.CanBeCanceled)
          tokenRegistration.Dispose();
        if (oce != null)
          throw oce;
      }
      catch (AggregateException ex)
      {
        if (parallelOptions.CancellationToken.CanBeCanceled)
          tokenRegistration.Dispose();
        Parallel.ThrowIfReducableToSingleOCE((IEnumerable<Exception>) ex.InnerExceptions, parallelOptions.CancellationToken);
        throw;
      }
      catch (TaskSchedulerException ex)
      {
        if (parallelOptions.CancellationToken.CanBeCanceled)
          tokenRegistration.Dispose();
        throw;
      }
      finally
      {
        int loopStateFlags = sharedPStateFlags.LoopStateFlags;
        parallelLoopResult.m_completed = loopStateFlags == ParallelLoopStateFlags.PLS_NONE;
        if ((loopStateFlags & ParallelLoopStateFlags.PLS_BROKEN) != 0)
          parallelLoopResult.m_lowestBreakIteration = new long?((long) sharedPStateFlags.LowestBreakIteration);
        if (rootTask != null && rootTask.IsCompleted)
          rootTask.Dispose();
        if (TplEtwProvider.Log.IsEnabled())
        {
          int num = loopStateFlags != ParallelLoopStateFlags.PLS_NONE ? ((loopStateFlags & ParallelLoopStateFlags.PLS_BROKEN) == 0 ? -1 : sharedPStateFlags.LowestBreakIteration - fromInclusive) : toExclusive - fromInclusive;
          TplEtwProvider.Log.ParallelLoopEnd(task != null ? task.m_taskScheduler.Id : TaskScheduler.Current.Id, task != null ? task.Id : 0, forkJoinContextID, (long) num);
        }
      }
      return parallelLoopResult;
    }

    private static ParallelLoopResult ForWorker64<TLocal>(long fromInclusive, long toExclusive, ParallelOptions parallelOptions, Action<long> body, Action<long, ParallelLoopState> bodyWithState, Func<long, ParallelLoopState, TLocal, TLocal> bodyWithLocal, Func<TLocal> localInit, Action<TLocal> localFinally)
    {
      ParallelLoopResult parallelLoopResult = new ParallelLoopResult();
      if (toExclusive <= fromInclusive)
      {
        parallelLoopResult.m_completed = true;
        return parallelLoopResult;
      }
      ParallelLoopStateFlags64 sharedPStateFlags = new ParallelLoopStateFlags64();
      TaskCreationOptions creationOptions = TaskCreationOptions.None;
      InternalTaskOptions internalOptions = InternalTaskOptions.SelfReplicating;
      if (parallelOptions.CancellationToken.IsCancellationRequested)
        throw new OperationCanceledException(parallelOptions.CancellationToken);
      int nNumExpectedWorkers = parallelOptions.EffectiveMaxConcurrencyLevel == -1 ? PlatformHelper.ProcessorCount : parallelOptions.EffectiveMaxConcurrencyLevel;
      RangeManager rangeManager = new RangeManager(fromInclusive, toExclusive, 1L, nNumExpectedWorkers);
      OperationCanceledException oce = (OperationCanceledException) null;
      CancellationTokenRegistration tokenRegistration = new CancellationTokenRegistration();
      CancellationToken cancellationToken;
      if (parallelOptions.CancellationToken.CanBeCanceled)
      {
        cancellationToken = parallelOptions.CancellationToken;
        tokenRegistration = cancellationToken.InternalRegisterWithoutEC((Action<object>) (o =>
        {
          sharedPStateFlags.Cancel();
          oce = new OperationCanceledException(parallelOptions.CancellationToken);
        }), (object) null);
      }
      Task task = (Task) null;
      int forkJoinContextID = 0;
      if (TplEtwProvider.Log.IsEnabled())
      {
        forkJoinContextID = Interlocked.Increment(ref Parallel.s_forkJoinContextID);
        task = Task.InternalCurrent;
        TplEtwProvider.Log.ParallelLoopBegin(task != null ? task.m_taskScheduler.Id : TaskScheduler.Current.Id, task != null ? task.Id : 0, forkJoinContextID, TplEtwProvider.ForkJoinOperationType.ParallelFor, fromInclusive, toExclusive);
      }
      ParallelForReplicatingTask rootTask = (ParallelForReplicatingTask) null;
      try
      {
        rootTask = new ParallelForReplicatingTask(parallelOptions, (Action) (() =>
        {
          Task internalCurrent = Task.InternalCurrent;
          bool flag1 = internalCurrent == rootTask;
          RangeWorker rangeWorker1 = new RangeWorker();
          object fromPreviousReplica = internalCurrent.SavedStateFromPreviousReplica;
          RangeWorker rangeWorker2 = !(fromPreviousReplica is RangeWorker) ? rangeManager.RegisterNewWorker() : (RangeWorker) fromPreviousReplica;
          long nFromInclusiveLocal;
          long nToExclusiveLocal;
          if (!rangeWorker2.FindNewWork(out nFromInclusiveLocal, out nToExclusiveLocal) || sharedPStateFlags.ShouldExitLoop(nFromInclusiveLocal))
            return;
          if (TplEtwProvider.Log.IsEnabled())
            TplEtwProvider.Log.ParallelFork(internalCurrent != null ? internalCurrent.m_taskScheduler.Id : TaskScheduler.Current.Id, internalCurrent != null ? internalCurrent.Id : 0, forkJoinContextID);
          TLocal local = default (TLocal);
          bool flag2 = false;
          try
          {
            ParallelLoopState64 parallelLoopState64 = (ParallelLoopState64) null;
            if (bodyWithState != null)
              parallelLoopState64 = new ParallelLoopState64(sharedPStateFlags);
            else if (bodyWithLocal != null)
            {
              parallelLoopState64 = new ParallelLoopState64(sharedPStateFlags);
              if (localInit != null)
              {
                local = localInit();
                flag2 = true;
              }
            }
            Parallel.LoopTimer loopTimer = new Parallel.LoopTimer(rootTask.ActiveChildCount);
            do
            {
              if (body != null)
              {
                for (long index = nFromInclusiveLocal; index < nToExclusiveLocal && (sharedPStateFlags.LoopStateFlags == ParallelLoopStateFlags.PLS_NONE || !sharedPStateFlags.ShouldExitLoop()); ++index)
                  body(index);
              }
              else if (bodyWithState != null)
              {
                for (long CallerIteration = nFromInclusiveLocal; CallerIteration < nToExclusiveLocal && (sharedPStateFlags.LoopStateFlags == ParallelLoopStateFlags.PLS_NONE || !sharedPStateFlags.ShouldExitLoop(CallerIteration)); ++CallerIteration)
                {
                  parallelLoopState64.CurrentIteration = CallerIteration;
                  bodyWithState(CallerIteration, (ParallelLoopState) parallelLoopState64);
                }
              }
              else
              {
                for (long CallerIteration = nFromInclusiveLocal; CallerIteration < nToExclusiveLocal && (sharedPStateFlags.LoopStateFlags == ParallelLoopStateFlags.PLS_NONE || !sharedPStateFlags.ShouldExitLoop(CallerIteration)); ++CallerIteration)
                {
                  parallelLoopState64.CurrentIteration = CallerIteration;
                  local = bodyWithLocal(CallerIteration, (ParallelLoopState) parallelLoopState64, local);
                }
              }
              if (!flag1 && loopTimer.LimitExceeded())
              {
                internalCurrent.SavedStateForNextReplica = (object) rangeWorker2;
                break;
              }
            }
            while (rangeWorker2.FindNewWork(out nFromInclusiveLocal, out nToExclusiveLocal) && (sharedPStateFlags.LoopStateFlags == ParallelLoopStateFlags.PLS_NONE || !sharedPStateFlags.ShouldExitLoop(nFromInclusiveLocal)));
          }
          catch
          {
            sharedPStateFlags.SetExceptional();
            throw;
          }
          finally
          {
            if (localFinally != null & flag2)
              localFinally(local);
            if (TplEtwProvider.Log.IsEnabled())
              TplEtwProvider.Log.ParallelJoin(internalCurrent != null ? internalCurrent.m_taskScheduler.Id : TaskScheduler.Current.Id, internalCurrent != null ? internalCurrent.Id : 0, forkJoinContextID);
          }
        }), creationOptions, internalOptions);
        rootTask.RunSynchronously(parallelOptions.EffectiveTaskScheduler);
        rootTask.Wait();
        cancellationToken = parallelOptions.CancellationToken;
        if (cancellationToken.CanBeCanceled)
          tokenRegistration.Dispose();
        if (oce != null)
          throw oce;
      }
      catch (AggregateException ex)
      {
        if (parallelOptions.CancellationToken.CanBeCanceled)
          tokenRegistration.Dispose();
        Parallel.ThrowIfReducableToSingleOCE((IEnumerable<Exception>) ex.InnerExceptions, parallelOptions.CancellationToken);
        throw;
      }
      catch (TaskSchedulerException ex)
      {
        if (parallelOptions.CancellationToken.CanBeCanceled)
          tokenRegistration.Dispose();
        throw;
      }
      finally
      {
        int loopStateFlags = sharedPStateFlags.LoopStateFlags;
        parallelLoopResult.m_completed = loopStateFlags == ParallelLoopStateFlags.PLS_NONE;
        if ((loopStateFlags & ParallelLoopStateFlags.PLS_BROKEN) != 0)
          parallelLoopResult.m_lowestBreakIteration = new long?(sharedPStateFlags.LowestBreakIteration);
        if (rootTask != null && rootTask.IsCompleted)
          rootTask.Dispose();
        if (TplEtwProvider.Log.IsEnabled())
        {
          long TotalIterations = loopStateFlags != ParallelLoopStateFlags.PLS_NONE ? ((loopStateFlags & ParallelLoopStateFlags.PLS_BROKEN) == 0 ? -1L : sharedPStateFlags.LowestBreakIteration - fromInclusive) : toExclusive - fromInclusive;
          TplEtwProvider.Log.ParallelLoopEnd(task != null ? task.m_taskScheduler.Id : TaskScheduler.Current.Id, task != null ? task.Id : 0, forkJoinContextID, TotalIterations);
        }
      }
      return parallelLoopResult;
    }

    /// <summary>执行 foreach（在 Visual Basic 中为 For Each）操作，其中在 <see cref="T:System.Collections.IEnumerable" /> 上可能会并行运行迭代。</summary>
    /// <returns>包含有关已完成的循环部分的信息的结构。</returns>
    /// <param name="source">可枚举的数据源。</param>
    /// <param name="body">将为每个迭代调用一次的委托。</param>
    /// <typeparam name="TSource">源中数据的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is null.-or-The <paramref name="body" /> argument is null.</exception>
    /// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
    [__DynamicallyInvokable]
    public static ParallelLoopResult ForEach<TSource>(IEnumerable<TSource> source, Action<TSource> body)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      if (body == null)
        throw new ArgumentNullException("body");
      return Parallel.ForEachWorker<TSource, object>(source, Parallel.s_defaultParallelOptions, body, (Action<TSource, ParallelLoopState>) null, (Action<TSource, ParallelLoopState, long>) null, (Func<TSource, ParallelLoopState, object, object>) null, (Func<TSource, ParallelLoopState, long, object, object>) null, (Func<object>) null, (Action<object>) null);
    }

    /// <summary>执行 foreach（在 Visual Basic 中为 For Each）操作，其中在 <see cref="T:System.Collections.IEnumerable" /> 中可能会并行运行迭代，而且可以匹配配置循环选项。</summary>
    /// <returns>包含有关已完成的循环部分的信息的结构。</returns>
    /// <param name="source">可枚举的数据源。</param>
    /// <param name="parallelOptions">一个对象，用于配置此操作的行为。</param>
    /// <param name="body">将为每个迭代调用一次的委托。</param>
    /// <typeparam name="TSource">源中数据的类型。</typeparam>
    /// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled</exception>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is null.-or-The <paramref name="parallelOptions" /> argument is null.-or-The <paramref name="body" /> argument is null.</exception>
    /// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
    /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
    [__DynamicallyInvokable]
    public static ParallelLoopResult ForEach<TSource>(IEnumerable<TSource> source, ParallelOptions parallelOptions, Action<TSource> body)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      if (body == null)
        throw new ArgumentNullException("body");
      if (parallelOptions == null)
        throw new ArgumentNullException("parallelOptions");
      return Parallel.ForEachWorker<TSource, object>(source, parallelOptions, body, (Action<TSource, ParallelLoopState>) null, (Action<TSource, ParallelLoopState, long>) null, (Func<TSource, ParallelLoopState, object, object>) null, (Func<TSource, ParallelLoopState, long, object, object>) null, (Func<object>) null, (Action<object>) null);
    }

    /// <summary>执行 foreach（在 Visual Basic 中为 For Each）操作，其中在 <see cref="T:System.Collections.IEnumerable" /> 中可能会并行运行迭代，而且可以监视和操作循环的状态。</summary>
    /// <returns>包含有关已完成的循环部分的信息的结构。</returns>
    /// <param name="source">可枚举的数据源。</param>
    /// <param name="body">将为每个迭代调用一次的委托。</param>
    /// <typeparam name="TSource">源中数据的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is null.-or-The <paramref name="body" /> argument is null.</exception>
    /// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
    [__DynamicallyInvokable]
    public static ParallelLoopResult ForEach<TSource>(IEnumerable<TSource> source, Action<TSource, ParallelLoopState> body)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      if (body == null)
        throw new ArgumentNullException("body");
      return Parallel.ForEachWorker<TSource, object>(source, Parallel.s_defaultParallelOptions, (Action<TSource>) null, body, (Action<TSource, ParallelLoopState, long>) null, (Func<TSource, ParallelLoopState, object, object>) null, (Func<TSource, ParallelLoopState, long, object, object>) null, (Func<object>) null, (Action<object>) null);
    }

    /// <summary>执行 foreach（在 Visual Basic 中为 For Each）操作，其中在 <see cref="T:System.Collections.IEnumerable" /> 中可能会并行运行迭代，而且可以配置循环选项，可以监视和操作循环的状态。</summary>
    /// <returns>包含有关已完成的循环部分的信息的结构。</returns>
    /// <param name="source">可枚举的数据源。</param>
    /// <param name="parallelOptions">一个对象，用于配置此操作的行为。</param>
    /// <param name="body">将为每个迭代调用一次的委托。</param>
    /// <typeparam name="TSource">源中数据的类型。</typeparam>
    /// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled</exception>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is null.-or-The <paramref name="parallelOptions" /> argument is null.-or-The <paramref name="body" /> argument is null.</exception>
    /// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
    /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
    [__DynamicallyInvokable]
    public static ParallelLoopResult ForEach<TSource>(IEnumerable<TSource> source, ParallelOptions parallelOptions, Action<TSource, ParallelLoopState> body)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      if (body == null)
        throw new ArgumentNullException("body");
      if (parallelOptions == null)
        throw new ArgumentNullException("parallelOptions");
      return Parallel.ForEachWorker<TSource, object>(source, parallelOptions, (Action<TSource>) null, body, (Action<TSource, ParallelLoopState, long>) null, (Func<TSource, ParallelLoopState, object, object>) null, (Func<TSource, ParallelLoopState, long, object, object>) null, (Func<object>) null, (Action<object>) null);
    }

    /// <summary>执行具有 64 位索引的 foreach（在 Visual Basic 中为 For Each）操作，其中在 <see cref="T:System.Collections.IEnumerable" /> 上可能会并行运行迭代，而且可以监视和操作循环的状态。</summary>
    /// <returns>包含有关已完成的循环部分的信息的结构。</returns>
    /// <param name="source">可枚举的数据源。</param>
    /// <param name="body">将为每个迭代调用一次的委托。</param>
    /// <typeparam name="TSource">源中数据的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is null.-or-The <paramref name="body" /> argument is null.</exception>
    /// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
    [__DynamicallyInvokable]
    public static ParallelLoopResult ForEach<TSource>(IEnumerable<TSource> source, Action<TSource, ParallelLoopState, long> body)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      if (body == null)
        throw new ArgumentNullException("body");
      return Parallel.ForEachWorker<TSource, object>(source, Parallel.s_defaultParallelOptions, (Action<TSource>) null, (Action<TSource, ParallelLoopState>) null, body, (Func<TSource, ParallelLoopState, object, object>) null, (Func<TSource, ParallelLoopState, long, object, object>) null, (Func<object>) null, (Action<object>) null);
    }

    /// <summary>执行具有 64 位索引的 foreach（在 Visual Basic 中为 For Each）操作，其中在 <see cref="T:System.Collections.IEnumerable" /> 上可能会并行运行迭代，而且可以配置循环选项，可以监视和操作循环的状态。</summary>
    /// <returns>包含有关已完成的循环部分的信息的结构。</returns>
    /// <param name="source">可枚举的数据源。</param>
    /// <param name="parallelOptions">一个对象，用于配置此操作的行为。</param>
    /// <param name="body">将为每个迭代调用一次的委托。</param>
    /// <typeparam name="TSource">源中数据的类型。</typeparam>
    /// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled</exception>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is null.-or-The <paramref name="parallelOptions" /> argument is null.-or-The <paramref name="body" /> argument is null.</exception>
    /// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
    /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
    [__DynamicallyInvokable]
    public static ParallelLoopResult ForEach<TSource>(IEnumerable<TSource> source, ParallelOptions parallelOptions, Action<TSource, ParallelLoopState, long> body)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      if (body == null)
        throw new ArgumentNullException("body");
      if (parallelOptions == null)
        throw new ArgumentNullException("parallelOptions");
      return Parallel.ForEachWorker<TSource, object>(source, parallelOptions, (Action<TSource>) null, (Action<TSource, ParallelLoopState>) null, body, (Func<TSource, ParallelLoopState, object, object>) null, (Func<TSource, ParallelLoopState, long, object, object>) null, (Func<object>) null, (Action<object>) null);
    }

    /// <summary>执行具有线程本地数据的 foreach（在 Visual Basic 中为 For Each）操作，其中在 <see cref="T:System.Collections.IEnumerable" /> 上可能会并行运行迭代，而且可以监视和操作循环的状态。</summary>
    /// <returns>包含有关已完成的循环部分的信息的结构。</returns>
    /// <param name="source">可枚举的数据源。</param>
    /// <param name="localInit">用于返回每个任务的本地数据的初始状态的函数委托。</param>
    /// <param name="body">将为每个迭代调用一次的委托。</param>
    /// <param name="localFinally">用于对每个任务的本地状态执行一个最终操作的委托。</param>
    /// <typeparam name="TSource">源中数据的类型。</typeparam>
    /// <typeparam name="TLocal">线程本地数据的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is null.-or-The <paramref name="body" /> argument is null.-or-The <paramref name="localInit" /> argument is null.-or-The <paramref name="localFinally" /> argument is null.</exception>
    /// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
    [__DynamicallyInvokable]
    public static ParallelLoopResult ForEach<TSource, TLocal>(IEnumerable<TSource> source, Func<TLocal> localInit, Func<TSource, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      if (body == null)
        throw new ArgumentNullException("body");
      if (localInit == null)
        throw new ArgumentNullException("localInit");
      if (localFinally == null)
        throw new ArgumentNullException("localFinally");
      return Parallel.ForEachWorker<TSource, TLocal>(source, Parallel.s_defaultParallelOptions, (Action<TSource>) null, (Action<TSource, ParallelLoopState>) null, (Action<TSource, ParallelLoopState, long>) null, body, (Func<TSource, ParallelLoopState, long, TLocal, TLocal>) null, localInit, localFinally);
    }

    /// <summary>执行具有线程本地数据的 foreach（在 Visual Basic 中为 For Each）操作，其中在 <see cref="T:System.Collections.IEnumerable" /> 上可能会并行运行迭代，而且可以配置循环选项，可以监视和操作循环的状态。</summary>
    /// <returns>包含有关已完成的循环部分的信息的结构。</returns>
    /// <param name="source">可枚举的数据源。</param>
    /// <param name="parallelOptions">一个对象，用于配置此操作的行为。</param>
    /// <param name="localInit">用于返回每个任务的本地数据的初始状态的函数委托。</param>
    /// <param name="body">将为每个迭代调用一次的委托。</param>
    /// <param name="localFinally">用于对每个任务的本地状态执行一个最终操作的委托。</param>
    /// <typeparam name="TSource">源中数据的类型。</typeparam>
    /// <typeparam name="TLocal">线程本地数据的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is null.-or-The <paramref name="parallelOptions" /> argument is null.-or-The <paramref name="body" /> argument is null.-or-The <paramref name="localInit" /> argument is null.-or-The <paramref name="localFinally" /> argument is null.</exception>
    /// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled.</exception>
    /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
    /// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
    [__DynamicallyInvokable]
    public static ParallelLoopResult ForEach<TSource, TLocal>(IEnumerable<TSource> source, ParallelOptions parallelOptions, Func<TLocal> localInit, Func<TSource, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      if (body == null)
        throw new ArgumentNullException("body");
      if (localInit == null)
        throw new ArgumentNullException("localInit");
      if (localFinally == null)
        throw new ArgumentNullException("localFinally");
      if (parallelOptions == null)
        throw new ArgumentNullException("parallelOptions");
      return Parallel.ForEachWorker<TSource, TLocal>(source, parallelOptions, (Action<TSource>) null, (Action<TSource, ParallelLoopState>) null, (Action<TSource, ParallelLoopState, long>) null, body, (Func<TSource, ParallelLoopState, long, TLocal, TLocal>) null, localInit, localFinally);
    }

    /// <summary>执行具有线程本地数据的 foreach（在 Visual Basic 中为 For Each）操作，其中在 <see cref="T:System.Collections.IEnumerable" /> 上可能会并行运行迭代，而且可以监视和操作循环的状态。</summary>
    /// <returns>包含有关已完成的循环部分的信息的结构。</returns>
    /// <param name="source">可枚举的数据源。</param>
    /// <param name="localInit">用于返回每个任务的本地数据的初始状态的函数委托。</param>
    /// <param name="body">将为每个迭代调用一次的委托。</param>
    /// <param name="localFinally">用于对每个任务的本地状态执行一个最终操作的委托。</param>
    /// <typeparam name="TSource">源中数据的类型。</typeparam>
    /// <typeparam name="TLocal">线程本地数据的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is null.-or-The <paramref name="body" /> argument is null.-or-The <paramref name="localInit" /> argument is null.-or-The <paramref name="localFinally" /> argument is null.</exception>
    /// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
    [__DynamicallyInvokable]
    public static ParallelLoopResult ForEach<TSource, TLocal>(IEnumerable<TSource> source, Func<TLocal> localInit, Func<TSource, ParallelLoopState, long, TLocal, TLocal> body, Action<TLocal> localFinally)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      if (body == null)
        throw new ArgumentNullException("body");
      if (localInit == null)
        throw new ArgumentNullException("localInit");
      if (localFinally == null)
        throw new ArgumentNullException("localFinally");
      return Parallel.ForEachWorker<TSource, TLocal>(source, Parallel.s_defaultParallelOptions, (Action<TSource>) null, (Action<TSource, ParallelLoopState>) null, (Action<TSource, ParallelLoopState, long>) null, (Func<TSource, ParallelLoopState, TLocal, TLocal>) null, body, localInit, localFinally);
    }

    /// <summary>执行具有线程本地数据和 64 位索引的 foreach（在 Visual Basic 中为 For Each）操作，其中在 <see cref="T:System.Collections.IEnumerable" /> 上可能会并行运行迭代，而且可以配置循环选项，可以监视和操作循环的状态。</summary>
    /// <returns>包含有关已完成的循环部分的信息的结构。</returns>
    /// <param name="source">可枚举的数据源。</param>
    /// <param name="parallelOptions">一个对象，用于配置此操作的行为。</param>
    /// <param name="localInit">用于返回每个任务的本地数据的初始状态的函数委托。</param>
    /// <param name="body">将为每个迭代调用一次的委托。</param>
    /// <param name="localFinally">用于对每个任务的本地状态执行一个最终操作的委托。</param>
    /// <typeparam name="TSource">源中数据的类型。</typeparam>
    /// <typeparam name="TLocal">线程本地数据的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is null.-or-The <paramref name="parallelOptions" /> argument is null.-or-The <paramref name="body" /> argument is null.-or-The <paramref name="localInit" /> argument is null.-or-The <paramref name="localFinally" /> argument is null.</exception>
    /// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled.</exception>
    /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
    /// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
    [__DynamicallyInvokable]
    public static ParallelLoopResult ForEach<TSource, TLocal>(IEnumerable<TSource> source, ParallelOptions parallelOptions, Func<TLocal> localInit, Func<TSource, ParallelLoopState, long, TLocal, TLocal> body, Action<TLocal> localFinally)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      if (body == null)
        throw new ArgumentNullException("body");
      if (localInit == null)
        throw new ArgumentNullException("localInit");
      if (localFinally == null)
        throw new ArgumentNullException("localFinally");
      if (parallelOptions == null)
        throw new ArgumentNullException("parallelOptions");
      return Parallel.ForEachWorker<TSource, TLocal>(source, parallelOptions, (Action<TSource>) null, (Action<TSource, ParallelLoopState>) null, (Action<TSource, ParallelLoopState, long>) null, (Func<TSource, ParallelLoopState, TLocal, TLocal>) null, body, localInit, localFinally);
    }

    private static ParallelLoopResult ForEachWorker<TSource, TLocal>(IEnumerable<TSource> source, ParallelOptions parallelOptions, Action<TSource> body, Action<TSource, ParallelLoopState> bodyWithState, Action<TSource, ParallelLoopState, long> bodyWithStateAndIndex, Func<TSource, ParallelLoopState, TLocal, TLocal> bodyWithStateAndLocal, Func<TSource, ParallelLoopState, long, TLocal, TLocal> bodyWithEverything, Func<TLocal> localInit, Action<TLocal> localFinally)
    {
      if (parallelOptions.CancellationToken.IsCancellationRequested)
        throw new OperationCanceledException(parallelOptions.CancellationToken);
      TSource[] array = source as TSource[];
      if (array != null)
        return Parallel.ForEachWorker<TSource, TLocal>(array, parallelOptions, body, bodyWithState, bodyWithStateAndIndex, bodyWithStateAndLocal, bodyWithEverything, localInit, localFinally);
      IList<TSource> list = source as IList<TSource>;
      if (list != null)
        return Parallel.ForEachWorker<TSource, TLocal>(list, parallelOptions, body, bodyWithState, bodyWithStateAndIndex, bodyWithStateAndLocal, bodyWithEverything, localInit, localFinally);
      return Parallel.PartitionerForEachWorker<TSource, TLocal>((Partitioner<TSource>) Partitioner.Create<TSource>(source), parallelOptions, body, bodyWithState, bodyWithStateAndIndex, bodyWithStateAndLocal, bodyWithEverything, localInit, localFinally);
    }

    private static ParallelLoopResult ForEachWorker<TSource, TLocal>(TSource[] array, ParallelOptions parallelOptions, Action<TSource> body, Action<TSource, ParallelLoopState> bodyWithState, Action<TSource, ParallelLoopState, long> bodyWithStateAndIndex, Func<TSource, ParallelLoopState, TLocal, TLocal> bodyWithStateAndLocal, Func<TSource, ParallelLoopState, long, TLocal, TLocal> bodyWithEverything, Func<TLocal> localInit, Action<TLocal> localFinally)
    {
      int lowerBound = array.GetLowerBound(0);
      int toExclusive = array.GetUpperBound(0) + 1;
      if (body != null)
        return Parallel.ForWorker<object>(lowerBound, toExclusive, parallelOptions, (Action<int>) (i => body(array[i])), (Action<int, ParallelLoopState>) null, (Func<int, ParallelLoopState, object, object>) null, (Func<object>) null, (Action<object>) null);
      if (bodyWithState != null)
        return Parallel.ForWorker<object>(lowerBound, toExclusive, parallelOptions, (Action<int>) null, (Action<int, ParallelLoopState>) ((i, state) => bodyWithState(array[i], state)), (Func<int, ParallelLoopState, object, object>) null, (Func<object>) null, (Action<object>) null);
      if (bodyWithStateAndIndex != null)
        return Parallel.ForWorker<object>(lowerBound, toExclusive, parallelOptions, (Action<int>) null, (Action<int, ParallelLoopState>) ((i, state) => bodyWithStateAndIndex(array[i], state, (long) i)), (Func<int, ParallelLoopState, object, object>) null, (Func<object>) null, (Action<object>) null);
      if (bodyWithStateAndLocal != null)
        return Parallel.ForWorker<TLocal>(lowerBound, toExclusive, parallelOptions, (Action<int>) null, (Action<int, ParallelLoopState>) null, (Func<int, ParallelLoopState, TLocal, TLocal>) ((i, state, local) => bodyWithStateAndLocal(array[i], state, local)), localInit, localFinally);
      return Parallel.ForWorker<TLocal>(lowerBound, toExclusive, parallelOptions, (Action<int>) null, (Action<int, ParallelLoopState>) null, (Func<int, ParallelLoopState, TLocal, TLocal>) ((i, state, local) => bodyWithEverything(array[i], state, (long) i, local)), localInit, localFinally);
    }

    private static ParallelLoopResult ForEachWorker<TSource, TLocal>(IList<TSource> list, ParallelOptions parallelOptions, Action<TSource> body, Action<TSource, ParallelLoopState> bodyWithState, Action<TSource, ParallelLoopState, long> bodyWithStateAndIndex, Func<TSource, ParallelLoopState, TLocal, TLocal> bodyWithStateAndLocal, Func<TSource, ParallelLoopState, long, TLocal, TLocal> bodyWithEverything, Func<TLocal> localInit, Action<TLocal> localFinally)
    {
      if (body != null)
        return Parallel.ForWorker<object>(0, list.Count, parallelOptions, (Action<int>) (i => body(list[i])), (Action<int, ParallelLoopState>) null, (Func<int, ParallelLoopState, object, object>) null, (Func<object>) null, (Action<object>) null);
      if (bodyWithState != null)
        return Parallel.ForWorker<object>(0, list.Count, parallelOptions, (Action<int>) null, (Action<int, ParallelLoopState>) ((i, state) => bodyWithState(list[i], state)), (Func<int, ParallelLoopState, object, object>) null, (Func<object>) null, (Action<object>) null);
      if (bodyWithStateAndIndex != null)
        return Parallel.ForWorker<object>(0, list.Count, parallelOptions, (Action<int>) null, (Action<int, ParallelLoopState>) ((i, state) => bodyWithStateAndIndex(list[i], state, (long) i)), (Func<int, ParallelLoopState, object, object>) null, (Func<object>) null, (Action<object>) null);
      if (bodyWithStateAndLocal != null)
        return Parallel.ForWorker<TLocal>(0, list.Count, parallelOptions, (Action<int>) null, (Action<int, ParallelLoopState>) null, (Func<int, ParallelLoopState, TLocal, TLocal>) ((i, state, local) => bodyWithStateAndLocal(list[i], state, local)), localInit, localFinally);
      return Parallel.ForWorker<TLocal>(0, list.Count, parallelOptions, (Action<int>) null, (Action<int, ParallelLoopState>) null, (Func<int, ParallelLoopState, TLocal, TLocal>) ((i, state, local) => bodyWithEverything(list[i], state, (long) i, local)), localInit, localFinally);
    }

    /// <summary>执行 foreach（在 Visual Basic 中为 For Each）操作，其中在 <see cref="T:System.Collections.Concurrent.Partitioner" /> 上可能会并行运行迭代。</summary>
    /// <returns>包含有关已完成的循环部分的信息的结构。</returns>
    /// <param name="source">包含原始数据源的分区程序。</param>
    /// <param name="body">将为每个迭代调用一次的委托。</param>
    /// <typeparam name="TSource">
    /// <paramref name="source" /> 中的元素的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is  null.-or-The <paramref name="body" /> argument is null.</exception>
    /// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Collections.Concurrent.Partitioner`1.SupportsDynamicPartitions" /> property in the <paramref name="source" /> partitioner returns false.-or-The exception that is thrown when any methods in the <paramref name="source" /> partitioner return null.-or-The <see cref="M:System.Collections.Concurrent.Partitioner`1.GetPartitions(System.Int32)" /> method in the <paramref name="source" /> partitioner does not return the correct number of partitions.</exception>
    [__DynamicallyInvokable]
    public static ParallelLoopResult ForEach<TSource>(Partitioner<TSource> source, Action<TSource> body)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      if (body == null)
        throw new ArgumentNullException("body");
      return Parallel.PartitionerForEachWorker<TSource, object>(source, Parallel.s_defaultParallelOptions, body, (Action<TSource, ParallelLoopState>) null, (Action<TSource, ParallelLoopState, long>) null, (Func<TSource, ParallelLoopState, object, object>) null, (Func<TSource, ParallelLoopState, long, object, object>) null, (Func<object>) null, (Action<object>) null);
    }

    /// <summary>执行 foreach（在 Visual Basic 中为 For Each）操作，其中在 <see cref="T:System.Collections.Concurrent.Partitioner" /> 中可能会并行运行迭代，而且可以监视和操作循环的状态。</summary>
    /// <returns>包含有关已完成的循环部分的信息的结构。</returns>
    /// <param name="source">包含原始数据源的分区程序。</param>
    /// <param name="body">将为每个迭代调用一次的委托。</param>
    /// <typeparam name="TSource">
    /// <paramref name="source" /> 中的元素的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is null.-or-The <paramref name="body" /> argument is null.</exception>
    /// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Collections.Concurrent.Partitioner`1.SupportsDynamicPartitions" /> property in the <paramref name="source" /> partitioner returns false.-or-A method in the <paramref name="source" /> partitioner returns null.-or-The <see cref="M:System.Collections.Concurrent.Partitioner`1.GetPartitions(System.Int32)" /> method in the <paramref name="source" /> partitioner does not return the correct number of partitions.</exception>
    [__DynamicallyInvokable]
    public static ParallelLoopResult ForEach<TSource>(Partitioner<TSource> source, Action<TSource, ParallelLoopState> body)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      if (body == null)
        throw new ArgumentNullException("body");
      return Parallel.PartitionerForEachWorker<TSource, object>(source, Parallel.s_defaultParallelOptions, (Action<TSource>) null, body, (Action<TSource, ParallelLoopState, long>) null, (Func<TSource, ParallelLoopState, object, object>) null, (Func<TSource, ParallelLoopState, long, object, object>) null, (Func<object>) null, (Action<object>) null);
    }

    /// <summary>执行 foreach（在 Visual Basic 中为 For Each）操作，其中在 <see cref="T:System.Collections.Concurrent.OrderablePartitioner`1" /> 中可能会并行运行迭代，而且可以监视和操作循环的状态。</summary>
    /// <returns>包含有关已完成的循环部分的信息的结构。</returns>
    /// <param name="source">包含原始数据源的可排序分区程序。</param>
    /// <param name="body">将为每个迭代调用一次的委托。</param>
    /// <typeparam name="TSource">
    /// <paramref name="source" /> 中的元素的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is null.-or-The <paramref name="body" /> argument is null.</exception>
    /// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Collections.Concurrent.Partitioner`1.SupportsDynamicPartitions" /> property in the <paramref name="source" /> orderable partitioner returns false.-or-The <see cref="P:System.Collections.Concurrent.OrderablePartitioner`1.KeysNormalized" /> property in the source orderable partitioner returns false.-or-Any methods in the source orderable partitioner return null.</exception>
    [__DynamicallyInvokable]
    public static ParallelLoopResult ForEach<TSource>(OrderablePartitioner<TSource> source, Action<TSource, ParallelLoopState, long> body)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      if (body == null)
        throw new ArgumentNullException("body");
      if (!source.KeysNormalized)
        throw new InvalidOperationException(Environment.GetResourceString("Parallel_ForEach_OrderedPartitionerKeysNotNormalized"));
      return Parallel.PartitionerForEachWorker<TSource, object>((Partitioner<TSource>) source, Parallel.s_defaultParallelOptions, (Action<TSource>) null, (Action<TSource, ParallelLoopState>) null, body, (Func<TSource, ParallelLoopState, object, object>) null, (Func<TSource, ParallelLoopState, long, object, object>) null, (Func<object>) null, (Action<object>) null);
    }

    /// <summary>执行具有线程本地数据的 foreach（在 Visual Basic 中为 For Each）操作，其中在 <see cref="T:System.Collections.Concurrent.Partitioner" /> 上可能会并行运行迭代，而且可以监视和操作循环的状态。</summary>
    /// <returns>包含有关已完成的循环部分的信息的结构。</returns>
    /// <param name="source">包含原始数据源的分区程序。</param>
    /// <param name="localInit">用于返回每个任务的本地数据的初始状态的函数委托。</param>
    /// <param name="body">将为每个迭代调用一次的委托。</param>
    /// <param name="localFinally">用于对每个任务的本地状态执行一个最终操作的委托。</param>
    /// <typeparam name="TSource">
    /// <paramref name="source" /> 中的元素的类型。</typeparam>
    /// <typeparam name="TLocal">线程本地数据的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is null.-or-The <paramref name="body" /> argument is null.-or-The <paramref name="localInit" /> argument is null.-or-The <paramref name="localFinally" /> argument is null.</exception>
    /// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Collections.Concurrent.Partitioner`1.SupportsDynamicPartitions" /> property in the <paramref name="source" /><see cref="T:System.Collections.Concurrent.Partitioner" /> returns false or the partitioner returns null partitions.</exception>
    /// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
    [__DynamicallyInvokable]
    public static ParallelLoopResult ForEach<TSource, TLocal>(Partitioner<TSource> source, Func<TLocal> localInit, Func<TSource, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      if (body == null)
        throw new ArgumentNullException("body");
      if (localInit == null)
        throw new ArgumentNullException("localInit");
      if (localFinally == null)
        throw new ArgumentNullException("localFinally");
      return Parallel.PartitionerForEachWorker<TSource, TLocal>(source, Parallel.s_defaultParallelOptions, (Action<TSource>) null, (Action<TSource, ParallelLoopState>) null, (Action<TSource, ParallelLoopState, long>) null, body, (Func<TSource, ParallelLoopState, long, TLocal, TLocal>) null, localInit, localFinally);
    }

    /// <summary>执行具有线程本地数据的 foreach（在 Visual Basic 中为 For Each）操作，其中在 <see cref="T:System.Collections.Concurrent.OrderablePartitioner`1" /> 上可能会并行运行迭代，而且可以配置循环选项，可以监视和操作循环的状态。</summary>
    /// <returns>包含有关已完成的循环部分的信息的结构。</returns>
    /// <param name="source">包含原始数据源的可排序分区程序。</param>
    /// <param name="localInit">用于返回每个任务的本地数据的初始状态的函数委托。</param>
    /// <param name="body">将为每个迭代调用一次的委托。</param>
    /// <param name="localFinally">用于对每个任务的本地状态执行一个最终操作的委托。</param>
    /// <typeparam name="TSource">
    /// <paramref name="source" /> 中的元素的类型。</typeparam>
    /// <typeparam name="TLocal">线程本地数据的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is null.-or-The <paramref name="body" /> argument is null.-or-The <paramref name="localInit" /> argument is null.-or-The <paramref name="localFinally" /> argument is null.</exception>
    /// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Collections.Concurrent.Partitioner`1.SupportsDynamicPartitions" /> property in the <paramref name="source" /><see cref="T:System.Collections.Concurrent.Partitioner" /> returns false or the partitioner returns null partitions.</exception>
    /// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
    [__DynamicallyInvokable]
    public static ParallelLoopResult ForEach<TSource, TLocal>(OrderablePartitioner<TSource> source, Func<TLocal> localInit, Func<TSource, ParallelLoopState, long, TLocal, TLocal> body, Action<TLocal> localFinally)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      if (body == null)
        throw new ArgumentNullException("body");
      if (localInit == null)
        throw new ArgumentNullException("localInit");
      if (localFinally == null)
        throw new ArgumentNullException("localFinally");
      if (!source.KeysNormalized)
        throw new InvalidOperationException(Environment.GetResourceString("Parallel_ForEach_OrderedPartitionerKeysNotNormalized"));
      return Parallel.PartitionerForEachWorker<TSource, TLocal>((Partitioner<TSource>) source, Parallel.s_defaultParallelOptions, (Action<TSource>) null, (Action<TSource, ParallelLoopState>) null, (Action<TSource, ParallelLoopState, long>) null, (Func<TSource, ParallelLoopState, TLocal, TLocal>) null, body, localInit, localFinally);
    }

    /// <summary>执行 foreach（在 Visual Basic 中为 For Each）操作，其中在 <see cref="T:System.Collections.Concurrent.Partitioner" /> 中可能会并行运行迭代，而且可以配置循环选项。</summary>
    /// <returns>包含有关已完成的循环部分的信息的结构。</returns>
    /// <param name="source">包含原始数据源的分区程序。</param>
    /// <param name="parallelOptions">一个对象，用于配置此操作的行为。</param>
    /// <param name="body">将为每个迭代调用一次的委托。</param>
    /// <typeparam name="TSource">
    /// <paramref name="source" /> 中的元素的类型。</typeparam>
    /// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled.</exception>
    /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is null.-or-The <paramref name="parallelOptions" /> argument is null.-or-The <paramref name="body" /> argument is null.</exception>
    /// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Collections.Concurrent.Partitioner`1.SupportsDynamicPartitions" /> property in the <paramref name="source" /> partitioner returns false.-or-The exception that is thrown when any methods in the <paramref name="source" /> partitioner return null.</exception>
    [__DynamicallyInvokable]
    public static ParallelLoopResult ForEach<TSource>(Partitioner<TSource> source, ParallelOptions parallelOptions, Action<TSource> body)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      if (body == null)
        throw new ArgumentNullException("body");
      if (parallelOptions == null)
        throw new ArgumentNullException("parallelOptions");
      return Parallel.PartitionerForEachWorker<TSource, object>(source, parallelOptions, body, (Action<TSource, ParallelLoopState>) null, (Action<TSource, ParallelLoopState, long>) null, (Func<TSource, ParallelLoopState, object, object>) null, (Func<TSource, ParallelLoopState, long, object, object>) null, (Func<object>) null, (Action<object>) null);
    }

    /// <summary>执行 foreach（在 Visual Basic 中为 For Each）操作，其中在 <see cref="T:System.Collections.Concurrent.Partitioner" /> 中可能会并行运行迭代，而且可以配置循环选项，可以监视和操作循环的状态。</summary>
    /// <returns>包含有关已完成的循环部分的信息的结构。</returns>
    /// <param name="source">包含原始数据源的分区程序。</param>
    /// <param name="parallelOptions">一个对象，用于配置此操作的行为。</param>
    /// <param name="body">将为每个迭代调用一次的委托。</param>
    /// <typeparam name="TSource">
    /// <paramref name="source" /> 中的元素的类型。</typeparam>
    /// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled.</exception>
    /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is null.-or-The <paramref name="parallelOptions" /> argument is null.-or-The <paramref name="body" /> argument is null.</exception>
    /// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Collections.Concurrent.Partitioner`1.SupportsDynamicPartitions" /> property in the <paramref name="source" /> partitioner returns false.-or-The exception that is thrown when any methods in the <paramref name="source" /> partitioner return null.</exception>
    [__DynamicallyInvokable]
    public static ParallelLoopResult ForEach<TSource>(Partitioner<TSource> source, ParallelOptions parallelOptions, Action<TSource, ParallelLoopState> body)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      if (body == null)
        throw new ArgumentNullException("body");
      if (parallelOptions == null)
        throw new ArgumentNullException("parallelOptions");
      return Parallel.PartitionerForEachWorker<TSource, object>(source, parallelOptions, (Action<TSource>) null, body, (Action<TSource, ParallelLoopState, long>) null, (Func<TSource, ParallelLoopState, object, object>) null, (Func<TSource, ParallelLoopState, long, object, object>) null, (Func<object>) null, (Action<object>) null);
    }

    /// <summary>执行 foreach（在 Visual Basic 中为 For Each）操作，其中在 <see cref="T:System.Collections.Concurrent.OrderablePartitioner`1" /> 中可能会并行运行迭代，而且可以配置循环选项，可以监视和操作循环的状态。</summary>
    /// <returns>包含有关已完成的循环部分的信息的结构。</returns>
    /// <param name="source">包含原始数据源的可排序分区程序。</param>
    /// <param name="parallelOptions">一个对象，用于配置此操作的行为。</param>
    /// <param name="body">将为每个迭代调用一次的委托。</param>
    /// <typeparam name="TSource">
    /// <paramref name="source" /> 中的元素的类型。</typeparam>
    /// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled</exception>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is  null.-or-The <paramref name="parallelOptions" /> argument is null.-or-The <paramref name="body" /> argument is null.</exception>
    /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
    /// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Collections.Concurrent.Partitioner`1.SupportsDynamicPartitions" /> property in the <paramref name="source" /> orderable partitioner returns false.-or-The <see cref="P:System.Collections.Concurrent.OrderablePartitioner`1.KeysNormalized" /> property in the <paramref name="source" /> orderable partitioner returns false.-or-The exception that is thrown when any methods in the <paramref name="source" /> orderable partitioner return null.</exception>
    [__DynamicallyInvokable]
    public static ParallelLoopResult ForEach<TSource>(OrderablePartitioner<TSource> source, ParallelOptions parallelOptions, Action<TSource, ParallelLoopState, long> body)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      if (body == null)
        throw new ArgumentNullException("body");
      if (parallelOptions == null)
        throw new ArgumentNullException("parallelOptions");
      if (!source.KeysNormalized)
        throw new InvalidOperationException(Environment.GetResourceString("Parallel_ForEach_OrderedPartitionerKeysNotNormalized"));
      return Parallel.PartitionerForEachWorker<TSource, object>((Partitioner<TSource>) source, parallelOptions, (Action<TSource>) null, (Action<TSource, ParallelLoopState>) null, body, (Func<TSource, ParallelLoopState, object, object>) null, (Func<TSource, ParallelLoopState, long, object, object>) null, (Func<object>) null, (Action<object>) null);
    }

    /// <summary>执行具有线程本地数据的 foreach（在 Visual Basic 中为 For Each）操作，其中在 <see cref="T:System.Collections.Concurrent.Partitioner" /> 上可能会并行运行迭代，而且可以配置循环选项，可以监视和操作循环的状态。</summary>
    /// <returns>包含有关已完成的循环部分的信息的结构。</returns>
    /// <param name="source">包含原始数据源的分区程序。</param>
    /// <param name="parallelOptions">一个对象，用于配置此操作的行为。</param>
    /// <param name="localInit">用于返回每个任务的本地数据的初始状态的函数委托。</param>
    /// <param name="body">将为每个迭代调用一次的委托。</param>
    /// <param name="localFinally">用于对每个任务的本地状态执行一个最终操作的委托。</param>
    /// <typeparam name="TSource">
    /// <paramref name="source" /> 中的元素的类型。</typeparam>
    /// <typeparam name="TLocal">线程本地数据的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is null.-or-The <paramref name="parallelOptions" /> argument is null.-or-The <paramref name="body" /> argument is null.-or-The <paramref name="localInit" /> argument is null.-or-The <paramref name="localFinally" /> argument is null.</exception>
    /// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Collections.Concurrent.Partitioner`1.SupportsDynamicPartitions" /> property in the <paramref name="source" /><see cref="T:System.Collections.Concurrent.Partitioner" /> returns false or the partitioner returns null partitions.</exception>
    /// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
    /// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled.</exception>
    /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
    [__DynamicallyInvokable]
    public static ParallelLoopResult ForEach<TSource, TLocal>(Partitioner<TSource> source, ParallelOptions parallelOptions, Func<TLocal> localInit, Func<TSource, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      if (body == null)
        throw new ArgumentNullException("body");
      if (localInit == null)
        throw new ArgumentNullException("localInit");
      if (localFinally == null)
        throw new ArgumentNullException("localFinally");
      if (parallelOptions == null)
        throw new ArgumentNullException("parallelOptions");
      return Parallel.PartitionerForEachWorker<TSource, TLocal>(source, parallelOptions, (Action<TSource>) null, (Action<TSource, ParallelLoopState>) null, (Action<TSource, ParallelLoopState, long>) null, body, (Func<TSource, ParallelLoopState, long, TLocal, TLocal>) null, localInit, localFinally);
    }

    /// <summary>执行具有 64 位索引和线程本地数据的 foreach（在 Visual Basic 中为 For Each）操作，其中在 <see cref="T:System.Collections.Concurrent.OrderablePartitioner`1" /> 上可能会并行运行迭代，而且可以配置循环选项，可以监视和操作循环的状态。</summary>
    /// <returns>包含有关已完成的循环部分的信息的结构。</returns>
    /// <param name="source">包含原始数据源的可排序分区程序。</param>
    /// <param name="parallelOptions">一个对象，用于配置此操作的行为。</param>
    /// <param name="localInit">用于返回每个任务的本地数据的初始状态的函数委托。</param>
    /// <param name="body">将为每个迭代调用一次的委托。</param>
    /// <param name="localFinally">用于对每个任务的本地状态执行一个最终操作的委托。</param>
    /// <typeparam name="TSource">
    /// <paramref name="source" /> 中的元素的类型。</typeparam>
    /// <typeparam name="TLocal">线程本地数据的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is null.-or-The <paramref name="parallelOptions" /> argument is null.-or-The <paramref name="body" /> argument is null.-or-The <paramref name="localInit" /> or <paramref name="localFinally" /> argument is null.</exception>
    /// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Collections.Concurrent.Partitioner`1.SupportsDynamicPartitions" /> property in the <paramref name="source" /><see cref="T:System.Collections.Concurrent.Partitioner" /> returns false or the partitioner returns null  partitions.</exception>
    /// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
    /// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled.</exception>
    /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
    [__DynamicallyInvokable]
    public static ParallelLoopResult ForEach<TSource, TLocal>(OrderablePartitioner<TSource> source, ParallelOptions parallelOptions, Func<TLocal> localInit, Func<TSource, ParallelLoopState, long, TLocal, TLocal> body, Action<TLocal> localFinally)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      if (body == null)
        throw new ArgumentNullException("body");
      if (localInit == null)
        throw new ArgumentNullException("localInit");
      if (localFinally == null)
        throw new ArgumentNullException("localFinally");
      if (parallelOptions == null)
        throw new ArgumentNullException("parallelOptions");
      if (!source.KeysNormalized)
        throw new InvalidOperationException(Environment.GetResourceString("Parallel_ForEach_OrderedPartitionerKeysNotNormalized"));
      return Parallel.PartitionerForEachWorker<TSource, TLocal>((Partitioner<TSource>) source, parallelOptions, (Action<TSource>) null, (Action<TSource, ParallelLoopState>) null, (Action<TSource, ParallelLoopState, long>) null, (Func<TSource, ParallelLoopState, TLocal, TLocal>) null, body, localInit, localFinally);
    }

    private static ParallelLoopResult PartitionerForEachWorker<TSource, TLocal>(Partitioner<TSource> source, ParallelOptions parallelOptions, Action<TSource> simpleBody, Action<TSource, ParallelLoopState> bodyWithState, Action<TSource, ParallelLoopState, long> bodyWithStateAndIndex, Func<TSource, ParallelLoopState, TLocal, TLocal> bodyWithStateAndLocal, Func<TSource, ParallelLoopState, long, TLocal, TLocal> bodyWithEverything, Func<TLocal> localInit, Action<TLocal> localFinally)
    {
      OrderablePartitioner<TSource> orderedSource = source as OrderablePartitioner<TSource>;
      if (!source.SupportsDynamicPartitions)
        throw new InvalidOperationException(Environment.GetResourceString("Parallel_ForEach_PartitionerNotDynamic"));
      CancellationToken cancellationToken = parallelOptions.CancellationToken;
      if (cancellationToken.IsCancellationRequested)
        throw new OperationCanceledException(parallelOptions.CancellationToken);
      int forkJoinContextID = 0;
      Task task = (Task) null;
      if (TplEtwProvider.Log.IsEnabled())
      {
        forkJoinContextID = Interlocked.Increment(ref Parallel.s_forkJoinContextID);
        task = Task.InternalCurrent;
        TplEtwProvider.Log.ParallelLoopBegin(task != null ? task.m_taskScheduler.Id : TaskScheduler.Current.Id, task != null ? task.Id : 0, forkJoinContextID, TplEtwProvider.ForkJoinOperationType.ParallelForEach, 0L, 0L);
      }
      ParallelLoopStateFlags64 sharedPStateFlags = new ParallelLoopStateFlags64();
      ParallelLoopResult parallelLoopResult = new ParallelLoopResult();
      OperationCanceledException oce = (OperationCanceledException) null;
      CancellationTokenRegistration tokenRegistration = new CancellationTokenRegistration();
      cancellationToken = parallelOptions.CancellationToken;
      if (cancellationToken.CanBeCanceled)
      {
        cancellationToken = parallelOptions.CancellationToken;
        tokenRegistration = cancellationToken.InternalRegisterWithoutEC((Action<object>) (o =>
        {
          sharedPStateFlags.Cancel();
          oce = new OperationCanceledException(parallelOptions.CancellationToken);
        }), (object) null);
      }
      IEnumerable<TSource> partitionerSource = (IEnumerable<TSource>) null;
      IEnumerable<KeyValuePair<long, TSource>> orderablePartitionerSource = (IEnumerable<KeyValuePair<long, TSource>>) null;
      if (orderedSource != null)
      {
        orderablePartitionerSource = orderedSource.GetOrderableDynamicPartitions();
        if (orderablePartitionerSource == null)
          throw new InvalidOperationException(Environment.GetResourceString("Parallel_ForEach_PartitionerReturnedNull"));
      }
      else
      {
        partitionerSource = source.GetDynamicPartitions();
        if (partitionerSource == null)
          throw new InvalidOperationException(Environment.GetResourceString("Parallel_ForEach_PartitionerReturnedNull"));
      }
      ParallelForReplicatingTask rootTask = (ParallelForReplicatingTask) null;
      Action action = (Action) (() =>
      {
        Task internalCurrent = Task.InternalCurrent;
        if (TplEtwProvider.Log.IsEnabled())
          TplEtwProvider.Log.ParallelFork(internalCurrent != null ? internalCurrent.m_taskScheduler.Id : TaskScheduler.Current.Id, internalCurrent != null ? internalCurrent.Id : 0, forkJoinContextID);
        TLocal local = default (TLocal);
        bool flag1 = false;
        IDisposable disposable = (IDisposable) null;
        try
        {
          ParallelLoopState64 parallelLoopState64 = (ParallelLoopState64) null;
          if (bodyWithState != null || bodyWithStateAndIndex != null)
            parallelLoopState64 = new ParallelLoopState64(sharedPStateFlags);
          else if (bodyWithStateAndLocal != null || bodyWithEverything != null)
          {
            parallelLoopState64 = new ParallelLoopState64(sharedPStateFlags);
            if (localInit != null)
            {
              local = localInit();
              flag1 = true;
            }
          }
          bool flag2 = rootTask == internalCurrent;
          Parallel.LoopTimer loopTimer = new Parallel.LoopTimer(rootTask.ActiveChildCount);
          if (orderedSource != null)
          {
            IEnumerator<KeyValuePair<long, TSource>> enumerator = internalCurrent.SavedStateFromPreviousReplica as IEnumerator<KeyValuePair<long, TSource>>;
            if (enumerator == null)
            {
              enumerator = orderablePartitionerSource.GetEnumerator();
              if (enumerator == null)
                throw new InvalidOperationException(Environment.GetResourceString("Parallel_ForEach_NullEnumerator"));
            }
            disposable = (IDisposable) enumerator;
            while (enumerator.MoveNext())
            {
              KeyValuePair<long, TSource> current = enumerator.Current;
              long key = current.Key;
              TSource source1 = current.Value;
              if (parallelLoopState64 != null)
                parallelLoopState64.CurrentIteration = key;
              if (simpleBody != null)
                simpleBody(source1);
              else if (bodyWithState != null)
                bodyWithState(source1, (ParallelLoopState) parallelLoopState64);
              else if (bodyWithStateAndIndex != null)
                bodyWithStateAndIndex(source1, (ParallelLoopState) parallelLoopState64, key);
              else
                local = bodyWithStateAndLocal == null ? bodyWithEverything(source1, (ParallelLoopState) parallelLoopState64, key, local) : bodyWithStateAndLocal(source1, (ParallelLoopState) parallelLoopState64, local);
              if (sharedPStateFlags.ShouldExitLoop(key))
                break;
              if (!flag2 && loopTimer.LimitExceeded())
              {
                internalCurrent.SavedStateForNextReplica = (object) enumerator;
                disposable = (IDisposable) null;
                break;
              }
            }
          }
          else
          {
            IEnumerator<TSource> enumerator = internalCurrent.SavedStateFromPreviousReplica as IEnumerator<TSource>;
            if (enumerator == null)
            {
              enumerator = partitionerSource.GetEnumerator();
              if (enumerator == null)
                throw new InvalidOperationException(Environment.GetResourceString("Parallel_ForEach_NullEnumerator"));
            }
            disposable = (IDisposable) enumerator;
            if (parallelLoopState64 != null)
              parallelLoopState64.CurrentIteration = 0L;
            while (enumerator.MoveNext())
            {
              TSource current = enumerator.Current;
              if (simpleBody != null)
                simpleBody(current);
              else if (bodyWithState != null)
                bodyWithState(current, (ParallelLoopState) parallelLoopState64);
              else if (bodyWithStateAndLocal != null)
                local = bodyWithStateAndLocal(current, (ParallelLoopState) parallelLoopState64, local);
              if (sharedPStateFlags.LoopStateFlags != ParallelLoopStateFlags.PLS_NONE)
                break;
              if (!flag2 && loopTimer.LimitExceeded())
              {
                internalCurrent.SavedStateForNextReplica = (object) enumerator;
                disposable = (IDisposable) null;
                break;
              }
            }
          }
        }
        catch
        {
          sharedPStateFlags.SetExceptional();
          throw;
        }
        finally
        {
          if (localFinally != null & flag1)
            localFinally(local);
          if (disposable != null)
            disposable.Dispose();
          if (TplEtwProvider.Log.IsEnabled())
            TplEtwProvider.Log.ParallelJoin(internalCurrent != null ? internalCurrent.m_taskScheduler.Id : TaskScheduler.Current.Id, internalCurrent != null ? internalCurrent.Id : 0, forkJoinContextID);
        }
      });
      try
      {
        rootTask = new ParallelForReplicatingTask(parallelOptions, action, TaskCreationOptions.None, InternalTaskOptions.SelfReplicating);
        rootTask.RunSynchronously(parallelOptions.EffectiveTaskScheduler);
        rootTask.Wait();
        cancellationToken = parallelOptions.CancellationToken;
        if (cancellationToken.CanBeCanceled)
          tokenRegistration.Dispose();
        if (oce != null)
          throw oce;
      }
      catch (AggregateException ex)
      {
        if (parallelOptions.CancellationToken.CanBeCanceled)
          tokenRegistration.Dispose();
        Parallel.ThrowIfReducableToSingleOCE((IEnumerable<Exception>) ex.InnerExceptions, parallelOptions.CancellationToken);
        throw;
      }
      catch (TaskSchedulerException ex)
      {
        if (parallelOptions.CancellationToken.CanBeCanceled)
          tokenRegistration.Dispose();
        throw;
      }
      finally
      {
        int loopStateFlags = sharedPStateFlags.LoopStateFlags;
        parallelLoopResult.m_completed = loopStateFlags == ParallelLoopStateFlags.PLS_NONE;
        if ((loopStateFlags & ParallelLoopStateFlags.PLS_BROKEN) != 0)
          parallelLoopResult.m_lowestBreakIteration = new long?(sharedPStateFlags.LowestBreakIteration);
        if (rootTask != null && rootTask.IsCompleted)
          rootTask.Dispose();
        IDisposable disposable = orderablePartitionerSource == null ? partitionerSource as IDisposable : orderablePartitionerSource as IDisposable;
        if (disposable != null)
          disposable.Dispose();
        if (TplEtwProvider.Log.IsEnabled())
          TplEtwProvider.Log.ParallelLoopEnd(task != null ? task.m_taskScheduler.Id : TaskScheduler.Current.Id, task != null ? task.Id : 0, forkJoinContextID, 0L);
      }
      return parallelLoopResult;
    }

    internal static void ThrowIfReducableToSingleOCE(IEnumerable<Exception> excCollection, CancellationToken ct)
    {
      bool flag = false;
      if (!ct.IsCancellationRequested)
        return;
      foreach (Exception exc in excCollection)
      {
        flag = true;
        OperationCanceledException canceledException = exc as OperationCanceledException;
        if (canceledException == null || canceledException.CancellationToken != ct)
          return;
      }
      if (flag)
        throw new OperationCanceledException(ct);
    }

    internal struct LoopTimer
    {
      private const int s_BaseNotifyPeriodMS = 100;
      private const int s_NotifyPeriodIncrementMS = 50;
      private int m_timeLimit;

      public LoopTimer(int nWorkerTaskIndex)
      {
        this.m_timeLimit = Environment.TickCount + (100 + nWorkerTaskIndex % PlatformHelper.ProcessorCount * 50);
      }

      public bool LimitExceeded()
      {
        return Environment.TickCount > this.m_timeLimit;
      }
    }
  }
}
