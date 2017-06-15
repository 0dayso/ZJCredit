// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.DirectoryObjectSecurity
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>提供无需直接操作访问控制列表 (ACL) 而控制对目录对象的访问的能力。</summary>
  public abstract class DirectoryObjectSecurity : ObjectSecurity
  {
    /// <summary>初始化 <see cref="T:System.Security.AccessControl.DirectoryObjectSecurity" /> 类的新实例。</summary>
    protected DirectoryObjectSecurity()
      : base(true, true)
    {
    }

    /// <summary>使用指定的安全性说明符初始化 <see cref="T:System.Security.AccessControl.DirectoryObjectSecurity" /> 类的新实例。</summary>
    /// <param name="securityDescriptor">将与新的 <see cref="T:System.Security.AccessControl.DirectoryObjectSecurity" /> 对象关联的安全性说明符。</param>
    protected DirectoryObjectSecurity(CommonSecurityDescriptor securityDescriptor)
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
            QualifiedAce qualifiedAce = commonAcl[index] as QualifiedAce;
            if (!((GenericAce) qualifiedAce == (GenericAce) null) && !qualifiedAce.IsCallback)
            {
              if (access)
              {
                if (qualifiedAce.AceQualifier != AceQualifier.AccessAllowed && qualifiedAce.AceQualifier != AceQualifier.AccessDenied)
                  continue;
              }
              else if (qualifiedAce.AceQualifier != AceQualifier.SystemAudit)
                continue;
              referenceCollection2.Add((IdentityReference) qualifiedAce.SecurityIdentifier);
            }
          }
          referenceCollection1 = referenceCollection2.Translate(targetType);
        }
        for (int index = 0; index < commonAcl.Count; ++index)
        {
          QualifiedAce qualifiedAce = (QualifiedAce) (commonAcl[index] as CommonAce);
          if ((GenericAce) qualifiedAce == (GenericAce) null)
          {
            qualifiedAce = (QualifiedAce) (commonAcl[index] as ObjectAce);
            if ((GenericAce) qualifiedAce == (GenericAce) null)
              continue;
          }
          if (!qualifiedAce.IsCallback)
          {
            if (access)
            {
              if (qualifiedAce.AceQualifier != AceQualifier.AccessAllowed && qualifiedAce.AceQualifier != AceQualifier.AccessDenied)
                continue;
            }
            else if (qualifiedAce.AceQualifier != AceQualifier.SystemAudit)
              continue;
            if (includeExplicit && (qualifiedAce.AceFlags & AceFlags.Inherited) == AceFlags.None || includeInherited && (qualifiedAce.AceFlags & AceFlags.Inherited) != AceFlags.None)
            {
              IdentityReference identityReference = targetType == typeof (SecurityIdentifier) ? (IdentityReference) qualifiedAce.SecurityIdentifier : referenceCollection1[index];
              if (access)
              {
                AccessControlType type = qualifiedAce.AceQualifier != AceQualifier.AccessAllowed ? AccessControlType.Deny : AccessControlType.Allow;
                if (qualifiedAce is ObjectAce)
                {
                  ObjectAce objectAce = qualifiedAce as ObjectAce;
                  authorizationRuleCollection.AddRule((AuthorizationRule) this.AccessRuleFactory(identityReference, objectAce.AccessMask, objectAce.IsInherited, objectAce.InheritanceFlags, objectAce.PropagationFlags, type, objectAce.ObjectAceType, objectAce.InheritedObjectAceType));
                }
                else
                {
                  CommonAce commonAce = qualifiedAce as CommonAce;
                  if (!((GenericAce) commonAce == (GenericAce) null))
                    authorizationRuleCollection.AddRule((AuthorizationRule) this.AccessRuleFactory(identityReference, commonAce.AccessMask, commonAce.IsInherited, commonAce.InheritanceFlags, commonAce.PropagationFlags, type));
                }
              }
              else if (qualifiedAce is ObjectAce)
              {
                ObjectAce objectAce = qualifiedAce as ObjectAce;
                authorizationRuleCollection.AddRule((AuthorizationRule) this.AuditRuleFactory(identityReference, objectAce.AccessMask, objectAce.IsInherited, objectAce.InheritanceFlags, objectAce.PropagationFlags, objectAce.AuditFlags, objectAce.ObjectAceType, objectAce.InheritedObjectAceType));
              }
              else
              {
                CommonAce commonAce = qualifiedAce as CommonAce;
                if (!((GenericAce) commonAce == (GenericAce) null))
                  authorizationRuleCollection.AddRule((AuthorizationRule) this.AuditRuleFactory(identityReference, commonAce.AccessMask, commonAce.IsInherited, commonAce.InheritanceFlags, commonAce.PropagationFlags, commonAce.AuditFlags));
              }
            }
          }
        }
        return authorizationRuleCollection;
      }
      finally
      {
        this.ReadUnlock();
      }
    }

    private bool ModifyAccess(AccessControlModification modification, ObjectAccessRule rule, out bool modified)
    {
      bool flag = true;
      if (this._securityDescriptor.DiscretionaryAcl == null)
      {
        if (modification == AccessControlModification.Remove || modification == AccessControlModification.RemoveAll || modification == AccessControlModification.RemoveSpecific)
        {
          modified = false;
          return flag;
        }
        this._securityDescriptor.DiscretionaryAcl = new DiscretionaryAcl(this.IsContainer, this.IsDS, GenericAcl.AclRevisionDS, 1);
        this._securityDescriptor.AddControlFlags(ControlFlags.DiscretionaryAclPresent);
      }
      else if ((modification == AccessControlModification.Add || modification == AccessControlModification.Set || modification == AccessControlModification.Reset) && (rule.ObjectFlags != ObjectAceFlags.None && (int) this._securityDescriptor.DiscretionaryAcl.Revision < (int) GenericAcl.AclRevisionDS))
      {
        byte[] binaryForm = new byte[this._securityDescriptor.DiscretionaryAcl.BinaryLength];
        this._securityDescriptor.DiscretionaryAcl.GetBinaryForm(binaryForm, 0);
        binaryForm[0] = GenericAcl.AclRevisionDS;
        this._securityDescriptor.DiscretionaryAcl = new DiscretionaryAcl(this.IsContainer, this.IsDS, new RawAcl(binaryForm, 0));
      }
      SecurityIdentifier sid = rule.IdentityReference.Translate(typeof (SecurityIdentifier)) as SecurityIdentifier;
      if (rule.AccessControlType == AccessControlType.Allow)
      {
        switch (modification)
        {
          case AccessControlModification.Add:
            this._securityDescriptor.DiscretionaryAcl.AddAccess(AccessControlType.Allow, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
            break;
          case AccessControlModification.Set:
            this._securityDescriptor.DiscretionaryAcl.SetAccess(AccessControlType.Allow, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
            break;
          case AccessControlModification.Reset:
            this._securityDescriptor.DiscretionaryAcl.RemoveAccess(AccessControlType.Deny, sid, -1, InheritanceFlags.ContainerInherit, PropagationFlags.None, ObjectAceFlags.None, Guid.Empty, Guid.Empty);
            this._securityDescriptor.DiscretionaryAcl.SetAccess(AccessControlType.Allow, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
            break;
          case AccessControlModification.Remove:
            flag = this._securityDescriptor.DiscretionaryAcl.RemoveAccess(AccessControlType.Allow, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
            break;
          case AccessControlModification.RemoveAll:
            flag = this._securityDescriptor.DiscretionaryAcl.RemoveAccess(AccessControlType.Allow, sid, -1, InheritanceFlags.ContainerInherit, PropagationFlags.None, ObjectAceFlags.None, Guid.Empty, Guid.Empty);
            if (!flag)
              throw new SystemException();
            break;
          case AccessControlModification.RemoveSpecific:
            this._securityDescriptor.DiscretionaryAcl.RemoveAccessSpecific(AccessControlType.Allow, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
            break;
          default:
            throw new ArgumentOutOfRangeException("modification", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
        }
      }
      else
      {
        if (rule.AccessControlType != AccessControlType.Deny)
          throw new SystemException();
        switch (modification)
        {
          case AccessControlModification.Add:
            this._securityDescriptor.DiscretionaryAcl.AddAccess(AccessControlType.Deny, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
            break;
          case AccessControlModification.Set:
            this._securityDescriptor.DiscretionaryAcl.SetAccess(AccessControlType.Deny, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
            break;
          case AccessControlModification.Reset:
            this._securityDescriptor.DiscretionaryAcl.RemoveAccess(AccessControlType.Allow, sid, -1, InheritanceFlags.ContainerInherit, PropagationFlags.None, ObjectAceFlags.None, Guid.Empty, Guid.Empty);
            this._securityDescriptor.DiscretionaryAcl.SetAccess(AccessControlType.Deny, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
            break;
          case AccessControlModification.Remove:
            flag = this._securityDescriptor.DiscretionaryAcl.RemoveAccess(AccessControlType.Deny, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
            break;
          case AccessControlModification.RemoveAll:
            flag = this._securityDescriptor.DiscretionaryAcl.RemoveAccess(AccessControlType.Deny, sid, -1, InheritanceFlags.ContainerInherit, PropagationFlags.None, ObjectAceFlags.None, Guid.Empty, Guid.Empty);
            if (!flag)
              throw new SystemException();
            break;
          case AccessControlModification.RemoveSpecific:
            this._securityDescriptor.DiscretionaryAcl.RemoveAccessSpecific(AccessControlType.Deny, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
            break;
          default:
            throw new ArgumentOutOfRangeException("modification", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
        }
      }
      modified = flag;
      this.AccessRulesModified = this.AccessRulesModified | modified;
      return flag;
    }

    private bool ModifyAudit(AccessControlModification modification, ObjectAuditRule rule, out bool modified)
    {
      bool flag = true;
      if (this._securityDescriptor.SystemAcl == null)
      {
        if (modification == AccessControlModification.Remove || modification == AccessControlModification.RemoveAll || modification == AccessControlModification.RemoveSpecific)
        {
          modified = false;
          return flag;
        }
        this._securityDescriptor.SystemAcl = new SystemAcl(this.IsContainer, this.IsDS, GenericAcl.AclRevisionDS, 1);
        this._securityDescriptor.AddControlFlags(ControlFlags.SystemAclPresent);
      }
      else if ((modification == AccessControlModification.Add || modification == AccessControlModification.Set || modification == AccessControlModification.Reset) && (rule.ObjectFlags != ObjectAceFlags.None && (int) this._securityDescriptor.SystemAcl.Revision < (int) GenericAcl.AclRevisionDS))
      {
        byte[] binaryForm = new byte[this._securityDescriptor.SystemAcl.BinaryLength];
        this._securityDescriptor.SystemAcl.GetBinaryForm(binaryForm, 0);
        binaryForm[0] = GenericAcl.AclRevisionDS;
        this._securityDescriptor.SystemAcl = new SystemAcl(this.IsContainer, this.IsDS, new RawAcl(binaryForm, 0));
      }
      SecurityIdentifier sid = rule.IdentityReference.Translate(typeof (SecurityIdentifier)) as SecurityIdentifier;
      switch (modification)
      {
        case AccessControlModification.Add:
          this._securityDescriptor.SystemAcl.AddAudit(rule.AuditFlags, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
          break;
        case AccessControlModification.Set:
          this._securityDescriptor.SystemAcl.SetAudit(rule.AuditFlags, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
          break;
        case AccessControlModification.Reset:
          this._securityDescriptor.SystemAcl.RemoveAudit(AuditFlags.Success | AuditFlags.Failure, sid, -1, InheritanceFlags.ContainerInherit, PropagationFlags.None, ObjectAceFlags.None, Guid.Empty, Guid.Empty);
          this._securityDescriptor.SystemAcl.SetAudit(rule.AuditFlags, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
          break;
        case AccessControlModification.Remove:
          flag = this._securityDescriptor.SystemAcl.RemoveAudit(rule.AuditFlags, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
          break;
        case AccessControlModification.RemoveAll:
          flag = this._securityDescriptor.SystemAcl.RemoveAudit(AuditFlags.Success | AuditFlags.Failure, sid, -1, InheritanceFlags.ContainerInherit, PropagationFlags.None, ObjectAceFlags.None, Guid.Empty, Guid.Empty);
          if (!flag)
            throw new SystemException();
          break;
        case AccessControlModification.RemoveSpecific:
          this._securityDescriptor.SystemAcl.RemoveAuditSpecific(rule.AuditFlags, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
          break;
        default:
          throw new ArgumentOutOfRangeException("modification", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
      }
      modified = flag;
      this.AuditRulesModified = this.AuditRulesModified | modified;
      return flag;
    }

    /// <summary>用指定的值初始化 <see cref="T:System.Security.AccessControl.AccessRule" /> 类的新实例。</summary>
    /// <returns>此方法所创建的 <see cref="T:System.Security.AccessControl.AccessRule" /> 对象。</returns>
    /// <param name="identityReference">应用访问规则的标识。它必须是可强制转换为 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 的对象。</param>
    /// <param name="accessMask">此规则的访问掩码。访问掩码是一个 32 位的匿名位集合，其含义是由每个集成器定义的。</param>
    /// <param name="isInherited">如果此规则继承自父容器，则为 true。</param>
    /// <param name="inheritanceFlags">指定访问规则的继承属性。</param>
    /// <param name="propagationFlags">指定继承的访问规则是否自动传播。如果 <paramref name="inheritanceFlags" /> 设置为 <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />，则将忽略传播标志。</param>
    /// <param name="type">指定有效的访问控制类型。</param>
    /// <param name="objectType">新访问规则所应用到的对象的类标识。</param>
    /// <param name="inheritedObjectType">可以继承新访问规则的子对象的类标识。</param>
    public virtual AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type, Guid objectType, Guid inheritedObjectType)
    {
      throw new NotImplementedException();
    }

    /// <summary>用指定的值初始化 <see cref="T:System.Security.AccessControl.AuditRule" /> 类的新实例。</summary>
    /// <returns>此方法所创建的 <see cref="T:System.Security.AccessControl.AuditRule" /> 对象。</returns>
    /// <param name="identityReference">审核规则应用到的标识。它必须是可强制转换为 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 的对象。</param>
    /// <param name="accessMask">此规则的访问掩码。访问掩码是一个 32 位的匿名位集合，其含义是由每个集成器定义的。</param>
    /// <param name="isInherited">如果此规则继承自父容器，则为 true。</param>
    /// <param name="inheritanceFlags">指定审核规则的继承属性。</param>
    /// <param name="propagationFlags">指定继承的审核规则是否自动传播。如果 <paramref name="inheritanceFlags" /> 设置为 <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />，则将忽略传播标志。</param>
    /// <param name="flags">指定对规则进行审核的条件。</param>
    /// <param name="objectType">新审核规则所应用到的对象的类标识。</param>
    /// <param name="inheritedObjectType">可以继承新审核规则的子对象的类标识。</param>
    public virtual AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags, Guid objectType, Guid inheritedObjectType)
    {
      throw new NotImplementedException();
    }

    /// <summary>将指定修改应用于与此 <see cref="T:System.Security.AccessControl.DirectoryObjectSecurity" /> 对象关联的自由访问控制列表 (DACL)。</summary>
    /// <returns>如果成功修改了 DACL，则为 true；否则为 false。</returns>
    /// <param name="modification">要应用于 DACL 的修改。</param>
    /// <param name="rule">要修改的访问规则。</param>
    /// <param name="modified">如果成功修改了 DACL，则为 true；否则为 false。</param>
    protected override bool ModifyAccess(AccessControlModification modification, AccessRule rule, out bool modified)
    {
      if (!this.AccessRuleType.IsAssignableFrom(rule.GetType()))
        throw new ArgumentException(Environment.GetResourceString("AccessControl_InvalidAccessRuleType"), "rule");
      return this.ModifyAccess(modification, rule as ObjectAccessRule, out modified);
    }

    /// <summary>将指定修改应用于与此 <see cref="T:System.Security.AccessControl.DirectoryObjectSecurity" /> 对象关联的系统访问控制列表 (SACL)。</summary>
    /// <returns>如果成功修改了 SACL，则为 true；否则为 false。</returns>
    /// <param name="modification">要应用于 SACL 的修改。</param>
    /// <param name="rule">要修改的审核规则。</param>
    /// <param name="modified">如果成功修改了 SACL，则为 true；否则为 false。</param>
    protected override bool ModifyAudit(AccessControlModification modification, AuditRule rule, out bool modified)
    {
      if (!this.AuditRuleType.IsAssignableFrom(rule.GetType()))
        throw new ArgumentException(Environment.GetResourceString("AccessControl_InvalidAuditRuleType"), "rule");
      return this.ModifyAudit(modification, rule as ObjectAuditRule, out modified);
    }

    /// <summary>将指定的访问规则添加到与此 <see cref="T:System.Security.AccessControl.DirectoryObjectSecurity" /> 对象关联的自由访问控制列表 (DACL)。</summary>
    /// <param name="rule">要添加的访问规则。</param>
    protected void AddAccessRule(ObjectAccessRule rule)
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

    /// <summary>从与此 <see cref="T:System.Security.AccessControl.DirectoryObjectSecurity" /> 对象关联的自由访问控制列表 (DACL) 中移除与指定的访问规则具有相同安全性标识符和限定符的所有访问规则，然后添加指定的访问规则。</summary>
    /// <param name="rule">要设置的访问规则。</param>
    protected void SetAccessRule(ObjectAccessRule rule)
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

    /// <summary>从与此 <see cref="T:System.Security.AccessControl.DirectoryObjectSecurity" /> 对象关联的自由访问控制列表 (DACL) 中移除所有访问规则，然后添加指定的访问规则。</summary>
    /// <param name="rule">要重置的访问规则。</param>
    protected void ResetAccessRule(ObjectAccessRule rule)
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

    /// <summary>从与此 <see cref="T:System.Security.AccessControl.DirectoryObjectSecurity" /> 对象关联的自由访问控制列表 (DACL) 中移除与指定的访问规则具有相同安全性标识符和访问掩码的访问规则。</summary>
    /// <returns>如果访问规则已成功移除，则为 true；否则为 false。</returns>
    /// <param name="rule">要移除的访问规则。</param>
    protected bool RemoveAccessRule(ObjectAccessRule rule)
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

    /// <summary>从与此 <see cref="T:System.Security.AccessControl.DirectoryObjectSecurity" /> 对象关联的自由访问控制列表 (DACL) 中移除与指定的访问规则具有相同安全性标识符的所有访问规则。</summary>
    /// <param name="rule">要移除的访问规则。</param>
    protected void RemoveAccessRuleAll(ObjectAccessRule rule)
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

    /// <summary>从与此 <see cref="T:System.Security.AccessControl.DirectoryObjectSecurity" /> 对象关联的自由访问控制列表 (DACL) 中移除与指定的访问规则完全匹配的所有访问规则。</summary>
    /// <param name="rule">要移除的访问规则。</param>
    protected void RemoveAccessRuleSpecific(ObjectAccessRule rule)
    {
      if (rule == null)
        throw new ArgumentNullException("rule");
      if (this._securityDescriptor == null)
        return;
      this.WriteLock();
      try
      {
        bool modified;
        this.ModifyAccess(AccessControlModification.RemoveSpecific, rule, out modified);
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>将指定的审核规则添加到与该 <see cref="T:System.Security.AccessControl.DirectoryObjectSecurity" /> 对象关联的系统访问控制列表 (SACL)。</summary>
    /// <param name="rule">要添加的审核规则。</param>
    protected void AddAuditRule(ObjectAuditRule rule)
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

    /// <summary>从与此 <see cref="T:System.Security.AccessControl.DirectoryObjectSecurity" /> 对象关联的系统访问控制列表 (SACL) 中移除与指定的审核规则具有相同的安全性标识符和限定符所有审核规则，然后添加指定的审核规则。</summary>
    /// <param name="rule">要设置的审核规则。</param>
    protected void SetAuditRule(ObjectAuditRule rule)
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
    protected bool RemoveAuditRule(ObjectAuditRule rule)
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

    /// <summary>从与此 <see cref="T:System.Security.AccessControl.DirectoryObjectSecurity" /> 对象关联的系统访问控制列表 (SACL) 中移除与指定的审核规则具有相同安全性标识符的所有审核规则。</summary>
    /// <param name="rule">要移除的审核规则。</param>
    protected void RemoveAuditRuleAll(ObjectAuditRule rule)
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

    /// <summary>从与此 <see cref="T:System.Security.AccessControl.DirectoryObjectSecurity" /> 对象关联的系统访问控制列表 (SACL) 中移除与指定的审核规则完全匹配的所有审核规则。</summary>
    /// <param name="rule">要移除的审核规则。</param>
    protected void RemoveAuditRuleSpecific(ObjectAuditRule rule)
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
    /// <param name="targetType">要为其检索访问规则的安全性标识符。这必须是可以强制转换为 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 对象的对象。</param>
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
