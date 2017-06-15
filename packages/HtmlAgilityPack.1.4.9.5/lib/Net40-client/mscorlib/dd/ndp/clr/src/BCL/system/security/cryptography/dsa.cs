// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.DSA
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Util;
using System.Text;

namespace System.Security.Cryptography
{
  /// <summary>表示所有数字签名算法 (<see cref="T:System.Security.Cryptography.DSA" />) 的实现都必须从中继承的抽象基类。</summary>
  [ComVisible(true)]
  public abstract class DSA : AsymmetricAlgorithm
  {
    /// <summary>创建用于执行不对称算法的默认加密对象。</summary>
    /// <returns>一个加密对象，用于执行不对称算法。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public static DSA Create()
    {
      return DSA.Create("System.Security.Cryptography.DSA");
    }

    /// <summary>创建用于执行不对称算法的指定加密对象。</summary>
    /// <returns>一个加密对象，用于执行不对称算法。</returns>
    /// <param name="algName">要使用的 <see cref="T:System.Security.Cryptography.DSA" /> 的特定实现的名称。</param>
    public static DSA Create(string algName)
    {
      return (DSA) CryptoConfig.CreateFromName(algName);
    }

    /// <summary>当在派生类中重写时，创建指定数据的 <see cref="T:System.Security.Cryptography.DSA" /> 签名。</summary>
    /// <returns>指定数据的数字签名。</returns>
    /// <param name="rgbHash">要签名的数据。</param>
    public abstract byte[] CreateSignature(byte[] rgbHash);

    /// <summary>当在派生类中重写时，验证指定数据的 <see cref="T:System.Security.Cryptography.DSA" /> 签名。</summary>
    /// <returns>如果 <paramref name="rgbSignature" /> 与使用指定的哈希算法和密钥在 <paramref name="rgbHash" /> 上计算出的签名匹配，则为 true；否则为 false。</returns>
    /// <param name="rgbHash">用 <paramref name="rgbSignature" /> 签名的数据的哈希值。</param>
    /// <param name="rgbSignature">要为 <paramref name="rgbData" /> 验证的签名。</param>
    public abstract bool VerifySignature(byte[] rgbHash, byte[] rgbSignature);

    /// <summary>通过 XML 字符串重新构造 <see cref="T:System.Security.Cryptography.DSA" /> 对象。</summary>
    /// <param name="xmlString">用于重新构造 <see cref="T:System.Security.Cryptography.DSA" /> 对象的 XML 字符串。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="xmlString" /> 参数为 null。</exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    /// <paramref name="xmlString" /> 参数的格式无效。</exception>
    public override void FromXmlString(string xmlString)
    {
      if (xmlString == null)
        throw new ArgumentNullException("xmlString");
      DSAParameters parameters = new DSAParameters();
      SecurityElement topElement = new Parser(xmlString).GetTopElement();
      string inputBuffer1 = topElement.SearchForTextOfLocalName("P");
      if (inputBuffer1 == null)
        throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidFromXmlString", (object) "DSA", (object) "P"));
      parameters.P = Convert.FromBase64String(Utils.DiscardWhiteSpaces(inputBuffer1));
      string inputBuffer2 = topElement.SearchForTextOfLocalName("Q");
      if (inputBuffer2 == null)
        throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidFromXmlString", (object) "DSA", (object) "Q"));
      parameters.Q = Convert.FromBase64String(Utils.DiscardWhiteSpaces(inputBuffer2));
      string inputBuffer3 = topElement.SearchForTextOfLocalName("G");
      if (inputBuffer3 == null)
        throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidFromXmlString", (object) "DSA", (object) "G"));
      parameters.G = Convert.FromBase64String(Utils.DiscardWhiteSpaces(inputBuffer3));
      string inputBuffer4 = topElement.SearchForTextOfLocalName("Y");
      if (inputBuffer4 == null)
        throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidFromXmlString", (object) "DSA", (object) "Y"));
      parameters.Y = Convert.FromBase64String(Utils.DiscardWhiteSpaces(inputBuffer4));
      string inputBuffer5 = topElement.SearchForTextOfLocalName("J");
      if (inputBuffer5 != null)
        parameters.J = Convert.FromBase64String(Utils.DiscardWhiteSpaces(inputBuffer5));
      string inputBuffer6 = topElement.SearchForTextOfLocalName("X");
      if (inputBuffer6 != null)
        parameters.X = Convert.FromBase64String(Utils.DiscardWhiteSpaces(inputBuffer6));
      string inputBuffer7 = topElement.SearchForTextOfLocalName("Seed");
      string inputBuffer8 = topElement.SearchForTextOfLocalName("PgenCounter");
      if (inputBuffer7 != null && inputBuffer8 != null)
      {
        parameters.Seed = Convert.FromBase64String(Utils.DiscardWhiteSpaces(inputBuffer7));
        parameters.Counter = Utils.ConvertByteArrayToInt(Convert.FromBase64String(Utils.DiscardWhiteSpaces(inputBuffer8)));
      }
      else if (inputBuffer7 != null || inputBuffer8 != null)
      {
        if (inputBuffer7 == null)
          throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidFromXmlString", (object) "DSA", (object) "Seed"));
        throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidFromXmlString", (object) "DSA", (object) "PgenCounter"));
      }
      this.ImportParameters(parameters);
    }

