﻿// Decompiled with JetBrains decompiler
// Type: Microsoft.Win32.SafeHandles.SafePEFileHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Win32.SafeHandles
{
  [SecurityCritical]
  internal sealed class SafePEFileHandle : SafeHandleZeroOrMinusOneIsInvalid
  {
    private SafePEFileHandle()
      : base(true)
    {
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void ReleaseSafePEFileHandle(IntPtr peFile);

    [SecurityCritical]
    protected override bool ReleaseHandle()
    {
      SafePEFileHandle.ReleaseSafePEFileHandle(this.handle);
      return true;
    }
  }
}
