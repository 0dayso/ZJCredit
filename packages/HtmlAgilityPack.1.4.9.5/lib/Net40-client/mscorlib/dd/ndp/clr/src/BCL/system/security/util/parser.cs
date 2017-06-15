// Decompiled with JetBrains decompiler
// Type: System.Security.Util.Parser
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.IO;
using System.Text;

namespace System.Security.Util
{
  internal sealed class Parser
  {
    private SecurityDocument _doc;
    private Tokenizer _t;
    private const short c_flag = 16384;
    private const short c_elementtag = 16640;
    private const short c_attributetag = 16896;
    private const short c_texttag = 17152;
    private const short c_additionaltexttag = 25344;
    private const short c_childrentag = 17408;
    private const short c_wastedstringtag = 20480;

    private Parser(Tokenizer t)
    {
      this._t = t;
      this._doc = (SecurityDocument) null;
      try
      {
        this.ParseContents();
      }
      finally
      {
        this._t.Recycle();
      }
    }

    internal Parser(string input)
      : this(new Tokenizer(input))
    {
    }

    internal Parser(string input, string[] searchStrings, string[] replaceStrings)
      : this(new Tokenizer(input, searchStrings, replaceStrings))
    {
    }

    internal Parser(byte[] array, Tokenizer.ByteTokenEncoding encoding)
      : this(new Tokenizer(array, encoding, 0))
    {
    }

    internal Parser(byte[] array, Tokenizer.ByteTokenEncoding encoding, int startIndex)
      : this(new Tokenizer(array, encoding, startIndex))
    {
    }

    internal Parser(StreamReader input)
      : this(new Tokenizer(input))
    {
    }

    internal Parser(char[] array)
      : this(new Tokenizer(array))
    {
    }

    internal SecurityElement GetTopElement()
    {
      return this._doc.GetRootElement();
    }

    private void GetRequiredSizes(TokenizerStream stream, ref int index)
    {
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = false;
      bool flag4 = false;
      int num1 = 1;
      SecurityElementType securityElementType = SecurityElementType.Regular;
      string str = (string) null;
      bool flag5 = false;
      bool flag6 = false;
      int num2 = 0;
      do
      {
        short nextToken;
        for (nextToken = stream.GetNextToken(); (int) nextToken != -1; nextToken = stream.GetNextToken())
        {
          switch ((int) nextToken & (int) byte.MaxValue)
          {
            case 0:
              flag4 = true;
              flag6 = false;
              nextToken = stream.GetNextToken();
              switch (nextToken)
              {
                case 2:
                  stream.TagLastToken((short) 17408);
                  while (true)
                  {
                    nextToken = stream.GetNextToken();
                    switch (nextToken)
                    {
                      case 3:
                        stream.ThrowAwayNextString();
                        stream.TagLastToken((short) 20480);
                        continue;
                      case -1:
                        goto label_18;
                      case 1:
                        goto label_20;
                      default:
                        goto label_19;
                    }
                  }
label_18:
                  throw new XmlSyntaxException(this._t.LineNo, Environment.GetResourceString("XMLSyntax_UnexpectedEndOfFile"));
label_19:
                  throw new XmlSyntaxException(this._t.LineNo, Environment.GetResourceString("XMLSyntax_ExpectedCloseBracket"));
label_20:
                  flag4 = false;
                  ++index;
                  flag6 = false;
                  --num1;
                  flag1 = true;
                  break;
                case 3:
                  flag3 = true;
                  stream.TagLastToken((short) 16640);
                  index += SecurityDocument.EncodedStringSize(stream.GetNextString()) + 1;
                  if (securityElementType != SecurityElementType.Regular)
                    throw new XmlSyntaxException(this._t.LineNo);
                  flag1 = true;
                  ++num1;
                  break;
                case 6:
                  num2 = 1;
                  do
                  {
                    nextToken = stream.GetNextToken();
                    switch (nextToken)
                    {
                      case 0:
                        ++num2;
                        break;
                      case 1:
                        --num2;
                        break;
                      case 3:
                        stream.ThrowAwayNextString();
                        stream.TagLastToken((short) 20480);
                        break;
                    }
                  }
                  while (num2 > 0);
                  flag4 = false;
                  flag6 = false;
                  flag1 = true;
                  break;
                case 5:
                  nextToken = stream.GetNextToken();
                  if ((int) nextToken != 3)
                    throw new XmlSyntaxException(this._t.LineNo);
                  flag3 = true;
                  securityElementType = SecurityElementType.Format;
                  stream.TagLastToken((short) 16640);
                  index += SecurityDocument.EncodedStringSize(stream.GetNextString()) + 1;
                  num2 = 1;
                  ++num1;
                  flag1 = true;
                  break;
                default:
                  throw new XmlSyntaxException(this._t.LineNo, Environment.GetResourceString("XMLSyntax_ExpectedSlashOrString"));
              }
            case 1:
              if (!flag4)
                throw new XmlSyntaxException(this._t.LineNo, Environment.GetResourceString("XMLSyntax_UnexpectedCloseBracket"));
              flag4 = false;
              continue;
            case 2:
              nextToken = stream.GetNextToken();
              if ((int) nextToken != 1)
                throw new XmlSyntaxException(this._t.LineNo, Environment.GetResourceString("XMLSyntax_ExpectedCloseBracket"));
              stream.TagLastToken((short) 17408);
              ++index;
              --num1;
              flag6 = false;
              flag1 = true;
              break;
            case 3:
              if (flag4)
              {
                if (securityElementType == SecurityElementType.Comment)
                {
                  stream.ThrowAwayNextString();
                  stream.TagLastToken((short) 20480);
                  break;
                }
                if (str == null)
                {
                  str = stream.GetNextString();
                  break;
                }
                if (!flag5)
                  throw new XmlSyntaxException(this._t.LineNo);
                stream.TagLastToken((short) 16896);
                index += SecurityDocument.EncodedStringSize(str) + SecurityDocument.EncodedStringSize(stream.GetNextString()) + 1;
                str = (string) null;
                flag5 = false;
                break;
              }
              if (flag6)
              {
                stream.TagLastToken((short) 25344);
                index += SecurityDocument.EncodedStringSize(stream.GetNextString()) + SecurityDocument.EncodedStringSize(" ");
                break;
              }
              stream.TagLastToken((short) 17152);
              index += SecurityDocument.EncodedStringSize(stream.GetNextString()) + 1;
              flag6 = true;
              break;
            case 4:
              flag5 = true;
              break;
            case 5:
              if (!flag4 || securityElementType != SecurityElementType.Format || num2 != 1)
                throw new XmlSyntaxException(this._t.LineNo);
              nextToken = stream.GetNextToken();
              if ((int) nextToken != 1)
                throw new XmlSyntaxException(this._t.LineNo, Environment.GetResourceString("XMLSyntax_ExpectedCloseBracket"));
              stream.TagLastToken((short) 17408);
              ++index;
              --num1;
              flag6 = false;
              flag1 = true;
              break;
            default:
              throw new XmlSyntaxException(this._t.LineNo);
          }
          if (flag1)
          {
            flag1 = false;
            flag2 = false;
            break;
          }
          flag2 = true;
        }
        if (flag2)
        {
          ++index;
          --num1;
          flag6 = false;
        }
        else if ((int) nextToken == -1 && (num1 != 1 || !flag3))
          throw new XmlSyntaxException(this._t.LineNo, Environment.GetResourceString("XMLSyntax_UnexpectedEndOfFile"));
      }
      while (num1 > 1);
    }

