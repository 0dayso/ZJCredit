// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.EventKeywords
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Tracing
{
  /// <summary>定义应用于事件的标准关键字。</summary>
  [Flags]
  [__DynamicallyInvokable]
  public enum EventKeywords : long
  {
    [__DynamicallyInvokable] None = 0,
    [__DynamicallyInvokable] All = -1,
    MicrosoftTelemetry = 562949953421312,
    [__DynamicallyInvokable] WdiContext = MicrosoftTelemetry,
    [__DynamicallyInvokable] WdiDiagnostic = 1125899906842624,
    [__DynamicallyInvokable] Sqm = 2251799813685248,
    [__DynamicallyInvokable] AuditFailure = 4503599627370496,
    [__DynamicallyInvokable] AuditSuccess = 9007199254740992,
    [__DynamicallyInvokable] CorrelationHint = AuditFailure,
    [__DynamicallyInvokable] EventLogClassic = 36028797018963968,
  }
}
