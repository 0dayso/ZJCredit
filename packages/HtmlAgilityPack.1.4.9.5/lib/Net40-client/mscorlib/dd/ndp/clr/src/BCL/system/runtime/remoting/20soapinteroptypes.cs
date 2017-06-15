// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNormalizedString
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
  /// <summary>包装 XML normalizedString 类型。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class SoapNormalizedString : ISoapXsd
  {
    private string _value;

    /// <summary>获取当前 SOAP 类型的 XML 架构定义语言 (XSD)。</summary>
    /// <returns>一个 <see cref="T:System.String" /> 指示当前 SOAP 类型的 XSD。</returns>
    public static string XsdType
    {
      get
      {
        return "normalizedString";
      }
    }

    /// <summary>获取或设置规范化的字符串。</summary>
    /// <returns>一个 <see cref="T:System.String" /> 对象，包含正常化的字符串。</returns>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    /// <paramref name="value" /> 包含无效字符（0xD、0xA 或 0x9）。</exception>
    public string Value
    {
      get
      {
        return this._value;
      }
      set
      {
        this._value = this.Validate(value);
      }
    }

    /// <summary>初始化 <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNormalizedString" /> 类的新实例。</summary>
    public SoapNormalizedString()
    {
    }

    /// <summary>用正常化的字符串初始化 <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNormalizedString" /> 类的新实例。</summary>
    /// <param name="value">一个 <see cref="T:System.String" /> 对象，包含正常化的字符串。</param>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    /// <paramref name="value" /> 包含无效字符（0xD、0xA 或 0x9）。</exception>
    public SoapNormalizedString(string value)
    {
      this._value = this.Validate(value);
    }

    /// <summary>返回当前 SOAP 类型的 XML 架构定义语言 (XSD)。</summary>
    /// <returns>一个 <see cref="T:System.String" /> 指示当前 SOAP 类型的 XSD。</returns>
    public string GetXsdType()
    {
      return SoapNormalizedString.XsdType;
    }

    /// <summary>以 <see cref="T:System.String" /> 的形式返回 <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNormalizedString.Value" />。</summary>
    /// <returns>从 <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNormalizedString.Value" /> 获取的格式为“&lt;![CDATA[" + <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNormalizedString.Value" /> + "]]&gt;”的 <see cref="T:System.String" />。</returns>
    public override string ToString()
    {
      return SoapType.Escape(this._value);
    }

    /// <summary>将指定的 <see cref="T:System.String" /> 转换为 <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNormalizedString" /> 对象。</summary>
    /// <returns>从 <paramref name="value" /> 获取的 <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNormalizedString" /> 对象。</returns>
    /// <param name="value">要转换的 String。</param>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    /// <paramref name="value" /> 包含无效字符（0xD、0xA 或 0x9）。</exception>
    public static SoapNormalizedString Parse(string value)
    {
      return new SoapNormalizedString(value);
    }

    private string Validate(string value)
    {
      if (value == null || value.Length == 0)
        return value;
      char[] anyOf = new char[3]{ '\r', '\n', '\t' };
      if (value.LastIndexOfAny(anyOf) > -1)
        throw new RemotingException(Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid", (object) "xsd:normalizedString", (object) value));
      return value;
    }
  }
}
