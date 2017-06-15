// Decompiled with JetBrains decompiler
// Type: System.Guid
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;

namespace System
{
  /// <summary>表示全局唯一标识符 (GUID)。若要浏览此类型的.NET Framework 源代码，请参阅参考源。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  [NonVersionable]
  [__DynamicallyInvokable]
  [Serializable]
  public struct Guid : IFormattable, IComparable, IComparable<Guid>, IEquatable<Guid>
  {
    /// <summary>
    /// <see cref="T:System.Guid" /> 结构的只读实例，其值均为零。</summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static readonly Guid Empty;
    private int _a;
    private short _b;
    private short _c;
    private byte _d;
    private byte _e;
    private byte _f;
    private byte _g;
    private byte _h;
    private byte _i;
    private byte _j;
    private byte _k;

    /// <summary>使用指定的字节数组初始化 <see cref="T:System.Guid" /> 类的新实例。</summary>
    /// <param name="b">包含用于初始化 GUID 的值的 16 元素字节数组。 </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="b" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="b" /> 的长度不是 16 个字节。</exception>
    [__DynamicallyInvokable]
    public Guid(byte[] b)
    {
      if (b == null)
        throw new ArgumentNullException("b");
      if (b.Length != 16)
        throw new ArgumentException(Environment.GetResourceString("Arg_GuidArrayCtor", (object) "16"));
      this._a = (int) b[3] << 24 | (int) b[2] << 16 | (int) b[1] << 8 | (int) b[0];
      this._b = (short) ((int) b[5] << 8 | (int) b[4]);
      this._c = (short) ((int) b[7] << 8 | (int) b[6]);
      this._d = b[8];
      this._e = b[9];
      this._f = b[10];
      this._g = b[11];
      this._h = b[12];
      this._i = b[13];
      this._j = b[14];
      this._k = b[15];
    }

    /// <summary>使用指定的无符号整数和字节初始化 <see cref="T:System.Guid" /> 类的新实例。</summary>
    /// <param name="a">GUID 的前 4 个字节。</param>
    /// <param name="b">GUID 的下两个字节。</param>
    /// <param name="c">GUID 的下两个字节。</param>
    /// <param name="d">GUID 的下一个字节。</param>
    /// <param name="e">GUID 的下一个字节。</param>
    /// <param name="f">GUID 的下一个字节。</param>
    /// <param name="g">GUID 的下一个字节。</param>
    /// <param name="h">GUID 的下一个字节。</param>
    /// <param name="i">GUID 的下一个字节。</param>
    /// <param name="j">GUID 的下一个字节。</param>
    /// <param name="k">GUID 的下一个字节。</param>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public Guid(uint a, ushort b, ushort c, byte d, byte e, byte f, byte g, byte h, byte i, byte j, byte k)
    {
      this._a = (int) a;
      this._b = (short) b;
      this._c = (short) c;
      this._d = d;
      this._e = e;
      this._f = f;
      this._g = g;
      this._h = h;
      this._i = i;
      this._j = j;
      this._k = k;
    }

    /// <summary>使用指定的整数和字节数组初始化 <see cref="T:System.Guid" /> 类的新实例。</summary>
    /// <param name="a">GUID 的前 4 个字节。</param>
    /// <param name="b">GUID 的下两个字节。</param>
    /// <param name="c">GUID 的下两个字节。</param>
    /// <param name="d">GUID 的其余 8 个字节。 </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="d" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="d" /> 的长度不是 8 个字节。 </exception>
    [__DynamicallyInvokable]
    public Guid(int a, short b, short c, byte[] d)
    {
      if (d == null)
        throw new ArgumentNullException("d");
      if (d.Length != 8)
        throw new ArgumentException(Environment.GetResourceString("Arg_GuidArrayCtor", (object) "8"));
      this._a = a;
      this._b = b;
      this._c = c;
      this._d = d[0];
      this._e = d[1];
      this._f = d[2];
      this._g = d[3];
      this._h = d[4];
      this._i = d[5];
      this._j = d[6];
      this._k = d[7];
    }

    /// <summary>使用指定的整数和字节初始化 <see cref="T:System.Guid" /> 类的新实例。</summary>
    /// <param name="a">GUID 的前 4 个字节。</param>
    /// <param name="b">GUID 的下两个字节。</param>
    /// <param name="c">GUID 的下两个字节。</param>
    /// <param name="d">GUID 的下一个字节。</param>
    /// <param name="e">GUID 的下一个字节。</param>
    /// <param name="f">GUID 的下一个字节。</param>
    /// <param name="g">GUID 的下一个字节。</param>
    /// <param name="h">GUID 的下一个字节。</param>
    /// <param name="i">GUID 的下一个字节。</param>
    /// <param name="j">GUID 的下一个字节。</param>
    /// <param name="k">GUID 的下一个字节。</param>
    [__DynamicallyInvokable]
    public Guid(int a, short b, short c, byte d, byte e, byte f, byte g, byte h, byte i, byte j, byte k)
    {
      this._a = a;
      this._b = b;
      this._c = c;
      this._d = d;
      this._e = e;
      this._f = f;
      this._g = g;
      this._h = h;
      this._i = i;
      this._j = j;
      this._k = k;
    }

    /// <summary>使用指定字符串所表示的值初始化 <see cref="T:System.Guid" /> 类的新实例。</summary>
    /// <param name="g">包含下面任一格式的 GUID 的字符串（“d”表示忽略大小写的十六进制数字）：32 个连续的数字：dddddddddddddddddddddddddddddddd- 或 -8、4、4、4 和 12 位数字的分组，各组之间有连线符。也可以用一对大括号或者圆括号将整个 GUID 括起来：dddddddd-dddd-dddd-dddd-dddddddddddd- 或 -{dddddddd-dddd-dddd-dddd-dddddddddddd}- 或 -(dddddddd-dddd-dddd-dddd-dddddddddddd)- 或 -8、4 和 4 位数字的分组，和一个 8 组 2 位数字的子集，每组都带有前缀“0x”或“0X”，以逗号分隔。整个 GUID 和子集用大括号括起来：{0xdddddddd, 0xdddd, 0xdddd,{0xdd,0xdd,0xdd,0xdd,0xdd,0xdd,0xdd,0xdd}}所有大括号、逗号和“0x”前缀都是必需的。所有内置的空格都将被忽略。组中的所有前导零都将被忽略。组中显示的数字为可在该组显示的有意义数字的最大数目。你可以指定从 1 到为组显示的位数。指定的位数被认为是该组低序位的位数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="g" /> 为 null。</exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="g" /> 的格式无效。</exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="g" /> 的格式无效。</exception>
    [__DynamicallyInvokable]
    public Guid(string g)
    {
      if (g == null)
        throw new ArgumentNullException("g");
      this = Guid.Empty;
      Guid.GuidResult result = new Guid.GuidResult();
      result.Init(Guid.GuidParseThrowStyle.All);
      if (!Guid.TryParseGuid(g, Guid.GuidStyles.Any, ref result))
        throw result.GetGuidParseException();
      this = result.parsedGuid;
    }

