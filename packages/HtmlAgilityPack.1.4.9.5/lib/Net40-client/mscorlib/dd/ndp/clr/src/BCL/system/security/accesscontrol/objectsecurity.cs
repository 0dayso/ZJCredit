// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.ObjectSecurity
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;

namespace System.Security.AccessControl
{
  /// <summary>提供在无需直接操作访问控制列表 (ACL) 的情况下控制对象访问的能力。此类为 <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> 和 <see cref="T:System.Security.AccessControl.DirectoryObjectSecurity" /> 类的抽象基类。</summary>
  public abstract class ObjectSecurity
  {
    private static readonly ControlFlags SACL_CONTROL_FLAGS = ControlFlags.SystemAclPresent | ControlFlags.SystemAclAutoInherited | ControlFlags.SystemAclProtected;
    private static readonly ControlFlags DACL_CONTROL_FLAGS = ControlFlags.DiscretionaryAclPresent | ControlFlags.DiscretionaryAclAutoInherited | ControlFlags.DiscretionaryAclProtected;
    private readonly ReaderWriterLock _lock = new ReaderWriterLock();
    internal CommonSecurityDescriptor _securityDescriptor;
    private bool _ownerModified;
    private bool _groupModified;
    private bool _saclModified;
    private bool _daclModified;

    /// <summary>获取或设置一个布尔值，该值指定可保护对象的所有者是否已被修改。</summary>
    /// <returns>如果可保护对象的所有者已被修改，则为 true；否则为 false。</returns>
    protected bool OwnerModified
    {
      get
      {
        if (!this._lock.IsReaderLockHeld && !this._lock.IsWriterLockHeld)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MustLockForReadOrWrite"));
        return this._ownerModified;
      }
      set
      {
        if (!this._lock.IsWriterLockHeld)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MustLockForWrite"));
        this._ownerModified = value;
      }
    }

    /// <summary>获取或设置一个布尔值，该值指定与可保护对象关联的组是否已被修改。 </summary>
    /// <returns>如果与可保护对象关联的组已被修改，则为 true；否则为 false。</returns>
    protected bool GroupModified
    {
      get
      {
        if (!this._lock.IsReaderLockHeld && !this._lock.IsWriterLockHeld)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MustLockForReadOrWrite"));
        return this._groupModified;
      }
      set
      {
        if (!this._lock.IsWriterLockHeld)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MustLockForWrite"));
        this._groupModified = value;
      }
    }

    /// <summary>获取或设置一个布尔值，该值指定与此 <see cref="T:System.Security.AccessControl.ObjectSecurity" /> 对象关联的审核规则是否已被修改。</summary>
    /// <returns>如果与此 <see cref="T:System.Security.AccessControl.ObjectSecurity" /> 对象关联的审核规则已被修改，则为 true；否则为 false。</returns>
    protected bool AuditRulesModified
    {
      get
      {
        if (!this._lock.IsReaderLockHeld && !this._lock.IsWriterLockHeld)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MustLockForReadOrWrite"));
        return this._saclModified;
      }
      set
      {
        if (!this._lock.IsWriterLockHeld)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MustLockForWrite"));
        this._saclModified = value;
      }
    }

    /// <summary>获取或设置一个布尔值，该值指定与此 <see cref="T:System.Security.AccessControl.ObjectSecurity" /> 对象关联的访问规则是否已被修改。</summary>
    /// <returns>如果与此 <see cref="T:System.Security.AccessControl.ObjectSecurity" /> 对象关联的访问规则已被修改，则为 true；否则为 false。</returns>
    protected bool AccessRulesModified
    {
      get
      {
        if (!this._lock.IsReaderLockHeld && !this._lock.IsWriterLockHeld)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MustLockForReadOrWrite"));
        return this._daclModified;
      }
      set
      {
        if (!this._lock.IsWriterLockHeld)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MustLockForWrite"));
        this._daclModified = value;
      }
    }

    /// <summary>获取一个布尔值，该值指定此 <see cref="T:System.Security.AccessControl.ObjectSecurity" /> 对象是否是一个容器对象。</summary>
    /// <returns>如果 <see cref="T:System.Security.AccessControl.ObjectSecurity" /> 对象是一个容器对象，则为 true；否则为 false。</returns>
    protected bool IsContainer
    {
      get
      {
        return this._securityDescriptor.IsContainer;
      }
    }

