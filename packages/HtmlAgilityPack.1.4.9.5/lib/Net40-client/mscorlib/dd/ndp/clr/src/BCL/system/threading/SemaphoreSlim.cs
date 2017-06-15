// Decompiled with JetBrains decompiler
// Type: System.Threading.SemaphoreSlim
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace System.Threading
{
  /// <summary>对可同时访问资源或资源池的线程数加以限制的 <see cref="T:System.Threading.Semaphore" /> 的轻量替代。</summary>
  [ComVisible(false)]
  [DebuggerDisplay("Current Count = {m_currentCount}")]
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public class SemaphoreSlim : IDisposable
  {
    private static readonly Task<bool> s_trueTask = new Task<bool>(false, true, (TaskCreationOptions) 16384, new CancellationToken());
    private static Action<object> s_cancellationTokenCanceledEventHandler = new Action<object>(SemaphoreSlim.CancellationTokenCanceledEventHandler);
    private volatile int m_currentCount;
    private readonly int m_maxCount;
    private volatile int m_waitCount;
    private object m_lockObj;
    private volatile ManualResetEvent m_waitHandle;
    private SemaphoreSlim.TaskNode m_asyncHead;
    private SemaphoreSlim.TaskNode m_asyncTail;
    private const int NO_MAXIMUM = 2147483647;

    /// <summary>获取可以输入 <see cref="T:System.Threading.SemaphoreSlim" /> 对象的剩余线程数。</summary>
    /// <returns>可以输入信号量的剩余线程数。</returns>
    [__DynamicallyInvokable]
    public int CurrentCount
    {
      [__DynamicallyInvokable] get
      {
        return this.m_currentCount;
      }
    }

    /// <summary>返回一个可用于在信号量上等待的 <see cref="T:System.Threading.WaitHandle" />。</summary>
    /// <returns>可用于在信号量上等待的 <see cref="T:System.Threading.WaitHandle" />。</returns>
    /// <exception cref="T:System.ObjectDisposedException">已释放了 <see cref="T:System.Threading.SemaphoreSlim" />。</exception>
    [__DynamicallyInvokable]
    public WaitHandle AvailableWaitHandle
    {
      [__DynamicallyInvokable] get
      {
        this.CheckDispose();
        if (this.m_waitHandle != null)
          return (WaitHandle) this.m_waitHandle;
        lock (this.m_lockObj)
        {
          if (this.m_waitHandle == null)
            this.m_waitHandle = new ManualResetEvent((uint) this.m_currentCount > 0U);
        }
        return (WaitHandle) this.m_waitHandle;
      }
    }

    /// <summary>初始化 <see cref="T:System.Threading.SemaphoreSlim" /> 类的新实例，以指定可同时授予的请求的初始数量。</summary>
    /// <param name="initialCount">可以同时授予的信号量的初始请求数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="initialCount" /> 小于 0。</exception>
    [__DynamicallyInvokable]
    public SemaphoreSlim(int initialCount)
      : this(initialCount, int.MaxValue)
    {
    }

    /// <summary>初始化 <see cref="T:System.Threading.SemaphoreSlim" /> 类的新实例，同时指定可同时授予的请求的初始数量和最大数量。</summary>
    /// <param name="initialCount">可以同时授予的信号量的初始请求数。</param>
    /// <param name="maxCount">可以同时授予的信号量的最大请求数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="initialCount" /> 小于 0，或 <paramref name="initialCount" /> 大于 <paramref name="maxCount" />，或 <paramref name="maxCount" /> 小于等于 0。</exception>
    [__DynamicallyInvokable]
    public SemaphoreSlim(int initialCount, int maxCount)
    {
      if (initialCount < 0 || initialCount > maxCount)
        throw new ArgumentOutOfRangeException("initialCount", (object) initialCount, SemaphoreSlim.GetResourceString("SemaphoreSlim_ctor_InitialCountWrong"));
      if (maxCount <= 0)
        throw new ArgumentOutOfRangeException("maxCount", (object) maxCount, SemaphoreSlim.GetResourceString("SemaphoreSlim_ctor_MaxCountWrong"));
      this.m_maxCount = maxCount;
      this.m_lockObj = new object();
      this.m_currentCount = initialCount;
    }

    /// <summary>阻止当前线程，直至它可进入 <see cref="T:System.Threading.SemaphoreSlim" /> 为止。</summary>
    /// <exception cref="T:System.ObjectDisposedException">当前实例已被释放。</exception>
    [__DynamicallyInvokable]
    public void Wait()
    {
      this.Wait(-1, new CancellationToken());
    }

    /// <summary>阻止当前线程，直至它可进入 <see cref="T:System.Threading.SemaphoreSlim" /> 为止，同时观察 <see cref="T:System.Threading.CancellationToken" />。</summary>
    /// <param name="cancellationToken">要观察的 <see cref="T:System.Threading.CancellationToken" /> 标记。</param>
    /// <exception cref="T:System.OperationCanceledException">
    /// <paramref name="cancellationToken" /> 已取消。</exception>
    /// <exception cref="T:System.ObjectDisposedException">当前实例已被释放。- 或 -<see cref="T:System.Threading.CancellationTokenSource" /> 创建<paramref name=" cancellationToken" /> 已释放。</exception>
    [__DynamicallyInvokable]
    public void Wait(CancellationToken cancellationToken)
    {
      this.Wait(-1, cancellationToken);
    }

    /// <summary>阻止当前线程，直至它可进入 <see cref="T:System.Threading.SemaphoreSlim" /> 为止，同时使用 <see cref="T:System.TimeSpan" /> 来指定超时。</summary>
    /// <returns>如果当前线程成功进入 <see cref="T:System.Threading.SemaphoreSlim" />，则为 true；否则为 false。</returns>
    /// <param name="timeout">表示等待毫秒数的 <see cref="T:System.TimeSpan" />，或表示 -1 毫秒（无限期等待）的 <see cref="T:System.TimeSpan" />。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="timeout" /> 是 -1 毫秒之外的负数，表示无限超时或者超时大于 <see cref="F:System.Int32.MaxValue" />。</exception>
    /// <exception cref="T:System.ObjectDisposedException">semaphoreSlim 实例已处理 <paramref name="." /></exception>
    [__DynamicallyInvokable]
    public bool Wait(TimeSpan timeout)
    {
      long num = (long) timeout.TotalMilliseconds;
      if (num < -1L || num > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException("timeout", (object) timeout, SemaphoreSlim.GetResourceString("SemaphoreSlim_Wait_TimeoutWrong"));
      return this.Wait((int) timeout.TotalMilliseconds, new CancellationToken());
    }

    /// <summary>阻止当前线程，直至它可进入 <see cref="T:System.Threading.SemaphoreSlim" /> 为止，并使用 <see cref="T:System.TimeSpan" /> 来指定超时，同时观察 <see cref="T:System.Threading.CancellationToken" />。</summary>
    /// <returns>如果当前线程成功进入 <see cref="T:System.Threading.SemaphoreSlim" />，则为 true；否则为 false。</returns>
    /// <param name="timeout">表示等待毫秒数的 <see cref="T:System.TimeSpan" />，或表示 -1 毫秒（无限期等待）的 <see cref="T:System.TimeSpan" />。</param>
    /// <param name="cancellationToken">要观察的 <see cref="T:System.Threading.CancellationToken" />。</param>
    /// <exception cref="T:System.OperationCanceledException">
    /// <paramref name="cancellationToken" /> 已取消。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="timeout" /> 是 -1 毫秒之外的负数，表示无限超时或者超时大于 <see cref="F:System.Int32.MaxValue" />。</exception>
    /// <exception cref="T:System.ObjectDisposedException">semaphoreSlim 实例已处理 <paramref name="." /><paramref name="-or-" />创建了 <see cref="T:System.Threading.CancellationTokenSource" /> 的 <paramref name="cancellationToken" /> 已经被释放。</exception>
    [__DynamicallyInvokable]
    public bool Wait(TimeSpan timeout, CancellationToken cancellationToken)
    {
      long num = (long) timeout.TotalMilliseconds;
      if (num < -1L || num > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException("timeout", (object) timeout, SemaphoreSlim.GetResourceString("SemaphoreSlim_Wait_TimeoutWrong"));
      return this.Wait((int) timeout.TotalMilliseconds, cancellationToken);
    }

    /// <summary>阻止当前线程，直至它可进入 <see cref="T:System.Threading.SemaphoreSlim" /> 为止，同时使用 32 位带符号整数来指定超时。</summary>
    /// <returns>如果当前线程成功进入 <see cref="T:System.Threading.SemaphoreSlim" />，则为 true；否则为 false。</returns>
    /// <param name="millisecondsTimeout">等待的毫秒数，或为 <see cref="F:System.Threading.Timeout.Infinite" /> (-1)，表示无限期等待。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="millisecondsTimeout" /> 是一个非 -1 的负数，而 -1 表示无限期超时。</exception>
    [__DynamicallyInvokable]
    public bool Wait(int millisecondsTimeout)
    {
      return this.Wait(millisecondsTimeout, new CancellationToken());
    }

    /// <summary>阻止当前线程，直至它可进入 <see cref="T:System.Threading.SemaphoreSlim" /> 为止，并使用 32 位带符号整数来指定超时，同时观察 <see cref="T:System.Threading.CancellationToken" />。</summary>
    /// <returns>如果当前线程成功进入 <see cref="T:System.Threading.SemaphoreSlim" />，则为 true；否则为 false。</returns>
    /// <param name="millisecondsTimeout">等待的毫秒数，或为 <see cref="F:System.Threading.Timeout.Infinite" /> (-1)，表示无限期等待。</param>
    /// <param name="cancellationToken">要观察的 <see cref="T:System.Threading.CancellationToken" />。</param>
    /// <exception cref="T:System.OperationCanceledException">
    /// <paramref name="cancellationToken" /> 已取消。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="millisecondsTimeout" /> 是一个非 -1 的负数，而 -1 表示无限期超时。</exception>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.Threading.SemaphoreSlim" /> 实例已被释放，或 <see cref="T:System.Threading.CancellationTokenSource" /> 创建 <paramref name="cancellationToken" /> 已被释放。</exception>
    [__DynamicallyInvokable]
    public bool Wait(int millisecondsTimeout, CancellationToken cancellationToken)
    {
      this.CheckDispose();
      if (millisecondsTimeout < -1)
        throw new ArgumentOutOfRangeException("totalMilliSeconds", (object) millisecondsTimeout, SemaphoreSlim.GetResourceString("SemaphoreSlim_Wait_TimeoutWrong"));
      cancellationToken.ThrowIfCancellationRequested();
      uint startTime = 0;
      if (millisecondsTimeout != -1 && millisecondsTimeout > 0)
        startTime = TimeoutHelper.GetTime();
      bool flag = false;
      Task<bool> task = (Task<bool>) null;
      bool lockTaken = false;
      CancellationTokenRegistration tokenRegistration = cancellationToken.InternalRegisterWithoutEC(SemaphoreSlim.s_cancellationTokenCanceledEventHandler, (object) this);
      try
      {
        SpinWait spinWait = new SpinWait();
        while (this.m_currentCount == 0 && !spinWait.NextSpinWillYield)
          spinWait.SpinOnce();
        try
        {
        }
        finally
        {
          Monitor.Enter(this.m_lockObj, ref lockTaken);
          if (lockTaken)
            this.m_waitCount = this.m_waitCount + 1;
        }
        if (this.m_asyncHead != null)
        {
          task = this.WaitAsync(millisecondsTimeout, cancellationToken);
        }
        else
        {
          OperationCanceledException canceledException = (OperationCanceledException) null;
          if (this.m_currentCount == 0)
          {
            if (millisecondsTimeout == 0)
              return false;
            try
            {
              flag = this.WaitUntilCountOrTimeout(millisecondsTimeout, startTime, cancellationToken);
            }
            catch (OperationCanceledException ex)
            {
              canceledException = ex;
            }
          }
          if (this.m_currentCount > 0)
          {
            flag = true;
            this.m_currentCount = this.m_currentCount - 1;
          }
          else if (canceledException != null)
            throw canceledException;
          if (this.m_waitHandle != null)
          {
            if (this.m_currentCount == 0)
              this.m_waitHandle.Reset();
          }
        }
      }
      finally
      {
        if (lockTaken)
        {
          this.m_waitCount = this.m_waitCount - 1;
          Monitor.Exit(this.m_lockObj);
        }
        tokenRegistration.Dispose();
      }
      if (task == null)
        return flag;
      return task.GetAwaiter().GetResult();
    }

    private bool WaitUntilCountOrTimeout(int millisecondsTimeout, uint startTime, CancellationToken cancellationToken)
    {
      int millisecondsTimeout1 = -1;
      while (this.m_currentCount == 0)
      {
        cancellationToken.ThrowIfCancellationRequested();
        if (millisecondsTimeout != -1)
        {
          millisecondsTimeout1 = TimeoutHelper.UpdateTimeOut(startTime, millisecondsTimeout);
          if (millisecondsTimeout1 <= 0)
            return false;
        }
        if (!Monitor.Wait(this.m_lockObj, millisecondsTimeout1))
          return false;
      }
      return true;
    }

    /// <summary>输入 <see cref="T:System.Threading.SemaphoreSlim" /> 的异步等待。</summary>
    /// <returns>输入信号量时完成任务。</returns>
    [__DynamicallyInvokable]
    public Task WaitAsync()
    {
      return (Task) this.WaitAsync(-1, new CancellationToken());
    }

    /// <summary>在观察 <see cref="T:System.Threading.CancellationToken" /> 时，输入 <see cref="T:System.Threading.SemaphoreSlim" /> 的异步等待。</summary>
    /// <returns>输入信号量时完成任务。</returns>
    /// <param name="cancellationToken">要观察的 <see cref="T:System.Threading.CancellationToken" /> 标记。</param>
    /// <exception cref="T:System.ObjectDisposedException">当前实例已被释放。</exception>
    /// <exception cref="T:System.OperationCanceledException">
    /// <paramref name="cancellationToken" /> 已取消。</exception>
    [__DynamicallyInvokable]
    public Task WaitAsync(CancellationToken cancellationToken)
    {
      return (Task) this.WaitAsync(-1, cancellationToken);
    }

    /// <summary>输入 <see cref="T:System.Threading.SemaphoreSlim" /> 的异步等待，使用 32 位带符号整数度量时间间隔。</summary>
    /// <returns>如果当前线程成功输入了 <see cref="T:System.Threading.SemaphoreSlim" />，则为将通过 true 的结果一起完成的任务，否则将通过 false 的结果完成。</returns>
    /// <param name="millisecondsTimeout">等待的毫秒数，或为 <see cref="F:System.Threading.Timeout.Infinite" /> (-1)，表示无限期等待。</param>
    /// <exception cref="T:System.ObjectDisposedException">当前实例已被释放。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="millisecondsTimeout" /> 是一个非 -1 的负数，而 -1 表示无限期超时。</exception>
    [__DynamicallyInvokable]
    public Task<bool> WaitAsync(int millisecondsTimeout)
    {
      return this.WaitAsync(millisecondsTimeout, new CancellationToken());
    }

    /// <summary>输入 <see cref="T:System.Threading.SemaphoreSlim" /> 的异步等待，使用 <see cref="T:System.TimeSpan" /> 度量时间间隔。</summary>
    /// <returns>如果当前线程成功输入了 <see cref="T:System.Threading.SemaphoreSlim" />，则为将通过 true 的结果一起完成的任务，否则将通过 false 的结果完成。</returns>
    /// <param name="timeout">表示等待毫秒数的 <see cref="T:System.TimeSpan" />，或表示 -1 毫秒（无限期等待）的 <see cref="T:System.TimeSpan" />。</param>
    /// <exception cref="T:System.ObjectDisposedException">当前实例已被释放。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="millisecondsTimeout" /> 是一个非 -1 的负数，而 -1 表示无限期超时 - 或 - 超时大于 <see cref="F:System.Int32.MaxValue" />。</exception>
    [__DynamicallyInvokable]
    public Task<bool> WaitAsync(TimeSpan timeout)
    {
      return this.WaitAsync(timeout, new CancellationToken());
    }

    /// <summary>在观察 <see cref="T:System.Threading.CancellationToken" /> 时，输入 <see cref="T:System.Threading.SemaphoreSlim" /> 的异步等待，使用 <see cref="T:System.TimeSpan" /> 度量时间间隔。</summary>
    /// <returns>如果当前线程成功输入了 <see cref="T:System.Threading.SemaphoreSlim" />，则为将通过 true 的结果一起完成的任务，否则将通过 false 的结果完成。</returns>
    /// <param name="timeout">表示等待毫秒数的 <see cref="T:System.TimeSpan" />，或表示 -1 毫秒（无限期等待）的 <see cref="T:System.TimeSpan" />。</param>
    /// <param name="cancellationToken">要观察的 <see cref="T:System.Threading.CancellationToken" /> 标记。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="millisecondsTimeout" /> 是一个非 -1 的负数，而 -1 表示无限期超时- 或 -超时大于 <see cref="F:System.Int32.MaxValue" />。</exception>
    /// <exception cref="T:System.OperationCanceledException">
    /// <paramref name="cancellationToken" /> 已取消。</exception>
    [__DynamicallyInvokable]
    public Task<bool> WaitAsync(TimeSpan timeout, CancellationToken cancellationToken)
    {
      long num = (long) timeout.TotalMilliseconds;
      if (num < -1L || num > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException("timeout", (object) timeout, SemaphoreSlim.GetResourceString("SemaphoreSlim_Wait_TimeoutWrong"));
      return this.WaitAsync((int) timeout.TotalMilliseconds, cancellationToken);
    }

    /// <summary>在观察 <see cref="T:System.Threading.CancellationToken" /> 时，输入 <see cref="T:System.Threading.SemaphoreSlim" /> 的异步等待，使用 32 位带符号整数度量时间间隔。</summary>
    /// <returns>如果当前线程成功输入了 <see cref="T:System.Threading.SemaphoreSlim" />，则为将通过 true 的结果一起完成的任务，否则将通过 false 的结果完成。</returns>
    /// <param name="millisecondsTimeout">等待的毫秒数，或为 <see cref="F:System.Threading.Timeout.Infinite" /> (-1)，表示无限期等待。</param>
    /// <param name="cancellationToken">要观察的 <see cref="T:System.Threading.CancellationToken" />。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="millisecondsTimeout" /> 是一个非 -1 的负数，而 -1 表示无限期超时。</exception>
    /// <exception cref="T:System.ObjectDisposedException">当前实例已被释放。</exception>
    /// <exception cref="T:System.OperationCanceledException">
    /// <paramref name="cancellationToken" /> 已取消。</exception>
    [__DynamicallyInvokable]
    public Task<bool> WaitAsync(int millisecondsTimeout, CancellationToken cancellationToken)
    {
      this.CheckDispose();
      if (millisecondsTimeout < -1)
        throw new ArgumentOutOfRangeException("totalMilliSeconds", (object) millisecondsTimeout, SemaphoreSlim.GetResourceString("SemaphoreSlim_Wait_TimeoutWrong"));
      if (cancellationToken.IsCancellationRequested)
        return Task.FromCancellation<bool>(cancellationToken);
      lock (this.m_lockObj)
      {
        if (this.m_currentCount > 0)
        {
          this.m_currentCount = this.m_currentCount - 1;
          if (this.m_waitHandle != null && this.m_currentCount == 0)
            this.m_waitHandle.Reset();
          return SemaphoreSlim.s_trueTask;
        }
        SemaphoreSlim.TaskNode local_3 = this.CreateAndAddAsyncWaiter();
        return millisecondsTimeout != -1 || cancellationToken.CanBeCanceled ? this.WaitUntilCountOrTimeoutAsync(local_3, millisecondsTimeout, cancellationToken) : (Task<bool>) local_3;
      }
    }

    private SemaphoreSlim.TaskNode CreateAndAddAsyncWaiter()
    {
      SemaphoreSlim.TaskNode taskNode = new SemaphoreSlim.TaskNode();
      if (this.m_asyncHead == null)
      {
        this.m_asyncHead = taskNode;
        this.m_asyncTail = taskNode;
      }
      else
      {
        this.m_asyncTail.Next = taskNode;
        taskNode.Prev = this.m_asyncTail;
        this.m_asyncTail = taskNode;
      }
      return taskNode;
    }

    private bool RemoveAsyncWaiter(SemaphoreSlim.TaskNode task)
    {
      int num = this.m_asyncHead == task ? 1 : (task.Prev != null ? 1 : 0);
      if (task.Next != null)
        task.Next.Prev = task.Prev;
      if (task.Prev != null)
        task.Prev.Next = task.Next;
      if (this.m_asyncHead == task)
        this.m_asyncHead = task.Next;
      if (this.m_asyncTail == task)
        this.m_asyncTail = task.Prev;
      SemaphoreSlim.TaskNode taskNode1 = task;
      // ISSUE: variable of the null type
      __Null local;
      SemaphoreSlim.TaskNode taskNode2 = (SemaphoreSlim.TaskNode) (local = null);
      taskNode1.Prev = (SemaphoreSlim.TaskNode) local;
      SemaphoreSlim.TaskNode taskNode3 = taskNode2;
      taskNode1.Next = taskNode3;
      return num != 0;
    }

    private async Task<bool> WaitUntilCountOrTimeoutAsync(SemaphoreSlim.TaskNode asyncWaiter, int millisecondsTimeout, CancellationToken cancellationToken)
    {
      CancellationTokenSource cts = cancellationToken.CanBeCanceled ? CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, new CancellationToken()) : new CancellationTokenSource();
      try
      {
        Task<Task> task1 = Task.WhenAny((Task) asyncWaiter, Task.Delay(millisecondsTimeout, cts.Token));
        object obj = (object) asyncWaiter;
        int num = 0;
        Task task2 = await task1.ConfigureAwait(num != 0);
        if (obj == task2)
        {
          obj = (object) null;
          cts.Cancel();
          return true;
        }
      }
      finally
      {
        if (cts != null)
          cts.Dispose();
      }
      cts = (CancellationTokenSource) null;
      lock (this.m_lockObj)
      {
        if (this.RemoveAsyncWaiter(asyncWaiter))
        {
          cancellationToken.ThrowIfCancellationRequested();
          return false;
        }
      }
      return await asyncWaiter.ConfigureAwait(false);
    }

    /// <summary>释放 <see cref="T:System.Threading.SemaphoreSlim" /> 对象一次。</summary>
    /// <returns>
    /// <see cref="T:System.Threading.SemaphoreSlim" /> 的前一个计数。</returns>
    /// <exception cref="T:System.ObjectDisposedException">当前实例已被释放。</exception>
    /// <exception cref="T:System.Threading.SemaphoreFullException">
    /// <see cref="T:System.Threading.SemaphoreSlim" /> 已达到其最大大小。</exception>
    [__DynamicallyInvokable]
    public int Release()
    {
      return this.Release(1);
    }

    /// <summary>释放 <see cref="T:System.Threading.SemaphoreSlim" /> 对象指定的次数。</summary>
    /// <returns>
    /// <see cref="T:System.Threading.SemaphoreSlim" /> 的前一个计数。</returns>
    /// <param name="releaseCount">退出信号量的次数。</param>
    /// <exception cref="T:System.ObjectDisposedException">当前实例已被释放。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="releaseCount" /> 为小于 1。</exception>
    /// <exception cref="T:System.Threading.SemaphoreFullException">
    /// <see cref="T:System.Threading.SemaphoreSlim" /> 已达到其最大大小。</exception>
    [__DynamicallyInvokable]
    public int Release(int releaseCount)
    {
      this.CheckDispose();
      if (releaseCount < 1)
        throw new ArgumentOutOfRangeException("releaseCount", (object) releaseCount, SemaphoreSlim.GetResourceString("SemaphoreSlim_Release_CountWrong"));
      int num;
      lock (this.m_lockObj)
      {
        int local_3 = this.m_currentCount;
        num = local_3;
        if (this.m_maxCount - local_3 < releaseCount)
          throw new SemaphoreFullException();
        int local_3_1 = local_3 + releaseCount;
        int local_4 = this.m_waitCount;
        if (local_3_1 == 1 || local_4 == 1)
          Monitor.Pulse(this.m_lockObj);
        else if (local_4 > 1)
          Monitor.PulseAll(this.m_lockObj);
        if (this.m_asyncHead != null)
        {
          int local_5 = local_3_1 - local_4;
          while (local_5 > 0 && this.m_asyncHead != null)
          {
            --local_3_1;
            --local_5;
            SemaphoreSlim.TaskNode local_6 = this.m_asyncHead;
            this.RemoveAsyncWaiter(local_6);
            SemaphoreSlim.QueueWaiterTask(local_6);
          }
        }
        this.m_currentCount = local_3_1;
        if (this.m_waitHandle != null)
        {
          if (num == 0)
          {
            if (local_3_1 > 0)
              this.m_waitHandle.Set();
          }
        }
      }
      return num;
    }

    [SecuritySafeCritical]
    private static void QueueWaiterTask(SemaphoreSlim.TaskNode waiterTask)
    {
      ThreadPool.UnsafeQueueCustomWorkItem((IThreadPoolWorkItem) waiterTask, false);
    }

    /// <summary>释放 <see cref="T:System.Threading.SemaphoreSlim" /> 类的当前实例所使用的所有资源。</summary>
    [__DynamicallyInvokable]
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>释放由 <see cref="T:System.Threading.SemaphoreSlim" /> 占用的非托管资源，还可以另外再释放托管资源。</summary>
    /// <param name="disposing">若要释放托管资源和非托管资源，则为 true；若仅释放非托管资源，则为 false。</param>
    [__DynamicallyInvokable]
    protected virtual void Dispose(bool disposing)
    {
      if (!disposing)
        return;
      if (this.m_waitHandle != null)
      {
        this.m_waitHandle.Close();
        this.m_waitHandle = (ManualResetEvent) null;
      }
      this.m_lockObj = (object) null;
      this.m_asyncHead = (SemaphoreSlim.TaskNode) null;
      this.m_asyncTail = (SemaphoreSlim.TaskNode) null;
    }

    private static void CancellationTokenCanceledEventHandler(object obj)
    {
      SemaphoreSlim semaphoreSlim = obj as SemaphoreSlim;
      lock (semaphoreSlim.m_lockObj)
        Monitor.PulseAll(semaphoreSlim.m_lockObj);
    }

    private void CheckDispose()
    {
      if (this.m_lockObj == null)
        throw new ObjectDisposedException((string) null, SemaphoreSlim.GetResourceString("SemaphoreSlim_Disposed"));
    }

    private static string GetResourceString(string str)
    {
      return Environment.GetResourceString(str);
    }

    private sealed class TaskNode : Task<bool>, IThreadPoolWorkItem
    {
      internal SemaphoreSlim.TaskNode Prev;
      internal SemaphoreSlim.TaskNode Next;

      internal TaskNode()
      {
      }

      [SecurityCritical]
      void IThreadPoolWorkItem.ExecuteWorkItem()
      {
        this.TrySetResult(true);
      }

      [SecurityCritical]
      void IThreadPoolWorkItem.MarkAborted(ThreadAbortException tae)
      {
      }
    }
  }
}
