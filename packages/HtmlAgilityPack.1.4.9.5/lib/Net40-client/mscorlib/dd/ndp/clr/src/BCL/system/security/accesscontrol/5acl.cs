// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.DiscretionaryAcl
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>表示自由访问控制列表 (DACL)。</summary>
  public sealed class DiscretionaryAcl : CommonAcl
  {
    private static SecurityIdentifier _sidEveryone = new SecurityIdentifier(WellKnownSidType.WorldSid, (SecurityIdentifier) null);
    private bool everyOneFullAccessForNullDacl;

    internal bool EveryOneFullAccessForNullDacl
    {
      get
      {
        return this.everyOneFullAccessForNullDacl;
      }
      set
      {
        this.everyOneFullAccessForNullDacl = value;
      }
    }

    /// <summary>用指定的值初始化 <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> 类的新实例。</summary>
    /// <param name="isContainer">true if the new <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> object is a container.</param>
    /// <param name="isDS">true if the new <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> object is a directory object Access Control List (ACL).</param>
    /// <param name="capacity">此 <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> 对象可包含的访问控制项 (ACE) 的数量。此数量只作为一种提示。</param>
    public DiscretionaryAcl(bool isContainer, bool isDS, int capacity)
    {
      int num1 = isContainer ? 1 : 0;
      int num2 = isDS ? 1 : 0;
      int num3 = num2 != 0 ? (int) GenericAcl.AclRevisionDS : (int) GenericAcl.AclRevision;
      int capacity1 = capacity;
      // ISSUE: explicit constructor call
      this.\u002Ector(num1 != 0, num2 != 0, (byte) num3, capacity1);
    }

    /// <summary>用指定的值初始化 <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> 类的新实例。</summary>
    /// <param name="isContainer">true if the new <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> object is a container.</param>
    /// <param name="isDS">true if the new <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> object is a directory object Access Control List (ACL).</param>
    /// <param name="revision">新的 <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> 对象的修订级别。</param>
    /// <param name="capacity">此 <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> 对象可包含的访问控制项 (ACE) 的数量。此数量只作为一种提示。</param>
    public DiscretionaryAcl(bool isContainer, bool isDS, byte revision, int capacity)
      : base(isContainer, isDS, revision, capacity)
    {
    }

    /// <summary>使用指定的 <see cref="T:System.Security.AccessControl.RawAcl" /> 对象中的指定值初始化 <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> 类的新实例。</summary>
    /// <param name="isContainer">true if the new <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> object is a container.</param>
    /// <param name="isDS">true if the new <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> object is a directory object Access Control List (ACL).</param>
    /// <param name="rawAcl">The underlying <see cref="T:System.Security.AccessControl.RawAcl" /> object for the new <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> object.指定 null 以创建空的 ACL。</param>
    public DiscretionaryAcl(bool isContainer, bool isDS, RawAcl rawAcl)
      : this(isContainer, isDS, rawAcl, false)
    {
    }

    internal DiscretionaryAcl(bool isContainer, bool isDS, RawAcl rawAcl, bool trusted)
      : base(isContainer, isDS, rawAcl == null ? new RawAcl(isDS ? GenericAcl.AclRevisionDS : GenericAcl.AclRevision, 0) : rawAcl, trusted, true)
    {
    }

    /// <summary>将具有指定设置的访问控制项 (ACE) 添加到当前 <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> 对象。</summary>
    /// <param name="accessType">要添加的访问控制类型（允许或拒绝）。</param>
    /// <param name="sid">要为其添加 ACE 的 <see cref="T:System.Security.Principal.SecurityIdentifier" />。</param>
    /// <param name="accessMask">新 ACE 的访问规则。</param>
    /// <param name="inheritanceFlags">指定新 ACE 的继承属性的标志。</param>
    /// <param name="propagationFlags">指定新 ACE 的继承传播属性的标志。</param>
    public void AddAccess(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
    {
      this.CheckAccessType(accessType);
      this.CheckFlags(inheritanceFlags, propagationFlags);
      this.everyOneFullAccessForNullDacl = false;
      this.AddQualifiedAce(sid, accessType == AccessControlType.Allow ? AceQualifier.AccessAllowed : AceQualifier.AccessDenied, accessMask, GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), ObjectAceFlags.None, Guid.Empty, Guid.Empty);
    }

    /// <summary>为指定的 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 对象设置指定的访问控制。</summary>
    /// <param name="accessType">要设置的访问控制类型（允许或拒绝）。</param>
    /// <param name="sid">要为其设置 ACE 的 <see cref="T:System.Security.Principal.SecurityIdentifier" />。</param>
    /// <param name="accessMask">新 ACE 的访问规则。</param>
    /// <param name="inheritanceFlags">指定新 ACE 的继承属性的标志。</param>
    /// <param name="propagationFlags">指定新 ACE 的继承传播属性的标志。</param>
    public void SetAccess(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
    {
      this.CheckAccessType(accessType);
      this.CheckFlags(inheritanceFlags, propagationFlags);
      this.everyOneFullAccessForNullDacl = false;
      this.SetQualifiedAce(sid, accessType == AccessControlType.Allow ? AceQualifier.AccessAllowed : AceQualifier.AccessDenied, accessMask, GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), ObjectAceFlags.None, Guid.Empty, Guid.Empty);
    }

    /// <summary>从当前 <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> 对象移除指定的访问控制规则。</summary>
    /// <returns>如果此方法成功移除指定的访问控制规则，则为 true；否则为 false。</returns>
    /// <param name="accessType">要移除的访问控制类型（允许或拒绝）。</param>
    /// <param name="sid">要移除其访问控制规则的 <see cref="T:System.Security.Principal.SecurityIdentifier" />。</param>
    /// <param name="accessMask">要移除的规则的访问掩码。</param>
    /// <param name="inheritanceFlags">指定要移除的规则的继承属性的标志。</param>
    /// <param name="propagationFlags">指定要移除的规则的继承传播属性的标志。</param>
    public bool RemoveAccess(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
    {
      this.CheckAccessType(accessType);
      this.everyOneFullAccessForNullDacl = false;
      return this.RemoveQualifiedAces(sid, accessType == AccessControlType.Allow ? AceQualifier.AccessAllowed : AceQualifier.AccessDenied, accessMask, GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), false, ObjectAceFlags.None, Guid.Empty, Guid.Empty);
    }

    /// <summary>从当前 <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> 对象移除指定的访问控制项 (ACE)。</summary>
    /// <param name="accessType">要移除的访问控制类型（允许或拒绝）。</param>
    /// <param name="sid">要为其移除 ACE 的 <see cref="T:System.Security.Principal.SecurityIdentifier" />。</param>
    /// <param name="accessMask">要移除的 ACE 的访问掩码。</param>
    /// <param name="inheritanceFlags">指定要移除的 ACE 的继承属性的标志。</param>
    /// <param name="propagationFlags">指定要移除的 ACE 的继承传播属性的标志。</param>
    public void RemoveAccessSpecific(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
    {
      this.CheckAccessType(accessType);
      this.everyOneFullAccessForNullDacl = false;
      this.RemoveQualifiedAcesSpecific(sid, accessType == AccessControlType.Allow ? AceQualifier.AccessAllowed : AceQualifier.AccessDenied, accessMask, GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), ObjectAceFlags.None, Guid.Empty, Guid.Empty);
    }

    /// <summary>将具有指定设置的访问控制项 (ACE) 添加到当前 <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> 对象。</summary>
    /// <param name="accessType">要添加的访问控制类型（允许或拒绝）。</param>
    /// <param name="sid">要为其添加 ACE 的 <see cref="T:System.Security.Principal.SecurityIdentifier" />。</param>
    /// <param name="rule">
    /// <see cref="T:System.Security.AccessControl.ObjectAccessRule" />为新的访问。</param>
    public void AddAccess(AccessControlType accessType, SecurityIdentifier sid, ObjectAccessRule rule)
    {
      this.AddAccess(accessType, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
    }

    /// <summary>将具有指定设置的访问控制项 (ACE) 添加到当前 <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> 对象。在指定新 ACE 的对象类型或继承的对象类型时，为目录对象的访问控制列表 (ACL) 使用此方法。</summary>
    /// <param name="accessType">要添加的访问控制类型（允许或拒绝）。</param>
    /// <param name="sid">要为其添加 ACE 的 <see cref="T:System.Security.Principal.SecurityIdentifier" />。</param>
    /// <param name="accessMask">新 ACE 的访问规则。</param>
    /// <param name="inheritanceFlags">指定新 ACE 的继承属性的标志。</param>
    /// <param name="propagationFlags">指定新 ACE 的继承传播属性的标志。</param>
    /// <param name="objectFlags">指定 <paramref name="objectType" /> 和 <paramref name="inheritedObjectType" /> 参数是否包含非 null 值的标志。</param>
    /// <param name="objectType">新 ACE 所应用到的对象的类标识。</param>
    /// <param name="inheritedObjectType">可以继承新 ACE 的子对象的类标识。</param>
    public void AddAccess(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
    {
      if (!this.IsDS)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_OnlyValidForDS"));
      this.CheckAccessType(accessType);
      this.CheckFlags(inheritanceFlags, propagationFlags);
      this.everyOneFullAccessForNullDacl = false;
      this.AddQualifiedAce(sid, accessType == AccessControlType.Allow ? AceQualifier.AccessAllowed : AceQualifier.AccessDenied, accessMask, GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), objectFlags, objectType, inheritedObjectType);
    }

    /// <summary>为指定的 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 对象设置指定的访问控制。</summary>
    /// <param name="accessType">要设置的访问控制类型（允许或拒绝）。</param>
    /// <param name="sid">要为其设置 ACE 的 <see cref="T:System.Security.Principal.SecurityIdentifier" />。</param>
    /// <param name="rule">
    /// <see cref="T:System.Security.AccessControl.ObjectAccessRule" />为其设置访问权限。</param>
    public void SetAccess(AccessControlType accessType, SecurityIdentifier sid, ObjectAccessRule rule)
    {
      this.SetAccess(accessType, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
    }

    /// <summary>为指定的 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 对象设置指定的访问控制。</summary>
    /// <param name="accessType">要设置的访问控制类型（允许或拒绝）。</param>
    /// <param name="sid">要为其设置 ACE 的 <see cref="T:System.Security.Principal.SecurityIdentifier" />。</param>
    /// <param name="accessMask">新 ACE 的访问规则。</param>
    /// <param name="inheritanceFlags">指定新 ACE 的继承属性的标志。</param>
    /// <param name="propagationFlags">指定新 ACE 的继承传播属性的标志。</param>
    /// <param name="objectFlags">指定 <paramref name="objectType" /> 和 <paramref name="inheritedObjectType" /> 参数是否包含非 null 值的标志。</param>
    /// <param name="objectType">新 ACE 所应用到的对象的类标识。</param>
    /// <param name="inheritedObjectType">可以继承新 ACE 的子对象的类标识。</param>
    public void SetAccess(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
    {
      if (!this.IsDS)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_OnlyValidForDS"));
      this.CheckAccessType(accessType);
      this.CheckFlags(inheritanceFlags, propagationFlags);
      this.everyOneFullAccessForNullDacl = false;
      this.SetQualifiedAce(sid, accessType == AccessControlType.Allow ? AceQualifier.AccessAllowed : AceQualifier.AccessDenied, accessMask, GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), objectFlags, objectType, inheritedObjectType);
    }

    /// <summary>从当前 <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> 对象移除指定的访问控制规则。</summary>
    /// <returns>返回 <see cref="T:System.Boolean" />。</returns>
    /// <param name="accessType">要移除的访问控制类型（允许或拒绝）。</param>
    /// <param name="sid">要移除其访问控制规则的 <see cref="T:System.Security.Principal.SecurityIdentifier" />。</param>
    /// <param name="rule">
    /// <see cref="T:System.Security.AccessControl.ObjectAccessRule" />为其删除的访问权限。</param>
    public bool RemoveAccess(AccessControlType accessType, SecurityIdentifier sid, ObjectAccessRule rule)
    {
      return this.RemoveAccess(accessType, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
    }

    /// <summary>从当前 <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> 对象移除指定的访问控制规则。在指定对象类型或继承的对象类型时，为目录对象的访问控制列表 (ACL) 使用此方法。</summary>
    /// <returns>如果此方法成功移除指定的访问控制规则，则为 true；否则为 false。</returns>
    /// <param name="accessType">要移除的访问控制类型（允许或拒绝）。</param>
    /// <param name="sid">要移除其访问控制规则的 <see cref="T:System.Security.Principal.SecurityIdentifier" />。</param>
    /// <param name="accessMask">要移除的访问控制规则的访问掩码。</param>
    /// <param name="inheritanceFlags">指定要移除的访问控制规则的继承属性的标志。</param>
    /// <param name="propagationFlags">指定要移除的访问控制规则的继承传播属性的标志。</param>
    /// <param name="objectFlags">指定 <paramref name="objectType" /> 和 <paramref name="inheritedObjectType" /> 参数是否包含非 null 值的标志。</param>
    /// <param name="objectType">移除的访问控制规则所应用到的对象的类标识。</param>
    /// <param name="inheritedObjectType">可以继承移除的访问控制规则的子对象的类标识。</param>
    public bool RemoveAccess(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
    {
      if (!this.IsDS)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_OnlyValidForDS"));
      this.CheckAccessType(accessType);
      this.everyOneFullAccessForNullDacl = false;
      return this.RemoveQualifiedAces(sid, accessType == AccessControlType.Allow ? AceQualifier.AccessAllowed : AceQualifier.AccessDenied, accessMask, GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), false, objectFlags, objectType, inheritedObjectType);
    }

    /// <summary>从当前 <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> 对象移除指定的访问控制项 (ACE)。</summary>
    /// <param name="accessType">要移除的访问控制类型（允许或拒绝）。</param>
    /// <param name="sid">要为其移除 ACE 的 <see cref="T:System.Security.Principal.SecurityIdentifier" />。</param>
    /// <param name="rule">
    /// <see cref="T:System.Security.AccessControl.ObjectAccessRule" />为其删除的访问权限。</param>
    public void RemoveAccessSpecific(AccessControlType accessType, SecurityIdentifier sid, ObjectAccessRule rule)
    {
      this.RemoveAccessSpecific(accessType, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
    }

    /// <summary>从当前 <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> 对象移除指定的访问控制项 (ACE)。在指定要移除的 ACE 的对象类型或继承的对象类型时，为目录对象的访问控制列表 (ACL) 使用此方法。</summary>
    /// <param name="accessType">要移除的访问控制类型（允许或拒绝）。</param>
    /// <param name="sid">要为其移除 ACE 的 <see cref="T:System.Security.Principal.SecurityIdentifier" />。</param>
    /// <param name="accessMask">要移除的 ACE 的访问掩码。</param>
    /// <param name="inheritanceFlags">指定要移除的 ACE 的继承属性的标志。</param>
    /// <param name="propagationFlags">指定要移除的 ACE 的继承传播属性的标志。</param>
    /// <param name="objectFlags">指定 <paramref name="objectType" /> 和 <paramref name="inheritedObjectType" /> 参数是否包含非 null 值的标志。</param>
    /// <param name="objectType">移除的 ACE 所应用到的对象的类标识。</param>
    /// <param name="inheritedObjectType">可以继承移除的 ACE 的子对象的类标识。</param>
    public void RemoveAccessSpecific(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
    {
      if (!this.IsDS)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_OnlyValidForDS"));
      this.CheckAccessType(accessType);
      this.everyOneFullAccessForNullDacl = false;
      this.RemoveQualifiedAcesSpecific(sid, accessType == AccessControlType.Allow ? AceQualifier.AccessAllowed : AceQualifier.AccessDenied, accessMask, GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), objectFlags, objectType, inheritedObjectType);
    }

    internal override void OnAclModificationTried()
    {
      this.everyOneFullAccessForNullDacl = false;
    }

    internal static DiscretionaryAcl CreateAllowEveryoneFullAccess(bool isDS, bool isContainer)
    {
      DiscretionaryAcl discretionaryAcl = new DiscretionaryAcl(isContainer, isDS, 1);
      int num1 = 0;
      SecurityIdentifier sid = DiscretionaryAcl._sidEveryone;
      int accessMask = -1;
      int num2 = isContainer ? 3 : 0;
      int num3 = 0;
      discretionaryAcl.AddAccess((AccessControlType) num1, sid, accessMask, (InheritanceFlags) num2, (PropagationFlags) num3);
      int num4 = 1;
      discretionaryAcl.everyOneFullAccessForNullDacl = num4 != 0;
      return discretionaryAcl;
    }
  }
}
