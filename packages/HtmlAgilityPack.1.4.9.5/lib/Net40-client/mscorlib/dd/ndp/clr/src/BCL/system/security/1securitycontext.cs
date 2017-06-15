// Decompiled with JetBrains decompiler
// Type: System.Security.SecurityContext
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Security.Principal;
using System.Threading;

namespace System.Security
{
  /// <summary>封装并传播在线程间传输的执行上下文的所有安全相关数据。此类不能被继承。</summary>
  public sealed class SecurityContext : IDisposable
  {
    private static bool _LegacyImpersonationPolicy = SecurityContext.GetImpersonationFlowMode() == WindowsImpersonationFlowMode.IMP_NOFLOW;
    private static bool _alwaysFlowImpersonationPolicy = SecurityContext.GetImpersonationFlowMode() == WindowsImpersonationFlowMode.IMP_ALWAYSFLOW;
    private ExecutionContext _executionContext;
    private volatile WindowsIdentity _windowsIdentity;
    private volatile CompressedStack _compressedStack;
    private static volatile SecurityContext _fullTrustSC;
    internal volatile bool isNewCapture;
    internal volatile SecurityContextDisableFlow _disableFlow;
    internal static volatile RuntimeHelpers.TryCode tryCode;
    internal static volatile RuntimeHelpers.CleanupCode cleanupCode;

    internal static SecurityContext FullTrustSecurityContext
    {
      [SecurityCritical] get
      {
        if (SecurityContext._fullTrustSC == null)
          SecurityContext._fullTrustSC = SecurityContext.CreateFullTrustSecurityContext();
        return SecurityContext._fullTrustSC;
      }
    }

