// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.EventIgnoreAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Tracing
{
  /// <summary>指定在编写具有的事件类型时，应忽略属性<see cref="M:System.Diagnostics.Tracing.EventSource.Write``1(System.String,System.Diagnostics.Tracing.EventSourceOptions@,``0@)" />方法。</summary>
  [AttributeUsage(AttributeTargets.Property)]
  [__DynamicallyInvokable]
  public class EventIgnoreAttribute : Attribute
  {
    /// <summary>初始化 <see cref="T:System.Diagnostics.Tracing.EventIgnoreAttribute" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public EventIgnoreAttribute()
    {
    }
  }
}
