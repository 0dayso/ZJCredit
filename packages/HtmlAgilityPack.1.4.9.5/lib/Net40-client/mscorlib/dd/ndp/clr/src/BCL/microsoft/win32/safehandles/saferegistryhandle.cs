// Decompiled with JetBrains decompiler
// Type: Microsoft.Win32.SafeHandles.SafeRegistryHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Win32.SafeHandles
{
  /// <summary>表示 Windows 注册表的安全句柄。</summary>
  [SecurityCritical]
  public sealed class SafeRegistryHandle : SafeHandleZeroOrMinusOneIsInvalid
  {
    [SecurityCritical]
    internal SafeRegistryHandle()
      : base(true)
    {
    }

    /// <summary>初始化 <see cref="T:Microsoft.Win32.SafeHandles.SafeRegistryHandle" /> 类的新实例。</summary>
    /// <param name="preexistingHandle">一个对象，它表示要使用的预先存在的句柄。</param>
    /// <param name="ownsHandle">如果为 true，则在完成阶段可靠地释放句柄；如果为 false，则阻止可靠释放。</param>
    [SecurityCritical]
    public SafeRegistryHandle(IntPtr preexistingHandle, bool ownsHandle)
      : base(ownsHandle)
    {
      this.SetHandle(preexistingHandle);
    }

    [SecurityCritical]
    protected override bool ReleaseHandle()
    {
      return SafeRegistryHandle.RegCloseKey(this.handle) == 0;
    }

    [SuppressUnmanagedCodeSecurity]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [DllImport("advapi32.dll")]
    internal static extern int RegCloseKey(IntPtr hKey);
  }
}
