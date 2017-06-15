// Decompiled with JetBrains decompiler
// Type: System.Threading.HostExecutionContextManager
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Threading
{
  /// <summary>提供使公共语言运行时宿主可以参与执行上下文的流动（或移植）的功能。</summary>
  /// <filterpriority>2</filterpriority>
  public class HostExecutionContextManager
  {
    private static volatile bool _fIsHostedChecked;
    private static volatile bool _fIsHosted;
    private static HostExecutionContextManager _hostExecutionContextManager;

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool HostSecurityManagerPresent();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int ReleaseHostSecurityContext(IntPtr context);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int CloneHostSecurityContext(SafeHandle context, SafeHandle clonedContext);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern int CaptureHostSecurityContext(SafeHandle capturedContext);

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern int SetHostSecurityContext(SafeHandle context, bool fReturnPrevious, SafeHandle prevContext);

    [SecurityCritical]
    internal static bool CheckIfHosted()
    {
      if (!HostExecutionContextManager._fIsHostedChecked)
      {
        HostExecutionContextManager._fIsHosted = HostExecutionContextManager.HostSecurityManagerPresent();
        HostExecutionContextManager._fIsHostedChecked = true;
      }
      return HostExecutionContextManager._fIsHosted;
    }

    /// <summary>从当前线程捕获宿主执行上下文。</summary>
    /// <returns>一个 <see cref="T:System.Threading.HostExecutionContext" /> 对象，表示当前线程的宿主执行上下文。</returns>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    public virtual HostExecutionContext Capture()
    {
      HostExecutionContext executionContext = (HostExecutionContext) null;
      if (HostExecutionContextManager.CheckIfHosted())
      {
        IUnknownSafeHandle iunknownSafeHandle = new IUnknownSafeHandle();
        executionContext = new HostExecutionContext((object) iunknownSafeHandle);
        HostExecutionContextManager.CaptureHostSecurityContext((SafeHandle) iunknownSafeHandle);
      }
      return executionContext;
    }

    /// <summary>将当前宿主执行上下文设置为指定的宿主执行上下文。</summary>
    /// <returns>一个对象，用于将 <see cref="T:System.Threading.HostExecutionContext" /> 还原为其以前的状态。</returns>
    /// <param name="hostExecutionContext">要设置的 <see cref="T:System.Threading.HostExecutionContext" />。</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="hostExecutionContext" /> 不是通过捕获操作获取的。- 或 -<paramref name="hostExecutionContext" /> 已作为上一次 <see cref="M:System.Threading.HostExecutionContextManager.SetHostExecutionContext(System.Threading.HostExecutionContext)" /> 方法调用的参数。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public virtual object SetHostExecutionContext(HostExecutionContext hostExecutionContext)
    {
      if (hostExecutionContext == null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotNewCaptureContext"));
      HostExecutionContextSwitcher executionContextSwitcher = new HostExecutionContextSwitcher();
      ExecutionContext executionContext = Thread.CurrentThread.GetMutableExecutionContext();
      executionContextSwitcher.executionContext = executionContext;
      executionContextSwitcher.currentHostContext = hostExecutionContext;
      executionContextSwitcher.previousHostContext = (HostExecutionContext) null;
      if (HostExecutionContextManager.CheckIfHosted() && hostExecutionContext.State is IUnknownSafeHandle)
      {
        IUnknownSafeHandle iunknownSafeHandle = new IUnknownSafeHandle();
        executionContextSwitcher.previousHostContext = new HostExecutionContext((object) iunknownSafeHandle);
        HostExecutionContextManager.SetHostSecurityContext((SafeHandle) hostExecutionContext.State, true, (SafeHandle) iunknownSafeHandle);
      }
      executionContext.HostExecutionContext = hostExecutionContext;
      return (object) executionContextSwitcher;
    }

    /// <summary>将宿主执行上下文还原为其以前的状态。</summary>
    /// <param name="previousState">要恢复为的以前的上下文状态。</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="previousState" /> 为 null。- 或 -未对当前线程创建 <paramref name="previousState" />。- 或 -<paramref name="previousState" /> 不是 <see cref="T:System.Threading.HostExecutionContext" /> 的最后的状态。</exception>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    public virtual void Revert(object previousState)
    {
      HostExecutionContextSwitcher executionContextSwitcher = previousState as HostExecutionContextSwitcher;
      if (executionContextSwitcher == null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotOverrideSetWithoutRevert"));
      ExecutionContext executionContext1 = Thread.CurrentThread.GetMutableExecutionContext();
      ExecutionContext executionContext2 = executionContextSwitcher.executionContext;
      if (executionContext1 != executionContext2)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotUseSwitcherOtherThread"));
      executionContextSwitcher.executionContext = (ExecutionContext) null;
      if (executionContext1.HostExecutionContext != executionContextSwitcher.currentHostContext)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotUseSwitcherOtherThread"));
      HostExecutionContext executionContext3 = executionContextSwitcher.previousHostContext;
      if (HostExecutionContextManager.CheckIfHosted() && executionContext3 != null && executionContext3.State is IUnknownSafeHandle)
        HostExecutionContextManager.SetHostSecurityContext((SafeHandle) executionContext3.State, false, (SafeHandle) null);
      HostExecutionContext executionContext4 = executionContext3;
      executionContext1.HostExecutionContext = executionContext4;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static HostExecutionContext CaptureHostExecutionContext()
    {
      HostExecutionContext executionContext = (HostExecutionContext) null;
      HostExecutionContextManager executionContextManager = HostExecutionContextManager.GetCurrentHostExecutionContextManager();
      if (executionContextManager != null)
        executionContext = executionContextManager.Capture();
      return executionContext;
    }

    [SecurityCritical]
    internal static object SetHostExecutionContextInternal(HostExecutionContext hostContext)
    {
      HostExecutionContextManager executionContextManager = HostExecutionContextManager.GetCurrentHostExecutionContextManager();
      object obj = (object) null;
      if (executionContextManager != null)
        obj = executionContextManager.SetHostExecutionContext(hostContext);
      return obj;
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static HostExecutionContextManager GetCurrentHostExecutionContextManager()
    {
      AppDomainManager appDomainManager = AppDomainManager.CurrentAppDomainManager;
      if (appDomainManager != null)
        return appDomainManager.HostExecutionContextManager;
      return (HostExecutionContextManager) null;
    }

    internal static HostExecutionContextManager GetInternalHostExecutionContextManager()
    {
      if (HostExecutionContextManager._hostExecutionContextManager == null)
        HostExecutionContextManager._hostExecutionContextManager = new HostExecutionContextManager();
      return HostExecutionContextManager._hostExecutionContextManager;
    }
  }
}
