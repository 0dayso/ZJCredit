// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.ObjectAuditRule
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>表示用户的标识、访问掩码和审核条件的组合。<see cref="T:System.Security.AccessControl.ObjectAuditRule" /> 对象还包含有关规则所应用到的对象的类型、能够继承规则的子对象的类型、子对象如何继承该规则以及继承如何传播的信息。</summary>
  public abstract class ObjectAuditRule : AuditRule
  {
    private readonly Guid _objectType;
    private readonly Guid _inheritedObjectType;
    private readonly ObjectAceFlags _objectFlags;

    /// <summary>获取 <see cref="System.Security.AccessControl.ObjectAuditRule" /> 所应用到的对象的类型。</summary>
    /// <returns>
    /// <see cref="System.Security.AccessControl.ObjectAuditRule" /> 所应用到的对象的类型。</returns>
    public Guid ObjectType
    {
      get
      {
        return this._objectType;
      }
    }

    /// <summary>获取能够继承 <see cref="System.Security.AccessControl.ObjectAuditRule" /> 对象的子对象的类型。</summary>
    /// <returns>能够继承 <see cref="System.Security.AccessControl.ObjectAuditRule" /> 对象的子对象的类型。</returns>
    public Guid InheritedObjectType
    {
      get
      {
        return this._inheritedObjectType;
      }
    }

    /// <summary>
    /// <see cref="System.Security.AccessControl.ObjectAuditRule" /> 对象的 <see cref="P:System.Security.AccessControl.ObjectAuditRule.ObjectType" /> 和 <see cref="P:System.Security.AccessControl.ObjectAuditRule.InheritedObjectType" /> 属性包含有效值。</summary>
    /// <returns>
    /// <see cref="F:System.Security.AccessControl.ObjectAceFlags.ObjectAceTypePresent" /> 指定 <see cref="P:System.Security.AccessControl.ObjectAuditRule.ObjectType" /> 属性包含有效值。<see cref="F:System.Security.AccessControl.ObjectAceFlags.InheritedObjectAceTypePresent" /> 指定 <see cref="P:System.Security.AccessControl.ObjectAuditRule.InheritedObjectType" /> 属性包含有效值。这些值可以使用逻辑或运算进行组合。</returns>
    public ObjectAceFlags ObjectFlags
    {
      get
      {
        return this._objectFlags;
      }
    }

    /// <summary>初始化 <see cref="T:System.Security.AccessControl.ObjectAuditRule" /> 类的新实例。</summary>
    /// <param name="identity">应用访问规则的标识。它必须是可强制转换为 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 的对象。</param>
    /// <param name="accessMask">此规则的访问掩码。访问掩码是一个 32 位的匿名位集合，其含义是由每个集成器定义的。</param>
    /// <param name="isInherited">如果此规则继承自父容器，则为 true。</param>
    /// <param name="inheritanceFlags">指定访问规则的继承属性。</param>
    /// <param name="propagationFlags">继承的访问规则是否自动传播。如果 <paramref name="inheritanceFlags" /> 设置为 <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />，则将忽略传播标志。</param>
    /// <param name="objectType">应用此规则的对象的类型。</param>
    /// <param name="inheritedObjectType">能够继承此规则的子对象的类型。</param>
    /// <param name="auditFlags">审核条件。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="identity" /> 参数的值不能强制转换为 <see cref="T:System.Security.Principal.SecurityIdentifier" />，或者 <paramref name="type" /> 参数包含无效值。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="accessMask" /> 参数的值为零，或者 <paramref name="inheritanceFlags" /> 或 <paramref name="propagationFlags" /> 参数包含无法识别的标志值。</exception>
    protected ObjectAuditRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, Guid objectType, Guid inheritedObjectType, AuditFlags auditFlags)
      : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, auditFlags)
    {
      if (!objectType.Equals(Guid.Empty) && (accessMask & ObjectAce.AccessMaskWithObjectType) != 0)
      {
        this._objectType = objectType;
        this._objectFlags = this._objectFlags | ObjectAceFlags.ObjectAceTypePresent;
      }
      else
        this._objectType = Guid.Empty;
      if (!inheritedObjectType.Equals(Guid.Empty) && (inheritanceFlags & InheritanceFlags.ContainerInherit) != InheritanceFlags.None)
      {
        this._inheritedObjectType = inheritedObjectType;
        this._objectFlags = this._objectFlags | ObjectAceFlags.InheritedObjectAceTypePresent;
      }
      else
        this._inheritedObjectType = Guid.Empty;
    }
  }
}
