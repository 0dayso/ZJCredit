// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.ApplicationTrust
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Security.Util;

namespace System.Security.Policy
{
  /// <summary>封装关于应用程序的安全决策。此类不能被继承。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class ApplicationTrust : EvidenceBase, ISecurityEncodable
  {
    private ApplicationIdentity m_appId;
    private bool m_appTrustedToRun;
    private bool m_persist;
    private object m_extraInfo;
    private SecurityElement m_elExtraInfo;
    private PolicyStatement m_psDefaultGrant;
    private IList<StrongName> m_fullTrustAssemblies;
    [NonSerialized]
    private int m_grantSetSpecialFlags;

    /// <summary>获取或设置应用程序信任对象的应用程序标识。</summary>
    /// <returns>应用程序信任对象的 <see cref="T:System.ApplicationIdentity" />。</returns>
    /// <exception cref="T:System.ArgumentNullException">无法设置 <see cref="T:System.ApplicationIdentity" />，因为它的值为 null。</exception>
    public ApplicationIdentity ApplicationIdentity
    {
      get
      {
        return this.m_appId;
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException(Environment.GetResourceString("Argument_InvalidAppId"));
        this.m_appId = value;
      }
    }

    /// <summary>获取或设置定义默认授予集的策略声明。</summary>
    /// <returns>描述默认授予的 <see cref="T:System.Security.Policy.PolicyStatement" />。</returns>
    public PolicyStatement DefaultGrantSet
    {
      get
      {
        if (this.m_psDefaultGrant == null)
          return new PolicyStatement(new PermissionSet(PermissionState.None));
        return this.m_psDefaultGrant;
      }
      set
      {
        if (value == null)
        {
          this.m_psDefaultGrant = (PolicyStatement) null;
          this.m_grantSetSpecialFlags = 0;
        }
        else
        {
          this.m_psDefaultGrant = value;
          this.m_grantSetSpecialFlags = SecurityManager.GetSpecialFlags(this.m_psDefaultGrant.PermissionSet, (PermissionSet) null);
        }
      }
    }

    /// <summary>获取此应用程序信任的完全信任程序集的列表。</summary>
    /// <returns>一个完全信任程序集的列表。</returns>
    public IList<StrongName> FullTrustAssemblies
    {
      get
      {
        return this.m_fullTrustAssemblies;
      }
    }

    /// <summary>获取或设置一个值，该值指示应用程序是否具有所需的权限授予并且受信任可运行。</summary>
    /// <returns>如果应用程序受信任可以运行，则为 true；否则为 false。默认值为 false。</returns>
    public bool IsApplicationTrustedToRun
    {
      get
      {
        return this.m_appTrustedToRun;
      }
      set
      {
        this.m_appTrustedToRun = value;
      }
    }

    /// <summary>获取或设置一个值，该值指示是否保留应用程序信任信息。</summary>
    /// <returns>如果保留应用程序信任信息，则为 true，否则为 false。默认值为 false。</returns>
    public bool Persist
    {
      get
      {
        return this.m_persist;
      }
      set
      {
        this.m_persist = value;
      }
    }

    /// <summary>获取或设置有关应用程序的额外安全信息。</summary>
    /// <returns>包含有关应用程序的附加安全信息的对象。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public object ExtraInfo
    {
      get
      {
        if (this.m_elExtraInfo != null)
        {
          this.m_extraInfo = ApplicationTrust.ObjectFromXml(this.m_elExtraInfo);
          this.m_elExtraInfo = (SecurityElement) null;
        }
        return this.m_extraInfo;
      }
      set
      {
        this.m_elExtraInfo = (SecurityElement) null;
        this.m_extraInfo = value;
      }
    }

    /// <summary>使用 <see cref="T:System.ApplicationIdentity" /> 初始化 <see cref="T:System.Security.Policy.ApplicationTrust" /> 类的新实例。</summary>
    /// <param name="applicationIdentity">唯一标识应用程序的 <see cref="T:System.ApplicationIdentity" />。</param>
    public ApplicationTrust(ApplicationIdentity applicationIdentity)
      : this()
    {
      this.ApplicationIdentity = applicationIdentity;
    }

    /// <summary>初始化 <see cref="T:System.Security.Policy.ApplicationTrust" /> 类的新实例。</summary>
    public ApplicationTrust()
      : this(new PermissionSet(PermissionState.None))
    {
    }

    internal ApplicationTrust(PermissionSet defaultGrantSet)
    {
      this.InitDefaultGrantSet(defaultGrantSet);
      this.m_fullTrustAssemblies = (IList<StrongName>) new List<StrongName>().AsReadOnly();
    }

    /// <summary>使用提供的授予集和完全信任程序集的集合，初始化 <see cref="T:System.Security.Policy.ApplicationTrust" /> 类的新实例。</summary>
    /// <param name="defaultGrantSet">一个默认权限集，被授予所有无特定权限的程序集。</param>
    /// <param name="fullTrustAssemblies">一组强名称，表示在应用程序域中应被认为完全受信任的程序集。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="defaultGrantSet" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="fullTrustAssemblies" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="fullTrustAssemblies" /> 包含不具有 <see cref="T:System.Security.Policy.StrongName" /> 的程序集。</exception>
    public ApplicationTrust(PermissionSet defaultGrantSet, IEnumerable<StrongName> fullTrustAssemblies)
    {
      if (fullTrustAssemblies == null)
        throw new ArgumentNullException("fullTrustAssemblies");
      this.InitDefaultGrantSet(defaultGrantSet);
      List<StrongName> strongNameList = new List<StrongName>();
      foreach (StrongName fullTrustAssembly in fullTrustAssemblies)
      {
        if (fullTrustAssembly == null)
          throw new ArgumentException(Environment.GetResourceString("Argument_NullFullTrustAssembly"));
        strongNameList.Add(new StrongName(fullTrustAssembly.PublicKey, fullTrustAssembly.Name, fullTrustAssembly.Version));
      }
      this.m_fullTrustAssemblies = (IList<StrongName>) strongNameList.AsReadOnly();
    }

    private void InitDefaultGrantSet(PermissionSet defaultGrantSet)
    {
      if (defaultGrantSet == null)
        throw new ArgumentNullException("defaultGrantSet");
      this.DefaultGrantSet = new PolicyStatement(defaultGrantSet);
    }

    /// <summary>创建 <see cref="T:System.Security.Policy.ApplicationTrust" /> 对象及其当前状态的 XML 编码。</summary>
    /// <returns>安全对象的 XML 编码，包括任何状态信息。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public SecurityElement ToXml()
    {
      SecurityElement securityElement = new SecurityElement("ApplicationTrust");
      securityElement.AddAttribute("version", "1");
      if (this.m_appId != null)
        securityElement.AddAttribute("FullName", SecurityElement.Escape(this.m_appId.FullName));
      if (this.m_appTrustedToRun)
        securityElement.AddAttribute("TrustedToRun", "true");
      if (this.m_persist)
        securityElement.AddAttribute("Persist", "true");
      if (this.m_psDefaultGrant != null)
      {
        SecurityElement child = new SecurityElement("DefaultGrant");
        child.AddChild(this.m_psDefaultGrant.ToXml());
        securityElement.AddChild(child);
      }
      if (this.m_fullTrustAssemblies.Count > 0)
      {
        SecurityElement child = new SecurityElement("FullTrustAssemblies");
        foreach (StrongName fullTrustAssembly in (IEnumerable<StrongName>) this.m_fullTrustAssemblies)
          child.AddChild(fullTrustAssembly.ToXml());
        securityElement.AddChild(child);
      }
      if (this.ExtraInfo != null)
        securityElement.AddChild(ApplicationTrust.ObjectToXml("ExtraInfo", this.ExtraInfo));
      return securityElement;
    }

    /// <summary>从 XML 编码重新构造具有给定状态的 <see cref="T:System.Security.Policy.ApplicationTrust" /> 对象。</summary>
    /// <param name="element">用于重新构造 <see cref="T:System.Security.Policy.ApplicationTrust" /> 对象的 XML 编码。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">用于 <paramref name="element" /> 的 XML 编码无效。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
    /// </PermissionSet>
    public void FromXml(SecurityElement element)
    {
      if (element == null)
        throw new ArgumentNullException("element");
      if (string.Compare(element.Tag, "ApplicationTrust", StringComparison.Ordinal) != 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidXML"));
      this.m_appTrustedToRun = false;
      string strA1 = element.Attribute("TrustedToRun");
      if (strA1 != null && string.Compare(strA1, "true", StringComparison.Ordinal) == 0)
        this.m_appTrustedToRun = true;
      this.m_persist = false;
      string strA2 = element.Attribute("Persist");
      if (strA2 != null && string.Compare(strA2, "true", StringComparison.Ordinal) == 0)
        this.m_persist = true;
      this.m_appId = (ApplicationIdentity) null;
      string applicationIdentityFullName = element.Attribute("FullName");
      if (applicationIdentityFullName != null && applicationIdentityFullName.Length > 0)
        this.m_appId = new ApplicationIdentity(applicationIdentityFullName);
      this.m_psDefaultGrant = (PolicyStatement) null;
      this.m_grantSetSpecialFlags = 0;
      SecurityElement securityElement1 = element.SearchForChildByTag("DefaultGrant");
      if (securityElement1 != null)
      {
        SecurityElement et = securityElement1.SearchForChildByTag("PolicyStatement");
        if (et != null)
        {
          PolicyStatement policyStatement = new PolicyStatement((PermissionSet) null);
          policyStatement.FromXml(et);
          this.m_psDefaultGrant = policyStatement;
          this.m_grantSetSpecialFlags = SecurityManager.GetSpecialFlags(policyStatement.PermissionSet, (PermissionSet) null);
        }
      }
      List<StrongName> strongNameList = new List<StrongName>();
      SecurityElement securityElement2 = element.SearchForChildByTag("FullTrustAssemblies");
      if (securityElement2 != null && securityElement2.InternalChildren != null)
      {
        foreach (object child in securityElement2.Children)
        {
          StrongName strongName = new StrongName();
          strongName.FromXml(child as SecurityElement);
          strongNameList.Add(strongName);
        }
      }
      this.m_fullTrustAssemblies = (IList<StrongName>) strongNameList.AsReadOnly();
      this.m_elExtraInfo = element.SearchForChildByTag("ExtraInfo");
    }

    private static SecurityElement ObjectToXml(string tag, object obj)
    {
      ISecurityEncodable securityEncodable = obj as ISecurityEncodable;
      if (securityEncodable != null && !securityEncodable.ToXml().Tag.Equals(tag))
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidXML"));
      MemoryStream memoryStream = new MemoryStream();
      new BinaryFormatter().Serialize((Stream) memoryStream, obj);
      byte[] array = memoryStream.ToArray();
      SecurityElement securityElement = new SecurityElement(tag);
      string name = "Data";
      string str = Hex.EncodeHexString(array);
      securityElement.AddAttribute(name, str);
      return securityElement;
    }

    private static object ObjectFromXml(SecurityElement elObject)
    {
      if (elObject.Attribute("class") != null)
      {
        ISecurityEncodable securityEncodable = XMLUtil.CreateCodeGroup(elObject) as ISecurityEncodable;
        if (securityEncodable != null)
        {
          securityEncodable.FromXml(elObject);
          return (object) securityEncodable;
        }
      }
      return new BinaryFormatter().Deserialize((Stream) new MemoryStream(Hex.DecodeHexString(elObject.Attribute("Data"))));
    }

    /// <summary>创建作为当前实例的完整副本的新对象。</summary>
    /// <returns>此应用程序信任对象的重复副本。</returns>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
    public override EvidenceBase Clone()
    {
      return base.Clone();
    }
  }
}
