﻿// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.IStore_BindingResult_BoundVersion
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
  internal struct IStore_BindingResult_BoundVersion
  {
    [MarshalAs(UnmanagedType.U2)]
    public ushort Revision;
    [MarshalAs(UnmanagedType.U2)]
    public ushort Build;
    [MarshalAs(UnmanagedType.U2)]
    public ushort Minor;
    [MarshalAs(UnmanagedType.U2)]
    public ushort Major;
  }
}
