// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.SecurityInfos
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.AccessControl
{
  /// <summary>指定要查询或设置的安全性说明符的部分。</summary>
  [Flags]
  public enum SecurityInfos
  {
    Owner = 1,
    Group = 2,
    DiscretionaryAcl = 4,
    SystemAcl = 8,
  }
}
