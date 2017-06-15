// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.NonEventAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Tracing
{
  /// <summary>是被不会形成事件的方法。</summary>
  [AttributeUsage(AttributeTargets.Method)]
  [__DynamicallyInvokable]
  public sealed class NonEventAttribute : Attribute
  {
    /// <summary>创建 <see cref="T:System.Diagnostics.Tracing.NonEventAttribute" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public NonEventAttribute()
    {
    }
  }
}