    /// <summary>指示两个指定的 <see cref="T:System.Guid" /> 对象的值是否相等。</summary>
    /// <returns>如果 <paramref name="a" /> 和 <paramref name="b" /> 相等，则为 true；否则为 false。</returns>
    /// <param name="a">要比较的第一个对象。</param>
    /// <param name="b">要比较的第二个对象。 </param>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static bool operator ==(Guid a, Guid b)
    {
      return a._a == b._a && (int) a._b == (int) b._b && ((int) a._c == (int) b._c && (int) a._d == (int) b._d) && ((int) a._e == (int) b._e && (int) a._f == (int) b._f && ((int) a._g == (int) b._g && (int) a._h == (int) b._h)) && ((int) a._i == (int) b._i && (int) a._j == (int) b._j && (int) a._k == (int) b._k);
    }

    /// <summary>指示两个指定的 <see cref="T:System.Guid" /> 对象的值是否不相等。</summary>
    /// <returns>true if <paramref name="a" /> and <paramref name="b" /> are not equal; otherwise, false.</returns>
    /// <param name="a">要比较的第一个对象。</param>
    /// <param name="b">要比较的第二个对象。 </param>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static bool operator !=(Guid a, Guid b)
    {
      return !(a == b);
    }

    /// <summary>将 GUID 的字符串表示形式转换为等效的 <see cref="T:System.Guid" /> 结构。</summary>
    /// <returns>一个包含已分析的值的结构。</returns>
    /// <param name="input">要转换的字符串。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="input" /> 为 null。</exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="input" /> 的格式无法识别。</exception>
    [__DynamicallyInvokable]
    public static Guid Parse(string input)
    {
      if (input == null)
        throw new ArgumentNullException("input");
      Guid.GuidResult result = new Guid.GuidResult();
      result.Init(Guid.GuidParseThrowStyle.AllButOverflow);
      if (Guid.TryParseGuid(input, Guid.GuidStyles.Any, ref result))
        return result.parsedGuid;
      throw result.GetGuidParseException();
    }

    /// <summary>将 GUID 的字符串表示形式转换为等效的 <see cref="T:System.Guid" /> 结构。</summary>
    /// <returns>如果分析操作成功，则为 true；否则为 false。</returns>
    /// <param name="input">要转换的 GUID。</param>
    /// <param name="result">将包含已分析的值的结构。如果此方法返回 true，<paramref name="result" /> 将包含有效的 <see cref="T:System.Guid" />。如果 <paramref name="result" /> 等于 <see cref="F:System.Guid.Empty" />，则此方法将返回 false。</param>
    [__DynamicallyInvokable]
    public static bool TryParse(string input, out Guid result)
    {
      Guid.GuidResult result1 = new Guid.GuidResult();
      result1.Init(Guid.GuidParseThrowStyle.None);
      if (Guid.TryParseGuid(input, Guid.GuidStyles.Any, ref result1))
      {
        result = result1.parsedGuid;
        return true;
      }
      result = Guid.Empty;
      return false;
    }

    /// <summary>将 GUID 的字符串表示形式转换为等效的 <see cref="T:System.Guid" /> 结构，前提是该字符串采用的是指定格式。</summary>
    /// <returns>一个包含已分析的值的结构。</returns>
    /// <param name="input">要转换的 GUID。</param>
    /// <param name="format">下列说明符之一，指示当解释 <paramref name="input" /> 时要使用的确切格式：“N”、“D”、“B”、“P”或“X”。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="input" /> 或 <paramref name="format" /> 为 null。</exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="input" />不在指定的格式<paramref name="format" />。</exception>
    [__DynamicallyInvokable]
    public static Guid ParseExact(string input, string format)
    {
      if (input == null)
        throw new ArgumentNullException("input");
      if (format == null)
        throw new ArgumentNullException("format");
      if (format.Length != 1)
        throw new FormatException(Environment.GetResourceString("Format_InvalidGuidFormatSpecification"));
      Guid.GuidStyles flags;
      switch (format[0])
      {
        case 'D':
        case 'd':
          flags = Guid.GuidStyles.RequireDashes;
          break;
        case 'N':
        case 'n':
          flags = Guid.GuidStyles.None;
          break;
        case 'B':
        case 'b':
          flags = Guid.GuidStyles.BraceFormat;
          break;
        case 'P':
        case 'p':
          flags = Guid.GuidStyles.ParenthesisFormat;
          break;
        case 'X':
        case 'x':
          flags = Guid.GuidStyles.HexFormat;
          break;
        default:
          throw new FormatException(Environment.GetResourceString("Format_InvalidGuidFormatSpecification"));
      }
      Guid.GuidResult result = new Guid.GuidResult();
      result.Init(Guid.GuidParseThrowStyle.AllButOverflow);
      if (Guid.TryParseGuid(input, flags, ref result))
        return result.parsedGuid;
      throw result.GetGuidParseException();
    }

