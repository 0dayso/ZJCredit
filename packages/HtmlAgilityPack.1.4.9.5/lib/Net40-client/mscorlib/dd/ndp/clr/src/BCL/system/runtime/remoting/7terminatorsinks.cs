// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.ClientAsyncReplyTerminatorSink
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Runtime.Remoting.Messaging
{
  internal class ClientAsyncReplyTerminatorSink : IMessageSink
  {
    internal IMessageSink _nextSink;

    public IMessageSink NextSink
    {
      [SecurityCritical] get
      {
        return this._nextSink;
      }
    }

    internal ClientAsyncReplyTerminatorSink(IMessageSink nextSink)
    {
      this._nextSink = nextSink;
    }

    [SecurityCritical]
    public virtual IMessage SyncProcessMessage(IMessage replyMsg)
    {
      Guid id = Guid.Empty;
      if (RemotingServices.CORProfilerTrackRemotingCookie())
      {
        object obj = replyMsg.Properties[(object) "CORProfilerCookie"];
        if (obj != null)
          id = (Guid) obj;
      }
      RemotingServices.CORProfilerRemotingClientReceivingReply(id, true);
      return this._nextSink.SyncProcessMessage(replyMsg);
    }

    [SecurityCritical]
    public virtual IMessageCtrl AsyncProcessMessage(IMessage replyMsg, IMessageSink replySink)
    {
      return (IMessageCtrl) null;
    }
  }
}
