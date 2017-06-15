// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.CommonObjectSecurity
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Permissions;
using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>无需直接操作访问控制列表 (ACL) 而控制对对象的访问。此类是 <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> 类的抽象基类。</summary>
  [PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
  public abstract class CommonObjectSecurity : ObjectSecurity
  {
    /// <summary>初始化 <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> 类的新实例。</summary>
    /// <param name="isContainer">如果新对象是一个容器对象，则为 true。</param>
    protected CommonObjectSecurity(bool isContainer)
      : base(isContainer, false)
    {
    }

    internal CommonObjectSecurity(CommonSecurityDescriptor securityDescriptor)
      : base(securityDescriptor)
    {
    }

    private AuthorizationRuleCollection GetRules(bool access, bool includeExplicit, bool includeInherited, Type targetType)
    {
      this.ReadLock();
      try
      {
        AuthorizationRuleCollection authorizationRuleCollection = new AuthorizationRuleCollection();
        if (!SecurityIdentifier.IsValidTargetTypeStatic(targetType))
          throw new ArgumentException(Environment.GetResourceString("Arg_MustBeIdentityReferenceType"), "targetType");
        CommonAcl commonAcl = (CommonAcl) null;
        if (access)
        {
          if ((this._securityDescriptor.ControlFlags & ControlFlags.DiscretionaryAclPresent) != ControlFlags.None)
            commonAcl = (CommonAcl) this._securityDescriptor.DiscretionaryAcl;
        }
        else if ((this._securityDescriptor.ControlFlags & ControlFlags.SystemAclPresent) != ControlFlags.None)
          commonAcl = (CommonAcl) this._securityDescriptor.SystemAcl;
        if (commonAcl == null)
          return authorizationRuleCollection;
        IdentityReferenceCollection referenceCollection1 = (IdentityReferenceCollection) null;
        if (targetType != typeof (SecurityIdentifier))
        {
          IdentityReferenceCollection referenceCollection2 = new IdentityReferenceCollection(commonAcl.Count);
          for (int index = 0; index < commonAcl.Count; ++index)
          {
            CommonAce ace = commonAcl[index] as CommonAce;
            if (this.AceNeedsTranslation(ace, access, includeExplicit, includeInherited))
              referenceCollection2.Add((IdentityReference) ace.SecurityIdentifier);
          }
          referenceCollection1 = referenceCollection2.Translate(targetType);
        }
        int num = 0;
        for (int index = 0; index < commonAcl.Count; ++index)
        {
          CommonAce ace = commonAcl[index] as CommonAce;
          if (this.AceNeedsTranslation(ace, access, includeExplicit, includeInherited))
          {
            IdentityReference identityReference = targetType == typeof (SecurityIdentifier) ? (IdentityReference) ace.SecurityIdentifier : referenceCollection1[num++];
            if (access)
            {
              AccessControlType type = ace.AceQualifier != AceQualifier.AccessAllowed ? AccessControlType.Deny : AccessControlType.Allow;
              authorizationRuleCollection.AddRule((AuthorizationRule) this.AccessRuleFactory(identityReference, ace.AccessMask, ace.IsInherited, ace.InheritanceFlags, ace.PropagationFlags, type));
            }
            else
              authorizationRuleCollection.AddRule((AuthorizationRule) this.AuditRuleFactory(identityReference, ace.AccessMask, ace.IsInherited, ace.InheritanceFlags, ace.PropagationFlags, ace.AuditFlags));
          }
        }
        return authorizationRuleCollection;
      }
      finally
      {
        this.ReadUnlock();
      }
    }

    private bool AceNeedsTranslation(CommonAce ace, bool isAccessAce, bool includeExplicit, bool includeInherited)
    {
      if ((GenericAce) ace == (GenericAce) null)
        return false;
      if (isAccessAce)
      {
        if (ace.AceQualifier != AceQualifier.AccessAllowed && ace.AceQualifier != AceQualifier.AccessDenied)
          return false;
      }
      else if (ace.AceQualifier != AceQualifier.SystemAudit)
        return false;
      return includeExplicit && (ace.AceFlags & AceFlags.Inherited) == AceFlags.None || includeInherited && (ace.AceFlags & AceFlags.Inherited) != AceFlags.None;
    }

    /// <summary>将指定修改应用于与此 <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> 对象关联的自由访问控制列表 (DACL)。</summary>
    /// <returns>如果成功修改了 DACL，则为 true；否则为 false。</returns>
    /// <param name="modification">要应用于 DACL 的修改。</param>
    /// <param name="rule">要修改的访问规则。</param>
    /// <param name="modified">如果成功修改了 DACL，则为 true；否则为 false。</param>
    protected override bool ModifyAccess(AccessControlModification modification, AccessRule rule, out bool modified)
    {
      if (rule == null)
        throw new ArgumentNullException("rule");
      this.WriteLock();
      try
      {
        bool flag = true;
        if (this._securityDescriptor.DiscretionaryAcl == null)
        {
          if (modification == AccessControlModification.Remove || modification == AccessControlModification.RemoveAll || modification == AccessControlModification.RemoveSpecific)
          {
            modified = false;
            return flag;
          }
          this._securityDescriptor.DiscretionaryAcl = new DiscretionaryAcl(this.IsContainer, this.IsDS, GenericAcl.AclRevision, 1);
          this._securityDescriptor.AddControlFlags(ControlFlags.DiscretionaryAclPresent);
        }
        SecurityIdentifier sid = rule.IdentityReference.Translate(typeof (SecurityIdentifier)) as SecurityIdentifier;
        if (rule.AccessControlType == AccessControlType.Allow)
        {
          switch (modification)
          {
            case AccessControlModification.Add:
              this._securityDescriptor.DiscretionaryAcl.AddAccess(AccessControlType.Allow, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
              break;
            case AccessControlModification.Set:
              this._securityDescriptor.DiscretionaryAcl.SetAccess(AccessControlType.Allow, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
              break;
            case AccessControlModification.Reset:
              this._securityDescriptor.DiscretionaryAcl.RemoveAccess(AccessControlType.Deny, sid, -1, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None);
              this._securityDescriptor.DiscretionaryAcl.SetAccess(AccessControlType.Allow, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
              break;
            case AccessControlModification.Remove:
              flag = this._securityDescriptor.DiscretionaryAcl.RemoveAccess(AccessControlType.Allow, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
              break;
            case AccessControlModification.RemoveAll:
              flag = this._securityDescriptor.DiscretionaryAcl.RemoveAccess(AccessControlType.Allow, sid, -1, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None);
              if (!flag)
                throw new SystemException();
              break;
            case AccessControlModification.RemoveSpecific:
              this._securityDescriptor.DiscretionaryAcl.RemoveAccessSpecific(AccessControlType.Allow, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
              break;
            default:
              throw new ArgumentOutOfRangeException("modification", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
          }
        }
        else if (rule.AccessControlType == AccessControlType.Deny)
        {
          switch (modification)
          {
            case AccessControlModification.Add:
              this._securityDescriptor.DiscretionaryAcl.AddAccess(AccessControlType.Deny, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
              break;
            case AccessControlModification.Set:
              this._securityDescriptor.DiscretionaryAcl.SetAccess(AccessControlType.Deny, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
              break;
            case AccessControlModification.Reset:
              this._securityDescriptor.DiscretionaryAcl.RemoveAccess(AccessControlType.Allow, sid, -1, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None);
              this._securityDescriptor.DiscretionaryAcl.SetAccess(AccessControlType.Deny, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
              break;
            case AccessControlModification.Remove:
              flag = this._securityDescriptor.DiscretionaryAcl.RemoveAccess(AccessControlType.Deny, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
              break;
            case AccessControlModification.RemoveAll:
              flag = this._securityDescriptor.DiscretionaryAcl.RemoveAccess(AccessControlType.Deny, sid, -1, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None);
              if (!flag)
                throw new SystemException();
              break;
            case AccessControlModification.RemoveSpecific:
              this._securityDescriptor.DiscretionaryAcl.RemoveAccessSpecific(AccessControlType.Deny, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
              break;
            default:
              throw new ArgumentOutOfRangeException("modification", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
          }
        }
        else
          throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) rule.AccessControlType), "rule.AccessControlType");
        modified = flag;
        this.AccessRulesModified = this.AccessRulesModified | modified;
        return flag;
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>将指定修改应用于与此 <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> 对象关联的系统访问控制列表 (SACL)。</summary>
    /// <returns>如果成功修改了 SACL，则为 true；否则为 false。</returns>
    /// <param name="modification">要应用于 SACL 的修改。</param>
    /// <param name="rule">要修改的审核规则。</param>
    /// <param name="modified">如果成功修改了 SACL，则为 true；否则为 false。</param>
    protected override bool ModifyAudit(AccessControlModification modification, AuditRule rule, out bool modified)
    {
      if (rule == null)
        throw new ArgumentNullException("rule");
      this.WriteLock();
      try
      {
        bool flag = true;
        if (this._securityDescriptor.SystemAcl == null)
        {
          if (modification == AccessControlModification.Remove || modification == AccessControlModification.RemoveAll || modification == AccessControlModification.RemoveSpecific)
          {
            modified = false;
            return flag;
          }
          this._securityDescriptor.SystemAcl = new SystemAcl(this.IsContainer, this.IsDS, GenericAcl.AclRevision, 1);
          this._securityDescriptor.AddControlFlags(ControlFlags.SystemAclPresent);
        }
        SecurityIdentifier sid = rule.IdentityReference.Translate(typeof (SecurityIdentifier)) as SecurityIdentifier;
        switch (modification)
        {
          case AccessControlModification.Add:
            this._securityDescriptor.SystemAcl.AddAudit(rule.AuditFlags, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
            break;
          case AccessControlModification.Set:
            this._securityDescriptor.SystemAcl.SetAudit(rule.AuditFlags, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
            break;
          case AccessControlModification.Reset:
            this._securityDescriptor.SystemAcl.SetAudit(rule.AuditFlags, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
            break;
          case AccessControlModification.Remove:
            flag = this._securityDescriptor.SystemAcl.RemoveAudit(rule.AuditFlags, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
            break;
          case AccessControlModification.RemoveAll:
            flag = this._securityDescriptor.SystemAcl.RemoveAudit(AuditFlags.Success | AuditFlags.Failure, sid, -1, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None);
            if (!flag)
              throw new InvalidProgramException();
            break;
          case AccessControlModification.RemoveSpecific:
            this._securityDescriptor.SystemAcl.RemoveAuditSpecific(rule.AuditFlags, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
            break;
          default:
            throw new ArgumentOutOfRangeException("modification", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
        }
        modified = flag;
        this.AuditRulesModified = this.AuditRulesModified | modified;
        return flag;
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>将指定的访问规则添加到与此 <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> 对象关联的自由访问控制列表 (DACL)。</summary>
    /// <param name="rule">要添加的访问规则。</param>
    protected void AddAccessRule(AccessRule rule)
    {
      if (rule == null)
        throw new ArgumentNullException("rule");
      this.WriteLock();
      try
      {
        bool modified;
        this.ModifyAccess(AccessControlModification.Add, rule, out modified);
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>从与此 <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> 对象关联的自由访问控制列表 (DACL) 中移除与指定的访问规则具有相同安全性标识符和限定符的所有访问规则，然后添加指定的访问规则。</summary>
    /// <param name="rule">要设置的访问规则。</param>
    protected void SetAccessRule(AccessRule rule)
    {
      if (rule == null)
        throw new ArgumentNullException("rule");
      this.WriteLock();
      try
      {
        bool modified;
        this.ModifyAccess(AccessControlModification.Set, rule, out modified);
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>从与此 <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> 对象关联的自由访问控制列表 (DACL) 中移除所有访问规则，然后添加指定的访问规则。</summary>
    /// <param name="rule">要重置的访问规则。</param>
    protected void ResetAccessRule(AccessRule rule)
    {
      if (rule == null)
        throw new ArgumentNullException("rule");
      this.WriteLock();
      try
      {
        bool modified;
        this.ModifyAccess(AccessControlModification.Reset, rule, out modified);
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>从与此 <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> 对象关联的自由访问控制列表 (DACL) 中移除与指定的访问规则具有相同安全性标识符和访问掩码的访问规则。</summary>
    /// <returns>如果访问规则已成功移除，则为 true；否则为 false。</returns>
    /// <param name="rule">要移除的访问规则。</param>
    protected bool RemoveAccessRule(AccessRule rule)
    {
      if (rule == null)
        throw new ArgumentNullException("rule");
      this.WriteLock();
      try
      {
        if (this._securityDescriptor == null)
          return true;
        bool modified;
        return this.ModifyAccess(AccessControlModification.Remove, rule, out modified);
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>从与此 <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> 对象关联的自由访问控制列表 (DACL) 中移除与指定的访问规则具有相同安全性标识符的所有访问规则。</summary>
    /// <param name="rule">要移除的访问规则。</param>
    protected void RemoveAccessRuleAll(AccessRule rule)
    {
      if (rule == null)
        throw new ArgumentNullException("rule");
      this.WriteLock();
      try
      {
        if (this._securityDescriptor == null)
          return;
        bool modified;
        this.ModifyAccess(AccessControlModification.RemoveAll, rule, out modified);
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>从与此 <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> 对象关联的自由访问控制列表 (DACL) 中移除与指定的访问规则完全匹配的所有访问规则。</summary>
    /// <param name="rule">要移除的访问规则。</param>
    protected void RemoveAccessRuleSpecific(AccessRule rule)
    {
      if (rule == null)
        throw new ArgumentNullException("rule");
      this.WriteLock();
      try
      {
        if (this._securityDescriptor == null)
          return;
        bool modified;
        this.ModifyAccess(AccessControlModification.RemoveSpecific, rule, out modified);
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>将指定的审核规则添加到与该 <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> 对象关联的系统访问控制列表 (SACL)。</summary>
    /// <param name="rule">要添加的审核规则。</param>
    protected void AddAuditRule(AuditRule rule)
    {
      if (rule == null)
        throw new ArgumentNullException("rule");
      this.WriteLock();
      try
      {
        bool modified;
        this.ModifyAudit(AccessControlModification.Add, rule, out modified);
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>从与此 <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> 对象关联的系统访问控制列表 (SACL) 中移除与指定的审核规则具有相同的安全性标识符和限定符所有审核规则，然后添加指定的审核规则。</summary>
    /// <param name="rule">要设置的审核规则。</param>
    protected void SetAuditRule(AuditRule rule)
    {
      if (rule == null)
        throw new ArgumentNullException("rule");
      this.WriteLock();
      try
      {
        bool modified;
        this.ModifyAudit(AccessControlModification.Set, rule, out modified);
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>从与此 <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> 对象关联的系统访问控制列表 (SACL) 中移除与指定的审核规则具有相同安全性标识符和访问掩码的审核规则。</summary>
    /// <returns>如果审核规则已成功移除，则为 true；否则为 false。</returns>
    /// <param name="rule">要移除的审核规则。</param>
    protected bool RemoveAuditRule(AuditRule rule)
    {
      if (rule == null)
        throw new ArgumentNullException("rule");
      this.WriteLock();
      try
      {
        bool modified;
        return this.ModifyAudit(AccessControlModification.Remove, rule, out modified);
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>从与此 <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> 对象关联的系统访问控制列表 (SACL) 中移除与指定的审核规则具有相同安全性标识符的所有审核规则。</summary>
    /// <param name="rule">要移除的审核规则。</param>
    protected void RemoveAuditRuleAll(AuditRule rule)
    {
      if (rule == null)
        throw new ArgumentNullException("rule");
      this.WriteLock();
      try
      {
        bool modified;
        this.ModifyAudit(AccessControlModification.RemoveAll, rule, out modified);
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>从与此 <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> 对象关联的系统访问控制列表 (SACL) 中移除与指定的审核规则完全匹配的所有审核规则。</summary>
    /// <param name="rule">要移除的审核规则。</param>
    protected void RemoveAuditRuleSpecific(AuditRule rule)
    {
      if (rule == null)
        throw new ArgumentNullException("rule");
      this.WriteLock();
      try
      {
        bool modified;
        this.ModifyAudit(AccessControlModification.RemoveSpecific, rule, out modified);
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>获取与指定的安全性标识符关联的访问规则的集合。</summary>
    /// <returns>与指定的 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 对象关联的访问规则的集合。</returns>
    /// <param name="includeExplicit">若要包括为对象显式设置的访问规则，则为 true。</param>
    /// <param name="includeInherited">若要包括继承的访问规则，则为 true。</param>
    /// <param name="targetType">指定要为其检索访问规则的安全标识符是属于 T:System.Security.Principal.SecurityIdentifier 类型，还是属于 T:System.Security.Principal.NTAccount 类型。此参数的值所隶属的类型必须要能转换为 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 类型。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    public AuthorizationRuleCollection GetAccessRules(bool includeExplicit, bool includeInherited, Type targetType)
    {
      return this.GetRules(true, includeExplicit, includeInherited, targetType);
    }

    /// <summary>获取与指定的安全性标识符关联的审核规则的集合。</summary>
    /// <returns>与指定的 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 对象关联的审核规则的集合。</returns>
    /// <param name="includeExplicit">若要包括为对象显式设置的审核规则，则为 true。</param>
    /// <param name="includeInherited">若要包括继承的审核规则，则为 true。</param>
    /// <param name="targetType">要为其检索审核规则的安全性标识符。这必须是可以强制转换为 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 对象的对象。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    public AuthorizationRuleCollection GetAuditRules(bool includeExplicit, bool includeInherited, Type targetType)
    {
      return this.GetRules(false, includeExplicit, includeInherited, targetType);
    }
  }
}
