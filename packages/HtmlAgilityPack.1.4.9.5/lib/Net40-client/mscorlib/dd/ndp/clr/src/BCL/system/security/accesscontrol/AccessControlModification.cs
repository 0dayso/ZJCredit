// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.AccessControlModification
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.AccessControl
{
  /// <summary>指定要执行的访问控制修改的类型。此枚举由 <see cref="T:System.Security.AccessControl.ObjectSecurity" /> 类及其子类的方法使用。</summary>
  public enum AccessControlModification
  {
    Add,
    Set,
    Reset,
    Remove,
    RemoveAll,
    RemoveSpecific,
  }
}
