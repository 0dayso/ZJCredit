// Decompiled with JetBrains decompiler
// Type: System.Threading.ThreadPool
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Security;
using System.Security.Permissions;

namespace System.Threading
{
  /// <summary>提供一个线程池，该线程池可用于执行任务、发送工作项、处理异步 I/O、代表其他线程等待以及处理计时器。</summary>
  /// <filterpriority>2</filterpriority>
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public static class ThreadPool
  {
    /// <summary>设置可以同时处于活动状态的线程池的请求数目。所有大于此数目的请求将保持排队状态，直到线程池线程变为可用。</summary>
    /// <returns>如果更改成功，则为 true；否则为 false。</returns>
    /// <param name="workerThreads">线程池中辅助线程的最大数目。</param>
    /// <param name="completionPortThreads">线程池中异步 I/O 线程的最大数目。</param>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlThread" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, ControlThread = true)]
    public static bool SetMaxThreads(int workerThreads, int completionPortThreads)
    {
      return ThreadPool.SetMaxThreadsNative(workerThreads, completionPortThreads);
    }

    /// <summary>检索可以同时处于活动状态的线程池请求的数目。所有大于此数目的请求将保持排队状态，直到线程池线程变为可用。</summary>
    /// <param name="workerThreads">线程池中辅助线程的最大数目。</param>
    /// <param name="completionPortThreads">线程池中异步 I/O 线程的最大数目。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    public static void GetMaxThreads(out int workerThreads, out int completionPortThreads)
    {
      ThreadPool.GetMaxThreadsNative(out workerThreads, out completionPortThreads);
    }

    /// <summary>发出新的请求时，在切换到管理线程创建和销毁的算法之前设置线程池按需创建的线程的最小数量。</summary>
    /// <returns>如果更改成功，则为 true；否则为 false。</returns>
    /// <param name="workerThreads">要由线程池根据需要创建的新的最小工作程序线程数。</param>
    /// <param name="completionPortThreads">要由线程池根据需要创建的新的最小空闲异步 I/O 线程数。</param>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlThread" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, ControlThread = true)]
    public static bool SetMinThreads(int workerThreads, int completionPortThreads)
    {
      return ThreadPool.SetMinThreadsNative(workerThreads, completionPortThreads);
    }

    /// <summary>发出新的请求时，在切换到管理线程创建和销毁的算法之前检索线程池按需创建的线程的最小数量。</summary>
    /// <param name="workerThreads">当此方法返回时，将包含线程池根据需要创建的最少数量的辅助线程。</param>
    /// <param name="completionPortThreads">当此方法返回时，将包含线程池根据需要创建的最少数量的异步 I/O 线程。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    public static void GetMinThreads(out int workerThreads, out int completionPortThreads)
    {
      ThreadPool.GetMinThreadsNative(out workerThreads, out completionPortThreads);
    }

    /// <summary>检索由 <see cref="M:System.Threading.ThreadPool.GetMaxThreads(System.Int32@,System.Int32@)" /> 方法返回的最大线程池线程数和当前活动线程数之间的差值。</summary>
    /// <param name="workerThreads">可用辅助线程的数目。</param>
    /// <param name="completionPortThreads">可用异步 I/O 线程的数目。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    public static void GetAvailableThreads(out int workerThreads, out int completionPortThreads)
    {
      ThreadPool.GetAvailableThreadsNative(out workerThreads, out completionPortThreads);
    }

    /// <summary>指定表示超时（以毫秒为单位）的 32 位无符号整数，注册一个委托等待 <see cref="T:System.Threading.WaitHandle" />。</summary>
    /// <returns>
    /// <see cref="T:System.Threading.RegisteredWaitHandle" />，可用于取消已注册的等待操作。</returns>
    /// <param name="waitObject">要注册的 <see cref="T:System.Threading.WaitHandle" />。使用 <see cref="T:System.Threading.WaitHandle" /> 而非 <see cref="T:System.Threading.Mutex" />。</param>
    /// <param name="callBack">向 <paramref name="waitObject" /> 参数发出信号时调用的 <see cref="T:System.Threading.WaitOrTimerCallback" /> 委托。</param>
    /// <param name="state">传递给委托的对象。</param>
    /// <param name="millisecondsTimeOutInterval">以毫秒为单位的超时。如果 <paramref name="millisecondsTimeOutInterval" /> 参数为 0（零），函数将测试对象的状态并立即返回。如果 <paramref name="millisecondsTimeOutInterval" /> 为 -1，则函数的超时间隔永远不过期。</param>
    /// <param name="executeOnlyOnce">如果为 true，表示在调用了委托后，线程将不再在 <paramref name="waitObject" /> 参数上等待；如果为 false，表示每次完成等待操作后都重置计时器，直到注销等待。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="millisecondsTimeOutInterval" /> parameter is less than -1. </exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static RegisteredWaitHandle RegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, uint millisecondsTimeOutInterval, bool executeOnlyOnce)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, millisecondsTimeOutInterval, executeOnlyOnce, ref stackMark, true);
    }

