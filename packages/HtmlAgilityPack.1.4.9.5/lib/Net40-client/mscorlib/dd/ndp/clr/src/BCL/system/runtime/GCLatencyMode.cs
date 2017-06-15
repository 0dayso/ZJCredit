// Decompiled with JetBrains decompiler
// Type: System.Runtime.GCLatencyMode
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime
{
  /// <summary>调整垃圾收集器侵入应用程序的时间。  </summary>
  [__DynamicallyInvokable]
  [Serializable]
  public enum GCLatencyMode
  {
    [__DynamicallyInvokable] Batch,
    [__DynamicallyInvokable] Interactive,
    [__DynamicallyInvokable] LowLatency,
    [__DynamicallyInvokable] SustainedLowLatency,
    NoGCRegion,
  }
}
