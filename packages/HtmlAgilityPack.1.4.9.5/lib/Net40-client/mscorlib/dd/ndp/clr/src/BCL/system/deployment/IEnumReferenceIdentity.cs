﻿// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.IEnumReferenceIdentity
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
  [Guid("b30352cf-23da-4577-9b3f-b4e6573be53b")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  internal interface IEnumReferenceIdentity
  {
    [SecurityCritical]
    uint Next([In] uint celt, [MarshalAs(UnmanagedType.LPArray), Out] IReferenceIdentity[] ReferenceIdentity);

    [SecurityCritical]
    void Skip(uint celt);

    [SecurityCritical]
    void Reset();

    [SecurityCritical]
    IEnumReferenceIdentity Clone();
  }
}
