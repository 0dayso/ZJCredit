// Decompiled with JetBrains decompiler
// Type: System.Security.Claims.ClaimValueTypes
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Claims
{
  /// <summary>基于 URI 类型定义了 W3C 和 OASIS.的声明值类型。此类不能被继承。</summary>
  [ComVisible(false)]
  public static class ClaimValueTypes
  {
    private const string XmlSchemaNamespace = "http://www.w3.org/2001/XMLSchema";
    /// <summary>表示 base64Binary XML 数据类型的 URI。</summary>
    public const string Base64Binary = "http://www.w3.org/2001/XMLSchema#base64Binary";
    /// <summary>表示 base64Octet XML 数据类型的 URI。</summary>
    public const string Base64Octet = "http://www.w3.org/2001/XMLSchema#base64Octet";
    /// <summary>表示 boolean XML 数据类型的 URI。</summary>
    public const string Boolean = "http://www.w3.org/2001/XMLSchema#boolean";
    /// <summary>表示 date XML 数据类型的 URI。</summary>
    public const string Date = "http://www.w3.org/2001/XMLSchema#date";
    /// <summary>表示 dateTime XML 数据类型的 URI。</summary>
    public const string DateTime = "http://www.w3.org/2001/XMLSchema#dateTime";
    /// <summary>表示 double XML 数据类型的 URI。</summary>
    public const string Double = "http://www.w3.org/2001/XMLSchema#double";
    /// <summary>表示 fqbn XML 数据类型的 URI。</summary>
    public const string Fqbn = "http://www.w3.org/2001/XMLSchema#fqbn";
    /// <summary>表示 hexBinary XML 数据类型的 URI。</summary>
    public const string HexBinary = "http://www.w3.org/2001/XMLSchema#hexBinary";
    /// <summary>表示 integer XML 数据类型的 URI。</summary>
    public const string Integer = "http://www.w3.org/2001/XMLSchema#integer";
    /// <summary>表示 integer32 XML 数据类型的 URI。</summary>
    public const string Integer32 = "http://www.w3.org/2001/XMLSchema#integer32";
    /// <summary>表示 integer64 XML 数据类型的 URI。</summary>
    public const string Integer64 = "http://www.w3.org/2001/XMLSchema#integer64";
    /// <summary>表示 sid XML 数据类型的 URI。</summary>
    public const string Sid = "http://www.w3.org/2001/XMLSchema#sid";
    /// <summary>表示 string XML 数据类型的 URI。</summary>
    public const string String = "http://www.w3.org/2001/XMLSchema#string";
    /// <summary>表示 time XML 数据类型的 URI。</summary>
    public const string Time = "http://www.w3.org/2001/XMLSchema#time";
    /// <summary>表示 uinteger32 XML 数据类型的 URI。</summary>
    public const string UInteger32 = "http://www.w3.org/2001/XMLSchema#uinteger32";
    /// <summary>表示 uinteger64 XML 数据类型的 URI。</summary>
    public const string UInteger64 = "http://www.w3.org/2001/XMLSchema#uinteger64";
    private const string SoapSchemaNamespace = "http://schemas.xmlsoap.org/";
    /// <summary>表示 dns SOAP 数据类型的 URI。</summary>
    public const string DnsName = "http://schemas.xmlsoap.org/claims/dns";
    /// <summary>表示 emailaddress SOAP 数据类型的 URI。</summary>
    public const string Email = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
    /// <summary>表示 rsa SOAP 数据类型的 URI。</summary>
    public const string Rsa = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/rsa";
    /// <summary>表示 UPN SOAP 数据类型的 URI。</summary>
    public const string UpnName = "http://schemas.xmlsoap.org/claims/UPN";
    private const string XmlSignatureConstantsNamespace = "http://www.w3.org/2000/09/xmldsig#";
    /// <summary>表示 DSAKeyValue XML 签名数据类型的 URI。</summary>
    public const string DsaKeyValue = "http://www.w3.org/2000/09/xmldsig#DSAKeyValue";
    /// <summary>表示 KeyInfo XML 签名数据类型的 URI。</summary>
    public const string KeyInfo = "http://www.w3.org/2000/09/xmldsig#KeyInfo";
    /// <summary>表示 RSAKeyValue XML 签名数据类型的 URI。</summary>
    public const string RsaKeyValue = "http://www.w3.org/2000/09/xmldsig#RSAKeyValue";
    private const string XQueryOperatorsNameSpace = "http://www.w3.org/TR/2002/WD-xquery-operators-20020816";
    /// <summary>表示 daytimeDuration XQuery 数据类型的 URI。</summary>
    public const string DaytimeDuration = "http://www.w3.org/TR/2002/WD-xquery-operators-20020816#dayTimeDuration";
    /// <summary>表示 yearMonthDuration XQuery 数据类型的 URI。</summary>
    public const string YearMonthDuration = "http://www.w3.org/TR/2002/WD-xquery-operators-20020816#yearMonthDuration";
    private const string Xacml10Namespace = "urn:oasis:names:tc:xacml:1.0";
    /// <summary>表示 rfc822Name XQuery 数据类型的 URI。</summary>
    public const string Rfc822Name = "urn:oasis:names:tc:xacml:1.0:data-type:rfc822Name";
    /// <summary>表示 x500Name XACML 1.0 数据类型的 URI。</summary>
    public const string X500Name = "urn:oasis:names:tc:xacml:1.0:data-type:x500Name";
  }
}
