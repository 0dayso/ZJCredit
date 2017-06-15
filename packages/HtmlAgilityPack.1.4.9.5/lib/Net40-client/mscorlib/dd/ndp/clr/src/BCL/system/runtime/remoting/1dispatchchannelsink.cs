// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.DispatchChannelSink
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
  internal class DispatchChannelSink : IServerChannelSink, IChannelSinkBase
  {
    public IServerChannelSink NextChannelSink
    {
      [SecurityCritical] get
      {
        return (IServerChannelSink) null;
      }
    }

    public IDictionary Properties
    {
      [SecurityCritical] get
      {
        return (IDictionary) null;
      }
    }

    internal DispatchChannelSink()
    {
    }

    [SecurityCritical]
    public ServerProcessing ProcessMessage(IServerChannelSinkStack sinkStack, IMessage requestMsg, ITransportHeaders requestHeaders, Stream requestStream, out IMessage responseMsg, out ITransportHeaders responseHeaders, out Stream responseStream)
    {
      if (requestMsg == null)
        throw new ArgumentNullException("requestMsg", Environment.GetResourceString("Remoting_Channel_DispatchSinkMessageMissing"));
      if (requestStream != null)
        throw new RemotingException(Environment.GetResourceString("Remoting_Channel_DispatchSinkWantsNullRequestStream"));
      responseHeaders = (ITransportHeaders) null;
      responseStream = (Stream) null;
      return ChannelServices.DispatchMessage(sinkStack, requestMsg, out responseMsg);
    }

    [SecurityCritical]
    public void AsyncProcessResponse(IServerResponseChannelSinkStack sinkStack, object state, IMessage msg, ITransportHeaders headers, Stream stream)
    {
      throw new NotSupportedException();
    }

    [SecurityCritical]
    public Stream GetResponseStream(IServerResponseChannelSinkStack sinkStack, object state, IMessage msg, ITransportHeaders headers)
    {
      throw new NotSupportedException();
    }
  }
}
