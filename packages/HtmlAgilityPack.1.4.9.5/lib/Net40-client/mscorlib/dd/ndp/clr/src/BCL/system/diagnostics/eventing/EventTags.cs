// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.EventTags
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Tracing
{
  /// <summary>指定活动开始和停止事件的跟踪。只应使用较低的 24 位。有关详细信息，请参阅 <see cref="T:System.Diagnostics.Tracing.EventSourceOptions" /> 和 <see cref="M:System.Diagnostics.Tracing.EventSource.Write(System.String,System.Diagnostics.Tracing.EventSourceOptions)" />。</summary>
  [Flags]
  [__DynamicallyInvokable]
  public enum EventTags
  {
    [__DynamicallyInvokable] None = 0,
  }
}
