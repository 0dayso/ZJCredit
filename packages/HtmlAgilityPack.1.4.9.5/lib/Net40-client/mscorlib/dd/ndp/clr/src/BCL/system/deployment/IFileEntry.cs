// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.Manifest.IFileEntry
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("A2A55FAD-349B-469b-BF12-ADC33D14A937")]
  [ComImport]
  internal interface IFileEntry
  {
    FileEntry AllData { [SecurityCritical] get; }

    string Name { [SecurityCritical] get; }

    uint HashAlgorithm { [SecurityCritical] get; }

    string LoadFrom { [SecurityCritical] get; }

    string SourcePath { [SecurityCritical] get; }

    string ImportPath { [SecurityCritical] get; }

    string SourceName { [SecurityCritical] get; }

    string Location { [SecurityCritical] get; }

    object HashValue { [SecurityCritical] get; }

    ulong Size { [SecurityCritical] get; }

    string Group { [SecurityCritical] get; }

    uint Flags { [SecurityCritical] get; }

    IMuiResourceMapEntry MuiMapping { [SecurityCritical] get; }

    uint WritableType { [SecurityCritical] get; }

    ISection HashElements { [SecurityCritical] get; }
  }
}
