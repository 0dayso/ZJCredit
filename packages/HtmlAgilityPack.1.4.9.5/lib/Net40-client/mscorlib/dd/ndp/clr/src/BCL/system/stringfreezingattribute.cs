// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.StringFreezingAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.CompilerServices
{
  /// <summary>已否决。在使用 Ngen.exe（本机映像生成器） 创建本机映像时冻结字符串。此类不能被继承。</summary>
  [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
  [Serializable]
  public sealed class StringFreezingAttribute : Attribute
  {
  }
}
