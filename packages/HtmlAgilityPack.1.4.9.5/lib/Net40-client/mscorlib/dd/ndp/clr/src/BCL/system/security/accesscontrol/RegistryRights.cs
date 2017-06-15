// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.RegistryRights
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.AccessControl
{
  /// <summary>指定能够应用于注册表对象的访问控制权限。</summary>
  [Flags]
  public enum RegistryRights
  {
    QueryValues = 1,
    SetValue = 2,
    CreateSubKey = 4,
    EnumerateSubKeys = 8,
    Notify = 16,
    CreateLink = 32,
    ExecuteKey = 131097,
    ReadKey = ExecuteKey,
    WriteKey = 131078,
    Delete = 65536,
    ReadPermissions = 131072,
    ChangePermissions = 262144,
    TakeOwnership = 524288,
    FullControl = TakeOwnership | ChangePermissions | ReadPermissions | Delete | CreateLink | Notify | EnumerateSubKeys | CreateSubKey | SetValue | QueryValues,
  }
}
