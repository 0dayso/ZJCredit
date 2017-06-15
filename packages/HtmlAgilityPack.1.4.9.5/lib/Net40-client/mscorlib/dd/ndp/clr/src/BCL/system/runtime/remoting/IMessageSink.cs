// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.IMessageSink
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
  /// <summary>定义消息接收器的接口。</summary>
  [ComVisible(true)]
  public interface IMessageSink
  {
    /// <summary>获取接收器链中的下一个消息接收器。</summary>
    /// <returns>接收器链中的下一个消息接收器。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方通过接口引用进行调用，它没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    IMessageSink NextSink { [SecurityCritical] get; }

    /// <summary>同步处理给定的消息。</summary>
    /// <returns>响应请求的答复消息。</returns>
    /// <param name="msg">要处理的消息。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方通过接口引用进行调用，它没有基础结构权限。</exception>
    [SecurityCritical]
    IMessage SyncProcessMessage(IMessage msg);

    /// <summary>异步处理给定的消息。</summary>
    /// <returns>返回 <see cref="T:System.Runtime.Remoting.Messaging.IMessageCtrl" /> 接口，该接口提供一种在调度异步消息之后控制这些消息的方法。</returns>
    /// <param name="msg">要处理的消息。</param>
    /// <param name="replySink">答复消息的答复接收器。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方通过接口引用进行调用，它没有基础结构权限。</exception>
    [SecurityCritical]
    IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink);
  }
}