    /// <summary>将 GUID 的字符串表示形式转换为等效的 <see cref="T:System.Guid" /> 结构，前提是该字符串采用的是指定格式。</summary>
    /// <returns>如果分析操作成功，则为 true；否则为 false。</returns>
    /// <param name="input">要转换的 GUID。</param>
    /// <param name="format">下列说明符之一，指示当解释 <paramref name="input" /> 时要使用的确切格式：“N”、“D”、“B”、“P”或“X”。</param>
    /// <param name="result">将包含已分析的值的结构。如果此方法返回 true，<paramref name="result" /> 将包含有效的 <see cref="T:System.Guid" />。如果 <paramref name="result" /> 等于 <see cref="F:System.Guid.Empty" />，则此方法将返回 false。</param>
    [__DynamicallyInvokable]
    public static bool TryParseExact(string input, string format, out Guid result)
    {
      if (format == null || format.Length != 1)
      {
        result = Guid.Empty;
        return false;
      }
      Guid.GuidStyles flags;
      switch (format[0])
      {
        case 'D':
        case 'd':
          flags = Guid.GuidStyles.RequireDashes;
          break;
        case 'N':
        case 'n':
          flags = Guid.GuidStyles.None;
          break;
        case 'B':
        case 'b':
          flags = Guid.GuidStyles.BraceFormat;
          break;
        case 'P':
        case 'p':
          flags = Guid.GuidStyles.ParenthesisFormat;
          break;
        case 'X':
        case 'x':
          flags = Guid.GuidStyles.HexFormat;
          break;
        default:
          result = Guid.Empty;
          return false;
      }
      Guid.GuidResult result1 = new Guid.GuidResult();
      result1.Init(Guid.GuidParseThrowStyle.None);
      if (Guid.TryParseGuid(input, flags, ref result1))
      {
        result = result1.parsedGuid;
        return true;
      }
      result = Guid.Empty;
      return false;
    }

    private static bool TryParseGuid(string g, Guid.GuidStyles flags, ref Guid.GuidResult result)
    {
      if (g == null)
      {
        result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidUnrecognized");
        return false;
      }
      string guidString = g.Trim();
      if (guidString.Length == 0)
      {
        result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidUnrecognized");
        return false;
      }
      bool flag1 = guidString.IndexOf('-', 0) >= 0;
      if (flag1)
      {
        if ((flags & (Guid.GuidStyles.AllowDashes | Guid.GuidStyles.RequireDashes)) == Guid.GuidStyles.None)
        {
          result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidUnrecognized");
          return false;
        }
      }
      else if ((flags & Guid.GuidStyles.RequireDashes) != Guid.GuidStyles.None)
      {
        result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidUnrecognized");
        return false;
      }
      bool flag2 = guidString.IndexOf('{', 0) >= 0;
      if (flag2)
      {
        if ((flags & (Guid.GuidStyles.AllowBraces | Guid.GuidStyles.RequireBraces)) == Guid.GuidStyles.None)
        {
          result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidUnrecognized");
          return false;
        }
      }
      else if ((flags & Guid.GuidStyles.RequireBraces) != Guid.GuidStyles.None)
      {
        result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidUnrecognized");
        return false;
      }
      if (guidString.IndexOf('(', 0) >= 0)
      {
        if ((flags & (Guid.GuidStyles.AllowParenthesis | Guid.GuidStyles.RequireParenthesis)) == Guid.GuidStyles.None)
        {
          result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidUnrecognized");
          return false;
        }
      }
      else if ((flags & Guid.GuidStyles.RequireParenthesis) != Guid.GuidStyles.None)
      {
        result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidUnrecognized");
        return false;
      }
      try
      {
        if (flag1)
          return Guid.TryParseGuidWithDashes(guidString, ref result);
        if (flag2)
          return Guid.TryParseGuidWithHexPrefix(guidString, ref result);
        return Guid.TryParseGuidWithNoStyle(guidString, ref result);
      }
      catch (IndexOutOfRangeException ex)
      {
        result.SetFailure(Guid.ParseFailureKind.FormatWithInnerException, "Format_GuidUnrecognized", (object) null, (string) null, (Exception) ex);
        return false;
      }
      catch (ArgumentException ex)
      {
        result.SetFailure(Guid.ParseFailureKind.FormatWithInnerException, "Format_GuidUnrecognized", (object) null, (string) null, (Exception) ex);
        return false;
      }
    }

    private static bool TryParseGuidWithHexPrefix(string guidString, ref Guid.GuidResult result)
    {
      guidString = Guid.EatAllWhitespace(guidString);
      if (string.IsNullOrEmpty(guidString) || (int) guidString[0] != 123)
      {
        result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidBrace");
        return false;
      }
      if (!Guid.IsHexPrefix(guidString, 1))
      {
        result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidHexPrefix", (object) "{0xdddddddd, etc}");
        return false;
      }
      int startIndex1 = 3;
      int length1 = guidString.IndexOf(',', startIndex1) - startIndex1;
      if (length1 <= 0)
      {
        result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidComma");
        return false;
      }
      if (!Guid.StringToInt(guidString.Substring(startIndex1, length1), -1, 4096, out result.parsedGuid._a, ref result))
        return false;
      if (!Guid.IsHexPrefix(guidString, startIndex1 + length1 + 1))
      {
        result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidHexPrefix", (object) "{0xdddddddd, 0xdddd, etc}");
        return false;
      }
      int startIndex2 = startIndex1 + length1 + 3;
      int length2 = guidString.IndexOf(',', startIndex2) - startIndex2;
      if (length2 <= 0)
      {
        result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidComma");
        return false;
      }
      if (!Guid.StringToShort(guidString.Substring(startIndex2, length2), -1, 4096, out result.parsedGuid._b, ref result))
        return false;
      if (!Guid.IsHexPrefix(guidString, startIndex2 + length2 + 1))
      {
        result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidHexPrefix", (object) "{0xdddddddd, 0xdddd, 0xdddd, etc}");
        return false;
      }
      int startIndex3 = startIndex2 + length2 + 3;
      int length3 = guidString.IndexOf(',', startIndex3) - startIndex3;
      if (length3 <= 0)
      {
        result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidComma");
        return false;
      }
      if (!Guid.StringToShort(guidString.Substring(startIndex3, length3), -1, 4096, out result.parsedGuid._c, ref result))
        return false;
      if (guidString.Length <= startIndex3 + length3 + 1 || (int) guidString[startIndex3 + length3 + 1] != 123)
      {
        result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidBrace");
        return false;
      }
      int length4 = length3 + 1;
      byte[] numArray = new byte[8];
      for (int index = 0; index < 8; ++index)
      {
        if (!Guid.IsHexPrefix(guidString, startIndex3 + length4 + 1))
        {
          result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidHexPrefix", (object) "{... { ... 0xdd, ...}}");
          return false;
        }
        startIndex3 = startIndex3 + length4 + 3;
        if (index < 7)
        {
          length4 = guidString.IndexOf(',', startIndex3) - startIndex3;
          if (length4 <= 0)
          {
            result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidComma");
            return false;
          }
        }
        else
        {
          length4 = guidString.IndexOf('}', startIndex3) - startIndex3;
          if (length4 <= 0)
          {
            result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidBraceAfterLastNumber");
            return false;
          }
        }
        uint num = (uint) Convert.ToInt32(guidString.Substring(startIndex3, length4), 16);
        if (num > (uint) byte.MaxValue)
        {
          result.SetFailure(Guid.ParseFailureKind.Format, "Overflow_Byte");
          return false;
        }
        numArray[index] = (byte) num;
      }
      result.parsedGuid._d = numArray[0];
      result.parsedGuid._e = numArray[1];
      result.parsedGuid._f = numArray[2];
      result.parsedGuid._g = numArray[3];
      result.parsedGuid._h = numArray[4];
      result.parsedGuid._i = numArray[5];
      result.parsedGuid._j = numArray[6];
      result.parsedGuid._k = numArray[7];
      if (startIndex3 + length4 + 1 >= guidString.Length || (int) guidString[startIndex3 + length4 + 1] != 125)
      {
        result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidEndBrace");
        return false;
      }
      if (startIndex3 + length4 + 1 == guidString.Length - 1)
        return true;
      result.SetFailure(Guid.ParseFailureKind.Format, "Format_ExtraJunkAtEnd");
      return false;
    }

