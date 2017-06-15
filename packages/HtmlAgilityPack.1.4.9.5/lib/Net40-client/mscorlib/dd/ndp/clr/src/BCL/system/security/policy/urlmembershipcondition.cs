// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.UrlMembershipCondition
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Policy
{
  /// <summary>通过测试程序集的 URL 确定该程序集是否属于代码组。此类不能被继承。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class UrlMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable, IConstantMembershipCondition, IReportMatchMembershipCondition
  {
    private URLString m_url;
    private SecurityElement m_element;

    /// <summary>获取或设置要针对其测试成员条件的 URL。</summary>
    /// <returns>要针对其测试成员条件的 URL。</returns>
    /// <exception cref="T:System.ArgumentNullException">尝试将 <see cref="P:System.Security.Policy.UrlMembershipCondition.Url" /> 设置为 null。</exception>
    /// <exception cref="T:System.ArgumentException">值不是绝对 URL。</exception>
    public string Url
    {
      get
      {
        if (this.m_url == null && this.m_element != null)
          this.ParseURL();
        return this.m_url.ToString();
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("value");
        URLString urlString = new URLString(value);
        if (urlString.IsRelativeFileUrl)
          throw new ArgumentException(Environment.GetResourceString("Argument_RelativeUrlMembershipCondition"), "value");
        this.m_url = urlString;
      }
    }

    internal UrlMembershipCondition()
    {
      this.m_url = (URLString) null;
    }

    /// <summary>用确定成员身份的 URL 初始化 <see cref="T:System.Security.Policy.UrlMembershipCondition" /> 类的新实例。</summary>
    /// <param name="url">要对其进行测试的 URL。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="url" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="url" /> 必须为绝对 URL。</exception>
    public UrlMembershipCondition(string url)
    {
      if (url == null)
        throw new ArgumentNullException("url");
      this.m_url = new URLString(url, false, true);
      if (this.m_url.IsRelativeFileUrl)
        throw new ArgumentException(Environment.GetResourceString("Argument_RelativeUrlMembershipCondition"), "url");
    }

    /// <summary>确定指定的证据是否能满足成员条件。</summary>
    /// <returns>如果指定的证据满足成员条件，则为 true；否则为 false。</returns>
    /// <param name="evidence">证据集，将根据它进行测试。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <see cref="P:System.Security.Policy.UrlMembershipCondition.Url" /> 属性为 null。</exception>
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
      System.Security.Policy.Url hostEvidence = evidence.GetHostEvidence<System.Security.Policy.Url>();
      if (hostEvidence != null)
      {
        if (this.m_url == null && this.m_element != null)
          this.ParseURL();
        if (hostEvidence.GetURLString().IsSubsetOf((SiteString) this.m_url))
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
    /// <see cref="P:System.Security.Policy.UrlMembershipCondition.Url" /> 属性为 null。</exception>
    public IMembershipCondition Copy()
    {
      if (this.m_url == null && this.m_element != null)
        this.ParseURL();
      return (IMembershipCondition) new UrlMembershipCondition() { m_url = new URLString(this.m_url.ToString()) };
    }

    /// <summary>创建安全对象及其当前状态的 XML 编码。</summary>
    /// <returns>安全对象的 XML 编码，包括任何状态信息。</returns>
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
    /// <param name="level">用于解析命名的权限集引用的策略级别上下文。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <see cref="P:System.Security.Policy.UrlMembershipCondition.Url" /> 属性为 null。</exception>
    public SecurityElement ToXml(PolicyLevel level)
    {
      if (this.m_url == null && this.m_element != null)
        this.ParseURL();
      SecurityElement element = new SecurityElement("IMembershipCondition");
      XMLUtil.AddClassAttribute(element, this.GetType(), "System.Security.Policy.UrlMembershipCondition");
      element.AddAttribute("version", "1");
      if (this.m_url != null)
        element.AddAttribute("Url", this.m_url.ToString());
      return element;
    }

    /// <summary>用 XML 编码重新构造具有指定状态的安全对象。</summary>
    /// <param name="e">用于重新构造安全对象的 XML 编码。</param>
    /// <param name="level">策略级别上下文，用于解析命名的权限集引用。</param>
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
        this.m_url = (URLString) null;
      }
    }

    private void ParseURL()
    {
      lock (this)
      {
        if (this.m_element == null)
          return;
        string local_2 = this.m_element.Attribute("Url");
        if (local_2 == null)
          throw new ArgumentException(Environment.GetResourceString("Argument_UrlCannotBeNull"));
        URLString local_3 = new URLString(local_2);
        if (local_3.IsRelativeFileUrl)
          throw new ArgumentException(Environment.GetResourceString("Argument_RelativeUrlMembershipCondition"));
        this.m_url = local_3;
        this.m_element = (SecurityElement) null;
      }
    }

    /// <summary>确定指定对象中的 URL 是否等效于包含在当前 <see cref="T:System.Security.Policy.UrlMembershipCondition" /> 中的 URL。</summary>
    /// <returns>如果指定对象中的 URL 等效于包含在当前 <see cref="T:System.Security.Policy.UrlMembershipCondition" /> 中的 URL，则为 true；否则为 false。</returns>
    /// <param name="o">与当前的 <see cref="T:System.Security.Policy.UrlMembershipCondition" /> 比较的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">当前对象或指定对象的 <see cref="P:System.Security.Policy.UrlMembershipCondition.Url" /> 属性是 null。</exception>
    public override bool Equals(object o)
    {
      UrlMembershipCondition membershipCondition = o as UrlMembershipCondition;
      if (membershipCondition != null)
      {
        if (this.m_url == null && this.m_element != null)
          this.ParseURL();
        if (membershipCondition.m_url == null && membershipCondition.m_element != null)
          membershipCondition.ParseURL();
        if (object.Equals((object) this.m_url, (object) membershipCondition.m_url))
          return true;
      }
      return false;
    }

    /// <summary>获取当前成员条件的哈希代码。</summary>
    /// <returns>当前成员条件的哈希代码。</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <see cref="P:System.Security.Policy.UrlMembershipCondition.Url" /> 属性为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public override int GetHashCode()
    {
      if (this.m_url == null && this.m_element != null)
        this.ParseURL();
      if (this.m_url != null)
        return this.m_url.GetHashCode();
      return typeof (UrlMembershipCondition).GetHashCode();
    }

    /// <summary>创建并返回成员条件的字符串表示形式。</summary>
    /// <returns>成员条件状态的字符串表示形式。</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <see cref="P:System.Security.Policy.UrlMembershipCondition.Url" /> 属性为 null。</exception>
    public override string ToString()
    {
      if (this.m_url == null && this.m_element != null)
        this.ParseURL();
      if (this.m_url != null)
        return string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Url_ToStringArg"), (object) this.m_url.ToString());
      return Environment.GetResourceString("Url_ToString");
    }
  }
}
