// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.SafeKeyHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32.SafeHandles;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  [SecurityCritical]
  internal sealed class SafeKeyHandle : SafeHandleZeroOrMinusOneIsInvalid
  {
    internal static SafeKeyHandle InvalidHandle
    {
      get
      {
        return new SafeKeyHandle();
      }
    }

    private SafeKeyHandle()
      : base(true)
    {
      this.SetHandle(IntPtr.Zero);
    }

    private SafeKeyHandle(IntPtr handle)
      : base(true)
    {
      this.SetHandle(handle);
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void FreeKey(IntPtr pKeyCotext);

    [SecurityCritical]
    protected override bool ReleaseHandle()
    {
      SafeKeyHandle.FreeKey(this.handle);
      return true;
    }
  }
}
