// Decompiled with JetBrains decompiler
// Type: System.TokenType
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  internal enum TokenType
  {
    NumberToken = 1,
    YearNumberToken = 2,
    Am = 3,
    Pm = 4,
    MonthToken = 5,
    EndOfString = 6,
    DayOfWeekToken = 7,
    TimeZoneToken = 8,
    EraToken = 9,
    DateWordToken = 10,
    UnknownToken = 11,
    HebrewNumber = 12,
    JapaneseEraToken = 13,
    TEraToken = 14,
    IgnorableSymbol = 15,
    RegularTokenMask = 255,
    SEP_Unk = 256,
    SEP_End = 512,
    SEP_Space = 768,
    SEP_Am = 1024,
    SEP_Pm = 1280,
    SEP_Date = 1536,
    SEP_Time = 1792,
    SEP_YearSuff = 2048,
    SEP_MonthSuff = 2304,
    SEP_DaySuff = 2560,
    SEP_HourSuff = 2816,
    SEP_MinuteSuff = 3072,
    SEP_SecondSuff = 3328,
    SEP_LocalTimeMark = 3584,
    SEP_DateOrOffset = 3840,
    SeparatorTokenMask = 65280,
  }
}
