﻿// Decompiled with JetBrains decompiler
// Type: System.StubHelpers.MngdRefCustomMarshaler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

namespace System.StubHelpers
{
  [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
  internal static class MngdRefCustomMarshaler
  {
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void CreateMarshaler(IntPtr pMarshalState, IntPtr pCMHelper);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void ConvertContentsToNative(IntPtr pMarshalState, ref object pManagedHome, IntPtr pNativeHome);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void ConvertContentsToManaged(IntPtr pMarshalState, ref object pManagedHome, IntPtr pNativeHome);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void ClearNative(IntPtr pMarshalState, ref object pManagedHome, IntPtr pNativeHome);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void ClearManaged(IntPtr pMarshalState, ref object pManagedHome, IntPtr pNativeHome);
  }
}
