// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.CryptoKeyAccessRule
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>表示加密密钥的访问规则。访问规则表示用户的标识、访问掩码和访问控制类型（允许或拒绝）的组合。访问规则对象还包含有关子对象如何继承规则以及如何传播继承的信息。</summary>
  public sealed class CryptoKeyAccessRule : AccessRule
  {
    /// <summary>获取由此访问规则控制其访问的加密密钥操作。</summary>
    /// <returns>由此访问规则控制其访问的加密密钥操作。</returns>
    public CryptoKeyRights CryptoKeyRights
    {
      get
      {
        return CryptoKeyAccessRule.RightsFromAccessMask(this.AccessMask);
      }
    }

    /// <summary>使用指定的值初始化 <see cref="T:System.Security.AccessControl.CryptoKeyAccessRule" /> 类的新实例。</summary>
    /// <param name="identity">应用访问规则的标识。此参数必须是可以强制转换为 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 的对象。</param>
    /// <param name="cryptoKeyRights">由此访问规则控制其访问的加密密钥操作。</param>
    /// <param name="type">有效的访问控制类型。</param>
    public CryptoKeyAccessRule(IdentityReference identity, CryptoKeyRights cryptoKeyRights, AccessControlType type)
      : this(identity, CryptoKeyAccessRule.AccessMaskFromRights(cryptoKeyRights, type), false, InheritanceFlags.None, PropagationFlags.None, type)
    {
    }

    /// <summary>使用指定的值初始化 <see cref="T:System.Security.AccessControl.CryptoKeyAccessRule" /> 类的新实例。</summary>
    /// <param name="identity">应用访问规则的标识。</param>
    /// <param name="cryptoKeyRights">由此访问规则控制其访问的加密密钥操作。</param>
    /// <param name="type">有效的访问控制类型。</param>
    public CryptoKeyAccessRule(string identity, CryptoKeyRights cryptoKeyRights, AccessControlType type)
      : this((IdentityReference) new NTAccount(identity), CryptoKeyAccessRule.AccessMaskFromRights(cryptoKeyRights, type), false, InheritanceFlags.None, PropagationFlags.None, type)
    {
    }

    private CryptoKeyAccessRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
      : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, type)
    {
    }

    private static int AccessMaskFromRights(CryptoKeyRights cryptoKeyRights, AccessControlType controlType)
    {
      if (controlType == AccessControlType.Allow)
        cryptoKeyRights |= CryptoKeyRights.Synchronize;
      else if (controlType == AccessControlType.Deny)
      {
        if (cryptoKeyRights != CryptoKeyRights.FullControl)
          cryptoKeyRights &= ~CryptoKeyRights.Synchronize;
      }
      else
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidEnumValue", (object) controlType, (object) "controlType"), "controlType");
      return (int) cryptoKeyRights;
    }

    internal static CryptoKeyRights RightsFromAccessMask(int accessMask)
    {
      return (CryptoKeyRights) accessMask;
    }
  }
}
