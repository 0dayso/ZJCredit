// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.MethodCall
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Messaging
{
  /// <summary>实现 <see cref="T:System.Runtime.Remoting.Messaging.IMethodCallMessage" /> 接口来创建一个请求消息，该消息作为远程对象上的方法调用。</summary>
  [SecurityCritical]
  [CLSCompliant(false)]
  [ComVisible(true)]
  [Serializable]
  [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
  public class MethodCall : IMethodCallMessage, IMethodMessage, IMessage, ISerializable, IInternalMessage, ISerializationRootObject
  {
    private const BindingFlags LookupAll = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
    private const BindingFlags LookupPublic = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;
    private string uri;
    private string methodName;
    private MethodBase MI;
    private string typeName;
    private object[] args;
    private Type[] instArgs;
    private LogicalCallContext callContext;
    private Type[] methodSignature;
    /// <summary>表示远程处理消息属性集合的 <see cref="T:System.Collections.IDictionary" /> 接口。</summary>
    protected IDictionary ExternalProperties;
    /// <summary>表示远程处理消息属性集合的 <see cref="T:System.Collections.IDictionary" /> 接口。</summary>
    protected IDictionary InternalProperties;
    private ServerIdentity srvID;
    private Identity identity;
    private bool fSoap;
    private bool fVarArgs;
    private ArgMapper argMapper;

    /// <summary>获取传递给某方法的参数的数目。</summary>
    /// <returns>表示传递给方法的参数数目的 <see cref="T:System.Int32" />。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public int ArgCount
    {
      [SecurityCritical] get
      {
        if (this.args != null)
          return this.args.Length;
        return 0;
      }
    }

    /// <summary>获取传递给方法的参数数组。</summary>
    /// <returns>类型 <see cref="T:System.Object" /> 的数组，表示传递给方法的参数。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public object[] Args
    {
      [SecurityCritical] get
      {
        return this.args;
      }
    }

    /// <summary>获取方法调用中未标记为 out 参数的参数数目。</summary>
    /// <returns>一个 <see cref="T:System.Int32" />，表示方法调用中未标记为 out 参数的参数数目。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public int InArgCount
    {
      [SecurityCritical] get
      {
        if (this.argMapper == null)
          this.argMapper = new ArgMapper((IMethodMessage) this, false);
        return this.argMapper.ArgCount;
      }
    }

    /// <summary>获取方法调用中未标记为 out 参数的一组参数。</summary>
    /// <returns>类型 <see cref="T:System.Object" /> 的数组，表示方法调用中未标记为 out 参数的参数。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public object[] InArgs
    {
      [SecurityCritical] get
      {
        if (this.argMapper == null)
          this.argMapper = new ArgMapper((IMethodMessage) this, false);
        return this.argMapper.Args;
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
        if (this.methodSignature != null)
          return (object) this.methodSignature;
        if (this.MI != (MethodBase) null)
          this.methodSignature = Message.GenerateMethodSignature(this.MethodBase);
        return (object) null;
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
        if (this.MI == (MethodBase) null)
          this.MI = RemotingServices.InternalGetMethodBaseFromMethodMessage((IMethodMessage) this);
        return this.MI;
      }
    }

    /// <summary>获取或设置在其上进行方法调用的远程对象的统一资源标识符 (URI)。</summary>
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

    /// <summary>获取一个值，该值指示该方法是否接受数目可变的参数。</summary>
    /// <returns>如果该方法可以接受数目可变的参数，则为 true；否则为 false。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public bool HasVarArgs
    {
      [SecurityCritical] get
      {
        return this.fVarArgs;
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
            this.ExternalProperties = (IDictionary) new MCMDictionary((IMethodCallMessage) this, this.InternalProperties);
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
        return this.srvID;
      }
      [SecurityCritical] set
      {
        this.srvID = value;
      }
    }

    Identity IInternalMessage.IdentityObject
    {
      [SecurityCritical] get
      {
        return this.identity;
      }
      [SecurityCritical] set
      {
        this.identity = value;
      }
    }

    /// <summary>从一个远程处理标头数组初始化 <see cref="T:System.Runtime.Remoting.Messaging.MethodCall" /> 类的一个新实例。</summary>
    /// <param name="h1">包含键/值对的远程处理标头数组。该数组用于初始化属于“http://schemas.microsoft.com/clr/soap/messageProperties”命名空间的标头的 <see cref="T:System.Runtime.Remoting.Messaging.MethodCall" /> 字段。</param>
    [SecurityCritical]
    public MethodCall(Header[] h1)
    {
      this.Init();
      this.fSoap = true;
      this.FillHeaders(h1);
      this.ResolveMethod();
    }

    /// <summary>通过复制现有消息来初始化 <see cref="T:System.Runtime.Remoting.Messaging.MethodCall" /> 类的一个新实例。</summary>
    /// <param name="msg">远程处理消息。</param>
    [SecurityCritical]
    public MethodCall(IMessage msg)
    {
      if (msg == null)
        throw new ArgumentNullException("msg");
      this.Init();
      IDictionaryEnumerator enumerator = msg.Properties.GetEnumerator();
      while (enumerator.MoveNext())
        this.FillHeader(enumerator.Key.ToString(), enumerator.Value);
      IMethodCallMessage methodCallMessage = msg as IMethodCallMessage;
      if (methodCallMessage != null)
        this.MI = methodCallMessage.MethodBase;
      this.ResolveMethod();
    }

    [SecurityCritical]
    internal MethodCall(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      this.Init();
      this.SetObjectData(info, context);
    }

    [SecurityCritical]
    internal MethodCall(SmuggledMethodCallMessage smuggledMsg, ArrayList deserializedArgs)
    {
      this.uri = smuggledMsg.Uri;
      this.typeName = smuggledMsg.TypeName;
      this.methodName = smuggledMsg.MethodName;
      this.methodSignature = (Type[]) smuggledMsg.GetMethodSignature(deserializedArgs);
      this.args = smuggledMsg.GetArgs(deserializedArgs);
      this.instArgs = smuggledMsg.GetInstantiation(deserializedArgs);
      this.callContext = smuggledMsg.GetCallContext(deserializedArgs);
      this.ResolveMethod();
      if (smuggledMsg.MessagePropertyCount <= 0)
        return;
      smuggledMsg.PopulateMessageProperties(this.Properties, deserializedArgs);
    }

    [SecurityCritical]
    internal MethodCall(object handlerObject, BinaryMethodCallMessage smuggledMsg)
    {
      if (handlerObject != null)
      {
        this.uri = handlerObject as string;
        if (this.uri == null)
        {
          MarshalByRefObject marshalByRefObject = handlerObject as MarshalByRefObject;
          if (marshalByRefObject != null)
          {
            bool fServer;
            this.srvID = MarshalByRefObject.GetIdentity(marshalByRefObject, out fServer) as ServerIdentity;
            this.uri = this.srvID.URI;
          }
        }
      }
      this.typeName = smuggledMsg.TypeName;
      this.methodName = smuggledMsg.MethodName;
      this.methodSignature = (Type[]) smuggledMsg.MethodSignature;
      this.args = smuggledMsg.Args;
      this.instArgs = smuggledMsg.InstantiationArgs;
      this.callContext = smuggledMsg.LogicalCallContext;
      this.ResolveMethod();
      if (!smuggledMsg.HasProperties)
        return;
      smuggledMsg.PopulateMessageProperties(this.Properties);
    }

    /// <summary>从序列化设置来设置方法信息。</summary>
    /// <param name="info">用于序列化或反序列化远程对象的数据。</param>
    /// <param name="ctx">给定序列化流的上下文。</param>
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
    internal void SetObjectData(SerializationInfo info, StreamingContext context)
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
        while (enumerator.MoveNext())
          this.FillHeader(enumerator.Name, enumerator.Value);
        if (context.State != StreamingContextStates.Remoting || context.Context == null)
          return;
        Header[] headerArray = context.Context as Header[];
        if (headerArray == null)
          return;
        for (int index = 0; index < headerArray.Length; ++index)
          this.FillHeader(headerArray[index].Name, headerArray[index].Value);
      }
    }

    private static Type ResolveTypeRelativeTo(string typeName, int offset, int count, Type serverType)
    {
      Type baseTypes = MethodCall.ResolveTypeRelativeToBaseTypes(typeName, offset, count, serverType);
      if (baseTypes == (Type) null)
      {
        foreach (Type @interface in serverType.GetInterfaces())
        {
          string fullName = @interface.FullName;
          if (fullName.Length == count && string.CompareOrdinal(typeName, offset, fullName, 0, count) == 0)
            return @interface;
        }
      }
      return baseTypes;
    }

    private static Type ResolveTypeRelativeToBaseTypes(string typeName, int offset, int count, Type serverType)
    {
      if (typeName == null || serverType == (Type) null)
        return (Type) null;
      string fullName = serverType.FullName;
      if (fullName.Length == count && string.CompareOrdinal(typeName, offset, fullName, 0, count) == 0)
        return serverType;
      return MethodCall.ResolveTypeRelativeToBaseTypes(typeName, offset, count, serverType.BaseType);
    }

    internal Type ResolveType()
    {
      Type newType = (Type) null;
      if (this.srvID == null)
        this.srvID = IdentityHolder.CasualResolveIdentity(this.uri) as ServerIdentity;
      if (this.srvID != null)
      {
        Type lastCalledType = this.srvID.GetLastCalledType(this.typeName);
        if (lastCalledType != (Type) null)
          return lastCalledType;
        int num1 = 0;
        if (string.CompareOrdinal(this.typeName, 0, "clr:", 0, 4) == 0)
          num1 = 4;
        int num2 = this.typeName.IndexOf(',', num1);
        if (num2 == -1)
          num2 = this.typeName.Length;
        Type serverType = this.srvID.ServerType;
        newType = MethodCall.ResolveTypeRelativeTo(this.typeName, num1, num2 - num1, serverType);
      }
      if (newType == (Type) null)
        newType = RemotingServices.InternalGetTypeFromQualifiedTypeName(this.typeName);
      if (this.srvID != null)
        this.srvID.SetLastCalledType(this.typeName, newType);
      return newType;
    }

    /// <summary>从先前初始化的远程处理消息属性设置方法信息。</summary>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public void ResolveMethod()
    {
      this.ResolveMethod(true);
    }

    [SecurityCritical]
    internal void ResolveMethod(bool bThrowIfNotResolved)
    {
      if (!(this.MI == (MethodBase) null) || this.methodName == null)
        return;
      RuntimeType runtimeType = this.ResolveType() as RuntimeType;
      if (this.methodName.Equals(".ctor"))
        return;
      if (runtimeType == (RuntimeType) null)
        throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_BadType"), (object) this.typeName));
      if (this.methodSignature != null)
      {
        bool flag = false;
        int num = this.instArgs == null ? 0 : this.instArgs.Length;
        if (num == 0)
        {
          try
          {
            this.MI = (MethodBase) runtimeType.GetMethod(this.methodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, (Binder) null, CallingConventions.Any, this.methodSignature, (ParameterModifier[]) null);
            flag = true;
          }
          catch (AmbiguousMatchException ex)
          {
          }
        }
        if (!flag)
        {
          MemberInfo[] members = runtimeType.FindMembers(MemberTypes.Method, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, Type.FilterName, (object) this.methodName);
          int length = 0;
          for (int index = 0; index < members.Length; ++index)
          {
            try
            {
              MethodInfo methodInfo = (MethodInfo) members[index];
              if ((methodInfo.IsGenericMethod ? methodInfo.GetGenericArguments().Length : 0) == num)
              {
                if (num > 0)
                  methodInfo = methodInfo.MakeGenericMethod(this.instArgs);
                members[length] = (MemberInfo) methodInfo;
                ++length;
              }
            }
            catch (ArgumentException ex)
            {
            }
            catch (VerificationException ex)
            {
            }
          }
          MethodInfo[] methodInfoArray = new MethodInfo[length];
          for (int index = 0; index < length; ++index)
            methodInfoArray[index] = (MethodInfo) members[index];
          this.MI = Type.DefaultBinder.SelectMethod(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, (MethodBase[]) methodInfoArray, this.methodSignature, (ParameterModifier[]) null);
        }
      }
      else
      {
        RemotingTypeCachedData remotingTypeCachedData = (RemotingTypeCachedData) null;
        if (this.instArgs == null)
        {
          remotingTypeCachedData = InternalRemotingServices.GetReflectionCachedData(runtimeType);
          this.MI = remotingTypeCachedData.GetLastCalledMethod(this.methodName);
          if (this.MI != (MethodBase) null)
            return;
        }
        bool flag = false;
        try
        {
          this.MI = (MethodBase) runtimeType.GetMethod(this.methodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
          if (this.instArgs != null)
          {
            if (this.instArgs.Length != 0)
              this.MI = (MethodBase) ((MethodInfo) this.MI).MakeGenericMethod(this.instArgs);
          }
        }
        catch (AmbiguousMatchException ex)
        {
          flag = true;
          this.ResolveOverloadedMethod(runtimeType);
        }
        if (this.MI != (MethodBase) null && !flag && remotingTypeCachedData != null)
          remotingTypeCachedData.SetLastCalledMethod(this.methodName, this.MI);
      }
      if (this.MI == (MethodBase) null & bThrowIfNotResolved)
        throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Message_MethodMissing"), (object) this.methodName, (object) this.typeName));
    }

    private void ResolveOverloadedMethod(RuntimeType t)
    {
      if (this.args == null)
        return;
      MemberInfo[] member = t.GetMember(this.methodName, MemberTypes.Method, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
      int length1 = member.Length;
      switch (length1)
      {
        case 1:
          this.MI = member[0] as MethodBase;
          break;
        case 0:
          break;
        default:
          int length2 = this.args.Length;
          MethodBase methodBase1 = (MethodBase) null;
          for (int index = 0; index < length1; ++index)
          {
            MethodBase methodBase2 = member[index] as MethodBase;
            if (methodBase2.GetParameters().Length == length2)
            {
              if (methodBase1 != (MethodBase) null)
                throw new RemotingException(Environment.GetResourceString("Remoting_AmbiguousMethod"));
              methodBase1 = methodBase2;
            }
          }
          if (!(methodBase1 != (MethodBase) null))
            break;
          this.MI = methodBase1;
          break;
      }
    }

    private void ResolveOverloadedMethod(RuntimeType t, string methodName, ArrayList argNames, ArrayList argValues)
    {
      MemberInfo[] member = t.GetMember(methodName, MemberTypes.Method, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
      int length = member.Length;
      switch (length)
      {
        case 1:
          this.MI = member[0] as MethodBase;
          break;
        case 0:
          break;
        default:
          MethodBase methodBase1 = (MethodBase) null;
          for (int index1 = 0; index1 < length; ++index1)
          {
            MethodBase methodBase2 = member[index1] as MethodBase;
            ParameterInfo[] parameters = methodBase2.GetParameters();
            if (parameters.Length == argValues.Count)
            {
              bool flag = true;
              for (int index2 = 0; index2 < parameters.Length; ++index2)
              {
                Type type = parameters[index2].ParameterType;
                if (type.IsByRef)
                  type = type.GetElementType();
                if (type != argValues[index2].GetType())
                {
                  flag = false;
                  break;
                }
              }
              if (flag)
              {
                methodBase1 = methodBase2;
                break;
              }
            }
          }
          if (methodBase1 == (MethodBase) null)
            throw new RemotingException(Environment.GetResourceString("Remoting_AmbiguousMethod"));
          this.MI = methodBase1;
          break;
      }
    }

    /// <summary>
    /// <see cref="M:System.Runtime.Remoting.Messaging.MethodCall.GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)" /> 方法未实现。</summary>
    /// <param name="info">用于序列化或反序列化远程对象的数据。</param>
    /// <param name="context">特定序列化流的上下文。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter, Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_Method"));
    }

    [SecurityCritical]
    internal void SetObjectFromSoapData(SerializationInfo info)
    {
      this.methodName = info.GetString("__methodName");
      ArrayList arrayList = (ArrayList) info.GetValue("__paramNameList", typeof (ArrayList));
      Hashtable keyToNamespaceTable = (Hashtable) info.GetValue("__keyToNamespaceTable", typeof (Hashtable));
      if (this.MI == (MethodBase) null)
      {
        ArrayList argValues = new ArrayList();
        ArrayList argNames = arrayList;
        for (int index = 0; index < argNames.Count; ++index)
          argValues.Add(info.GetValue((string) argNames[index], typeof (object)));
        RuntimeType t = this.ResolveType() as RuntimeType;
        if (t == (RuntimeType) null)
          throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_BadType"), (object) this.typeName));
        this.ResolveOverloadedMethod(t, this.methodName, argNames, argValues);
        if (this.MI == (MethodBase) null)
          throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Message_MethodMissing"), (object) this.methodName, (object) this.typeName));
      }
      RemotingMethodCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(this.MI);
      ParameterInfo[] parameters = reflectionCachedData.Parameters;
      int[] marshalRequestArgMap = reflectionCachedData.MarshalRequestArgMap;
      object obj = this.InternalProperties == null ? (object) null : this.InternalProperties[(object) "__UnorderedParams"];
      this.args = new object[parameters.Length];
      if (obj != null && obj is bool && (bool) obj)
      {
        for (int index1 = 0; index1 < arrayList.Count; ++index1)
        {
          string name = (string) arrayList[index1];
          int index2 = -1;
          for (int index3 = 0; index3 < parameters.Length; ++index3)
          {
            if (name.Equals(parameters[index3].Name))
            {
              index2 = parameters[index3].Position;
              break;
            }
          }
          if (index2 == -1)
          {
            if (!name.StartsWith("__param", StringComparison.Ordinal))
              throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadSerialization"));
            index2 = int.Parse(name.Substring(7), (IFormatProvider) CultureInfo.InvariantCulture);
          }
          if (index2 >= this.args.Length)
            throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadSerialization"));
          this.args[index2] = Message.SoapCoerceArg(info.GetValue(name, typeof (object)), parameters[index2].ParameterType, keyToNamespaceTable);
        }
      }
      else
      {
        for (int index = 0; index < arrayList.Count; ++index)
        {
          string name = (string) arrayList[index];
          this.args[marshalRequestArgMap[index]] = Message.SoapCoerceArg(info.GetValue(name, typeof (object)), parameters[marshalRequestArgMap[index]].ParameterType, keyToNamespaceTable);
        }
        this.PopulateOutArguments(reflectionCachedData);
      }
    }

    [SecurityCritical]
    [PermissionSet(SecurityAction.Assert, Unrestricted = true)]
    private void PopulateOutArguments(RemotingMethodCachedData methodCache)
    {
      ParameterInfo[] parameters = methodCache.Parameters;
      foreach (int outOnlyArg in methodCache.OutOnlyArgMap)
      {
        Type elementType = parameters[outOnlyArg].ParameterType.GetElementType();
        if (elementType.IsValueType)
          this.args[outOnlyArg] = Activator.CreateInstance(elementType, true);
      }
    }

    /// <summary>初始化一个 <see cref="T:System.Runtime.Remoting.Messaging.MethodCall" />。</summary>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public virtual void Init()
    {
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
      return this.args[argNum];
    }

    /// <summary>获取指定索引处的方法参数的名称。</summary>
    /// <returns>方法参数的名称。</returns>
    /// <param name="index">请求的参数的索引。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public string GetArgName(int index)
    {
      this.ResolveMethod();
      return InternalRemotingServices.GetReflectionCachedData(this.MI).Parameters[index].Name;
    }

    /// <summary>获取指定索引处未标记为 out 参数的方法参数。</summary>
    /// <returns>未标记为 out 参数的方法参数。</returns>
    /// <param name="argNum">请求的参数的索引。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public object GetInArg(int argNum)
    {
      if (this.argMapper == null)
        this.argMapper = new ArgMapper((IMethodMessage) this, false);
      return this.argMapper.GetArg(argNum);
    }

    /// <summary>获取指定索引处未标记为 out 参数的方法参数的名称。</summary>
    /// <returns>未标记为 out 参数的方法参数的名称。</returns>
    /// <param name="index">请求的参数的索引。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public string GetInArgName(int index)
    {
      if (this.argMapper == null)
        this.argMapper = new ArgMapper((IMethodMessage) this, false);
      return this.argMapper.GetArgName(index);
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
    internal virtual bool FillSpecialHeader(string key, object value)
    {
      if (key != null)
      {
        if (key.Equals("__Uri"))
          this.uri = (string) value;
        else if (key.Equals("__MethodName"))
          this.methodName = (string) value;
        else if (key.Equals("__MethodSignature"))
          this.methodSignature = (Type[]) value;
        else if (key.Equals("__TypeName"))
          this.typeName = (string) value;
        else if (key.Equals("__Args"))
        {
          this.args = (object[]) value;
        }
        else
        {
          if (!key.Equals("__CallContext"))
            return false;
          if (value is string)
          {
            this.callContext = new LogicalCallContext();
            this.callContext.RemotingData.LogicalCallID = (string) value;
          }
          else
            this.callContext = (LogicalCallContext) value;
        }
      }
      return true;
    }

    [SecurityCritical]
    internal void FillHeader(string key, object value)
    {
      if (this.FillSpecialHeader(key, value))
        return;
      if (this.InternalProperties == null)
        this.InternalProperties = (IDictionary) new Hashtable();
      this.InternalProperties[(object) key] = value;
    }

    /// <summary>从应用到一个方法的远程处理标头数组初始化一个内部序列化处理程序。</summary>
    /// <returns>内部序列化处理程序。</returns>
    /// <param name="h">包含键/值对的远程处理标头数组。该数组用于初始化属于“http://schemas.microsoft.com/clr/soap/messageProperties”命名空间的标头的 <see cref="T:System.Runtime.Remoting.Messaging.MethodCall" /> 字段。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public virtual object HeaderHandler(Header[] h)
    {
      SerializationMonkey serializationMonkey = (SerializationMonkey) FormatterServices.GetUninitializedObject(typeof (SerializationMonkey));
      Header[] h1;
      if (h != null && h.Length != 0 && h[0].Name == "__methodName")
      {
        this.methodName = (string) h[0].Value;
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
      this.FillHeaders(h1, true);
      this.ResolveMethod(false);
      serializationMonkey._obj = (ISerializationRootObject) this;
      if (this.MI != (MethodBase) null)
      {
        ArgMapper argMapper = new ArgMapper(this.MI, false);
        serializationMonkey.fieldNames = argMapper.ArgNames;
        serializationMonkey.fieldTypes = argMapper.ArgTypes;
      }
      return (object) serializationMonkey;
    }
  }
}
