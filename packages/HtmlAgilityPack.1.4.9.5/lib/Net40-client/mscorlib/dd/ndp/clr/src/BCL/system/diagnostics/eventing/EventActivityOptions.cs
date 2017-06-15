// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.EventActivityOptions
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Tracing
{
  /// <summary>指定跟踪活动的启动和停止事件。</summary>
  [Flags]
  [__DynamicallyInvokable]
  public enum EventActivityOptions
  {
    [__DynamicallyInvokable] None = 0,
    [__DynamicallyInvokable] Disable = 2,
    [__DynamicallyInvokable] Recursive = 4,
    [__DynamicallyInvokable] Detachable = 8,
  }
}
