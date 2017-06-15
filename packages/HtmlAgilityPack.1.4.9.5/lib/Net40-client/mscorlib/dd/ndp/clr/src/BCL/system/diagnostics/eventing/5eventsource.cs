// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.EventAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Tracing
{
  /// <summary>指定事件的附加事件架构信息。</summary>
  [AttributeUsage(AttributeTargets.Method)]
  [__DynamicallyInvokable]
  public sealed class EventAttribute : Attribute
  {
    private EventOpcode m_opcode;
    private bool m_opcodeSet;

    /// <summary>获取或设置事件的标识符。</summary>
    /// <returns>事件标识符。该值应介于 0 到 65535 之间。</returns>
    [__DynamicallyInvokable]
    public int EventId { [__DynamicallyInvokable] get; private set; }

    /// <summary>获取或设置事件的级别。</summary>
    /// <returns>指定事件级别的枚举值之一。</returns>
    [__DynamicallyInvokable]
    public EventLevel Level { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

    /// <summary>获取或设置事件的关键字。</summary>
    /// <returns>枚举值的按位组合。</returns>
    [__DynamicallyInvokable]
    public EventKeywords Keywords { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

    /// <summary>获取或设置事件的操作代码。</summary>
    /// <returns>用于指定操作代码的枚举值之一。</returns>
    [__DynamicallyInvokable]
    public EventOpcode Opcode
    {
      [__DynamicallyInvokable] get
      {
        return this.m_opcode;
      }
      [__DynamicallyInvokable] set
      {
        this.m_opcode = value;
        this.m_opcodeSet = true;
      }
    }

    internal bool IsOpcodeSet
    {
      get
      {
        return this.m_opcodeSet;
      }
    }

    /// <summary>获取或设置事件的任务。</summary>
    /// <returns>事件的任务。</returns>
    [__DynamicallyInvokable]
    public EventTask Task { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

    /// <summary>获取或设置应在其中写入事件的附加事件日志。</summary>
    /// <returns>应在其中写入事件的附加事件日志。</returns>
    [__DynamicallyInvokable]
    public EventChannel Channel { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

    /// <summary>获取或设置事件的版本。</summary>
    /// <returns>事件的版本。</returns>
    [__DynamicallyInvokable]
    public byte Version { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

    /// <summary>获取或设置事件的消息。</summary>
    /// <returns>事件的消息。</returns>
    [__DynamicallyInvokable]
    public string Message { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

    /// <summary>获取和设置<see cref="T:System.Diagnostics.Tracing.EventTags" />为此值<see cref="T:System.Diagnostics.Tracing.EventAttribute" />对象。事件标记是在记录事件时传递的用户定义值。</summary>
    /// <returns>返回 <see cref="T:System.Diagnostics.Tracing.EventTags" /> 值。</returns>
    [__DynamicallyInvokable]
    public EventTags Tags { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

    /// <summary>指定活动开始和停止事件的行为。活动是应用中开始与停止之间的时间区域。</summary>
    /// <returns>返回 <see cref="T:System.Diagnostics.Tracing.EventActivityOptions" />。</returns>
    [__DynamicallyInvokable]
    public EventActivityOptions ActivityOptions { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

    /// <summary>使用指定的事件标识符初始化 <see cref="T:System.Diagnostics.Tracing.EventAttribute" /> 类的新实例。</summary>
    /// <param name="eventId">该事件的事件标识符。</param>
    [__DynamicallyInvokable]
    public EventAttribute(int eventId)
    {
      this.EventId = eventId;
      this.Level = EventLevel.Informational;
      this.m_opcodeSet = false;
    }
  }
}
