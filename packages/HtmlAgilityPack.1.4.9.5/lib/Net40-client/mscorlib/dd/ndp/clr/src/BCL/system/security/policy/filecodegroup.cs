// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.FileCodeGroup
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Util;

namespace System.Security.Policy
{
  /// <summary>向符合成员条件的代码程序集授予权限以操作位于代码程序集中的文件。此类不能被继承。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class FileCodeGroup : CodeGroup, IUnionSemanticCodeGroup
  {
    private FileIOPermissionAccess m_access;

    /// <summary>获取合并逻辑。</summary>
    /// <returns>字符串“Union”。</returns>
    public override string MergeLogic
    {
      get
      {
        return Environment.GetResourceString("MergeLogic_Union");
      }
    }

    /// <summary>获取代码组的命名的权限集的名称。</summary>
    /// <returns>字符串“Same directory FileIO -”和访问类型的连接。</returns>
    public override string PermissionSetName
    {
      get
      {
        return Environment.GetResourceString("FileCodeGroup_PermissionSet", (object) XMLUtil.BitFieldEnumToString(typeof (FileIOPermissionAccess), (object) this.m_access));
      }
    }

    /// <summary>获取代码组策略声明的特性的字符串表示形式。</summary>
    /// <returns>始终为 null。</returns>
    public override string AttributeString
    {
      get
      {
        return (string) null;
      }
    }

    internal FileCodeGroup()
    {
    }

    /// <summary>初始化 <see cref="T:System.Security.Policy.FileCodeGroup" /> 类的新实例。</summary>
    /// <param name="membershipCondition">成员条件，它测试证据以确定此代码组是否应用策略。</param>
    /// <param name="access">
    /// <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> 值之一。该值用于构造授予的 <see cref="T:System.Security.Permissions.FileIOPermission" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="membershipCondition" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="membershipCondition" /> 参数的类型无效。- 或 -<paramref name="access" /> 参数的类型无效。</exception>
    public FileCodeGroup(IMembershipCondition membershipCondition, FileIOPermissionAccess access)
      : base(membershipCondition, (PolicyStatement) null)
    {
      this.m_access = access;
    }

    /// <summary>对一组证据解析代码组及其子代的策略。</summary>
    /// <returns>由具有可选特性的代码组授予的权限组成的策略声明；或者，如果代码组不适用（成员条件与指定的证据不匹配），则为 null。</returns>
    /// <param name="evidence">程序集的证据。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="evidence" /> 参数为 null。</exception>
    /// <exception cref="T:System.Security.Policy.PolicyException">当前策略是 null。- 或 -将不止一个代码组（包括父代码组和所有子代码组）标记为 <see cref="F:System.Security.Policy.PolicyStatementAttribute.Exclusive" />。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public override PolicyStatement Resolve(Evidence evidence)
    {
      if (evidence == null)
        throw new ArgumentNullException("evidence");
      object usedEvidence = (object) null;
      if (!PolicyManager.CheckMembershipCondition(this.MembershipCondition, evidence, out usedEvidence))
        return (PolicyStatement) null;
      PolicyStatement assemblyPolicy = this.CalculateAssemblyPolicy(evidence);
      IDelayEvaluatedEvidence dependentEvidence = usedEvidence as IDelayEvaluatedEvidence;
      if ((dependentEvidence == null ? 0 : (!dependentEvidence.IsVerified ? 1 : 0)) != 0)
        assemblyPolicy.AddDependentEvidence(dependentEvidence);
      bool flag = false;
      IEnumerator enumerator = this.Children.GetEnumerator();
      while (enumerator.MoveNext() && !flag)
      {
        PolicyStatement childPolicy = PolicyManager.ResolveCodeGroup(enumerator.Current as CodeGroup, evidence);
        if (childPolicy != null)
        {
          assemblyPolicy.InplaceUnion(childPolicy);
          if ((childPolicy.Attributes & PolicyStatementAttribute.Exclusive) == PolicyStatementAttribute.Exclusive)
            flag = true;
        }
      }
      return assemblyPolicy;
    }

    PolicyStatement IUnionSemanticCodeGroup.InternalResolve(Evidence evidence)
    {
      if (evidence == null)
        throw new ArgumentNullException("evidence");
      if (this.MembershipCondition.Check(evidence))
        return this.CalculateAssemblyPolicy(evidence);
      return (PolicyStatement) null;
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
          codeGroup.AddChild(group);
      }
      return codeGroup;
    }

    internal PolicyStatement CalculatePolicy(Url url)
    {
      URLString urlString = url.GetURLString();
      if (string.Compare(urlString.Scheme, "file", StringComparison.OrdinalIgnoreCase) != 0)
        return (PolicyStatement) null;
      string directoryName = urlString.GetDirectoryName();
      PermissionSet permSet = new PermissionSet(PermissionState.None);
      FileIOPermission fileIoPermission = new FileIOPermission(this.m_access, Path.GetFullPath(directoryName));
      permSet.SetPermission((IPermission) fileIoPermission);
      int num = 0;
      return new PolicyStatement(permSet, (PolicyStatementAttribute) num);
    }

    private PolicyStatement CalculateAssemblyPolicy(Evidence evidence)
    {
      PolicyStatement policyStatement = (PolicyStatement) null;
      Url hostEvidence = evidence.GetHostEvidence<Url>();
      if (hostEvidence != null)
        policyStatement = this.CalculatePolicy(hostEvidence);
      if (policyStatement == null)
        policyStatement = new PolicyStatement(new PermissionSet(false), PolicyStatementAttribute.Nothing);
      return policyStatement;
    }

    /// <summary>生成当前代码组的深层副本。</summary>
    /// <returns>当前代码组（包括其成员条件和子代码组）的等效副本。</returns>
    public override CodeGroup Copy()
    {
      FileCodeGroup fileCodeGroup = new FileCodeGroup(this.MembershipCondition, this.m_access);
      fileCodeGroup.Name = this.Name;
      fileCodeGroup.Description = this.Description;
      foreach (CodeGroup child in (IEnumerable) this.Children)
        fileCodeGroup.AddChild(child);
      return (CodeGroup) fileCodeGroup;
    }

    protected override void CreateXml(SecurityElement element, PolicyLevel level)
    {
      element.AddAttribute("Access", XMLUtil.BitFieldEnumToString(typeof (FileIOPermissionAccess), (object) this.m_access));
    }

    protected override void ParseXml(SecurityElement e, PolicyLevel level)
    {
      string str = e.Attribute("Access");
      if (str != null)
        this.m_access = (FileIOPermissionAccess) Enum.Parse(typeof (FileIOPermissionAccess), str);
      else
        this.m_access = FileIOPermissionAccess.NoAccess;
    }

    /// <summary>确定指定的代码组是否等效于当前代码组。</summary>
    /// <returns>如果指定的代码组等效于当前代码组，则为 true；否则为 false。</returns>
    /// <param name="o">要与当前代码组比较的代码组。</param>
    public override bool Equals(object o)
    {
      FileCodeGroup fileCodeGroup = o as FileCodeGroup;
      return fileCodeGroup != null && base.Equals((object) fileCodeGroup) && this.m_access == fileCodeGroup.m_access;
    }

    /// <summary>获取当前代码组的哈希代码。</summary>
    /// <returns>当前代码组的哈希代码。</returns>
    public override int GetHashCode()
    {
      return base.GetHashCode() + this.m_access.GetHashCode();
    }

    internal override string GetTypeName()
    {
      return "System.Security.Policy.FileCodeGroup";
    }
  }
}
