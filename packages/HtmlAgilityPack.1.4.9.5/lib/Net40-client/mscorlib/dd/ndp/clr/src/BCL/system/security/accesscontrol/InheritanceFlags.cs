// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.InheritanceFlags
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.AccessControl
{
  /// <summary>继承标志指定访问控制项 (ACE) 的继承语义。</summary>
  [Flags]
  public enum InheritanceFlags
  {
    None = 0,
    ContainerInherit = 1,
    ObjectInherit = 2,
  }
}
