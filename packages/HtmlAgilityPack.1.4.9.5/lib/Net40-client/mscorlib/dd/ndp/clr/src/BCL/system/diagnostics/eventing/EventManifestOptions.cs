// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.EventManifestOptions
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Tracing
{
  /// <summary>指定如何生成事件源的 ETW 清单。</summary>
  [Flags]
  [__DynamicallyInvokable]
  public enum EventManifestOptions
  {
    [__DynamicallyInvokable] None = 0,
    [__DynamicallyInvokable] Strict = 1,
    [__DynamicallyInvokable] AllCultures = 2,
    [__DynamicallyInvokable] OnlyIfNeededForRegistration = 4,
    [__DynamicallyInvokable] AllowEventSourceOverride = 8,
  }
}
