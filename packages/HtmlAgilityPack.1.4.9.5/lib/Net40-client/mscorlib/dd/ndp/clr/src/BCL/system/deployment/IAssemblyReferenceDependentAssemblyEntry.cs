// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.Manifest.IAssemblyReferenceDependentAssemblyEntry
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("C31FF59E-CD25-47b8-9EF3-CF4433EB97CC")]
  [ComImport]
  internal interface IAssemblyReferenceDependentAssemblyEntry
  {
    AssemblyReferenceDependentAssemblyEntry AllData { [SecurityCritical] get; }

    string Group { [SecurityCritical] get; }

    string Codebase { [SecurityCritical] get; }

    ulong Size { [SecurityCritical] get; }

    object HashValue { [SecurityCritical] get; }

    uint HashAlgorithm { [SecurityCritical] get; }

    uint Flags { [SecurityCritical] get; }

    string ResourceFallbackCulture { [SecurityCritical] get; }

    string Description { [SecurityCritical] get; }

    string SupportUrl { [SecurityCritical] get; }

    ISection HashElements { [SecurityCritical] get; }
  }
}
