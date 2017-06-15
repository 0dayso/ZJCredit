// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDateTime
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
  /// <summary>提供在 <see cref="T:System.DateTime" /> 与 XSD dateTime 格式的字符串之间进行序列化和反序列化的静态方法。</summary>
  [ComVisible(true)]
  public sealed class SoapDateTime
  {
    private static string[] formats = new string[22]{ "yyyy-MM-dd'T'HH:mm:ss.fffffffzzz", "yyyy-MM-dd'T'HH:mm:ss.ffff", "yyyy-MM-dd'T'HH:mm:ss.ffffzzz", "yyyy-MM-dd'T'HH:mm:ss.fff", "yyyy-MM-dd'T'HH:mm:ss.fffzzz", "yyyy-MM-dd'T'HH:mm:ss.ff", "yyyy-MM-dd'T'HH:mm:ss.ffzzz", "yyyy-MM-dd'T'HH:mm:ss.f", "yyyy-MM-dd'T'HH:mm:ss.fzzz", "yyyy-MM-dd'T'HH:mm:ss", "yyyy-MM-dd'T'HH:mm:sszzz", "yyyy-MM-dd'T'HH:mm:ss.fffff", "yyyy-MM-dd'T'HH:mm:ss.fffffzzz", "yyyy-MM-dd'T'HH:mm:ss.ffffff", "yyyy-MM-dd'T'HH:mm:ss.ffffffzzz", "yyyy-MM-dd'T'HH:mm:ss.fffffff", "yyyy-MM-dd'T'HH:mm:ss.ffffffff", "yyyy-MM-dd'T'HH:mm:ss.ffffffffzzz", "yyyy-MM-dd'T'HH:mm:ss.fffffffff", "yyyy-MM-dd'T'HH:mm:ss.fffffffffzzz", "yyyy-MM-dd'T'HH:mm:ss.ffffffffff", "yyyy-MM-dd'T'HH:mm:ss.ffffffffffzzz" };

    /// <summary>获取当前 SOAP 类型的 XML 架构定义语言 (XSD)。</summary>
    /// <returns>一个 <see cref="T:System.String" /> 指示当前 SOAP 类型的 XSD。</returns>
    public static string XsdType
    {
      get
      {
        return "dateTime";
      }
    }

    /// <summary>以 <see cref="T:System.String" /> 的形式返回指定的 <see cref="T:System.DateTime" /> 对象。</summary>
    /// <returns>
    /// <paramref name="value" /> 的“yyyy-MM-dd'T'HH:mm:ss.fffffffzzz”格式的 <see cref="T:System.String" /> 表示形式。</returns>
    /// <param name="value">要转换的 <see cref="T:System.DateTime" /> 对象。</param>
    public static string ToString(DateTime value)
    {
      return value.ToString("yyyy-MM-dd'T'HH:mm:ss.fffffffzzz", (IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>将指定的 <see cref="T:System.String" /> 转换为 <see cref="T:System.DateTime" /> 对象。</summary>
    /// <returns>从 <paramref name="value" /> 获取的 <see cref="T:System.DateTime" /> 对象。</returns>
    /// <param name="value">要转换的 String。</param>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">以下之一：<paramref name="value" /> 是空字符串。<paramref name="value" /> 是 null 引用。<paramref name="value" /> 不包含与任何识别的格式模式相对应的日期和时间。</exception>
    public static DateTime Parse(string value)
    {
      DateTime dateTime;
      try
      {
        if (value == null)
        {
          dateTime = DateTime.MinValue;
        }
        else
        {
          string s = value;
          if (value.EndsWith("Z", StringComparison.Ordinal))
            s = value.Substring(0, value.Length - 1) + "-00:00";
          dateTime = DateTime.ParseExact(s, SoapDateTime.formats, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None);
        }
      }
      catch (Exception ex)
      {
        throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), (object) "xsd:dateTime", (object) value));
      }
      return dateTime;
    }
  }
}
