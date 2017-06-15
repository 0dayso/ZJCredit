// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.IClientChannelSink
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
  /// <summary>为客户端信道接收器提供所需的函数和属性。</summary>
  [ComVisible(true)]
  public interface IClientChannelSink : IChannelSinkBase
  {
    /// <summary>获取客户端接收器链中的下一个客户端信道接收器。</summary>
    /// <returns>客户端接收器链中的下一个客户端信道接收器。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    IClientChannelSink NextChannelSink { [SecurityCritical] get; }

    /// <summary>请求从当前接收器处理消息。</summary>
    /// <param name="msg">要处理的消息。</param>
    /// <param name="requestHeaders">要添加到发往服务器的传出消息的标头。</param>
    /// <param name="requestStream">发往传输接收器的流。</param>
    /// <param name="responseHeaders">当此方法返回时，包含 <see cref="T:System.Runtime.Remoting.Channels.ITransportHeaders" /> 接口，其中含有服务器返回的标头。该参数未经初始化即被传递。</param>
    /// <param name="responseStream">当此方法返回时，包含从传输接收器返回的 <see cref="T:System.IO.Stream" />。该参数未经初始化即被传递。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    [SecurityCritical]
    void ProcessMessage(IMessage msg, ITransportHeaders requestHeaders, Stream requestStream, out ITransportHeaders responseHeaders, out Stream responseStream);

    /// <summary>请求异步处理对当前接收器的方法调用。</summary>
    /// <param name="sinkStack">调用该接收器的信道接收器堆栈。</param>
    /// <param name="msg">要处理的消息。</param>
    /// <param name="headers">要添加到发往服务器的传出消息的标头。</param>
    /// <param name="stream">发往传输接收器的流。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    [SecurityCritical]
    void AsyncProcessRequest(IClientChannelSinkStack sinkStack, IMessage msg, ITransportHeaders headers, Stream stream);

    /// <summary>请求异步处理对当前接收器上的方法调用的响应。</summary>
    /// <param name="sinkStack">调用该接收器的接收器堆栈。</param>
    /// <param name="state">在请求端生成的、与此接收器关联的信息。</param>
    /// <param name="headers">从服务器响应流中检索到的标头。</param>
    /// <param name="stream">从传输接收器返回的流。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    [SecurityCritical]
    void AsyncProcessResponse(IClientResponseChannelSinkStack sinkStack, object state, ITransportHeaders headers, Stream stream);

    /// <summary>返回所提供的消息将要序列化到的 <see cref="T:System.IO.Stream" />。</summary>
    /// <returns>所提供的消息将要序列化到的 <see cref="T:System.IO.Stream" />。</returns>
    /// <param name="msg">
    /// <see cref="T:System.Runtime.Remoting.Messaging.IMethodCallMessage" />，包含有关方法调用的详细信息。</param>
    /// <param name="headers">要添加到发往服务器的传出消息的标头。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    [SecurityCritical]
    Stream GetRequestStream(IMessage msg, ITransportHeaders headers);
  }
}
