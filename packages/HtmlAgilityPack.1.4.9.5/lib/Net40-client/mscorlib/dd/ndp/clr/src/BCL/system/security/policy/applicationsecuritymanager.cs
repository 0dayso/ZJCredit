// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.ApplicationSecurityManager
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Deployment.Internal.Isolation.Manifest;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Util;

namespace System.Security.Policy
{
  /// <summary>管理清单激活应用程序的信任决定。</summary>
  [ComVisible(true)]
  public static class ApplicationSecurityManager
  {
    private static volatile IApplicationTrustManager m_appTrustManager = (IApplicationTrustManager) null;
    private static string s_machineConfigFile = Config.MachineDirectory + "applicationtrust.config";

    /// <summary>获取应用程序信任集合，该集合包含用户的缓存的信任决定。</summary>
    /// <returns>包含用户的缓存的信任决定的 <see cref="T:System.Security.Policy.ApplicationTrustCollection" />。</returns>
    public static ApplicationTrustCollection UserApplicationTrusts
    {
      [SecuritySafeCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPolicy)] get
      {
        return new ApplicationTrustCollection(true);
      }
    }

    /// <summary>获取当前应用程序信任关系管理器。</summary>
    /// <returns>表示当前信任关系管理器的 <see cref="T:System.Security.Policy.IApplicationTrustManager" />。</returns>
    /// <exception cref="T:System.Security.Policy.PolicyException">针对此应用程序的策略没有信任关系管理器。</exception>
    public static IApplicationTrustManager ApplicationTrustManager
    {
      [SecuritySafeCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPolicy)] get
      {
        if (ApplicationSecurityManager.m_appTrustManager == null)
        {
          ApplicationSecurityManager.m_appTrustManager = ApplicationSecurityManager.DecodeAppTrustManager();
          if (ApplicationSecurityManager.m_appTrustManager == null)
            throw new PolicyException(Environment.GetResourceString("Policy_NoTrustManager"));
        }
        return ApplicationSecurityManager.m_appTrustManager;
      }
    }

    [SecuritySafeCritical]
    static ApplicationSecurityManager()
    {
    }

    /// <summary>确定用户是否批准指定的应用程序以所请求的权限集执行。</summary>
    /// <returns>如果执行指定的应用程序，则为 true；否则为 false。</returns>
    /// <param name="activationContext">标识应用程序的激活上下文的 <see cref="T:System.ActivationContext" />。</param>
    /// <param name="context">标识应用程序的信任关系管理器上下文的 <see cref="T:System.Security.Policy.TrustManagerContext" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="activationContext" /> 参数为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
    /// </PermissionSet>
    [SecurityCritical]
    [SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
    public static bool DetermineApplicationTrust(ActivationContext activationContext, TrustManagerContext context)
    {
      if (activationContext == null)
        throw new ArgumentNullException("activationContext");
      AppDomainManager domainManager = AppDomain.CurrentDomain.DomainManager;
      if (domainManager != null)
      {
        HostSecurityManager hostSecurityManager = domainManager.HostSecurityManager;
        if (hostSecurityManager != null && (hostSecurityManager.Flags & HostSecurityManagerOptions.HostDetermineApplicationTrust) == HostSecurityManagerOptions.HostDetermineApplicationTrust)
        {
          ApplicationTrust applicationTrust = hostSecurityManager.DetermineApplicationTrust(CmsUtils.MergeApplicationEvidence((Evidence) null, activationContext.Identity, activationContext, (string[]) null), (Evidence) null, context);
          if (applicationTrust == null)
            return false;
          return applicationTrust.IsApplicationTrustedToRun;
        }
      }
      ApplicationTrust applicationTrustInternal = ApplicationSecurityManager.DetermineApplicationTrustInternal(activationContext, context);
      if (applicationTrustInternal == null)
        return false;
      return applicationTrustInternal.IsApplicationTrustedToRun;
    }

    [SecurityCritical]
    internal static ApplicationTrust DetermineApplicationTrustInternal(ActivationContext activationContext, TrustManagerContext context)
    {
      ApplicationTrustCollection applicationTrustCollection = new ApplicationTrustCollection(true);
      if (context == null || !context.IgnorePersistedDecision)
      {
        ApplicationTrust applicationTrust = applicationTrustCollection[activationContext.Identity.FullName];
        if (applicationTrust != null)
          return applicationTrust;
      }
      ApplicationTrust trust = ApplicationSecurityManager.ApplicationTrustManager.DetermineApplicationTrust(activationContext, context) ?? new ApplicationTrust(activationContext.Identity);
      trust.ApplicationIdentity = activationContext.Identity;
      if (trust.Persist)
        applicationTrustCollection.Add(trust);
      return trust;
    }

    [SecurityCritical]
    private static IApplicationTrustManager DecodeAppTrustManager()
    {
      if (File.InternalExists(ApplicationSecurityManager.s_machineConfigFile))
      {
        string end;
        using (FileStream fileStream = new FileStream(ApplicationSecurityManager.s_machineConfigFile, FileMode.Open, FileAccess.Read))
          end = new StreamReader((Stream) fileStream).ReadToEnd();
        SecurityElement securityElement1 = SecurityElement.FromString(end).SearchForChildByTag("mscorlib");
        if (securityElement1 != null)
        {
          SecurityElement securityElement2 = securityElement1.SearchForChildByTag("security");
          if (securityElement2 != null)
          {
            SecurityElement securityElement3 = securityElement2.SearchForChildByTag("policy");
            if (securityElement3 != null)
            {
              SecurityElement securityElement4 = securityElement3.SearchForChildByTag("ApplicationSecurityManager");
              if (securityElement4 != null)
              {
                SecurityElement elTrustManager = securityElement4.SearchForChildByTag("IApplicationTrustManager");
                if (elTrustManager != null)
                {
                  IApplicationTrustManager applicationTrustManager = ApplicationSecurityManager.DecodeAppTrustManagerFromElement(elTrustManager);
                  if (applicationTrustManager != null)
                    return applicationTrustManager;
                }
              }
            }
          }
        }
      }
      return ApplicationSecurityManager.DecodeAppTrustManagerFromElement(ApplicationSecurityManager.CreateDefaultApplicationTrustManagerElement());
    }

    [SecurityCritical]
    private static SecurityElement CreateDefaultApplicationTrustManagerElement()
    {
      SecurityElement securityElement = new SecurityElement("IApplicationTrustManager");
      string name1 = "class";
      string str1 = "System.Security.Policy.TrustManager, System.Windows.Forms, Version=" + (object) ((RuntimeAssembly) Assembly.GetExecutingAssembly()).GetVersion() + ", Culture=neutral, PublicKeyToken=b77a5c561934e089";
      securityElement.AddAttribute(name1, str1);
      string name2 = "version";
      string str2 = "1";
      securityElement.AddAttribute(name2, str2);
      return securityElement;
    }

    [SecurityCritical]
    private static IApplicationTrustManager DecodeAppTrustManagerFromElement(SecurityElement elTrustManager)
    {
      new ReflectionPermission(ReflectionPermissionFlag.MemberAccess).Assert();
      Type type = Type.GetType(elTrustManager.Attribute("class"), false, false);
      if (type == (Type) null)
        return (IApplicationTrustManager) null;
      IApplicationTrustManager applicationTrustManager = Activator.CreateInstance(type) as IApplicationTrustManager;
      if (applicationTrustManager != null)
        applicationTrustManager.FromXml(elTrustManager);
      return applicationTrustManager;
    }
  }
}
