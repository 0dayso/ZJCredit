// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.MethodResponse
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace System.Runtime.Remoting.Messaging
{
  /// <summary>实现 <see cref="T:System.Runtime.Remoting.Messaging.IMethodReturnMessage" /> 接口来创建一条消息，该消息作为远程对象上的方法响应。</summary>
  [SecurityCritical]
  [CLSCompliant(false)]
  [ComVisible(true)]
  [Serializable]
  [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
  public class MethodResponse : IMethodReturnMessage, IMethodMessage, IMessage, ISerializable, ISerializationRootObject, IInternalMessage
  {
    private MethodBase MI;
    private string methodName;
    private Type[] methodSignature;
    private string uri;
    private string typeName;
    private object retVal;
    private Exception fault;
    private object[] outArgs;
    private LogicalCallContext callContext;
    /// <summary>指定表示远程处理消息属性集合的 <see cref="T:System.Collections.IDictionary" /> 接口。</summary>
    protected IDictionary InternalProperties;
    /// <summary>指定表示远程处理消息属性集合的 <see cref="T:System.Collections.IDictionary" /> 接口。</summary>
    protected IDictionary ExternalProperties;
    private int argCount;
    private bool fSoap;
    private ArgMapper argMapper;
    private RemotingMethodCachedData _methodCache;

    /// <summary>获取在其上进行方法调用的远程对象的统一资源标识符 (URI)。</summary>
    /// <returns>远程对象的 URI。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public string Uri
    {
      [SecurityCritical] get
      {
        return this.uri;
      }
      set
      {
        this.uri = value;
      }
    }

    /// <summary>获取被调用方法的名称。</summary>
    /// <returns>一个 <see cref="T:System.String" />，包含调用的方法的名称。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public string MethodName
    {
      [SecurityCritical] get
      {
        return this.methodName;
      }
    }

    /// <summary>获取在其上进行方法调用的远程对象的完整类型名称。</summary>
    /// <returns>一个 <see cref="T:System.String" />，包含在其上进行方法调用的远程对象的完整类型名称。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public string TypeName
    {
      [SecurityCritical] get
      {
        return this.typeName;
      }
    }

    /// <summary>获取包含方法签名的对象。</summary>
    /// <returns>包含方法签名的 <see cref="T:System.Object" />。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public object MethodSignature
    {
      [SecurityCritical] get
      {
        return (object) this.methodSignature;
      }
    }

    /// <summary>获取被调用方法的 <see cref="T:System.Reflection.MethodBase" />。</summary>
    /// <returns>被调用方法的 <see cref="T:System.Reflection.MethodBase" />。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public MethodBase MethodBase
    {
      [SecurityCritical] get
      {
        return this.MI;
      }
    }

    /// <summary>获取一个值，该值指示该方法是否接受数目可变的参数。</summary>
    /// <returns>如果该方法可以接受数目可变的参数，则为 true；否则为 false。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public bool HasVarArgs
    {
      [SecurityCritical] get
      {
        return false;
      }
    }

    /// <summary>获取传递给该方法的参数的数目。</summary>
    /// <returns>表示传递给方法的参数数目的 <see cref="T:System.Int32" />。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public int ArgCount
    {
      [SecurityCritical] get
      {
        if (this.outArgs == null)
          return 0;
        return this.outArgs.Length;
      }
    }

    /// <summary>获取传递给该方法的参数数组。</summary>
    /// <returns>类型 <see cref="T:System.Object" /> 的数组，表示传递给方法的参数。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public object[] Args
    {
      [SecurityCritical] get
      {
        return this.outArgs;
      }
    }

    /// <summary>获取方法调用中标记为 ref 或 out 参数的参数的数目。</summary>
    /// <returns>一个 <see cref="T:System.Int32" />，表示方法调用中标记为 ref 或 out 参数的参数的数目。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public int OutArgCount
    {
      [SecurityCritical] get
      {
        if (this.argMapper == null)
          this.argMapper = new ArgMapper((IMethodMessage) this, true);
        return this.argMapper.ArgCount;
      }
    }

    /// <summary>获取方法调用中标记为 ref 或 out 参数的一组参数。</summary>
    /// <returns>类型 <see cref="T:System.Object" /> 的数组，表示方法调用中标记为 ref 或 out 参数的参数。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public object[] OutArgs
    {
      [SecurityCritical] get
      {
        if (this.argMapper == null)
          this.argMapper = new ArgMapper((IMethodMessage) this, true);
        return this.argMapper.Args;
      }
    }

    /// <summary>获取方法调用期间引发的异常；或者如果该方法未引发异常，则为 null。</summary>
    /// <returns>方法调用期间引发的 <see cref="T:System.Exception" />；或者如果该方法未引发异常，则为 null。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public Exception Exception
    {
      [SecurityCritical] get
      {
        return this.fault;
      }
    }

    /// <summary>获取方法调用的返回值。</summary>
    /// <returns>一个 <see cref="T:System.Object" />，表示方法调用的返回值。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public object ReturnValue
    {
      [SecurityCritical] get
      {
        return this.retVal;
      }
    }

    /// <summary>获取表示远程处理消息属性集合的 <see cref="T:System.Collections.IDictionary" /> 接口。</summary>
    /// <returns>表示远程处理消息属性集合的 <see cref="T:System.Collections.IDictionary" /> 接口。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public virtual IDictionary Properties
    {
      [SecurityCritical] get
      {
        lock (this)
        {
          if (this.InternalProperties == null)
            this.InternalProperties = (IDictionary) new Hashtable();
          if (this.ExternalProperties == null)
            this.ExternalProperties = (IDictionary) new MRMDictionary((IMethodReturnMessage) this, this.InternalProperties);
          return this.ExternalProperties;
        }
      }
    }

    /// <summary>获取当前方法调用的 <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" />。</summary>
    /// <returns>当前方法调用的 <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" />。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public LogicalCallContext LogicalCallContext
    {
      [SecurityCritical] get
      {
        return this.GetLogicalCallContext();
      }
    }

    ServerIdentity IInternalMessage.ServerIdentityObject
    {
      [SecurityCritical] get
      {
        return (ServerIdentity) null;
      }
      [SecurityCritical] set
      {
      }
    }

    Identity IInternalMessage.IdentityObject
    {
      [SecurityCritical] get
      {
        return (Identity) null;
      }
      [SecurityCritical] set
      {
      }
    }

    /// <summary>从一个远程处理标头数组和一个请求消息初始化 <see cref="T:System.Runtime.Remoting.Messaging.MethodResponse" /> 类的一个新实例。</summary>
    /// <param name="h1">包含键/值对的远程处理标头数组。该数组用于初始化属于“http://schemas.microsoft.com/clr/soap/messageProperties”命名空间的标头的 <see cref="T:System.Runtime.Remoting.Messaging.MethodResponse" /> 字段。</param>
    /// <param name="mcm">作为远程对象上的方法调用的请求消息。</param>
    [SecurityCritical]
    public MethodResponse(Header[] h1, IMethodCallMessage mcm)
    {
      if (mcm == null)
        throw new ArgumentNullException("mcm");
      Message message = mcm as Message;
      this.MI = message == null ? mcm.MethodBase : message.GetMethodBase();
      if (this.MI == (MethodBase) null)
        throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Message_MethodMissing"), (object) mcm.MethodName, (object) mcm.TypeName));
      this._methodCache = InternalRemotingServices.GetReflectionCachedData(this.MI);
      this.argCount = this._methodCache.Parameters.Length;
      this.fSoap = true;
      this.FillHeaders(h1);
    }

    [SecurityCritical]
    internal MethodResponse(IMethodCallMessage msg, SmuggledMethodReturnMessage smuggledMrm, ArrayList deserializedArgs)
    {
      this.MI = msg.MethodBase;
      this._methodCache = InternalRemotingServices.GetReflectionCachedData(this.MI);
      this.methodName = msg.MethodName;
      this.uri = msg.Uri;
      this.typeName = msg.TypeName;
      if (this._methodCache.IsOverloaded())
        this.methodSignature = (Type[]) msg.MethodSignature;
      this.retVal = smuggledMrm.GetReturnValue(deserializedArgs);
      this.outArgs = smuggledMrm.GetArgs(deserializedArgs);
      this.fault = smuggledMrm.GetException(deserializedArgs);
      this.callContext = smuggledMrm.GetCallContext(deserializedArgs);
      if (smuggledMrm.MessagePropertyCount > 0)
        smuggledMrm.PopulateMessageProperties(this.Properties, deserializedArgs);
      this.argCount = this._methodCache.Parameters.Length;
      this.fSoap = false;
    }

    [SecurityCritical]
    internal MethodResponse(IMethodCallMessage msg, object handlerObject, BinaryMethodReturnMessage smuggledMrm)
    {
      if (msg != null)
      {
        this.MI = msg.MethodBase;
        this._methodCache = InternalRemotingServices.GetReflectionCachedData(this.MI);
        this.methodName = msg.MethodName;
        this.uri = msg.Uri;
        this.typeName = msg.TypeName;
        if (this._methodCache.IsOverloaded())
          this.methodSignature = (Type[]) msg.MethodSignature;
        this.argCount = this._methodCache.Parameters.Length;
      }
      this.retVal = smuggledMrm.ReturnValue;
      this.outArgs = smuggledMrm.Args;
      this.fault = smuggledMrm.Exception;
      this.callContext = smuggledMrm.LogicalCallContext;
      if (smuggledMrm.HasProperties)
        smuggledMrm.PopulateMessageProperties(this.Properties);
      this.fSoap = false;
    }

    [SecurityCritical]
    internal MethodResponse(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      this.SetObjectData(info, context);
    }

    /// <summary>从应用到一个方法的远程处理标头数组初始化一个内部序列化处理程序。</summary>
    /// <returns>内部序列化处理程序。</returns>
    /// <param name="h">包含键/值对的远程处理标头数组。该数组用于初始化属于“http://schemas.microsoft.com/clr/soap/messageProperties”命名空间的标头的 <see cref="T:System.Runtime.Remoting.Messaging.MethodResponse" /> 字段。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public virtual object HeaderHandler(Header[] h)
    {
      SerializationMonkey serializationMonkey = (SerializationMonkey) FormatterServices.GetUninitializedObject(typeof (SerializationMonkey));
      Header[] h1;
      if (h != null && h.Length != 0 && h[0].Name == "__methodName")
      {
        if (h.Length > 1)
        {
          h1 = new Header[h.Length - 1];
          Array.Copy((Array) h, 1, (Array) h1, 0, h.Length - 1);
        }
        else
          h1 = (Header[]) null;
      }
      else
        h1 = h;
      Type type = (Type) null;
      MethodInfo methodInfo = this.MI as MethodInfo;
      if (methodInfo != (MethodInfo) null)
        type = methodInfo.ReturnType;
      ParameterInfo[] parameters = this._methodCache.Parameters;
      int length = this._methodCache.MarshalResponseArgMap.Length;
      if (!(type == (Type) null) && !(type == typeof (void)))
        ++length;
      Type[] typeArray = new Type[length];
      string[] strArray = new string[length];
      int index = 0;
      if (!(type == (Type) null) && !(type == typeof (void)))
        typeArray[index++] = type;
      foreach (int marshalResponseArg in this._methodCache.MarshalResponseArgMap)
      {
        strArray[index] = parameters[marshalResponseArg].Name;
        typeArray[index++] = !parameters[marshalResponseArg].ParameterType.IsByRef ? parameters[marshalResponseArg].ParameterType : parameters[marshalResponseArg].ParameterType.GetElementType();
      }
      serializationMonkey.FieldTypes = typeArray;
      serializationMonkey.FieldNames = strArray;
      this.FillHeaders(h1, true);
      serializationMonkey._obj = (ISerializationRootObject) this;
      return (object) serializationMonkey;
    }

    /// <summary>从序列化设置来设置方法信息。</summary>
    /// <param name="info">用于序列化或反序列化远程对象的数据。</param>
    /// <param name="ctx">特定序列化流的上下文。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public void RootSetObjectData(SerializationInfo info, StreamingContext ctx)
    {
      this.SetObjectData(info, ctx);
    }

    [SecurityCritical]
    internal void SetObjectData(SerializationInfo info, StreamingContext ctx)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      if (this.fSoap)
      {
        this.SetObjectFromSoapData(info);
      }
      else
      {
        SerializationInfoEnumerator enumerator = info.GetEnumerator();
        bool flag1 = false;
        bool flag2 = false;
        while (enumerator.MoveNext())
        {
          if (enumerator.Name.Equals("__return"))
          {
            flag1 = true;
            break;
          }
          if (enumerator.Name.Equals("__fault"))
          {
            flag2 = true;
            this.fault = (Exception) enumerator.Value;
            break;
          }
          this.FillHeader(enumerator.Name, enumerator.Value);
        }
        if (flag2 & flag1)
          throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadSerialization"));
      }
    }

    /// <summary>
    /// <see cref="M:System.Runtime.Remoting.Messaging.MethodResponse.GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)" /> 方法未实现。</summary>
    /// <param name="info">用于序列化或反序列化远程对象的数据。</param>
    /// <param name="context">特定序列化流的上下文。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter, Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_Method"));
    }

    internal void SetObjectFromSoapData(SerializationInfo info)
    {
      Hashtable keyToNamespaceTable = (Hashtable) info.GetValue("__keyToNamespaceTable", typeof (Hashtable));
      ArrayList arrayList = (ArrayList) info.GetValue("__paramNameList", typeof (ArrayList));
      SoapFault soapFault = (SoapFault) info.GetValue("__fault", typeof (SoapFault));
      if (soapFault != null)
      {
        ServerFault serverFault = soapFault.Detail as ServerFault;
        if (serverFault != null)
        {
          if (serverFault.Exception != null)
          {
            this.fault = serverFault.Exception;
          }
          else
          {
            Type type = Type.GetType(serverFault.ExceptionType, false, false);
            if (type == (Type) null)
            {
              StringBuilder stringBuilder = new StringBuilder();
              stringBuilder.Append("\nException Type: ");
              stringBuilder.Append(serverFault.ExceptionType);
              stringBuilder.Append("\n");
              stringBuilder.Append("Exception Message: ");
              stringBuilder.Append(serverFault.ExceptionMessage);
              stringBuilder.Append("\n");
              stringBuilder.Append(serverFault.StackTrace);
              this.fault = (Exception) new ServerException(stringBuilder.ToString());
            }
            else
            {
              object[] args = new object[1]{ (object) serverFault.ExceptionMessage };
              this.fault = (Exception) Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, (Binder) null, args, (CultureInfo) null, (object[]) null);
            }
          }
        }
        else if (soapFault.Detail != null && soapFault.Detail.GetType() == typeof (string) && ((string) soapFault.Detail).Length != 0)
          this.fault = (Exception) new ServerException((string) soapFault.Detail);
        else
          this.fault = (Exception) new ServerException(soapFault.FaultString);
      }
      else
      {
        MethodInfo methodInfo = this.MI as MethodInfo;
        int num = 0;
        if (methodInfo != (MethodInfo) null)
        {
          Type returnType = methodInfo.ReturnType;
          if (returnType != typeof (void))
          {
            ++num;
            object obj = info.GetValue((string) arrayList[0], typeof (object));
            this.retVal = !(obj is string) ? obj : Message.SoapCoerceArg(obj, returnType, keyToNamespaceTable);
          }
        }
        ParameterInfo[] parameters = this._methodCache.Parameters;
        object obj1 = this.InternalProperties == null ? (object) null : this.InternalProperties[(object) "__UnorderedParams"];
        if (obj1 != null && obj1 is bool && (bool) obj1)
        {
          for (int index1 = num; index1 < arrayList.Count; ++index1)
          {
            string name = (string) arrayList[index1];
            int index2 = -1;
            for (int index3 = 0; index3 < parameters.Length; ++index3)
            {
              if (name.Equals(parameters[index3].Name))
                index2 = parameters[index3].Position;
            }
            if (index2 == -1)
            {
              if (!name.StartsWith("__param", StringComparison.Ordinal))
                throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadSerialization"));
              index2 = int.Parse(name.Substring(7), (IFormatProvider) CultureInfo.InvariantCulture);
            }
            if (index2 >= this.argCount)
              throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadSerialization"));
            if (this.outArgs == null)
              this.outArgs = new object[this.argCount];
            this.outArgs[index2] = Message.SoapCoerceArg(info.GetValue(name, typeof (object)), parameters[index2].ParameterType, keyToNamespaceTable);
          }
        }
        else
        {
          if (this.argMapper == null)
            this.argMapper = new ArgMapper((IMethodMessage) this, true);
          for (int index1 = num; index1 < arrayList.Count; ++index1)
          {
            string name = (string) arrayList[index1];
            if (this.outArgs == null)
              this.outArgs = new object[this.argCount];
            int index2 = this.argMapper.Map[index1 - num];
            this.outArgs[index2] = Message.SoapCoerceArg(info.GetValue(name, typeof (object)), parameters[index2].ParameterType, keyToNamespaceTable);
          }
        }
      }
    }

    [SecurityCritical]
    internal LogicalCallContext GetLogicalCallContext()
    {
      if (this.callContext == null)
        this.callContext = new LogicalCallContext();
      return this.callContext;
    }

    internal LogicalCallContext SetLogicalCallContext(LogicalCallContext ctx)
    {
      LogicalCallContext logicalCallContext = this.callContext;
      this.callContext = ctx;
      return logicalCallContext;
    }

    /// <summary>获取指定索引处作为对象的方法参数。</summary>
    /// <returns>作为对象的方法参数。</returns>
    /// <param name="argNum">请求的参数的索引。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public object GetArg(int argNum)
    {
      return this.outArgs[argNum];
    }

    /// <summary>获取指定索引处的方法参数的名称。</summary>
    /// <returns>方法参数的名称。</returns>
    /// <param name="index">请求的参数的索引。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public string GetArgName(int index)
    {
      if (!(this.MI != (MethodBase) null))
        return "__param" + (object) index;
      RemotingMethodCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(this.MI);
      ParameterInfo[] parameters = reflectionCachedData.Parameters;
      if (index < 0 || index >= parameters.Length)
        throw new ArgumentOutOfRangeException("index");
      return reflectionCachedData.Parameters[index].Name;
    }

    /// <summary>返回标记为 ref 或 out 参数的指定参数。</summary>
    /// <returns>标记为 ref 或 out 参数的指定参数。</returns>
    /// <param name="argNum">请求的参数的索引。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public object GetOutArg(int argNum)
    {
      if (this.argMapper == null)
        this.argMapper = new ArgMapper((IMethodMessage) this, true);
      return this.argMapper.GetArg(argNum);
    }

    /// <summary>返回标记为 ref 或 out 参数的指定参数的名称。</summary>
    /// <returns>参数名称；或者如果未实现当前方法，则为 null。</returns>
    /// <param name="index">请求的参数的索引。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public string GetOutArgName(int index)
    {
      if (this.argMapper == null)
        this.argMapper = new ArgMapper((IMethodMessage) this, true);
      return this.argMapper.GetArgName(index);
    }

    [SecurityCritical]
    internal void FillHeaders(Header[] h)
    {
      this.FillHeaders(h, false);
    }

    [SecurityCritical]
    private void FillHeaders(Header[] h, bool bFromHeaderHandler)
    {
      if (h == null)
        return;
      if (bFromHeaderHandler && this.fSoap)
      {
        for (int index = 0; index < h.Length; ++index)
        {
          Header header = h[index];
          if (header.HeaderNamespace == "http://schemas.microsoft.com/clr/soap/messageProperties")
            this.FillHeader(header.Name, header.Value);
          else
            this.FillHeader(LogicalCallContext.GetPropertyKeyForHeader(header), (object) header);
        }
      }
      else
      {
        for (int index = 0; index < h.Length; ++index)
          this.FillHeader(h[index].Name, h[index].Value);
      }
    }

    [SecurityCritical]
    internal void FillHeader(string name, object value)
    {
      if (name.Equals("__MethodName"))
        this.methodName = (string) value;
      else if (name.Equals("__Uri"))
        this.uri = (string) value;
      else if (name.Equals("__MethodSignature"))
        this.methodSignature = (Type[]) value;
      else if (name.Equals("__TypeName"))
        this.typeName = (string) value;
      else if (name.Equals("__OutArgs"))
        this.outArgs = (object[]) value;
      else if (name.Equals("__CallContext"))
      {
        if (value is string)
        {
          this.callContext = new LogicalCallContext();
          this.callContext.RemotingData.LogicalCallID = (string) value;
        }
        else
          this.callContext = (LogicalCallContext) value;
      }
      else if (name.Equals("__Return"))
      {
        this.retVal = value;
      }
      else
      {
        if (this.InternalProperties == null)
          this.InternalProperties = (IDictionary) new Hashtable();
        this.InternalProperties[(object) name] = value;
      }
    }

    [SecurityCritical]
    void IInternalMessage.SetURI(string val)
    {
      this.uri = val;
    }

    [SecurityCritical]
    void IInternalMessage.SetCallContext(LogicalCallContext newCallContext)
    {
      this.callContext = newCallContext;
    }

    [SecurityCritical]
    bool IInternalMessage.HasProperties()
    {
      if (this.ExternalProperties == null)
        return this.InternalProperties != null;
      return true;
    }
  }
}
