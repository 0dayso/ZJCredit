// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.EventCommand
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Tracing
{
  /// <summary>描述传递给 <see cref="M:System.Diagnostics.Tracing.EventSource.OnEventCommand(System.Diagnostics.Tracing.EventCommandEventArgs)" /> 恢复命令 (<see cref="P:System.Diagnostics.Tracing.EventCommandEventArgs.Command" /> 属性。</summary>
  [__DynamicallyInvokable]
  public enum EventCommand
  {
    [__DynamicallyInvokable] Disable = -3,
    [__DynamicallyInvokable] Enable = -2,
    [__DynamicallyInvokable] SendManifest = -1,
    [__DynamicallyInvokable] Update = 0,
  }
}
