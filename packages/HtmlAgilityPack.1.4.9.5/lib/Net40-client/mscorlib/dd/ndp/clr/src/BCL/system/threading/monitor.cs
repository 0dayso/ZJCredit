// Decompiled with JetBrains decompiler
// Type: System.Threading.Monitor
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Threading
{
  /// <summary>提供同步访问对象的机制。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public static class Monitor
  {
    /// <summary>在指定对象上获取排他锁。</summary>
    /// <param name="obj">在其上获取监视器锁的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="obj" /> 参数为 null。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern void Enter(object obj);

    /// <summary>获取指定对象上的排他锁，并自动设置一个值，指示是否获取了该锁。</summary>
    /// <param name="obj">要在其上等待的对象。</param>
    /// <param name="lockTaken">尝试获取锁的结果，通过引用传递。输入必须为 false。如果已获取锁，则输出为 true；否则输出为 false。即使在尝试获取锁的过程中发生异常，也会设置输出。注意   如果没有发生异常，则此方法的输出始终为 true。</param>
    /// <exception cref="T:System.ArgumentException">对 <paramref name="lockTaken" /> 的输入是 true。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="obj" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    public static void Enter(object obj, ref bool lockTaken)
    {
      if (lockTaken)
        Monitor.ThrowLockTakenException();
      Monitor.ReliableEnter(obj, ref lockTaken);
    }

    private static void ThrowLockTakenException()
    {
      throw new ArgumentException(Environment.GetResourceString("Argument_MustBeFalse"), "lockTaken");
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void ReliableEnter(object obj, ref bool lockTaken);

    /// <summary>释放指定对象上的排他锁。</summary>
    /// <param name="obj">在其上释放锁的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="obj" /> 参数为 null。</exception>
    /// <exception cref="T:System.Threading.SynchronizationLockException">当前线程不拥有指定对象的锁。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern void Exit(object obj);

    /// <summary>尝试获取指定对象的排他锁。</summary>
    /// <returns>如果当前线程获取该锁，则为 true；否则为 false。</returns>
    /// <param name="obj">在其上获取锁的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="obj" /> 参数为 null。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool TryEnter(object obj)
    {
      bool lockTaken = false;
      Monitor.TryEnter(obj, 0, ref lockTaken);
      return lockTaken;
    }

    /// <summary>尝试获取指定对象上的排他锁，并自动设置一个值，指示是否获取了该锁。</summary>
    /// <param name="obj">在其上获取锁的对象。</param>
    /// <param name="lockTaken">尝试获取锁的结果，通过引用传递。输入必须为 false。如果已获取锁，则输出为 true；否则输出为 false。即使在尝试获取锁的过程中发生异常，也会设置输出。</param>
    /// <exception cref="T:System.ArgumentException">对 <paramref name="lockTaken" /> 的输入是 true。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="obj" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    public static void TryEnter(object obj, ref bool lockTaken)
    {
      if (lockTaken)
        Monitor.ThrowLockTakenException();
      Monitor.ReliableEnterTimeout(obj, 0, ref lockTaken);
    }

    /// <summary>在指定的毫秒数内尝试获取指定对象上的排他锁。</summary>
    /// <returns>如果当前线程获取该锁，则为 true；否则为 false。</returns>
    /// <param name="obj">在其上获取锁的对象。</param>
    /// <param name="millisecondsTimeout">等待锁所需的毫秒数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="obj" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="millisecondsTimeout" /> 为负且不等于 <see cref="F:System.Threading.Timeout.Infinite" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool TryEnter(object obj, int millisecondsTimeout)
    {
      bool lockTaken = false;
      Monitor.TryEnter(obj, millisecondsTimeout, ref lockTaken);
      return lockTaken;
    }

    private static int MillisecondsTimeoutFromTimeSpan(TimeSpan timeout)
    {
      long num = (long) timeout.TotalMilliseconds;
      if (num < -1L || num > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      return (int) num;
    }

    /// <summary>在指定的时间内尝试获取指定对象上的排他锁。</summary>
    /// <returns>如果当前线程获取该锁，则为 true；否则为 false。</returns>
    /// <param name="obj">在其上获取锁的对象。</param>
    /// <param name="timeout">
    /// <see cref="T:System.TimeSpan" />，表示等待锁所需的时间量。值为 -1 毫秒表示指定无限期等待。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="obj" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="timeout" /> 值（以毫秒为单位）为负且不等于 <see cref="F:System.Threading.Timeout.Infinite" />（-1 毫秒），或者大于 <see cref="F:System.Int32.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool TryEnter(object obj, TimeSpan timeout)
    {
      return Monitor.TryEnter(obj, Monitor.MillisecondsTimeoutFromTimeSpan(timeout));
    }

    /// <summary>在指定的毫秒数内尝试获取指定对象上的排他锁，并自动设置一个值，指示是否获取了该锁。</summary>
    /// <param name="obj">在其上获取锁的对象。</param>
    /// <param name="millisecondsTimeout">等待锁所需的毫秒数。</param>
    /// <param name="lockTaken">尝试获取锁的结果，通过引用传递。输入必须为 false。如果已获取锁，则输出为 true；否则输出为 false。即使在尝试获取锁的过程中发生异常，也会设置输出。</param>
    /// <exception cref="T:System.ArgumentException">对 <paramref name="lockTaken" /> 的输入是 true。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="obj" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="millisecondsTimeout" /> 为负且不等于 <see cref="F:System.Threading.Timeout.Infinite" />。</exception>
    [__DynamicallyInvokable]
    public static void TryEnter(object obj, int millisecondsTimeout, ref bool lockTaken)
    {
      if (lockTaken)
        Monitor.ThrowLockTakenException();
      Monitor.ReliableEnterTimeout(obj, millisecondsTimeout, ref lockTaken);
    }

    /// <summary>在指定的一段时间内尝试获取指定对象上的排他锁，并自动设置一个值，指示是否获得了该锁。</summary>
    /// <param name="obj">在其上获取锁的对象。</param>
    /// <param name="timeout">用于等待锁的时间。值为 -1 毫秒表示指定无限期等待。</param>
    /// <param name="lockTaken">尝试获取锁的结果，通过引用传递。输入必须为 false。如果已获取锁，则输出为 true；否则输出为 false。即使在尝试获取锁的过程中发生异常，也会设置输出。</param>
    /// <exception cref="T:System.ArgumentException">对 <paramref name="lockTaken" /> 的输入是 true。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="obj" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="timeout" /> 值（以毫秒为单位）为负且不等于 <see cref="F:System.Threading.Timeout.Infinite" />（-1 毫秒），或者大于 <see cref="F:System.Int32.MaxValue" />。</exception>
    [__DynamicallyInvokable]
    public static void TryEnter(object obj, TimeSpan timeout, ref bool lockTaken)
    {
      if (lockTaken)
        Monitor.ThrowLockTakenException();
      Monitor.ReliableEnterTimeout(obj, Monitor.MillisecondsTimeoutFromTimeSpan(timeout), ref lockTaken);
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void ReliableEnterTimeout(object obj, int timeout, ref bool lockTaken);

    /// <summary>确定当前线程是否保留指定对象上的锁。</summary>
    /// <returns>如果当前线程持有 <paramref name="obj" /> 锁，则为 true；否则为 false。</returns>
    /// <param name="obj">要测试的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="obj" /> 为 null。</exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static bool IsEntered(object obj)
    {
      if (obj == null)
        throw new ArgumentNullException("obj");
      return Monitor.IsEnteredNative(obj);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool IsEnteredNative(object obj);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool ObjWait(bool exitContext, int millisecondsTimeout, object obj);

    /// <summary>释放对象上的锁并阻止当前线程，直到它重新获取该锁。如果已用指定的超时时间间隔，则线程进入就绪队列。此方法还指定是否在等待之前退出上下文的同步域（如果在同步上下文中）然后重新获取该同步域。</summary>
    /// <returns>如果在指定的时间过期之前重新获取该锁，则为 true；如果在指定的时间过期之后重新获取该锁，则为 false。此方法只有在重新获取该锁后才会返回。</returns>
    /// <param name="obj">要在其上等待的对象。</param>
    /// <param name="millisecondsTimeout">线程进入就绪队列之前等待的毫秒数。</param>
    /// <param name="exitContext">如果在等待前退出并重新获取上下文的同步域（如果在同步上下文中），则为 true；否则为 false。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="obj" /> 参数为 null。</exception>
    /// <exception cref="T:System.Threading.SynchronizationLockException">不是从同步的代码块内调用 Wait。</exception>
    /// <exception cref="T:System.Threading.ThreadInterruptedException">调用 Wait 的线程稍后从等待状态中断。当另一个线程调用此线程的 <see cref="M:System.Threading.Thread.Interrupt" /> 方法时会发生这种情况。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="millisecondsTimeout" /> 参数值为负且不等于 <see cref="F:System.Threading.Timeout.Infinite" />。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    public static bool Wait(object obj, int millisecondsTimeout, bool exitContext)
    {
      if (obj == null)
        throw new ArgumentNullException("obj");
      return Monitor.ObjWait(exitContext, millisecondsTimeout, obj);
    }

    /// <summary>释放对象上的锁并阻止当前线程，直到它重新获取该锁。如果已用指定的超时时间间隔，则线程进入就绪队列。可以在等待之前退出同步上下文的同步域，随后重新获取该域。</summary>
    /// <returns>如果在指定的时间过期之前重新获取该锁，则为 true；如果在指定的时间过期之后重新获取该锁，则为 false。此方法只有在重新获取该锁后才会返回。</returns>
    /// <param name="obj">要在其上等待的对象。</param>
    /// <param name="timeout">
    /// <see cref="T:System.TimeSpan" />，表示线程进入就绪队列之前等待的时间量。</param>
    /// <param name="exitContext">如果在等待前退出并重新获取上下文的同步域（如果在同步上下文中），则为 true；否则为 false。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="obj" /> 参数为 null。</exception>
    /// <exception cref="T:System.Threading.SynchronizationLockException">不是从同步的代码块内调用 Wait。</exception>
    /// <exception cref="T:System.Threading.ThreadInterruptedException">调用 Wait 的线程稍后从等待状态中断。当另一个线程调用此线程的 <see cref="M:System.Threading.Thread.Interrupt" /> 方法时会发生这种情况。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="timeout" /> 参数为负且不表示 <see cref="F:System.Threading.Timeout.Infinite" />（-1 毫秒），或者大于 <see cref="F:System.Int32.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    public static bool Wait(object obj, TimeSpan timeout, bool exitContext)
    {
      return Monitor.Wait(obj, Monitor.MillisecondsTimeoutFromTimeSpan(timeout), exitContext);
    }

    /// <summary>释放对象上的锁并阻止当前线程，直到它重新获取该锁。如果已用指定的超时时间间隔，则线程进入就绪队列。</summary>
    /// <returns>如果在指定的时间过期之前重新获取该锁，则为 true；如果在指定的时间过期之后重新获取该锁，则为 false。此方法只有在重新获取该锁后才会返回。</returns>
    /// <param name="obj">要在其上等待的对象。</param>
    /// <param name="millisecondsTimeout">线程进入就绪队列之前等待的毫秒数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="obj" /> 参数为 null。</exception>
    /// <exception cref="T:System.Threading.SynchronizationLockException">调用线程不拥有指定对象的锁。</exception>
    /// <exception cref="T:System.Threading.ThreadInterruptedException">调用 Wait 的线程稍后从等待状态中断。当另一个线程调用此线程的 <see cref="M:System.Threading.Thread.Interrupt" /> 方法时会发生这种情况。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="millisecondsTimeout" /> 参数值为负且不等于 <see cref="F:System.Threading.Timeout.Infinite" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool Wait(object obj, int millisecondsTimeout)
    {
      return Monitor.Wait(obj, millisecondsTimeout, false);
    }

    /// <summary>释放对象上的锁并阻止当前线程，直到它重新获取该锁。如果已用指定的超时时间间隔，则线程进入就绪队列。</summary>
    /// <returns>如果在指定的时间过期之前重新获取该锁，则为 true；如果在指定的时间过期之后重新获取该锁，则为 false。此方法只有在重新获取该锁后才会返回。</returns>
    /// <param name="obj">要在其上等待的对象。</param>
    /// <param name="timeout">
    /// <see cref="T:System.TimeSpan" />，表示线程进入就绪队列之前等待的时间量。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="obj" /> 参数为 null。</exception>
    /// <exception cref="T:System.Threading.SynchronizationLockException">调用线程不拥有指定对象的锁。</exception>
    /// <exception cref="T:System.Threading.ThreadInterruptedException">调用 Wait 的线程稍后从等待状态中断。当另一个线程调用此线程的 <see cref="M:System.Threading.Thread.Interrupt" /> 方法时会发生这种情况。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="timeout" /> 参数值（以毫秒为单位）为负且不表示 <see cref="F:System.Threading.Timeout.Infinite" />（-1 毫秒），或者大于 <see cref="F:System.Int32.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool Wait(object obj, TimeSpan timeout)
    {
      return Monitor.Wait(obj, Monitor.MillisecondsTimeoutFromTimeSpan(timeout), false);
    }

    /// <summary>释放对象上的锁并阻止当前线程，直到它重新获取该锁。</summary>
    /// <returns>如果调用由于调用方重新获取了指定对象的锁而返回，则为 true。如果未重新获取该锁，则此方法不会返回。</returns>
    /// <param name="obj">要在其上等待的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="obj" /> 参数为 null。</exception>
    /// <exception cref="T:System.Threading.SynchronizationLockException">调用线程不拥有指定对象的锁。</exception>
    /// <exception cref="T:System.Threading.ThreadInterruptedException">调用 Wait 的线程稍后从等待状态中断。当另一个线程调用此线程的 <see cref="M:System.Threading.Thread.Interrupt" /> 方法时会发生这种情况。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool Wait(object obj)
    {
      return Monitor.Wait(obj, -1, false);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void ObjPulse(object obj);

    /// <summary>通知等待队列中的线程锁定对象状态的更改。</summary>
    /// <param name="obj">线程正在等待的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="obj" /> 参数为 null。</exception>
    /// <exception cref="T:System.Threading.SynchronizationLockException">调用线程不拥有指定对象的锁。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static void Pulse(object obj)
    {
      if (obj == null)
        throw new ArgumentNullException("obj");
      Monitor.ObjPulse(obj);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void ObjPulseAll(object obj);

    /// <summary>通知所有的等待线程对象状态的更改。</summary>
    /// <param name="obj">发送脉冲的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="obj" /> 参数为 null。</exception>
    /// <exception cref="T:System.Threading.SynchronizationLockException">调用线程不拥有指定对象的锁。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static void PulseAll(object obj)
    {
      if (obj == null)
        throw new ArgumentNullException("obj");
      Monitor.ObjPulseAll(obj);
    }
  }
}
