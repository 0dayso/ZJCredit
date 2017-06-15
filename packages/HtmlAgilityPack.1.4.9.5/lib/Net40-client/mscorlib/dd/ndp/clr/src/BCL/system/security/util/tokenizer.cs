// Decompiled with JetBrains decompiler
// Type: System.Security.Util.Tokenizer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.IO;
using System.Text;

namespace System.Security.Util
{
  internal sealed class Tokenizer
  {
    internal const byte bra = 0;
    internal const byte ket = 1;
    internal const byte slash = 2;
    internal const byte cstr = 3;
    internal const byte equals = 4;
    internal const byte quest = 5;
    internal const byte bang = 6;
    internal const byte dash = 7;
    internal const int intOpenBracket = 60;
    internal const int intCloseBracket = 62;
    internal const int intSlash = 47;
    internal const int intEquals = 61;
    internal const int intQuote = 34;
    internal const int intQuest = 63;
    internal const int intBang = 33;
    internal const int intDash = 45;
    internal const int intTab = 9;
    internal const int intCR = 13;
    internal const int intLF = 10;
    internal const int intSpace = 32;
    public int LineNo;
    private int _inProcessingTag;
    private byte[] _inBytes;
    private char[] _inChars;
    private string _inString;
    private int _inIndex;
    private int _inSize;
    private int _inSavedCharacter;
    private Tokenizer.TokenSource _inTokenSource;
    private Tokenizer.ITokenReader _inTokenReader;
    private Tokenizer.StringMaker _maker;
    private string[] _searchStrings;
    private string[] _replaceStrings;
    private int _inNestedIndex;
    private int _inNestedSize;
    private string _inNestedString;

    internal Tokenizer(string input)
    {
      this.BasicInitialization();
      this._inString = input;
      this._inSize = input.Length;
      this._inTokenSource = Tokenizer.TokenSource.String;
    }

    internal Tokenizer(string input, string[] searchStrings, string[] replaceStrings)
    {
      this.BasicInitialization();
      this._inString = input;
      this._inSize = this._inString.Length;
      this._inTokenSource = Tokenizer.TokenSource.NestedStrings;
      this._searchStrings = searchStrings;
      this._replaceStrings = replaceStrings;
    }

