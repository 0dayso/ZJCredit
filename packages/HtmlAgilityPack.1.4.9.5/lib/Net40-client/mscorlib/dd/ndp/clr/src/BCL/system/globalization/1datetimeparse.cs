// Decompiled with JetBrains decompiler
// Type: System.__DTString
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Security;
using System.Threading;

namespace System
{
  internal struct __DTString
  {
    private static char[] WhiteSpaceChecks = new char[2]{ ' ', ' ' };
    internal string Value;
    internal int Index;
    internal int len;
    internal char m_current;
    private CompareInfo m_info;
    private bool m_checkDigitToken;

    internal CompareInfo CompareInfo
    {
      get
      {
        return this.m_info;
      }
    }

    internal __DTString(string str, DateTimeFormatInfo dtfi, bool checkDigitToken)
    {
      this = new __DTString(str, dtfi);
      this.m_checkDigitToken = checkDigitToken;
    }

    internal __DTString(string str, DateTimeFormatInfo dtfi)
    {
      this.Index = -1;
      this.Value = str;
      this.len = this.Value.Length;
      this.m_current = char.MinValue;
      if (dtfi != null)
      {
        this.m_info = dtfi.CompareInfo;
        this.m_checkDigitToken = (uint) (dtfi.FormatFlags & DateTimeFormatFlags.UseDigitPrefixInTokens) > 0U;
      }
      else
      {
        this.m_info = Thread.CurrentThread.CurrentCulture.CompareInfo;
        this.m_checkDigitToken = false;
      }
    }

    internal bool GetNext()
    {
      this.Index = this.Index + 1;
      if (this.Index >= this.len)
        return false;
      this.m_current = this.Value[this.Index];
      return true;
    }

    internal bool AtEnd()
    {
      return this.Index >= this.len;
    }

    internal bool Advance(int count)
    {
      this.Index = this.Index + count;
      if (this.Index >= this.len)
        return false;
      this.m_current = this.Value[this.Index];
      return true;
    }

    [SecurityCritical]
    internal void GetRegularToken(out TokenType tokenType, out int tokenValue, DateTimeFormatInfo dtfi)
    {
      tokenValue = 0;
      if (this.Index >= this.len)
      {
        tokenType = TokenType.EndOfString;
      }
      else
      {
        tokenType = TokenType.UnknownToken;
        while (!DateTimeParse.IsDigit(this.m_current))
        {
          if (char.IsWhiteSpace(this.m_current))
          {
label_18:
            int num = this.Index + 1;
            this.Index = num;
            if (num < this.len)
            {
              this.m_current = this.Value[this.Index];
              if (char.IsWhiteSpace(this.m_current))
                goto label_18;
            }
            else
            {
              tokenType = TokenType.EndOfString;
              return;
            }
          }
          else
          {
            dtfi.Tokenize(TokenType.RegularTokenMask, out tokenType, out tokenValue, ref this);
            return;
          }
        }
        tokenValue = (int) this.m_current - 48;
        int num1 = this.Index;
        while (true)
        {
          int num2 = this.Index + 1;
          this.Index = num2;
          if (num2 < this.len)
          {
            this.m_current = this.Value[this.Index];
            int num3 = (int) this.m_current - 48;
            if (num3 >= 0 && num3 <= 9)
            {
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              int& local = @tokenValue;
              // ISSUE: explicit reference operation
              int num4 = ^local * 10 + num3;
              // ISSUE: explicit reference operation
              ^local = num4;
            }
            else
              break;
          }
          else
            break;
        }
        if (this.Index - num1 > 8)
        {
          tokenType = TokenType.NumberToken;
          tokenValue = -1;
        }
        else
          tokenType = this.Index - num1 >= 3 ? TokenType.YearNumberToken : TokenType.NumberToken;
        if (!this.m_checkDigitToken)
          return;
        int num5 = this.Index;
        char ch = this.m_current;
        this.Index = num1;
        this.m_current = this.Value[this.Index];
        TokenType tokenType1;
        int tokenValue1;
        if (dtfi.Tokenize(TokenType.RegularTokenMask, out tokenType1, out tokenValue1, ref this))
        {
          tokenType = tokenType1;
          tokenValue = tokenValue1;
        }
        else
        {
          this.Index = num5;
          this.m_current = ch;
        }
      }
    }

