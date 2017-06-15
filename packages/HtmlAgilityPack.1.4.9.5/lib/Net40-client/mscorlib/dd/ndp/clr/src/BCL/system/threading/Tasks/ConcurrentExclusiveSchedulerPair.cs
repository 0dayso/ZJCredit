// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.ConcurrentExclusiveSchedulerPair
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security;
using System.Security.Permissions;

namespace System.Threading.Tasks
{
  /// <summary>提供任务计划程序，其用于执行任务，同时确保并发任务可同时运行，而独占任务从不运行。</summary>
  [DebuggerDisplay("Concurrent={ConcurrentTaskCountForDebugger}, Exclusive={ExclusiveTaskCountForDebugger}, Mode={ModeForDebugger}")]
  [DebuggerTypeProxy(typeof (ConcurrentExclusiveSchedulerPair.DebugView))]
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public class ConcurrentExclusiveSchedulerPair
  {
    private readonly ConcurrentDictionary<int, ConcurrentExclusiveSchedulerPair.ProcessingMode> m_threadProcessingMapping = new ConcurrentDictionary<int, ConcurrentExclusiveSchedulerPair.ProcessingMode>();
    private readonly ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler m_concurrentTaskScheduler;
    private readonly ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler m_exclusiveTaskScheduler;
    private readonly TaskScheduler m_underlyingTaskScheduler;
    private readonly int m_maxConcurrencyLevel;
    private readonly int m_maxItemsPerTask;
    private int m_processingCount;
    private ConcurrentExclusiveSchedulerPair.CompletionState m_completionState;
    private const int UNLIMITED_PROCESSING = -1;
    private const int EXCLUSIVE_PROCESSING_SENTINEL = -1;
    private const int DEFAULT_MAXITEMSPERTASK = -1;

    private static int DefaultMaxConcurrencyLevel
    {
      get
      {
        return Environment.ProcessorCount;
      }
    }

    private object ValueLock
    {
      get
      {
        return (object) this.m_threadProcessingMapping;
      }
    }

    /// <summary>当调度程序已经完成处理过程时，获取将要完成的 <see cref="T:System.Threading.Tasks.Task" />。</summary>
    /// <returns>计划程序完成处理时将完成的异步操作。</returns>
    [__DynamicallyInvokable]
    public Task Completion
    {
      [__DynamicallyInvokable] get
      {
        return (Task) this.EnsureCompletionStateInitialized().Task;
      }
    }

    private bool CompletionRequested
    {
      get
      {
        if (this.m_completionState != null)
          return Volatile.Read(ref this.m_completionState.m_completionRequested);
        return false;
      }
    }

    private bool ReadyToComplete
    {
      get
      {
        if (!this.CompletionRequested || this.m_processingCount != 0)
          return false;
        ConcurrentExclusiveSchedulerPair.CompletionState completionState = this.EnsureCompletionStateInitialized();
        if (completionState.m_exceptions != null && completionState.m_exceptions.Count > 0)
          return true;
        if (this.m_concurrentTaskScheduler.m_tasks.IsEmpty)
          return this.m_exclusiveTaskScheduler.m_tasks.IsEmpty;
        return false;
      }
    }

    /// <summary>获取可用于调度任务到此添加线对的 <see cref="T:System.Threading.Tasks.TaskScheduler" />，其中的线对可以与此线对上的其他任务同时运行。</summary>
    /// <returns>可用于当前安排任务的对象。</returns>
    [__DynamicallyInvokable]
    public TaskScheduler ConcurrentScheduler
    {
      [__DynamicallyInvokable] get
      {
        return (TaskScheduler) this.m_concurrentTaskScheduler;
      }
    }

    /// <summary>获取可用于调度任务到此添加线对的 <see cref="T:System.Threading.Tasks.TaskScheduler" />，其中的线对必须独占地运行，不考虑此线对上的其他任务。</summary>
    /// <returns>可用于安排任务没有与其他任务并发运行的对象。</returns>
    [__DynamicallyInvokable]
    public TaskScheduler ExclusiveScheduler
    {
      [__DynamicallyInvokable] get
      {
        return (TaskScheduler) this.m_exclusiveTaskScheduler;
      }
    }

    private int ConcurrentTaskCountForDebugger
    {
      get
      {
        return this.m_concurrentTaskScheduler.m_tasks.Count;
      }
    }

    private int ExclusiveTaskCountForDebugger
    {
      get
      {
        return this.m_exclusiveTaskScheduler.m_tasks.Count;
      }
    }

    private ConcurrentExclusiveSchedulerPair.ProcessingMode ModeForDebugger
    {
      get
      {
        if (this.m_completionState != null && this.m_completionState.Task.IsCompleted)
          return ConcurrentExclusiveSchedulerPair.ProcessingMode.Completed;
        ConcurrentExclusiveSchedulerPair.ProcessingMode processingMode = ConcurrentExclusiveSchedulerPair.ProcessingMode.NotCurrentlyProcessing;
        if (this.m_processingCount == -1)
          processingMode |= ConcurrentExclusiveSchedulerPair.ProcessingMode.ProcessingExclusiveTask;
        if (this.m_processingCount >= 1)
          processingMode |= ConcurrentExclusiveSchedulerPair.ProcessingMode.ProcessingConcurrentTasks;
        if (this.CompletionRequested)
          processingMode |= ConcurrentExclusiveSchedulerPair.ProcessingMode.Completing;
        return processingMode;
      }
    }

    /// <summary>初始化 <see cref="T:System.Threading.Tasks.ConcurrentExclusiveSchedulerPair" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public ConcurrentExclusiveSchedulerPair()
      : this(TaskScheduler.Default, ConcurrentExclusiveSchedulerPair.DefaultMaxConcurrencyLevel, -1)
    {
    }

    /// <summary>用指定的计划程序目标初始化 <see cref="T:System.Threading.Tasks.ConcurrentExclusiveSchedulerPair" /> 类的新实例。</summary>
    /// <param name="taskScheduler">应执行的目标计划程序。</param>
    [__DynamicallyInvokable]
    public ConcurrentExclusiveSchedulerPair(TaskScheduler taskScheduler)
      : this(taskScheduler, ConcurrentExclusiveSchedulerPair.DefaultMaxConcurrencyLevel, -1)
    {
    }

    /// <summary>初始化针对最大并发级别的指定计划程序 <see cref="T:System.Threading.Tasks.ConcurrentExclusiveSchedulerPair" /> 类的新实例。</summary>
    /// <param name="taskScheduler">应执行的目标计划程序。</param>
    /// <param name="maxConcurrencyLevel">并发运行的最大任务数。</param>
    [__DynamicallyInvokable]
    public ConcurrentExclusiveSchedulerPair(TaskScheduler taskScheduler, int maxConcurrencyLevel)
      : this(taskScheduler, maxConcurrencyLevel, -1)
    {
    }

    /// <summary>初始化 <see cref="T:System.Threading.Tasks.ConcurrentExclusiveSchedulerPair" />  类的新实例，该实例针对最大并发级别和最大计划任务数的指定计划程序，并可能作为一个单元来处理。</summary>
    /// <param name="taskScheduler">应执行的目标计划程序。</param>
    /// <param name="maxConcurrencyLevel">并发运行的最大任务数。</param>
    /// <param name="maxItemsPerTask">要处理的被成对用于每一个基础计划任务的最大任务数。</param>
    [__DynamicallyInvokable]
    public ConcurrentExclusiveSchedulerPair(TaskScheduler taskScheduler, int maxConcurrencyLevel, int maxItemsPerTask)
    {
      if (taskScheduler == null)
        throw new ArgumentNullException("taskScheduler");
      if (maxConcurrencyLevel == 0 || maxConcurrencyLevel < -1)
        throw new ArgumentOutOfRangeException("maxConcurrencyLevel");
      if (maxItemsPerTask == 0 || maxItemsPerTask < -1)
        throw new ArgumentOutOfRangeException("maxItemsPerTask");
      this.m_underlyingTaskScheduler = taskScheduler;
      this.m_maxConcurrencyLevel = maxConcurrencyLevel;
      this.m_maxItemsPerTask = maxItemsPerTask;
      int concurrencyLevel = taskScheduler.MaximumConcurrencyLevel;
      if (concurrencyLevel > 0 && concurrencyLevel < this.m_maxConcurrencyLevel)
        this.m_maxConcurrencyLevel = concurrencyLevel;
      if (this.m_maxConcurrencyLevel == -1)
        this.m_maxConcurrencyLevel = int.MaxValue;
      if (this.m_maxItemsPerTask == -1)
        this.m_maxItemsPerTask = int.MaxValue;
      this.m_exclusiveTaskScheduler = new ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler(this, 1, ConcurrentExclusiveSchedulerPair.ProcessingMode.ProcessingExclusiveTask);
      this.m_concurrentTaskScheduler = new ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler(this, this.m_maxConcurrencyLevel, ConcurrentExclusiveSchedulerPair.ProcessingMode.ProcessingConcurrentTasks);
    }

    /// <summary>通知不能接受更多任务的计划程序对。</summary>
    [__DynamicallyInvokable]
    public void Complete()
    {
      lock (this.ValueLock)
      {
        if (this.CompletionRequested)
          return;
        this.RequestCompletion();
        this.CleanupStateIfCompletingAndQuiesced();
      }
    }

    private ConcurrentExclusiveSchedulerPair.CompletionState EnsureCompletionStateInitialized()
    {
      return LazyInitializer.EnsureInitialized<ConcurrentExclusiveSchedulerPair.CompletionState>(ref this.m_completionState, (Func<ConcurrentExclusiveSchedulerPair.CompletionState>) (() => new ConcurrentExclusiveSchedulerPair.CompletionState()));
    }

    private void RequestCompletion()
    {
      this.EnsureCompletionStateInitialized().m_completionRequested = true;
    }

    private void CleanupStateIfCompletingAndQuiesced()
    {
      if (!this.ReadyToComplete)
        return;
      this.CompleteTaskAsync();
    }

    private void CompleteTaskAsync()
    {
      ConcurrentExclusiveSchedulerPair.CompletionState completionState1 = this.EnsureCompletionStateInitialized();
      if (completionState1.m_completionQueued)
        return;
      completionState1.m_completionQueued = true;
      ThreadPool.QueueUserWorkItem((WaitCallback) (state =>
      {
        ConcurrentExclusiveSchedulerPair.CompletionState completionState2 = (ConcurrentExclusiveSchedulerPair.CompletionState) state;
        List<Exception> exceptionList = completionState2.m_exceptions;
        if (exceptionList == null || exceptionList.Count <= 0)
          completionState2.TrySetResult(new VoidTaskResult());
        else
          completionState2.TrySetException((IEnumerable<Exception>) exceptionList);
      }), (object) completionState1);
    }

    private void FaultWithTask(Task faultedTask)
    {
      ConcurrentExclusiveSchedulerPair.CompletionState completionState = this.EnsureCompletionStateInitialized();
      if (completionState.m_exceptions == null)
        completionState.m_exceptions = new List<Exception>();
      completionState.m_exceptions.AddRange((IEnumerable<Exception>) faultedTask.Exception.InnerExceptions);
      this.RequestCompletion();
    }

    private void ProcessAsyncIfNecessary(bool fairly = false)
    {
      if (this.m_processingCount < 0)
        return;
      bool flag = !this.m_exclusiveTaskScheduler.m_tasks.IsEmpty;
      Task faultedTask = (Task) null;
      if (this.m_processingCount == 0 & flag)
      {
        this.m_processingCount = -1;
        try
        {
          CancellationToken cancellationToken = new CancellationToken();
          int num = (int) ConcurrentExclusiveSchedulerPair.GetCreationOptionsForTask(fairly);
          faultedTask = new Task((Action<object>) (thisPair => ((ConcurrentExclusiveSchedulerPair) thisPair).ProcessExclusiveTasks()), (object) this, cancellationToken, (TaskCreationOptions) num);
          faultedTask.Start(this.m_underlyingTaskScheduler);
        }
        catch
        {
          this.m_processingCount = 0;
          this.FaultWithTask(faultedTask);
        }
      }
      else
      {
        int count = this.m_concurrentTaskScheduler.m_tasks.Count;
        if (count > 0 && !flag && this.m_processingCount < this.m_maxConcurrencyLevel)
        {
          for (int index = 0; index < count && this.m_processingCount < this.m_maxConcurrencyLevel; ++index)
          {
            this.m_processingCount = this.m_processingCount + 1;
            try
            {
              CancellationToken cancellationToken = new CancellationToken();
              int num = (int) ConcurrentExclusiveSchedulerPair.GetCreationOptionsForTask(fairly);
              faultedTask = new Task((Action<object>) (thisPair => ((ConcurrentExclusiveSchedulerPair) thisPair).ProcessConcurrentTasks()), (object) this, cancellationToken, (TaskCreationOptions) num);
              faultedTask.Start(this.m_underlyingTaskScheduler);
            }
            catch
            {
              this.m_processingCount = this.m_processingCount - 1;
              this.FaultWithTask(faultedTask);
            }
          }
        }
      }
      this.CleanupStateIfCompletingAndQuiesced();
    }

    private void ProcessExclusiveTasks()
    {
      try
      {
        this.m_threadProcessingMapping[Thread.CurrentThread.ManagedThreadId] = ConcurrentExclusiveSchedulerPair.ProcessingMode.ProcessingExclusiveTask;
        Task result;
        for (int index = 0; index < this.m_maxItemsPerTask && this.m_exclusiveTaskScheduler.m_tasks.TryDequeue(out result); ++index)
        {
          if (!result.IsFaulted)
            this.m_exclusiveTaskScheduler.ExecuteTask(result);
        }
      }
      finally
      {
        ConcurrentExclusiveSchedulerPair.ProcessingMode processingMode;
        this.m_threadProcessingMapping.TryRemove(Thread.CurrentThread.ManagedThreadId, out processingMode);
        lock (this.ValueLock)
        {
          this.m_processingCount = 0;
          this.ProcessAsyncIfNecessary(true);
        }
      }
    }

    private void ProcessConcurrentTasks()
    {
      try
      {
        this.m_threadProcessingMapping[Thread.CurrentThread.ManagedThreadId] = ConcurrentExclusiveSchedulerPair.ProcessingMode.ProcessingConcurrentTasks;
        Task result;
        for (int index = 0; index < this.m_maxItemsPerTask && this.m_concurrentTaskScheduler.m_tasks.TryDequeue(out result); ++index)
        {
          if (!result.IsFaulted)
            this.m_concurrentTaskScheduler.ExecuteTask(result);
          if (!this.m_exclusiveTaskScheduler.m_tasks.IsEmpty)
            break;
        }
      }
      finally
      {
        ConcurrentExclusiveSchedulerPair.ProcessingMode processingMode;
        this.m_threadProcessingMapping.TryRemove(Thread.CurrentThread.ManagedThreadId, out processingMode);
        lock (this.ValueLock)
        {
          if (this.m_processingCount > 0)
            this.m_processingCount = this.m_processingCount - 1;
          this.ProcessAsyncIfNecessary(true);
        }
      }
    }

    [Conditional("DEBUG")]
    internal static void ContractAssertMonitorStatus(object syncObj, bool held)
    {
    }

    internal static TaskCreationOptions GetCreationOptionsForTask(bool isReplacementReplica = false)
    {
      TaskCreationOptions taskCreationOptions = TaskCreationOptions.DenyChildAttach;
      if (isReplacementReplica)
        taskCreationOptions |= TaskCreationOptions.PreferFairness;
      return taskCreationOptions;
    }

    private sealed class CompletionState : TaskCompletionSource<VoidTaskResult>
    {
      internal bool m_completionRequested;
      internal bool m_completionQueued;
      internal List<Exception> m_exceptions;
    }

    [DebuggerDisplay("Count={CountForDebugger}, MaxConcurrencyLevel={m_maxConcurrencyLevel}, Id={Id}")]
    [DebuggerTypeProxy(typeof (ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler.DebugView))]
    private sealed class ConcurrentExclusiveTaskScheduler : TaskScheduler
    {
      private static readonly Func<object, bool> s_tryExecuteTaskShim = new Func<object, bool>(ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler.TryExecuteTaskShim);
      private readonly ConcurrentExclusiveSchedulerPair m_pair;
      private readonly int m_maxConcurrencyLevel;
      private readonly ConcurrentExclusiveSchedulerPair.ProcessingMode m_processingMode;
      internal readonly IProducerConsumerQueue<Task> m_tasks;

      public override int MaximumConcurrencyLevel
      {
        get
        {
          return this.m_maxConcurrencyLevel;
        }
      }

      private int CountForDebugger
      {
        get
        {
          return this.m_tasks.Count;
        }
      }

      internal ConcurrentExclusiveTaskScheduler(ConcurrentExclusiveSchedulerPair pair, int maxConcurrencyLevel, ConcurrentExclusiveSchedulerPair.ProcessingMode processingMode)
      {
        this.m_pair = pair;
        this.m_maxConcurrencyLevel = maxConcurrencyLevel;
        this.m_processingMode = processingMode;
        this.m_tasks = processingMode == ConcurrentExclusiveSchedulerPair.ProcessingMode.ProcessingExclusiveTask ? (IProducerConsumerQueue<Task>) new SingleProducerSingleConsumerQueue<Task>() : (IProducerConsumerQueue<Task>) new MultiProducerMultiConsumerQueue<Task>();
      }

      [SecurityCritical]
      protected internal override void QueueTask(Task task)
      {
        lock (this.m_pair.ValueLock)
        {
          if (this.m_pair.CompletionRequested)
            throw new InvalidOperationException(this.GetType().Name);
          this.m_tasks.Enqueue(task);
          this.m_pair.ProcessAsyncIfNecessary(false);
        }
      }

      [SecuritySafeCritical]
      internal void ExecuteTask(Task task)
      {
        this.TryExecuteTask(task);
      }

      [SecurityCritical]
      protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
      {
        if (!taskWasPreviouslyQueued && this.m_pair.CompletionRequested)
          return false;
        bool flag = this.m_pair.m_underlyingTaskScheduler == TaskScheduler.Default;
        ConcurrentExclusiveSchedulerPair.ProcessingMode processingMode;
        if (flag & taskWasPreviouslyQueued && !Thread.CurrentThread.IsThreadPoolThread || (!this.m_pair.m_threadProcessingMapping.TryGetValue(Thread.CurrentThread.ManagedThreadId, out processingMode) || processingMode != this.m_processingMode))
          return false;
        if (!flag || taskWasPreviouslyQueued)
          return this.TryExecuteTaskInlineOnTargetScheduler(task);
        return this.TryExecuteTask(task);
      }

      private bool TryExecuteTaskInlineOnTargetScheduler(Task task)
      {
        Task<bool> task1 = new Task<bool>(ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler.s_tryExecuteTaskShim, (object) Tuple.Create<ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler, Task>(this, task));
        try
        {
          task1.RunSynchronously(this.m_pair.m_underlyingTaskScheduler);
          return task1.Result;
        }
        catch
        {
          AggregateException exception = task1.Exception;
          throw;
        }
        finally
        {
          task1.Dispose();
        }
      }

      [SecuritySafeCritical]
      private static bool TryExecuteTaskShim(object state)
      {
        Tuple<ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler, Task> tuple = (Tuple<ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler, Task>) state;
        return tuple.Item1.TryExecuteTask(tuple.Item2);
      }

      [SecurityCritical]
      protected override IEnumerable<Task> GetScheduledTasks()
      {
        return (IEnumerable<Task>) this.m_tasks;
      }

      private sealed class DebugView
      {
        private readonly ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler m_taskScheduler;

        public int MaximumConcurrencyLevel
        {
          get
          {
            return this.m_taskScheduler.m_maxConcurrencyLevel;
          }
        }

        public IEnumerable<Task> ScheduledTasks
        {
          get
          {
            return (IEnumerable<Task>) this.m_taskScheduler.m_tasks;
          }
        }

        public ConcurrentExclusiveSchedulerPair SchedulerPair
        {
          get
          {
            return this.m_taskScheduler.m_pair;
          }
        }

        public DebugView(ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler scheduler)
        {
          this.m_taskScheduler = scheduler;
        }
      }
    }

    private sealed class DebugView
    {
      private readonly ConcurrentExclusiveSchedulerPair m_pair;

      public ConcurrentExclusiveSchedulerPair.ProcessingMode Mode
      {
        get
        {
          return this.m_pair.ModeForDebugger;
        }
      }

      public IEnumerable<Task> ScheduledExclusive
      {
        get
        {
          return (IEnumerable<Task>) this.m_pair.m_exclusiveTaskScheduler.m_tasks;
        }
      }

      public IEnumerable<Task> ScheduledConcurrent
      {
        get
        {
          return (IEnumerable<Task>) this.m_pair.m_concurrentTaskScheduler.m_tasks;
        }
      }

      public int CurrentlyExecutingTaskCount
      {
        get
        {
          if (this.m_pair.m_processingCount != -1)
            return this.m_pair.m_processingCount;
          return 1;
        }
      }

      public TaskScheduler TargetScheduler
      {
        get
        {
          return this.m_pair.m_underlyingTaskScheduler;
        }
      }

      public DebugView(ConcurrentExclusiveSchedulerPair pair)
      {
        this.m_pair = pair;
      }
    }

    [Flags]
    private enum ProcessingMode : byte
    {
      NotCurrentlyProcessing = 0,
      ProcessingExclusiveTask = 1,
      ProcessingConcurrentTasks = 2,
      Completing = 4,
      Completed = 8,
    }
  }
}
