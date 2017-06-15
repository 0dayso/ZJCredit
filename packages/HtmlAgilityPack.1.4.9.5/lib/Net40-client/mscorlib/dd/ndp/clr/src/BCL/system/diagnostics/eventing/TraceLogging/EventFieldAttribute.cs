// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.EventFieldAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Tracing
{
  /// <summary>
  /// <see cref="T:System.Diagnostics.Tracing.EventFieldAttribute" />放置在作为传递的用户定义类型的字段上<see cref="T:System.Diagnostics.Tracing.EventSource" />负载。</summary>
  [AttributeUsage(AttributeTargets.Property)]
  [__DynamicallyInvokable]
  public class EventFieldAttribute : Attribute
  {
    /// <summary>获取和设置用户定义<see cref="T:System.Diagnostics.Tracing.EventFieldTags" />是所必需的字段包含不受支持的类型之一的数据的值。</summary>
    /// <returns>返回 <see cref="T:System.Diagnostics.Tracing.EventFieldTags" />。</returns>
    [__DynamicallyInvokable]
    public EventFieldTags Tags { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

    internal string Name { get; set; }

    /// <summary>获取和设置指定如何设置用户定义类型的值的格式的值。</summary>
    /// <returns>返回一个 <see cref="T:System.Diagnostics.Tracing.EventFieldFormat" /> 值。</returns>
    [__DynamicallyInvokable]
    public EventFieldFormat Format { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

    /// <summary>初始化 <see cref="T:System.Diagnostics.Tracing.EventFieldAttribute" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public EventFieldAttribute()
    {
    }
  }
}
