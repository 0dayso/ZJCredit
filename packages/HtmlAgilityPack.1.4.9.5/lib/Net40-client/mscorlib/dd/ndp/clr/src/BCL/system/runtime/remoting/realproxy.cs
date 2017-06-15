// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Proxies.RealProxy
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Metadata;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;

namespace System.Runtime.Remoting.Proxies
{
  /// <summary>提供代理的基本功能。</summary>
  [SecurityCritical]
  [ComVisible(true)]
  [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
  public abstract class RealProxy
  {
    private static IntPtr _defaultStub = RealProxy.GetDefaultStub();
    private static IntPtr _defaultStubValue = new IntPtr(-1);
    private static object _defaultStubData = (object) RealProxy._defaultStubValue;
    private object _tp;
    private object _identity;
    private MarshalByRefObject _serverObject;
    private RealProxyFlags _flags;
    internal GCHandle _srvIdentity;
    internal int _optFlags;
    internal int _domainID;

    internal bool Initialized
    {
      get
      {
        return (this._flags & RealProxyFlags.Initialized) == RealProxyFlags.Initialized;
      }
      set
      {
        if (value)
          this._flags = this._flags | RealProxyFlags.Initialized;
        else
          this._flags = this._flags & ~RealProxyFlags.Initialized;
      }
    }

    internal MarshalByRefObject UnwrappedServerObject
    {
      get
      {
        return this._serverObject;
      }
    }

    internal virtual Identity IdentityObject
    {
      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)] get
      {
        return (Identity) this._identity;
      }
      set
      {
        this._identity = (object) value;
      }
    }

    [SecuritySafeCritical]
    static RealProxy()
    {
    }

    /// <summary>初始化表示指定的 <see cref="T:System.Type" /> 的远程对象的 <see cref="T:System.Runtime.Remoting.Proxies.RealProxy" /> 类的新实例。</summary>
    /// <param name="classToProxy">为其创建代理的远程对象的 <see cref="T:System.Type" />。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="classToProxy" /> 不是一个接口，并且不是从 <see cref="T:System.MarshalByRefObject" /> 派生的。</exception>
    [SecurityCritical]
    protected RealProxy(Type classToProxy)
      : this(classToProxy, (IntPtr) 0, (object) null)
    {
    }

    /// <summary>初始化 <see cref="T:System.Runtime.Remoting.Proxies.RealProxy" /> 类的新实例。</summary>
    /// <param name="classToProxy">为其创建代理的远程对象的 <see cref="T:System.Type" />。</param>
    /// <param name="stub">与新的代理实例相关联的存根 (Stub)。</param>
    /// <param name="stubData">为指定的存根 (Stub) 和新的代理实例设置的存根 (Stub) 数据。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="classToProxy" /> 不是一个接口，并且不是从 <see cref="T:System.MarshalByRefObject" /> 派生的。</exception>
    [SecurityCritical]
    protected RealProxy(Type classToProxy, IntPtr stub, object stubData)
    {
      if (!classToProxy.IsMarshalByRef && !classToProxy.IsInterface)
        throw new ArgumentException(Environment.GetResourceString("Remoting_Proxy_ProxyTypeIsNotMBR"));
      if ((IntPtr) 0 == stub)
      {
        stub = RealProxy._defaultStub;
        stubData = RealProxy._defaultStubData;
      }
      this._tp = (object) null;
      if (stubData == null)
        throw new ArgumentNullException("stubdata");
      this._tp = RemotingServices.CreateTransparentProxy(this, classToProxy, stub, stubData);
      if (!(this is RemotingProxy))
        return;
      this._flags = this._flags | RealProxyFlags.RemotingProxy;
    }

    /// <summary>使用默认值初始化 <see cref="T:System.Runtime.Remoting.Proxies.RealProxy" /> 类的新实例。</summary>
    protected RealProxy()
    {
    }

    internal bool IsRemotingProxy()
    {
      return (this._flags & RealProxyFlags.RemotingProxy) == RealProxyFlags.RemotingProxy;
    }

