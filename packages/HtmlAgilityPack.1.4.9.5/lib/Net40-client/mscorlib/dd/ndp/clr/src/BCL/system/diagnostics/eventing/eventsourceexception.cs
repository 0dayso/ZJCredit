// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.EventSourceException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Serialization;

namespace System.Diagnostics.Tracing
{
  /// <summary>在 Windows （ETW） 中追踪事件时发生错误时引发的异常。</summary>
  [__DynamicallyInvokable]
  [Serializable]
  public class EventSourceException : Exception
  {
    /// <summary>初始化 <see cref="T:System.Diagnostics.Tracing.EventSourceException" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public EventSourceException()
      : base(Environment.GetResourceString("EventSource_ListenerWriteFailure"))
    {
    }

    /// <summary>使用指定的错误消息初始化 <see cref="T:System.Diagnostics.Tracing.EventSourceException" /> 类的新实例。</summary>
    /// <param name="message">描述错误的消息。</param>
    [__DynamicallyInvokable]
    public EventSourceException(string message)
      : base(message)
    {
    }

    /// <summary>使用指定错误消息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.Diagnostics.Tracing.EventSourceException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="innerException">作为当前异常原因的异常，如果没有指定内部异常，则为 null。</param>
    [__DynamicallyInvokable]
    public EventSourceException(string message, Exception innerException)
      : base(message, innerException)
    {
    }

    /// <summary>用序列化数据初始化 <see cref="T:System.Diagnostics.Tracing.EventSourceException" /> 类的新实例。</summary>
    /// <param name="info">承载序列化对象数据的对象。</param>
    /// <param name="context">关于来源和目标的上下文信息</param>
    protected EventSourceException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    internal EventSourceException(Exception innerException)
      : base(Environment.GetResourceString("EventSource_ListenerWriteFailure"), innerException)
    {
    }
  }
}
