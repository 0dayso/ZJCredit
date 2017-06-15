// Decompiled with JetBrains decompiler
// Type: Microsoft.Win32.SafeHandles.SafeAccessTokenHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Win32.SafeHandles
{
  /// <summary>提供 Windows 线程或进程访问令牌的安全句柄。有关详细信息，请参阅访问令牌</summary>
  [SecurityCritical]
  public sealed class SafeAccessTokenHandle : SafeHandle
  {
    /// <summary>通过使用 <see cref="F:System.IntPtr.Zero" /> 实例化 <see cref="T:Microsoft.Win32.SafeHandles.SafeAccessTokenHandle" /> 对象来返回无效句柄。</summary>
    /// <returns>返回 <see cref="T:Microsoft.Win32.SafeHandles.SafeAccessTokenHandle" /> 对象。</returns>
    public static SafeAccessTokenHandle InvalidHandle
    {
      [SecurityCritical] get
      {
        return new SafeAccessTokenHandle(IntPtr.Zero);
      }
    }

    /// <summary>获取一个值，该值指示句柄是否无效。</summary>
    /// <returns>如果句柄无效，则为 true；否则为 false。</returns>
    public override bool IsInvalid
    {
      [SecurityCritical] get
      {
        if (!(this.handle == IntPtr.Zero))
          return this.handle == new IntPtr(-1);
        return true;
      }
    }

    private SafeAccessTokenHandle()
      : base(IntPtr.Zero, true)
    {
    }

    /// <summary>初始化 <see cref="T:Microsoft.Win32.SafeHandles.SafeAccessTokenHandle" /> 类的新实例。</summary>
    /// <param name="handle">
    /// <see cref="T:System.IntPtr" /> 对象，表示要使用的预先存在的句柄。使用 <see cref="F:System.IntPtr.Zero" /> 返回无效句柄。</param>
    public SafeAccessTokenHandle(IntPtr handle)
      : base(IntPtr.Zero, true)
    {
      this.SetHandle(handle);
    }

    [SecurityCritical]
    protected override bool ReleaseHandle()
    {
      return Win32Native.CloseHandle(this.handle);
    }
  }
}
