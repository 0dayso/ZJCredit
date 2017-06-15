// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.Manifest.IMuiResourceMapEntry
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("397927f5-10f2-4ecb-bfe1-3c264212a193")]
  [ComImport]
  internal interface IMuiResourceMapEntry
  {
    MuiResourceMapEntry AllData { [SecurityCritical] get; }

    object ResourceTypeIdInt { [SecurityCritical] get; }

    object ResourceTypeIdString { [SecurityCritical] get; }
  }
}
