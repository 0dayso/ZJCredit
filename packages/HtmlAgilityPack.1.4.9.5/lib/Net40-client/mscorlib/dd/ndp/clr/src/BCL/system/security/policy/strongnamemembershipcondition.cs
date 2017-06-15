// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.StrongNameMembershipCondition
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Util;

namespace System.Security.Policy
{
  /// <summary>通过测试程序集的强名称确定该程序集是否属于代码组。此类不能被继承。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class StrongNameMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable, IConstantMembershipCondition, IReportMatchMembershipCondition
  {
    private StrongNamePublicKeyBlob m_publicKeyBlob;
    private string m_name;
    private Version m_version;
    private SecurityElement m_element;
    private const string s_tagName = "Name";
    private const string s_tagVersion = "AssemblyVersion";
    private const string s_tagPublicKeyBlob = "PublicKeyBlob";

    /// <summary>获取或设置要针对其测试成员条件的 <see cref="T:System.Security.Policy.StrongName" /> 的 <see cref="T:System.Security.Permissions.StrongNamePublicKeyBlob" />。</summary>
    /// <returns>要针对其测试成员条件的 <see cref="T:System.Security.Policy.StrongName" /> 的 <see cref="T:System.Security.Permissions.StrongNamePublicKeyBlob" />。</returns>
    /// <exception cref="T:System.ArgumentNullException">尝试将 <see cref="P:System.Security.Policy.StrongNameMembershipCondition.PublicKey" /> 设置为 null。</exception>
    public StrongNamePublicKeyBlob PublicKey
    {
      get
      {
        if (this.m_publicKeyBlob == null && this.m_element != null)
          this.ParseKeyBlob();
        return this.m_publicKeyBlob;
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("PublicKey");
        this.m_publicKeyBlob = value;
      }
    }

    /// <summary>获取或设置要针对其测试成员条件的 <see cref="T:System.Security.Policy.StrongName" /> 的简单名称。</summary>
    /// <returns>要针对其测试成员条件的 <see cref="T:System.Security.Policy.StrongName" /> 的简单名称。</returns>
    /// <exception cref="T:System.ArgumentException">该值为 null。- 或 -该值是空字符串 ("")。</exception>
    public string Name
    {
      get
      {
        if (this.m_name == null && this.m_element != null)
          this.ParseName();
        return this.m_name;
      }
      set
      {
        if (value == null)
        {
          if (this.m_publicKeyBlob == null && this.m_element != null)
            this.ParseKeyBlob();
          if (this.m_version == null && this.m_element != null)
            this.ParseVersion();
          this.m_element = (SecurityElement) null;
        }
        else if (value.Length == 0)
          throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"));
        this.m_name = value;
      }
    }

    /// <summary>获取或设置要针对其测试成员条件的 <see cref="T:System.Security.Policy.StrongName" /> 的 <see cref="T:System.Version" />。</summary>
    /// <returns>要针对其测试成员条件的 <see cref="T:System.Security.Policy.StrongName" /> 的 <see cref="T:System.Version" />。</returns>
    public Version Version
    {
      get
      {
        if (this.m_version == null && this.m_element != null)
          this.ParseVersion();
        return this.m_version;
      }
      set
      {
        if (value == (Version) null)
        {
          if (this.m_name == null && this.m_element != null)
            this.ParseName();
          if (this.m_publicKeyBlob == null && this.m_element != null)
            this.ParseKeyBlob();
          this.m_element = (SecurityElement) null;
        }
        this.m_version = value;
      }
    }

    internal StrongNameMembershipCondition()
    {
    }

    /// <summary>用确定成员身份的强名称公钥 Blob、名称和版本号初始化 <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> 类的新实例。</summary>
    /// <param name="blob">软件发行者的强名称公钥 Blob。</param>
    /// <param name="name">强名称中的简单名称部分。</param>
    /// <param name="version">强名称的版本号。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="blob" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 参数为 null。- 或 -<paramref name="name" /> 参数是空字符串 ("")。</exception>
    public StrongNameMembershipCondition(StrongNamePublicKeyBlob blob, string name, Version version)
    {
      if (blob == null)
        throw new ArgumentNullException("blob");
      if (name != null && name.Equals(""))
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyStrongName"));
      this.m_publicKeyBlob = blob;
      this.m_name = name;
      this.m_version = version;
    }

    /// <summary>确定指定的证据是否能满足成员条件。</summary>
    /// <returns>如果指定的证据满足成员条件，则为 true；否则为 false。</returns>
    /// <param name="evidence">进行测试所依据的 <see cref="T:System.Security.Policy.Evidence" />。</param>
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
      StrongName evaluatedHostEvidence = evidence.GetDelayEvaluatedHostEvidence<StrongName>();
      if (evaluatedHostEvidence != null)
      {
        int num1 = this.PublicKey == null ? 0 : (this.PublicKey.Equals(evaluatedHostEvidence.PublicKey) ? 1 : 0);
        bool flag1 = this.Name == null || evaluatedHostEvidence.Name != null && StrongName.CompareNames(evaluatedHostEvidence.Name, this.Name);
        bool flag2 = this.Version == null || evaluatedHostEvidence.Version != null && evaluatedHostEvidence.Version.CompareTo(this.Version) == 0;
        int num2 = flag1 ? 1 : 0;
        if ((num1 & num2 & (flag2 ? 1 : 0)) != 0)
        {
          usedEvidence = (object) evaluatedHostEvidence;
          return true;
        }
      }
      return false;
    }

