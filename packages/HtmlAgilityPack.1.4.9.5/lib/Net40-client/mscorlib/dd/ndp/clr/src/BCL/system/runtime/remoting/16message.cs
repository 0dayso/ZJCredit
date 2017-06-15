// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.TransitionCall
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
  [Serializable]
  internal class TransitionCall : IMessage, IInternalMessage, IMessageSink, ISerializable
  {
    private IDictionary _props;
    private IntPtr _sourceCtxID;
    private IntPtr _targetCtxID;
    private int _targetDomainID;
    private ServerIdentity _srvID;
    private Identity _ID;
    private CrossContextDelegate _delegate;
    private IntPtr _eeData;

    public IDictionary Properties
    {
      [SecurityCritical] get
      {
        if (this._props == null)
        {
          lock (this)
          {
            if (this._props == null)
              this._props = (IDictionary) new Hashtable();
          }
        }
        return this._props;
      }
    }

    ServerIdentity IInternalMessage.ServerIdentityObject
    {
      [SecurityCritical] get
      {
        if (this._targetDomainID != 0 && this._srvID == null)
        {
          lock (this)
          {
            if (Thread.GetContextInternal(this._targetCtxID) == null)
            {
              Context temp_25 = Context.DefaultContext;
            }
            this._srvID = new ServerIdentity((MarshalByRefObject) null, Thread.GetContextInternal(this._targetCtxID));
            this._srvID.RaceSetServerObjectChain((IMessageSink) this);
          }
        }
        return this._srvID;
      }
      [SecurityCritical] set
      {
        throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
      }
    }

    Identity IInternalMessage.IdentityObject
    {
      [SecurityCritical] get
      {
        return this._ID;
      }
      [SecurityCritical] set
      {
        throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
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
    internal TransitionCall(IntPtr targetCtxID, CrossContextDelegate deleg)
    {
      this._sourceCtxID = Thread.CurrentContext.InternalContextID;
      this._targetCtxID = targetCtxID;
      this._delegate = deleg;
      this._targetDomainID = 0;
      this._eeData = IntPtr.Zero;
      this._srvID = new ServerIdentity((MarshalByRefObject) null, Thread.GetContextInternal(this._targetCtxID));
      this._ID = (Identity) this._srvID;
      this._ID.RaceSetChannelSink(CrossContextChannel.MessageSink);
      this._srvID.RaceSetServerObjectChain((IMessageSink) this);
    }

    [SecurityCritical]
    internal TransitionCall(IntPtr targetCtxID, IntPtr eeData, int targetDomainID)
    {
      this._sourceCtxID = Thread.CurrentContext.InternalContextID;
      this._targetCtxID = targetCtxID;
      this._delegate = (CrossContextDelegate) null;
      this._targetDomainID = targetDomainID;
      this._eeData = eeData;
      this._srvID = (ServerIdentity) null;
      this._ID = new Identity("TransitionCallURI", (string) null);
      string objectURI;
      this._ID.RaceSetChannelSink(CrossAppDomainChannel.AppDomainChannel.CreateMessageSink((string) null, (object) new CrossAppDomainData(this._targetCtxID, this._targetDomainID, Identity.ProcessGuid), out objectURI));
    }

    internal TransitionCall(SerializationInfo info, StreamingContext context)
    {
      if (info == null || context.State != StreamingContextStates.CrossAppDomain)
        throw new ArgumentNullException("info");
      this._props = (IDictionary) info.GetValue("props", typeof (IDictionary));
      this._delegate = (CrossContextDelegate) info.GetValue("delegate", typeof (CrossContextDelegate));
      this._sourceCtxID = (IntPtr) info.GetValue("sourceCtxID", typeof (IntPtr));
      this._targetCtxID = (IntPtr) info.GetValue("targetCtxID", typeof (IntPtr));
      this._eeData = (IntPtr) info.GetValue("eeData", typeof (IntPtr));
      this._targetDomainID = info.GetInt32("targetDomainID");
    }

    [SecurityCritical]
    void IInternalMessage.SetURI(string uri)
    {
      throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
    }

    [SecurityCritical]
    void IInternalMessage.SetCallContext(LogicalCallContext callContext)
    {
      throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
    }

    [SecurityCritical]
    bool IInternalMessage.HasProperties()
    {
      throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
    }

    [SecurityCritical]
    public IMessage SyncProcessMessage(IMessage msg)
    {
      try
      {
        LogicalCallContext thread = Message.PropagateCallContextFromMessageToThread(msg);
        if (this._delegate != null)
          this._delegate();
        else
          new CrossContextDelegate(new CallBackHelper(this._eeData, true, this._targetDomainID).Func)();
        Message.PropagateCallContextFromThreadToMessage(msg, thread);
      }
      catch (Exception ex)
      {
        ErrorMessage errorMessage = new ErrorMessage();
        ReturnMessage returnMessage = new ReturnMessage(ex, (IMethodCallMessage) errorMessage);
        LogicalCallContext ctx = (LogicalCallContext) msg.Properties[(object) Message.CallContextKey];
        returnMessage.SetLogicalCallContext(ctx);
        return (IMessage) returnMessage;
      }
      return (IMessage) this;
    }

    [SecurityCritical]
    public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
    {
      IMessage msg1 = this.SyncProcessMessage(msg);
      replySink.SyncProcessMessage(msg1);
      return (IMessageCtrl) null;
    }

    [SecurityCritical]
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null || context.State != StreamingContextStates.CrossAppDomain)
        throw new ArgumentNullException("info");
      info.AddValue("props", (object) this._props, typeof (IDictionary));
      info.AddValue("delegate", (object) this._delegate, typeof (CrossContextDelegate));
      info.AddValue("sourceCtxID", (object) this._sourceCtxID);
      info.AddValue("targetCtxID", (object) this._targetCtxID);
      info.AddValue("targetDomainID", this._targetDomainID);
      info.AddValue("eeData", (object) this._eeData);
    }
  }
}
