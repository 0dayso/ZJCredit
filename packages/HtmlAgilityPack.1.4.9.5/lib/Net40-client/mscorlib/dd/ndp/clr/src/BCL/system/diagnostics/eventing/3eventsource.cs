// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.EventWrittenEventArgs
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Security;

namespace System.Diagnostics.Tracing
{
  /// <summary>为 <see cref="M:System.Diagnostics.Tracing.EventListener.OnEventWritten(System.Diagnostics.Tracing.EventWrittenEventArgs)" /> 回调提供数据。</summary>
  [__DynamicallyInvokable]
  public class EventWrittenEventArgs : EventArgs
  {
    private string m_message;
    private string m_eventName;
    private EventSource m_eventSource;
    private ReadOnlyCollection<string> m_payloadNames;
    internal EventTags m_tags;
    internal EventOpcode m_opcode;
    internal EventKeywords m_keywords;

    /// <summary>获取事件的名称。</summary>
    /// <returns>事件的名称。</returns>
    [__DynamicallyInvokable]
    public string EventName
    {
      [__DynamicallyInvokable] get
      {
        if (this.m_eventName != null || this.EventId < 0)
          return this.m_eventName;
        return this.m_eventSource.m_eventData[this.EventId].Name;
      }
      internal set
      {
        this.m_eventName = value;
      }
    }

    /// <summary>获取事件标识符。</summary>
    /// <returns>事件标识符。</returns>
    [__DynamicallyInvokable]
    public int EventId { [__DynamicallyInvokable] get; internal set; }

    /// <summary>[在 .NET Framework 4.5.1 和更高版本中受支持] 获取向其写入了事件的线程上的活动 ID。</summary>
    /// <returns>向其写入了事件的线程上的活动 ID。</returns>
    [__DynamicallyInvokable]
    public Guid ActivityId
    {
      [SecurityCritical, __DynamicallyInvokable] get
      {
        return EventSource.CurrentThreadActivityId;
      }
    }

    /// <summary>[在 .NET Framework 4.5.1 和更高版本中受支持] 获取与当前实例表示的活动相关的活动的标识符。</summary>
    /// <returns>相关活动的标识符或 <see cref="F:System.Guid.Empty" />（如果没有相关活动）。</returns>
    [__DynamicallyInvokable]
    public Guid RelatedActivityId { [SecurityCritical, __DynamicallyInvokable] get; internal set; }

    /// <summary>获取事件的负载。</summary>
    /// <returns>事件的负载。</returns>
    [__DynamicallyInvokable]
    public ReadOnlyCollection<object> Payload { [__DynamicallyInvokable] get; internal set; }

    /// <summary>返回表示事件的属性名称的字符串的列表。</summary>
    /// <returns>返回 <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" />。</returns>
    [__DynamicallyInvokable]
    public ReadOnlyCollection<string> PayloadNames
    {
      [__DynamicallyInvokable] get
      {
        if (this.m_payloadNames == null)
        {
          List<string> stringList = new List<string>();
          foreach (ParameterInfo parameter in this.m_eventSource.m_eventData[this.EventId].Parameters)
            stringList.Add(parameter.Name);
          this.m_payloadNames = new ReadOnlyCollection<string>((IList<string>) stringList);
        }
        return this.m_payloadNames;
      }
      internal set
      {
        this.m_payloadNames = value;
      }
    }

    /// <summary>获取事件源对象。</summary>
    /// <returns>事件源对象。</returns>
    [__DynamicallyInvokable]
    public EventSource EventSource
    {
      [__DynamicallyInvokable] get
      {
        return this.m_eventSource;
      }
    }

    /// <summary>获取事件的关键字。</summary>
    /// <returns>事件的关键字。</returns>
    [__DynamicallyInvokable]
    public EventKeywords Keywords
    {
      [__DynamicallyInvokable] get
      {
        if (this.EventId < 0)
          return this.m_keywords;
        return (EventKeywords) this.m_eventSource.m_eventData[this.EventId].Descriptor.Keywords;
      }
    }

    /// <summary>获取事件的操作代码。</summary>
    /// <returns>事件的操作代码。</returns>
    [__DynamicallyInvokable]
    public EventOpcode Opcode
    {
      [__DynamicallyInvokable] get
      {
        if (this.EventId < 0)
          return this.m_opcode;
        return (EventOpcode) this.m_eventSource.m_eventData[this.EventId].Descriptor.Opcode;
      }
    }

    /// <summary>获取事件的任务。</summary>
    /// <returns>事件的任务。</returns>
    [__DynamicallyInvokable]
    public EventTask Task
    {
      [__DynamicallyInvokable] get
      {
        if (this.EventId < 0)
          return EventTask.None;
        return (EventTask) this.m_eventSource.m_eventData[this.EventId].Descriptor.Task;
      }
    }

    /// <summary>返回在 <see cref="M:System.Diagnostics.Tracing.EventSource.Write(System.String,System.Diagnostics.Tracing.EventSourceOptions)" /> 方法调用中指定的标记。</summary>
    /// <returns>返回 <see cref="T:System.Diagnostics.Tracing.EventTags" />。</returns>
    [__DynamicallyInvokable]
    public EventTags Tags
    {
      [__DynamicallyInvokable] get
      {
        if (this.EventId < 0)
          return this.m_tags;
        return this.m_eventSource.m_eventData[this.EventId].Tags;
      }
    }

    /// <summary>获取事件的消息。</summary>
    /// <returns>事件的消息。</returns>
    [__DynamicallyInvokable]
    public string Message
    {
      [__DynamicallyInvokable] get
      {
        if (this.EventId < 0)
          return this.m_message;
        return this.m_eventSource.m_eventData[this.EventId].Message;
      }
      internal set
      {
        this.m_message = value;
      }
    }

    /// <summary>获取事件的通道。</summary>
    /// <returns>事件的通道。</returns>
    [__DynamicallyInvokable]
    public EventChannel Channel
    {
      [__DynamicallyInvokable] get
      {
        if (this.EventId < 0)
          return EventChannel.None;
        return (EventChannel) this.m_eventSource.m_eventData[this.EventId].Descriptor.Channel;
      }
    }

    /// <summary>获取事件的版本。</summary>
    /// <returns>事件的版本。</returns>
    [__DynamicallyInvokable]
    public byte Version
    {
      [__DynamicallyInvokable] get
      {
        if (this.EventId < 0)
          return 0;
        return this.m_eventSource.m_eventData[this.EventId].Descriptor.Version;
      }
    }

    /// <summary>获取事件的级别。</summary>
    /// <returns>事件级别。</returns>
    [__DynamicallyInvokable]
    public EventLevel Level
    {
      [__DynamicallyInvokable] get
      {
        if (this.EventId < 0)
          return EventLevel.LogAlways;
        return (EventLevel) this.m_eventSource.m_eventData[this.EventId].Descriptor.Level;
      }
    }

    internal EventWrittenEventArgs(EventSource eventSource)
    {
      this.m_eventSource = eventSource;
    }
  }
}