    /// <summary>获取一个布尔值，该值指定此 <see cref="T:System.Security.AccessControl.ObjectSecurity" /> 对象是否是一个目录对象。</summary>
    /// <returns>如果 <see cref="T:System.Security.AccessControl.ObjectSecurity" /> 对象是一个目录对象，则为 true；否则为 false。</returns>
    protected bool IsDS
    {
      get
      {
        return this._securityDescriptor.IsDS;
      }
    }

    /// <summary>获取一个布尔值，该值指定与此 <see cref="T:System.Security.AccessControl.ObjectSecurity" /> 对象关联的自由访问控制列表 (DACL) 是否受到保护。</summary>
    /// <returns>如果 DACL 受到保护，则为 true；否则为 false。</returns>
    public bool AreAccessRulesProtected
    {
      get
      {
        this.ReadLock();
        try
        {
          return (uint) (this._securityDescriptor.ControlFlags & ControlFlags.DiscretionaryAclProtected) > 0U;
        }
        finally
        {
          this.ReadUnlock();
        }
      }
    }

    /// <summary>获取一个布尔值，该值指定与此 <see cref="T:System.Security.AccessControl.ObjectSecurity" /> 对象关联的系统访问控制列表 (SACL) 是否受到保护。</summary>
    /// <returns>如果 SACL 受到保护，则为 true；否则为 false。</returns>
    public bool AreAuditRulesProtected
    {
      get
      {
        this.ReadLock();
        try
        {
          return (uint) (this._securityDescriptor.ControlFlags & ControlFlags.SystemAclProtected) > 0U;
        }
        finally
        {
          this.ReadUnlock();
        }
      }
    }

    /// <summary>获取一个布尔值，该值指定与此 <see cref="T:System.Security.AccessControl.ObjectSecurity" /> 对象关联的访问规则是否处于规范顺序。</summary>
    /// <returns>如果访问规则处于规范顺序，则为 true；否则为 false。</returns>
    public bool AreAccessRulesCanonical
    {
      get
      {
        this.ReadLock();
        try
        {
          return this._securityDescriptor.IsDiscretionaryAclCanonical;
        }
        finally
        {
          this.ReadUnlock();
        }
      }
    }

    /// <summary>获取一个布尔值，该值指定与此 <see cref="T:System.Security.AccessControl.ObjectSecurity" /> 对象关联的审核规则是否处于规范顺序。</summary>
    /// <returns>如果审核规则处于规范顺序，则为 true；否则为 false。</returns>
    public bool AreAuditRulesCanonical
    {
      get
      {
        this.ReadLock();
        try
        {
          return this._securityDescriptor.IsSystemAclCanonical;
        }
        finally
        {
          this.ReadUnlock();
        }
      }
    }

    /// <summary>Gets the <see cref="T:System.Type" /> of the securable object associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</summary>
    /// <returns>与此 <see cref="T:System.Security.AccessControl.ObjectSecurity" /> 对象关联的可保护对象的类型。</returns>
    public abstract Type AccessRightType { get; }

    /// <summary>Gets the <see cref="T:System.Type" /> of the object associated with the access rules of this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.<see cref="T:System.Type" /> 对象必须是可以强制转换为 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 对象的对象。</summary>
    /// <returns>与此 <see cref="T:System.Security.AccessControl.ObjectSecurity" /> 对象的访问规则关联的对象的类型。</returns>
    public abstract Type AccessRuleType { get; }

    /// <summary>Gets the <see cref="T:System.Type" /> object associated with the audit rules of this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.<see cref="T:System.Type" /> 对象必须是可以强制转换为 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 对象的对象。</summary>
    /// <returns>与此 <see cref="T:System.Security.AccessControl.ObjectSecurity" /> 对象的审核规则关联的对象的类型。</returns>
    public abstract Type AuditRuleType { get; }

    /// <summary>初始化 <see cref="T:System.Security.AccessControl.ObjectSecurity" /> 类的新实例。</summary>
    protected ObjectSecurity()
    {
    }

    /// <summary>初始化 <see cref="T:System.Security.AccessControl.ObjectSecurity" /> 类的新实例。</summary>
    /// <param name="isContainer">如果新 <see cref="T:System.Security.AccessControl.ObjectSecurity" /> 对象是一个容器对象，则为 true。</param>
    /// <param name="isDS">如果新 <see cref="T:System.Security.AccessControl.ObjectSecurity" /> 对象是一个目录对象，则为 true。</param>
    protected ObjectSecurity(bool isContainer, bool isDS)
      : this()
    {
      DiscretionaryAcl discretionaryAcl = new DiscretionaryAcl(isContainer, isDS, 5);
      this._securityDescriptor = new CommonSecurityDescriptor(isContainer, isDS, ControlFlags.None, (SecurityIdentifier) null, (SecurityIdentifier) null, (SystemAcl) null, discretionaryAcl);
    }

