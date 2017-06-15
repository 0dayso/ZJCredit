// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Metadata.W3cXsd2001.SoapAnyUri
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
  /// <summary>包装 XSD anyURI 类型。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class SoapAnyUri : ISoapXsd
  {
    private string _value;

    /// <summary>获取当前 SOAP 类型的 XML 架构定义语言 (XSD)。</summary>
    /// <returns>一个 <see cref="T:System.String" /> 指示当前 SOAP 类型的 XSD。</returns>
    public static string XsdType
    {
      get
      {
        return "anyURI";
      }
    }

    /// <summary>获取或设置一个 URI.。</summary>
    /// <returns>一个 <see cref="T:System.String" /> 包含 URI。</returns>
    public string Value
    {
      get
      {
        return this._value;
      }
      set
      {
        this._value = value;
      }
    }

    /// <summary>初始化 <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapAnyUri" /> 类的新实例。</summary>
    public SoapAnyUri()
    {
    }

    /// <summary>用指定的 URI 初始化 <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapAnyUri" /> 类的新实例。</summary>
    /// <param name="value">一个 <see cref="T:System.String" /> 包含 URI。</param>
    public SoapAnyUri(string value)
    {
      this._value = value;
    }

    /// <summary>返回当前 SOAP 类型的 XML 架构定义语言 (XSD)。</summary>
    /// <returns>一个 <see cref="T:System.String" /> 指示当前 SOAP 类型的 XSD。</returns>
    public string GetXsdType()
    {
      return SoapAnyUri.XsdType;
    }

    /// <summary>以 <see cref="T:System.String" /> 的形式返回 <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapAnyUri.Value" />。</summary>
    /// <returns>从 <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapAnyUri.Value" /> 中获取 <see cref="T:System.String" />。</returns>
    public override string ToString()
    {
      return this._value;
    }

    /// <summary>将指定的 <see cref="T:System.String" /> 转换为 <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapAnyUri" /> 对象。</summary>
    /// <returns>从 <paramref name="value" /> 获取的 <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapAnyUri" /> 对象。</returns>
    /// <param name="value">要转换的 <see cref="T:System.String" />。</param>
    public static SoapAnyUri Parse(string value)
    {
      return new SoapAnyUri(value);
    }
  }
}
