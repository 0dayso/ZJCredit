// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNegativeInteger
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
  /// <summary>包装 XSD negativeInteger 类型。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class SoapNegativeInteger : ISoapXsd
  {
    private Decimal _value;

    /// <summary>获取当前 SOAP 类型的 XML 架构定义语言 (XSD)。</summary>
    /// <returns>一个 <see cref="T:System.String" /> 指示当前 SOAP 类型的 XSD。</returns>
    public static string XsdType
    {
      get
      {
        return "negativeInteger";
      }
    }

    /// <summary>获取或设置当前实例的数值。</summary>
    /// <returns>
    /// <see cref="T:System.Decimal" />，指示当前实例的数值。</returns>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    /// <paramref name="value" /> 大于 -1。</exception>
    public Decimal Value
    {
      get
      {
        return this._value;
      }
      set
      {
        this._value = Decimal.Truncate(value);
        if (this._value > Decimal.MinusOne)
          throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), (object) "xsd:negativeInteger", (object) value));
      }
    }

    /// <summary>初始化 <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNegativeInteger" /> 类的新实例。</summary>
    public SoapNegativeInteger()
    {
    }

    /// <summary>用 <see cref="T:System.Decimal" /> 值初始化 <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNegativeInteger" /> 类的新实例。</summary>
    /// <param name="value">用于初始化当前实例的 <see cref="T:System.Decimal" /> 值。</param>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    /// <paramref name="value" /> 大于 -1。</exception>
    public SoapNegativeInteger(Decimal value)
    {
      this._value = Decimal.Truncate(value);
      if (value > Decimal.MinusOne)
        throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), (object) "xsd:negativeInteger", (object) value));
    }

    /// <summary>返回当前 SOAP 类型的 XML 架构定义语言 (XSD)。</summary>
    /// <returns>一个 <see cref="T:System.String" /> 指示当前 SOAP 类型的 XSD。</returns>
    public string GetXsdType()
    {
      return SoapNegativeInteger.XsdType;
    }

    /// <summary>以 <see cref="T:System.String" /> 的形式返回 <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNegativeInteger.Value" />。</summary>
    /// <returns>从 Value 中获取 <see cref="T:System.String" />。</returns>
    public override string ToString()
    {
      return this._value.ToString((IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>将指定的 <see cref="T:System.String" /> 转换为 <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNegativeInteger" /> 对象。</summary>
    /// <returns>从 <paramref name="value" /> 获取的 <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNegativeInteger" /> 对象。</returns>
    /// <param name="value">要转换的 <see cref="T:System.String" />。</param>
    public static SoapNegativeInteger Parse(string value)
    {
      return new SoapNegativeInteger(Decimal.Parse(value, NumberStyles.Integer, (IFormatProvider) CultureInfo.InvariantCulture));
    }
  }
}
