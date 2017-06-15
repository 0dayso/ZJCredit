// Decompiled with JetBrains decompiler
// Type: System.Threading.CountdownEvent
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Threading
{
  /// <summary>表示在计数变为零时处于有信号状态的同步基元。</summary>
  [ComVisible(false)]
  [DebuggerDisplay("Initial Count={InitialCount}, Current Count={CurrentCount}")]
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public class CountdownEvent : IDisposable
  {
    private int m_initialCount;
    private volatile int m_currentCount;
    private ManualResetEventSlim m_event;
    private volatile bool m_disposed;

    /// <summary>获取设置事件时所必需的剩余信号数。</summary>
    /// <returns> 设置事件时所必需的剩余信号数。</returns>
    [__DynamicallyInvokable]
    public int CurrentCount
    {
      [__DynamicallyInvokable] get
      {
        int num = this.m_currentCount;
        if (num >= 0)
          return num;
        return 0;
      }
    }

    /// <summary>获取设置事件时最初必需的信号数。</summary>
    /// <returns> 设置事件时最初必需的信号数。</returns>
    [__DynamicallyInvokable]
    public int InitialCount
    {
      [__DynamicallyInvokable] get
      {
        return this.m_initialCount;
      }
    }

    /// <summary>确定是否设置了事件。</summary>
    /// <returns>如果设置了事件，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool IsSet
    {
      [__DynamicallyInvokable] get
      {
        return this.m_currentCount <= 0;
      }
    }

    /// <summary>获取用于等待要设置的事件的 <see cref="T:System.Threading.WaitHandle" />。</summary>
    /// <returns>用于等待要设置的事件的 <see cref="T:System.Threading.WaitHandle" />。</returns>
    /// <exception cref="T:System.ObjectDisposedException">当前实例已被释放。</exception>
    [__DynamicallyInvokable]
    public WaitHandle WaitHandle
    {
      [__DynamicallyInvokable] get
      {
        this.ThrowIfDisposed();
        return this.m_event.WaitHandle;
      }
    }

    /// <summary>使用指定计数初始化 <see cref="T:System.Threading.CountdownEvent" /> 类的新实例。</summary>
    /// <param name="initialCount">设置 <see cref="T:System.Threading.CountdownEvent" /> 时最初必需的信号数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="initialCount" /> 小于 0。</exception>
    [__DynamicallyInvokable]
    public CountdownEvent(int initialCount)
    {
      if (initialCount < 0)
        throw new ArgumentOutOfRangeException("initialCount");
      this.m_initialCount = initialCount;
      this.m_currentCount = initialCount;
      this.m_event = new ManualResetEventSlim();
      if (initialCount != 0)
        return;
      this.m_event.Set();
    }

    /// <summary>释放由 <see cref="T:System.Threading.CountdownEvent" /> 类的当前实例占用的所有资源。</summary>
    [__DynamicallyInvokable]
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>释放由 <see cref="T:System.Threading.CountdownEvent" /> 占用的非托管资源，还可以另外再释放托管资源。</summary>
    /// <param name="disposing">如果为 true，则同时释放托管资源和非托管资源；如果为 false，则仅释放非托管资源。</param>
    [__DynamicallyInvokable]
    protected virtual void Dispose(bool disposing)
    {
      if (!disposing)
        return;
      this.m_event.Dispose();
      this.m_disposed = true;
    }

    /// <summary>向 <see cref="T:System.Threading.CountdownEvent" /> 注册信号，同时减小 <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> 的值。</summary>
    /// <returns>如果信号导致计数变为零并且设置了事件，则为 true；否则为 false。</returns>
    /// <exception cref="T:System.ObjectDisposedException">当前实例已被释放。</exception>
    /// <exception cref="T:System.InvalidOperationException">当前实例已设置 。</exception>
    [__DynamicallyInvokable]
    public bool Signal()
    {
      this.ThrowIfDisposed();
      if (this.m_currentCount <= 0)
        throw new InvalidOperationException(Environment.GetResourceString("CountdownEvent_Decrement_BelowZero"));
      int num = Interlocked.Decrement(ref this.m_currentCount);
      if (num == 0)
      {
        this.m_event.Set();
        return true;
      }
      if (num < 0)
        throw new InvalidOperationException(Environment.GetResourceString("CountdownEvent_Decrement_BelowZero"));
      return false;
    }

    /// <summary>向 <see cref="T:System.Threading.CountdownEvent" /> 注册多个信号，同时将 <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> 的值减少指定数量。</summary>
    /// <returns>如果信号导致计数变为零并且设置了事件，则为 true；否则为 false。</returns>
    /// <param name="signalCount">要注册的信号的数量。</param>
    /// <exception cref="T:System.ObjectDisposedException">当前实例已被释放。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="signalCount" /> 小于 1。</exception>
    /// <exception cref="T:System.InvalidOperationException">当前实例已设置 。- 或 - <paramref name="signalCount" /> 大于 <see cref="P:System.Threading.CountdownEvent.CurrentCount" />。</exception>
    [__DynamicallyInvokable]
    public bool Signal(int signalCount)
    {
      if (signalCount <= 0)
        throw new ArgumentOutOfRangeException("signalCount");
      this.ThrowIfDisposed();
      SpinWait spinWait = new SpinWait();
      int comparand;
      while (true)
      {
        comparand = this.m_currentCount;
        if (comparand >= signalCount)
        {
          if (Interlocked.CompareExchange(ref this.m_currentCount, comparand - signalCount, comparand) != comparand)
            spinWait.SpinOnce();
          else
            goto label_7;
        }
        else
          break;
      }
      throw new InvalidOperationException(Environment.GetResourceString("CountdownEvent_Decrement_BelowZero"));
label_7:
      if (comparand != signalCount)
        return false;
      this.m_event.Set();
      return true;
    }

    /// <summary>将 <see cref="T:System.Threading.CountdownEvent" /> 的当前计数加 1。</summary>
    /// <exception cref="T:System.ObjectDisposedException">当前实例已被释放。</exception>
    /// <exception cref="T:System.InvalidOperationException">当前实例已设置 。- 或 -<see cref="P:System.Threading.CountdownEvent.CurrentCount" /> 等于或大于 <see cref="F:System.Int32.MaxValue" />。</exception>
    [__DynamicallyInvokable]
    public void AddCount()
    {
      this.AddCount(1);
    }

    /// <summary>增加一个 <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> 的尝试。</summary>
    /// <returns>如果成功增加，则为 true；否则为 false。如果 <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> 已为零，则此方法将返回 false。</returns>
    /// <exception cref="T:System.ObjectDisposedException">当前实例已被释放。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> 等于 <see cref="F:System.Int32.MaxValue" />。</exception>
    [__DynamicallyInvokable]
    public bool TryAddCount()
    {
      return this.TryAddCount(1);
    }

    /// <summary>将 <see cref="T:System.Threading.CountdownEvent" /> 的当前计数增加指定值。</summary>
    /// <param name="signalCount">
    /// <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> 的增量值。</param>
    /// <exception cref="T:System.ObjectDisposedException">当前实例已被释放。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="signalCount" /> 小于或等于零。</exception>
    /// <exception cref="T:System.InvalidOperationException">当前实例已设置 。- 或 -在计数由 <paramref name="signalCount." /> 递增后，<see cref="P:System.Threading.CountdownEvent.CurrentCount" /> 大于或等于 <see cref="F:System.Int32.MaxValue" />。</exception>
    [__DynamicallyInvokable]
    public void AddCount(int signalCount)
    {
      if (!this.TryAddCount(signalCount))
        throw new InvalidOperationException(Environment.GetResourceString("CountdownEvent_Increment_AlreadyZero"));
    }

    /// <summary>增加指定值的 <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> 的尝试。</summary>
    /// <returns>如果成功增加，则为 true；否则为 false。如果 <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> 已为零，则此方法将返回 false。</returns>
    /// <param name="signalCount">
    /// <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> 的增量值。</param>
    /// <exception cref="T:System.ObjectDisposedException">当前实例已被释放。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="signalCount" /> 小于或等于零。</exception>
    /// <exception cref="T:System.InvalidOperationException">当前实例已设置 。- 或 -<see cref="P:System.Threading.CountdownEvent.CurrentCount" /> + <paramref name="signalCount" /> 大于等于 <see cref="F:System.Int32.MaxValue" />。</exception>
    [__DynamicallyInvokable]
    public bool TryAddCount(int signalCount)
    {
      if (signalCount <= 0)
        throw new ArgumentOutOfRangeException("signalCount");
      this.ThrowIfDisposed();
      SpinWait spinWait = new SpinWait();
      while (true)
      {
        int comparand = this.m_currentCount;
        if (comparand > 0)
        {
          if (comparand <= int.MaxValue - signalCount)
          {
            if (Interlocked.CompareExchange(ref this.m_currentCount, comparand + signalCount, comparand) != comparand)
              spinWait.SpinOnce();
            else
              goto label_9;
          }
          else
            goto label_6;
        }
        else
          break;
      }
      return false;
label_6:
      throw new InvalidOperationException(Environment.GetResourceString("CountdownEvent_Increment_AlreadyMax"));
label_9:
      return true;
    }

    /// <summary>将 <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> 重置为 <see cref="P:System.Threading.CountdownEvent.InitialCount" /> 的值。</summary>
    /// <exception cref="T:System.ObjectDisposedException">当前实例已被释放。</exception>
    [__DynamicallyInvokable]
    public void Reset()
    {
      this.Reset(this.m_initialCount);
    }

    /// <summary>将 <see cref="P:System.Threading.CountdownEvent.InitialCount" /> 属性重新设置为指定值。</summary>
    /// <param name="count">设置 <see cref="T:System.Threading.CountdownEvent" /> 时所必需的信号的数量。</param>
    /// <exception cref="T:System.ObjectDisposedException">当前实例已被释放。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="count" /> 小于 0。</exception>
    [__DynamicallyInvokable]
    public void Reset(int count)
    {
      this.ThrowIfDisposed();
      if (count < 0)
        throw new ArgumentOutOfRangeException("count");
      this.m_currentCount = count;
      this.m_initialCount = count;
      if (count == 0)
        this.m_event.Set();
      else
        this.m_event.Reset();
    }

    /// <summary>阻止当前线程，直到设置了 <see cref="T:System.Threading.CountdownEvent" /> 为止。</summary>
    /// <exception cref="T:System.ObjectDisposedException">当前实例已被释放。</exception>
    [__DynamicallyInvokable]
    public void Wait()
    {
      this.Wait(-1, new CancellationToken());
    }

    /// <summary>阻止当前线程，直到设置了 <see cref="T:System.Threading.CountdownEvent" /> 为止，同时观察 <see cref="T:System.Threading.CancellationToken" />。</summary>
    /// <param name="cancellationToken">要观察的 <see cref="T:System.Threading.CancellationToken" />。</param>
    /// <exception cref="T:System.OperationCanceledException">
    /// <paramref name="cancellationToken" /> 已取消。</exception>
    /// <exception cref="T:System.ObjectDisposedException">当前实例已被释放。- 或 - 创建 <paramref name="cancellationToken" /> 的 <see cref="T:System.Threading.CancellationTokenSource" /> 已被释放。</exception>
    [__DynamicallyInvokable]
    public void Wait(CancellationToken cancellationToken)
    {
      this.Wait(-1, cancellationToken);
    }

    /// <summary>阻止当前线程，直到设置了 <see cref="T:System.Threading.CountdownEvent" /> 为止，同时使用 <see cref="T:System.TimeSpan" /> 测量超时。</summary>
    /// <returns>如果设置了 <see cref="T:System.Threading.CountdownEvent" />，则为 true；否则为 false。</returns>
    /// <param name="timeout">表示等待的毫秒数的 <see cref="T:System.TimeSpan" />，或表示 -1 毫秒（无限期等待）的 <see cref="T:System.TimeSpan" />。</param>
    /// <exception cref="T:System.ObjectDisposedException">当前实例已被释放。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="timeout" /> 是 -1 毫秒之外的负数，表示无限超时或者超时大于 <see cref="F:System.Int32.MaxValue" />。</exception>
    [__DynamicallyInvokable]
    public bool Wait(TimeSpan timeout)
    {
      long num = (long) timeout.TotalMilliseconds;
      if (num < -1L || num > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException("timeout");
      return this.Wait((int) num, new CancellationToken());
    }

    /// <summary>阻止当前线程，直到设置了 <see cref="T:System.Threading.CountdownEvent" /> 为止，并使用 <see cref="T:System.TimeSpan" /> 测量超时，同时观察 <see cref="T:System.Threading.CancellationToken" />。</summary>
    /// <returns>如果设置了 <see cref="T:System.Threading.CountdownEvent" />，则为 true；否则为 false。</returns>
    /// <param name="timeout">表示等待的毫秒数的 <see cref="T:System.TimeSpan" />，或表示 -1 毫秒（无限期等待）的 <see cref="T:System.TimeSpan" />。</param>
    /// <param name="cancellationToken">要观察的 <see cref="T:System.Threading.CancellationToken" />。</param>
    /// <exception cref="T:System.OperationCanceledException">
    /// <paramref name="cancellationToken" /> 已取消。</exception>
    /// <exception cref="T:System.ObjectDisposedException">当前实例已被释放。- 或 - 创建 <paramref name="cancellationToken" /> 的 <see cref="T:System.Threading.CancellationTokenSource" /> 已被释放。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="timeout" /> 是 -1 毫秒之外的负数，表示无限超时或者超时大于 <see cref="F:System.Int32.MaxValue" />。</exception>
    [__DynamicallyInvokable]
    public bool Wait(TimeSpan timeout, CancellationToken cancellationToken)
    {
      long num = (long) timeout.TotalMilliseconds;
      if (num < -1L || num > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException("timeout");
      return this.Wait((int) num, cancellationToken);
    }

    /// <summary>阻止当前线程，直到设置了 <see cref="T:System.Threading.CountdownEvent" /> 为止，同时使用 32 位带符号整数测量超时。</summary>
    /// <returns>如果设置了 <see cref="T:System.Threading.CountdownEvent" />，则为 true；否则为 false。</returns>
    /// <param name="millisecondsTimeout">等待的毫秒数，或为 <see cref="F:System.Threading.Timeout.Infinite" /> (-1)，表示无限期等待。</param>
    /// <exception cref="T:System.ObjectDisposedException">当前实例已被释放。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="millisecondsTimeout" /> 是一个非 -1 的负数，而 -1 表示无限期超时。</exception>
    [__DynamicallyInvokable]
    public bool Wait(int millisecondsTimeout)
    {
      return this.Wait(millisecondsTimeout, new CancellationToken());
    }

    /// <summary>阻止当前线程，直到设置了 <see cref="T:System.Threading.CountdownEvent" /> 为止，并使用 32 位带符号整数测量超时，同时观察 <see cref="T:System.Threading.CancellationToken" />。</summary>
    /// <returns>如果设置了 <see cref="T:System.Threading.CountdownEvent" />，则为 true；否则为 false。</returns>
    /// <param name="millisecondsTimeout">等待的毫秒数，或为 <see cref="F:System.Threading.Timeout.Infinite" /> (-1)，表示无限期等待。</param>
    /// <param name="cancellationToken">要观察的 <see cref="T:System.Threading.CancellationToken" />。</param>
    /// <exception cref="T:System.OperationCanceledException">
    /// <paramref name="cancellationToken" /> 已取消。</exception>
    /// <exception cref="T:System.ObjectDisposedException">当前实例已被释放。- 或 - 创建 <paramref name="cancellationToken" /> 的 <see cref="T:System.Threading.CancellationTokenSource" /> 已被释放。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="millisecondsTimeout" /> 是一个非 -1 的负数，而 -1 表示无限期超时。</exception>
    [__DynamicallyInvokable]
    public bool Wait(int millisecondsTimeout, CancellationToken cancellationToken)
    {
      if (millisecondsTimeout < -1)
        throw new ArgumentOutOfRangeException("millisecondsTimeout");
      this.ThrowIfDisposed();
      cancellationToken.ThrowIfCancellationRequested();
      bool flag = this.IsSet;
      if (!flag)
        flag = this.m_event.Wait(millisecondsTimeout, cancellationToken);
      return flag;
    }

    private void ThrowIfDisposed()
    {
      if (this.m_disposed)
        throw new ObjectDisposedException("CountdownEvent");
    }
  }
}
