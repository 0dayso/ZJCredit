// Decompiled with JetBrains decompiler
// Type: System.Threading.SendOrPostCallback
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Threading
{
  /// <summary>表示在消息即将被调度到同步上下文时要调用的方法。</summary>
  /// <param name="state">传递给委托的对象。</param>
  /// <filterpriority>2</filterpriority>
  [__DynamicallyInvokable]
  public delegate void SendOrPostCallback(object state);
}
