﻿// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.IManifestInformation
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
  [Guid("81c85208-fe61-4c15-b5bb-ff5ea66baad9")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  internal interface IManifestInformation
  {
    [SecurityCritical]
    void get_FullPath([MarshalAs(UnmanagedType.LPWStr)] out string FullPath);
  }
}
