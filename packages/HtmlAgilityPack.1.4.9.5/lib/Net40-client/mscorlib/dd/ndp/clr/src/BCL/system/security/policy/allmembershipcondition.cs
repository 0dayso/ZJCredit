// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.AllMembershipCondition
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Policy
{
  /// <summary>表示与所有代码匹配的成员条件。此类不能被继承。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class AllMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable, IConstantMembershipCondition, IReportMatchMembershipCondition
  {
    /// <summary>确定指定的证据是否能满足成员条件。</summary>
    /// <returns>始终为 true。</returns>
    /// <param name="evidence">证据集，将根据它进行测试。</param>
    public bool Check(Evidence evidence)
    {
      object usedEvidence = (object) null;
      return ((IReportMatchMembershipCondition) this).Check(evidence, out usedEvidence);
    }

    bool IReportMatchMembershipCondition.Check(Evidence evidence, out object usedEvidence)
    {
      usedEvidence = (object) null;
      return true;
    }

    /// <summary>创建成员条件的等效副本。</summary>
    /// <returns>当前成员条件的完全相同的新副本。</returns>
    public IMembershipCondition Copy()
    {
      return (IMembershipCondition) new AllMembershipCondition();
    }

    /// <summary>创建并返回成员条件的字符串表示形式。</summary>
    /// <returns>成员条件的表示形式。</returns>
    public override string ToString()
    {
      return Environment.GetResourceString("All_ToString");
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
    /// <param name="level">用于解析命名的权限集引用的策略级别上下文。</param>
    public SecurityElement ToXml(PolicyLevel level)
    {
      SecurityElement element = new SecurityElement("IMembershipCondition");
      Type type = this.GetType();
      string typename = "System.Security.Policy.AllMembershipCondition";
      XMLUtil.AddClassAttribute(element, type, typename);
      string name = "version";
      string str = "1";
      element.AddAttribute(name, str);
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
    }

    /// <summary>确定指定的成员条件是否为 <see cref="T:System.Security.Policy.AllMembershipCondition" />。</summary>
    /// <returns>如果指定的成员条件是 <see cref="T:System.Security.Policy.AllMembershipCondition" />，则为 true；否则为 false。</returns>
    /// <param name="o">要与 <see cref="T:System.Security.Policy.AllMembershipCondition" /> 比较的对象。</param>
    public override bool Equals(object o)
    {
      return o is AllMembershipCondition;
    }

    /// <summary>获取当前成员条件的哈希代码。</summary>
    /// <returns>当前成员条件的哈希代码。</returns>
    public override int GetHashCode()
    {
      return typeof (AllMembershipCondition).GetHashCode();
    }
  }
}
