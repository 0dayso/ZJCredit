// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.EventChannel
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;

namespace System.Diagnostics.Tracing
{
  /// <summary>指定事件的事件日志通道。</summary>
  [FriendAccessAllowed]
  [__DynamicallyInvokable]
  public enum EventChannel : byte
  {
    [__DynamicallyInvokable] None = 0,
    [__DynamicallyInvokable] Admin = 16,
    [__DynamicallyInvokable] Operational = 17,
    [__DynamicallyInvokable] Analytic = 18,
    [__DynamicallyInvokable] Debug = 19,
  }
}
