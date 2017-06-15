// Decompiled with JetBrains decompiler
// Type: System.Threading.WaitHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Security;
using System.Security.Permissions;

namespace System.Threading
{
  /// <summary>封装等待对共享资源的独占访问的操作系统特定的对象。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public abstract class WaitHandle : MarshalByRefObject, IDisposable
  {
    /// <summary>表示无效的本机操作系统句柄。此字段为只读。</summary>
    protected static readonly IntPtr InvalidHandle = WaitHandle.GetInvalidHandle();
    /// <summary>指示在任何等待句柄终止之前 <see cref="M:System.Threading.WaitHandle.WaitAny(System.Threading.WaitHandle[],System.Int32,System.Boolean)" /> 操作已超时。此字段为常数。</summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public const int WaitTimeout = 258;
    private const int MAX_WAITHANDLES = 64;
    private IntPtr waitHandle;
    [SecurityCritical]
    internal volatile SafeWaitHandle safeWaitHandle;
    internal bool hasThreadAffinity;
    private const int WAIT_OBJECT_0 = 0;
    private const int WAIT_ABANDONED = 128;
    private const int WAIT_FAILED = 2147483647;
    private const int ERROR_TOO_MANY_POSTS = 298;

    /// <summary>获取或设置本机操作系统句柄。</summary>
    /// <returns>IntPtr，它表示本机操作系统句柄。默认为 <see cref="F:System.Threading.WaitHandle.InvalidHandle" /> 字段的值。</returns>
    /// <filterpriority>2</filterpriority>
    [Obsolete("Use the SafeWaitHandle property instead.")]
    public virtual IntPtr Handle
    {
      [SecuritySafeCritical] get
      {
        if (this.safeWaitHandle != null)
          return this.safeWaitHandle.DangerousGetHandle();
        return WaitHandle.InvalidHandle;
      }
      [SecurityCritical, SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)] set
      {
        if (value == WaitHandle.InvalidHandle)
        {
          if (this.safeWaitHandle != null)
          {
            this.safeWaitHandle.SetHandleAsInvalid();
            this.safeWaitHandle = (SafeWaitHandle) null;
          }
        }
        else
          this.safeWaitHandle = new SafeWaitHandle(value, true);
        this.waitHandle = value;
      }
    }

