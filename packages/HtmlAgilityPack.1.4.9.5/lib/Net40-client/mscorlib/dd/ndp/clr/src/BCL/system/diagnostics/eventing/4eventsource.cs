// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.EventSourceAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Tracing
{
  /// <summary>允许 Windows (ETW) 名称事件追踪，要独立定义事件源类的名称。</summary>
  [AttributeUsage(AttributeTargets.Class)]
  [__DynamicallyInvokable]
  public sealed class EventSourceAttribute : Attribute
  {
    /// <summary>获取或设置事件源的名称。</summary>
    /// <returns>事件源的名称。</returns>
    [__DynamicallyInvokable]
    public string Name { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

    /// <summary>获取或设置事件源标识符。</summary>
    /// <returns>事件源标识符。</returns>
    [__DynamicallyInvokable]
    public string Guid { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

    /// <summary>获取或设置本地化资源文件的名称。</summary>
    /// <returns>本地化资源文件的名称或如果本地化资源文件不存在，则为 null。</returns>
    [__DynamicallyInvokable]
    public string LocalizationResources { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

    /// <summary>初始化 <see cref="T:System.Diagnostics.Tracing.EventSourceAttribute" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public EventSourceAttribute()
    {
    }
  }
}
