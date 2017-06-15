// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.CallerLineNumberAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.CompilerServices
{
  /// <summary>允许您获取调用了方法的源文件的行号。</summary>
  [AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
  [__DynamicallyInvokable]
  public sealed class CallerLineNumberAttribute : Attribute
  {
    /// <summary>初始化 <see cref="T:System.Runtime.CompilerServices.CallerLineNumberAttribute" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public CallerLineNumberAttribute()
    {
    }
  }
}
