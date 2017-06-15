// Decompiled with JetBrains decompiler
// Type: System.Globalization.TimeSpanParse
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Text;

namespace System.Globalization
{
  internal static class TimeSpanParse
  {
    private static readonly TimeSpanParse.TimeSpanToken zero = new TimeSpanParse.TimeSpanToken(0);
    internal const int unlimitedDigits = -1;
    internal const int maxFractionDigits = 7;
    internal const int maxDays = 10675199;
    internal const int maxHours = 23;
    internal const int maxMinutes = 59;
    internal const int maxSeconds = 59;
    internal const int maxFraction = 9999999;

    internal static void ValidateStyles(TimeSpanStyles style, string parameterName)
    {
      if (style != TimeSpanStyles.None && style != TimeSpanStyles.AssumeNegative)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidTimeSpanStyles"), parameterName);
    }

    private static bool TryTimeToTicks(bool positive, TimeSpanParse.TimeSpanToken days, TimeSpanParse.TimeSpanToken hours, TimeSpanParse.TimeSpanToken minutes, TimeSpanParse.TimeSpanToken seconds, TimeSpanParse.TimeSpanToken fraction, out long result)
    {
      if (days.IsInvalidNumber(10675199, -1) || hours.IsInvalidNumber(23, -1) || (minutes.IsInvalidNumber(59, -1) || seconds.IsInvalidNumber(59, -1)) || fraction.IsInvalidNumber(9999999, 7))
      {
        result = 0L;
        return false;
      }
      long num1 = ((long) days.num * 3600L * 24L + (long) hours.num * 3600L + (long) minutes.num * 60L + (long) seconds.num) * 1000L;
      if (num1 > 922337203685477L || num1 < -922337203685477L)
      {
        result = 0L;
        return false;
      }
      long num2 = (long) fraction.num;
      if (num2 != 0L)
      {
        long num3 = 1000000;
        if (fraction.zeroes > 0)
        {
          long num4 = (long) Math.Pow(10.0, (double) fraction.zeroes);
          num3 /= num4;
        }
        while (num2 < num3)
          num2 *= 10L;
      }
      result = num1 * 10000L + num2;
      if (!positive || result >= 0L)
        return true;
      result = 0L;
      return false;
    }

    internal static TimeSpan Parse(string input, IFormatProvider formatProvider)
    {
      TimeSpanParse.TimeSpanResult result = new TimeSpanParse.TimeSpanResult();
      result.Init(TimeSpanParse.TimeSpanThrowStyle.All);
      if (TimeSpanParse.TryParseTimeSpan(input, TimeSpanParse.TimeSpanStandardStyles.Any, formatProvider, ref result))
        return result.parsedTimeSpan;
      throw result.GetTimeSpanParseException();
    }

    internal static bool TryParse(string input, IFormatProvider formatProvider, out TimeSpan result)
    {
      TimeSpanParse.TimeSpanResult result1 = new TimeSpanParse.TimeSpanResult();
      result1.Init(TimeSpanParse.TimeSpanThrowStyle.None);
      if (TimeSpanParse.TryParseTimeSpan(input, TimeSpanParse.TimeSpanStandardStyles.Any, formatProvider, ref result1))
      {
        result = result1.parsedTimeSpan;
        return true;
      }
      result = new TimeSpan();
      return false;
    }

    internal static TimeSpan ParseExact(string input, string format, IFormatProvider formatProvider, TimeSpanStyles styles)
    {
      TimeSpanParse.TimeSpanResult result = new TimeSpanParse.TimeSpanResult();
      result.Init(TimeSpanParse.TimeSpanThrowStyle.All);
      if (TimeSpanParse.TryParseExactTimeSpan(input, format, formatProvider, styles, ref result))
        return result.parsedTimeSpan;
      throw result.GetTimeSpanParseException();
    }

    internal static bool TryParseExact(string input, string format, IFormatProvider formatProvider, TimeSpanStyles styles, out TimeSpan result)
    {
      TimeSpanParse.TimeSpanResult result1 = new TimeSpanParse.TimeSpanResult();
      result1.Init(TimeSpanParse.TimeSpanThrowStyle.None);
      if (TimeSpanParse.TryParseExactTimeSpan(input, format, formatProvider, styles, ref result1))
      {
        result = result1.parsedTimeSpan;
        return true;
      }
      result = new TimeSpan();
      return false;
    }

    internal static TimeSpan ParseExactMultiple(string input, string[] formats, IFormatProvider formatProvider, TimeSpanStyles styles)
    {
      TimeSpanParse.TimeSpanResult result = new TimeSpanParse.TimeSpanResult();
      result.Init(TimeSpanParse.TimeSpanThrowStyle.All);
      if (TimeSpanParse.TryParseExactMultipleTimeSpan(input, formats, formatProvider, styles, ref result))
        return result.parsedTimeSpan;
      throw result.GetTimeSpanParseException();
    }

    internal static bool TryParseExactMultiple(string input, string[] formats, IFormatProvider formatProvider, TimeSpanStyles styles, out TimeSpan result)
    {
      TimeSpanParse.TimeSpanResult result1 = new TimeSpanParse.TimeSpanResult();
      result1.Init(TimeSpanParse.TimeSpanThrowStyle.None);
      if (TimeSpanParse.TryParseExactMultipleTimeSpan(input, formats, formatProvider, styles, ref result1))
      {
        result = result1.parsedTimeSpan;
        return true;
      }
      result = new TimeSpan();
      return false;
    }

    private static bool TryParseTimeSpan(string input, TimeSpanParse.TimeSpanStandardStyles style, IFormatProvider formatProvider, ref TimeSpanParse.TimeSpanResult result)
    {
      if (input == null)
      {
        result.SetFailure(TimeSpanParse.ParseFailureKind.ArgumentNull, "ArgumentNull_String", (object) null, "input");
        return false;
      }
      input = input.Trim();
      if (input == string.Empty)
      {
        result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
        return false;
      }
      TimeSpanParse.TimeSpanTokenizer timeSpanTokenizer = new TimeSpanParse.TimeSpanTokenizer();
      timeSpanTokenizer.Init(input);
      TimeSpanParse.TimeSpanRawInfo raw = new TimeSpanParse.TimeSpanRawInfo();
      raw.Init(DateTimeFormatInfo.GetInstance(formatProvider));
      for (TimeSpanParse.TimeSpanToken nextToken = timeSpanTokenizer.GetNextToken(); nextToken.ttt != TimeSpanParse.TTT.End; nextToken = timeSpanTokenizer.GetNextToken())
      {
        if (!raw.ProcessToken(ref nextToken, ref result))
        {
          result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
          return false;
        }
      }
      if (!timeSpanTokenizer.EOL)
      {
        result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
        return false;
      }
      if (TimeSpanParse.ProcessTerminalState(ref raw, style, ref result))
        return true;
      result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
      return false;
    }

    private static bool ProcessTerminalState(ref TimeSpanParse.TimeSpanRawInfo raw, TimeSpanParse.TimeSpanStandardStyles style, ref TimeSpanParse.TimeSpanResult result)
    {
      if (raw.lastSeenTTT == TimeSpanParse.TTT.Num)
      {
        if (!raw.ProcessToken(ref new TimeSpanParse.TimeSpanToken() { ttt = TimeSpanParse.TTT.Sep, sep = string.Empty }, ref result))
        {
          result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
          return false;
        }
      }
      switch (raw.NumCount)
      {
        case 1:
          return TimeSpanParse.ProcessTerminal_D(ref raw, style, ref result);
        case 2:
          return TimeSpanParse.ProcessTerminal_HM(ref raw, style, ref result);
        case 3:
          return TimeSpanParse.ProcessTerminal_HM_S_D(ref raw, style, ref result);
        case 4:
          return TimeSpanParse.ProcessTerminal_HMS_F_D(ref raw, style, ref result);
        case 5:
          return TimeSpanParse.ProcessTerminal_DHMSF(ref raw, style, ref result);
        default:
          result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
          return false;
      }
    }

