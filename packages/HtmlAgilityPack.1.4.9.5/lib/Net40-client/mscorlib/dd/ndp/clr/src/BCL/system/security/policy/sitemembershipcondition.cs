// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.SiteMembershipCondition
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Policy
{
  /// <summary>通过测试从其中产生程序集的站点确定该程序集是否属于代码组。此类不能被继承。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class SiteMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable, IConstantMembershipCondition, IReportMatchMembershipCondition
  {
    private SiteString m_site;
    private SecurityElement m_element;

    /// <summary>获取或设置要针对其测试成员条件的站点。</summary>
    /// <returns>要针对其测试成员条件的站点。</returns>
    /// <exception cref="T:System.ArgumentNullException">尝试将 <see cref="P:System.Security.Policy.SiteMembershipCondition.Site" /> 设置为 null。</exception>
    /// <exception cref="T:System.ArgumentException">尝试将 <see cref="P:System.Security.Policy.SiteMembershipCondition.Site" /> 设置为无效的 <see cref="T:System.Security.Policy.Site" />。</exception>
    public string Site
    {
      get
      {
        if (this.m_site == null && this.m_element != null)
          this.ParseSite();
        if (this.m_site != null)
          return this.m_site.ToString();
        return "";
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("value");
        this.m_site = new SiteString(value);
      }
    }

    internal SiteMembershipCondition()
    {
      this.m_site = (SiteString) null;
    }

    /// <summary>用确定成员身份的站点名称初始化 <see cref="T:System.Security.Policy.SiteMembershipCondition" /> 类的新实例。</summary>
    /// <param name="site">站点名称或通配符表达式。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="site" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="site" /> 参数不是有效的 <see cref="T:System.Security.Policy.Site" />。</exception>
    public SiteMembershipCondition(string site)
    {
      if (site == null)
        throw new ArgumentNullException("site");
      this.m_site = new SiteString(site);
    }

    /// <summary>确定指定的证据是否能满足成员条件。</summary>
    /// <returns>如果指定的证据满足成员条件，则为 true；否则为 false。</returns>
    /// <param name="evidence">进行测试所依据的 <see cref="T:System.Security.Policy.Evidence" />。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <see cref="P:System.Security.Policy.SiteMembershipCondition.Site" /> 属性为 null。</exception>
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
      System.Security.Policy.Site hostEvidence = evidence.GetHostEvidence<System.Security.Policy.Site>();
      if (hostEvidence != null)
      {
        if (this.m_site == null && this.m_element != null)
          this.ParseSite();
        if (hostEvidence.GetSiteString().IsSubsetOf(this.m_site))
        {
          usedEvidence = (object) hostEvidence;
          return true;
        }
      }
      return false;
    }

    /// <summary>创建成员条件的等效副本。</summary>
    /// <returns>当前成员条件的完全相同的新副本。</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <see cref="P:System.Security.Policy.SiteMembershipCondition.Site" /> 属性为 null。</exception>
    public IMembershipCondition Copy()
    {
      if (this.m_site == null && this.m_element != null)
        this.ParseSite();
      return (IMembershipCondition) new SiteMembershipCondition(this.m_site.ToString());
    }

    /// <summary>创建安全对象及其当前状态的 XML 编码。</summary>
    /// <returns>安全对象的 XML 编码，包括任何状态信息。</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <see cref="P:System.Security.Policy.SiteMembershipCondition.Site" /> 属性为 null。</exception>
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
    /// <exception cref="T:System.ArgumentException">
    /// <see cref="P:System.Security.Policy.SiteMembershipCondition.Site" /> 属性为 null。</exception>
    public SecurityElement ToXml(PolicyLevel level)
    {
      if (this.m_site == null && this.m_element != null)
        this.ParseSite();
      SecurityElement element = new SecurityElement("IMembershipCondition");
      XMLUtil.AddClassAttribute(element, this.GetType(), "System.Security.Policy.SiteMembershipCondition");
      element.AddAttribute("version", "1");
      if (this.m_site != null)
        element.AddAttribute("Site", this.m_site.ToString());
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
        this.m_site = (SiteString) null;
        this.m_element = e;
      }
    }

    private void ParseSite()
    {
      lock (this)
      {
        if (this.m_element == null)
          return;
        string local_2 = this.m_element.Attribute("Site");
        if (local_2 == null)
          throw new ArgumentException(Environment.GetResourceString("Argument_SiteCannotBeNull"));
        this.m_site = new SiteString(local_2);
        this.m_element = (SecurityElement) null;
      }
    }

    /// <summary>确定指定 <see cref="T:System.Security.Policy.SiteMembershipCondition" /> 对象中的站点是否等效于包含在当前 <see cref="T:System.Security.Policy.SiteMembershipCondition" /> 中的站点。</summary>
    /// <returns>如果指定 <see cref="T:System.Security.Policy.SiteMembershipCondition" /> 对象中的站点等效于包含在当前 <see cref="T:System.Security.Policy.SiteMembershipCondition" /> 中的站点，则为 true；否则，为 false。</returns>
    /// <param name="o">要与当前 <see cref="T:System.Security.Policy.SiteMembershipCondition" /> 进行比较的 <see cref="T:System.Security.Policy.SiteMembershipCondition" /> 对象。</param>
    /// <exception cref="T:System.ArgumentException">当前对象或指定对象的 <see cref="P:System.Security.Policy.SiteMembershipCondition.Site" /> 属性为 null。</exception>
    public override bool Equals(object o)
    {
      SiteMembershipCondition membershipCondition = o as SiteMembershipCondition;
      if (membershipCondition != null)
      {
        if (this.m_site == null && this.m_element != null)
          this.ParseSite();
        if (membershipCondition.m_site == null && membershipCondition.m_element != null)
          membershipCondition.ParseSite();
        if (object.Equals((object) this.m_site, (object) membershipCondition.m_site))
          return true;
      }
      return false;
    }

    /// <summary>获取当前成员条件的哈希代码。</summary>
    /// <returns>当前成员条件的哈希代码。</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <see cref="P:System.Security.Policy.SiteMembershipCondition.Site" /> 属性为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public override int GetHashCode()
    {
      if (this.m_site == null && this.m_element != null)
        this.ParseSite();
      if (this.m_site != null)
        return this.m_site.GetHashCode();
      return typeof (SiteMembershipCondition).GetHashCode();
    }

    /// <summary>创建并返回成员条件的字符串表示形式。</summary>
    /// <returns>成员条件的字符串表示形式。</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <see cref="P:System.Security.Policy.SiteMembershipCondition.Site" /> 属性为 null。</exception>
    public override string ToString()
    {
      if (this.m_site == null && this.m_element != null)
        this.ParseSite();
      if (this.m_site != null)
        return string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Site_ToStringArg"), (object) this.m_site);
      return Environment.GetResourceString("Site_ToString");
    }
  }
}