    /// <summary>用指定的 <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" /> 初始化 <see cref="T:System.Runtime.Remoting.Proxies.RealProxy" /> 的当前实例所表示的远程对象的对象 <see cref="T:System.Type" /> 的新实例。</summary>
    /// <returns>构造请求的结果。</returns>
    /// <param name="ctorMsg">构造调用消息，它包含当前 <see cref="T:System.Runtime.Remoting.Proxies.RealProxy" /> 所表示的远程对象的新实例的构造函数参数。可以为 null。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有 UnmanagedCode 权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    [ComVisible(true)]
    public IConstructionReturnMessage InitializeServerObject(IConstructionCallMessage ctorMsg)
    {
      IConstructionReturnMessage constructionReturnMessage = (IConstructionReturnMessage) null;
      if (this._serverObject == null)
      {
        Type proxiedType = this.GetProxiedType();
        if (ctorMsg != null && ctorMsg.ActivationType != proxiedType)
          throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Proxy_BadTypeForActivation"), (object) proxiedType.FullName, (object) ctorMsg.ActivationType));
        this._serverObject = RemotingServices.AllocateUninitializedObject(proxiedType);
        this.SetContextForDefaultStub();
        MarshalByRefObject marshalByRefObject = (MarshalByRefObject) this.GetTransparentProxy();
        IMethodReturnMessage methodReturnMessage = (IMethodReturnMessage) null;
        Exception e = (Exception) null;
        if (ctorMsg != null)
        {
          methodReturnMessage = RemotingServices.ExecuteMessage(marshalByRefObject, (IMethodCallMessage) ctorMsg);
          e = methodReturnMessage.Exception;
        }
        else
        {
          try
          {
            RemotingServices.CallDefaultCtor((object) marshalByRefObject);
          }
          catch (Exception ex)
          {
            e = ex;
          }
        }
        if (e == null)
        {
          object[] outArgs = methodReturnMessage == null ? (object[]) null : methodReturnMessage.OutArgs;
          int outArgsCount = outArgs == null ? 0 : outArgs.Length;
          LogicalCallContext callCtx = methodReturnMessage == null ? (LogicalCallContext) null : methodReturnMessage.LogicalCallContext;
          constructionReturnMessage = (IConstructionReturnMessage) new ConstructorReturnMessage(marshalByRefObject, outArgs, outArgsCount, callCtx, ctorMsg);
          this.SetupIdentity();
          if (this.IsRemotingProxy())
            this.Initialized = true;
        }
        else
          constructionReturnMessage = (IConstructionReturnMessage) new ConstructorReturnMessage(e, ctorMsg);
      }
      return constructionReturnMessage;
    }

    /// <summary>返回当前代理实例所表示的服务器对象。</summary>
    /// <returns>当前代理实例所表示的服务器对象。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有 UnmanagedCode 权限。</exception>
    [SecurityCritical]
    protected MarshalByRefObject GetUnwrappedServer()
    {
      return this.UnwrappedServerObject;
    }

    /// <summary>将当前代理实例从它所表示的远程服务器对象分离。</summary>
    /// <returns>被分离的服务器对象。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有 UnmanagedCode 权限。</exception>
    [SecurityCritical]
    protected MarshalByRefObject DetachServer()
    {
      object transparentProxy = this.GetTransparentProxy();
      if (transparentProxy != null)
        RemotingServices.ResetInterfaceCache(transparentProxy);
      MarshalByRefObject marshalByRefObject = this._serverObject;
      this._serverObject = (MarshalByRefObject) null;
      marshalByRefObject.__ResetServerIdentity();
      return marshalByRefObject;
    }

    /// <summary>将当前代理实例附加到指定的远程 <see cref="T:System.MarshalByRefObject" />。</summary>
    /// <param name="s">当前代理实例所表示的 <see cref="T:System.MarshalByRefObject" />。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有 UnmanagedCode 权限。</exception>
    [SecurityCritical]
    protected void AttachServer(MarshalByRefObject s)
    {
      object transparentProxy = this.GetTransparentProxy();
      if (transparentProxy != null)
        RemotingServices.ResetInterfaceCache(transparentProxy);
      this.AttachServerHelper(s);
    }

    [SecurityCritical]
    private void SetupIdentity()
    {
      if (this._identity != null)
        return;
      this._identity = (object) IdentityHolder.FindOrCreateServerIdentity(this._serverObject, (string) null, 0);
      ((Identity) this._identity).RaceSetTransparentProxy(this.GetTransparentProxy());
    }

    [SecurityCritical]
    private void SetContextForDefaultStub()
    {
      if (!(this.GetStub() == RealProxy._defaultStub))
        return;
      object stubData = RealProxy.GetStubData(this);
      if (!(stubData is IntPtr) || !((IntPtr) stubData).Equals((object) RealProxy._defaultStubValue))
        return;
      RealProxy.SetStubData(this, (object) Thread.CurrentContext.InternalContextID);
    }

