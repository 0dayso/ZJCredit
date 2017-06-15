// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.DSASignatureDeformatter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace System.Security.Cryptography
{
  /// <summary>验证数字签名算法 (<see cref="T:System.Security.Cryptography.DSA" />) PKCS#1 1.5 版签名。</summary>
  [ComVisible(true)]
  public class DSASignatureDeformatter : AsymmetricSignatureDeformatter
  {
    private DSA _dsaKey;
    private string _oid;

    /// <summary>初始化 <see cref="T:System.Security.Cryptography.DSASignatureDeformatter" /> 类的新实例。</summary>
    public DSASignatureDeformatter()
    {
      this._oid = CryptoConfig.MapNameToOID("SHA1", OidGroup.HashAlgorithm);
    }

    /// <summary>使用指定的密钥初始化 <see cref="T:System.Security.Cryptography.DSASignatureDeformatter" /> 类的新实例。</summary>
    /// <param name="key">包含密钥的数字签名算法 (<see cref="T:System.Security.Cryptography.DSA" />) 的实例。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> 为 null。</exception>
    public DSASignatureDeformatter(AsymmetricAlgorithm key)
      : this()
    {
      if (key == null)
        throw new ArgumentNullException("key");
      this._dsaKey = (DSA) key;
    }

    /// <summary>指定用于数字签名算法 (<see cref="T:System.Security.Cryptography.DSA" />) 签名反格式化程序的密钥。</summary>
    /// <param name="key">包含密钥的 <see cref="T:System.Security.Cryptography.DSA" /> 的实例。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> 为 null。</exception>
    public override void SetKey(AsymmetricAlgorithm key)
    {
      if (key == null)
        throw new ArgumentNullException("key");
      this._dsaKey = (DSA) key;
    }

    /// <summary>指定数字签名算法 (<see cref="T:System.Security.Cryptography.DSA" />) 签名反格式化程序的哈希算法。</summary>
    /// <param name="strName">用于签名反格式化程序的哈希算法的名称。</param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">
    /// <paramref name="strName" /> 参数不映射到 <see cref="T:System.Security.Cryptography.SHA1" /> 哈希算法。</exception>
    public override void SetHashAlgorithm(string strName)
    {
      if (CryptoConfig.MapNameToOID(strName, OidGroup.HashAlgorithm) != this._oid)
        throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_InvalidOperation"));
    }

    /// <summary>在数据上验证数字签名算法 (<see cref="T:System.Security.Cryptography.DSA" />) 签名。</summary>
    /// <returns>如果签名对数据有效，则为 true；否则，为 false。</returns>
    /// <param name="rgbHash">用 <paramref name="rgbSignature" /> 签名的数据。</param>
    /// <param name="rgbSignature">要为 <paramref name="rgbHash" /> 验证的签名。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="rgbHash" /> 为 null。- 或 -<paramref name="rgbSignature" /> 为 null。</exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">缺少 DSA 密钥。</exception>
    public override bool VerifySignature(byte[] rgbHash, byte[] rgbSignature)
    {
      if (rgbHash == null)
        throw new ArgumentNullException("rgbHash");
      if (rgbSignature == null)
        throw new ArgumentNullException("rgbSignature");
      if (this._dsaKey == null)
        throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_MissingKey"));
      return this._dsaKey.VerifySignature(rgbHash, rgbSignature);
    }
  }
}
