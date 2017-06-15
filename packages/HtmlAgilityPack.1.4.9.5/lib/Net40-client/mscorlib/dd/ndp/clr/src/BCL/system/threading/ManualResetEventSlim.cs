// Decompiled with JetBrains decompiler
// Type: System.Threading.ManualResetEventSlim
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Threading
{
  /// <summary>提供 <see cref="T:System.Threading.ManualResetEvent" /> 的简化版本。</summary>
  [ComVisible(false)]
  [DebuggerDisplay("Set = {IsSet}")]
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public class ManualResetEventSlim : IDisposable
  {
    private static Action<object> s_cancellationTokenCallback = new Action<object>(ManualResetEventSlim.CancellationTokenCallback);
    private const int DEFAULT_SPIN_SP = 1;
    private const int DEFAULT_SPIN_MP = 10;
    private volatile object m_lock;
    private volatile ManualResetEvent m_eventObj;
    private volatile int m_combinedState;
    private const int SignalledState_BitMask = -2147483648;
    private const int SignalledState_ShiftCount = 31;
    private const int Dispose_BitMask = 1073741824;
    private const int SpinCountState_BitMask = 1073217536;
    private const int SpinCountState_ShiftCount = 19;
    private const int SpinCountState_MaxValue = 2047;
    private const int NumWaitersState_BitMask = 524287;
    private const int NumWaitersState_ShiftCount = 0;
    private const int NumWaitersState_MaxValue = 524287;

    /// <summary>获取此 <see cref="T:System.Threading.ManualResetEventSlim" /> 的基础 <see cref="T:System.Threading.WaitHandle" /> 对象。</summary>
    /// <returns>此 <see cref="T:System.Threading.ManualResetEventSlim" /> 的基础 <see cref="T:System.Threading.WaitHandle" /> 事件对象。</returns>
    [__DynamicallyInvokable]
    public WaitHandle WaitHandle
    {
      [__DynamicallyInvokable] get
      {
        this.ThrowIfDisposed();
        if (this.m_eventObj == null)
          this.LazyInitializeEvent();
        return (WaitHandle) this.m_eventObj;
      }
    }

    /// <summary>获取是否已设置事件。</summary>
    /// <returns>如果设置了事件，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool IsSet
    {
      [__DynamicallyInvokable] get
      {
        return (uint) ManualResetEventSlim.ExtractStatePortion(this.m_combinedState, int.MinValue) > 0U;
      }
      private set
      {
        this.UpdateStateAtomically((value ? 1 : 0) << 31, int.MinValue);
      }
    }

    /// <summary>获取在回退到基于内核的等待操作之前发生的自旋等待数量。</summary>
    /// <returns>返回在回退到基于内核的等待操作之前发生的自旋等待数量。</returns>
    [__DynamicallyInvokable]
    public int SpinCount
    {
      [__DynamicallyInvokable] get
      {
        return ManualResetEventSlim.ExtractStatePortionAndShiftRight(this.m_combinedState, 1073217536, 19);
      }
      private set
      {
        this.m_combinedState = this.m_combinedState & -1073217537 | value << 19;
      }
    }

    private int Waiters
    {
      get
      {
        return ManualResetEventSlim.ExtractStatePortionAndShiftRight(this.m_combinedState, 524287, 0);
      }
      set
      {
        if (value >= 524287)
          throw new InvalidOperationException(string.Format(Environment.GetResourceString("ManualResetEventSlim_ctor_TooManyWaiters"), (object) 524287));
        this.UpdateStateAtomically(value, 524287);
      }
    }

    /// <summary>使用非终止初始状态初始化 <see cref="T:System.Threading.ManualResetEventSlim" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public ManualResetEventSlim()
      : this(false)
    {
    }

    /// <summary>使用 Boolean 值（指示是否将初始状态设置为终止状态）初始化 <see cref="T:System.Threading.ManualResetEventSlim" /> 类的新实例。</summary>
    /// <param name="initialState">若要将初始状态设置为终止，则为 true；若要将初始状态设置为非终止，则为 false。</param>
    [__DynamicallyInvokable]
    public ManualResetEventSlim(bool initialState)
    {
      this.Initialize(initialState, 10);
    }

    /// <summary>使用 Boolean 值（指示是否将初始状态设置为终止或指定的旋转数）初始化 <see cref="T:System.Threading.ManualResetEventSlim" /> 类的新实例。</summary>
    /// <param name="initialState">若要将初始状态设置为终止，则为 true；若要将初始状态设置为非终止，则为 false。</param>
    /// <param name="spinCount">在回退到基于内核的等待操作之前发生的自旋等待数量。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="spinCount" /> is less than 0 or greater than the maximum allowed value.</exception>
    [__DynamicallyInvokable]
    public ManualResetEventSlim(bool initialState, int spinCount)
    {
      if (spinCount < 0)
        throw new ArgumentOutOfRangeException("spinCount");
      if (spinCount > 2047)
        throw new ArgumentOutOfRangeException("spinCount", string.Format(Environment.GetResourceString("ManualResetEventSlim_ctor_SpinCountOutOfRange"), (object) 2047));
      this.Initialize(initialState, spinCount);
    }

    private void Initialize(bool initialState, int spinCount)
    {
      this.m_combinedState = initialState ? int.MinValue : 0;
      this.SpinCount = PlatformHelper.IsSingleProcessor ? 1 : spinCount;
    }

    private void EnsureLockObjectCreated()
    {
      if (this.m_lock != null)
        return;
      Interlocked.CompareExchange(ref this.m_lock, new object(), (object) null);
    }

    private bool LazyInitializeEvent()
    {
      bool isSet = this.IsSet;
      ManualResetEvent manualResetEvent = new ManualResetEvent(isSet);
      if (Interlocked.CompareExchange<ManualResetEvent>(ref this.m_eventObj, manualResetEvent, (ManualResetEvent) null) != null)
      {
        manualResetEvent.Close();
        return false;
      }
      if (this.IsSet != isSet)
      {
        lock (manualResetEvent)
        {
          if (this.m_eventObj == manualResetEvent)
            manualResetEvent.Set();
        }
      }
      return true;
    }

    /// <summary>将事件状态设置为有信号，从而允许一个或多个等待该事件的线程继续。</summary>
    [__DynamicallyInvokable]
    public void Set()
    {
      this.Set(false);
    }

    private void Set(bool duringCancellation)
    {
      this.IsSet = true;
      if (this.Waiters > 0)
      {
        lock (this.m_lock)
          Monitor.PulseAll(this.m_lock);
      }
      ManualResetEvent manualResetEvent = this.m_eventObj;
      if (manualResetEvent == null || duringCancellation)
        return;
      lock (manualResetEvent)
      {
        if (this.m_eventObj == null)
          return;
        this.m_eventObj.Set();
      }
    }

    /// <summary>将事件状态设置为非终止，从而导致线程受阻。</summary>
    /// <exception cref="T:System.ObjectDisposedException">The object has already been disposed.</exception>
    [__DynamicallyInvokable]
    public void Reset()
    {
      this.ThrowIfDisposed();
      if (this.m_eventObj != null)
        this.m_eventObj.Reset();
      this.IsSet = false;
    }

    /// <summary>阻止当前线程，直到设置了当前 <see cref="T:System.Threading.ManualResetEventSlim" /> 为止。</summary>
    /// <exception cref="T:System.InvalidOperationException">The maximum number of waiters has been exceeded.</exception>
    /// <exception cref="T:System.ObjectDisposedException">The object has already been disposed.</exception>
    [__DynamicallyInvokable]
    public void Wait()
    {
      this.Wait(-1, new CancellationToken());
    }

    /// <summary>阻止当前线程，直到 <see cref="T:System.Threading.ManualResetEventSlim" /> 接收到信号，同时观察 <see cref="T:System.Threading.CancellationToken" />。</summary>
    /// <param name="cancellationToken">要观察的 <see cref="T:System.Threading.CancellationToken" />。</param>
    /// <exception cref="T:System.InvalidOperationException">The maximum number of waiters has been exceeded.</exception>
    /// <exception cref="T:System.OperationCanceledException">
    /// <paramref name="cancellationToken" /> was canceled.</exception>
    /// <exception cref="T:System.ObjectDisposedException">The object has already been disposed or the <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has been disposed.</exception>
    [__DynamicallyInvokable]
    public void Wait(CancellationToken cancellationToken)
    {
      this.Wait(-1, cancellationToken);
    }

    /// <summary>阻止当前线程，直到当前 <see cref="T:System.Threading.ManualResetEventSlim" /> 已设定，使用 <see cref="T:System.TimeSpan" /> 测量时间间隔。</summary>
    /// <returns>如果已设置 <see cref="T:System.Threading.ManualResetEventSlim" />，则为 true；否则为 false。</returns>
    /// <param name="timeout">表示等待毫秒数的 <see cref="T:System.TimeSpan" />，或表示 -1 毫秒（无限期等待）的 <see cref="T:System.TimeSpan" />。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out. -or-The number of milliseconds in <paramref name="timeout" /> is greater than <see cref="F:System.Int32.MaxValue" />. </exception>
    /// <exception cref="T:System.InvalidOperationException">The maximum number of waiters has been exceeded.</exception>
    /// <exception cref="T:System.ObjectDisposedException">The object has already been disposed.</exception>
    [__DynamicallyInvokable]
    public bool Wait(TimeSpan timeout)
    {
      long num = (long) timeout.TotalMilliseconds;
      if (num < -1L || num > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException("timeout");
      return this.Wait((int) num, new CancellationToken());
    }

    /// <summary>阻止当前线程，直到当前 <see cref="T:System.Threading.ManualResetEventSlim" /> 已设定，使用 <see cref="T:System.TimeSpan" /> 测量时间间隔，同时观察 <see cref="T:System.Threading.CancellationToken" />。</summary>
    /// <returns>如果已设置 <see cref="T:System.Threading.ManualResetEventSlim" />，则为 true；否则为 false。</returns>
    /// <param name="timeout">表示等待毫秒数的 <see cref="T:System.TimeSpan" />，或表示 -1 毫秒（无限期等待）的 <see cref="T:System.TimeSpan" />。</param>
    /// <param name="cancellationToken">要观察的 <see cref="T:System.Threading.CancellationToken" />。</param>
    /// <exception cref="T:System.OperationCanceledException">
    /// <paramref name="cancellationToken" /> was canceled.</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out. -or-The number of milliseconds in <paramref name="timeout" /> is greater than <see cref="F:System.Int32.MaxValue" />. </exception>
    /// <exception cref="T:System.InvalidOperationException">The maximum number of waiters has been exceeded. </exception>
    /// <exception cref="T:System.ObjectDisposedException">The object has already been disposed or the <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has been disposed.</exception>
    [__DynamicallyInvokable]
    public bool Wait(TimeSpan timeout, CancellationToken cancellationToken)
    {
      long num = (long) timeout.TotalMilliseconds;
      if (num < -1L || num > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException("timeout");
      return this.Wait((int) num, cancellationToken);
    }

    /// <summary>阻止当前线程，直到设定 <see cref="T:System.Threading.ManualResetEventSlim" />，使用 32 位已签名整数测量时间间隔。</summary>
    /// <returns>如果已设置 <see cref="T:System.Threading.ManualResetEventSlim" />，则为 true；否则为 false。</returns>
    /// <param name="millisecondsTimeout">等待的毫秒数，或为 <see cref="F:System.Threading.Timeout.Infinite" /> (-1)，表示无限期等待。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
    /// <exception cref="T:System.InvalidOperationException">The maximum number of waiters has been exceeded.</exception>
    /// <exception cref="T:System.ObjectDisposedException">The object has already been disposed.</exception>
    [__DynamicallyInvokable]
    public bool Wait(int millisecondsTimeout)
    {
      return this.Wait(millisecondsTimeout, new CancellationToken());
    }

    /// <summary>阻止当前线程，直到设定 <see cref="T:System.Threading.ManualResetEventSlim" />，使用 32 位已签名整数测量时间间隔，同时观察 <see cref="T:System.Threading.CancellationToken" />。</summary>
    /// <returns>如果已设置 <see cref="T:System.Threading.ManualResetEventSlim" />，则为 true；否则为 false。</returns>
    /// <param name="millisecondsTimeout">等待的毫秒数，或为 <see cref="F:System.Threading.Timeout.Infinite" /> (-1)，表示无限期等待。</param>
    /// <param name="cancellationToken">要观察的 <see cref="T:System.Threading.CancellationToken" />。</param>
    /// <exception cref="T:System.OperationCanceledException">
    /// <paramref name="cancellationToken" /> was canceled.</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
    /// <exception cref="T:System.InvalidOperationException">The maximum number of waiters has been exceeded.</exception>
    /// <exception cref="T:System.ObjectDisposedException">The object has already been disposed or the <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has been disposed.</exception>
    [__DynamicallyInvokable]
    public bool Wait(int millisecondsTimeout, CancellationToken cancellationToken)
    {
      this.ThrowIfDisposed();
      cancellationToken.ThrowIfCancellationRequested();
      if (millisecondsTimeout < -1)
        throw new ArgumentOutOfRangeException("millisecondsTimeout");
      if (!this.IsSet)
      {
        if (millisecondsTimeout == 0)
          return false;
        uint startTime = 0;
        bool flag = false;
        int millisecondsTimeout1 = millisecondsTimeout;
        if (millisecondsTimeout != -1)
        {
          startTime = TimeoutHelper.GetTime();
          flag = true;
        }
        int num1 = 10;
        int num2 = 5;
        int num3 = 20;
        int spinCount = this.SpinCount;
        for (int index = 0; index < spinCount; ++index)
        {
          if (this.IsSet)
            return true;
          if (index < num1)
          {
            if (index == num1 / 2)
              Thread.Yield();
            else
              Thread.SpinWait(PlatformHelper.ProcessorCount * (4 << index));
          }
          else if (index % num3 == 0)
            Thread.Sleep(1);
          else if (index % num2 == 0)
            Thread.Sleep(0);
          else
            Thread.Yield();
          if (index >= 100 && index % 10 == 0)
            cancellationToken.ThrowIfCancellationRequested();
        }
        this.EnsureLockObjectCreated();
        using (cancellationToken.InternalRegisterWithoutEC(ManualResetEventSlim.s_cancellationTokenCallback, (object) this))
        {
          lock (this.m_lock)
          {
            while (!this.IsSet)
            {
              cancellationToken.ThrowIfCancellationRequested();
              if (flag)
              {
                millisecondsTimeout1 = TimeoutHelper.UpdateTimeOut(startTime, millisecondsTimeout);
                if (millisecondsTimeout1 <= 0)
                  return false;
              }
              this.Waiters = this.Waiters + 1;
              if (this.IsSet)
              {
                this.Waiters = this.Waiters - 1;
                return true;
              }
              try
              {
                if (!Monitor.Wait(this.m_lock, millisecondsTimeout1))
                  return false;
              }
              finally
              {
                this.Waiters = this.Waiters - 1;
              }
            }
          }
        }
      }
      return true;
    }

    /// <summary>释放由 <see cref="T:System.Threading.ManualResetEventSlim" /> 类的当前实例占用的所有资源。</summary>
    [__DynamicallyInvokable]
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>释放由 <see cref="T:System.Threading.ManualResetEventSlim" /> 占用的非托管资源，还可以另外再释放托管资源。</summary>
    /// <param name="disposing">为 true 则释放托管资源和非托管资源；为 false 则仅释放非托管资源。</param>
    [__DynamicallyInvokable]
    protected virtual void Dispose(bool disposing)
    {
      if ((this.m_combinedState & 1073741824) != 0)
        return;
      this.m_combinedState = this.m_combinedState | 1073741824;
      if (!disposing)
        return;
      ManualResetEvent manualResetEvent = this.m_eventObj;
      if (manualResetEvent == null)
        return;
      lock (manualResetEvent)
      {
        manualResetEvent.Close();
        this.m_eventObj = (ManualResetEvent) null;
      }
    }

    private void ThrowIfDisposed()
    {
      if ((this.m_combinedState & 1073741824) != 0)
        throw new ObjectDisposedException(Environment.GetResourceString("ManualResetEventSlim_Disposed"));
    }

    private static void CancellationTokenCallback(object obj)
    {
      ManualResetEventSlim manualResetEventSlim = obj as ManualResetEventSlim;
      lock (manualResetEventSlim.m_lock)
        Monitor.PulseAll(manualResetEventSlim.m_lock);
    }

    private void UpdateStateAtomically(int newBits, int updateBitsMask)
    {
      SpinWait spinWait = new SpinWait();
      while (true)
      {
        int comparand = this.m_combinedState;
        if (Interlocked.CompareExchange(ref this.m_combinedState, comparand & ~updateBitsMask | newBits, comparand) != comparand)
          spinWait.SpinOnce();
        else
          break;
      }
    }

    private static int ExtractStatePortionAndShiftRight(int state, int mask, int rightBitShiftCount)
    {
      return (int) ((uint) (state & mask) >> rightBitShiftCount);
    }

    private static int ExtractStatePortion(int state, int mask)
    {
      return state & mask;
    }
  }
}
