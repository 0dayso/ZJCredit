// Decompiled with JetBrains decompiler
// Type: Microsoft.Win32.SafeHandles.CriticalHandleMinusOneIsInvalid
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
  /// <summary>为 Win32 关键句柄实现提供基类，在这些实现中值为 -1 时表示无效句柄。</summary>
  [SecurityCritical]
  [SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
  public abstract class CriticalHandleMinusOneIsInvalid : CriticalHandle
  {
    /// <summary>获取一个值，该值指示句柄是否无效。</summary>
    /// <returns>如果句柄无效，则为 true；否则为 false。</returns>
    public override bool IsInvalid
    {
      [SecurityCritical] get
      {
        return this.handle == new IntPtr(-1);
      }
    }

    /// <summary>初始化 <see cref="T:Microsoft.Win32.SafeHandles.CriticalHandleMinusOneIsInvalid" /> 类的新实例。</summary>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    protected CriticalHandleMinusOneIsInvalid()
      : base(new IntPtr(-1))
    {
    }
  }
}
