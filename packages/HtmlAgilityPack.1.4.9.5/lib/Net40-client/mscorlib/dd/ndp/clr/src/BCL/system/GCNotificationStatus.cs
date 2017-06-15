// Decompiled with JetBrains decompiler
// Type: System.GCNotificationStatus
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  /// <summary>提供针对下一次完整垃圾回收通知的当前注册的相关信息。</summary>
  /// <filterpriority>2</filterpriority>
  [Serializable]
  public enum GCNotificationStatus
  {
    Succeeded,
    Failed,
    Canceled,
    Timeout,
    NotApplicable,
  }
}
