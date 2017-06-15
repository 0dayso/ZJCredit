// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.IWinRTClassActivator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  [Guid("86ddd2d7-ad80-44f6-a12e-63698b52825d")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  internal interface IWinRTClassActivator
  {
    [SecurityCritical]
    [return: MarshalAs((UnmanagedType) 0)]
    object ActivateInstance([MarshalAs((UnmanagedType) 0)] string activatableClassId);

    [SecurityCritical]
    IntPtr GetActivationFactory([MarshalAs((UnmanagedType) 0)] string activatableClassId, ref Guid iid);
  }
}
