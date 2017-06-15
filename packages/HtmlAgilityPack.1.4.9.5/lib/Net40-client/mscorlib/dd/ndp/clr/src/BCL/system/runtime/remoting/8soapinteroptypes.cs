// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDay
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
  /// <summary>包装 XSD gDay 类型。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class SoapDay : ISoapXsd
  {
    private static string[] formats = new string[2]{ "---dd", "---ddzzz" };
    private DateTime _value = DateTime.MinValue;

    /// <summary>获取当前 SOAP 类型的 XML 架构定义语言 (XSD)。</summary>
    /// <returns>一个 <see cref="T:System.String" /> 指示当前 SOAP 类型的 XSD。</returns>
    public static string XsdType
    {
      get
      {
        return "gDay";
      }
    }

    /// <summary>获取或设置当前实例的日期和时间。</summary>
    /// <returns>该 <see cref="T:System.DateTime" /> 对象，包含当前实例的日期和时间。</returns>
    public DateTime Value
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

    /// <summary>初始化 <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDay" /> 类的新实例。</summary>
    public SoapDay()
    {
    }

    /// <summary>用指定的 <see cref="T:System.DateTime" /> 对象初始化 <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDay" /> 类的新实例。</summary>
    /// <param name="value">用于初始化当前实例的 <see cref="T:System.DateTime" /> 对象。</param>
    public SoapDay(DateTime value)
    {
      this._value = value;
    }

    /// <summary>返回当前 SOAP 类型的 XML 架构定义语言 (XSD)。</summary>
    /// <returns>一个 <see cref="T:System.String" /> 指示当前 SOAP 类型的 XSD。</returns>
    public string GetXsdType()
    {
      return SoapDay.XsdType;
    }

    /// <summary>以 <see cref="T:System.String" /> 的形式返回 <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDay.Value" />。</summary>
    /// <returns>从 <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDay.Value" /> 获取的“---dd”格式的 <see cref="T:System.String" />。</returns>
    public override string ToString()
    {
      return this._value.ToString("---dd", (IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>将指定的 <see cref="T:System.String" /> 转换为 <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDay" /> 对象。</summary>
    /// <returns>从 <paramref name="value" /> 获取的 <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDay" /> 对象。</returns>
    /// <param name="value">要转换的 <see cref="T:System.String" />。</param>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    /// <paramref name="value" /> 不包含与任何识别的格式模式相对应的日期和时间。</exception>
    public static SoapDay Parse(string value)
    {
      return new SoapDay(DateTime.ParseExact(value, SoapDay.formats, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None));
    }
  }
}
