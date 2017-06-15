// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtoken
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
  /// <summary>包装 XML NMTOKEN 属性。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class SoapNmtoken : ISoapXsd
  {
    private string _value;

    /// <summary>获取当前 SOAP 类型的 XML 架构定义语言 (XSD)。</summary>
    /// <returns>一个 <see cref="T:System.String" /> 指示当前 SOAP 类型的 XSD。</returns>
    public static string XsdType
    {
      get
      {
        return "NMTOKEN";
      }
    }

    /// <summary>获取或设置 XML NMTOKEN 属性。</summary>
    /// <returns>
    /// <see cref="T:System.String" />，包含 XML NMTOKEN 属性。</returns>
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

    /// <summary>初始化 <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtoken" /> 类的新实例。</summary>
    public SoapNmtoken()
    {
    }

    /// <summary>用 XML NMTOKEN 属性初始化 <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtoken" /> 类的新实例。</summary>
    /// <param name="value">
    /// <see cref="T:System.String" />，包含 XML NMTOKEN 属性。</param>
    public SoapNmtoken(string value)
    {
      this._value = value;
    }

    /// <summary>返回当前 SOAP 类型的 XML 架构定义语言 (XSD)。</summary>
    /// <returns>一个 <see cref="T:System.String" /> 指示当前 SOAP 类型的 XSD。</returns>
    public string GetXsdType()
    {
      return SoapNmtoken.XsdType;
    }

    /// <summary>以 <see cref="T:System.String" /> 的形式返回 <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtoken.Value" />。</summary>
    /// <returns>从 <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtoken.Value" /> 中获取 <see cref="T:System.String" />。</returns>
    public override string ToString()
    {
      return SoapType.Escape(this._value);
    }

    /// <summary>将指定的 <see cref="T:System.String" /> 转换为 <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtoken" /> 对象。</summary>
    /// <returns>从 <paramref name="value" /> 获取的 <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtoken" /> 对象。</returns>
    /// <param name="value">要转换的 String。</param>
    public static SoapNmtoken Parse(string value)
    {
      return new SoapNmtoken(value);
    }
  }
}
