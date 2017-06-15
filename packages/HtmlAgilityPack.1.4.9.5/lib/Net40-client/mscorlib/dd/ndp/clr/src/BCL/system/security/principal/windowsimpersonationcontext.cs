// Decompiled with JetBrains decompiler
// Type: System.Security.Principal.WindowsImpersonationContext
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
  /// <summary>表示模拟操作之前的 Windows 用户。</summary>
  [ComVisible(true)]
  public class WindowsImpersonationContext : IDisposable
  {
    [SecurityCritical]
    private SafeAccessTokenHandle m_safeTokenHandle = SafeAccessTokenHandle.InvalidHandle;
    private WindowsIdentity m_wi;
    private FrameSecurityDescriptor m_fsd;

    [SecurityCritical]
    private WindowsImpersonationContext()
    {
    }

    [SecurityCritical]
    internal WindowsImpersonationContext(SafeAccessTokenHandle safeTokenHandle, WindowsIdentity wi, bool isImpersonating, FrameSecurityDescriptor fsd)
    {
      if (safeTokenHandle.IsInvalid)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidImpersonationToken"));
      if (isImpersonating)
      {
        if (!Win32Native.DuplicateHandle(Win32Native.GetCurrentProcess(), safeTokenHandle, Win32Native.GetCurrentProcess(), out this.m_safeTokenHandle, 0U, true, 2U))
          throw new SecurityException(Win32Native.GetMessage(Marshal.GetLastWin32Error()));
        this.m_wi = wi;
      }
      this.m_fsd = fsd;
    }

    /// <summary>将用户上下文恢复为该对象表示的 Windows 用户。</summary>
    /// <exception cref="T:System.Security.SecurityException">尝试将该方法用作将标识恢复为其自身之外的任何其他目的。</exception>
    [SecuritySafeCritical]
    public void Undo()
    {
      if (this.m_safeTokenHandle.IsInvalid)
      {
        int self = System.Security.Principal.Win32.RevertToSelf();
        if (self < 0)
          Environment.FailFast(Win32Native.GetMessage(self));
      }
      else
      {
        int self = System.Security.Principal.Win32.RevertToSelf();
        if (self < 0)
          Environment.FailFast(Win32Native.GetMessage(self));
        int errorCode = System.Security.Principal.Win32.ImpersonateLoggedOnUser(this.m_safeTokenHandle);
        if (errorCode < 0)
          throw new SecurityException(Win32Native.GetMessage(errorCode));
      }
      WindowsIdentity.UpdateThreadWI(this.m_wi);
      if (this.m_fsd == null)
        return;
      this.m_fsd.SetTokenHandles((SafeAccessTokenHandle) null, (SafeAccessTokenHandle) null);
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [HandleProcessCorruptedStateExceptions]
    internal bool UndoNoThrow()
    {
      bool flag;
      try
      {
        int errorCode;
        if (this.m_safeTokenHandle.IsInvalid)
        {
          errorCode = System.Security.Principal.Win32.RevertToSelf();
          if (errorCode < 0)
            Environment.FailFast(Win32Native.GetMessage(errorCode));
        }
        else
        {
          errorCode = System.Security.Principal.Win32.RevertToSelf();
          if (errorCode >= 0)
            errorCode = System.Security.Principal.Win32.ImpersonateLoggedOnUser(this.m_safeTokenHandle);
          else
            Environment.FailFast(Win32Native.GetMessage(errorCode));
        }
        flag = errorCode >= 0;
        if (this.m_fsd != null)
          this.m_fsd.SetTokenHandles((SafeAccessTokenHandle) null, (SafeAccessTokenHandle) null);
      }
      catch
      {
        flag = false;
      }
      return flag;
    }

    /// <summary>释放由 <see cref="T:System.Security.Principal.WindowsImpersonationContext" /> 占用的非托管资源，还可以另外再释放托管资源。</summary>
    /// <param name="disposing">若要释放托管资源和非托管资源，则为 true；若仅释放非托管资源，则为 false。</param>
    [SecuritySafeCritical]
    [ComVisible(false)]
    protected virtual void Dispose(bool disposing)
    {
      if (!disposing || this.m_safeTokenHandle == null || this.m_safeTokenHandle.IsClosed)
        return;
      this.Undo();
      this.m_safeTokenHandle.Dispose();
    }

    /// <summary>释放由 <see cref="T:System.Security.Principal.WindowsImpersonationContext" /> 使用的所有资源。</summary>
    [ComVisible(false)]
    public void Dispose()
    {
      this.Dispose(true);
    }
  }
}
