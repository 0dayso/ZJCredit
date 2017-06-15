// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDate
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
  /// <summary>包装 XSD date 类型。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class SoapDate : ISoapXsd
  {
    private static string[] formats = new string[6]{ "yyyy-MM-dd", "'+'yyyy-MM-dd", "'-'yyyy-MM-dd", "yyyy-MM-ddzzz", "'+'yyyy-MM-ddzzz", "'-'yyyy-MM-ddzzz" };
    private DateTime _value = DateTime.MinValue.Date;
    private int _sign;

    /// <summary>获取当前 SOAP 类型的 XML 架构定义语言 (XSD)。</summary>
    /// <returns>一个 <see cref="T:System.String" /> 指示当前 SOAP 类型的 XSD。</returns>
    public static string XsdType
    {
      get
      {
        return "date";
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
        this._value = value.Date;
      }
    }

    /// <summary>获取或设置当前实例的日期和时间是正还是负。</summary>
    /// <returns>一个整数，指示 <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDate.Value" /> 是正还是负。</returns>
    public int Sign
    {
      get
      {
        return this._sign;
      }
      set
      {
        this._sign = value;
      }
    }

    /// <summary>初始化 <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDate" /> 类的新实例。</summary>
    public SoapDate()
    {
    }

    /// <summary>用指定的 <see cref="T:System.DateTime" /> 对象初始化 <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDate" /> 类的新实例。</summary>
    /// <param name="value">用于初始化当前实例的 <see cref="T:System.DateTime" /> 对象。</param>
    public SoapDate(DateTime value)
    {
      this._value = value;
    }

    /// <summary>用指定的 <see cref="T:System.DateTime" /> 对象和一个指示 <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDate.Value" /> 是正值还是负值的整数初始化 <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDate" /> 类的新实例。</summary>
    /// <param name="value">用于初始化当前实例的 <see cref="T:System.DateTime" /> 对象。</param>
    /// <param name="sign">一个整数，指示 <paramref name="value" /> 是否为正。</param>
    public SoapDate(DateTime value, int sign)
    {
      this._value = value;
      this._sign = sign;
    }

    /// <summary>返回当前 SOAP 类型的 XML 架构定义语言 (XSD)。</summary>
    /// <returns>一个 <see cref="T:System.String" /> 指示当前 SOAP 类型的 XSD。</returns>
    public string GetXsdType()
    {
      return SoapDate.XsdType;
    }

    /// <summary>以 <see cref="T:System.String" /> 的形式返回 <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDate.Value" />。</summary>
    /// <returns>从 <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDate.Value" /> 获取的“yyyy-MM-dd”格式或“'-'yyyy-MM-dd”格式（如果 <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDate.Sign" /> 为负）的 <see cref="T:System.String" />。</returns>
    public override string ToString()
    {
      if (this._sign < 0)
        return this._value.ToString("'-'yyyy-MM-dd", (IFormatProvider) CultureInfo.InvariantCulture);
      return this._value.ToString("yyyy-MM-dd", (IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>将指定的 <see cref="T:System.String" /> 转换为 <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDate" /> 对象。</summary>
    /// <returns>从 <paramref name="value" /> 获取的 <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDate" /> 对象。</returns>
    /// <param name="value">要转换的 <see cref="T:System.String" />。</param>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    /// <paramref name="value" /> 不包含与任何识别的格式模式相对应的日期和时间。</exception>
    public static SoapDate Parse(string value)
    {
      int sign = 0;
      if ((int) value[0] == 45)
        sign = -1;
      return new SoapDate(DateTime.ParseExact(value, SoapDate.formats, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None), sign);
    }
  }
}
