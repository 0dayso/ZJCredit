// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.AuditRule
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>表示用户的标识和访问掩码的组合。<see cref="T:System.Security.AccessControl.AuditRule" /> 对象还包含有关子对象如何继承规则、继承如何传播以及规则的审核条件是什么的信息。</summary>
  public abstract class AuditRule : AuthorizationRule
  {
    private readonly AuditFlags _flags;

    /// <summary>获取此审核规则的审核标志。</summary>
    /// <returns>枚举值的按位组合。此组合为此审核规则指定审核条件。</returns>
    public AuditFlags AuditFlags
    {
      get
      {
        return this._flags;
      }
    }

    /// <summary>使用指定的值初始化 <see cref="T:System.Security.AccessControl.AuditRule" /> 类的一个新实例。</summary>
    /// <param name="identity">审核规则应用到的标识。它必须是可强制转换为 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 的对象。</param>
    /// <param name="accessMask">此规则的访问掩码。访问掩码是一个 32 位的匿名位集合，其含义是由每个集成器定义的。</param>
    /// <param name="isInherited">若从父容器继承此规则，则为 true。</param>
    /// <param name="inheritanceFlags">审核规则的继承属性。</param>
    /// <param name="propagationFlags">继承的审核规则是否自动传播。如果 <paramref name="inheritanceFlags" /> 设置为 <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />，则将忽略传播标志。</param>
    /// <param name="auditFlags">审核规则的条件。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="identity" /> 参数的值不能强制转换为 <see cref="T:System.Security.Principal.SecurityIdentifier" />，否则 <paramref name="auditFlags" /> 参数包含无效值。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="accessMask" /> 参数的值为零，或者 <paramref name="inheritanceFlags" /> 或 <paramref name="propagationFlags" /> 参数包含无法识别的标志值。</exception>
    protected AuditRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags auditFlags)
      : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags)
    {
      if (auditFlags == AuditFlags.None)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumAtLeastOneFlag"), "auditFlags");
      if ((auditFlags & ~(AuditFlags.Success | AuditFlags.Failure)) != AuditFlags.None)
        throw new ArgumentOutOfRangeException("auditFlags", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
      this._flags = auditFlags;
    }
  }
}
