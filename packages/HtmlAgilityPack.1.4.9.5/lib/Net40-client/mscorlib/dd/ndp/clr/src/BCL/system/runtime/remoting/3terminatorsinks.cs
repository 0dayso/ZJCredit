// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.AsyncReplySink
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Remoting.Contexts;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
  internal class AsyncReplySink : IMessageSink
  {
    private IMessageSink _replySink;
    private Context _cliCtx;

    public IMessageSink NextSink
    {
      [SecurityCritical] get
      {
        return this._replySink;
      }
    }

    internal AsyncReplySink(IMessageSink replySink, Context cliCtx)
    {
      this._replySink = replySink;
      this._cliCtx = cliCtx;
    }

    [SecurityCritical]
    internal static object SyncProcessMessageCallback(object[] args)
    {
      IMessage msg1 = (IMessage) args[0];
      IMessageSink messageSink = (IMessageSink) args[1];
      Thread.CurrentContext.NotifyDynamicSinks(msg1, true, false, true, true);
      IMessage msg2 = msg1;
      return (object) messageSink.SyncProcessMessage(msg2);
    }

    [SecurityCritical]
    public virtual IMessage SyncProcessMessage(IMessage reqMsg)
    {
      IMessage message = (IMessage) null;
      if (this._replySink != null)
        message = (IMessage) Thread.CurrentThread.InternalCrossContextCallback(this._cliCtx, new InternalCrossContextDelegate(AsyncReplySink.SyncProcessMessageCallback), new object[2]
        {
          (object) reqMsg,
          (object) this._replySink
        });
      return message;
    }

    [SecurityCritical]
    public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
    {
      throw new NotSupportedException();
    }
  }
}
