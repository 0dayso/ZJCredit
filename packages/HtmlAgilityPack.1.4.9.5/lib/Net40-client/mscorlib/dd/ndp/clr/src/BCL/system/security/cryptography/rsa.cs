// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.RSA
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.IO;
using System.Runtime.InteropServices;
using System.Security.Util;
using System.Text;

namespace System.Security.Cryptography
{
  /// <summary>表示 <see cref="T:System.Security.Cryptography.RSA" /> 算法的所有实现均从中继承的基类。</summary>
  [ComVisible(true)]
  public abstract class RSA : AsymmetricAlgorithm
  {
    public override string KeyExchangeAlgorithm
    {
      get
      {
        return "RSA";
      }
    }

    public override string SignatureAlgorithm
    {
      get
      {
        return "RSA";
      }
    }

    /// <summary>创建 <see cref="T:System.Security.Cryptography.RSA" /> 算法的默认实现的实例。</summary>
    /// <returns>
    /// <see cref="T:System.Security.Cryptography.RSA" /> 的默认实现的新实例。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public static RSA Create()
    {
      return RSA.Create("System.Security.Cryptography.RSA");
    }

    /// <summary>创建 <see cref="T:System.Security.Cryptography.RSA" /> 的指定实现的实例。</summary>
    /// <returns>
    /// <see cref="T:System.Security.Cryptography.RSA" /> 的指定实现的新实例。</returns>
    /// <param name="algName">要使用的 <see cref="T:System.Security.Cryptography.RSA" /> 的实现的名称。</param>
    public static RSA Create(string algName)
    {
      return (RSA) CryptoConfig.CreateFromName(algName);
    }

    public virtual byte[] Encrypt(byte[] data, RSAEncryptionPadding padding)
    {
      throw RSA.DerivedClassMustOverride();
    }

    public virtual byte[] Decrypt(byte[] data, RSAEncryptionPadding padding)
    {
      throw RSA.DerivedClassMustOverride();
    }