    [SecurityCritical]
    internal bool DoContextsMatch()
    {
      bool flag = false;
      if (this.GetStub() == RealProxy._defaultStub)
      {
        object stubData = RealProxy.GetStubData(this);
        if (stubData is IntPtr && ((IntPtr) stubData).Equals((object) Thread.CurrentContext.InternalContextID))
          flag = true;
      }
      return flag;
    }

    [SecurityCritical]
    internal void AttachServerHelper(MarshalByRefObject s)
    {
      if (s == null || this._serverObject != null)
        throw new ArgumentException(Environment.GetResourceString("ArgumentNull_Generic"), "s");
      this._serverObject = s;
      this.SetupIdentity();
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    private IntPtr GetStub();

    /// <summary>为指定的代理设置存根 (Stub) 数据。</summary>
    /// <param name="rp">要为其设置存根 (Stub) 数据的代理。</param>
    /// <param name="stubData">新的存根 (Stub) 数据。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有 UnmanagedCode 权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern void SetStubData(RealProxy rp, object stubData);

    internal void SetSrvInfo(GCHandle srvIdentity, int domainID)
    {
      this._srvIdentity = srvIdentity;
      this._domainID = domainID;
    }

    /// <summary>检索为指定的代理存储的存根 (Stub) 数据。</summary>
    /// <returns>指定代理的存根 (Stub) 数据。</returns>
    /// <param name="rp">需要存根 (Stub) 数据的代理。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有 UnmanagedCode 权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern object GetStubData(RealProxy rp);

    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern IntPtr GetDefaultStub();

    /// <summary>返回 <see cref="T:System.Runtime.Remoting.Proxies.RealProxy" /> 的当前实例所表示的对象的 <see cref="T:System.Type" />。</summary>
    /// <returns>
    /// <see cref="T:System.Runtime.Remoting.Proxies.RealProxy" /> 的当前实例所表示的对象的 <see cref="T:System.Type" />。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public Type GetProxiedType();

    /// <summary>当在派生类中重写时，对当前实例所表示的远程对象调用在所提供的 <see cref="T:System.Runtime.Remoting.Messaging.IMessage" /> 中指定的方法。</summary>
    /// <returns>调用的方法所返回的消息，包含返回值和所有 out 或 ref 参数。</returns>
    /// <param name="msg">
    /// <see cref="T:System.Runtime.Remoting.Messaging.IMessage" />，包含有关方法调用的信息的 <see cref="T:System.Collections.IDictionary" />。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public abstract IMessage Invoke(IMessage msg);

    /// <summary>为指定的对象类型创建 <see cref="T:System.Runtime.Remoting.ObjRef" />，并将其作为客户端激活的对象注册到远程处理结构。</summary>
    /// <returns>为指定类型创建的 <see cref="T:System.Runtime.Remoting.ObjRef" /> 的新实例。</returns>
    /// <param name="requestedType">为其创建 <see cref="T:System.Runtime.Remoting.ObjRef" /> 的对象类型。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public virtual ObjRef CreateObjRef(Type requestedType)
    {
      if (this._identity == null)
        throw new RemotingException(Environment.GetResourceString("Remoting_NoIdentityEntry"));
      return new ObjRef((MarshalByRefObject) this.GetTransparentProxy(), requestedType);
    }

    /// <summary>将 <see cref="T:System.Runtime.Remoting.Proxies.RealProxy" /> 的当前实例所表示的对象的透明代理添加到指定的 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 中。</summary>
    /// <param name="info">将该透明代理序列化到其中的 <see cref="T:System.Runtime.Serialization.SerializationInfo" />。</param>
    /// <param name="context">序列化的源和目标。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="info" /> 或 <paramref name="context" /> 参数为 null。</exception>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有 SerializationFormatter 权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      RemotingServices.GetObjectData(this.GetTransparentProxy(), info, context);
    }

    [SecurityCritical]
    private static void HandleReturnMessage(IMessage reqMsg, IMessage retMsg)
    {
      IMethodReturnMessage methodReturnMessage = retMsg as IMethodReturnMessage;
      if (retMsg == null || methodReturnMessage == null)
        throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadType"));
      Exception exception = methodReturnMessage.Exception;
      if (exception != null)
        throw exception.PrepForRemoting();
      if (retMsg is StackBasedReturnMessage)
        return;
      if (reqMsg is Message)
      {
        RealProxy.PropagateOutParameters(reqMsg, methodReturnMessage.Args, methodReturnMessage.ReturnValue);
      }
      else
      {
        if (!(reqMsg is ConstructorCallMessage))
          return;
        RealProxy.PropagateOutParameters(reqMsg, methodReturnMessage.Args, (object) null);
      }
    }