    [SecurityCritical]
    internal TokenType GetSeparatorToken(DateTimeFormatInfo dtfi, out int indexBeforeSeparator, out char charBeforeSeparator)
    {
      indexBeforeSeparator = this.Index;
      charBeforeSeparator = this.m_current;
      if (!this.SkipWhiteSpaceCurrent())
        return TokenType.SEP_End;
      TokenType tokenType;
      if (!DateTimeParse.IsDigit(this.m_current))
      {
        int tokenValue;
        if (!dtfi.Tokenize(TokenType.SeparatorTokenMask, out tokenType, out tokenValue, ref this))
          tokenType = TokenType.SEP_Space;
      }
      else
        tokenType = TokenType.SEP_Space;
      return tokenType;
    }

    internal bool MatchSpecifiedWord(string target)
    {
      string target1 = target;
      int endIndex = target1.Length + this.Index;
      return this.MatchSpecifiedWord(target1, endIndex);
    }

    internal bool MatchSpecifiedWord(string target, int endIndex)
    {
      int num = endIndex - this.Index;
      if (num != target.Length || this.Index + num > this.len)
        return false;
      return this.m_info.Compare(this.Value, this.Index, num, target, 0, num, CompareOptions.IgnoreCase) == 0;
    }

    internal bool MatchSpecifiedWords(string target, bool checkWordBoundary, ref int matchLength)
    {
      int num1 = this.Value.Length - this.Index;
      matchLength = target.Length;
      if (matchLength > num1 || this.m_info.Compare(this.Value, this.Index, matchLength, target, 0, matchLength, CompareOptions.IgnoreCase) != 0)
      {
        int num2 = 0;
        int offset1 = this.Index;
        int num3 = target.IndexOfAny(__DTString.WhiteSpaceChecks, num2);
        if (num3 == -1)
          return false;
        do
        {
          int num4 = num3 - num2;
          if (offset1 >= this.Value.Length - num4)
            return false;
          if (num4 == 0)
          {
            --matchLength;
          }
          else
          {
            if (!char.IsWhiteSpace(this.Value[offset1 + num4]) || this.m_info.Compare(this.Value, offset1, num4, target, num2, num4, CompareOptions.IgnoreCase) != 0)
              return false;
            offset1 = offset1 + num4 + 1;
          }
          num2 = num3 + 1;
          while (offset1 < this.Value.Length && char.IsWhiteSpace(this.Value[offset1]))
          {
            ++offset1;
            ++matchLength;
          }
        }
        while ((num3 = target.IndexOfAny(__DTString.WhiteSpaceChecks, num2)) >= 0);
        if (num2 < target.Length)
        {
          int num4 = target.Length - num2;
          if (offset1 > this.Value.Length - num4 || this.m_info.Compare(this.Value, offset1, num4, target, num2, num4, CompareOptions.IgnoreCase) != 0)
            return false;
        }
      }
      if (checkWordBoundary)
      {
        int index = this.Index + matchLength;
        if (index < this.Value.Length && char.IsLetter(this.Value[index]))
          return false;
      }
      return true;
    }

    internal bool Match(string str)
    {
      int num = this.Index + 1;
      this.Index = num;
      if (num >= this.len || str.Length > this.Value.Length - this.Index || this.m_info.Compare(this.Value, this.Index, str.Length, str, 0, str.Length, CompareOptions.Ordinal) != 0)
        return false;
      this.Index = this.Index + (str.Length - 1);
      return true;
    }

    internal bool Match(char ch)
    {
      int num = this.Index + 1;
      this.Index = num;
      if (num >= this.len)
        return false;
      if ((int) this.Value[this.Index] == (int) ch)
      {
        this.m_current = ch;
        return true;
      }
      this.Index = this.Index - 1;
      return false;
    }

    internal int MatchLongestWords(string[] words, ref int maxMatchStrLen)
    {
      int num = -1;
      for (int index = 0; index < words.Length; ++index)
      {
        string target = words[index];
        int length = target.Length;
        if (this.MatchSpecifiedWords(target, false, ref length) && length > maxMatchStrLen)
        {
          maxMatchStrLen = length;
          num = index;
        }
      }
      return num;
    }

