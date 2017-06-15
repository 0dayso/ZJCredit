// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.PermissionState
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>指定权限在创建时是否对资源有所有访问权限或没有任何访问权限。</summary>
  [ComVisible(true)]
  [Serializable]
  public enum PermissionState
  {
    None,
    Unrestricted,
  }
}
