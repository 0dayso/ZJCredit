// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.AuditRule`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>表示用户的标识和访问掩码的组合。</summary>
  /// <typeparam name="T"></typeparam>
  public class AuditRule<T> : AuditRule where T : struct
  {
    /// <summary>审核规则的权限。</summary>
    /// <returns>返回 <see cref="{0}" />。</returns>
    public T Rights
    {
      get
      {
        return (T) (ValueType) this.AccessMask;
      }
    }

    /// <summary>使用指定的值初始化 AuditRule’1 类的一个新实例。</summary>
    /// <param name="identity">审核规则应用到的标识。</param>
    /// <param name="rights">审核规则的权限。</param>
    /// <param name="flags">审核规则的条件。</param>
    public AuditRule(IdentityReference identity, T rights, AuditFlags flags)
      : this(identity, rights, InheritanceFlags.None, PropagationFlags.None, flags)
    {
    }

    /// <summary>使用指定的值初始化 AuditRule’1 类的一个新实例。</summary>
    /// <param name="identity">审核规则应用到的标识。</param>
    /// <param name="rights">审核规则的权限。</param>
    /// <param name="inheritanceFlags">审核规则的继承属性。</param>
    /// <param name="propagationFlags">继承的审核规则是否自动传播。</param>
    /// <param name="flags">审核规则的条件。</param>
    public AuditRule(IdentityReference identity, T rights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
      : this(identity, (int) (ValueType) rights, false, inheritanceFlags, propagationFlags, flags)
    {
    }

    /// <summary>使用指定的值初始化 AuditRule’1 类的一个新实例。</summary>
    /// <param name="identity">审核规则应用到的标识。</param>
    /// <param name="rights">审核规则的权限。</param>
    /// <param name="flags">审核规则的属性。</param>
    public AuditRule(string identity, T rights, AuditFlags flags)
      : this((IdentityReference) new NTAccount(identity), rights, InheritanceFlags.None, PropagationFlags.None, flags)
    {
    }

    /// <summary>使用指定的值初始化 AuditRule’1 类的一个新实例。</summary>
    /// <param name="identity">审核规则应用到的标识。</param>
    /// <param name="rights">审核规则的权限。</param>
    /// <param name="inheritanceFlags">审核规则的继承属性。</param>
    /// <param name="propagationFlags">继承的审核规则是否自动传播。</param>
    /// <param name="flags">审核规则的条件。</param>
    public AuditRule(string identity, T rights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
      : this((IdentityReference) new NTAccount(identity), (int) (ValueType) rights, false, inheritanceFlags, propagationFlags, flags)
    {
    }

    internal AuditRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
      : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, flags)
    {
    }
  }
}
