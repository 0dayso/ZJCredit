// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.AuthorizationRule
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>确定对可保护对象的访问权限。派生类 <see cref="T:System.Security.AccessControl.AccessRule" /> 和 <see cref="T:System.Security.AccessControl.AuditRule" /> 提供了用于访问功能和审核功能的规范。</summary>
  public abstract class AuthorizationRule
  {
    private readonly IdentityReference _identity;
    private readonly int _accessMask;
    private readonly bool _isInherited;
    private readonly InheritanceFlags _inheritanceFlags;
    private readonly PropagationFlags _propagationFlags;

    /// <summary>获取此规则应用到的 <see cref="T:System.Security.Principal.IdentityReference" />。</summary>
    /// <returns>此规则应用到的 <see cref="T:System.Security.Principal.IdentityReference" />。</returns>
    public IdentityReference IdentityReference
    {
      get
      {
        return this._identity;
      }
    }

    /// <summary>获取此规则的访问掩码。</summary>
    /// <returns>此规则的访问掩码。</returns>
    protected internal int AccessMask
    {
      get
      {
        return this._accessMask;
      }
    }

    /// <summary>获取指示此规则是显式设置的还是从父容器对象继承的值。</summary>
    /// <returns>如果此规则不是显式设置的，而是从父容器继承的，则为 true。</returns>
    public bool IsInherited
    {
      get
      {
        return this._isInherited;
      }
    }

    /// <summary>获取确定子对象如何继承此规则的标志值。</summary>
    /// <returns>枚举值的按位组合。</returns>
    public InheritanceFlags InheritanceFlags
    {
      get
      {
        return this._inheritanceFlags;
      }
    }

    /// <summary>获取传播标志的值，该值确定如何将此规则的继承传播到子对象。仅当 <see cref="T:System.Security.AccessControl.InheritanceFlags" /> 枚举的值不为 <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />，此属性才有意义。</summary>
    /// <returns>枚举值的按位组合。</returns>
    public PropagationFlags PropagationFlags
    {
      get
      {
        return this._propagationFlags;
      }
    }

    /// <summary>使用指定的值初始化 <see cref="T:System.Security.AuthorizationControl.AccessRule" /> 类的一个新实例。</summary>
    /// <param name="identity">应用访问规则的标识。此参数必须是可以强制转换为 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 的对象。</param>
    /// <param name="accessMask">此规则的访问掩码。访问掩码是一个 32 位的匿名位集合，其含义是由每个集成器定义的。</param>
    /// <param name="isInherited">若从父容器继承此规则，则为 true。</param>
    /// <param name="inheritanceFlags">访问规则的继承属性。</param>
    /// <param name="propagationFlags">继承的访问规则是否自动传播。如果 <paramref name="inheritanceFlags" /> 设置为 <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />，则将忽略传播标志。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="identity" /> 参数的值不能强制转换为 <see cref="T:System.Security.Principal.SecurityIdentifier" />。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="accessMask" /> 参数的值为零，或者 <paramref name="inheritanceFlags" /> 或 <paramref name="propagationFlags" /> 参数包含无法识别的标志值。</exception>
    protected internal AuthorizationRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
    {
      if (identity == (IdentityReference) null)
        throw new ArgumentNullException("identity");
      if (accessMask == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_ArgumentZero"), "accessMask");
      if (inheritanceFlags < InheritanceFlags.None || inheritanceFlags > (InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit))
        throw new ArgumentOutOfRangeException("inheritanceFlags", Environment.GetResourceString("Argument_InvalidEnumValue", (object) inheritanceFlags, (object) "InheritanceFlags"));
      if (propagationFlags < PropagationFlags.None || propagationFlags > (PropagationFlags.NoPropagateInherit | PropagationFlags.InheritOnly))
        throw new ArgumentOutOfRangeException("propagationFlags", Environment.GetResourceString("Argument_InvalidEnumValue", (object) inheritanceFlags, (object) "PropagationFlags"));
      if (!identity.IsValidTargetType(typeof (SecurityIdentifier)))
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeIdentityReferenceType"), "identity");
      this._identity = identity;
      this._accessMask = accessMask;
      this._isInherited = isInherited;
      this._inheritanceFlags = inheritanceFlags;
      if (inheritanceFlags != InheritanceFlags.None)
        this._propagationFlags = propagationFlags;
      else
        this._propagationFlags = PropagationFlags.None;
    }
  }
}
