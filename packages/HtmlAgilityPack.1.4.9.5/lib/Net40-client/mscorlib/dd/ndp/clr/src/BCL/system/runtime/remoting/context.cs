// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Contexts.Context
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Runtime.Remoting.Contexts
{
  /// <summary>为驻留在其中的对象定义环境，在此环境中可以实施策略。</summary>
  [ComVisible(true)]
  public class Context
  {
    private static DynamicPropertyHolder _dphGlobal = new DynamicPropertyHolder();
    private static LocalDataStoreMgr _localDataStoreMgr = new LocalDataStoreMgr();
    private static int _ctxIDCounter = 0;
    internal const int CTX_DEFAULT_CONTEXT = 1;
    internal const int CTX_FROZEN = 2;
    internal const int CTX_THREADPOOL_AWARE = 4;
    private const int GROW_BY = 8;
    private const int STATICS_BUCKET_SIZE = 8;
    private IContextProperty[] _ctxProps;
    private DynamicPropertyHolder _dphCtx;
    private volatile LocalDataStoreHolder _localDataStore;
    private IMessageSink _serverContextChain;
    private IMessageSink _clientContextChain;
    private AppDomain _appDomain;
    private object[] _ctxStatics;
    private IntPtr _internalContext;
    private int _ctxID;
    private int _ctxFlags;
    private int _numCtxProps;
    private int _ctxStaticsCurrentBucket;
    private int _ctxStaticsFreeIndex;

    /// <summary>获取当前上下文的上下文 ID。</summary>
    /// <returns>当前上下文的上下文 ID。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public virtual int ContextID
    {
      [SecurityCritical] get
      {
        return this._ctxID;
      }
    }

    internal virtual IntPtr InternalContextID
    {
      get
      {
        return this._internalContext;
      }
    }

    internal virtual AppDomain AppDomain
    {
      get
      {
        return this._appDomain;
      }
    }

    internal bool IsDefaultContext
    {
      get
      {
        return this._ctxID == 0;
      }
    }

    /// <summary>获取当前应用程序域的默认上下文。</summary>
    /// <returns>
    /// <see cref="T:System.AppDomain" /> 命名空间的默认上下文。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public static Context DefaultContext
    {
      [SecurityCritical] get
      {
        return Thread.GetDomain().GetDefaultContext();
      }
    }

    internal virtual bool IsThreadPoolAware
    {
      get
      {
        return (this._ctxFlags & 4) == 4;
      }
    }

    /// <summary>获取当前上下文属性的数组。</summary>
    /// <returns>当前上下文属性的数组；如果没有以任何属性对上下文进行特性化，则为 null。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public virtual IContextProperty[] ContextProperties
    {
      [SecurityCritical] get
      {
        if (this._ctxProps == null)
          return (IContextProperty[]) null;
        lock (this)
        {
          IContextProperty[] local_2 = new IContextProperty[this._numCtxProps];
          Array.Copy((Array) this._ctxProps, (Array) local_2, this._numCtxProps);
          return local_2;
        }
      }
    }

    private LocalDataStore MyLocalStore
    {
      get
      {
        if (this._localDataStore == null)
        {
          lock (Context._localDataStoreMgr)
          {
            if (this._localDataStore == null)
              this._localDataStore = Context._localDataStoreMgr.CreateLocalDataStore();
          }
        }
        return this._localDataStore.Store;
      }
    }

    internal virtual IDynamicProperty[] PerContextDynamicProperties
    {
      get
      {
        if (this._dphCtx == null)
          return (IDynamicProperty[]) null;
        return this._dphCtx.DynamicProperties;
      }
    }

    internal static ArrayWithSize GlobalDynamicSinks
    {
      [SecurityCritical] get
      {
        return Context._dphGlobal.DynamicSinks;
      }
    }

    internal virtual ArrayWithSize DynamicSinks
    {
      [SecurityCritical] get
      {
        if (this._dphCtx == null)
          return (ArrayWithSize) null;
        return this._dphCtx.DynamicSinks;
      }
    }

    /// <summary>初始化 <see cref="T:System.Runtime.Remoting.Contexts.Context" /> 类的新实例。</summary>
    [SecurityCritical]
    public Context()
      : this(0)
    {
    }

    [SecurityCritical]
    private Context(int flags)
    {
      this._ctxFlags = flags;
      this._ctxID = (this._ctxFlags & 1) == 0 ? Interlocked.Increment(ref Context._ctxIDCounter) : 0;
      DomainSpecificRemotingData remotingData = Thread.GetDomain().RemotingData;
      if (remotingData != null)
      {
        IContextProperty[] contextProperties = remotingData.AppDomainContextProperties;
        if (contextProperties != null)
        {
          for (int index = 0; index < contextProperties.Length; ++index)
            this.SetProperty(contextProperties[index]);
        }
      }
      if ((this._ctxFlags & 1) != 0)
        this.Freeze();
      this.SetupInternalContext((this._ctxFlags & 1) == 1);
    }

    /// <summary>清理非默认上下文的后备对象。</summary>
    [SecuritySafeCritical]
    ~Context()
    {
      if (!(this._internalContext != IntPtr.Zero) || (this._ctxFlags & 1) != 0)
        return;
      this.CleanupInternalContext();
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private void SetupInternalContext(bool bDefault);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private void CleanupInternalContext();

    [SecurityCritical]
    internal static Context CreateDefaultContext()
    {
      return new Context(1);
    }

    /// <summary>返回由名称指定的特定上下文属性。</summary>
    /// <returns>指定的上下文属性。</returns>
    /// <param name="name">属性的名称。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public virtual IContextProperty GetProperty(string name)
    {
      if (this._ctxProps == null || name == null)
        return (IContextProperty) null;
      IContextProperty contextProperty = (IContextProperty) null;
      for (int index = 0; index < this._numCtxProps; ++index)
      {
        if (this._ctxProps[index].Name.Equals(name))
        {
          contextProperty = this._ctxProps[index];
          break;
        }
      }
      return contextProperty;
    }

    /// <summary>通过名称设置特定的上下文属性。</summary>
    /// <param name="prop">实际的上下文属性。</param>
    /// <exception cref="T:System.InvalidOperationException">尝试将属性添加到默认上下文。</exception>
    /// <exception cref="T:System.InvalidOperationException">上下文被冻结。</exception>
    /// <exception cref="T:System.ArgumentNullException">属性或属性名称为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public virtual void SetProperty(IContextProperty prop)
    {
      if (prop == null || prop.Name == null)
        throw new ArgumentNullException(prop == null ? "prop" : "property name");
      if ((this._ctxFlags & 2) != 0)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_AddContextFrozen"));
      lock (this)
      {
        Context.CheckPropertyNameClash(prop.Name, this._ctxProps, this._numCtxProps);
        if (this._ctxProps == null || this._numCtxProps == this._ctxProps.Length)
          this._ctxProps = Context.GrowPropertiesArray(this._ctxProps);
        IContextProperty[] temp_28 = this._ctxProps;
        int local_2 = this._numCtxProps;
        this._numCtxProps = local_2 + 1;
        int temp_35 = local_2;
        IContextProperty temp_36 = prop;
        temp_28[temp_35] = temp_36;
      }
    }

    [SecurityCritical]
    internal virtual void InternalFreeze()
    {
      this._ctxFlags = this._ctxFlags | 2;
      for (int index = 0; index < this._numCtxProps; ++index)
        this._ctxProps[index].Freeze(this);
    }

    /// <summary>冻结上下文，使其无法从当前上下文添加或移除上下文属性。</summary>
    /// <exception cref="T:System.InvalidOperationException">上下文已经冻结。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public virtual void Freeze()
    {
      lock (this)
      {
        if ((this._ctxFlags & 2) != 0)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ContextAlreadyFrozen"));
        this.InternalFreeze();
      }
    }

    internal virtual void SetThreadPoolAware()
    {
      this._ctxFlags = this._ctxFlags | 4;
    }

    [SecurityCritical]
    internal static void CheckPropertyNameClash(string name, IContextProperty[] props, int count)
    {
      for (int index = 0; index < count; ++index)
      {
        if (props[index].Name.Equals(name))
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_DuplicatePropertyName"));
      }
    }

    internal static IContextProperty[] GrowPropertiesArray(IContextProperty[] props)
    {
      IContextProperty[] contextPropertyArray = new IContextProperty[(props != null ? props.Length : 0) + 8];
      if (props != null)
        Array.Copy((Array) props, (Array) contextPropertyArray, props.Length);
      return contextPropertyArray;
    }

    [SecurityCritical]
    internal virtual IMessageSink GetServerContextChain()
    {
      if (this._serverContextChain == null)
      {
        IMessageSink nextSink = ServerContextTerminatorSink.MessageSink;
        int index = this._numCtxProps;
        while (index-- > 0)
        {
          IContributeServerContextSink serverContextSink = this._ctxProps[index] as IContributeServerContextSink;
          if (serverContextSink != null)
          {
            nextSink = serverContextSink.GetServerContextSink(nextSink);
            if (nextSink == null)
              throw new RemotingException(Environment.GetResourceString("Remoting_Contexts_BadProperty"));
          }
        }
        lock (this)
        {
          if (this._serverContextChain == null)
            this._serverContextChain = nextSink;
        }
      }
      return this._serverContextChain;
    }

    [SecurityCritical]
    internal virtual IMessageSink GetClientContextChain()
    {
      if (this._clientContextChain == null)
      {
        IMessageSink nextSink = ClientContextTerminatorSink.MessageSink;
        for (int index = 0; index < this._numCtxProps; ++index)
        {
          IContributeClientContextSink clientContextSink = this._ctxProps[index] as IContributeClientContextSink;
          if (clientContextSink != null)
          {
            nextSink = clientContextSink.GetClientContextSink(nextSink);
            if (nextSink == null)
              throw new RemotingException(Environment.GetResourceString("Remoting_Contexts_BadProperty"));
          }
        }
        lock (this)
        {
          if (this._clientContextChain == null)
            this._clientContextChain = nextSink;
        }
      }
      return this._clientContextChain;
    }

    [SecurityCritical]
    internal virtual IMessageSink CreateServerObjectChain(MarshalByRefObject serverObj)
    {
      IMessageSink nextSink = (IMessageSink) new ServerObjectTerminatorSink(serverObj);
      int index = this._numCtxProps;
      while (index-- > 0)
      {
        IContributeObjectSink contributeObjectSink = this._ctxProps[index] as IContributeObjectSink;
        if (contributeObjectSink != null)
        {
          nextSink = contributeObjectSink.GetObjectSink(serverObj, nextSink);
          if (nextSink == null)
            throw new RemotingException(Environment.GetResourceString("Remoting_Contexts_BadProperty"));
        }
      }
      return nextSink;
    }

    [SecurityCritical]
    internal virtual IMessageSink CreateEnvoyChain(MarshalByRefObject objectOrProxy)
    {
      IMessageSink nextSink = EnvoyTerminatorSink.MessageSink;
      int index = 0;
      MarshalByRefObject marshalByRefObject = objectOrProxy;
      for (; index < this._numCtxProps; ++index)
      {
        IContributeEnvoySink contributeEnvoySink = this._ctxProps[index] as IContributeEnvoySink;
        if (contributeEnvoySink != null)
        {
          nextSink = contributeEnvoySink.GetEnvoySink(marshalByRefObject, nextSink);
          if (nextSink == null)
            throw new RemotingException(Environment.GetResourceString("Remoting_Contexts_BadProperty"));
        }
      }
      return nextSink;
    }

    [SecurityCritical]
    internal IMessage NotifyActivatorProperties(IMessage msg, bool bServerSide)
    {
      IMessage message = (IMessage) null;
      try
      {
        int index = this._numCtxProps;
        while (index-- != 0)
        {
          IContextPropertyActivator propertyActivator = this._ctxProps[index] as IContextPropertyActivator;
          if (propertyActivator != null)
          {
            IConstructionCallMessage msg1 = msg as IConstructionCallMessage;
            if (msg1 != null)
            {
              if (!bServerSide)
                propertyActivator.CollectFromClientContext(msg1);
              else
                propertyActivator.DeliverClientContextToServerContext(msg1);
            }
            else if (bServerSide)
              propertyActivator.CollectFromServerContext((IConstructionReturnMessage) msg);
            else
              propertyActivator.DeliverServerContextToClientContext((IConstructionReturnMessage) msg);
          }
        }
      }
      catch (Exception ex)
      {
        IMethodCallMessage mcm = !(msg is IConstructionCallMessage) ? (IMethodCallMessage) new ErrorMessage() : (IMethodCallMessage) msg;
        message = (IMessage) new ReturnMessage(ex, mcm);
        if (msg != null)
          ((ReturnMessage) message).SetLogicalCallContext((LogicalCallContext) msg.Properties[(object) Message.CallContextKey]);
      }
      return message;
    }

    /// <summary>返回当前上下文的 <see cref="T:System.String" /> 类表示形式。</summary>
    /// <returns>当前上下文的 <see cref="T:System.String" /> 类表示形式。</returns>
    public override string ToString()
    {
      return "ContextID: " + (object) this._ctxID;
    }

    /// <summary>执行另一上下文中的代码。</summary>
    /// <param name="deleg">用于请求回调的委托。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public void DoCallBack(CrossContextDelegate deleg)
    {
      if (deleg == null)
        throw new ArgumentNullException("deleg");
      if ((this._ctxFlags & 2) == 0)
        throw new RemotingException(Environment.GetResourceString("Remoting_Contexts_ContextNotFrozenForCallBack"));
      Context currentContext = Thread.CurrentContext;
      if (currentContext == this)
      {
        deleg();
      }
      else
      {
        currentContext.DoCallBackGeneric(this.InternalContextID, deleg);
        GC.KeepAlive((object) this);
      }
    }

    [SecurityCritical]
    internal static void DoCallBackFromEE(IntPtr targetCtxID, IntPtr privateData, int targetDomainID)
    {
      if (targetDomainID == 0)
      {
        CrossContextDelegate deleg = new CrossContextDelegate(new CallBackHelper(privateData, true, targetDomainID).Func);
        Thread.CurrentContext.DoCallBackGeneric(targetCtxID, deleg);
      }
      else
      {
        TransitionCall transitionCall = new TransitionCall(targetCtxID, privateData, targetDomainID);
        Message.PropagateCallContextFromThreadToMessage((IMessage) transitionCall);
        IMessage msg = Thread.CurrentContext.GetClientContextChain().SyncProcessMessage((IMessage) transitionCall);
        Message.PropagateCallContextFromMessageToThread(msg);
        IMethodReturnMessage methodReturnMessage = msg as IMethodReturnMessage;
        if (methodReturnMessage != null && methodReturnMessage.Exception != null)
          throw methodReturnMessage.Exception;
      }
    }

    [SecurityCritical]
    internal void DoCallBackGeneric(IntPtr targetCtxID, CrossContextDelegate deleg)
    {
      TransitionCall transitionCall = new TransitionCall(targetCtxID, deleg);
      Message.PropagateCallContextFromThreadToMessage((IMessage) transitionCall);
      IMessage msg = this.GetClientContextChain().SyncProcessMessage((IMessage) transitionCall);
      if (msg != null)
        Message.PropagateCallContextFromMessageToThread(msg);
      IMethodReturnMessage methodReturnMessage = msg as IMethodReturnMessage;
      if (methodReturnMessage != null && methodReturnMessage.Exception != null)
        throw methodReturnMessage.Exception;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void ExecuteCallBackInEE(IntPtr privateData);

    /// <summary>分配未命名的数据槽。</summary>
    /// <returns>局部数据槽。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public static LocalDataStoreSlot AllocateDataSlot()
    {
      return Context._localDataStoreMgr.AllocateDataSlot();
    }

    /// <summary>分配已命名的数据槽。</summary>
    /// <returns>局部数据槽对象。</returns>
    /// <param name="name">数据槽所需的名称。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public static LocalDataStoreSlot AllocateNamedDataSlot(string name)
    {
      return Context._localDataStoreMgr.AllocateNamedDataSlot(name);
    }

    /// <summary>查找已命名的数据槽。</summary>
    /// <returns>返回局部数据槽。</returns>
    /// <param name="name">数据槽名称。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public static LocalDataStoreSlot GetNamedDataSlot(string name)
    {
      return Context._localDataStoreMgr.GetNamedDataSlot(name);
    }

    /// <summary>释放所有上下文中的命名数据槽。</summary>
    /// <param name="name">要释放的数据槽的名称。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public static void FreeNamedDataSlot(string name)
    {
      Context._localDataStoreMgr.FreeNamedDataSlot(name);
    }

    /// <summary>设置当前上下文中的指定槽中的数据。</summary>
    /// <param name="slot">数据要添加到的数据槽。</param>
    /// <param name="data">要添加的数据。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public static void SetData(LocalDataStoreSlot slot, object data)
    {
      Thread.CurrentContext.MyLocalStore.SetData(slot, data);
    }

    /// <summary>从当前上下文上的指定槽检索值。</summary>
    /// <returns>返回与 <paramref name="slot" /> 关联的数据。</returns>
    /// <param name="slot">包含数据的数据槽。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public static object GetData(LocalDataStoreSlot slot)
    {
      return Thread.CurrentContext.MyLocalStore.GetData(slot);
    }

    private int ReserveSlot()
    {
      if (this._ctxStatics == null)
      {
        this._ctxStatics = new object[8];
        this._ctxStatics[0] = (object) null;
        this._ctxStaticsFreeIndex = 1;
        this._ctxStaticsCurrentBucket = 0;
      }
      if (this._ctxStaticsFreeIndex == 8)
      {
        object[] objArray1 = new object[8];
        object[] objArray2 = this._ctxStatics;
        while (objArray2[0] != null)
          objArray2 = (object[]) objArray2[0];
        objArray2[0] = (object) objArray1;
        this._ctxStaticsFreeIndex = 1;
        this._ctxStaticsCurrentBucket = this._ctxStaticsCurrentBucket + 1;
      }
      int num = this._ctxStaticsFreeIndex;
      this._ctxStaticsFreeIndex = num + 1;
      return num | this._ctxStaticsCurrentBucket << 16;
    }

    /// <summary>用远程处理服务注册实现 <see cref="T:System.Runtime.Remoting.Contexts.IDynamicProperty" /> 接口的动态属性。</summary>
    /// <returns>如果该属性已成功注册，则为 true；否则为 false。</returns>
    /// <param name="prop">要注册的动态属性。</param>
    /// <param name="obj">为其注册 <paramref name="property" /> 的对象/代理。</param>
    /// <param name="ctx">为其注册 <paramref name="property" /> 的上下文。</param>
    /// <exception cref="T:System.ArgumentNullException">要么 <paramref name="prop" /> 或其名称为 null，要么它不是动态的（不实现 <see cref="T:System.Runtime.Remoting.Contexts.IDynamicProperty" />）。</exception>
    /// <exception cref="T:System.ArgumentException">对象和上下文都已指定（<paramref name="obj" /> 和 <paramref name="ctx" /> 都不为 null）。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.Infrastructure)]
    public static bool RegisterDynamicProperty(IDynamicProperty prop, ContextBoundObject obj, Context ctx)
    {
      if (prop == null || prop.Name == null || !(prop is IContributeDynamicSink))
        throw new ArgumentNullException("prop");
      if (obj != null && ctx != null)
        throw new ArgumentException(Environment.GetResourceString("Argument_NonNullObjAndCtx"));
      return obj == null ? Context.AddDynamicProperty(ctx, prop) : IdentityHolder.AddDynamicProperty((MarshalByRefObject) obj, prop);
    }

    /// <summary>注销实现 <see cref="T:System.Runtime.Remoting.Contexts.IDynamicProperty" /> 接口的动态属性。</summary>
    /// <returns>如果对象已成功注销，则为 true；否则为 false。</returns>
    /// <param name="name">要注销的动态属性的名称。</param>
    /// <param name="obj">为其注册 <paramref name="property" /> 的对象/代理。</param>
    /// <param name="ctx">为其注册 <paramref name="property" /> 的上下文。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">对象和上下文都已指定（<paramref name="obj" /> 和 <paramref name="ctx" /> 都不为 null）。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.Infrastructure)]
    public static bool UnregisterDynamicProperty(string name, ContextBoundObject obj, Context ctx)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      if (obj != null && ctx != null)
        throw new ArgumentException(Environment.GetResourceString("Argument_NonNullObjAndCtx"));
      return obj == null ? Context.RemoveDynamicProperty(ctx, name) : IdentityHolder.RemoveDynamicProperty((MarshalByRefObject) obj, name);
    }

    [SecurityCritical]
    internal static bool AddDynamicProperty(Context ctx, IDynamicProperty prop)
    {
      if (ctx != null)
        return ctx.AddPerContextDynamicProperty(prop);
      return Context.AddGlobalDynamicProperty(prop);
    }

    [SecurityCritical]
    private bool AddPerContextDynamicProperty(IDynamicProperty prop)
    {
      if (this._dphCtx == null)
      {
        DynamicPropertyHolder dynamicPropertyHolder = new DynamicPropertyHolder();
        lock (this)
        {
          if (this._dphCtx == null)
            this._dphCtx = dynamicPropertyHolder;
        }
      }
      return this._dphCtx.AddDynamicProperty(prop);
    }

    [SecurityCritical]
    private static bool AddGlobalDynamicProperty(IDynamicProperty prop)
    {
      return Context._dphGlobal.AddDynamicProperty(prop);
    }

    [SecurityCritical]
    internal static bool RemoveDynamicProperty(Context ctx, string name)
    {
      if (ctx != null)
        return ctx.RemovePerContextDynamicProperty(name);
      return Context.RemoveGlobalDynamicProperty(name);
    }

    [SecurityCritical]
    private bool RemovePerContextDynamicProperty(string name)
    {
      if (this._dphCtx == null)
        throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Contexts_NoProperty"), (object) name));
      return this._dphCtx.RemoveDynamicProperty(name);
    }

    [SecurityCritical]
    private static bool RemoveGlobalDynamicProperty(string name)
    {
      return Context._dphGlobal.RemoveDynamicProperty(name);
    }

    [SecurityCritical]
    internal virtual bool NotifyDynamicSinks(IMessage msg, bool bCliSide, bool bStart, bool bAsync, bool bNotifyGlobals)
    {
      bool flag = false;
      if (bNotifyGlobals && Context._dphGlobal.DynamicProperties != null)
      {
        ArrayWithSize globalDynamicSinks = Context.GlobalDynamicSinks;
        if (globalDynamicSinks != null)
        {
          DynamicPropertyHolder.NotifyDynamicSinks(msg, globalDynamicSinks, bCliSide, bStart, bAsync);
          flag = true;
        }
      }
      ArrayWithSize dynamicSinks = this.DynamicSinks;
      if (dynamicSinks != null)
      {
        DynamicPropertyHolder.NotifyDynamicSinks(msg, dynamicSinks, bCliSide, bStart, bAsync);
        flag = true;
      }
      return flag;
    }
  }
}
