// Decompiled with JetBrains decompiler
// Type: Microsoft.Win32.SafeHandles.SafeHandleZeroOrMinusOneIsInvalid
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace Microsoft.Win32.SafeHandles
{
  /// <summary>为 Win32 安全句柄实现提供基类，在这些实现中，值为 0 或 -1 都表示无效句柄。</summary>
  [SecurityCritical]
  [SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
  public abstract class SafeHandleZeroOrMinusOneIsInvalid : SafeHandle
  {
    /// <summary>获取一个值，该值指示句柄是否无效。</summary>
    /// <returns>如果句柄无效，则为 true；否则为 false。</returns>
    public override bool IsInvalid
    {
      [SecurityCritical] get
      {
        if (!this.handle.IsNull())
          return this.handle == new IntPtr(-1);
        return true;
      }
    }

    /// <summary>初始化 <see cref="T:Microsoft.Win32.SafeHandles.SafeHandleZeroOrMinusOneIsInvalid" /> 类的新实例，并指定是否要可靠地释放句柄。</summary>
    /// <param name="ownsHandle">如果为 true，则在完成阶段可靠地释放句柄；如果为 false，则阻止可靠释放（建议不要这样做）。</param>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    protected SafeHandleZeroOrMinusOneIsInvalid(bool ownsHandle)
      : base(IntPtr.Zero, ownsHandle)
    {
    }
  }
}
