// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.SafeHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.InteropServices
{
  /// <summary>表示操作系统句柄的包装类。必须继承此类。</summary>
  [SecurityCritical]
  [__DynamicallyInvokable]
  [SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
  public abstract class SafeHandle : CriticalFinalizerObject, IDisposable
  {
    /// <summary>指定要包装的句柄。</summary>
    protected IntPtr handle;
    private int _state;
    private bool _ownsHandle;
    private bool _fullyInitialized;

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
        return (this._state & 1) == 1;
      }
    }

    /// <summary>在派生类中重写时，获取一个值，该值指示句柄值是否无效。</summary>
    /// <returns>如果句柄值无效，则为 true；否则为 false。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [__DynamicallyInvokable]
    public abstract bool IsInvalid { [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success), __DynamicallyInvokable] get; }

    /// <summary>用指定的无效句柄值初始化 <see cref="T:System.Runtime.InteropServices.SafeHandle" /> 类的新实例。</summary>
    /// <param name="invalidHandleValue">无效句柄的值（通常为 0 或 -1）。<see cref="P:System.Runtime.InteropServices.SafeHandle.IsInvalid" /> 的实现应对此值返回 true。</param>
    /// <param name="ownsHandle">在终止阶段使 true 可靠地释放句柄，则为 <see cref="T:System.Runtime.InteropServices.SafeHandle" />；否则为 false（不建议使用）。</param>
    /// <exception cref="T:System.TypeLoadException">该派生类位于没有非托管代码访问权限的程序集中。</exception>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    protected SafeHandle(IntPtr invalidHandleValue, bool ownsHandle)
    {
      this.handle = invalidHandleValue;
      this._state = 4;
      this._ownsHandle = ownsHandle;
      if (!ownsHandle)
        GC.SuppressFinalize((object) this);
      this._fullyInitialized = true;
    }

    /// <summary>释放与句柄关联的所有资源。</summary>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    ~SafeHandle()
    {
      this.Dispose(false);
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private void InternalFinalize();

    /// <summary>将句柄设置为预先存在的指定句柄。</summary>
    /// <param name="handle">要使用的预先存在的句柄。</param>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    protected void SetHandle(IntPtr handle)
    {
      this.handle = handle;
    }

    /// <summary>返回 <see cref="F:System.Runtime.InteropServices.SafeHandle.handle" /> 字段的值。</summary>
    /// <returns>IntPtr，表示 <see cref="F:System.Runtime.InteropServices.SafeHandle.handle" /> 字段的值。如果句柄已使用 <see cref="M:System.Runtime.InteropServices.SafeHandle.SetHandleAsInvalid" /> 标记为无效，此方法仍返回原来的句柄值，该值可能已失效。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    public IntPtr DangerousGetHandle()
    {
      return this.handle;
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

    /// <summary>释放 <see cref="T:System.Runtime.InteropServices.SafeHandle" /> 类使用的所有资源。</summary>
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

    /// <summary>释放 <see cref="T:System.Runtime.InteropServices.SafeHandle" /> 类所使用的非托管资源，指定是否执行常规释放操作。</summary>
    /// <param name="disposing">如进行常规释放操作，则为 true；如终结句柄，则为 false。</param>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    protected virtual void Dispose(bool disposing)
    {
      if (disposing)
        this.InternalDispose();
      else
        this.InternalFinalize();
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private void InternalDispose();

    /// <summary>将句柄标记为不再使用。</summary>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public void SetHandleAsInvalid();

    /// <summary>在派生类中重写时，执行释放句柄所需的代码。</summary>
    /// <returns>如果句柄释放成功，则为 true；如果出现灾难性故障，则为  false。这种情况下，它生成一个 releaseHandleFailed MDA 托管调试助手。</returns>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    protected abstract bool ReleaseHandle();

    /// <summary>手动递增 <see cref="T:System.Runtime.InteropServices.SafeHandle" /> 实例中的引用计数器。</summary>
    /// <param name="success">如果成功递增引用计数器，则为 true；否则为 false。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public void DangerousAddRef(ref bool success);

    /// <summary>手动递减 <see cref="T:System.Runtime.InteropServices.SafeHandle" /> 实例中的引用计数器。</summary>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public void DangerousRelease();
  }
}
