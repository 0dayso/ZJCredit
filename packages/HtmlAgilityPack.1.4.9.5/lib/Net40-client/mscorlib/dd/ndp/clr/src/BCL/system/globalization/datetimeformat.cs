// Decompiled with JetBrains decompiler
// Type: System.DateTimeFormat
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Globalization;
using System.Security;
using System.Text;

namespace System
{
  internal static class DateTimeFormat
  {
    internal static readonly TimeSpan NullOffset = TimeSpan.MinValue;
    internal static char[] allStandardFormats = new char[19]{ 'd', 'D', 'f', 'F', 'g', 'G', 'm', 'M', 'o', 'O', 'r', 'R', 's', 't', 'T', 'u', 'U', 'y', 'Y' };
    internal static string[] fixedNumberFormats = new string[7]{ "0", "00", "000", "0000", "00000", "000000", "0000000" };
    internal const int MaxSecondsFractionDigits = 7;
    internal const string RoundtripFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK";
    internal const string RoundtripDateTimeUnfixed = "yyyy'-'MM'-'ddTHH':'mm':'ss zzz";
    private const int DEFAULT_ALL_DATETIMES_SIZE = 132;

    internal static void FormatDigits(StringBuilder outputBuffer, int value, int len)
    {
      DateTimeFormat.FormatDigits(outputBuffer, value, len, false);
    }

    [SecuritySafeCritical]
    internal static unsafe void FormatDigits(StringBuilder outputBuffer, int value, int len, bool overrideLengthLimit)
    {
      if (!overrideLengthLimit && len > 2)
        len = 2;
      char* chPtr1 = stackalloc char[16];
      char* chPtr2 = chPtr1 + 16;
      int num = value;
      do
      {
        *(chPtr2 -= 2) = (char) (num % 10 + 48);
        num /= 10;
      }
      while (num != 0 && chPtr2 > chPtr1);
      int valueCount;
      for (valueCount = (int) (chPtr1 + 16 - chPtr2); valueCount < len && chPtr2 > chPtr1; ++valueCount)
        *(chPtr2 -= 2) = '0';
      outputBuffer.Append(chPtr2, valueCount);
    }

    private static void HebrewFormatDigits(StringBuilder outputBuffer, int digits)
    {
      outputBuffer.Append(HebrewNumber.ToString(digits));
    }

    internal static int ParseRepeatPattern(string format, int pos, char patternChar)
    {
      int length = format.Length;
      int index = pos + 1;
      while (index < length && (int) format[index] == (int) patternChar)
        ++index;
      return index - pos;
    }

    private static string FormatDayOfWeek(int dayOfWeek, int repeat, DateTimeFormatInfo dtfi)
    {
      if (repeat == 3)
        return dtfi.GetAbbreviatedDayName((DayOfWeek) dayOfWeek);
      return dtfi.GetDayName((DayOfWeek) dayOfWeek);
    }

    private static string FormatMonth(int month, int repeatCount, DateTimeFormatInfo dtfi)
    {
      if (repeatCount == 3)
        return dtfi.GetAbbreviatedMonthName(month);
      return dtfi.GetMonthName(month);
    }

    private static string FormatHebrewMonthName(DateTime time, int month, int repeatCount, DateTimeFormatInfo dtfi)
    {
      if (dtfi.Calendar.IsLeapYear(dtfi.Calendar.GetYear(time)))
        return dtfi.internalGetMonthName(month, MonthNameStyles.LeapYear, repeatCount == 3);
      if (month >= 7)
        ++month;
      if (repeatCount == 3)
        return dtfi.GetAbbreviatedMonthName(month);
      return dtfi.GetMonthName(month);
    }