    internal Tokenizer(byte[] array, Tokenizer.ByteTokenEncoding encoding, int startIndex)
    {
      this.BasicInitialization();
      this._inBytes = array;
      this._inSize = array.Length;
      this._inIndex = startIndex;
      switch (encoding)
      {
        case Tokenizer.ByteTokenEncoding.UnicodeTokens:
          this._inTokenSource = Tokenizer.TokenSource.UnicodeByteArray;
          break;
        case Tokenizer.ByteTokenEncoding.UTF8Tokens:
          this._inTokenSource = Tokenizer.TokenSource.UTF8ByteArray;
          break;
        case Tokenizer.ByteTokenEncoding.ByteTokens:
          this._inTokenSource = Tokenizer.TokenSource.ASCIIByteArray;
          break;
        default:
          throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) encoding));
      }
    }

    internal Tokenizer(char[] array)
    {
      this.BasicInitialization();
      this._inChars = array;
      this._inSize = array.Length;
      this._inTokenSource = Tokenizer.TokenSource.CharArray;
    }

    internal Tokenizer(StreamReader input)
    {
      this.BasicInitialization();
      this._inTokenReader = (Tokenizer.ITokenReader) new Tokenizer.StreamTokenReader(input);
    }

    internal void BasicInitialization()
    {
      this.LineNo = 1;
      this._inProcessingTag = 0;
      this._inSavedCharacter = -1;
      this._inIndex = 0;
      this._inSize = 0;
      this._inNestedSize = 0;
      this._inNestedIndex = 0;
      this._inTokenSource = Tokenizer.TokenSource.Other;
      this._maker = SharedStatics.GetSharedStringMaker();
    }

    public void Recycle()
    {
      SharedStatics.ReleaseSharedStringMaker(ref this._maker);
    }

    internal void ChangeFormat(Encoding encoding)
    {
      if (encoding == null)
        return;
      switch (this._inTokenSource)
      {
        case Tokenizer.TokenSource.UnicodeByteArray:
        case Tokenizer.TokenSource.UTF8ByteArray:
        case Tokenizer.TokenSource.ASCIIByteArray:
          if (encoding == Encoding.Unicode)
          {
            this._inTokenSource = Tokenizer.TokenSource.UnicodeByteArray;
            return;
          }
          if (encoding == Encoding.UTF8)
          {
            this._inTokenSource = Tokenizer.TokenSource.UTF8ByteArray;
            return;
          }
          if (encoding == Encoding.ASCII)
          {
            this._inTokenSource = Tokenizer.TokenSource.ASCIIByteArray;
            return;
          }
          break;
        case Tokenizer.TokenSource.CharArray:
          return;
        case Tokenizer.TokenSource.String:
          return;
        case Tokenizer.TokenSource.NestedStrings:
          return;
      }
      Stream stream;
      switch (this._inTokenSource)
      {
        case Tokenizer.TokenSource.UnicodeByteArray:
        case Tokenizer.TokenSource.UTF8ByteArray:
        case Tokenizer.TokenSource.ASCIIByteArray:
          stream = (Stream) new MemoryStream(this._inBytes, this._inIndex, this._inSize - this._inIndex);
          break;
        case Tokenizer.TokenSource.CharArray:
          return;
        case Tokenizer.TokenSource.String:
          return;
        case Tokenizer.TokenSource.NestedStrings:
          return;
        default:
          Tokenizer.StreamTokenReader streamTokenReader = this._inTokenReader as Tokenizer.StreamTokenReader;
          if (streamTokenReader == null)
            return;
          stream = streamTokenReader._in.BaseStream;
          string s = new string(' ', streamTokenReader.NumCharEncountered);
          stream.Position = (long) streamTokenReader._in.CurrentEncoding.GetByteCount(s);
          break;
      }
      this._inTokenReader = (Tokenizer.ITokenReader) new Tokenizer.StreamTokenReader(new StreamReader(stream, encoding));
      this._inTokenSource = Tokenizer.TokenSource.Other;
    }

    internal void GetTokens(TokenizerStream stream, int maxNum, bool endAfterKet)
    {
      while (maxNum == -1 || stream.GetTokenCount() < maxNum)
      {
        int num1 = 0;
        bool flag1 = false;
        bool flag2 = false;
        Tokenizer.StringMaker stringMaker1 = this._maker;
        stringMaker1._outStringBuilder = (StringBuilder) null;
        stringMaker1._outIndex = 0;
        int num2;
        while (true)
        {
          if (this._inSavedCharacter != -1)
          {
            num2 = this._inSavedCharacter;
            this._inSavedCharacter = -1;
          }
          else
          {
            switch (this._inTokenSource)
            {
              case Tokenizer.TokenSource.UnicodeByteArray:
                if (this._inIndex + 1 < this._inSize)
                {
                  num2 = ((int) this._inBytes[this._inIndex + 1] << 8) + (int) this._inBytes[this._inIndex];
                  this._inIndex = this._inIndex + 2;
                  break;
                }
                goto label_6;
              case Tokenizer.TokenSource.UTF8ByteArray:
                if (this._inIndex < this._inSize)
                {
                  byte[] numArray1 = this._inBytes;
                  int num3 = this._inIndex;
                  this._inIndex = num3 + 1;
                  int index1 = num3;
                  num2 = (int) numArray1[index1];
                  if ((num2 & 128) != 0)
                  {
                    switch ((num2 & 240) >> 4)
                    {
                      case 8:
                      case 9:
                      case 10:
                      case 11:
                        goto label_12;
                      case 12:
                      case 13:
                        num2 &= 31;
                        num1 = 2;
                        break;
                      case 14:
                        num2 &= 15;
                        num1 = 3;
                        break;
                      case 15:
                        goto label_15;
                    }
                    if (this._inIndex < this._inSize)
                    {
                      byte[] numArray2 = this._inBytes;
                      int num4 = this._inIndex;
                      this._inIndex = num4 + 1;
                      int index2 = num4;
                      byte num5 = numArray2[index2];
                      if (((int) num5 & 192) == 128)
                      {
                        num2 = num2 << 6 | (int) num5 & 63;
                        if (num1 != 2)
                        {
                          if (this._inIndex < this._inSize)
                          {
                            byte[] numArray3 = this._inBytes;
                            int num6 = this._inIndex;
                            this._inIndex = num6 + 1;
                            int index3 = num6;
                            byte num7 = numArray3[index3];
                            if (((int) num7 & 192) == 128)
                            {
                              num2 = num2 << 6 | (int) num7 & 63;
                              break;
                            }
                            goto label_24;
                          }
                          else
                            goto label_22;
                        }
                        else
                          break;
                      }
                      else
                        goto label_19;
                    }
                    else
                      goto label_17;
                  }
                  else
                    break;
                }
                else
                  goto label_9;
              case Tokenizer.TokenSource.ASCIIByteArray:
                if (this._inIndex < this._inSize)
                {
                  byte[] numArray = this._inBytes;
                  int num3 = this._inIndex;
                  this._inIndex = num3 + 1;
                  int index = num3;
                  num2 = (int) numArray[index];
                  break;
                }
                goto label_27;
              case Tokenizer.TokenSource.CharArray:
                if (this._inIndex < this._inSize)
                {
                  char[] chArray = this._inChars;
                  int num3 = this._inIndex;
                  this._inIndex = num3 + 1;
                  int index = num3;
                  num2 = (int) chArray[index];
                  break;
                }
                goto label_30;
              case Tokenizer.TokenSource.String:
                if (this._inIndex < this._inSize)
                {
                  string str = this._inString;
                  int num3 = this._inIndex;
                  this._inIndex = num3 + 1;
                  int index = num3;
                  num2 = (int) str[index];
                  break;
                }
                goto label_33;
              case Tokenizer.TokenSource.NestedStrings:
                if (this._inNestedSize != 0)
                {
                  if (this._inNestedIndex < this._inNestedSize)
                  {
                    string str = this._inNestedString;
                    int num3 = this._inNestedIndex;
                    this._inNestedIndex = num3 + 1;
                    int index = num3;
                    num2 = (int) str[index];
                    break;
                  }
                  this._inNestedSize = 0;
                }
                if (this._inIndex < this._inSize)
                {
                  string str = this._inString;
                  int num3 = this._inIndex;
                  this._inIndex = num3 + 1;
                  int index1 = num3;
                  num2 = (int) str[index1];
                  if (num2 == 123)
                  {
                    for (int index2 = 0; index2 < this._searchStrings.Length; ++index2)
                    {
                      if (string.Compare(this._searchStrings[index2], 0, this._inString, this._inIndex - 1, this._searchStrings[index2].Length, StringComparison.Ordinal) == 0)
                      {
                        this._inNestedString = this._replaceStrings[index2];
                        this._inNestedSize = this._inNestedString.Length;
                        this._inNestedIndex = 1;
                        num2 = (int) this._inNestedString[0];
                        this._inIndex = this._inIndex + (this._searchStrings[index2].Length - 1);
                        break;
                      }
                    }
                    break;
                  }
                  break;
                }
                goto label_40;
              default:
                num2 = this._inTokenReader.Read();
                if (num2 != -1)
                  break;
                goto label_48;
            }
          }
          if (!flag1)
          {
            switch (num2)
            {
              case 45:
                if (this._inProcessingTag == 0)
                  break;
                goto label_63;
              case 47:
                if (this._inProcessingTag == 0)
                  break;
                goto label_57;
              case 60:
                goto label_52;
              case 61:
                goto label_55;
              case 62:
                goto label_53;
              case 63:
                if (this._inProcessingTag == 0)
                  break;
                goto label_59;
              case 9:
              case 13:
              case 32:
                continue;
              case 10:
                this.LineNo = this.LineNo + 1;
                continue;
              case 33:
                if (this._inProcessingTag == 0)
                  break;
                goto label_61;
              case 34:
                flag1 = true;
                flag2 = true;
                continue;
            }
          }
          else
          {
            switch (num2)
            {
              case 34:
                if (!flag2)
                  break;
                goto label_72;
              case 47:
              case 61:
              case 62:
                if (flag2 || this._inProcessingTag == 0)
                  break;
                goto label_70;
              case 60:
                if (flag2)
                  break;
                goto label_68;
              case 9:
              case 13:
              case 32:
                if (flag2)
                  break;
                goto label_74;
              case 10:
                this.LineNo = this.LineNo + 1;
                if (flag2)
                  break;
                goto label_76;
            }
          }
          flag1 = true;
          if (stringMaker1._outIndex < 512)
          {
            char[] chArray = stringMaker1._outChars;
            Tokenizer.StringMaker stringMaker2 = stringMaker1;
            int num3 = stringMaker2._outIndex;
            int num4 = num3 + 1;
            stringMaker2._outIndex = num4;
            int index = num3;
            int num5 = (int) (ushort) num2;
            chArray[index] = (char) num5;
          }
          else
          {
            if (stringMaker1._outStringBuilder == null)
              stringMaker1._outStringBuilder = new StringBuilder();
            stringMaker1._outStringBuilder.Append(stringMaker1._outChars, 0, 512);
            stringMaker1._outChars[0] = (char) num2;
            stringMaker1._outIndex = 1;
          }
        }
label_6:
        stream.AddToken((short) -1);
        break;
label_9:
        stream.AddToken((short) -1);
        break;
label_12:
        throw new XmlSyntaxException(this.LineNo);
label_15:
        throw new XmlSyntaxException(this.LineNo);
label_17:
        throw new XmlSyntaxException(this.LineNo, Environment.GetResourceString("XMLSyntax_UnexpectedEndOfFile"));
label_19:
        throw new XmlSyntaxException(this.LineNo);
label_22:
        throw new XmlSyntaxException(this.LineNo, Environment.GetResourceString("XMLSyntax_UnexpectedEndOfFile"));
label_24:
        throw new XmlSyntaxException(this.LineNo);
label_27:
        stream.AddToken((short) -1);
        break;
label_30:
        stream.AddToken((short) -1);
        break;
label_33:
        stream.AddToken((short) -1);
        break;
label_40:
        stream.AddToken((short) -1);
        break;
label_48:
        stream.AddToken((short) -1);
        break;
label_52:
        this._inProcessingTag = this._inProcessingTag + 1;
        stream.AddToken((short) 0);
        continue;
label_53:
        this._inProcessingTag = this._inProcessingTag - 1;
        stream.AddToken((short) 1);
        if (endAfterKet)
          break;
        continue;
label_55:
        stream.AddToken((short) 4);
        continue;
label_57:
        stream.AddToken((short) 2);
        continue;
label_59:
        stream.AddToken((short) 5);
        continue;
label_61:
        stream.AddToken((short) 6);
        continue;
label_63:
        stream.AddToken((short) 7);
        continue;
label_68:
        this._inSavedCharacter = num2;
        stream.AddToken((short) 3);
        stream.AddString(this.GetStringToken());
        continue;
label_70:
        this._inSavedCharacter = num2;
        stream.AddToken((short) 3);
        stream.AddString(this.GetStringToken());
        continue;
label_72:
        stream.AddToken((short) 3);
        stream.AddString(this.GetStringToken());
        continue;
label_74:
        stream.AddToken((short) 3);
        stream.AddString(this.GetStringToken());
        continue;
label_76:
        stream.AddToken((short) 3);
        stream.AddString(this.GetStringToken());
      }
    }

    private string GetStringToken()
    {
      return this._maker.MakeString();
    }

    private enum TokenSource
    {
      UnicodeByteArray,
      UTF8ByteArray,
      ASCIIByteArray,
      CharArray,
      String,
      NestedStrings,
      Other,
    }

    internal enum ByteTokenEncoding
    {
      UnicodeTokens,
      UTF8Tokens,
      ByteTokens,
    }

    [Serializable]
    internal sealed class StringMaker
    {
      private string[] aStrings;
      private uint cStringsMax;
      private uint cStringsUsed;
      public StringBuilder _outStringBuilder;
      public char[] _outChars;
      public int _outIndex;
      public const int outMaxSize = 512;

      public StringMaker()
      {
        this.cStringsMax = 2048U;
        this.cStringsUsed = 0U;
        this.aStrings = new string[(int) this.cStringsMax];
        this._outChars = new char[512];
      }

      private static uint HashString(string str)
      {
        uint num = 0;
        int length = str.Length;
        for (int index = 0; index < length; ++index)
          num = num << 3 ^ (uint) str[index] ^ num >> 29;
        return num;
      }

      private static uint HashCharArray(char[] a, int l)
      {
        uint num = 0;
        for (int index = 0; index < l; ++index)
          num = num << 3 ^ (uint) a[index] ^ num >> 29;
        return num;
      }

      private bool CompareStringAndChars(string str, char[] a, int l)
      {
        if (str.Length != l)
          return false;
        for (int index = 0; index < l; ++index)
        {
          if ((int) a[index] != (int) str[index])
            return false;
        }
        return true;
      }

      public string MakeString()
      {
        char[] a = this._outChars;
        int num1 = this._outIndex;
        if (this._outStringBuilder != null)
        {
          this._outStringBuilder.Append(this._outChars, 0, this._outIndex);
          return this._outStringBuilder.ToString();
        }
        if (this.cStringsUsed > this.cStringsMax / 4U * 3U)
        {
          uint num2 = this.cStringsMax * 2U;
          string[] strArray = new string[(int) num2];
          for (int index = 0; (long) index < (long) this.cStringsMax; ++index)
          {
            if (this.aStrings[index] != null)
            {
              uint num3 = Tokenizer.StringMaker.HashString(this.aStrings[index]) % num2;
              while (strArray[(int) num3] != null)
              {
                if (++num3 >= num2)
                  num3 = 0U;
              }
              strArray[(int) num3] = this.aStrings[index];
            }
          }
          this.cStringsMax = num2;
          this.aStrings = strArray;
        }
        uint num4 = Tokenizer.StringMaker.HashCharArray(a, num1) % this.cStringsMax;
        string str1;
        while ((str1 = this.aStrings[(int) num4]) != null)
        {
          if (this.CompareStringAndChars(str1, a, num1))
            return str1;
          if (++num4 >= this.cStringsMax)
            num4 = 0U;
        }
        string str2 = new string(a, 0, num1);
        this.aStrings[(int) num4] = str2;
        this.cStringsUsed = this.cStringsUsed + 1U;
        return str2;
      }
    }

    internal interface ITokenReader
    {
      int Read();
    }

    internal class StreamTokenReader : Tokenizer.ITokenReader
    {
      internal StreamReader _in;
      internal int _numCharRead;

      internal int NumCharEncountered
      {
        get
        {
          return this._numCharRead;
        }
      }

      internal StreamTokenReader(StreamReader input)
      {
        this._in = input;
        this._numCharRead = 0;
      }

      public virtual int Read()
      {
        int num1 = this._in.Read();
        int num2 = -1;
        if (num1 == num2)
          return num1;
        this._numCharRead = this._numCharRead + 1;
        return num1;
      }
    }
  }
}
