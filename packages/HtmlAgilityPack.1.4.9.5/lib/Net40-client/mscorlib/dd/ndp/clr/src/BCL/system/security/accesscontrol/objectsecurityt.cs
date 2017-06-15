// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.ObjectSecurity`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>提供在不直接操作访问控制列表 (ACL) 的情况下控制对对象的访问权限的功能；还提供对访问权限进行类型转换的功能。</summary>
  /// <typeparam name="T">对象的访问权限。</typeparam>
  public abstract class ObjectSecurity<T> : NativeObjectSecurity where T : struct
  {
    /// <summary>获取与此 ObjectSecurity`1 对象关联的可保护对象的类型。</summary>
    /// <returns>与当前实例关联的可保护对象的类型。</returns>
    public override Type AccessRightType
    {
      get
      {
        return typeof (T);
      }
    }

    /// <summary>获取与此 ObjectSecurity`1 对象的访问规则关联的对象的类型。</summary>
    /// <returns>与当前实例的访问规则关联的对象的类型。</returns>
    public override Type AccessRuleType
    {
      get
      {
        return typeof (AccessRule<T>);
      }
    }

    /// <summary>获取与此 ObjectSecurity`1 对象的审核规则关联的类型对象。</summary>
    /// <returns>与当前实例的审核规则关联的类型对象。</returns>
    public override Type AuditRuleType
    {
      get
      {
        return typeof (AuditRule<T>);
      }
    }

    /// <summary>初始化 ObjectSecurity`1 类的新实例。</summary>
    /// <param name="isContainer">如果新 <see cref="T:System.Security.AccessControl.ObjectSecurity`1" /> 对象是一个容器对象，则为 true。</param>
    /// <param name="resourceType">资源的类型。</param>
    protected ObjectSecurity(bool isContainer, ResourceType resourceType)
      : base(isContainer, resourceType, (NativeObjectSecurity.ExceptionFromErrorCode) null, (object) null)
    {
    }

    /// <summary>初始化 ObjectSecurity`1 类的新实例。</summary>
    /// <param name="isContainer">如果新 <see cref="T:System.Security.AccessControl.ObjectSecurity`1" /> 对象是一个容器对象，则为 true。</param>
    /// <param name="resourceType">资源的类型。</param>
    /// <param name="name">新的 <see cref="T:System.Security.AccessControl.ObjectSecurity`1" /> 对象与其相关联的可保护对象的名称。</param>
    /// <param name="includeSections">要包含的部分。</param>
    protected ObjectSecurity(bool isContainer, ResourceType resourceType, string name, AccessControlSections includeSections)
      : base(isContainer, resourceType, name, includeSections, (NativeObjectSecurity.ExceptionFromErrorCode) null, (object) null)
    {
    }

    /// <summary>初始化 ObjectSecurity`1 类的新实例。</summary>
    /// <param name="isContainer">如果新 <see cref="T:System.Security.AccessControl.ObjectSecurity`1" /> 对象是一个容器对象，则为 true。</param>
    /// <param name="resourceType">资源的类型。</param>
    /// <param name="name">新的 <see cref="T:System.Security.AccessControl.ObjectSecurity`1" /> 对象与其相关联的可保护对象的名称。</param>
    /// <param name="includeSections">要包含的部分。</param>
    /// <param name="exceptionFromErrorCode">由提供自定义异常的集成器实现的委托。</param>
    /// <param name="exceptionContext">包含有关异常的源或目标的上下文信息的对象。</param>
    protected ObjectSecurity(bool isContainer, ResourceType resourceType, string name, AccessControlSections includeSections, NativeObjectSecurity.ExceptionFromErrorCode exceptionFromErrorCode, object exceptionContext)
      : base(isContainer, resourceType, name, includeSections, exceptionFromErrorCode, exceptionContext)
    {
    }

    /// <summary>初始化 ObjectSecurity`1 类的新实例。</summary>
    /// <param name="isContainer">如果新 <see cref="T:System.Security.AccessControl.ObjectSecurity`1" /> 对象是一个容器对象，则为 true。</param>
    /// <param name="resourceType">资源的类型。</param>
    /// <param name="safeHandle">句柄。</param>
    /// <param name="includeSections">要包含的部分。</param>
    [SecuritySafeCritical]
    protected ObjectSecurity(bool isContainer, ResourceType resourceType, SafeHandle safeHandle, AccessControlSections includeSections)
      : base(isContainer, resourceType, safeHandle, includeSections, (NativeObjectSecurity.ExceptionFromErrorCode) null, (object) null)
    {
    }

    /// <summary>初始化 ObjectSecurity`1 类的新实例。</summary>
    /// <param name="isContainer">如果新 <see cref="T:System.Security.AccessControl.ObjectSecurity`1" /> 对象是一个容器对象，则为 true。</param>
    /// <param name="resourceType">资源的类型。</param>
    /// <param name="safeHandle">句柄。</param>
    /// <param name="includeSections">要包含的部分。</param>
    /// <param name="exceptionFromErrorCode">由提供自定义异常的集成器实现的委托。</param>
    /// <param name="exceptionContext">包含有关异常的源或目标的上下文信息的对象。</param>
    [SecuritySafeCritical]
    protected ObjectSecurity(bool isContainer, ResourceType resourceType, SafeHandle safeHandle, AccessControlSections includeSections, NativeObjectSecurity.ExceptionFromErrorCode exceptionFromErrorCode, object exceptionContext)
      : base(isContainer, resourceType, safeHandle, includeSections, exceptionFromErrorCode, exceptionContext)
    {
    }

    /// <summary>初始化表示相关安全对象的新访问控制规则 ObjectAccessRule 类的新实例。</summary>
    /// <returns>使用指定的访问权限、访问控制和标志为指定用户表示新的访问控制规则。</returns>
    /// <param name="identityReference">表示用户帐户。</param>
    /// <param name="accessMask">访问类型。</param>
    /// <param name="isInherited">如果该访问规则是继承的，则为 true；否则为 false。</param>
    /// <param name="inheritanceFlags">指定将访问掩码传播到子对象的方法。</param>
    /// <param name="propagationFlags">指定如何将访问控制项 (ACE) 传播到子对象。</param>
    /// <param name="type">指定是允许还是拒绝访问。</param>
    public override AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
    {
      return (AccessRule) new AccessRule<T>(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, type);
    }

    /// <summary>初始化 <see cref="T:System.Security.AccessControl.AuditRule" /> 类的新实例，它表示指定用户的指定审核规则。</summary>
    /// <returns>返回指定用户的指定审核规则。</returns>
    /// <param name="identityReference">表示用户帐户。</param>
    /// <param name="accessMask">指定访问类型的整数。</param>
    /// <param name="isInherited">如果该访问规则是继承的，则为 true；否则为 false。</param>
    /// <param name="inheritanceFlags">指定将访问掩码传播到子对象的方法。</param>
    /// <param name="propagationFlags">指定如何将访问控制项 (ACE) 传播到子对象。</param>
    /// <param name="flags">描述要执行的审核类型。</param>
    public override AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
    {
      return (AuditRule) new AuditRule<T>(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, flags);
    }

    private AccessControlSections GetAccessControlSectionsFromChanges()
    {
      AccessControlSections accessControlSections = AccessControlSections.None;
      if (this.AccessRulesModified)
        accessControlSections = AccessControlSections.Access;
      if (this.AuditRulesModified)
        accessControlSections |= AccessControlSections.Audit;
      if (this.OwnerModified)
        accessControlSections |= AccessControlSections.Owner;
      if (this.GroupModified)
        accessControlSections |= AccessControlSections.Group;
      return accessControlSections;
    }

    /// <summary>使用指定句柄将与此 ObjectSecurity`1 对象关联的安全描述符保存到永久性存储。</summary>
    /// <param name="handle">与此 ObjectSecurity`1 对象关联的可保护对象的句柄。</param>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
    protected internal void Persist(SafeHandle handle)
    {
      this.WriteLock();
      try
      {
        AccessControlSections sectionsFromChanges = this.GetAccessControlSectionsFromChanges();
        this.Persist(handle, sectionsFromChanges);
        this.OwnerModified = this.GroupModified = this.AuditRulesModified = this.AccessRulesModified = false;
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>使用指定名称将与此 ObjectSecurity`1 对象关联的安全描述符保存到永久性存储。</summary>
    /// <param name="name">与此 ObjectSecurity`1 对象关联的可保护对象的名称。</param>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
    protected internal void Persist(string name)
    {
      this.WriteLock();
      try
      {
        AccessControlSections sectionsFromChanges = this.GetAccessControlSectionsFromChanges();
        this.Persist(name, sectionsFromChanges);
        this.OwnerModified = this.GroupModified = this.AuditRulesModified = this.AccessRulesModified = false;
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>将指定的访问规则添加到与此 ObjectSecurity`1 对象关联的自由访问控制列表 (DACL)。</summary>
    /// <param name="rule">要添加的规则。</param>
    public virtual void AddAccessRule(AccessRule<T> rule)
    {
      this.AddAccessRule((AccessRule) rule);
    }

    /// <summary>从与此 ObjectSecurity`1 对象关联的自由访问控制列表 (DACL) 中移除与指定的访问规则具有相同安全标识符和限定符的所有访问规则，然后添加指定的访问规则。</summary>
    /// <param name="rule">要设置的访问规则。</param>
    public virtual void SetAccessRule(AccessRule<T> rule)
    {
      this.SetAccessRule((AccessRule) rule);
    }

    /// <summary>移除与此 ObjectSecurity`1 对象关联的自由访问控制列表 (DACL) 中的所有访问规则，然后添加指定的访问规则。</summary>
    /// <param name="rule">要重置的访问规则。</param>
    public virtual void ResetAccessRule(AccessRule<T> rule)
    {
      this.ResetAccessRule((AccessRule) rule);
    }

    /// <summary>从与此 ObjectSecurity`1 对象关联的自由访问控制列表 (DACL) 中移除与指定的访问规则具有相同安全标识符和访问掩码的访问规则。</summary>
    /// <returns>如果访问规则已成功移除，则返回 true；否则返回 false。</returns>
    /// <param name="rule">要移除的规则。</param>
    public virtual bool RemoveAccessRule(AccessRule<T> rule)
    {
      return this.RemoveAccessRule((AccessRule) rule);
    }

    /// <summary>从与此 ObjectSecurity`1 对象关联的自由访问控制列表 (DACL) 中移除与指定的访问规则具有相同安全标识符的所有访问规则。</summary>
    /// <param name="rule">要移除的访问规则。</param>
    public virtual void RemoveAccessRuleAll(AccessRule<T> rule)
    {
      this.RemoveAccessRuleAll((AccessRule) rule);
    }

    /// <summary>从与此 ObjectSecurity`1 对象关联的自由访问控制列表 (DACL) 中移除与指定的访问规则完全匹配的所有访问规则</summary>
    /// <param name="rule">要移除的访问规则。</param>
    public virtual void RemoveAccessRuleSpecific(AccessRule<T> rule)
    {
      this.RemoveAccessRuleSpecific((AccessRule) rule);
    }

    /// <summary>将指定的审核规则添加到与此 ObjectSecurity`1 对象关联的系统访问控制列表 (SACL)。</summary>
    /// <param name="rule">要添加的审核规则。</param>
    public virtual void AddAuditRule(AuditRule<T> rule)
    {
      this.AddAuditRule((AuditRule) rule);
    }

    /// <summary>从与此 ObjectSecurity`1 对象关联的系统访问控制列表 (SACL) 中移除与指定的审核规则具有相同安全标识符和限定符的所有审核规则，然后添加指定的审核规则。</summary>
    /// <param name="rule">要设置的审核规则。</param>
    public virtual void SetAuditRule(AuditRule<T> rule)
    {
      this.SetAuditRule((AuditRule) rule);
    }

    /// <summary>从与此 ObjectSecurity`1 对象关联的系统访问控制列表 (SACL) 中移除与指定的审核规则具有相同安全标识符和访问掩码的审核规则。</summary>
    /// <returns>如果对象已移除，则返回 true；否则返回 false。</returns>
    /// <param name="rule">要移除的审核规则</param>
    public virtual bool RemoveAuditRule(AuditRule<T> rule)
    {
      return this.RemoveAuditRule((AuditRule) rule);
    }

    /// <summary>从与此 ObjectSecurity`1 对象关联的系统访问控制列表 (SACL) 中移除与指定的审核规则具有相同安全标识符的所有审核规则。</summary>
    /// <param name="rule">要移除的审核规则。</param>
    public virtual void RemoveAuditRuleAll(AuditRule<T> rule)
    {
      this.RemoveAuditRuleAll((AuditRule) rule);
    }

    /// <summary>从与此 ObjectSecurity`1 对象关联的系统访问控制列表 (SACL) 中移除与指定的审核规则完全匹配的所有审核规则</summary>
    /// <param name="rule">要移除的审核规则。</param>
    public virtual void RemoveAuditRuleSpecific(AuditRule<T> rule)
    {
      this.RemoveAuditRuleSpecific((AuditRule) rule);
    }
  }
}
