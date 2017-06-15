// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.IChannelSender
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
  /// <summary>为发送方信道提供所需的函数和属性。</summary>
  [ComVisible(true)]
  public interface IChannelSender : IChannel
  {
    /// <summary>返回将消息传送到指定 URL 或信道数据对象的信道消息接收器。</summary>
    /// <returns>将消息传送到指定 URL 或信道数据对象的信道消息接收器，或者如果该信道不能连接到给定的终结点，则为 null。</returns>
    /// <param name="url">新接收器将把消息传送到的 URL。可以为 null。</param>
    /// <param name="remoteChannelData">新接收器将把消息传送到的远程主机的信道数据对象。可以为 null。</param>
    /// <param name="objectURI">此方法返回时，包含新信道消息接收器的 URI，该信道消息接收器将消息传送到指定 URL 或信道数据对象。该参数未经初始化即被传递。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    [SecurityCritical]
    IMessageSink CreateMessageSink(string url, object remoteChannelData, out string objectURI);
  }
}