    /// <summary>初始化 <see cref="T:System.Security.AccessControl.ObjectSecurity" /> 类的新实例。</summary>
    /// <param name="securityDescriptor">新的 <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> 实例的 <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" />。</param>
    protected ObjectSecurity(CommonSecurityDescriptor securityDescriptor)
      : this()
    {
      if (securityDescriptor == null)
        throw new ArgumentNullException("securityDescriptor");
      this._securityDescriptor = securityDescriptor;
    }

    private void UpdateWithNewSecurityDescriptor(RawSecurityDescriptor newOne, AccessControlSections includeSections)
    {
      if ((includeSections & AccessControlSections.Owner) != AccessControlSections.None)
      {
        this._ownerModified = true;
        this._securityDescriptor.Owner = newOne.Owner;
      }
      if ((includeSections & AccessControlSections.Group) != AccessControlSections.None)
      {
        this._groupModified = true;
        this._securityDescriptor.Group = newOne.Group;
      }
      if ((includeSections & AccessControlSections.Audit) != AccessControlSections.None)
      {
        this._saclModified = true;
        this._securityDescriptor.SystemAcl = newOne.SystemAcl == null ? (SystemAcl) null : new SystemAcl(this.IsContainer, this.IsDS, newOne.SystemAcl, true);
        this._securityDescriptor.UpdateControlFlags(ObjectSecurity.SACL_CONTROL_FLAGS, newOne.ControlFlags & ObjectSecurity.SACL_CONTROL_FLAGS);
      }
      if ((includeSections & AccessControlSections.Access) == AccessControlSections.None)
        return;
      this._daclModified = true;
      this._securityDescriptor.DiscretionaryAcl = newOne.DiscretionaryAcl == null ? (DiscretionaryAcl) null : new DiscretionaryAcl(this.IsContainer, this.IsDS, newOne.DiscretionaryAcl, true);
      ControlFlags controlFlags = this._securityDescriptor.ControlFlags & ControlFlags.DiscretionaryAclPresent;
      this._securityDescriptor.UpdateControlFlags(ObjectSecurity.DACL_CONTROL_FLAGS, (newOne.ControlFlags | controlFlags) & ObjectSecurity.DACL_CONTROL_FLAGS);
    }

    /// <summary>锁定此 <see cref="T:System.Security.AccessControl.ObjectSecurity" /> 对象以进行读访问。</summary>
    protected void ReadLock()
    {
      this._lock.AcquireReaderLock(-1);
    }

    /// <summary>取消此 <see cref="T:System.Security.AccessControl.ObjectSecurity" /> 对象的锁定以进行读访问。</summary>
    protected void ReadUnlock()
    {
      this._lock.ReleaseReaderLock();
    }

    /// <summary>锁定此 <see cref="T:System.Security.AccessControl.ObjectSecurity" /> 对象以进行写访问。</summary>
    protected void WriteLock()
    {
      this._lock.AcquireWriterLock(-1);
    }

    /// <summary>取消此 <see cref="T:System.Security.AccessControl.ObjectSecurity" /> 对象的锁定以进行写访问。</summary>
    protected void WriteUnlock()
    {
      this._lock.ReleaseWriterLock();
    }

    /// <summary>将与此 <see cref="T:System.Security.AccessControl.ObjectSecurity" /> 对象关联的安全说明符的指定部分保存到永久性存储。建议传递给构造函数和 Persist 方法的 <paramref name="includeSections" /> 参数的值应该相同。有关更多信息，请参见“备注”。</summary>
    /// <param name="name">用于检索保持的信息的名称。</param>
    /// <param name="includeSections">
    /// <see cref="T:System.Security.AccessControl.AccessControlSections" /> 枚举值之一，指定要保存的可保护对象的安全说明符（访问规则、审核规则、所有者和主要组）的各个部分。</param>
    protected virtual void Persist(string name, AccessControlSections includeSections)
    {
      throw new NotImplementedException();
    }

