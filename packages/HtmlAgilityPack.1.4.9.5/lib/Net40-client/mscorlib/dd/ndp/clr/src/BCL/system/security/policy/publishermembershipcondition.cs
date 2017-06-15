// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.PublisherMembershipCondition
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Util;

namespace System.Security.Policy
{
  /// <summary>通过测试程序集的软件发行者 Authenticode X.509v3 证书确定程序集是否属于代码组。此类不能被继承。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class PublisherMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable, IConstantMembershipCondition, IReportMatchMembershipCondition
  {
    private X509Certificate m_certificate;
    private SecurityElement m_element;

    /// <summary>获取或设置要针对其测试成员条件的 Authenticode X.509v3 证书。</summary>
    /// <returns>要针对其测试成员条件的 <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" />。</returns>
    /// <exception cref="T:System.ArgumentNullException">属性值为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Create" />
    /// </PermissionSet>
    public X509Certificate Certificate
    {
      get
      {
        if (this.m_certificate == null && this.m_element != null)
          this.ParseCertificate();
        if (this.m_certificate != null)
          return new X509Certificate(this.m_certificate);
        return (X509Certificate) null;
      }
      set
      {
        PublisherMembershipCondition.CheckCertificate(value);
        this.m_certificate = new X509Certificate(value);
      }
    }

    internal PublisherMembershipCondition()
    {
      this.m_element = (SecurityElement) null;
      this.m_certificate = (X509Certificate) null;
    }

    /// <summary>使用确定成员身份的 Authenticode X.509v3 证书初始化 <see cref="T:System.Security.Policy.PublisherMembershipCondition" /> 类的新实例。</summary>
    /// <param name="certificate">一个 <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" />，它包含软件发行者的公钥。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="certificate" /> 参数为 null。</exception>
    public PublisherMembershipCondition(X509Certificate certificate)
    {
      PublisherMembershipCondition.CheckCertificate(certificate);
      this.m_certificate = new X509Certificate(certificate);
    }

    private static void CheckCertificate(X509Certificate certificate)
    {
      if (certificate == null)
        throw new ArgumentNullException("certificate");
    }

    /// <summary>创建并返回 <see cref="T:System.Security.Policy.PublisherMembershipCondition" /> 的字符串表示形式。</summary>
    /// <returns>
    /// <see cref="T:System.Security.Policy.PublisherMembershipCondition" /> 的表示形式。</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <see cref="P:System.Security.Policy.PublisherMembershipCondition.Certificate" /> 属性为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Create" />
    /// </PermissionSet>
    public override string ToString()
    {
      if (this.m_certificate == null && this.m_element != null)
        this.ParseCertificate();
      if (this.m_certificate == null || this.m_certificate.Subject == null)
        return Environment.GetResourceString("Publisher_ToString");
      return string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Publisher_ToStringArg"), (object) Hex.EncodeHexString(this.m_certificate.GetPublicKey()));
    }

    /// <summary>确定指定的证据是否能满足成员条件。</summary>
    /// <returns>如果指定的证据满足成员条件，则为 true；否则为 false。</returns>
    /// <param name="evidence">进行测试所依据的 <see cref="T:System.Security.Policy.Evidence" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <see cref="P:System.Security.Policy.PublisherMembershipCondition.Certificate" /> 属性为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Create" />
    /// </PermissionSet>
    public bool Check(Evidence evidence)
    {
      object usedEvidence = (object) null;
      return ((IReportMatchMembershipCondition) this).Check(evidence, out usedEvidence);
    }

    bool IReportMatchMembershipCondition.Check(Evidence evidence, out object usedEvidence)
    {
      usedEvidence = (object) null;
      if (evidence == null)
        return false;
      Publisher hostEvidence = evidence.GetHostEvidence<Publisher>();
      if (hostEvidence != null)
      {
        if (this.m_certificate == null && this.m_element != null)
          this.ParseCertificate();
        if (hostEvidence.Equals((object) new Publisher(this.m_certificate)))
        {
          usedEvidence = (object) hostEvidence;
          return true;
        }
      }
      return false;
    }

    /// <summary>创建成员条件的等效副本。</summary>
    /// <returns>当前成员条件的完全相同的新副本。</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <see cref="P:System.Security.Policy.PublisherMembershipCondition.Certificate" /> 属性为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Create" />
    /// </PermissionSet>
    public IMembershipCondition Copy()
    {
      if (this.m_certificate == null && this.m_element != null)
        this.ParseCertificate();
      return (IMembershipCondition) new PublisherMembershipCondition(this.m_certificate);
    }

    /// <summary>创建安全对象及其当前状态的 XML 编码。</summary>
    /// <returns>安全对象的 XML 编码，包括任何状态信息。</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <see cref="P:System.Security.Policy.PublisherMembershipCondition.Certificate" /> 属性为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Create" />
    /// </PermissionSet>
    public SecurityElement ToXml()
    {
      return this.ToXml((PolicyLevel) null);
    }

    /// <summary>用 XML 编码重新构造具有指定状态的安全对象。</summary>
    /// <param name="e">用于重新构造安全对象的 XML 编码。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="e" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="e" /> 参数不是有效的成员条件元素。</exception>
    public void FromXml(SecurityElement e)
    {
      this.FromXml(e, (PolicyLevel) null);
    }

    /// <summary>使用指定的 <see cref="T:System.Security.Policy.PolicyLevel" /> 创建安全对象及其当前状态的 XML 编码。</summary>
    /// <returns>安全对象的 XML 编码，包括任何状态信息。</returns>
    /// <param name="level">
    /// <see cref="T:System.Security.Policy.PolicyLevel" /> 上下文，它用于解析 <see cref="T:System.Security.NamedPermissionSet" /> 引用。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <see cref="P:System.Security.Policy.PublisherMembershipCondition.Certificate" /> 属性为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Create" />
    /// </PermissionSet>
    public SecurityElement ToXml(PolicyLevel level)
    {
      if (this.m_certificate == null && this.m_element != null)
        this.ParseCertificate();
      SecurityElement element = new SecurityElement("IMembershipCondition");
      XMLUtil.AddClassAttribute(element, this.GetType(), "System.Security.Policy.PublisherMembershipCondition");
      element.AddAttribute("version", "1");
      if (this.m_certificate != null)
        element.AddAttribute("X509Certificate", this.m_certificate.GetRawCertDataString());
      return element;
    }

    /// <summary>用 XML 编码重新构造具有指定状态的安全对象。</summary>
    /// <param name="e">用于重新构造安全对象的 XML 编码。</param>
    /// <param name="level">
    /// <see cref="T:System.Security.Policy.PolicyLevel" /> 上下文，它用于解析 <see cref="T:System.Security.NamedPermissionSet" /> 引用。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="e" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="e" /> 参数不是有效的成员条件元素。</exception>
    public void FromXml(SecurityElement e, PolicyLevel level)
    {
      if (e == null)
        throw new ArgumentNullException("e");
      if (!e.Tag.Equals("IMembershipCondition"))
        throw new ArgumentException(Environment.GetResourceString("Argument_MembershipConditionElement"));
      lock (this)
      {
        this.m_element = e;
        this.m_certificate = (X509Certificate) null;
      }
    }

    private void ParseCertificate()
    {
      lock (this)
      {
        if (this.m_element == null)
          return;
        string local_2 = this.m_element.Attribute("X509Certificate");
        this.m_certificate = local_2 == null ? (X509Certificate) null : new X509Certificate(Hex.DecodeHexString(local_2));
        PublisherMembershipCondition.CheckCertificate(this.m_certificate);
        this.m_element = (SecurityElement) null;
      }
    }

    /// <summary>确定指定对象中的发行者证书是否等效于包含在当前 <see cref="T:System.Security.Policy.PublisherMembershipCondition" /> 中的发行者证书。</summary>
    /// <returns>如果指定对象中的发行者证书等效于包含在当前 <see cref="T:System.Security.Policy.PublisherMembershipCondition" /> 中的发行者证书，则为 true；否则，为 false。</returns>
    /// <param name="o">与当前的 <see cref="T:System.Security.Policy.PublisherMembershipCondition" /> 比较的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <see cref="P:System.Security.Policy.PublisherMembershipCondition.Certificate" /> 属性为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Create" />
    /// </PermissionSet>
    public override bool Equals(object o)
    {
      PublisherMembershipCondition membershipCondition = o as PublisherMembershipCondition;
      if (membershipCondition != null)
      {
        if (this.m_certificate == null && this.m_element != null)
          this.ParseCertificate();
        if (membershipCondition.m_certificate == null && membershipCondition.m_element != null)
          membershipCondition.ParseCertificate();
        if (Publisher.PublicKeyEquals(this.m_certificate, membershipCondition.m_certificate))
          return true;
      }
      return false;
    }

    /// <summary>获取当前成员条件的哈希代码。</summary>
    /// <returns>当前成员条件的哈希代码。</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <see cref="P:System.Security.Policy.PublisherMembershipCondition.Certificate" /> 属性为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Create" />
    /// </PermissionSet>
    public override int GetHashCode()
    {
      if (this.m_certificate == null && this.m_element != null)
        this.ParseCertificate();
      if (this.m_certificate != null)
        return this.m_certificate.GetHashCode();
      return typeof (PublisherMembershipCondition).GetHashCode();
    }
  }
}
