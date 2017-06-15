// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.EventWaitHandleSecurity
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;

namespace System.Security.AccessControl
{
  /// <summary>表示应用于命名的系统等待句柄的 Windows 访问控制安全性。此类不能被继承。</summary>
  public sealed class EventWaitHandleSecurity : NativeObjectSecurity
  {
    /// <summary>获取 <see cref="T:System.Security.AccessControl.EventWaitHandleSecurity" /> 类用于表示访问权限的枚举类型。</summary>
    /// <returns>一个 <see cref="T:System.Type" /> 对象，表示 <see cref="T:System.Security.AccessControl.EventWaitHandleRights" /> 枚举。</returns>
    public override Type AccessRightType
    {
      get
      {
        return typeof (EventWaitHandleRights);
      }
    }

    /// <summary>获取 <see cref="T:System.Security.AccessControl.EventWaitHandleSecurity" /> 类用于表示访问规则的类型。</summary>
    /// <returns>一个 <see cref="T:System.Type" /> 对象，表示 <see cref="T:System.Security.AccessControl.EventWaitHandleAccessRule" /> 类。</returns>
    public override Type AccessRuleType
    {
      get
      {
        return typeof (EventWaitHandleAccessRule);
      }
    }

    /// <summary>获取 <see cref="T:System.Security.AccessControl.EventWaitHandleSecurity" /> 类用于表示审核规则的类型。</summary>
    /// <returns>一个 <see cref="T:System.Type" /> 对象，表示 <see cref="T:System.Security.AccessControl.EventWaitHandleAuditRule" /> 类。</returns>
    public override Type AuditRuleType
    {
      get
      {
        return typeof (EventWaitHandleAuditRule);
      }
    }

    /// <summary>使用默认值初始化 <see cref="T:System.Security.AccessControl.EventWaitHandleSecurity" /> 类的新实例。</summary>
    /// <exception cref="T:System.NotSupportedException">Windows 98 或 Windows Millennium Edition 不支持此类。</exception>
    public EventWaitHandleSecurity()
      : base(true, ResourceType.KernelObject)
    {
    }

    [SecurityCritical]
    internal EventWaitHandleSecurity(string name, AccessControlSections includeSections)
      : base(true, ResourceType.KernelObject, name, includeSections, new NativeObjectSecurity.ExceptionFromErrorCode(EventWaitHandleSecurity._HandleErrorCode), (object) null)
    {
    }

    [SecurityCritical]
    internal EventWaitHandleSecurity(SafeWaitHandle handle, AccessControlSections includeSections)
      : base(true, ResourceType.KernelObject, (SafeHandle) handle, includeSections, new NativeObjectSecurity.ExceptionFromErrorCode(EventWaitHandleSecurity._HandleErrorCode), (object) null)
    {
    }

    [SecurityCritical]
    private static Exception _HandleErrorCode(int errorCode, string name, SafeHandle handle, object context)
    {
      Exception exception = (Exception) null;
      if (errorCode == 2 || errorCode == 6 || errorCode == 123)
      {
        if (name != null && name.Length != 0)
          exception = (Exception) new WaitHandleCannotBeOpenedException(Environment.GetResourceString("Threading.WaitHandleCannotBeOpenedException_InvalidHandle", (object) name));
        else
          exception = (Exception) new WaitHandleCannotBeOpenedException();
      }
      return exception;
    }

    /// <summary>使用指定的访问权限、访问控制和标志为指定用户创建新的访问控制规则。</summary>
    /// <returns>一个 <see cref="T:System.Security.AccessControl.EventWaitHandleAccessRule" /> 对象，表示指定用户的指定权限。</returns>
    /// <param name="identityReference">一个 <see cref="T:System.Security.Principal.IdentityReference" />，用于标识此规则应用到的用户或组。</param>
    /// <param name="accessMask">
    /// <see cref="T:System.Security.AccessControl.EventWaitHandleRights" /> 值的按位组合，用于指定允许或拒绝的访问权限，该组合将被强制转换为整数。</param>
    /// <param name="isInherited">这对于命名的等待句柄没有意义，因为这些句柄没有层次结构。</param>
    /// <param name="inheritanceFlags">这对于命名的等待句柄没有意义，因为这些句柄没有层次结构。</param>
    /// <param name="propagationFlags">这对于命名的等待句柄没有意义，因为这些句柄没有层次结构。</param>
    /// <param name="type">
    /// <see cref="T:System.Security.AccessControl.AccessControlType" /> 值之一，用于指定是允许还是拒绝相应权限。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="accessMask" />、<paramref name="inheritanceFlags" />、<paramref name="propagationFlags" /> 或 <paramref name="type" /> 指定了一个无效值。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="identityReference" /> 为 null。- 或 -<paramref name="accessMask" /> 是零。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="identityReference" /> 既不属于类型 <see cref="T:System.Security.Principal.SecurityIdentifier" />，也不属于可以转换为 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 类型的类型（如 <see cref="T:System.Security.Principal.NTAccount" />）。</exception>
    public override AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
    {
      return (AccessRule) new EventWaitHandleAccessRule(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, type);
    }