    private static bool TryParseGuidWithNoStyle(string guidString, ref Guid.GuidResult result)
    {
      int startIndex1 = 0;
      if (guidString.Length != 32)
      {
        result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidInvLen");
        return false;
      }
      for (int index = 0; index < guidString.Length; ++index)
      {
        char c = guidString[index];
        if ((int) c < 48 || (int) c > 57)
        {
          char upper = char.ToUpper(c, CultureInfo.InvariantCulture);
          if ((int) upper < 65 || (int) upper > 70)
          {
            result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidInvalidChar");
            return false;
          }
        }
      }
      if (!Guid.StringToInt(guidString.Substring(startIndex1, 8), -1, 4096, out result.parsedGuid._a, ref result))
        return false;
      int startIndex2 = startIndex1 + 8;
      if (!Guid.StringToShort(guidString.Substring(startIndex2, 4), -1, 4096, out result.parsedGuid._b, ref result))
        return false;
      int startIndex3 = startIndex2 + 4;
      if (!Guid.StringToShort(guidString.Substring(startIndex3, 4), -1, 4096, out result.parsedGuid._c, ref result))
        return false;
      int startIndex4 = startIndex3 + 4;
      int result1;
      if (!Guid.StringToInt(guidString.Substring(startIndex4, 4), -1, 4096, out result1, ref result))
        return false;
      int flags = startIndex4 + 4;
      int parsePos = flags;
      long result2;
      if (!Guid.StringToLong(guidString, ref parsePos, flags, out result2, ref result))
        return false;
      if (parsePos - flags != 12)
      {
        result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidInvLen");
        return false;
      }
      result.parsedGuid._d = (byte) (result1 >> 8);
      result.parsedGuid._e = (byte) result1;
      int num1 = (int) (result2 >> 32);
      result.parsedGuid._f = (byte) (num1 >> 8);
      result.parsedGuid._g = (byte) num1;
      int num2 = (int) result2;
      result.parsedGuid._h = (byte) (num2 >> 24);
      result.parsedGuid._i = (byte) (num2 >> 16);
      result.parsedGuid._j = (byte) (num2 >> 8);
      result.parsedGuid._k = (byte) num2;
      return true;
    }

    private static bool TryParseGuidWithDashes(string guidString, ref Guid.GuidResult result)
    {
      int num1 = 0;
      if ((int) guidString[0] == 123)
      {
        if (guidString.Length != 38 || (int) guidString[37] != 125)
        {
          result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidInvLen");
          return false;
        }
        num1 = 1;
      }
      else if ((int) guidString[0] == 40)
      {
        if (guidString.Length != 38 || (int) guidString[37] != 41)
        {
          result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidInvLen");
          return false;
        }
        num1 = 1;
      }
      else if (guidString.Length != 36)
      {
        result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidInvLen");
        return false;
      }
      if ((int) guidString[8 + num1] != 45 || (int) guidString[13 + num1] != 45 || ((int) guidString[18 + num1] != 45 || (int) guidString[23 + num1] != 45))
      {
        result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidDashes");
        return false;
      }
      int parsePos = num1;
      int result1;
      if (!Guid.StringToInt(guidString, ref parsePos, 8, 8192, out result1, ref result))
        return false;
      result.parsedGuid._a = result1;
      ++parsePos;
      if (!Guid.StringToInt(guidString, ref parsePos, 4, 8192, out result1, ref result))
        return false;
      result.parsedGuid._b = (short) result1;
      ++parsePos;
      if (!Guid.StringToInt(guidString, ref parsePos, 4, 8192, out result1, ref result))
        return false;
      result.parsedGuid._c = (short) result1;
      ++parsePos;
      if (!Guid.StringToInt(guidString, ref parsePos, 4, 8192, out result1, ref result))
        return false;
      ++parsePos;
      int num2 = parsePos;
      long result2;
      if (!Guid.StringToLong(guidString, ref parsePos, 8192, out result2, ref result))
        return false;
      if (parsePos - num2 != 12)
      {
        result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidInvLen");
        return false;
      }
      result.parsedGuid._d = (byte) (result1 >> 8);
      result.parsedGuid._e = (byte) result1;
      result1 = (int) (result2 >> 32);
      result.parsedGuid._f = (byte) (result1 >> 8);
      result.parsedGuid._g = (byte) result1;
      result1 = (int) result2;
      result.parsedGuid._h = (byte) (result1 >> 24);
      result.parsedGuid._i = (byte) (result1 >> 16);
      result.parsedGuid._j = (byte) (result1 >> 8);
      result.parsedGuid._k = (byte) result1;
      return true;
    }

    [SecuritySafeCritical]
    private static unsafe bool StringToShort(string str, int requiredLength, int flags, out short result, ref Guid.GuidResult parseResult)
    {
      return Guid.StringToShort(str, (int*) null, requiredLength, flags, out result, ref parseResult);
    }

    [SecuritySafeCritical]
    private static unsafe bool StringToShort(string str, ref int parsePos, int requiredLength, int flags, out short result, ref Guid.GuidResult parseResult)
    {
      fixed (int* parsePos1 = &parsePos)
        return Guid.StringToShort(str, parsePos1, requiredLength, flags, out result, ref parseResult);
    }

