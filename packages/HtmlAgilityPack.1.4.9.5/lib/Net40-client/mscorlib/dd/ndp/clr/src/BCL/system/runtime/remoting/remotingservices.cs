// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.RemotingServices
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Services;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Runtime.Remoting
{
  /// <summary>提供多种使用和发布远程对象及代理的方法。此类不能被继承。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public static class RemotingServices
  {
    private static readonly object s_delayLoadChannelLock = new object();
    private const BindingFlags LookupAll = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
    private const string FieldGetterName = "FieldGetter";
    private const string FieldSetterName = "FieldSetter";
    private const string IsInstanceOfTypeName = "IsInstanceOfType";
    private const string CanCastToXmlTypeName = "CanCastToXmlType";
    private const string InvokeMemberName = "InvokeMember";
    private static volatile MethodBase s_FieldGetterMB;
    private static volatile MethodBase s_FieldSetterMB;
    private static volatile MethodBase s_IsInstanceOfTypeMB;
    private static volatile MethodBase s_CanCastToXmlTypeMB;
    private static volatile MethodBase s_InvokeMemberMB;
    private static volatile bool s_bRemoteActivationConfigured;
    private static volatile bool s_bRegisteredWellKnownChannels;
    private static bool s_bInProcessOfRegisteringWellKnownChannels;

    /// <summary>返回一个布尔值，该值指示给定的对象是透明代理还是实际对象。</summary>
    /// <returns>一个布尔值，该值指示 <paramref name="proxy" /> 参数中指定的对象是透明代理还是实际对象。</returns>
    /// <param name="proxy">对要检查的对象的引用。</param>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern bool IsTransparentProxy(object proxy);

    /// <summary>返回一个布尔值，该值指示给定代理表示的对象是否包含在与调用当前方法的对象不同的上下文中。</summary>
    /// <returns>如果该对象在当前上下文之外，则为 true；否则为 false。</returns>
    /// <param name="tp">要检查的对象。</param>
    [SecuritySafeCritical]
    public static bool IsObjectOutOfContext(object tp)
    {
      if (!RemotingServices.IsTransparentProxy(tp))
        return false;
      RealProxy realProxy = RemotingServices.GetRealProxy(tp);
      ServerIdentity serverIdentity = realProxy.IdentityObject as ServerIdentity;
      if (serverIdentity == null || !(realProxy is RemotingProxy))
        return true;
      return Thread.CurrentContext != serverIdentity.ServerContext;
    }

    /// <summary>返回一个布尔值，该值指示给定透明代理指定的对象是否包含在与调用当前方法的对象不同的应用程序域中。</summary>
    /// <returns>如果该对象在当前应用程序域之外，则为 true；否则为 false。</returns>
    /// <param name="tp">要检查的对象。</param>
    [__DynamicallyInvokable]
    public static bool IsObjectOutOfAppDomain(object tp)
    {
      return RemotingServices.IsClientProxy(tp);
    }

    internal static bool IsClientProxy(object obj)
    {
      MarshalByRefObject marshalByRefObject = obj as MarshalByRefObject;
      if (marshalByRefObject == null)
        return false;
      bool flag = false;
      bool fServer;
      Identity identity = MarshalByRefObject.GetIdentity(marshalByRefObject, out fServer);
      if (identity != null && !(identity is ServerIdentity))
        flag = true;
      return flag;
    }

    [SecurityCritical]
    internal static bool IsObjectOutOfProcess(object tp)
    {
      if (!RemotingServices.IsTransparentProxy(tp))
        return false;
      Identity identityObject = RemotingServices.GetRealProxy(tp).IdentityObject;
      if (identityObject is ServerIdentity)
        return false;
      if (identityObject == null)
        return true;
      ObjRef objectRef = identityObject.ObjectRef;
      return objectRef == null || !objectRef.IsFromThisProcess();
    }

    /// <summary>返回指定透明代理后面的真实代理。</summary>
    /// <returns>透明代理后面的真实代理实例。</returns>
    /// <param name="proxy">透明代理。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern RealProxy GetRealProxy(object proxy);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern object CreateTransparentProxy(RealProxy rp, RuntimeType typeToProxy, IntPtr stub, object stubData);

    [SecurityCritical]
    internal static object CreateTransparentProxy(RealProxy rp, Type typeToProxy, IntPtr stub, object stubData)
    {
      RuntimeType typeToProxy1 = typeToProxy as RuntimeType;
      if (typeToProxy1 == (RuntimeType) null)
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_WrongType"), (object) "typeToProxy"));
      return RemotingServices.CreateTransparentProxy(rp, typeToProxy1, stub, stubData);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern MarshalByRefObject AllocateUninitializedObject(RuntimeType objectType);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void CallDefaultCtor(object o);

    [SecurityCritical]
    internal static MarshalByRefObject AllocateUninitializedObject(Type objectType)
    {
      RuntimeType objectType1 = objectType as RuntimeType;
      // ISSUE: variable of the null type
      __Null local = null;
      if (objectType1 == (RuntimeType) local)
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_WrongType"), (object) "objectType"));
      return RemotingServices.AllocateUninitializedObject(objectType1);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern MarshalByRefObject AllocateInitializedObject(RuntimeType objectType);

    [SecurityCritical]
    internal static MarshalByRefObject AllocateInitializedObject(Type objectType)
    {
      RuntimeType objectType1 = objectType as RuntimeType;
      // ISSUE: variable of the null type
      __Null local = null;
      if (objectType1 == (RuntimeType) local)
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_WrongType"), (object) "objectType"));
      return RemotingServices.AllocateInitializedObject(objectType1);
    }

    [SecurityCritical]
    internal static bool RegisterWellKnownChannels()
    {
      if (!RemotingServices.s_bRegisteredWellKnownChannels)
      {
        bool lockTaken = false;
        object configLock = Thread.GetDomain().RemotingData.ConfigLock;
        RuntimeHelpers.PrepareConstrainedRegions();
        try
        {
          Monitor.Enter(configLock, ref lockTaken);
          if (!RemotingServices.s_bRegisteredWellKnownChannels)
          {
            if (!RemotingServices.s_bInProcessOfRegisteringWellKnownChannels)
            {
              RemotingServices.s_bInProcessOfRegisteringWellKnownChannels = true;
              CrossAppDomainChannel.RegisterChannel();
              RemotingServices.s_bRegisteredWellKnownChannels = true;
            }
          }
        }
        finally
        {
          if (lockTaken)
            Monitor.Exit(configLock);
        }
      }
      return true;
    }

    [SecurityCritical]
    internal static void InternalSetRemoteActivationConfigured()
    {
      if (RemotingServices.s_bRemoteActivationConfigured)
        return;
      RemotingServices.nSetRemoteActivationConfigured();
      RemotingServices.s_bRemoteActivationConfigured = true;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void nSetRemoteActivationConfigured();

    /// <summary>检索消息的会话 ID。</summary>
    /// <returns>唯一标识当前会话的会话 ID 字符串。</returns>
    /// <param name="msg">为其请求会话 ID 的 <see cref="T:System.Runtime.Remoting.Messaging.IMethodMessage" />。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public static string GetSessionIdForMethodMessage(IMethodMessage msg)
    {
      return msg.Uri;
    }

    /// <summary>返回控制指定对象的生存期策略的生存期服务对象。</summary>
    /// <returns>控制 <paramref name="obj" /> 的生存期的对象。</returns>
    /// <param name="obj">为其获得生存期服务的对象。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static object GetLifetimeService(MarshalByRefObject obj)
    {
      if (obj != null)
        return obj.GetLifetimeService();
      return (object) null;
    }

    /// <summary>为指定的对象检索 URI。</summary>
    /// <returns>如果指定的对象具有 URI，则为该对象的 URI；或者如果该对象尚未被封送，则为 null。</returns>
    /// <param name="obj">为其请求 URI 的 <see cref="T:System.MarshalByRefObject" />。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public static string GetObjectUri(MarshalByRefObject obj)
    {
      bool fServer;
      Identity identity = MarshalByRefObject.GetIdentity(obj, out fServer);
      if (identity != null)
        return identity.URI;
      return (string) null;
    }

    /// <summary>为后续的 <see cref="M:System.Runtime.Remoting.RemotingServices.Marshal(System.MarshalByRefObject)" /> 方法调用设置 URI。</summary>
    /// <param name="obj">为其设置 URI 的对象。</param>
    /// <param name="uri">要分配给指定对象的 URI。</param>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    /// <paramref name="obj" /> 不是局部对象、已被封送、或已对其调用了当前方法。</exception>
    /// <exception cref="T:System.Security.SecurityException">在调用堆栈上部，至少有一个调用方没有配置远程处理类型和通道的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static void SetObjectUriForMarshal(MarshalByRefObject obj, string uri)
    {
      bool fServer;
      Identity identity1 = MarshalByRefObject.GetIdentity(obj, out fServer);
      Identity identity2 = (Identity) (identity1 as ServerIdentity);
      if (identity1 != null && identity2 == null)
        throw new RemotingException(Environment.GetResourceString("Remoting_SetObjectUriForMarshal__ObjectNeedsToBeLocal"));
      if (identity1 != null && identity1.URI != null)
        throw new RemotingException(Environment.GetResourceString("Remoting_SetObjectUriForMarshal__UriExists"));
      if (identity1 == null)
      {
        Context defaultContext = Thread.GetDomain().GetDefaultContext();
        ServerIdentity id = new ServerIdentity(obj, defaultContext, uri);
        if ((Identity) obj.__RaceSetServerIdentity(id) != id)
          throw new RemotingException(Environment.GetResourceString("Remoting_SetObjectUriForMarshal__UriExists"));
      }
      else
        identity1.SetOrCreateURI(uri, true);
    }

    /// <summary>接受 <see cref="T:System.MarshalByRefObject" />，将其注册到远程处理基础结构，然后将其转换为 <see cref="T:System.Runtime.Remoting.ObjRef" /> 类的实例。</summary>
    /// <returns>
    /// <see cref="T:System.Runtime.Remoting.ObjRef" /> 类的一个实例，它表示 <paramref name="Obj" /> 参数中指定的对象。</returns>
    /// <param name="Obj">要转换的对象。</param>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    /// <paramref name="Obj" /> 参数是一个对象代理。</exception>
    /// <exception cref="T:System.Security.SecurityException">在调用堆栈上部，至少有一个调用方没有配置远程处理类型和通道的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static ObjRef Marshal(MarshalByRefObject Obj)
    {
      return RemotingServices.MarshalInternal(Obj, (string) null, (Type) null);
    }

    /// <summary>将给定的 <see cref="T:System.MarshalByRefObject" /> 转换为具有指定 URI 的 <see cref="T:System.Runtime.Remoting.ObjRef" /> 类的实例。</summary>
    /// <returns>
    /// <see cref="T:System.Runtime.Remoting.ObjRef" /> 类的一个实例，它表示 <paramref name="Obj" /> 参数中指定的对象。</returns>
    /// <param name="Obj">要转换的对象。</param>
    /// <param name="URI">指定的 URI，使用它来初始化新 <see cref="T:System.Runtime.Remoting.ObjRef" />。可以为 null。</param>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    /// <paramref name="Obj" /> 是一个对象代理，<paramref name="URI" /> 参数不为 null。</exception>
    /// <exception cref="T:System.Security.SecurityException">在调用堆栈上部，至少有一个调用方没有配置远程处理类型和通道的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static ObjRef Marshal(MarshalByRefObject Obj, string URI)
    {
      return RemotingServices.MarshalInternal(Obj, URI, (Type) null);
    }

    /// <summary>接受 <see cref="T:System.MarshalByRefObject" />，并将其转换为具有指定 URI 和提供的 <see cref="T:System.Type" /> 的 <see cref="T:System.Runtime.Remoting.ObjRef" /> 类的实例。</summary>
    /// <returns>
    /// <see cref="T:System.Runtime.Remoting.ObjRef" /> 类的一个实例，它表示 <paramref name="Obj" /> 参数中指定的对象。</returns>
    /// <param name="Obj">要转换为 <see cref="T:System.Runtime.Remoting.ObjRef" /> 的对象。</param>
    /// <param name="ObjURI">URI，使用它对 <paramref name="Obj" /> 参数中指定的对象进行封送。可以为 null。</param>
    /// <param name="RequestedType">
    /// <paramref name="Obj" /> 被封送为的 <see cref="T:System.Type" />。可以为 null。</param>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    /// <paramref name="Obj" /> 是远程对象的代理，<paramref name="ObjUri" /> 参数不为 null。</exception>
    /// <exception cref="T:System.Security.SecurityException">在调用堆栈上部，至少有一个调用方没有配置远程处理类型和通道的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static ObjRef Marshal(MarshalByRefObject Obj, string ObjURI, Type RequestedType)
    {
      return RemotingServices.MarshalInternal(Obj, ObjURI, RequestedType);
    }

    [SecurityCritical]
    internal static ObjRef MarshalInternal(MarshalByRefObject Obj, string ObjURI, Type RequestedType)
    {
      return RemotingServices.MarshalInternal(Obj, ObjURI, RequestedType, true);
    }

    [SecurityCritical]
    internal static ObjRef MarshalInternal(MarshalByRefObject Obj, string ObjURI, Type RequestedType, bool updateChannelData)
    {
      return RemotingServices.MarshalInternal(Obj, ObjURI, RequestedType, updateChannelData, false);
    }

    [SecurityCritical]
    internal static ObjRef MarshalInternal(MarshalByRefObject Obj, string ObjURI, Type RequestedType, bool updateChannelData, bool isInitializing)
    {
      if (Obj == null)
        return (ObjRef) null;
      Identity identity = RemotingServices.GetOrCreateIdentity(Obj, ObjURI, isInitializing);
      if (RequestedType != (Type) null)
      {
        ServerIdentity serverIdentity = identity as ServerIdentity;
        if (serverIdentity != null)
        {
          serverIdentity.ServerType = RequestedType;
          serverIdentity.MarshaledAsSpecificType = true;
        }
      }
      ObjRef or = identity.ObjectRef;
      if (or == null)
      {
        ObjRef objRefGiven = !RemotingServices.IsTransparentProxy((object) Obj) ? Obj.CreateObjRef(RequestedType) : RemotingServices.GetRealProxy((object) Obj).CreateObjRef(RequestedType);
        if (identity == null || objRefGiven == null)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidMarshalByRefObject"), "Obj");
        or = identity.RaceSetObjRef(objRefGiven);
      }
      ServerIdentity serverIdentity1 = identity as ServerIdentity;
      if (serverIdentity1 != null)
      {
        MarshalByRefObject marshalByRefObject = (MarshalByRefObject) null;
        serverIdentity1.GetServerObjectChain(out marshalByRefObject);
        Lease lease = identity.Lease;
        if (lease != null)
        {
          lock (lease)
          {
            if (lease.CurrentState == LeaseState.Expired)
              lease.ActivateLease();
            else
              lease.RenewInternal(identity.Lease.InitialLeaseTime);
          }
        }
        if (updateChannelData && or.ChannelInfo != null)
        {
          object[] currentChannelData = ChannelServices.CurrentChannelData;
          if (!(Obj is AppDomain))
          {
            or.ChannelInfo.ChannelData = currentChannelData;
          }
          else
          {
            int length = currentChannelData.Length;
            object[] objArray = new object[length];
            Array.Copy((Array) currentChannelData, (Array) objArray, length);
            for (int index = 0; index < length; ++index)
            {
              if (!(objArray[index] is CrossAppDomainData))
                objArray[index] = (object) null;
            }
            or.ChannelInfo.ChannelData = objArray;
          }
        }
      }
      TrackingServices.MarshaledObject((object) Obj, or);
      return or;
    }

    [SecurityCritical]
    private static Identity GetOrCreateIdentity(MarshalByRefObject Obj, string ObjURI, bool isInitializing)
    {
      int flags = 2;
      if (isInitializing)
        flags |= 4;
      Identity identity;
      if (RemotingServices.IsTransparentProxy((object) Obj))
      {
        identity = RemotingServices.GetRealProxy((object) Obj).IdentityObject;
        if (identity == null)
        {
          identity = (Identity) IdentityHolder.FindOrCreateServerIdentity(Obj, ObjURI, flags);
          identity.RaceSetTransparentProxy((object) Obj);
        }
        ServerIdentity serverIdentity = identity as ServerIdentity;
        if (serverIdentity != null)
        {
          identity = (Identity) IdentityHolder.FindOrCreateServerIdentity(serverIdentity.TPOrObject, ObjURI, flags);
          if (ObjURI != null && ObjURI != Identity.RemoveAppNameOrAppGuidIfNecessary(identity.ObjURI))
            throw new RemotingException(Environment.GetResourceString("Remoting_URIExists"));
        }
        else if (ObjURI != null && ObjURI != identity.ObjURI)
          throw new RemotingException(Environment.GetResourceString("Remoting_URIToProxy"));
      }
      else
        identity = (Identity) IdentityHolder.FindOrCreateServerIdentity(Obj, ObjURI, flags);
      return identity;
    }

    /// <summary>按引用对象将指定封送序列化为所提供的 <see cref="T:System.Runtime.Serialization.SerializationInfo" />。</summary>
    /// <param name="obj">要序列化的对象。</param>
    /// <param name="info">将对象序列化为的 <see cref="T:System.Runtime.Serialization.SerializationInfo" />。</param>
    /// <param name="context">序列化的源和目标。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="obj" /> 或 <paramref name="info" /> 参数为 null。</exception>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public static void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
    {
      if (obj == null)
        throw new ArgumentNullException("obj");
      if (info == null)
        throw new ArgumentNullException("info");
      RemotingServices.MarshalInternal((MarshalByRefObject) obj, (string) null, (Type) null).GetObjectData(info, context);
    }

    /// <summary>接受 <see cref="T:System.Runtime.Remoting.ObjRef" /> 并从它创建一个代理对象。</summary>
    /// <returns>给定的 <see cref="T:System.Runtime.Remoting.ObjRef" /> 所代表对象的代理。</returns>
    /// <param name="objectRef">
    /// <see cref="T:System.Runtime.Remoting.ObjRef" />，它代表正在为其创建代理的远程对象。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="objectRef" /> 参数中指定的 <see cref="T:System.Runtime.Remoting.ObjRef" /> 实例不是格式良好的。</exception>
    /// <exception cref="T:System.Security.SecurityException">在调用堆栈上部，至少有一个调用方没有配置远程处理类型和通道的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration, Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public static object Unmarshal(ObjRef objectRef)
    {
      return RemotingServices.InternalUnmarshal(objectRef, (object) null, false);
    }

    /// <summary>接受 <see cref="T:System.Runtime.Remoting.ObjRef" /> 并从它创建一个代理对象，然后将其精炼为服务器上的类型。</summary>
    /// <returns>给定的 <see cref="T:System.Runtime.Remoting.ObjRef" /> 所代表对象的代理。</returns>
    /// <param name="objectRef">
    /// <see cref="T:System.Runtime.Remoting.ObjRef" />，它代表正在为其创建代理的远程对象。</param>
    /// <param name="fRefine">如果为 true，则将代理精炼为服务器上的类型；否则为 false。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="objectRef" /> 参数中指定的 <see cref="T:System.Runtime.Remoting.ObjRef" /> 实例不是格式良好的。</exception>
    /// <exception cref="T:System.Security.SecurityException">在调用堆栈上部，至少有一个调用方没有配置远程处理类型和通道的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration, Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public static object Unmarshal(ObjRef objectRef, bool fRefine)
    {
      return RemotingServices.InternalUnmarshal(objectRef, (object) null, fRefine);
    }

    /// <summary>使用给定的 <see cref="T:System.Type" /> 和 URL，为已知对象创建一个代理。</summary>
    /// <returns>远程对象的代理，指向由指定的已知对象提供的终结点。</returns>
    /// <param name="classToProxy">要连接到的位于服务器端的已知对象的 <see cref="T:System.Type" />。</param>
    /// <param name="url">服务器类的 URL。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有配置远程处理类型和通道的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, RemotingConfiguration" />
    /// </PermissionSet>
    [SecurityCritical]
    [ComVisible(true)]
    public static object Connect(Type classToProxy, string url)
    {
      return RemotingServices.Unmarshal(classToProxy, url, (object) null);
    }

    /// <summary>使用给定的 <see cref="T:System.Type" />、URL 和信道特定数据，为已知对象创建一个代理。</summary>
    /// <returns>指向由所请求的已知对象提供的终结点的代理。</returns>
    /// <param name="classToProxy">要连接到的已知对象的 <see cref="T:System.Type" />。</param>
    /// <param name="url">已知对象的 URL。</param>
    /// <param name="data">信道特定的数据。可以为 null。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有配置远程处理类型和通道的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, RemotingConfiguration" />
    /// </PermissionSet>
    [SecurityCritical]
    [ComVisible(true)]
    public static object Connect(Type classToProxy, string url, object data)
    {
      return RemotingServices.Unmarshal(classToProxy, url, data);
    }

    /// <summary>阻止对象通过注册的远程处理信道再接收任何消息。</summary>
    /// <returns>如果对象与注册的远程处理信道成功断开连接，则为 true；否则为 false。</returns>
    /// <param name="obj">要与其信道断开连接的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="obj" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="obj" /> 参数是一个代理。</exception>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有配置远程处理类型和通道的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public static bool Disconnect(MarshalByRefObject obj)
    {
      return RemotingServices.Disconnect(obj, true);
    }

    [SecurityCritical]
    internal static bool Disconnect(MarshalByRefObject obj, bool bResetURI)
    {
      if (obj == null)
        throw new ArgumentNullException("obj");
      bool fServer;
      Identity identity = MarshalByRefObject.GetIdentity(obj, out fServer);
      bool flag = false;
      if (identity != null)
      {
        if (!(identity is ServerIdentity))
          throw new RemotingException(Environment.GetResourceString("Remoting_CantDisconnectClientProxy"));
        if (identity.IsInIDTable())
        {
          IdentityHolder.RemoveIdentity(identity.URI, bResetURI);
          flag = true;
        }
        TrackingServices.DisconnectedObject((object) obj);
      }
      return flag;
    }

    /// <summary>返回在将消息发送到指定的代理所表示的远程对象时应使用的一系列 Envoy 接收器。</summary>
    /// <returns>与指定的代理关联的一系列 Envoy 接收器。</returns>
    /// <param name="obj">与请求的 Envoy 接收器关联的远程对象的代理。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public static IMessageSink GetEnvoyChainForProxy(MarshalByRefObject obj)
    {
      IMessageSink messageSink = (IMessageSink) null;
      if (RemotingServices.IsObjectOutOfContext((object) obj))
      {
        Identity identityObject = RemotingServices.GetRealProxy((object) obj).IdentityObject;
        if (identityObject != null)
          messageSink = identityObject.EnvoyChain;
      }
      return messageSink;
    }

    /// <summary>从指定的代理返回表示远程对象的 <see cref="T:System.Runtime.Remoting.ObjRef" />。</summary>
    /// <returns>
    /// <see cref="T:System.Runtime.Remoting.ObjRef" />，它表示指定的代理要连接到的远程对象；或者如果对象或代理未被封送，则为 null。</returns>
    /// <param name="obj">一个代理，连接到要为其创建 <see cref="T:System.Runtime.Remoting.ObjRef" /> 的对象。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public static ObjRef GetObjRefForProxy(MarshalByRefObject obj)
    {
      ObjRef objRef = (ObjRef) null;
      if (!RemotingServices.IsTransparentProxy((object) obj))
        throw new RemotingException(Environment.GetResourceString("Remoting_Proxy_BadType"));
      Identity identityObject = RemotingServices.GetRealProxy((object) obj).IdentityObject;
      if (identityObject != null)
        objRef = identityObject.ObjectRef;
      return objRef;
    }

    [SecurityCritical]
    internal static object Unmarshal(Type classToProxy, string url)
    {
      return RemotingServices.Unmarshal(classToProxy, url, (object) null);
    }

    [SecurityCritical]
    internal static object Unmarshal(Type classToProxy, string url, object data)
    {
      if ((Type) null == classToProxy)
        throw new ArgumentNullException("classToProxy");
      if (url == null)
        throw new ArgumentNullException("url");
      if (!classToProxy.IsMarshalByRef && !classToProxy.IsInterface)
        throw new RemotingException(Environment.GetResourceString("Remoting_NotRemotableByReference"));
      Identity idObj = IdentityHolder.ResolveIdentity(url);
      if (idObj == null || idObj.ChannelSink == null || idObj.EnvoyChain == null)
      {
        IMessageSink chnlSink = (IMessageSink) null;
        IMessageSink envoySink = (IMessageSink) null;
        string envoyAndChannelSinks = RemotingServices.CreateEnvoyAndChannelSinks(url, data, out chnlSink, out envoySink);
        if (chnlSink == null)
          throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Connect_CantCreateChannelSink"), (object) url));
        if (envoyAndChannelSinks == null)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidUrl"));
        string URL = url;
        // ISSUE: variable of the null type
        __Null local = null;
        idObj = IdentityHolder.FindOrCreateIdentity(envoyAndChannelSinks, URL, (ObjRef) local);
        RemotingServices.SetEnvoyAndChannelSinks(idObj, chnlSink, envoySink);
      }
      return RemotingServices.GetOrCreateProxy(classToProxy, idObj);
    }

    [SecurityCritical]
    internal static object Wrap(ContextBoundObject obj)
    {
      return RemotingServices.Wrap(obj, (object) null, true);
    }

    [SecurityCritical]
    internal static object Wrap(ContextBoundObject obj, object proxy, bool fCreateSinks)
    {
      if (obj == null || RemotingServices.IsTransparentProxy((object) obj))
        return (object) obj;
      Identity idObj;
      if (proxy != null)
      {
        RealProxy realProxy = RemotingServices.GetRealProxy(proxy);
        if (realProxy.UnwrappedServerObject == null)
          realProxy.AttachServerHelper((MarshalByRefObject) obj);
        idObj = MarshalByRefObject.GetIdentity((MarshalByRefObject) obj);
      }
      else
        idObj = (Identity) IdentityHolder.FindOrCreateServerIdentity((MarshalByRefObject) obj, (string) null, 0);
      proxy = RemotingServices.GetOrCreateProxy(idObj, proxy, true);
      RemotingServices.GetRealProxy(proxy).Wrap();
      if (fCreateSinks)
      {
        IMessageSink chnlSink = (IMessageSink) null;
        IMessageSink envoySink = (IMessageSink) null;
        RemotingServices.CreateEnvoyAndChannelSinks((MarshalByRefObject) proxy, (ObjRef) null, out chnlSink, out envoySink);
        RemotingServices.SetEnvoyAndChannelSinks(idObj, chnlSink, envoySink);
      }
      RealProxy realProxy1 = RemotingServices.GetRealProxy(proxy);
      if (realProxy1.UnwrappedServerObject == null)
        realProxy1.AttachServerHelper((MarshalByRefObject) obj);
      return proxy;
    }

    internal static string GetObjectUriFromFullUri(string fullUri)
    {
      if (fullUri == null)
        return (string) null;
      int num = fullUri.LastIndexOf('/');
      if (num == -1)
        return fullUri;
      return fullUri.Substring(num + 1);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern object Unwrap(ContextBoundObject obj);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern object AlwaysUnwrap(ContextBoundObject obj);

    [SecurityCritical]
    internal static object InternalUnmarshal(ObjRef objectRef, object proxy, bool fRefine)
    {
      Context currentContext1 = Thread.CurrentContext;
      if (!ObjRef.IsWellFormed(objectRef))
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_BadObjRef"), (object) "Unmarshal"));
      if (objectRef.IsWellKnown())
      {
        object obj = RemotingServices.Unmarshal(typeof (MarshalByRefObject), objectRef.URI);
        Identity identity = IdentityHolder.ResolveIdentity(objectRef.URI);
        if (identity.ObjectRef == null)
          identity.RaceSetObjRef(objectRef);
        return obj;
      }
      Identity orCreateIdentity = IdentityHolder.FindOrCreateIdentity(objectRef.URI, (string) null, objectRef);
      Context currentContext2 = Thread.CurrentContext;
      ServerIdentity serverIdentity = orCreateIdentity as ServerIdentity;
      object obj1;
      if (serverIdentity != null)
      {
        Context currentContext3 = Thread.CurrentContext;
        if (!serverIdentity.IsContextBound)
        {
          if (proxy != null)
            throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_BadInternalState_ProxySameAppDomain"), Array.Empty<object>()));
          obj1 = (object) serverIdentity.TPOrObject;
        }
        else
        {
          IMessageSink chnlSink = (IMessageSink) null;
          IMessageSink envoySink = (IMessageSink) null;
          RemotingServices.CreateEnvoyAndChannelSinks(serverIdentity.TPOrObject, (ObjRef) null, out chnlSink, out envoySink);
          RemotingServices.SetEnvoyAndChannelSinks(orCreateIdentity, chnlSink, envoySink);
          obj1 = RemotingServices.GetOrCreateProxy(orCreateIdentity, proxy, true);
        }
      }
      else
      {
        IMessageSink chnlSink = (IMessageSink) null;
        IMessageSink envoySink = (IMessageSink) null;
        if (!objectRef.IsObjRefLite())
          RemotingServices.CreateEnvoyAndChannelSinks((MarshalByRefObject) null, objectRef, out chnlSink, out envoySink);
        else
          RemotingServices.CreateEnvoyAndChannelSinks(objectRef.URI, (object) null, out chnlSink, out envoySink);
        RemotingServices.SetEnvoyAndChannelSinks(orCreateIdentity, chnlSink, envoySink);
        if (objectRef.HasProxyAttribute())
          fRefine = true;
        obj1 = RemotingServices.GetOrCreateProxy(orCreateIdentity, proxy, fRefine);
      }
      TrackingServices.UnmarshaledObject(obj1, objectRef);
      return obj1;
    }

    [SecurityCritical]
    private static object GetOrCreateProxy(Identity idObj, object proxy, bool fRefine)
    {
      if (proxy == null)
      {
        ServerIdentity serverIdentity = idObj as ServerIdentity;
        Type classToProxy;
        if (serverIdentity != null)
        {
          classToProxy = serverIdentity.ServerType;
        }
        else
        {
          IRemotingTypeInfo typeInfo = idObj.ObjectRef.TypeInfo;
          classToProxy = (Type) null;
          if (typeInfo is TypeInfo && !fRefine || typeInfo == null)
          {
            classToProxy = typeof (MarshalByRefObject);
          }
          else
          {
            string typeName1 = typeInfo.TypeName;
            if (typeName1 != null)
            {
              string typeName2 = (string) null;
              string assemName = (string) null;
              TypeInfo.ParseTypeAndAssembly(typeName1, out typeName2, out assemName);
              Assembly assembly = FormatterServices.LoadAssemblyFromStringNoThrow(assemName);
              if (assembly != (Assembly) null)
                classToProxy = assembly.GetType(typeName2, false, false);
            }
          }
          if ((Type) null == classToProxy)
            throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_BadType"), (object) typeInfo.TypeName));
        }
        proxy = (object) RemotingServices.SetOrCreateProxy(idObj, classToProxy, (object) null);
      }
      else
        proxy = (object) RemotingServices.SetOrCreateProxy(idObj, (Type) null, proxy);
      if (proxy == null)
        throw new RemotingException(Environment.GetResourceString("Remoting_UnexpectedNullTP"));
      return proxy;
    }

    [SecurityCritical]
    private static object GetOrCreateProxy(Type classToProxy, Identity idObj)
    {
      object obj = (object) idObj.TPOrObject ?? (object) RemotingServices.SetOrCreateProxy(idObj, classToProxy, (object) null);
      ServerIdentity serverIdentity = idObj as ServerIdentity;
      if (serverIdentity != null)
      {
        Type serverType = serverIdentity.ServerType;
        if (!classToProxy.IsAssignableFrom(serverType))
          throw new InvalidCastException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("InvalidCast_FromTo"), (object) serverType.FullName, (object) classToProxy.FullName));
      }
      return obj;
    }

    [SecurityCritical]
    private static MarshalByRefObject SetOrCreateProxy(Identity idObj, Type classToProxy, object proxy)
    {
      RealProxy realProxy = (RealProxy) null;
      if (proxy == null)
      {
        ServerIdentity serverIdentity = idObj as ServerIdentity;
        if (idObj.ObjectRef != null)
          realProxy = ActivationServices.GetProxyAttribute(classToProxy).CreateProxy(idObj.ObjectRef, classToProxy, (object) null, (Context) null);
        if (realProxy == null)
          realProxy = ActivationServices.DefaultProxyAttribute.CreateProxy(idObj.ObjectRef, classToProxy, (object) null, serverIdentity == null ? (Context) null : serverIdentity.ServerContext);
      }
      else
        realProxy = RemotingServices.GetRealProxy(proxy);
      realProxy.IdentityObject = idObj;
      proxy = realProxy.GetTransparentProxy();
      proxy = idObj.RaceSetTransparentProxy(proxy);
      return (MarshalByRefObject) proxy;
    }

    private static bool AreChannelDataElementsNull(object[] channelData)
    {
      foreach (object obj in channelData)
      {
        if (obj != null)
          return false;
      }
      return true;
    }

    [SecurityCritical]
    internal static void CreateEnvoyAndChannelSinks(MarshalByRefObject tpOrObject, ObjRef objectRef, out IMessageSink chnlSink, out IMessageSink envoySink)
    {
      chnlSink = (IMessageSink) null;
      envoySink = (IMessageSink) null;
      if (objectRef == null)
      {
        chnlSink = ChannelServices.GetCrossContextChannelSink();
        envoySink = Thread.CurrentContext.CreateEnvoyChain(tpOrObject);
      }
      else
      {
        object[] channelData = objectRef.ChannelInfo.ChannelData;
        if (channelData != null && !RemotingServices.AreChannelDataElementsNull(channelData))
        {
          for (int index = 0; index < channelData.Length; ++index)
          {
            chnlSink = ChannelServices.CreateMessageSink(channelData[index]);
            if (chnlSink != null)
              break;
          }
          if (chnlSink == null)
          {
            lock (RemotingServices.s_delayLoadChannelLock)
            {
              for (int local_4 = 0; local_4 < channelData.Length; ++local_4)
              {
                chnlSink = ChannelServices.CreateMessageSink(channelData[local_4]);
                if (chnlSink != null)
                  break;
              }
              if (chnlSink == null)
              {
                foreach (object item_0 in channelData)
                {
                  string local_8;
                  chnlSink = RemotingConfigHandler.FindDelayLoadChannelForCreateMessageSink((string) null, item_0, out local_8);
                  if (chnlSink != null)
                    break;
                }
              }
            }
          }
        }
        if (objectRef.EnvoyInfo != null && objectRef.EnvoyInfo.EnvoySinks != null)
          envoySink = objectRef.EnvoyInfo.EnvoySinks;
        else
          envoySink = EnvoyTerminatorSink.MessageSink;
      }
    }

    [SecurityCritical]
    internal static string CreateEnvoyAndChannelSinks(string url, object data, out IMessageSink chnlSink, out IMessageSink envoySink)
    {
      string channelSink = RemotingServices.CreateChannelSink(url, data, out chnlSink);
      envoySink = EnvoyTerminatorSink.MessageSink;
      return channelSink;
    }

    [SecurityCritical]
    private static string CreateChannelSink(string url, object data, out IMessageSink chnlSink)
    {
      string objectURI = (string) null;
      chnlSink = ChannelServices.CreateMessageSink(url, data, out objectURI);
      if (chnlSink == null)
      {
        lock (RemotingServices.s_delayLoadChannelLock)
        {
          chnlSink = ChannelServices.CreateMessageSink(url, data, out objectURI);
          if (chnlSink == null)
            chnlSink = RemotingConfigHandler.FindDelayLoadChannelForCreateMessageSink(url, data, out objectURI);
        }
      }
      return objectURI;
    }

    internal static void SetEnvoyAndChannelSinks(Identity idObj, IMessageSink chnlSink, IMessageSink envoySink)
    {
      if (idObj.ChannelSink == null && chnlSink != null)
        idObj.RaceSetChannelSink(chnlSink);
      if (idObj.EnvoyChain != null)
        return;
      if (envoySink == null)
        throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_BadInternalState_FailEnvoySink"), Array.Empty<object>()));
      idObj.RaceSetEnvoyChain(envoySink);
    }

    [SecurityCritical]
    private static bool CheckCast(RealProxy rp, RuntimeType castType)
    {
      bool flag = false;
      if ((Type) castType == typeof (object))
        return true;
      if (!castType.IsInterface && !castType.IsMarshalByRef)
        return false;
      if ((Type) castType != typeof (IObjectReference))
      {
        IRemotingTypeInfo remotingTypeInfo = rp as IRemotingTypeInfo;
        if (remotingTypeInfo != null)
        {
          flag = remotingTypeInfo.CanCastTo((Type) castType, rp.GetTransparentProxy());
        }
        else
        {
          Identity identityObject = rp.IdentityObject;
          if (identityObject != null)
          {
            ObjRef objectRef = identityObject.ObjectRef;
            if (objectRef != null)
            {
              IRemotingTypeInfo typeInfo = objectRef.TypeInfo;
              if (typeInfo != null)
                flag = typeInfo.CanCastTo((Type) castType, rp.GetTransparentProxy());
            }
          }
        }
      }
      return flag;
    }

    [SecurityCritical]
    internal static bool ProxyCheckCast(RealProxy rp, RuntimeType castType)
    {
      return RemotingServices.CheckCast(rp, castType);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern object CheckCast(object objToExpand, RuntimeType type);

    [SecurityCritical]
    internal static GCHandle CreateDelegateInvocation(WaitCallback waitDelegate, object state)
    {
      return GCHandle.Alloc((object) new object[2]{ (object) waitDelegate, state });
    }

    [SecurityCritical]
    internal static void DisposeDelegateInvocation(GCHandle delegateCallToken)
    {
      delegateCallToken.Free();
    }

    [SecurityCritical]
    internal static object CreateProxyForDomain(int appDomainId, IntPtr defCtxID)
    {
      return (object) (AppDomain) RemotingServices.Unmarshal(RemotingServices.CreateDataForDomain(appDomainId, defCtxID));
    }

    [SecurityCritical]
    internal static object CreateDataForDomainCallback(object[] args)
    {
      RemotingServices.RegisterWellKnownChannels();
      ObjRef objRef = RemotingServices.MarshalInternal((MarshalByRefObject) Thread.CurrentContext.AppDomain, (string) null, (Type) null, false);
      ServerIdentity serverIdentity = (ServerIdentity) MarshalByRefObject.GetIdentity((MarshalByRefObject) Thread.CurrentContext.AppDomain);
      serverIdentity.SetHandle();
      GCHandle handle = serverIdentity.GetHandle();
      objRef.SetServerIdentity(handle);
      int id = AppDomain.CurrentDomain.GetId();
      objRef.SetDomainID(id);
      return (object) objRef;
    }

    [SecurityCritical]
    internal static ObjRef CreateDataForDomain(int appDomainId, IntPtr defCtxID)
    {
      RemotingServices.RegisterWellKnownChannels();
      InternalCrossContextDelegate ftnToCall = new InternalCrossContextDelegate(RemotingServices.CreateDataForDomainCallback);
      return (ObjRef) Thread.CurrentThread.InternalCrossContextCallback((Context) null, defCtxID, appDomainId, ftnToCall, (object[]) null);
    }

    /// <summary>从给定的 <see cref="T:System.Runtime.Remoting.Messaging.IMethodMessage" /> 返回方法库。</summary>
    /// <returns>从 <paramref name="msg" /> 参数提取的方法库。</returns>
    /// <param name="msg">从其中提取方法库的方法消息。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限，或者调用堆栈上部至少有一个调用方没有检索非公共成员的类型信息的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public static MethodBase GetMethodBaseFromMethodMessage(IMethodMessage msg)
    {
      return RemotingServices.InternalGetMethodBaseFromMethodMessage(msg);
    }

    [SecurityCritical]
    internal static MethodBase InternalGetMethodBaseFromMethodMessage(IMethodMessage msg)
    {
      if (msg == null)
        return (MethodBase) null;
      Type qualifiedTypeName = RemotingServices.InternalGetTypeFromQualifiedTypeName(msg.TypeName);
      if (qualifiedTypeName == (Type) null)
        throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_BadType"), (object) msg.TypeName));
      Type[] signature = (Type[]) msg.MethodSignature;
      return RemotingServices.GetMethodBase(msg, qualifiedTypeName, signature);
    }

    /// <summary>返回一个布尔值，该值指示是否重载给定消息中的方法。</summary>
    /// <returns>如果重载 <paramref name="msg" /> 中调用的方法，则为 true；否则为 false。</returns>
    /// <param name="msg">包含对上述方法的调用的消息。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public static bool IsMethodOverloaded(IMethodMessage msg)
    {
      return InternalRemotingServices.GetReflectionCachedData(msg.MethodBase).IsOverloaded();
    }

    [SecurityCritical]
    private static MethodBase GetMethodBase(IMethodMessage msg, Type t, Type[] signature)
    {
      MethodBase methodBase = (MethodBase) null;
      if (msg is IConstructionCallMessage || msg is IConstructionReturnMessage)
      {
        if (signature == null)
        {
          RuntimeType runtimeType = t as RuntimeType;
          ConstructorInfo[] constructorInfoArray = !(runtimeType == (RuntimeType) null) ? runtimeType.GetConstructors() : t.GetConstructors();
          if (1 != constructorInfoArray.Length)
            throw new AmbiguousMatchException(Environment.GetResourceString("Remoting_AmbiguousCTOR"));
          methodBase = (MethodBase) constructorInfoArray[0];
        }
        else
        {
          RuntimeType runtimeType = t as RuntimeType;
          methodBase = !(runtimeType == (RuntimeType) null) ? (MethodBase) runtimeType.GetConstructor(signature) : (MethodBase) t.GetConstructor(signature);
        }
      }
      else if (msg is IMethodCallMessage || msg is IMethodReturnMessage)
      {
        if (signature == null)
        {
          RuntimeType runtimeType = t as RuntimeType;
          methodBase = !(runtimeType == (RuntimeType) null) ? (MethodBase) runtimeType.GetMethod(msg.MethodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic) : (MethodBase) t.GetMethod(msg.MethodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        }
        else
        {
          RuntimeType runtimeType = t as RuntimeType;
          methodBase = !(runtimeType == (RuntimeType) null) ? (MethodBase) runtimeType.GetMethod(msg.MethodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, (Binder) null, CallingConventions.Any, signature, (ParameterModifier[]) null) : (MethodBase) t.GetMethod(msg.MethodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, (Binder) null, signature, (ParameterModifier[]) null);
        }
      }
      return methodBase;
    }

    [SecurityCritical]
    internal static bool IsMethodAllowedRemotely(MethodBase method)
    {
      if (RemotingServices.s_FieldGetterMB == (MethodBase) null || RemotingServices.s_FieldSetterMB == (MethodBase) null || (RemotingServices.s_IsInstanceOfTypeMB == (MethodBase) null || RemotingServices.s_InvokeMemberMB == (MethodBase) null) || RemotingServices.s_CanCastToXmlTypeMB == (MethodBase) null)
      {
        CodeAccessPermission.Assert(true);
        if (RemotingServices.s_FieldGetterMB == (MethodBase) null)
          RemotingServices.s_FieldGetterMB = (MethodBase) typeof (object).GetMethod("FieldGetter", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        if (RemotingServices.s_FieldSetterMB == (MethodBase) null)
          RemotingServices.s_FieldSetterMB = (MethodBase) typeof (object).GetMethod("FieldSetter", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        if (RemotingServices.s_IsInstanceOfTypeMB == (MethodBase) null)
          RemotingServices.s_IsInstanceOfTypeMB = (MethodBase) typeof (MarshalByRefObject).GetMethod("IsInstanceOfType", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        if (RemotingServices.s_CanCastToXmlTypeMB == (MethodBase) null)
          RemotingServices.s_CanCastToXmlTypeMB = (MethodBase) typeof (MarshalByRefObject).GetMethod("CanCastToXmlType", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        if (RemotingServices.s_InvokeMemberMB == (MethodBase) null)
          RemotingServices.s_InvokeMemberMB = (MethodBase) typeof (MarshalByRefObject).GetMethod("InvokeMember", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
      }
      if (!(method == RemotingServices.s_FieldGetterMB) && !(method == RemotingServices.s_FieldSetterMB) && (!(method == RemotingServices.s_IsInstanceOfTypeMB) && !(method == RemotingServices.s_InvokeMemberMB)))
        return method == RemotingServices.s_CanCastToXmlTypeMB;
      return true;
    }

    /// <summary>返回一个布尔值，该值指示调用给定消息中指定的方法的客户端在继续执行之前是否等待服务器完成该方法的处理。</summary>
    /// <returns>如果该方法是单向的，则为 true；否则为 false。</returns>
    /// <param name="method">上述方法。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public static bool IsOneWay(MethodBase method)
    {
      if (method == (MethodBase) null)
        return false;
      return InternalRemotingServices.GetReflectionCachedData(method).IsOneWayMethod();
    }

    internal static bool FindAsyncMethodVersion(MethodInfo method, out MethodInfo beginMethod, out MethodInfo endMethod)
    {
      beginMethod = (MethodInfo) null;
      endMethod = (MethodInfo) null;
      string str1 = "Begin" + method.Name;
      string str2 = "End" + method.Name;
      ArrayList params1_1 = new ArrayList();
      ArrayList params1_2 = new ArrayList();
      Type type = typeof (IAsyncResult);
      Type returnType = method.ReturnType;
      foreach (ParameterInfo parameter in method.GetParameters())
      {
        if (parameter.IsOut)
          params1_2.Add((object) parameter);
        else if (parameter.ParameterType.IsByRef)
        {
          params1_1.Add((object) parameter);
          params1_2.Add((object) parameter);
        }
        else
          params1_1.Add((object) parameter);
      }
      params1_1.Add((object) typeof (AsyncCallback));
      params1_1.Add((object) typeof (object));
      params1_2.Add((object) typeof (IAsyncResult));
      foreach (MethodInfo method1 in method.DeclaringType.GetMethods())
      {
        ParameterInfo[] parameters = method1.GetParameters();
        if (method1.Name.Equals(str1) && method1.ReturnType == type && RemotingServices.CompareParameterList(params1_1, parameters))
          beginMethod = method1;
        else if (method1.Name.Equals(str2) && method1.ReturnType == returnType && RemotingServices.CompareParameterList(params1_2, parameters))
          endMethod = method1;
      }
      return beginMethod != (MethodInfo) null && endMethod != (MethodInfo) null;
    }

    private static bool CompareParameterList(ArrayList params1, ParameterInfo[] params2)
    {
      if (params1.Count != params2.Length)
        return false;
      int index = 0;
      foreach (object obj in params1)
      {
        ParameterInfo parameterInfo1 = params2[index];
        ParameterInfo parameterInfo2 = obj as ParameterInfo;
        if (parameterInfo2 != null)
        {
          if (parameterInfo2.ParameterType != parameterInfo1.ParameterType || parameterInfo2.IsIn != parameterInfo1.IsIn || parameterInfo2.IsOut != parameterInfo1.IsOut)
            return false;
        }
        else if ((Type) obj != parameterInfo1.ParameterType && parameterInfo1.IsIn)
          return false;
        ++index;
      }
      return true;
    }

    /// <summary>返回具有指定 URI 的对象的 <see cref="T:System.Type" />。</summary>
    /// <returns>具有指定 URI 的对象的 <see cref="T:System.Type" />。</returns>
    /// <param name="URI">请求其 <see cref="T:System.Type" /> 的对象的 URI。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限，或者调用堆栈上部至少有一个调用方没有检索非公共成员的类型信息的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public static Type GetServerTypeForUri(string URI)
    {
      Type type = (Type) null;
      if (URI != null)
      {
        ServerIdentity serverIdentity = (ServerIdentity) IdentityHolder.ResolveIdentity(URI);
        type = serverIdentity != null ? serverIdentity.ServerType : RemotingConfigHandler.GetServerTypeForUri(URI);
      }
      return type;
    }

    [SecurityCritical]
    internal static void DomainUnloaded(int domainID)
    {
      IdentityHolder.FlushIdentityTable();
      CrossAppDomainSink.DomainUnloaded(domainID);
    }

    [SecurityCritical]
    internal static IntPtr GetServerContextForProxy(object tp)
    {
      ObjRef objRef = (ObjRef) null;
      bool bSameDomain;
      int domainId;
      return RemotingServices.GetServerContextForProxy(tp, out objRef, out bSameDomain, out domainId);
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal static int GetServerDomainIdForProxy(object tp)
    {
      return RemotingServices.GetRealProxy(tp).IdentityObject.ObjectRef.GetServerDomainId();
    }

    [SecurityCritical]
    internal static void GetServerContextAndDomainIdForProxy(object tp, out IntPtr contextId, out int domainId)
    {
      ObjRef objRef;
      bool bSameDomain;
      contextId = RemotingServices.GetServerContextForProxy(tp, out objRef, out bSameDomain, out domainId);
    }

    [SecurityCritical]
    private static IntPtr GetServerContextForProxy(object tp, out ObjRef objRef, out bool bSameDomain, out int domainId)
    {
      IntPtr num = IntPtr.Zero;
      objRef = (ObjRef) null;
      bSameDomain = false;
      domainId = 0;
      if (RemotingServices.IsTransparentProxy(tp))
      {
        Identity identityObject = RemotingServices.GetRealProxy(tp).IdentityObject;
        if (identityObject != null)
        {
          ServerIdentity serverIdentity = identityObject as ServerIdentity;
          if (serverIdentity != null)
          {
            bSameDomain = true;
            num = serverIdentity.ServerContext.InternalContextID;
            domainId = Thread.GetDomain().GetId();
          }
          else
          {
            objRef = identityObject.ObjectRef;
            num = objRef == null ? IntPtr.Zero : objRef.GetServerContext(out domainId);
          }
        }
        else
          num = Context.DefaultContext.InternalContextID;
      }
      return num;
    }

    [SecurityCritical]
    internal static Context GetServerContext(MarshalByRefObject obj)
    {
      Context context = (Context) null;
      if (!RemotingServices.IsTransparentProxy((object) obj) && obj is ContextBoundObject)
      {
        context = Thread.CurrentContext;
      }
      else
      {
        ServerIdentity serverIdentity = RemotingServices.GetRealProxy((object) obj).IdentityObject as ServerIdentity;
        if (serverIdentity != null)
          context = serverIdentity.ServerContext;
      }
      return context;
    }

    [SecurityCritical]
    private static object GetType(object tp)
    {
      Type type = (Type) null;
      Identity identityObject = RemotingServices.GetRealProxy(tp).IdentityObject;
      if (identityObject != null && identityObject.ObjectRef != null && identityObject.ObjectRef.TypeInfo != null)
      {
        string typeName = identityObject.ObjectRef.TypeInfo.TypeName;
        if (typeName != null)
          type = RemotingServices.InternalGetTypeFromQualifiedTypeName(typeName);
      }
      return (object) type;
    }

    [SecurityCritical]
    internal static byte[] MarshalToBuffer(object o, bool crossRuntime)
    {
      if (crossRuntime)
      {
        if (RemotingServices.IsTransparentProxy(o))
        {
          if (RemotingServices.GetRealProxy(o) is RemotingProxy && ChannelServices.RegisteredChannels.Length == 0)
            return (byte[]) null;
        }
        else
        {
          MarshalByRefObject marshalByRefObject = o as MarshalByRefObject;
          if (marshalByRefObject != null && ActivationServices.GetProxyAttribute(marshalByRefObject.GetType()) == ActivationServices.DefaultProxyAttribute && ChannelServices.RegisteredChannels.Length == 0)
            return (byte[]) null;
        }
      }
      MemoryStream memoryStream1 = new MemoryStream();
      RemotingSurrogateSelector surrogateSelector = new RemotingSurrogateSelector();
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      binaryFormatter.SurrogateSelector = (ISurrogateSelector) surrogateSelector;
      StreamingContext streamingContext = new StreamingContext(StreamingContextStates.Other);
      binaryFormatter.Context = streamingContext;
      MemoryStream memoryStream2 = memoryStream1;
      object graph = o;
      // ISSUE: variable of the null type
      __Null local = null;
      int num = 0;
      binaryFormatter.Serialize((Stream) memoryStream2, graph, (Header[]) local, num != 0);
      return memoryStream1.GetBuffer();
    }

    [SecurityCritical]
    internal static object UnmarshalFromBuffer(byte[] b, bool crossRuntime)
    {
      MemoryStream memoryStream1 = new MemoryStream(b);
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      binaryFormatter.AssemblyFormat = FormatterAssemblyStyle.Simple;
      binaryFormatter.SurrogateSelector = (ISurrogateSelector) null;
      StreamingContext streamingContext = new StreamingContext(StreamingContextStates.Other);
      binaryFormatter.Context = streamingContext;
      MemoryStream memoryStream2 = memoryStream1;
      // ISSUE: variable of the null type
      __Null local = null;
      int num = 0;
      object proxy = binaryFormatter.Deserialize((Stream) memoryStream2, (HeaderHandler) local, num != 0);
      if (crossRuntime && RemotingServices.IsTransparentProxy(proxy) && RemotingServices.GetRealProxy(proxy) is RemotingProxy)
      {
        if (ChannelServices.RegisteredChannels.Length == 0)
          return (object) null;
        proxy.GetHashCode();
      }
      return proxy;
    }

    internal static object UnmarshalReturnMessageFromBuffer(byte[] b, IMethodCallMessage msg)
    {
      MemoryStream memoryStream1 = new MemoryStream(b);
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      binaryFormatter.SurrogateSelector = (ISurrogateSelector) null;
      StreamingContext streamingContext = new StreamingContext(StreamingContextStates.Other);
      binaryFormatter.Context = streamingContext;
      MemoryStream memoryStream2 = memoryStream1;
      // ISSUE: variable of the null type
      __Null local = null;
      IMethodCallMessage methodCallMessage = msg;
      return binaryFormatter.DeserializeMethodResponse((Stream) memoryStream2, (HeaderHandler) local, methodCallMessage);
    }

    /// <summary>连接到指定的远程对象，并对其执行提供的 <see cref="T:System.Runtime.Remoting.Messaging.IMethodCallMessage" />。</summary>
    /// <returns>远程方法的响应。</returns>
    /// <param name="target">要调用其方法的远程对象。</param>
    /// <param name="reqMsg">指定的远程对象的方法的方法调用消息。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">该方法从除对象的本机上下文之外的上下文调用。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public static IMethodReturnMessage ExecuteMessage(MarshalByRefObject target, IMethodCallMessage reqMsg)
    {
      if (target == null)
        throw new ArgumentNullException("target");
      RealProxy realProxy = RemotingServices.GetRealProxy((object) target);
      if (realProxy is RemotingProxy && !realProxy.DoContextsMatch())
        throw new RemotingException(Environment.GetResourceString("Remoting_Proxy_WrongContext"));
      return (IMethodReturnMessage) new StackBuilderSink(target).SyncProcessMessage((IMessage) reqMsg);
    }

    [SecurityCritical]
    internal static string DetermineDefaultQualifiedTypeName(Type type)
    {
      if (type == (Type) null)
        throw new ArgumentNullException("type");
      string xmlType = (string) null;
      string xmlTypeNamespace = (string) null;
      if (SoapServices.GetXmlTypeForInteropType(type, out xmlType, out xmlTypeNamespace))
        return "soap:" + xmlType + ", " + xmlTypeNamespace;
      return type.AssemblyQualifiedName;
    }

    [SecurityCritical]
    internal static string GetDefaultQualifiedTypeName(RuntimeType type)
    {
      return InternalRemotingServices.GetReflectionCachedData(type).QualifiedTypeName;
    }

    internal static string InternalGetClrTypeNameFromQualifiedTypeName(string qualifiedTypeName)
    {
      if (qualifiedTypeName.Length > 4 && string.CompareOrdinal(qualifiedTypeName, 0, "clr:", 0, 4) == 0)
        return qualifiedTypeName.Substring(4);
      return (string) null;
    }

    private static int IsSoapType(string qualifiedTypeName)
    {
      if (qualifiedTypeName.Length > 5 && string.CompareOrdinal(qualifiedTypeName, 0, "soap:", 0, 5) == 0)
        return qualifiedTypeName.IndexOf(',', 5);
      return -1;
    }

    [SecurityCritical]
    internal static string InternalGetSoapTypeNameFromQualifiedTypeName(string xmlTypeName, string xmlTypeNamespace)
    {
      string typeNamespace;
      string assemblyName;
      if (!SoapServices.DecodeXmlNamespaceForClrTypeNamespace(xmlTypeNamespace, out typeNamespace, out assemblyName))
        return (string) null;
      string str = typeNamespace == null || typeNamespace.Length <= 0 ? xmlTypeName : typeNamespace + "." + xmlTypeName;
      try
      {
        return str + ", " + assemblyName;
      }
      catch
      {
      }
      return (string) null;
    }

    [SecurityCritical]
    internal static string InternalGetTypeNameFromQualifiedTypeName(string qualifiedTypeName)
    {
      if (qualifiedTypeName == null)
        throw new ArgumentNullException("qualifiedTypeName");
      string qualifiedTypeName1 = RemotingServices.InternalGetClrTypeNameFromQualifiedTypeName(qualifiedTypeName);
      if (qualifiedTypeName1 != null)
        return qualifiedTypeName1;
      int num = RemotingServices.IsSoapType(qualifiedTypeName);
      if (num != -1)
      {
        string qualifiedTypeName2 = RemotingServices.InternalGetSoapTypeNameFromQualifiedTypeName(qualifiedTypeName.Substring(5, num - 5), qualifiedTypeName.Substring(num + 2, qualifiedTypeName.Length - (num + 2)));
        if (qualifiedTypeName2 != null)
          return qualifiedTypeName2;
      }
      return qualifiedTypeName;
    }

    [SecurityCritical]
    internal static RuntimeType InternalGetTypeFromQualifiedTypeName(string qualifiedTypeName, bool partialFallback)
    {
      if (qualifiedTypeName == null)
        throw new ArgumentNullException("qualifiedTypeName");
      string qualifiedTypeName1 = RemotingServices.InternalGetClrTypeNameFromQualifiedTypeName(qualifiedTypeName);
      if (qualifiedTypeName1 != null)
        return RemotingServices.LoadClrTypeWithPartialBindFallback(qualifiedTypeName1, partialFallback);
      int num = RemotingServices.IsSoapType(qualifiedTypeName);
      if (num != -1)
      {
        string str = qualifiedTypeName.Substring(5, num - 5);
        string xmlTypeNamespace = qualifiedTypeName.Substring(num + 2, qualifiedTypeName.Length - (num + 2));
        RuntimeType runtimeType = (RuntimeType) SoapServices.GetInteropTypeFromXmlType(str, xmlTypeNamespace);
        if (runtimeType != (RuntimeType) null)
          return runtimeType;
        string qualifiedTypeName2 = RemotingServices.InternalGetSoapTypeNameFromQualifiedTypeName(str, xmlTypeNamespace);
        if (qualifiedTypeName2 != null)
          return RemotingServices.LoadClrTypeWithPartialBindFallback(qualifiedTypeName2, true);
      }
      return RemotingServices.LoadClrTypeWithPartialBindFallback(qualifiedTypeName, partialFallback);
    }

    [SecurityCritical]
    internal static Type InternalGetTypeFromQualifiedTypeName(string qualifiedTypeName)
    {
      return (Type) RemotingServices.InternalGetTypeFromQualifiedTypeName(qualifiedTypeName, true);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static RuntimeType LoadClrTypeWithPartialBindFallback(string typeName, bool partialFallback)
    {
      if (!partialFallback)
        return (RuntimeType) Type.GetType(typeName, false);
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return RuntimeTypeHandle.GetTypeByName(typeName, false, false, false, ref stackMark, true);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool CORProfilerTrackRemoting();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool CORProfilerTrackRemotingCookie();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool CORProfilerTrackRemotingAsync();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void CORProfilerRemotingClientSendingMessage(out Guid id, bool fIsAsync);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void CORProfilerRemotingClientReceivingReply(Guid id, bool fIsAsync);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void CORProfilerRemotingServerReceivingMessage(Guid id, bool fIsAsync);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void CORProfilerRemotingServerSendingReply(out Guid id, bool fIsAsync);

    /// <summary>记录与外部调试器进行的远程处理交换所处的阶段。</summary>
    /// <param name="stage">标识远程处理交换中所处的阶段的内部定义常数。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    [Conditional("REMOTING_PERF")]
    [Obsolete("Use of this method is not recommended. The LogRemotingStage existed for internal diagnostic purposes only.")]
    public static void LogRemotingStage(int stage)
    {
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void ResetInterfaceCache(object proxy);
  }
}
