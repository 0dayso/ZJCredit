// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.RSAPKCS1KeyExchangeDeformatter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>解密 PKCS #1 密钥交换数据。</summary>
  [ComVisible(true)]
  public class RSAPKCS1KeyExchangeDeformatter : AsymmetricKeyExchangeDeformatter
  {
    private RSA _rsaKey;
    private RandomNumberGenerator RngValue;

    /// <summary>获取或设置要在密钥交换的创建中使用的随机数生成器算法。</summary>
    /// <returns>要使用的随机数生成器算法的实例。</returns>
    public RandomNumberGenerator RNG
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

    /// <summary>获取 PKCS #1 密钥交换的参数。</summary>
    /// <returns>XML 字符串，包含 PKCS #1 密钥交换操作的参数。</returns>
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

    /// <summary>初始化 <see cref="T:System.Security.Cryptography.RSAPKCS1KeyExchangeDeformatter" /> 类的新实例。</summary>
    public RSAPKCS1KeyExchangeDeformatter()
    {
    }

    /// <summary>使用指定的密钥初始化 <see cref="T:System.Security.Cryptography.RSAPKCS1KeyExchangeDeformatter" /> 类的新实例。</summary>
    /// <param name="key">包含私钥的 <see cref="T:System.Security.Cryptography.RSA" /> 算法的实例。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> 为 null。</exception>
    public RSAPKCS1KeyExchangeDeformatter(AsymmetricAlgorithm key)
    {
      if (key == null)
        throw new ArgumentNullException("key");
      this._rsaKey = (RSA) key;
    }

    /// <summary>从加密密钥交换数据中提取机密信息。</summary>
    /// <returns>从密钥交换数据导出的机密信息。</returns>
    /// <param name="rgbIn">其中隐藏有机密信息的密钥交换数据。</param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">缺少密钥。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public override byte[] DecryptKeyExchange(byte[] rgbIn)
    {
      if (this._rsaKey == null)
        throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_MissingKey"));
      byte[] numArray1;
      if (this._rsaKey is RSACryptoServiceProvider)
      {
        numArray1 = ((RSACryptoServiceProvider) this._rsaKey).Decrypt(rgbIn, false);
      }
      else
      {
        byte[] numArray2 = this._rsaKey.DecryptValue(rgbIn);
        int index = 2;
        while (index < numArray2.Length && (int) numArray2[index] != 0)
          ++index;
        if (index >= numArray2.Length)
          throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_PKCS1Decoding"));
        int srcOffsetBytes = index + 1;
        numArray1 = new byte[numArray2.Length - srcOffsetBytes];
        Buffer.InternalBlockCopy((Array) numArray2, srcOffsetBytes, (Array) numArray1, 0, numArray1.Length);
      }
      return numArray1;
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
