// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Contexts.WorkItem
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Contexts
{
  internal class WorkItem
  {
    internal static InternalCrossContextDelegate _xctxDel = new InternalCrossContextDelegate(WorkItem.ExecuteCallback);
    private const int FLG_WAITING = 1;
    private const int FLG_SIGNALED = 2;
    private const int FLG_ASYNC = 4;
    private const int FLG_DUMMY = 8;
    internal int _flags;
    internal IMessage _reqMsg;
    internal IMessageSink _nextSink;
    internal IMessageSink _replySink;
    internal IMessage _replyMsg;
    internal Context _ctx;
    [SecurityCritical]
    internal LogicalCallContext _callCtx;

    internal virtual IMessage ReplyMessage
    {
      get
      {
        return this._replyMsg;
      }
    }

    [SecuritySafeCritical]
    static WorkItem()
    {
    }

    [SecurityCritical]
    internal WorkItem(IMessage reqMsg, IMessageSink nextSink, IMessageSink replySink)
    {
      this._reqMsg = reqMsg;
      this._replyMsg = (IMessage) null;
      this._nextSink = nextSink;
      this._replySink = replySink;
      this._ctx = Thread.CurrentContext;
      this._callCtx = Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext;
    }

    internal virtual void SetWaiting()
    {
      this._flags = this._flags | 1;
    }

    internal virtual bool IsWaiting()
    {
      return (this._flags & 1) == 1;
    }

    internal virtual void SetSignaled()
    {
      this._flags = this._flags | 2;
    }

    internal virtual bool IsSignaled()
    {
      return (this._flags & 2) == 2;
    }

    internal virtual void SetAsync()
    {
      this._flags = this._flags | 4;
    }

    internal virtual bool IsAsync()
    {
      return (this._flags & 4) == 4;
    }

    internal virtual void SetDummy()
    {
      this._flags = this._flags | 8;
    }

    internal virtual bool IsDummy()
    {
      return (this._flags & 8) == 8;
    }

    [SecurityCritical]
    internal static object ExecuteCallback(object[] args)
    {
      WorkItem workItem1 = (WorkItem) args[0];
      if (workItem1.IsAsync())
        workItem1._nextSink.AsyncProcessMessage(workItem1._reqMsg, workItem1._replySink);
      else if (workItem1._nextSink != null)
      {
        WorkItem workItem2 = workItem1;
        IMessage message = workItem2._nextSink.SyncProcessMessage(workItem1._reqMsg);
        workItem2._replyMsg = message;
      }
      return (object) null;
    }

    [SecurityCritical]
    internal virtual void Execute()
    {
      Thread.CurrentThread.InternalCrossContextCallback(this._ctx, WorkItem._xctxDel, new object[1]{ (object) this });
    }
  }
}