    [SecurityCritical]
    private static unsafe bool StringToShort(string str, int* parsePos, int requiredLength, int flags, out short result, ref Guid.GuidResult parseResult)
    {
      result = (short) 0;
      int result1;
      int num = Guid.StringToInt(str, parsePos, requiredLength, flags, out result1, ref parseResult) ? 1 : 0;
      result = (short) result1;
      return num != 0;
    }

    [SecuritySafeCritical]
    private static unsafe bool StringToInt(string str, int requiredLength, int flags, out int result, ref Guid.GuidResult parseResult)
    {
      return Guid.StringToInt(str, (int*) null, requiredLength, flags, out result, ref parseResult);
    }

    [SecuritySafeCritical]
    private static unsafe bool StringToInt(string str, ref int parsePos, int requiredLength, int flags, out int result, ref Guid.GuidResult parseResult)
    {
      fixed (int* parsePos1 = &parsePos)
        return Guid.StringToInt(str, parsePos1, requiredLength, flags, out result, ref parseResult);
    }

    [SecurityCritical]
    private static unsafe bool StringToInt(string str, int* parsePos, int requiredLength, int flags, out int result, ref Guid.GuidResult parseResult)
    {
      result = 0;
      int num = (IntPtr) parsePos == IntPtr.Zero ? 0 : *parsePos;
      try
      {
        result = ParseNumbers.StringToInt(str, 16, flags, parsePos);
      }
      catch (OverflowException ex)
      {
        if (parseResult.throwStyle == Guid.GuidParseThrowStyle.All)
        {
          throw;
        }
        else
        {
          if (parseResult.throwStyle == Guid.GuidParseThrowStyle.AllButOverflow)
            throw new FormatException(Environment.GetResourceString("Format_GuidUnrecognized"), (Exception) ex);
          parseResult.SetFailure((Exception) ex);
          return false;
        }
      }
      catch (Exception ex)
      {
        if (parseResult.throwStyle == Guid.GuidParseThrowStyle.None)
        {
          parseResult.SetFailure(ex);
          return false;
        }
        throw;
      }
      if (requiredLength == -1 || (IntPtr) parsePos == IntPtr.Zero || *parsePos - num == requiredLength)
        return true;
      parseResult.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidInvalidChar");
      return false;
    }

    [SecuritySafeCritical]
    private static unsafe bool StringToLong(string str, int flags, out long result, ref Guid.GuidResult parseResult)
    {
      return Guid.StringToLong(str, (int*) null, flags, out result, ref parseResult);
    }

    [SecuritySafeCritical]
    private static unsafe bool StringToLong(string str, ref int parsePos, int flags, out long result, ref Guid.GuidResult parseResult)
    {
      fixed (int* parsePos1 = &parsePos)
        return Guid.StringToLong(str, parsePos1, flags, out result, ref parseResult);
    }

    [SecuritySafeCritical]
    private static unsafe bool StringToLong(string str, int* parsePos, int flags, out long result, ref Guid.GuidResult parseResult)
    {
      result = 0L;
      try
      {
        result = ParseNumbers.StringToLong(str, 16, flags, parsePos);
      }
      catch (OverflowException ex)
      {
        if (parseResult.throwStyle == Guid.GuidParseThrowStyle.All)
        {
          throw;
        }
        else
        {
          if (parseResult.throwStyle == Guid.GuidParseThrowStyle.AllButOverflow)
            throw new FormatException(Environment.GetResourceString("Format_GuidUnrecognized"), (Exception) ex);
          parseResult.SetFailure((Exception) ex);
          return false;
        }
      }
      catch (Exception ex)
      {
        if (parseResult.throwStyle == Guid.GuidParseThrowStyle.None)
        {
          parseResult.SetFailure(ex);
          return false;
        }
        throw;
      }
      return true;
    }

    private static string EatAllWhitespace(string str)
    {
      int length = 0;
      char[] chArray = new char[str.Length];
      for (int index = 0; index < str.Length; ++index)
      {
        char c = str[index];
        if (!char.IsWhiteSpace(c))
          chArray[length++] = c;
      }
      return new string(chArray, 0, length);
    }

    private static bool IsHexPrefix(string str, int i)
    {
      return str.Length > i + 1 && (int) str[i] == 48 && (int) char.ToLower(str[i + 1], CultureInfo.InvariantCulture) == 120;
    }

    /// <summary>返回包含此实例的值的 16 元素字节数组。</summary>
    /// <returns>16 元素字节数组。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public byte[] ToByteArray()
    {
      return new byte[16]{ (byte) this._a, (byte) (this._a >> 8), (byte) (this._a >> 16), (byte) (this._a >> 24), (byte) this._b, (byte) ((uint) this._b >> 8), (byte) this._c, (byte) ((uint) this._c >> 8), this._d, this._e, this._f, this._g, this._h, this._i, this._j, this._k };
    }

    /// <summary>返回注册表格式的此实例值的字符串表示形式。</summary>
    /// <returns>这 <see cref="T:System.Guid" />的值，格式化通过使用“D”格式说明符如下所示: xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx 其中 GUID 的值表示为一系列小写的十六进制位，这些十六进制位分别以 8 个、4 个、4 个、4 个和 12 个位为一组并由连字符分隔开。例如，返回值可以是“382c74c3-721d-4f34-80e5-57657b6cbc27”。若要将从 a 到 f 的十六进制数转换为大写，请对返回的字符串调用 <see cref="M:System.String.ToUpper" /> 方法。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return this.ToString("D", (IFormatProvider) null);
    }

