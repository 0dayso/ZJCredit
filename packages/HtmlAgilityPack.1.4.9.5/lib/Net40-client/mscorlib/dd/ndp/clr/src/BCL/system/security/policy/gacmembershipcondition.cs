// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.GacMembershipCondition
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Policy
{
  /// <summary>通过测试程序集的全局程序集缓存成员资格，确定该程序集是否属于代码组。此类不能被继承。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class GacMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable, IConstantMembershipCondition, IReportMatchMembershipCondition
  {
    /// <summary>指示指定的证据是否满足成员条件。</summary>
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
      return evidence.GetHostEvidence<GacInstalled>() != null;
    }

    /// <summary>创建成员条件的等效副本。</summary>
    /// <returns>一个新的 <see cref="T:System.Security.Policy.GacMembershipCondition" /> 对象。</returns>
    public IMembershipCondition Copy()
    {
      return (IMembershipCondition) new GacMembershipCondition();
    }

    /// <summary>创建安全对象及其当前状态的 XML 编码。</summary>
    /// <returns>包含安全对象的 XML 编码（包括所有状态信息）的 <see cref="T:System.Security.SecurityElement" />。</returns>
    public SecurityElement ToXml()
    {
      return this.ToXml((PolicyLevel) null);
    }

    /// <summary>使用指定的 XML 编码重新构造安全对象。</summary>
    /// <param name="e">包含用于重新构造安全对象的 XML 编码的 <see cref="T:System.Security.SecurityElement" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="e" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="e" /> 不是有效的成员条件元素。</exception>
    public void FromXml(SecurityElement e)
    {
      this.FromXml(e, (PolicyLevel) null);
    }

    /// <summary>使用指定的策略级别上下文创建安全对象的 XML 编码及其当前状态。</summary>
    /// <returns>包含安全对象的 XML 编码（包括所有状态信息）的 <see cref="T:System.Security.SecurityElement" />。</returns>
    /// <param name="level">用于解析 <see cref="T:System.Security.NamedPermissionSet" /> 引用的 <see cref="T:System.Security.Policy.PolicyLevel" /> 上下文。</param>
    public SecurityElement ToXml(PolicyLevel level)
    {
      SecurityElement element = new SecurityElement("IMembershipCondition");
      Type type = this.GetType();
      string fullName = this.GetType().FullName;
      XMLUtil.AddClassAttribute(element, type, fullName);
      string name = "version";
      string str = "1";
      element.AddAttribute(name, str);
      return element;
    }

    /// <summary>使用指定的 XML 编码，利用指定的策略级别上下文重新构造安全对象。</summary>
    /// <param name="e">包含用于重新构造安全对象的 XML 编码的 <see cref="T:System.Security.SecurityElement" />。</param>
    /// <param name="level">用于解析 <see cref="T:System.Security.NamedPermissionSet" /> 引用的 <see cref="T:System.Security.Policy.PolicyLevel" /> 上下文。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="e" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="e" /> 不是有效的成员条件元素。</exception>
    public void FromXml(SecurityElement e, PolicyLevel level)
    {
      if (e == null)
        throw new ArgumentNullException("e");
      if (!e.Tag.Equals("IMembershipCondition"))
        throw new ArgumentException(Environment.GetResourceString("Argument_MembershipConditionElement"));
    }

    /// <summary>指示当前对象是否等效于指定的对象。</summary>
    /// <returns>如果 <paramref name="o" /> 为 <see cref="T:System.Security.Policy.GacMembershipCondition" />，则为 true；否则为 false。</returns>
    /// <param name="o">要与当前对象进行比较的对象。</param>
    public override bool Equals(object o)
    {
      return o is GacMembershipCondition;
    }

    /// <summary>获取当前成员条件的哈希代码。</summary>
    /// <returns>0（零）。</returns>
    public override int GetHashCode()
    {
      return 0;
    }

    /// <summary>返回成员条件的字符串表示形式。</summary>
    /// <returns>成员条件的字符串表示形式。</returns>
    public override string ToString()
    {
      return Environment.GetResourceString("GAC_ToString");
    }
  }
}
