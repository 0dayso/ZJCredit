// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.AceFlags
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.AccessControl
{
  /// <summary>指定访问控制项 (ACE) 的继承和审核行为。</summary>
  [Flags]
  public enum AceFlags : byte
  {
    None = 0,
    ObjectInherit = 1,
    ContainerInherit = 2,
    NoPropagateInherit = 4,
    InheritOnly = 8,
    Inherited = 16,
    SuccessfulAccess = 64,
    FailedAccess = 128,
    InheritanceFlags = InheritOnly | NoPropagateInherit | ContainerInherit | ObjectInherit,
    AuditFlags = FailedAccess | SuccessfulAccess,
  }
}
