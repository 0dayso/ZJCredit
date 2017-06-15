// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.RC2
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>表示 <see cref="T:System.Security.Cryptography.RC2" /> 算法的所有实现都必须从中派生的基类。</summary>
  [ComVisible(true)]
  public abstract class RC2 : SymmetricAlgorithm
  {
    private static KeySizes[] s_legalBlockSizes = new KeySizes[1]{ new KeySizes(64, 64, 0) };
    private static KeySizes[] s_legalKeySizes = new KeySizes[1]{ new KeySizes(40, 1024, 8) };
    /// <summary>表示 <see cref="T:System.Security.Cryptography.RC2" /> 算法使用的机密密钥的有效大小（以位为单位）。</summary>
    protected int EffectiveKeySizeValue;

    /// <summary>获取或设置 <see cref="T:System.Security.Cryptography.RC2" /> 算法使用的机密密钥的有效大小（以位为单位）。</summary>
    /// <returns>
    /// <see cref="T:System.Security.Cryptography.RC2" /> 算法使用的有效密钥大小。</returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">有效密钥大小无效。</exception>
    public virtual int EffectiveKeySize
    {
      get
      {
        if (this.EffectiveKeySizeValue == 0)
          return this.KeySizeValue;
        return this.EffectiveKeySizeValue;
      }
      set
      {
        if (value > this.KeySizeValue)
          throw new CryptographicException(Environment.GetResourceString("Cryptography_RC2_EKSKS"));
        if (value == 0)
        {
          this.EffectiveKeySizeValue = value;
        }
        else
        {
          if (value < 40)
            throw new CryptographicException(Environment.GetResourceString("Cryptography_RC2_EKS40"));
          if (!this.ValidKeySize(value))
            throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKeySize"));
          this.EffectiveKeySizeValue = value;
        }
      }
    }

    /// <summary>获取或设置 <see cref="T:System.Security.Cryptography.RC2" /> 算法使用的机密密钥的大小（以位为单位）。</summary>
    /// <returns>
    /// <see cref="T:System.Security.Cryptography.RC2" /> 算法使用的机密密钥的大小。</returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">RC2 密钥大小的值小于有效密钥大小值。</exception>
    public override int KeySize
    {
      get
      {
        return this.KeySizeValue;
      }
      set
      {
        if (value < this.EffectiveKeySizeValue)
          throw new CryptographicException(Environment.GetResourceString("Cryptography_RC2_EKSKS"));
        base.KeySize = value;
      }
    }

    /// <summary>初始化 <see cref="T:System.Security.Cryptography.RC2" /> 的新实例。</summary>
    protected RC2()
    {
      this.KeySizeValue = 128;
      this.BlockSizeValue = 64;
      this.FeedbackSizeValue = this.BlockSizeValue;
      this.LegalBlockSizesValue = RC2.s_legalBlockSizes;
      this.LegalKeySizesValue = RC2.s_legalKeySizes;
    }

    /// <summary>创建加密对象的实例以执行 <see cref="T:System.Security.Cryptography.RC2" /> 算法。</summary>
    /// <returns>加密对象的实例。</returns>
    /// <exception cref="T:System.Reflection.TargetInvocationException">该算法在使用中启用了联邦信息处理标准 (FIPS) 模式，但与 FIPS 不兼容。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public static RC2 Create()
    {
      return RC2.Create("System.Security.Cryptography.RC2");
    }

    /// <summary>创建加密对象的实例以执行 <see cref="T:System.Security.Cryptography.RC2" /> 算法的指定实现。</summary>
    /// <returns>加密对象的实例。</returns>
    /// <param name="AlgName">要使用的 <see cref="T:System.Security.Cryptography.RC2" /> 的特定实现的名称。</param>
    /// <exception cref="T:System.Reflection.TargetInvocationException">由 <paramref name="algName" /> 参数描述的算法在使用中已启用联邦信息处理标准 (FIPS) 模式，但与 FIPS 不兼容。</exception>
    public static RC2 Create(string AlgName)
    {
      return (RC2) CryptoConfig.CreateFromName(AlgName);
    }
  }
}
