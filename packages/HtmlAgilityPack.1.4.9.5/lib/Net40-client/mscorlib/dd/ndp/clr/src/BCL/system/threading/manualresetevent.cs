// Decompiled with JetBrains decompiler
// Type: System.Threading.ManualResetEvent
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Threading
{
  /// <summary>通知一个或多个正在等待的线程已发生事件。此类不能被继承。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public sealed class ManualResetEvent : EventWaitHandle
  {
    /// <summary>用一个指示是否将初始状态设置为终止的布尔值初始化 <see cref="T:System.Threading.ManualResetEvent" /> 类的新实例。</summary>
    /// <param name="initialState">如果为 true，则将初始状态设置为终止；如果为 false，则将初始状态设置为非终止。</param>
    [__DynamicallyInvokable]
    public ManualResetEvent(bool initialState)
      : base(initialState, EventResetMode.ManualReset)
    {
    }
  }
}
