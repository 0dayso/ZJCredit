// Decompiled with JetBrains decompiler
// Type: System.Threading.Timer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Threading
{
  /// <summary>提供以指定的时间间隔执行方法的机制。此类不能被继承。若要浏览此类型的 .NET Framework 源代码，请参阅引用源。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public sealed class Timer : MarshalByRefObject, IDisposable
  {
    private const uint MAX_SUPPORTED_TIMEOUT = 4294967294;
    private TimerHolder m_timer;

    /// <summary>初始化 Timer 类的新实例，该类使用 32 位有符号整数指定时间间隔。</summary>
    /// <param name="callback">
    /// <see cref="T:System.Threading.TimerCallback" /> 委托，表示要执行的方法。</param>
    /// <param name="state">一个包含回调方法要使用的信息的对象，或者为 null。</param>
    /// <param name="dueTime">调用 <paramref name="callback" /> 之前延迟的时间量（以毫秒为单位）。指定 <see cref="F:System.Threading.Timeout.Infinite" /> 可防止启动计时器。指定零 (0) 可立即启动计时器。</param>
    /// <param name="period">调用 <paramref name="callback" /> 的时间间隔（以毫秒为单位）。指定 <see cref="F:System.Threading.Timeout.Infinite" /> 可以禁用定期终止。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="dueTime" /> or <paramref name="period" /> parameter is negative and is not equal to <see cref="F:System.Threading.Timeout.Infinite" />. </exception>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="callback" /> parameter is null. </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Timer(TimerCallback callback, object state, int dueTime, int period)
    {
      if (dueTime < -1)
        throw new ArgumentOutOfRangeException("dueTime", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      if (period < -1)
        throw new ArgumentOutOfRangeException("period", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.TimerSetup(callback, state, (uint) dueTime, (uint) period, ref stackMark);
    }

    /// <summary>初始化 Timer 类的新实例，该类使用 <see cref="T:System.TimeSpan" /> 值度量时间间隔。</summary>
    /// <param name="callback">表示要执行的方法的委托。</param>
    /// <param name="state">一个包含回调方法要使用的信息的对象，或者为 null。</param>
    /// <param name="dueTime">在 <paramref name="callback" /> 参数调用其方法之前延迟的时间量。指定 -1 毫秒以防止启动计时器。指定零 (0) 可立即启动计时器。</param>
    /// <param name="period">在调用 <paramref name="callback" /> 所引用的方法之间的时间间隔。指定 -1 毫秒可以禁用定期终止。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">The number of milliseconds in the value of <paramref name="dueTime" /> or <paramref name="period" /> is negative and not equal to <see cref="F:System.Threading.Timeout.Infinite" />, or is greater than <see cref="F:System.Int32.MaxValue" />. </exception>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="callback" /> parameter is null. </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Timer(TimerCallback callback, object state, TimeSpan dueTime, TimeSpan period)
    {
      long num1 = (long) dueTime.TotalMilliseconds;
      if (num1 < -1L)
        throw new ArgumentOutOfRangeException("dueTm", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      if (num1 > 4294967294L)
        throw new ArgumentOutOfRangeException("dueTm", Environment.GetResourceString("ArgumentOutOfRange_TimeoutTooLarge"));
      long num2 = (long) period.TotalMilliseconds;
      if (num2 < -1L)
        throw new ArgumentOutOfRangeException("periodTm", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      if (num2 > 4294967294L)
        throw new ArgumentOutOfRangeException("periodTm", Environment.GetResourceString("ArgumentOutOfRange_PeriodTooLarge"));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.TimerSetup(callback, state, (uint) num1, (uint) num2, ref stackMark);
    }

    /// <summary>初始化 Timer 类的新实例，该类使用 32 位无符号整数度量时间间隔。</summary>
    /// <param name="callback">表示要执行的方法的委托。</param>
    /// <param name="state">一个包含回调方法要使用的信息的对象，或者为 null。</param>
    /// <param name="dueTime">调用 <paramref name="callback" /> 之前延迟的时间量（以毫秒为单位）。指定 <see cref="F:System.Threading.Timeout.Infinite" /> 可防止启动计时器。指定零 (0) 可立即启动计时器。</param>
    /// <param name="period">调用 <paramref name="callback" /> 的时间间隔（以毫秒为单位）。指定 <see cref="F:System.Threading.Timeout.Infinite" /> 可以禁用定期终止。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="dueTime" /> or <paramref name="period" /> parameter is negative and is not equal to <see cref="F:System.Threading.Timeout.Infinite" />. </exception>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="callback" /> parameter is null. </exception>
    [CLSCompliant(false)]
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Timer(TimerCallback callback, object state, uint dueTime, uint period)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.TimerSetup(callback, state, dueTime, period, ref stackMark);
    }

    /// <summary>初始化 Timer 类的新实例，该类使用 64 位有符号整数度量时间间隔。</summary>
    /// <param name="callback">
    /// <see cref="T:System.Threading.TimerCallback" /> 委托，表示要执行的方法。</param>
    /// <param name="state">一个包含回调方法要使用的信息的对象，或者为 null。</param>
    /// <param name="dueTime">调用 <paramref name="callback" /> 之前延迟的时间量（以毫秒为单位）。指定 <see cref="F:System.Threading.Timeout.Infinite" /> 可防止启动计时器。指定零 (0) 可立即启动计时器。</param>
    /// <param name="period">调用 <paramref name="callback" /> 的时间间隔（以毫秒为单位）。指定 <see cref="F:System.Threading.Timeout.Infinite" /> 可以禁用定期终止。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="dueTime" /> or <paramref name="period" /> parameter is negative and is not equal to <see cref="F:System.Threading.Timeout.Infinite" />. </exception>
    /// <exception cref="T:System.NotSupportedException">The <paramref name="dueTime" /> or <paramref name="period" /> parameter is greater than 4294967294. </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Timer(TimerCallback callback, object state, long dueTime, long period)
    {
      if (dueTime < -1L)
        throw new ArgumentOutOfRangeException("dueTime", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      if (period < -1L)
        throw new ArgumentOutOfRangeException("period", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      if (dueTime > 4294967294L)
        throw new ArgumentOutOfRangeException("dueTime", Environment.GetResourceString("ArgumentOutOfRange_TimeoutTooLarge"));
      if (period > 4294967294L)
        throw new ArgumentOutOfRangeException("period", Environment.GetResourceString("ArgumentOutOfRange_PeriodTooLarge"));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.TimerSetup(callback, state, (uint) dueTime, (uint) period, ref stackMark);
    }

    /// <summary>使用无限期和无限期时间初始化 <see cref="T:System.Threading.Timer" /> 类的新实例，该类使用新创建的 <see cref="T:System.Threading.Timer" /> 对象作为状态对象。</summary>
    /// <param name="callback">
    /// <see cref="T:System.Threading.TimerCallback" /> 委托，表示要执行的方法。</param>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Timer(TimerCallback callback)
    {
      int num1 = -1;
      int num2 = -1;
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.TimerSetup(callback, (object) this, (uint) num1, (uint) num2, ref stackMark);
    }

    [SecurityCritical]
    private void TimerSetup(TimerCallback callback, object state, uint dueTime, uint period, ref StackCrawlMark stackMark)
    {
      if (callback == null)
        throw new ArgumentNullException("TimerCallback");
      this.m_timer = new TimerHolder(new TimerQueueTimer(callback, state, dueTime, period, ref stackMark));
    }

    [SecurityCritical]
    internal static void Pause()
    {
      TimerQueue.Instance.Pause();
    }

    [SecurityCritical]
    internal static void Resume()
    {
      TimerQueue.Instance.Resume();
    }

    /// <summary>更改计时器的启动时间和方法调用之间的间隔，用 32 位有符号整数度量时间间隔。</summary>
    /// <returns>如果计时器更新成功，则为 true；否则为 false。</returns>
    /// <param name="dueTime">在调用构造 <see cref="T:System.Threading.Timer" /> 时指定的回调方法之前的延迟时间量（以毫秒为单位）。指定 <see cref="F:System.Threading.Timeout.Infinite" /> 可防止重新启动计时器。指定零 (0) 可立即重新启动计时器。</param>
    /// <param name="period">调用构造 <see cref="T:System.Threading.Timer" /> 时指定的回调方法的时间间隔（以毫秒为单位）。指定 <see cref="F:System.Threading.Timeout.Infinite" /> 可以禁用定期终止。</param>
    /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Timer" /> has already been disposed. </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="dueTime" /> or <paramref name="period" /> parameter is negative and is not equal to <see cref="F:System.Threading.Timeout.Infinite" />. </exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public bool Change(int dueTime, int period)
    {
      if (dueTime < -1)
        throw new ArgumentOutOfRangeException("dueTime", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      if (period < -1)
        throw new ArgumentOutOfRangeException("period", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      return this.m_timer.m_timer.Change((uint) dueTime, (uint) period);
    }

    /// <summary>更改计时器的启动时间和方法调用之间的间隔，使用 <see cref="T:System.TimeSpan" /> 值度量时间间隔。</summary>
    /// <returns>如果计时器更新成功，则为 true；否则为 false。</returns>
    /// <param name="dueTime">一个 <see cref="T:System.TimeSpan" />，表示在调用构造 <see cref="T:System.Threading.Timer" /> 时指定的回调方法之前延迟的时间量。指定负 -1 毫秒以防止计时器重新启动。指定零 (0) 可立即重新启动计时器。</param>
    /// <param name="period">在构造 <see cref="T:System.Threading.Timer" /> 时指定的回调方法调用之间的时间间隔。指定 -1 毫秒可以禁用定期终止。</param>
    /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Timer" /> has already been disposed. </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="dueTime" /> or <paramref name="period" /> parameter, in milliseconds, is less than -1. </exception>
    /// <exception cref="T:System.NotSupportedException">The <paramref name="dueTime" /> or <paramref name="period" /> parameter, in milliseconds, is greater than 4294967294. </exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public bool Change(TimeSpan dueTime, TimeSpan period)
    {
      return this.Change((long) dueTime.TotalMilliseconds, (long) period.TotalMilliseconds);
    }

    /// <summary>更改计时器的启动时间和方法调用之间的间隔，用 32 位无符号整数度量时间间隔。</summary>
    /// <returns>如果计时器更新成功，则为 true；否则为 false。</returns>
    /// <param name="dueTime">在调用构造 <see cref="T:System.Threading.Timer" /> 时指定的回调方法之前的延迟时间量（以毫秒为单位）。指定 <see cref="F:System.Threading.Timeout.Infinite" /> 可防止重新启动计时器。指定零 (0) 可立即重新启动计时器。</param>
    /// <param name="period">调用构造 <see cref="T:System.Threading.Timer" /> 时指定的回调方法的时间间隔（以毫秒为单位）。指定 <see cref="F:System.Threading.Timeout.Infinite" /> 可以禁用定期终止。</param>
    /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Timer" /> has already been disposed. </exception>
    /// <filterpriority>2</filterpriority>
    [CLSCompliant(false)]
    public bool Change(uint dueTime, uint period)
    {
      return this.m_timer.m_timer.Change(dueTime, period);
    }

    /// <summary>更改计时器的启动时间和方法调用之间的间隔，用 64 位有符号整数度量时间间隔。</summary>
    /// <returns>如果计时器更新成功，则为 true；否则为 false。</returns>
    /// <param name="dueTime">在调用构造 <see cref="T:System.Threading.Timer" /> 时指定的回调方法之前的延迟时间量（以毫秒为单位）。指定 <see cref="F:System.Threading.Timeout.Infinite" /> 可防止重新启动计时器。指定零 (0) 可立即重新启动计时器。</param>
    /// <param name="period">调用构造 <see cref="T:System.Threading.Timer" /> 时指定的回调方法的时间间隔（以毫秒为单位）。指定 <see cref="F:System.Threading.Timeout.Infinite" /> 可以禁用定期终止。</param>
    /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Timer" /> has already been disposed. </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="dueTime" /> or <paramref name="period" /> parameter is less than -1. </exception>
    /// <exception cref="T:System.NotSupportedException">The <paramref name="dueTime" /> or <paramref name="period" /> parameter is greater than 4294967294. </exception>
    /// <filterpriority>2</filterpriority>
    public bool Change(long dueTime, long period)
    {
      if (dueTime < -1L)
        throw new ArgumentOutOfRangeException("dueTime", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      if (period < -1L)
        throw new ArgumentOutOfRangeException("period", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      if (dueTime > 4294967294L)
        throw new ArgumentOutOfRangeException("dueTime", Environment.GetResourceString("ArgumentOutOfRange_TimeoutTooLarge"));
      if (period > 4294967294L)
        throw new ArgumentOutOfRangeException("period", Environment.GetResourceString("ArgumentOutOfRange_PeriodTooLarge"));
      return this.m_timer.m_timer.Change((uint) dueTime, (uint) period);
    }

    /// <summary>释放 <see cref="T:System.Threading.Timer" /> 的当前实例使用的所有资源并在释放完计时器时发出信号。</summary>
    /// <returns>如果函数成功，则为 true；否则为 false。</returns>
    /// <param name="notifyObject">当已释放 Timer 后要发送信号的 <see cref="T:System.Threading.WaitHandle" />。 </param>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="notifyObject" /> parameter is null. </exception>
    /// <filterpriority>2</filterpriority>
    public bool Dispose(WaitHandle notifyObject)
    {
      if (notifyObject == null)
        throw new ArgumentNullException("notifyObject");
      return this.m_timer.Close(notifyObject);
    }

    /// <summary>释放由 <see cref="T:System.Threading.Timer" /> 的当前实例使用的所有资源。</summary>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public void Dispose()
    {
      this.m_timer.Close();
    }

    internal void KeepRootedWhileScheduled()
    {
      GC.SuppressFinalize((object) this.m_timer);
    }
  }
}
