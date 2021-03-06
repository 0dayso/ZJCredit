﻿// Decompiled with JetBrains decompiler
// Type: Microsoft.Win32.SafeHandles.SafeViewOfFileHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System;
using System.Security;

namespace Microsoft.Win32.SafeHandles
{
  [SecurityCritical]
  internal sealed class SafeViewOfFileHandle : SafeHandleZeroOrMinusOneIsInvalid
  {
    [SecurityCritical]
    internal SafeViewOfFileHandle()
      : base(true)
    {
    }

    [SecurityCritical]
    internal SafeViewOfFileHandle(IntPtr handle, bool ownsHandle)
      : base(ownsHandle)
    {
      this.SetHandle(handle);
    }

    [SecurityCritical]
    protected override bool ReleaseHandle()
    {
      if (!Win32Native.UnmapViewOfFile(this.handle))
        return false;
      this.handle = IntPtr.Zero;
      return true;
    }
  }
}
