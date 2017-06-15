// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.Manifest.IMetadataSectionEntry
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("AB1ED79F-943E-407d-A80B-0744E3A95B28")]
  [ComImport]
  internal interface IMetadataSectionEntry
  {
    MetadataSectionEntry AllData { [SecurityCritical] get; }

    uint SchemaVersion { [SecurityCritical] get; }

    uint ManifestFlags { [SecurityCritical] get; }

    uint UsagePatterns { [SecurityCritical] get; }

    IDefinitionIdentity CdfIdentity { [SecurityCritical] get; }

    string LocalPath { [SecurityCritical] get; }

    uint HashAlgorithm { [SecurityCritical] get; }

    object ManifestHash { [SecurityCritical] get; }

    string ContentType { [SecurityCritical] get; }

    string RuntimeImageVersion { [SecurityCritical] get; }

    object MvidValue { [SecurityCritical] get; }

    IDescriptionMetadataEntry DescriptionData { [SecurityCritical] get; }

    IDeploymentMetadataEntry DeploymentData { [SecurityCritical] get; }

    IDependentOSMetadataEntry DependentOSData { [SecurityCritical] get; }

    string defaultPermissionSetID { [SecurityCritical] get; }

    string RequestedExecutionLevel { [SecurityCritical] get; }

    bool RequestedExecutionLevelUIAccess { [SecurityCritical] get; }

    IReferenceIdentity ResourceTypeResourcesDependency { [SecurityCritical] get; }

    IReferenceIdentity ResourceTypeManifestResourcesDependency { [SecurityCritical] get; }

    string KeyInfoElement { [SecurityCritical] get; }

    ICompatibleFrameworksMetadataEntry CompatibleFrameworksData { [SecurityCritical] get; }
  }
}
