// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.EventSourceOptions
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Tracing
{
  /// <summary>指定在调用 <see cref="M:System.Diagnostics.Tracing.EventSource.Write``1(System.String,System.Diagnostics.Tracing.EventSourceOptions,``0)" /> 方法时重写默认事件设置（如日志级别、关键字和操作代码）。</summary>
  [__DynamicallyInvokable]
  public struct EventSourceOptions
  {
    internal EventKeywords keywords;
    internal EventTags tags;
    internal EventActivityOptions activityOptions;
    internal byte level;
    internal byte opcode;
    internal byte valuesSet;
    internal const byte keywordsSet = 1;
    internal const byte tagsSet = 2;
    internal const byte levelSet = 4;
    internal const byte opcodeSet = 8;
    internal const byte activityOptionsSet = 16;

    /// <summary>获取或设置应用于事件的事件级别。</summary>
    /// <returns>事件的事件级别。如果未设置，则默认为 Verbose (5)。</returns>
    [__DynamicallyInvokable]
    public EventLevel Level
    {
      [__DynamicallyInvokable] get
      {
        return (EventLevel) this.level;
      }
      [__DynamicallyInvokable] set
      {
        this.level = checked ((byte) (uint) value);
        this.valuesSet = (byte) ((uint) this.valuesSet | 4U);
      }
    }

    /// <summary>获取或设置用于指定事件的操作代码。</summary>
    /// <returns>用于指定事件的操作代码。如果未设置，则默认为 Info (0)。</returns>
    [__DynamicallyInvokable]
    public EventOpcode Opcode
    {
      [__DynamicallyInvokable] get
      {
        return (EventOpcode) this.opcode;
      }
      [__DynamicallyInvokable] set
      {
        this.opcode = checked ((byte) (uint) value);
        this.valuesSet = (byte) ((uint) this.valuesSet | 8U);
      }
    }

    internal bool IsOpcodeSet
    {
      get
      {
        return ((uint) this.valuesSet & 8U) > 0U;
      }
    }

    /// <summary>获取或设置应用于事件的关键字。如果未设置此属性，则事件的关键字将为 None。</summary>
    /// <returns>应用于事件的关键字；如果未设置关键字，则为 None。</returns>
    [__DynamicallyInvokable]
    public EventKeywords Keywords
    {
      [__DynamicallyInvokable] get
      {
        return this.keywords;
      }
      [__DynamicallyInvokable] set
      {
        this.keywords = value;
        this.valuesSet = (byte) ((uint) this.valuesSet | 1U);
      }
    }

    /// <summary>为此事件源定义的事件标记。</summary>
    /// <returns>返回 <see cref="T:System.Diagnostics.Tracing.EventTags" />。</returns>
    [__DynamicallyInvokable]
    public EventTags Tags
    {
      [__DynamicallyInvokable] get
      {
        return this.tags;
      }
      [__DynamicallyInvokable] set
      {
        this.tags = value;
        this.valuesSet = (byte) ((uint) this.valuesSet | 2U);
      }
    }

    /// <summary>为此事件源定义的活动选项。</summary>
    /// <returns>返回 <see cref="T:System.Diagnostics.Tracing.EventActivityOptions" />。</returns>
    [__DynamicallyInvokable]
    public EventActivityOptions ActivityOptions
    {
      [__DynamicallyInvokable] get
      {
        return this.activityOptions;
      }
      [__DynamicallyInvokable] set
      {
        this.activityOptions = value;
        this.valuesSet = (byte) ((uint) this.valuesSet | 16U);
      }
    }
  }
}
