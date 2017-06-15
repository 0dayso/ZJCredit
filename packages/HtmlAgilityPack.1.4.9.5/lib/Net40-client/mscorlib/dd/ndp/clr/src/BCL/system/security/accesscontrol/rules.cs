// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.AccessRule
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>表示用户的标识、访问掩码和访问控制类型（允许或拒绝）的组合。<see cref="T:System.Security.AccessControl.AccessRule" /> 对象还包含有关子对象如何继承规则以及如何传播继承的信息。</summary>
  public abstract class AccessRule : AuthorizationRule
  {
    private readonly AccessControlType _type;

    /// <summary>获取与此 <see cref="T:System.Security.AccessControl.AccessRule" /> 对象关联的 <see cref="T:System.Security.AccessControl.AccessControlType" /> 对象。</summary>
    /// <returns>与此 <see cref="T:System.Security.AccessControl.AccessRule" /> 对象关联的 <see cref="T:System.Security.AccessControl.AccessControlType" /> 对象。</returns>
    public AccessControlType AccessControlType
    {
      get
      {
        return this._type;
      }
    }

    /// <summary>使用指定的值初始化 <see cref="T:System.Security.AccessControl.AccessRule" /> 类的一个新实例。</summary>
    /// <param name="identity">应用访问规则的标识。此参数必须是可以强制转换为 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 的对象。</param>
    /// <param name="accessMask">此规则的访问掩码。访问掩码是一个 32 位的匿名位集合，其含义是由每个集成器定义的。</param>
    /// <param name="isInherited">如果此规则继承自父容器，则为 true。</param>
    /// <param name="inheritanceFlags">访问规则的继承属性。</param>
    /// <param name="propagationFlags">继承的访问规则是否自动传播。如果 <paramref name="inheritanceFlags" /> 设置为 <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />，则将忽略传播标志。</param>
    /// <param name="type">有效的访问控制类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="identity" /> 参数的值不能强制转换为 <see cref="T:System.Security.Principal.SecurityIdentifier" />，或者 <paramref name="type" /> 参数包含无效值。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="accessMask" /> 参数的值为零，或者 <paramref name="inheritanceFlags" /> 或 <paramref name="propagationFlags" /> 参数包含无法识别的标志值。</exception>
    protected AccessRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
      : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags)
    {
      if (type != AccessControlType.Allow && type != AccessControlType.Deny)
        throw new ArgumentOutOfRangeException("type", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
      if (inheritanceFlags < InheritanceFlags.None || inheritanceFlags > (InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit))
        throw new ArgumentOutOfRangeException("inheritanceFlags", Environment.GetResourceString("Argument_InvalidEnumValue", (object) inheritanceFlags, (object) "InheritanceFlags"));
      if (propagationFlags < PropagationFlags.None || propagationFlags > (PropagationFlags.NoPropagateInherit | PropagationFlags.InheritOnly))
        throw new ArgumentOutOfRangeException("propagationFlags", Environment.GetResourceString("Argument_InvalidEnumValue", (object) inheritanceFlags, (object) "PropagationFlags"));
      this._type = type;
    }
  }
}
