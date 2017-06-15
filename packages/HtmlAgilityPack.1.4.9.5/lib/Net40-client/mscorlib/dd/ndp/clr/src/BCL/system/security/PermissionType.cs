// Decompiled with JetBrains decompiler
// Type: System.Security.PermissionType
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security
{
  [Serializable]
  internal enum PermissionType
  {
    SecurityUnmngdCodeAccess = 0,
    SecuritySkipVerification = 1,
    ReflectionTypeInfo = 2,
    SecurityAssert = 3,
    ReflectionMemberAccess = 4,
    SecuritySerialization = 5,
    ReflectionRestrictedMemberAccess = 6,
    FullTrust = 7,
    SecurityBindingRedirects = 8,
    UIPermission = 9,
    EnvironmentPermission = 10,
    FileDialogPermission = 11,
    FileIOPermission = 12,
    ReflectionPermission = 13,
    SecurityPermission = 14,
    SecurityControlEvidence = 16,
    SecurityControlPrincipal = 17,
  }
}
