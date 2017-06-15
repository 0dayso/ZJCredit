// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Metadata.W3cXsd2001.SoapHexBinary
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
  /// <summary>包装 XSD hexBinary 类型。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class SoapHexBinary : ISoapXsd
  {
    private StringBuilder sb = new StringBuilder(100);
    private byte[] _value;

    /// <summary>获取当前 SOAP 类型的 XML 架构定义语言 (XSD)。</summary>
    /// <returns>
    /// <see cref="T:System.String" />，指示当前 SOAP 类型的 XSD。</returns>
    public static string XsdType
    {
      get
      {
        return "hexBinary";
      }
    }

    /// <summary>获取或设置数字的十六进制表示形式。</summary>
    /// <returns>一个 <see cref="T:System.Byte" /> 数组，它包含数字的十六进制表示形式。</returns>
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

    /// <summary>初始化 <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapHexBinary" /> 类的新实例。</summary>
    public SoapHexBinary()
    {
    }

    /// <summary>初始化 <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapHexBinary" /> 类的新实例。</summary>
    /// <param name="value">一个 <see cref="T:System.Byte" /> 数组，它包含一个十六进制数字。</param>
    public SoapHexBinary(byte[] value)
    {
      this._value = value;
    }

    /// <summary>返回当前 SOAP 类型的 XML 架构定义语言 (XSD)。</summary>
    /// <returns>一个 <see cref="T:System.String" /> 指示当前 SOAP 类型的 XSD。</returns>
    public string GetXsdType()
    {
      return SoapHexBinary.XsdType;
    }

    /// <summary>以 <see cref="T:System.String" /> 的形式返回 <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapHexBinary.Value" />。</summary>
    /// <returns>从 <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapHexBinary.Value" /> 中获取 <see cref="T:System.String" />。</returns>
    public override string ToString()
    {
      this.sb.Length = 0;
      for (int index = 0; index < this._value.Length; ++index)
      {
        string @string = this._value[index].ToString("X", (IFormatProvider) CultureInfo.InvariantCulture);
        if (@string.Length == 1)
          this.sb.Append('0');
        this.sb.Append(@string);
      }
      return this.sb.ToString();
    }

    /// <summary>将指定的 <see cref="T:System.String" /> 转换为 <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapHexBinary" /> 对象。</summary>
    /// <returns>从 <paramref name="value" /> 获取的 <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapHexBinary" /> 对象。</returns>
    /// <param name="value">要转换的 String。</param>
    public static SoapHexBinary Parse(string value)
    {
      return new SoapHexBinary(SoapHexBinary.ToByteArray(SoapType.FilterBin64(value)));
    }

    private static byte[] ToByteArray(string value)
    {
      char[] charArray = value.ToCharArray();
      if (charArray.Length % 2 != 0)
        throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), (object) "xsd:hexBinary", (object) value));
      byte[] numArray = new byte[charArray.Length / 2];
      for (int index = 0; index < charArray.Length / 2; ++index)
        numArray[index] = (byte) ((uint) SoapHexBinary.ToByte(charArray[index * 2], value) * 16U + (uint) SoapHexBinary.ToByte(charArray[index * 2 + 1], value));
      return numArray;
    }

    private static byte ToByte(char c, string value)
    {
      c.ToString();
      try
      {
        return byte.Parse(c.ToString(), NumberStyles.HexNumber, (IFormatProvider) CultureInfo.InvariantCulture);
      }
      catch (Exception ex)
      {
        throw new RemotingException(Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid", (object) "xsd:hexBinary", (object) value));
      }
    }
  }
}
