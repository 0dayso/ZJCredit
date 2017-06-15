// Decompiled with JetBrains decompiler
// Type: System.Threading.SafeCompressedStackHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Threading
{
  [SecurityCritical]
  internal class SafeCompressedStackHandle : SafeHandle
  {
    public override bool IsInvalid
    {
      [SecurityCritical] get
      {
        return this.handle == IntPtr.Zero;
      }
    }

    public SafeCompressedStackHandle()
      : base(IntPtr.Zero, true)
    {
    }

    [SecurityCritical]
    protected override bool ReleaseHandle()
    {
      CompressedStack.DestroyDelayedCompressedStack(this.handle);
      this.handle = IntPtr.Zero;
      return true;
    }
  }
}
