// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.SuppressIldasmAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.CompilerServices
{
  /// <summary>防止 Ildasm.exe（IL 反汇编程序） 反汇编程序集。此类不能被继承。</summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module)]
  public sealed class SuppressIldasmAttribute : Attribute
  {
  }
}