    internal static int ParseQuoteString(string format, int pos, StringBuilder result)
    {
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
            throw new FormatException(Environment.GetResourceString("Format_InvalidString"));
          result.Append(format[pos++]);
        }
        else
          result.Append(ch2);
      }
      if (!flag)
        throw new FormatException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Format_BadQuote"), (object) ch1));
      return pos - num;
    }

    internal static int ParseNextChar(string format, int pos)
    {
      if (pos >= format.Length - 1)
        return -1;
      return (int) format[pos + 1];
    }

    private static bool IsUseGenitiveForm(string format, int index, int tokenLen, char patternToMatch)
    {
      int num1 = 0;
      int index1 = index - 1;
      while (index1 >= 0 && (int) format[index1] != (int) patternToMatch)
        --index1;
      if (index1 >= 0)
      {
        while (--index1 >= 0 && (int) format[index1] == (int) patternToMatch)
          ++num1;
        if (num1 <= 1)
          return true;
      }
      int index2 = index + tokenLen;
      while (index2 < format.Length && (int) format[index2] != (int) patternToMatch)
        ++index2;
      if (index2 < format.Length)
      {
        int num2 = 0;
        while (++index2 < format.Length && (int) format[index2] == (int) patternToMatch)
          ++num2;
        if (num2 <= 1)
          return true;
      }
      return false;
    }

    private static string FormatCustomized(DateTime dateTime, string format, DateTimeFormatInfo dtfi, TimeSpan offset)
    {
      Calendar calendar = dtfi.Calendar;
      StringBuilder stringBuilder1 = StringBuilderCache.Acquire(16);
      bool flag = calendar.ID == 8;
      bool timeOnly = true;
      int index1 = 0;
      while (index1 < format.Length)
      {
        char patternChar = format[index1];
        int num1;
        if ((uint) patternChar <= 75U)
        {
          if ((uint) patternChar <= 47U)
          {
            if ((uint) patternChar <= 37U)
            {
              if ((int) patternChar != 34)
              {
                if ((int) patternChar == 37)
                {
                  int nextChar = DateTimeFormat.ParseNextChar(format, index1);
                  if (nextChar < 0 || nextChar == 37)
                    throw new FormatException(Environment.GetResourceString("Format_InvalidString"));
                  stringBuilder1.Append(DateTimeFormat.FormatCustomized(dateTime, ((char) nextChar).ToString(), dtfi, offset));
                  num1 = 2;
                  goto label_85;
                }
                else
                  goto label_84;
              }
            }
            else if ((int) patternChar != 39)
            {
              if ((int) patternChar == 47)
              {
                stringBuilder1.Append(dtfi.DateSeparator);
                num1 = 1;
                goto label_85;
              }
              else
                goto label_84;
            }
            StringBuilder result = new StringBuilder();
            num1 = DateTimeFormat.ParseQuoteString(format, index1, result);
            stringBuilder1.Append((object) result);
            goto label_85;
          }
          else if ((uint) patternChar <= 70U)
          {
            if ((int) patternChar != 58)
            {
              if ((int) patternChar != 70)
                goto label_84;
            }
            else
            {
              stringBuilder1.Append(dtfi.TimeSeparator);
              num1 = 1;
              goto label_85;
            }
          }
          else if ((int) patternChar != 72)
          {
            if ((int) patternChar == 75)
            {
              num1 = 1;
              DateTimeFormat.FormatCustomizedRoundripTimeZone(dateTime, offset, stringBuilder1);
              goto label_85;
            }
            else
              goto label_84;
          }
          else
          {
            num1 = DateTimeFormat.ParseRepeatPattern(format, index1, patternChar);
            DateTimeFormat.FormatDigits(stringBuilder1, dateTime.Hour, num1);
            goto label_85;
          }
        }
        else if ((uint) patternChar <= 109U)
        {
          if ((uint) patternChar <= 92U)
          {
            if ((int) patternChar != 77)
            {
              if ((int) patternChar == 92)
              {
                int nextChar = DateTimeFormat.ParseNextChar(format, index1);
                if (nextChar < 0)
                  throw new FormatException(Environment.GetResourceString("Format_InvalidString"));
                stringBuilder1.Append((char) nextChar);
                num1 = 2;
                goto label_85;
              }
              else
                goto label_84;
            }
            else
            {
              num1 = DateTimeFormat.ParseRepeatPattern(format, index1, patternChar);
              int month = calendar.GetMonth(dateTime);
              if (num1 <= 2)
              {
                if (flag)
                  DateTimeFormat.HebrewFormatDigits(stringBuilder1, month);
                else
                  DateTimeFormat.FormatDigits(stringBuilder1, month, num1);
              }
              else if (flag)
                stringBuilder1.Append(DateTimeFormat.FormatHebrewMonthName(dateTime, month, num1, dtfi));
              else if ((dtfi.FormatFlags & DateTimeFormatFlags.UseGenitiveMonth) != DateTimeFormatFlags.None && num1 >= 4)
                stringBuilder1.Append(dtfi.internalGetMonthName(month, DateTimeFormat.IsUseGenitiveForm(format, index1, num1, 'd') ? MonthNameStyles.Genitive : MonthNameStyles.Regular, false));
              else
                stringBuilder1.Append(DateTimeFormat.FormatMonth(month, num1, dtfi));
              timeOnly = false;
              goto label_85;
            }
          }
          else
          {
            switch (patternChar)
            {
              case 'd':
                num1 = DateTimeFormat.ParseRepeatPattern(format, index1, patternChar);
                if (num1 <= 2)
                {
                  int dayOfMonth = calendar.GetDayOfMonth(dateTime);
                  if (flag)
                    DateTimeFormat.HebrewFormatDigits(stringBuilder1, dayOfMonth);
                  else
                    DateTimeFormat.FormatDigits(stringBuilder1, dayOfMonth, num1);
                }
                else
                {
                  int dayOfWeek = (int) calendar.GetDayOfWeek(dateTime);
                  stringBuilder1.Append(DateTimeFormat.FormatDayOfWeek(dayOfWeek, num1, dtfi));
                }
                timeOnly = false;
                goto label_85;
              case 'f':
                break;
              case 'g':
                num1 = DateTimeFormat.ParseRepeatPattern(format, index1, patternChar);
                stringBuilder1.Append(dtfi.GetEraName(calendar.GetEra(dateTime)));
                goto label_85;
              case 'h':
                num1 = DateTimeFormat.ParseRepeatPattern(format, index1, patternChar);
                int num2 = dateTime.Hour % 12;
                if (num2 == 0)
                  num2 = 12;
                DateTimeFormat.FormatDigits(stringBuilder1, num2, num1);
                goto label_85;
              case 'm':
                num1 = DateTimeFormat.ParseRepeatPattern(format, index1, patternChar);
                DateTimeFormat.FormatDigits(stringBuilder1, dateTime.Minute, num1);
                goto label_85;
              default:
                goto label_84;
            }
          }
        }
        else if ((uint) patternChar <= 116U)
        {
          if ((int) patternChar != 115)
          {
            if ((int) patternChar == 116)
            {
              num1 = DateTimeFormat.ParseRepeatPattern(format, index1, patternChar);
              if (num1 == 1)
              {
                if (dateTime.Hour < 12)
                {
                  if (dtfi.AMDesignator.Length >= 1)
                  {
                    stringBuilder1.Append(dtfi.AMDesignator[0]);
                    goto label_85;
                  }
                  else
                    goto label_85;
                }
                else if (dtfi.PMDesignator.Length >= 1)
                {
                  stringBuilder1.Append(dtfi.PMDesignator[0]);
                  goto label_85;
                }
                else
                  goto label_85;
              }
              else
              {
                stringBuilder1.Append(dateTime.Hour < 12 ? dtfi.AMDesignator : dtfi.PMDesignator);
                goto label_85;
              }
            }
            else
              goto label_84;
          }
          else
          {
            num1 = DateTimeFormat.ParseRepeatPattern(format, index1, patternChar);
            DateTimeFormat.FormatDigits(stringBuilder1, dateTime.Second, num1);
            goto label_85;
          }
        }
        else if ((int) patternChar != 121)
        {
          if ((int) patternChar == 122)
          {
            num1 = DateTimeFormat.ParseRepeatPattern(format, index1, patternChar);
            DateTimeFormat.FormatCustomizedTimeZone(dateTime, offset, format, num1, timeOnly, stringBuilder1);
            goto label_85;
          }
          else
            goto label_84;
        }
        else
        {
          int year = calendar.GetYear(dateTime);
          num1 = DateTimeFormat.ParseRepeatPattern(format, index1, patternChar);
          if (dtfi.HasForceTwoDigitYears)
            DateTimeFormat.FormatDigits(stringBuilder1, year, num1 <= 2 ? num1 : 2);
          else if (calendar.ID == 8)
            DateTimeFormat.HebrewFormatDigits(stringBuilder1, year);
          else if (num1 <= 2)
          {
            DateTimeFormat.FormatDigits(stringBuilder1, year % 100, num1);
          }
          else
          {
            string format1 = "D" + (object) num1;
            stringBuilder1.Append(year.ToString(format1, (IFormatProvider) CultureInfo.InvariantCulture));
          }
          timeOnly = false;
          goto label_85;
        }
        num1 = DateTimeFormat.ParseRepeatPattern(format, index1, patternChar);
        if (num1 > 7)
          throw new FormatException(Environment.GetResourceString("Format_InvalidString"));
        long num3 = dateTime.Ticks % 10000000L / (long) Math.Pow(10.0, (double) (7 - num1));
        if ((int) patternChar == 102)
        {
          stringBuilder1.Append(((int) num3).ToString(DateTimeFormat.fixedNumberFormats[num1 - 1], (IFormatProvider) CultureInfo.InvariantCulture));
          goto label_85;
        }
        else
        {
          int num4;
          for (num4 = num1; num4 > 0 && num3 % 10L == 0L; --num4)
            num3 /= 10L;
          if (num4 > 0)
          {
            stringBuilder1.Append(((int) num3).ToString(DateTimeFormat.fixedNumberFormats[num4 - 1], (IFormatProvider) CultureInfo.InvariantCulture));
            goto label_85;
          }
          else if (stringBuilder1.Length > 0)
          {
            StringBuilder stringBuilder2 = stringBuilder1;
            int index2 = stringBuilder2.Length - 1;
            if ((int) stringBuilder2[index2] == 46)
            {
              StringBuilder stringBuilder3 = stringBuilder1;
              int startIndex = stringBuilder3.Length - 1;
              int length = 1;
              stringBuilder3.Remove(startIndex, length);
              goto label_85;
            }
            else
              goto label_85;
          }
          else
            goto label_85;
        }
label_84:
        stringBuilder1.Append(patternChar);
        num1 = 1;
label_85:
        index1 += num1;
      }
      return StringBuilderCache.GetStringAndRelease(stringBuilder1);
    }

    private static void FormatCustomizedTimeZone(DateTime dateTime, TimeSpan offset, string format, int tokenLen, bool timeOnly, StringBuilder result)
    {
      if (offset == DateTimeFormat.NullOffset)
      {
        if (timeOnly && dateTime.Ticks < 864000000000L)
          offset = TimeZoneInfo.GetLocalUtcOffset(DateTime.Now, TimeZoneInfoOptions.NoThrowOnInvalidTime);
        else if (dateTime.Kind == DateTimeKind.Utc)
        {
          DateTimeFormat.InvalidFormatForUtc(format, dateTime);
          dateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Local);
          offset = TimeZoneInfo.GetLocalUtcOffset(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime);
        }
        else
          offset = TimeZoneInfo.GetLocalUtcOffset(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime);
      }
      if (offset >= TimeSpan.Zero)
      {
        result.Append('+');
      }
      else
      {
        result.Append('-');
        offset = offset.Negate();
      }
      if (tokenLen <= 1)
      {
        result.AppendFormat((IFormatProvider) CultureInfo.InvariantCulture, "{0:0}", (object) offset.Hours);
      }
      else
      {
        result.AppendFormat((IFormatProvider) CultureInfo.InvariantCulture, "{0:00}", (object) offset.Hours);
        if (tokenLen < 3)
          return;
        result.AppendFormat((IFormatProvider) CultureInfo.InvariantCulture, ":{0:00}", (object) offset.Minutes);
      }
    }

    private static void FormatCustomizedRoundripTimeZone(DateTime dateTime, TimeSpan offset, StringBuilder result)
    {
      if (offset == DateTimeFormat.NullOffset)
      {
        switch (dateTime.Kind)
        {
          case DateTimeKind.Utc:
            result.Append("Z");
            return;
          case DateTimeKind.Local:
            offset = TimeZoneInfo.GetLocalUtcOffset(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime);
            break;
          default:
            return;
        }
      }
      if (offset >= TimeSpan.Zero)
      {
        result.Append('+');
      }
      else
      {
        result.Append('-');
        offset = offset.Negate();
      }
      result.AppendFormat((IFormatProvider) CultureInfo.InvariantCulture, "{0:00}:{1:00}", (object) offset.Hours, (object) offset.Minutes);
    }

    internal static string GetRealFormat(string format, DateTimeFormatInfo dtfi)
    {
      char ch = format[0];
      if ((uint) ch <= 85U)
      {
        switch (ch)
        {
          case 'D':
            return dtfi.LongDatePattern;
          case 'F':
            return dtfi.FullDateTimePattern;
          case 'G':
            return dtfi.GeneralLongTimePattern;
          case 'M':
            break;
          case 'O':
            goto label_10;
          case 'R':
            goto label_11;
          case 'T':
            return dtfi.LongTimePattern;
          case 'U':
            return dtfi.FullDateTimePattern;
          default:
            goto label_18;
        }
      }
      else
      {
        switch (ch)
        {
          case 'Y':
          case 'y':
            return dtfi.YearMonthPattern;
          case 'd':
            return dtfi.ShortDatePattern;
          case 'f':
            return dtfi.LongDatePattern + " " + dtfi.ShortTimePattern;
          case 'g':
            return dtfi.GeneralShortTimePattern;
          case 'm':
            break;
          case 'o':
            goto label_10;
          case 'r':
            goto label_11;
          case 's':
            return dtfi.SortableDateTimePattern;
          case 't':
            return dtfi.ShortTimePattern;
          case 'u':
            return dtfi.UniversalSortableDateTimePattern;
          default:
            goto label_18;
        }
      }
      return dtfi.MonthDayPattern;
label_10:
      return "yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK";
label_11:
      return dtfi.RFC1123Pattern;
label_18:
      throw new FormatException(Environment.GetResourceString("Format_InvalidString"));
    }

    private static string ExpandPredefinedFormat(string format, ref DateTime dateTime, ref DateTimeFormatInfo dtfi, ref TimeSpan offset)
    {
      char ch = format[0];
      if ((uint) ch <= 82U)
      {
        if ((int) ch != 79)
        {
          if ((int) ch == 82)
            goto label_5;
          else
            goto label_21;
        }
      }
      else
      {
        switch (ch)
        {
          case 'U':
            if (offset != DateTimeFormat.NullOffset)
              throw new FormatException(Environment.GetResourceString("Format_InvalidString"));
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            DateTimeFormatInfo& local1 = @dtfi;
            // ISSUE: explicit reference operation
            DateTimeFormatInfo dateTimeFormatInfo = (DateTimeFormatInfo) (^local1).Clone();
            // ISSUE: explicit reference operation
            ^local1 = dateTimeFormatInfo;
            if (dtfi.Calendar.GetType() != typeof (GregorianCalendar))
              dtfi.Calendar = GregorianCalendar.GetDefaultInstance();
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            DateTime& local2 = @dateTime;
            // ISSUE: explicit reference operation
            DateTime universalTime = (^local2).ToUniversalTime();
            // ISSUE: explicit reference operation
            ^local2 = universalTime;
            goto label_21;
          case 'o':
            break;
          case 'r':
            goto label_5;
          case 's':
            dtfi = DateTimeFormatInfo.InvariantInfo;
            goto label_21;
          case 'u':
            if (offset != DateTimeFormat.NullOffset)
              dateTime = dateTime - offset;
            else if (dateTime.Kind == DateTimeKind.Local)
              DateTimeFormat.InvalidFormatForLocal(format, dateTime);
            dtfi = DateTimeFormatInfo.InvariantInfo;
            goto label_21;
          default:
            goto label_21;
        }
      }
      dtfi = DateTimeFormatInfo.InvariantInfo;
      goto label_21;
label_5:
      if (offset != DateTimeFormat.NullOffset)
        dateTime = dateTime - offset;
      else if (dateTime.Kind == DateTimeKind.Local)
        DateTimeFormat.InvalidFormatForLocal(format, dateTime);
      dtfi = DateTimeFormatInfo.InvariantInfo;
label_21:
      format = DateTimeFormat.GetRealFormat(format, dtfi);
      return format;
    }

    internal static string Format(DateTime dateTime, string format, DateTimeFormatInfo dtfi)
    {
      return DateTimeFormat.Format(dateTime, format, dtfi, DateTimeFormat.NullOffset);
    }

    internal static string Format(DateTime dateTime, string format, DateTimeFormatInfo dtfi, TimeSpan offset)
    {
      if (format == null || format.Length == 0)
      {
        bool flag = false;
        if (dateTime.Ticks < 864000000000L)
        {
          switch (dtfi.Calendar.ID)
          {
            case 22:
            case 23:
            case 3:
            case 4:
            case 6:
            case 8:
            case 13:
              flag = true;
              dtfi = DateTimeFormatInfo.InvariantInfo;
              break;
          }
        }
        format = !(offset == DateTimeFormat.NullOffset) ? (!flag ? dtfi.DateTimeOffsetPattern : "yyyy'-'MM'-'ddTHH':'mm':'ss zzz") : (!flag ? "G" : "s");
      }
      if (format.Length == 1)
        format = DateTimeFormat.ExpandPredefinedFormat(format, ref dateTime, ref dtfi, ref offset);
      return DateTimeFormat.FormatCustomized(dateTime, format, dtfi, offset);
    }

    internal static string[] GetAllDateTimes(DateTime dateTime, char format, DateTimeFormatInfo dtfi)
    {
      string[] strArray;
      if ((uint) format <= 85U)
      {
        switch (format)
        {
          case 'D':
          case 'F':
          case 'G':
          case 'M':
          case 'T':
            break;
          case 'O':
          case 'R':
            goto label_9;
          case 'U':
            DateTime universalTime = dateTime.ToUniversalTime();
            string[] dateTimePatterns1 = dtfi.GetAllDateTimePatterns(format);
            strArray = new string[dateTimePatterns1.Length];
            for (int index = 0; index < dateTimePatterns1.Length; ++index)
              strArray[index] = DateTimeFormat.Format(universalTime, dateTimePatterns1[index], dtfi);
            goto label_11;
          default:
            goto label_10;
        }
      }
      else
      {
        switch (format)
        {
          case 'Y':
          case 'd':
          case 'f':
          case 'g':
          case 'm':
          case 't':
          case 'y':
            break;
          case 'o':
          case 'r':
          case 's':
          case 'u':
            goto label_9;
          default:
            goto label_10;
        }
      }
      string[] dateTimePatterns2 = dtfi.GetAllDateTimePatterns(format);
      strArray = new string[dateTimePatterns2.Length];
      for (int index = 0; index < dateTimePatterns2.Length; ++index)
        strArray[index] = DateTimeFormat.Format(dateTime, dateTimePatterns2[index], dtfi);
      goto label_11;
label_9:
      strArray = new string[1]
      {
        DateTimeFormat.Format(dateTime, new string(new char[1]{ format }), dtfi)
      };
      goto label_11;
label_10:
      throw new FormatException(Environment.GetResourceString("Format_InvalidString"));
label_11:
      return strArray;
    }

    internal static string[] GetAllDateTimes(DateTime dateTime, DateTimeFormatInfo dtfi)
    {
      List<string> stringList = new List<string>(132);
      for (int index = 0; index < DateTimeFormat.allStandardFormats.Length; ++index)
      {
        foreach (string allDateTime in DateTimeFormat.GetAllDateTimes(dateTime, DateTimeFormat.allStandardFormats[index], dtfi))
          stringList.Add(allDateTime);
      }
      string[] array = new string[stringList.Count];
      stringList.CopyTo(0, array, 0, stringList.Count);
      return array;
    }

    internal static void InvalidFormatForLocal(string format, DateTime dateTime)
    {
    }

    [SecuritySafeCritical]
    internal static void InvalidFormatForUtc(string format, DateTime dateTime)
    {
      Mda.DateTimeInvalidLocalFormat();
    }
  }
}
