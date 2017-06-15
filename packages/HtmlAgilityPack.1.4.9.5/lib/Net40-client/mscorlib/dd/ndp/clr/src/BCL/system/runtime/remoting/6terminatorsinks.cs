// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.ServerObjectTerminatorSink
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Remoting.Contexts;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
  [Serializable]
  internal class ServerObjectTerminatorSink : InternalSink, IMessageSink
  {
    internal StackBuilderSink _stackBuilderSink;

    public IMessageSink NextSink
    {
      [SecurityCritical] get
      {
        return (IMessageSink) null;
      }
    }

    internal ServerObjectTerminatorSink(MarshalByRefObject srvObj)
    {
      this._stackBuilderSink = new StackBuilderSink(srvObj);
    }

    [SecurityCritical]
    public virtual IMessage SyncProcessMessage(IMessage reqMsg)
    {
      IMessage message = InternalSink.ValidateMessage(reqMsg);
      if (message != null)
        return message;
      ArrayWithSize sideDynamicSinks = InternalSink.GetServerIdentity(reqMsg).ServerSideDynamicSinks;
      if (sideDynamicSinks != null)
        DynamicPropertyHolder.NotifyDynamicSinks(reqMsg, sideDynamicSinks, false, true, false);
      IMessageSink messageSink = this._stackBuilderSink.ServerObject as IMessageSink;
      IMessage msg = messageSink == null ? this._stackBuilderSink.SyncProcessMessage(reqMsg) : messageSink.SyncProcessMessage(reqMsg);
      if (sideDynamicSinks != null)
        DynamicPropertyHolder.NotifyDynamicSinks(msg, sideDynamicSinks, false, false, false);
      return msg;
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
      {
        IMessageSink messageSink = this._stackBuilderSink.ServerObject as IMessageSink;
        messageCtrl = messageSink == null ? this._stackBuilderSink.AsyncProcessMessage(reqMsg, replySink) : messageSink.AsyncProcessMessage(reqMsg, replySink);
      }
      return messageCtrl;
    }
  }
}
