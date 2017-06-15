// Decompiled with JetBrains decompiler
// Type: System.Security.UnverifiableCodeAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security
{
  /// <summary>标记包含无法验证的代码的模块。此类不能被继承。</summary>
  [AttributeUsage(AttributeTargets.Module, AllowMultiple = true, Inherited = false)]
  [ComVisible(true)]
  public sealed class UnverifiableCodeAttribute : Attribute
  {
  }
}
