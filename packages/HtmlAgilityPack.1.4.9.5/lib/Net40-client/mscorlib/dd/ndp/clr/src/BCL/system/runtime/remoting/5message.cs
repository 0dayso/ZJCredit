// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.MCMDictionary
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
  internal class MCMDictionary : MessageDictionary
  {
    public static string[] MCMkeys = new string[6]{ "__Uri", "__MethodName", "__MethodSignature", "__TypeName", "__Args", "__CallContext" };
    internal IMethodCallMessage _mcmsg;

    public MCMDictionary(IMethodCallMessage msg, IDictionary idict)
      : base(MCMDictionary.MCMkeys, idict)
    {
      this._mcmsg = msg;
    }

    [SecuritySafeCritical]
    internal override object GetMessageValue(int i)
    {
      switch (i)
      {
        case 0:
          return (object) this._mcmsg.Uri;
        case 1:
          return (object) this._mcmsg.MethodName;
        case 2:
          return this._mcmsg.MethodSignature;
        case 3:
          return (object) this._mcmsg.TypeName;
        case 4:
          return (object) this._mcmsg.Args;
        case 5:
          return (object) this.FetchLogicalCallContext();
        default:
          throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
      }
    }

    [SecurityCritical]
    private LogicalCallContext FetchLogicalCallContext()
    {
      Message message = this._mcmsg as Message;
      if (message != null)
        return message.GetLogicalCallContext();
      MethodCall methodCall = this._mcmsg as MethodCall;
      if (methodCall != null)
        return methodCall.GetLogicalCallContext();
      throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadType"));
    }

    [SecurityCritical]
    internal override void SetSpecialKey(int keyNum, object value)
    {
      Message message = this._mcmsg as Message;
      MethodCall methodCall = this._mcmsg as MethodCall;
      if (keyNum != 0)
      {
        if (keyNum != 1)
          throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
        if (message == null)
          throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadType"));
        message.SetLogicalCallContext((LogicalCallContext) value);
      }
      else if (message != null)
      {
        message.Uri = (string) value;
      }
      else
      {
        if (methodCall == null)
          throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadType"));
        methodCall.Uri = (string) value;
      }
    }
  }
}
