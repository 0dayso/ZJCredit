// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.ServerContextTerminatorSink
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Contexts;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
  [Serializable]
  internal class ServerContextTerminatorSink : InternalSink, IMessageSink
  {
    private static object staticSyncObject = new object();
    private static volatile ServerContextTerminatorSink messageSink;

    internal static IMessageSink MessageSink
    {
      get
      {
        if (ServerContextTerminatorSink.messageSink == null)
        {
          ServerContextTerminatorSink contextTerminatorSink = new ServerContextTerminatorSink();
          lock (ServerContextTerminatorSink.staticSyncObject)
          {
            if (ServerContextTerminatorSink.messageSink == null)
              ServerContextTerminatorSink.messageSink = contextTerminatorSink;
          }
        }
        return (IMessageSink) ServerContextTerminatorSink.messageSink;
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
      IMessage message1 = InternalSink.ValidateMessage(reqMsg);
      if (message1 != null)
        return message1;
      Context currentContext = Thread.CurrentContext;
      IMessage msg;
      if (reqMsg is IConstructionCallMessage)
      {
        IMessage message2 = currentContext.NotifyActivatorProperties(reqMsg, true);
        if (message2 != null)
          return message2;
        msg = (IMessage) ((IConstructionCallMessage) reqMsg).Activator.Activate((IConstructionCallMessage) reqMsg);
        IMessage message3 = currentContext.NotifyActivatorProperties(msg, true);
        if (message3 != null)
          return message3;
      }
      else
      {
        MarshalByRefObject marshalByRefObject = (MarshalByRefObject) null;
        try
        {
          msg = this.GetObjectChain(reqMsg, out marshalByRefObject).SyncProcessMessage(reqMsg);
        }
        finally
        {
          IDisposable disposable;
          if (marshalByRefObject != null && (disposable = marshalByRefObject as IDisposable) != null)
            disposable.Dispose();
        }
      }
      return msg;
    }

    [SecurityCritical]
    public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
    {
      IMessageCtrl messageCtrl = (IMessageCtrl) null;
      IMessage msg1 = InternalSink.ValidateMessage(reqMsg) ?? InternalSink.DisallowAsyncActivation(reqMsg);
      if (msg1 != null)
      {
        if (replySink != null)
          replySink.SyncProcessMessage(msg1);
      }
      else
      {
        MarshalByRefObject marshalByRefObject;
        IMessageSink objectChain = this.GetObjectChain(reqMsg, out marshalByRefObject);
        IDisposable iDis;
        if (marshalByRefObject != null && (iDis = marshalByRefObject as IDisposable) != null)
          replySink = (IMessageSink) new DisposeSink(iDis, replySink);
        IMessage msg2 = reqMsg;
        IMessageSink replySink1 = replySink;
        messageCtrl = objectChain.AsyncProcessMessage(msg2, replySink1);
      }
      return messageCtrl;
    }

    [SecurityCritical]
    internal virtual IMessageSink GetObjectChain(IMessage reqMsg, out MarshalByRefObject obj)
    {
      return InternalSink.GetServerIdentity(reqMsg).GetServerObjectChain(out obj);
    }
  }
}
