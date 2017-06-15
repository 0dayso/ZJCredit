// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.MutexAccessRule
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>表示一组允许或拒绝用户或组进行访问的权限。此类不能被继承。</summary>
  public sealed class MutexAccessRule : AccessRule
  {
    /// <summary>获取访问规则允许或拒绝的权限。</summary>
    /// <returns>
    /// <see cref="T:System.Security.AccessControl.MutexRights" /> 值的按位组合，它指示访问规则允许或拒绝的权限。</returns>
    public MutexRights MutexRights
    {
      get
      {
        return (MutexRights) this.AccessMask;
      }
    }

    /// <summary>初始化 <see cref="T:System.Security.AccessControl.MutexAccessRule" /> 类的新实例，指定此规则应用到的用户或组、访问权限以及是否允许或拒绝指定的访问权限。</summary>
    /// <param name="identity">此规则应用到的用户或组。必须为 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 类型，或可以转换为 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 类型的类型，如 <see cref="T:System.Security.Principal.NTAccount" />。</param>
    /// <param name="eventRights">
    /// <see cref="T:System.Security.AccessControl.MutexRights" /> 值的按位组合，它指定允许或拒绝的权限。</param>
    /// <param name="type">
    /// <see cref="T:System.Security.AccessControl.AccessControlType" /> 值之一，用于指定是允许还是拒绝相应权限。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="eventRights" /> 指定了一个无效值。- 或 -<paramref name="type" /> 指定了一个无效值。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="identity" /> 为 null。- 或 -<paramref name="eventRights" /> 是零。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="identity" /> 既不属于类型 <see cref="T:System.Security.Principal.SecurityIdentifier" />，也不属于可以转换为 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 类型的类型（如 <see cref="T:System.Security.Principal.NTAccount" />）。</exception>
    public MutexAccessRule(IdentityReference identity, MutexRights eventRights, AccessControlType type)
      : this(identity, (int) eventRights, false, InheritanceFlags.None, PropagationFlags.None, type)
    {
    }

    /// <summary>初始化 <see cref="T:System.Security.AccessControl.MutexAccessRule" /> 类的新实例，指定应用此规则的用户或组的名称、访问权限以及是否允许或拒绝指定的访问权限。</summary>
    /// <param name="identity">应用此规则的用户或组的名称。</param>
    /// <param name="eventRights">
    /// <see cref="T:System.Security.AccessControl.MutexRights" /> 值的按位组合，它指定允许或拒绝的权限。</param>
    /// <param name="type">
    /// <see cref="T:System.Security.AccessControl.AccessControlType" /> 值之一，用于指定是允许还是拒绝相应权限。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="eventRights" /> 指定了一个无效值。- 或 -<paramref name="type" /> 指定了一个无效值。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="eventRights" /> 是零。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="identity" /> 为 null。- 或 -<paramref name="identity" /> 是零长度字符串。- 或 -<paramref name="identity" /> 的长度超过 512 个字符。</exception>
    public MutexAccessRule(string identity, MutexRights eventRights, AccessControlType type)
      : this((IdentityReference) new NTAccount(identity), (int) eventRights, false, InheritanceFlags.None, PropagationFlags.None, type)
    {
    }

    internal MutexAccessRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
      : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, type)
    {
    }
  }
}
