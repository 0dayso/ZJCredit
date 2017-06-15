// Decompiled with JetBrains decompiler
// Type: System.DateTimeParse
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Globalization;
using System.Security;
using System.Text;

namespace System
{
  internal static class DateTimeParse
  {
    internal static DateTimeParse.MatchNumberDelegate m_hebrewNumberParser = new DateTimeParse.MatchNumberDelegate(DateTimeParse.MatchHebrewDigits);
    internal static bool enableAmPmParseAdjustment = DateTimeParse.GetAmPmParseFlag();
    private static DateTimeParse.DS[][] dateParsingStates = new DateTimeParse.DS[20][]{ new DateTimeParse.DS[18]{ DateTimeParse.DS.BEGIN, DateTimeParse.DS.ERROR, DateTimeParse.DS.TX_N, DateTimeParse.DS.N, DateTimeParse.DS.D_Nd, DateTimeParse.DS.T_Nt, DateTimeParse.DS.ERROR, DateTimeParse.DS.D_M, DateTimeParse.DS.D_M, DateTimeParse.DS.D_S, DateTimeParse.DS.T_S, DateTimeParse.DS.BEGIN, DateTimeParse.DS.D_Y, DateTimeParse.DS.D_Y, DateTimeParse.DS.ERROR, DateTimeParse.DS.BEGIN, DateTimeParse.DS.BEGIN, DateTimeParse.DS.ERROR }, new DateTimeParse.DS[18]{ DateTimeParse.DS.ERROR, DateTimeParse.DS.DX_NN, DateTimeParse.DS.ERROR, DateTimeParse.DS.NN, DateTimeParse.DS.D_NNd, DateTimeParse.DS.ERROR, DateTimeParse.DS.DX_NM, DateTimeParse.DS.D_NM, DateTimeParse.DS.D_MNd, DateTimeParse.DS.D_NDS, DateTimeParse.DS.ERROR, DateTimeParse.DS.N, DateTimeParse.DS.D_YN, DateTimeParse.DS.D_YNd, DateTimeParse.DS.DX_YN, DateTimeParse.DS.N, DateTimeParse.DS.N, DateTimeParse.DS.ERROR }, new DateTimeParse.DS[18]{ DateTimeParse.DS.DX_NN, DateTimeParse.DS.DX_NNN, DateTimeParse.DS.TX_N, DateTimeParse.DS.DX_NNN, DateTimeParse.DS.ERROR, DateTimeParse.DS.T_Nt, DateTimeParse.DS.DX_MNN, DateTimeParse.DS.DX_MNN, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.T_S, DateTimeParse.DS.NN, DateTimeParse.DS.DX_NNY, DateTimeParse.DS.ERROR, DateTimeParse.DS.DX_NNY, DateTimeParse.DS.NN, DateTimeParse.DS.NN, DateTimeParse.DS.ERROR }, new DateTimeParse.DS[18]{ DateTimeParse.DS.ERROR, DateTimeParse.DS.DX_NN, DateTimeParse.DS.ERROR, DateTimeParse.DS.D_NN, DateTimeParse.DS.D_NNd, DateTimeParse.DS.ERROR, DateTimeParse.DS.DX_NM, DateTimeParse.DS.D_MN, DateTimeParse.DS.D_MNd, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.D_Nd, DateTimeParse.DS.D_YN, DateTimeParse.DS.D_YNd, DateTimeParse.DS.DX_YN, DateTimeParse.DS.ERROR, DateTimeParse.DS.D_Nd, DateTimeParse.DS.ERROR }, new DateTimeParse.DS[18]{ DateTimeParse.DS.DX_NN, DateTimeParse.DS.DX_NNN, DateTimeParse.DS.TX_N, DateTimeParse.DS.DX_NNN, DateTimeParse.DS.ERROR, DateTimeParse.DS.T_Nt, DateTimeParse.DS.DX_MNN, DateTimeParse.DS.DX_MNN, DateTimeParse.DS.ERROR, DateTimeParse.DS.DX_DS, DateTimeParse.DS.T_S, DateTimeParse.DS.D_NN, DateTimeParse.DS.DX_NNY, DateTimeParse.DS.ERROR, DateTimeParse.DS.DX_NNY, DateTimeParse.DS.ERROR, DateTimeParse.DS.D_NN, DateTimeParse.DS.ERROR }, new DateTimeParse.DS[18]{ DateTimeParse.DS.ERROR, DateTimeParse.DS.DX_NNN, DateTimeParse.DS.DX_NNN, DateTimeParse.DS.DX_NNN, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.DX_MNN, DateTimeParse.DS.DX_MNN, DateTimeParse.DS.ERROR, DateTimeParse.DS.DX_DS, DateTimeParse.DS.ERROR, DateTimeParse.DS.D_NNd, DateTimeParse.DS.DX_NNY, DateTimeParse.DS.ERROR, DateTimeParse.DS.DX_NNY, DateTimeParse.DS.ERROR, DateTimeParse.DS.D_NNd, DateTimeParse.DS.ERROR }, new DateTimeParse.DS[18]{ DateTimeParse.DS.ERROR, DateTimeParse.DS.DX_MN, DateTimeParse.DS.ERROR, DateTimeParse.DS.D_MN, DateTimeParse.DS.D_MNd, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.D_M, DateTimeParse.DS.D_YM, DateTimeParse.DS.D_YMd, DateTimeParse.DS.DX_YM, DateTimeParse.DS.ERROR, DateTimeParse.DS.D_M, DateTimeParse.DS.ERROR }, new DateTimeParse.DS[18]{ DateTimeParse.DS.DX_MN, DateTimeParse.DS.DX_MNN, DateTimeParse.DS.DX_MNN, DateTimeParse.DS.DX_MNN, DateTimeParse.DS.ERROR, DateTimeParse.DS.T_Nt, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.DX_DS, DateTimeParse.DS.T_S, DateTimeParse.DS.D_MN, DateTimeParse.DS.DX_YMN, DateTimeParse.DS.ERROR, DateTimeParse.DS.DX_YMN, DateTimeParse.DS.ERROR, DateTimeParse.DS.D_MN, DateTimeParse.DS.ERROR }, new DateTimeParse.DS[18]{ DateTimeParse.DS.DX_NM, DateTimeParse.DS.DX_MNN, DateTimeParse.DS.DX_MNN, DateTimeParse.DS.DX_MNN, DateTimeParse.DS.ERROR, DateTimeParse.DS.T_Nt, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.DX_DS, DateTimeParse.DS.T_S, DateTimeParse.DS.D_NM, DateTimeParse.DS.DX_YMN, DateTimeParse.DS.ERROR, DateTimeParse.DS.DX_YMN, DateTimeParse.DS.ERROR, DateTimeParse.DS.D_NM, DateTimeParse.DS.ERROR }, new DateTimeParse.DS[18]{ DateTimeParse.DS.ERROR, DateTimeParse.DS.DX_MNN, DateTimeParse.DS.ERROR, DateTimeParse.DS.DX_MNN, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.D_MNd, DateTimeParse.DS.DX_YMN, DateTimeParse.DS.ERROR, DateTimeParse.DS.DX_YMN, DateTimeParse.DS.ERROR, DateTimeParse.DS.D_MNd, DateTimeParse.DS.ERROR }, new DateTimeParse.DS[18]{ DateTimeParse.DS.DX_NDS, DateTimeParse.DS.DX_NNDS, DateTimeParse.DS.DX_NNDS, DateTimeParse.DS.DX_NNDS, DateTimeParse.DS.ERROR, DateTimeParse.DS.T_Nt, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.D_NDS, DateTimeParse.DS.T_S, DateTimeParse.DS.D_NDS, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.D_NDS, DateTimeParse.DS.ERROR }, new DateTimeParse.DS[18]{ DateTimeParse.DS.ERROR, DateTimeParse.DS.DX_YN, DateTimeParse.DS.ERROR, DateTimeParse.DS.D_YN, DateTimeParse.DS.D_YNd, DateTimeParse.DS.ERROR, DateTimeParse.DS.DX_YM, DateTimeParse.DS.D_YM, DateTimeParse.DS.D_YMd, DateTimeParse.DS.D_YM, DateTimeParse.DS.ERROR, DateTimeParse.DS.D_Y, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.D_Y, DateTimeParse.DS.ERROR }, new DateTimeParse.DS[18]{ DateTimeParse.DS.DX_YN, DateTimeParse.DS.DX_YNN, DateTimeParse.DS.DX_YNN, DateTimeParse.DS.DX_YNN, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.DX_YMN, DateTimeParse.DS.DX_YMN, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.D_YN, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.D_YN, DateTimeParse.DS.ERROR }, new DateTimeParse.DS[18]{ DateTimeParse.DS.ERROR, DateTimeParse.DS.DX_YNN, DateTimeParse.DS.DX_YNN, DateTimeParse.DS.DX_YNN, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.DX_YMN, DateTimeParse.DS.DX_YMN, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.D_YN, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.D_YN, DateTimeParse.DS.ERROR }, new DateTimeParse.DS[18]{ DateTimeParse.DS.DX_YM, DateTimeParse.DS.DX_YMN, DateTimeParse.DS.DX_YMN, DateTimeParse.DS.DX_YMN, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.D_YM, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.D_YM, DateTimeParse.DS.ERROR }, new DateTimeParse.DS[18]{ DateTimeParse.DS.ERROR, DateTimeParse.DS.DX_YMN, DateTimeParse.DS.DX_YMN, DateTimeParse.DS.DX_YMN, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.D_YM, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.D_YM, DateTimeParse.DS.ERROR }, new DateTimeParse.DS[18]{ DateTimeParse.DS.DX_DS, DateTimeParse.DS.DX_DSN, DateTimeParse.DS.TX_N, DateTimeParse.DS.T_Nt, DateTimeParse.DS.ERROR, DateTimeParse.DS.T_Nt, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.D_S, DateTimeParse.DS.T_S, DateTimeParse.DS.D_S, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.D_S, DateTimeParse.DS.ERROR }, new DateTimeParse.DS[18]{ DateTimeParse.DS.TX_TS, DateTimeParse.DS.TX_TS, DateTimeParse.DS.TX_TS, DateTimeParse.DS.T_Nt, DateTimeParse.DS.D_Nd, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.D_S, DateTimeParse.DS.T_S, DateTimeParse.DS.T_S, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.T_S, DateTimeParse.DS.T_S, DateTimeParse.DS.ERROR }, new DateTimeParse.DS[18]{ DateTimeParse.DS.ERROR, DateTimeParse.DS.TX_NN, DateTimeParse.DS.TX_NN, DateTimeParse.DS.TX_NN, DateTimeParse.DS.ERROR, DateTimeParse.DS.T_NNt, DateTimeParse.DS.DX_NM, DateTimeParse.DS.D_NM, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.T_S, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.T_Nt, DateTimeParse.DS.T_Nt, DateTimeParse.DS.TX_NN }, new DateTimeParse.DS[18]{ DateTimeParse.DS.ERROR, DateTimeParse.DS.TX_NNN, DateTimeParse.DS.TX_NNN, DateTimeParse.DS.TX_NNN, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.T_S, DateTimeParse.DS.T_NNt, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.ERROR, DateTimeParse.DS.T_NNt, DateTimeParse.DS.T_NNt, DateTimeParse.DS.TX_NNN } };
    internal const int MaxDateTimeNumberDigits = 8;
    internal const string GMTName = "GMT";
    internal const string ZuluName = "Z";
    private const int ORDER_YMD = 0;
    private const int ORDER_MDY = 1;
    private const int ORDER_DMY = 2;
    private const int ORDER_YDM = 3;
    private const int ORDER_YM = 4;
    private const int ORDER_MY = 5;
    private const int ORDER_MD = 6;
    private const int ORDER_DM = 7;

    [SecuritySafeCritical]
    internal static bool GetAmPmParseFlag()
    {
      return DateTime.EnableAmPmParseAdjustment();
    }

    internal static DateTime ParseExact(string s, string format, DateTimeFormatInfo dtfi, DateTimeStyles style)
    {
      DateTimeResult result = new DateTimeResult();
      result.Init();
      if (DateTimeParse.TryParseExact(s, format, dtfi, style, ref result))
        return result.parsedDate;
      throw DateTimeParse.GetDateTimeParseException(ref result);
    }

    internal static DateTime ParseExact(string s, string format, DateTimeFormatInfo dtfi, DateTimeStyles style, out TimeSpan offset)
    {
      DateTimeResult result = new DateTimeResult();
      offset = TimeSpan.Zero;
      result.Init();
      result.flags |= ParseFlags.CaptureOffset;
      if (!DateTimeParse.TryParseExact(s, format, dtfi, style, ref result))
        throw DateTimeParse.GetDateTimeParseException(ref result);
      offset = result.timeZoneOffset;
      return result.parsedDate;
    }

    internal static bool TryParseExact(string s, string format, DateTimeFormatInfo dtfi, DateTimeStyles style, out DateTime result)
    {
      result = DateTime.MinValue;
      DateTimeResult result1 = new DateTimeResult();
      result1.Init();
      if (!DateTimeParse.TryParseExact(s, format, dtfi, style, ref result1))
        return false;
      result = result1.parsedDate;
      return true;
    }

    internal static bool TryParseExact(string s, string format, DateTimeFormatInfo dtfi, DateTimeStyles style, out DateTime result, out TimeSpan offset)
    {
      result = DateTime.MinValue;
      offset = TimeSpan.Zero;
      DateTimeResult result1 = new DateTimeResult();
      result1.Init();
      result1.flags |= ParseFlags.CaptureOffset;
      if (!DateTimeParse.TryParseExact(s, format, dtfi, style, ref result1))
        return false;
      result = result1.parsedDate;
      offset = result1.timeZoneOffset;
      return true;
    }

    internal static bool TryParseExact(string s, string format, DateTimeFormatInfo dtfi, DateTimeStyles style, ref DateTimeResult result)
    {
      if (s == null)
      {
        result.SetFailure(ParseFailureKind.ArgumentNull, "ArgumentNull_String", (object) null, "s");
        return false;
      }
      if (format == null)
      {
        result.SetFailure(ParseFailureKind.ArgumentNull, "ArgumentNull_String", (object) null, "format");
        return false;
      }
      if (s.Length == 0)
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
      if (format.Length != 0)
        return DateTimeParse.DoStrictParse(s, format, style, dtfi, ref result);
      result.SetFailure(ParseFailureKind.Format, "Format_BadFormatSpecifier", (object) null);
      return false;
    }

    internal static DateTime ParseExactMultiple(string s, string[] formats, DateTimeFormatInfo dtfi, DateTimeStyles style)
    {
      DateTimeResult result = new DateTimeResult();
      result.Init();
      if (DateTimeParse.TryParseExactMultiple(s, formats, dtfi, style, ref result))
        return result.parsedDate;
      throw DateTimeParse.GetDateTimeParseException(ref result);
    }

    internal static DateTime ParseExactMultiple(string s, string[] formats, DateTimeFormatInfo dtfi, DateTimeStyles style, out TimeSpan offset)
    {
      DateTimeResult result = new DateTimeResult();
      offset = TimeSpan.Zero;
      result.Init();
      result.flags |= ParseFlags.CaptureOffset;
      if (!DateTimeParse.TryParseExactMultiple(s, formats, dtfi, style, ref result))
        throw DateTimeParse.GetDateTimeParseException(ref result);
      offset = result.timeZoneOffset;
      return result.parsedDate;
    }

