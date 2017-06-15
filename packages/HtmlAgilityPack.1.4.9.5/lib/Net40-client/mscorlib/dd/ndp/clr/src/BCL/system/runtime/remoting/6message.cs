// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.MRMDictionary
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
  internal class MRMDictionary : MessageDictionary
  {
    public static string[] MCMkeysFault = new string[1]{ "__CallContext" };
    public static string[] MCMkeysNoFault = new string[7]{ "__Uri", "__MethodName", "__MethodSignature", "__TypeName", "__Return", "__OutArgs", "__CallContext" };
    internal IMethodReturnMessage _mrmsg;
    internal bool fault;

    [SecurityCritical]
    public MRMDictionary(IMethodReturnMessage msg, IDictionary idict)
      : base(msg.Exception != null ? MRMDictionary.MCMkeysFault : MRMDictionary.MCMkeysNoFault, idict)
    {
      this.fault = msg.Exception != null;
      this._mrmsg = msg;
    }

    [SecuritySafeCritical]
    internal override object GetMessageValue(int i)
    {
      switch (i)
      {
        case 0:
          if (this.fault)
            return (object) this.FetchLogicalCallContext();
          return (object) this._mrmsg.Uri;
        case 1:
          return (object) this._mrmsg.MethodName;
        case 2:
          return this._mrmsg.MethodSignature;
        case 3:
          return (object) this._mrmsg.TypeName;
        case 4:
          if (this.fault)
            return (object) this._mrmsg.Exception;
          return this._mrmsg.ReturnValue;
        case 5:
          return (object) this._mrmsg.Args;
        case 6:
          return (object) this.FetchLogicalCallContext();
        default:
          throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
      }
    }

    [SecurityCritical]
    private LogicalCallContext FetchLogicalCallContext()
    {
      ReturnMessage returnMessage = this._mrmsg as ReturnMessage;
      if (returnMessage != null)
        return returnMessage.GetLogicalCallContext();
      MethodResponse methodResponse = this._mrmsg as MethodResponse;
      if (methodResponse != null)
        return methodResponse.GetLogicalCallContext();
      StackBasedReturnMessage basedReturnMessage = this._mrmsg as StackBasedReturnMessage;
      if (basedReturnMessage != null)
        return basedReturnMessage.GetLogicalCallContext();
      throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadType"));
    }

    [SecurityCritical]
    internal override void SetSpecialKey(int keyNum, object value)
    {
      ReturnMessage returnMessage = this._mrmsg as ReturnMessage;
      MethodResponse methodResponse = this._mrmsg as MethodResponse;
      if (keyNum != 0)
      {
        if (keyNum != 1)
          throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
        if (returnMessage != null)
        {
          returnMessage.SetLogicalCallContext((LogicalCallContext) value);
        }
        else
        {
          if (methodResponse == null)
            throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadType"));
          methodResponse.SetLogicalCallContext((LogicalCallContext) value);
        }
      }
      else if (returnMessage != null)
      {
        returnMessage.Uri = (string) value;
      }
      else
      {
        if (methodResponse == null)
          throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadType"));
        methodResponse.Uri = (string) value;
      }
    }
  }
}
