// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.ComRedirectionProxy
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting
{
  internal class ComRedirectionProxy : MarshalByRefObject, IMessageSink
  {
    private MarshalByRefObject _comObject;
    private Type _serverType;

    public IMessageSink NextSink
    {
      [SecurityCritical] get
      {
        return (IMessageSink) null;
      }
    }

    internal ComRedirectionProxy(MarshalByRefObject comObject, Type serverType)
    {
      this._comObject = comObject;
      this._serverType = serverType;
    }

    [SecurityCritical]
    public virtual IMessage SyncProcessMessage(IMessage msg)
    {
      IMethodCallMessage reqMsg = (IMethodCallMessage) msg;
      IMethodReturnMessage methodReturnMessage = RemotingServices.ExecuteMessage(this._comObject, reqMsg);
      if (methodReturnMessage != null)
      {
        COMException comException = methodReturnMessage.Exception as COMException;
        if (comException != null && (comException._HResult == -2147023174 || comException._HResult == -2147023169))
        {
          this._comObject = (MarshalByRefObject) Activator.CreateInstance(this._serverType, true);
          methodReturnMessage = RemotingServices.ExecuteMessage(this._comObject, reqMsg);
        }
      }
      return (IMessage) methodReturnMessage;
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
