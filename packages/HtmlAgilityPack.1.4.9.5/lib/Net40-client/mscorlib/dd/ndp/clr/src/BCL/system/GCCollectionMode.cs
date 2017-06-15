// Decompiled with JetBrains decompiler
// Type: System.GCCollectionMode
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  /// <summary>指定强制执行的垃圾回收的行为。</summary>
  /// <filterpriority>2</filterpriority>
  [__DynamicallyInvokable]
  [Serializable]
  public enum GCCollectionMode
  {
    [__DynamicallyInvokable] Default,
    [__DynamicallyInvokable] Forced,
    [__DynamicallyInvokable] Optimized,
  }
}
