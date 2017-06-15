// Decompiled with JetBrains decompiler
// Type: System.Threading.CompressedStack
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Threading
{
  /// <summary>提供方法用于设置和捕获当前线程上的压缩堆栈。此类不能被继承。</summary>
  /// <filterpriority>2</filterpriority>
  [Serializable]
  public sealed class CompressedStack : ISerializable
  {
    private volatile PermissionListSet m_pls;
    [SecurityCritical]
    private volatile SafeCompressedStackHandle m_csHandle;
    private bool m_canSkipEvaluation;
    internal static volatile RuntimeHelpers.TryCode tryCode;
    internal static volatile RuntimeHelpers.CleanupCode cleanupCode;

    internal bool CanSkipEvaluation
    {
      get
      {
        return this.m_canSkipEvaluation;
      }
      private set
      {
        this.m_canSkipEvaluation = value;
      }
    }

    internal PermissionListSet PLS
    {
      get
      {
        return this.m_pls;
      }
    }

    internal SafeCompressedStackHandle CompressedStackHandle
    {
      [SecurityCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)] get
      {
        return this.m_csHandle;
      }
      [SecurityCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)] private set
      {
        this.m_csHandle = value;
      }
    }

    [SecurityCritical]
    internal CompressedStack(SafeCompressedStackHandle csHandle)
    {
      this.m_csHandle = csHandle;
    }

    [SecurityCritical]
    private CompressedStack(SafeCompressedStackHandle csHandle, PermissionListSet pls)
    {
      this.m_csHandle = csHandle;
      this.m_pls = pls;
    }

    private CompressedStack(SerializationInfo info, StreamingContext context)
    {
      this.m_pls = (PermissionListSet) info.GetValue("PLS", typeof (PermissionListSet));
    }

    /// <summary>用重新创建此执行上下文的实例所需的逻辑上下文信息设置 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 对象。</summary>
    /// <param name="info">要用序列化信息填充的 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 对象。</param>
    /// <param name="context">表示序列化的目标上下文的 <see cref="T:System.Runtime.Serialization.StreamingContext" /> 结构。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="info" /> 为 null。</exception>
    /// <filterpriority>1</filterpriority>
    [SecurityCritical]
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      this.CompleteConstruction((CompressedStack) null);
      info.AddValue("PLS", (object) this.m_pls);
    }

    /// <summary>获取当前线程的压缩堆栈。</summary>
    /// <returns>当前线程的 <see cref="T:System.Threading.CompressedStack" />。</returns>
    /// <exception cref="T:System.Security.SecurityException">调用链中的调用方无权访问非托管代码。- 或 -对 <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" /> 的请求失败。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    ///   <IPermission class="System.Security.Permissions.StrongNameIdentityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PublicKeyBlob="00000000000000000400000000000000" />
    /// </PermissionSet>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static CompressedStack GetCompressedStack()
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return CompressedStack.GetCompressedStack(ref stackMark);
    }

    [SecurityCritical]
    internal static CompressedStack GetCompressedStack(ref StackCrawlMark stackMark)
    {
      CompressedStack innerCS = (CompressedStack) null;
      CompressedStack compressedStack;
      if (CodeAccessSecurityEngine.QuickCheckForAllDemands())
      {
        compressedStack = new CompressedStack((SafeCompressedStackHandle) null);
        compressedStack.CanSkipEvaluation = true;
      }
      else if (CodeAccessSecurityEngine.AllDomainsHomogeneousWithNoStackModifiers())
      {
        compressedStack = new CompressedStack(CompressedStack.GetDelayedCompressedStack(ref stackMark, false));
        compressedStack.m_pls = PermissionListSet.CreateCompressedState_HG();
      }
      else
      {
        compressedStack = new CompressedStack((SafeCompressedStackHandle) null);
        RuntimeHelpers.PrepareConstrainedRegions();
        try
        {
        }
        finally
        {
          compressedStack.CompressedStackHandle = CompressedStack.GetDelayedCompressedStack(ref stackMark, true);
          if (compressedStack.CompressedStackHandle != null)
          {
            if (CompressedStack.IsImmediateCompletionCandidate(compressedStack.CompressedStackHandle, out innerCS))
            {
              try
              {
                compressedStack.CompleteConstruction(innerCS);
              }
              finally
              {
                CompressedStack.DestroyDCSList(compressedStack.CompressedStackHandle);
              }
            }
          }
        }
      }
      return compressedStack;
    }

    /// <summary>从当前线程捕获压缩堆栈。</summary>
    /// <returns>一个 <see cref="T:System.Threading.CompressedStack" /> 对象。</returns>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static CompressedStack Capture()
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return CompressedStack.GetCompressedStack(ref stackMark);
    }

    /// <summary>在当前线程上的指定压缩堆栈中运行某个方法。</summary>
    /// <param name="compressedStack">要设置的 <see cref="T:System.Threading.CompressedStack" />。</param>
    /// <param name="callback">一个 <see cref="T:System.Threading.ContextCallback" />，表示要在指定安全上下文中运行的方法。</param>
    /// <param name="state">要传递给该回调方法的对象。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="compressedStack" /> 为 null。</exception>
    [SecurityCritical]
    public static void Run(CompressedStack compressedStack, ContextCallback callback, object state)
    {
      if (compressedStack == null)
        throw new ArgumentException(Environment.GetResourceString("Arg_NamedParamNull"), "compressedStack");
      if (CompressedStack.cleanupCode == null)
      {
        CompressedStack.tryCode = new RuntimeHelpers.TryCode(CompressedStack.runTryCode);
        CompressedStack.cleanupCode = new RuntimeHelpers.CleanupCode(CompressedStack.runFinallyCode);
      }
      CompressedStack.CompressedStackRunData compressedStackRunData = new CompressedStack.CompressedStackRunData(compressedStack, callback, state);
      RuntimeHelpers.ExecuteCodeWithGuaranteedCleanup(CompressedStack.tryCode, CompressedStack.cleanupCode, (object) compressedStackRunData);
    }

    [SecurityCritical]
    internal static void runTryCode(object userData)
    {
      CompressedStack.CompressedStackRunData compressedStackRunData = (CompressedStack.CompressedStackRunData) userData;
      compressedStackRunData.cssw = CompressedStack.SetCompressedStack(compressedStackRunData.cs, CompressedStack.GetCompressedStackThread());
      compressedStackRunData.callBack(compressedStackRunData.state);
    }

    [SecurityCritical]
    [PrePrepareMethod]
    internal static void runFinallyCode(object userData, bool exceptionThrown)
    {
      ((CompressedStack.CompressedStackRunData) userData).cssw.Undo();
    }

    [SecurityCritical]
    [HandleProcessCorruptedStateExceptions]
    internal static CompressedStackSwitcher SetCompressedStack(CompressedStack cs, CompressedStack prevCS)
    {
      CompressedStackSwitcher compressedStackSwitcher = new CompressedStackSwitcher();
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        RuntimeHelpers.PrepareConstrainedRegions();
        try
        {
        }
        finally
        {
          CompressedStack.SetCompressedStackThread(cs);
          compressedStackSwitcher.prev_CS = prevCS;
          compressedStackSwitcher.curr_CS = cs;
          compressedStackSwitcher.prev_ADStack = CompressedStack.SetAppDomainStack(cs);
        }
      }
      catch
      {
        compressedStackSwitcher.UndoNoThrow();
        throw;
      }
      return compressedStackSwitcher;
    }

    /// <summary>创建当前压缩堆栈的副本。</summary>
    /// <returns>一个 <see cref="T:System.Threading.CompressedStack" /> 对象，表示当前压缩堆栈。</returns>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public CompressedStack CreateCopy()
    {
      return new CompressedStack(this.m_csHandle, this.m_pls);
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal static IntPtr SetAppDomainStack(CompressedStack cs)
    {
      return Thread.CurrentThread.SetAppDomainStack(cs == null ? (SafeCompressedStackHandle) null : cs.CompressedStackHandle);
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal static void RestoreAppDomainStack(IntPtr appDomainStack)
    {
      Thread.CurrentThread.RestoreAppDomainStack(appDomainStack);
    }

    [SecurityCritical]
    internal static CompressedStack GetCompressedStackThread()
    {
      return Thread.CurrentThread.GetExecutionContextReader().SecurityContext.CompressedStack;
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    internal static void SetCompressedStackThread(CompressedStack cs)
    {
      Thread currentThread = Thread.CurrentThread;
      if (currentThread.GetExecutionContextReader().SecurityContext.CompressedStack == cs)
        return;
      ExecutionContext executionContext = currentThread.GetMutableExecutionContext();
      if (executionContext.SecurityContext != null)
      {
        executionContext.SecurityContext.CompressedStack = cs;
      }
      else
      {
        if (cs == null)
          return;
        executionContext.SecurityContext = new SecurityContext()
        {
          CompressedStack = cs
        };
      }
    }

    [SecurityCritical]
    internal bool CheckDemand(CodeAccessPermission demand, PermissionToken permToken, RuntimeMethodHandleInternal rmh)
    {
      this.CompleteConstruction((CompressedStack) null);
      if (this.PLS == null)
        return false;
      this.PLS.CheckDemand(demand, permToken, rmh);
      return false;
    }

    [SecurityCritical]
    internal bool CheckDemandNoHalt(CodeAccessPermission demand, PermissionToken permToken, RuntimeMethodHandleInternal rmh)
    {
      this.CompleteConstruction((CompressedStack) null);
      if (this.PLS == null)
        return true;
      return this.PLS.CheckDemand(demand, permToken, rmh);
    }

    [SecurityCritical]
    internal bool CheckSetDemand(PermissionSet pset, RuntimeMethodHandleInternal rmh)
    {
      this.CompleteConstruction((CompressedStack) null);
      if (this.PLS == null)
        return false;
      return this.PLS.CheckSetDemand(pset, rmh);
    }

    [SecurityCritical]
    internal bool CheckSetDemandWithModificationNoHalt(PermissionSet pset, out PermissionSet alteredDemandSet, RuntimeMethodHandleInternal rmh)
    {
      alteredDemandSet = (PermissionSet) null;
      this.CompleteConstruction((CompressedStack) null);
      if (this.PLS == null)
        return true;
      return this.PLS.CheckSetDemandWithModification(pset, out alteredDemandSet, rmh);
    }

    [SecurityCritical]
    internal void DemandFlagsOrGrantSet(int flags, PermissionSet grantSet)
    {
      this.CompleteConstruction((CompressedStack) null);
      if (this.PLS == null)
        return;
      this.PLS.DemandFlagsOrGrantSet(flags, grantSet);
    }

    [SecurityCritical]
    internal void GetZoneAndOrigin(ArrayList zoneList, ArrayList originList, PermissionToken zoneToken, PermissionToken originToken)
    {
      this.CompleteConstruction((CompressedStack) null);
      if (this.PLS == null)
        return;
      this.PLS.GetZoneAndOrigin(zoneList, originList, zoneToken, originToken);
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    internal void CompleteConstruction(CompressedStack innerCS)
    {
      if (this.PLS != null)
        return;
      PermissionListSet compressedState = PermissionListSet.CreateCompressedState(this, innerCS);
      lock (this)
      {
        if (this.PLS != null)
          return;
        this.m_pls = compressedState;
      }
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern SafeCompressedStackHandle GetDelayedCompressedStack(ref StackCrawlMark stackMark, bool walkStack);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void DestroyDelayedCompressedStack(IntPtr unmanagedCompressedStack);

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void DestroyDCSList(SafeCompressedStackHandle compressedStack);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int GetDCSCount(SafeCompressedStackHandle compressedStack);

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool IsImmediateCompletionCandidate(SafeCompressedStackHandle compressedStack, out CompressedStack innerCS);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern DomainCompressedStack GetDomainCompressedStack(SafeCompressedStackHandle compressedStack, int index);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void GetHomogeneousPLS(PermissionListSet hgPLS);

    internal class CompressedStackRunData
    {
      internal CompressedStack cs;
      internal ContextCallback callBack;
      internal object state;
      internal CompressedStackSwitcher cssw;

      internal CompressedStackRunData(CompressedStack cs, ContextCallback cb, object state)
      {
        this.cs = cs;
        this.callBack = cb;
        this.state = state;
        this.cssw = new CompressedStackSwitcher();
      }
    }
  }
}
