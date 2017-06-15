// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.DisposeSink
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Runtime.Remoting.Messaging
{
  internal class DisposeSink : IMessageSink
  {
    private IDisposable _iDis;
    private IMessageSink _replySink;

    public IMessageSink NextSink
    {
      [SecurityCritical] get
      {
        return this._replySink;
      }
    }

    internal DisposeSink(IDisposable iDis, IMessageSink replySink)
    {
      this._iDis = iDis;
      this._replySink = replySink;
    }

    [SecurityCritical]
    public virtual IMessage SyncProcessMessage(IMessage reqMsg)
    {
      IMessage message = (IMessage) null;
      try
      {
        if (this._replySink != null)
          message = this._replySink.SyncProcessMessage(reqMsg);
      }
      finally
      {
        this._iDis.Dispose();
      }
      return message;
    }

    [SecurityCritical]
    public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
    {
      throw new NotSupportedException();
    }
  }
}
