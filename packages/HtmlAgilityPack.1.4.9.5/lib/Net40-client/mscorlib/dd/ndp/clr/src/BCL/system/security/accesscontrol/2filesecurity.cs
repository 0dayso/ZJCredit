// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.FileSystemSecurity
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32.SafeHandles;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>表示文件或目录的访问控制和审核安全。</summary>
  public abstract class FileSystemSecurity : NativeObjectSecurity
  {
    private const ResourceType s_ResourceType = ResourceType.FileObject;

    /// <summary>获取 <see cref="T:System.Security.AccessControl.FileSystemSecurity" /> 类用于表示访问权限的枚举。</summary>
    /// <returns>一个 <see cref="T:System.Type" /> 对象，表示 <see cref="T:System.Security.AccessControl.FileSystemRights" /> 枚举。</returns>
    public override Type AccessRightType
    {
      get
      {
        return typeof (FileSystemRights);
      }
    }

    /// <summary>获取 <see cref="T:System.Security.AccessControl.FileSystemSecurity" /> 类用于表示访问规则的枚举。</summary>
    /// <returns>一个 <see cref="T:System.Type" /> 对象，表示 <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> 类。</returns>
    public override Type AccessRuleType
    {
      get
      {
        return typeof (FileSystemAccessRule);
      }
    }

    /// <summary>获取 <see cref="T:System.Security.AccessControl.FileSystemSecurity" /> 类用于表示审核规则的类型。</summary>
    /// <returns>一个 <see cref="T:System.Type" /> 对象，表示 <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> 类。</returns>
    public override Type AuditRuleType
    {
      get
      {
        return typeof (FileSystemAuditRule);
      }
    }

    [SecurityCritical]
    internal FileSystemSecurity(bool isContainer)
      : base(isContainer, ResourceType.FileObject, new NativeObjectSecurity.ExceptionFromErrorCode(FileSystemSecurity._HandleErrorCode), (object) isContainer)
    {
    }

    [SecurityCritical]
    internal FileSystemSecurity(bool isContainer, string name, AccessControlSections includeSections, bool isDirectory)
      : base(isContainer, ResourceType.FileObject, name, includeSections, new NativeObjectSecurity.ExceptionFromErrorCode(FileSystemSecurity._HandleErrorCode), (object) isDirectory)
    {
    }

    [SecurityCritical]
    internal FileSystemSecurity(bool isContainer, SafeFileHandle handle, AccessControlSections includeSections, bool isDirectory)
      : base(isContainer, ResourceType.FileObject, (SafeHandle) handle, includeSections, new NativeObjectSecurity.ExceptionFromErrorCode(FileSystemSecurity._HandleErrorCode), (object) isDirectory)
    {
    }

    [SecurityCritical]
    private static Exception _HandleErrorCode(int errorCode, string name, SafeHandle handle, object context)
    {
      Exception exception = (Exception) null;
      if (errorCode != 2)
      {
        if (errorCode != 6)
        {
          if (errorCode == 123)
            exception = (Exception) new ArgumentException(Environment.GetResourceString("Argument_InvalidName"), "name");
        }
        else
          exception = (Exception) new ArgumentException(Environment.GetResourceString("AccessControl_InvalidHandle"));
      }
      else
        exception = context == null || !(context is bool) || !(bool) context ? (name == null || name.Length == 0 ? (Exception) new FileNotFoundException() : (Exception) new FileNotFoundException(name)) : (name == null || name.Length == 0 ? (Exception) new DirectoryNotFoundException() : (Exception) new DirectoryNotFoundException(name));
      return exception;
    }

    /// <summary>使用指定的访问权限、访问控制和标志初始化 <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> 类的新实例，该实例表示指定用户的新的访问控制规则。</summary>
    /// <returns>表示指定用户的新访问控制规则的新的 <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> 对象，具有指定的访问权限、访问控制和标志。</returns>
    /// <param name="identityReference">表示用户帐户的 <see cref="T:System.Security.Principal.IdentityReference" /> 对象。</param>
    /// <param name="accessMask">指定访问类型的整数。</param>
    /// <param name="isInherited">如果该访问规则是继承的，则为 true；否则为 false。</param>
    /// <param name="inheritanceFlags">
    /// <see cref="T:System.Security.AccessControl.InheritanceFlags" /> 值之一，指定如何将访问掩码传播到子对象。</param>
    /// <param name="propagationFlags">
    /// <see cref="T:System.Security.AccessControl.PropagationFlags" /> 值之一，指定如何将访问控制项 (ACE) 传播到子对象。</param>
    /// <param name="type">
    /// <see cref="T:System.Security.AccessControl.AccessControlType" /> 值之一，指定允许还是拒绝访问。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="accessMask" />、<paramref name="inheritanceFlags" />、<paramref name="propagationFlags" /> 或 <paramref name="type" /> 参数指定了无效值。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="identityReference" /> 参数为 null。- 或 -<paramref name="accessMask" /> 参数为零。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="identityReference" /> 既不是 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 类型，也不是可转换为 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 类型的类型，如 <see cref="T:System.Security.Principal.NTAccount" />。</exception>
    public override sealed AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
    {
      return (AccessRule) new FileSystemAccessRule(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, type);
    }

    /// <summary>初始化 <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> 类的新实例，它表示指定用户的指定审核规则。</summary>
    /// <returns>新的 <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> 对象，表示指定用户的指定审核规则。</returns>
    /// <param name="identityReference">表示用户帐户的 <see cref="T:System.Security.Principal.IdentityReference" /> 对象。</param>
    /// <param name="accessMask">指定访问类型的整数。</param>
    /// <param name="isInherited">如果该访问规则是继承的，则为 true；否则为 false。</param>
    /// <param name="inheritanceFlags">
    /// <see cref="T:System.Security.AccessControl.InheritanceFlags" /> 值之一，指定如何将访问掩码传播到子对象。</param>
    /// <param name="propagationFlags">
    /// <see cref="T:System.Security.AccessControl.PropagationFlags" /> 值之一，指定如何将访问控制项 (ACE) 传播到子对象。</param>
    /// <param name="flags">
    /// <see cref="T:System.Security.AccessControl.AuditFlags" /> 值之一，指定要执行的审核的类型。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="accessMask" />、<paramref name="inheritanceFlags" />、<paramref name="propagationFlags" /> 或 <paramref name="flags" /> 属性指定了无效值。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="identityReference" /> 属性为 null。- 或 -<paramref name="accessMask" /> 属性零。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="identityReference" /> 属性既不是 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 类型，也不是可转换为 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 类型的类型，如 <see cref="T:System.Security.Principal.NTAccount" />。</exception>
    public override sealed AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
    {
      return (AuditRule) new FileSystemAuditRule(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, flags);
    }

    internal AccessControlSections GetAccessControlSectionsFromChanges()
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

    [SecurityCritical]
    [SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
    internal void Persist(string fullPath)
    {
      new FileIOPermission(FileIOPermissionAccess.NoAccess, AccessControlActions.Change, fullPath).Demand();
      this.WriteLock();
      try
      {
        AccessControlSections sectionsFromChanges = this.GetAccessControlSectionsFromChanges();
        this.Persist(fullPath, sectionsFromChanges);
        this.OwnerModified = this.GroupModified = this.AuditRulesModified = this.AccessRulesModified = false;
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
    internal void Persist(SafeFileHandle handle, string fullPath)
    {
      if (fullPath != null)
        new FileIOPermission(FileIOPermissionAccess.NoAccess, AccessControlActions.Change, fullPath).Demand();
      else
        new FileIOPermission(PermissionState.Unrestricted).Demand();
      this.WriteLock();
      try
      {
        AccessControlSections sectionsFromChanges = this.GetAccessControlSectionsFromChanges();
        this.Persist((SafeHandle) handle, sectionsFromChanges);
        this.OwnerModified = this.GroupModified = this.AuditRulesModified = this.AccessRulesModified = false;
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>将指定的访问控制列表 (ACL) 权限添加到当前文件或目录。</summary>
    /// <param name="rule">一个 <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> 对象，表示要添加到文件或目录的访问控制列表 (ACL) 权限。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="rule" /> 参数为 null。</exception>
    public void AddAccessRule(FileSystemAccessRule rule)
    {
      this.AddAccessRule((AccessRule) rule);
    }

    /// <summary>设置当前文件或目录的指定访问控制列表 (ACL) 权限。</summary>
    /// <param name="rule">一个 <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> 对象，表示要为文件或目录设置的访问控制列表 (ACL) 权限。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="rule" /> 参数为 null。</exception>
    public void SetAccessRule(FileSystemAccessRule rule)
    {
      this.SetAccessRule((AccessRule) rule);
    }

    /// <summary>将指定的访问控制列表 (ACL) 权限添加到当前文件或目录，并移除所有匹配的 ACL 权限。</summary>
    /// <param name="rule">一个 <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> 对象，表示要添加到文件或目录的访问控制列表 (ACL) 权限。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="rule" /> 参数为 null。</exception>
    public void ResetAccessRule(FileSystemAccessRule rule)
    {
      this.ResetAccessRule((AccessRule) rule);
    }

    /// <summary>从当前文件或目录移除所有匹配的允许或拒绝访问控制列表 (ACL) 权限。</summary>
    /// <returns>如果访问规则已移除，则为 true；否则为 false。</returns>
    /// <param name="rule">一个 <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> 对象，表示要从文件或目录中移除的访问控制列表 (ACL) 权限。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="rule" /> 参数为 null。</exception>
    public bool RemoveAccessRule(FileSystemAccessRule rule)
    {
      if (rule == null)
        throw new ArgumentNullException("rule");
      AuthorizationRuleCollection accessRules = this.GetAccessRules(true, true, rule.IdentityReference.GetType());
      for (int index = 0; index < accessRules.Count; ++index)
      {
        FileSystemAccessRule systemAccessRule = accessRules[index] as FileSystemAccessRule;
        if (systemAccessRule != null && systemAccessRule.FileSystemRights == rule.FileSystemRights && (systemAccessRule.IdentityReference == rule.IdentityReference && systemAccessRule.AccessControlType == rule.AccessControlType))
          return this.RemoveAccessRule((AccessRule) rule);
      }
      return this.RemoveAccessRule((AccessRule) new FileSystemAccessRule(rule.IdentityReference, FileSystemAccessRule.AccessMaskFromRights(rule.FileSystemRights, AccessControlType.Deny), rule.IsInherited, rule.InheritanceFlags, rule.PropagationFlags, rule.AccessControlType));
    }

    /// <summary>从当前文件或目录移除指定用户的所有访问控制列表 (ACL) 权限。</summary>
    /// <param name="rule">一个 <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> 对象，该对象指定应该从文件或目录移除其访问控制列表 (ACL) 权限的用户。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="rule" /> 参数为 null。</exception>
    public void RemoveAccessRuleAll(FileSystemAccessRule rule)
    {
      this.RemoveAccessRuleAll((AccessRule) rule);
    }

    /// <summary>从当前文件或目录移除单个匹配的允许或拒绝访问控制列表 (ACL) 权限。</summary>
    /// <param name="rule">一个 <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> 对象，该对象指定应该从文件或目录移除其访问控制列表 (ACL) 权限的用户。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="rule" /> 参数为 null。</exception>
    public void RemoveAccessRuleSpecific(FileSystemAccessRule rule)
    {
      if (rule == null)
        throw new ArgumentNullException("rule");
      AuthorizationRuleCollection accessRules = this.GetAccessRules(true, true, rule.IdentityReference.GetType());
      for (int index = 0; index < accessRules.Count; ++index)
      {
        FileSystemAccessRule systemAccessRule = accessRules[index] as FileSystemAccessRule;
        if (systemAccessRule != null && systemAccessRule.FileSystemRights == rule.FileSystemRights && (systemAccessRule.IdentityReference == rule.IdentityReference && systemAccessRule.AccessControlType == rule.AccessControlType))
        {
          this.RemoveAccessRuleSpecific((AccessRule) rule);
          return;
        }
      }
      this.RemoveAccessRuleSpecific((AccessRule) new FileSystemAccessRule(rule.IdentityReference, FileSystemAccessRule.AccessMaskFromRights(rule.FileSystemRights, AccessControlType.Deny), rule.IsInherited, rule.InheritanceFlags, rule.PropagationFlags, rule.AccessControlType));
    }

    /// <summary>将指定的审核规则添加到当前文件或目录。</summary>
    /// <param name="rule">一个 <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> 对象，表示要添加到文件或目录的审核规则。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="rule" /> 参数为 null。</exception>
    public void AddAuditRule(FileSystemAuditRule rule)
    {
      this.AddAuditRule((AuditRule) rule);
    }

    /// <summary>设置当前文件或目录的指定审核规则。</summary>
    /// <param name="rule">一个 <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> 对象，表示要为文件或目录设置的审核规则。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="rule" /> 参数为 null。</exception>
    public void SetAuditRule(FileSystemAuditRule rule)
    {
      this.SetAuditRule((AuditRule) rule);
    }

    /// <summary>从当前文件或目录移除所有匹配的允许或拒绝审核规则。</summary>
    /// <returns>true（如果审核规则已移除）；否则为 false</returns>
    /// <param name="rule">一个 <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> 对象，表示要从文件或目录移除的审核规则。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="rule" /> 参数为 null。</exception>
    public bool RemoveAuditRule(FileSystemAuditRule rule)
    {
      return this.RemoveAuditRule((AuditRule) rule);
    }

    /// <summary>从当前文件或目录移除指定用户的所有审核规则。</summary>
    /// <param name="rule">一个 <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> 对象，该对象指定应该从文件或目录删除其审核规则的用户。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="rule" /> 参数为 null。</exception>
    public void RemoveAuditRuleAll(FileSystemAuditRule rule)
    {
      this.RemoveAuditRuleAll((AuditRule) rule);
    }

    /// <summary>从当前文件或目录移除单个匹配的允许或拒绝审核规则。</summary>
    /// <param name="rule">一个 <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> 对象，表示要从文件或目录移除的审核规则。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="rule" /> 参数为 null。</exception>
    public void RemoveAuditRuleSpecific(FileSystemAuditRule rule)
    {
      this.RemoveAuditRuleSpecific((AuditRule) rule);
    }
  }
}
