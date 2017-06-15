// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.RC2CryptoServiceProvider
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>定义访问 <see cref="T:System.Security.Cryptography.RC2" /> 算法的加密服务提供程序 (CSP) 实现的包装对象。此类不能被继承。</summary>
  [ComVisible(true)]
  public sealed class RC2CryptoServiceProvider : RC2
  {
    private static KeySizes[] s_legalKeySizes = new KeySizes[1]{ new KeySizes(40, 128, 8) };
    private bool m_use40bitSalt;

    /// <summary>获取或设置 <see cref="T:System.Security.Cryptography.RC2" /> 算法所用密钥的有效大小（以位为单位）。</summary>
    /// <returns>
    /// <see cref="T:System.Security.Cryptography.RC2" /> 算法使用的有效密钥大小（以位为单位）。</returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">
    /// <see cref="P:System.Security.Cryptography.RC2CryptoServiceProvider.EffectiveKeySize" /> 属性被设置为 <see cref="F:System.Security.Cryptography.SymmetricAlgorithm.KeySizeValue" /> 属性以外的值。</exception>
    public override int EffectiveKeySize
    {
      get
      {
        return this.KeySizeValue;
      }
      set
      {
        if (value != this.KeySizeValue)
          throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_RC2_EKSKS2"));
      }
    }

    /// <summary>获取或设置一个值，该值确定是否创建一个具有 11 字节长的零值 salt 的密钥。</summary>
    /// <returns>如果应该创建具有 11 字节长的零值 salt 的密钥，则为 true；否则为 false。默认值为 false。</returns>
    [ComVisible(false)]
    public bool UseSalt
    {
      get
      {
        return this.m_use40bitSalt;
      }
      set
      {
        this.m_use40bitSalt = value;
      }
    }

    /// <summary>初始化 <see cref="T:System.Security.Cryptography.RC2CryptoServiceProvider" /> 类的新实例。</summary>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">无法获取加密服务提供程序 (CSP)。</exception>
    /// <exception cref="T:System.InvalidOperationException">找到不兼容的 FIPS 算法。</exception>
    [SecuritySafeCritical]
    public RC2CryptoServiceProvider()
    {
      if (CryptoConfig.AllowOnlyFipsAlgorithms)
        throw new InvalidOperationException(Environment.GetResourceString("Cryptography_NonCompliantFIPSAlgorithm"));
      if (!Utils.HasAlgorithm(26114, 0))
        throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_AlgorithmNotAvailable"));
      this.LegalKeySizesValue = RC2CryptoServiceProvider.s_legalKeySizes;
      this.FeedbackSizeValue = 8;
    }

    /// <summary>用指定的密钥 (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" />) 和初始化向量 (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />) 创建对称的 <see cref="T:System.Security.Cryptography.RC2" /> 加密器对象。</summary>
    /// <returns>对称 <see cref="T:System.Security.Cryptography.RC2" /> 加密器对象。</returns>
    /// <param name="rgbKey">用于对称算法的密钥。</param>
    /// <param name="rgbIV">用于对称算法的初始化向量。</param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">使用了 <see cref="F:System.Security.Cryptography.CipherMode.OFB" /> 密码模式。- 或 -使用了反馈大小不是 8 位的 <see cref="F:System.Security.Cryptography.CipherMode.CFB" /> 密码模式。- 或 -使用了无效的密钥大小。- 或 -算法密钥大小不可用。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV)
    {
      return this._NewEncryptor(rgbKey, this.ModeValue, rgbIV, this.EffectiveKeySizeValue, this.FeedbackSizeValue, CryptoAPITransformMode.Encrypt);
    }

    /// <summary>用指定的密钥 (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" />) 和初始化向量 (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />) 创建对称的 <see cref="T:System.Security.Cryptography.RC2" /> 解密器对象。</summary>
    /// <returns>对称 <see cref="T:System.Security.Cryptography.RC2" /> 解密器对象。</returns>
    /// <param name="rgbKey">用于对称算法的密钥。</param>
    /// <param name="rgbIV">用于对称算法的初始化向量。</param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">使用了 <see cref="F:System.Security.Cryptography.CipherMode.OFB" /> 密码模式。- 或 -使用了反馈大小不是 8 位的 <see cref="F:System.Security.Cryptography.CipherMode.CFB" /> 密码模式。- 或 -使用了无效的密钥大小。- 或 -算法密钥大小不可用。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV)
    {
      return this._NewEncryptor(rgbKey, this.ModeValue, rgbIV, this.EffectiveKeySizeValue, this.FeedbackSizeValue, CryptoAPITransformMode.Decrypt);
    }

    /// <summary>生成用于该算法的随机密钥 (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" />)。</summary>
    public override void GenerateKey()
    {
      this.KeyValue = new byte[this.KeySizeValue / 8];
      Utils.StaticRandomNumberGenerator.GetBytes(this.KeyValue);
    }

    /// <summary>生成用于该算法的随机初始化向量 (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />)。</summary>
    public override void GenerateIV()
    {
      this.IVValue = new byte[8];
      Utils.StaticRandomNumberGenerator.GetBytes(this.IVValue);
    }

    [SecurityCritical]
    private ICryptoTransform _NewEncryptor(byte[] rgbKey, CipherMode mode, byte[] rgbIV, int effectiveKeySize, int feedbackSize, CryptoAPITransformMode encryptMode)
    {
      int index = 0;
      int[] rgArgIds = new int[10];
      object[] rgArgValues = new object[10];
      if (mode == CipherMode.OFB)
        throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_OFBNotSupported"));
      if (mode == CipherMode.CFB && feedbackSize != 8)
        throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_CFBSizeNotSupported"));
      if (rgbKey == null)
      {
        rgbKey = new byte[this.KeySizeValue / 8];
        Utils.StaticRandomNumberGenerator.GetBytes(rgbKey);
      }
      int num = rgbKey.Length * 8;
      if (!this.ValidKeySize(num))
        throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKeySize"));
      rgArgIds[index] = 19;
      rgArgValues[index] = this.EffectiveKeySizeValue != 0 ? (object) effectiveKeySize : (object) num;
      int cArgs = index + 1;
      if (mode != CipherMode.CBC)
      {
        rgArgIds[cArgs] = 4;
        rgArgValues[cArgs] = (object) mode;
        ++cArgs;
      }
      if (mode != CipherMode.ECB)
      {
        if (rgbIV == null)
        {
          rgbIV = new byte[8];
          Utils.StaticRandomNumberGenerator.GetBytes(rgbIV);
        }
        if (rgbIV.Length < 8)
          throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidIVSize"));
        rgArgIds[cArgs] = 1;
        rgArgValues[cArgs] = (object) rgbIV;
        ++cArgs;
      }
      if (mode == CipherMode.OFB || mode == CipherMode.CFB)
      {
        rgArgIds[cArgs] = 5;
        rgArgValues[cArgs] = (object) feedbackSize;
        ++cArgs;
      }
      if (!Utils.HasAlgorithm(26114, num))
        throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_AlgKeySizeNotAvailable", (object) num));
      return (ICryptoTransform) new CryptoAPITransform(26114, cArgs, rgArgIds, rgArgValues, rgbKey, this.PaddingValue, mode, this.BlockSizeValue, feedbackSize, this.m_use40bitSalt, encryptMode);
    }
  }
}
