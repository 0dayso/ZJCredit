// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.EventWaitHandleAuditRule
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>表示要为用户或组审核的一组访问权限。此类不能被继承。</summary>
  public sealed class EventWaitHandleAuditRule : AuditRule
  {
    /// <summary>获取受此审核规则影响的访问权限。</summary>
    /// <returns>
    /// <see cref="T:System.Security.AccessControl.EventWaitHandleRights" /> 值的按位组合，它指示受此审核规则影响的权限。</returns>
    public EventWaitHandleRights EventWaitHandleRights
    {
      get
      {
        return (EventWaitHandleRights) this.AccessMask;
      }
    }

    /// <summary>初始化 <see cref="T:System.Security.AccessControl.EventWaitHandleAuditRule" /> 类的新实例，指定要审核的用户或组，要审核的权限，以及是否审核成功和（或）失败。</summary>
    /// <param name="identity">此规则应用到的用户或组。必须为 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 类型，或可以转换为 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 类型的类型，如 <see cref="T:System.Security.Principal.NTAccount" />。</param>
    /// <param name="eventRights">
    /// <see cref="T:System.Security.AccessControl.EventWaitHandleRights" /> 值的按位组合，它指定要审核的访问类型。</param>
    /// <param name="flags">
    /// <see cref="T:System.Security.AccessControl.AuditFlags" /> 值的按位组合，它指定审核是成功、失败还是包括这两种情况。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="eventRights" /> 指定了一个无效值。- 或 -<paramref name="flags" /> 指定了一个无效值。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="identity" /> 为 null。- 或 -<paramref name="eventRights" /> 是零。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="flags" /> 为 <see cref="F:System.Security.AccessControl.AuditFlags.None" />。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="identity" /> 既不属于类型 <see cref="T:System.Security.Principal.SecurityIdentifier" />，也不属于可以转换为 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 类型的类型（如 <see cref="T:System.Security.Principal.NTAccount" />）。</exception>
    public EventWaitHandleAuditRule(IdentityReference identity, EventWaitHandleRights eventRights, AuditFlags flags)
      : this(identity, (int) eventRights, false, InheritanceFlags.None, PropagationFlags.None, flags)
    {
    }

    internal EventWaitHandleAuditRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
      : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, flags)
    {
    }
  }
}
