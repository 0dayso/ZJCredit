// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.EnvoyTerminatorSink
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
  [Serializable]
  internal class EnvoyTerminatorSink : InternalSink, IMessageSink
  {
    private static object staticSyncObject = new object();
    private static volatile EnvoyTerminatorSink messageSink;

    internal static IMessageSink MessageSink
    {
      get
      {
        if (EnvoyTerminatorSink.messageSink == null)
        {
          EnvoyTerminatorSink envoyTerminatorSink = new EnvoyTerminatorSink();
          lock (EnvoyTerminatorSink.staticSyncObject)
          {
            if (EnvoyTerminatorSink.messageSink == null)
              EnvoyTerminatorSink.messageSink = envoyTerminatorSink;
          }
        }
        return (IMessageSink) EnvoyTerminatorSink.messageSink;
      }
    }

    public IMessageSink NextSink
    {
      [SecurityCritical] get
      {
        return (IMessageSink) null;
      }
    }

    [SecurityCritical]
    public virtual IMessage SyncProcessMessage(IMessage reqMsg)
    {
      return InternalSink.ValidateMessage(reqMsg) ?? Thread.CurrentContext.GetClientContextChain().SyncProcessMessage(reqMsg);
    }

    [SecurityCritical]
    public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
    {
      IMessageCtrl messageCtrl = (IMessageCtrl) null;
      IMessage msg = InternalSink.ValidateMessage(reqMsg);
      if (msg != null)
      {
        if (replySink != null)
          replySink.SyncProcessMessage(msg);
      }
      else
        messageCtrl = Thread.CurrentContext.GetClientContextChain().AsyncProcessMessage(reqMsg, replySink);
      return messageCtrl;
    }
  }
}