    /// <summary>返回此实例的哈希代码。</summary>
    /// <returns>此实例的哈希代码。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return this._a ^ ((int) this._b << 16 | (int) (ushort) this._c) ^ ((int) this._f << 24 | (int) this._k);
    }

    /// <summary>返回一个值，该值指示此实例是否与指定的对象相等。</summary>
    /// <returns>如果 <paramref name="o" /> 是值与此实例相等的 <see cref="T:System.Guid" />，则为 true；否则为 false。</returns>
    /// <param name="o">与该实例进行比较的对象。 </param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override bool Equals(object o)
    {
      if (o == null || !(o is Guid))
        return false;
      Guid guid = (Guid) o;
      return guid._a == this._a && (int) guid._b == (int) this._b && ((int) guid._c == (int) this._c && (int) guid._d == (int) this._d) && ((int) guid._e == (int) this._e && (int) guid._f == (int) this._f && ((int) guid._g == (int) this._g && (int) guid._h == (int) this._h)) && ((int) guid._i == (int) this._i && (int) guid._j == (int) this._j && (int) guid._k == (int) this._k);
    }

    /// <summary>返回一个值，该值指示此实例和指定的 <see cref="T:System.Guid" /> 对象是否表示相同的值。</summary>
    /// <returns>true if <paramref name="g" /> is equal to this instance; otherwise, false.</returns>
    /// <param name="g">要与此实例进行比较的对象。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public bool Equals(Guid g)
    {
      return g._a == this._a && (int) g._b == (int) this._b && ((int) g._c == (int) this._c && (int) g._d == (int) this._d) && ((int) g._e == (int) this._e && (int) g._f == (int) this._f && ((int) g._g == (int) this._g && (int) g._h == (int) this._h)) && ((int) g._i == (int) this._i && (int) g._j == (int) this._j && (int) g._k == (int) this._k);
    }

    private int GetResult(uint me, uint them)
    {
      return me < them ? -1 : 1;
    }

    /// <summary>将此实例与指定对象进行比较并返回一个对二者的相对值的指示。</summary>
    /// <returns>一个有符号数字，指示此实例和 <paramref name="value" /> 的相对值。返回值描述负整数此实例小于 <paramref name="value" />。零此实例等于 <paramref name="value" />。正整数此实例大于 <paramref name="value" />，或 <paramref name="value" /> 为 null。 </returns>
    /// <param name="value">要比较的对象，或为 null。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="value" /> 不是 <see cref="T:System.Guid" />。</exception>
    /// <filterpriority>2</filterpriority>
    public int CompareTo(object value)
    {
      if (value == null)
        return 1;
      if (!(value is Guid))
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeGuid"));
      Guid guid = (Guid) value;
      if (guid._a != this._a)
        return this.GetResult((uint) this._a, (uint) guid._a);
      if ((int) guid._b != (int) this._b)
        return this.GetResult((uint) this._b, (uint) guid._b);
      if ((int) guid._c != (int) this._c)
        return this.GetResult((uint) this._c, (uint) guid._c);
      if ((int) guid._d != (int) this._d)
        return this.GetResult((uint) this._d, (uint) guid._d);
      if ((int) guid._e != (int) this._e)
        return this.GetResult((uint) this._e, (uint) guid._e);
      if ((int) guid._f != (int) this._f)
        return this.GetResult((uint) this._f, (uint) guid._f);
      if ((int) guid._g != (int) this._g)
        return this.GetResult((uint) this._g, (uint) guid._g);
      if ((int) guid._h != (int) this._h)
        return this.GetResult((uint) this._h, (uint) guid._h);
      if ((int) guid._i != (int) this._i)
        return this.GetResult((uint) this._i, (uint) guid._i);
      if ((int) guid._j != (int) this._j)
        return this.GetResult((uint) this._j, (uint) guid._j);
      if ((int) guid._k != (int) this._k)
        return this.GetResult((uint) this._k, (uint) guid._k);
      return 0;
    }

    /// <summary>将此实例与指定 <see cref="T:System.Guid" /> 对象进行比较并返回对其相对值的指示。</summary>
    /// <returns>一个有符号数字，指示此实例和 <paramref name="value" /> 的相对值。返回值描述负整数此实例小于 <paramref name="value" />。零此实例等于 <paramref name="value" />。正整数此实例大于 <paramref name="value" />。</returns>
    /// <param name="value">要与此实例进行比较的对象。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public int CompareTo(Guid value)
    {
      if (value._a != this._a)
        return this.GetResult((uint) this._a, (uint) value._a);
      if ((int) value._b != (int) this._b)
        return this.GetResult((uint) this._b, (uint) value._b);
      if ((int) value._c != (int) this._c)
        return this.GetResult((uint) this._c, (uint) value._c);
      if ((int) value._d != (int) this._d)
        return this.GetResult((uint) this._d, (uint) value._d);
      if ((int) value._e != (int) this._e)
        return this.GetResult((uint) this._e, (uint) value._e);
      if ((int) value._f != (int) this._f)
        return this.GetResult((uint) this._f, (uint) value._f);
      if ((int) value._g != (int) this._g)
        return this.GetResult((uint) this._g, (uint) value._g);
      if ((int) value._h != (int) this._h)
        return this.GetResult((uint) this._h, (uint) value._h);
      if ((int) value._i != (int) this._i)
        return this.GetResult((uint) this._i, (uint) value._i);
      if ((int) value._j != (int) this._j)
        return this.GetResult((uint) this._j, (uint) value._j);
      if ((int) value._k != (int) this._k)
        return this.GetResult((uint) this._k, (uint) value._k);
      return 0;
    }

    /// <summary>初始化 <see cref="T:System.Guid" /> 结构的新实例。</summary>
    /// <returns>一个新的 GUID 对象。</returns>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static Guid NewGuid()
    {
      Guid guid;
      Marshal.ThrowExceptionForHR(Win32Native.CoCreateGuid(out guid), new IntPtr(-1));
      return guid;
    }

    /// <summary>根据所提供的格式说明符，返回此 <see cref="T:System.Guid" /> 实例值的字符串表示形式。</summary>
    /// <returns>此 <see cref="T:System.Guid" /> 的值，用一系列指定格式的小写十六进制位表示。</returns>
    /// <param name="format">一个单格式说明符，它指示如何格式化此 <see cref="T:System.Guid" /> 的值。<paramref name="format" /> 参数可以是“N”、“D”、“B”、“P”或“X”。如果 <paramref name="format" /> 为 null 或空字符串 ("")，则使用“D”。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="format" /> 的值不为 null、空字符串 ("")、"N"、"D"、"B"、"P" 或 "X"。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public string ToString(string format)
    {
      return this.ToString(format, (IFormatProvider) null);
    }

    private static char HexToChar(int a)
    {
      a &= 15;
      return a > 9 ? (char) (a - 10 + 97) : (char) (a + 48);
    }

    [SecurityCritical]
    private static unsafe int HexsToChars(char* guidChars, int offset, int a, int b)
    {
      return Guid.HexsToChars(guidChars, offset, a, b, false);
    }

    [SecurityCritical]
    private static unsafe int HexsToChars(char* guidChars, int offset, int a, int b, bool hex)
    {
      if (hex)
      {
        guidChars[offset++] = '0';
        guidChars[offset++] = 'x';
      }
      guidChars[offset++] = Guid.HexToChar(a >> 4);
      guidChars[offset++] = Guid.HexToChar(a);
      if (hex)
      {
        guidChars[offset++] = ',';
        guidChars[offset++] = '0';
        guidChars[offset++] = 'x';
      }
      guidChars[offset++] = Guid.HexToChar(b >> 4);
      guidChars[offset++] = Guid.HexToChar(b);
      return offset;
    }

    /// <summary>根据所提供的格式说明符和区域性特定的格式信息，返回 <see cref="T:System.Guid" /> 类的此实例值的字符串表示形式。</summary>
    /// <returns>此 <see cref="T:System.Guid" /> 的值，用一系列指定格式的小写十六进制位表示。</returns>
    /// <param name="format">一个单格式说明符，它指示如何格式化此 <see cref="T:System.Guid" /> 的值。<paramref name="format" /> 参数可以是“N”、“D”、“B”、“P”或“X”。如果 <paramref name="format" /> 为 null 或空字符串 ("")，则使用“D”。</param>
    /// <param name="provider">（保留）一个对象，用于提供区域性特定的格式设置信息。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="format" /> 的值不为 null、空字符串 ("")、"N"、"D"、"B"、"P" 或 "X"。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    public unsafe string ToString(string format, IFormatProvider provider)
    {
      if (format == null || format.Length == 0)
        format = "D";
      int offset1 = 0;
      bool flag1 = true;
      bool flag2 = false;
      if (format.Length != 1)
        throw new FormatException(Environment.GetResourceString("Format_InvalidGuidFormatSpecification"));
      string str1;
      switch (format[0])
      {
        case 'D':
        case 'd':
          str1 = string.FastAllocateString(36);
          break;
        case 'N':
        case 'n':
          str1 = string.FastAllocateString(32);
          flag1 = false;
          break;
        case 'B':
        case 'b':
          str1 = string.FastAllocateString(38);
          string str2 = str1;
          char* chPtr1 = (char*) str2;
          if ((IntPtr) chPtr1 != IntPtr.Zero)
            chPtr1 += RuntimeHelpers.OffsetToStringData;
          chPtr1[offset1++] = '{';
          chPtr1[37] = '}';
          str2 = (string) null;
          break;
        case 'P':
        case 'p':
          str1 = string.FastAllocateString(38);
          string str3 = str1;
          char* chPtr2 = (char*) str3;
          if ((IntPtr) chPtr2 != IntPtr.Zero)
            chPtr2 += RuntimeHelpers.OffsetToStringData;
          chPtr2[offset1++] = '(';
          chPtr2[37] = ')';
          str3 = (string) null;
          break;
        case 'X':
        case 'x':
          str1 = string.FastAllocateString(68);
          string str4 = str1;
          char* chPtr3 = (char*) str4;
          if ((IntPtr) chPtr3 != IntPtr.Zero)
            chPtr3 += RuntimeHelpers.OffsetToStringData;
          chPtr3[offset1++] = '{';
          chPtr3[67] = '}';
          str4 = (string) null;
          flag1 = false;
          flag2 = true;
          break;
        default:
          throw new FormatException(Environment.GetResourceString("Format_InvalidGuidFormatSpecification"));
      }
      string str5 = str1;
      char* guidChars = (char*) str5;
      if ((IntPtr) guidChars != IntPtr.Zero)
        guidChars += RuntimeHelpers.OffsetToStringData;
      int num1;
      if (flag2)
      {
        char* chPtr4 = guidChars;
        int num2 = offset1;
        int num3 = 1;
        int num4 = num2 + num3;
        IntPtr num5 = (IntPtr) num2 * 2;
        *(short*) ((IntPtr) chPtr4 + num5) = (short) 48;
        char* chPtr5 = guidChars;
        int num6 = num4;
        int num7 = 1;
        int offset2 = num6 + num7;
        IntPtr num8 = (IntPtr) num6 * 2;
        *(short*) ((IntPtr) chPtr5 + num8) = (short) 120;
        int chars1 = Guid.HexsToChars(guidChars, offset2, this._a >> 24, this._a >> 16);
        int chars2 = Guid.HexsToChars(guidChars, chars1, this._a >> 8, this._a);
        char* chPtr6 = guidChars;
        int num9 = chars2;
        int num10 = 1;
        int num11 = num9 + num10;
        IntPtr num12 = (IntPtr) num9 * 2;
        *(short*) ((IntPtr) chPtr6 + num12) = (short) 44;
        char* chPtr7 = guidChars;
        int num13 = num11;
        int num14 = 1;
        int num15 = num13 + num14;
        IntPtr num16 = (IntPtr) num13 * 2;
        *(short*) ((IntPtr) chPtr7 + num16) = (short) 48;
        char* chPtr8 = guidChars;
        int num17 = num15;
        int num18 = 1;
        int offset3 = num17 + num18;
        IntPtr num19 = (IntPtr) num17 * 2;
        *(short*) ((IntPtr) chPtr8 + num19) = (short) 120;
        int chars3 = Guid.HexsToChars(guidChars, offset3, (int) this._b >> 8, (int) this._b);
        char* chPtr9 = guidChars;
        int num20 = chars3;
        int num21 = 1;
        int num22 = num20 + num21;
        IntPtr num23 = (IntPtr) num20 * 2;
        *(short*) ((IntPtr) chPtr9 + num23) = (short) 44;
        char* chPtr10 = guidChars;
        int num24 = num22;
        int num25 = 1;
        int num26 = num24 + num25;
        IntPtr num27 = (IntPtr) num24 * 2;
        *(short*) ((IntPtr) chPtr10 + num27) = (short) 48;
        char* chPtr11 = guidChars;
        int num28 = num26;
        int num29 = 1;
        int offset4 = num28 + num29;
        IntPtr num30 = (IntPtr) num28 * 2;
        *(short*) ((IntPtr) chPtr11 + num30) = (short) 120;
        int chars4 = Guid.HexsToChars(guidChars, offset4, (int) this._c >> 8, (int) this._c);
        char* chPtr12 = guidChars;
        int num31 = chars4;
        int num32 = 1;
        int num33 = num31 + num32;
        IntPtr num34 = (IntPtr) num31 * 2;
        *(short*) ((IntPtr) chPtr12 + num34) = (short) 44;
        char* chPtr13 = guidChars;
        int num35 = num33;
        int num36 = 1;
        int offset5 = num35 + num36;
        IntPtr num37 = (IntPtr) num35 * 2;
        *(short*) ((IntPtr) chPtr13 + num37) = (short) 123;
        int chars5 = Guid.HexsToChars(guidChars, offset5, (int) this._d, (int) this._e, true);
        char* chPtr14 = guidChars;
        int num38 = chars5;
        int num39 = 1;
        int offset6 = num38 + num39;
        IntPtr num40 = (IntPtr) num38 * 2;
        *(short*) ((IntPtr) chPtr14 + num40) = (short) 44;
        int chars6 = Guid.HexsToChars(guidChars, offset6, (int) this._f, (int) this._g, true);
        char* chPtr15 = guidChars;
        int num41 = chars6;
        int num42 = 1;
        int offset7 = num41 + num42;
        IntPtr num43 = (IntPtr) num41 * 2;
        *(short*) ((IntPtr) chPtr15 + num43) = (short) 44;
        int chars7 = Guid.HexsToChars(guidChars, offset7, (int) this._h, (int) this._i, true);
        char* chPtr16 = guidChars;
        int num44 = chars7;
        int num45 = 1;
        int offset8 = num44 + num45;
        IntPtr num46 = (IntPtr) num44 * 2;
        *(short*) ((IntPtr) chPtr16 + num46) = (short) 44;
        int chars8 = Guid.HexsToChars(guidChars, offset8, (int) this._j, (int) this._k, true);
        char* chPtr17 = guidChars;
        int num47 = chars8;
        int num48 = 1;
        num1 = num47 + num48;
        IntPtr num49 = (IntPtr) num47 * 2;
        *(short*) ((IntPtr) chPtr17 + num49) = (short) 125;
      }
      else
      {
        int chars1 = Guid.HexsToChars(guidChars, offset1, this._a >> 24, this._a >> 16);
        int chars2 = Guid.HexsToChars(guidChars, chars1, this._a >> 8, this._a);
        if (flag1)
          guidChars[chars2++] = '-';
        int chars3 = Guid.HexsToChars(guidChars, chars2, (int) this._b >> 8, (int) this._b);
        if (flag1)
          guidChars[chars3++] = '-';
        int chars4 = Guid.HexsToChars(guidChars, chars3, (int) this._c >> 8, (int) this._c);
        if (flag1)
          guidChars[chars4++] = '-';
        int chars5 = Guid.HexsToChars(guidChars, chars4, (int) this._d, (int) this._e);
        if (flag1)
          guidChars[chars5++] = '-';
        int chars6 = Guid.HexsToChars(guidChars, chars5, (int) this._f, (int) this._g);
        int chars7 = Guid.HexsToChars(guidChars, chars6, (int) this._h, (int) this._i);
        num1 = Guid.HexsToChars(guidChars, chars7, (int) this._j, (int) this._k);
      }
      str5 = (string) null;
      return str1;
    }

    [Flags]
    private enum GuidStyles
    {
      None = 0,
      AllowParenthesis = 1,
      AllowBraces = 2,
      AllowDashes = 4,
      AllowHexPrefix = 8,
      RequireParenthesis = 16,
      RequireBraces = 32,
      RequireDashes = 64,
      RequireHexPrefix = 128,
      HexFormat = RequireHexPrefix | RequireBraces,
      NumberFormat = 0,
      DigitFormat = RequireDashes,
      BraceFormat = DigitFormat | RequireBraces,
      ParenthesisFormat = DigitFormat | RequireParenthesis,
      Any = AllowHexPrefix | AllowDashes | AllowBraces | AllowParenthesis,
    }

    private enum GuidParseThrowStyle
    {
      None,
      All,
      AllButOverflow,
    }

    private enum ParseFailureKind
    {
      None,
      ArgumentNull,
      Format,
      FormatWithParameter,
      NativeException,
      FormatWithInnerException,
    }

    private struct GuidResult
    {
      internal Guid parsedGuid;
      internal Guid.GuidParseThrowStyle throwStyle;
      internal Guid.ParseFailureKind m_failure;
      internal string m_failureMessageID;
      internal object m_failureMessageFormatArgument;
      internal string m_failureArgumentName;
      internal Exception m_innerException;

      internal void Init(Guid.GuidParseThrowStyle canThrow)
      {
        this.parsedGuid = Guid.Empty;
        this.throwStyle = canThrow;
      }

      internal void SetFailure(Exception nativeException)
      {
        this.m_failure = Guid.ParseFailureKind.NativeException;
        this.m_innerException = nativeException;
      }

      internal void SetFailure(Guid.ParseFailureKind failure, string failureMessageID)
      {
        this.SetFailure(failure, failureMessageID, (object) null, (string) null, (Exception) null);
      }

      internal void SetFailure(Guid.ParseFailureKind failure, string failureMessageID, object failureMessageFormatArgument)
      {
        this.SetFailure(failure, failureMessageID, failureMessageFormatArgument, (string) null, (Exception) null);
      }

      internal void SetFailure(Guid.ParseFailureKind failure, string failureMessageID, object failureMessageFormatArgument, string failureArgumentName, Exception innerException)
      {
        this.m_failure = failure;
        this.m_failureMessageID = failureMessageID;
        this.m_failureMessageFormatArgument = failureMessageFormatArgument;
        this.m_failureArgumentName = failureArgumentName;
        this.m_innerException = innerException;
        if (this.throwStyle != Guid.GuidParseThrowStyle.None)
          throw this.GetGuidParseException();
      }

      internal Exception GetGuidParseException()
      {
        switch (this.m_failure)
        {
          case Guid.ParseFailureKind.ArgumentNull:
            return (Exception) new ArgumentNullException(this.m_failureArgumentName, Environment.GetResourceString(this.m_failureMessageID));
          case Guid.ParseFailureKind.Format:
            return (Exception) new FormatException(Environment.GetResourceString(this.m_failureMessageID));
          case Guid.ParseFailureKind.FormatWithParameter:
            return (Exception) new FormatException(Environment.GetResourceString(this.m_failureMessageID, this.m_failureMessageFormatArgument));
          case Guid.ParseFailureKind.NativeException:
            return this.m_innerException;
          case Guid.ParseFailureKind.FormatWithInnerException:
            return (Exception) new FormatException(Environment.GetResourceString(this.m_failureMessageID), this.m_innerException);
          default:
            return (Exception) new FormatException(Environment.GetResourceString("Format_GuidUnrecognized"));
        }
      }
    }
  }
}
