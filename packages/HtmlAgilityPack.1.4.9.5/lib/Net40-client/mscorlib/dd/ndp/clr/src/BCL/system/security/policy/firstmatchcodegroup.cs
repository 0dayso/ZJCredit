// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.FirstMatchCodeGroup
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
  /// <summary>允许由代码组的策略声明和第一个匹配的子代码组的策略声明的联合来定义安全策略。此类不能被继承。</summary>
  [ComVisible(true)]
  [Obsolete("This type is obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
  [Serializable]
  public sealed class FirstMatchCodeGroup : CodeGroup
  {
    /// <summary>获取合并逻辑。</summary>
    /// <returns>字符串“First Match”。</returns>
    public override string MergeLogic
    {
      get
      {
        return Environment.GetResourceString("MergeLogic_FirstMatch");
      }
    }

    internal FirstMatchCodeGroup()
    {
    }

    /// <summary>初始化 <see cref="T:System.Security.Policy.FirstMatchCodeGroup" /> 类的新实例。</summary>
    /// <param name="membershipCondition">成员条件，它测试证据以确定此代码组是否应用策略。</param>
    /// <param name="policy">形式为权限集和特性的代码组的策略声明，这些权限集和特性将被授予匹配成员条件的代码。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="membershipCondition" /> 参数的类型无效。- 或 -<paramref name="policy" /> 参数的类型无效。</exception>
    public FirstMatchCodeGroup(IMembershipCondition membershipCondition, PolicyStatement policy)
      : base(membershipCondition, policy)
    {
    }

    /// <summary>对一组证据解析代码组及其子代的策略。</summary>
    /// <returns>由具有可选特性的代码组授予的权限组成的策略声明；或者，如果代码组不适用（成员条件与指定的证据不匹配），则为 null。</returns>
    /// <param name="evidence">程序集的证据。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="evidence" /> 参数为 null。</exception>
    /// <exception cref="T:System.Security.Policy.PolicyException">将不止一个代码组（包括父代码组和任何子代码组）标记为 <see cref="F:System.Security.Policy.PolicyStatementAttribute.Exclusive" />。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public override PolicyStatement Resolve(Evidence evidence)
    {
      if (evidence == null)
        throw new ArgumentNullException("evidence");
      object usedEvidence = (object) null;
      if (!PolicyManager.CheckMembershipCondition(this.MembershipCondition, evidence, out usedEvidence))
        return (PolicyStatement) null;
      PolicyStatement childPolicy = (PolicyStatement) null;
      foreach (object child in (IEnumerable) this.Children)
      {
        childPolicy = PolicyManager.ResolveCodeGroup(child as CodeGroup, evidence);
        if (childPolicy != null)
          break;
      }
      IDelayEvaluatedEvidence dependentEvidence = usedEvidence as IDelayEvaluatedEvidence;
      bool flag = dependentEvidence != null && !dependentEvidence.IsVerified;
      PolicyStatement policyStatement1 = this.PolicyStatement;
      if (policyStatement1 == null)
      {
        if (flag)
        {
          childPolicy = childPolicy.Copy();
          childPolicy.AddDependentEvidence(dependentEvidence);
        }
        return childPolicy;
      }
      if (childPolicy != null)
      {
        PolicyStatement policyStatement2 = policyStatement1.Copy();
        if (flag)
          policyStatement2.AddDependentEvidence(dependentEvidence);
        policyStatement2.InplaceUnion(childPolicy);
        return policyStatement2;
      }
      if (flag)
        policyStatement1.AddDependentEvidence(dependentEvidence);
      return policyStatement1;
    }

    /// <summary>解析匹配的代码组。</summary>
    /// <returns>一个 <see cref="T:System.Security.Policy.CodeGroup" />，它是匹配代码组的树的根。</returns>
    /// <param name="evidence">程序集的证据。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="evidence" /> 参数为 null。</exception>
    public override CodeGroup ResolveMatchingCodeGroups(Evidence evidence)
    {
      if (evidence == null)
        throw new ArgumentNullException("evidence");
      if (!this.MembershipCondition.Check(evidence))
        return (CodeGroup) null;
      CodeGroup codeGroup = this.Copy();
      codeGroup.Children = (IList) new ArrayList();
      foreach (CodeGroup child in (IEnumerable) this.Children)
      {
        CodeGroup group = child.ResolveMatchingCodeGroups(evidence);
        if (group != null)
        {
          codeGroup.AddChild(group);
          break;
        }
      }
      return codeGroup;
    }

    /// <summary>生成代码组的深层副本。</summary>
    /// <returns>代码组（包括其成员条件和子代码组）的等效副本。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public override CodeGroup Copy()
    {
      FirstMatchCodeGroup firstMatchCodeGroup = new FirstMatchCodeGroup();
      firstMatchCodeGroup.MembershipCondition = this.MembershipCondition;
      firstMatchCodeGroup.PolicyStatement = this.PolicyStatement;
      firstMatchCodeGroup.Name = this.Name;
      firstMatchCodeGroup.Description = this.Description;
      foreach (CodeGroup child in (IEnumerable) this.Children)
        firstMatchCodeGroup.AddChild(child);
      return (CodeGroup) firstMatchCodeGroup;
    }

    internal override string GetTypeName()
    {
      return "System.Security.Policy.FirstMatchCodeGroup";
    }
  }
}
