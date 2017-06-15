// Decompiled with JetBrains decompiler
// Type: System.Threading.AutoResetEvent
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Threading
{
  /// <summary>通知正在等待的线程已发生事件。此类不能被继承。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public sealed class AutoResetEvent : EventWaitHandle
  {
    /// <summary>使用 Boolean 值（指示是否将初始状态设置为终止的）初始化 <see cref="T:System.Threading.AutoResetEvent" /> 类的新实例。</summary>
    /// <param name="initialState">若要将初始状态设置为终止，则为 true；若要将初始状态设置为非终止，则为 false。</param>
    [__DynamicallyInvokable]
    public AutoResetEvent(bool initialState)
      : base(initialState, EventResetMode.AutoReset)
    {
    }
  }
}
