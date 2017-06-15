// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.SystemAcl
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>表示系统访问控制列表 (SACL)。</summary>
  public sealed class SystemAcl : CommonAcl
  {
    /// <summary>用指定的值初始化 <see cref="T:System.Security.AccessControl.SystemAcl" /> 类的新实例。</summary>
    /// <param name="isContainer">如果新的 <see cref="T:System.Security.AccessControl.SystemAcl" /> 对象是一个容器，则为 true。</param>
    /// <param name="isDS">如果新的 <see cref="T:System.Security.AccessControl.SystemAcl" /> 对象是一个目录对象的访问控制列表 (ACL)，则为 true。</param>
    /// <param name="capacity">此 <see cref="T:System.Security.AccessControl.SystemAcl" /> 对象可包含的访问控制项 (ACE) 的数量。此数量只作为一种提示。</param>
    public SystemAcl(bool isContainer, bool isDS, int capacity)
    {
      int num1 = isContainer ? 1 : 0;
      int num2 = isDS ? 1 : 0;
      int num3 = num2 != 0 ? (int) GenericAcl.AclRevisionDS : (int) GenericAcl.AclRevision;
      int capacity1 = capacity;
      // ISSUE: explicit constructor call
      this.\u002Ector(num1 != 0, num2 != 0, (byte) num3, capacity1);
    }

    /// <summary>用指定的值初始化 <see cref="T:System.Security.AccessControl.SystemAcl" /> 类的新实例。</summary>
    /// <param name="isContainer">如果新的 <see cref="T:System.Security.AccessControl.SystemAcl" /> 对象是一个容器，则为 true。</param>
    /// <param name="isDS">如果新的 <see cref="T:System.Security.AccessControl.SystemAcl" /> 对象是一个目录对象的访问控制列表 (ACL)，则为 true。</param>
    /// <param name="revision">新的 <see cref="T:System.Security.AccessControl.SystemAcl" /> 对象的修订级别。</param>
    /// <param name="capacity">此 <see cref="T:System.Security.AccessControl.SystemAcl" /> 对象可包含的访问控制项 (ACE) 的数量。此数量只作为一种提示。</param>
    public SystemAcl(bool isContainer, bool isDS, byte revision, int capacity)
      : base(isContainer, isDS, revision, capacity)
    {
    }

    /// <summary>使用指定的 <see cref="T:System.Security.AccessControl.RawAcl" /> 对象中的指定值初始化 <see cref="T:System.Security.AccessControl.SystemAcl" /> 类的新实例。</summary>
    /// <param name="isContainer">如果新的 <see cref="T:System.Security.AccessControl.SystemAcl" /> 对象是一个容器，则为 true。</param>
    /// <param name="isDS">如果新的 <see cref="T:System.Security.AccessControl.SystemAcl" /> 对象是一个目录对象的访问控制列表 (ACL)，则为 true。</param>
    /// <param name="rawAcl">新的 <see cref="T:System.Security.AccessControl.SystemAcl" /> 对象的基础 <see cref="T:System.Security.AccessControl.RawAcl" /> 对象。指定 null 以创建空的 ACL。</param>
    public SystemAcl(bool isContainer, bool isDS, RawAcl rawAcl)
      : this(isContainer, isDS, rawAcl, false)
    {
    }

    internal SystemAcl(bool isContainer, bool isDS, RawAcl rawAcl, bool trusted)
      : base(isContainer, isDS, rawAcl, trusted, false)
    {
    }

    /// <summary>将一个审核规则添加到当前 <see cref="T:System.Security.AccessControl.SystemAcl" /> 对象。</summary>
    /// <param name="auditFlags">要添加的审核规则的类型。</param>
    /// <param name="sid">要为其添加审核规则的 <see cref="T:System.Security.Principal.SecurityIdentifier" />。</param>
    /// <param name="accessMask">新审核规则的访问掩码。</param>
    /// <param name="inheritanceFlags">指定新审核规则的继承属性的标志。</param>
    /// <param name="propagationFlags">指定新审核规则的继承传播属性的标志。</param>
    public void AddAudit(AuditFlags auditFlags, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
    {
      this.CheckFlags(inheritanceFlags, propagationFlags);
      this.AddQualifiedAce(sid, AceQualifier.SystemAudit, accessMask, GenericAce.AceFlagsFromAuditFlags(auditFlags) | GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), ObjectAceFlags.None, Guid.Empty, Guid.Empty);
    }

    /// <summary>为指定的 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 对象设置指定的审核规则。</summary>
    /// <param name="auditFlags">要设置的审核条件。</param>
    /// <param name="sid">要为其设置审核规则的 <see cref="T:System.Security.Principal.SecurityIdentifier" />。</param>
    /// <param name="accessMask">新审核规则的访问掩码。</param>
    /// <param name="inheritanceFlags">指定新审核规则的继承属性的标志。</param>
    /// <param name="propagationFlags">指定新审核规则的继承传播属性的标志。</param>
    public void SetAudit(AuditFlags auditFlags, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
    {
      this.CheckFlags(inheritanceFlags, propagationFlags);
      this.SetQualifiedAce(sid, AceQualifier.SystemAudit, accessMask, GenericAce.AceFlagsFromAuditFlags(auditFlags) | GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), ObjectAceFlags.None, Guid.Empty, Guid.Empty);
    }

    /// <summary>从当前 <see cref="T:System.Security.AccessControl.SystemAcl" /> 对象移除指定的审核规则。</summary>
    /// <returns>如果此方法成功移除指定的审核规则，则为 true；否则为 false。</returns>
    /// <param name="auditFlags">要移除的审核规则的类型。</param>
    /// <param name="sid">要为其移除审核规则的 <see cref="T:System.Security.Principal.SecurityIdentifier" />。</param>
    /// <param name="accessMask">要移除的规则的访问掩码。</param>
    /// <param name="inheritanceFlags">指定要移除的规则的继承属性的标志。</param>
    /// <param name="propagationFlags">指定要移除的规则的继承传播属性的标志。</param>
    public bool RemoveAudit(AuditFlags auditFlags, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
    {
      return this.RemoveQualifiedAces(sid, AceQualifier.SystemAudit, accessMask, GenericAce.AceFlagsFromAuditFlags(auditFlags) | GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), true, ObjectAceFlags.None, Guid.Empty, Guid.Empty);
    }

    /// <summary>从当前 <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> 对象移除指定的审核规则。</summary>
    /// <param name="auditFlags">要移除的审核规则的类型。</param>
    /// <param name="sid">要为其移除审核规则的 <see cref="T:System.Security.Principal.SecurityIdentifier" />。</param>
    /// <param name="accessMask">要移除的规则的访问掩码。</param>
    /// <param name="inheritanceFlags">指定要移除的规则的继承属性的标志。</param>
    /// <param name="propagationFlags">指定要移除的规则的继承传播属性的标志。</param>
    public void RemoveAuditSpecific(AuditFlags auditFlags, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
    {
      this.RemoveQualifiedAcesSpecific(sid, AceQualifier.SystemAudit, accessMask, GenericAce.AceFlagsFromAuditFlags(auditFlags) | GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), ObjectAceFlags.None, Guid.Empty, Guid.Empty);
    }

    /// <summary>将一个审核规则添加到当前 <see cref="T:System.Security.AccessControl.SystemAcl" /> 对象。</summary>
    /// <param name="sid">要为其添加审核规则的 <see cref="T:System.Security.Principal.SecurityIdentifier" />。</param>
    /// <param name="rule">
    /// <see cref="T:System.Security.AccessControl.ObjectAuditRule" />新审核规则。</param>
    public void AddAudit(SecurityIdentifier sid, ObjectAuditRule rule)
    {
      this.AddAudit(rule.AuditFlags, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
    }

    /// <summary>将具有指定设置的审核规则添加到当前 <see cref="T:System.Security.AccessControl.SystemAcl" /> 对象。在指定新审核规则的对象类型或继承的对象类型时，为目录对象的访问控制列表 (ACL) 使用此方法。</summary>
    /// <param name="auditFlags">要添加的审核规则的类型。</param>
    /// <param name="sid">要为其添加审核规则的 <see cref="T:System.Security.Principal.SecurityIdentifier" />。</param>
    /// <param name="accessMask">新审核规则的访问掩码。</param>
    /// <param name="inheritanceFlags">指定新审核规则的继承属性的标志。</param>
    /// <param name="propagationFlags">指定新审核规则的继承传播属性的标志。</param>
    /// <param name="objectFlags">指定 <paramref name="objectType" /> 和 <paramref name="inheritedObjectType" /> 参数是否包含非 null 值的标志。</param>
    /// <param name="objectType">新审核规则所应用到的对象的类标识。</param>
    /// <param name="inheritedObjectType">可以继承新审核规则的子对象的类标识。</param>
    public void AddAudit(AuditFlags auditFlags, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
    {
      if (!this.IsDS)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_OnlyValidForDS"));
      this.CheckFlags(inheritanceFlags, propagationFlags);
      this.AddQualifiedAce(sid, AceQualifier.SystemAudit, accessMask, GenericAce.AceFlagsFromAuditFlags(auditFlags) | GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), objectFlags, objectType, inheritedObjectType);
    }

    /// <summary>为指定的 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 对象设置指定的审核规则。</summary>
    /// <param name="sid">要为其设置审核规则的 <see cref="T:System.Security.Principal.SecurityIdentifier" />。</param>
    /// <param name="rule">要为其设置审核规则的 <see cref="T:System.Security.AccessControl.ObjectAuditRule" />。</param>
    public void SetAudit(SecurityIdentifier sid, ObjectAuditRule rule)
    {
      this.SetAudit(rule.AuditFlags, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
    }

    /// <summary>为指定的 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 对象设置指定的审核规则。在指定对象类型或继承的对象类型时，为目录对象的访问控制列表 (ACL) 使用此方法。</summary>
    /// <param name="auditFlags">要设置的审核条件。</param>
    /// <param name="sid">要为其设置审核规则的 <see cref="T:System.Security.Principal.SecurityIdentifier" />。</param>
    /// <param name="accessMask">新审核规则的访问掩码。</param>
    /// <param name="inheritanceFlags">指定新审核规则的继承属性的标志。</param>
    /// <param name="propagationFlags">指定新审核规则的继承传播属性的标志。</param>
    /// <param name="objectFlags">指定 <paramref name="objectType" /> 和 <paramref name="inheritedObjectType" /> 参数是否包含非 null 值的标志。</param>
    /// <param name="objectType">新审核规则所应用到的对象的类标识。</param>
    /// <param name="inheritedObjectType">可以继承新审核规则的子对象的类标识。</param>
    public void SetAudit(AuditFlags auditFlags, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
    {
      if (!this.IsDS)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_OnlyValidForDS"));
      this.CheckFlags(inheritanceFlags, propagationFlags);
      this.SetQualifiedAce(sid, AceQualifier.SystemAudit, accessMask, GenericAce.AceFlagsFromAuditFlags(auditFlags) | GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), objectFlags, objectType, inheritedObjectType);
    }

    /// <summary>从当前 <see cref="T:System.Security.AccessControl.SystemAcl" /> 对象移除指定的审核规则。</summary>
    /// <returns>如果此方法成功移除指定的审核规则，则为 true；否则为 false。</returns>
    /// <param name="sid">要为其移除审核规则的 <see cref="T:System.Security.Principal.SecurityIdentifier" />。</param>
    /// <param name="rule">要为其移除审核规则的 <see cref="T:System.Security.AccessControl.ObjectAuditRule" />。</param>
    public bool RemoveAudit(SecurityIdentifier sid, ObjectAuditRule rule)
    {
      return this.RemoveAudit(rule.AuditFlags, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
    }

    /// <summary>从当前 <see cref="T:System.Security.AccessControl.SystemAcl" /> 对象移除指定的审核规则。在指定对象类型或继承的对象类型时，为目录对象的访问控制列表 (ACL) 使用此方法。</summary>
    /// <returns>如果此方法成功移除指定的审核规则，则为 true；否则为 false。</returns>
    /// <param name="auditFlags">要移除的审核规则的类型。</param>
    /// <param name="sid">要为其移除审核规则的 <see cref="T:System.Security.Principal.SecurityIdentifier" />。</param>
    /// <param name="accessMask">要移除的规则的访问掩码。</param>
    /// <param name="inheritanceFlags">指定要移除的规则的继承属性的标志。</param>
    /// <param name="propagationFlags">指定要移除的规则的继承传播属性的标志。</param>
    /// <param name="objectFlags">指定 <paramref name="objectType" /> 和 <paramref name="inheritedObjectType" /> 参数是否包含非 null 值的标志。</param>
    /// <param name="objectType">移除的审核控制规则所应用到的对象的类标识。</param>
    /// <param name="inheritedObjectType">可以继承移除的审核规则的子对象的类标识。</param>
    public bool RemoveAudit(AuditFlags auditFlags, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
    {
      if (!this.IsDS)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_OnlyValidForDS"));
      return this.RemoveQualifiedAces(sid, AceQualifier.SystemAudit, accessMask, GenericAce.AceFlagsFromAuditFlags(auditFlags) | GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), true, objectFlags, objectType, inheritedObjectType);
    }

    /// <summary>从当前 <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> 对象移除指定的审核规则。</summary>
    /// <param name="sid">要为其移除审核规则的 <see cref="T:System.Security.Principal.SecurityIdentifier" />。</param>
    /// <param name="rule">
    /// <see cref="T:System.Security.AccessControl.ObjectAuditRule" />要删除的规则。</param>
    public void RemoveAuditSpecific(SecurityIdentifier sid, ObjectAuditRule rule)
    {
      this.RemoveAuditSpecific(rule.AuditFlags, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
    }

    /// <summary>从当前 <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> 对象移除指定的审核规则。在指定对象类型或继承的对象类型时，为目录对象的访问控制列表 (ACL) 使用此方法。</summary>
    /// <param name="auditFlags">要移除的审核规则的类型。</param>
    /// <param name="sid">要为其移除审核规则的 <see cref="T:System.Security.Principal.SecurityIdentifier" />。</param>
    /// <param name="accessMask">要移除的规则的访问掩码。</param>
    /// <param name="inheritanceFlags">指定要移除的规则的继承属性的标志。</param>
    /// <param name="propagationFlags">指定要移除的规则的继承传播属性的标志。</param>
    /// <param name="objectFlags">指定 <paramref name="objectType" /> 和 <paramref name="inheritedObjectType" /> 参数是否包含非 null 值的标志。</param>
    /// <param name="objectType">移除的审核控制规则所应用到的对象的类标识。</param>
    /// <param name="inheritedObjectType">可以继承移除的审核规则的子对象的类标识。</param>
    public void RemoveAuditSpecific(AuditFlags auditFlags, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
    {
      if (!this.IsDS)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_OnlyValidForDS"));
      this.RemoveQualifiedAcesSpecific(sid, AceQualifier.SystemAudit, accessMask, GenericAce.AceFlagsFromAuditFlags(auditFlags) | GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), objectFlags, objectType, inheritedObjectType);
    }
  }
}
