// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.RSAPKCS1KeyExchangeFormatter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>使用 <see cref="T:System.Security.Cryptography.RSA" /> 创建 PKCS#1 密钥交换数据。</summary>
  [ComVisible(true)]
  public class RSAPKCS1KeyExchangeFormatter : AsymmetricKeyExchangeFormatter
  {
    private RandomNumberGenerator RngValue;
    private RSA _rsaKey;

    /// <summary>获取 PKCS #1 密钥交换的参数。</summary>
    /// <returns>XML 字符串，包含 PKCS #1 密钥交换操作的参数。</returns>
    public override string Parameters
    {
      get
      {
        return "<enc:KeyEncryptionMethod enc:Algorithm=\"http://www.microsoft.com/xml/security/algorithm/PKCS1-v1.5-KeyEx\" xmlns:enc=\"http://www.microsoft.com/xml/security/encryption/v1.0\" />";
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

    /// <summary>初始化 <see cref="T:System.Security.Cryptography.RSAPKCS1KeyExchangeFormatter" /> 类的新实例。</summary>
    public RSAPKCS1KeyExchangeFormatter()
    {
    }

    /// <summary>使用指定的密钥初始化 <see cref="T:System.Security.Cryptography.RSAPKCS1KeyExchangeFormatter" /> 类的新实例。</summary>
    /// <param name="key">包含公钥的 <see cref="T:System.Security.Cryptography.RSA" /> 算法的实例。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key " />为 null。</exception>
    public RSAPKCS1KeyExchangeFormatter(AsymmetricAlgorithm key)
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
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    /// <paramref name="rgbData " /> 太大。</exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">密钥为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public override byte[] CreateKeyExchange(byte[] rgbData)
    {
      if (this._rsaKey == null)
        throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_MissingKey"));
      byte[] numArray1;
      if (this._rsaKey is RSACryptoServiceProvider)
      {
        numArray1 = ((RSACryptoServiceProvider) this._rsaKey).Encrypt(rgbData, false);
      }
      else
      {
        int length = this._rsaKey.KeySize / 8;
        if (rgbData.Length + 11 > length)
          throw new CryptographicException(Environment.GetResourceString("Cryptography_Padding_EncDataTooBig", (object) (length - 11)));
        byte[] numArray2 = new byte[length];
        if (this.RngValue == null)
          this.RngValue = RandomNumberGenerator.Create();
        this.Rng.GetNonZeroBytes(numArray2);
        numArray2[0] = (byte) 0;
        numArray2[1] = (byte) 2;
        numArray2[length - rgbData.Length - 1] = (byte) 0;
        Buffer.InternalBlockCopy((Array) rgbData, 0, (Array) numArray2, length - rgbData.Length, rgbData.Length);
        numArray1 = this._rsaKey.EncryptValue(numArray2);
      }
      return numArray1;
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
