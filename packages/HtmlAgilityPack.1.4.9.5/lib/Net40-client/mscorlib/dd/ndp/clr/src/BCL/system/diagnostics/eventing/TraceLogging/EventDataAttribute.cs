// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.EventDataAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Tracing
{
  /// <summary>指定的类型传递给<see cref="M:System.Diagnostics.Tracing.EventSource.Write``1(System.String,System.Diagnostics.Tracing.EventSourceOptions,``0)" />方法。</summary>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
  [__DynamicallyInvokable]
  public class EventDataAttribute : Attribute
  {
    private EventLevel level = ~EventLevel.LogAlways;
    private EventOpcode opcode = ~EventOpcode.Info;

    /// <summary>如果未显式命名事件类型或属性，则获取或设置要应用于事件的名称。</summary>
    /// <returns>要应用于事件或属性的名称。</returns>
    [__DynamicallyInvokable]
    public string Name { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

    internal EventLevel Level
    {
      get
      {
        return this.level;
      }
      set
      {
        this.level = value;
      }
    }

    internal EventOpcode Opcode
    {
      get
      {
        return this.opcode;
      }
      set
      {
        this.opcode = value;
      }
    }

    internal EventKeywords Keywords { get; set; }

    internal EventTags Tags { get; set; }

    /// <summary>初始化 <see cref="T:System.Diagnostics.Tracing.EventDataAttribute" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public EventDataAttribute()
    {
    }
  }
}
