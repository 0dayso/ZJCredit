// Decompiled with JetBrains decompiler
// Type: System.Globalization.TimeSpanFormat
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;
using System.Text;

namespace System.Globalization
{
  internal static class TimeSpanFormat
  {
    internal static readonly TimeSpanFormat.FormatLiterals PositiveInvariantFormatLiterals = TimeSpanFormat.FormatLiterals.InitInvariant(false);
    internal static readonly TimeSpanFormat.FormatLiterals NegativeInvariantFormatLiterals = TimeSpanFormat.FormatLiterals.InitInvariant(true);

    [SecuritySafeCritical]
    private static string IntToString(int n, int digits)
    {
      return ParseNumbers.IntToString(n, 10, digits, '0', 0);
    }

    internal static string Format(TimeSpan value, string format, IFormatProvider formatProvider)
    {
      if (format == null || format.Length == 0)
        format = "c";
      if (format.Length != 1)
        return TimeSpanFormat.FormatCustomized(value, format, DateTimeFormatInfo.GetInstance(formatProvider));
      char ch = format[0];
      switch (ch)
      {
        case 'c':
        case 't':
        case 'T':
          return TimeSpanFormat.FormatStandard(value, true, format, TimeSpanFormat.Pattern.Minimum);
        case 'g':
        case 'G':
          DateTimeFormatInfo instance = DateTimeFormatInfo.GetInstance(formatProvider);
          format = value._ticks >= 0L ? instance.FullTimeSpanPositivePattern : instance.FullTimeSpanNegativePattern;
          TimeSpanFormat.Pattern pattern = (int) ch != 103 ? TimeSpanFormat.Pattern.Full : TimeSpanFormat.Pattern.Minimum;
          return TimeSpanFormat.FormatStandard(value, false, format, pattern);
        default:
          throw new FormatException(Environment.GetResourceString("Format_InvalidString"));
      }
    }

    private static string FormatStandard(TimeSpan value, bool isInvariant, string format, TimeSpanFormat.Pattern pattern)
    {
      StringBuilder sb = StringBuilderCache.Acquire(16);
      int num1 = (int) (value._ticks / 864000000000L);
      long num2 = value._ticks % 864000000000L;
      if (value._ticks < 0L)
      {
        num1 = -num1;
        num2 = -num2;
      }
      int n1 = (int) (num2 / 36000000000L % 24L);
      int n2 = (int) (num2 / 600000000L % 60L);
      int n3 = (int) (num2 / 10000000L % 60L);
      int n4 = (int) (num2 % 10000000L);
      TimeSpanFormat.FormatLiterals formatLiterals;
      if (isInvariant)
      {
        formatLiterals = value._ticks >= 0L ? TimeSpanFormat.PositiveInvariantFormatLiterals : TimeSpanFormat.NegativeInvariantFormatLiterals;
      }
      else
      {
        formatLiterals = new TimeSpanFormat.FormatLiterals();
        formatLiterals.Init(format, pattern == TimeSpanFormat.Pattern.Full);
      }
      if (n4 != 0)
        n4 = (int) ((long) n4 / (long) Math.Pow(10.0, (double) (7 - formatLiterals.ff)));
      sb.Append(formatLiterals.Start);
      if (pattern == TimeSpanFormat.Pattern.Full || num1 != 0)
      {
        sb.Append(num1);
        sb.Append(formatLiterals.DayHourSep);
      }
      sb.Append(TimeSpanFormat.IntToString(n1, formatLiterals.hh));
      sb.Append(formatLiterals.HourMinuteSep);
      sb.Append(TimeSpanFormat.IntToString(n2, formatLiterals.mm));
      sb.Append(formatLiterals.MinuteSecondSep);
      sb.Append(TimeSpanFormat.IntToString(n3, formatLiterals.ss));
      if (!isInvariant && pattern == TimeSpanFormat.Pattern.Minimum)
      {
        int num3;
        for (num3 = formatLiterals.ff; num3 > 0 && n4 % 10 == 0; --num3)
          n4 /= 10;
        if (num3 > 0)
        {
          sb.Append(formatLiterals.SecondFractionSep);
          sb.Append(n4.ToString(DateTimeFormat.fixedNumberFormats[num3 - 1], (IFormatProvider) CultureInfo.InvariantCulture));
        }
      }
      else if (pattern == TimeSpanFormat.Pattern.Full || n4 != 0)
      {
        sb.Append(formatLiterals.SecondFractionSep);
        sb.Append(TimeSpanFormat.IntToString(n4, formatLiterals.ff));
      }
      sb.Append(formatLiterals.End);
      return StringBuilderCache.GetStringAndRelease(sb);
    }

