// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.RegistryAuditRule
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>表示要为用户或组审核的一组访问权限。此类不能被继承。</summary>
  public sealed class RegistryAuditRule : AuditRule
  {
    /// <summary>获取受此审核规则影响的访问权限。</summary>
    /// <returns>
    /// <see cref="T:System.Security.AccessControl.RegistryRights" /> 值的按位组合，它指示受此审核规则影响的权限。</returns>
    public RegistryRights RegistryRights
    {
      get
      {
        return (RegistryRights) this.AccessMask;
      }
    }

    /// <summary>初始化 <see cref="T:System.Security.AccessControl.RegistryAuditRule" /> 类的新实例，指定要审核的用户或组，要审核的权限，是否考虑继承以及是否审核成功和（或）失败。</summary>
    /// <param name="identity">此规则应用到的用户或组。必须为 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 类型，或可以转换为 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 类型的类型，如 <see cref="T:System.Security.Principal.NTAccount" />。</param>
    /// <param name="registryRights">
    /// <see cref="T:System.Security.AccessControl.RegistryRights" /> 值的按位组合，它指定要审核的访问类型。</param>
    /// <param name="inheritanceFlags">
    /// <see cref="T:System.Security.AccessControl.InheritanceFlags" /> 值的按位组合，它指定审核规则是否应用于当前注册表项的子项。</param>
    /// <param name="propagationFlags">
    /// <see cref="T:System.Security.AccessControl.PropagationFlags" /> 值的按位组合，它将影响将继承的审核规则传播到当前注册表项的子项的方式。</param>
    /// <param name="flags">
    /// <see cref="T:System.Security.AccessControl.AuditFlags" /> 值的按位组合，它指定审核是成功、失败还是包括这两种情况。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="eventRights" /> 指定了一个无效值。- 或 -<paramref name="flags" /> 指定了一个无效值。- 或 -<paramref name="inheritanceFlags" /> 指定了一个无效值。- 或 -<paramref name="propagationFlags" /> 指定了一个无效值。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="identity" /> 为 null。- 或 -<paramref name="registryRights" /> 是零。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="identity" /> 既不属于类型 <see cref="T:System.Security.Principal.SecurityIdentifier" />，也不属于可以转换为 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 类型的类型（如 <see cref="T:System.Security.Principal.NTAccount" />）。</exception>
    public RegistryAuditRule(IdentityReference identity, RegistryRights registryRights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
      : this(identity, (int) registryRights, false, inheritanceFlags, propagationFlags, flags)
    {
    }

    /// <summary>初始化 <see cref="T:System.Security.AccessControl.RegistryAuditRule" /> 类的新实例，指定要审核的用户或组的名称，要审核的权限，是否考虑继承以及是否审核成功和（或）失败。</summary>
    /// <param name="identity">应用此规则的用户或组的名称。</param>
    /// <param name="registryRights">
    /// <see cref="T:System.Security.AccessControl.RegistryRights" /> 值的按位组合，它指定要审核的访问类型。</param>
    /// <param name="inheritanceFlags">
    /// <see cref="T:System.Security.AccessControl.InheritanceFlags" /> 标志的按位组合，它指定审核规则是否应用于当前注册表项的子项。</param>
    /// <param name="propagationFlags">
    /// <see cref="T:System.Security.AccessControl.PropagationFlags" /> 标志的按位组合，它将影响将继承的审核规则传播到当前注册表项的子项的方式。</param>
    /// <param name="flags">
    /// <see cref="T:System.Security.AccessControl.AuditFlags" /> 值的按位组合，它指定审核是成功、失败还是包括这两种情况。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="eventRights" /> 指定了一个无效值。- 或 -<paramref name="flags" /> 指定了一个无效值。- 或 -<paramref name="inheritanceFlags" /> 指定了一个无效值。- 或 -<paramref name="propagationFlags" /> 指定了一个无效值。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="registryRights" /> 是零。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="identity" /> 为 null。- 或 -<paramref name="identity" /> 是零长度字符串。- 或 -<paramref name="identity" /> 的长度超过 512 个字符。</exception>
    public RegistryAuditRule(string identity, RegistryRights registryRights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
      : this((IdentityReference) new NTAccount(identity), (int) registryRights, false, inheritanceFlags, propagationFlags, flags)
    {
    }

    internal RegistryAuditRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
      : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, flags)
    {
    }
  }
}
