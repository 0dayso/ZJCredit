// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Lifetime.LeaseSink
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Lifetime
{
  internal class LeaseSink : IMessageSink
  {
    private Lease lease;
    private IMessageSink nextSink;

    public IMessageSink NextSink
    {
      [SecurityCritical] get
      {
        return this.nextSink;
      }
    }

    public LeaseSink(Lease lease, IMessageSink nextSink)
    {
      this.lease = lease;
      this.nextSink = nextSink;
    }

    [SecurityCritical]
    public IMessage SyncProcessMessage(IMessage msg)
    {
      this.lease.RenewOnCall();
      return this.nextSink.SyncProcessMessage(msg);
    }

    [SecurityCritical]
    public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
    {
      this.lease.RenewOnCall();
      return this.nextSink.AsyncProcessMessage(msg, replySink);
    }
  }
}