    [SecurityCritical]
    internal static void PropagateOutParameters(IMessage msg, object[] outArgs, object returnValue)
    {
      Message message = msg as Message;
      if (message == null)
      {
        ConstructorCallMessage constructorCallMessage = msg as ConstructorCallMessage;
        if (constructorCallMessage != null)
          message = constructorCallMessage.GetMessage();
      }
      if (message == null)
        throw new ArgumentException(Environment.GetResourceString("Remoting_Proxy_ExpectedOriginalMessage"));
      RemotingMethodCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(message.GetMethodBase());
      if (outArgs != null && outArgs.Length != 0)
      {
        object[] args = message.Args;
        ParameterInfo[] parameters = reflectionCachedData.Parameters;
        foreach (int marshalRequestArg in reflectionCachedData.MarshalRequestArgMap)
        {
          ParameterInfo parameterInfo = parameters[marshalRequestArg];
          if (parameterInfo.IsIn && parameterInfo.ParameterType.IsByRef && !parameterInfo.IsOut)
            outArgs[marshalRequestArg] = args[marshalRequestArg];
        }
        if (reflectionCachedData.NonRefOutArgMap.Length != 0)
        {
          foreach (int nonRefOutArg in reflectionCachedData.NonRefOutArgMap)
          {
            Array array = args[nonRefOutArg] as Array;
            if (array != null)
            {
              Array sourceArray = (Array) outArgs[nonRefOutArg];
              Array destinationArray = array;
              int length = destinationArray.Length;
              Array.Copy(sourceArray, destinationArray, length);
            }
          }
        }
        int[] outRefArgMap = reflectionCachedData.OutRefArgMap;
        if (outRefArgMap.Length != 0)
        {
          foreach (int index in outRefArgMap)
            RealProxy.ValidateReturnArg(outArgs[index], parameters[index].ParameterType);
        }
      }
      if ((message.GetCallType() & 15) != 1)
      {
        Type returnType = reflectionCachedData.ReturnType;
        if (returnType != (Type) null)
          RealProxy.ValidateReturnArg(returnValue, returnType);
      }
      message.PropagateOutParameters(outArgs, returnValue);
    }

    private static void ValidateReturnArg(object arg, Type paramType)
    {
      if (paramType.IsByRef)
        paramType = paramType.GetElementType();
      if (paramType.IsValueType)
      {
        if (arg == null)
        {
          if (!paramType.IsGenericType || !(paramType.GetGenericTypeDefinition() == typeof (Nullable<>)))
            throw new RemotingException(Environment.GetResourceString("Remoting_Proxy_ReturnValueTypeCannotBeNull"));
        }
        else if (!paramType.IsInstanceOfType(arg))
          throw new InvalidCastException(Environment.GetResourceString("Remoting_Proxy_BadReturnType"));
      }
      else if (arg != null && !paramType.IsInstanceOfType(arg))
        throw new InvalidCastException(Environment.GetResourceString("Remoting_Proxy_BadReturnType"));
    }

    [SecurityCritical]
    internal static IMessage EndInvokeHelper(Message reqMsg, bool bProxyCase)
    {
      AsyncResult asyncResult = reqMsg.GetAsyncResult() as AsyncResult;
      IMessage message = (IMessage) null;
      if (asyncResult == null)
        throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadAsyncResult"));
      if (asyncResult.AsyncDelegate != reqMsg.GetThisPtr())
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MismatchedAsyncResult"));
      if (!asyncResult.IsCompleted)
        asyncResult.AsyncWaitHandle.WaitOne(-1, Thread.CurrentContext.IsThreadPoolAware);
      lock (asyncResult)
      {
        if (asyncResult.EndInvokeCalled)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EndInvokeCalledMultiple"));
        asyncResult.EndInvokeCalled = true;
        IMethodReturnMessage local_4 = (IMethodReturnMessage) asyncResult.GetReplyMessage();
        if (!bProxyCase)
        {
          Exception local_5 = local_4.Exception;
          if (local_5 != null)
            throw local_5.PrepForRemoting();
          reqMsg.PropagateOutParameters(local_4.Args, local_4.ReturnValue);
        }
        else
          message = (IMessage) local_4;
        Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext.Merge(local_4.LogicalCallContext);
      }
      return message;
    }

