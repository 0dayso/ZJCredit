// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.AccessControlSections
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.AccessControl
{
  /// <summary>指定要保存或加载安全性说明符的哪些部分。</summary>
  [Flags]
  public enum AccessControlSections
  {
    None = 0,
    Audit = 1,
    Access = 2,
    Owner = 4,
    Group = 8,
    All = Group | Owner | Access | Audit,
  }
}
