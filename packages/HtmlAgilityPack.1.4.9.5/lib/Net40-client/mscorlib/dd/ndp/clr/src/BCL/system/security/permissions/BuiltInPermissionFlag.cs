// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.BuiltInPermissionFlag
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.Permissions
{
  [Serializable]
  internal enum BuiltInPermissionFlag
  {
    EnvironmentPermission = 1,
    FileDialogPermission = 2,
    FileIOPermission = 4,
    IsolatedStorageFilePermission = 8,
    ReflectionPermission = 16,
    RegistryPermission = 32,
    SecurityPermission = 64,
    UIPermission = 128,
    PrincipalPermission = 256,
    PublisherIdentityPermission = 512,
    SiteIdentityPermission = 1024,
    StrongNameIdentityPermission = 2048,
    UrlIdentityPermission = 4096,
    ZoneIdentityPermission = 8192,
    KeyContainerPermission = 16384,
  }
}
