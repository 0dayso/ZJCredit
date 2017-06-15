// Decompiled with JetBrains decompiler
// Type: System.AppDomainManager
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Hosting;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Threading;

namespace System
{
  /// <summary>提供非托管宿主的等效托管宿主。</summary>
  /// <exception cref="T:System.Security.SecurityException">调用方没有正确的权限。请参阅“要求”一节。</exception>
  /// <filterpriority>2</filterpriority>
  [SecurityCritical]
  [ComVisible(true)]
  [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
  public class AppDomainManager : MarshalByRefObject
  {
    private AppDomainManagerInitializationOptions m_flags;
    private ApplicationActivator m_appActivator;
    private Assembly m_entryAssembly;

    /// <summary>获取自定义应用程序域管理器的初始化标志。</summary>
    /// <returns>枚举值的按位组合，这些枚举值描述要执行的初始化操作。默认值为 <see cref="F:System.AppDomainManagerInitializationOptions.None" />。</returns>
    /// <filterpriority>1</filterpriority>
    public AppDomainManagerInitializationOptions InitializationFlags
    {
      get
      {
        return this.m_flags;
      }
      set
      {
        this.m_flags = value;
      }
    }

    /// <summary>获取应用程序激活器，该激活器用于激活相应域中的外接程序和基于清单的应用程序。</summary>
    /// <returns>应用程序激活器。</returns>
    /// <filterpriority>1</filterpriority>
    public virtual ApplicationActivator ApplicationActivator
    {
      get
      {
        if (this.m_appActivator == null)
          this.m_appActivator = new ApplicationActivator();
        return this.m_appActivator;
      }
    }

    /// <summary>获取宿主安全管理器，该管理器参与应用程序域的安全决策。</summary>
    /// <returns>宿主安全管理器。</returns>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public virtual HostSecurityManager HostSecurityManager
    {
      get
      {
        return (HostSecurityManager) null;
      }
    }

    /// <summary>获取宿主执行上下文管理器，该管理器对执行上下文的流进行管理。</summary>
    /// <returns>宿主执行上下文管理器。</returns>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public virtual HostExecutionContextManager HostExecutionContextManager
    {
      get
      {
        return HostExecutionContextManager.GetInternalHostExecutionContextManager();
      }
    }

    /// <summary>获取应用程序的入口程序集。</summary>
    /// <returns>应用程序的入口程序集。</returns>
    /// <filterpriority>1</filterpriority>
    public virtual Assembly EntryAssembly
    {
      [SecurityCritical] get
      {
        if (this.m_entryAssembly == (Assembly) null)
        {
          AppDomain currentDomain = AppDomain.CurrentDomain;
          if (currentDomain.IsDefaultAppDomain() && currentDomain.ActivationContext != null)
          {
            AppDomain domain = currentDomain;
            ActivationContext activationContext = domain.ActivationContext;
            this.m_entryAssembly = (Assembly) new ManifestRunner(domain, activationContext).EntryAssembly;
          }
          else
          {
            RuntimeAssembly o = (RuntimeAssembly) null;
            AppDomainManager.GetEntryAssembly(JitHelpers.GetObjectHandleOnStack<RuntimeAssembly>(ref o));
            this.m_entryAssembly = (Assembly) o;
          }
        }
        return this.m_entryAssembly;
      }
    }

    internal static AppDomainManager CurrentAppDomainManager
    {
      [SecurityCritical] get
      {
        return AppDomain.CurrentDomain.DomainManager;
      }
    }

    /// <summary>返回新的或现有的应用程序域。</summary>
    /// <returns>新的或现有的应用程序域。</returns>
    /// <param name="friendlyName">域的友好名称。</param>
    /// <param name="securityInfo">一个对象，其中包含通过安全策略映射的证据，这些证据用于建立堆栈顶层的权限集。</param>
    /// <param name="appDomainInfo">包含应用程序域初始化信息的对象。</param>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlAppDomain, Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public virtual AppDomain CreateDomain(string friendlyName, Evidence securityInfo, AppDomainSetup appDomainInfo)
    {
      return AppDomainManager.CreateDomainHelper(friendlyName, securityInfo, appDomainInfo);
    }

    /// <summary>提供帮助器方法以创建一个应用程序域。</summary>
    /// <returns>新创建的应用程序域。</returns>
    /// <param name="friendlyName">域的友好名称。</param>
    /// <param name="securityInfo">一个对象，其中包含通过安全策略映射的证据，这些证据用于建立堆栈顶层的权限集。</param>
    /// <param name="appDomainInfo">包含应用程序域初始化信息的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="friendlyName" /> 为 null。</exception>
    [SecurityCritical]
    [SecurityPermission(SecurityAction.Demand, ControlAppDomain = true)]
    protected static AppDomain CreateDomainHelper(string friendlyName, Evidence securityInfo, AppDomainSetup appDomainInfo)
    {
      if (friendlyName == null)
        throw new ArgumentNullException(Environment.GetResourceString("ArgumentNull_String"));
      if (securityInfo != null)
      {
        new SecurityPermission(SecurityPermissionFlag.ControlEvidence).Demand();
        AppDomain.CheckDomainCreationEvidence(appDomainInfo, securityInfo);
      }
      if (appDomainInfo == null)
        appDomainInfo = new AppDomainSetup();
      if (appDomainInfo.AppDomainManagerAssembly == null || appDomainInfo.AppDomainManagerType == null)
      {
        string assembly;
        string type;
        AppDomain.CurrentDomain.GetAppDomainManagerType(out assembly, out type);
        if (appDomainInfo.AppDomainManagerAssembly == null)
          appDomainInfo.AppDomainManagerAssembly = assembly;
        if (appDomainInfo.AppDomainManagerType == null)
          appDomainInfo.AppDomainManagerType = type;
      }
      if (appDomainInfo.TargetFrameworkName == null)
        appDomainInfo.TargetFrameworkName = AppDomain.CurrentDomain.GetTargetFrameworkName();
      string friendlyName1 = friendlyName;
      AppDomainSetup setup = appDomainInfo;
      Evidence providedSecurityInfo = securityInfo;
      Evidence creatorsSecurityInfo = providedSecurityInfo == null ? AppDomain.CurrentDomain.InternalEvidence : (Evidence) null;
      IntPtr securityDescriptor = AppDomain.CurrentDomain.GetSecurityDescriptor();
      return AppDomain.nCreateDomain(friendlyName1, setup, providedSecurityInfo, creatorsSecurityInfo, securityDescriptor);
    }

    /// <summary>初始化新应用程序域。</summary>
    /// <param name="appDomainInfo">包含应用程序域初始化信息的对象。</param>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public virtual void InitializeNewDomain(AppDomainSetup appDomainInfo)
    {
    }

    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetEntryAssembly(ObjectHandleOnStack retAssembly);

    /// <summary>指示是否允许在应用程序域中执行指定的操作。</summary>
    /// <returns>如果宿主允许在应用程序域中执行 <paramref name="state" /> 指定的操作，则为 true；否则为 false。</returns>
    /// <param name="state">
    /// <see cref="T:System.Security.SecurityState" /> 的一个子类，用来标识请求其安全状态的操作。</param>
    public virtual bool CheckSecuritySettings(SecurityState state)
    {
      return false;
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool HasHost();

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void RegisterWithHost(IntPtr appDomainManager);

    internal void RegisterWithHost()
    {
      if (!AppDomainManager.HasHost())
        return;
      IntPtr num = IntPtr.Zero;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        num = Marshal.GetIUnknownForObject((object) this);
        AppDomainManager.RegisterWithHost(num);
      }
      finally
      {
        if (!num.IsNull())
          Marshal.Release(num);
      }
    }
  }
}
