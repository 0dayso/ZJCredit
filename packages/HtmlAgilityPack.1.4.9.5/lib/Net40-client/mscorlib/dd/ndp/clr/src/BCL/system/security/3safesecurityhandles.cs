// Decompiled with JetBrains decompiler
// Type: Microsoft.Win32.SafeHandles.SafeLsaPolicyHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System;
using System.Security;

namespace Microsoft.Win32.SafeHandles
{
  [SecurityCritical]
  internal sealed class SafeLsaPolicyHandle : SafeHandleZeroOrMinusOneIsInvalid
  {
    internal static SafeLsaPolicyHandle InvalidHandle
    {
      get
      {
        return new SafeLsaPolicyHandle(IntPtr.Zero);
      }
    }

    private SafeLsaPolicyHandle()
      : base(true)
    {
    }

    internal SafeLsaPolicyHandle(IntPtr handle)
      : base(true)
    {
      this.SetHandle(handle);
    }

    [SecurityCritical]
    protected override bool ReleaseHandle()
    {
      return Win32Native.LsaClose(this.handle) == 0;
    }
  }
}