    public virtual byte[] SignHash(byte[] hash, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
    {
      throw RSA.DerivedClassMustOverride();
    }

    public virtual bool VerifyHash(byte[] hash, byte[] signature, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
    {
      throw RSA.DerivedClassMustOverride();
    }

    protected virtual byte[] HashData(byte[] data, int offset, int count, HashAlgorithmName hashAlgorithm)
    {
      throw RSA.DerivedClassMustOverride();
    }

    protected virtual byte[] HashData(Stream data, HashAlgorithmName hashAlgorithm)
    {
      throw RSA.DerivedClassMustOverride();
    }

    public byte[] SignData(byte[] data, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
    {
      if (data == null)
        throw new ArgumentNullException("data");
      return this.SignData(data, 0, data.Length, hashAlgorithm, padding);
    }

    public virtual byte[] SignData(byte[] data, int offset, int count, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
    {
      if (data == null)
        throw new ArgumentNullException("data");
      if (offset < 0 || offset > data.Length)
        throw new ArgumentOutOfRangeException("offset");
      if (count < 0 || count > data.Length - offset)
        throw new ArgumentOutOfRangeException("count");
      if (string.IsNullOrEmpty(hashAlgorithm.Name))
        throw RSA.HashAlgorithmNameNullOrEmpty();
      if (padding == (RSASignaturePadding) null)
        throw new ArgumentNullException("padding");
      return this.SignHash(this.HashData(data, offset, count, hashAlgorithm), hashAlgorithm, padding);
    }

    public virtual byte[] SignData(Stream data, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
    {
      if (data == null)
        throw new ArgumentNullException("data");
      if (string.IsNullOrEmpty(hashAlgorithm.Name))
        throw RSA.HashAlgorithmNameNullOrEmpty();
      if (padding == (RSASignaturePadding) null)
        throw new ArgumentNullException("padding");
      return this.SignHash(this.HashData(data, hashAlgorithm), hashAlgorithm, padding);
    }

    public bool VerifyData(byte[] data, byte[] signature, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
    {
      if (data == null)
        throw new ArgumentNullException("data");
      return this.VerifyData(data, 0, data.Length, signature, hashAlgorithm, padding);
    }

    public virtual bool VerifyData(byte[] data, int offset, int count, byte[] signature, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
    {
      if (data == null)
        throw new ArgumentNullException("data");
      if (offset < 0 || offset > data.Length)
        throw new ArgumentOutOfRangeException("offset");
      if (count < 0 || count > data.Length - offset)
        throw new ArgumentOutOfRangeException("count");
      if (signature == null)
        throw new ArgumentNullException("signature");
      if (string.IsNullOrEmpty(hashAlgorithm.Name))
        throw RSA.HashAlgorithmNameNullOrEmpty();
      if (padding == (RSASignaturePadding) null)
        throw new ArgumentNullException("padding");
      return this.VerifyHash(this.HashData(data, offset, count, hashAlgorithm), signature, hashAlgorithm, padding);
    }

    public bool VerifyData(Stream data, byte[] signature, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
    {
      if (data == null)
        throw new ArgumentNullException("data");
      if (signature == null)
        throw new ArgumentNullException("signature");
      if (string.IsNullOrEmpty(hashAlgorithm.Name))
        throw RSA.HashAlgorithmNameNullOrEmpty();
      if (padding == (RSASignaturePadding) null)
        throw new ArgumentNullException("padding");
      return this.VerifyHash(this.HashData(data, hashAlgorithm), signature, hashAlgorithm, padding);
    }

    private static Exception DerivedClassMustOverride()
    {
      return (Exception) new NotImplementedException(Environment.GetResourceString("NotSupported_SubclassOverride"));
    }

    internal static Exception HashAlgorithmNameNullOrEmpty()
    {
      return (Exception) new ArgumentException(Environment.GetResourceString("Cryptography_HashAlgorithmNameNullOrEmpty"), "hashAlgorithm");
    }

    /// <summary>当在派生类中重写时，使用私钥解密输入数据。[此文档是初定版，随时可能在下一个 .NET Framework 4.6 版本时进行更改。]</summary>
    /// <returns>
    /// <paramref name="rgb" /> 参数产生的纯文本形式的解密结果。</returns>
    /// <param name="rgb">要解密的密码文本。</param>
    public virtual byte[] DecryptValue(byte[] rgb)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_Method"));
    }

    /// <summary>当在派生类中重写时，使用公钥加密输入数据。[此文档是初定版，随时可能在下一个 .NET Framework 4.6 版本时进行更改。]</summary>
    /// <returns>
    /// <paramref name="rgb" /> 参数产生的密码文本形式的加密结果。</returns>
    /// <param name="rgb">要加密的纯文本。</param>
    public virtual byte[] EncryptValue(byte[] rgb)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_Method"));
    }

    /// <summary>通过 XML 字符串中的密钥信息初始化 <see cref="T:System.Security.Cryptography.RSA" /> 对象。</summary>
    /// <param name="xmlString">包含 <see cref="T:System.Security.Cryptography.RSA" /> 密钥信息的 XML 字符串。</param>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="xmlString" /> parameter is null. </exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">The format of the <paramref name="xmlString" /> parameter is not valid. </exception>
    public override void FromXmlString(string xmlString)
    {
      if (xmlString == null)
        throw new ArgumentNullException("xmlString");
      RSAParameters parameters = new RSAParameters();
      SecurityElement topElement = new Parser(xmlString).GetTopElement();
      string inputBuffer1 = topElement.SearchForTextOfLocalName("Modulus");
      if (inputBuffer1 == null)
        throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidFromXmlString", (object) "RSA", (object) "Modulus"));
      parameters.Modulus = Convert.FromBase64String(Utils.DiscardWhiteSpaces(inputBuffer1));
      string inputBuffer2 = topElement.SearchForTextOfLocalName("Exponent");
      if (inputBuffer2 == null)
        throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidFromXmlString", (object) "RSA", (object) "Exponent"));
      parameters.Exponent = Convert.FromBase64String(Utils.DiscardWhiteSpaces(inputBuffer2));
      string inputBuffer3 = topElement.SearchForTextOfLocalName("P");
      if (inputBuffer3 != null)
        parameters.P = Convert.FromBase64String(Utils.DiscardWhiteSpaces(inputBuffer3));
      string inputBuffer4 = topElement.SearchForTextOfLocalName("Q");
      if (inputBuffer4 != null)
        parameters.Q = Convert.FromBase64String(Utils.DiscardWhiteSpaces(inputBuffer4));
      string inputBuffer5 = topElement.SearchForTextOfLocalName("DP");
      if (inputBuffer5 != null)
        parameters.DP = Convert.FromBase64String(Utils.DiscardWhiteSpaces(inputBuffer5));
      string inputBuffer6 = topElement.SearchForTextOfLocalName("DQ");
      if (inputBuffer6 != null)
        parameters.DQ = Convert.FromBase64String(Utils.DiscardWhiteSpaces(inputBuffer6));
      string inputBuffer7 = topElement.SearchForTextOfLocalName("InverseQ");
      if (inputBuffer7 != null)
        parameters.InverseQ = Convert.FromBase64String(Utils.DiscardWhiteSpaces(inputBuffer7));
      string inputBuffer8 = topElement.SearchForTextOfLocalName("D");
      if (inputBuffer8 != null)
        parameters.D = Convert.FromBase64String(Utils.DiscardWhiteSpaces(inputBuffer8));
      this.ImportParameters(parameters);
    }

    /// <summary>创建并返回包含当前 <see cref="T:System.Security.Cryptography.RSA" /> 对象的密钥的 XML 字符串。</summary>
    /// <returns>包含当前 <see cref="T:System.Security.Cryptography.RSA" /> 对象的密钥的 XML 字符串。</returns>
    /// <param name="includePrivateParameters">true 表示同时包含 RSA 公钥和私钥；false 表示仅包含公钥。</param>
    public override string ToXmlString(bool includePrivateParameters)
    {
      RSAParameters rsaParameters = this.ExportParameters(includePrivateParameters);
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("<RSAKeyValue>");
      stringBuilder.Append("<Modulus>" + Convert.ToBase64String(rsaParameters.Modulus) + "</Modulus>");
      stringBuilder.Append("<Exponent>" + Convert.ToBase64String(rsaParameters.Exponent) + "</Exponent>");
      if (includePrivateParameters)
      {
        stringBuilder.Append("<P>" + Convert.ToBase64String(rsaParameters.P) + "</P>");
        stringBuilder.Append("<Q>" + Convert.ToBase64String(rsaParameters.Q) + "</Q>");
        stringBuilder.Append("<DP>" + Convert.ToBase64String(rsaParameters.DP) + "</DP>");
        stringBuilder.Append("<DQ>" + Convert.ToBase64String(rsaParameters.DQ) + "</DQ>");
        stringBuilder.Append("<InverseQ>" + Convert.ToBase64String(rsaParameters.InverseQ) + "</InverseQ>");
        stringBuilder.Append("<D>" + Convert.ToBase64String(rsaParameters.D) + "</D>");
      }
      stringBuilder.Append("</RSAKeyValue>");
      return stringBuilder.ToString();
    }

    /// <summary>当在派生类中重写时，导出 <see cref="T:System.Security.Cryptography.RSAParameters" />。</summary>
    /// <returns>
    /// <see cref="T:System.Security.Cryptography.DSA" /> 的参数。</returns>
    /// <param name="includePrivateParameters">若要包含私有参数，则为 true；否则为 false。</param>
    public abstract RSAParameters ExportParameters(bool includePrivateParameters);

    /// <summary>当在派生类中重写时，导入指定的 <see cref="T:System.Security.Cryptography.RSAParameters" />。</summary>
    /// <param name="parameters">
    /// <see cref="T:System.Security.Cryptography.RSA" /> 的参数。</param>
    public abstract void ImportParameters(RSAParameters parameters);
  }
}
