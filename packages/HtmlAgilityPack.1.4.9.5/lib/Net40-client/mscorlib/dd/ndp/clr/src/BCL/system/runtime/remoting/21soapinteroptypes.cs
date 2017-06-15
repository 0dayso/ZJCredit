// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Metadata.W3cXsd2001.SoapToken
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
  /// <summary>包装 XML token 类型。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class SoapToken : ISoapXsd
  {
    private string _value;

    /// <summary>获取当前 SOAP 类型的 XML 架构定义语言 (XSD)。</summary>
    /// <returns>一个 <see cref="T:System.String" /> 指示当前 SOAP 类型的 XSD。</returns>
    public static string XsdType
    {
      get
      {
        return "token";
      }
    }

    /// <summary>获取或设置 XML token.</summary>
    /// <returns>一个 <see cref="T:System.String" /> 包含 XML token。</returns>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">下列情况之一：<paramref name="value" /> 包含无效字符（0xD 或 0x9）。<paramref name="value" />[0] 或 <paramref name="value" />[<paramref name="value" />.Length - 1] 包含空白。<paramref name="value" /> 包含任何空白。</exception>
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

    /// <summary>初始化 <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapToken" /> 类的新实例。</summary>
    public SoapToken()
    {
    }

    /// <summary>使用 XML token 初始化 <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapToken" /> 类的新实例。</summary>
    /// <param name="value">一个 <see cref="T:System.String" /> 包含 XML token。</param>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">以下之一：<paramref name="value" /> 包含无效字符（0xD 或 0x9）。<paramref name="value" />[0] 或 <paramref name="value" />[<paramref name="value" />.Length - 1] 包含空白。<paramref name="value" /> 包含任何空白。</exception>
    public SoapToken(string value)
    {
      this._value = this.Validate(value);
    }

    /// <summary>返回当前 SOAP 类型的 XML 架构定义语言 (XSD)。</summary>
    /// <returns>一个 <see cref="T:System.String" /> 指示当前 SOAP 类型的 XSD。</returns>
    public string GetXsdType()
    {
      return SoapToken.XsdType;
    }

    /// <summary>以 <see cref="T:System.String" /> 的形式返回 <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapToken.Value" />。</summary>
    /// <returns>从 <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapToken.Value" /> 中获取 <see cref="T:System.String" />。</returns>
    public override string ToString()
    {
      return SoapType.Escape(this._value);
    }

    /// <summary>将指定的 <see cref="T:System.String" /> 转换为 <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapToken" /> 对象。</summary>
    /// <returns>从 <paramref name="value" /> 获取的 <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapToken" /> 对象。</returns>
    /// <param name="value">要转换的 String。</param>
    public static SoapToken Parse(string value)
    {
      return new SoapToken(value);
    }

    private string Validate(string value)
    {
      if (value == null || value.Length == 0)
        return value;
      char[] anyOf = new char[2]{ '\r', '\t' };
      if (value.LastIndexOfAny(anyOf) > -1)
        throw new RemotingException(Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid", (object) "xsd:token", (object) value));
      if (value.Length > 0)
      {
        if (!char.IsWhiteSpace(value[0]))
        {
          string str = value;
          int index = str.Length - 1;
          if (!char.IsWhiteSpace(str[index]))
            goto label_8;
        }
        throw new RemotingException(Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid", (object) "xsd:token", (object) value));
      }
label_8:
      if (value.IndexOf("  ") > -1)
        throw new RemotingException(Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid", (object) "xsd:token", (object) value));
      return value;
    }
  }
}
