﻿// Decompiled with JetBrains decompiler
// Type: System.Globalization.HebrewNumber
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Text;

namespace System.Globalization
{
  internal class HebrewNumber
  {
    private static HebrewNumber.HebrewValue[] HebrewValues = new HebrewNumber.HebrewValue[27]{ new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit1, 1), new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit1, 2), new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit1, 3), new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit1, 4), new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit1, 5), new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit6_7, 6), new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit6_7, 7), new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit1, 8), new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit9, 9), new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit10, 10), new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Invalid, -1), new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit10, 20), new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit10, 30), new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Invalid, -1), new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit10, 40), new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Invalid, -1), new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit10, 50), new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit10, 60), new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit10, 70), new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Invalid, -1), new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit10, 80), new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Invalid, -1), new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit10, 90), new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit100, 100), new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit200_300, 200), new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit200_300, 300), new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit400, 400) };
    private static char maxHebrewNumberCh = (char) (1488 + HebrewNumber.HebrewValues.Length - 1);
    private static readonly HebrewNumber.HS[][] NumberPasingState = new HebrewNumber.HS[17][]{ new HebrewNumber.HS[10]{ HebrewNumber.HS.S400, HebrewNumber.HS.X00, HebrewNumber.HS.X00, HebrewNumber.HS.X0, HebrewNumber.HS.X, HebrewNumber.HS.X, HebrewNumber.HS.X, HebrewNumber.HS.S9, HebrewNumber.HS._err, HebrewNumber.HS._err }, new HebrewNumber.HS[10]{ HebrewNumber.HS.S400_400, HebrewNumber.HS.S400_X00, HebrewNumber.HS.S400_X00, HebrewNumber.HS.S400_X0, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS.X00_S9, HebrewNumber.HS.END, HebrewNumber.HS.S400_DQ }, new HebrewNumber.HS[10]{ HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS.S400_400_100, HebrewNumber.HS.S400_X0, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS.X00_S9, HebrewNumber.HS._err, HebrewNumber.HS.S400_400_DQ }, new HebrewNumber.HS[10]{ HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS.S400_X00_X0, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS.X00_S9, HebrewNumber.HS._err, HebrewNumber.HS.X00_DQ }, new HebrewNumber.HS[10]{ HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS.X0_DQ }, new HebrewNumber.HS[10]{ HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS.END, HebrewNumber.HS.END, HebrewNumber.HS.END, HebrewNumber.HS.END, HebrewNumber.HS.END, HebrewNumber.HS._err, HebrewNumber.HS._err }, new HebrewNumber.HS[10]{ HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS.X0_DQ }, new HebrewNumber.HS[10]{ HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS.END, HebrewNumber.HS.END, HebrewNumber.HS.END, HebrewNumber.HS.END, HebrewNumber.HS._err, HebrewNumber.HS._err }, new HebrewNumber.HS[10]{ HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS.END, HebrewNumber.HS._err }, new HebrewNumber.HS[10]{ HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS.END, HebrewNumber.HS.X0_DQ }, new HebrewNumber.HS[10]{ HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS.S400_X0, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS.X00_S9, HebrewNumber.HS.END, HebrewNumber.HS.X00_DQ }, new HebrewNumber.HS[10]{ HebrewNumber.HS.END, HebrewNumber.HS.END, HebrewNumber.HS.END, HebrewNumber.HS.END, HebrewNumber.HS.END, HebrewNumber.HS.END, HebrewNumber.HS.END, HebrewNumber.HS.END, HebrewNumber.HS._err, HebrewNumber.HS._err }, new HebrewNumber.HS[10]{ HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS.END, HebrewNumber.HS.END, HebrewNumber.HS.END, HebrewNumber.HS.END, HebrewNumber.HS.END, HebrewNumber.HS.END, HebrewNumber.HS._err, HebrewNumber.HS._err }, new HebrewNumber.HS[10]{ HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS.S400_X00_X0, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS.X00_S9, HebrewNumber.HS._err, HebrewNumber.HS.X00_DQ }, new HebrewNumber.HS[10]{ HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS.END, HebrewNumber.HS.S9_DQ }, new HebrewNumber.HS[10]{ HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS.S9_DQ }, new HebrewNumber.HS[10]{ HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS.END, HebrewNumber.HS.END, HebrewNumber.HS._err, HebrewNumber.HS._err, HebrewNumber.HS._err } };
    private const int minHebrewNumberCh = 1488;

    private HebrewNumber()
    {
    }

    internal static string ToString(int Number)
    {
      char ch1 = char.MinValue;
      StringBuilder stringBuilder1 = new StringBuilder();
      if (Number > 5000)
        Number -= 5000;
      int num1 = Number / 100;
      if (num1 > 0)
      {
        Number -= num1 * 100;
        for (int index = 0; index < num1 / 4; ++index)
          stringBuilder1.Append('ת');
        int num2 = num1 % 4;
        if (num2 > 0)
          stringBuilder1.Append((char) (1510 + num2));
      }
      int num3 = Number / 10;
      Number %= 10;
      switch (num3)
      {
        case 0:
          ch1 = char.MinValue;
          break;
        case 1:
          ch1 = 'י';
          break;
        case 2:
          ch1 = 'כ';
          break;
        case 3:
          ch1 = 'ל';
          break;
        case 4:
          ch1 = 'מ';
          break;
        case 5:
          ch1 = 'נ';
          break;
        case 6:
          ch1 = 'ס';
          break;
        case 7:
          ch1 = 'ע';
          break;
        case 8:
          ch1 = 'פ';
          break;
        case 9:
          ch1 = 'צ';
          break;
      }
      char ch2 = Number > 0 ? (char) (1488 + Number - 1) : char.MinValue;
      if ((int) ch2 == 1492 && (int) ch1 == 1497)
      {
        ch2 = 'ו';
        ch1 = 'ט';
      }
      if ((int) ch2 == 1493 && (int) ch1 == 1497)
      {
        ch2 = 'ז';
        ch1 = 'ט';
      }
      if ((int) ch1 != 0)
        stringBuilder1.Append(ch1);
      if ((int) ch2 != 0)
        stringBuilder1.Append(ch2);
      if (stringBuilder1.Length > 1)
      {
        StringBuilder stringBuilder2 = stringBuilder1;
        int index = stringBuilder2.Length - 1;
        int num2 = 34;
        stringBuilder2.Insert(index, (char) num2);
      }
      else
        stringBuilder1.Append('\'');
      return stringBuilder1.ToString();
    }

    internal static HebrewNumberParsingState ParseByChar(char ch, ref HebrewNumberParsingContext context)
    {
      HebrewNumber.HebrewToken hebrewToken;
      if ((int) ch == 39)
        hebrewToken = HebrewNumber.HebrewToken.SingleQuote;
      else if ((int) ch == 34)
      {
        hebrewToken = HebrewNumber.HebrewToken.DoubleQuote;
      }
      else
      {
        int index = (int) ch - 1488;
        if (index < 0 || index >= HebrewNumber.HebrewValues.Length)
          return HebrewNumberParsingState.NotHebrewDigit;
        hebrewToken = HebrewNumber.HebrewValues[index].token;
        if (hebrewToken == HebrewNumber.HebrewToken.Invalid)
          return HebrewNumberParsingState.NotHebrewDigit;
        context.result += HebrewNumber.HebrewValues[index].value;
      }
      context.state = HebrewNumber.NumberPasingState[(int) context.state][(int) hebrewToken];
      if (context.state == HebrewNumber.HS._err)
        return HebrewNumberParsingState.InvalidHebrewNumber;
      return context.state == HebrewNumber.HS.END ? HebrewNumberParsingState.FoundEndOfHebrewNumber : HebrewNumberParsingState.ContinueParsing;
    }

    internal static bool IsDigit(char ch)
    {
      if ((int) ch >= 1488 && (int) ch <= (int) HebrewNumber.maxHebrewNumberCh)
        return HebrewNumber.HebrewValues[(int) ch - 1488].value >= 0;
      if ((int) ch != 39)
        return (int) ch == 34;
      return true;
    }

    private enum HebrewToken
    {
      Invalid = -1,
      Digit400 = 0,
      Digit200_300 = 1,
      Digit100 = 2,
      Digit10 = 3,
      Digit1 = 4,
      Digit6_7 = 5,
      Digit7 = 6,
      Digit9 = 7,
      SingleQuote = 8,
      DoubleQuote = 9,
    }

    private class HebrewValue
    {
      internal HebrewNumber.HebrewToken token;
      internal int value;

      internal HebrewValue(HebrewNumber.HebrewToken token, int value)
      {
        this.token = token;
        this.value = value;
      }
    }

    internal enum HS
    {
      _err = -1,
      Start = 0,
      S400 = 1,
      S400_400 = 2,
      S400_X00 = 3,
      S400_X0 = 4,
      X00_DQ = 5,
      S400_X00_X0 = 6,
      X0_DQ = 7,
      X = 8,
      X0 = 9,
      X00 = 10,
      S400_DQ = 11,
      S400_400_DQ = 12,
      S400_400_100 = 13,
      S9 = 14,
      X00_S9 = 15,
      S9_DQ = 16,
      END = 100,
    }
  }
}
