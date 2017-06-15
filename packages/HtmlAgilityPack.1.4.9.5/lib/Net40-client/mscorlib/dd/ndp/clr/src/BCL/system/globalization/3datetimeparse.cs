// Decompiled with JetBrains decompiler
// Type: System.DateTimeRawInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System
{
  internal struct DateTimeRawInfo
  {
    [SecurityCritical]
    private unsafe int* num;
    internal int numCount;
    internal int month;
    internal int year;
    internal int dayOfWeek;
    internal int era;
    internal DateTimeParse.TM timeMark;
    internal double fraction;
    internal bool hasSameDateAndTimeSeparators;
    internal bool timeZone;

    [SecurityCritical]
    internal unsafe void Init(int* numberBuffer)
    {
      this.month = -1;
      this.year = -1;
      this.dayOfWeek = -1;
      this.era = -1;
      this.timeMark = DateTimeParse.TM.NotSet;
      this.fraction = -1.0;
      this.num = numberBuffer;
    }

    [SecuritySafeCritical]
    internal unsafe void AddNumber(int value)
    {
      int* numPtr = this.num;
      int num1 = this.numCount;
      this.numCount = num1 + 1;
      IntPtr num2 = (IntPtr) num1 * 4;
      *(int*) ((IntPtr) numPtr + num2) = value;
    }

    [SecuritySafeCritical]
    internal unsafe int GetNumber(int index)
    {
      return this.num[index];
    }
  }
}
