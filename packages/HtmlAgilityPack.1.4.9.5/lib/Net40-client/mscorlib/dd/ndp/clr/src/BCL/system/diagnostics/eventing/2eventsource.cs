// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.EventCommandEventArgs
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;

namespace System.Diagnostics.Tracing
{
  /// <summary>提供 <see cref="M:System.Diagnostics.Tracing.EventSource.OnEventCommand(System.Diagnostics.Tracing.EventCommandEventArgs)" /> 回调的参数。</summary>
  [__DynamicallyInvokable]
  public class EventCommandEventArgs : EventArgs
  {
    internal EventSource eventSource;
    internal EventDispatcher dispatcher;
    internal EventListener listener;
    internal int perEventSourceSessionId;
    internal int etwSessionId;
    internal bool enable;
    internal EventLevel level;
    internal EventKeywords matchAnyKeyword;
    internal EventCommandEventArgs nextCommand;

    /// <summary>获取回调的命令。</summary>
    /// <returns>回调命令。</returns>
    [__DynamicallyInvokable]
    public EventCommand Command { [__DynamicallyInvokable] get; internal set; }

    /// <summary>获取回调的参数数组。</summary>
    /// <returns>回调参数数组。</returns>
    [__DynamicallyInvokable]
    public IDictionary<string, string> Arguments { [__DynamicallyInvokable] get; internal set; }

    internal EventCommandEventArgs(EventCommand command, IDictionary<string, string> arguments, EventSource eventSource, EventListener listener, int perEventSourceSessionId, int etwSessionId, bool enable, EventLevel level, EventKeywords matchAnyKeyword)
    {
      this.Command = command;
      this.Arguments = arguments;
      this.eventSource = eventSource;
      this.listener = listener;
      this.perEventSourceSessionId = perEventSourceSessionId;
      this.etwSessionId = etwSessionId;
      this.enable = enable;
      this.level = level;
      this.matchAnyKeyword = matchAnyKeyword;
    }

    /// <summary>启用有指定标识符的事件。</summary>
    /// <returns>如果 <paramref name="eventId" /> 在范围中，则为 true；否则为 false。</returns>
    /// <param name="eventId">启用事件的标识符。</param>
    [__DynamicallyInvokable]
    public bool EnableEvent(int eventId)
    {
      if (this.Command != EventCommand.Enable && this.Command != EventCommand.Disable)
        throw new InvalidOperationException();
      return this.eventSource.EnableEventForDispatcher(this.dispatcher, eventId, true);
    }

    /// <summary>禁用有指定标识符的事件。</summary>
    /// <returns>如果 <paramref name="eventId" /> 在范围中，则为 true；否则为 false。</returns>
    /// <param name="eventId">禁用事件的标识符。</param>
    [__DynamicallyInvokable]
    public bool DisableEvent(int eventId)
    {
      if (this.Command != EventCommand.Enable && this.Command != EventCommand.Disable)
        throw new InvalidOperationException();
      return this.eventSource.EnableEventForDispatcher(this.dispatcher, eventId, false);
    }
  }
}
