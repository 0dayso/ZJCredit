// Decompiled with JetBrains decompiler
// Type: Microsoft.Win32.SafeHandles.SafeWaitHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace Microsoft.Win32.SafeHandles
{
  /// <summary>表示等待句柄的包装类。</summary>
  [SecurityCritical]
  [__DynamicallyInvokable]
  public sealed class SafeWaitHandle : SafeHandleZeroOrMinusOneIsInvalid
  {
    private SafeWaitHandle()
      : base(true)
    {
    }

    /// <summary>初始化 <see cref="T:Microsoft.Win32.SafeHandles.SafeWaitHandle" /> 类的新实例。</summary>
    /// <param name="existingHandle">
    /// <see cref="T:System.IntPtr" /> 对象，表示要使用的预先存在的句柄。</param>
    /// <param name="ownsHandle">如果为 true，则在完成阶段可靠地释放句柄；如果为 false，则阻止可靠释放（建议不要这样做）。</param>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    public SafeWaitHandle(IntPtr existingHandle, bool ownsHandle)
      : base(ownsHandle)
    {
      this.SetHandle(existingHandle);
    }

    [SecurityCritical]
    protected override bool ReleaseHandle()
    {
      return Win32Native.CloseHandle(this.handle);
    }
  }
}
