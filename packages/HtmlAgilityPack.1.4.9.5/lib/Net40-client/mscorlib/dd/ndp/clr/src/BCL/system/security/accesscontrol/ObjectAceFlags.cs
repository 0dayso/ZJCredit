// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.ObjectAceFlags
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.AccessControl
{
  /// <summary>指定访问控制项 (ACE) 的对象类型的存在性。</summary>
  [Flags]
  public enum ObjectAceFlags
  {
    None = 0,
    ObjectAceTypePresent = 1,
    InheritedObjectAceTypePresent = 2,
  }
}
