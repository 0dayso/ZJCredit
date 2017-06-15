// Decompiled with JetBrains decompiler
// Type: Microsoft.Win32.SafeHandles.SafeProcessHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System;
using System.Security;

namespace Microsoft.Win32.SafeHandles
{
  [SecurityCritical]
  internal sealed class SafeProcessHandle : SafeHandleZeroOrMinusOneIsInvalid
  {
    internal static SafeProcessHandle InvalidHandle
    {
      get
      {
        return new SafeProcessHandle(IntPtr.Zero);
      }
    }

    private SafeProcessHandle()
      : base(true)
    {
    }

    internal SafeProcessHandle(IntPtr handle)
      : base(true)
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
