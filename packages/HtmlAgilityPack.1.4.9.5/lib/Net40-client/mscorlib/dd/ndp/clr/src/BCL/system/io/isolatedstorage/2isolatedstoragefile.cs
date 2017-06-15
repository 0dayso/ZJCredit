// Decompiled with JetBrains decompiler
// Type: System.IO.IsolatedStorage.SafeIsolatedStorageFileHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32.SafeHandles;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace System.IO.IsolatedStorage
{
  [SecurityCritical]
  internal sealed class SafeIsolatedStorageFileHandle : SafeHandleZeroOrMinusOneIsInvalid
  {
    private SafeIsolatedStorageFileHandle()
      : base(true)
    {
      this.SetHandle(IntPtr.Zero);
    }

    [SuppressUnmanagedCodeSecurity]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void Close(IntPtr file);

    [SecurityCritical]
    protected override bool ReleaseHandle()
    {
      SafeIsolatedStorageFileHandle.Close(this.handle);
      return true;
    }
  }
}
