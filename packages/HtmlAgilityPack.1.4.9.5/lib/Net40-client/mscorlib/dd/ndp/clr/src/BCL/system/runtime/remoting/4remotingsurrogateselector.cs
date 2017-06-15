// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.MessageSurrogate
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.Remoting.Activation;
using System.Runtime.Serialization;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
  internal class MessageSurrogate : ISerializationSurrogate
  {
    private static Type _constructionCallType = typeof (ConstructionCall);
    private static Type _methodCallType = typeof (MethodCall);
    private static Type _constructionResponseType = typeof (ConstructionResponse);
    private static Type _methodResponseType = typeof (MethodResponse);
    private static Type _exceptionType = typeof (Exception);
    private static Type _objectType = typeof (object);
    [SecurityCritical]
    private RemotingSurrogateSelector _ss;

    [SecuritySafeCritical]
    static MessageSurrogate()
    {
    }

    [SecurityCritical]
    internal MessageSurrogate(RemotingSurrogateSelector ss)
    {
      this._ss = ss;
    }

    [SecurityCritical]
    public virtual void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
    {
      if (obj == null)
        throw new ArgumentNullException("obj");
      if (info == null)
        throw new ArgumentNullException("info");
      bool flag1 = false;
      bool flag2 = false;
      IMethodMessage msg = obj as IMethodMessage;
      if (msg == null)
        throw new RemotingException(Environment.GetResourceString("Remoting_InvalidMsg"));
      IDictionaryEnumerator enumerator = msg.Properties.GetEnumerator();
      if (msg is IMethodCallMessage)
      {
        if (obj is IConstructionCallMessage)
          flag2 = true;
        info.SetType(flag2 ? MessageSurrogate._constructionCallType : MessageSurrogate._methodCallType);
      }
      else
      {
        if (!(msg is IMethodReturnMessage))
          throw new RemotingException(Environment.GetResourceString("Remoting_InvalidMsg"));
        flag1 = true;
        info.SetType(obj is IConstructionReturnMessage ? MessageSurrogate._constructionResponseType : MessageSurrogate._methodResponseType);
        if (((IMethodReturnMessage) msg).Exception != null)
          info.AddValue("__fault", (object) ((IMethodReturnMessage) msg).Exception, MessageSurrogate._exceptionType);
      }
      while (enumerator.MoveNext())
      {
        if (obj != this._ss.GetRootObject() || this._ss.Filter == null || !this._ss.Filter((string) enumerator.Key, enumerator.Value))
        {
          if (enumerator.Value != null)
          {
            string @string = enumerator.Key.ToString();
            if (@string.Equals("__CallContext"))
            {
              LogicalCallContext logicalCallContext = (LogicalCallContext) enumerator.Value;
              if (logicalCallContext.HasInfo)
                info.AddValue(@string, (object) logicalCallContext);
              else
                info.AddValue(@string, (object) logicalCallContext.RemotingData.LogicalCallID);
            }
            else if (@string.Equals("__MethodSignature"))
            {
              if (flag2 || RemotingServices.IsMethodOverloaded(msg))
                info.AddValue(@string, enumerator.Value);
            }
            else
            {
              flag1 = flag1;
              info.AddValue(@string, enumerator.Value);
            }
          }
          else
            info.AddValue(enumerator.Key.ToString(), enumerator.Value, MessageSurrogate._objectType);
        }
      }
    }

    [SecurityCritical]
    public virtual object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_PopulateData"));
    }
  }
}
