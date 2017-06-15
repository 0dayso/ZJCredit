// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.RSAPKCS1SignatureFormatter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace System.Security.Cryptography
{
  /// <summary>创建 <see cref="T:System.Security.Cryptography.RSA" /> PKCS #1 1.5 版签名。</summary>
  [ComVisible(true)]
  public class RSAPKCS1SignatureFormatter : AsymmetricSignatureFormatter
  {
    private RSA _rsaKey;
    private string _strOID;

    /// <summary>初始化 <see cref="T:System.Security.Cryptography.RSAPKCS1SignatureFormatter" /> 类的新实例。</summary>
    public RSAPKCS1SignatureFormatter()
    {
    }

    /// <summary>使用指定的密钥初始化 <see cref="T:System.Security.Cryptography.RSAPKCS1SignatureFormatter" /> 类的新实例。</summary>
    /// <param name="key">包含私钥的 <see cref="T:System.Security.Cryptography.RSA" /> 算法的实例。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> 为 null。</exception>
    public RSAPKCS1SignatureFormatter(AsymmetricAlgorithm key)
    {
      if (key == null)
        throw new ArgumentNullException("key");
      this._rsaKey = (RSA) key;
    }

    /// <summary>设置用于创建签名的私钥。</summary>
    /// <param name="key">包含私钥的 <see cref="T:System.Security.Cryptography.RSA" /> 算法的实例。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> 为 null。</exception>
    public override void SetKey(AsymmetricAlgorithm key)
    {
      if (key == null)
        throw new ArgumentNullException("key");
      this._rsaKey = (RSA) key;
    }

    /// <summary>设置用于创建签名的哈希算法。</summary>
    /// <param name="strName">要用于创建签名的哈希算法的名称。</param>
    public override void SetHashAlgorithm(string strName)
    {
      this._strOID = CryptoConfig.MapNameToOID(strName, OidGroup.HashAlgorithm);
    }

    /// <summary>创建指定数据的 <see cref="T:System.Security.Cryptography.RSA" /> PKCS #1 签名。</summary>
    /// <returns>
    /// <paramref name="rgbHash" /> 的数字签名。</returns>
    /// <param name="rgbHash">要签名的数据。</param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">密钥为 null。- 或 -该哈希算法为 null。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="rgbHash" /> 参数为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public override byte[] CreateSignature(byte[] rgbHash)
    {
      if (rgbHash == null)
        throw new ArgumentNullException("rgbHash");
      if (this._strOID == null)
        throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_MissingOID"));
      if (this._rsaKey == null)
        throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_MissingKey"));
      if (!(this._rsaKey is RSACryptoServiceProvider))
        return this._rsaKey.DecryptValue(Utils.RsaPkcs1Padding(this._rsaKey, CryptoConfig.EncodeOID(this._strOID), rgbHash));
      int algIdFromOid = X509Utils.GetAlgIdFromOid(this._strOID, OidGroup.HashAlgorithm);
      return ((RSACryptoServiceProvider) this._rsaKey).SignHash(rgbHash, algIdFromOid);
    }
  }
}
