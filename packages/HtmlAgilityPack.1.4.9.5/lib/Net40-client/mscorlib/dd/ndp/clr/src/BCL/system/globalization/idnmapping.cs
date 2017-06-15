// Decompiled with JetBrains decompiler
// Type: System.Globalization.IdnMapping
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace System.Globalization
{
  /// <summary>支持在 Internet 域名中使用非 ASCII 字符。此类不能被继承。</summary>
  public sealed class IdnMapping
  {
    private static char[] M_Dots = new char[4]{ '.', '。', '．', '｡' };
    private const int M_labelLimit = 63;
    private const int M_defaultNameLimit = 255;
    private const string M_strAcePrefix = "xn--";
    private bool m_bAllowUnassigned;
    private bool m_bUseStd3AsciiRules;
    private const int punycodeBase = 36;
    private const int tmin = 1;
    private const int tmax = 26;
    private const int skew = 38;
    private const int damp = 700;
    private const int initial_bias = 72;
    private const int initial_n = 128;
    private const char delimiter = '-';
    private const int maxint = 134217727;
    private const int IDN_ALLOW_UNASSIGNED = 1;
    private const int IDN_USE_STD3_ASCII_RULES = 2;
    private const int ERROR_INVALID_NAME = 123;

    /// <summary>获取或设置一个值，该值指示当前 <see cref="T:System.Globalization.IdnMapping" /> 对象的成员所执行的操作中是否使用未分配的 Unicode 码位。</summary>
    /// <returns>如果在操作中使用未分配的码位，则为 true；否则为 false。</returns>
    public bool AllowUnassigned
    {
      get
      {
        return this.m_bAllowUnassigned;
      }
      set
      {
        this.m_bAllowUnassigned = value;
      }
    }

    /// <summary>获取或设置一个值，该值指示在当前 <see cref="T:System.Globalization.IdnMapping" /> 对象的成员所执行的操作中是使用标准命名约定还是宽松命名约定。</summary>
    /// <returns>如果在操作中使用标准命名转换，则为 true；否则为 false。</returns>
    public bool UseStd3AsciiRules
    {
      get
      {
        return this.m_bUseStd3AsciiRules;
      }
      set
      {
        this.m_bUseStd3AsciiRules = value;
      }
    }

    /// <summary>将由 Unicode 字符组成的域名标签的字符串编码为 US-ASCII 字符范围内的可显示的 Unicode 字符的字符串。根据 IDNA 标准格式化的字符串。</summary>
    /// <returns>由 <paramref name="unicode" /> 参数指定的字符串的等效项包括 US-ASCII 字符范围（U+0020 至 U+007E）内的可显示 Unicode 字符并根据 IDNA 标准格式化。</returns>
    /// <param name="unicode">要转换的字符串，它包含一个或多个由标签分隔符分隔的域名标签。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="unicode" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">根据 <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> 和 <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> 属性以及 IDNA 标准，<paramref name="unicode" /> 是无效的。</exception>
    public string GetAscii(string unicode)
    {
      return this.GetAscii(unicode, 0);
    }

    /// <summary>编码包含US-ASCII字符范围以外的 Unicode 字符的域名称标签子字符串。子串转换为在 US-ASCII 字符范围内可显示的“ Unicode ”字符串并根据 IDNA 标准格式化。</summary>
    /// <returns>由 <paramref name="unicode" /> 和 <paramref name="index" /> 指定的子字符串的等效项包括 US-ASCII 字符范围（U+0020 至 U+007E）内的可显示 Unicode 字符并根据 IDNA 标准格式化。</returns>
    /// <param name="unicode">要转换的字符串，它包含一个或多个由标签分隔符分隔的域名标签。</param>
    /// <param name="index">
    /// <paramref name="unicode" /> 的从零开始的偏移量，用于指定开始转换的子字符串的位置。转换运算将继续执行到 <paramref name="unicode" /> 字符串的末尾。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="unicode" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于零。- 或 -<paramref name="index" /> 大于 <paramref name="unicode" /> 的长度。</exception>
    /// <exception cref="T:System.ArgumentException">根据 <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> 和 <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> 属性以及 IDNA 标准，<paramref name="unicode" /> 是无效的。</exception>
    public string GetAscii(string unicode, int index)
    {
      if (unicode == null)
        throw new ArgumentNullException("unicode");
      return this.GetAscii(unicode, index, unicode.Length - index);
    }

    /// <summary>编码包含 US-ASCII 字符范围以外的 Unicode 字符的域名称标签子字符串的指定字符数。子串转换为在 US-ASCII 字符范围内可显示的“ Unicode ”字符串并根据 IDNA 标准格式化。</summary>
    /// <returns>由 <paramref name="unicode" />、<paramref name="index" /> 和 <paramref name="count" /> 参数指定的子字符串的等效项，包括 US-ASCII 字符范围（U+0020 至 U+007E）内的可显示 Unicode 字符并根据 IDNA 标准格式化。</returns>
    /// <param name="unicode">要转换的字符串，它包含一个或多个由标签分隔符分隔的域名标签。</param>
    /// <param name="index">
    /// <paramref name="unicode" /> 的从零开始的偏移量，用于指定子字符串的起始位置。</param>
    /// <param name="count">要在 <paramref name="unicode" /> 字符串中的 <paramref name="index" /> 指定的位置开始的子字符串中转换的字符数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="unicode" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 或 <paramref name="count" /> 小于零。- 或 -<paramref name="index" /> 大于 <paramref name="unicode" /> 的长度。- 或 -<paramref name="index" /> 大于 <paramref name="unicode" /> 的长度减去 <paramref name="count" />。</exception>
    /// <exception cref="T:System.ArgumentException">根据 <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> 和 <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> 属性以及 IDNA 标准，<paramref name="unicode" /> 是无效的。</exception>
    public string GetAscii(string unicode, int index, int count)
    {
      if (unicode == null)
        throw new ArgumentNullException("unicode");
      if (index < 0 || count < 0)
        throw new ArgumentOutOfRangeException(index < 0 ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (index > unicode.Length)
        throw new ArgumentOutOfRangeException("byteIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (index > unicode.Length - count)
        throw new ArgumentOutOfRangeException("unicode", Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
      unicode = unicode.Substring(index, count);
      if (Environment.IsWindows8OrAbove)
        return this.GetAsciiUsingOS(unicode);
      if (IdnMapping.ValidateStd3AndAscii(unicode, this.UseStd3AsciiRules, true))
        return unicode;
      string str1 = unicode;
      int index1 = str1.Length - 1;
      if ((int) str1[index1] <= 31)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidCharSequence", (object) (unicode.Length - 1)), "unicode");
      int num;
      if (unicode.Length > 0)
      {
        string str2 = unicode;
        int index2 = str2.Length - 1;
        num = IdnMapping.IsDot(str2[index2]) ? 1 : 0;
      }
      else
        num = 0;
      unicode = unicode.Normalize(this.m_bAllowUnassigned ? (NormalizationForm) 13 : (NormalizationForm) 269);
      if (num == 0 && unicode.Length > 0)
      {
        string str2 = unicode;
        int index2 = str2.Length - 1;
        if (IdnMapping.IsDot(str2[index2]))
          throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadLabelSize"), "unicode");
      }
      if (this.UseStd3AsciiRules)
        IdnMapping.ValidateStd3AndAscii(unicode, true, false);
      return IdnMapping.punycode_encode(unicode);
    }

    [SecuritySafeCritical]
    private string GetAsciiUsingOS(string unicode)
    {
      if (unicode.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadLabelSize"), "unicode");
      string str = unicode;
      int index = str.Length - 1;
      if ((int) str[index] == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidCharSequence", (object) (unicode.Length - 1)), "unicode");
      int num = (this.AllowUnassigned ? 1 : 0) | (this.UseStd3AsciiRules ? 2 : 0);
      string lpUnicodeCharStr1 = unicode;
      int length1 = lpUnicodeCharStr1.Length;
      // ISSUE: variable of the null type
      __Null local = null;
      int cchASCIIChar1 = 0;
      int ascii1 = IdnMapping.IdnToAscii((uint) num, lpUnicodeCharStr1, length1, (char[]) local, cchASCIIChar1);
      if (ascii1 == 0)
      {
        if (Marshal.GetLastWin32Error() == 123)
          throw new ArgumentException(Environment.GetResourceString("Argument_IdnIllegalName"), "unicode");
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidCharSequenceNoIndex"), "unicode");
      }
      char[] chArray = new char[ascii1];
      string lpUnicodeCharStr2 = unicode;
      int length2 = lpUnicodeCharStr2.Length;
      char[] lpASCIICharStr = chArray;
      int cchASCIIChar2 = ascii1;
      int ascii2 = IdnMapping.IdnToAscii((uint) num, lpUnicodeCharStr2, length2, lpASCIICharStr, cchASCIIChar2);
      if (ascii2 != 0)
        return new string(chArray, 0, ascii2);
      if (Marshal.GetLastWin32Error() == 123)
        throw new ArgumentException(Environment.GetResourceString("Argument_IdnIllegalName"), "unicode");
      throw new ArgumentException(Environment.GetResourceString("Argument_InvalidCharSequenceNoIndex"), "unicode");
    }

    /// <summary>对基于 IDNA 标准编码的一个或者多个域名标签的字符串进行解码，解码为一个 Unicode 字符串。</summary>
    /// <returns>由 <paramref name="ascii" /> 参数指定的 IDNA 子字符串的 Unicode 等效项。</returns>
    /// <param name="ascii">要解码的字符串，包含根据 IDNA 标准在 US-ASCII 字符范围 （U+0020 至 U+007E） 编码的一个或多个标签。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="ascii" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">根据 <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> 和 <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> 属性以及 IDNA 标准，<paramref name="ascii" /> 是无效的。</exception>
    public string GetUnicode(string ascii)
    {
      return this.GetUnicode(ascii, 0);
    }

    /// <summary>对基于 IDNA 标准编码的一个或者多个域名标签的子字符串进行解码，解码为 Unicode 字符串。</summary>
    /// <returns>由 <paramref name="ascii" /> 和 <paramref name="index" /> 参数指定的 IDNA 子字符串的 Unicode 等效项。</returns>
    /// <param name="ascii">要解码的字符串，包含根据 IDNA 标准在 US-ASCII 字符范围 （U+0020 至 U+007E） 编码的一个或多个标签。</param>
    /// <param name="index">
    /// <paramref name="ascii" /> 的从零开始的偏移量，用于指定开始解码的子字符串的位置。解码运算将继续执行到 <paramref name="ascii" /> 字符串的末尾。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="ascii" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于零。- 或 -<paramref name="index" /> 大于 <paramref name="ascii" /> 的长度。</exception>
    /// <exception cref="T:System.ArgumentException">根据 <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> 和 <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> 属性以及 IDNA 标准，<paramref name="ascii" /> 是无效的。</exception>
    public string GetUnicode(string ascii, int index)
    {
      if (ascii == null)
        throw new ArgumentNullException("ascii");
      return this.GetUnicode(ascii, index, ascii.Length - index);
    }

    /// <summary>对基于 IDNA 标准编码、具有指定长度并包含一个或者多个域名标签的子字符串进行解码，解码为一个 Unicode 字符串。</summary>
    /// <returns>由 <paramref name="ascii" />、<paramref name="index" /> 和 <paramref name="count" /> 参数指定的 IDNA 子字符串的 Unicode 等效项。</returns>
    /// <param name="ascii">要解码的字符串，包含根据 IDNA 标准在 US-ASCII 字符范围 （U+0020 至 U+007E） 编码的一个或多个标签。</param>
    /// <param name="index">
    /// <paramref name="ascii" /> 的从零开始的偏移量，用于指定子字符串的起始位置。</param>
    /// <param name="count">开始 <paramref name="ascii" /> 字符串中 <paramref name="index" /> 指定的位置的子字符串中要转换的字符数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="ascii" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 或 <paramref name="count" /> 小于零。- 或 -<paramref name="index" /> 大于 <paramref name="ascii" /> 的长度。- 或 -<paramref name="index" /> 大于 <paramref name="ascii" /> 的长度减去 <paramref name="count" />。</exception>
    /// <exception cref="T:System.ArgumentException">根据 <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> 和 <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> 属性以及 IDNA 标准，<paramref name="ascii" /> 是无效的。</exception>
    public string GetUnicode(string ascii, int index, int count)
    {
      if (ascii == null)
        throw new ArgumentNullException("ascii");
      if (index < 0 || count < 0)
        throw new ArgumentOutOfRangeException(index < 0 ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (index > ascii.Length)
        throw new ArgumentOutOfRangeException("byteIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (index > ascii.Length - count)
        throw new ArgumentOutOfRangeException("ascii", Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
      if (count > 0 && (int) ascii[index + count - 1] == 0)
        throw new ArgumentException("ascii", Environment.GetResourceString("Argument_IdnBadPunycode"));
      ascii = ascii.Substring(index, count);
      if (Environment.IsWindows8OrAbove)
        return this.GetUnicodeUsingOS(ascii);
      string unicode = IdnMapping.punycode_decode(ascii);
      if (!ascii.Equals(this.GetAscii(unicode), StringComparison.OrdinalIgnoreCase))
        throw new ArgumentException(Environment.GetResourceString("Argument_IdnIllegalName"), "ascii");
      return unicode;
    }

    [SecuritySafeCritical]
    private string GetUnicodeUsingOS(string ascii)
    {
      int num = (this.AllowUnassigned ? 1 : 0) | (this.UseStd3AsciiRules ? 2 : 0);
      string lpASCIICharStr1 = ascii;
      int length1 = lpASCIICharStr1.Length;
      // ISSUE: variable of the null type
      __Null local = null;
      int cchUnicodeChar1 = 0;
      int unicode1 = IdnMapping.IdnToUnicode((uint) num, lpASCIICharStr1, length1, (char[]) local, cchUnicodeChar1);
      if (unicode1 == 0)
      {
        if (Marshal.GetLastWin32Error() == 123)
          throw new ArgumentException(Environment.GetResourceString("Argument_IdnIllegalName"), "ascii");
        throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadPunycode"), "ascii");
      }
      char[] chArray = new char[unicode1];
      string lpASCIICharStr2 = ascii;
      int length2 = lpASCIICharStr2.Length;
      char[] lpUnicodeCharStr = chArray;
      int cchUnicodeChar2 = unicode1;
      int unicode2 = IdnMapping.IdnToUnicode((uint) num, lpASCIICharStr2, length2, lpUnicodeCharStr, cchUnicodeChar2);
      if (unicode2 != 0)
        return new string(chArray, 0, unicode2);
      if (Marshal.GetLastWin32Error() == 123)
        throw new ArgumentException(Environment.GetResourceString("Argument_IdnIllegalName"), "ascii");
      throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadPunycode"), "ascii");
    }

    /// <summary>指示当前 <see cref="T:System.Globalization.IdnMapping" /> 对象与指定对象是否相等。</summary>
    /// <returns>如果 <paramref name="obj" /> 参数指定的对象从 <see cref="T:System.Globalization.IdnMapping" /> 派生且它的 <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> 和 <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> 属性相等，则为 true；否则为 false。</returns>
    /// <param name="obj">要与当前对象进行比较的对象。</param>
    public override bool Equals(object obj)
    {
      IdnMapping idnMapping = obj as IdnMapping;
      if (idnMapping != null && this.m_bAllowUnassigned == idnMapping.m_bAllowUnassigned)
        return this.m_bUseStd3AsciiRules == idnMapping.m_bUseStd3AsciiRules;
      return false;
    }

    /// <summary>返回此 <see cref="T:System.Globalization.IdnMapping" /> 对象的哈希代码。</summary>
    /// <returns>从 <see cref="T:System.Globalization.IdnMapping" /> 对象的属性派生的四个 32 位常量中的一个。返回值没有特殊意义，不适合在哈希代码算法中使用。</returns>
    public override int GetHashCode()
    {
      return (this.m_bAllowUnassigned ? 100 : 200) + (this.m_bUseStd3AsciiRules ? 1000 : 2000);
    }

    private static bool IsSupplementary(int cTest)
    {
      return cTest >= 65536;
    }

    private static bool IsDot(char c)
    {
      if ((int) c != 46 && (int) c != 12290 && (int) c != 65294)
        return (int) c == 65377;
      return true;
    }

    private static bool ValidateStd3AndAscii(string unicode, bool bUseStd3, bool bCheckAscii)
    {
      if (unicode.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadLabelSize"), "unicode");
      int num1 = -1;
      for (int index = 0; index < unicode.Length; ++index)
      {
        if ((int) unicode[index] <= 31)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidCharSequence", (object) index), "unicode");
        if (bCheckAscii && (int) unicode[index] >= (int) sbyte.MaxValue)
          return false;
        if (IdnMapping.IsDot(unicode[index]))
        {
          if (index == num1 + 1)
            throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadLabelSize"), "unicode");
          if (index - num1 > 64)
            throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadLabelSize"), "Unicode");
          if (bUseStd3 && index > 0)
            IdnMapping.ValidateStd3(unicode[index - 1], true);
          num1 = index;
        }
        else if (bUseStd3)
          IdnMapping.ValidateStd3(unicode[index], index == num1 + 1);
      }
      if (num1 == -1 && unicode.Length > 63)
        throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadLabelSize"), "unicode");
      int length = unicode.Length;
      int num2 = (int) byte.MaxValue;
      string str1 = unicode;
      int index1 = str1.Length - 1;
      int num3 = IdnMapping.IsDot(str1[index1]) ? 0 : 1;
      int num4 = num2 - num3;
      if (length > num4)
      {
        string key = "Argument_IdnBadNameSize";
        object[] objArray = new object[1];
        int index2 = 0;
        int num5 = (int) byte.MaxValue;
        string str2 = unicode;
        int index3 = str2.Length - 1;
        int num6 = IdnMapping.IsDot(str2[index3]) ? 0 : 1;
        // ISSUE: variable of a boxed type
        __Boxed<int> local = (ValueType) (num5 - num6);
        objArray[index2] = (object) local;
        throw new ArgumentException(Environment.GetResourceString(key, objArray), "unicode");
      }
      if (bUseStd3)
      {
        string str2 = unicode;
        int index2 = str2.Length - 1;
        if (!IdnMapping.IsDot(str2[index2]))
        {
          string str3 = unicode;
          int index3 = str3.Length - 1;
          IdnMapping.ValidateStd3(str3[index3], true);
        }
      }
      return true;
    }

    private static void ValidateStd3(char c, bool bNextToDot)
    {
      if ((int) c <= 44 || (int) c == 47 || (int) c >= 58 && (int) c <= 64 || ((int) c >= 91 && (int) c <= 96 || (int) c >= 123 && (int) c <= (int) sbyte.MaxValue) || (int) c == 45 & bNextToDot)
        throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadStd3", (object) c), "Unicode");
    }

    private static bool HasUpperCaseFlag(char punychar)
    {
      if ((int) punychar >= 65)
        return (int) punychar <= 90;
      return false;
    }

    private static bool basic(uint cp)
    {
      return cp < 128U;
    }

    private static int decode_digit(char cp)
    {
      if ((int) cp >= 48 && (int) cp <= 57)
        return (int) cp - 48 + 26;
      if ((int) cp >= 97 && (int) cp <= 122)
        return (int) cp - 97;
      if ((int) cp >= 65 && (int) cp <= 90)
        return (int) cp - 65;
      throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadPunycode"), "ascii");
    }

    private static char encode_digit(int d)
    {
      if (d > 25)
        return (char) (d - 26 + 48);
      return (char) (d + 97);
    }

    private static char encode_basic(char bcp)
    {
      if (IdnMapping.HasUpperCaseFlag(bcp))
        bcp += ' ';
      return bcp;
    }

    private static int adapt(int delta, int numpoints, bool firsttime)
    {
      delta = firsttime ? delta / 700 : delta / 2;
      int num1 = delta;
      int num2 = numpoints;
      int num3 = num1 / num2;
      delta = num1 + num3;
      uint num4 = 0;
      while (delta > 455)
      {
        delta /= 35;
        num4 += 36U;
      }
      return (int) ((long) num4 + (long) (36 * delta / (delta + 38)));
    }

    private static string punycode_encode(string unicode)
    {
      if (unicode.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadLabelSize"), "unicode");
      StringBuilder stringBuilder = new StringBuilder(unicode.Length);
      int num1 = 0;
      int num2 = 0;
      int startIndex = 0;
      while (num1 < unicode.Length)
      {
        num1 = unicode.IndexOfAny(IdnMapping.M_Dots, num2);
        if (num1 < 0)
          num1 = unicode.Length;
        if (num1 == num2)
        {
          if (num1 != unicode.Length)
            throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadLabelSize"), "unicode");
          break;
        }
        stringBuilder.Append("xn--");
        bool flag = false;
        switch (CharUnicodeInfo.GetBidiCategory(unicode, num2))
        {
          case BidiCategory.RightToLeft:
          case BidiCategory.RightToLeftArabic:
            flag = true;
            int index1 = num1 - 1;
            if (char.IsLowSurrogate(unicode, index1))
              --index1;
            switch (CharUnicodeInfo.GetBidiCategory(unicode, index1))
            {
              case BidiCategory.RightToLeft:
              case BidiCategory.RightToLeftArabic:
                break;
              default:
                throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadBidi"), "unicode");
            }
        }
        int num3 = 0;
        for (int index2 = num2; index2 < num1; ++index2)
        {
          BidiCategory bidiCategory = CharUnicodeInfo.GetBidiCategory(unicode, index2);
          if (flag && bidiCategory == BidiCategory.LeftToRight)
            throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadBidi"), "unicode");
          if (!flag && (bidiCategory == BidiCategory.RightToLeft || bidiCategory == BidiCategory.RightToLeftArabic))
            throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadBidi"), "unicode");
          if (IdnMapping.basic((uint) unicode[index2]))
          {
            stringBuilder.Append(IdnMapping.encode_basic(unicode[index2]));
            ++num3;
          }
          else if (char.IsSurrogatePair(unicode, index2))
            ++index2;
        }
        int num4 = num3;
        if (num4 == num1 - num2)
        {
          stringBuilder.Remove(startIndex, "xn--".Length);
        }
        else
        {
          if (unicode.Length - num2 >= "xn--".Length && unicode.Substring(num2, "xn--".Length).Equals("xn--", StringComparison.OrdinalIgnoreCase))
            throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadPunycode"), "unicode");
          int num5 = 0;
          if (num4 > 0)
            stringBuilder.Append('-');
          int num6 = 128;
          int num7 = 0;
          int num8 = 72;
          while (num3 < num1 - num2)
          {
            int cTest = 134217727;
            int index2 = num2;
            while (index2 < num1)
            {
              int utf32 = char.ConvertToUtf32(unicode, index2);
              if (utf32 >= num6 && utf32 < cTest)
                cTest = utf32;
              index2 += IdnMapping.IsSupplementary(utf32) ? 2 : 1;
            }
            int delta = num7 + (cTest - num6) * (num3 - num5 + 1);
            int num9 = cTest;
            int index3 = num2;
            while (index3 < num1)
            {
              int utf32 = char.ConvertToUtf32(unicode, index3);
              if (utf32 < num9)
                ++delta;
              if (utf32 == num9)
              {
                int d = delta;
                int num10 = 36;
                while (true)
                {
                  int num11 = num10 <= num8 ? 1 : (num10 >= num8 + 26 ? 26 : num10 - num8);
                  if (d >= num11)
                  {
                    stringBuilder.Append(IdnMapping.encode_digit(num11 + (d - num11) % (36 - num11)));
                    d = (d - num11) / (36 - num11);
                    num10 += 36;
                  }
                  else
                    break;
                }
                stringBuilder.Append(IdnMapping.encode_digit(d));
                num8 = IdnMapping.adapt(delta, num3 - num5 + 1, num3 == num4);
                delta = 0;
                ++num3;
                if (IdnMapping.IsSupplementary(cTest))
                {
                  ++num3;
                  ++num5;
                }
              }
              index3 += IdnMapping.IsSupplementary(utf32) ? 2 : 1;
            }
            num7 = delta + 1;
            num6 = num9 + 1;
          }
        }
        if (stringBuilder.Length - startIndex > 63)
          throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadLabelSize"), "unicode");
        if (num1 != unicode.Length)
          stringBuilder.Append('.');
        num2 = num1 + 1;
        startIndex = stringBuilder.Length;
      }
      int length = stringBuilder.Length;
      int num12 = (int) byte.MaxValue;
      string str1 = unicode;
      int index4 = str1.Length - 1;
      int num13 = IdnMapping.IsDot(str1[index4]) ? 0 : 1;
      int num14 = num12 - num13;
      if (length > num14)
      {
        string key = "Argument_IdnBadNameSize";
        object[] objArray = new object[1];
        int index1 = 0;
        int num3 = (int) byte.MaxValue;
        string str2 = unicode;
        int index2 = str2.Length - 1;
        int num4 = IdnMapping.IsDot(str2[index2]) ? 0 : 1;
        // ISSUE: variable of a boxed type
        __Boxed<int> local = (ValueType) (num3 - num4);
        objArray[index1] = (object) local;
        throw new ArgumentException(Environment.GetResourceString(key, objArray), "unicode");
      }
      return stringBuilder.ToString();
    }

    private static string punycode_decode(string ascii)
    {
      if (ascii.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadLabelSize"), "ascii");
      int length1 = ascii.Length;
      int num1 = (int) byte.MaxValue;
      string str1 = ascii;
      int index1 = str1.Length - 1;
      int num2 = IdnMapping.IsDot(str1[index1]) ? 0 : 1;
      int num3 = num1 - num2;
      if (length1 > num3)
      {
        string key = "Argument_IdnBadNameSize";
        object[] objArray = new object[1];
        int index2 = 0;
        int num4 = (int) byte.MaxValue;
        string str2 = ascii;
        int index3 = str2.Length - 1;
        int num5 = IdnMapping.IsDot(str2[index3]) ? 0 : 1;
        // ISSUE: variable of a boxed type
        __Boxed<int> local = (ValueType) (num4 - num5);
        objArray[index2] = (object) local;
        throw new ArgumentException(Environment.GetResourceString(key, objArray), "ascii");
      }
      StringBuilder stringBuilder1 = new StringBuilder(ascii.Length);
      int num6 = 0;
      int startIndex = 0;
      int index4 = 0;
      while (num6 < ascii.Length)
      {
        num6 = ascii.IndexOf('.', startIndex);
        if (num6 < 0 || num6 > ascii.Length)
          num6 = ascii.Length;
        if (num6 == startIndex)
        {
          if (num6 != ascii.Length)
            throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadLabelSize"), "ascii");
          break;
        }
        if (num6 - startIndex > 63)
          throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadLabelSize"), "ascii");
        if (ascii.Length < "xn--".Length + startIndex || !ascii.Substring(startIndex, "xn--".Length).Equals("xn--", StringComparison.OrdinalIgnoreCase))
        {
          stringBuilder1.Append(ascii.Substring(startIndex, num6 - startIndex));
        }
        else
        {
          startIndex += "xn--".Length;
          int num4 = ascii.LastIndexOf('-', num6 - 1);
          if (num4 == num6 - 1)
            throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadPunycode"), "ascii");
          int num5;
          if (num4 <= startIndex)
          {
            num5 = 0;
          }
          else
          {
            num5 = num4 - startIndex;
            for (int index2 = startIndex; index2 < startIndex + num5; ++index2)
            {
              if ((int) ascii[index2] > (int) sbyte.MaxValue)
                throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadPunycode"), "ascii");
              stringBuilder1.Append((int) ascii[index2] < 65 || (int) ascii[index2] > 90 ? ascii[index2] : (char) ((int) ascii[index2] - 65 + 97));
            }
          }
          int num7 = startIndex + (num5 > 0 ? num5 + 1 : 0);
          int num8 = 128;
          int num9 = 72;
          int num10 = 0;
          int num11 = 0;
label_49:
          while (num7 < num6)
          {
            int num12 = num10;
            int num13 = 1;
            int num14 = 36;
            while (num7 < num6)
            {
              int num15 = IdnMapping.decode_digit(ascii[num7++]);
              if (num15 > (134217727 - num10) / num13)
                throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadPunycode"), "ascii");
              num10 += num15 * num13;
              int num16 = num14 <= num9 ? 1 : (num14 >= num9 + 26 ? 26 : num14 - num9);
              if (num15 >= num16)
              {
                if (num13 > 134217727 / (36 - num16))
                  throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadPunycode"), "ascii");
                num13 *= 36 - num16;
                num14 += 36;
              }
              else
              {
                num9 = IdnMapping.adapt(num10 - num12, stringBuilder1.Length - index4 - num11 + 1, num12 == 0);
                if (num10 / (stringBuilder1.Length - index4 - num11 + 1) > 134217727 - num8)
                  throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadPunycode"), "ascii");
                num8 += num10 / (stringBuilder1.Length - index4 - num11 + 1);
                int num17 = num10 % (stringBuilder1.Length - index4 - num11 + 1);
                if (num8 < 0 || num8 > 1114111 || num8 >= 55296 && num8 <= 57343)
                  throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadPunycode"), "ascii");
                string str2 = char.ConvertFromUtf32(num8);
                int index2;
                if (num11 > 0)
                {
                  int num18 = num17;
                  index2 = index4;
                  while (num18 > 0)
                  {
                    if (index2 >= stringBuilder1.Length)
                      throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadPunycode"), "ascii");
                    if (char.IsSurrogate(stringBuilder1[index2]))
                      ++index2;
                    --num18;
                    ++index2;
                  }
                }
                else
                  index2 = index4 + num17;
                stringBuilder1.Insert(index2, str2);
                if (IdnMapping.IsSupplementary(num8))
                  ++num11;
                num10 = num17 + 1;
                goto label_49;
              }
            }
            throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadPunycode"), "ascii");
          }
          bool flag = false;
          BidiCategory bidiCategory = CharUnicodeInfo.GetBidiCategory(stringBuilder1.ToString(), index4);
          switch (bidiCategory)
          {
            case BidiCategory.RightToLeft:
            case BidiCategory.RightToLeftArabic:
              flag = true;
              break;
          }
          for (int index2 = index4; index2 < stringBuilder1.Length; ++index2)
          {
            if (!char.IsLowSurrogate(stringBuilder1.ToString(), index2))
            {
              bidiCategory = CharUnicodeInfo.GetBidiCategory(stringBuilder1.ToString(), index2);
              if (flag && bidiCategory == BidiCategory.LeftToRight || !flag && (bidiCategory == BidiCategory.RightToLeft || bidiCategory == BidiCategory.RightToLeftArabic))
                throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadBidi"), "ascii");
            }
          }
          if (flag && bidiCategory != BidiCategory.RightToLeft && bidiCategory != BidiCategory.RightToLeftArabic)
            throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadBidi"), "ascii");
        }
        if (num6 - startIndex > 63)
          throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadLabelSize"), "ascii");
        if (num6 != ascii.Length)
          stringBuilder1.Append('.');
        startIndex = num6 + 1;
        index4 = stringBuilder1.Length;
      }
      int length2 = stringBuilder1.Length;
      int num19 = (int) byte.MaxValue;
      StringBuilder stringBuilder2 = stringBuilder1;
      int index5 = stringBuilder2.Length - 1;
      int num20 = IdnMapping.IsDot(stringBuilder2[index5]) ? 0 : 1;
      int num21 = num19 - num20;
      if (length2 > num21)
      {
        string key = "Argument_IdnBadNameSize";
        object[] objArray = new object[1];
        int index2 = 0;
        int num4 = (int) byte.MaxValue;
        StringBuilder stringBuilder3 = stringBuilder1;
        int index3 = stringBuilder3.Length - 1;
        int num5 = IdnMapping.IsDot(stringBuilder3[index3]) ? 0 : 1;
        // ISSUE: variable of a boxed type
        __Boxed<int> local = (ValueType) (num4 - num5);
        objArray[index2] = (object) local;
        throw new ArgumentException(Environment.GetResourceString(key, objArray), "ascii");
      }
      return stringBuilder1.ToString();
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern int IdnToAscii(uint dwFlags, [MarshalAs(UnmanagedType.LPWStr), In] string lpUnicodeCharStr, int cchUnicodeChar, [Out] char[] lpASCIICharStr, int cchASCIIChar);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern int IdnToUnicode(uint dwFlags, [MarshalAs(UnmanagedType.LPWStr), In] string lpASCIICharStr, int cchASCIIChar, [Out] char[] lpUnicodeCharStr, int cchUnicodeChar);
  }
}
