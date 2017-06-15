// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Contexts.SynchronizedServerContextSink
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
  internal class SynchronizedServerContextSink : InternalSink, IMessageSink
  {
    internal IMessageSink _nextSink;
    [SecurityCritical]
    internal SynchronizationAttribute _property;

    public IMessageSink NextSink
    {
      [SecurityCritical] get
      {
        return this._nextSink;
      }
    }

    [SecurityCritical]
    internal SynchronizedServerContextSink(SynchronizationAttribute prop, IMessageSink nextSink)
    {
      this._property = prop;
      this._nextSink = nextSink;
    }

    [SecuritySafeCritical]
    ~SynchronizedServerContextSink()
    {
      this._property.Dispose();
    }

    [SecurityCritical]
    public virtual IMessage SyncProcessMessage(IMessage reqMsg)
    {
      WorkItem work = new WorkItem(reqMsg, this._nextSink, (IMessageSink) null);
      this._property.HandleWorkRequest(work);
      return work.ReplyMessage;
    }

    [SecurityCritical]
    public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
    {
      WorkItem work = new WorkItem(reqMsg, this._nextSink, replySink);
      work.SetAsync();
      this._property.HandleWorkRequest(work);
      return (IMessageCtrl) null;
    }
  }
}
