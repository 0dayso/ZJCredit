// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.ZoneMembershipCondition
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Policy
{
  /// <summary>通过测试程序集的原始区域确定该程序集是否属于代码组。此类不能被继承。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class ZoneMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable, IConstantMembershipCondition, IReportMatchMembershipCondition
  {
    private static readonly string[] s_names = new string[5]{ "MyComputer", "Intranet", "Trusted", "Internet", "Untrusted" };
    private SecurityZone m_zone;
    private SecurityElement m_element;

    /// <summary>获取或设置要针对其测试成员条件的区域。</summary>
    /// <returns>要针对其测试成员条件的区域。</returns>
    /// <exception cref="T:System.ArgumentNullException">该值为 null。</exception>
    /// <exception cref="T:System.ArgumentException">尝试将 <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> 设置为无效的 <see cref="T:System.Security.SecurityZone" />。</exception>
    public SecurityZone SecurityZone
    {
      get
      {
        if (this.m_zone == SecurityZone.NoZone && this.m_element != null)
          this.ParseZone();
        return this.m_zone;
      }
      set
      {
        ZoneMembershipCondition.VerifyZone(value);
        this.m_zone = value;
      }
    }

    internal ZoneMembershipCondition()
    {
      this.m_zone = SecurityZone.NoZone;
    }

    /// <summary>用确定成员身份的区域初始化 <see cref="T:System.Security.Policy.ZoneMembershipCondition" /> 类的新实例。</summary>
    /// <param name="zone">要对其进行测试的 <see cref="T:System.Security.SecurityZone" />。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="zone" /> 参数不是有效的 <see cref="T:System.Security.SecurityZone" />。</exception>
    public ZoneMembershipCondition(SecurityZone zone)
    {
      ZoneMembershipCondition.VerifyZone(zone);
      this.SecurityZone = zone;
    }

    private static void VerifyZone(SecurityZone zone)
    {
      if (zone < SecurityZone.MyComputer || zone > SecurityZone.Untrusted)
        throw new ArgumentException(Environment.GetResourceString("Argument_IllegalZone"));
    }

    /// <summary>确定指定的证据是否能满足成员条件。</summary>
    /// <returns>如果指定的证据满足成员条件，则为 true；否则为 false。</returns>
    /// <param name="evidence">证据集，将根据它进行测试。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> 属性为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> 属性不是有效的 <see cref="T:System.Security.SecurityZone" />。</exception>
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
      Zone hostEvidence = evidence.GetHostEvidence<Zone>();
      if (hostEvidence != null)
      {
        if (this.m_zone == SecurityZone.NoZone && this.m_element != null)
          this.ParseZone();
        if (hostEvidence.SecurityZone == this.m_zone)
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
    /// <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> 属性为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> 属性不是有效的 <see cref="T:System.Security.SecurityZone" />。</exception>
    public IMembershipCondition Copy()
    {
      if (this.m_zone == SecurityZone.NoZone && this.m_element != null)
        this.ParseZone();
      return (IMembershipCondition) new ZoneMembershipCondition(this.m_zone);
    }

    /// <summary>创建安全对象及其当前状态的 XML 编码。</summary>
    /// <returns>安全对象的 XML 编码，包括任何状态信息。</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> 属性为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> 属性不是有效的 <see cref="T:System.Security.SecurityZone" />。</exception>
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
    /// <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> 属性为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> 属性不是有效的 <see cref="T:System.Security.SecurityZone" />。</exception>
    public SecurityElement ToXml(PolicyLevel level)
    {
      if (this.m_zone == SecurityZone.NoZone && this.m_element != null)
        this.ParseZone();
      SecurityElement element = new SecurityElement("IMembershipCondition");
      XMLUtil.AddClassAttribute(element, this.GetType(), "System.Security.Policy.ZoneMembershipCondition");
      element.AddAttribute("version", "1");
      if (this.m_zone != SecurityZone.NoZone)
        element.AddAttribute("Zone", Enum.GetName(typeof (SecurityZone), (object) this.m_zone));
      return element;
    }

    /// <summary>用 XML 编码重新构造具有指定状态的安全对象。</summary>
    /// <param name="e">用于重新构造安全对象的 XML 编码。</param>
    /// <param name="level">用于解析命名的权限集引用的策略级别上下文。</param>
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
        this.m_zone = SecurityZone.NoZone;
        this.m_element = e;
      }
    }

    private void ParseZone()
    {
      lock (this)
      {
        if (this.m_element == null)
          return;
        string local_2 = this.m_element.Attribute("Zone");
        this.m_zone = SecurityZone.NoZone;
        if (local_2 == null)
          throw new ArgumentException(Environment.GetResourceString("Argument_ZoneCannotBeNull"));
        this.m_zone = (SecurityZone) Enum.Parse(typeof (SecurityZone), local_2);
        ZoneMembershipCondition.VerifyZone(this.m_zone);
        this.m_element = (SecurityElement) null;
      }
    }

    /// <summary>确定指定对象中的区域是否等效于包含在当前 <see cref="T:System.Security.Policy.ZoneMembershipCondition" /> 中的区域。</summary>
    /// <returns>如果指定对象中的区域等效于包含在当前 <see cref="T:System.Security.Policy.ZoneMembershipCondition" /> 中的区域，则为 true；否则为 false。</returns>
    /// <param name="o">与当前的 <see cref="T:System.Security.Policy.ZoneMembershipCondition" /> 比较的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">当前对象或指定对象的 <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> 属性是 null。</exception>
    /// <exception cref="T:System.ArgumentException">当前对象或指定对象的 <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> 属性不是有效的 <see cref="T:System.Security.SecurityZone" />。</exception>
    public override bool Equals(object o)
    {
      ZoneMembershipCondition membershipCondition = o as ZoneMembershipCondition;
      if (membershipCondition != null)
      {
        if (this.m_zone == SecurityZone.NoZone && this.m_element != null)
          this.ParseZone();
        if (membershipCondition.m_zone == SecurityZone.NoZone && membershipCondition.m_element != null)
          membershipCondition.ParseZone();
        if (this.m_zone == membershipCondition.m_zone)
          return true;
      }
      return false;
    }

    /// <summary>获取当前成员条件的哈希代码。</summary>
    /// <returns>当前成员条件的哈希代码。</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> 属性为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> 属性不是有效的 <see cref="T:System.Security.SecurityZone" />。</exception>
    public override int GetHashCode()
    {
      if (this.m_zone == SecurityZone.NoZone && this.m_element != null)
        this.ParseZone();
      return (int) this.m_zone;
    }

    /// <summary>创建并返回成员条件的字符串表示形式。</summary>
    /// <returns>成员条件状态的字符串表示形式。</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> 属性为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> 属性不是有效的 <see cref="T:System.Security.SecurityZone" />。</exception>
    public override string ToString()
    {
      if (this.m_zone == SecurityZone.NoZone && this.m_element != null)
        this.ParseZone();
      return string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Zone_ToString"), (object) ZoneMembershipCondition.s_names[(int) this.m_zone]);
    }
  }
}
