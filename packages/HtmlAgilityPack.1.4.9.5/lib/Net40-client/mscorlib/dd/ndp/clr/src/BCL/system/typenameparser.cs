// Decompiled with JetBrains decompiler
// Type: System.SafeTypeNameParserHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using System.Security;

namespace System
{
  [SecurityCritical]
  internal class SafeTypeNameParserHandle : SafeHandleZeroOrMinusOneIsInvalid
  {
    public SafeTypeNameParserHandle()
      : base(true)
    {
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void _ReleaseTypeNameParser(IntPtr pTypeNameParser);

    [SecurityCritical]
    protected override bool ReleaseHandle()
    {
      SafeTypeNameParserHandle._ReleaseTypeNameParser(this.handle);
      this.handle = IntPtr.Zero;
      return true;
    }
  }
}
