// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.ExtensionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.CompilerServices
{
  /// <summary>指示某个方法为扩展方法，或某个类或程序集包含扩展方法。</summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method)]
  [__DynamicallyInvokable]
  public sealed class ExtensionAttribute : Attribute
  {
    /// <summary>初始化 <see cref="T:System.Runtime.CompilerServices.ExtensionAttribute" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public ExtensionAttribute()
    {
    }
  }
}