    private static bool ProcessTerminal_DHMSF(ref TimeSpanParse.TimeSpanRawInfo raw, TimeSpanParse.TimeSpanStandardStyles style, ref TimeSpanParse.TimeSpanResult result)
    {
      if (raw.SepCount != 6 || raw.NumCount != 5)
      {
        result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
        return false;
      }
      int num = (uint) (style & TimeSpanParse.TimeSpanStandardStyles.Invariant) > 0U ? 1 : 0;
      bool flag1 = (uint) (style & TimeSpanParse.TimeSpanStandardStyles.Localized) > 0U;
      bool positive = false;
      bool flag2 = false;
      if (num != 0)
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        TimeSpanParse.TimeSpanRawInfo& local1 = @raw;
        // ISSUE: explicit reference operation
        TimeSpanFormat.FormatLiterals positiveInvariant = (^local1).PositiveInvariant;
        // ISSUE: explicit reference operation
        if ((^local1).FullMatch(positiveInvariant))
        {
          flag2 = true;
          positive = true;
        }
        if (!flag2)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          TimeSpanParse.TimeSpanRawInfo& local2 = @raw;
          // ISSUE: explicit reference operation
          TimeSpanFormat.FormatLiterals negativeInvariant = (^local2).NegativeInvariant;
          // ISSUE: explicit reference operation
          if ((^local2).FullMatch(negativeInvariant))
          {
            flag2 = true;
            positive = false;
          }
        }
      }
      if (flag1)
      {
        if (!flag2)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          TimeSpanParse.TimeSpanRawInfo& local = @raw;
          // ISSUE: explicit reference operation
          TimeSpanFormat.FormatLiterals positiveLocalized = (^local).PositiveLocalized;
          // ISSUE: explicit reference operation
          if ((^local).FullMatch(positiveLocalized))
          {
            flag2 = true;
            positive = true;
          }
        }
        if (!flag2)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          TimeSpanParse.TimeSpanRawInfo& local = @raw;
          // ISSUE: explicit reference operation
          TimeSpanFormat.FormatLiterals negativeLocalized = (^local).NegativeLocalized;
          // ISSUE: explicit reference operation
          if ((^local).FullMatch(negativeLocalized))
          {
            flag2 = true;
            positive = false;
          }
        }
      }
      if (flag2)
      {
        long result1;
        if (!TimeSpanParse.TryTimeToTicks(positive, raw.numbers[0], raw.numbers[1], raw.numbers[2], raw.numbers[3], raw.numbers[4], out result1))
        {
          result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "Overflow_TimeSpanElementTooLarge");
          return false;
        }
        if (!positive)
        {
          result1 = -result1;
          if (result1 > 0L)
          {
            result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "Overflow_TimeSpanElementTooLarge");
            return false;
          }
        }
        result.parsedTimeSpan._ticks = result1;
        return true;
      }
      result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
      return false;
    }

    private static bool ProcessTerminal_HMS_F_D(ref TimeSpanParse.TimeSpanRawInfo raw, TimeSpanParse.TimeSpanStandardStyles style, ref TimeSpanParse.TimeSpanResult result)
    {
      if (raw.SepCount != 5 || raw.NumCount != 4 || (style & TimeSpanParse.TimeSpanStandardStyles.RequireFull) != TimeSpanParse.TimeSpanStandardStyles.None)
      {
        result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
        return false;
      }
      int num = (uint) (style & TimeSpanParse.TimeSpanStandardStyles.Invariant) > 0U ? 1 : 0;
      bool flag1 = (uint) (style & TimeSpanParse.TimeSpanStandardStyles.Localized) > 0U;
      long result1 = 0;
      bool positive = false;
      bool flag2 = false;
      bool flag3 = false;
      if (num != 0)
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        TimeSpanParse.TimeSpanRawInfo& local1 = @raw;
        // ISSUE: explicit reference operation
        TimeSpanFormat.FormatLiterals positiveInvariant1 = (^local1).PositiveInvariant;
        // ISSUE: explicit reference operation
        if ((^local1).FullHMSFMatch(positiveInvariant1))
        {
          positive = true;
          flag2 = TimeSpanParse.TryTimeToTicks(positive, TimeSpanParse.zero, raw.numbers[0], raw.numbers[1], raw.numbers[2], raw.numbers[3], out result1);
          flag3 = flag3 || !flag2;
        }
        if (!flag2)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          TimeSpanParse.TimeSpanRawInfo& local2 = @raw;
          // ISSUE: explicit reference operation
          TimeSpanFormat.FormatLiterals positiveInvariant2 = (^local2).PositiveInvariant;
          // ISSUE: explicit reference operation
          if ((^local2).FullDHMSMatch(positiveInvariant2))
          {
            positive = true;
            flag2 = TimeSpanParse.TryTimeToTicks(positive, raw.numbers[0], raw.numbers[1], raw.numbers[2], raw.numbers[3], TimeSpanParse.zero, out result1);
            flag3 = flag3 || !flag2;
          }
        }
        if (!flag2)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          TimeSpanParse.TimeSpanRawInfo& local2 = @raw;
          // ISSUE: explicit reference operation
          TimeSpanFormat.FormatLiterals positiveInvariant2 = (^local2).PositiveInvariant;
          // ISSUE: explicit reference operation
          if ((^local2).FullAppCompatMatch(positiveInvariant2))
          {
            positive = true;
            flag2 = TimeSpanParse.TryTimeToTicks(positive, raw.numbers[0], raw.numbers[1], raw.numbers[2], TimeSpanParse.zero, raw.numbers[3], out result1);
            flag3 = flag3 || !flag2;
          }
        }
        if (!flag2)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          TimeSpanParse.TimeSpanRawInfo& local2 = @raw;
          // ISSUE: explicit reference operation
          TimeSpanFormat.FormatLiterals negativeInvariant = (^local2).NegativeInvariant;
          // ISSUE: explicit reference operation
          if ((^local2).FullHMSFMatch(negativeInvariant))
          {
            positive = false;
            flag2 = TimeSpanParse.TryTimeToTicks(positive, TimeSpanParse.zero, raw.numbers[0], raw.numbers[1], raw.numbers[2], raw.numbers[3], out result1);
            flag3 = flag3 || !flag2;
          }
        }
        if (!flag2)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          TimeSpanParse.TimeSpanRawInfo& local2 = @raw;
          // ISSUE: explicit reference operation
          TimeSpanFormat.FormatLiterals negativeInvariant = (^local2).NegativeInvariant;
          // ISSUE: explicit reference operation
          if ((^local2).FullDHMSMatch(negativeInvariant))
          {
            positive = false;
            flag2 = TimeSpanParse.TryTimeToTicks(positive, raw.numbers[0], raw.numbers[1], raw.numbers[2], raw.numbers[3], TimeSpanParse.zero, out result1);
            flag3 = flag3 || !flag2;
          }
        }
        if (!flag2)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          TimeSpanParse.TimeSpanRawInfo& local2 = @raw;
          // ISSUE: explicit reference operation
          TimeSpanFormat.FormatLiterals negativeInvariant = (^local2).NegativeInvariant;
          // ISSUE: explicit reference operation
          if ((^local2).FullAppCompatMatch(negativeInvariant))
          {
            positive = false;
            flag2 = TimeSpanParse.TryTimeToTicks(positive, raw.numbers[0], raw.numbers[1], raw.numbers[2], TimeSpanParse.zero, raw.numbers[3], out result1);
            flag3 = flag3 || !flag2;
          }
        }
      }
      if (flag1)
      {
        if (!flag2)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          TimeSpanParse.TimeSpanRawInfo& local = @raw;
          // ISSUE: explicit reference operation
          TimeSpanFormat.FormatLiterals positiveLocalized = (^local).PositiveLocalized;
          // ISSUE: explicit reference operation
          if ((^local).FullHMSFMatch(positiveLocalized))
          {
            positive = true;
            flag2 = TimeSpanParse.TryTimeToTicks(positive, TimeSpanParse.zero, raw.numbers[0], raw.numbers[1], raw.numbers[2], raw.numbers[3], out result1);
            flag3 = flag3 || !flag2;
          }
        }
        if (!flag2)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          TimeSpanParse.TimeSpanRawInfo& local = @raw;
          // ISSUE: explicit reference operation
          TimeSpanFormat.FormatLiterals positiveLocalized = (^local).PositiveLocalized;
          // ISSUE: explicit reference operation
          if ((^local).FullDHMSMatch(positiveLocalized))
          {
            positive = true;
            flag2 = TimeSpanParse.TryTimeToTicks(positive, raw.numbers[0], raw.numbers[1], raw.numbers[2], raw.numbers[3], TimeSpanParse.zero, out result1);
            flag3 = flag3 || !flag2;
          }
        }
        if (!flag2)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          TimeSpanParse.TimeSpanRawInfo& local = @raw;
          // ISSUE: explicit reference operation
          TimeSpanFormat.FormatLiterals positiveLocalized = (^local).PositiveLocalized;
          // ISSUE: explicit reference operation
          if ((^local).FullAppCompatMatch(positiveLocalized))
          {
            positive = true;
            flag2 = TimeSpanParse.TryTimeToTicks(positive, raw.numbers[0], raw.numbers[1], raw.numbers[2], TimeSpanParse.zero, raw.numbers[3], out result1);
            flag3 = flag3 || !flag2;
          }
        }
        if (!flag2)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          TimeSpanParse.TimeSpanRawInfo& local = @raw;
          // ISSUE: explicit reference operation
          TimeSpanFormat.FormatLiterals negativeLocalized = (^local).NegativeLocalized;
          // ISSUE: explicit reference operation
          if ((^local).FullHMSFMatch(negativeLocalized))
          {
            positive = false;
            flag2 = TimeSpanParse.TryTimeToTicks(positive, TimeSpanParse.zero, raw.numbers[0], raw.numbers[1], raw.numbers[2], raw.numbers[3], out result1);
            flag3 = flag3 || !flag2;
          }
        }
        if (!flag2)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          TimeSpanParse.TimeSpanRawInfo& local = @raw;
          // ISSUE: explicit reference operation
          TimeSpanFormat.FormatLiterals negativeLocalized = (^local).NegativeLocalized;
          // ISSUE: explicit reference operation
          if ((^local).FullDHMSMatch(negativeLocalized))
          {
            positive = false;
            flag2 = TimeSpanParse.TryTimeToTicks(positive, raw.numbers[0], raw.numbers[1], raw.numbers[2], raw.numbers[3], TimeSpanParse.zero, out result1);
            flag3 = flag3 || !flag2;
          }
        }
        if (!flag2)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          TimeSpanParse.TimeSpanRawInfo& local = @raw;
          // ISSUE: explicit reference operation
          TimeSpanFormat.FormatLiterals negativeLocalized = (^local).NegativeLocalized;
          // ISSUE: explicit reference operation
          if ((^local).FullAppCompatMatch(negativeLocalized))
          {
            positive = false;
            flag2 = TimeSpanParse.TryTimeToTicks(positive, raw.numbers[0], raw.numbers[1], raw.numbers[2], TimeSpanParse.zero, raw.numbers[3], out result1);
            flag3 = flag3 || !flag2;
          }
        }
      }
      if (flag2)
      {
        if (!positive)
        {
          result1 = -result1;
          if (result1 > 0L)
          {
            result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "Overflow_TimeSpanElementTooLarge");
            return false;
          }
        }
        result.parsedTimeSpan._ticks = result1;
        return true;
      }
      if (flag3)
      {
        result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "Overflow_TimeSpanElementTooLarge");
        return false;
      }
      result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
      return false;
    }

    private static bool ProcessTerminal_HM_S_D(ref TimeSpanParse.TimeSpanRawInfo raw, TimeSpanParse.TimeSpanStandardStyles style, ref TimeSpanParse.TimeSpanResult result)
    {
      if (raw.SepCount != 4 || raw.NumCount != 3 || (style & TimeSpanParse.TimeSpanStandardStyles.RequireFull) != TimeSpanParse.TimeSpanStandardStyles.None)
      {
        result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
        return false;
      }
      int num = (uint) (style & TimeSpanParse.TimeSpanStandardStyles.Invariant) > 0U ? 1 : 0;
      bool flag1 = (uint) (style & TimeSpanParse.TimeSpanStandardStyles.Localized) > 0U;
      bool positive = false;
      bool flag2 = false;
      bool flag3 = false;
      long result1 = 0;
      if (num != 0)
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        TimeSpanParse.TimeSpanRawInfo& local1 = @raw;
        // ISSUE: explicit reference operation
        TimeSpanFormat.FormatLiterals positiveInvariant1 = (^local1).PositiveInvariant;
        // ISSUE: explicit reference operation
        if ((^local1).FullHMSMatch(positiveInvariant1))
        {
          positive = true;
          flag2 = TimeSpanParse.TryTimeToTicks(positive, TimeSpanParse.zero, raw.numbers[0], raw.numbers[1], raw.numbers[2], TimeSpanParse.zero, out result1);
          flag3 = flag3 || !flag2;
        }
        if (!flag2)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          TimeSpanParse.TimeSpanRawInfo& local2 = @raw;
          // ISSUE: explicit reference operation
          TimeSpanFormat.FormatLiterals positiveInvariant2 = (^local2).PositiveInvariant;
          // ISSUE: explicit reference operation
          if ((^local2).FullDHMMatch(positiveInvariant2))
          {
            positive = true;
            flag2 = TimeSpanParse.TryTimeToTicks(positive, raw.numbers[0], raw.numbers[1], raw.numbers[2], TimeSpanParse.zero, TimeSpanParse.zero, out result1);
            flag3 = flag3 || !flag2;
          }
        }
        if (!flag2)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          TimeSpanParse.TimeSpanRawInfo& local2 = @raw;
          // ISSUE: explicit reference operation
          TimeSpanFormat.FormatLiterals positiveInvariant2 = (^local2).PositiveInvariant;
          // ISSUE: explicit reference operation
          if ((^local2).PartialAppCompatMatch(positiveInvariant2))
          {
            positive = true;
            flag2 = TimeSpanParse.TryTimeToTicks(positive, TimeSpanParse.zero, raw.numbers[0], raw.numbers[1], TimeSpanParse.zero, raw.numbers[2], out result1);
            flag3 = flag3 || !flag2;
          }
        }
        if (!flag2)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          TimeSpanParse.TimeSpanRawInfo& local2 = @raw;
          // ISSUE: explicit reference operation
          TimeSpanFormat.FormatLiterals negativeInvariant = (^local2).NegativeInvariant;
          // ISSUE: explicit reference operation
          if ((^local2).FullHMSMatch(negativeInvariant))
          {
            positive = false;
            flag2 = TimeSpanParse.TryTimeToTicks(positive, TimeSpanParse.zero, raw.numbers[0], raw.numbers[1], raw.numbers[2], TimeSpanParse.zero, out result1);
            flag3 = flag3 || !flag2;
          }
        }
        if (!flag2)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          TimeSpanParse.TimeSpanRawInfo& local2 = @raw;
          // ISSUE: explicit reference operation
          TimeSpanFormat.FormatLiterals negativeInvariant = (^local2).NegativeInvariant;
          // ISSUE: explicit reference operation
          if ((^local2).FullDHMMatch(negativeInvariant))
          {
            positive = false;
            flag2 = TimeSpanParse.TryTimeToTicks(positive, raw.numbers[0], raw.numbers[1], raw.numbers[2], TimeSpanParse.zero, TimeSpanParse.zero, out result1);
            flag3 = flag3 || !flag2;
          }
        }
        if (!flag2)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          TimeSpanParse.TimeSpanRawInfo& local2 = @raw;
          // ISSUE: explicit reference operation
          TimeSpanFormat.FormatLiterals negativeInvariant = (^local2).NegativeInvariant;
          // ISSUE: explicit reference operation
          if ((^local2).PartialAppCompatMatch(negativeInvariant))
          {
            positive = false;
            flag2 = TimeSpanParse.TryTimeToTicks(positive, TimeSpanParse.zero, raw.numbers[0], raw.numbers[1], TimeSpanParse.zero, raw.numbers[2], out result1);
            flag3 = flag3 || !flag2;
          }
        }
      }
      if (flag1)
      {
        if (!flag2)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          TimeSpanParse.TimeSpanRawInfo& local = @raw;
          // ISSUE: explicit reference operation
          TimeSpanFormat.FormatLiterals positiveLocalized = (^local).PositiveLocalized;
          // ISSUE: explicit reference operation
          if ((^local).FullHMSMatch(positiveLocalized))
          {
            positive = true;
            flag2 = TimeSpanParse.TryTimeToTicks(positive, TimeSpanParse.zero, raw.numbers[0], raw.numbers[1], raw.numbers[2], TimeSpanParse.zero, out result1);
            flag3 = flag3 || !flag2;
          }
        }
        if (!flag2)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          TimeSpanParse.TimeSpanRawInfo& local = @raw;
          // ISSUE: explicit reference operation
          TimeSpanFormat.FormatLiterals positiveLocalized = (^local).PositiveLocalized;
          // ISSUE: explicit reference operation
          if ((^local).FullDHMMatch(positiveLocalized))
          {
            positive = true;
            flag2 = TimeSpanParse.TryTimeToTicks(positive, raw.numbers[0], raw.numbers[1], raw.numbers[2], TimeSpanParse.zero, TimeSpanParse.zero, out result1);
            flag3 = flag3 || !flag2;
          }
        }
        if (!flag2)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          TimeSpanParse.TimeSpanRawInfo& local = @raw;
          // ISSUE: explicit reference operation
          TimeSpanFormat.FormatLiterals positiveLocalized = (^local).PositiveLocalized;
          // ISSUE: explicit reference operation
          if ((^local).PartialAppCompatMatch(positiveLocalized))
          {
            positive = true;
            flag2 = TimeSpanParse.TryTimeToTicks(positive, TimeSpanParse.zero, raw.numbers[0], raw.numbers[1], TimeSpanParse.zero, raw.numbers[2], out result1);
            flag3 = flag3 || !flag2;
          }
        }
        if (!flag2)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          TimeSpanParse.TimeSpanRawInfo& local = @raw;
          // ISSUE: explicit reference operation
          TimeSpanFormat.FormatLiterals negativeLocalized = (^local).NegativeLocalized;
          // ISSUE: explicit reference operation
          if ((^local).FullHMSMatch(negativeLocalized))
          {
            positive = false;
            flag2 = TimeSpanParse.TryTimeToTicks(positive, TimeSpanParse.zero, raw.numbers[0], raw.numbers[1], raw.numbers[2], TimeSpanParse.zero, out result1);
            flag3 = flag3 || !flag2;
          }
        }
        if (!flag2)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          TimeSpanParse.TimeSpanRawInfo& local = @raw;
          // ISSUE: explicit reference operation
          TimeSpanFormat.FormatLiterals negativeLocalized = (^local).NegativeLocalized;
          // ISSUE: explicit reference operation
          if ((^local).FullDHMMatch(negativeLocalized))
          {
            positive = false;
            flag2 = TimeSpanParse.TryTimeToTicks(positive, raw.numbers[0], raw.numbers[1], raw.numbers[2], TimeSpanParse.zero, TimeSpanParse.zero, out result1);
            flag3 = flag3 || !flag2;
          }
        }
        if (!flag2)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          TimeSpanParse.TimeSpanRawInfo& local = @raw;
          // ISSUE: explicit reference operation
          TimeSpanFormat.FormatLiterals negativeLocalized = (^local).NegativeLocalized;
          // ISSUE: explicit reference operation
          if ((^local).PartialAppCompatMatch(negativeLocalized))
          {
            positive = false;
            flag2 = TimeSpanParse.TryTimeToTicks(positive, TimeSpanParse.zero, raw.numbers[0], raw.numbers[1], TimeSpanParse.zero, raw.numbers[2], out result1);
            flag3 = flag3 || !flag2;
          }
        }
      }
      if (flag2)
      {
        if (!positive)
        {
          result1 = -result1;
          if (result1 > 0L)
          {
            result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "Overflow_TimeSpanElementTooLarge");
            return false;
          }
        }
        result.parsedTimeSpan._ticks = result1;
        return true;
      }
      if (flag3)
      {
        result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "Overflow_TimeSpanElementTooLarge");
        return false;
      }
      result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
      return false;
    }

    private static bool ProcessTerminal_HM(ref TimeSpanParse.TimeSpanRawInfo raw, TimeSpanParse.TimeSpanStandardStyles style, ref TimeSpanParse.TimeSpanResult result)
    {
      if (raw.SepCount != 3 || raw.NumCount != 2 || (style & TimeSpanParse.TimeSpanStandardStyles.RequireFull) != TimeSpanParse.TimeSpanStandardStyles.None)
      {
        result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
        return false;
      }
      int num = (uint) (style & TimeSpanParse.TimeSpanStandardStyles.Invariant) > 0U ? 1 : 0;
      bool flag1 = (uint) (style & TimeSpanParse.TimeSpanStandardStyles.Localized) > 0U;
      bool positive = false;
      bool flag2 = false;
      if (num != 0)
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        TimeSpanParse.TimeSpanRawInfo& local1 = @raw;
        // ISSUE: explicit reference operation
        TimeSpanFormat.FormatLiterals positiveInvariant = (^local1).PositiveInvariant;
        // ISSUE: explicit reference operation
        if ((^local1).FullHMMatch(positiveInvariant))
        {
          flag2 = true;
          positive = true;
        }
        if (!flag2)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          TimeSpanParse.TimeSpanRawInfo& local2 = @raw;
          // ISSUE: explicit reference operation
          TimeSpanFormat.FormatLiterals negativeInvariant = (^local2).NegativeInvariant;
          // ISSUE: explicit reference operation
          if ((^local2).FullHMMatch(negativeInvariant))
          {
            flag2 = true;
            positive = false;
          }
        }
      }
      if (flag1)
      {
        if (!flag2)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          TimeSpanParse.TimeSpanRawInfo& local = @raw;
          // ISSUE: explicit reference operation
          TimeSpanFormat.FormatLiterals positiveLocalized = (^local).PositiveLocalized;
          // ISSUE: explicit reference operation
          if ((^local).FullHMMatch(positiveLocalized))
          {
            flag2 = true;
            positive = true;
          }
        }
        if (!flag2)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          TimeSpanParse.TimeSpanRawInfo& local = @raw;
          // ISSUE: explicit reference operation
          TimeSpanFormat.FormatLiterals negativeLocalized = (^local).NegativeLocalized;
          // ISSUE: explicit reference operation
          if ((^local).FullHMMatch(negativeLocalized))
          {
            flag2 = true;
            positive = false;
          }
        }
      }
      long result1 = 0;
      if (flag2)
      {
        if (!TimeSpanParse.TryTimeToTicks(positive, TimeSpanParse.zero, raw.numbers[0], raw.numbers[1], TimeSpanParse.zero, TimeSpanParse.zero, out result1))
        {
          result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "Overflow_TimeSpanElementTooLarge");
          return false;
        }
        if (!positive)
        {
          result1 = -result1;
          if (result1 > 0L)
          {
            result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "Overflow_TimeSpanElementTooLarge");
            return false;
          }
        }
        result.parsedTimeSpan._ticks = result1;
        return true;
      }
      result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
      return false;
    }

    private static bool ProcessTerminal_D(ref TimeSpanParse.TimeSpanRawInfo raw, TimeSpanParse.TimeSpanStandardStyles style, ref TimeSpanParse.TimeSpanResult result)
    {
      if (raw.SepCount != 2 || raw.NumCount != 1 || (style & TimeSpanParse.TimeSpanStandardStyles.RequireFull) != TimeSpanParse.TimeSpanStandardStyles.None)
      {
        result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
        return false;
      }
      int num = (uint) (style & TimeSpanParse.TimeSpanStandardStyles.Invariant) > 0U ? 1 : 0;
      bool flag1 = (uint) (style & TimeSpanParse.TimeSpanStandardStyles.Localized) > 0U;
      bool positive = false;
      bool flag2 = false;
      if (num != 0)
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        TimeSpanParse.TimeSpanRawInfo& local1 = @raw;
        // ISSUE: explicit reference operation
        TimeSpanFormat.FormatLiterals positiveInvariant = (^local1).PositiveInvariant;
        // ISSUE: explicit reference operation
        if ((^local1).FullDMatch(positiveInvariant))
        {
          flag2 = true;
          positive = true;
        }
        if (!flag2)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          TimeSpanParse.TimeSpanRawInfo& local2 = @raw;
          // ISSUE: explicit reference operation
          TimeSpanFormat.FormatLiterals negativeInvariant = (^local2).NegativeInvariant;
          // ISSUE: explicit reference operation
          if ((^local2).FullDMatch(negativeInvariant))
          {
            flag2 = true;
            positive = false;
          }
        }
      }
      if (flag1)
      {
        if (!flag2)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          TimeSpanParse.TimeSpanRawInfo& local = @raw;
          // ISSUE: explicit reference operation
          TimeSpanFormat.FormatLiterals positiveLocalized = (^local).PositiveLocalized;
          // ISSUE: explicit reference operation
          if ((^local).FullDMatch(positiveLocalized))
          {
            flag2 = true;
            positive = true;
          }
        }
        if (!flag2)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          TimeSpanParse.TimeSpanRawInfo& local = @raw;
          // ISSUE: explicit reference operation
          TimeSpanFormat.FormatLiterals negativeLocalized = (^local).NegativeLocalized;
          // ISSUE: explicit reference operation
          if ((^local).FullDMatch(negativeLocalized))
          {
            flag2 = true;
            positive = false;
          }
        }
      }
      long result1 = 0;
      if (flag2)
      {
        if (!TimeSpanParse.TryTimeToTicks(positive, raw.numbers[0], TimeSpanParse.zero, TimeSpanParse.zero, TimeSpanParse.zero, TimeSpanParse.zero, out result1))
        {
          result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "Overflow_TimeSpanElementTooLarge");
          return false;
        }
        if (!positive)
        {
          result1 = -result1;
          if (result1 > 0L)
          {
            result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "Overflow_TimeSpanElementTooLarge");
            return false;
          }
        }
        result.parsedTimeSpan._ticks = result1;
        return true;
      }
      result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
      return false;
    }

    private static bool TryParseExactTimeSpan(string input, string format, IFormatProvider formatProvider, TimeSpanStyles styles, ref TimeSpanParse.TimeSpanResult result)
    {
      if (input == null)
      {
        result.SetFailure(TimeSpanParse.ParseFailureKind.ArgumentNull, "ArgumentNull_String", (object) null, "input");
        return false;
      }
      if (format == null)
      {
        result.SetFailure(TimeSpanParse.ParseFailureKind.ArgumentNull, "ArgumentNull_String", (object) null, "format");
        return false;
      }
      if (format.Length == 0)
      {
        result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadFormatSpecifier");
        return false;
      }
      if (format.Length != 1)
        return TimeSpanParse.TryParseByFormat(input, format, styles, ref result);
      if ((int) format[0] == 99 || (int) format[0] == 116 || (int) format[0] == 84)
        return TimeSpanParse.TryParseTimeSpanConstant(input, ref result);
      TimeSpanParse.TimeSpanStandardStyles style;
      if ((int) format[0] == 103)
        style = TimeSpanParse.TimeSpanStandardStyles.Localized;
      else if ((int) format[0] == 71)
      {
        style = TimeSpanParse.TimeSpanStandardStyles.Localized | TimeSpanParse.TimeSpanStandardStyles.RequireFull;
      }
      else
      {
        result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadFormatSpecifier");
        return false;
      }
      return TimeSpanParse.TryParseTimeSpan(input, style, formatProvider, ref result);
    }

    private static bool TryParseByFormat(string input, string format, TimeSpanStyles styles, ref TimeSpanParse.TimeSpanResult result)
    {
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = false;
      bool flag4 = false;
      bool flag5 = false;
      int result1 = 0;
      int result2 = 0;
      int result3 = 0;
      int result4 = 0;
      int leadingZeroes = 0;
      int number = 0;
      int pos = 0;
      int returnValue = 0;
      TimeSpanParse.TimeSpanTokenizer tokenizer1 = new TimeSpanParse.TimeSpanTokenizer();
      tokenizer1.Init(input, -1);
      while (pos < format.Length)
      {
        char patternChar = format[pos];
        if ((uint) patternChar <= 70U)
        {
          if ((uint) patternChar <= 37U)
          {
            if ((int) patternChar != 34)
            {
              if ((int) patternChar == 37)
              {
                int nextChar = DateTimeFormat.ParseNextChar(format, pos);
                if (nextChar >= 0 && nextChar != 37)
                {
                  returnValue = 1;
                  goto label_41;
                }
                else
                {
                  result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_InvalidString");
                  return false;
                }
              }
              else
                goto label_40;
            }
          }
          else if ((int) patternChar != 39)
          {
            if ((int) patternChar == 70)
            {
              returnValue = DateTimeFormat.ParseRepeatPattern(format, pos, patternChar);
              if (returnValue > 7 | flag5)
              {
                result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_InvalidString");
                return false;
              }
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              TimeSpanParse.TimeSpanTokenizer& tokenizer2 = @tokenizer1;
              int num = returnValue;
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              int& zeroes = @leadingZeroes;
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              int& result5 = @number;
              TimeSpanParse.ParseExactDigits(tokenizer2, num, num, zeroes, result5);
              flag5 = true;
              goto label_41;
            }
            else
              goto label_40;
          }
          StringBuilder stringBuilder = new StringBuilder();
          if (!DateTimeParse.TryParseQuoteString(format, pos, stringBuilder, out returnValue))
          {
            result.SetFailure(TimeSpanParse.ParseFailureKind.FormatWithParameter, "Format_BadQuote", (object) patternChar);
            return false;
          }
          if (!TimeSpanParse.ParseExactLiteral(ref tokenizer1, stringBuilder))
          {
            result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_InvalidString");
            return false;
          }
          goto label_41;
        }
        else if ((uint) patternChar <= 104U)
        {
          switch (patternChar)
          {
            case '\\':
              int nextChar1 = DateTimeFormat.ParseNextChar(format, pos);
              if (nextChar1 >= 0 && (int) tokenizer1.NextChar == (int) (ushort) nextChar1)
              {
                returnValue = 2;
                goto label_41;
              }
              else
              {
                result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_InvalidString");
                return false;
              }
            case 'd':
              returnValue = DateTimeFormat.ParseRepeatPattern(format, pos, patternChar);
              int zeroes1 = 0;
              if (returnValue > 8 | flag1 || !TimeSpanParse.ParseExactDigits(ref tokenizer1, returnValue < 2 ? 1 : returnValue, returnValue < 2 ? 8 : returnValue, out zeroes1, out result1))
              {
                result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_InvalidString");
                return false;
              }
              flag1 = true;
              goto label_41;
            case 'f':
              returnValue = DateTimeFormat.ParseRepeatPattern(format, pos, patternChar);
              if (!(returnValue > 7 | flag5))
              {
                // ISSUE: explicit reference operation
                // ISSUE: variable of a reference type
                TimeSpanParse.TimeSpanTokenizer& tokenizer2 = @tokenizer1;
                int num = returnValue;
                // ISSUE: explicit reference operation
                // ISSUE: variable of a reference type
                int& zeroes2 = @leadingZeroes;
                // ISSUE: explicit reference operation
                // ISSUE: variable of a reference type
                int& result5 = @number;
                if (TimeSpanParse.ParseExactDigits(tokenizer2, num, num, zeroes2, result5))
                {
                  flag5 = true;
                  goto label_41;
                }
              }
              result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_InvalidString");
              return false;
            case 'h':
              returnValue = DateTimeFormat.ParseRepeatPattern(format, pos, patternChar);
              if (returnValue > 2 | flag2 || !TimeSpanParse.ParseExactDigits(ref tokenizer1, returnValue, out result2))
              {
                result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_InvalidString");
                return false;
              }
              flag2 = true;
              goto label_41;
          }
        }
        else if ((int) patternChar != 109)
        {
          if ((int) patternChar == 115)
          {
            returnValue = DateTimeFormat.ParseRepeatPattern(format, pos, patternChar);
            if (returnValue > 2 | flag4 || !TimeSpanParse.ParseExactDigits(ref tokenizer1, returnValue, out result4))
            {
              result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_InvalidString");
              return false;
            }
            flag4 = true;
            goto label_41;
          }
        }
        else
        {
          returnValue = DateTimeFormat.ParseRepeatPattern(format, pos, patternChar);
          if (returnValue > 2 | flag3 || !TimeSpanParse.ParseExactDigits(ref tokenizer1, returnValue, out result3))
          {
            result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_InvalidString");
            return false;
          }
          flag3 = true;
          goto label_41;
        }
label_40:
        result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_InvalidString");
        return false;
label_41:
        pos += returnValue;
      }
      if (!tokenizer1.EOL)
      {
        result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
        return false;
      }
      long result6 = 0;
      bool positive = (styles & TimeSpanStyles.AssumeNegative) == TimeSpanStyles.None;
      if (TimeSpanParse.TryTimeToTicks(positive, new TimeSpanParse.TimeSpanToken(result1), new TimeSpanParse.TimeSpanToken(result2), new TimeSpanParse.TimeSpanToken(result3), new TimeSpanParse.TimeSpanToken(result4), new TimeSpanParse.TimeSpanToken(leadingZeroes, number), out result6))
      {
        if (!positive)
          result6 = -result6;
        result.parsedTimeSpan._ticks = result6;
        return true;
      }
      result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "Overflow_TimeSpanElementTooLarge");
      return false;
    }

    private static bool ParseExactDigits(ref TimeSpanParse.TimeSpanTokenizer tokenizer, int minDigitLength, out int result)
    {
      result = 0;
      int zeroes = 0;
      int maxDigitLength = minDigitLength == 1 ? 2 : minDigitLength;
      return TimeSpanParse.ParseExactDigits(ref tokenizer, minDigitLength, maxDigitLength, out zeroes, out result);
    }

    private static bool ParseExactDigits(ref TimeSpanParse.TimeSpanTokenizer tokenizer, int minDigitLength, int maxDigitLength, out int zeroes, out int result)
    {
      result = 0;
      zeroes = 0;
      int num1;
      for (num1 = 0; num1 < maxDigitLength; ++num1)
      {
        char nextChar = tokenizer.NextChar;
        if ((int) nextChar < 48 || (int) nextChar > 57)
        {
          tokenizer.BackOne();
          break;
        }
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        int& local = @result;
        // ISSUE: explicit reference operation
        int num2 = ^local * 10 + ((int) nextChar - 48);
        // ISSUE: explicit reference operation
        ^local = num2;
        if (result == 0)
          ++zeroes;
      }
      return num1 >= minDigitLength;
    }

    private static bool ParseExactLiteral(ref TimeSpanParse.TimeSpanTokenizer tokenizer, StringBuilder enquotedString)
    {
      for (int index = 0; index < enquotedString.Length; ++index)
      {
        if ((int) enquotedString[index] != (int) tokenizer.NextChar)
          return false;
      }
      return true;
    }

    private static bool TryParseTimeSpanConstant(string input, ref TimeSpanParse.TimeSpanResult result)
    {
      return new TimeSpanParse.StringParser().TryParse(input, ref result);
    }

    private static bool TryParseExactMultipleTimeSpan(string input, string[] formats, IFormatProvider formatProvider, TimeSpanStyles styles, ref TimeSpanParse.TimeSpanResult result)
    {
      if (input == null)
      {
        result.SetFailure(TimeSpanParse.ParseFailureKind.ArgumentNull, "ArgumentNull_String", (object) null, "input");
        return false;
      }
      if (formats == null)
      {
        result.SetFailure(TimeSpanParse.ParseFailureKind.ArgumentNull, "ArgumentNull_String", (object) null, "formats");
        return false;
      }
      if (input.Length == 0)
      {
        result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
        return false;
      }
      if (formats.Length == 0)
      {
        result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadFormatSpecifier");
        return false;
      }
      for (int index = 0; index < formats.Length; ++index)
      {
        if (formats[index] == null || formats[index].Length == 0)
        {
          result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadFormatSpecifier");
          return false;
        }
        TimeSpanParse.TimeSpanResult result1 = new TimeSpanParse.TimeSpanResult();
        result1.Init(TimeSpanParse.TimeSpanThrowStyle.None);
        if (TimeSpanParse.TryParseExactTimeSpan(input, formats[index], formatProvider, styles, ref result1))
        {
          result.parsedTimeSpan = result1.parsedTimeSpan;
          return true;
        }
      }
      result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
      return false;
    }

    private enum TimeSpanThrowStyle
    {
      None,
      All,
    }

    private enum ParseFailureKind
    {
      None,
      ArgumentNull,
      Format,
      FormatWithParameter,
      Overflow,
    }

    [Flags]
    private enum TimeSpanStandardStyles
    {
      None = 0,
      Invariant = 1,
      Localized = 2,
      RequireFull = 4,
      Any = Localized | Invariant,
    }

    private enum TTT
    {
      None,
      End,
      Num,
      Sep,
      NumOverflow,
    }

    private struct TimeSpanToken
    {
      internal TimeSpanParse.TTT ttt;
      internal int num;
      internal int zeroes;
      internal string sep;

      public TimeSpanToken(int number)
      {
        this.ttt = TimeSpanParse.TTT.Num;
        this.num = number;
        this.zeroes = 0;
        this.sep = (string) null;
      }

      public TimeSpanToken(int leadingZeroes, int number)
      {
        this.ttt = TimeSpanParse.TTT.Num;
        this.num = number;
        this.zeroes = leadingZeroes;
        this.sep = (string) null;
      }

      public bool IsInvalidNumber(int maxValue, int maxPrecision)
      {
        if (this.num > maxValue)
          return true;
        if (maxPrecision == -1)
          return false;
        if (this.zeroes > maxPrecision)
          return true;
        if (this.num == 0 || this.zeroes == 0)
          return false;
        return (long) this.num >= (long) maxValue / (long) Math.Pow(10.0, (double) (this.zeroes - 1));
      }
    }

    private struct TimeSpanTokenizer
    {
      private int m_pos;
      private string m_value;

      internal bool EOL
      {
        get
        {
          return this.m_pos >= this.m_value.Length - 1;
        }
      }

      internal char NextChar
      {
        get
        {
          this.m_pos = this.m_pos + 1;
          return this.CurrentChar;
        }
      }

      internal char CurrentChar
      {
        get
        {
          if (this.m_pos > -1 && this.m_pos < this.m_value.Length)
            return this.m_value[this.m_pos];
          return char.MinValue;
        }
      }

      internal void Init(string input)
      {
        this.Init(input, 0);
      }

      internal void Init(string input, int startPosition)
      {
        this.m_pos = startPosition;
        this.m_value = input;
      }

      internal TimeSpanParse.TimeSpanToken GetNextToken()
      {
        TimeSpanParse.TimeSpanToken timeSpanToken = new TimeSpanParse.TimeSpanToken();
        char ch = this.CurrentChar;
        if ((int) ch == 0)
        {
          timeSpanToken.ttt = TimeSpanParse.TTT.End;
          return timeSpanToken;
        }
        if ((int) ch >= 48 && (int) ch <= 57)
        {
          timeSpanToken.ttt = TimeSpanParse.TTT.Num;
          timeSpanToken.num = 0;
          timeSpanToken.zeroes = 0;
          while (((long) timeSpanToken.num & 4026531840L) == 0L)
          {
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            TimeSpanParse.TimeSpanToken& local = @timeSpanToken;
            // ISSUE: explicit reference operation
            int num = (^local).num * 10 + (int) ch - 48;
            // ISSUE: explicit reference operation
            (^local).num = num;
            if (timeSpanToken.num == 0)
              ++timeSpanToken.zeroes;
            if (timeSpanToken.num < 0)
            {
              timeSpanToken.ttt = TimeSpanParse.TTT.NumOverflow;
              return timeSpanToken;
            }
            ch = this.NextChar;
            if ((int) ch < 48 || (int) ch > 57)
              return timeSpanToken;
          }
          timeSpanToken.ttt = TimeSpanParse.TTT.NumOverflow;
          return timeSpanToken;
        }
        timeSpanToken.ttt = TimeSpanParse.TTT.Sep;
        int startIndex = this.m_pos;
        int length = 0;
        while ((int) ch != 0 && ((int) ch < 48 || 57 < (int) ch))
        {
          ch = this.NextChar;
          ++length;
        }
        timeSpanToken.sep = this.m_value.Substring(startIndex, length);
        return timeSpanToken;
      }

      internal void BackOne()
      {
        if (this.m_pos <= 0)
          return;
        this.m_pos = this.m_pos - 1;
      }
    }

    private struct TimeSpanRawInfo
    {
      internal TimeSpanParse.TTT lastSeenTTT;
      internal int tokenCount;
      internal int SepCount;
      internal int NumCount;
      internal string[] literals;
      internal TimeSpanParse.TimeSpanToken[] numbers;
      private TimeSpanFormat.FormatLiterals m_posLoc;
      private TimeSpanFormat.FormatLiterals m_negLoc;
      private bool m_posLocInit;
      private bool m_negLocInit;
      private string m_fullPosPattern;
      private string m_fullNegPattern;
      private const int MaxTokens = 11;
      private const int MaxLiteralTokens = 6;
      private const int MaxNumericTokens = 5;

      internal TimeSpanFormat.FormatLiterals PositiveInvariant
      {
        get
        {
          return TimeSpanFormat.PositiveInvariantFormatLiterals;
        }
      }

      internal TimeSpanFormat.FormatLiterals NegativeInvariant
      {
        get
        {
          return TimeSpanFormat.NegativeInvariantFormatLiterals;
        }
      }

      internal TimeSpanFormat.FormatLiterals PositiveLocalized
      {
        get
        {
          if (!this.m_posLocInit)
          {
            this.m_posLoc = new TimeSpanFormat.FormatLiterals();
            this.m_posLoc.Init(this.m_fullPosPattern, false);
            this.m_posLocInit = true;
          }
          return this.m_posLoc;
        }
      }

      internal TimeSpanFormat.FormatLiterals NegativeLocalized
      {
        get
        {
          if (!this.m_negLocInit)
          {
            this.m_negLoc = new TimeSpanFormat.FormatLiterals();
            this.m_negLoc.Init(this.m_fullNegPattern, false);
            this.m_negLocInit = true;
          }
          return this.m_negLoc;
        }
      }

      internal bool FullAppCompatMatch(TimeSpanFormat.FormatLiterals pattern)
      {
        if (this.SepCount == 5 && this.NumCount == 4 && (pattern.Start == this.literals[0] && pattern.DayHourSep == this.literals[1]) && (pattern.HourMinuteSep == this.literals[2] && pattern.AppCompatLiteral == this.literals[3]))
          return pattern.End == this.literals[4];
        return false;
      }

      internal bool PartialAppCompatMatch(TimeSpanFormat.FormatLiterals pattern)
      {
        if (this.SepCount == 4 && this.NumCount == 3 && (pattern.Start == this.literals[0] && pattern.HourMinuteSep == this.literals[1]) && pattern.AppCompatLiteral == this.literals[2])
          return pattern.End == this.literals[3];
        return false;
      }

      internal bool FullMatch(TimeSpanFormat.FormatLiterals pattern)
      {
        if (this.SepCount == 6 && this.NumCount == 5 && (pattern.Start == this.literals[0] && pattern.DayHourSep == this.literals[1]) && (pattern.HourMinuteSep == this.literals[2] && pattern.MinuteSecondSep == this.literals[3] && pattern.SecondFractionSep == this.literals[4]))
          return pattern.End == this.literals[5];
        return false;
      }

      internal bool FullDMatch(TimeSpanFormat.FormatLiterals pattern)
      {
        if (this.SepCount == 2 && this.NumCount == 1 && pattern.Start == this.literals[0])
          return pattern.End == this.literals[1];
        return false;
      }

      internal bool FullHMMatch(TimeSpanFormat.FormatLiterals pattern)
      {
        if (this.SepCount == 3 && this.NumCount == 2 && (pattern.Start == this.literals[0] && pattern.HourMinuteSep == this.literals[1]))
          return pattern.End == this.literals[2];
        return false;
      }

      internal bool FullDHMMatch(TimeSpanFormat.FormatLiterals pattern)
      {
        if (this.SepCount == 4 && this.NumCount == 3 && (pattern.Start == this.literals[0] && pattern.DayHourSep == this.literals[1]) && pattern.HourMinuteSep == this.literals[2])
          return pattern.End == this.literals[3];
        return false;
      }

      internal bool FullHMSMatch(TimeSpanFormat.FormatLiterals pattern)
      {
        if (this.SepCount == 4 && this.NumCount == 3 && (pattern.Start == this.literals[0] && pattern.HourMinuteSep == this.literals[1]) && pattern.MinuteSecondSep == this.literals[2])
          return pattern.End == this.literals[3];
        return false;
      }

      internal bool FullDHMSMatch(TimeSpanFormat.FormatLiterals pattern)
      {
        if (this.SepCount == 5 && this.NumCount == 4 && (pattern.Start == this.literals[0] && pattern.DayHourSep == this.literals[1]) && (pattern.HourMinuteSep == this.literals[2] && pattern.MinuteSecondSep == this.literals[3]))
          return pattern.End == this.literals[4];
        return false;
      }

      internal bool FullHMSFMatch(TimeSpanFormat.FormatLiterals pattern)
      {
        if (this.SepCount == 5 && this.NumCount == 4 && (pattern.Start == this.literals[0] && pattern.HourMinuteSep == this.literals[1]) && (pattern.MinuteSecondSep == this.literals[2] && pattern.SecondFractionSep == this.literals[3]))
          return pattern.End == this.literals[4];
        return false;
      }

      internal void Init(DateTimeFormatInfo dtfi)
      {
        this.lastSeenTTT = TimeSpanParse.TTT.None;
        this.tokenCount = 0;
        this.SepCount = 0;
        this.NumCount = 0;
        this.literals = new string[6];
        this.numbers = new TimeSpanParse.TimeSpanToken[5];
        this.m_fullPosPattern = dtfi.FullTimeSpanPositivePattern;
        this.m_fullNegPattern = dtfi.FullTimeSpanNegativePattern;
        this.m_posLocInit = false;
        this.m_negLocInit = false;
      }

      internal bool ProcessToken(ref TimeSpanParse.TimeSpanToken tok, ref TimeSpanParse.TimeSpanResult result)
      {
        if (tok.ttt == TimeSpanParse.TTT.NumOverflow)
        {
          result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "Overflow_TimeSpanElementTooLarge", (object) null);
          return false;
        }
        if (tok.ttt != TimeSpanParse.TTT.Sep && tok.ttt != TimeSpanParse.TTT.Num)
        {
          result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan", (object) null);
          return false;
        }
        switch (tok.ttt)
        {
          case TimeSpanParse.TTT.Num:
            if (this.tokenCount == 0 && !this.AddSep(string.Empty, ref result) || !this.AddNum(tok, ref result))
              return false;
            break;
          case TimeSpanParse.TTT.Sep:
            if (!this.AddSep(tok.sep, ref result))
              return false;
            break;
        }
        this.lastSeenTTT = tok.ttt;
        return true;
      }

      private bool AddSep(string sep, ref TimeSpanParse.TimeSpanResult result)
      {
        if (this.SepCount >= 6 || this.tokenCount >= 11)
        {
          result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan", (object) null);
          return false;
        }
        string[] strArray = this.literals;
        int num = this.SepCount;
        this.SepCount = num + 1;
        int index = num;
        string str = sep;
        strArray[index] = str;
        this.tokenCount = this.tokenCount + 1;
        return true;
      }

      private bool AddNum(TimeSpanParse.TimeSpanToken num, ref TimeSpanParse.TimeSpanResult result)
      {
        if (this.NumCount >= 5 || this.tokenCount >= 11)
        {
          result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan", (object) null);
          return false;
        }
        TimeSpanParse.TimeSpanToken[] timeSpanTokenArray = this.numbers;
        int num1 = this.NumCount;
        this.NumCount = num1 + 1;
        int index = num1;
        TimeSpanParse.TimeSpanToken timeSpanToken = num;
        timeSpanTokenArray[index] = timeSpanToken;
        this.tokenCount = this.tokenCount + 1;
        return true;
      }
    }

    private struct TimeSpanResult
    {
      internal TimeSpan parsedTimeSpan;
      internal TimeSpanParse.TimeSpanThrowStyle throwStyle;
      internal TimeSpanParse.ParseFailureKind m_failure;
      internal string m_failureMessageID;
      internal object m_failureMessageFormatArgument;
      internal string m_failureArgumentName;

      internal void Init(TimeSpanParse.TimeSpanThrowStyle canThrow)
      {
        this.parsedTimeSpan = new TimeSpan();
        this.throwStyle = canThrow;
      }

      internal void SetFailure(TimeSpanParse.ParseFailureKind failure, string failureMessageID)
      {
        this.SetFailure(failure, failureMessageID, (object) null, (string) null);
      }

      internal void SetFailure(TimeSpanParse.ParseFailureKind failure, string failureMessageID, object failureMessageFormatArgument)
      {
        this.SetFailure(failure, failureMessageID, failureMessageFormatArgument, (string) null);
      }

      internal void SetFailure(TimeSpanParse.ParseFailureKind failure, string failureMessageID, object failureMessageFormatArgument, string failureArgumentName)
      {
        this.m_failure = failure;
        this.m_failureMessageID = failureMessageID;
        this.m_failureMessageFormatArgument = failureMessageFormatArgument;
        this.m_failureArgumentName = failureArgumentName;
        if (this.throwStyle != TimeSpanParse.TimeSpanThrowStyle.None)
          throw this.GetTimeSpanParseException();
      }

      internal Exception GetTimeSpanParseException()
      {
        switch (this.m_failure)
        {
          case TimeSpanParse.ParseFailureKind.ArgumentNull:
            return (Exception) new ArgumentNullException(this.m_failureArgumentName, Environment.GetResourceString(this.m_failureMessageID));
          case TimeSpanParse.ParseFailureKind.Format:
            return (Exception) new FormatException(Environment.GetResourceString(this.m_failureMessageID));
          case TimeSpanParse.ParseFailureKind.FormatWithParameter:
            return (Exception) new FormatException(Environment.GetResourceString(this.m_failureMessageID, this.m_failureMessageFormatArgument));
          case TimeSpanParse.ParseFailureKind.Overflow:
            return (Exception) new OverflowException(Environment.GetResourceString(this.m_failureMessageID));
          default:
            return (Exception) new FormatException(Environment.GetResourceString("Format_InvalidString"));
        }
      }
    }

    private struct StringParser
    {
      private string str;
      private char ch;
      private int pos;
      private int len;

      internal void NextChar()
      {
        if (this.pos < this.len)
          this.pos = this.pos + 1;
        this.ch = this.pos < this.len ? this.str[this.pos] : char.MinValue;
      }

      internal char NextNonDigit()
      {
        for (int index = this.pos; index < this.len; ++index)
        {
          char ch = this.str[index];
          if ((int) ch < 48 || (int) ch > 57)
            return ch;
        }
        return char.MinValue;
      }

      internal bool TryParse(string input, ref TimeSpanParse.TimeSpanResult result)
      {
        result.parsedTimeSpan._ticks = 0L;
        if (input == null)
        {
          result.SetFailure(TimeSpanParse.ParseFailureKind.ArgumentNull, "ArgumentNull_String", (object) null, "input");
          return false;
        }
        this.str = input;
        this.len = input.Length;
        this.pos = -1;
        this.NextChar();
        this.SkipBlanks();
        bool flag = false;
        if ((int) this.ch == 45)
        {
          flag = true;
          this.NextChar();
        }
        long time1;
        if ((int) this.NextNonDigit() == 58)
        {
          if (!this.ParseTime(out time1, ref result))
            return false;
        }
        else
        {
          int i;
          if (!this.ParseInt(10675199, out i, ref result))
            return false;
          time1 = (long) i * 864000000000L;
          if ((int) this.ch == 46)
          {
            this.NextChar();
            long time2;
            if (!this.ParseTime(out time2, ref result))
              return false;
            time1 += time2;
          }
        }
        if (flag)
        {
          time1 = -time1;
          if (time1 > 0L)
          {
            result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "Overflow_TimeSpanElementTooLarge");
            return false;
          }
        }
        else if (time1 < 0L)
        {
          result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "Overflow_TimeSpanElementTooLarge");
          return false;
        }
        this.SkipBlanks();
        if (this.pos < this.len)
        {
          result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
          return false;
        }
        result.parsedTimeSpan._ticks = time1;
        return true;
      }

      internal bool ParseInt(int max, out int i, ref TimeSpanParse.TimeSpanResult result)
      {
        i = 0;
        int num1 = this.pos;
        while ((int) this.ch >= 48 && (int) this.ch <= 57)
        {
          if (((long) i & 4026531840L) != 0L)
          {
            result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "Overflow_TimeSpanElementTooLarge");
            return false;
          }
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          int& local = @i;
          // ISSUE: explicit reference operation
          int num2 = ^local * 10 + (int) this.ch - 48;
          // ISSUE: explicit reference operation
          ^local = num2;
          if (i < 0)
          {
            result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "Overflow_TimeSpanElementTooLarge");
            return false;
          }
          this.NextChar();
        }
        if (num1 == this.pos)
        {
          result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
          return false;
        }
        if (i <= max)
          return true;
        result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "Overflow_TimeSpanElementTooLarge");
        return false;
      }

      internal bool ParseTime(out long time, ref TimeSpanParse.TimeSpanResult result)
      {
        time = 0L;
        int i;
        if (!this.ParseInt(23, out i, ref result))
          return false;
        time = (long) i * 36000000000L;
        if ((int) this.ch != 58)
        {
          result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
          return false;
        }
        this.NextChar();
        if (!this.ParseInt(59, out i, ref result))
          return false;
        time += (long) i * 600000000L;
        if ((int) this.ch == 58)
        {
          this.NextChar();
          if ((int) this.ch != 46)
          {
            if (!this.ParseInt(59, out i, ref result))
              return false;
            time += (long) i * 10000000L;
          }
          if ((int) this.ch == 46)
          {
            this.NextChar();
            int num = 10000000;
            while (num > 1 && (int) this.ch >= 48 && (int) this.ch <= 57)
            {
              num /= 10;
              time += (long) (((int) this.ch - 48) * num);
              this.NextChar();
            }
          }
        }
        return true;
      }

      internal void SkipBlanks()
      {
        while ((int) this.ch == 32 || (int) this.ch == 9)
          this.NextChar();
      }
    }
  }
}
