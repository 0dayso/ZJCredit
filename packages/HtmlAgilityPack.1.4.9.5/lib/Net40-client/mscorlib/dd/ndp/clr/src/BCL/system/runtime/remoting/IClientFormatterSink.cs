// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.IClientFormatterSink
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Channels
{
  /// <summary>将接收器标记为将消息序列化为流的客户端格式化程序接收器。</summary>
  [ComVisible(true)]
  public interface IClientFormatterSink : IMessageSink, IClientChannelSink, IChannelSinkBase
  {
  }
}
