// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.EventWaitHandleRights
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.AccessControl
{
  /// <summary>指定可应用于命名的系统事件对象的访问控制权限。</summary>
  [Flags]
  public enum EventWaitHandleRights
  {
    Modify = 2,
    Delete = 65536,
    ReadPermissions = 131072,
    ChangePermissions = 262144,
    TakeOwnership = 524288,
    Synchronize = 1048576,
    FullControl = 2031619,
  }
}