    /// <summary>指定表示超时（以毫秒为单位）的 32 位无符号整数，注册一个委托等待 <see cref="T:System.Threading.WaitHandle" />。此方法不将调用堆栈传播到辅助线程。</summary>
    /// <returns>
    /// <see cref="T:System.Threading.RegisteredWaitHandle" /> 对象，可用于取消已注册的等待操作。</returns>
    /// <param name="waitObject">要注册的 <see cref="T:System.Threading.WaitHandle" />。使用 <see cref="T:System.Threading.WaitHandle" /> 而非 <see cref="T:System.Threading.Mutex" />。</param>
    /// <param name="callBack">向 <paramref name="waitObject" /> 参数发出信号时调用的委托。</param>
    /// <param name="state">传递给委托的对象。</param>
    /// <param name="millisecondsTimeOutInterval">以毫秒为单位的超时。如果 <paramref name="millisecondsTimeOutInterval" /> 参数为 0（零），函数将测试对象的状态并立即返回。如果 <paramref name="millisecondsTimeOutInterval" /> 为 -1，则函数的超时间隔永远不过期。</param>
    /// <param name="executeOnlyOnce">如果为 true，表示在调用了委托后，线程将不再在 <paramref name="waitObject" /> 参数上等待；如果为 false，表示每次完成等待操作后都重置计时器，直到注销等待。</param>
    /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
    /// </PermissionSet>
    [SecurityCritical]
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static RegisteredWaitHandle UnsafeRegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, uint millisecondsTimeOutInterval, bool executeOnlyOnce)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, millisecondsTimeOutInterval, executeOnlyOnce, ref stackMark, false);
    }

    [SecurityCritical]
    private static RegisteredWaitHandle RegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, uint millisecondsTimeOutInterval, bool executeOnlyOnce, ref StackCrawlMark stackMark, bool compressStack)
    {
      if (RemotingServices.IsTransparentProxy((object) waitObject))
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_WaitOnTransparentProxy"));
      RegisteredWaitHandle registeredWaitHandle = new RegisteredWaitHandle();
      if (callBack == null)
        throw new ArgumentNullException("WaitOrTimerCallback");
      state = (object) new _ThreadPoolWaitOrTimerCallback(callBack, state, compressStack, ref stackMark);
      registeredWaitHandle.SetWaitObject(waitObject);
      IntPtr handle = ThreadPool.RegisterWaitForSingleObjectNative(waitObject, state, millisecondsTimeOutInterval, executeOnlyOnce, registeredWaitHandle, ref stackMark, compressStack);
      registeredWaitHandle.SetHandle(handle);
      return registeredWaitHandle;
    }

    /// <summary>注册一个等待 <see cref="T:System.Threading.WaitHandle" /> 的委托，并指定一个 32 位有符号整数来表示超时值（以毫秒为单位）。</summary>
    /// <returns>封装本机句柄的 <see cref="T:System.Threading.RegisteredWaitHandle" />。</returns>
    /// <param name="waitObject">要注册的 <see cref="T:System.Threading.WaitHandle" />。使用 <see cref="T:System.Threading.WaitHandle" /> 而非 <see cref="T:System.Threading.Mutex" />。</param>
    /// <param name="callBack">向 <paramref name="waitObject" /> 参数发出信号时调用的 <see cref="T:System.Threading.WaitOrTimerCallback" /> 委托。</param>
    /// <param name="state">传递给委托的对象。</param>
    /// <param name="millisecondsTimeOutInterval">以毫秒为单位的超时。如果 <paramref name="millisecondsTimeOutInterval" /> 参数为 0（零），函数将测试对象的状态并立即返回。如果 <paramref name="millisecondsTimeOutInterval" /> 为 -1，则函数的超时间隔永远不过期。</param>
    /// <param name="executeOnlyOnce">如果为 true，表示在调用了委托后，线程将不再在 <paramref name="waitObject" /> 参数上等待；如果为 false，表示每次完成等待操作后都重置计时器，直到注销等待。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="millisecondsTimeOutInterval" /> parameter is less than -1. </exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static RegisteredWaitHandle RegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, int millisecondsTimeOutInterval, bool executeOnlyOnce)
    {
      if (millisecondsTimeOutInterval < -1)
        throw new ArgumentOutOfRangeException("millisecondsTimeOutInterval", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, (uint) millisecondsTimeOutInterval, executeOnlyOnce, ref stackMark, true);
    }

    /// <summary>注册一个等待 <see cref="T:System.Threading.WaitHandle" /> 的委托，并使用一个 32 位带符号整数来表示超时时间（以毫秒为单位）。此方法不将调用堆栈传播到辅助线程。</summary>
    /// <returns>
    /// <see cref="T:System.Threading.RegisteredWaitHandle" /> 对象，可用于取消已注册的等待操作。</returns>
    /// <param name="waitObject">要注册的 <see cref="T:System.Threading.WaitHandle" />。使用 <see cref="T:System.Threading.WaitHandle" /> 而非 <see cref="T:System.Threading.Mutex" />。</param>
    /// <param name="callBack">向 <paramref name="waitObject" /> 参数发出信号时调用的委托。</param>
    /// <param name="state">传递给委托的对象。</param>
    /// <param name="millisecondsTimeOutInterval">以毫秒为单位的超时。如果 <paramref name="millisecondsTimeOutInterval" /> 参数为 0（零），函数将测试对象的状态并立即返回。如果 <paramref name="millisecondsTimeOutInterval" /> 为 -1，则函数的超时间隔永远不过期。</param>
    /// <param name="executeOnlyOnce">如果为 true，表示在调用了委托后，线程将不再在 <paramref name="waitObject" /> 参数上等待；如果为 false，表示每次完成等待操作后都重置计时器，直到注销等待。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="millisecondsTimeOutInterval" /> parameter is less than -1. </exception>
    /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
    /// </PermissionSet>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static RegisteredWaitHandle UnsafeRegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, int millisecondsTimeOutInterval, bool executeOnlyOnce)
    {
      if (millisecondsTimeOutInterval < -1)
        throw new ArgumentOutOfRangeException("millisecondsTimeOutInterval", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, (uint) millisecondsTimeOutInterval, executeOnlyOnce, ref stackMark, false);
    }

    /// <summary>注册一个等待 <see cref="T:System.Threading.WaitHandle" /> 的委托，并指定一个 64 位有符号整数来表示超时值（以毫秒为单位）。</summary>
    /// <returns>封装本机句柄的 <see cref="T:System.Threading.RegisteredWaitHandle" />。</returns>
    /// <param name="waitObject">要注册的 <see cref="T:System.Threading.WaitHandle" />。使用 <see cref="T:System.Threading.WaitHandle" /> 而非 <see cref="T:System.Threading.Mutex" />。</param>
    /// <param name="callBack">向 <paramref name="waitObject" /> 参数发出信号时调用的 <see cref="T:System.Threading.WaitOrTimerCallback" /> 委托。</param>
    /// <param name="state">传递给委托的对象。</param>
    /// <param name="millisecondsTimeOutInterval">以毫秒为单位的超时。如果 <paramref name="millisecondsTimeOutInterval" /> 参数为 0（零），函数将测试对象的状态并立即返回。如果 <paramref name="millisecondsTimeOutInterval" /> 为 -1，则函数的超时间隔永远不过期。</param>
    /// <param name="executeOnlyOnce">如果为 true，表示在调用了委托后，线程将不再在 <paramref name="waitObject" /> 参数上等待；如果为 false，表示每次完成等待操作后都重置计时器，直到注销等待。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="millisecondsTimeOutInterval" /> parameter is less than -1. </exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static RegisteredWaitHandle RegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, long millisecondsTimeOutInterval, bool executeOnlyOnce)
    {
      if (millisecondsTimeOutInterval < -1L)
        throw new ArgumentOutOfRangeException("millisecondsTimeOutInterval", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, (uint) millisecondsTimeOutInterval, executeOnlyOnce, ref stackMark, true);
    }

    /// <summary>注册一个等待 <see cref="T:System.Threading.WaitHandle" /> 的委托，并指定一个 64 位有符号整数来表示超时值（以毫秒为单位）。此方法不将调用堆栈传播到辅助线程。</summary>
    /// <returns>
    /// <see cref="T:System.Threading.RegisteredWaitHandle" /> 对象，可用于取消已注册的等待操作。</returns>
    /// <param name="waitObject">要注册的 <see cref="T:System.Threading.WaitHandle" />。使用 <see cref="T:System.Threading.WaitHandle" /> 而非 <see cref="T:System.Threading.Mutex" />。</param>
    /// <param name="callBack">向 <paramref name="waitObject" /> 参数发出信号时调用的委托。</param>
    /// <param name="state">传递给委托的对象。</param>
    /// <param name="millisecondsTimeOutInterval">以毫秒为单位的超时。如果 <paramref name="millisecondsTimeOutInterval" /> 参数为 0（零），函数将测试对象的状态并立即返回。如果 <paramref name="millisecondsTimeOutInterval" /> 为 -1，则函数的超时间隔永远不过期。</param>
    /// <param name="executeOnlyOnce">如果为 true，表示在调用了委托后，线程将不再在 <paramref name="waitObject" /> 参数上等待；如果为 false，表示每次完成等待操作后都重置计时器，直到注销等待。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="millisecondsTimeOutInterval" /> parameter is less than -1. </exception>
    /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
    /// </PermissionSet>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static RegisteredWaitHandle UnsafeRegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, long millisecondsTimeOutInterval, bool executeOnlyOnce)
    {
      if (millisecondsTimeOutInterval < -1L)
        throw new ArgumentOutOfRangeException("millisecondsTimeOutInterval", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, (uint) millisecondsTimeOutInterval, executeOnlyOnce, ref stackMark, false);
    }

    /// <summary>注册一个等待 <see cref="T:System.Threading.WaitHandle" /> 的委托，并指定一个 <see cref="T:System.TimeSpan" /> 值来表示超时时间。</summary>
    /// <returns>封装本机句柄的 <see cref="T:System.Threading.RegisteredWaitHandle" />。</returns>
    /// <param name="waitObject">要注册的 <see cref="T:System.Threading.WaitHandle" />。使用 <see cref="T:System.Threading.WaitHandle" /> 而非 <see cref="T:System.Threading.Mutex" />。</param>
    /// <param name="callBack">向 <paramref name="waitObject" /> 参数发出信号时调用的 <see cref="T:System.Threading.WaitOrTimerCallback" /> 委托。</param>
    /// <param name="state">传递给委托的对象。</param>
    /// <param name="timeout">
    /// <see cref="T:System.TimeSpan" /> 表示的超时时间。如果 <paramref name="timeout" /> 为 0（零），则函数将测试对象的状态并立即返回。如果 <paramref name="timeout" /> 为 -1，则函数的超时间隔永远不过期。</param>
    /// <param name="executeOnlyOnce">如果为 true，表示在调用了委托后，线程将不再在 <paramref name="waitObject" /> 参数上等待；如果为 false，表示每次完成等待操作后都重置计时器，直到注销等待。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="timeout" /> parameter is less than -1. </exception>
    /// <exception cref="T:System.NotSupportedException">The <paramref name="timeout" /> parameter is greater than <see cref="F:System.Int32.MaxValue" />. </exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static RegisteredWaitHandle RegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, TimeSpan timeout, bool executeOnlyOnce)
    {
      long num = (long) timeout.TotalMilliseconds;
      if (num < -1L)
        throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      if (num > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("ArgumentOutOfRange_LessEqualToIntegerMaxVal"));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, (uint) num, executeOnlyOnce, ref stackMark, true);
    }

    /// <summary>注册一个等待 <see cref="T:System.Threading.WaitHandle" /> 的委托，并指定一个 <see cref="T:System.TimeSpan" /> 值来表示超时时间。此方法不将调用堆栈传播到辅助线程。</summary>
    /// <returns>
    /// <see cref="T:System.Threading.RegisteredWaitHandle" /> 对象，可用于取消已注册的等待操作。</returns>
    /// <param name="waitObject">要注册的 <see cref="T:System.Threading.WaitHandle" />。使用 <see cref="T:System.Threading.WaitHandle" /> 而非 <see cref="T:System.Threading.Mutex" />。</param>
    /// <param name="callBack">向 <paramref name="waitObject" /> 参数发出信号时调用的委托。</param>
    /// <param name="state">传递给委托的对象。</param>
    /// <param name="timeout">
    /// <see cref="T:System.TimeSpan" /> 表示的超时时间。如果 <paramref name="timeout" /> 为 0（零），则函数将测试对象的状态并立即返回。如果 <paramref name="timeout" /> 为 -1，则函数的超时间隔永远不过期。</param>
    /// <param name="executeOnlyOnce">如果为 true，表示在调用了委托后，线程将不再在 <paramref name="waitObject" /> 参数上等待；如果为 false，表示每次完成等待操作后都重置计时器，直到注销等待。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="timeout" /> parameter is less than -1. </exception>
    /// <exception cref="T:System.NotSupportedException">The <paramref name="timeout" /> parameter is greater than <see cref="F:System.Int32.MaxValue" />. </exception>
    /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
    /// </PermissionSet>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static RegisteredWaitHandle UnsafeRegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, TimeSpan timeout, bool executeOnlyOnce)
    {
      long num = (long) timeout.TotalMilliseconds;
      if (num < -1L)
        throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      if (num > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("ArgumentOutOfRange_LessEqualToIntegerMaxVal"));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, (uint) num, executeOnlyOnce, ref stackMark, false);
    }

    /// <summary>将方法排入队列以便执行，并指定包含该方法所用数据的对象。此方法在有线程池线程变得可用时执行。</summary>
    /// <returns>如果此方法成功排队，则为 true；如果无法将该工作项排队，则引发 <see cref="T:System.NotSupportedException" />。</returns>
    /// <param name="callBack">
    /// <see cref="T:System.Threading.WaitCallback" />，它表示要执行的方法。</param>
    /// <param name="state">包含方法所用数据的对象。</param>
    /// <exception cref="T:System.NotSupportedException">The common language runtime (CLR) is hosted, and the host does not support this action.</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="callBack" /> is null.</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static bool QueueUserWorkItem(WaitCallback callBack, object state)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return ThreadPool.QueueUserWorkItemHelper(callBack, state, ref stackMark, true);
    }

    /// <summary>将方法排入队列以便执行。此方法在有线程池线程变得可用时执行。</summary>
    /// <returns>如果此方法成功排队，则为 true；如果无法将该工作项排队，则引发 <see cref="T:System.NotSupportedException" />。</returns>
    /// <param name="callBack">一个 <see cref="T:System.Threading.WaitCallback" />，表示要执行的方法。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="callBack" /> is null.</exception>
    /// <exception cref="T:System.NotSupportedException">The common language runtime (CLR) is hosted, and the host does not support this action.</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static bool QueueUserWorkItem(WaitCallback callBack)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return ThreadPool.QueueUserWorkItemHelper(callBack, (object) null, ref stackMark, true);
    }

    /// <summary>将指定的委托排队到线程池，但不会将调用堆栈传播到辅助线程。</summary>
    /// <returns>如果方法成功，则为 true；如果未能将该工作项排队，则引发 <see cref="T:System.OutOfMemoryException" />。</returns>
    /// <param name="callBack">一个 <see cref="T:System.Threading.WaitCallback" />，表示当线程池中的线程选择工作项时调用的委托。</param>
    /// <param name="state">在接受线程池服务时传递给委托的对象。</param>
    /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
    /// <exception cref="T:System.ApplicationException">An out-of-memory condition was encountered.</exception>
    /// <exception cref="T:System.OutOfMemoryException">The work item could not be queued.</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="callBack" /> is null.</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
    /// </PermissionSet>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static bool UnsafeQueueUserWorkItem(WaitCallback callBack, object state)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return ThreadPool.QueueUserWorkItemHelper(callBack, state, ref stackMark, false);
    }

    [SecurityCritical]
    private static bool QueueUserWorkItemHelper(WaitCallback callBack, object state, ref StackCrawlMark stackMark, bool compressStack)
    {
      bool flag = true;
      if (callBack == null)
        throw new ArgumentNullException("WaitCallback");
      ThreadPool.EnsureVMInitialized();
      try
      {
      }
      finally
      {
        QueueUserWorkItemCallback workItemCallback = new QueueUserWorkItemCallback(callBack, state, compressStack, ref stackMark);
        ThreadPoolGlobals.workQueue.Enqueue((IThreadPoolWorkItem) workItemCallback, true);
        flag = true;
      }
      return flag;
    }

    [SecurityCritical]
    internal static void UnsafeQueueCustomWorkItem(IThreadPoolWorkItem workItem, bool forceGlobal)
    {
      ThreadPool.EnsureVMInitialized();
      try
      {
      }
      finally
      {
        ThreadPoolGlobals.workQueue.Enqueue(workItem, forceGlobal);
      }
    }

    [SecurityCritical]
    internal static bool TryPopCustomWorkItem(IThreadPoolWorkItem workItem)
    {
      if (!ThreadPoolGlobals.vmTpInitialized)
        return false;
      return ThreadPoolGlobals.workQueue.LocalFindAndPop(workItem);
    }

    [SecurityCritical]
    internal static IEnumerable<IThreadPoolWorkItem> GetQueuedWorkItems()
    {
      return ThreadPool.EnumerateQueuedWorkItems(ThreadPoolWorkQueue.allThreadQueues.Current, ThreadPoolGlobals.workQueue.queueTail);
    }

    internal static IEnumerable<IThreadPoolWorkItem> EnumerateQueuedWorkItems(ThreadPoolWorkQueue.WorkStealingQueue[] wsQueues, ThreadPoolWorkQueue.QueueSegment globalQueueTail)
    {
      if (wsQueues != null)
      {
        ThreadPoolWorkQueue.WorkStealingQueue[] workStealingQueueArray = wsQueues;
        for (int index = 0; index < workStealingQueueArray.Length; ++index)
        {
          ThreadPoolWorkQueue.WorkStealingQueue workStealingQueue = workStealingQueueArray[index];
          if (workStealingQueue != null && workStealingQueue.m_array != null)
          {
            IThreadPoolWorkItem[] items = workStealingQueue.m_array;
            for (int i = 0; i < items.Length; ++i)
            {
              IThreadPoolWorkItem threadPoolWorkItem = items[i];
              if (threadPoolWorkItem != null)
                yield return threadPoolWorkItem;
            }
            items = (IThreadPoolWorkItem[]) null;
          }
        }
        workStealingQueueArray = (ThreadPoolWorkQueue.WorkStealingQueue[]) null;
      }
      if (globalQueueTail != null)
      {
        ThreadPoolWorkQueue.QueueSegment segment;
        for (segment = globalQueueTail; segment != null; segment = segment.Next)
        {
          IThreadPoolWorkItem[] items = segment.nodes;
          for (int i = 0; i < items.Length; ++i)
          {
            IThreadPoolWorkItem threadPoolWorkItem = items[i];
            if (threadPoolWorkItem != null)
              yield return threadPoolWorkItem;
          }
          items = (IThreadPoolWorkItem[]) null;
        }
        segment = (ThreadPoolWorkQueue.QueueSegment) null;
      }
    }

    [SecurityCritical]
    internal static IEnumerable<IThreadPoolWorkItem> GetLocallyQueuedWorkItems()
    {
      return ThreadPool.EnumerateQueuedWorkItems(new ThreadPoolWorkQueue.WorkStealingQueue[1]{ ThreadPoolWorkQueueThreadLocals.threadLocals.workStealingQueue }, (ThreadPoolWorkQueue.QueueSegment) null);
    }

    [SecurityCritical]
    internal static IEnumerable<IThreadPoolWorkItem> GetGloballyQueuedWorkItems()
    {
      return ThreadPool.EnumerateQueuedWorkItems((ThreadPoolWorkQueue.WorkStealingQueue[]) null, ThreadPoolGlobals.workQueue.queueTail);
    }

    private static object[] ToObjectArray(IEnumerable<IThreadPoolWorkItem> workitems)
    {
      int length = 0;
      foreach (IThreadPoolWorkItem workitem in workitems)
        ++length;
      object[] objArray = new object[length];
      int index = 0;
      foreach (IThreadPoolWorkItem workitem in workitems)
      {
        if (index < objArray.Length)
          objArray[index] = (object) workitem;
        ++index;
      }
      return objArray;
    }

    [SecurityCritical]
    internal static object[] GetQueuedWorkItemsForDebugger()
    {
      return ThreadPool.ToObjectArray(ThreadPool.GetQueuedWorkItems());
    }

    [SecurityCritical]
    internal static object[] GetGloballyQueuedWorkItemsForDebugger()
    {
      return ThreadPool.ToObjectArray(ThreadPool.GetGloballyQueuedWorkItems());
    }

    [SecurityCritical]
    internal static object[] GetLocallyQueuedWorkItemsForDebugger()
    {
      return ThreadPool.ToObjectArray(ThreadPool.GetLocallyQueuedWorkItems());
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern bool RequestWorkerThread();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern unsafe bool PostQueuedCompletionStatus(NativeOverlapped* overlapped);

    /// <summary>将重叠的 I/O 操作排队以便执行。</summary>
    /// <returns>如果成功地将此操作排队到 I/O 完成端口，则为 true；否则为 false。</returns>
    /// <param name="overlapped">要排队的 <see cref="T:System.Threading.NativeOverlapped" /> 结构。</param>
    [SecurityCritical]
    [CLSCompliant(false)]
    public static unsafe bool UnsafeQueueNativeOverlapped(NativeOverlapped* overlapped)
    {
      return ThreadPool.PostQueuedCompletionStatus(overlapped);
    }

    [SecurityCritical]
    private static void EnsureVMInitialized()
    {
      if (ThreadPoolGlobals.vmTpInitialized)
        return;
      ThreadPool.InitializeVMTp(ref ThreadPoolGlobals.enableWorkerTracking);
      ThreadPoolGlobals.vmTpInitialized = true;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool SetMinThreadsNative(int workerThreads, int completionPortThreads);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool SetMaxThreadsNative(int workerThreads, int completionPortThreads);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void GetMinThreadsNative(out int workerThreads, out int completionPortThreads);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void GetMaxThreadsNative(out int workerThreads, out int completionPortThreads);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void GetAvailableThreadsNative(out int workerThreads, out int completionPortThreads);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool NotifyWorkItemComplete();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void ReportThreadStatus(bool isWorking);

    [SecuritySafeCritical]
    internal static void NotifyWorkItemProgress()
    {
      if (!ThreadPoolGlobals.vmTpInitialized)
        ThreadPool.InitializeVMTp(ref ThreadPoolGlobals.enableWorkerTracking);
      ThreadPool.NotifyWorkItemProgressNative();
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void NotifyWorkItemProgressNative();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool IsThreadPoolHosted();

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void InitializeVMTp(ref bool enableWorkerTracking);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern IntPtr RegisterWaitForSingleObjectNative(WaitHandle waitHandle, object state, uint timeOutInterval, bool executeOnlyOnce, RegisteredWaitHandle registeredWaitHandle, ref StackCrawlMark stackMark, bool compressStack);

    /// <summary>将操作系统句柄绑定到 <see cref="T:System.Threading.ThreadPool" />。</summary>
    /// <returns>如果绑定了句柄，则为 true；否则为 false。</returns>
    /// <param name="osHandle">持有句柄的 <see cref="T:System.IntPtr" />。在非托管端必须为重叠 I/O 打开该句柄。</param>
    /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [Obsolete("ThreadPool.BindHandle(IntPtr) has been deprecated.  Please use ThreadPool.BindHandle(SafeHandle) instead.", false)]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public static bool BindHandle(IntPtr osHandle)
    {
      return ThreadPool.BindIOCompletionCallbackNative(osHandle);
    }

    /// <summary>将操作系统句柄绑定到 <see cref="T:System.Threading.ThreadPool" />。</summary>
    /// <returns>如果绑定了句柄，则为 true；否则为 false。</returns>
    /// <param name="osHandle">保存操作系统句柄的 <see cref="T:System.Runtime.InteropServices.SafeHandle" />。在非托管端必须为重叠 I/O 打开该句柄。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="osHandle" /> is null. </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public static bool BindHandle(SafeHandle osHandle)
    {
      if (osHandle == null)
        throw new ArgumentNullException("osHandle");
      bool success = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        osHandle.DangerousAddRef(ref success);
        return ThreadPool.BindIOCompletionCallbackNative(osHandle.DangerousGetHandle());
      }
      finally
      {
        if (success)
          osHandle.DangerousRelease();
      }
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool BindIOCompletionCallbackNative(IntPtr fileHandle);
  }
}
