// Decompiled with JetBrains decompiler
// Type: System.ParsingInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;

namespace System
{
  internal struct ParsingInfo
  {
    internal Calendar calendar;
    internal int dayOfWeek;
    internal DateTimeParse.TM timeMark;
    internal bool fUseHour12;
    internal bool fUseTwoDigitYear;
    internal bool fAllowInnerWhite;
    internal bool fAllowTrailingWhite;
    internal bool fCustomNumberParser;
    internal DateTimeParse.MatchNumberDelegate parseNumberDelegate;

    internal void Init()
    {
      this.dayOfWeek = -1;
      this.timeMark = DateTimeParse.TM.NotSet;
    }
  }
}