    internal int GetRepeatCount()
    {
      char ch = this.Value[this.Index];
      int index = this.Index + 1;
      while (index < this.len && (int) this.Value[index] == (int) ch)
        ++index;
      int num = index - this.Index;
      this.Index = index - 1;
      return num;
    }

    internal bool GetNextDigit()
    {
      int num = this.Index + 1;
      this.Index = num;
      if (num >= this.len)
        return false;
      return DateTimeParse.IsDigit(this.Value[this.Index]);
    }

    internal char GetChar()
    {
      return this.Value[this.Index];
    }

    internal int GetDigit()
    {
      return (int) this.Value[this.Index] - 48;
    }

    internal void SkipWhiteSpaces()
    {
      while (this.Index + 1 < this.len && char.IsWhiteSpace(this.Value[this.Index + 1]))
        this.Index = this.Index + 1;
    }

    internal bool SkipWhiteSpaceCurrent()
    {
      if (this.Index >= this.len)
        return false;
      if (!char.IsWhiteSpace(this.m_current))
        return true;
      do
      {
        int num = this.Index + 1;
        this.Index = num;
        if (num < this.len)
          this.m_current = this.Value[this.Index];
        else
          goto label_7;
      }
      while (char.IsWhiteSpace(this.m_current));
      return true;
label_7:
      return false;
    }

    internal void TrimTail()
    {
      int index = this.len - 1;
      while (index >= 0 && char.IsWhiteSpace(this.Value[index]))
        --index;
      this.Value = this.Value.Substring(0, index + 1);
      this.len = this.Value.Length;
    }

    internal void RemoveTrailingInQuoteSpaces()
    {
      int index = this.len - 1;
      if (index <= 1)
        return;
      switch (this.Value[index])
      {
        case '\'':
        case '"':
          if (!char.IsWhiteSpace(this.Value[index - 1]))
            break;
          int startIndex = index - 1;
          while (startIndex >= 1 && char.IsWhiteSpace(this.Value[startIndex - 1]))
            --startIndex;
          this.Value = this.Value.Remove(startIndex, this.Value.Length - 1 - startIndex);
          this.len = this.Value.Length;
          break;
      }
    }

    internal void RemoveLeadingInQuoteSpaces()
    {
      if (this.len <= 2)
        return;
      int count = 0;
      switch (this.Value[count])
      {
        case '\'':
        case '"':
          while (count + 1 < this.len && char.IsWhiteSpace(this.Value[count + 1]))
            ++count;
          if (count == 0)
            break;
          this.Value = this.Value.Remove(1, count);
          this.len = this.Value.Length;
          break;
      }
    }

    internal DTSubString GetSubString()
    {
      DTSubString dtSubString = new DTSubString();
      dtSubString.index = this.Index;
      dtSubString.s = this.Value;
      while (this.Index + dtSubString.length < this.len)
      {
        char ch = this.Value[this.Index + dtSubString.length];
        DTSubStringType dtSubStringType = (int) ch < 48 || (int) ch > 57 ? DTSubStringType.Other : DTSubStringType.Number;
        if (dtSubString.length == 0)
          dtSubString.type = dtSubStringType;
        else if (dtSubString.type != dtSubStringType)
          break;
        ++dtSubString.length;
        if (dtSubStringType == DTSubStringType.Number)
        {
          if (dtSubString.length > 8)
          {
            dtSubString.type = DTSubStringType.Invalid;
            return dtSubString;
          }
          int num1 = (int) ch - 48;
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          DTSubString& local = @dtSubString;
          // ISSUE: explicit reference operation
          int num2 = (^local).value * 10 + num1;
          // ISSUE: explicit reference operation
          (^local).value = num2;
        }
        else
          break;
      }
      if (dtSubString.length != 0)
        return dtSubString;
      dtSubString.type = DTSubStringType.End;
      return dtSubString;
    }

    internal void ConsumeSubString(DTSubString sub)
    {
      this.Index = sub.index + sub.length;
      if (this.Index >= this.len)
        return;
      this.m_current = this.Value[this.Index];
    }
  }
}