    /// <summary>将与此 <see cref="T:System.Security.AccessControl.ObjectSecurity" /> 对象关联的安全说明符的指定部分保存到永久性存储。建议传递给构造函数和 Persist 方法的 <paramref name="includeSections" /> 参数的值应该相同。有关更多信息，请参见“备注”。</summary>
    /// <param name="enableOwnershipPrivilege">若要启用允许调用方取得对象所有权的特权，则为 true。</param>
    /// <param name="name">用于检索保持的信息的名称。</param>
    /// <param name="includeSections">
    /// <see cref="T:System.Security.AccessControl.AccessControlSections" /> 枚举值之一，指定要保存的可保护对象的安全说明符（访问规则、审核规则、所有者和主要组）的各个部分。</param>
    [SecuritySafeCritical]
    [HandleProcessCorruptedStateExceptions]
    protected virtual void Persist(bool enableOwnershipPrivilege, string name, AccessControlSections includeSections)
    {
      Privilege privilege = (Privilege) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        if (enableOwnershipPrivilege)
        {
          privilege = new Privilege("SeTakeOwnershipPrivilege");
          try
          {
            privilege.Enable();
          }
          catch (PrivilegeNotHeldException ex)
          {
          }
        }
        this.Persist(name, includeSections);
      }
      catch
      {
        if (privilege != null)
          privilege.Revert();
        throw;
      }
      finally
      {
        if (privilege != null)
          privilege.Revert();
      }
    }

    /// <summary>将与此 <see cref="T:System.Security.AccessControl.ObjectSecurity" /> 对象关联的安全说明符的指定部分保存到永久性存储。建议传递给构造函数和 Persist 方法的 <paramref name="includeSections" /> 参数的值应该相同。有关更多信息，请参见“备注”。</summary>
    /// <param name="handle">用于检索保持的信息的句柄。</param>
    /// <param name="includeSections">
    /// <see cref="T:System.Security.AccessControl.AccessControlSections" /> 枚举值之一，指定要保存的可保护对象的安全说明符（访问规则、审核规则、所有者和主要组）的各个部分。</param>
    [SecuritySafeCritical]
    protected virtual void Persist(SafeHandle handle, AccessControlSections includeSections)
    {
      throw new NotImplementedException();
    }

    /// <summary>获取与指定的主要组关联的所有者。</summary>
    /// <returns>与指定的组关联的所有者。</returns>
    /// <param name="targetType">要获取其所有者的主要组。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    public IdentityReference GetOwner(Type targetType)
    {
      this.ReadLock();
      try
      {
        if (this._securityDescriptor.Owner == (SecurityIdentifier) null)
          return (IdentityReference) null;
        return this._securityDescriptor.Owner.Translate(targetType);
      }
      finally
      {
        this.ReadUnlock();
      }
    }

