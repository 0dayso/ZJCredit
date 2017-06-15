// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.Manifest.IEntryPointEntry
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("1583EFE9-832F-4d08-B041-CAC5ACEDB948")]
  [ComImport]
  internal interface IEntryPointEntry
  {
    EntryPointEntry AllData { [SecurityCritical] get; }

    string Name { [SecurityCritical] get; }

    string CommandLine_File { [SecurityCritical] get; }

    string CommandLine_Parameters { [SecurityCritical] get; }

    IReferenceIdentity Identity { [SecurityCritical] get; }

    uint Flags { [SecurityCritical] get; }
  }
}
