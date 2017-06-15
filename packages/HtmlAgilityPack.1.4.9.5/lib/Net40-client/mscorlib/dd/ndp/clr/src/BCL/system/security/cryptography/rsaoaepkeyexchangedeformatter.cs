// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.RSAOAEPKeyExchangeDeformatter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>对最优不对称加密填充 (OAEP) 密钥交换数据进行解密。</summary>
  [ComVisible(true)]
  public class RSAOAEPKeyExchangeDeformatter : AsymmetricKeyExchangeDeformatter
  {
    private RSA _rsaKey;

    /// <summary>获取最优不对称加密填充 (OAEP) 密钥交换的参数。</summary>
    /// <returns>XML 字符串，包含 OAEP 密钥交换操作的参数。</returns>
    public override string Parameters
    {
      get
      {
        return (string) null;
      }
      set
      {
      }
    }

    /// <summary>初始化 <see cref="T:System.Security.Cryptography.RSAOAEPKeyExchangeDeformatter" /> 类的新实例。</summary>
    public RSAOAEPKeyExchangeDeformatter()
    {
    }

    /// <summary>使用指定的密钥初始化 <see cref="T:System.Security.Cryptography.RSAOAEPKeyExchangeDeformatter" /> 类的新实例。</summary>
    /// <param name="key">包含私钥的 <see cref="T:System.Security.Cryptography.RSA" /> 算法的实例。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key " />为 null。</exception>
    public RSAOAEPKeyExchangeDeformatter(AsymmetricAlgorithm key)
    {
      if (key == null)
        throw new ArgumentNullException("key");
      this._rsaKey = (RSA) key;
    }

    /// <summary>从加密密钥交换数据中提取机密信息。</summary>
    /// <returns>从密钥交换数据导出的机密信息。</returns>
    /// <param name="rgbData">其中隐藏有机密信息的密钥交换数据。</param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">密钥交换数据验证已失败。</exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">缺少密钥。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public override byte[] DecryptKeyExchange(byte[] rgbData)
    {
      if (this._rsaKey == null)
        throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_MissingKey"));
      if (this._rsaKey is RSACryptoServiceProvider)
        return ((RSACryptoServiceProvider) this._rsaKey).Decrypt(rgbData, true);
      return Utils.RsaOaepDecrypt(this._rsaKey, (HashAlgorithm) SHA1.Create(), new PKCS1MaskGenerationMethod(), rgbData);
    }

    /// <summary>设置用于解密机密信息的私钥。</summary>
    /// <param name="key">包含私钥的 <see cref="T:System.Security.Cryptography.RSA" /> 算法的实例。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> 为 null。</exception>
    public override void SetKey(AsymmetricAlgorithm key)
    {
      if (key == null)
        throw new ArgumentNullException("key");
      this._rsaKey = (RSA) key;
    }
  }
}