    /// <summary>获取或设置本机操作系统句柄。</summary>
    /// <returns>
    /// <see cref="T:Microsoft.Win32.SafeHandles.SafeWaitHandle" />，它表示本机操作系统句柄。</returns>
    public SafeWaitHandle SafeWaitHandle
    {
      [SecurityCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail), SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        if (this.safeWaitHandle == null)
          this.safeWaitHandle = new SafeWaitHandle(WaitHandle.InvalidHandle, false);
        return this.safeWaitHandle;
      }
      [SecurityCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success), SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)] set
      {
        RuntimeHelpers.PrepareConstrainedRegions();
        try
        {
        }
        finally
        {
          if (value == null)
          {
            this.safeWaitHandle = (SafeWaitHandle) null;
            this.waitHandle = WaitHandle.InvalidHandle;
          }
          else
          {
            this.safeWaitHandle = value;
            this.waitHandle = this.safeWaitHandle.DangerousGetHandle();
          }
        }
      }
    }

    /// <summary>初始化 <see cref="T:System.Threading.WaitHandle" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    protected WaitHandle()
    {
      this.Init();
    }

    [SecuritySafeCritical]
    private static IntPtr GetInvalidHandle()
    {
      return Win32Native.INVALID_HANDLE_VALUE;
    }

    [SecuritySafeCritical]
    private void Init()
    {
      this.safeWaitHandle = (SafeWaitHandle) null;
      this.waitHandle = WaitHandle.InvalidHandle;
      this.hasThreadAffinity = false;
    }

    [SecurityCritical]
    internal void SetHandleInternal(SafeWaitHandle handle)
    {
      this.safeWaitHandle = handle;
      this.waitHandle = handle.DangerousGetHandle();
    }

    /// <summary>阻止当前线程，直到当前的 <see cref="T:System.Threading.WaitHandle" /> 收到信号为止，同时使用 32 位带符号整数指定时间间隔，并指定是否在等待之前退出同步域。</summary>
    /// <returns>如果当前实例收到信号，则为 true；否则为 false。</returns>
    /// <param name="millisecondsTimeout">等待的毫秒数，或为 <see cref="F:System.Threading.Timeout.Infinite" /> (-1)，表示无限期等待。</param>
    /// <param name="exitContext">如果等待之前先退出上下文的同步域（如果在同步上下文中），并在稍后重新获取它，则为 true；否则为 false。</param>
    /// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed. </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out. </exception>
    /// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex.This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
    /// <exception cref="T:System.InvalidOperationException">The current instance is a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
    /// <filterpriority>2</filterpriority>
    public virtual bool WaitOne(int millisecondsTimeout, bool exitContext)
    {
      if (millisecondsTimeout < -1)
        throw new ArgumentOutOfRangeException("millisecondsTimeout", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      return this.WaitOne((long) millisecondsTimeout, exitContext);
    }

    /// <summary>阻止当前线程，直到当前实例收到信号为止，同时使用 <see cref="T:System.TimeSpan" /> 指定时间间隔，并指定是否在等待之前退出同步域。</summary>
    /// <returns>如果当前实例收到信号，则为 true；否则为 false。</returns>
    /// <param name="timeout">表示等待毫秒数的 <see cref="T:System.TimeSpan" />，或表示 -1 毫秒（无限期等待）的 <see cref="T:System.TimeSpan" />。</param>
    /// <param name="exitContext">如果等待之前先退出上下文的同步域（如果在同步上下文中），并在稍后重新获取它，则为 true；否则为 false。</param>
    /// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed. </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out.-or-<paramref name="timeout" /> is greater than <see cref="F:System.Int32.MaxValue" />. </exception>
    /// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex.This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
    /// <exception cref="T:System.InvalidOperationException">The current instance is a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
    /// <filterpriority>2</filterpriority>
    public virtual bool WaitOne(TimeSpan timeout, bool exitContext)
    {
      long timeout1 = (long) timeout.TotalMilliseconds;
      if (-1L > timeout1 || (long) int.MaxValue < timeout1)
        throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      return this.WaitOne(timeout1, exitContext);
    }

    /// <summary>阻止当前线程，直到当前 <see cref="T:System.Threading.WaitHandle" /> 收到信号。</summary>
    /// <returns>如果当前实例收到信号，则为 true。如果当前实例永不发出信号，则 <see cref="M:System.Threading.WaitHandle.WaitOne(System.Int32,System.Boolean)" /> 永不返回。</returns>
    /// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed. </exception>
    /// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex.This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
    /// <exception cref="T:System.InvalidOperationException">The current instance is a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual bool WaitOne()
    {
      return this.WaitOne(-1, false);
    }

    /// <summary>阻止当前线程，直到当前 <see cref="T:System.Threading.WaitHandle" /> 收到信号，同时使用 32 位带符号整数指定时间间隔（以毫秒为单位）。</summary>
    /// <returns>如果当前实例收到信号，则为 true；否则为 false。</returns>
    /// <param name="millisecondsTimeout">等待的毫秒数，或为 <see cref="F:System.Threading.Timeout.Infinite" /> (-1)，表示无限期等待。</param>
    /// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed. </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out. </exception>
    /// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex.This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
    /// <exception cref="T:System.InvalidOperationException">The current instance is a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
    [__DynamicallyInvokable]
    public virtual bool WaitOne(int millisecondsTimeout)
    {
      return this.WaitOne(millisecondsTimeout, false);
    }

    /// <summary>阻止当前线程，直到当前实例收到信号，同时使用 <see cref="T:System.TimeSpan" /> 指定时间间隔。</summary>
    /// <returns>如果当前实例收到信号，则为 true；否则为 false。</returns>
    /// <param name="timeout">表示等待毫秒数的 <see cref="T:System.TimeSpan" />，或表示 -1 毫秒（无限期等待）的 <see cref="T:System.TimeSpan" />。</param>
    /// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed. </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out.-or-<paramref name="timeout" /> is greater than <see cref="F:System.Int32.MaxValue" />. </exception>
    /// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex.This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
    /// <exception cref="T:System.InvalidOperationException">The current instance is a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
    [__DynamicallyInvokable]
    public virtual bool WaitOne(TimeSpan timeout)
    {
      return this.WaitOne(timeout, false);
    }

    [SecuritySafeCritical]
    private bool WaitOne(long timeout, bool exitContext)
    {
      return WaitHandle.InternalWaitOne((SafeHandle) this.safeWaitHandle, timeout, this.hasThreadAffinity, exitContext);
    }

    [SecurityCritical]
    internal static bool InternalWaitOne(SafeHandle waitableSafeHandle, long millisecondsTimeout, bool hasThreadAffinity, bool exitContext)
    {
      if (waitableSafeHandle == null)
        throw new ObjectDisposedException((string) null, Environment.GetResourceString("ObjectDisposed_Generic"));
      int num1 = WaitHandle.WaitOneNative(waitableSafeHandle, (uint) millisecondsTimeout, hasThreadAffinity, exitContext);
      if (AppDomainPauseManager.IsPaused)
        AppDomainPauseManager.ResumeEvent.WaitOneWithoutFAS();
      int num2 = 128;
      if (num1 == num2)
        WaitHandle.ThrowAbandonedMutexException();
      int num3 = 258;
      return num1 != num3;
    }

    [SecurityCritical]
    internal bool WaitOneWithoutFAS()
    {
      if (this.safeWaitHandle == null)
        throw new ObjectDisposedException((string) null, Environment.GetResourceString("ObjectDisposed_Generic"));
      int num1 = WaitHandle.WaitOneNative((SafeHandle) this.safeWaitHandle, uint.MaxValue, this.hasThreadAffinity, false);
      int num2 = 128;
      if (num1 == num2)
        WaitHandle.ThrowAbandonedMutexException();
      int num3 = 258;
      return num1 != num3;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern int WaitOneNative(SafeHandle waitableSafeHandle, uint millisecondsTimeout, bool hasThreadAffinity, bool exitContext);

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern int WaitMultiple(WaitHandle[] waitHandles, int millisecondsTimeout, bool exitContext, bool WaitAll);

    /// <summary>等待指定数组中的所有元素收到信号，使用 <see cref="T:System.Int32" /> 值指定时间间隔，并指定是否在等待之前退出同步域。</summary>
    /// <returns>如果 <paramref name="waitHandles" /> 中的每个元素都已收到信号，则为 true；否则为 false。</returns>
    /// <param name="waitHandles">一个 WaitHandle 数组，包含当前实例将等待的对象。此数组不能包含对同一对象的多个引用（重复的元素）。</param>
    /// <param name="millisecondsTimeout">等待的毫秒数，或为 <see cref="F:System.Threading.Timeout.Infinite" /> (-1)，表示无限期等待。</param>
    /// <param name="exitContext">如果等待之前先退出上下文的同步域（如果在同步上下文中），并在稍后重新获取它，则为 true；否则为 false。</param>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="waitHandles" /> parameter is null.-or- One or more of the objects in the <paramref name="waitHandles" /> array is null. -or-<paramref name="waitHandles" /> is an array with no elements and the .NET Framework version is 2.0 or later. </exception>
    /// <exception cref="T:System.DuplicateWaitObjectException">The <paramref name="waitHandles" /> array contains elements that are duplicates. </exception>
    /// <exception cref="T:System.NotSupportedException">The number of objects in <paramref name="waitHandles" /> is greater than the system permits.-or- The <see cref="T:System.STAThreadAttribute" /> attribute is applied to the thread procedure for the current thread, and <paramref name="waitHandles" /> contains more than one element. </exception>
    /// <exception cref="T:System.ApplicationException">
    /// <paramref name="waitHandles" /> is an array with no elements and the .NET Framework version is 1.0 or 1.1. </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out. </exception>
    /// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex.This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
    /// <exception cref="T:System.InvalidOperationException">The <paramref name="waitHandles" /> array contains a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    public static bool WaitAll(WaitHandle[] waitHandles, int millisecondsTimeout, bool exitContext)
    {
      if (waitHandles == null)
        throw new ArgumentNullException(Environment.GetResourceString("ArgumentNull_Waithandles"));
      if (waitHandles.Length == 0)
        throw new ArgumentNullException(Environment.GetResourceString("Argument_EmptyWaithandleArray"));
      if (waitHandles.Length > 64)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_MaxWaitHandles"));
      if (-1 > millisecondsTimeout)
        throw new ArgumentOutOfRangeException("millisecondsTimeout", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      WaitHandle[] waitHandles1 = new WaitHandle[waitHandles.Length];
      for (int index = 0; index < waitHandles.Length; ++index)
      {
        WaitHandle waitHandle = waitHandles[index];
        if (waitHandle == null)
          throw new ArgumentNullException(Environment.GetResourceString("ArgumentNull_ArrayElement"));
        if (RemotingServices.IsTransparentProxy((object) waitHandle))
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_WaitOnTransparentProxy"));
        waitHandles1[index] = waitHandle;
      }
      int num = WaitHandle.WaitMultiple(waitHandles1, millisecondsTimeout, exitContext, true);
      if (AppDomainPauseManager.IsPaused)
        AppDomainPauseManager.ResumeEvent.WaitOneWithoutFAS();
      if (128 <= num && 128 + waitHandles1.Length > num)
        WaitHandle.ThrowAbandonedMutexException();
      GC.KeepAlive((object) waitHandles1);
      return num != 258;
    }

    /// <summary>等待指定数组中的所有元素收到信号，使用 <see cref="T:System.TimeSpan" /> 值指定时间间隔，并指定是否在等待之前退出同步域。</summary>
    /// <returns>如果 true 中的每个元素都收到信号，则为 <paramref name="waitHandles" />；否则为 false。</returns>
    /// <param name="waitHandles">一个 WaitHandle 数组，包含当前实例将等待的对象。此数组不能包含对同一对象的多个引用。</param>
    /// <param name="timeout">表示等待毫秒数的 <see cref="T:System.TimeSpan" />，或表示 -1 毫秒（无限期等待）的 <see cref="T:System.TimeSpan" />。</param>
    /// <param name="exitContext">如果等待之前先退出上下文的同步域（如果在同步上下文中），并在稍后重新获取它，则为 true；否则为 false。</param>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="waitHandles" /> parameter is null. -or- One or more of the objects in the <paramref name="waitHandles" /> array is null. -or-<paramref name="waitHandles" /> is an array with no elements and the .NET Framework version is 2.0 or later. </exception>
    /// <exception cref="T:System.DuplicateWaitObjectException">The <paramref name="waitHandles" /> array contains elements that are duplicates. </exception>
    /// <exception cref="T:System.NotSupportedException">The number of objects in <paramref name="waitHandles" /> is greater than the system permits.-or- The <see cref="T:System.STAThreadAttribute" /> attribute is applied to the thread procedure for the current thread, and <paramref name="waitHandles" /> contains more than one element. </exception>
    /// <exception cref="T:System.ApplicationException">
    /// <paramref name="waitHandles" /> is an array with no elements and the .NET Framework version is 1.0 or 1.1. </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out. -or-<paramref name="timeout" /> is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
    /// <exception cref="T:System.Threading.AbandonedMutexException">The wait terminated because a thread exited without releasing a mutex.This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
    /// <exception cref="T:System.InvalidOperationException">The <paramref name="waitHandles" /> array contains a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
    /// <filterpriority>1</filterpriority>
    public static bool WaitAll(WaitHandle[] waitHandles, TimeSpan timeout, bool exitContext)
    {
      long num = (long) timeout.TotalMilliseconds;
      if (-1L > num || (long) int.MaxValue < num)
        throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      return WaitHandle.WaitAll(waitHandles, (int) num, exitContext);
    }

    /// <summary>等待指定数组中的所有元素都收到信号。</summary>
    /// <returns>如果 true 中的每个元素都收到信号，则返回 <paramref name="waitHandles" />；否则该方法永不返回。</returns>
    /// <param name="waitHandles">一个 WaitHandle 数组，包含当前实例将等待的对象。此数组不能包含对同一对象的多个引用。</param>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="waitHandles" /> parameter is null.-or-One or more of the objects in the <paramref name="waitHandles" /> array are null. -or-<paramref name="waitHandles" /> is an array with no elements and the .NET Framework version is 2.0 or later.</exception>
    /// <exception cref="T:System.DuplicateWaitObjectException">In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.ArgumentException" />, instead.The <paramref name="waitHandles" /> array contains elements that are duplicates. </exception>
    /// <exception cref="T:System.NotSupportedException">The number of objects in <paramref name="waitHandles" /> is greater than the system permits.-or- The <see cref="T:System.STAThreadAttribute" /> attribute is applied to the thread procedure for the current thread, and <paramref name="waitHandles" /> contains more than one element. </exception>
    /// <exception cref="T:System.ApplicationException">
    /// <paramref name="waitHandles" /> is an array with no elements and the .NET Framework version is 1.0 or 1.1. </exception>
    /// <exception cref="T:System.Threading.AbandonedMutexException">The wait terminated because a thread exited without releasing a mutex.This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
    /// <exception cref="T:System.InvalidOperationException">The <paramref name="waitHandles" /> array contains a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool WaitAll(WaitHandle[] waitHandles)
    {
      return WaitHandle.WaitAll(waitHandles, -1, true);
    }

    /// <summary>等待指定数组中的所有元素接收信号，同时使用 <see cref="T:System.Int32" /> 值指定时间间隔。</summary>
    /// <returns>如果 <paramref name="waitHandles" /> 中的每个元素都已收到信号，则为 true；否则为 false。</returns>
    /// <param name="waitHandles">一个 WaitHandle 数组，包含当前实例将等待的对象。此数组不能包含对同一对象的多个引用（重复的元素）。</param>
    /// <param name="millisecondsTimeout">等待的毫秒数，或为 <see cref="F:System.Threading.Timeout.Infinite" /> (-1)，表示无限期等待。</param>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="waitHandles" /> parameter is null.-or- One or more of the objects in the <paramref name="waitHandles" /> array is null. -or-<paramref name="waitHandles" /> is an array with no elements. </exception>
    /// <exception cref="T:System.DuplicateWaitObjectException">In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.ArgumentException" />, instead.The <paramref name="waitHandles" /> array contains elements that are duplicates. </exception>
    /// <exception cref="T:System.NotSupportedException">The number of objects in <paramref name="waitHandles" /> is greater than the system permits.-or- The <see cref="T:System.STAThreadAttribute" /> attribute is applied to the thread procedure for the current thread, and <paramref name="waitHandles" /> contains more than one element. </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out. </exception>
    /// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex.This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
    /// <exception cref="T:System.InvalidOperationException">The <paramref name="waitHandles" /> array contains a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
    [__DynamicallyInvokable]
    public static bool WaitAll(WaitHandle[] waitHandles, int millisecondsTimeout)
    {
      return WaitHandle.WaitAll(waitHandles, millisecondsTimeout, true);
    }

    /// <summary>等待指定数组中的所有元素接收信号，同时使用 <see cref="T:System.TimeSpan" /> 值指定时间间隔。</summary>
    /// <returns>如果 <paramref name="waitHandles" /> 中的每个元素都已收到信号，则为 true；否则为 false。</returns>
    /// <param name="waitHandles">一个 WaitHandle 数组，包含当前实例将等待的对象。此数组不能包含对同一对象的多个引用。</param>
    /// <param name="timeout">表示等待毫秒数的 <see cref="T:System.TimeSpan" />，或表示 -1 毫秒（无限期等待）的 <see cref="T:System.TimeSpan" />。</param>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="waitHandles" /> parameter is null. -or- One or more of the objects in the <paramref name="waitHandles" /> array is null. -or-<paramref name="waitHandles" /> is an array with no elements. </exception>
    /// <exception cref="T:System.DuplicateWaitObjectException">In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.ArgumentException" />, instead.The <paramref name="waitHandles" /> array contains elements that are duplicates. </exception>
    /// <exception cref="T:System.NotSupportedException">The number of objects in <paramref name="waitHandles" /> is greater than the system permits.-or- The <see cref="T:System.STAThreadAttribute" /> attribute is applied to the thread procedure for the current thread, and <paramref name="waitHandles" /> contains more than one element. </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out. -or-<paramref name="timeout" /> is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
    /// <exception cref="T:System.Threading.AbandonedMutexException">The wait terminated because a thread exited without releasing a mutex.This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
    /// <exception cref="T:System.InvalidOperationException">The <paramref name="waitHandles" /> array contains a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
    [__DynamicallyInvokable]
    public static bool WaitAll(WaitHandle[] waitHandles, TimeSpan timeout)
    {
      return WaitHandle.WaitAll(waitHandles, timeout, true);
    }

    /// <summary>等待指定数组中的任一元素收到信号，使用 32 位带符号整数指定时间间隔并指定是否在等待之前退出同步域。</summary>
    /// <returns>满足等待的对象的数组索引；如果没有任何对象满足等待，并且等效于 <see cref="F:System.Threading.WaitHandle.WaitTimeout" /> 的时间间隔已过，则为 <paramref name="millisecondsTimeout" />。</returns>
    /// <param name="waitHandles">一个 WaitHandle 数组，包含当前实例将等待的对象。</param>
    /// <param name="millisecondsTimeout">等待的毫秒数，或为 <see cref="F:System.Threading.Timeout.Infinite" /> (-1)，表示无限期等待。</param>
    /// <param name="exitContext">如果等待之前先退出上下文的同步域（如果在同步上下文中），并在稍后重新获取它，则为 true；否则为 false。</param>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="waitHandles" /> parameter is null.-or-One or more of the objects in the <paramref name="waitHandles" /> array is null. </exception>
    /// <exception cref="T:System.NotSupportedException">The number of objects in <paramref name="waitHandles" /> is greater than the system permits. </exception>
    /// <exception cref="T:System.ApplicationException">
    /// <paramref name="waitHandles" /> is an array with no elements, and the .NET Framework version is 1.0 or 1.1. </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out. </exception>
    /// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex.This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="waitHandles" /> is an array with no elements, and the .NET Framework version is 2.0 or later. </exception>
    /// <exception cref="T:System.InvalidOperationException">The <paramref name="waitHandles" /> array contains a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    public static int WaitAny(WaitHandle[] waitHandles, int millisecondsTimeout, bool exitContext)
    {
      if (waitHandles == null)
        throw new ArgumentNullException(Environment.GetResourceString("ArgumentNull_Waithandles"));
      if (waitHandles.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyWaithandleArray"));
      if (64 < waitHandles.Length)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_MaxWaitHandles"));
      if (-1 > millisecondsTimeout)
        throw new ArgumentOutOfRangeException("millisecondsTimeout", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      WaitHandle[] waitHandles1 = new WaitHandle[waitHandles.Length];
      for (int index = 0; index < waitHandles.Length; ++index)
      {
        WaitHandle waitHandle = waitHandles[index];
        if (waitHandle == null)
          throw new ArgumentNullException(Environment.GetResourceString("ArgumentNull_ArrayElement"));
        if (RemotingServices.IsTransparentProxy((object) waitHandle))
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_WaitOnTransparentProxy"));
        waitHandles1[index] = waitHandle;
      }
      int num = WaitHandle.WaitMultiple(waitHandles1, millisecondsTimeout, exitContext, false);
      if (AppDomainPauseManager.IsPaused)
        AppDomainPauseManager.ResumeEvent.WaitOneWithoutFAS();
      if (128 <= num && 128 + waitHandles1.Length > num)
      {
        int location = num - 128;
        if (0 <= location && location < waitHandles1.Length)
          WaitHandle.ThrowAbandonedMutexException(location, waitHandles1[location]);
        else
          WaitHandle.ThrowAbandonedMutexException();
      }
      GC.KeepAlive((object) waitHandles1);
      return num;
    }

    /// <summary>等待指定数组中的任一元素收到信号，使用 <see cref="T:System.TimeSpan" /> 指定时间间隔并指定是否在等待之前退出同步域。</summary>
    /// <returns>满足等待的对象的数组索引；如果没有任何对象满足等待，并且等效于 <paramref name="timeout" /> 的时间间隔已过，则为 <see cref="F:System.Threading.WaitHandle.WaitTimeout" />。</returns>
    /// <param name="waitHandles">一个 WaitHandle 数组，包含当前实例将等待的对象。</param>
    /// <param name="timeout">表示等待毫秒数的 <see cref="T:System.TimeSpan" />，或表示 -1 毫秒（无限期等待）的 <see cref="T:System.TimeSpan" />。</param>
    /// <param name="exitContext">如果等待之前先退出上下文的同步域（如果在同步上下文中），并在稍后重新获取它，则为 true；否则为 false。</param>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="waitHandles" /> parameter is null.-or-One or more of the objects in the <paramref name="waitHandles" /> array is null. </exception>
    /// <exception cref="T:System.NotSupportedException">The number of objects in <paramref name="waitHandles" /> is greater than the system permits. </exception>
    /// <exception cref="T:System.ApplicationException">
    /// <paramref name="waitHandles" /> is an array with no elements, and the .NET Framework version is 1.0 or 1.1. </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out. -or-<paramref name="timeout" /> is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
    /// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex.This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="waitHandles" /> is an array with no elements, and the .NET Framework version is 2.0 or later. </exception>
    /// <exception cref="T:System.InvalidOperationException">The <paramref name="waitHandles" /> array contains a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
    /// <filterpriority>1</filterpriority>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    public static int WaitAny(WaitHandle[] waitHandles, TimeSpan timeout, bool exitContext)
    {
      long num = (long) timeout.TotalMilliseconds;
      if (-1L > num || (long) int.MaxValue < num)
        throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      return WaitHandle.WaitAny(waitHandles, (int) num, exitContext);
    }

    /// <summary>等待指定数组中的任意元素接收信号，同时使用 <see cref="T:System.TimeSpan" /> 指定时间间隔。</summary>
    /// <returns>满足等待的对象的数组索引；如果没有任何对象满足等待，并且等效于 <paramref name="timeout" /> 的时间间隔已过，则为 <see cref="F:System.Threading.WaitHandle.WaitTimeout" />。</returns>
    /// <param name="waitHandles">一个 WaitHandle 数组，包含当前实例将等待的对象。</param>
    /// <param name="timeout">表示等待毫秒数的 <see cref="T:System.TimeSpan" />，或表示 -1 毫秒（无限期等待）的 <see cref="T:System.TimeSpan" />。</param>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="waitHandles" /> parameter is null.-or-One or more of the objects in the <paramref name="waitHandles" /> array is null. </exception>
    /// <exception cref="T:System.NotSupportedException">The number of objects in <paramref name="waitHandles" /> is greater than the system permits. </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out. -or-<paramref name="timeout" /> is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
    /// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex.This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="waitHandles" /> is an array with no elements. </exception>
    /// <exception cref="T:System.InvalidOperationException">The <paramref name="waitHandles" /> array contains a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static int WaitAny(WaitHandle[] waitHandles, TimeSpan timeout)
    {
      return WaitHandle.WaitAny(waitHandles, timeout, true);
    }

    /// <summary>等待指定数组中的任一元素收到信号。</summary>
    /// <returns>满足等待的对象的数组索引。</returns>
    /// <param name="waitHandles">一个 WaitHandle 数组，包含当前实例将等待的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="waitHandles" /> parameter is null.-or-One or more of the objects in the <paramref name="waitHandles" /> array is null. </exception>
    /// <exception cref="T:System.NotSupportedException">The number of objects in <paramref name="waitHandles" /> is greater than the system permits. </exception>
    /// <exception cref="T:System.ApplicationException">
    /// <paramref name="waitHandles" /> is an array with no elements, and the .NET Framework version is 1.0 or 1.1. </exception>
    /// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex.This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="waitHandles" /> is an array with no elements, and the .NET Framework version is 2.0 or later. </exception>
    /// <exception cref="T:System.InvalidOperationException">The <paramref name="waitHandles" /> array contains a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
    /// <filterpriority>1</filterpriority>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static int WaitAny(WaitHandle[] waitHandles)
    {
      return WaitHandle.WaitAny(waitHandles, -1, true);
    }

    /// <summary>等待指定数组中的任意元素接收信号，同时使用 32 位有符号整数指定时间间隔。</summary>
    /// <returns>满足等待的对象的数组索引；如果没有任何对象满足等待，并且等效于 <see cref="F:System.Threading.WaitHandle.WaitTimeout" /> 的时间间隔已过，则为 <paramref name="millisecondsTimeout" />。</returns>
    /// <param name="waitHandles">一个 WaitHandle 数组，包含当前实例将等待的对象。</param>
    /// <param name="millisecondsTimeout">等待的毫秒数，或为 <see cref="F:System.Threading.Timeout.Infinite" /> (-1)，表示无限期等待。</param>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="waitHandles" /> parameter is null.-or-One or more of the objects in the <paramref name="waitHandles" /> array is null. </exception>
    /// <exception cref="T:System.NotSupportedException">The number of objects in <paramref name="waitHandles" /> is greater than the system permits. </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out. </exception>
    /// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex.This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="waitHandles" /> is an array with no elements. </exception>
    /// <exception cref="T:System.InvalidOperationException">The <paramref name="waitHandles" /> array contains a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static int WaitAny(WaitHandle[] waitHandles, int millisecondsTimeout)
    {
      return WaitHandle.WaitAny(waitHandles, millisecondsTimeout, true);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern int SignalAndWaitOne(SafeWaitHandle waitHandleToSignal, SafeWaitHandle waitHandleToWaitOn, int millisecondsTimeout, bool hasThreadAffinity, bool exitContext);

    /// <summary>向一个 <see cref="T:System.Threading.WaitHandle" /> 发出信号并等待另一个。</summary>
    /// <returns>如果信号和等待都成功完成，则为 true；如果等待没有完成，则此方法不返回。</returns>
    /// <param name="toSignal">要发出信号的 <see cref="T:System.Threading.WaitHandle" />。</param>
    /// <param name="toWaitOn">要等待的 <see cref="T:System.Threading.WaitHandle" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="toSignal" /> is null.-or-<paramref name="toWaitOn" /> is null. </exception>
    /// <exception cref="T:System.NotSupportedException">The method was called on a thread that has <see cref="T:System.STAThreadAttribute" />. </exception>
    /// <exception cref="T:System.PlatformNotSupportedException">This method is not supported on Windows 98 or Windows Millennium Edition. </exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="toSignal" /> is a semaphore, and it already has a full count. </exception>
    /// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex.This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
    /// <filterpriority>1</filterpriority>
    public static bool SignalAndWait(WaitHandle toSignal, WaitHandle toWaitOn)
    {
      return WaitHandle.SignalAndWait(toSignal, toWaitOn, -1, false);
    }

    /// <summary>向一个 <see cref="T:System.Threading.WaitHandle" /> 发出信号并等待另一个，指定超时间隔为 <see cref="T:System.TimeSpan" />，并指定在进入等待前是否退出上下文的同步域。</summary>
    /// <returns>如果信号发送和等待均成功完成，则为 true；如果信号发送完成，但等待超时，则为 false。</returns>
    /// <param name="toSignal">要发出信号的 <see cref="T:System.Threading.WaitHandle" />。</param>
    /// <param name="toWaitOn">要等待的 <see cref="T:System.Threading.WaitHandle" />。</param>
    /// <param name="timeout">一个 <see cref="T:System.TimeSpan" />，表示要等待的间隔。如果值是 -1，则等待是无限期的。</param>
    /// <param name="exitContext">如果等待之前先退出上下文的同步域（如果在同步上下文中），并在稍后重新获取它，则为 true；否则为 false。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="toSignal" /> is null.-or-<paramref name="toWaitOn" /> is null. </exception>
    /// <exception cref="T:System.NotSupportedException">The method was called on a thread that has <see cref="T:System.STAThreadAttribute" />. </exception>
    /// <exception cref="T:System.PlatformNotSupportedException">This method is not supported on Windows 98 or Windows Millennium Edition.</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="toSignal" /> is a semaphore, and it already has a full count. </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="timeout" /> evaluates to a negative number of milliseconds other than -1. -or-<paramref name="timeout" /> is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
    /// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex.This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
    /// <filterpriority>1</filterpriority>
    public static bool SignalAndWait(WaitHandle toSignal, WaitHandle toWaitOn, TimeSpan timeout, bool exitContext)
    {
      long num = (long) timeout.TotalMilliseconds;
      if (-1L > num || (long) int.MaxValue < num)
        throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      return WaitHandle.SignalAndWait(toSignal, toWaitOn, (int) num, exitContext);
    }

    /// <summary>向一个 <see cref="T:System.Threading.WaitHandle" /> 发出信号并等待另一个，指定超时间隔为 32 位有符号整数，并指定在进入等待前是否退出上下文的同步域。</summary>
    /// <returns>如果信号发送和等待均成功完成，则为 true；如果信号发送完成，但等待超时，则为 false。</returns>
    /// <param name="toSignal">要发出信号的 <see cref="T:System.Threading.WaitHandle" />。</param>
    /// <param name="toWaitOn">要等待的 <see cref="T:System.Threading.WaitHandle" />。</param>
    /// <param name="millisecondsTimeout">一个整数，表示要等待的间隔。如果值是 <see cref="F:System.Threading.Timeout.Infinite" />，即 -1，则等待是无限期的。</param>
    /// <param name="exitContext">如果等待之前先退出上下文的同步域（如果在同步上下文中），并在稍后重新获取它，则为 true；否则为 false。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="toSignal" /> is null.-or-<paramref name="toWaitOn" /> is null. </exception>
    /// <exception cref="T:System.NotSupportedException">The method is called on a thread that has <see cref="T:System.STAThreadAttribute" />. </exception>
    /// <exception cref="T:System.PlatformNotSupportedException">This method is not supported on Windows 98 or Windows Millennium Edition. </exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="toSignal" /> is a semaphore, and it already has a full count. </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out. </exception>
    /// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex.This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
    /// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Threading.WaitHandle" /> cannot be signaled because it would exceed its maximum count.</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    public static bool SignalAndWait(WaitHandle toSignal, WaitHandle toWaitOn, int millisecondsTimeout, bool exitContext)
    {
      if (toSignal == null)
        throw new ArgumentNullException("toSignal");
      if (toWaitOn == null)
        throw new ArgumentNullException("toWaitOn");
      if (-1 > millisecondsTimeout)
        throw new ArgumentOutOfRangeException("millisecondsTimeout", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      int num = WaitHandle.SignalAndWaitOne(toSignal.safeWaitHandle, toWaitOn.safeWaitHandle, millisecondsTimeout, toWaitOn.hasThreadAffinity, exitContext);
      if (int.MaxValue != num && toSignal.hasThreadAffinity)
      {
        Thread.EndCriticalRegion();
        Thread.EndThreadAffinity();
      }
      if (128 == num)
        WaitHandle.ThrowAbandonedMutexException();
      if (298 == num)
        throw new InvalidOperationException(Environment.GetResourceString("Threading.WaitHandleTooManyPosts"));
      return num == 0;
    }

    private static void ThrowAbandonedMutexException()
    {
      throw new AbandonedMutexException();
    }

    private static void ThrowAbandonedMutexException(int location, WaitHandle handle)
    {
      throw new AbandonedMutexException(location, handle);
    }

    /// <summary>释放由当前 <see cref="T:System.Threading.WaitHandle" /> 占用的所有资源。</summary>
    /// <filterpriority>2</filterpriority>
    public virtual void Close()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>当在派生类中重写时，释放 <see cref="T:System.Threading.WaitHandle" /> 使用的非托管资源，并且可选择释放托管资源。</summary>
    /// <param name="explicitDisposing">若要释放托管资源和非托管资源，则为 true；若仅释放非托管资源，则为 false。</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    protected virtual void Dispose(bool explicitDisposing)
    {
      if (this.safeWaitHandle == null)
        return;
      this.safeWaitHandle.Close();
    }

    /// <summary>释放 <see cref="T:System.Threading.WaitHandle" /> 类的当前实例所使用的所有资源。</summary>
    [__DynamicallyInvokable]
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    internal enum OpenExistingResult
    {
      Success,
      NameNotFound,
      PathNotFound,
      NameInvalid,
    }
  }
}
