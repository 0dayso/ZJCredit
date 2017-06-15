// Decompiled with JetBrains decompiler
// Type: System.ParseFlags
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  [Flags]
  internal enum ParseFlags
  {
    HaveYear = 1,
    HaveMonth = 2,
    HaveDay = 4,
    HaveHour = 8,
    HaveMinute = 16,
    HaveSecond = 32,
    HaveTime = 64,
    HaveDate = 128,
    TimeZoneUsed = 256,
    TimeZoneUtc = 512,
    ParsedMonthName = 1024,
    CaptureOffset = 2048,
    YearDefault = 4096,
    Rfc1123Pattern = 8192,
    UtcSortPattern = 16384,
  }
}
