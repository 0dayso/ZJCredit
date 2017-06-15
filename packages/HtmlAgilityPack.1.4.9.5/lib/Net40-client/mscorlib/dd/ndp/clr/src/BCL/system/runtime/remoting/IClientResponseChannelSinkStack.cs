// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.IClientResponseChannelSinkStack
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
  /// <summary>为异步消息响应解码过程中必须调用的客户端响应信道接收器堆栈提供堆栈功能。</summary>
  [ComVisible(true)]
  public interface IClientResponseChannelSinkStack
  {
    /// <summary>请求对当前接收器堆栈中接收器的方法调用进行异步处理。</summary>
    /// <param name="headers">从服务器响应流中检索到的标头。</param>
    /// <param name="stream">从传输接收器返回的流。</param>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">当前接收器堆栈为空。</exception>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    [SecurityCritical]
    void AsyncProcessResponse(ITransportHeaders headers, Stream stream);

    /// <summary>调度答复接收器上的指定答复消息。</summary>
    /// <param name="msg">要调度的 <see cref="T:System.Runtime.Remoting.Messaging.IMessage" />。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    [SecurityCritical]
    void DispatchReplyMessage(IMessage msg);

    /// <summary>调度答复接收器上的指定异常。</summary>
    /// <param name="e">要调度到服务器的异常。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    [SecurityCritical]
    void DispatchException(Exception e);
  }
}
