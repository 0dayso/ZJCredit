// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.RegistryPermissionAccess
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>指定允许的对注册表项和值的访问权。</summary>
  [Flags]
  [ComVisible(true)]
  [Serializable]
  public enum RegistryPermissionAccess
  {
    NoAccess = 0,
    Read = 1,
    Write = 2,
    Create = 4,
    AllAccess = Create | Write | Read,
  }
}
