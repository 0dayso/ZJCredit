// Decompiled with JetBrains decompiler
// Type: System.Security.HostSecurityManager
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Deployment.Internal.Isolation.Manifest;
using System.Reflection;
using System.Runtime.Hosting;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Policy;

namespace System.Security
{
  /// <summary>允许控制和自定义应用程序域的安全行为。</summary>
  [SecurityCritical]
  [ComVisible(true)]
  [Serializable]
  [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
  public class HostSecurityManager
  {
    /// <summary>获取表示与主机相关的安全策略组件的标志。</summary>
    /// <returns>用于指定安全策略组件的枚举值之一。默认值为 <see cref="F:System.Security.HostSecurityManagerOptions.AllFlags" />。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public virtual HostSecurityManagerOptions Flags
    {
      get
      {
        return HostSecurityManagerOptions.AllFlags;
      }
    }

    /// <summary>当在派生类中重写时，获取当前应用程序域的安全策略。</summary>
    /// <returns>当前应用程序域的安全策略。默认值为 null。</returns>
    /// <exception cref="T:System.NotSupportedException">此方法使用代码访问安全性 (CAS) 策略，而该策略在 .NET Framework 4 中已过时。若要使 CAS 策略兼容于早期版本的 .NET Framework，请使用 &lt;legacyCasPolicy&gt; 元素。&lt;NetFx40_LegacySecurityPolicy&gt; 元素</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [Obsolete("AppDomain policy levels are obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    public virtual PolicyLevel DomainPolicy
    {
      get
      {
        if (!AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyExplicit"));
        return (PolicyLevel) null;
      }
    }

    /// <summary>为正在加载的程序集提供应用程序域证据。</summary>
    /// <returns>要用于 <see cref="T:System.AppDomain" /> 的证据。</returns>
    /// <param name="inputEvidence">要添加到 <see cref="T:System.AppDomain" /> 证据中的附加证据。</param>
    public virtual Evidence ProvideAppDomainEvidence(Evidence inputEvidence)
    {
      return inputEvidence;
    }

    /// <summary>为正在加载的程序集提供程序集证据。</summary>
    /// <returns>要用于程序集的证据。</returns>
    /// <param name="loadedAssembly">加载的程序集。</param>
    /// <param name="inputEvidence">要添加到程序集证据中的附加证据。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public virtual Evidence ProvideAssemblyEvidence(Assembly loadedAssembly, Evidence inputEvidence)
    {
      return inputEvidence;
    }

    /// <summary>决定是否应执行应用程序。</summary>
    /// <returns>一个对象，包含有关应用程序的信任信息。</returns>
    /// <param name="applicationEvidence">要激活的应用程序的证据。</param>
    /// <param name="activatorEvidence">也可以是正在激活的应用程序域的证据。</param>
    /// <param name="context">信任上下文。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="applicationEvidence" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">未能在应用程序证据中找到 <see cref="T:System.Runtime.Hosting.ActivationArguments" /> 对象。- 或 -激活参数中的 <see cref="P:System.Runtime.Hosting.ActivationArguments.ActivationContext" /> 属性为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <see cref="T:System.Security.Policy.ApplicationTrust" /> 授予集不包含由 <see cref="T:System.ActivationContext" /> 指定的最小请求集。</exception>
    [SecurityCritical]
    [SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
    public virtual ApplicationTrust DetermineApplicationTrust(Evidence applicationEvidence, Evidence activatorEvidence, TrustManagerContext context)
    {
      if (applicationEvidence == null)
        throw new ArgumentNullException("applicationEvidence");
      ActivationArguments hostEvidence = applicationEvidence.GetHostEvidence<ActivationArguments>();
      if (hostEvidence == null)
        throw new ArgumentException(Environment.GetResourceString("Policy_MissingActivationContextInAppEvidence"));
      ActivationContext activationContext = hostEvidence.ActivationContext;
      if (activationContext == null)
        throw new ArgumentException(Environment.GetResourceString("Policy_MissingActivationContextInAppEvidence"));
      ApplicationTrust applicationTrust = applicationEvidence.GetHostEvidence<ApplicationTrust>();
      if (applicationTrust != null && !CmsUtils.CompareIdentities(applicationTrust.ApplicationIdentity, hostEvidence.ApplicationIdentity, ApplicationVersionMatch.MatchExactVersion))
        applicationTrust = (ApplicationTrust) null;
      if (applicationTrust == null)
        applicationTrust = AppDomain.CurrentDomain.ApplicationTrust == null || !CmsUtils.CompareIdentities(AppDomain.CurrentDomain.ApplicationTrust.ApplicationIdentity, hostEvidence.ApplicationIdentity, ApplicationVersionMatch.MatchExactVersion) ? ApplicationSecurityManager.DetermineApplicationTrustInternal(activationContext, context) : AppDomain.CurrentDomain.ApplicationTrust;
      ApplicationSecurityInfo applicationSecurityInfo = new ApplicationSecurityInfo(activationContext);
      if (applicationTrust != null && applicationTrust.IsApplicationTrustedToRun && !applicationSecurityInfo.DefaultRequestSet.IsSubsetOf(applicationTrust.DefaultGrantSet.PermissionSet))
        throw new InvalidOperationException(Environment.GetResourceString("Policy_AppTrustMustGrantAppRequest"));
      return applicationTrust;
    }

    /// <summary>基于指定的证据确定授予代码哪些权限。</summary>
    /// <returns>可由安全系统授予的权限集。</returns>
    /// <param name="evidence">用于评估策略的证据集。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="evidence" /> 为 null。</exception>
    public virtual PermissionSet ResolvePolicy(Evidence evidence)
    {
      if (evidence == null)
        throw new ArgumentNullException("evidence");
      if (evidence.GetHostEvidence<GacInstalled>() != null)
        return new PermissionSet(PermissionState.Unrestricted);
      if (AppDomain.CurrentDomain.IsHomogenous)
        return AppDomain.CurrentDomain.GetHomogenousGrantSet(evidence);
      if (!AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
        return new PermissionSet(PermissionState.Unrestricted);
      return SecurityManager.PolicyManager.CodeGroupResolve(evidence, false);
    }

    /// <summary>确定主机可以为应用程序域提供哪些证据类型（如果请求了证据类型）。</summary>
    /// <returns>证据类型的数组。</returns>
    public virtual Type[] GetHostSuppliedAppDomainEvidenceTypes()
    {
      return (Type[]) null;
    }

    /// <summary>确定主机可以为程序集提供哪些证据类型（如果请求了证据类型）。</summary>
    /// <returns>证据类型的数组。</returns>
    /// <param name="assembly">目标程序集。</param>
    public virtual Type[] GetHostSuppliedAssemblyEvidenceTypes(Assembly assembly)
    {
      return (Type[]) null;
    }

    /// <summary>请求应用程序域的特定证据类型。</summary>
    /// <returns>请求的应用程序域证据。</returns>
    /// <param name="evidenceType">证据类型。</param>
    public virtual EvidenceBase GenerateAppDomainEvidence(Type evidenceType)
    {
      return (EvidenceBase) null;
    }

    /// <summary>请求程序集的特定证据类型。</summary>
    /// <returns>请求的程序集证据。</returns>
    /// <param name="evidenceType">证据类型。</param>
    /// <param name="assembly">目标程序集。</param>
    public virtual EvidenceBase GenerateAssemblyEvidence(Type evidenceType, Assembly assembly)
    {
      return (EvidenceBase) null;
    }
  }
}
