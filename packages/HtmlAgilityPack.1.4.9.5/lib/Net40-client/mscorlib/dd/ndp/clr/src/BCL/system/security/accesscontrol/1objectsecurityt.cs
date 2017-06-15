// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.AccessRule`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>表示用户的标识、访问掩码和访问控制类型（允许或拒绝）的组合。AccessRule`1 对象还包含有关子对象如何继承规则以及如何传播继承的信息。</summary>
  /// <typeparam name="T">访问规则的访问权限类型。</typeparam>
  public class AccessRule<T> : AccessRule where T : struct
  {
    /// <summary>获取当前实例的权限。</summary>
    /// <returns>当前实例的强制转换为类型 &lt;T&gt; 的权利。</returns>
    public T Rights
    {
      get
      {
        return (T) (ValueType) this.AccessMask;
      }
    }

    /// <summary>使用指定的值初始化 AccessRule’1 类的一个新实例。</summary>
    /// <param name="identity">应用访问规则的标识。</param>
    /// <param name="rights">访问规则的权限。</param>
    /// <param name="type">有效的访问控制类型。</param>
    public AccessRule(IdentityReference identity, T rights, AccessControlType type)
      : this(identity, (int) (ValueType) rights, false, InheritanceFlags.None, PropagationFlags.None, type)
    {
    }

    /// <summary>使用指定的值初始化 AccessRule’1 类的一个新实例。</summary>
    /// <param name="identity">应用访问规则的标识。</param>
    /// <param name="rights">访问规则的权限。</param>
    /// <param name="type">有效的访问控制类型。</param>
    public AccessRule(string identity, T rights, AccessControlType type)
      : this((IdentityReference) new NTAccount(identity), (int) (ValueType) rights, false, InheritanceFlags.None, PropagationFlags.None, type)
    {
    }

    /// <summary>使用指定的值初始化 AccessRule’1 类的一个新实例。</summary>
    /// <param name="identity">应用访问规则的标识。</param>
    /// <param name="rights">访问规则的权限。</param>
    /// <param name="inheritanceFlags">访问规则的继承属性。</param>
    /// <param name="propagationFlags">继承的访问规则是否自动传播。如果 <paramref name="inheritanceFlags" /> 设置为 <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />，则将忽略传播标志。</param>
    /// <param name="type">有效的访问控制类型。</param>
    public AccessRule(IdentityReference identity, T rights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
      : this(identity, (int) (ValueType) rights, false, inheritanceFlags, propagationFlags, type)
    {
    }

    /// <summary>使用指定的值初始化 AccessRule’1 类的一个新实例。</summary>
    /// <param name="identity">应用访问规则的标识。</param>
    /// <param name="rights">访问规则的权限。</param>
    /// <param name="inheritanceFlags">访问规则的继承属性。</param>
    /// <param name="propagationFlags">继承的访问规则是否自动传播。如果 <paramref name="inheritanceFlags" /> 设置为 <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />，则将忽略传播标志。</param>
    /// <param name="type">有效的访问控制类型。</param>
    public AccessRule(string identity, T rights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
      : this((IdentityReference) new NTAccount(identity), (int) (ValueType) rights, false, inheritanceFlags, propagationFlags, type)
    {
    }

    internal AccessRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
      : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, type)
    {
    }
  }
}
