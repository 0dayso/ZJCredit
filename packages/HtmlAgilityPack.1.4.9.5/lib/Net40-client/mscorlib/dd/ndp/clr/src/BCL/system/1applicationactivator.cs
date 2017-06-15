// Decompiled with JetBrains decompiler
// Type: System.Runtime.Hosting.ApplicationActivator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Deployment.Internal.Isolation.Manifest;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Security;
using System.Security.Policy;

namespace System.Runtime.Hosting
{
  /// <summary>提供用于激活基于清单的程序集的基类。</summary>
  [ComVisible(true)]
  public class ApplicationActivator
  {
    /// <summary>使用指定的激活上下文创建要激活的应用程序的实例。</summary>
    /// <returns>一个 <see cref="T:System.Runtime.Remoting.ObjectHandle" />，是应用程序执行操作的返回值的包装。返回值需要打开包装才能访问真实对象。</returns>
    /// <param name="activationContext">标识要激活的应用程序的 <see cref="T:System.ActivationContext" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="activationContext" /> 为 null。</exception>
    public virtual ObjectHandle CreateInstance(ActivationContext activationContext)
    {
      return this.CreateInstance(activationContext, (string[]) null);
    }

    /// <summary>使用指定的激活上下文和自定义激活数据创建要激活的应用程序的实例。</summary>
    /// <returns>一个 <see cref="T:System.Runtime.Remoting.ObjectHandle" />，是应用程序执行操作的返回值的包装。返回值需要打开包装才能访问真实对象。</returns>
    /// <param name="activationContext">标识要激活的应用程序的 <see cref="T:System.ActivationContext" />。</param>
    /// <param name="activationCustomData">自定义激活数据。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="activationContext" /> 为 null。</exception>
    [SecuritySafeCritical]
    public virtual ObjectHandle CreateInstance(ActivationContext activationContext, string[] activationCustomData)
    {
      if (activationContext == null)
        throw new ArgumentNullException("activationContext");
      if (CmsUtils.CompareIdentities(AppDomain.CurrentDomain.ActivationContext, activationContext))
        return new ObjectHandle((object) new ManifestRunner(AppDomain.CurrentDomain, activationContext).ExecuteAsAssembly());
      AppDomainSetup adSetup = new AppDomainSetup(new ActivationArguments(activationContext, activationCustomData));
      AppDomainSetup setupInformation = AppDomain.CurrentDomain.SetupInformation;
      string domainManagerType = setupInformation.AppDomainManagerType;
      adSetup.AppDomainManagerType = domainManagerType;
      string domainManagerAssembly = setupInformation.AppDomainManagerAssembly;
      adSetup.AppDomainManagerAssembly = domainManagerAssembly;
      return ApplicationActivator.CreateInstanceHelper(adSetup);
    }

    /// <summary>使用指定的 <see cref="T:System.AppDomainSetup" /> 对象创建应用程序的实例。</summary>
    /// <returns>一个 <see cref="T:System.Runtime.Remoting.ObjectHandle" />，是应用程序执行操作的返回值的包装。返回值需要打开包装才能访问真实对象。</returns>
    /// <param name="adSetup">一个 <see cref="T:System.AppDomainSetup" /> 对象，它的 <see cref="P:System.AppDomainSetup.ActivationArguments" /> 属性标识要激活的应用程序。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="adSetup " /> 的 <see cref="P:System.AppDomainSetup.ActivationArguments" /> 属性为 null。</exception>
    /// <exception cref="T:System.Security.Policy.PolicyException">未能执行应用程序实例，因为当前应用程序域上的策略设置没有提供运行该应用程序的权限。</exception>
    [SecuritySafeCritical]
    protected static ObjectHandle CreateInstanceHelper(AppDomainSetup adSetup)
    {
      if (adSetup.ActivationArguments == null)
        throw new ArgumentException(Environment.GetResourceString("Arg_MissingActivationArguments"));
      adSetup.ActivationArguments.ActivateInstance = true;
      Evidence evidence = AppDomain.CurrentDomain.Evidence;
      Evidence applicationEvidence = CmsUtils.MergeApplicationEvidence((Evidence) null, adSetup.ActivationArguments.ApplicationIdentity, adSetup.ActivationArguments.ActivationContext, adSetup.ActivationArguments.ActivationData);
      ApplicationTrust applicationTrust = AppDomain.CurrentDomain.HostSecurityManager.DetermineApplicationTrust(applicationEvidence, evidence, new TrustManagerContext());
      if (applicationTrust == null || !applicationTrust.IsApplicationTrustedToRun)
        throw new PolicyException(Environment.GetResourceString("Policy_NoExecutionPermission"), -2146233320, (Exception) null);
      string fullName = adSetup.ActivationArguments.ApplicationIdentity.FullName;
      AppDomainSetup setup = adSetup;
      Evidence providedSecurityInfo = applicationEvidence;
      Evidence creatorsSecurityInfo = providedSecurityInfo == null ? AppDomain.CurrentDomain.InternalEvidence : (Evidence) null;
      IntPtr securityDescriptor = AppDomain.CurrentDomain.GetSecurityDescriptor();
      ObjRef instance = AppDomain.nCreateInstance(fullName, setup, providedSecurityInfo, creatorsSecurityInfo, securityDescriptor);
      if (instance == null)
        return (ObjectHandle) null;
      return RemotingServices.Unmarshal(instance) as ObjectHandle;
    }
  }
}