    internal static string FormatCustomized(TimeSpan value, string format, DateTimeFormatInfo dtfi)
    {
      int num1 = (int) (value._ticks / 864000000000L);
      long num2 = value._ticks % 864000000000L;
      if (value._ticks < 0L)
      {
        num1 = -num1;
        num2 = -num2;
      }
      int num3 = (int) (num2 / 36000000000L % 24L);
      int num4 = (int) (num2 / 600000000L % 60L);
      int num5 = (int) (num2 / 10000000L % 60L);
      int num6 = (int) (num2 % 10000000L);
      int pos = 0;
      StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
      while (pos < format.Length)
      {
        char patternChar = format[pos];
        int len;
        if ((uint) patternChar <= 70U)
        {
          if ((uint) patternChar <= 37U)
          {
            if ((int) patternChar != 34)
            {
              if ((int) patternChar == 37)
              {
                int nextChar = DateTimeFormat.ParseNextChar(format, pos);
                if (nextChar < 0 || nextChar == 37)
                  throw new FormatException(Environment.GetResourceString("Format_InvalidString"));
                stringBuilder.Append(TimeSpanFormat.FormatCustomized(value, ((char) nextChar).ToString(), dtfi));
                len = 2;
                goto label_43;
              }
              else
                goto label_42;
            }
          }
          else if ((int) patternChar != 39)
          {
            if ((int) patternChar == 70)
            {
              len = DateTimeFormat.ParseRepeatPattern(format, pos, patternChar);
              if (len > 7)
                throw new FormatException(Environment.GetResourceString("Format_InvalidString"));
              long num7 = (long) num6 / (long) Math.Pow(10.0, (double) (7 - len));
              int num8;
              for (num8 = len; num8 > 0 && num7 % 10L == 0L; --num8)
                num7 /= 10L;
              if (num8 > 0)
              {
                stringBuilder.Append(num7.ToString(DateTimeFormat.fixedNumberFormats[num8 - 1], (IFormatProvider) CultureInfo.InvariantCulture));
                goto label_43;
              }
              else
                goto label_43;
            }
            else
              goto label_42;
          }
          StringBuilder result = new StringBuilder();
          len = DateTimeFormat.ParseQuoteString(format, pos, result);
          stringBuilder.Append((object) result);
          goto label_43;
        }
        else if ((uint) patternChar <= 104U)
        {
          switch (patternChar)
          {
            case '\\':
              int nextChar1 = DateTimeFormat.ParseNextChar(format, pos);
              if (nextChar1 < 0)
                throw new FormatException(Environment.GetResourceString("Format_InvalidString"));
              stringBuilder.Append((char) nextChar1);
              len = 2;
              goto label_43;
            case 'd':
              len = DateTimeFormat.ParseRepeatPattern(format, pos, patternChar);
              if (len > 8)
                throw new FormatException(Environment.GetResourceString("Format_InvalidString"));
              DateTimeFormat.FormatDigits(stringBuilder, num1, len, true);
              goto label_43;
            case 'f':
              len = DateTimeFormat.ParseRepeatPattern(format, pos, patternChar);
              if (len > 7)
                throw new FormatException(Environment.GetResourceString("Format_InvalidString"));
              long num9 = (long) num6 / (long) Math.Pow(10.0, (double) (7 - len));
              stringBuilder.Append(num9.ToString(DateTimeFormat.fixedNumberFormats[len - 1], (IFormatProvider) CultureInfo.InvariantCulture));
              goto label_43;
            case 'h':
              len = DateTimeFormat.ParseRepeatPattern(format, pos, patternChar);
              if (len > 2)
                throw new FormatException(Environment.GetResourceString("Format_InvalidString"));
              DateTimeFormat.FormatDigits(stringBuilder, num3, len);
              goto label_43;
          }
        }
        else if ((int) patternChar != 109)
        {
          if ((int) patternChar == 115)
          {
            len = DateTimeFormat.ParseRepeatPattern(format, pos, patternChar);
            if (len > 2)
              throw new FormatException(Environment.GetResourceString("Format_InvalidString"));
            DateTimeFormat.FormatDigits(stringBuilder, num5, len);
            goto label_43;
          }
        }
        else
        {
          len = DateTimeFormat.ParseRepeatPattern(format, pos, patternChar);
          if (len > 2)
            throw new FormatException(Environment.GetResourceString("Format_InvalidString"));
          DateTimeFormat.FormatDigits(stringBuilder, num4, len);
          goto label_43;
        }
label_42:
        throw new FormatException(Environment.GetResourceString("Format_InvalidString"));
label_43:
        pos += len;
      }
      return StringBuilderCache.GetStringAndRelease(stringBuilder);
    }

    internal enum Pattern
    {
      None,
      Minimum,
      Full,
    }

