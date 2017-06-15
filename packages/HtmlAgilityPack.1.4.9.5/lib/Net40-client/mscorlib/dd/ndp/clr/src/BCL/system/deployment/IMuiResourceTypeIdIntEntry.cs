// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.Manifest.IMuiResourceTypeIdIntEntry
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("55b2dec1-d0f6-4bf4-91b1-30f73ad8e4df")]
  [ComImport]
  internal interface IMuiResourceTypeIdIntEntry
  {
    MuiResourceTypeIdIntEntry AllData { [SecurityCritical] get; }

    object StringIds { [SecurityCritical] get; }

    object IntegerIds { [SecurityCritical] get; }
  }
}
