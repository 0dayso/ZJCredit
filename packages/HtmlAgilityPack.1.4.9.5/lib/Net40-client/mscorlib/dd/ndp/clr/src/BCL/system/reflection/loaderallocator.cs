// Decompiled with JetBrains decompiler
// Type: System.Reflection.LoaderAllocatorScout
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection
{
  internal sealed class LoaderAllocatorScout
  {
    internal IntPtr m_nativeLoaderAllocator;

    [SecuritySafeCritical]
    ~LoaderAllocatorScout()
    {
      if (this.m_nativeLoaderAllocator.IsNull() || Environment.HasShutdownStarted || (AppDomain.CurrentDomain.IsFinalizingForUnload() || LoaderAllocatorScout.Destroy(this.m_nativeLoaderAllocator)))
        return;
      GC.ReRegisterForFinalize((object) this);
    }

    [SuppressUnmanagedCodeSecurity]
    [SecurityCritical]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern bool Destroy(IntPtr nativeLoaderAllocator);
  }
}