    /// <summary>创建当前 <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> 的等效副本。</summary>
    /// <returns>当前 <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> 的相同的新副本。</returns>
    public IMembershipCondition Copy()
    {
      return (IMembershipCondition) new StrongNameMembershipCondition(this.PublicKey, this.Name, this.Version);
    }

    /// <summary>创建安全对象及其当前状态的 XML 编码。</summary>
    /// <returns>安全对象的 XML 编码，包括任何状态信息。</returns>
    public SecurityElement ToXml()
    {
      return this.ToXml((PolicyLevel) null);
    }

    /// <summary>用 XML 编码重新构造具有指定状态的安全对象。</summary>
    /// <param name="e">用于重新构造安全对象的 XML 编码。</param>
    public void FromXml(SecurityElement e)
    {
      this.FromXml(e, (PolicyLevel) null);
    }

    /// <summary>使用指定的 <see cref="T:System.Security.Policy.PolicyLevel" /> 创建安全对象及其当前状态的 XML 编码。</summary>
    /// <returns>安全对象的 XML 编码，包括任何状态信息。</returns>
    /// <param name="level">
    /// <see cref="T:System.Security.Policy.PolicyLevel" /> 上下文，它用于解析 <see cref="T:System.Security.NamedPermissionSet" /> 引用。</param>
    public SecurityElement ToXml(PolicyLevel level)
    {
      SecurityElement element = new SecurityElement("IMembershipCondition");
      XMLUtil.AddClassAttribute(element, this.GetType(), "System.Security.Policy.StrongNameMembershipCondition");
      element.AddAttribute("version", "1");
      if (this.PublicKey != null)
        element.AddAttribute("PublicKeyBlob", Hex.EncodeHexString(this.PublicKey.PublicKey));
      if (this.Name != null)
        element.AddAttribute("Name", this.Name);
      if (this.Version != null)
        element.AddAttribute("AssemblyVersion", this.Version.ToString());
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
        this.m_name = (string) null;
        this.m_publicKeyBlob = (StrongNamePublicKeyBlob) null;
        this.m_version = (Version) null;
        this.m_element = e;
      }
    }

    private void ParseName()
    {
      lock (this)
      {
        if (this.m_element == null)
          return;
        string local_2 = this.m_element.Attribute("Name");
        this.m_name = local_2 == null ? (string) null : local_2;
        if (this.m_version == null || this.m_name == null || this.m_publicKeyBlob == null)
          return;
        this.m_element = (SecurityElement) null;
      }
    }

    private void ParseKeyBlob()
    {
      lock (this)
      {
        if (this.m_element == null)
          return;
        string local_2 = this.m_element.Attribute("PublicKeyBlob");
        StrongNamePublicKeyBlob local_3 = new StrongNamePublicKeyBlob();
        if (local_2 == null)
          throw new ArgumentException(Environment.GetResourceString("Argument_BlobCannotBeNull"));
        local_3.PublicKey = Hex.DecodeHexString(local_2);
        this.m_publicKeyBlob = local_3;
        if (this.m_version == null || this.m_name == null || this.m_publicKeyBlob == null)
          return;
        this.m_element = (SecurityElement) null;
      }
    }

    private void ParseVersion()
    {
      lock (this)
      {
        if (this.m_element == null)
          return;
        string local_2 = this.m_element.Attribute("AssemblyVersion");
        this.m_version = local_2 == null ? (Version) null : new Version(local_2);
        if (this.m_version == null || this.m_name == null || this.m_publicKeyBlob == null)
          return;
        this.m_element = (SecurityElement) null;
      }
    }

    /// <summary>创建并返回当前 <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> 的字符串表示形式。</summary>
    /// <returns>当前 <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> 的表示形式。</returns>
    public override string ToString()
    {
      string str1 = "";
      string str2 = "";
      if (this.Name != null)
        str1 = " " + string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("StrongName_Name"), (object) this.Name);
      if (this.Version != null)
        str2 = " " + string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("StrongName_Version"), (object) this.Version);
      return string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("StrongName_ToString"), (object) Hex.EncodeHexString(this.PublicKey.PublicKey), (object) str1, (object) str2);
    }

    /// <summary>确定指定对象中的 <see cref="T:System.Security.Policy.StrongName" /> 是否等效于包含在当前 <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> 中的 <see cref="T:System.Security.Policy.StrongName" />。</summary>
    /// <returns>如果指定对象中的 <see cref="T:System.Security.Policy.StrongName" /> 等效于包含在当前 <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> 中的 <see cref="T:System.Security.Policy.StrongName" />，则为 true；否则为 false。</returns>
    /// <param name="o">与当前的 <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> 比较的对象。</param>
    /// <exception cref="T:System.ArgumentException">当前对象或指定对象的 <see cref="P:System.Security.Policy.StrongNameMembershipCondition.PublicKey" /> 属性是 null。</exception>
    public override bool Equals(object o)
    {
      StrongNameMembershipCondition membershipCondition = o as StrongNameMembershipCondition;
      if (membershipCondition != null)
      {
        if (this.m_publicKeyBlob == null && this.m_element != null)
          this.ParseKeyBlob();
        if (membershipCondition.m_publicKeyBlob == null && membershipCondition.m_element != null)
          membershipCondition.ParseKeyBlob();
        if (object.Equals((object) this.m_publicKeyBlob, (object) membershipCondition.m_publicKeyBlob))
        {
          if (this.m_name == null && this.m_element != null)
            this.ParseName();
          if (membershipCondition.m_name == null && membershipCondition.m_element != null)
            membershipCondition.ParseName();
          if (object.Equals((object) this.m_name, (object) membershipCondition.m_name))
          {
            if (this.m_version == (Version) null && this.m_element != null)
              this.ParseVersion();
            if (membershipCondition.m_version == (Version) null && membershipCondition.m_element != null)
              membershipCondition.ParseVersion();
            if (object.Equals((object) this.m_version, (object) membershipCondition.m_version))
              return true;
          }
        }
      }
      return false;
    }

    /// <summary>返回当前 <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> 的哈希代码。</summary>
    /// <returns>当前 <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> 的哈希代码。</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <see cref="P:System.Security.Policy.StrongNameMembershipCondition.PublicKey" /> 属性为 null。</exception>
    public override int GetHashCode()
    {
      if (this.m_publicKeyBlob == null && this.m_element != null)
        this.ParseKeyBlob();
      if (this.m_publicKeyBlob != null)
        return this.m_publicKeyBlob.GetHashCode();
      if (this.m_name == null && this.m_element != null)
        this.ParseName();
      if (this.m_version == (Version) null && this.m_element != null)
        this.ParseVersion();
      if (this.m_name != null || this.m_version != (Version) null)
        return (this.m_name == null ? 0 : this.m_name.GetHashCode()) + (this.m_version == (Version) null ? 0 : this.m_version.GetHashCode());
      return typeof (StrongNameMembershipCondition).GetHashCode();
    }
  }
}
