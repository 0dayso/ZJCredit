// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.IServerChannelSink
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
  /// <summary>提供用于安全和传输接收器的方法。</summary>
  [ComVisible(true)]
  public interface IServerChannelSink : IChannelSinkBase
  {
    /// <summary>获取服务器接收器链中的下一个服务器信道接收器。</summary>
    /// <returns>服务器接收器链中的下一个服务器信道接收器。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有所要求的 <see cref="F:System.Security.Permissions.SecurityPermissionFlag.Infrastructure" /> 权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    IServerChannelSink NextChannelSink { [SecurityCritical] get; }

    /// <summary>请求从当前接收器处理消息。</summary>
    /// <returns>一个 <see cref="T:System.Runtime.Remoting.Channels.ServerProcessing" /> 状态值，该值提供有关如何处理消息的信息。</returns>
    /// <param name="sinkStack">调用了当前接收器的信道接收器的堆栈。</param>
    /// <param name="requestMsg">包含请求的消息。</param>
    /// <param name="requestHeaders">从来自客户端的传入消息中检索到的标头。</param>
    /// <param name="requestStream">需要进行处理并传递到反序列化接收器的流。</param>
    /// <param name="responseMsg">此方法返回时，包含持有响应消息的 <see cref="T:System.Runtime.Remoting.Messaging.IMessage" />。该参数未经初始化即被传递。</param>
    /// <param name="responseHeaders">当该方法返回时，包含一个 <see cref="T:System.Runtime.Remoting.Channels.ITransportHeaders" />，它持有要添加到将发往客户端的返回消息的标头。该参数未经初始化即被传递。</param>
    /// <param name="responseStream">当该方法返回时，包含将发回到传输接收器的 <see cref="T:System.IO.Stream" />。该参数未经初始化即被传递。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    [SecurityCritical]
    ServerProcessing ProcessMessage(IServerChannelSinkStack sinkStack, IMessage requestMsg, ITransportHeaders requestHeaders, Stream requestStream, out IMessage responseMsg, out ITransportHeaders responseHeaders, out Stream responseStream);

    /// <summary>请求从当前接收器对异步发送的方法调用的响应进行处理。</summary>
    /// <param name="sinkStack">将发回服务器传输接收器的接收器堆栈。</param>
    /// <param name="state">在请求端生成的、与此接收器关联的信息。</param>
    /// <param name="msg">响应消息。</param>
    /// <param name="headers">要添加到将发往客户端的返回消息的标头。</param>
    /// <param name="stream">将发回传输接收器的流。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    [SecurityCritical]
    void AsyncProcessResponse(IServerResponseChannelSinkStack sinkStack, object state, IMessage msg, ITransportHeaders headers, Stream stream);

    /// <summary>返回提供的响应消息将序列化到其上的 <see cref="T:System.IO.Stream" />。</summary>
    /// <returns>提供的响应消息将序列化到其上的 <see cref="T:System.IO.Stream" />。</returns>
    /// <param name="sinkStack">将发回服务器传输接收器的接收器堆栈。</param>
    /// <param name="state">已被该接收器推送到堆栈的状态。</param>
    /// <param name="msg">要序列化的响应消息。</param>
    /// <param name="headers">要放置在将发送到客户端的响应流中的标头。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    [SecurityCritical]
    Stream GetResponseStream(IServerResponseChannelSinkStack sinkStack, object state, IMessage msg, ITransportHeaders headers);
  }
}
