// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.Manifest.IDescriptionMetadataEntry
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("CB73147E-5FC2-4c31-B4E6-58D13DBE1A08")]
  [ComImport]
  internal interface IDescriptionMetadataEntry
  {
    DescriptionMetadataEntry AllData { [SecurityCritical] get; }

    string Publisher { [SecurityCritical] get; }

    string Product { [SecurityCritical] get; }

    string SupportUrl { [SecurityCritical] get; }

    string IconFile { [SecurityCritical] get; }

    string ErrorReportUrl { [SecurityCritical] get; }

    string SuiteName { [SecurityCritical] get; }
  }
}