    internal ExecutionContext ExecutionContext
    {
      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)] set
      {
        this._executionContext = value;
      }
    }

    internal WindowsIdentity WindowsIdentity
    {
      get
      {
        return this._windowsIdentity;
      }
      set
      {
        this._windowsIdentity = value;
      }
    }

    internal CompressedStack CompressedStack
    {
      get
      {
        return this._compressedStack;
      }
      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)] set
      {
        this._compressedStack = value;
      }
    }

    internal static bool AlwaysFlowImpersonationPolicy
    {
      get
      {
        return SecurityContext._alwaysFlowImpersonationPolicy;
      }
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal SecurityContext()
    {
    }

    /// <summary>释放由 <see cref="T:System.Security.SecurityContext" /> 类的当前实例占用的所有资源。</summary>
    public void Dispose()
    {
      if (this._windowsIdentity == null)
        return;
      this._windowsIdentity.Dispose();
    }

    /// <summary>在异步线程间取消安全上下文的流动。</summary>
    /// <returns>用于恢复流动的 <see cref="T:System.Threading.AsyncFlowControl" /> 结构。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public static AsyncFlowControl SuppressFlow()
    {
      return SecurityContext.SuppressFlow(SecurityContextDisableFlow.All);
    }

    /// <summary>在异步线程间取消当前安全上下文的 Windows 标识部分的流动。</summary>
    /// <returns>用于恢复流动的结构。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public static AsyncFlowControl SuppressFlowWindowsIdentity()
    {
      return SecurityContext.SuppressFlow(SecurityContextDisableFlow.WI);
    }

    [SecurityCritical]
    internal static AsyncFlowControl SuppressFlow(SecurityContextDisableFlow flags)
    {
      if (SecurityContext.IsFlowSuppressed(flags))
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotSupressFlowMultipleTimes"));
      ExecutionContext executionContext = Thread.CurrentThread.GetMutableExecutionContext();
      if (executionContext.SecurityContext == null)
        executionContext.SecurityContext = new SecurityContext();
      AsyncFlowControl asyncFlowControl = new AsyncFlowControl();
      asyncFlowControl.Setup(flags);
      return asyncFlowControl;
    }

    /// <summary>在异步线程间恢复安全上下文的流动。</summary>
    /// <exception cref="T:System.InvalidOperationException">The security context is null or an empty string.</exception>
    [SecuritySafeCritical]
    public static void RestoreFlow()
    {
      SecurityContext securityContext = Thread.CurrentThread.GetMutableExecutionContext().SecurityContext;
      if (securityContext == null || securityContext._disableFlow == SecurityContextDisableFlow.Nothing)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotRestoreUnsupressedFlow"));
      securityContext._disableFlow = SecurityContextDisableFlow.Nothing;
    }

    /// <summary>确定是否已取消安全上下文的流动。</summary>
    /// <returns>如果已取消流动，则为 true；否则为 false。</returns>
    public static bool IsFlowSuppressed()
    {
      return SecurityContext.IsFlowSuppressed(SecurityContextDisableFlow.All);
    }

    /// <summary>确定是否已取消当前安全上下文的 Windows 标识部分的流动。</summary>
    /// <returns>如果已取消流动，则为 true；否则为 false。</returns>
    public static bool IsWindowsIdentityFlowSuppressed()
    {
      if (!SecurityContext._LegacyImpersonationPolicy)
        return SecurityContext.IsFlowSuppressed(SecurityContextDisableFlow.WI);
      return true;
    }

    [SecuritySafeCritical]
    internal static bool IsFlowSuppressed(SecurityContextDisableFlow flags)
    {
      return Thread.CurrentThread.GetExecutionContextReader().SecurityContext.IsFlowSuppressed(flags);
    }

    /// <summary>在当前线程上指定的安全上下文中运行指定的方法。</summary>
    /// <param name="securityContext">要设置的安全上下文。</param>
    /// <param name="callback">表示要在指定的安全上下文中运行的方法的委托。</param>
    /// <param name="state">要传递给回调方法的对象。</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="securityContext" /> is null.-or-<paramref name="securityContext" /> was not acquired through a capture operation. -or-<paramref name="securityContext" /> has already been used as the argument to a <see cref="M:System.Security.SecurityContext.Run(System.Security.SecurityContext,System.Threading.ContextCallback,System.Object)" /> method call.</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void Run(SecurityContext securityContext, ContextCallback callback, object state)
    {
      if (securityContext == null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullContext"));
      StackCrawlMark stackMark = StackCrawlMark.LookForMe;
      if (!securityContext.isNewCapture)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotNewCaptureContext"));
      securityContext.isNewCapture = false;
      if (SecurityContext.CurrentlyInDefaultFTSecurityContext(Thread.CurrentThread.GetExecutionContextReader()) && securityContext.IsDefaultFTSecurityContext())
      {
        callback(state);
        if (SecurityContext.GetCurrentWI(Thread.CurrentThread.GetExecutionContextReader()) == null)
          return;
        WindowsIdentity.SafeRevertToSelf(ref stackMark);
      }
      else
        SecurityContext.RunInternal(securityContext, callback, state);
    }

    [SecurityCritical]
    internal static void RunInternal(SecurityContext securityContext, ContextCallback callBack, object state)
    {
      if (SecurityContext.cleanupCode == null)
      {
        SecurityContext.tryCode = new RuntimeHelpers.TryCode(SecurityContext.runTryCode);
        SecurityContext.cleanupCode = new RuntimeHelpers.CleanupCode(SecurityContext.runFinallyCode);
      }
      SecurityContext.SecurityContextRunData securityContextRunData = new SecurityContext.SecurityContextRunData(securityContext, callBack, state);
      RuntimeHelpers.ExecuteCodeWithGuaranteedCleanup(SecurityContext.tryCode, SecurityContext.cleanupCode, (object) securityContextRunData);
    }

    [SecurityCritical]
    internal static void runTryCode(object userData)
    {
      SecurityContext.SecurityContextRunData securityContextRunData = (SecurityContext.SecurityContextRunData) userData;
      securityContextRunData.scsw = SecurityContext.SetSecurityContext(securityContextRunData.sc, Thread.CurrentThread.GetExecutionContextReader().SecurityContext, true);
      securityContextRunData.callBack(securityContextRunData.state);
    }

    [SecurityCritical]
    [PrePrepareMethod]
    internal static void runFinallyCode(object userData, bool exceptionThrown)
    {
      ((SecurityContext.SecurityContextRunData) userData).scsw.Undo();
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static SecurityContextSwitcher SetSecurityContext(SecurityContext sc, SecurityContext.Reader prevSecurityContext, bool modifyCurrentExecutionContext)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return SecurityContext.SetSecurityContext(sc, prevSecurityContext, modifyCurrentExecutionContext, ref stackMark);
    }

    [SecurityCritical]
    [HandleProcessCorruptedStateExceptions]
    internal static SecurityContextSwitcher SetSecurityContext(SecurityContext sc, SecurityContext.Reader prevSecurityContext, bool modifyCurrentExecutionContext, ref StackCrawlMark stackMark)
    {
      SecurityContextDisableFlow contextDisableFlow = sc._disableFlow;
      sc._disableFlow = SecurityContextDisableFlow.Nothing;
      SecurityContextSwitcher securityContextSwitcher = new SecurityContextSwitcher();
      securityContextSwitcher.currSC = sc;
      securityContextSwitcher.prevSC = prevSecurityContext;
      if (modifyCurrentExecutionContext)
      {
        ExecutionContext executionContext = Thread.CurrentThread.GetMutableExecutionContext();
        securityContextSwitcher.currEC = executionContext;
        executionContext.SecurityContext = sc;
      }
      if (sc != null)
      {
        RuntimeHelpers.PrepareConstrainedRegions();
        try
        {
          securityContextSwitcher.wic = (WindowsImpersonationContext) null;
          if (!SecurityContext._LegacyImpersonationPolicy)
          {
            if (sc.WindowsIdentity != null)
              securityContextSwitcher.wic = sc.WindowsIdentity.Impersonate(ref stackMark);
            else if ((contextDisableFlow & SecurityContextDisableFlow.WI) == SecurityContextDisableFlow.Nothing && prevSecurityContext.WindowsIdentity != null)
              securityContextSwitcher.wic = WindowsIdentity.SafeRevertToSelf(ref stackMark);
          }
          securityContextSwitcher.cssw = CompressedStack.SetCompressedStack(sc.CompressedStack, prevSecurityContext.CompressedStack);
        }
        catch
        {
          securityContextSwitcher.UndoNoThrow();
          throw;
        }
      }
      return securityContextSwitcher;
    }

    /// <summary>创建当前安全上下文的副本。</summary>
    /// <returns>当前线程的安全上下文。</returns>
    /// <exception cref="T:System.InvalidOperationException">The current security context has been previously used, was marshaled across application domains, or was not acquired through the <see cref="M:System.Security.SecurityContext.Capture" /> method.</exception>
    [SecuritySafeCritical]
    public SecurityContext CreateCopy()
    {
      if (!this.isNewCapture)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotNewCaptureContext"));
      SecurityContext securityContext = new SecurityContext();
      securityContext.isNewCapture = true;
      securityContext._disableFlow = this._disableFlow;
      if (this.WindowsIdentity != null)
        securityContext._windowsIdentity = new WindowsIdentity(this.WindowsIdentity.AccessToken);
      if (this._compressedStack != null)
        securityContext._compressedStack = this._compressedStack.CreateCopy();
      return securityContext;
    }

    [SecuritySafeCritical]
    internal SecurityContext CreateMutableCopy()
    {
      SecurityContext securityContext = new SecurityContext();
      securityContext._disableFlow = this._disableFlow;
      if (this.WindowsIdentity != null)
        securityContext._windowsIdentity = new WindowsIdentity(this.WindowsIdentity.AccessToken);
      if (this._compressedStack != null)
        securityContext._compressedStack = this._compressedStack.CreateCopy();
      return securityContext;
    }

    /// <summary>捕获当前线程的安全上下文。</summary>
    /// <returns>当前线程的安全上下文。</returns>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static SecurityContext Capture()
    {
      if (SecurityContext.IsFlowSuppressed())
        return (SecurityContext) null;
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return SecurityContext.Capture(Thread.CurrentThread.GetExecutionContextReader(), ref stackMark) ?? SecurityContext.CreateFullTrustSecurityContext();
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static SecurityContext Capture(ExecutionContext.Reader currThreadEC, ref StackCrawlMark stackMark)
    {
      if (currThreadEC.SecurityContext.IsFlowSuppressed(SecurityContextDisableFlow.All))
        return (SecurityContext) null;
      if (SecurityContext.CurrentlyInDefaultFTSecurityContext(currThreadEC))
        return (SecurityContext) null;
      return SecurityContext.CaptureCore(currThreadEC, ref stackMark);
    }

    [SecurityCritical]
    private static SecurityContext CaptureCore(ExecutionContext.Reader currThreadEC, ref StackCrawlMark stackMark)
    {
      SecurityContext securityContext = new SecurityContext();
      securityContext.isNewCapture = true;
      if (!SecurityContext.IsWindowsIdentityFlowSuppressed())
      {
        WindowsIdentity currentWi = SecurityContext.GetCurrentWI(currThreadEC);
        if (currentWi != null)
          securityContext._windowsIdentity = new WindowsIdentity(currentWi.AccessToken);
      }
      else
        securityContext._disableFlow = SecurityContextDisableFlow.WI;
      securityContext.CompressedStack = CompressedStack.GetCompressedStack(ref stackMark);
      return securityContext;
    }

    [SecurityCritical]
    internal static SecurityContext CreateFullTrustSecurityContext()
    {
      SecurityContext securityContext = new SecurityContext();
      securityContext.isNewCapture = true;
      if (SecurityContext.IsWindowsIdentityFlowSuppressed())
        securityContext._disableFlow = SecurityContextDisableFlow.WI;
      securityContext.CompressedStack = new CompressedStack((SafeCompressedStackHandle) null);
      return securityContext;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static WindowsIdentity GetCurrentWI(ExecutionContext.Reader threadEC)
    {
      return SecurityContext.GetCurrentWI(threadEC, SecurityContext._alwaysFlowImpersonationPolicy);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static WindowsIdentity GetCurrentWI(ExecutionContext.Reader threadEC, bool cachedAlwaysFlowImpersonationPolicy)
    {
      if (cachedAlwaysFlowImpersonationPolicy)
        return WindowsIdentity.GetCurrentInternal(TokenAccessLevels.MaximumAllowed, true);
      return threadEC.SecurityContext.WindowsIdentity;
    }

    [SecurityCritical]
    internal static void RestoreCurrentWI(ExecutionContext.Reader currentEC, ExecutionContext.Reader prevEC, WindowsIdentity targetWI, bool cachedAlwaysFlowImpersonationPolicy)
    {
      if (!cachedAlwaysFlowImpersonationPolicy && prevEC.SecurityContext.WindowsIdentity == targetWI)
        return;
      SecurityContext.RestoreCurrentWIInternal(targetWI);
    }

    [SecurityCritical]
    private static void RestoreCurrentWIInternal(WindowsIdentity targetWI)
    {
      int self = System.Security.Principal.Win32.RevertToSelf();
      if (self < 0)
        Environment.FailFast(Win32Native.GetMessage(self));
      if (targetWI == null)
        return;
      SafeAccessTokenHandle accessToken = targetWI.AccessToken;
      if (accessToken == null || accessToken.IsInvalid)
        return;
      int errorCode = System.Security.Principal.Win32.ImpersonateLoggedOnUser(accessToken);
      if (errorCode >= 0)
        return;
      Environment.FailFast(Win32Native.GetMessage(errorCode));
    }

    [SecurityCritical]
    internal bool IsDefaultFTSecurityContext()
    {
      if (this.WindowsIdentity != null)
        return false;
      if (this.CompressedStack != null)
        return this.CompressedStack.CompressedStackHandle == null;
      return true;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool CurrentlyInDefaultFTSecurityContext(ExecutionContext.Reader threadEC)
    {
      if (SecurityContext.IsDefaultThreadSecurityInfo())
        return SecurityContext.GetCurrentWI(threadEC) == null;
      return false;
    }

    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern WindowsImpersonationFlowMode GetImpersonationFlowMode();

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool IsDefaultThreadSecurityInfo();

    internal struct Reader
    {
      private SecurityContext m_sc;

      public bool IsNull
      {
        get
        {
          return this.m_sc == null;
        }
      }

      public CompressedStack CompressedStack
      {
        get
        {
          if (!this.IsNull)
            return this.m_sc.CompressedStack;
          return (CompressedStack) null;
        }
      }

      public WindowsIdentity WindowsIdentity
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          if (!this.IsNull)
            return this.m_sc.WindowsIdentity;
          return (WindowsIdentity) null;
        }
      }

      public Reader(SecurityContext sc)
      {
        this.m_sc = sc;
      }

      public SecurityContext DangerousGetRawSecurityContext()
      {
        return this.m_sc;
      }

      public bool IsSame(SecurityContext sc)
      {
        return this.m_sc == sc;
      }

      public bool IsSame(SecurityContext.Reader sc)
      {
        return this.m_sc == sc.m_sc;
      }

      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public bool IsFlowSuppressed(SecurityContextDisableFlow flags)
      {
        if (this.m_sc != null)
          return (this.m_sc._disableFlow & flags) == flags;
        return false;
      }
    }

    internal class SecurityContextRunData
    {
      internal SecurityContext sc;
      internal ContextCallback callBack;
      internal object state;
      internal SecurityContextSwitcher scsw;

      internal SecurityContextRunData(SecurityContext securityContext, ContextCallback cb, object state)
      {
        this.sc = securityContext;
        this.callBack = cb;
        this.state = state;
        this.scsw = new SecurityContextSwitcher();
      }
    }
  }
}
