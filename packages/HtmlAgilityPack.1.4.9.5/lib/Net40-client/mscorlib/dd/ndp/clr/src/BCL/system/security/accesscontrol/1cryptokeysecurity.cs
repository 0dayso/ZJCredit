// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.CryptoKeyAuditRule
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>表示加密密钥的审核规则。审核规则表示用户的标识和访问掩码的组合。审核规则还包含有关子对象如何继承规则、如何传播继承以及规则的审核条件是什么的信息。</summary>
  public sealed class CryptoKeyAuditRule : AuditRule
  {
    /// <summary>获取此审核规则为其生成审核的加密密钥操作。</summary>
    /// <returns>此审核规则为其生成审核的加密密钥操作。</returns>
    public CryptoKeyRights CryptoKeyRights
    {
      get
      {
        return CryptoKeyAuditRule.RightsFromAccessMask(this.AccessMask);
      }
    }

    /// <summary>使用指定的值初始化 <see cref="T:System.Security.AccessControl.CryptoKeyAuditRule" /> 类的新实例。</summary>
    /// <param name="identity">审核规则应用到的标识。此参数必须是可以强制转换为 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 的对象。</param>
    /// <param name="cryptoKeyRights">此审核规则为其生成审核的加密密钥操作。</param>
    /// <param name="flags">生成审核的条件。</param>
    public CryptoKeyAuditRule(IdentityReference identity, CryptoKeyRights cryptoKeyRights, AuditFlags flags)
      : this(identity, CryptoKeyAuditRule.AccessMaskFromRights(cryptoKeyRights), false, InheritanceFlags.None, PropagationFlags.None, flags)
    {
    }

    /// <summary>使用指定的值初始化 <see cref="T:System.Security.AccessControl.CryptoKeyAuditRule" /> 类的新实例。</summary>
    /// <param name="identity">审核规则应用到的标识。</param>
    /// <param name="cryptoKeyRights">此审核规则为其生成审核的加密密钥操作。</param>
    /// <param name="flags">生成审核的条件。</param>
    public CryptoKeyAuditRule(string identity, CryptoKeyRights cryptoKeyRights, AuditFlags flags)
      : this((IdentityReference) new NTAccount(identity), CryptoKeyAuditRule.AccessMaskFromRights(cryptoKeyRights), false, InheritanceFlags.None, PropagationFlags.None, flags)
    {
    }

    private CryptoKeyAuditRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
      : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, flags)
    {
    }

    private static int AccessMaskFromRights(CryptoKeyRights cryptoKeyRights)
    {
      return (int) cryptoKeyRights;
    }

    internal static CryptoKeyRights RightsFromAccessMask(int accessMask)
    {
      return (CryptoKeyRights) accessMask;
    }
  }
}
