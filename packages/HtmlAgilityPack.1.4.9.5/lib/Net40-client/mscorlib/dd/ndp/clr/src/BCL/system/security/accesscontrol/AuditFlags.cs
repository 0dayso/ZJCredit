// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.AuditFlags
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.AccessControl
{
  /// <summary>指定用于审核对可保护对象的访问尝试的条件。</summary>
  [Flags]
  public enum AuditFlags
  {
    None = 0,
    Success = 1,
    Failure = 2,
  }
}
