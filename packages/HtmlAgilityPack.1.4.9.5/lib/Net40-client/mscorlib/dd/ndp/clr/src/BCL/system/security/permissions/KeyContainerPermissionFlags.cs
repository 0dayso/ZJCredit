// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.KeyContainerPermissionFlags
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>指定允许的密钥容器访问类型。</summary>
  [Flags]
  [ComVisible(true)]
  [Serializable]
  public enum KeyContainerPermissionFlags
  {
    NoFlags = 0,
    Create = 1,
    Open = 2,
    Delete = 4,
    Import = 16,
    Export = 32,
    Sign = 256,
    Decrypt = 512,
    ViewAcl = 4096,
    ChangeAcl = 8192,
    AllFlags = ChangeAcl | ViewAcl | Decrypt | Sign | Export | Import | Delete | Open | Create,
  }
}
