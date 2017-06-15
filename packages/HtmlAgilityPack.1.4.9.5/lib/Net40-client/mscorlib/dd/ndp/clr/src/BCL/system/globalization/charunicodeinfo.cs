// Decompiled with JetBrains decompiler
// Type: System.Globalization.CharUnicodeInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Globalization
{
  /// <summary>检索 Unicode 字符的信息。此类不能被继承。</summary>
  [__DynamicallyInvokable]
  public static class CharUnicodeInfo
  {
    private static bool s_initialized = CharUnicodeInfo.InitTable();
    internal const char HIGH_SURROGATE_START = '\xD800';
    internal const char HIGH_SURROGATE_END = '\xDBFF';
    internal const char LOW_SURROGATE_START = '\xDC00';
    internal const char LOW_SURROGATE_END = '\xDFFF';
    internal const int UNICODE_CATEGORY_OFFSET = 0;
    internal const int BIDI_CATEGORY_OFFSET = 1;
    [SecurityCritical]
    private static unsafe ushort* s_pCategoryLevel1Index;
    [SecurityCritical]
    private static unsafe byte* s_pCategoriesValue;
    [SecurityCritical]
    private static unsafe ushort* s_pNumericLevel1Index;
    [SecurityCritical]
    private static unsafe byte* s_pNumericValues;
    [SecurityCritical]
    private static unsafe CharUnicodeInfo.DigitValues* s_pDigitValues;
    internal const string UNICODE_INFO_FILE_NAME = "charinfo.nlp";
    internal const int UNICODE_PLANE01_START = 65536;

    [SecuritySafeCritical]
    private static unsafe bool InitTable()
    {
      byte* globalizationResourceBytePtr;
      CharUnicodeInfo.UnicodeDataHeader* unicodeDataHeaderPtr = (CharUnicodeInfo.UnicodeDataHeader*) (globalizationResourceBytePtr = GlobalizationAssembly.GetGlobalizationResourceBytePtr(typeof (CharUnicodeInfo).Assembly, "charinfo.nlp"));
      IntPtr num1 = (IntPtr) unicodeDataHeaderPtr->OffsetToCategoriesIndex;
      CharUnicodeInfo.s_pCategoryLevel1Index = (ushort*) (globalizationResourceBytePtr + num1.ToInt64());
      IntPtr num2 = (IntPtr) unicodeDataHeaderPtr->OffsetToCategoriesValue;
      CharUnicodeInfo.s_pCategoriesValue = globalizationResourceBytePtr + num2.ToInt64();
      IntPtr num3 = (IntPtr) unicodeDataHeaderPtr->OffsetToNumbericIndex;
      CharUnicodeInfo.s_pNumericLevel1Index = (ushort*) (globalizationResourceBytePtr + num3.ToInt64());
      IntPtr num4 = (IntPtr) unicodeDataHeaderPtr->OffsetToNumbericValue;
      CharUnicodeInfo.s_pNumericValues = globalizationResourceBytePtr + num4.ToInt64();
      IntPtr num5 = (IntPtr) unicodeDataHeaderPtr->OffsetToDigitValue;
      CharUnicodeInfo.s_pDigitValues = (CharUnicodeInfo.DigitValues*) (globalizationResourceBytePtr + num5.ToInt64());
      return true;
    }

    internal static int InternalConvertToUtf32(string s, int index)
    {
      if (index < s.Length - 1)
      {
        int num1 = (int) s[index] - 55296;
        if (num1 >= 0 && num1 <= 1023)
        {
          int num2 = (int) s[index + 1] - 56320;
          if (num2 >= 0 && num2 <= 1023)
            return num1 * 1024 + num2 + 65536;
        }
      }
      return (int) s[index];
    }

    internal static int InternalConvertToUtf32(string s, int index, out int charLength)
    {
      charLength = 1;
      if (index < s.Length - 1)
      {
        int num1 = (int) s[index] - 55296;
        if (num1 >= 0 && num1 <= 1023)
        {
          int num2 = (int) s[index + 1] - 56320;
          if (num2 >= 0 && num2 <= 1023)
          {
            ++charLength;
            return num1 * 1024 + num2 + 65536;
          }
        }
      }
      return (int) s[index];
    }

    internal static bool IsWhiteSpace(string s, int index)
    {
      switch (CharUnicodeInfo.GetUnicodeCategory(s, index))
      {
        case UnicodeCategory.SpaceSeparator:
        case UnicodeCategory.LineSeparator:
        case UnicodeCategory.ParagraphSeparator:
          return true;
        default:
          return false;
      }
    }

    internal static bool IsWhiteSpace(char c)
    {
      switch (CharUnicodeInfo.GetUnicodeCategory(c))
      {
        case UnicodeCategory.SpaceSeparator:
        case UnicodeCategory.LineSeparator:
        case UnicodeCategory.ParagraphSeparator:
          return true;
        default:
          return false;
      }
    }

    [SecuritySafeCritical]
    internal static unsafe double InternalGetNumericValue(int ch)
    {
      ushort num1 = CharUnicodeInfo.s_pNumericLevel1Index[ch >> 8];
      ushort num2 = CharUnicodeInfo.s_pNumericLevel1Index[(int) num1 + (ch >> 4 & 15)];
      byte* numPtr = (byte*) (CharUnicodeInfo.s_pNumericLevel1Index + num2);
      return *(double*) (CharUnicodeInfo.s_pNumericValues + ((IntPtr) numPtr[ch & 15] * 8).ToInt64());
    }

    [SecuritySafeCritical]
    internal static unsafe CharUnicodeInfo.DigitValues* InternalGetDigitValues(int ch)
    {
      ushort num1 = CharUnicodeInfo.s_pNumericLevel1Index[ch >> 8];
      ushort num2 = CharUnicodeInfo.s_pNumericLevel1Index[(int) num1 + (ch >> 4 & 15)];
      byte* numPtr = (byte*) (CharUnicodeInfo.s_pNumericLevel1Index + num2);
      return CharUnicodeInfo.s_pDigitValues + numPtr[ch & 15];
    }

    [SecuritySafeCritical]
    internal static unsafe sbyte InternalGetDecimalDigitValue(int ch)
    {
      return CharUnicodeInfo.InternalGetDigitValues(ch)->decimalDigit;
    }

    [SecuritySafeCritical]
    internal static unsafe sbyte InternalGetDigitValue(int ch)
    {
      return CharUnicodeInfo.InternalGetDigitValues(ch)->digit;
    }

    /// <summary>获取与指定字符关联的数值。</summary>
    /// <returns>与指定的字符关联的数值。- 或 --1，如果指定的字符不是一个数值型字符。</returns>
    /// <param name="ch">要获取其数值的 Unicode 字符。</param>
    [__DynamicallyInvokable]
    public static double GetNumericValue(char ch)
    {
      return CharUnicodeInfo.InternalGetNumericValue((int) ch);
    }

    /// <summary>获取与位于指定字符串的指定索引位置的字符关联的数值。</summary>
    /// <returns>与位于指定字符串的指定索引位置的字符关联的数值。- 或 --1，如果位于指定字符串的指定索引位置的字符不是一个数值型字符。</returns>
    /// <param name="s">
    /// <see cref="T:System.String" />，包含要获取其数值的 Unicode 字符。</param>
    /// <param name="index">要获取其数值的 Unicode 字符的索引。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 超出了 <paramref name="s" /> 中的有效索引范围。</exception>
    [__DynamicallyInvokable]
    public static double GetNumericValue(string s, int index)
    {
      if (s == null)
        throw new ArgumentNullException("s");
      if (index < 0 || index >= s.Length)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      return CharUnicodeInfo.InternalGetNumericValue(CharUnicodeInfo.InternalConvertToUtf32(s, index));
    }

    /// <summary>获取指定数值型字符的十进制数字值。</summary>
    /// <returns>指定数值型字符的十进制数字值。- 或 --1，如果指定字符不是十进制数字。</returns>
    /// <param name="ch">获取其十进制数字值的 Unicode 字符。</param>
    public static int GetDecimalDigitValue(char ch)
    {
      return (int) CharUnicodeInfo.InternalGetDecimalDigitValue((int) ch);
    }

    /// <summary>获取位于指定字符串的指定索引位置的数值型字符的十进制数字值。</summary>
    /// <returns>位于指定字符串的指定索引位置的数值型字符的十进制数字值。- 或 --1，如果位于指定字符串的指定索引位置的字符不是十进制数字。</returns>
    /// <param name="s">
    /// <see cref="T:System.String" />，包含获取其十进制数字值的 Unicode 字符。</param>
    /// <param name="index">获取其十进制数字值的 Unicode 字符的索引。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 超出了 <paramref name="s" /> 中的有效索引范围。</exception>
    public static int GetDecimalDigitValue(string s, int index)
    {
      if (s == null)
        throw new ArgumentNullException("s");
      if (index < 0 || index >= s.Length)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      return (int) CharUnicodeInfo.InternalGetDecimalDigitValue(CharUnicodeInfo.InternalConvertToUtf32(s, index));
    }

    /// <summary>获取指定数值型字符的数字值。</summary>
    /// <returns>指定数值型字符的数字值。- 或 --1，如果指定字符不是一个数字。</returns>
    /// <param name="ch">要获取其数字值的 Unicode 字符。</param>
    public static int GetDigitValue(char ch)
    {
      return (int) CharUnicodeInfo.InternalGetDigitValue((int) ch);
    }

    /// <summary>获取位于指定字符串的指定索引位置的数值型字符的数字值。</summary>
    /// <returns>位于指定字符串的指定索引位置的数值型字符的数字值。- 或 --1，如果位于指定字符串的指定索引位置的字符不是一个数字。</returns>
    /// <param name="s">
    /// <see cref="T:System.String" />，包含要获取其数字值的 Unicode 字符。</param>
    /// <param name="index">要获取其数字值的 Unicode 字符的索引。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 超出了 <paramref name="s" /> 中的有效索引范围。</exception>
    public static int GetDigitValue(string s, int index)
    {
      if (s == null)
        throw new ArgumentNullException("s");
      if (index < 0 || index >= s.Length)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      return (int) CharUnicodeInfo.InternalGetDigitValue(CharUnicodeInfo.InternalConvertToUtf32(s, index));
    }

    /// <summary>获取指定字符的 Unicode 类别。</summary>
    /// <returns>一个 <see cref="T:System.Globalization.UnicodeCategory" /> 值，指示指定字符的类别。</returns>
    /// <param name="ch">要获取其 Unicode 类别的 Unicode 字符。</param>
    [__DynamicallyInvokable]
    public static UnicodeCategory GetUnicodeCategory(char ch)
    {
      return CharUnicodeInfo.InternalGetUnicodeCategory((int) ch);
    }

    /// <summary>获取位于指定字符串的指定索引位置的字符的 Unicode 类别。</summary>
    /// <returns>一个 <see cref="T:System.Globalization.UnicodeCategory" /> 值，指示位于指定字符串的指定索引位置的字符的类别。</returns>
    /// <param name="s">
    /// <see cref="T:System.String" />，包含要获取其 Unicode 类别的 Unicode 字符。</param>
    /// <param name="index">要获取其 Unicode 类别的 Unicode 字符的索引。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 超出了 <paramref name="s" /> 中的有效索引范围。</exception>
    [__DynamicallyInvokable]
    public static UnicodeCategory GetUnicodeCategory(string s, int index)
    {
      if (s == null)
        throw new ArgumentNullException("s");
      if ((uint) index >= (uint) s.Length)
        throw new ArgumentOutOfRangeException("index");
      return CharUnicodeInfo.InternalGetUnicodeCategory(s, index);
    }

    internal static UnicodeCategory InternalGetUnicodeCategory(int ch)
    {
      return (UnicodeCategory) CharUnicodeInfo.InternalGetCategoryValue(ch, 0);
    }

    [SecuritySafeCritical]
    internal static unsafe byte InternalGetCategoryValue(int ch, int offset)
    {
      ushort num1 = CharUnicodeInfo.s_pCategoryLevel1Index[ch >> 8];
      ushort num2 = CharUnicodeInfo.s_pCategoryLevel1Index[(int) num1 + (ch >> 4 & 15)];
      byte num3 = *(byte*) ((IntPtr) (CharUnicodeInfo.s_pCategoryLevel1Index + num2) + (ch & 15));
      return CharUnicodeInfo.s_pCategoriesValue[(int) num3 * 2 + offset];
    }

    internal static BidiCategory GetBidiCategory(string s, int index)
    {
      if (s == null)
        throw new ArgumentNullException("s");
      if ((uint) index >= (uint) s.Length)
        throw new ArgumentOutOfRangeException("index");
      return (BidiCategory) CharUnicodeInfo.InternalGetCategoryValue(CharUnicodeInfo.InternalConvertToUtf32(s, index), 1);
    }

    internal static UnicodeCategory InternalGetUnicodeCategory(string value, int index)
    {
      return CharUnicodeInfo.InternalGetUnicodeCategory(CharUnicodeInfo.InternalConvertToUtf32(value, index));
    }

    internal static UnicodeCategory InternalGetUnicodeCategory(string str, int index, out int charLength)
    {
      return CharUnicodeInfo.InternalGetUnicodeCategory(CharUnicodeInfo.InternalConvertToUtf32(str, index, out charLength));
    }

    internal static bool IsCombiningCategory(UnicodeCategory uc)
    {
      if (uc != UnicodeCategory.NonSpacingMark && uc != UnicodeCategory.SpacingCombiningMark)
        return uc == UnicodeCategory.EnclosingMark;
      return true;
    }

    [StructLayout(LayoutKind.Explicit)]
    internal struct UnicodeDataHeader
    {
      [FieldOffset(0)]
      internal char TableName;
      [FieldOffset(32)]
      internal ushort version;
      [FieldOffset(40)]
      internal uint OffsetToCategoriesIndex;
      [FieldOffset(44)]
      internal uint OffsetToCategoriesValue;
      [FieldOffset(48)]
      internal uint OffsetToNumbericIndex;
      [FieldOffset(52)]
      internal uint OffsetToDigitValue;
      [FieldOffset(56)]
      internal uint OffsetToNumbericValue;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    internal struct DigitValues
    {
      internal sbyte decimalDigit;
      internal sbyte digit;
    }
  }
}
