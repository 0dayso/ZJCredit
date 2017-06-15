// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.EnvironmentPermissionAccess
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>指定可访问环境变量。</summary>
  [Flags]
  [ComVisible(true)]
  [Serializable]
  public enum EnvironmentPermissionAccess
  {
    NoAccess = 0,
    Read = 1,
    Write = 2,
    AllAccess = Write | Read,
  }
}
