// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.CryptoKeySecurity
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>提供无需直接操作访问控制列表 (ACL) 而控制对加密密钥对象的访问的能力。</summary>
  public sealed class CryptoKeySecurity : NativeObjectSecurity
  {
    private const ResourceType s_ResourceType = ResourceType.FileObject;

    /// <summary>获取与此 <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> 对象关联的可保护对象的 <see cref="T:System.Type" />。</summary>
    /// <returns>与此 <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> 对象关联的可保护对象的类型。</returns>
    public override Type AccessRightType
    {
      get
      {
        return typeof (CryptoKeyRights);
      }
    }

    /// <summary>获取与此 <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> 对象的访问规则关联的对象的 <see cref="T:System.Type" />。<see cref="T:System.Type" /> 对象必须是可以强制转换为 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 对象的对象。</summary>
    /// <returns>与此 <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> 对象的访问规则关联的对象的类型。</returns>
    public override Type AccessRuleType
    {
      get
      {
        return typeof (CryptoKeyAccessRule);
      }
    }

    /// <summary>获取与此 <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> 对象的审核规则关联的 <see cref="T:System.Type" /> 对象。<see cref="T:System.Type" /> 对象必须是可以强制转换为 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 对象的对象。</summary>
    /// <returns>与此 <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> 对象的审核规则关联的对象的类型。</returns>
    public override Type AuditRuleType
    {
      get
      {
        return typeof (CryptoKeyAuditRule);
      }
    }

    internal AccessControlSections ChangedAccessControlSections
    {
      [SecurityCritical] get
      {
        AccessControlSections accessControlSections = AccessControlSections.None;
        bool flag = false;
        RuntimeHelpers.PrepareConstrainedRegions();
        try
        {
          RuntimeHelpers.PrepareConstrainedRegions();
          try
          {
          }
          finally
          {
            this.ReadLock();
            flag = true;
          }
          if (this.AccessRulesModified)
            accessControlSections |= AccessControlSections.Access;
          if (this.AuditRulesModified)
            accessControlSections |= AccessControlSections.Audit;
          if (this.GroupModified)
            accessControlSections |= AccessControlSections.Group;
          if (this.OwnerModified)
            accessControlSections |= AccessControlSections.Owner;
        }
        finally
        {
          if (flag)
            this.ReadUnlock();
        }
        return accessControlSections;
      }
    }

    /// <summary>初始化 <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> 类的新实例。</summary>
    public CryptoKeySecurity()
      : base(false, ResourceType.FileObject)
    {
    }

    /// <summary>使用指定的安全性说明符初始化 <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> 类的新实例。</summary>
    /// <param name="securityDescriptor">用于创建新 <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> 对象的安全性说明符。</param>
    [SecuritySafeCritical]
    public CryptoKeySecurity(CommonSecurityDescriptor securityDescriptor)
      : base(ResourceType.FileObject, securityDescriptor)
    {
    }

    /// <summary>用指定的值初始化 <see cref="T:System.Security.AccessControl.AccessRule" /> 类的新实例。</summary>
    /// <returns>此方法所创建的 <see cref="T:System.Security.AccessControl.AccessRule" /> 对象。</returns>
    /// <param name="identityReference">应用访问规则的标识。它必须是可强制转换为 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 的对象。</param>
    /// <param name="accessMask">此规则的访问掩码。访问掩码是一个 32 位的匿名位集合，其含义是由每个集成器定义的。</param>
    /// <param name="isInherited">如果此规则继承自父容器，则为 true。</param>
    /// <param name="inheritanceFlags">指定访问规则的继承属性。</param>
    /// <param name="propagationFlags">指定继承的访问规则是否自动传播。如果 <paramref name="inheritanceFlags" /> 设置为 <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />，则将忽略传播标志。</param>
    /// <param name="type">指定有效的访问控制类型。</param>
    public override sealed AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
    {
      return (AccessRule) new CryptoKeyAccessRule(identityReference, CryptoKeyAccessRule.RightsFromAccessMask(accessMask), type);
    }

    /// <summary>用指定的值初始化 <see cref="T:System.Security.AccessControl.AuditRule" /> 类的新实例。</summary>
    /// <returns>此方法所创建的 <see cref="T:System.Security.AccessControl.AuditRule" /> 对象。</returns>
    /// <param name="identityReference">审核规则应用到的标识。它必须是可强制转换为 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 的对象。</param>
    /// <param name="accessMask">此规则的访问掩码。访问掩码是一个 32 位的匿名位集合，其含义是由每个集成器定义的。</param>
    /// <param name="isInherited">如果此规则继承自父容器，则为 true。</param>
    /// <param name="inheritanceFlags">指定审核规则的继承属性。</param>
    /// <param name="propagationFlags">指定继承的审核规则是否自动传播。如果 <paramref name="inheritanceFlags" /> 设置为 <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />，则将忽略传播标志。</param>
    /// <param name="flags">指定对规则进行审核的条件。</param>
    public override sealed AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
    {
      return (AuditRule) new CryptoKeyAuditRule(identityReference, CryptoKeyAuditRule.RightsFromAccessMask(accessMask), flags);
    }

    /// <summary>将指定的访问规则添加到与此 <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> 对象关联的自由访问控制列表 (DACL)。</summary>
    /// <param name="rule">要添加的访问规则。</param>
    public void AddAccessRule(CryptoKeyAccessRule rule)
    {
      this.AddAccessRule((AccessRule) rule);
    }

    /// <summary>从与此 <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> 对象关联的自由访问控制列表 (DACL) 中移除与指定的访问规则具有相同安全性标识符和限定符的所有访问规则，然后添加指定的访问规则。</summary>
    /// <param name="rule">要设置的访问规则。</param>
    public void SetAccessRule(CryptoKeyAccessRule rule)
    {
      this.SetAccessRule((AccessRule) rule);
    }

    /// <summary>从与此 <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> 对象关联的自由访问控制列表 (DACL) 中移除所有访问规则，然后添加指定的访问规则。</summary>
    /// <param name="rule">要重置的访问规则。</param>
    public void ResetAccessRule(CryptoKeyAccessRule rule)
    {
      this.ResetAccessRule((AccessRule) rule);
    }

    /// <summary>从与此 <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> 对象关联的自由访问控制列表 (DACL) 中移除与指定的访问规则具有相同安全性标识符和访问掩码的访问规则。</summary>
    /// <returns>如果访问规则已成功移除，则为 true；否则为 false。</returns>
    /// <param name="rule">要移除的访问规则。</param>
    public bool RemoveAccessRule(CryptoKeyAccessRule rule)
    {
      return this.RemoveAccessRule((AccessRule) rule);
    }

    /// <summary>从与此 <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> 对象关联的自由访问控制列表 (DACL) 中移除与指定的访问规则具有相同安全性标识符的所有访问规则。</summary>
    /// <param name="rule">要移除的访问规则。</param>
    public void RemoveAccessRuleAll(CryptoKeyAccessRule rule)
    {
      this.RemoveAccessRuleAll((AccessRule) rule);
    }

    /// <summary>从与此 <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> 对象关联的自由访问控制列表 (DACL) 中移除与指定的访问规则完全匹配的所有访问规则。</summary>
    /// <param name="rule">要移除的访问规则。</param>
    public void RemoveAccessRuleSpecific(CryptoKeyAccessRule rule)
    {
      this.RemoveAccessRuleSpecific((AccessRule) rule);
    }

    /// <summary>将指定的审核规则添加到与该 <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> 对象关联的系统访问控制列表 (SACL)。</summary>
    /// <param name="rule">要添加的审核规则。</param>
    public void AddAuditRule(CryptoKeyAuditRule rule)
    {
      this.AddAuditRule((AuditRule) rule);
    }

    /// <summary>从与此 <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> 对象关联的系统访问控制列表 (SACL) 中移除与指定的审核规则具有相同的安全性标识符和限定符所有审核规则，然后添加指定的审核规则。</summary>
    /// <param name="rule">要设置的审核规则。</param>
    public void SetAuditRule(CryptoKeyAuditRule rule)
    {
      this.SetAuditRule((AuditRule) rule);
    }

    /// <summary>从与此 <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> 对象关联的系统访问控制列表 (SACL) 中移除与指定的审核规则具有相同安全性标识符和访问掩码的审核规则。</summary>
    /// <returns>如果审核规则已成功移除，则为 true；否则为 false。</returns>
    /// <param name="rule">要移除的审核规则。</param>
    public bool RemoveAuditRule(CryptoKeyAuditRule rule)
    {
      return this.RemoveAuditRule((AuditRule) rule);
    }

    /// <summary>从与此 <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> 对象关联的系统访问控制列表 (SACL) 中移除与指定的审核规则具有相同安全性标识符的所有审核规则。</summary>
    /// <param name="rule">要移除的审核规则。</param>
    public void RemoveAuditRuleAll(CryptoKeyAuditRule rule)
    {
      this.RemoveAuditRuleAll((AuditRule) rule);
    }

    /// <summary>从与此 <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> 对象关联的系统访问控制列表 (SACL) 中移除与指定的审核规则完全匹配的所有审核规则。</summary>
    /// <param name="rule">要移除的审核规则。</param>
    public void RemoveAuditRuleSpecific(CryptoKeyAuditRule rule)
    {
      this.RemoveAuditRuleSpecific((AuditRule) rule);
    }
  }
}
