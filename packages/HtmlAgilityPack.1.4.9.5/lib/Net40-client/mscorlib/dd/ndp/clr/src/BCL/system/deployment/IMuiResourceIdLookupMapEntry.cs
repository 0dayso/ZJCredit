﻿// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.Manifest.IMuiResourceIdLookupMapEntry
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("24abe1f7-a396-4a03-9adf-1d5b86a5569f")]
  [ComImport]
  internal interface IMuiResourceIdLookupMapEntry
  {
    MuiResourceIdLookupMapEntry AllData { [SecurityCritical] get; }

    uint Count { [SecurityCritical] get; }
  }
}
