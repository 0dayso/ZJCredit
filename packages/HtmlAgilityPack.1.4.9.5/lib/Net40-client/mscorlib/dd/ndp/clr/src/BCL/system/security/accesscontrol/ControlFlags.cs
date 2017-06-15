// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.ControlFlags
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.AccessControl
{
  /// <summary>这些标志将影响安全性说明符的行为。</summary>
  [Flags]
  public enum ControlFlags
  {
    None = 0,
    OwnerDefaulted = 1,
    GroupDefaulted = 2,
    DiscretionaryAclPresent = 4,
    DiscretionaryAclDefaulted = 8,
    SystemAclPresent = 16,
    SystemAclDefaulted = 32,
    DiscretionaryAclUntrusted = 64,
    ServerSecurity = 128,
    DiscretionaryAclAutoInheritRequired = 256,
    SystemAclAutoInheritRequired = 512,
    DiscretionaryAclAutoInherited = 1024,
    SystemAclAutoInherited = 2048,
    DiscretionaryAclProtected = 4096,
    SystemAclProtected = 8192,
    RMControlValid = 16384,
    SelfRelative = 32768,
  }
}
