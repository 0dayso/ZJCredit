// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.Manifest.IPermissionSetEntry
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("EBE5A1ED-FEBC-42c4-A9E1-E087C6E36635")]
  [ComImport]
  internal interface IPermissionSetEntry
  {
    PermissionSetEntry AllData { [SecurityCritical] get; }

    string Id { [SecurityCritical] get; }

    string XmlSegment { [SecurityCritical] get; }
  }
}
