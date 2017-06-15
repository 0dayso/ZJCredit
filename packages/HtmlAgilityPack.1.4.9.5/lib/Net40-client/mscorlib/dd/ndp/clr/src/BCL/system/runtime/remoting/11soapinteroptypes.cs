// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Metadata.W3cXsd2001.SoapBase64Binary
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
  /// <summary>包装 XSD base64Binary 类型。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class SoapBase64Binary : ISoapXsd
  {
    private byte[] _value;

    /// <summary>获取当前 SOAP 类型的 XML 架构定义语言 (XSD)。</summary>
    /// <returns>一个 <see cref="T:System.String" /> 指示当前 SOAP 类型的 XSD。</returns>
    public static string XsdType
    {
      get
      {
        return "base64Binary";
      }
    }

    /// <summary>获取或设置 64 位数字的二进制表示形式。</summary>
    /// <returns>一个 <see cref="T:System.Byte" /> 数组，它包含一个 64 位数字的二进制表示形式。</returns>
    public byte[] Value
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

    /// <summary>初始化 <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapBase64Binary" /> 类的新实例。</summary>
    public SoapBase64Binary()
    {
    }

    /// <summary>使用 64 位数字的二进制表示形式初始化 <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapBase64Binary" /> 类的新实例。</summary>
    /// <param name="value">一个 <see cref="T:System.Byte" /> 数组，它包含一个 64 位数字。</param>
    public SoapBase64Binary(byte[] value)
    {
      this._value = value;
    }

    /// <summary>返回当前 SOAP 类型的 XML 架构定义语言 (XSD)。</summary>
    /// <returns>一个 <see cref="T:System.String" /> 指示当前 SOAP 类型的 XSD。</returns>
    public string GetXsdType()
    {
      return SoapBase64Binary.XsdType;
    }

    /// <summary>以 <see cref="T:System.String" /> 的形式返回 <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapBase64Binary.Value" />。</summary>
    /// <returns>从 <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapBase64Binary.Value" /> 中获取 <see cref="T:System.String" />。</returns>
    public override string ToString()
    {
      if (this._value == null)
        return (string) null;
      return SoapType.LineFeedsBin64(Convert.ToBase64String(this._value));
    }

    /// <summary>将指定的 <see cref="T:System.String" /> 转换为 <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapBase64Binary" /> 对象。</summary>
    /// <returns>从 <paramref name="value" /> 获取的 <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapBase64Binary" /> 对象。</returns>
    /// <param name="value">要转换的 String。</param>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">以下之一：<paramref name="value" /> 为 null。<paramref name="value" /> 的长度小于4 。<paramref name="value" /> 的长度不是 4 的倍数。</exception>
    public static SoapBase64Binary Parse(string value)
    {
      if (value != null)
      {
        if (value.Length != 0)
        {
          byte[] numArray;
          try
          {
            numArray = Convert.FromBase64String(SoapType.FilterBin64(value));
          }
          catch (Exception ex)
          {
            throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), (object) "base64Binary", (object) value));
          }
          return new SoapBase64Binary(numArray);
        }
      }
      return new SoapBase64Binary(new byte[0]);
    }
  }
}
