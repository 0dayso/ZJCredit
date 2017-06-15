// Decompiled with JetBrains decompiler
// Type: System.Security.Principal.WindowsBuiltInRole
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Principal
{
  /// <summary>指定要与 <see cref="M:System.Security.Principal.WindowsPrincipal.IsInRole(System.String)" /> 一起使用的公共角色。</summary>
  [ComVisible(true)]
  [Serializable]
  public enum WindowsBuiltInRole
  {
    Administrator = 544,
    User = 545,
    Guest = 546,
    PowerUser = 547,
    AccountOperator = 548,
    SystemOperator = 549,
    PrintOperator = 550,
    BackupOperator = 551,
    Replicator = 552,
  }
}
