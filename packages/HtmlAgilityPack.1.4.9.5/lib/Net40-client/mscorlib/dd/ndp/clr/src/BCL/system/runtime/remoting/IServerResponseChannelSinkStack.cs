// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.IServerResponseChannelSinkStack
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
  /// <summary>为服务器响应信道接收器的堆栈提供堆栈功能。</summary>
  [ComVisible(true)]
  public interface IServerResponseChannelSinkStack
  {
    /// <summary>请求对当前接收器堆栈中接收器的方法调用进行异步处理。</summary>
    /// <param name="msg">响应消息。</param>
    /// <param name="headers">从服务器响应流中检索到的标头。</param>
    /// <param name="stream">从传输接收器返回的流。</param>
    [SecurityCritical]
    void AsyncProcessResponse(IMessage msg, ITransportHeaders headers, Stream stream);

    /// <summary>返回指定的消息要序列化到其上的 <see cref="T:System.IO.Stream" />。</summary>
    /// <returns>指定的消息要序列化到其上的 <see cref="T:System.IO.Stream" />。</returns>
    /// <param name="msg">要序列化到请求流上的消息。</param>
    /// <param name="headers">从服务器响应流中检索到的标头。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    [SecurityCritical]
    Stream GetResponseStream(IMessage msg, ITransportHeaders headers);
  }
}