    internal static bool TryParseExactMultiple(string s, string[] formats, DateTimeFormatInfo dtfi, DateTimeStyles style, out DateTime result, out TimeSpan offset)
    {
      result = DateTime.MinValue;
      offset = TimeSpan.Zero;
      DateTimeResult result1 = new DateTimeResult();
      result1.Init();
      result1.flags |= ParseFlags.CaptureOffset;
      if (!DateTimeParse.TryParseExactMultiple(s, formats, dtfi, style, ref result1))
        return false;
      result = result1.parsedDate;
      offset = result1.timeZoneOffset;
      return true;
    }

    internal static bool TryParseExactMultiple(string s, string[] formats, DateTimeFormatInfo dtfi, DateTimeStyles style, out DateTime result)
    {
      result = DateTime.MinValue;
      DateTimeResult result1 = new DateTimeResult();
      result1.Init();
      if (!DateTimeParse.TryParseExactMultiple(s, formats, dtfi, style, ref result1))
        return false;
      result = result1.parsedDate;
      return true;
    }

    internal static bool TryParseExactMultiple(string s, string[] formats, DateTimeFormatInfo dtfi, DateTimeStyles style, ref DateTimeResult result)
    {
      if (s == null)
      {
        result.SetFailure(ParseFailureKind.ArgumentNull, "ArgumentNull_String", (object) null, "s");
        return false;
      }
      if (formats == null)
      {
        result.SetFailure(ParseFailureKind.ArgumentNull, "ArgumentNull_String", (object) null, "formats");
        return false;
      }
      if (s.Length == 0)
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
      if (formats.Length == 0)
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadFormatSpecifier", (object) null);
        return false;
      }
      for (int index = 0; index < formats.Length; ++index)
      {
        if (formats[index] == null || formats[index].Length == 0)
        {
          result.SetFailure(ParseFailureKind.Format, "Format_BadFormatSpecifier", (object) null);
          return false;
        }
        DateTimeResult result1 = new DateTimeResult();
        result1.Init();
        result1.flags = result.flags;
        if (DateTimeParse.TryParseExact(s, formats[index], dtfi, style, ref result1))
        {
          result.parsedDate = result1.parsedDate;
          result.timeZoneOffset = result1.timeZoneOffset;
          return true;
        }
      }
      result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
      return false;
    }

    private static bool MatchWord(ref __DTString str, string target)
    {
      int length = target.Length;
      if (length > str.Value.Length - str.Index || str.CompareInfo.Compare(str.Value, str.Index, length, target, 0, length, CompareOptions.IgnoreCase) != 0)
        return false;
      int index = str.Index + target.Length;
      if (index < str.Value.Length && char.IsLetter(str.Value[index]))
        return false;
      str.Index = index;
      if (str.Index < str.len)
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        __DTString& local = @str;
        // ISSUE: explicit reference operation
        int num = (int) (^local).Value[str.Index];
        // ISSUE: explicit reference operation
        (^local).m_current = (char) num;
      }
      return true;
    }

    private static bool GetTimeZoneName(ref __DTString str)
    {
      return DateTimeParse.MatchWord(ref str, "GMT") || DateTimeParse.MatchWord(ref str, "Z");
    }

    internal static bool IsDigit(char ch)
    {
      if ((int) ch >= 48)
        return (int) ch <= 57;
      return false;
    }

    private static bool ParseFraction(ref __DTString str, out double result)
    {
      result = 0.0;
      double num1 = 0.1;
      int num2 = 0;
      char ch;
      while (str.GetNext() && DateTimeParse.IsDigit(ch = str.m_current))
      {
        result += (double) ((int) ch - 48) * num1;
        num1 *= 0.1;
        ++num2;
      }
      return num2 > 0;
    }

    private static bool ParseTimeZone(ref __DTString str, ref TimeSpan result)
    {
      int minutes = 0;
      DTSubString subString1 = str.GetSubString();
      if (subString1.length != 1)
        return false;
      char ch = subString1[0];
      switch (ch)
      {
        case '+':
        case '-':
          str.ConsumeSubString(subString1);
          DTSubString subString2 = str.GetSubString();
          if (subString2.type != DTSubStringType.Number)
            return false;
          int num = subString2.value;
          int hours;
          switch (subString2.length)
          {
            case 1:
            case 2:
              hours = num;
              str.ConsumeSubString(subString2);
              DTSubString subString3 = str.GetSubString();
              if (subString3.length == 1 && (int) subString3[0] == 58)
              {
                str.ConsumeSubString(subString3);
                DTSubString subString4 = str.GetSubString();
                if (subString4.type != DTSubStringType.Number || subString4.length < 1 || subString4.length > 2)
                  return false;
                minutes = subString4.value;
                str.ConsumeSubString(subString4);
                break;
              }
              break;
            case 3:
            case 4:
              hours = num / 100;
              minutes = num % 100;
              str.ConsumeSubString(subString2);
              break;
            default:
              return false;
          }
          if (minutes < 0 || minutes >= 60)
            return false;
          result = new TimeSpan(hours, minutes, 0);
          if ((int) ch == 45)
          {
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            TimeSpan& local = @result;
            // ISSUE: explicit reference operation
            TimeSpan timeSpan = (^local).Negate();
            // ISSUE: explicit reference operation
            ^local = timeSpan;
          }
          return true;
        default:
          return false;
      }
    }

    private static bool HandleTimeZone(ref __DTString str, ref DateTimeResult result)
    {
      if (str.Index < str.len - 1)
      {
        char c = str.Value[str.Index];
        int num;
        for (num = 0; char.IsWhiteSpace(c) && str.Index + num < str.len - 1; c = str.Value[str.Index + num])
          ++num;
        if ((int) c == 43 || (int) c == 45)
        {
          str.Index += num;
          if ((result.flags & ParseFlags.TimeZoneUsed) != (ParseFlags) 0)
          {
            result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
            return false;
          }
          result.flags |= ParseFlags.TimeZoneUsed;
          if (!DateTimeParse.ParseTimeZone(ref str, ref result.timeZoneOffset))
          {
            result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
            return false;
          }
        }
      }
      return true;
    }

    [SecuritySafeCritical]
    private static bool Lex(DateTimeParse.DS dps, ref __DTString str, ref DateTimeToken dtok, ref DateTimeRawInfo raw, ref DateTimeResult result, ref DateTimeFormatInfo dtfi, DateTimeStyles styles)
    {
      dtok.dtt = DateTimeParse.DTT.Unk;
      TokenType tokenType;
      int tokenValue;
      str.GetRegularToken(out tokenType, out tokenValue, dtfi);
      TokenType separatorToken1;
      switch (tokenType)
      {
        case TokenType.NumberToken:
        case TokenType.YearNumberToken:
          if (raw.numCount == 3 || tokenValue == -1)
          {
            result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
            return false;
          }
          if (dps == DateTimeParse.DS.T_NNt && str.Index < str.len - 1 && (int) str.Value[str.Index] == 46)
            DateTimeParse.ParseFraction(ref str, out raw.fraction);
          if ((dps == DateTimeParse.DS.T_NNt || dps == DateTimeParse.DS.T_Nt) && (str.Index < str.len - 1 && !DateTimeParse.HandleTimeZone(ref str, ref result)))
            return false;
          dtok.num = tokenValue;
          if (tokenType == TokenType.YearNumberToken)
          {
            if (raw.year == -1)
            {
              raw.year = tokenValue;
              int indexBeforeSeparator;
              char charBeforeSeparator;
              TokenType separatorToken2;
              switch (separatorToken2 = str.GetSeparatorToken(dtfi, out indexBeforeSeparator, out charBeforeSeparator))
              {
                case TokenType.SEP_SecondSuff:
                case TokenType.SEP_HourSuff:
                case TokenType.SEP_MinuteSuff:
                  dtok.dtt = DateTimeParse.DTT.NumTimesuff;
                  dtok.suffix = separatorToken2;
                  break;
                case TokenType.SEP_DateOrOffset:
                  if (DateTimeParse.dateParsingStates[(int) dps][13] == DateTimeParse.DS.ERROR && DateTimeParse.dateParsingStates[(int) dps][12] > DateTimeParse.DS.ERROR)
                  {
                    str.Index = indexBeforeSeparator;
                    str.m_current = charBeforeSeparator;
                    dtok.dtt = DateTimeParse.DTT.YearSpace;
                    break;
                  }
                  dtok.dtt = DateTimeParse.DTT.YearDateSep;
                  break;
                case TokenType.SEP_YearSuff:
                case TokenType.SEP_MonthSuff:
                case TokenType.SEP_DaySuff:
                  dtok.dtt = DateTimeParse.DTT.NumDatesuff;
                  dtok.suffix = separatorToken2;
                  break;
                case TokenType.SEP_Pm:
                case TokenType.SEP_Am:
                  if (raw.timeMark == DateTimeParse.TM.NotSet)
                  {
                    raw.timeMark = separatorToken2 == TokenType.SEP_Am ? DateTimeParse.TM.AM : DateTimeParse.TM.PM;
                    dtok.dtt = DateTimeParse.DTT.YearSpace;
                    break;
                  }
                  result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
                  break;
                case TokenType.SEP_Date:
                  dtok.dtt = DateTimeParse.DTT.YearDateSep;
                  break;
                case TokenType.SEP_Time:
                  if (!raw.hasSameDateAndTimeSeparators)
                  {
                    result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
                    return false;
                  }
                  dtok.dtt = DateTimeParse.DTT.YearDateSep;
                  break;
                case TokenType.SEP_End:
                  dtok.dtt = DateTimeParse.DTT.YearEnd;
                  break;
                case TokenType.SEP_Space:
                  dtok.dtt = DateTimeParse.DTT.YearSpace;
                  break;
                default:
                  result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
                  return false;
              }
              return true;
            }
            result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
            return false;
          }
          int indexBeforeSeparator1;
          char charBeforeSeparator1;
          TokenType separatorToken3;
          switch (separatorToken3 = str.GetSeparatorToken(dtfi, out indexBeforeSeparator1, out charBeforeSeparator1))
          {
            case TokenType.SEP_LocalTimeMark:
              dtok.dtt = DateTimeParse.DTT.NumLocalTimeMark;
              raw.AddNumber(dtok.num);
              break;
            case TokenType.SEP_DateOrOffset:
              if (DateTimeParse.dateParsingStates[(int) dps][4] == DateTimeParse.DS.ERROR && DateTimeParse.dateParsingStates[(int) dps][3] > DateTimeParse.DS.ERROR)
              {
                str.Index = indexBeforeSeparator1;
                str.m_current = charBeforeSeparator1;
                dtok.dtt = DateTimeParse.DTT.NumSpace;
              }
              else
                dtok.dtt = DateTimeParse.DTT.NumDatesep;
              raw.AddNumber(dtok.num);
              break;
            case TokenType.SEP_MinuteSuff:
            case TokenType.SEP_SecondSuff:
            case TokenType.SEP_HourSuff:
              dtok.dtt = DateTimeParse.DTT.NumTimesuff;
              dtok.suffix = separatorToken3;
              break;
            case TokenType.SEP_MonthSuff:
            case TokenType.SEP_DaySuff:
              dtok.dtt = DateTimeParse.DTT.NumDatesuff;
              dtok.suffix = separatorToken3;
              break;
            case TokenType.SEP_Time:
              if (raw.hasSameDateAndTimeSeparators && (dps == DateTimeParse.DS.D_Y || dps == DateTimeParse.DS.D_YN || (dps == DateTimeParse.DS.D_YNd || dps == DateTimeParse.DS.D_YM) || dps == DateTimeParse.DS.D_YMd))
              {
                dtok.dtt = DateTimeParse.DTT.NumDatesep;
                raw.AddNumber(dtok.num);
                break;
              }
              dtok.dtt = DateTimeParse.DTT.NumTimesep;
              raw.AddNumber(dtok.num);
              break;
            case TokenType.SEP_YearSuff:
              try
              {
                dtok.num = dtfi.Calendar.ToFourDigitYear(tokenValue);
              }
              catch (ArgumentOutOfRangeException ex)
              {
                result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) ex);
                return false;
              }
              dtok.dtt = DateTimeParse.DTT.NumDatesuff;
              dtok.suffix = separatorToken3;
              break;
            case TokenType.SEP_Pm:
            case TokenType.SEP_Am:
              if (raw.timeMark == DateTimeParse.TM.NotSet)
              {
                raw.timeMark = separatorToken3 == TokenType.SEP_Am ? DateTimeParse.TM.AM : DateTimeParse.TM.PM;
                dtok.dtt = DateTimeParse.DTT.NumAmpm;
                if (dps == DateTimeParse.DS.D_NN && DateTimeParse.enableAmPmParseAdjustment && !DateTimeParse.ProcessTerminaltState(DateTimeParse.DS.DX_NN, ref result, ref styles, ref raw, dtfi))
                  return false;
                raw.AddNumber(dtok.num);
                if ((dps == DateTimeParse.DS.T_NNt || dps == DateTimeParse.DS.T_Nt) && !DateTimeParse.HandleTimeZone(ref str, ref result))
                  return false;
                break;
              }
              result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
              break;
            case TokenType.SEP_Date:
              dtok.dtt = DateTimeParse.DTT.NumDatesep;
              raw.AddNumber(dtok.num);
              break;
            case TokenType.SEP_End:
              dtok.dtt = DateTimeParse.DTT.NumEnd;
              raw.AddNumber(dtok.num);
              break;
            case TokenType.SEP_Space:
              dtok.dtt = DateTimeParse.DTT.NumSpace;
              raw.AddNumber(dtok.num);
              break;
            default:
              result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
              return false;
          }
        case TokenType.Am:
        case TokenType.Pm:
          if (raw.timeMark == DateTimeParse.TM.NotSet)
          {
            raw.timeMark = (DateTimeParse.TM) tokenValue;
            break;
          }
          result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
          return false;
        case TokenType.MonthToken:
          if (raw.month == -1)
          {
            int indexBeforeSeparator2;
            char charBeforeSeparator2;
            switch (separatorToken1 = str.GetSeparatorToken(dtfi, out indexBeforeSeparator2, out charBeforeSeparator2))
            {
              case TokenType.SEP_Date:
                dtok.dtt = DateTimeParse.DTT.MonthDatesep;
                break;
              case TokenType.SEP_Time:
                if (!raw.hasSameDateAndTimeSeparators)
                {
                  result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
                  return false;
                }
                dtok.dtt = DateTimeParse.DTT.MonthDatesep;
                break;
              case TokenType.SEP_DateOrOffset:
                if (DateTimeParse.dateParsingStates[(int) dps][8] == DateTimeParse.DS.ERROR && DateTimeParse.dateParsingStates[(int) dps][7] > DateTimeParse.DS.ERROR)
                {
                  str.Index = indexBeforeSeparator2;
                  str.m_current = charBeforeSeparator2;
                  dtok.dtt = DateTimeParse.DTT.MonthSpace;
                  break;
                }
                dtok.dtt = DateTimeParse.DTT.MonthDatesep;
                break;
              case TokenType.SEP_End:
                dtok.dtt = DateTimeParse.DTT.MonthEnd;
                break;
              case TokenType.SEP_Space:
                dtok.dtt = DateTimeParse.DTT.MonthSpace;
                break;
              default:
                result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
                return false;
            }
            raw.month = tokenValue;
            break;
          }
          result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
          return false;
        case TokenType.EndOfString:
          dtok.dtt = DateTimeParse.DTT.End;
          break;
        case TokenType.DayOfWeekToken:
          if (raw.dayOfWeek == -1)
          {
            raw.dayOfWeek = tokenValue;
            dtok.dtt = DateTimeParse.DTT.DayOfWeek;
            break;
          }
          result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
          return false;
        case TokenType.TimeZoneToken:
          if ((result.flags & ParseFlags.TimeZoneUsed) != (ParseFlags) 0)
          {
            result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
            return false;
          }
          dtok.dtt = DateTimeParse.DTT.TimeZone;
          result.flags |= ParseFlags.TimeZoneUsed;
          result.timeZoneOffset = new TimeSpan(0L);
          result.flags |= ParseFlags.TimeZoneUtc;
          break;
        case TokenType.EraToken:
          if (result.era != -1)
          {
            result.era = tokenValue;
            dtok.dtt = DateTimeParse.DTT.Era;
            break;
          }
          result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
          return false;
        case TokenType.UnknownToken:
          if (char.IsLetter(str.m_current))
          {
            result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_UnknowDateTimeWord", (object) str.Index);
            return false;
          }
          if (Environment.GetCompatibilityFlag(CompatibilityFlag.DateTimeParseIgnorePunctuation) && (result.flags & ParseFlags.CaptureOffset) == (ParseFlags) 0)
          {
            str.GetNext();
            return true;
          }
          if (((int) str.m_current == 45 || (int) str.m_current == 43) && (result.flags & ParseFlags.TimeZoneUsed) == (ParseFlags) 0)
          {
            int num = str.Index;
            if (DateTimeParse.ParseTimeZone(ref str, ref result.timeZoneOffset))
            {
              result.flags |= ParseFlags.TimeZoneUsed;
              return true;
            }
            str.Index = num;
          }
          if (DateTimeParse.VerifyValidPunctuation(ref str))
            return true;
          result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
          return false;
        case TokenType.HebrewNumber:
          if (tokenValue >= 100)
          {
            if (raw.year == -1)
            {
              raw.year = tokenValue;
              int indexBeforeSeparator2;
              char charBeforeSeparator2;
              switch (separatorToken1 = str.GetSeparatorToken(dtfi, out indexBeforeSeparator2, out charBeforeSeparator2))
              {
                case TokenType.SEP_End:
                  dtok.dtt = DateTimeParse.DTT.YearEnd;
                  goto label_111;
                case TokenType.SEP_Space:
                  dtok.dtt = DateTimeParse.DTT.YearSpace;
                  goto label_111;
                case TokenType.SEP_DateOrOffset:
                  if (DateTimeParse.dateParsingStates[(int) dps][12] > DateTimeParse.DS.ERROR)
                  {
                    str.Index = indexBeforeSeparator2;
                    str.m_current = charBeforeSeparator2;
                    dtok.dtt = DateTimeParse.DTT.YearSpace;
                    goto label_111;
                  }
                  else
                    break;
              }
              result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
              return false;
            }
            result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
            return false;
          }
          dtok.num = tokenValue;
          raw.AddNumber(dtok.num);
          int indexBeforeSeparator3;
          char charBeforeSeparator3;
          switch (separatorToken1 = str.GetSeparatorToken(dtfi, out indexBeforeSeparator3, out charBeforeSeparator3))
          {
            case TokenType.SEP_Date:
            case TokenType.SEP_Space:
              dtok.dtt = DateTimeParse.DTT.NumDatesep;
              break;
            case TokenType.SEP_DateOrOffset:
              if (DateTimeParse.dateParsingStates[(int) dps][4] == DateTimeParse.DS.ERROR && DateTimeParse.dateParsingStates[(int) dps][3] > DateTimeParse.DS.ERROR)
              {
                str.Index = indexBeforeSeparator3;
                str.m_current = charBeforeSeparator3;
                dtok.dtt = DateTimeParse.DTT.NumSpace;
                break;
              }
              dtok.dtt = DateTimeParse.DTT.NumDatesep;
              break;
            case TokenType.SEP_End:
              dtok.dtt = DateTimeParse.DTT.NumEnd;
              break;
            default:
              result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
              return false;
          }
        case TokenType.JapaneseEraToken:
          result.calendar = JapaneseCalendar.GetDefaultInstance();
          dtfi = DateTimeFormatInfo.GetJapaneseCalendarDTFI();
          if (result.era != -1)
          {
            result.era = tokenValue;
            dtok.dtt = DateTimeParse.DTT.Era;
            break;
          }
          result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
          return false;
        case TokenType.TEraToken:
          result.calendar = TaiwanCalendar.GetDefaultInstance();
          dtfi = DateTimeFormatInfo.GetTaiwanCalendarDTFI();
          if (result.era != -1)
          {
            result.era = tokenValue;
            dtok.dtt = DateTimeParse.DTT.Era;
            break;
          }
          result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
          return false;
      }
