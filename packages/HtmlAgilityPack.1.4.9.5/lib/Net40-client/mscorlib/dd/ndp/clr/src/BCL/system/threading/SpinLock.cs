// Decompiled with JetBrains decompiler
// Type: System.Threading.SpinLock
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Threading
{
  /// <summary>提供一个相互排斥锁基元，在该基元中，尝试获取锁的线程将在重复检查的循环中等待，直至该锁变为可用为止。</summary>
  [ComVisible(false)]
  [DebuggerTypeProxy(typeof (SpinLock.SystemThreading_SpinLockDebugView))]
  [DebuggerDisplay("IsHeld = {IsHeld}")]
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public struct SpinLock
  {
    private static int MAXIMUM_WAITERS = 2147483646;
    private volatile int m_owner;
    private const int SPINNING_FACTOR = 100;
    private const int SLEEP_ONE_FREQUENCY = 40;
    private const int SLEEP_ZERO_FREQUENCY = 10;
    private const int TIMEOUT_CHECK_FREQUENCY = 10;
    private const int LOCK_ID_DISABLE_MASK = -2147483648;
    private const int LOCK_ANONYMOUS_OWNED = 1;
    private const int WAITERS_MASK = 2147483646;
    private const int ID_DISABLED_AND_ANONYMOUS_OWNED = -2147483647;
    private const int LOCK_UNOWNED = 0;

    /// <summary>获取锁当前是否已由任何线程占用。</summary>
    /// <returns>如果锁当前已由任何线程占用，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool IsHeld
    {
      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success), __DynamicallyInvokable] get
      {
        if (this.IsThreadOwnerTrackingEnabled)
          return (uint) this.m_owner > 0U;
        return (uint) (this.m_owner & 1) > 0U;
      }
    }

    /// <summary>获取锁是否已由当前线程占用。</summary>
    /// <returns>如果锁已由当前线程占用，则为 true；否则为 false。</returns>
    /// <exception cref="T:System.InvalidOperationException">禁用线程所有权跟踪。</exception>
    [__DynamicallyInvokable]
    public bool IsHeldByCurrentThread
    {
      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success), __DynamicallyInvokable] get
      {
        if (!this.IsThreadOwnerTrackingEnabled)
          throw new InvalidOperationException(Environment.GetResourceString("SpinLock_IsHeldByCurrentThread"));
        return (this.m_owner & int.MaxValue) == Thread.CurrentThread.ManagedThreadId;
      }
    }

    /// <summary>获取是否已为此实例启用了线程所有权跟踪。</summary>
    /// <returns>如果已为此实例启用了线程所有权跟踪，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool IsThreadOwnerTrackingEnabled
    {
      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success), __DynamicallyInvokable] get
      {
        return (this.m_owner & int.MinValue) == 0;
      }
    }

    /// <summary>使用用于跟踪线程 ID 以改善调试的选项初始化 <see cref="T:System.Threading.SpinLock" /> 结构的新实例。</summary>
    /// <param name="enableThreadOwnerTracking">是否捕获线程 ID 并将其用于调试目的。</param>
    [__DynamicallyInvokable]
    public SpinLock(bool enableThreadOwnerTracking)
    {
      this.m_owner = 0;
      if (enableThreadOwnerTracking)
        return;
      this.m_owner = this.m_owner | int.MinValue;
    }

    /// <summary>采用可靠的方式获取锁，这样，即使在方法调用中发生异常的情况下，都能采用可靠的方式检查 <paramref name="lockTaken" /> 以确定是否已获取锁。</summary>
    /// <param name="lockTaken">如果已获取锁，则为 true，否则为 false。调用此方法前，必须将 <paramref name="lockTaken" /> 始化为 false。</param>
    /// <exception cref="T:System.ArgumentException">在调用 Enter 之前，<paramref name="lockTaken" /> 参数必须初始化为 false。</exception>
    /// <exception cref="T:System.Threading.LockRecursionException">线程所有权跟踪已启用，当前线程已获取此锁定。</exception>
    [__DynamicallyInvokable]
    public void Enter(ref bool lockTaken)
    {
      Thread.BeginCriticalRegion();
      int comparand = this.m_owner;
      if (!lockTaken && (comparand & -2147483647) == int.MinValue && Interlocked.CompareExchange(ref this.m_owner, comparand | 1, comparand, ref lockTaken) == comparand)
        return;
      this.ContinueTryEnter(-1, ref lockTaken);
    }

    /// <summary>尝试采用可靠的方式获取锁，这样，即使在方法调用中发生异常的情况下，都能采用可靠的方式检查  <paramref name="lockTaken" /> 以确定是否已获取锁。</summary>
    /// <param name="lockTaken">如果已获取锁，则为 true，否则为 false。调用此方法前，必须将 <paramref name="lockTaken" /> 始化为 false。</param>
    /// <exception cref="T:System.ArgumentException">在调用 TryEnter 之前，<paramref name="lockTaken" /> 参数必须在初始化为 false。</exception>
    /// <exception cref="T:System.Threading.LockRecursionException">线程所有权跟踪已启用，当前线程已获取此锁定。</exception>
    [__DynamicallyInvokable]
    public void TryEnter(ref bool lockTaken)
    {
      this.TryEnter(0, ref lockTaken);
    }

    /// <summary>尝试采用可靠的方式获取锁，这样，即使在方法调用中发生异常的情况下，都能采用可靠的方式检查  <paramref name="lockTaken" /> 以确定是否已获取锁。</summary>
    /// <param name="timeout">表示等待的毫秒数的 <see cref="T:System.TimeSpan" />，或表示 -1 毫秒（无限期等待）的 <see cref="T:System.TimeSpan" />。</param>
    /// <param name="lockTaken">如果已获取锁，则为 true，否则为 false。调用此方法前，必须将 <paramref name="lockTaken" /> 始化为 false。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="timeout" /> 是 -1 毫秒之外的负数，表示无限超时或者超时大于 <see cref="F:System.Int32.MaxValue" /> 毫秒。</exception>
    /// <exception cref="T:System.ArgumentException">在调用 TryEnter 之前，<paramref name="lockTaken" /> 参数必须在初始化为 false。</exception>
    /// <exception cref="T:System.Threading.LockRecursionException">线程所有权跟踪已启用，当前线程已获取此锁定。</exception>
    [__DynamicallyInvokable]
    public void TryEnter(TimeSpan timeout, ref bool lockTaken)
    {
      long num = (long) timeout.TotalMilliseconds;
      if (num < -1L || num > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException("timeout", (object) timeout, Environment.GetResourceString("SpinLock_TryEnter_ArgumentOutOfRange"));
      this.TryEnter((int) timeout.TotalMilliseconds, ref lockTaken);
    }

    /// <summary>尝试采用可靠的方式获取锁，这样，即使在方法调用中发生异常的情况下，都能采用可靠的方式检查  <paramref name="lockTaken" /> 以确定是否已获取锁。</summary>
    /// <param name="millisecondsTimeout">等待的毫秒数，或为 <see cref="F:System.Threading.Timeout.Infinite" /> (-1)，表示无限期等待。</param>
    /// <param name="lockTaken">如果已获取锁，则为 true，否则为 false。调用此方法前，必须将 <paramref name="lockTaken" /> 始化为 false。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="millisecondsTimeout" /> 是一个非 -1 的负数，而 -1 表示无限期超时。</exception>
    /// <exception cref="T:System.ArgumentException">在调用 TryEnter 之前，<paramref name="lockTaken" /> 参数必须在初始化为 false。</exception>
    /// <exception cref="T:System.Threading.LockRecursionException">线程所有权跟踪已启用，当前线程已获取此锁定。</exception>
    [__DynamicallyInvokable]
    public void TryEnter(int millisecondsTimeout, ref bool lockTaken)
    {
      Thread.BeginCriticalRegion();
      int comparand = this.m_owner;
      if (!(millisecondsTimeout < -1 | lockTaken) && (comparand & -2147483647) == int.MinValue && Interlocked.CompareExchange(ref this.m_owner, comparand | 1, comparand, ref lockTaken) == comparand)
        return;
      this.ContinueTryEnter(millisecondsTimeout, ref lockTaken);
    }

    private void ContinueTryEnter(int millisecondsTimeout, ref bool lockTaken)
    {
      Thread.EndCriticalRegion();
      if (lockTaken)
      {
        lockTaken = false;
        throw new ArgumentException(Environment.GetResourceString("SpinLock_TryReliableEnter_ArgumentException"));
      }
      if (millisecondsTimeout < -1)
        throw new ArgumentOutOfRangeException("millisecondsTimeout", (object) millisecondsTimeout, Environment.GetResourceString("SpinLock_TryEnter_ArgumentOutOfRange"));
      uint startTime = 0;
      if (millisecondsTimeout != -1 && millisecondsTimeout != 0)
        startTime = TimeoutHelper.GetTime();
      if (CdsSyncEtwBCLProvider.Log.IsEnabled())
        CdsSyncEtwBCLProvider.Log.SpinLock_FastPathFailed(this.m_owner);
      if (this.IsThreadOwnerTrackingEnabled)
      {
        this.ContinueTryEnterWithThreadTracking(millisecondsTimeout, startTime, ref lockTaken);
      }
      else
      {
        int num1 = int.MaxValue;
        int comparand1 = this.m_owner;
        if ((comparand1 & 1) == 0)
        {
          Thread.BeginCriticalRegion();
          if (Interlocked.CompareExchange(ref this.m_owner, comparand1 | 1, comparand1, ref lockTaken) == comparand1)
            return;
          Thread.EndCriticalRegion();
        }
        else if ((comparand1 & 2147483646) != SpinLock.MAXIMUM_WAITERS)
          num1 = (Interlocked.Add(ref this.m_owner, 2) & 2147483646) >> 1;
        if (millisecondsTimeout == 0 || millisecondsTimeout != -1 && TimeoutHelper.UpdateTimeOut(startTime, millisecondsTimeout) <= 0)
        {
          this.DecrementWaiters();
        }
        else
        {
          int processorCount = PlatformHelper.ProcessorCount;
          if (num1 < processorCount)
          {
            int num2 = 1;
            for (int index = 1; index <= num1 * 100; ++index)
            {
              Thread.SpinWait((num1 + index) * 100 * num2);
              if (num2 < processorCount)
                ++num2;
              int comparand2 = this.m_owner;
              if ((comparand2 & 1) == 0)
              {
                Thread.BeginCriticalRegion();
                if (Interlocked.CompareExchange(ref this.m_owner, (comparand2 & 2147483646) == 0 ? comparand2 | 1 : comparand2 - 2 | 1, comparand2, ref lockTaken) == comparand2)
                  return;
                Thread.EndCriticalRegion();
              }
            }
          }
          if (millisecondsTimeout != -1 && TimeoutHelper.UpdateTimeOut(startTime, millisecondsTimeout) <= 0)
          {
            this.DecrementWaiters();
          }
          else
          {
            int num2 = 0;
            while (true)
            {
              int comparand2 = this.m_owner;
              if ((comparand2 & 1) == 0)
              {
                Thread.BeginCriticalRegion();
                if (Interlocked.CompareExchange(ref this.m_owner, (comparand2 & 2147483646) == 0 ? comparand2 | 1 : comparand2 - 2 | 1, comparand2, ref lockTaken) != comparand2)
                  Thread.EndCriticalRegion();
                else
                  break;
              }
              if (num2 % 40 == 0)
                Thread.Sleep(1);
              else if (num2 % 10 == 0)
                Thread.Sleep(0);
              else
                Thread.Yield();
              if (num2 % 10 != 0 || millisecondsTimeout == -1 || TimeoutHelper.UpdateTimeOut(startTime, millisecondsTimeout) > 0)
                ++num2;
              else
                goto label_40;
            }
            return;
label_40:
            this.DecrementWaiters();
          }
        }
      }
    }

    private void DecrementWaiters()
    {
      SpinWait spinWait = new SpinWait();
      while (true)
      {
        int comparand = this.m_owner;
        if ((comparand & 2147483646) != 0 && Interlocked.CompareExchange(ref this.m_owner, comparand - 2, comparand) != comparand)
          spinWait.SpinOnce();
        else
          break;
      }
    }

    private void ContinueTryEnterWithThreadTracking(int millisecondsTimeout, uint startTime, ref bool lockTaken)
    {
      int comparand = 0;
      int managedThreadId = Thread.CurrentThread.ManagedThreadId;
      if (this.m_owner == managedThreadId)
        throw new LockRecursionException(Environment.GetResourceString("SpinLock_TryEnter_LockRecursionException"));
      SpinWait spinWait = new SpinWait();
      do
      {
        spinWait.SpinOnce();
        if (this.m_owner == comparand)
        {
          Thread.BeginCriticalRegion();
          if (Interlocked.CompareExchange(ref this.m_owner, managedThreadId, comparand, ref lockTaken) == comparand)
            break;
          Thread.EndCriticalRegion();
        }
      }
      while (millisecondsTimeout != 0 && (millisecondsTimeout == -1 || !spinWait.NextSpinWillYield || TimeoutHelper.UpdateTimeOut(startTime, millisecondsTimeout) > 0));
    }

    /// <summary>释放锁。</summary>
    /// <exception cref="T:System.Threading.SynchronizationLockException">启用线程所有权跟踪，当前线程不是此锁的所有者。</exception>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public void Exit()
    {
      if ((this.m_owner & int.MinValue) == 0)
        this.ExitSlowPath(true);
      else
        Interlocked.Decrement(ref this.m_owner);
      Thread.EndCriticalRegion();
    }

    /// <summary>释放锁。</summary>
    /// <param name="useMemoryBarrier">一个布尔值，该值指示是否应发出内存界定，以便将退出操作立即发布到其他线程。</param>
    /// <exception cref="T:System.Threading.SynchronizationLockException">启用线程所有权跟踪，当前线程不是此锁的所有者。</exception>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public void Exit(bool useMemoryBarrier)
    {
      if ((this.m_owner & int.MinValue) != 0 && !useMemoryBarrier)
        this.m_owner = this.m_owner & -2;
      else
        this.ExitSlowPath(useMemoryBarrier);
      Thread.EndCriticalRegion();
    }

    private void ExitSlowPath(bool useMemoryBarrier)
    {
      bool flag = (this.m_owner & int.MinValue) == 0;
      if (flag && !this.IsHeldByCurrentThread)
        throw new SynchronizationLockException(Environment.GetResourceString("SpinLock_Exit_SynchronizationLockException"));
      if (useMemoryBarrier)
      {
        if (flag)
          Interlocked.Exchange(ref this.m_owner, 0);
        else
          Interlocked.Decrement(ref this.m_owner);
      }
      else if (flag)
        this.m_owner = 0;
      else
        this.m_owner = this.m_owner & -2;
    }

    internal class SystemThreading_SpinLockDebugView
    {
      private SpinLock m_spinLock;

      public bool? IsHeldByCurrentThread
      {
        get
        {
          try
          {
            return new bool?(this.m_spinLock.IsHeldByCurrentThread);
          }
          catch (InvalidOperationException ex)
          {
            return new bool?();
          }
        }
      }

      public int? OwnerThreadID
      {
        get
        {
          if (this.m_spinLock.IsThreadOwnerTrackingEnabled)
            return new int?(this.m_spinLock.m_owner);
          return new int?();
        }
      }

      public bool IsHeld
      {
        get
        {
          return this.m_spinLock.IsHeld;
        }
      }

      public SystemThreading_SpinLockDebugView(SpinLock spinLock)
      {
        this.m_spinLock = spinLock;
      }
    }
  }
}