    /// <summary>创建并返回当前 <see cref="T:System.Security.Cryptography.DSA" /> 对象的 XML 字符串表示形式。</summary>
    /// <returns>当前 <see cref="T:System.Security.Cryptography.DSA" /> 对象的 XML 字符串编码。</returns>
    /// <param name="includePrivateParameters">要包括私有参数，则为 true；否则为 false。</param>
    public override string ToXmlString(bool includePrivateParameters)
    {
      DSAParameters dsaParameters = this.ExportParameters(includePrivateParameters);
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("<DSAKeyValue>");
      stringBuilder.Append("<P>" + Convert.ToBase64String(dsaParameters.P) + "</P>");
      stringBuilder.Append("<Q>" + Convert.ToBase64String(dsaParameters.Q) + "</Q>");
      stringBuilder.Append("<G>" + Convert.ToBase64String(dsaParameters.G) + "</G>");
      stringBuilder.Append("<Y>" + Convert.ToBase64String(dsaParameters.Y) + "</Y>");
      if (dsaParameters.J != null)
        stringBuilder.Append("<J>" + Convert.ToBase64String(dsaParameters.J) + "</J>");
      if (dsaParameters.Seed != null)
      {
        stringBuilder.Append("<Seed>" + Convert.ToBase64String(dsaParameters.Seed) + "</Seed>");
        stringBuilder.Append("<PgenCounter>" + Convert.ToBase64String(Utils.ConvertIntToByteArray(dsaParameters.Counter)) + "</PgenCounter>");
      }
      if (includePrivateParameters)
        stringBuilder.Append("<X>" + Convert.ToBase64String(dsaParameters.X) + "</X>");
      stringBuilder.Append("</DSAKeyValue>");
      return stringBuilder.ToString();
    }

    /// <summary>当在派生类中重写时，导出 <see cref="T:System.Security.Cryptography.DSAParameters" />。</summary>
    /// <returns>
    /// <see cref="T:System.Security.Cryptography.DSA" /> 的参数。</returns>
    /// <param name="includePrivateParameters">要包括私有参数，则为 true；否则为 false。</param>
    public abstract DSAParameters ExportParameters(bool includePrivateParameters);

    /// <summary>当在派生类中重写时，导入指定的 <see cref="T:System.Security.Cryptography.DSAParameters" />。</summary>
    /// <param name="parameters">
    /// <see cref="T:System.Security.Cryptography.DSA" /> 的参数。</param>
    public abstract void ImportParameters(DSAParameters parameters);
  }
}