label_111:
      return true;
    }

    private static bool VerifyValidPunctuation(ref __DTString str)
    {
      switch (str.Value[str.Index])
      {
        case '#':
          bool flag1 = false;
          bool flag2 = false;
          for (int index = 0; index < str.len; ++index)
          {
            char c = str.Value[index];
            switch (c)
            {
              case '#':
                if (flag1)
                {
                  if (flag2)
                    return false;
                  flag2 = true;
                  break;
                }
                flag1 = true;
                break;
              case char.MinValue:
                if (!flag2)
                  return false;
                break;
              default:
                if (!char.IsWhiteSpace(c) && !flag1 | flag2)
                  return false;
                break;
            }
          }
          if (!flag2)
            return false;
          str.GetNext();
          return true;
        case char.MinValue:
          for (int index = str.Index; index < str.len; ++index)
          {
            if ((int) str.Value[index] != 0)
              return false;
          }
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          __DTString& local = @str;
          // ISSUE: explicit reference operation
          int num = (^local).len;
          // ISSUE: explicit reference operation
          (^local).Index = num;
          return true;
        default:
          return false;
      }
    }

    private static bool GetYearMonthDayOrder(string datePattern, DateTimeFormatInfo dtfi, out int order)
    {
      int num1 = -1;
      int num2 = -1;
      int num3 = -1;
      int num4 = 0;
      bool flag = false;
      for (int index = 0; index < datePattern.Length && num4 < 3; ++index)
      {
        char ch = datePattern[index];
        switch (ch)
        {
          case '\\':
          case '%':
            ++index;
            break;
          case '\'':
          case '"':
            flag = !flag;
            goto default;
          default:
            if (!flag)
            {
              if ((int) ch == 121)
              {
                num1 = num4++;
                while (index + 1 < datePattern.Length && (int) datePattern[index + 1] == 121)
                  ++index;
                break;
              }
              if ((int) ch == 77)
              {
                num2 = num4++;
                while (index + 1 < datePattern.Length && (int) datePattern[index + 1] == 77)
                  ++index;
                break;
              }
              if ((int) ch == 100)
              {
                int num5 = 1;
                for (; index + 1 < datePattern.Length && (int) datePattern[index + 1] == 100; ++index)
                  ++num5;
                if (num5 <= 2)
                {
                  num3 = num4++;
                  break;
                }
                break;
              }
              break;
            }
            break;
        }
      }
      if (num1 == 0 && num2 == 1 && num3 == 2)
      {
        order = 0;
        return true;
      }
      if (num2 == 0 && num3 == 1 && num1 == 2)
      {
        order = 1;
        return true;
      }
      if (num3 == 0 && num2 == 1 && num1 == 2)
      {
        order = 2;
        return true;
      }
      if (num1 == 0 && num3 == 1 && num2 == 2)
      {
        order = 3;
        return true;
      }
      order = -1;
      return false;
    }

    private static bool GetYearMonthOrder(string pattern, DateTimeFormatInfo dtfi, out int order)
    {
      int num1 = -1;
      int num2 = -1;
      int num3 = 0;
      bool flag = false;
      for (int index = 0; index < pattern.Length && num3 < 2; ++index)
      {
        char ch = pattern[index];
        switch (ch)
        {
          case '\\':
          case '%':
            ++index;
            break;
          case '\'':
          case '"':
            flag = !flag;
            goto default;
          default:
            if (!flag)
            {
              if ((int) ch == 121)
              {
                num1 = num3++;
                while (index + 1 < pattern.Length && (int) pattern[index + 1] == 121)
                  ++index;
                break;
              }
              if ((int) ch == 77)
              {
                num2 = num3++;
                while (index + 1 < pattern.Length && (int) pattern[index + 1] == 77)
                  ++index;
                break;
              }
              break;
            }
            break;
        }
      }
      if (num1 == 0 && num2 == 1)
      {
        order = 4;
        return true;
      }
      if (num2 == 0 && num1 == 1)
      {
        order = 5;
        return true;
      }
      order = -1;
      return false;
    }

    private static bool GetMonthDayOrder(string pattern, DateTimeFormatInfo dtfi, out int order)
    {
      int num1 = -1;
      int num2 = -1;
      int num3 = 0;
      bool flag = false;
      for (int index = 0; index < pattern.Length && num3 < 2; ++index)
      {
        char ch = pattern[index];
        switch (ch)
        {
          case '\\':
          case '%':
            ++index;
            break;
          case '\'':
          case '"':
            flag = !flag;
            goto default;
          default:
            if (!flag)
            {
              if ((int) ch == 100)
              {
                int num4 = 1;
                for (; index + 1 < pattern.Length && (int) pattern[index + 1] == 100; ++index)
                  ++num4;
                if (num4 <= 2)
                {
                  num2 = num3++;
                  break;
                }
                break;
              }
              if ((int) ch == 77)
              {
                num1 = num3++;
                while (index + 1 < pattern.Length && (int) pattern[index + 1] == 77)
                  ++index;
                break;
              }
              break;
            }
            break;
        }
      }
      if (num1 == 0 && num2 == 1)
      {
        order = 6;
        return true;
      }
      if (num2 == 0 && num1 == 1)
      {
        order = 7;
        return true;
      }
      order = -1;
      return false;
    }

    private static bool TryAdjustYear(ref DateTimeResult result, int year, out int adjustedYear)
    {
      if (year < 100)
      {
        try
        {
          year = result.calendar.ToFourDigitYear(year);
        }
        catch (ArgumentOutOfRangeException ex)
        {
          adjustedYear = -1;
          return false;
        }
      }
      adjustedYear = year;
      return true;
    }

    private static bool SetDateYMD(ref DateTimeResult result, int year, int month, int day)
    {
      if (!result.calendar.IsValidDay(year, month, day, result.era))
        return false;
      result.SetDate(year, month, day);
      return true;
    }

    private static bool SetDateMDY(ref DateTimeResult result, int month, int day, int year)
    {
      return DateTimeParse.SetDateYMD(ref result, year, month, day);
    }

    private static bool SetDateDMY(ref DateTimeResult result, int day, int month, int year)
    {
      return DateTimeParse.SetDateYMD(ref result, year, month, day);
    }

    private static bool SetDateYDM(ref DateTimeResult result, int year, int day, int month)
    {
      return DateTimeParse.SetDateYMD(ref result, year, month, day);
    }

    private static void GetDefaultYear(ref DateTimeResult result, ref DateTimeStyles styles)
    {
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      DateTimeResult& local = @result;
      // ISSUE: explicit reference operation
      int year = (^local).calendar.GetYear(DateTimeParse.GetDateTimeNow(ref result, ref styles));
      // ISSUE: explicit reference operation
      (^local).Year = year;
      result.flags |= ParseFlags.YearDefault;
    }

    private static bool GetDayOfNN(ref DateTimeResult result, ref DateTimeStyles styles, ref DateTimeRawInfo raw, DateTimeFormatInfo dtfi)
    {
      if ((result.flags & ParseFlags.HaveDate) != (ParseFlags) 0)
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
      int number1 = raw.GetNumber(0);
      int number2 = raw.GetNumber(1);
      DateTimeParse.GetDefaultYear(ref result, ref styles);
      int order;
      if (!DateTimeParse.GetMonthDayOrder(dtfi.MonthDayPattern, dtfi, out order))
      {
        result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_BadDatePattern", (object) dtfi.MonthDayPattern);
        return false;
      }
      if (order == 6)
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        DateTimeResult& result1 = @result;
        // ISSUE: explicit reference operation
        int year = (^result1).Year;
        int month = number1;
        int day = number2;
        if (DateTimeParse.SetDateYMD(result1, year, month, day))
        {
          result.flags |= ParseFlags.HaveDate;
          return true;
        }
      }
      else
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        DateTimeResult& result1 = @result;
        // ISSUE: explicit reference operation
        int year = (^result1).Year;
        int month = number2;
        int day = number1;
        if (DateTimeParse.SetDateYMD(result1, year, month, day))
        {
          result.flags |= ParseFlags.HaveDate;
          return true;
        }
      }
      result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
      return false;
    }

    private static bool GetDayOfNNN(ref DateTimeResult result, ref DateTimeRawInfo raw, DateTimeFormatInfo dtfi)
    {
      if ((result.flags & ParseFlags.HaveDate) != (ParseFlags) 0)
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
      int number1 = raw.GetNumber(0);
      int number2 = raw.GetNumber(1);
      int number3 = raw.GetNumber(2);
      int order;
      if (!DateTimeParse.GetYearMonthDayOrder(dtfi.ShortDatePattern, dtfi, out order))
      {
        result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_BadDatePattern", (object) dtfi.ShortDatePattern);
        return false;
      }
      if (order == 0)
      {
        int adjustedYear;
        if (DateTimeParse.TryAdjustYear(ref result, number1, out adjustedYear) && DateTimeParse.SetDateYMD(ref result, adjustedYear, number2, number3))
        {
          result.flags |= ParseFlags.HaveDate;
          return true;
        }
      }
      else if (order == 1)
      {
        int adjustedYear;
        if (DateTimeParse.TryAdjustYear(ref result, number3, out adjustedYear) && DateTimeParse.SetDateMDY(ref result, number1, number2, adjustedYear))
        {
          result.flags |= ParseFlags.HaveDate;
          return true;
        }
      }
      else if (order == 2)
      {
        int adjustedYear;
        if (DateTimeParse.TryAdjustYear(ref result, number3, out adjustedYear) && DateTimeParse.SetDateDMY(ref result, number1, number2, adjustedYear))
        {
          result.flags |= ParseFlags.HaveDate;
          return true;
        }
      }
      else
      {
        int adjustedYear;
        if (order == 3 && DateTimeParse.TryAdjustYear(ref result, number1, out adjustedYear) && DateTimeParse.SetDateYDM(ref result, adjustedYear, number2, number3))
        {
          result.flags |= ParseFlags.HaveDate;
          return true;
        }
      }
      result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
      return false;
    }

    private static bool GetDayOfMN(ref DateTimeResult result, ref DateTimeStyles styles, ref DateTimeRawInfo raw, DateTimeFormatInfo dtfi)
    {
      if ((result.flags & ParseFlags.HaveDate) != (ParseFlags) 0)
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
      int order1;
      if (!DateTimeParse.GetMonthDayOrder(dtfi.MonthDayPattern, dtfi, out order1))
      {
        result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_BadDatePattern", (object) dtfi.MonthDayPattern);
        return false;
      }
      if (order1 == 7)
      {
        int order2;
        if (!DateTimeParse.GetYearMonthOrder(dtfi.YearMonthPattern, dtfi, out order2))
        {
          result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_BadDatePattern", (object) dtfi.YearMonthPattern);
          return false;
        }
        if (order2 == 5)
        {
          int adjustedYear;
          if (DateTimeParse.TryAdjustYear(ref result, raw.GetNumber(0), out adjustedYear) && DateTimeParse.SetDateYMD(ref result, adjustedYear, raw.month, 1))
            return true;
          result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
          return false;
        }
      }
      DateTimeParse.GetDefaultYear(ref result, ref styles);
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      DateTimeResult& result1 = @result;
      // ISSUE: explicit reference operation
      int year = (^result1).Year;
      int month = raw.month;
      int number = raw.GetNumber(0);
      if (DateTimeParse.SetDateYMD(result1, year, month, number))
        return true;
      result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
      return false;
    }

    private static bool GetHebrewDayOfNM(ref DateTimeResult result, ref DateTimeRawInfo raw, DateTimeFormatInfo dtfi)
    {
      int order;
      if (!DateTimeParse.GetMonthDayOrder(dtfi.MonthDayPattern, dtfi, out order))
      {
        result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_BadDatePattern", (object) dtfi.MonthDayPattern);
        return false;
      }
      result.Month = raw.month;
      if ((order == 7 || order == 6) && result.calendar.IsValidDay(result.Year, result.Month, raw.GetNumber(0), result.era))
      {
        result.Day = raw.GetNumber(0);
        return true;
      }
      result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
      return false;
    }

    private static bool GetDayOfNM(ref DateTimeResult result, ref DateTimeStyles styles, ref DateTimeRawInfo raw, DateTimeFormatInfo dtfi)
    {
      if ((result.flags & ParseFlags.HaveDate) != (ParseFlags) 0)
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
      int order1;
      if (!DateTimeParse.GetMonthDayOrder(dtfi.MonthDayPattern, dtfi, out order1))
      {
        result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_BadDatePattern", (object) dtfi.MonthDayPattern);
        return false;
      }
      if (order1 == 6)
      {
        int order2;
        if (!DateTimeParse.GetYearMonthOrder(dtfi.YearMonthPattern, dtfi, out order2))
        {
          result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_BadDatePattern", (object) dtfi.YearMonthPattern);
          return false;
        }
        if (order2 == 4)
        {
          int adjustedYear;
          if (DateTimeParse.TryAdjustYear(ref result, raw.GetNumber(0), out adjustedYear) && DateTimeParse.SetDateYMD(ref result, adjustedYear, raw.month, 1))
            return true;
          result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
          return false;
        }
      }
      DateTimeParse.GetDefaultYear(ref result, ref styles);
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      DateTimeResult& result1 = @result;
      // ISSUE: explicit reference operation
      int year = (^result1).Year;
      int month = raw.month;
      int number = raw.GetNumber(0);
      if (DateTimeParse.SetDateYMD(result1, year, month, number))
        return true;
      result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
      return false;
    }

    private static bool GetDayOfMNN(ref DateTimeResult result, ref DateTimeRawInfo raw, DateTimeFormatInfo dtfi)
    {
      if ((result.flags & ParseFlags.HaveDate) != (ParseFlags) 0)
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
      int number1 = raw.GetNumber(0);
      int number2 = raw.GetNumber(1);
      int order;
      if (!DateTimeParse.GetYearMonthDayOrder(dtfi.ShortDatePattern, dtfi, out order))
      {
        result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_BadDatePattern", (object) dtfi.ShortDatePattern);
        return false;
      }
      if (order == 1)
      {
        int adjustedYear;
        if (DateTimeParse.TryAdjustYear(ref result, number2, out adjustedYear) && result.calendar.IsValidDay(adjustedYear, raw.month, number1, result.era))
        {
          result.SetDate(adjustedYear, raw.month, number1);
          result.flags |= ParseFlags.HaveDate;
          return true;
        }
        if (DateTimeParse.TryAdjustYear(ref result, number1, out adjustedYear) && result.calendar.IsValidDay(adjustedYear, raw.month, number2, result.era))
        {
          result.SetDate(adjustedYear, raw.month, number2);
          result.flags |= ParseFlags.HaveDate;
          return true;
        }
      }
      else if (order == 0)
      {
        int adjustedYear;
        if (DateTimeParse.TryAdjustYear(ref result, number1, out adjustedYear) && result.calendar.IsValidDay(adjustedYear, raw.month, number2, result.era))
        {
          result.SetDate(adjustedYear, raw.month, number2);
          result.flags |= ParseFlags.HaveDate;
          return true;
        }
        if (DateTimeParse.TryAdjustYear(ref result, number2, out adjustedYear) && result.calendar.IsValidDay(adjustedYear, raw.month, number1, result.era))
        {
          result.SetDate(adjustedYear, raw.month, number1);
          result.flags |= ParseFlags.HaveDate;
          return true;
        }
      }
      else if (order == 2)
      {
        int adjustedYear;
        if (DateTimeParse.TryAdjustYear(ref result, number2, out adjustedYear) && result.calendar.IsValidDay(adjustedYear, raw.month, number1, result.era))
        {
          result.SetDate(adjustedYear, raw.month, number1);
          result.flags |= ParseFlags.HaveDate;
          return true;
        }
        if (DateTimeParse.TryAdjustYear(ref result, number1, out adjustedYear) && result.calendar.IsValidDay(adjustedYear, raw.month, number2, result.era))
        {
          result.SetDate(adjustedYear, raw.month, number2);
          result.flags |= ParseFlags.HaveDate;
          return true;
        }
      }
      result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
      return false;
    }

    private static bool GetDayOfYNN(ref DateTimeResult result, ref DateTimeRawInfo raw, DateTimeFormatInfo dtfi)
    {
      if ((result.flags & ParseFlags.HaveDate) != (ParseFlags) 0)
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
      int number1 = raw.GetNumber(0);
      int number2 = raw.GetNumber(1);
      int order;
      if (DateTimeParse.GetYearMonthDayOrder(dtfi.ShortDatePattern, dtfi, out order) && order == 3)
      {
        if (DateTimeParse.SetDateYMD(ref result, raw.year, number2, number1))
        {
          result.flags |= ParseFlags.HaveDate;
          return true;
        }
      }
      else if (DateTimeParse.SetDateYMD(ref result, raw.year, number1, number2))
      {
        result.flags |= ParseFlags.HaveDate;
        return true;
      }
      result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
      return false;
    }

    private static bool GetDayOfNNY(ref DateTimeResult result, ref DateTimeRawInfo raw, DateTimeFormatInfo dtfi)
    {
      if ((result.flags & ParseFlags.HaveDate) != (ParseFlags) 0)
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
      int number1 = raw.GetNumber(0);
      int number2 = raw.GetNumber(1);
      int order;
      if (!DateTimeParse.GetYearMonthDayOrder(dtfi.ShortDatePattern, dtfi, out order))
      {
        result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_BadDatePattern", (object) dtfi.ShortDatePattern);
        return false;
      }
      if (order == 1 || order == 0)
      {
        if (DateTimeParse.SetDateYMD(ref result, raw.year, number1, number2))
        {
          result.flags |= ParseFlags.HaveDate;
          return true;
        }
      }
      else if (DateTimeParse.SetDateYMD(ref result, raw.year, number2, number1))
      {
        result.flags |= ParseFlags.HaveDate;
        return true;
      }
      result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
      return false;
    }

    private static bool GetDayOfYMN(ref DateTimeResult result, ref DateTimeRawInfo raw, DateTimeFormatInfo dtfi)
    {
      if ((result.flags & ParseFlags.HaveDate) != (ParseFlags) 0)
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
      if (DateTimeParse.SetDateYMD(ref result, raw.year, raw.month, raw.GetNumber(0)))
      {
        result.flags |= ParseFlags.HaveDate;
        return true;
      }
      result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
      return false;
    }

    private static bool GetDayOfYN(ref DateTimeResult result, ref DateTimeRawInfo raw, DateTimeFormatInfo dtfi)
    {
      if ((result.flags & ParseFlags.HaveDate) != (ParseFlags) 0)
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
      if (DateTimeParse.SetDateYMD(ref result, raw.year, raw.GetNumber(0), 1))
      {
        result.flags |= ParseFlags.HaveDate;
        return true;
      }
      result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
      return false;
    }

    private static bool GetDayOfYM(ref DateTimeResult result, ref DateTimeRawInfo raw, DateTimeFormatInfo dtfi)
    {
      if ((result.flags & ParseFlags.HaveDate) != (ParseFlags) 0)
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
      if (DateTimeParse.SetDateYMD(ref result, raw.year, raw.month, 1))
      {
        result.flags |= ParseFlags.HaveDate;
        return true;
      }
      result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
      return false;
    }

    private static void AdjustTimeMark(DateTimeFormatInfo dtfi, ref DateTimeRawInfo raw)
    {
      if (raw.timeMark != DateTimeParse.TM.NotSet || dtfi.AMDesignator == null || dtfi.PMDesignator == null)
        return;
      if (dtfi.AMDesignator.Length == 0 && dtfi.PMDesignator.Length != 0)
        raw.timeMark = DateTimeParse.TM.AM;
      if (dtfi.PMDesignator.Length != 0 || dtfi.AMDesignator.Length == 0)
        return;
      raw.timeMark = DateTimeParse.TM.PM;
    }

    private static bool AdjustHour(ref int hour, DateTimeParse.TM timeMark)
    {
      if (timeMark != DateTimeParse.TM.NotSet)
      {
        if (timeMark == DateTimeParse.TM.AM)
        {
          if (hour < 0 || hour > 12)
            return false;
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          int& local = @hour;
          // ISSUE: explicit reference operation
          int num = ^local == 12 ? 0 : hour;
          // ISSUE: explicit reference operation
          ^local = num;
        }
        else
        {
          if (hour < 0 || hour > 23)
            return false;
          if (hour < 12)
            hour += 12;
        }
      }
      return true;
    }

    private static bool GetTimeOfN(DateTimeFormatInfo dtfi, ref DateTimeResult result, ref DateTimeRawInfo raw)
    {
      if ((result.flags & ParseFlags.HaveTime) != (ParseFlags) 0)
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
      if (raw.timeMark == DateTimeParse.TM.NotSet)
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
      result.Hour = raw.GetNumber(0);
      result.flags |= ParseFlags.HaveTime;
      return true;
    }

    private static bool GetTimeOfNN(DateTimeFormatInfo dtfi, ref DateTimeResult result, ref DateTimeRawInfo raw)
    {
      if ((result.flags & ParseFlags.HaveTime) != (ParseFlags) 0)
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
      result.Hour = raw.GetNumber(0);
      result.Minute = raw.GetNumber(1);
      result.flags |= ParseFlags.HaveTime;
      return true;
    }

    private static bool GetTimeOfNNN(DateTimeFormatInfo dtfi, ref DateTimeResult result, ref DateTimeRawInfo raw)
    {
      if ((result.flags & ParseFlags.HaveTime) != (ParseFlags) 0)
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
      result.Hour = raw.GetNumber(0);
      result.Minute = raw.GetNumber(1);
      result.Second = raw.GetNumber(2);
      result.flags |= ParseFlags.HaveTime;
      return true;
    }

    private static bool GetDateOfDSN(ref DateTimeResult result, ref DateTimeRawInfo raw)
    {
      if (raw.numCount != 1 || result.Day != -1)
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
      result.Day = raw.GetNumber(0);
      return true;
    }

    private static bool GetDateOfNDS(ref DateTimeResult result, ref DateTimeRawInfo raw)
    {
      if (result.Month == -1)
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
      if (result.Year != -1)
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
      if (!DateTimeParse.TryAdjustYear(ref result, raw.GetNumber(0), out result.Year))
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
      result.Day = 1;
      return true;
    }

    private static bool GetDateOfNNDS(ref DateTimeResult result, ref DateTimeRawInfo raw, DateTimeFormatInfo dtfi)
    {
      if ((result.flags & ParseFlags.HaveYear) != (ParseFlags) 0)
      {
        if ((result.flags & ParseFlags.HaveMonth) == (ParseFlags) 0 && (result.flags & ParseFlags.HaveDay) == (ParseFlags) 0 && DateTimeParse.TryAdjustYear(ref result, raw.year, out result.Year))
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          DateTimeResult& result1 = @result;
          // ISSUE: explicit reference operation
          int year = (^result1).Year;
          int number1 = raw.GetNumber(0);
          int number2 = raw.GetNumber(1);
          if (DateTimeParse.SetDateYMD(result1, year, number1, number2))
            return true;
        }
      }
      else if ((result.flags & ParseFlags.HaveMonth) != (ParseFlags) 0 && (result.flags & ParseFlags.HaveYear) == (ParseFlags) 0 && (result.flags & ParseFlags.HaveDay) == (ParseFlags) 0)
      {
        int order;
        if (!DateTimeParse.GetYearMonthDayOrder(dtfi.ShortDatePattern, dtfi, out order))
        {
          result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_BadDatePattern", (object) dtfi.ShortDatePattern);
          return false;
        }
        if (order == 0)
        {
          int adjustedYear;
          if (DateTimeParse.TryAdjustYear(ref result, raw.GetNumber(0), out adjustedYear) && DateTimeParse.SetDateYMD(ref result, adjustedYear, result.Month, raw.GetNumber(1)))
            return true;
        }
        else
        {
          int adjustedYear;
          if (DateTimeParse.TryAdjustYear(ref result, raw.GetNumber(1), out adjustedYear) && DateTimeParse.SetDateYMD(ref result, adjustedYear, result.Month, raw.GetNumber(0)))
            return true;
        }
      }
      result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
      return false;
    }

    private static bool ProcessDateTimeSuffix(ref DateTimeResult result, ref DateTimeRawInfo raw, ref DateTimeToken dtok)
    {
      switch (dtok.suffix)
      {
        case TokenType.SEP_HourSuff:
          if ((result.flags & ParseFlags.HaveHour) != (ParseFlags) 0)
            return false;
          result.flags |= ParseFlags.HaveHour;
          result.Hour = dtok.num;
          break;
        case TokenType.SEP_MinuteSuff:
          if ((result.flags & ParseFlags.HaveMinute) != (ParseFlags) 0)
            return false;
          result.flags |= ParseFlags.HaveMinute;
          result.Minute = dtok.num;
          break;
        case TokenType.SEP_SecondSuff:
          if ((result.flags & ParseFlags.HaveSecond) != (ParseFlags) 0)
            return false;
          result.flags |= ParseFlags.HaveSecond;
          result.Second = dtok.num;
          break;
        case TokenType.SEP_YearSuff:
          if ((result.flags & ParseFlags.HaveYear) != (ParseFlags) 0)
            return false;
          result.flags |= ParseFlags.HaveYear;
          result.Year = raw.year = dtok.num;
          break;
        case TokenType.SEP_MonthSuff:
          if ((result.flags & ParseFlags.HaveMonth) != (ParseFlags) 0)
            return false;
          result.flags |= ParseFlags.HaveMonth;
          result.Month = raw.month = dtok.num;
          break;
        case TokenType.SEP_DaySuff:
          if ((result.flags & ParseFlags.HaveDay) != (ParseFlags) 0)
            return false;
          result.flags |= ParseFlags.HaveDay;
          result.Day = dtok.num;
          break;
      }
      return true;
    }

    internal static bool ProcessHebrewTerminalState(DateTimeParse.DS dps, ref DateTimeResult result, ref DateTimeStyles styles, ref DateTimeRawInfo raw, DateTimeFormatInfo dtfi)
    {
      switch (dps)
      {
        case DateTimeParse.DS.DX_MN:
        case DateTimeParse.DS.DX_NM:
          DateTimeParse.GetDefaultYear(ref result, ref styles);
          if (!dtfi.YearMonthAdjustment(ref result.Year, ref raw.month, true))
          {
            result.SetFailure(ParseFailureKind.FormatBadDateTimeCalendar, "Format_BadDateTimeCalendar", (object) null);
            return false;
          }
          if (!DateTimeParse.GetHebrewDayOfNM(ref result, ref raw, dtfi))
            return false;
          break;
        case DateTimeParse.DS.DX_MNN:
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          DateTimeRawInfo& local = @raw;
          int index = 1;
          // ISSUE: explicit reference operation
          int number = (^local).GetNumber(index);
          // ISSUE: explicit reference operation
          (^local).year = number;
          if (!dtfi.YearMonthAdjustment(ref raw.year, ref raw.month, true))
          {
            result.SetFailure(ParseFailureKind.FormatBadDateTimeCalendar, "Format_BadDateTimeCalendar", (object) null);
            return false;
          }
          if (!DateTimeParse.GetDayOfMNN(ref result, ref raw, dtfi))
            return false;
          break;
        case DateTimeParse.DS.DX_YMN:
          if (!dtfi.YearMonthAdjustment(ref raw.year, ref raw.month, true))
          {
            result.SetFailure(ParseFailureKind.FormatBadDateTimeCalendar, "Format_BadDateTimeCalendar", (object) null);
            return false;
          }
          if (!DateTimeParse.GetDayOfYMN(ref result, ref raw, dtfi))
            return false;
          break;
        case DateTimeParse.DS.DX_YM:
          if (!dtfi.YearMonthAdjustment(ref raw.year, ref raw.month, true))
          {
            result.SetFailure(ParseFailureKind.FormatBadDateTimeCalendar, "Format_BadDateTimeCalendar", (object) null);
            return false;
          }
          if (!DateTimeParse.GetDayOfYM(ref result, ref raw, dtfi))
            return false;
          break;
        case DateTimeParse.DS.TX_N:
          if (!DateTimeParse.GetTimeOfN(dtfi, ref result, ref raw))
            return false;
          break;
        case DateTimeParse.DS.TX_NN:
          if (!DateTimeParse.GetTimeOfNN(dtfi, ref result, ref raw))
            return false;
          break;
        case DateTimeParse.DS.TX_NNN:
          if (!DateTimeParse.GetTimeOfNNN(dtfi, ref result, ref raw))
            return false;
          break;
        default:
          result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
          return false;
      }
      if (dps > DateTimeParse.DS.ERROR)
        raw.numCount = 0;
      return true;
    }

    internal static bool ProcessTerminaltState(DateTimeParse.DS dps, ref DateTimeResult result, ref DateTimeStyles styles, ref DateTimeRawInfo raw, DateTimeFormatInfo dtfi)
    {
      bool flag = true;
      switch (dps)
      {
        case DateTimeParse.DS.DX_NN:
          flag = DateTimeParse.GetDayOfNN(ref result, ref styles, ref raw, dtfi);
          break;
        case DateTimeParse.DS.DX_NNN:
          flag = DateTimeParse.GetDayOfNNN(ref result, ref raw, dtfi);
          break;
        case DateTimeParse.DS.DX_MN:
          flag = DateTimeParse.GetDayOfMN(ref result, ref styles, ref raw, dtfi);
          break;
        case DateTimeParse.DS.DX_NM:
          flag = DateTimeParse.GetDayOfNM(ref result, ref styles, ref raw, dtfi);
          break;
        case DateTimeParse.DS.DX_MNN:
          flag = DateTimeParse.GetDayOfMNN(ref result, ref raw, dtfi);
          break;
        case DateTimeParse.DS.DX_DS:
          flag = true;
          break;
        case DateTimeParse.DS.DX_DSN:
          flag = DateTimeParse.GetDateOfDSN(ref result, ref raw);
          break;
        case DateTimeParse.DS.DX_NDS:
          flag = DateTimeParse.GetDateOfNDS(ref result, ref raw);
          break;
        case DateTimeParse.DS.DX_NNDS:
          flag = DateTimeParse.GetDateOfNNDS(ref result, ref raw, dtfi);
          break;
        case DateTimeParse.DS.DX_YNN:
          flag = DateTimeParse.GetDayOfYNN(ref result, ref raw, dtfi);
          break;
        case DateTimeParse.DS.DX_YMN:
          flag = DateTimeParse.GetDayOfYMN(ref result, ref raw, dtfi);
          break;
        case DateTimeParse.DS.DX_YN:
          flag = DateTimeParse.GetDayOfYN(ref result, ref raw, dtfi);
          break;
        case DateTimeParse.DS.DX_YM:
          flag = DateTimeParse.GetDayOfYM(ref result, ref raw, dtfi);
          break;
        case DateTimeParse.DS.TX_N:
          flag = DateTimeParse.GetTimeOfN(dtfi, ref result, ref raw);
          break;
        case DateTimeParse.DS.TX_NN:
          flag = DateTimeParse.GetTimeOfNN(dtfi, ref result, ref raw);
          break;
        case DateTimeParse.DS.TX_NNN:
          flag = DateTimeParse.GetTimeOfNNN(dtfi, ref result, ref raw);
          break;
        case DateTimeParse.DS.TX_TS:
          flag = true;
          break;
        case DateTimeParse.DS.DX_NNY:
          flag = DateTimeParse.GetDayOfNNY(ref result, ref raw, dtfi);
          break;
      }
      if (!flag)
        return false;
      if (dps > DateTimeParse.DS.ERROR)
        raw.numCount = 0;
      return true;
    }

    internal static DateTime Parse(string s, DateTimeFormatInfo dtfi, DateTimeStyles styles)
    {
      DateTimeResult result = new DateTimeResult();
      result.Init();
      if (DateTimeParse.TryParse(s, dtfi, styles, ref result))
        return result.parsedDate;
      throw DateTimeParse.GetDateTimeParseException(ref result);
    }

    internal static DateTime Parse(string s, DateTimeFormatInfo dtfi, DateTimeStyles styles, out TimeSpan offset)
    {
      DateTimeResult result = new DateTimeResult();
      result.Init();
      result.flags |= ParseFlags.CaptureOffset;
      if (!DateTimeParse.TryParse(s, dtfi, styles, ref result))
        throw DateTimeParse.GetDateTimeParseException(ref result);
      offset = result.timeZoneOffset;
      return result.parsedDate;
    }

    internal static bool TryParse(string s, DateTimeFormatInfo dtfi, DateTimeStyles styles, out DateTime result)
    {
      result = DateTime.MinValue;
      DateTimeResult result1 = new DateTimeResult();
      result1.Init();
      if (!DateTimeParse.TryParse(s, dtfi, styles, ref result1))
        return false;
      result = result1.parsedDate;
      return true;
    }

    internal static bool TryParse(string s, DateTimeFormatInfo dtfi, DateTimeStyles styles, out DateTime result, out TimeSpan offset)
    {
      result = DateTime.MinValue;
      offset = TimeSpan.Zero;
      DateTimeResult result1 = new DateTimeResult();
      result1.Init();
      result1.flags |= ParseFlags.CaptureOffset;
      if (!DateTimeParse.TryParse(s, dtfi, styles, ref result1))
        return false;
      result = result1.parsedDate;
      offset = result1.timeZoneOffset;
      return true;
    }

    [SecuritySafeCritical]
    internal static unsafe bool TryParse(string s, DateTimeFormatInfo dtfi, DateTimeStyles styles, ref DateTimeResult result)
    {
      if (s == null)
      {
        result.SetFailure(ParseFailureKind.ArgumentNull, "ArgumentNull_String", (object) null, "s");
        return false;
      }
      if (s.Length == 0)
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
      DateTimeParse.DS dps = DateTimeParse.DS.BEGIN;
      bool flag1 = false;
      DateTimeToken dtok = new DateTimeToken();
      dtok.suffix = TokenType.SEP_Unk;
      DateTimeRawInfo raw = new DateTimeRawInfo();
      int* numberBuffer = stackalloc int[3];
      raw.Init(numberBuffer);
      raw.hasSameDateAndTimeSeparators = dtfi.DateSeparator.Equals(dtfi.TimeSeparator, StringComparison.Ordinal);
      result.calendar = dtfi.Calendar;
      result.era = 0;
      __DTString str = new __DTString(s, dtfi);
      str.GetNext();
      while (DateTimeParse.Lex(dps, ref str, ref dtok, ref raw, ref result, ref dtfi, styles))
      {
        if (dtok.dtt != DateTimeParse.DTT.Unk)
        {
          if (dtok.suffix != TokenType.SEP_Unk)
          {
            if (!DateTimeParse.ProcessDateTimeSuffix(ref result, ref raw, ref dtok))
            {
              result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
              return false;
            }
            dtok.suffix = TokenType.SEP_Unk;
          }
          if (dtok.dtt == DateTimeParse.DTT.NumLocalTimeMark)
          {
            if (dps == DateTimeParse.DS.D_YNd || dps == DateTimeParse.DS.D_YN)
              return DateTimeParse.ParseISO8601(ref raw, ref str, styles, ref result);
            result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
            return false;
          }
          if (raw.hasSameDateAndTimeSeparators)
          {
            if (dtok.dtt == DateTimeParse.DTT.YearEnd || dtok.dtt == DateTimeParse.DTT.YearSpace || dtok.dtt == DateTimeParse.DTT.YearDateSep)
            {
              if (dps == DateTimeParse.DS.T_Nt)
                dps = DateTimeParse.DS.D_Nd;
              if (dps == DateTimeParse.DS.T_NNt)
                dps = DateTimeParse.DS.D_NNd;
            }
            bool flag2 = str.AtEnd();
            if (DateTimeParse.dateParsingStates[(int) dps][(int) dtok.dtt] == DateTimeParse.DS.ERROR | flag2)
            {
              switch (dtok.dtt)
              {
                case DateTimeParse.DTT.NumDatesep:
                  dtok.dtt = flag2 ? DateTimeParse.DTT.NumEnd : DateTimeParse.DTT.NumSpace;
                  break;
                case DateTimeParse.DTT.NumTimesep:
                  dtok.dtt = flag2 ? DateTimeParse.DTT.NumEnd : DateTimeParse.DTT.NumSpace;
                  break;
                case DateTimeParse.DTT.MonthDatesep:
                  dtok.dtt = flag2 ? DateTimeParse.DTT.MonthEnd : DateTimeParse.DTT.MonthSpace;
                  break;
                case DateTimeParse.DTT.YearDateSep:
                  dtok.dtt = flag2 ? DateTimeParse.DTT.YearEnd : DateTimeParse.DTT.YearSpace;
                  break;
              }
            }
          }
          dps = DateTimeParse.dateParsingStates[(int) dps][(int) dtok.dtt];
          if (dps == DateTimeParse.DS.ERROR)
          {
            result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
            return false;
          }
          if (dps > DateTimeParse.DS.ERROR)
          {
            if ((dtfi.FormatFlags & DateTimeFormatFlags.UseHebrewRule) != DateTimeFormatFlags.None)
            {
              if (!DateTimeParse.ProcessHebrewTerminalState(dps, ref result, ref styles, ref raw, dtfi))
                return false;
            }
            else if (!DateTimeParse.ProcessTerminaltState(dps, ref result, ref styles, ref raw, dtfi))
              return false;
            flag1 = true;
            dps = DateTimeParse.DS.BEGIN;
          }
        }
        if (dtok.dtt == DateTimeParse.DTT.End || dtok.dtt == DateTimeParse.DTT.NumEnd || dtok.dtt == DateTimeParse.DTT.MonthEnd)
        {
          if (!flag1)
          {
            result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
            return false;
          }
          DateTimeParse.AdjustTimeMark(dtfi, ref raw);
          if (!DateTimeParse.AdjustHour(ref result.Hour, raw.timeMark))
          {
            result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
            return false;
          }
          bool bTimeOnly = result.Year == -1 && result.Month == -1 && result.Day == -1;
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          DateTimeResult& result1 = @result;
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Calendar& cal = @(^result1).calendar;
          int num = (int) styles;
          if (!DateTimeParse.CheckDefaultDateTime(result1, cal, (DateTimeStyles) num))
            return false;
          DateTime result2;
          if (!result.calendar.TryToDateTime(result.Year, result.Month, result.Day, result.Hour, result.Minute, result.Second, 0, result.era, out result2))
          {
            result.SetFailure(ParseFailureKind.FormatBadDateTimeCalendar, "Format_BadDateTimeCalendar", (object) null);
            return false;
          }
          if (raw.fraction > 0.0)
            result2 = result2.AddTicks((long) Math.Round(raw.fraction * 10000000.0));
          if (raw.dayOfWeek != -1 && (DayOfWeek) raw.dayOfWeek != result.calendar.GetDayOfWeek(result2))
          {
            result.SetFailure(ParseFailureKind.Format, "Format_BadDayOfWeek", (object) null);
            return false;
          }
          result.parsedDate = result2;
          return DateTimeParse.DetermineTimeZoneAdjustments(ref result, styles, bTimeOnly);
        }
      }
      return false;
    }

    private static bool DetermineTimeZoneAdjustments(ref DateTimeResult result, DateTimeStyles styles, bool bTimeOnly)
    {
      if ((result.flags & ParseFlags.CaptureOffset) != (ParseFlags) 0)
        return DateTimeParse.DateTimeOffsetTimeZonePostProcessing(ref result, styles);
      if ((result.flags & ParseFlags.TimeZoneUsed) == (ParseFlags) 0)
      {
        if ((styles & DateTimeStyles.AssumeLocal) != DateTimeStyles.None)
        {
          if ((styles & DateTimeStyles.AdjustToUniversal) != DateTimeStyles.None)
          {
            result.flags |= ParseFlags.TimeZoneUsed;
            result.timeZoneOffset = TimeZoneInfo.GetLocalUtcOffset(result.parsedDate, TimeZoneInfoOptions.NoThrowOnInvalidTime);
          }
          else
          {
            result.parsedDate = DateTime.SpecifyKind(result.parsedDate, DateTimeKind.Local);
            return true;
          }
        }
        else
        {
          if ((styles & DateTimeStyles.AssumeUniversal) == DateTimeStyles.None)
            return true;
          if ((styles & DateTimeStyles.AdjustToUniversal) != DateTimeStyles.None)
          {
            result.parsedDate = DateTime.SpecifyKind(result.parsedDate, DateTimeKind.Utc);
            return true;
          }
          result.flags |= ParseFlags.TimeZoneUsed;
          result.timeZoneOffset = TimeSpan.Zero;
        }
      }
      if ((styles & DateTimeStyles.RoundtripKind) != DateTimeStyles.None && (result.flags & ParseFlags.TimeZoneUtc) != (ParseFlags) 0)
      {
        result.parsedDate = DateTime.SpecifyKind(result.parsedDate, DateTimeKind.Utc);
        return true;
      }
      if ((styles & DateTimeStyles.AdjustToUniversal) != DateTimeStyles.None)
        return DateTimeParse.AdjustTimeZoneToUniversal(ref result);
      return DateTimeParse.AdjustTimeZoneToLocal(ref result, bTimeOnly);
    }

    private static bool DateTimeOffsetTimeZonePostProcessing(ref DateTimeResult result, DateTimeStyles styles)
    {
      if ((result.flags & ParseFlags.TimeZoneUsed) == (ParseFlags) 0)
        result.timeZoneOffset = (styles & DateTimeStyles.AssumeUniversal) == DateTimeStyles.None ? TimeZoneInfo.GetLocalUtcOffset(result.parsedDate, TimeZoneInfoOptions.NoThrowOnInvalidTime) : TimeSpan.Zero;
      long ticks1 = result.timeZoneOffset.Ticks;
      long ticks2 = result.parsedDate.Ticks - ticks1;
      if (ticks2 < 0L || ticks2 > 3155378975999999999L)
      {
        result.SetFailure(ParseFailureKind.Format, "Format_UTCOutOfRange", (object) null);
        return false;
      }
      if (ticks1 < -504000000000L || ticks1 > 504000000000L)
      {
        result.SetFailure(ParseFailureKind.Format, "Format_OffsetOutOfRange", (object) null);
        return false;
      }
      if ((styles & DateTimeStyles.AdjustToUniversal) != DateTimeStyles.None)
      {
        if ((result.flags & ParseFlags.TimeZoneUsed) == (ParseFlags) 0 && (styles & DateTimeStyles.AssumeUniversal) == DateTimeStyles.None)
        {
          int num = DateTimeParse.AdjustTimeZoneToUniversal(ref result) ? 1 : 0;
          result.timeZoneOffset = TimeSpan.Zero;
          return num != 0;
        }
        result.parsedDate = new DateTime(ticks2, DateTimeKind.Utc);
        result.timeZoneOffset = TimeSpan.Zero;
      }
      return true;
    }

    private static bool AdjustTimeZoneToUniversal(ref DateTimeResult result)
    {
      long ticks = result.parsedDate.Ticks - result.timeZoneOffset.Ticks;
      if (ticks < 0L)
        ticks += 864000000000L;
      if (ticks < 0L || ticks > 3155378975999999999L)
      {
        result.SetFailure(ParseFailureKind.Format, "Format_DateOutOfRange", (object) null);
        return false;
      }
      result.parsedDate = new DateTime(ticks, DateTimeKind.Utc);
      return true;
    }

    private static bool AdjustTimeZoneToLocal(ref DateTimeResult result, bool bTimeOnly)
    {
      long ticks1 = result.parsedDate.Ticks;
      TimeZoneInfo local = TimeZoneInfo.Local;
      bool isAmbiguousLocalDst = false;
      long ticks2;
      if (ticks1 < 864000000000L)
      {
        ticks2 = ticks1 - result.timeZoneOffset.Ticks + local.GetUtcOffset(bTimeOnly ? DateTime.Now : result.parsedDate, TimeZoneInfoOptions.NoThrowOnInvalidTime).Ticks;
        if (ticks2 < 0L)
          ticks2 += 864000000000L;
      }
      else
      {
        long ticks3 = ticks1 - result.timeZoneOffset.Ticks;
        if (ticks3 < 0L || ticks3 > 3155378975999999999L)
        {
          ticks2 = ticks3 + local.GetUtcOffset(result.parsedDate, TimeZoneInfoOptions.NoThrowOnInvalidTime).Ticks;
        }
        else
        {
          DateTime time = new DateTime(ticks3, DateTimeKind.Utc);
          bool isDaylightSavings = false;
          ticks2 = ticks3 + TimeZoneInfo.GetUtcOffsetFromUtc(time, TimeZoneInfo.Local, out isDaylightSavings, out isAmbiguousLocalDst).Ticks;
        }
      }
      if (ticks2 < 0L || ticks2 > 3155378975999999999L)
      {
        result.parsedDate = DateTime.MinValue;
        result.SetFailure(ParseFailureKind.Format, "Format_DateOutOfRange", (object) null);
        return false;
      }
      result.parsedDate = new DateTime(ticks2, DateTimeKind.Local, isAmbiguousLocalDst);
      return true;
    }

    private static bool ParseISO8601(ref DateTimeRawInfo raw, ref __DTString str, DateTimeStyles styles, ref DateTimeResult result)
    {
      if (raw.year >= 0 && raw.GetNumber(0) >= 0)
        raw.GetNumber(1);
      --str.Index;
      int result1 = 0;
      double result2 = 0.0;
      str.SkipWhiteSpaces();
      int result3;
      if (!DateTimeParse.ParseDigits(ref str, 2, out result3))
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
      str.SkipWhiteSpaces();
      if (!str.Match(':'))
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
      str.SkipWhiteSpaces();
      int result4;
      if (!DateTimeParse.ParseDigits(ref str, 2, out result4))
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
      str.SkipWhiteSpaces();
      if (str.Match(':'))
      {
        str.SkipWhiteSpaces();
        if (!DateTimeParse.ParseDigits(ref str, 2, out result1))
        {
          result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
          return false;
        }
        if (str.Match('.'))
        {
          if (!DateTimeParse.ParseFraction(ref str, out result2))
          {
            result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
            return false;
          }
          --str.Index;
        }
        str.SkipWhiteSpaces();
      }
      if (str.GetNext())
      {
        switch (str.GetChar())
        {
          case '+':
          case '-':
            result.flags |= ParseFlags.TimeZoneUsed;
            if (!DateTimeParse.ParseTimeZone(ref str, ref result.timeZoneOffset))
            {
              result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
              return false;
            }
            break;
          case 'Z':
          case 'z':
            result.flags |= ParseFlags.TimeZoneUsed;
            result.timeZoneOffset = TimeSpan.Zero;
            result.flags |= ParseFlags.TimeZoneUtc;
            break;
          default:
            --str.Index;
            break;
        }
        str.SkipWhiteSpaces();
        if (str.Match('#'))
        {
          if (!DateTimeParse.VerifyValidPunctuation(ref str))
          {
            result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
            return false;
          }
          str.SkipWhiteSpaces();
        }
        if (str.Match(char.MinValue) && !DateTimeParse.VerifyValidPunctuation(ref str))
        {
          result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
          return false;
        }
        if (str.GetNext())
        {
          result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
          return false;
        }
      }
      DateTime result5;
      if (!GregorianCalendar.GetDefaultInstance().TryToDateTime(raw.year, raw.GetNumber(0), raw.GetNumber(1), result3, result4, result1, 0, result.era, out result5))
      {
        result.SetFailure(ParseFailureKind.FormatBadDateTimeCalendar, "Format_BadDateTimeCalendar", (object) null);
        return false;
      }
      result5 = result5.AddTicks((long) Math.Round(result2 * 10000000.0));
      result.parsedDate = result5;
      return DateTimeParse.DetermineTimeZoneAdjustments(ref result, styles, false);
    }

    internal static bool MatchHebrewDigits(ref __DTString str, int digitLen, out int number)
    {
      number = 0;
      HebrewNumberParsingContext context = new HebrewNumberParsingContext(0);
      HebrewNumberParsingState numberParsingState = HebrewNumberParsingState.ContinueParsing;
      while (numberParsingState == HebrewNumberParsingState.ContinueParsing && str.GetNext())
        numberParsingState = HebrewNumber.ParseByChar(str.GetChar(), ref context);
      if (numberParsingState != HebrewNumberParsingState.FoundEndOfHebrewNumber)
        return false;
      number = context.result;
      return true;
    }

    internal static bool ParseDigits(ref __DTString str, int digitLen, out int result)
    {
      if (digitLen == 1)
        return DateTimeParse.ParseDigits(ref str, 1, 2, out result);
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      __DTString& str1 = @str;
      int num = digitLen;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      int& result1 = @result;
      return DateTimeParse.ParseDigits(str1, num, num, result1);
    }

    internal static bool ParseDigits(ref __DTString str, int minDigitLen, int maxDigitLen, out int result)
    {
      result = 0;
      int num1 = str.Index;
      int num2;
      for (num2 = 0; num2 < maxDigitLen; ++num2)
      {
        if (!str.GetNextDigit())
        {
          --str.Index;
          break;
        }
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        int& local = @result;
        // ISSUE: explicit reference operation
        int num3 = ^local * 10 + str.GetDigit();
        // ISSUE: explicit reference operation
        ^local = num3;
      }
      if (num2 >= minDigitLen)
        return true;
      str.Index = num1;
      return false;
    }

    private static bool ParseFractionExact(ref __DTString str, int maxDigitLen, ref double result)
    {
      if (!str.GetNextDigit())
      {
        --str.Index;
        return false;
      }
      result = (double) str.GetDigit();
      int num1;
      for (num1 = 1; num1 < maxDigitLen; ++num1)
      {
        if (!str.GetNextDigit())
        {
          --str.Index;
          break;
        }
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        double& local = @result;
        // ISSUE: explicit reference operation
        double num2 = ^local * 10.0 + (double) str.GetDigit();
        // ISSUE: explicit reference operation
        ^local = num2;
      }
      result /= Math.Pow(10.0, (double) num1);
      return num1 == maxDigitLen;
    }

    private static bool ParseSign(ref __DTString str, ref bool result)
    {
      if (!str.GetNext())
        return false;
      switch (str.GetChar())
      {
        case '+':
          result = true;
          return true;
        case '-':
          result = false;
          return true;
        default:
          return false;
      }
    }

    private static bool ParseTimeZoneOffset(ref __DTString str, int len, ref TimeSpan result)
    {
      bool result1 = true;
      int result2 = 0;
      int result3;
      if (len == 1 || len == 2)
      {
        if (!DateTimeParse.ParseSign(ref str, ref result1) || !DateTimeParse.ParseDigits(ref str, len, out result3))
          return false;
      }
      else
      {
        if (!DateTimeParse.ParseSign(ref str, ref result1) || !DateTimeParse.ParseDigits(ref str, 1, out result3))
          return false;
        if (str.Match(":"))
        {
          if (!DateTimeParse.ParseDigits(ref str, 2, out result2))
            return false;
        }
        else
        {
          --str.Index;
          if (!DateTimeParse.ParseDigits(ref str, 2, out result2))
            return false;
        }
      }
      if (result2 < 0 || result2 >= 60)
        return false;
      result = new TimeSpan(result3, result2, 0);
      if (!result1)
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        TimeSpan& local = @result;
        // ISSUE: explicit reference operation
        TimeSpan timeSpan = (^local).Negate();
        // ISSUE: explicit reference operation
        ^local = timeSpan;
      }
      return true;
    }

    private static bool MatchAbbreviatedMonthName(ref __DTString str, DateTimeFormatInfo dtfi, ref int result)
    {
      int maxMatchStrLen = 0;
      result = -1;
      if (str.GetNext())
      {
        int num1 = dtfi.GetMonthName(13).Length == 0 ? 12 : 13;
        for (int month = 1; month <= num1; ++month)
        {
          string abbreviatedMonthName = dtfi.GetAbbreviatedMonthName(month);
          int length = abbreviatedMonthName.Length;
          if ((dtfi.HasSpacesInMonthNames ? (str.MatchSpecifiedWords(abbreviatedMonthName, false, ref length) ? 1 : 0) : (str.MatchSpecifiedWord(abbreviatedMonthName) ? 1 : 0)) != 0 && length > maxMatchStrLen)
          {
            maxMatchStrLen = length;
            result = month;
          }
        }
        if ((dtfi.FormatFlags & DateTimeFormatFlags.UseLeapYearMonth) != DateTimeFormatFlags.None)
        {
          int num2 = str.MatchLongestWords(dtfi.internalGetLeapYearMonthNames(), ref maxMatchStrLen);
          if (num2 >= 0)
            result = num2 + 1;
        }
      }
      if (result <= 0)
        return false;
      str.Index += maxMatchStrLen - 1;
      return true;
    }

    private static bool MatchMonthName(ref __DTString str, DateTimeFormatInfo dtfi, ref int result)
    {
      int maxMatchStrLen = 0;
      result = -1;
      if (str.GetNext())
      {
        int num1 = dtfi.GetMonthName(13).Length == 0 ? 12 : 13;
        for (int month = 1; month <= num1; ++month)
        {
          string monthName = dtfi.GetMonthName(month);
          int length = monthName.Length;
          if ((dtfi.HasSpacesInMonthNames ? (str.MatchSpecifiedWords(monthName, false, ref length) ? 1 : 0) : (str.MatchSpecifiedWord(monthName) ? 1 : 0)) != 0 && length > maxMatchStrLen)
          {
            maxMatchStrLen = length;
            result = month;
          }
        }
        if ((dtfi.FormatFlags & DateTimeFormatFlags.UseGenitiveMonth) != DateTimeFormatFlags.None)
        {
          int num2 = str.MatchLongestWords(dtfi.MonthGenitiveNames, ref maxMatchStrLen);
          if (num2 >= 0)
            result = num2 + 1;
        }
        if ((dtfi.FormatFlags & DateTimeFormatFlags.UseLeapYearMonth) != DateTimeFormatFlags.None)
        {
          int num2 = str.MatchLongestWords(dtfi.internalGetLeapYearMonthNames(), ref maxMatchStrLen);
          if (num2 >= 0)
            result = num2 + 1;
        }
      }
      if (result <= 0)
        return false;
      str.Index += maxMatchStrLen - 1;
      return true;
    }

    private static bool MatchAbbreviatedDayName(ref __DTString str, DateTimeFormatInfo dtfi, ref int result)
    {
      int num = 0;
      result = -1;
      if (str.GetNext())
      {
        for (DayOfWeek dayofweek = DayOfWeek.Sunday; dayofweek <= DayOfWeek.Saturday; ++dayofweek)
        {
          string abbreviatedDayName = dtfi.GetAbbreviatedDayName(dayofweek);
          int length = abbreviatedDayName.Length;
          if ((dtfi.HasSpacesInDayNames ? (str.MatchSpecifiedWords(abbreviatedDayName, false, ref length) ? 1 : 0) : (str.MatchSpecifiedWord(abbreviatedDayName) ? 1 : 0)) != 0 && length > num)
          {
            num = length;
            result = (int) dayofweek;
          }
        }
      }
      if (result < 0)
        return false;
      str.Index += num - 1;
      return true;
    }

    private static bool MatchDayName(ref __DTString str, DateTimeFormatInfo dtfi, ref int result)
    {
      int num = 0;
      result = -1;
      if (str.GetNext())
      {
        for (DayOfWeek dayofweek = DayOfWeek.Sunday; dayofweek <= DayOfWeek.Saturday; ++dayofweek)
        {
          string dayName = dtfi.GetDayName(dayofweek);
          int length = dayName.Length;
          if ((dtfi.HasSpacesInDayNames ? (str.MatchSpecifiedWords(dayName, false, ref length) ? 1 : 0) : (str.MatchSpecifiedWord(dayName) ? 1 : 0)) != 0 && length > num)
          {
            num = length;
            result = (int) dayofweek;
          }
        }
      }
      if (result < 0)
        return false;
      str.Index += num - 1;
      return true;
    }

    private static bool MatchEraName(ref __DTString str, DateTimeFormatInfo dtfi, ref int result)
    {
      if (str.GetNext())
      {
        int[] eras = dtfi.Calendar.Eras;
        if (eras != null)
        {
          for (int index = 0; index < eras.Length; ++index)
          {
            string eraName = dtfi.GetEraName(eras[index]);
            if (str.MatchSpecifiedWord(eraName))
            {
              str.Index += eraName.Length - 1;
              result = eras[index];
              return true;
            }
            string abbreviatedEraName = dtfi.GetAbbreviatedEraName(eras[index]);
            if (str.MatchSpecifiedWord(abbreviatedEraName))
            {
              str.Index += abbreviatedEraName.Length - 1;
              result = eras[index];
              return true;
            }
          }
        }
      }
      return false;
    }

    private static bool MatchTimeMark(ref __DTString str, DateTimeFormatInfo dtfi, ref DateTimeParse.TM result)
    {
      result = DateTimeParse.TM.NotSet;
      if (dtfi.AMDesignator.Length == 0)
        result = DateTimeParse.TM.AM;
      if (dtfi.PMDesignator.Length == 0)
        result = DateTimeParse.TM.PM;
      if (str.GetNext())
      {
        string amDesignator = dtfi.AMDesignator;
        if (amDesignator.Length > 0 && str.MatchSpecifiedWord(amDesignator))
        {
          str.Index += amDesignator.Length - 1;
          result = DateTimeParse.TM.AM;
          return true;
        }
        string pmDesignator = dtfi.PMDesignator;
        if (pmDesignator.Length > 0 && str.MatchSpecifiedWord(pmDesignator))
        {
          str.Index += pmDesignator.Length - 1;
          result = DateTimeParse.TM.PM;
          return true;
        }
        --str.Index;
      }
      return result != DateTimeParse.TM.NotSet;
    }

    private static bool MatchAbbreviatedTimeMark(ref __DTString str, DateTimeFormatInfo dtfi, ref DateTimeParse.TM result)
    {
      if (str.GetNext())
      {
        if ((int) str.GetChar() == (int) dtfi.AMDesignator[0])
        {
          result = DateTimeParse.TM.AM;
          return true;
        }
        if ((int) str.GetChar() == (int) dtfi.PMDesignator[0])
        {
          result = DateTimeParse.TM.PM;
          return true;
        }
      }
      return false;
    }

    private static bool CheckNewValue(ref int currentValue, int newValue, char patternChar, ref DateTimeResult result)
    {
      if (currentValue == -1)
      {
        currentValue = newValue;
        return true;
      }
      if (newValue == currentValue)
        return true;
      result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_RepeatDateTimePattern", (object) patternChar);
      return false;
    }

    private static DateTime GetDateTimeNow(ref DateTimeResult result, ref DateTimeStyles styles)
    {
      if ((result.flags & ParseFlags.CaptureOffset) != (ParseFlags) 0)
      {
        if ((result.flags & ParseFlags.TimeZoneUsed) != (ParseFlags) 0)
          return new DateTime(DateTime.UtcNow.Ticks + result.timeZoneOffset.Ticks, DateTimeKind.Unspecified);
        if ((styles & DateTimeStyles.AssumeUniversal) != DateTimeStyles.None)
          return DateTime.UtcNow;
      }
      return DateTime.Now;
    }

    private static bool CheckDefaultDateTime(ref DateTimeResult result, ref Calendar cal, DateTimeStyles styles)
    {
      if ((result.flags & ParseFlags.CaptureOffset) != (ParseFlags) 0 && (result.Month != -1 || result.Day != -1) && ((result.Year == -1 || (result.flags & ParseFlags.YearDefault) != (ParseFlags) 0) && (result.flags & ParseFlags.TimeZoneUsed) != (ParseFlags) 0))
      {
        result.SetFailure(ParseFailureKind.Format, "Format_MissingIncompleteDate", (object) null);
        return false;
      }
      if (result.Year == -1 || result.Month == -1 || result.Day == -1)
      {
        DateTime dateTimeNow = DateTimeParse.GetDateTimeNow(ref result, ref styles);
        if (result.Month == -1 && result.Day == -1)
        {
          if (result.Year == -1)
          {
            if ((styles & DateTimeStyles.NoCurrentDateDefault) != DateTimeStyles.None)
            {
              cal = GregorianCalendar.GetDefaultInstance();
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              DateTimeResult& local = @result;
              int num1;
              int num2 = num1 = 1;
              // ISSUE: explicit reference operation
              (^local).Day = num1;
              int num3;
              int num4 = num3 = num2;
              // ISSUE: explicit reference operation
              (^local).Month = num3;
              int num5 = num4;
              // ISSUE: explicit reference operation
              (^local).Year = num5;
            }
            else
            {
              result.Year = cal.GetYear(dateTimeNow);
              result.Month = cal.GetMonth(dateTimeNow);
              result.Day = cal.GetDayOfMonth(dateTimeNow);
            }
          }
          else
          {
            result.Month = 1;
            result.Day = 1;
          }
        }
        else
        {
          if (result.Year == -1)
            result.Year = cal.GetYear(dateTimeNow);
          if (result.Month == -1)
            result.Month = 1;
          if (result.Day == -1)
            result.Day = 1;
        }
      }
      if (result.Hour == -1)
        result.Hour = 0;
      if (result.Minute == -1)
        result.Minute = 0;
      if (result.Second == -1)
        result.Second = 0;
      if (result.era == -1)
        result.era = 0;
      return true;
    }

    private static string ExpandPredefinedFormat(string format, ref DateTimeFormatInfo dtfi, ref ParsingInfo parseInfo, ref DateTimeResult result)
    {
      char ch = format[0];
      if ((uint) ch <= 82U)
      {
        if ((int) ch != 79)
        {
          if ((int) ch == 82)
            goto label_5;
          else
            goto label_12;
        }
      }
      else
      {
        switch (ch)
        {
          case 'U':
            parseInfo.calendar = GregorianCalendar.GetDefaultInstance();
            result.flags |= ParseFlags.TimeZoneUsed;
            result.timeZoneOffset = new TimeSpan(0L);
            result.flags |= ParseFlags.TimeZoneUtc;
            if (dtfi.Calendar.GetType() != typeof (GregorianCalendar))
            {
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              DateTimeFormatInfo& local = @dtfi;
              // ISSUE: explicit reference operation
              DateTimeFormatInfo dateTimeFormatInfo = (DateTimeFormatInfo) (^local).Clone();
              // ISSUE: explicit reference operation
              ^local = dateTimeFormatInfo;
              dtfi.Calendar = GregorianCalendar.GetDefaultInstance();
              goto label_12;
            }
            else
              goto label_12;
          case 'o':
            break;
          case 'r':
            goto label_5;
          case 's':
            dtfi = DateTimeFormatInfo.InvariantInfo;
            parseInfo.calendar = GregorianCalendar.GetDefaultInstance();
            goto label_12;
          case 'u':
            parseInfo.calendar = GregorianCalendar.GetDefaultInstance();
            dtfi = DateTimeFormatInfo.InvariantInfo;
            if ((result.flags & ParseFlags.CaptureOffset) != (ParseFlags) 0)
            {
              result.flags |= ParseFlags.UtcSortPattern;
              goto label_12;
            }
            else
              goto label_12;
          default:
            goto label_12;
        }
      }
      parseInfo.calendar = GregorianCalendar.GetDefaultInstance();
      dtfi = DateTimeFormatInfo.InvariantInfo;
      goto label_12;
label_5:
      parseInfo.calendar = GregorianCalendar.GetDefaultInstance();
      dtfi = DateTimeFormatInfo.InvariantInfo;
      if ((result.flags & ParseFlags.CaptureOffset) != (ParseFlags) 0)
        result.flags |= ParseFlags.Rfc1123Pattern;
label_12:
      return DateTimeFormat.GetRealFormat(format, dtfi);
    }

    private static bool ParseByFormat(ref __DTString str, ref __DTString format, ref ParsingInfo parseInfo, DateTimeFormatInfo dtfi, ref DateTimeResult result)
    {
      int returnValue = 0;
      int result1 = 0;
      int result2 = 0;
      int result3 = 0;
      int result4 = 0;
      int result5 = 0;
      int result6 = 0;
      int result7 = 0;
      double result8 = 0.0;
      DateTimeParse.TM result9 = DateTimeParse.TM.AM;
      char @char = format.GetChar();
      if ((uint) @char <= 75U)
      {
        if ((uint) @char <= 46U)
        {
          if ((uint) @char <= 37U)
          {
            if ((int) @char != 34)
            {
              if ((int) @char == 37)
              {
                if (format.Index >= format.Value.Length - 1 || (int) format.Value[format.Index + 1] == 37)
                {
                  result.SetFailure(ParseFailureKind.Format, "Format_BadFormatSpecifier", (object) null);
                  return false;
                }
                goto label_149;
              }
              else
                goto label_141;
            }
          }
          else if ((int) @char != 39)
          {
            if ((int) @char == 46)
            {
              if (!str.Match(@char))
              {
                if (format.GetNext() && format.Match('F'))
                {
                  format.GetRepeatCount();
                  goto label_149;
                }
                else
                {
                  result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
                  return false;
                }
              }
              else
                goto label_149;
            }
            else
              goto label_141;
          }
          StringBuilder result10 = new StringBuilder();
          if (!DateTimeParse.TryParseQuoteString(format.Value, format.Index, result10, out returnValue))
          {
            result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_BadQuote", (object) @char);
            return false;
          }
          format.Index += returnValue - 1;
          string @string = result10.ToString();
          for (int index = 0; index < @string.Length; ++index)
          {
            if ((int) @string[index] == 32 && parseInfo.fAllowInnerWhite)
              str.SkipWhiteSpaces();
            else if (!str.Match(@string[index]))
            {
              result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
              return false;
            }
          }
          if ((result.flags & ParseFlags.CaptureOffset) != (ParseFlags) 0)
          {
            if ((result.flags & ParseFlags.Rfc1123Pattern) != (ParseFlags) 0 && @string == "GMT")
            {
              result.flags |= ParseFlags.TimeZoneUsed;
              result.timeZoneOffset = TimeSpan.Zero;
              goto label_149;
            }
            else if ((result.flags & ParseFlags.UtcSortPattern) != (ParseFlags) 0 && @string == "Z")
            {
              result.flags |= ParseFlags.TimeZoneUsed;
              result.timeZoneOffset = TimeSpan.Zero;
              goto label_149;
            }
            else
              goto label_149;
          }
          else
            goto label_149;
        }
        else if ((uint) @char <= 58U)
        {
          if ((int) @char != 47)
          {
            if ((int) @char == 58)
            {
              if ((dtfi.TimeSeparator.Length > 1 && (int) dtfi.TimeSeparator[0] == 58 || !str.Match(':')) && !str.Match(dtfi.TimeSeparator))
              {
                result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
                return false;
              }
              goto label_149;
            }
            else
              goto label_141;
          }
          else
          {
            if ((dtfi.DateSeparator.Length > 1 && (int) dtfi.DateSeparator[0] == 47 || !str.Match('/')) && !str.Match(dtfi.DateSeparator))
            {
              result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
              return false;
            }
            goto label_149;
          }
        }
        else if ((int) @char != 70)
        {
          if ((int) @char != 72)
          {
            if ((int) @char == 75)
            {
              if (str.Match('Z'))
              {
                if ((result.flags & ParseFlags.TimeZoneUsed) != (ParseFlags) 0 && result.timeZoneOffset != TimeSpan.Zero)
                {
                  result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_RepeatDateTimePattern", (object) 'K');
                  return false;
                }
                result.flags |= ParseFlags.TimeZoneUsed;
                result.timeZoneOffset = new TimeSpan(0L);
                result.flags |= ParseFlags.TimeZoneUtc;
                goto label_149;
              }
              else if (str.Match('+') || str.Match('-'))
              {
                --str.Index;
                TimeSpan result10 = new TimeSpan(0L);
                if (!DateTimeParse.ParseTimeZoneOffset(ref str, 3, ref result10))
                {
                  result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
                  return false;
                }
                if ((result.flags & ParseFlags.TimeZoneUsed) != (ParseFlags) 0 && result10 != result.timeZoneOffset)
                {
                  result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_RepeatDateTimePattern", (object) 'K');
                  return false;
                }
                result.timeZoneOffset = result10;
                result.flags |= ParseFlags.TimeZoneUsed;
                goto label_149;
              }
              else
                goto label_149;
            }
            else
              goto label_141;
          }
          else
          {
            int repeatCount = format.GetRepeatCount();
            if (!DateTimeParse.ParseDigits(ref str, repeatCount < 2 ? 1 : 2, out result5))
            {
              result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
              return false;
            }
            if (!DateTimeParse.CheckNewValue(ref result.Hour, result5, @char, ref result))
              return false;
            goto label_149;
          }
        }
      }
      else if ((uint) @char <= 104U)
      {
        if ((uint) @char <= 90U)
        {
          if ((int) @char != 77)
          {
            if ((int) @char == 90)
            {
              if ((result.flags & ParseFlags.TimeZoneUsed) != (ParseFlags) 0 && result.timeZoneOffset != TimeSpan.Zero)
              {
                result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_RepeatDateTimePattern", (object) 'Z');
                return false;
              }
              result.flags |= ParseFlags.TimeZoneUsed;
              result.timeZoneOffset = new TimeSpan(0L);
              result.flags |= ParseFlags.TimeZoneUtc;
              ++str.Index;
              if (!DateTimeParse.GetTimeZoneName(ref str))
              {
                result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
                return false;
              }
              --str.Index;
              goto label_149;
            }
            else
              goto label_141;
          }
          else
          {
            int repeatCount = format.GetRepeatCount();
            if (repeatCount <= 2)
            {
              if (!DateTimeParse.ParseDigits(ref str, repeatCount, out result2) && (!parseInfo.fCustomNumberParser || !parseInfo.parseNumberDelegate(ref str, repeatCount, out result2)))
              {
                result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
                return false;
              }
            }
            else
            {
              if (repeatCount == 3)
              {
                if (!DateTimeParse.MatchAbbreviatedMonthName(ref str, dtfi, ref result2))
                {
                  result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
                  return false;
                }
              }
              else if (!DateTimeParse.MatchMonthName(ref str, dtfi, ref result2))
              {
                result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
                return false;
              }
              result.flags |= ParseFlags.ParsedMonthName;
            }
            if (!DateTimeParse.CheckNewValue(ref result.Month, result2, @char, ref result))
              return false;
            goto label_149;
          }
        }
        else
        {
          switch (@char)
          {
            case '\\':
              if (format.GetNext())
              {
                if (!str.Match(format.GetChar()))
                {
                  result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
                  return false;
                }
                goto label_149;
              }
              else
              {
                result.SetFailure(ParseFailureKind.Format, "Format_BadFormatSpecifier", (object) null);
                return false;
              }
            case 'd':
              int repeatCount1 = format.GetRepeatCount();
              if (repeatCount1 <= 2)
              {
                if (!DateTimeParse.ParseDigits(ref str, repeatCount1, out result3) && (!parseInfo.fCustomNumberParser || !parseInfo.parseNumberDelegate(ref str, repeatCount1, out result3)))
                {
                  result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
                  return false;
                }
                if (!DateTimeParse.CheckNewValue(ref result.Day, result3, @char, ref result))
                  return false;
                goto label_149;
              }
              else
              {
                if (repeatCount1 == 3)
                {
                  if (!DateTimeParse.MatchAbbreviatedDayName(ref str, dtfi, ref result4))
                  {
                    result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
                    return false;
                  }
                }
                else if (!DateTimeParse.MatchDayName(ref str, dtfi, ref result4))
                {
                  result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
                  return false;
                }
                if (!DateTimeParse.CheckNewValue(ref parseInfo.dayOfWeek, result4, @char, ref result))
                  return false;
                goto label_149;
              }
            case 'f':
              break;
            case 'g':
              format.GetRepeatCount();
              if (!DateTimeParse.MatchEraName(ref str, dtfi, ref result.era))
              {
                result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
                return false;
              }
              goto label_149;
            case 'h':
              parseInfo.fUseHour12 = true;
              int repeatCount2 = format.GetRepeatCount();
              if (!DateTimeParse.ParseDigits(ref str, repeatCount2 < 2 ? 1 : 2, out result5))
              {
                result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
                return false;
              }
              if (!DateTimeParse.CheckNewValue(ref result.Hour, result5, @char, ref result))
                return false;
              goto label_149;
            default:
              goto label_141;
          }
        }
      }
      else if ((uint) @char <= 115U)
      {
        if ((int) @char != 109)
        {
          if ((int) @char == 115)
          {
            int repeatCount3 = format.GetRepeatCount();
            if (!DateTimeParse.ParseDigits(ref str, repeatCount3 < 2 ? 1 : 2, out result7))
            {
              result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
              return false;
            }
            if (!DateTimeParse.CheckNewValue(ref result.Second, result7, @char, ref result))
              return false;
            goto label_149;
          }
          else
            goto label_141;
        }
        else
        {
          int repeatCount3 = format.GetRepeatCount();
          if (!DateTimeParse.ParseDigits(ref str, repeatCount3 < 2 ? 1 : 2, out result6))
          {
            result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
            return false;
          }
          if (!DateTimeParse.CheckNewValue(ref result.Minute, result6, @char, ref result))
            return false;
          goto label_149;
        }
      }
      else if ((int) @char != 116)
      {
        if ((int) @char != 121)
        {
          if ((int) @char == 122)
          {
            int repeatCount3 = format.GetRepeatCount();
            TimeSpan result10 = new TimeSpan(0L);
            if (!DateTimeParse.ParseTimeZoneOffset(ref str, repeatCount3, ref result10))
            {
              result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
              return false;
            }
            if ((result.flags & ParseFlags.TimeZoneUsed) != (ParseFlags) 0 && result10 != result.timeZoneOffset)
            {
              result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_RepeatDateTimePattern", (object) 'z');
              return false;
            }
            result.timeZoneOffset = result10;
            result.flags |= ParseFlags.TimeZoneUsed;
            goto label_149;
          }
          else
            goto label_141;
        }
        else
        {
          int repeatCount3 = format.GetRepeatCount();
          bool flag;
          if (dtfi.HasForceTwoDigitYears)
          {
            flag = DateTimeParse.ParseDigits(ref str, 1, 4, out result1);
          }
          else
          {
            if (repeatCount3 <= 2)
              parseInfo.fUseTwoDigitYear = true;
            flag = DateTimeParse.ParseDigits(ref str, repeatCount3, out result1);
          }
          if (!flag && parseInfo.fCustomNumberParser)
            flag = parseInfo.parseNumberDelegate(ref str, repeatCount3, out result1);
          if (!flag)
          {
            result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
            return false;
          }
          if (!DateTimeParse.CheckNewValue(ref result.Year, result1, @char, ref result))
            return false;
          goto label_149;
        }
      }
      else
      {
        if (format.GetRepeatCount() == 1)
        {
          if (!DateTimeParse.MatchAbbreviatedTimeMark(ref str, dtfi, ref result9))
          {
            result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
            return false;
          }
        }
        else if (!DateTimeParse.MatchTimeMark(ref str, dtfi, ref result9))
        {
          result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
          return false;
        }
        if (parseInfo.timeMark == DateTimeParse.TM.NotSet)
        {
          parseInfo.timeMark = result9;
          goto label_149;
        }
        else
        {
          if (parseInfo.timeMark != result9)
          {
            result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_RepeatDateTimePattern", (object) @char);
            return false;
          }
          goto label_149;
        }
      }
      int repeatCount4 = format.GetRepeatCount();
      if (repeatCount4 <= 7)
      {
        if (!DateTimeParse.ParseFractionExact(ref str, repeatCount4, ref result8) && (int) @char == 102)
        {
          result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
          return false;
        }
        if (result.fraction < 0.0)
        {
          result.fraction = result8;
          goto label_149;
        }
        else
        {
          if (result8 != result.fraction)
          {
            result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_RepeatDateTimePattern", (object) @char);
            return false;
          }
          goto label_149;
        }
      }
      else
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
label_141:
      if ((int) @char == 32)
      {
        if (!parseInfo.fAllowInnerWhite && !str.Match(@char) && (!parseInfo.fAllowTrailingWhite || !format.GetNext() || !DateTimeParse.ParseByFormat(ref str, ref format, ref parseInfo, dtfi, ref result)))
        {
          result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
          return false;
        }
      }
      else if (format.MatchSpecifiedWord("GMT"))
      {
        format.Index += "GMT".Length - 1;
        result.flags |= ParseFlags.TimeZoneUsed;
        result.timeZoneOffset = TimeSpan.Zero;
        if (!str.Match("GMT"))
        {
          result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
          return false;
        }
      }
      else if (!str.Match(@char))
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
label_149:
      return true;
    }

    internal static bool TryParseQuoteString(string format, int pos, StringBuilder result, out int returnValue)
    {
      returnValue = 0;
      int length = format.Length;
      int num = pos;
      char ch1 = format[pos++];
      bool flag = false;
      while (pos < length)
      {
        char ch2 = format[pos++];
        if ((int) ch2 == (int) ch1)
        {
          flag = true;
          break;
        }
        if ((int) ch2 == 92)
        {
          if (pos >= length)
            return false;
          result.Append(format[pos++]);
        }
        else
          result.Append(ch2);
      }
      if (!flag)
        return false;
      returnValue = pos - num;
      return true;
    }

    private static bool DoStrictParse(string s, string formatParam, DateTimeStyles styles, DateTimeFormatInfo dtfi, ref DateTimeResult result)
    {
      ParsingInfo parseInfo = new ParsingInfo();
      parseInfo.Init();
      parseInfo.calendar = dtfi.Calendar;
      parseInfo.fAllowInnerWhite = (uint) (styles & DateTimeStyles.AllowInnerWhite) > 0U;
      parseInfo.fAllowTrailingWhite = (uint) (styles & DateTimeStyles.AllowTrailingWhite) > 0U;
      if (formatParam.Length == 1)
      {
        if ((result.flags & ParseFlags.CaptureOffset) != (ParseFlags) 0 && (int) formatParam[0] == 85)
        {
          result.SetFailure(ParseFailureKind.Format, "Format_BadFormatSpecifier", (object) null);
          return false;
        }
        formatParam = DateTimeParse.ExpandPredefinedFormat(formatParam, ref dtfi, ref parseInfo, ref result);
      }
      result.calendar = parseInfo.calendar;
      if (parseInfo.calendar.ID == 8)
      {
        parseInfo.parseNumberDelegate = DateTimeParse.m_hebrewNumberParser;
        parseInfo.fCustomNumberParser = true;
      }
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      DateTimeResult& local1 = @result;
      int num1;
      int num2 = num1 = -1;
      // ISSUE: explicit reference operation
      (^local1).Second = num1;
      int num3;
      int num4 = num3 = num2;
      // ISSUE: explicit reference operation
      (^local1).Minute = num3;
      int num5 = num4;
      // ISSUE: explicit reference operation
      (^local1).Hour = num5;
      __DTString format = new __DTString(formatParam, dtfi, false);
      __DTString str = new __DTString(s, dtfi, false);
      if (parseInfo.fAllowTrailingWhite)
      {
        format.TrimTail();
        format.RemoveTrailingInQuoteSpaces();
        str.TrimTail();
      }
      if ((styles & DateTimeStyles.AllowLeadingWhite) != DateTimeStyles.None)
      {
        format.SkipWhiteSpaces();
        format.RemoveLeadingInQuoteSpaces();
        str.SkipWhiteSpaces();
      }
      while (format.GetNext())
      {
        if (parseInfo.fAllowInnerWhite)
          str.SkipWhiteSpaces();
        if (!DateTimeParse.ParseByFormat(ref str, ref format, ref parseInfo, dtfi, ref result))
          return false;
      }
      if (str.Index < str.Value.Length - 1)
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
      if (parseInfo.fUseTwoDigitYear && (dtfi.FormatFlags & DateTimeFormatFlags.UseHebrewRule) == DateTimeFormatFlags.None)
      {
        if (result.Year >= 100)
        {
          result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
          return false;
        }
        try
        {
          result.Year = parseInfo.calendar.ToFourDigitYear(result.Year);
        }
        catch (ArgumentOutOfRangeException ex)
        {
          result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) ex);
          return false;
        }
      }
      if (parseInfo.fUseHour12)
      {
        if (parseInfo.timeMark == DateTimeParse.TM.NotSet)
          parseInfo.timeMark = DateTimeParse.TM.AM;
        if (result.Hour > 12)
        {
          result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
          return false;
        }
        if (parseInfo.timeMark == DateTimeParse.TM.AM)
        {
          if (result.Hour == 12)
            result.Hour = 0;
        }
        else
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          DateTimeResult& local2 = @result;
          // ISSUE: explicit reference operation
          int num6 = (^local2).Hour == 12 ? 12 : result.Hour + 12;
          // ISSUE: explicit reference operation
          (^local2).Hour = num6;
        }
      }
      else if (parseInfo.timeMark == DateTimeParse.TM.AM && result.Hour >= 12 || parseInfo.timeMark == DateTimeParse.TM.PM && result.Hour < 12)
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDateTime", (object) null);
        return false;
      }
      bool bTimeOnly = result.Year == -1 && result.Month == -1 && result.Day == -1;
      if (!DateTimeParse.CheckDefaultDateTime(ref result, ref parseInfo.calendar, styles))
        return false;
      if (!bTimeOnly && dtfi.HasYearMonthAdjustment && !dtfi.YearMonthAdjustment(ref result.Year, ref result.Month, (uint) (result.flags & ParseFlags.ParsedMonthName) > 0U))
      {
        result.SetFailure(ParseFailureKind.FormatBadDateTimeCalendar, "Format_BadDateTimeCalendar", (object) null);
        return false;
      }
      if (!parseInfo.calendar.TryToDateTime(result.Year, result.Month, result.Day, result.Hour, result.Minute, result.Second, 0, result.era, out result.parsedDate))
      {
        result.SetFailure(ParseFailureKind.FormatBadDateTimeCalendar, "Format_BadDateTimeCalendar", (object) null);
        return false;
      }
      if (result.fraction > 0.0)
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        DateTimeResult& local2 = @result;
        // ISSUE: explicit reference operation
        DateTime dateTime = (^local2).parsedDate.AddTicks((long) Math.Round(result.fraction * 10000000.0));
        // ISSUE: explicit reference operation
        (^local2).parsedDate = dateTime;
      }
      if (parseInfo.dayOfWeek != -1 && (DayOfWeek) parseInfo.dayOfWeek != parseInfo.calendar.GetDayOfWeek(result.parsedDate))
      {
        result.SetFailure(ParseFailureKind.Format, "Format_BadDayOfWeek", (object) null);
        return false;
      }
      return DateTimeParse.DetermineTimeZoneAdjustments(ref result, styles, bTimeOnly);
    }

    private static Exception GetDateTimeParseException(ref DateTimeResult result)
    {
      switch (result.failure)
      {
        case ParseFailureKind.ArgumentNull:
          return (Exception) new ArgumentNullException(result.failureArgumentName, Environment.GetResourceString(result.failureMessageID));
        case ParseFailureKind.Format:
          return (Exception) new FormatException(Environment.GetResourceString(result.failureMessageID));
        case ParseFailureKind.FormatWithParameter:
          return (Exception) new FormatException(Environment.GetResourceString(result.failureMessageID, result.failureMessageFormatArgument));
        case ParseFailureKind.FormatBadDateTimeCalendar:
          return (Exception) new FormatException(Environment.GetResourceString(result.failureMessageID, (object) result.calendar));
        default:
          return (Exception) null;
      }
    }

    [Conditional("_LOGGING")]
    internal static void LexTraceExit(string message, DateTimeParse.DS dps)
    {
    }

    [Conditional("_LOGGING")]
    internal static void PTSTraceExit(DateTimeParse.DS dps, bool passed)
    {
    }

    [Conditional("_LOGGING")]
    internal static void TPTraceExit(string message, DateTimeParse.DS dps)
    {
    }

    [Conditional("_LOGGING")]
    internal static void DTFITrace(DateTimeFormatInfo dtfi)
    {
    }

    internal delegate bool MatchNumberDelegate(ref __DTString str, int digitLen, out int result);

    internal enum DTT
    {
      End,
      NumEnd,
      NumAmpm,
      NumSpace,
      NumDatesep,
      NumTimesep,
      MonthEnd,
      MonthSpace,
      MonthDatesep,
      NumDatesuff,
      NumTimesuff,
      DayOfWeek,
      YearSpace,
      YearDateSep,
      YearEnd,
      TimeZone,
      Era,
      NumUTCTimeMark,
      Unk,
      NumLocalTimeMark,
      Max,
    }

    internal enum TM
    {
      NotSet = -1,
      AM = 0,
      PM = 1,
    }

    internal enum DS
    {
      BEGIN,
      N,
      NN,
      D_Nd,
      D_NN,
      D_NNd,
      D_M,
      D_MN,
      D_NM,
      D_MNd,
      D_NDS,
      D_Y,
      D_YN,
      D_YNd,
      D_YM,
      D_YMd,
      D_S,
      T_S,
      T_Nt,
      T_NNt,
      ERROR,
      DX_NN,
      DX_NNN,
      DX_MN,
      DX_NM,
      DX_MNN,
      DX_DS,
      DX_DSN,
      DX_NDS,
      DX_NNDS,
      DX_YNN,
      DX_YMN,
      DX_YN,
      DX_YM,
      TX_N,
      TX_NN,
      TX_NNN,
      TX_TS,
      DX_NNY,
    }
  }
}
