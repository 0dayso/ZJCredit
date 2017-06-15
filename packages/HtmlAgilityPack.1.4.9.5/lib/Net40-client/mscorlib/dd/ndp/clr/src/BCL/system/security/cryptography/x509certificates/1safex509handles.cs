// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.X509Certificates.SafeCertStoreHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32.SafeHandles;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

namespace System.Security.Cryptography.X509Certificates
{
  [SecurityCritical]
  internal sealed class SafeCertStoreHandle : SafeHandleZeroOrMinusOneIsInvalid
  {
    internal static SafeCertStoreHandle InvalidHandle
    {
      get
      {
        return new SafeCertStoreHandle(IntPtr.Zero);
      }
    }

    private SafeCertStoreHandle()
      : base(true)
    {
    }

    internal SafeCertStoreHandle(IntPtr handle)
      : base(true)
    {
      this.SetHandle(handle);
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void _FreeCertStoreContext(IntPtr hCertStore);

    [SecurityCritical]
    protected override bool ReleaseHandle()
    {
      SafeCertStoreHandle._FreeCertStoreContext(this.handle);
      return true;
    }
  }
}
