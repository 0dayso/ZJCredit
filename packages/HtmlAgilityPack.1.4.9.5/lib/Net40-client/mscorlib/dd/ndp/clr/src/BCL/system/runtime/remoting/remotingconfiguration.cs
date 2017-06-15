// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.RemotingConfiguration
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting
{
  /// <summary>提供多种配置远程处理结构的静态方法。</summary>
  [ComVisible(true)]
  public static class RemotingConfiguration
  {
    private static volatile bool s_ListeningForActivationRequests;

    /// <summary>获取或设置远程处理应用程序的名称。</summary>
    /// <returns>远程处理应用程序的名称。</returns>
    /// <exception cref="T:System.Security.SecurityException">在调用堆栈上部，至少有一个调用方没有配置远程处理类型和通道的权限。仅在设置该属性值时才会引发此异常。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
    /// </PermissionSet>
    public static string ApplicationName
    {
      get
      {
        if (!RemotingConfigHandler.HasApplicationNameBeenSet())
          return (string) null;
        return RemotingConfigHandler.ApplicationName;
      }
      [SecuritySafeCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)] set
      {
        RemotingConfigHandler.ApplicationName = value;
      }
    }

    /// <summary>获取当前正在执行的应用程序的 ID。</summary>
    /// <returns>一个 <see cref="T:System.String" />，包含当前正在执行的应用程序的 ID。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public static string ApplicationId
    {
      [SecurityCritical] get
      {
        return Identity.AppDomainUniqueId;
      }
    }

    /// <summary>获取当前正在执行的进程的 ID。</summary>
    /// <returns>一个 <see cref="T:System.String" />，其中包含当前正在执行的进程的 ID。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public static string ProcessId
    {
      [SecurityCritical] get
      {
        return Identity.ProcessGuid;
      }
    }

    /// <summary>获取或设置指示如何处理自定义错误的值。</summary>
    /// <returns>一个 <see cref="T:System.Runtime.Remoting.CustomErrorsModes" /> 枚举的成员，指示如何处理自定义错误。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
    /// </PermissionSet>
    public static CustomErrorsModes CustomErrorsMode
    {
      get
      {
        return RemotingConfigHandler.CustomErrorsMode;
      }
      [SecuritySafeCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)] set
      {
        RemotingConfigHandler.CustomErrorsMode = value;
      }
    }

    /// <summary>读取配置文件并配置远程处理结构。<see cref="M:System.Runtime.Remoting.RemotingConfiguration.Configure(System.String)" /> 已过时。请改用 <see cref="M:System.Runtime.Remoting.RemotingConfiguration.Configure(System.String,System.Boolean)" />。</summary>
    /// <param name="filename">远程处理配置文件的名称。可以为 null。</param>
    /// <exception cref="T:System.Security.SecurityException">在调用堆栈上部，至少有一个调用方没有配置远程处理类型和通道的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [Obsolete("Use System.Runtime.Remoting.RemotingConfiguration.Configure(string fileName, bool ensureSecurity) instead.", false)]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static void Configure(string filename)
    {
      RemotingConfiguration.Configure(filename, false);
    }

    /// <summary>读取配置文件并配置远程处理结构。</summary>
    /// <param name="filename">远程处理配置文件的名称。可以为 null。</param>
    /// <param name="ensureSecurity">如果设置为 true，则安全是必选项。如果设置为 false，则安全不是必选项，但仍可能会用到。</param>
    /// <exception cref="T:System.Security.SecurityException">在调用堆栈上部，至少有一个调用方没有配置远程处理类型和通道的权限。</exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static void Configure(string filename, bool ensureSecurity)
    {
      RemotingConfigHandler.DoConfiguration(filename, ensureSecurity);
      RemotingServices.InternalSetRemoteActivationConfigured();
    }

    /// <summary>指示该应用程序域中的服务器信道是将筛选过的异常信息返回给本地调用方或远程调用方，还是将完整的异常信息返回给本地调用方或远程调用方。</summary>
    /// <returns>如果只将筛选过的异常信息返回给本地调用方或远程调用方（具体返回给哪些调用方由 <paramref name="isLocalRequest" /> 参数指定），则为 true；如果返回完整的异常信息，则为 false。</returns>
    /// <param name="isLocalRequest">true 用于指定本地调用方，false 用于指定远程调用方。</param>
    public static bool CustomErrorsEnabled(bool isLocalRequest)
    {
      switch (RemotingConfiguration.CustomErrorsMode)
      {
        case CustomErrorsModes.On:
          return true;
        case CustomErrorsModes.Off:
          return false;
        case CustomErrorsModes.RemoteOnly:
          return !isLocalRequest;
        default:
          return true;
      }
    }

    /// <summary>将服务端上指定的对象类型注册为可根据请求从客户端激活的类型。</summary>
    /// <param name="type">要注册的对象的 <see cref="T:System.Type" />。</param>
    /// <exception cref="T:System.Security.SecurityException">在调用堆栈上部，至少有一个调用方没有配置远程处理类型和通道的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static void RegisterActivatedServiceType(Type type)
    {
      RemotingConfiguration.RegisterActivatedServiceType(new ActivatedServiceTypeEntry(type));
    }

    /// <summary>将在服务端提供的 <see cref="T:System.Runtime.Remoting.ActivatedServiceTypeEntry" /> 中记录的对象类型注册为可根据请求从客户端激活的类型。</summary>
    /// <param name="entry">客户端激活类型的配置设置。</param>
    /// <exception cref="T:System.Security.SecurityException">在调用堆栈上部，至少有一个调用方没有配置远程处理类型和通道的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static void RegisterActivatedServiceType(ActivatedServiceTypeEntry entry)
    {
      RemotingConfigHandler.RegisterActivatedServiceType(entry);
      if (RemotingConfiguration.s_ListeningForActivationRequests)
        return;
      RemotingConfiguration.s_ListeningForActivationRequests = true;
      ActivationServices.StartListeningForRemoteRequests();
    }

    /// <summary>通过使用给定的参数初始化 <see cref="T:System.Runtime.Remoting.WellKnownServiceTypeEntry" /> 的新实例，将服务端上的对象 <see cref="T:System.Type" /> 注册为已知类型。</summary>
    /// <param name="type">对象 <see cref="T:System.Type" />。</param>
    /// <param name="objectUri">对象 URI。</param>
    /// <param name="mode">正在被注册的已知对象类型的激活方式。（请参见<see cref="T:System.Runtime.Remoting.WellKnownObjectMode" />。）</param>
    /// <exception cref="T:System.Security.SecurityException">在调用堆栈上部，至少有一个调用方没有配置远程处理类型和通道的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static void RegisterWellKnownServiceType(Type type, string objectUri, WellKnownObjectMode mode)
    {
      RemotingConfiguration.RegisterWellKnownServiceType(new WellKnownServiceTypeEntry(type, objectUri, mode));
    }

    /// <summary>将在服务端提供的 <see cref="T:System.Runtime.Remoting.WellKnownServiceTypeEntry" /> 中记录的对象 <see cref="T:System.Type" /> 注册为已知类型。</summary>
    /// <param name="entry">已知类型的配置设置。</param>
    /// <exception cref="T:System.Security.SecurityException">在调用堆栈上部，至少有一个调用方没有配置远程处理类型和通道的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static void RegisterWellKnownServiceType(WellKnownServiceTypeEntry entry)
    {
      RemotingConfigHandler.RegisterWellKnownServiceType(entry);
    }

    /// <summary>通过使用给定的参数初始化 <see cref="T:System.Runtime.Remoting.ActivatedClientTypeEntry" /> 类的新实例，将客户端上的对象 <see cref="T:System.Type" /> 注册为可在服务器上激活的类型。</summary>
    /// <param name="type">对象 <see cref="T:System.Type" />。</param>
    /// <param name="appUrl">在该处激活类型的应用程序的 URL。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="typeName" /> 或 <paramref name="URI" /> 参数为 null。</exception>
    /// <exception cref="T:System.Security.SecurityException">在调用堆栈上部，至少有一个调用方没有配置远程处理类型和通道的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static void RegisterActivatedClientType(Type type, string appUrl)
    {
      RemotingConfiguration.RegisterActivatedClientType(new ActivatedClientTypeEntry(type, appUrl));
    }

    /// <summary>将在客户端提供的 <see cref="T:System.Runtime.Remoting.ActivatedClientTypeEntry" /> 中记录的对象 <see cref="T:System.Type" /> 注册为可在服务器上激活的类型。</summary>
    /// <param name="entry">客户端激活类型的配置设置。</param>
    /// <exception cref="T:System.Security.SecurityException">在调用堆栈上部，至少有一个调用方没有配置远程处理类型和通道的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static void RegisterActivatedClientType(ActivatedClientTypeEntry entry)
    {
      RemotingConfigHandler.RegisterActivatedClientType(entry);
      RemotingServices.InternalSetRemoteActivationConfigured();
    }

    /// <summary>通过使用给定的参数初始化 <see cref="T:System.Runtime.Remoting.WellKnownClientTypeEntry" /> 类的新实例，将客户端上的对象 <see cref="T:System.Type" /> 注册为可在服务器上激活的已知类型。</summary>
    /// <param name="type">对象 <see cref="T:System.Type" />。</param>
    /// <param name="objectUrl">已知客户端对象的 URL。</param>
    /// <exception cref="T:System.Security.SecurityException">在调用堆栈上部，至少有一个调用方没有配置远程处理类型和通道的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static void RegisterWellKnownClientType(Type type, string objectUrl)
    {
      RemotingConfiguration.RegisterWellKnownClientType(new WellKnownClientTypeEntry(type, objectUrl));
    }

    /// <summary>将在客户端提供的 <see cref="T:System.Runtime.Remoting.WellKnownClientTypeEntry" /> 中记录的对象 <see cref="T:System.Type" /> 注册为可在服务器上激活的已知类型。</summary>
    /// <param name="entry">已知类型的配置设置。</param>
    /// <exception cref="T:System.Security.SecurityException">在调用堆栈上部，至少有一个调用方没有配置远程处理类型和通道的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static void RegisterWellKnownClientType(WellKnownClientTypeEntry entry)
    {
      RemotingConfigHandler.RegisterWellKnownClientType(entry);
      RemotingServices.InternalSetRemoteActivationConfigured();
    }

    /// <summary>检索在服务端上注册的对象类型的数组，可以从客户端根据请求激活这些对象类型。</summary>
    /// <returns>在服务端上注册的对象类型的数组，可以从客户端根据请求激活这些对象类型。</returns>
    /// <exception cref="T:System.Security.SecurityException">在调用堆栈上部，至少有一个调用方没有配置远程处理类型和通道的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static ActivatedServiceTypeEntry[] GetRegisteredActivatedServiceTypes()
    {
      return RemotingConfigHandler.GetRegisteredActivatedServiceTypes();
    }

    /// <summary>检索对象类型的数组，这些对象类型在服务端上注册为已知类型。</summary>
    /// <returns>对象类型的数组，这些对象类型在服务端上注册为已知类型。</returns>
    /// <exception cref="T:System.Security.SecurityException">在调用堆栈上部，至少有一个调用方没有配置远程处理类型和通道的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static WellKnownServiceTypeEntry[] GetRegisteredWellKnownServiceTypes()
    {
      return RemotingConfigHandler.GetRegisteredWellKnownServiceTypes();
    }

    /// <summary>检索对象类型的数组，这些对象类型在客户端上注册为将被远程激活的类型。</summary>
    /// <returns>对象类型的数组，这些对象类型在客户端上注册为将被远程激活的类型。</returns>
    /// <exception cref="T:System.Security.SecurityException">在调用堆栈上部，至少有一个调用方没有配置远程处理类型和通道的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static ActivatedClientTypeEntry[] GetRegisteredActivatedClientTypes()
    {
      return RemotingConfigHandler.GetRegisteredActivatedClientTypes();
    }

    /// <summary>检索对象类型的数组，这些对象类型在客户端上注册为已知类型。</summary>
    /// <returns>对象类型的数组，这些对象类型在客户端上注册为已知类型。</returns>
    /// <exception cref="T:System.Security.SecurityException">在调用堆栈上部，至少有一个调用方没有配置远程处理类型和通道的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static WellKnownClientTypeEntry[] GetRegisteredWellKnownClientTypes()
    {
      return RemotingConfigHandler.GetRegisteredWellKnownClientTypes();
    }

    /// <summary>检查指定的对象 <see cref="T:System.Type" /> 是否注册为远程激活的客户端类型。</summary>
    /// <returns>与指定的对象类型对应的 <see cref="T:System.Runtime.Remoting.ActivatedClientTypeEntry" />。</returns>
    /// <param name="svrType">要检查的对象类型。</param>
    /// <exception cref="T:System.Security.SecurityException">在调用堆栈上部，至少有一个调用方没有配置远程处理类型和通道的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static ActivatedClientTypeEntry IsRemotelyActivatedClientType(Type svrType)
    {
      if (svrType == (Type) null)
        throw new ArgumentNullException("svrType");
      RuntimeType svrType1 = svrType as RuntimeType;
      // ISSUE: variable of the null type
      __Null local = null;
      if (svrType1 == (RuntimeType) local)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
      return RemotingConfigHandler.IsRemotelyActivatedClientType(svrType1);
    }

    /// <summary>检查由其类型名称和程序集名称指定的对象是否注册为远程激活的客户端类型。</summary>
    /// <returns>与指定的对象类型对应的 <see cref="T:System.Runtime.Remoting.ActivatedClientTypeEntry" />。</returns>
    /// <param name="typeName">要检查的对象的类型名称。</param>
    /// <param name="assemblyName">要检查的对象的程序集名称。</param>
    /// <exception cref="T:System.Security.SecurityException">在调用堆栈上部，至少有一个调用方没有配置远程处理类型和通道的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static ActivatedClientTypeEntry IsRemotelyActivatedClientType(string typeName, string assemblyName)
    {
      return RemotingConfigHandler.IsRemotelyActivatedClientType(typeName, assemblyName);
    }

    /// <summary>检查指定的对象 <see cref="T:System.Type" /> 是否注册为已知客户端类型。</summary>
    /// <returns>与指定的对象类型对应的 <see cref="T:System.Runtime.Remoting.WellKnownClientTypeEntry" />。</returns>
    /// <param name="svrType">要检查的对象 <see cref="T:System.Type" />。</param>
    /// <exception cref="T:System.Security.SecurityException">在调用堆栈上部，至少有一个调用方没有配置远程处理类型和通道的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static WellKnownClientTypeEntry IsWellKnownClientType(Type svrType)
    {
      if (svrType == (Type) null)
        throw new ArgumentNullException("svrType");
      RuntimeType svrType1 = svrType as RuntimeType;
      // ISSUE: variable of the null type
      __Null local = null;
      if (svrType1 == (RuntimeType) local)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
      return RemotingConfigHandler.IsWellKnownClientType(svrType1);
    }

    /// <summary>检查由其类型名称和程序集名称指定的对象是否注册为已知客户端类型。</summary>
    /// <returns>与指定的对象类型对应的 <see cref="T:System.Runtime.Remoting.WellKnownClientTypeEntry" />。</returns>
    /// <param name="typeName">要检查的对象的类型名称。</param>
    /// <param name="assemblyName">要检查的对象的程序集名称。</param>
    /// <exception cref="T:System.Security.SecurityException">在调用堆栈上部，至少有一个调用方没有配置远程处理类型和通道的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static WellKnownClientTypeEntry IsWellKnownClientType(string typeName, string assemblyName)
    {
      return RemotingConfigHandler.IsWellKnownClientType(typeName, assemblyName);
    }

    /// <summary>返回一个布尔值，该值指示是否允许由客户端激活指定的 <see cref="T:System.Type" />。</summary>
    /// <returns>如果允许由客户端激活指定的 <see cref="T:System.Type" />，则为 true；否则为 false。</returns>
    /// <param name="svrType">要检查的对象 <see cref="T:System.Type" />。</param>
    /// <exception cref="T:System.Security.SecurityException">在调用堆栈上部，至少有一个调用方没有配置远程处理类型和通道的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static bool IsActivationAllowed(Type svrType)
    {
      RuntimeType svrType1 = svrType as RuntimeType;
      if (svrType != (Type) null && svrType1 == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
      return RemotingConfigHandler.IsActivationAllowed(svrType1);
    }
  }
}
