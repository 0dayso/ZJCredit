// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.SafeCspHashHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  [SecurityCritical]
  internal sealed class SafeCspHashHandle : SafeHandleZeroOrMinusOneIsInvalid
  {
    private SafeCspHashHandle()
      : base(true)
    {
    }

    [DllImport("advapi32")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool CryptDestroyHash(IntPtr hKey);

    [SecurityCritical]
    protected override bool ReleaseHandle()
    {
      return SafeCspHashHandle.CryptDestroyHash(this.handle);
    }
  }
}
