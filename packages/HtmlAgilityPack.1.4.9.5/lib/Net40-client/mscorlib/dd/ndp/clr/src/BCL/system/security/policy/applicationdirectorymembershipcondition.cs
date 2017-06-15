// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.ApplicationDirectoryMembershipCondition
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Policy
{
  /// <summary>通过测试程序集的应用程序目录确定该程序集是否属于代码组。此类不能被继承。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class ApplicationDirectoryMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable, IConstantMembershipCondition, IReportMatchMembershipCondition
  {
    /// <summary>确定指定的证据是否满足成员条件。</summary>
    /// <returns>如果指定的证据满足成员条件，则为 true；否则为 false。</returns>
    /// <param name="evidence">证据集，将根据它进行测试。</param>
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
      ApplicationDirectory hostEvidence1 = evidence.GetHostEvidence<ApplicationDirectory>();
      Url hostEvidence2 = evidence.GetHostEvidence<Url>();
      if (hostEvidence1 != null && hostEvidence2 != null)
      {
        string directory = hostEvidence1.Directory;
        if (directory != null && directory.Length > 1)
        {
          string str = directory;
          int index = str.Length - 1;
          URLString urlString = new URLString((int) str[index] != 47 ? directory + "/*" : directory + "*");
          if (hostEvidence2.GetURLString().IsSubsetOf((SiteString) urlString))
          {
            usedEvidence = (object) hostEvidence1;
            return true;
          }
        }
      }
      return false;
    }

    /// <summary>创建成员条件的等效副本。</summary>
    /// <returns>当前成员条件的完全相同的新副本。</returns>
    public IMembershipCondition Copy()
    {
      return (IMembershipCondition) new ApplicationDirectoryMembershipCondition();
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
    /// <paramref name="e" /> 参数不是有效的应用程序目录成员条件元素。</exception>
    public void FromXml(SecurityElement e)
    {
      this.FromXml(e, (PolicyLevel) null);
    }

    /// <summary>使用指定的 <see cref="T:System.Security.Policy.PolicyLevel" /> 创建安全对象及其当前状态的 XML 编码。</summary>
    /// <returns>安全对象的 XML 编码，包括任何状态信息。</returns>
    /// <param name="level">用于解析命名的权限集引用的策略级别上下文。</param>
    public SecurityElement ToXml(PolicyLevel level)
    {
      SecurityElement element = new SecurityElement("IMembershipCondition");
      Type type = this.GetType();
      string typename = "System.Security.Policy.ApplicationDirectoryMembershipCondition";
      XMLUtil.AddClassAttribute(element, type, typename);
      string name = "version";
      string str = "1";
      element.AddAttribute(name, str);
      return element;
    }

    /// <summary>用 XML 编码重新构造具有指定状态的安全对象。</summary>
    /// <param name="e">用于重新构造安全对象的 XML 编码。</param>
    /// <param name="level">策略级别上下文，用于解析命名的权限集引用。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="e" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="e" /> 参数不是有效的应用程序目录成员条件元素。</exception>
    public void FromXml(SecurityElement e, PolicyLevel level)
    {
      if (e == null)
        throw new ArgumentNullException("e");
      if (!e.Tag.Equals("IMembershipCondition"))
        throw new ArgumentException(Environment.GetResourceString("Argument_MembershipConditionElement"));
    }

    /// <summary>确定指定的成员条件是否为 <see cref="T:System.Security.Policy.ApplicationDirectoryMembershipCondition" />。</summary>
    /// <returns>如果指定的成员条件是 <see cref="T:System.Security.Policy.ApplicationDirectoryMembershipCondition" />，则为 true；否则为 false。</returns>
    /// <param name="o">要与 <see cref="T:System.Security.Policy.ApplicationDirectoryMembershipCondition" /> 比较的对象。</param>
    public override bool Equals(object o)
    {
      return o is ApplicationDirectoryMembershipCondition;
    }

    /// <summary>获取当前成员条件的哈希代码。</summary>
    /// <returns>当前成员条件的哈希代码。</returns>
    public override int GetHashCode()
    {
      return typeof (ApplicationDirectoryMembershipCondition).GetHashCode();
    }

    /// <summary>创建并返回成员条件的字符串表示形式。</summary>
    /// <returns>成员条件状态的字符串表示形式。</returns>
    public override string ToString()
    {
      return Environment.GetResourceString("ApplicationDirectory_ToString");
    }
  }
}