    /// <summary>新建审核规则，指定规则应用到的用户、要审核的访问权限以及触发审核规则的结果。</summary>
    /// <returns>一个 <see cref="T:System.Security.AccessControl.EventWaitHandleAuditRule" /> 对象，表示指定用户的指定审核规则。该方法的返回类型是基类 <see cref="T:System.Security.AccessControl.AuditRule" />，但可以安全地将返回值强制转换为派生类。</returns>
    /// <param name="identityReference">一个 <see cref="T:System.Security.Principal.IdentityReference" />，用于标识此规则应用到的用户或组。</param>
    /// <param name="accessMask">
    /// <see cref="T:System.Security.AccessControl.EventWaitHandleRights" /> 值的按位组合，用于指定要审核的访问权限，该组合将被强制转换为整数。</param>
    /// <param name="isInherited">这对于命名的等待句柄没有意义，因为这些句柄没有层次结构。</param>
    /// <param name="inheritanceFlags">这对于命名的等待句柄没有意义，因为这些句柄没有层次结构。</param>
    /// <param name="propagationFlags">这对于命名的等待句柄没有意义，因为这些句柄没有层次结构。</param>
    /// <param name="flags">
    /// <see cref="T:System.Security.AccessControl.AuditFlags" /> 值的按位组合，它指定是审核成功的访问、失败的访问还是对这两种情况都进行审核。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="accessMask" />、<paramref name="inheritanceFlags" />、<paramref name="propagationFlags" /> 或 <paramref name="flags" /> 指定了一个无效值。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="identityReference" /> 为 null。- 或 -<paramref name="accessMask" /> 是零。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="identityReference" /> 既不属于类型 <see cref="T:System.Security.Principal.SecurityIdentifier" />，也不属于可以转换为 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 类型的类型（如 <see cref="T:System.Security.Principal.NTAccount" />）。</exception>
    public override AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
    {
      return (AuditRule) new EventWaitHandleAuditRule(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, flags);
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
    internal void Persist(SafeWaitHandle handle)
    {
      this.WriteLock();
      try
      {
        AccessControlSections sectionsFromChanges = this.GetAccessControlSectionsFromChanges();
        if (sectionsFromChanges == AccessControlSections.None)
          return;
        this.Persist((SafeHandle) handle, sectionsFromChanges);
        this.OwnerModified = this.GroupModified = this.AuditRulesModified = this.AccessRulesModified = false;
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>搜索可以将新规则与之合并的匹配访问控制规则。如果未找到符合条件的规则，则添加新规则。</summary>
    /// <param name="rule">要添加的访问控制规则。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="rule" /> 为 null。</exception>
    public void AddAccessRule(EventWaitHandleAccessRule rule)
    {
      this.AddAccessRule((AccessRule) rule);
    }

    /// <summary>移除与指定的规则具有相同用户和 <see cref="T:System.Security.AccessControl.AccessControlType" />（允许或拒绝）的所有控制规则，然后添加指定的规则。</summary>
    /// <param name="rule">要相加的 <see cref="T:System.Security.AccessControl.EventWaitHandleAccessRule" />。由此规则的用户和 <see cref="T:System.Security.AccessControl.AccessControlType" /> 确定在添加此规则之前要移除的规则。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="rule" /> 为 null。</exception>
    public void SetAccessRule(EventWaitHandleAccessRule rule)
    {
      this.SetAccessRule((AccessRule) rule);
    }

    /// <summary>不论 <see cref="T:System.Security.AccessControl.AccessControlType" /> 如何，移除与指定的规则具有相同用户的所有访问控制规则，然后添加指定的规则。</summary>
    /// <param name="rule">要相加的 <see cref="T:System.Security.AccessControl.EventWaitHandleAccessRule" />。由此规则指定的用户确定在添加此规则之前要移除的规则。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="rule" /> 为 null。</exception>
    public void ResetAccessRule(EventWaitHandleAccessRule rule)
    {
      this.ResetAccessRule((AccessRule) rule);
    }

    /// <summary>搜索如下的访问控制规则：与指定的访问规则具有相同的用户和 <see cref="T:System.Security.AccessControl.AccessControlType" />（允许或拒绝），并具有兼容的继承和传播标志；如果找到，则从中移除指定访问规则中包含的权限。</summary>
    /// <returns>如果找到一个兼容规则，则为 true；否则为 false。</returns>
    /// <param name="rule">指定要搜索的用户和 <see cref="T:System.Security.AccessControl.AccessControlType" /> 的 <see cref="T:System.Security.AccessControl.EventWaitHandleAccessRule" />，以及匹配规则（如果找到）必须兼容的一组继承和传播标志。指定要从兼容规则移除的权限（如果找到）。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="rule" /> 为 null。</exception>
    public bool RemoveAccessRule(EventWaitHandleAccessRule rule)
    {
      return this.RemoveAccessRule((AccessRule) rule);
    }

    /// <summary>搜索与指定的规则具有相同用户和 <see cref="T:System.Security.AccessControl.AccessControlType" />（允许或拒绝）的所有访问控制规则，如果找到则将其移除。</summary>
    /// <param name="rule">一个 <see cref="T:System.Security.AccessControl.EventWaitHandleAccessRule" />，指定要搜索的用户和 <see cref="T:System.Security.AccessControl.AccessControlType" />。此规则指定的任何权限都被忽略。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="rule" /> 为 null。</exception>
    public void RemoveAccessRuleAll(EventWaitHandleAccessRule rule)
    {
      this.RemoveAccessRuleAll((AccessRule) rule);
    }

    /// <summary>搜索与指定的规则完全匹配的访问控制规则，如果找到则将其移除。</summary>
    /// <param name="rule">要移除的 <see cref="T:System.Security.AccessControl.EventWaitHandleAccessRule" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="rule" /> 为 null。</exception>
    public void RemoveAccessRuleSpecific(EventWaitHandleAccessRule rule)
    {
      this.RemoveAccessRuleSpecific((AccessRule) rule);
    }

    /// <summary>搜索可以将新规则与之合并的审核规则。如果未找到符合条件的规则，则添加新规则。</summary>
    /// <param name="rule">要添加的审核规则。由此规则指定的用户来确定搜索。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="rule" /> 为 null。</exception>
    public void AddAuditRule(EventWaitHandleAuditRule rule)
    {
      this.AddAuditRule((AuditRule) rule);
    }

    /// <summary>不论 <see cref="T:System.Security.AccessControl.AuditFlags" /> 的值如何，移除与指定的规则具有相同用户的所有审核规则，然后添加指定的规则。</summary>
    /// <param name="rule">要相加的 <see cref="T:System.Security.AccessControl.EventWaitHandleAuditRule" />。由此规则指定的用户确定在添加此规则之前要移除的规则。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="rule" /> 为 null。</exception>
    public void SetAuditRule(EventWaitHandleAuditRule rule)
    {
      this.SetAuditRule((AuditRule) rule);
    }

    /// <summary>搜索以下的审核规则：与指定的规则具有相同的用户，并具有兼容的继承和传播标志；如果找到兼容规则，则从中移除指定的规则中包含的权限。</summary>
    /// <returns>如果找到一个兼容规则，则为 true；否则为 false。</returns>
    /// <param name="rule">一个 <see cref="T:System.Security.AccessControl.EventWaitHandleAuditRule" />，指定要搜索的用户以及匹配规则（如果找到）必须兼容的一组继承和传播标志。指定要从兼容规则移除的权限（如果找到）。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="rule" /> 为 null。</exception>
    public bool RemoveAuditRule(EventWaitHandleAuditRule rule)
    {
      return this.RemoveAuditRule((AuditRule) rule);
    }

    /// <summary>搜索所有使用相同用户作为指定规则的审核规则，如果找到符合条件的规则，则移除它们。</summary>
    /// <param name="rule">指定要搜索的用户的 <see cref="T:System.Security.AccessControl.EventWaitHandleAuditRule" />。此规则指定的任何权限都被忽略。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="rule" /> 为 null。</exception>
    public void RemoveAuditRuleAll(EventWaitHandleAuditRule rule)
    {
      this.RemoveAuditRuleAll((AuditRule) rule);
    }

    /// <summary>搜索与指定的规则完全匹配的审核规则；如果找到，则移除这些规则。</summary>
    /// <param name="rule">要移除的 <see cref="T:System.Security.AccessControl.EventWaitHandleAuditRule" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="rule" /> 为 null。</exception>
    public void RemoveAuditRuleSpecific(EventWaitHandleAuditRule rule)
    {
      this.RemoveAuditRuleSpecific((AuditRule) rule);
    }
  }
}