    /// <summary>请求对由当前代理实例表示的对象的非托管引用。</summary>
    /// <returns>如果通过 COM 与当前进程中的非托管对象进行通信需要此对象引用，则为指向 COM 可调用包装 的指针；或者，如果封送处理到远程位置需要此对象引用，则为指向缓存的或新生成的 IUnknown COM 接口的指针。</returns>
    /// <param name="fIsMarshalled">如果封送处理到远程位置需要该对象引用，则为 true；如果通过 COM 与当前进程中的非托管对象进行通讯需要该对象引用，则为 false。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public virtual IntPtr GetCOMIUnknown(bool fIsMarshalled)
    {
      return MarshalByRefObject.GetComIUnknown((MarshalByRefObject) this.GetTransparentProxy());
    }

    /// <summary>存储当前实例所表示的对象的非托管代理。</summary>
    /// <param name="i">一个指针，指向当前代理实例所表示的对象的 IUnknown 接口。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public virtual void SetCOMIUnknown(IntPtr i)
    {
    }

    /// <summary>请求具有指定 ID 的 COM 接口。</summary>
    /// <returns>指向所请求的接口的指针。</returns>
    /// <param name="iid">对所请求的接口的引用。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public virtual IntPtr SupportsInterface(ref Guid iid)
    {
      return IntPtr.Zero;
    }

    /// <summary>返回 <see cref="T:System.Runtime.Remoting.Proxies.RealProxy" /> 的当前实例的透明代理。</summary>
    /// <returns>当前代理实例的透明代理。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public virtual object GetTransparentProxy()
    {
      return this._tp;
    }

    [SecurityCritical]
    private void PrivateInvoke(ref MessageData msgData, int type)
    {
      IMessage message1 = (IMessage) null;
      CallType callType = (CallType) type;
      IMessage message2 = (IMessage) null;
      int msgFlags = -1;
      RemotingProxy remotingProxy = (RemotingProxy) null;
      if (CallType.MethodCall == callType)
      {
        Message message3 = new Message();
        MessageData msgData1 = msgData;
        message3.InitFields(msgData1);
        message1 = (IMessage) message3;
        msgFlags = message3.GetCallType();
      }
      else if (CallType.ConstructorCall == callType)
      {
        msgFlags = 0;
        remotingProxy = this as RemotingProxy;
        bool flag = false;
        ConstructorCallMessage constructorCallMessage1;
        if (!this.IsRemotingProxy())
        {
          constructorCallMessage1 = new ConstructorCallMessage((object[]) null, (object[]) null, (object[]) null, (RuntimeType) this.GetProxiedType());
        }
        else
        {
          constructorCallMessage1 = remotingProxy.ConstructorMessage;
          Identity identityObject = remotingProxy.IdentityObject;
          if (identityObject != null)
            flag = identityObject.IsWellKnown();
        }
        if (constructorCallMessage1 == null | flag)
        {
          ConstructorCallMessage constructorCallMessage2 = new ConstructorCallMessage((object[]) null, (object[]) null, (object[]) null, (RuntimeType) this.GetProxiedType());
          constructorCallMessage2.SetFrame(msgData);
          message1 = (IMessage) constructorCallMessage2;
          if (flag)
          {
            remotingProxy.ConstructorMessage = (ConstructorCallMessage) null;
            if (constructorCallMessage2.ArgCount != 0)
              throw new RemotingException(Environment.GetResourceString("Remoting_Activation_WellKnownCTOR"));
          }
          message2 = (IMessage) new ConstructorReturnMessage((MarshalByRefObject) this.GetTransparentProxy(), (object[]) null, 0, (LogicalCallContext) null, (IConstructionCallMessage) constructorCallMessage2);
        }
        else
        {
          constructorCallMessage1.SetFrame(msgData);
          message1 = (IMessage) constructorCallMessage1;
        }
      }
      ChannelServices.IncrementRemoteCalls();
      if (!this.IsRemotingProxy() && (msgFlags & 2) == 2)
        message2 = RealProxy.EndInvokeHelper(message1 as Message, true);
      if (message2 == null)
      {
        Thread currentThread = Thread.CurrentThread;
        LogicalCallContext logicalCallContext = currentThread.GetMutableExecutionContext().LogicalCallContext;
        this.SetCallContextInMessage(message1, msgFlags, logicalCallContext);
        logicalCallContext.PropagateOutgoingHeadersToMessage(message1);
        message2 = this.Invoke(message1);
        this.ReturnCallContextToThread(currentThread, message2, msgFlags, logicalCallContext);
        Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext.PropagateIncomingHeadersToCallContext(message2);
      }
      if (!this.IsRemotingProxy() && (msgFlags & 1) == 1)
      {
        Message m = message1 as Message;
        AsyncResult asyncResult = new AsyncResult(m);
        IMessage msg = message2;
        asyncResult.SyncProcessMessage(msg);
        // ISSUE: variable of the null type
        __Null local1 = null;
        int outArgsCount = 0;
        // ISSUE: variable of the null type
        __Null local2 = null;
        Message message3 = m;
        message2 = (IMessage) new ReturnMessage((object) asyncResult, (object[]) local1, outArgsCount, (LogicalCallContext) local2, (IMethodCallMessage) message3);
      }
      RealProxy.HandleReturnMessage(message1, message2);
      if (CallType.ConstructorCall != callType)
        return;
      IConstructionReturnMessage constructionReturnMessage = message2 as IConstructionReturnMessage;
      if (constructionReturnMessage == null)
        throw new RemotingException(Environment.GetResourceString("Remoting_Proxy_BadReturnTypeForActivation"));
      ConstructorReturnMessage constructorReturnMessage = constructionReturnMessage as ConstructorReturnMessage;
      MarshalByRefObject marshalByRefObject;
      if (constructorReturnMessage != null)
      {
        marshalByRefObject = (MarshalByRefObject) constructorReturnMessage.GetObject();
        if (marshalByRefObject == null)
          throw new RemotingException(Environment.GetResourceString("Remoting_Activation_NullReturnValue"));
      }
      else
      {
        marshalByRefObject = (MarshalByRefObject) RemotingServices.InternalUnmarshal((ObjRef) constructionReturnMessage.ReturnValue, this.GetTransparentProxy(), true);
        if (marshalByRefObject == null)
          throw new RemotingException(Environment.GetResourceString("Remoting_Activation_NullFromInternalUnmarshal"));
      }
      if (marshalByRefObject != (MarshalByRefObject) this.GetTransparentProxy())
        throw new RemotingException(Environment.GetResourceString("Remoting_Activation_InconsistentState"));
      if (!this.IsRemotingProxy())
        return;
      remotingProxy.ConstructorMessage = (ConstructorCallMessage) null;
    }

