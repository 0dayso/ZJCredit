// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.RedirectionProxy
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Security;

namespace System.Runtime.Remoting
{
  internal class RedirectionProxy : MarshalByRefObject, IMessageSink
  {
    private MarshalByRefObject _proxy;
    [SecurityCritical]
    private RealProxy _realProxy;
    private Type _serverType;
    private WellKnownObjectMode _objectMode;

    public WellKnownObjectMode ObjectMode
    {
      set
      {
        this._objectMode = value;
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
    internal RedirectionProxy(MarshalByRefObject proxy, Type serverType)
    {
      this._proxy = proxy;
      this._realProxy = RemotingServices.GetRealProxy((object) this._proxy);
      this._serverType = serverType;
      this._objectMode = WellKnownObjectMode.Singleton;
    }

    [SecurityCritical]
    public virtual IMessage SyncProcessMessage(IMessage msg)
    {
      IMessage message;
      try
      {
        msg.Properties[(object) "__Uri"] = (object) this._realProxy.IdentityObject.URI;
        message = this._objectMode != WellKnownObjectMode.Singleton ? RemotingServices.GetRealProxy((object) (MarshalByRefObject) Activator.CreateInstance(this._serverType, true)).Invoke(msg) : this._realProxy.Invoke(msg);
      }
      catch (Exception ex)
      {
        IMethodCallMessage mcm = msg as IMethodCallMessage;
        message = (IMessage) new ReturnMessage(ex, mcm);
      }
      return message;
    }

    [SecurityCritical]
    public virtual IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
    {
      IMessage msg1 = this.SyncProcessMessage(msg);
      if (replySink != null)
        replySink.SyncProcessMessage(msg1);
      return (IMessageCtrl) null;
    }
  }
}
