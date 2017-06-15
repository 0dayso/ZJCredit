// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.CodeGroup
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Policy
{
  /// <summary>表示抽象基类，必须从该基类中导出代码组的所有实现。</summary>
  [ComVisible(true)]
  [Serializable]
  public abstract class CodeGroup
  {
    private IMembershipCondition m_membershipCondition;
    private IList m_children;
    private PolicyStatement m_policy;
    private SecurityElement m_element;
    private PolicyLevel m_parentLevel;
    private string m_name;
    private string m_description;

    /// <summary>获取或设置代码组的子代码组的排序列表。</summary>
    /// <returns>子代码组的列表。</returns>
    /// <exception cref="T:System.ArgumentNullException">尝试将该属性设置为 null。</exception>
    /// <exception cref="T:System.ArgumentException">尝试使用非 <see cref="T:System.Security.Policy.CodeGroup" /> 对象的子级列表设置此属性。</exception>
    public IList Children
    {
      [SecuritySafeCritical] get
      {
        if (this.m_children == null)
          this.ParseChildren();
        lock (this)
        {
          IList local_2 = (IList) new ArrayList(this.m_children.Count);
          foreach (CodeGroup item_0 in (IEnumerable) this.m_children)
            local_2.Add((object) item_0.Copy());
          return local_2;
        }
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("Children");
        ArrayList arrayList = ArrayList.Synchronized(new ArrayList(value.Count));
        foreach (object obj in (IEnumerable) value)
        {
          CodeGroup codeGroup = obj as CodeGroup;
          if (codeGroup == null)
            throw new ArgumentException(Environment.GetResourceString("Argument_CodeGroupChildrenMustBeCodeGroups"));
          arrayList.Add((object) codeGroup.Copy());
        }
        this.m_children = (IList) arrayList;
      }
    }

    /// <summary>获取或设置代码组的成员条件。</summary>
    /// <returns>成员条件，它确定该代码组适用于哪个证据。</returns>
    /// <exception cref="T:System.ArgumentNullException">尝试将该参数设置为 null。</exception>
    public IMembershipCondition MembershipCondition
    {
      [SecuritySafeCritical] get
      {
        if (this.m_membershipCondition == null && this.m_element != null)
          this.ParseMembershipCondition();
        return this.m_membershipCondition.Copy();
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("MembershipCondition");
        this.m_membershipCondition = value.Copy();
      }
    }

    /// <summary>获取或设置与该代码组关联的策略声明。</summary>
    /// <returns>该代码组的策略声明。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public PolicyStatement PolicyStatement
    {
      get
      {
        if (this.m_policy == null && this.m_element != null)
          this.ParsePolicy();
        if (this.m_policy != null)
          return this.m_policy.Copy();
        return (PolicyStatement) null;
      }
      set
      {
        if (value != null)
          this.m_policy = value.Copy();
        else
          this.m_policy = (PolicyStatement) null;
      }
    }

    /// <summary>获取或设置代码组的名称。</summary>
    /// <returns>代码组的名称。</returns>
    public string Name
    {
      get
      {
        return this.m_name;
      }
      set
      {
        this.m_name = value;
      }
    }

    /// <summary>获取或设置代码组的说明。</summary>
    /// <returns>代码组的说明。</returns>
    public string Description
    {
      get
      {
        return this.m_description;
      }
      set
      {
        this.m_description = value;
      }
    }

    /// <summary>获取代码组的命名的权限集的名称。</summary>
    /// <returns>策略级别的命名的权限集的名称。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public virtual string PermissionSetName
    {
      get
      {
        if (this.m_policy == null && this.m_element != null)
          this.ParsePolicy();
        if (this.m_policy == null)
          return (string) null;
        NamedPermissionSet namedPermissionSet = this.m_policy.GetPermissionSetNoCopy() as NamedPermissionSet;
        if (namedPermissionSet != null)
          return namedPermissionSet.Name;
        return (string) null;
      }
    }

    /// <summary>获取代码组策略声明的特性的字符串表示形式。</summary>
    /// <returns>代码组策略声明的特性的字符串表示形式。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public virtual string AttributeString
    {
      get
      {
        if (this.m_policy == null && this.m_element != null)
          this.ParsePolicy();
        if (this.m_policy != null)
          return this.m_policy.AttributeString;
        return (string) null;
      }
    }

    /// <summary>当在派生类中被重写时，获取该代码组的合并逻辑。</summary>
    /// <returns>该代码组合并逻辑的说明。</returns>
    public abstract string MergeLogic { get; }

    internal CodeGroup()
    {
    }

    internal CodeGroup(IMembershipCondition membershipCondition, PermissionSet permSet)
    {
      this.m_membershipCondition = membershipCondition;
      this.m_policy = new PolicyStatement();
      this.m_policy.SetPermissionSetNoCopy(permSet);
      this.m_children = (IList) ArrayList.Synchronized(new ArrayList());
      this.m_element = (SecurityElement) null;
      this.m_parentLevel = (PolicyLevel) null;
    }

    /// <summary>初始化 <see cref="T:System.Security.Policy.CodeGroup" /> 的新实例。</summary>
    /// <param name="membershipCondition">成员条件，它测试证据以确定此代码组是否应用策略。</param>
    /// <param name="policy">形式为权限集和特性的代码组的策略声明，这些权限集和特性将被授予匹配成员条件的代码。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="membershipCondition" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="membershipCondition" /> 参数的类型无效。- 或 -<paramref name="policy" /> 参数的类型无效。</exception>
    protected CodeGroup(IMembershipCondition membershipCondition, PolicyStatement policy)
    {
      if (membershipCondition == null)
        throw new ArgumentNullException("membershipCondition");
      this.m_policy = policy != null ? policy.Copy() : (PolicyStatement) null;
      this.m_membershipCondition = membershipCondition.Copy();
      this.m_children = (IList) ArrayList.Synchronized(new ArrayList());
      this.m_element = (SecurityElement) null;
      this.m_parentLevel = (PolicyLevel) null;
    }

    /// <summary>将子代码组添加到当前代码组。</summary>
    /// <param name="group">要作为子级添加的代码组。新的子代码组被添加到列表的末端。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="group" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="group" /> 参数不是有效的代码组。</exception>
    [SecuritySafeCritical]
    public void AddChild(CodeGroup group)
    {
      if (group == null)
        throw new ArgumentNullException("group");
      if (this.m_children == null)
        this.ParseChildren();
      lock (this)
        this.m_children.Add((object) group.Copy());
    }

    [SecurityCritical]
    internal void AddChildInternal(CodeGroup group)
    {
      if (group == null)
        throw new ArgumentNullException("group");
      if (this.m_children == null)
        this.ParseChildren();
      lock (this)
        this.m_children.Add((object) group);
    }

    /// <summary>移除指定的子代码组。</summary>
    /// <param name="group">要作为子级移除的代码组。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="group" /> 参数不是当前代码组的直接子代码组。</exception>
    [SecuritySafeCritical]
    public void RemoveChild(CodeGroup group)
    {
      if (group == null)
        return;
      if (this.m_children == null)
        this.ParseChildren();
      lock (this)
      {
        int local_2 = this.m_children.IndexOf((object) group);
        if (local_2 == -1)
          return;
        this.m_children.RemoveAt(local_2);
      }
    }

    [SecurityCritical]
    internal IList GetChildrenInternal()
    {
      if (this.m_children == null)
        this.ParseChildren();
      return this.m_children;
    }

    /// <summary>当在派生类中被重写时，解析证据集的代码组及其子代的策略。</summary>
    /// <returns>由具有可选特性的代码组授予的权限组成的策略声明；或者，如果代码组不适用（成员条件与指定的证据不匹配），则为 null。</returns>
    /// <param name="evidence">程序集的证据。</param>
    public abstract PolicyStatement Resolve(Evidence evidence);

    /// <summary>当在派生类中被重写时，解析匹配的代码组。</summary>
    /// <returns>一个 <see cref="T:System.Security.Policy.CodeGroup" />，它是匹配代码组的树的根。</returns>
    /// <param name="evidence">程序集的证据。</param>
    public abstract CodeGroup ResolveMatchingCodeGroups(Evidence evidence);

    /// <summary>当在派生类中被重写时，制作当前代码组的一个深层副本。</summary>
    /// <returns>当前代码组（包括其成员条件和子代码组）的等效副本。</returns>
    public abstract CodeGroup Copy();

    /// <summary>创建安全对象及其当前状态的 XML 编码。</summary>
    /// <returns>安全对象的 XML 编码，包括任何状态信息。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
    /// </PermissionSet>
    public SecurityElement ToXml()
    {
      return this.ToXml((PolicyLevel) null);
    }

    /// <summary>从 XML 编码重新构造具有给定状态的安全对象。</summary>
    /// <param name="e">用于重新构造安全对象的 XML 编码。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="e" /> 参数为 null。</exception>
    public void FromXml(SecurityElement e)
    {
      this.FromXml(e, (PolicyLevel) null);
    }

    /// <summary>创建安全对象、其当前状态以及代码所在策略级别的 XML 编码。</summary>
    /// <returns>安全对象的 XML 编码，包括任何状态信息。</returns>
    /// <param name="level">代码组所在的策略级别。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public SecurityElement ToXml(PolicyLevel level)
    {
      return this.ToXml(level, this.GetTypeName());
    }

    internal virtual string GetTypeName()
    {
      return this.GetType().FullName;
    }

    [SecurityCritical]
    internal SecurityElement ToXml(PolicyLevel level, string policyClassName)
    {
      if (this.m_membershipCondition == null && this.m_element != null)
        this.ParseMembershipCondition();
      if (this.m_children == null)
        this.ParseChildren();
      if (this.m_policy == null && this.m_element != null)
        this.ParsePolicy();
      SecurityElement element = new SecurityElement("CodeGroup");
      XMLUtil.AddClassAttribute(element, this.GetType(), policyClassName);
      element.AddAttribute("version", "1");
      element.AddChild(this.m_membershipCondition.ToXml(level));
      if (this.m_policy != null)
      {
        PermissionSet permissionSetNoCopy = this.m_policy.GetPermissionSetNoCopy();
        NamedPermissionSet namedPermissionSet = permissionSetNoCopy as NamedPermissionSet;
        if (namedPermissionSet != null && level != null && level.GetNamedPermissionSetInternal(namedPermissionSet.Name) != null)
          element.AddAttribute("PermissionSetName", namedPermissionSet.Name);
        else if (!permissionSetNoCopy.IsEmpty())
          element.AddChild(permissionSetNoCopy.ToXml());
        if (this.m_policy.Attributes != PolicyStatementAttribute.Nothing)
          element.AddAttribute("Attributes", XMLUtil.BitFieldEnumToString(typeof (PolicyStatementAttribute), (object) this.m_policy.Attributes));
      }
      if (this.m_children.Count > 0)
      {
        lock (this)
        {
          foreach (CodeGroup item_0 in (IEnumerable) this.m_children)
            element.AddChild(item_0.ToXml(level));
        }
      }
      if (this.m_name != null)
        element.AddAttribute("Name", SecurityElement.Escape(this.m_name));
      if (this.m_description != null)
        element.AddAttribute("Description", SecurityElement.Escape(this.m_description));
      this.CreateXml(element, level);
      return element;
    }

    /// <summary>当在派生类中被重写时，序列化导出的代码组所特有的属性和内部状态，并将序列化添加到指定的 <see cref="T:System.Security.SecurityElement" />。</summary>
    /// <param name="element">向其中添加序列化的 XML 编码。</param>
    /// <param name="level">代码组所在的策略级别。</param>
    protected virtual void CreateXml(SecurityElement element, PolicyLevel level)
    {
    }

    /// <summary>从 XML 编码重新构造具有给定的状态和策略级别的安全对象。</summary>
    /// <param name="e">用于重新构造安全对象的 XML 编码。</param>
    /// <param name="level">代码组所在的策略级别。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="e" /> 参数为 null。</exception>
    public void FromXml(SecurityElement e, PolicyLevel level)
    {
      if (e == null)
        throw new ArgumentNullException("e");
      lock (this)
      {
        this.m_element = e;
        this.m_parentLevel = level;
        this.m_children = (IList) null;
        this.m_membershipCondition = (IMembershipCondition) null;
        this.m_policy = (PolicyStatement) null;
        this.m_name = e.Attribute("Name");
        this.m_description = e.Attribute("Description");
        this.ParseXml(e, level);
      }
    }

    /// <summary>当在派生类中被重写时，从指定的 <see cref="T:System.Security.SecurityElement" /> 重新构造导出的代码组所特有的属性和内部状态。</summary>
    /// <param name="e">用于重新构造安全对象的 XML 编码。</param>
    /// <param name="level">代码组所在的策略级别。</param>
    protected virtual void ParseXml(SecurityElement e, PolicyLevel level)
    {
    }

    [SecurityCritical]
    private bool ParseMembershipCondition(bool safeLoad)
    {
      lock (this)
      {
        SecurityElement local_3 = this.m_element.SearchForChildByTag("IMembershipCondition");
        if (local_3 == null)
          throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidXMLElement"), (object) "IMembershipCondition", (object) this.GetType().FullName));
        IMembershipCondition local_2_1;
        try
        {
          local_2_1 = XMLUtil.CreateMembershipCondition(local_3);
          if (local_2_1 == null)
            return false;
        }
        catch (Exception exception_0)
        {
          throw new ArgumentException(Environment.GetResourceString("Argument_MembershipConditionElement"), exception_0);
        }
        local_2_1.FromXml(local_3, this.m_parentLevel);
        this.m_membershipCondition = local_2_1;
        return true;
      }
    }

    [SecurityCritical]
    private void ParseMembershipCondition()
    {
      this.ParseMembershipCondition(false);
    }

    [SecurityCritical]
    internal void ParseChildren()
    {
      lock (this)
      {
        ArrayList local_2 = ArrayList.Synchronized(new ArrayList());
        if (this.m_element != null && this.m_element.InternalChildren != null)
        {
          this.m_element.Children = (ArrayList) this.m_element.InternalChildren.Clone();
          ArrayList local_3 = ArrayList.Synchronized(new ArrayList());
          Evidence local_4 = new Evidence();
          int local_5 = this.m_element.InternalChildren.Count;
          int local_6 = 0;
          while (local_6 < local_5)
          {
            SecurityElement local_8 = (SecurityElement) this.m_element.Children[local_6];
            if (local_8.Tag.Equals("CodeGroup"))
            {
              CodeGroup local_9 = XMLUtil.CreateCodeGroup(local_8);
              if (local_9 != null)
              {
                local_9.FromXml(local_8, this.m_parentLevel);
                if (this.ParseMembershipCondition(true))
                {
                  local_9.Resolve(local_4);
                  local_9.MembershipCondition.Check(local_4);
                  local_2.Add((object) local_9);
                  ++local_6;
                }
                else
                {
                  this.m_element.InternalChildren.RemoveAt(local_6);
                  local_5 = this.m_element.InternalChildren.Count;
                  local_3.Add((object) new CodeGroupPositionMarker(local_6, local_2.Count, local_8));
                }
              }
              else
              {
                this.m_element.InternalChildren.RemoveAt(local_6);
                local_5 = this.m_element.InternalChildren.Count;
                local_3.Add((object) new CodeGroupPositionMarker(local_6, local_2.Count, local_8));
              }
            }
            else
              ++local_6;
          }
          foreach (CodeGroupPositionMarker item_0 in local_3)
          {
            CodeGroup local_11 = XMLUtil.CreateCodeGroup(item_0.element);
            if (local_11 == null)
              throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_FailedCodeGroup"), (object) item_0.element.Attribute("class")));
            local_11.FromXml(item_0.element, this.m_parentLevel);
            local_11.Resolve(local_4);
            local_11.MembershipCondition.Check(local_4);
            local_2.Insert(item_0.groupIndex, (object) local_11);
            this.m_element.InternalChildren.Insert(item_0.elementIndex, (object) item_0.element);
          }
        }
        this.m_children = (IList) local_2;
      }
    }

    private void ParsePolicy()
    {
label_0:
      PolicyStatement policyStatement = new PolicyStatement();
      bool flag = false;
      SecurityElement et = new SecurityElement("PolicyStatement");
      et.AddAttribute("version", "1");
      SecurityElement securityElement = this.m_element;
      lock (this)
      {
        if (this.m_element != null)
        {
          string local_6 = this.m_element.Attribute("PermissionSetName");
          if (local_6 != null)
          {
            et.AddAttribute("PermissionSetName", local_6);
            flag = true;
          }
          else
          {
            SecurityElement local_8 = this.m_element.SearchForChildByTag("PermissionSet");
            if (local_8 != null)
            {
              et.AddChild(local_8);
              flag = true;
            }
            else
            {
              et.AddChild(new PermissionSet(false).ToXml());
              flag = true;
            }
          }
          string local_7 = this.m_element.Attribute("Attributes");
          if (local_7 != null)
          {
            et.AddAttribute("Attributes", local_7);
            flag = true;
          }
        }
      }
      if (flag)
        policyStatement.FromXml(et, this.m_parentLevel);
      else
        policyStatement.PermissionSet = (PermissionSet) null;
      lock (this)
      {
        if (securityElement == this.m_element && this.m_policy == null)
          this.m_policy = policyStatement;
        else if (this.m_policy == null)
          goto label_0;
      }
      if (this.m_policy == null || this.m_children == null)
        return;
      IMembershipCondition membershipCondition = this.m_membershipCondition;
    }

    /// <summary>确定指定的代码组是否等效于当前代码组。</summary>
    /// <returns>如果指定的代码组等效于当前代码组，则为 true；否则为 false。</returns>
    /// <param name="o">要与当前代码组比较的代码组。</param>
    [SecuritySafeCritical]
    public override bool Equals(object o)
    {
      CodeGroup codeGroup = o as CodeGroup;
      if (codeGroup != null && this.GetType().Equals(codeGroup.GetType()) && (object.Equals((object) this.m_name, (object) codeGroup.m_name) && object.Equals((object) this.m_description, (object) codeGroup.m_description)))
      {
        if (this.m_membershipCondition == null && this.m_element != null)
          this.ParseMembershipCondition();
        if (codeGroup.m_membershipCondition == null && codeGroup.m_element != null)
          codeGroup.ParseMembershipCondition();
        if (object.Equals((object) this.m_membershipCondition, (object) codeGroup.m_membershipCondition))
          return true;
      }
      return false;
    }

    /// <summary>确定指定的代码组是否等效于当前代码组，如果指定，还检查子代码组。</summary>
    /// <returns>如果指定的代码组等效于当前代码组，则为 true；否则为 false。</returns>
    /// <param name="cg">要与当前代码组比较的代码组。</param>
    /// <param name="compareChildren">为 true 则还比较子代码组；否则为 false。</param>
    [SecuritySafeCritical]
    public bool Equals(CodeGroup cg, bool compareChildren)
    {
      if (!this.Equals((object) cg))
        return false;
      if (compareChildren)
      {
        if (this.m_children == null)
          this.ParseChildren();
        if (cg.m_children == null)
          cg.ParseChildren();
        ArrayList arrayList1 = new ArrayList((ICollection) this.m_children);
        ArrayList arrayList2 = new ArrayList((ICollection) cg.m_children);
        if (arrayList1.Count != arrayList2.Count)
          return false;
        for (int index = 0; index < arrayList1.Count; ++index)
        {
          if (!((CodeGroup) arrayList1[index]).Equals((CodeGroup) arrayList2[index], true))
            return false;
        }
      }
      return true;
    }

    /// <summary>获取当前代码组的哈希代码。</summary>
    /// <returns>当前代码组的哈希代码。</returns>
    [SecuritySafeCritical]
    public override int GetHashCode()
    {
      if (this.m_membershipCondition == null && this.m_element != null)
        this.ParseMembershipCondition();
      if (this.m_name != null || this.m_membershipCondition != null)
        return (this.m_name == null ? 0 : this.m_name.GetHashCode()) + (this.m_membershipCondition == null ? 0 : this.m_membershipCondition.GetHashCode());
      return this.GetType().GetHashCode();
    }
  }
}
