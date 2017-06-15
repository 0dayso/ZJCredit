// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.SafeProvHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32.SafeHandles;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  [SecurityCritical]
  internal sealed class SafeProvHandle : SafeHandleZeroOrMinusOneIsInvalid
  {
    internal static SafeProvHandle InvalidHandle
    {
      get
      {
        return new SafeProvHandle();
      }
    }

    private SafeProvHandle()
      : base(true)
    {
      this.SetHandle(IntPtr.Zero);
    }

    private SafeProvHandle(IntPtr handle)
      : base(true)
    {
      this.SetHandle(handle);
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void FreeCsp(IntPtr pProviderContext);

    [SecurityCritical]
    protected override bool ReleaseHandle()
    {
      SafeProvHandle.FreeCsp(this.handle);
      return true;
    }
  }
}
