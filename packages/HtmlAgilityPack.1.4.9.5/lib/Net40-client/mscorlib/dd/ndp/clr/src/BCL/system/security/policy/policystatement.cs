// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.PolicyStatement
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Util;
using System.Text;

namespace System.Security.Policy
{
  /// <summary>表示描述权限和其他适用于具有特定证据集的代码的信息的 <see cref="T:System.Security.Policy.CodeGroup" /> 的语句。此类不能被继承。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class PolicyStatement : ISecurityPolicyEncodable, ISecurityEncodable
  {
    internal PermissionSet m_permSet;
    [NonSerialized]
    private List<IDelayEvaluatedEvidence> m_dependentEvidence;
    internal PolicyStatementAttribute m_attributes;

    /// <summary>获取或设置策略语句的 <see cref="T:System.Security.PermissionSet" />。</summary>
    /// <returns>策略语句的 <see cref="T:System.Security.PermissionSet" />。</returns>
    public PermissionSet PermissionSet
    {
      get
      {
        lock (this)
          return this.m_permSet.Copy();
      }
      set
      {
        lock (this)
        {
          if (value == null)
            this.m_permSet = new PermissionSet(false);
          else
            this.m_permSet = value.Copy();
        }
      }
    }

    /// <summary>获取或设置策略语句的特性。</summary>
    /// <returns>策略语句的特性。</returns>
    public PolicyStatementAttribute Attributes
    {
      get
      {
        return this.m_attributes;
      }
      set
      {
        if (!PolicyStatement.ValidProperties(value))
          return;
        this.m_attributes = value;
      }
    }

    /// <summary>获取策略语句的特性的字符串表示形式。</summary>
    /// <returns>表示策略语句的特性的文本字符串。</returns>
    public string AttributeString
    {
      get
      {
        StringBuilder stringBuilder = new StringBuilder();
        bool flag = true;
        if (this.GetFlag(1))
        {
          stringBuilder.Append("Exclusive");
          flag = false;
        }
        if (this.GetFlag(2))
        {
          if (!flag)
            stringBuilder.Append(" ");
          stringBuilder.Append("LevelFinal");
        }
        return stringBuilder.ToString();
      }
    }

    internal IEnumerable<IDelayEvaluatedEvidence> DependentEvidence
    {
      get
      {
        return (IEnumerable<IDelayEvaluatedEvidence>) this.m_dependentEvidence.AsReadOnly();
      }
    }

    internal bool HasDependentEvidence
    {
      get
      {
        if (this.m_dependentEvidence != null)
          return this.m_dependentEvidence.Count > 0;
        return false;
      }
    }

    internal PolicyStatement()
    {
      this.m_permSet = (PermissionSet) null;
      this.m_attributes = PolicyStatementAttribute.Nothing;
    }

    /// <summary>用指定的 <see cref="T:System.Security.PermissionSet" /> 初始化 <see cref="T:System.Security.Policy.PolicyStatement" /> 类的新实例。</summary>
    /// <param name="permSet">用于初始化新实例的 <see cref="T:System.Security.PermissionSet" />。</param>
    public PolicyStatement(PermissionSet permSet)
      : this(permSet, PolicyStatementAttribute.Nothing)
    {
    }

    /// <summary>使用指定的 <see cref="T:System.Security.PermissionSet" /> 和特性初始化 <see cref="T:System.Security.Policy.PolicyStatement" /> 类的新实例。</summary>
    /// <param name="permSet">用于初始化新实例的 <see cref="T:System.Security.PermissionSet" />。</param>
    /// <param name="attributes">
    /// <see cref="T:System.Security.Policy.PolicyStatementAttribute" /> 值的按位组合。</param>
    public PolicyStatement(PermissionSet permSet, PolicyStatementAttribute attributes)
    {
      this.m_permSet = permSet != null ? permSet.Copy() : new PermissionSet(false);
      if (!PolicyStatement.ValidProperties(attributes))
        return;
      this.m_attributes = attributes;
    }

    private PolicyStatement(PermissionSet permSet, PolicyStatementAttribute attributes, bool copy)
    {
      this.m_permSet = permSet == null ? new PermissionSet(false) : (!copy ? permSet : permSet.Copy());
      this.m_attributes = attributes;
    }

    internal void SetPermissionSetNoCopy(PermissionSet permSet)
    {
      this.m_permSet = permSet;
    }

    internal PermissionSet GetPermissionSetNoCopy()
    {
      lock (this)
        return this.m_permSet;
    }

    /// <summary>创建当前策略语句的等效副本。</summary>
    /// <returns>
    /// <see cref="T:System.Security.Policy.PolicyStatement" /> 的新副本，其 <see cref="P:System.Security.Policy.PolicyStatement.PermissionSet" /> 和 <see cref="P:System.Security.Policy.PolicyStatement.Attributes" /> 与当前 <see cref="T:System.Security.Policy.PolicyStatement" /> 的相同。</returns>
    public PolicyStatement Copy()
    {
      PolicyStatement policyStatement = new PolicyStatement(this.m_permSet, this.Attributes, true);
      if (this.HasDependentEvidence)
        policyStatement.m_dependentEvidence = new List<IDelayEvaluatedEvidence>((IEnumerable<IDelayEvaluatedEvidence>) this.m_dependentEvidence);
      return policyStatement;
    }

    private static bool ValidProperties(PolicyStatementAttribute attributes)
    {
      if ((attributes & ~PolicyStatementAttribute.All) == PolicyStatementAttribute.Nothing)
        return true;
      throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"));
    }

    private bool GetFlag(int flag)
    {
      return (uint) ((PolicyStatementAttribute) flag & this.m_attributes) > 0U;
    }

    internal void AddDependentEvidence(IDelayEvaluatedEvidence dependentEvidence)
    {
      if (this.m_dependentEvidence == null)
        this.m_dependentEvidence = new List<IDelayEvaluatedEvidence>();
      this.m_dependentEvidence.Add(dependentEvidence);
    }

    internal void InplaceUnion(PolicyStatement childPolicy)
    {
      if ((this.Attributes & childPolicy.Attributes & PolicyStatementAttribute.Exclusive) == PolicyStatementAttribute.Exclusive)
        throw new PolicyException(Environment.GetResourceString("Policy_MultipleExclusive"));
      if (childPolicy.HasDependentEvidence && this.HasDependentEvidence | (this.m_permSet.IsSubsetOf(childPolicy.GetPermissionSetNoCopy()) && !childPolicy.GetPermissionSetNoCopy().IsSubsetOf(this.m_permSet)))
      {
        if (this.m_dependentEvidence == null)
          this.m_dependentEvidence = new List<IDelayEvaluatedEvidence>();
        this.m_dependentEvidence.AddRange(childPolicy.DependentEvidence);
      }
      if ((childPolicy.Attributes & PolicyStatementAttribute.Exclusive) == PolicyStatementAttribute.Exclusive)
      {
        this.m_permSet = childPolicy.GetPermissionSetNoCopy();
        this.Attributes = childPolicy.Attributes;
      }
      else
      {
        this.m_permSet.InplaceUnion(childPolicy.GetPermissionSetNoCopy());
        this.Attributes = this.Attributes | childPolicy.Attributes;
      }
    }

    /// <summary>创建安全对象及其当前状态的 XML 编码。</summary>
    /// <returns>安全对象的 XML 编码，包括任何状态信息。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public SecurityElement ToXml()
    {
      return this.ToXml((PolicyLevel) null);
    }

    /// <summary>从 XML 编码重新构造具有给定状态的安全对象。</summary>
    /// <param name="et">用于重新构造安全对象的 XML 编码。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="et" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="et" /> 参数不是有效的 <see cref="T:System.Security.Policy.PolicyStatement" /> 编码。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
    /// </PermissionSet>
    public void FromXml(SecurityElement et)
    {
      this.FromXml(et, (PolicyLevel) null);
    }

    /// <summary>创建安全对象及其当前状态的 XML 编码。</summary>
    /// <returns>安全对象的 XML 编码，包括任何状态信息。</returns>
    /// <param name="level">用于查找 <see cref="T:System.Security.NamedPermissionSet" /> 值的 <see cref="T:System.Security.Policy.PolicyLevel" /> 上下文。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public SecurityElement ToXml(PolicyLevel level)
    {
      return this.ToXml(level, false);
    }

    internal SecurityElement ToXml(PolicyLevel level, bool useInternal)
    {
      SecurityElement securityElement = new SecurityElement("PolicyStatement");
      securityElement.AddAttribute("version", "1");
      if (this.m_attributes != PolicyStatementAttribute.Nothing)
        securityElement.AddAttribute("Attributes", XMLUtil.BitFieldEnumToString(typeof (PolicyStatementAttribute), (object) this.m_attributes));
      lock (this)
      {
        if (this.m_permSet != null)
        {
          if (this.m_permSet is NamedPermissionSet)
          {
            NamedPermissionSet local_3 = (NamedPermissionSet) this.m_permSet;
            if (level != null && level.GetNamedPermissionSet(local_3.Name) != null)
              securityElement.AddAttribute("PermissionSetName", local_3.Name);
            else if (useInternal)
              securityElement.AddChild(local_3.InternalToXml());
            else
              securityElement.AddChild(local_3.ToXml());
          }
          else if (useInternal)
            securityElement.AddChild(this.m_permSet.InternalToXml());
          else
            securityElement.AddChild(this.m_permSet.ToXml());
        }
      }
      return securityElement;
    }

    /// <summary>从 XML 编码重新构造具有给定状态的安全对象。</summary>
    /// <param name="et">用于重新构造安全对象的 XML 编码。</param>
    /// <param name="level">用于查找 <see cref="T:System.Security.NamedPermissionSet" /> 值的 <see cref="T:System.Security.Policy.PolicyLevel" /> 上下文。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="et" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="et" /> 参数不是有效的 <see cref="T:System.Security.Policy.PolicyStatement" /> 编码。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public void FromXml(SecurityElement et, PolicyLevel level)
    {
      this.FromXml(et, level, false);
    }

    [SecurityCritical]
    internal void FromXml(SecurityElement et, PolicyLevel level, bool allowInternalOnly)
    {
      if (et == null)
        throw new ArgumentNullException("et");
      if (!et.Tag.Equals("PolicyStatement"))
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidXMLElement"), (object) "PolicyStatement", (object) this.GetType().FullName));
      this.m_attributes = PolicyStatementAttribute.Nothing;
      string str = et.Attribute("Attributes");
      if (str != null)
        this.m_attributes = (PolicyStatementAttribute) Enum.Parse(typeof (PolicyStatementAttribute), str);
      lock (this)
      {
        this.m_permSet = (PermissionSet) null;
        if (level != null)
        {
          string local_3 = et.Attribute("PermissionSetName");
          if (local_3 != null)
          {
            this.m_permSet = (PermissionSet) level.GetNamedPermissionSetInternal(local_3);
            if (this.m_permSet == null)
              this.m_permSet = new PermissionSet(PermissionState.None);
          }
        }
        if (this.m_permSet == null)
        {
          SecurityElement local_4 = et.SearchForChildByTag("PermissionSet");
          if (local_4 == null)
            throw new ArgumentException(Environment.GetResourceString("Argument_InvalidXML"));
          string local_5 = local_4.Attribute("class");
          this.m_permSet = local_5 == null || !local_5.Equals("NamedPermissionSet") && !local_5.Equals("System.Security.NamedPermissionSet") ? new PermissionSet(PermissionState.None) : (PermissionSet) new NamedPermissionSet("DefaultName", PermissionState.None);
          try
          {
            this.m_permSet.FromXml(local_4, allowInternalOnly, true);
          }
          catch
          {
          }
        }
        if (this.m_permSet != null)
          return;
        this.m_permSet = new PermissionSet(PermissionState.None);
      }
    }

    [SecurityCritical]
    internal void FromXml(SecurityDocument doc, int position, PolicyLevel level, bool allowInternalOnly)
    {
      if (doc == null)
        throw new ArgumentNullException("doc");
      if (!doc.GetTagForElement(position).Equals("PolicyStatement"))
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidXMLElement"), (object) "PolicyStatement", (object) this.GetType().FullName));
      this.m_attributes = PolicyStatementAttribute.Nothing;
      string attributeForElement = doc.GetAttributeForElement(position, "Attributes");
      if (attributeForElement != null)
        this.m_attributes = (PolicyStatementAttribute) Enum.Parse(typeof (PolicyStatementAttribute), attributeForElement);
      lock (this)
      {
        this.m_permSet = (PermissionSet) null;
        if (level != null)
        {
          string local_3 = doc.GetAttributeForElement(position, "PermissionSetName");
          if (local_3 != null)
          {
            this.m_permSet = (PermissionSet) level.GetNamedPermissionSetInternal(local_3);
            if (this.m_permSet == null)
              this.m_permSet = new PermissionSet(PermissionState.None);
          }
        }
        if (this.m_permSet == null)
        {
          ArrayList local_4 = doc.GetChildrenPositionForElement(position);
          int local_5 = -1;
          for (int local_6 = 0; local_6 < local_4.Count; ++local_6)
          {
            if (doc.GetTagForElement((int) local_4[local_6]).Equals("PermissionSet"))
              local_5 = (int) local_4[local_6];
          }
          if (local_5 == -1)
            throw new ArgumentException(Environment.GetResourceString("Argument_InvalidXML"));
          string local_7 = doc.GetAttributeForElement(local_5, "class");
          this.m_permSet = local_7 == null || !local_7.Equals("NamedPermissionSet") && !local_7.Equals("System.Security.NamedPermissionSet") ? new PermissionSet(PermissionState.None) : (PermissionSet) new NamedPermissionSet("DefaultName", PermissionState.None);
          this.m_permSet.FromXml(doc, local_5, allowInternalOnly);
        }
        if (this.m_permSet != null)
          return;
        this.m_permSet = new PermissionSet(PermissionState.None);
      }
    }

    /// <summary>确定指定的 <see cref="T:System.Security.Policy.PolicyStatement" /> 对象是否等于当前的 <see cref="T:System.Security.Policy.PolicyStatement" />。</summary>
    /// <returns>如果指定的 <see cref="T:System.Security.Policy.PolicyStatement" /> 等于当前的 <see cref="T:System.Security.Policy.PolicyStatement" /> 对象，则为 true；否则为 false。</returns>
    /// <param name="obj">要与当前的 <see cref="T:System.Security.Policy.PolicyStatement" /> 进行比较的 <see cref="T:System.Security.Policy.PolicyStatement" /> 对象。</param>
    [ComVisible(false)]
    public override bool Equals(object obj)
    {
      PolicyStatement policyStatement = obj as PolicyStatement;
      return policyStatement != null && this.m_attributes == policyStatement.m_attributes && object.Equals((object) this.m_permSet, (object) policyStatement.m_permSet);
    }

    /// <summary>获取适合在哈希算法和类似哈希表的数据结构中使用的 <see cref="T:System.Security.Policy.PolicyStatement" /> 对象的哈希代码。</summary>
    /// <returns>当前 <see cref="T:System.Security.Policy.PolicyStatement" /> 对象的哈希代码。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [ComVisible(false)]
    public override int GetHashCode()
    {
      int num = (int) this.m_attributes;
      if (this.m_permSet != null)
        num ^= this.m_permSet.GetHashCode();
      return num;
    }
  }
}
