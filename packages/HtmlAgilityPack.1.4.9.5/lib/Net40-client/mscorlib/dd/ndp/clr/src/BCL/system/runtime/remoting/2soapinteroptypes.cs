// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDuration
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
  /// <summary>提供在 <see cref="T:System.TimeSpan" /> 与 XSD duration 格式的字符串之间进行序列化和反序列化的静态方法。</summary>
  [ComVisible(true)]
  public sealed class SoapDuration
  {
    /// <summary>获取当前 SOAP 类型的 XML 架构定义语言 (XSD)。</summary>
    /// <returns>一个 <see cref="T:System.String" /> 指示当前 SOAP 类型的 XSD。</returns>
    public static string XsdType
    {
      get
      {
        return "duration";
      }
    }

    private static void CarryOver(int inDays, out int years, out int months, out int days)
    {
      years = inDays / 360;
      int num1 = years * 360;
      months = Math.Max(0, inDays - num1) / 30;
      int num2 = months * 30;
      days = Math.Max(0, inDays - (num1 + num2));
      days = inDays % 30;
    }

    /// <summary>以 <see cref="T:System.String" /> 的形式返回指定的 <see cref="T:System.TimeSpan" /> 对象。</summary>
    /// <returns>
    /// <paramref name="timeSpan" /> 的“PxxYxxDTxxHxxMxx.xxxS”格式或“PxxYxxDTxxHxxMxxS”格式的 <see cref="T:System.String" /> 表示形式。如果 <see cref="P:System.TimeSpan.Milliseconds" /> 不等于零，则使用“PxxYxxDTxxHxxMxx.xxxS”格式。</returns>
    /// <param name="timeSpan">要转换的 <see cref="T:System.TimeSpan" /> 对象。</param>
    [SecuritySafeCritical]
    public static string ToString(TimeSpan timeSpan)
    {
      StringBuilder stringBuilder = new StringBuilder(10);
      stringBuilder.Length = 0;
      if (TimeSpan.Compare(timeSpan, TimeSpan.Zero) < 1)
        stringBuilder.Append('-');
      int years = 0;
      int months = 0;
      int days = 0;
      SoapDuration.CarryOver(Math.Abs(timeSpan.Days), out years, out months, out days);
      stringBuilder.Append('P');
      stringBuilder.Append(years);
      stringBuilder.Append('Y');
      stringBuilder.Append(months);
      stringBuilder.Append('M');
      stringBuilder.Append(days);
      stringBuilder.Append("DT");
      stringBuilder.Append(Math.Abs(timeSpan.Hours));
      stringBuilder.Append('H');
      stringBuilder.Append(Math.Abs(timeSpan.Minutes));
      stringBuilder.Append('M');
      stringBuilder.Append(Math.Abs(timeSpan.Seconds));
      int l = (int) (Math.Abs(timeSpan.Ticks % 864000000000L) % 10000000L);
      if (l != 0)
      {
        string @string = ParseNumbers.IntToString(l, 10, 7, '0', 0);
        stringBuilder.Append('.');
        stringBuilder.Append(@string);
      }
      stringBuilder.Append('S');
      return stringBuilder.ToString();
    }

    /// <summary>将指定的 <see cref="T:System.String" /> 转换为 <see cref="T:System.TimeSpan" /> 对象。</summary>
    /// <returns>从 <paramref name="value" /> 获取的 <see cref="T:System.TimeSpan" /> 对象。</returns>
    /// <param name="value">要转换的 <see cref="T:System.String" />。</param>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    /// <paramref name="value" /> 不包含与任何识别的格式模式相对应的日期和时间。</exception>
    public static TimeSpan Parse(string value)
    {
      int num = 1;
      try
      {
        if (value == null)
          return TimeSpan.Zero;
        if ((int) value[0] == 45)
          num = -1;
        char[] charArray = value.ToCharArray();
        int[] numArray = new int[7];
        string s1 = "0";
        string s2 = "0";
        string s3 = "0";
        string s4 = "0";
        string s5 = "0";
        string str1 = "0";
        string str2 = "0";
        bool flag1 = false;
        bool flag2 = false;
        int startIndex = 0;
        for (int index = 0; index < charArray.Length; ++index)
        {
          char ch = charArray[index];
          if ((uint) ch <= 72U)
          {
            if ((int) ch != 46)
            {
              if ((int) ch != 68)
              {
                if ((int) ch == 72)
                {
                  s4 = new string(charArray, startIndex, index - startIndex);
                  startIndex = index + 1;
                }
              }
              else
              {
                s3 = new string(charArray, startIndex, index - startIndex);
                startIndex = index + 1;
              }
            }
            else
            {
              flag2 = true;
              str1 = new string(charArray, startIndex, index - startIndex);
              startIndex = index + 1;
            }
          }
          else if ((uint) ch <= 84U)
          {
            switch (ch)
            {
              case 'M':
                if (flag1)
                  s5 = new string(charArray, startIndex, index - startIndex);
                else
                  s2 = new string(charArray, startIndex, index - startIndex);
                startIndex = index + 1;
                continue;
              case 'P':
                startIndex = index + 1;
                continue;
              case 'S':
                if (!flag2)
                {
                  str1 = new string(charArray, startIndex, index - startIndex);
                  continue;
                }
                str2 = new string(charArray, startIndex, index - startIndex);
                continue;
              case 'T':
                flag1 = true;
                startIndex = index + 1;
                continue;
              default:
                continue;
            }
          }
          else if ((int) ch != 89)
          {
            if ((int) ch == 90)
              ;
          }
          else
          {
            s1 = new string(charArray, startIndex, index - startIndex);
            startIndex = index + 1;
          }
        }
        return new TimeSpan((long) num * ((long.Parse(s1, (IFormatProvider) CultureInfo.InvariantCulture) * 360L + long.Parse(s2, (IFormatProvider) CultureInfo.InvariantCulture) * 30L + long.Parse(s3, (IFormatProvider) CultureInfo.InvariantCulture)) * 864000000000L + long.Parse(s4, (IFormatProvider) CultureInfo.InvariantCulture) * 36000000000L + long.Parse(s5, (IFormatProvider) CultureInfo.InvariantCulture) * 600000000L + Convert.ToInt64(double.Parse(str1 + "." + str2, (IFormatProvider) CultureInfo.InvariantCulture) * 10000000.0)));
      }
      catch (Exception ex)
      {
        throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), (object) "xsd:duration", (object) value));
      }
    }
  }
}
