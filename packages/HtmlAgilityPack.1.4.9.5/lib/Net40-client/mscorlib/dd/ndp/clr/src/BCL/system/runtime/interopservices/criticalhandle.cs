// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.CriticalHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.InteropServices
{
  /// <summary>表示句柄资源的包装类。</summary>
  [SecurityCritical]
  [__DynamicallyInvokable]
  [SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
  public abstract class CriticalHandle : CriticalFinalizerObject, IDisposable
  {
    /// <summary>指定要包装的句柄。</summary>
    protected IntPtr handle;
    private bool _isClosed;

    /// <summary>获取一个值，该值指示句柄是否已关闭。</summary>
    /// <returns>如果句柄已关闭，则为 true；否则为 false。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [__DynamicallyInvokable]
    public bool IsClosed
    {
      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success), __DynamicallyInvokable] get
      {
        return this._isClosed;
      }
    }

    /// <summary>在派生类中重写后，获取一个值，该值指示句柄值是否无效。</summary>
    /// <returns>如果句柄有效，则为 true；否则为 false。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [__DynamicallyInvokable]
    public abstract bool IsInvalid { [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success), __DynamicallyInvokable] get; }

    /// <summary>用指定的无效句柄值初始化 <see cref="T:System.Runtime.InteropServices.CriticalHandle" /> 类的新实例。</summary>
    /// <param name="invalidHandleValue">无效句柄的值（通常为 0 或 -1）。</param>
    /// <exception cref="T:System.TypeLoadException">该派生类位于没有非托管代码访问权限的程序集中。</exception>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    protected CriticalHandle(IntPtr invalidHandleValue)
    {
      this.handle = invalidHandleValue;
      this._isClosed = false;
    }

    /// <summary>释放与句柄关联的所有资源。</summary>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    ~CriticalHandle()
    {
      this.Dispose(false);
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    private void Cleanup()
    {
      if (this.IsClosed)
        return;
      this._isClosed = true;
      if (this.IsInvalid)
        return;
      int lastWin32Error = Marshal.GetLastWin32Error();
      if (!this.ReleaseHandle())
        this.FireCustomerDebugProbe();
      Marshal.SetLastWin32Error(lastWin32Error);
      GC.SuppressFinalize((object) this);
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private void FireCustomerDebugProbe();

    /// <summary>将句柄设置为预先存在的指定句柄。</summary>
    /// <param name="handle">要使用的预先存在的句柄。</param>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    protected void SetHandle(IntPtr handle)
    {
      this.handle = handle;
    }

    /// <summary>标记句柄，以便释放资源。</summary>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    public void Close()
    {
      this.Dispose(true);
    }

    /// <summary>释放由 <see cref="T:System.Runtime.InteropServices.CriticalHandle" /> 使用的所有资源。</summary>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public void Dispose()
    {
      this.Dispose(true);
    }

    /// <summary>释放 <see cref="T:System.Runtime.InteropServices.CriticalHandle" /> 类所使用的非托管资源，并指定是否执行常规释放 (Dispose) 操作。</summary>
    /// <param name="disposing">如进行常规释放操作，则为 true；如终结句柄，则为 false。</param>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    protected virtual void Dispose(bool disposing)
    {
      this.Cleanup();
    }

    /// <summary>将句柄标记为无效。</summary>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public void SetHandleAsInvalid()
    {
      this._isClosed = true;
      GC.SuppressFinalize((object) this);
    }

    /// <summary>如果在派生类中重写，执行释放句柄所需的代码。</summary>
    /// <returns>如果句柄释放成功，则为 true；如果出现灾难性故障，则为 false。这种情况下，该方法生成一个 releaseHandleFailed MDA 托管调试助手。</returns>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    protected abstract bool ReleaseHandle();
  }
}
