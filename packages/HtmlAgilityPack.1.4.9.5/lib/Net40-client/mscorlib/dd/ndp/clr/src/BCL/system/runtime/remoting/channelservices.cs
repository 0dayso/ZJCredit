// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.ChannelServices
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Runtime.Remoting.Channels
{
  /// <summary>提供帮助进行远程处理信道注册、解析和 URL 发现的静态方法。此类不能被继承。</summary>
  [ComVisible(true)]
  public sealed class ChannelServices
  {
    private static volatile object[] s_currentChannelData = (object[]) null;
    private static object s_channelLock = new object();
    private static volatile RegisteredChannelList s_registeredChannels = new RegisteredChannelList();
    [SecurityCritical]
    private static volatile unsafe Perf_Contexts* perf_Contexts = ChannelServices.GetPrivateContextsPerfCounters();
    private static bool unloadHandlerRegistered = false;
    private static volatile IMessageSink xCtxChannel;

    internal static object[] CurrentChannelData
    {
      [SecurityCritical] get
      {
        if (ChannelServices.s_currentChannelData == null)
          ChannelServices.RefreshChannelData();
        return ChannelServices.s_currentChannelData;
      }
    }

    private static long remoteCalls
    {
      get
      {
        return Thread.GetDomain().RemotingData.ChannelServicesData.remoteCalls;
      }
      set
      {
        Thread.GetDomain().RemotingData.ChannelServicesData.remoteCalls = value;
      }
    }

    /// <summary>获取当前已注册信道的列表。</summary>
    /// <returns>所有当前注册信道的数组。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public static IChannel[] RegisteredChannels
    {
      [SecurityCritical] get
      {
        RegisteredChannelList registeredChannelList = ChannelServices.s_registeredChannels;
        int count = registeredChannelList.Count;
        if (count == 0)
          return new IChannel[0];
        int length = count - 1;
        int num = 0;
        IChannel[] channelArray = new IChannel[length];
        for (int index = 0; index < count; ++index)
        {
          IChannel channel = registeredChannelList.GetChannel(index);
          if (!(channel is CrossAppDomainChannel))
            channelArray[num++] = channel;
        }
        return channelArray;
      }
    }

    [SecuritySafeCritical]
    static unsafe ChannelServices()
    {
    }

    private ChannelServices()
    {
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern unsafe Perf_Contexts* GetPrivateContextsPerfCounters();

    /// <summary>向信道服务注册信道。</summary>
    /// <param name="chnl">要注册的信道。</param>
    /// <param name="ensureSecurity">如果启用了安全，则为 true；否则为 false。将该值设置为 false 不会影响 TCP 或 IPC 通道的安全设置。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="chnl" /> 参数为 null。</exception>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">已注册该信道。</exception>
    /// <exception cref="T:System.Security.SecurityException">在调用堆栈上部，至少有一个调用方没有配置远程处理类型和通道的权限。</exception>
    /// <exception cref="T:System.NotSupportedException">Windows 98 中不支持 <see cref="T:System.Runtime.Remoting.Channels.Tcp.TcpServerChannel" />，并且在所有平台上不支持 <see cref="T:System.Runtime.Remoting.Channels.Http.HttpServerChannel" />。如果需要安全的 HTTP 信道，请使用 Internet 信息服务 (IIS) 承载服务。</exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static void RegisterChannel(IChannel chnl, bool ensureSecurity)
    {
      ChannelServices.RegisterChannelInternal(chnl, ensureSecurity);
    }

    /// <summary>向信道服务注册信道。<see cref="M:System.Runtime.Remoting.Channels.ChannelServices.RegisterChannel(System.Runtime.Remoting.Channels.IChannel)" /> 已过时。请改用 <see cref="M:System.Runtime.Remoting.Channels.ChannelServices.RegisterChannel(System.Runtime.Remoting.Channels.IChannel,System.Boolean)" />。</summary>
    /// <param name="chnl">要注册的信道。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="chnl" /> 参数为 null。</exception>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">已注册该信道。</exception>
    /// <exception cref="T:System.Security.SecurityException">在调用堆栈上部，至少有一个调用方没有配置远程处理类型和通道的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [Obsolete("Use System.Runtime.Remoting.ChannelServices.RegisterChannel(IChannel chnl, bool ensureSecurity) instead.", false)]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static void RegisterChannel(IChannel chnl)
    {
      ChannelServices.RegisterChannelInternal(chnl, false);
    }

    [SecurityCritical]
    internal static unsafe void RegisterChannelInternal(IChannel chnl, bool ensureSecurity)
    {
      if (chnl == null)
        throw new ArgumentNullException("chnl");
      bool lockTaken = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        Monitor.Enter(ChannelServices.s_channelLock, ref lockTaken);
        string channelName = chnl.ChannelName;
        RegisteredChannelList registeredChannelList = ChannelServices.s_registeredChannels;
        if (channelName == null || channelName.Length == 0 || -1 == registeredChannelList.FindChannelIndex(chnl.ChannelName))
        {
          if (ensureSecurity)
          {
            ISecurableChannel securableChannel = chnl as ISecurableChannel;
            if (securableChannel != null)
              securableChannel.IsSecured = ensureSecurity;
            else
              throw new RemotingException(Environment.GetResourceString("Remoting_Channel_CannotBeSecured", (object) (chnl.ChannelName ?? chnl.ToString())));
          }
          RegisteredChannel[] registeredChannels = registeredChannelList.RegisteredChannels;
          RegisteredChannel[] channels = registeredChannels != null ? new RegisteredChannel[registeredChannels.Length + 1] : new RegisteredChannel[1];
          if (!ChannelServices.unloadHandlerRegistered && !(chnl is CrossAppDomainChannel))
          {
            AppDomain.CurrentDomain.DomainUnload += new EventHandler(ChannelServices.UnloadHandler);
            ChannelServices.unloadHandlerRegistered = true;
          }
          int channelPriority = chnl.ChannelPriority;
          int index;
          for (index = 0; index < registeredChannels.Length; ++index)
          {
            RegisteredChannel registeredChannel = registeredChannels[index];
            if (channelPriority > registeredChannel.Channel.ChannelPriority)
            {
              channels[index] = new RegisteredChannel(chnl);
              break;
            }
            channels[index] = registeredChannel;
          }
          if (index == registeredChannels.Length)
          {
            channels[registeredChannels.Length] = new RegisteredChannel(chnl);
          }
          else
          {
            for (; index < registeredChannels.Length; ++index)
              channels[index + 1] = registeredChannels[index];
          }
          if ((IntPtr) ChannelServices.perf_Contexts != IntPtr.Zero)
            ++ChannelServices.perf_Contexts->cChannels;
          ChannelServices.s_registeredChannels = new RegisteredChannelList(channels);
          ChannelServices.RefreshChannelData();
        }
        else
          throw new RemotingException(Environment.GetResourceString("Remoting_ChannelNameAlreadyRegistered", (object) chnl.ChannelName));
      }
      finally
      {
        if (lockTaken)
          Monitor.Exit(ChannelServices.s_channelLock);
      }
    }

    /// <summary>从注册信道列表中注销特定的信道。</summary>
    /// <param name="chnl">要注销的信道。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="chnl" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">未注册该信道。</exception>
    /// <exception cref="T:System.Security.SecurityException">在调用堆栈上部，至少有一个调用方没有配置远程处理类型和通道的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static unsafe void UnregisterChannel(IChannel chnl)
    {
      bool lockTaken = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        Monitor.Enter(ChannelServices.s_channelLock, ref lockTaken);
        if (chnl != null)
        {
          RegisteredChannelList registeredChannelList = ChannelServices.s_registeredChannels;
          int channelIndex = registeredChannelList.FindChannelIndex(chnl);
          if (-1 == channelIndex)
            throw new RemotingException(Environment.GetResourceString("Remoting_ChannelNotRegistered", (object) chnl.ChannelName));
          RegisteredChannel[] registeredChannels = registeredChannelList.RegisteredChannels;
          RegisteredChannel[] channels = new RegisteredChannel[registeredChannels.Length - 1];
          IChannelReceiver channelReceiver = chnl as IChannelReceiver;
          if (channelReceiver != null)
            channelReceiver.StopListening((object) null);
          int index1 = 0;
          int index2 = 0;
          while (index2 < registeredChannels.Length)
          {
            if (index2 == channelIndex)
            {
              ++index2;
            }
            else
            {
              channels[index1] = registeredChannels[index2];
              ++index1;
              ++index2;
            }
          }
          if ((IntPtr) ChannelServices.perf_Contexts != IntPtr.Zero)
            --ChannelServices.perf_Contexts->cChannels;
          ChannelServices.s_registeredChannels = new RegisteredChannelList(channels);
        }
        ChannelServices.RefreshChannelData();
      }
      finally
      {
        if (lockTaken)
          Monitor.Exit(ChannelServices.s_channelLock);
      }
    }

    [SecurityCritical]
    internal static IMessageSink CreateMessageSink(string url, object data, out string objectURI)
    {
      IMessageSink messageSink = (IMessageSink) null;
      objectURI = (string) null;
      RegisteredChannelList registeredChannelList = ChannelServices.s_registeredChannels;
      int count = registeredChannelList.Count;
      for (int index = 0; index < count; ++index)
      {
        if (registeredChannelList.IsSender(index))
        {
          messageSink = ((IChannelSender) registeredChannelList.GetChannel(index)).CreateMessageSink(url, data, out objectURI);
          if (messageSink != null)
            break;
        }
      }
      if (objectURI == null)
        objectURI = url;
      return messageSink;
    }

    [SecurityCritical]
    internal static IMessageSink CreateMessageSink(object data)
    {
      string objectURI;
      return ChannelServices.CreateMessageSink((string) null, data, out objectURI);
    }

    /// <summary>返回具有指定名称的注册信道。</summary>
    /// <returns>到注册信道的接口，或者如果该信道未注册，则为 null。</returns>
    /// <param name="name">信道名称。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public static IChannel GetChannel(string name)
    {
      RegisteredChannelList registeredChannelList = ChannelServices.s_registeredChannels;
      int channelIndex = registeredChannelList.FindChannelIndex(name);
      if (0 > channelIndex)
        return (IChannel) null;
      IChannel channel = registeredChannelList.GetChannel(channelIndex);
      if (channel is CrossAppDomainChannel || channel is CrossContextChannel)
        return (IChannel) null;
      return channel;
    }

    /// <summary>返回所有可用于到达指定对象的 URL 的数组。</summary>
    /// <returns>包含可用于远程标识对象的 URL 的字符串数组，或者如果未找到，则为 null。</returns>
    /// <param name="obj">为其检索 URL 数组的对象。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public static string[] GetUrlsForObject(MarshalByRefObject obj)
    {
      if (obj == null)
        return (string[]) null;
      RegisteredChannelList registeredChannelList = ChannelServices.s_registeredChannels;
      int count = registeredChannelList.Count;
      Hashtable hashtable = new Hashtable();
      bool fServer;
      Identity identity = MarshalByRefObject.GetIdentity(obj, out fServer);
      if (identity != null)
      {
        string objUri = identity.ObjURI;
        if (objUri != null)
        {
          for (int index1 = 0; index1 < count; ++index1)
          {
            if (registeredChannelList.IsReceiver(index1))
            {
              try
              {
                string[] urlsForUri = ((IChannelReceiver) registeredChannelList.GetChannel(index1)).GetUrlsForUri(objUri);
                for (int index2 = 0; index2 < urlsForUri.Length; ++index2)
                  hashtable.Add((object) urlsForUri[index2], (object) urlsForUri[index2]);
              }
              catch (NotSupportedException ex)
              {
              }
            }
          }
        }
      }
      ICollection keys = hashtable.Keys;
      string[] strArray = new string[keys.Count];
      int num = 0;
      foreach (string str in (IEnumerable) keys)
        strArray[num++] = str;
      return strArray;
    }

    [SecurityCritical]
    internal static IMessageSink GetChannelSinkForProxy(object obj)
    {
      IMessageSink messageSink = (IMessageSink) null;
      if (RemotingServices.IsTransparentProxy(obj))
      {
        RemotingProxy remotingProxy = RemotingServices.GetRealProxy(obj) as RemotingProxy;
        if (remotingProxy != null)
          messageSink = remotingProxy.IdentityObject.ChannelSink;
      }
      return messageSink;
    }

    /// <summary>返回给定代理的属性的 <see cref="T:System.Collections.IDictionary" />。</summary>
    /// <returns>到属性字典的接口，或者如果未找到任何属性，则为 null。</returns>
    /// <param name="obj">为其检索属性的代理。</param>
    /// <exception cref="T:System.Security.SecurityException">在调用堆栈上部，至少有一个调用方没有配置远程处理类型和通道的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static IDictionary GetChannelSinkProperties(object obj)
    {
      IMessageSink channelSinkForProxy = ChannelServices.GetChannelSinkForProxy(obj);
      IClientChannelSink clientChannelSink = channelSinkForProxy as IClientChannelSink;
      if (clientChannelSink == null)
        return channelSinkForProxy as IDictionary ?? (IDictionary) null;
      ArrayList arrayList = new ArrayList();
      do
      {
        IDictionary properties = clientChannelSink.Properties;
        if (properties != null)
          arrayList.Add((object) properties);
        clientChannelSink = clientChannelSink.NextChannelSink;
      }
      while (clientChannelSink != null);
      return (IDictionary) new AggregateDictionary((ICollection) arrayList);
    }

    internal static IMessageSink GetCrossContextChannelSink()
    {
      if (ChannelServices.xCtxChannel == null)
        ChannelServices.xCtxChannel = CrossContextChannel.MessageSink;
      return ChannelServices.xCtxChannel;
    }

    [SecurityCritical]
    internal static unsafe void IncrementRemoteCalls(long cCalls)
    {
      ChannelServices.remoteCalls += cCalls;
      if ((IntPtr) ChannelServices.perf_Contexts == IntPtr.Zero)
        return;
      ChannelServices.perf_Contexts->cRemoteCalls += (int) cCalls;
    }

    [SecurityCritical]
    internal static void IncrementRemoteCalls()
    {
      ChannelServices.IncrementRemoteCalls(1L);
    }

    [SecurityCritical]
    internal static void RefreshChannelData()
    {
      bool lockTaken = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        Monitor.Enter(ChannelServices.s_channelLock, ref lockTaken);
        ChannelServices.s_currentChannelData = ChannelServices.CollectChannelDataFromChannels();
      }
      finally
      {
        if (lockTaken)
          Monitor.Exit(ChannelServices.s_channelLock);
      }
    }

    [SecurityCritical]
    private static object[] CollectChannelDataFromChannels()
    {
      RemotingServices.RegisterWellKnownChannels();
      RegisteredChannelList registeredChannelList = ChannelServices.s_registeredChannels;
      int count = registeredChannelList.Count;
      int receiverCount = registeredChannelList.ReceiverCount;
      object[] objArray1 = new object[receiverCount];
      int length = 0;
      int index1 = 0;
      int index2 = 0;
      for (; index1 < count; ++index1)
      {
        IChannel channel = registeredChannelList.GetChannel(index1);
        if (channel == null)
          throw new RemotingException(Environment.GetResourceString("Remoting_ChannelNotRegistered", (object) ""));
        if (registeredChannelList.IsReceiver(index1))
        {
          object channelData = ((IChannelReceiver) channel).ChannelData;
          objArray1[index2] = channelData;
          if (channelData != null)
            ++length;
          ++index2;
        }
      }
      if (length != receiverCount)
      {
        object[] objArray2 = new object[length];
        int num = 0;
        for (int index3 = 0; index3 < receiverCount; ++index3)
        {
          object obj = objArray1[index3];
          if (obj != null)
            objArray2[num++] = obj;
        }
        objArray1 = objArray2;
      }
      return objArray1;
    }

    private static bool IsMethodReallyPublic(MethodInfo mi)
    {
      if (!mi.IsPublic || mi.IsStatic)
        return false;
      if (!mi.IsGenericMethod)
        return true;
      foreach (Type genericArgument in mi.GetGenericArguments())
      {
        if (!genericArgument.IsVisible)
          return false;
      }
      return true;
    }

    /// <summary>调度传入的远程调用。</summary>
    /// <returns>给出服务器消息处理的状态的 <see cref="T:System.Runtime.Remoting.Channels.ServerProcessing" />。</returns>
    /// <param name="sinkStack">消息已经过的服务器信道接收器的堆栈。</param>
    /// <param name="msg">要调度的消息。</param>
    /// <param name="replyMsg">当此方法返回时，包含 <see cref="T:System.Runtime.Remoting.Messaging.IMessage" />，它持有从服务器到包含在 <paramref name="msg" /> 参数中的消息的答复。该参数未经初始化即被传递。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="msg" /> 参数为 null。</exception>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public static ServerProcessing DispatchMessage(IServerChannelSinkStack sinkStack, IMessage msg, out IMessage replyMsg)
    {
      ServerProcessing serverProcessing = ServerProcessing.Complete;
      replyMsg = (IMessage) null;
      try
      {
        if (msg == null)
          throw new ArgumentNullException("msg");
        ChannelServices.IncrementRemoteCalls();
        ServerIdentity wellKnownObject = ChannelServices.CheckDisconnectedOrCreateWellKnownObject(msg);
        if (wellKnownObject.ServerType == typeof (AppDomain))
          throw new RemotingException(Environment.GetResourceString("Remoting_AppDomainsCantBeCalledRemotely"));
        IMethodCallMessage methodCallMessage = msg as IMethodCallMessage;
        if (methodCallMessage == null)
        {
          if (!typeof (IMessageSink).IsAssignableFrom(wellKnownObject.ServerType))
            throw new RemotingException(Environment.GetResourceString("Remoting_AppDomainsCantBeCalledRemotely"));
          serverProcessing = ServerProcessing.Complete;
          replyMsg = ChannelServices.GetCrossContextChannelSink().SyncProcessMessage(msg);
        }
        else
        {
          MethodInfo mi = (MethodInfo) methodCallMessage.MethodBase;
          if (!ChannelServices.IsMethodReallyPublic(mi) && !RemotingServices.IsMethodAllowedRemotely((MethodBase) mi))
            throw new RemotingException(Environment.GetResourceString("Remoting_NonPublicOrStaticCantBeCalledRemotely"));
          InternalRemotingServices.GetReflectionCachedData((MethodBase) mi);
          if (RemotingServices.IsOneWay((MethodBase) mi))
          {
            serverProcessing = ServerProcessing.OneWay;
            ChannelServices.GetCrossContextChannelSink().AsyncProcessMessage(msg, (IMessageSink) null);
          }
          else
          {
            serverProcessing = ServerProcessing.Complete;
            if (!wellKnownObject.ServerType.IsContextful)
            {
              object[] args = new object[2]{ (object) msg, (object) wellKnownObject.ServerContext };
              replyMsg = (IMessage) CrossContextChannel.SyncProcessMessageCallback(args);
            }
            else
              replyMsg = ChannelServices.GetCrossContextChannelSink().SyncProcessMessage(msg);
          }
        }
      }
      catch (Exception ex1)
      {
        if (serverProcessing != ServerProcessing.OneWay)
        {
          try
          {
            IMethodCallMessage mcm = msg != null ? (IMethodCallMessage) msg : (IMethodCallMessage) new ErrorMessage();
            replyMsg = (IMessage) new ReturnMessage(ex1, mcm);
            if (msg != null)
              ((ReturnMessage) replyMsg).SetLogicalCallContext((LogicalCallContext) msg.Properties[(object) Message.CallContextKey]);
          }
          catch (Exception ex2)
          {
          }
        }
      }
      return serverProcessing;
    }

    /// <summary>根据嵌入在消息中的 URI 将传入的消息同步调度到服务器端链。</summary>
    /// <returns>调用将答复消息返回到服务器端链。</returns>
    /// <param name="msg">要调度的消息。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="msg" /> 参数为 null。</exception>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public static IMessage SyncDispatchMessage(IMessage msg)
    {
      IMessage message = (IMessage) null;
      bool flag = false;
      try
      {
        if (msg == null)
          throw new ArgumentNullException("msg");
        ChannelServices.IncrementRemoteCalls();
        if (!(msg is TransitionCall))
        {
          ChannelServices.CheckDisconnectedOrCreateWellKnownObject(msg);
          flag = RemotingServices.IsOneWay(((IMethodMessage) msg).MethodBase);
        }
        IMessageSink contextChannelSink = ChannelServices.GetCrossContextChannelSink();
        if (!flag)
          message = contextChannelSink.SyncProcessMessage(msg);
        else
          contextChannelSink.AsyncProcessMessage(msg, (IMessageSink) null);
      }
      catch (Exception ex1)
      {
        if (!flag)
        {
          try
          {
            IMethodCallMessage mcm = msg != null ? (IMethodCallMessage) msg : (IMethodCallMessage) new ErrorMessage();
            message = (IMessage) new ReturnMessage(ex1, mcm);
            if (msg != null)
              ((ReturnMessage) message).SetLogicalCallContext(mcm.LogicalCallContext);
          }
          catch (Exception ex2)
          {
          }
        }
      }
      return message;
    }

    /// <summary>根据嵌入在消息中的 URI 将给定的消息异步调度到服务器端链。</summary>
    /// <returns>用于控制异步调度消息的 <see cref="T:System.Runtime.Remoting.Messaging.IMessageCtrl" /> 对象。</returns>
    /// <param name="msg">要调度的消息。</param>
    /// <param name="replySink">如果返回消息不为 null，将处理该返回消息的接收器。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="msg" /> 参数为 null。</exception>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public static IMessageCtrl AsyncDispatchMessage(IMessage msg, IMessageSink replySink)
    {
      IMessageCtrl messageCtrl = (IMessageCtrl) null;
      try
      {
        if (msg == null)
          throw new ArgumentNullException("msg");
        ChannelServices.IncrementRemoteCalls();
        if (!(msg is TransitionCall))
          ChannelServices.CheckDisconnectedOrCreateWellKnownObject(msg);
        messageCtrl = ChannelServices.GetCrossContextChannelSink().AsyncProcessMessage(msg, replySink);
      }
      catch (Exception ex1)
      {
        if (replySink != null)
        {
          try
          {
            IMethodCallMessage methodCallMessage = (IMethodCallMessage) msg;
            ReturnMessage returnMessage = new ReturnMessage(ex1, (IMethodCallMessage) msg);
            if (msg != null)
              returnMessage.SetLogicalCallContext(methodCallMessage.LogicalCallContext);
            replySink.SyncProcessMessage((IMessage) returnMessage);
          }
          catch (Exception ex2)
          {
          }
        }
      }
      return messageCtrl;
    }

    /// <summary>为指定的信道创建信道接收器链。</summary>
    /// <returns>指定信道的新信道接收器链。</returns>
    /// <param name="provider">接收器提供程序链中将创建信道接收器链的第一个提供程序。</param>
    /// <param name="channel">要为其创建信道接收器链的 <see cref="T:System.Runtime.Remoting.Channels.IChannelReceiver" />。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public static IServerChannelSink CreateServerChannelSinkChain(IServerChannelSinkProvider provider, IChannelReceiver channel)
    {
      if (provider == null)
        return (IServerChannelSink) new DispatchChannelSink();
      IServerChannelSinkProvider channelSinkProvider = provider;
      while (channelSinkProvider.Next != null)
        channelSinkProvider = channelSinkProvider.Next;
      channelSinkProvider.Next = (IServerChannelSinkProvider) new DispatchChannelSinkProvider();
      IServerChannelSink sink = provider.CreateSink(channel);
      channelSinkProvider.Next = (IServerChannelSinkProvider) null;
      return sink;
    }

    [SecurityCritical]
    internal static ServerIdentity CheckDisconnectedOrCreateWellKnownObject(IMessage msg)
    {
      ServerIdentity serverIdentity = InternalSink.GetServerIdentity(msg);
      if (serverIdentity == null || serverIdentity.IsRemoteDisconnected())
      {
        string uri = InternalSink.GetURI(msg);
        if (uri != null)
        {
          ServerIdentity wellKnownObject = RemotingConfigHandler.CreateWellKnownObject(uri);
          if (wellKnownObject != null)
            serverIdentity = wellKnownObject;
        }
      }
      if (serverIdentity == null || serverIdentity.IsRemoteDisconnected())
        throw new RemotingException(Environment.GetResourceString("Remoting_Disconnected", (object) InternalSink.GetURI(msg)));
      return serverIdentity;
    }

    [SecurityCritical]
    internal static void UnloadHandler(object sender, EventArgs e)
    {
      ChannelServices.StopListeningOnAllChannels();
    }

    [SecurityCritical]
    private static void StopListeningOnAllChannels()
    {
      try
      {
        RegisteredChannelList registeredChannelList = ChannelServices.s_registeredChannels;
        int count = registeredChannelList.Count;
        for (int index = 0; index < count; ++index)
        {
          if (registeredChannelList.IsReceiver(index))
            ((IChannelReceiver) registeredChannelList.GetChannel(index)).StopListening((object) null);
        }
      }
      catch (Exception ex)
      {
      }
    }

    [SecurityCritical]
    internal static void NotifyProfiler(IMessage msg, RemotingProfilerEvent profilerEvent)
    {
      if (profilerEvent != RemotingProfilerEvent.ClientSend)
      {
        if (profilerEvent != RemotingProfilerEvent.ClientReceive || !RemotingServices.CORProfilerTrackRemoting())
          return;
        Guid id = Guid.Empty;
        if (RemotingServices.CORProfilerTrackRemotingCookie())
        {
          object obj = msg.Properties[(object) "CORProfilerCookie"];
          if (obj != null)
            id = (Guid) obj;
        }
        RemotingServices.CORProfilerRemotingClientReceivingReply(id, false);
      }
      else
      {
        if (!RemotingServices.CORProfilerTrackRemoting())
          return;
        Guid id;
        RemotingServices.CORProfilerRemotingClientSendingMessage(out id, false);
        if (!RemotingServices.CORProfilerTrackRemotingCookie())
          return;
        msg.Properties[(object) "CORProfilerCookie"] = (object) id;
      }
    }

    [SecurityCritical]
    internal static string FindFirstHttpUrlForObject(string objectUri)
    {
      if (objectUri == null)
        return (string) null;
      RegisteredChannelList registeredChannelList = ChannelServices.s_registeredChannels;
      int count = registeredChannelList.Count;
      for (int index = 0; index < count; ++index)
      {
        if (registeredChannelList.IsReceiver(index))
        {
          IChannelReceiver channelReceiver = (IChannelReceiver) registeredChannelList.GetChannel(index);
          string fullName = channelReceiver.GetType().FullName;
          if (string.CompareOrdinal(fullName, "System.Runtime.Remoting.Channels.Http.HttpChannel") == 0 || string.CompareOrdinal(fullName, "System.Runtime.Remoting.Channels.Http.HttpServerChannel") == 0)
          {
            string[] urlsForUri = channelReceiver.GetUrlsForUri(objectUri);
            if (urlsForUri != null && urlsForUri.Length != 0)
              return urlsForUri[0];
          }
        }
      }
      return (string) null;
    }
  }
}