    private void SetCallContextInMessage(IMessage reqMsg, int msgFlags, LogicalCallContext cctx)
    {
      Message message = reqMsg as Message;
      if (msgFlags != 0)
        return;
      if (message != null)
        message.SetLogicalCallContext(cctx);
      else
        ((ConstructorCallMessage) reqMsg).SetLogicalCallContext(cctx);
    }

    [SecurityCritical]
    private void ReturnCallContextToThread(Thread currentThread, IMessage retMsg, int msgFlags, LogicalCallContext currCtx)
    {
      if (msgFlags != 0 || retMsg == null)
        return;
      IMethodReturnMessage methodReturnMessage = retMsg as IMethodReturnMessage;
      if (methodReturnMessage == null)
        return;
      LogicalCallContext logicalCallContext1 = methodReturnMessage.LogicalCallContext;
      if (logicalCallContext1 == null)
      {
        currentThread.GetMutableExecutionContext().LogicalCallContext = currCtx;
      }
      else
      {
        if (methodReturnMessage is StackBasedReturnMessage)
          return;
        ExecutionContext executionContext = currentThread.GetMutableExecutionContext();
        LogicalCallContext logicalCallContext2 = executionContext.LogicalCallContext;
        LogicalCallContext logicalCallContext3 = logicalCallContext1;
        executionContext.LogicalCallContext = logicalCallContext3;
        if (logicalCallContext2 == logicalCallContext1)
          return;
        IPrincipal principal = logicalCallContext2.Principal;
        if (principal == null)
          return;
        logicalCallContext1.Principal = principal;
      }
    }

    [SecurityCritical]
    internal virtual void Wrap()
    {
      ServerIdentity serverIdentity = this._identity as ServerIdentity;
      if (serverIdentity == null || !(this is RemotingProxy))
        return;
      RealProxy.SetStubData(this, (object) serverIdentity.ServerContext.InternalContextID);
    }
  }
}