    /// <summary>设置与此 <see cref="T:System.Security.AccessControl.ObjectSecurity" /> 对象关联的安全说明符的所有者。</summary>
    /// <param name="identity">要设置的所有者。</param>
    public void SetOwner(IdentityReference identity)
    {
      if (identity == (IdentityReference) null)
        throw new ArgumentNullException("identity");
      this.WriteLock();
      try
      {
        this._securityDescriptor.Owner = identity.Translate(typeof (SecurityIdentifier)) as SecurityIdentifier;
        this._ownerModified = true;
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>获取与指定的所有者关联的主要组。</summary>
    /// <returns>与指定的所有者关联的主要组。</returns>
    /// <param name="targetType">要获取其主要组的所有者。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    public IdentityReference GetGroup(Type targetType)
    {
      this.ReadLock();
      try
      {
        if (this._securityDescriptor.Group == (SecurityIdentifier) null)
          return (IdentityReference) null;
        return this._securityDescriptor.Group.Translate(targetType);
      }
      finally
      {
        this.ReadUnlock();
      }
    }

    /// <summary>设置与此 <see cref="T:System.Security.AccessControl.ObjectSecurity" /> 对象关联的安全说明符的主要组。</summary>
    /// <param name="identity">要设置的主要组。</param>
    public void SetGroup(IdentityReference identity)
    {
      if (identity == (IdentityReference) null)
        throw new ArgumentNullException("identity");
      this.WriteLock();
      try
      {
        this._securityDescriptor.Group = identity.Translate(typeof (SecurityIdentifier)) as SecurityIdentifier;
        this._groupModified = true;
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>移除与指定的 <see cref="T:System.Security.Principal.IdentityReference" /> 关联的所有访问规则。</summary>
    /// <param name="identity">要移除其所有访问规则的 <see cref="T:System.Security.Principal.IdentityReference" />。</param>
    /// <exception cref="T:System.InvalidOperationException">所有访问规则的顺序都不规范。</exception>
    public virtual void PurgeAccessRules(IdentityReference identity)
    {
      if (identity == (IdentityReference) null)
        throw new ArgumentNullException("identity");
      this.WriteLock();
      try
      {
        this._securityDescriptor.PurgeAccessControl(identity.Translate(typeof (SecurityIdentifier)) as SecurityIdentifier);
        this._daclModified = true;
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>移除与指定的 <see cref="T:System.Security.Principal.IdentityReference" /> 关联的所有审核规则。</summary>
    /// <param name="identity">要移除其审核规则的 <see cref="T:System.Security.Principal.IdentityReference" />。</param>
    /// <exception cref="T:System.InvalidOperationException">所有审核规则的顺序都不规范。</exception>
    public virtual void PurgeAuditRules(IdentityReference identity)
    {
      if (identity == (IdentityReference) null)
        throw new ArgumentNullException("identity");
      this.WriteLock();
      try
      {
        this._securityDescriptor.PurgeAudit(identity.Translate(typeof (SecurityIdentifier)) as SecurityIdentifier);
        this._saclModified = true;
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>设置或移除与此 <see cref="T:System.Security.AccessControl.ObjectSecurity" /> 对象关联的访问规则的保护。受保护的访问规则不会通过继承被父对象修改。</summary>
    /// <param name="isProtected">要防止与此 <see cref="T:System.Security.AccessControl.ObjectSecurity" /> 对象关联的访问规则被继承，则为 true；要允许继承，则为 false。</param>
    /// <param name="preserveInheritance">要保留继承的访问规则，则为 true；要移除继承的访问规则，则为 false。如果 <paramref name="isProtected" /> 为 false，则忽略此参数。</param>
    /// <exception cref="T:System.InvalidOperationException">此方法尝试从非规范的自由访问控制列表 (DACL) 移除继承的规则。</exception>
    public void SetAccessRuleProtection(bool isProtected, bool preserveInheritance)
    {
      this.WriteLock();
      try
      {
        this._securityDescriptor.SetDiscretionaryAclProtection(isProtected, preserveInheritance);
        this._daclModified = true;
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>设置或移除与此 <see cref="T:System.Security.AccessControl.ObjectSecurity" /> 对象关联的审核规则的保护。受保护的审核规则不会通过继承被父对象修改。</summary>
    /// <param name="isProtected">要防止与此 <see cref="T:System.Security.AccessControl.ObjectSecurity" /> 对象关联的审核规则被继承，则为 true；要允许继承，则为 false。</param>
    /// <param name="preserveInheritance">要保留继承的审核规则，则为 true；要移除继承的审核规则，则为 false。如果 <paramref name="isProtected" /> 为 false，则忽略此参数。</param>
    /// <exception cref="T:System.InvalidOperationException">此方法尝试从非规范的系统访问控制列表 (SACL) 移除继承的规则。</exception>
    public void SetAuditRuleProtection(bool isProtected, bool preserveInheritance)
    {
      this.WriteLock();
      try
      {
        this._securityDescriptor.SetSystemAclProtection(isProtected, preserveInheritance);
        this._saclModified = true;
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>返回一个布尔值，该值指定与此 <see cref="T:System.Security.AccessControl.ObjectSecurity" /> 对象关联的安全说明符是否能够转换为安全说明符定义语言 (SDDL) 格式。</summary>
    /// <returns>如果与此 <see cref="T:System.Security.AccessControl.ObjectSecurity" /> 对象关联的安全性说明符能够转换为安全性说明符定义语言 (SDDL) 格式，则为 true；否则为 false。</returns>
    public static bool IsSddlConversionSupported()
    {
      return true;
    }

    /// <summary>返回与此 <see cref="T:System.Security.AccessControl.ObjectSecurity" /> 对象关联的安全说明符指定部分的安全说明符定义语言 (SDDL) 表示形式。</summary>
    /// <returns>与此 <see cref="T:System.Security.AccessControl.ObjectSecurity" /> 对象关联的安全说明符指定部分的 SDDL 表示形式。</returns>
    /// <param name="includeSections">指定要获取安全性说明符的哪些部分（访问规则、审核规则、主要组、所有者）。</param>
    public string GetSecurityDescriptorSddlForm(AccessControlSections includeSections)
    {
      this.ReadLock();
      try
      {
        return this._securityDescriptor.GetSddlForm(includeSections);
      }
      finally
      {
        this.ReadUnlock();
      }
    }

    /// <summary>根据指定的安全说明符定义语言 (SDDL) 字符串为此 <see cref="T:System.Security.AccessControl.ObjectSecurity" /> 对象设置安全说明符。</summary>
    /// <param name="sddlForm">用于设置安全说明符的 SDDL 字符串。</param>
    public void SetSecurityDescriptorSddlForm(string sddlForm)
    {
      this.SetSecurityDescriptorSddlForm(sddlForm, AccessControlSections.All);
    }

    /// <summary>根据指定的安全说明符定义语言 (SDDL) 字符串为此 <see cref="T:System.Security.AccessControl.ObjectSecurity" /> 对象设置安全说明符的指定部分。</summary>
    /// <param name="sddlForm">用于设置安全说明符的 SDDL 字符串。</param>
    /// <param name="includeSections">安全说明符中要设置的部分（访问规则、审核规则、所有者、主要组）。</param>
    public void SetSecurityDescriptorSddlForm(string sddlForm, AccessControlSections includeSections)
    {
      if (sddlForm == null)
        throw new ArgumentNullException("sddlForm");
      if ((includeSections & AccessControlSections.All) == AccessControlSections.None)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumAtLeastOneFlag"), "includeSections");
      this.WriteLock();
      try
      {
        this.UpdateWithNewSecurityDescriptor(new RawSecurityDescriptor(sddlForm), includeSections);
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>返回一个字节值数组，表示此 <see cref="T:System.Security.AccessControl.ObjectSecurity" /> 对象的安全说明符信息。</summary>
    /// <returns>一个字节值数组，表示此 <see cref="T:System.Security.AccessControl.ObjectSecurity" /> 对象的安全说明符。如果此 <see cref="T:System.Security.AccessControl.ObjectSecurity" /> 对象中没有安全性信息，则此方法返回 null。</returns>
    public byte[] GetSecurityDescriptorBinaryForm()
    {
      this.ReadLock();
      try
      {
        byte[] binaryForm = new byte[this._securityDescriptor.BinaryLength];
        this._securityDescriptor.GetBinaryForm(binaryForm, 0);
        return binaryForm;
      }
      finally
      {
        this.ReadUnlock();
      }
    }

    /// <summary>根据指定的字节值数组设置此 <see cref="T:System.Security.AccessControl.ObjectSecurity" /> 对象的安全说明符。</summary>
    /// <param name="binaryForm">用于设置安全说明符的字节数组。</param>
    public void SetSecurityDescriptorBinaryForm(byte[] binaryForm)
    {
      this.SetSecurityDescriptorBinaryForm(binaryForm, AccessControlSections.All);
    }

    /// <summary>根据指定的字节值数组设置此 <see cref="T:System.Security.AccessControl.ObjectSecurity" /> 对象的安全说明符中的指定部分。</summary>
    /// <param name="binaryForm">用于设置安全说明符的字节数组。</param>
    /// <param name="includeSections">安全说明符中要设置的部分（访问规则、审核规则、所有者、主要组）。</param>
    public void SetSecurityDescriptorBinaryForm(byte[] binaryForm, AccessControlSections includeSections)
    {
      if (binaryForm == null)
        throw new ArgumentNullException("binaryForm");
      if ((includeSections & AccessControlSections.All) == AccessControlSections.None)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumAtLeastOneFlag"), "includeSections");
      this.WriteLock();
      try
      {
        this.UpdateWithNewSecurityDescriptor(new RawSecurityDescriptor(binaryForm, 0), includeSections);
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>将指定修改应用于与此 <see cref="T:System.Security.AccessControl.ObjectSecurity" /> 对象关联的自由访问控制列表 (DACL)。</summary>
    /// <returns>如果成功修改了 DACL，则为 true；否则为 false。</returns>
    /// <param name="modification">要应用于 DACL 的修改。</param>
    /// <param name="rule">要修改的访问规则。</param>
    /// <param name="modified">如果成功修改了 DACL，则为 true；否则为 false。</param>
    protected abstract bool ModifyAccess(AccessControlModification modification, AccessRule rule, out bool modified);

    /// <summary>将指定修改应用于与此 <see cref="T:System.Security.AccessControl.ObjectSecurity" /> 对象关联的系统访问控制列表 (SACL)。</summary>
    /// <returns>如果成功修改了 SACL，则为 true；否则为 false。</returns>
    /// <param name="modification">要应用于 SACL 的修改。</param>
    /// <param name="rule">要修改的审核规则。</param>
    /// <param name="modified">如果成功修改了 SACL，则为 true；否则为 false。</param>
    protected abstract bool ModifyAudit(AccessControlModification modification, AuditRule rule, out bool modified);

    /// <summary>将指定修改应用于与此 <see cref="T:System.Security.AccessControl.ObjectSecurity" /> 对象关联的自由访问控制列表 (DACL)。</summary>
    /// <returns>如果成功修改了 DACL，则为 true；否则为 false。</returns>
    /// <param name="modification">要应用于 DACL 的修改。</param>
    /// <param name="rule">要修改的访问规则。</param>
    /// <param name="modified">如果成功修改了 DACL，则为 true；否则为 false。</param>
    public virtual bool ModifyAccessRule(AccessControlModification modification, AccessRule rule, out bool modified)
    {
      if (rule == null)
        throw new ArgumentNullException("rule");
      if (!this.AccessRuleType.IsAssignableFrom(rule.GetType()))
        throw new ArgumentException(Environment.GetResourceString("AccessControl_InvalidAccessRuleType"), "rule");
      this.WriteLock();
      try
      {
        return this.ModifyAccess(modification, rule, out modified);
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>将指定修改应用于与此 <see cref="T:System.Security.AccessControl.ObjectSecurity" /> 对象关联的系统访问控制列表 (SACL)。</summary>
    /// <returns>如果成功修改了 SACL，则为 true；否则为 false。</returns>
    /// <param name="modification">要应用于 SACL 的修改。</param>
    /// <param name="rule">要修改的审核规则。</param>
    /// <param name="modified">如果成功修改了 SACL，则为 true；否则为 false。</param>
    public virtual bool ModifyAuditRule(AccessControlModification modification, AuditRule rule, out bool modified)
    {
      if (rule == null)
        throw new ArgumentNullException("rule");
      if (!this.AuditRuleType.IsAssignableFrom(rule.GetType()))
        throw new ArgumentException(Environment.GetResourceString("AccessControl_InvalidAuditRuleType"), "rule");
      this.WriteLock();
      try
      {
        return this.ModifyAudit(modification, rule, out modified);
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>用指定的值初始化 <see cref="T:System.Security.AccessControl.AccessRule" /> 类的新实例。</summary>
    /// <returns>此方法所创建的 <see cref="T:System.Security.AccessControl.AccessRule" /> 对象。</returns>
    /// <param name="identityReference">应用访问规则的标识。它必须是可强制转换为 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 的对象。</param>
    /// <param name="accessMask">此规则的访问掩码。访问掩码是一个 32 位的匿名位集合，其含义是由每个集成器定义的。</param>
    /// <param name="isInherited">如果此规则继承自父容器，则为 true。</param>
    /// <param name="inheritanceFlags">指定访问规则的继承属性。</param>
    /// <param name="propagationFlags">指定继承的访问规则是否自动传播。如果 <paramref name="inheritanceFlags" /> 设置为 <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />，则将忽略传播标志。</param>
    /// <param name="type">指定有效的访问控制类型。</param>
    public abstract AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type);

    /// <summary>用指定的值初始化 <see cref="T:System.Security.AccessControl.AuditRule" /> 类的新实例。</summary>
    /// <returns>此方法所创建的 <see cref="T:System.Security.AccessControl.AuditRule" /> 对象。</returns>
    /// <param name="identityReference">审核规则应用到的标识。它必须是可强制转换为 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 的对象。</param>
    /// <param name="accessMask">此规则的访问掩码。访问掩码是一个 32 位的匿名位集合，其含义是由每个集成器定义的。</param>
    /// <param name="isInherited">如果此规则继承自父容器，则为 true。</param>
    /// <param name="inheritanceFlags">指定审核规则的继承属性。</param>
    /// <param name="propagationFlags">指定继承的审核规则是否自动传播。如果 <paramref name="inheritanceFlags" /> 设置为 <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />，则将忽略传播标志。</param>
    /// <param name="flags">指定对规则进行审核的条件。</param>
    public abstract AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags);
  }
}