    internal struct FormatLiterals
    {
      internal string AppCompatLiteral;
      internal int dd;
      internal int hh;
      internal int mm;
      internal int ss;
      internal int ff;
      private string[] literals;

      internal string Start
      {
        get
        {
          return this.literals[0];
        }
      }

      internal string DayHourSep
      {
        get
        {
          return this.literals[1];
        }
      }

      internal string HourMinuteSep
      {
        get
        {
          return this.literals[2];
        }
      }

      internal string MinuteSecondSep
      {
        get
        {
          return this.literals[3];
        }
      }

      internal string SecondFractionSep
      {
        get
        {
          return this.literals[4];
        }
      }

      internal string End
      {
        get
        {
          return this.literals[5];
        }
      }

      internal static TimeSpanFormat.FormatLiterals InitInvariant(bool isNegative)
      {
        TimeSpanFormat.FormatLiterals formatLiterals = new TimeSpanFormat.FormatLiterals();
        formatLiterals.literals = new string[6];
        formatLiterals.literals[0] = isNegative ? "-" : string.Empty;
        formatLiterals.literals[1] = ".";
        formatLiterals.literals[2] = ":";
        formatLiterals.literals[3] = ":";
        formatLiterals.literals[4] = ".";
        formatLiterals.literals[5] = string.Empty;
        formatLiterals.AppCompatLiteral = ":.";
        formatLiterals.dd = 2;
        formatLiterals.hh = 2;
        formatLiterals.mm = 2;
        formatLiterals.ss = 2;
        formatLiterals.ff = 7;
        return formatLiterals;
      }

      internal void Init(string format, bool useInvariantFieldLengths)
      {
        this.literals = new string[6];
        for (int index = 0; index < this.literals.Length; ++index)
          this.literals[index] = string.Empty;
        this.dd = 0;
        this.hh = 0;
        this.mm = 0;
        this.ss = 0;
        this.ff = 0;
        StringBuilder sb = StringBuilderCache.Acquire(16);
        bool flag = false;
        char ch1 = '\'';
        int index1 = 0;
        for (int index2 = 0; index2 < format.Length; ++index2)
        {
          char ch2 = format[index2];
          if ((uint) ch2 <= 70U)
          {
            if ((uint) ch2 <= 37U)
            {
              if ((int) ch2 != 34)
              {
                if ((int) ch2 == 37)
                  goto label_32;
                else
                  goto label_32;
              }
            }
            else if ((int) ch2 != 39)
            {
              if ((int) ch2 == 70)
                goto label_30;
              else
                goto label_32;
            }
            if (flag && (int) ch1 == (int) format[index2])
            {
              if (index1 < 0 || index1 > 5)
                return;
              this.literals[index1] = sb.ToString();
              sb.Length = 0;
              flag = false;
              continue;
            }
            if (!flag)
            {
              ch1 = format[index2];
              flag = true;
              continue;
            }
            continue;
          }
          if ((uint) ch2 <= 104U)
          {
            switch (ch2)
            {
              case '\\':
                if (!flag)
                {
                  ++index2;
                  continue;
                }
                goto label_32;
              case 'd':
                if (!flag)
                {
                  index1 = 1;
                  this.dd = this.dd + 1;
                  continue;
                }
                continue;
              case 'f':
                break;
              case 'h':
                if (!flag)
                {
                  index1 = 2;
                  this.hh = this.hh + 1;
                  continue;
                }
                continue;
              default:
                goto label_32;
            }
          }
          else if ((int) ch2 != 109)
          {
            if ((int) ch2 == 115)
            {
              if (!flag)
              {
                index1 = 4;
                this.ss = this.ss + 1;
                continue;
              }
              continue;
            }
            goto label_32;
          }
          else
          {
            if (!flag)
            {
              index1 = 3;
              this.mm = this.mm + 1;
              continue;
            }
            continue;
          }
label_30:
          if (!flag)
          {
            index1 = 5;
            this.ff = this.ff + 1;
            continue;
          }
          continue;
label_32:
          sb.Append(format[index2]);
        }
        this.AppCompatLiteral = this.MinuteSecondSep + this.SecondFractionSep;
        if (useInvariantFieldLengths)
        {
          this.dd = 2;
          this.hh = 2;
          this.mm = 2;
          this.ss = 2;
          this.ff = 7;
        }
        else
        {
          if (this.dd < 1 || this.dd > 2)
            this.dd = 2;
          if (this.hh < 1 || this.hh > 2)
            this.hh = 2;
          if (this.mm < 1 || this.mm > 2)
            this.mm = 2;
          if (this.ss < 1 || this.ss > 2)
            this.ss = 2;
          if (this.ff < 1 || this.ff > 7)
            this.ff = 7;
        }
        StringBuilderCache.Release(sb);
      }
    }
  }
}
