// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.RuntimeClass
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  internal abstract class RuntimeClass : __ComObject
  {
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal IntPtr GetRedirectedGetHashCodeMD();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal int RedirectGetHashCode(IntPtr pMD);

    [SecuritySafeCritical]
    public override int GetHashCode()
    {
      IntPtr redirectedGetHashCodeMd = this.GetRedirectedGetHashCodeMD();
      if (redirectedGetHashCodeMd == IntPtr.Zero)
        return base.GetHashCode();
      return this.RedirectGetHashCode(redirectedGetHashCodeMd);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal IntPtr GetRedirectedToStringMD();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal string RedirectToString(IntPtr pMD);

    [SecuritySafeCritical]
    public override string ToString()
    {
      IStringable stringable = this as IStringable;
      if (stringable != null)
        return stringable.ToString();
      IntPtr redirectedToStringMd = this.GetRedirectedToStringMD();
      if (redirectedToStringMd == IntPtr.Zero)
        return base.ToString();
      return this.RedirectToString(redirectedToStringMd);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal IntPtr GetRedirectedEqualsMD();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal bool RedirectEquals(object obj, IntPtr pMD);

    [SecuritySafeCritical]
    public override bool Equals(object obj)
    {
      IntPtr redirectedEqualsMd = this.GetRedirectedEqualsMD();
      if (redirectedEqualsMd == IntPtr.Zero)
        return base.Equals(obj);
      return this.RedirectEquals(obj, redirectedEqualsMd);
    }
  }
}
