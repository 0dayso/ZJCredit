// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.Manifest.ICOMServerEntry
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("3903B11B-FBE8-477c-825F-DB828B5FD174")]
  [ComImport]
  internal interface ICOMServerEntry
  {
    COMServerEntry AllData { [SecurityCritical] get; }

    Guid Clsid { [SecurityCritical] get; }

    uint Flags { [SecurityCritical] get; }

    Guid ConfiguredGuid { [SecurityCritical] get; }

    Guid ImplementedClsid { [SecurityCritical] get; }

    Guid TypeLibrary { [SecurityCritical] get; }

    uint ThreadingModel { [SecurityCritical] get; }

    string RuntimeVersion { [SecurityCritical] get; }

    string HostFile { [SecurityCritical] get; }
  }
}
