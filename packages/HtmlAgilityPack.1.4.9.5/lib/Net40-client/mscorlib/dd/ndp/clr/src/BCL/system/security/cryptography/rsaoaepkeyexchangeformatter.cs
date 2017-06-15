// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.RSAOAEPKeyExchangeFormatter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>使用 <see cref="T:System.Security.Cryptography.RSA" /> 创建最优不对称加密填充 (OAEP) 密钥交换数据。</summary>
  [ComVisible(true)]
  public class RSAOAEPKeyExchangeFormatter : AsymmetricKeyExchangeFormatter
  {
    private byte[] ParameterValue;
    private RSA _rsaKey;
    private RandomNumberGenerator RngValue;

    /// <summary>获取或设置用于在密钥交换创建过程中创建空白的参数。</summary>
    /// <returns>参数值。</returns>
    public byte[] Parameter
    {
      get
      {
        if (this.ParameterValue != null)
          return (byte[]) this.ParameterValue.Clone();
        return (byte[]) null;
      }
      set
      {
        if (value != null)
          this.ParameterValue = (byte[]) value.Clone();
        else
          this.ParameterValue = (byte[]) null;
      }
    }

    /// <summary>获取最优不对称加密填充 (OAEP) 密钥交换的参数。</summary>
    /// <returns>XML 字符串，包含 OAEP 密钥交换操作的参数。</returns>
    public override string Parameters
    {
      get
      {
        return (string) null;
      }
    }

    /// <summary>获取或设置要在密钥交换的创建中使用的随机数生成器算法。</summary>
    /// <returns>要使用的随机数生成器算法的实例。</returns>
    public RandomNumberGenerator Rng
    {
      get
      {
        return this.RngValue;
      }
      set
      {
        this.RngValue = value;
      }
    }

    /// <summary>初始化 <see cref="T:System.Security.Cryptography.RSAOAEPKeyExchangeFormatter" /> 类的新实例。</summary>
    public RSAOAEPKeyExchangeFormatter()
    {
    }

    /// <summary>使用指定的密钥初始化 <see cref="T:System.Security.Cryptography.RSAOAEPKeyExchangeFormatter" /> 类的新实例。</summary>
    /// <param name="key">包含公钥的 <see cref="T:System.Security.Cryptography.RSA" /> 算法的实例。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key " />为 null。</exception>
    public RSAOAEPKeyExchangeFormatter(AsymmetricAlgorithm key)
    {
      if (key == null)
        throw new ArgumentNullException("key");
      this._rsaKey = (RSA) key;
    }

    /// <summary>设置用于对密钥交换数据进行加密的公钥。</summary>
    /// <param name="key">包含公钥的 <see cref="T:System.Security.Cryptography.RSA" /> 算法的实例。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key " />为 null。</exception>
    public override void SetKey(AsymmetricAlgorithm key)
    {
      if (key == null)
        throw new ArgumentNullException("key");
      this._rsaKey = (RSA) key;
    }

    /// <summary>从指定的输入数据创建加密密钥交换数据。</summary>
    /// <returns>要发送给目标接收者的加密的密钥交换数据。</returns>
    /// <param name="rgbData">在密钥交换中要传递的机密信息。</param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">缺少密钥。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public override byte[] CreateKeyExchange(byte[] rgbData)
    {
      if (this._rsaKey == null)
        throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_MissingKey"));
      if (this._rsaKey is RSACryptoServiceProvider)
        return ((RSACryptoServiceProvider) this._rsaKey).Encrypt(rgbData, true);
      return Utils.RsaOaepEncrypt(this._rsaKey, (HashAlgorithm) SHA1.Create(), new PKCS1MaskGenerationMethod(), RandomNumberGenerator.Create(), rgbData);
    }

    /// <summary>从指定的输入数据创建加密密钥交换数据。</summary>
    /// <returns>要发送给目标接收者的加密的密钥交换数据。</returns>
    /// <param name="rgbData">在密钥交换中要传递的机密信息。</param>
    /// <param name="symAlgType">在当前版本中不使用此参数。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public override byte[] CreateKeyExchange(byte[] rgbData, Type symAlgType)
    {
      return this.CreateKeyExchange(rgbData);
    }
  }
}