    private int DetermineFormat(TokenizerStream stream)
    {
      if ((int) stream.GetNextToken() != 0 || (int) stream.GetNextToken() != 5)
        return 2;
      this._t.GetTokens(stream, -1, true);
      stream.GoToPosition(2);
      bool flag1 = false;
      bool flag2 = false;
      for (short nextToken = stream.GetNextToken(); (int) nextToken != -1 && (int) nextToken != 1; nextToken = stream.GetNextToken())
      {
        if ((int) nextToken != 3)
        {
          if ((int) nextToken != 4)
            throw new XmlSyntaxException(this._t.LineNo, Environment.GetResourceString("XMLSyntax_UnexpectedEndOfFile"));
          flag1 = true;
        }
        else
        {
          if (flag1 & flag2)
          {
            this._t.ChangeFormat(Encoding.GetEncoding(stream.GetNextString()));
            return 0;
          }
          if (!flag1)
          {
            if (string.Compare(stream.GetNextString(), "encoding", StringComparison.Ordinal) == 0)
              flag2 = true;
          }
          else
          {
            flag1 = false;
            flag2 = false;
            stream.ThrowAwayNextString();
          }
        }
      }
      return 0;
    }

    private void ParseContents()
    {
      TokenizerStream stream = new TokenizerStream();
      this._t.GetTokens(stream, 2, false);
      stream.Reset();
      int format = this.DetermineFormat(stream);
      stream.GoToPosition(format);
      this._t.GetTokens(stream, -1, false);
      stream.Reset();
      int index = 0;
      this.GetRequiredSizes(stream, ref index);
      this._doc = new SecurityDocument(index);
      int position = 0;
      stream.Reset();
      for (short nextFullToken = stream.GetNextFullToken(); (int) nextFullToken != -1; nextFullToken = stream.GetNextFullToken())
      {
        if (((int) nextFullToken & 16384) == 16384)
        {
          switch ((short) ((int) nextFullToken & 65280))
          {
            case 17408:
              this._doc.AddToken((byte) 4, ref position);
              continue;
            case 20480:
              stream.ThrowAwayNextString();
              continue;
            case 25344:
              this._doc.AppendString(" ", ref position);
              this._doc.AppendString(stream.GetNextString(), ref position);
              continue;
            case 16640:
              this._doc.AddToken((byte) 1, ref position);
              this._doc.AddString(stream.GetNextString(), ref position);
              continue;
            case 16896:
              this._doc.AddToken((byte) 2, ref position);
              this._doc.AddString(stream.GetNextString(), ref position);
              this._doc.AddString(stream.GetNextString(), ref position);
              continue;
            case 17152:
              this._doc.AddToken((byte) 3, ref position);
              this._doc.AddString(stream.GetNextString(), ref position);
              continue;
            default:
              throw new XmlSyntaxException();
          }
        }
      }
    }
  }
}
