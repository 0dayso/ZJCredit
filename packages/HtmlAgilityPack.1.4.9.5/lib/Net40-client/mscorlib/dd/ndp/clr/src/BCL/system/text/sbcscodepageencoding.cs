// Decompiled with JetBrains decompiler
// Type: System.Text.SBCSCodePageEncoding
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System.Text
{
  [Serializable]
  internal class SBCSCodePageEncoding : BaseCodePageEncoding, ISerializable
  {
    [SecurityCritical]
    [NonSerialized]
    private unsafe char* mapBytesToUnicode = (char*) null;
    [SecurityCritical]
    [NonSerialized]
    private unsafe byte* mapUnicodeToBytes = (byte*) null;
    [SecurityCritical]
    [NonSerialized]
    private unsafe int* mapCodePageCached = (int*) null;
    private const char UNKNOWN_CHAR = '�';
    [NonSerialized]
    private byte byteUnknown;
    [NonSerialized]
    private char charUnknown;
    private static object s_InternalSyncObject;

    private static object InternalSyncObject
    {
      get
      {
        if (SBCSCodePageEncoding.s_InternalSyncObject == null)
        {
          object obj = new object();
          Interlocked.CompareExchange<object>(ref SBCSCodePageEncoding.s_InternalSyncObject, obj, (object) null);
        }
        return SBCSCodePageEncoding.s_InternalSyncObject;
      }
    }

    public override bool IsSingleByte
    {
      get
      {
        return true;
      }
    }

    [SecurityCritical]
    public SBCSCodePageEncoding(int codePage)
    {
      int num = codePage;
      // ISSUE: explicit constructor call
      this.\u002Ector(num, num);
    }

    [SecurityCritical]
    internal unsafe SBCSCodePageEncoding(int codePage, int dataCodePage)
      : base(codePage, dataCodePage)
    {
    }

    [SecurityCritical]
    internal unsafe SBCSCodePageEncoding(SerializationInfo info, StreamingContext context)
      : base(0)
    {
      throw new ArgumentNullException("this");
    }

    [SecurityCritical]
    protected override unsafe void LoadManagedCodePage()
    {
      if ((int) this.pCodePage->ByteCount != 1)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_NoCodepageData", (object) this.CodePage));
      this.byteUnknown = (byte) this.pCodePage->ByteReplace;
      this.charUnknown = this.pCodePage->UnicodeReplace;
      byte* sharedMemory = this.GetSharedMemory(66052 + this.iExtraBytes);
      this.mapBytesToUnicode = (char*) sharedMemory;
      this.mapUnicodeToBytes = sharedMemory + 512;
      this.mapCodePageCached = (int*) (sharedMemory + 512 + 65536 + this.iExtraBytes);
      if (*this.mapCodePageCached != 0)
      {
        if (*this.mapCodePageCached != this.dataTableCodePage)
          throw new OutOfMemoryException(Environment.GetResourceString("Arg_OutOfMemoryException"));
      }
      else
      {
        char* chPtr = (char*) &this.pCodePage->FirstDataWord;
        for (int index = 0; index < 256; ++index)
        {
          if ((int) chPtr[index] != 0 || index == 0)
          {
            this.mapBytesToUnicode[index] = chPtr[index];
            if ((int) chPtr[index] != 65533)
              this.mapUnicodeToBytes[(int) chPtr[index]] = (byte) index;
          }
          else
            this.mapBytesToUnicode[index] = '�';
        }
        *this.mapCodePageCached = this.dataTableCodePage;
      }
    }

    [SecurityCritical]
    protected override unsafe void ReadBestFitTable()
    {
      lock (SBCSCodePageEncoding.InternalSyncObject)
      {
        if (this.arrayUnicodeBestFit != null)
          return;
        byte* local_2_1 = (byte*) ((IntPtr) &this.pCodePage->FirstDataWord + 512);
        char[] local_3 = new char[256];
        for (int local_8 = 0; local_8 < 256; ++local_8)
          local_3[local_8] = this.mapBytesToUnicode[local_8];
        byte* local_2_2;
        ushort local_4;
        for (; (int) (local_4 = *(ushort*) local_2_1) != 0; local_2_1 = local_2_2 + 2)
        {
          local_2_2 = local_2_1 + 2;
          local_3[(int) local_4] = (char) *(ushort*) local_2_2;
        }
        this.arrayBytesBestFit = local_3;
        byte* local_2_3 = local_2_1 + 2;
        byte* local_5 = local_2_3;
        int local_6 = 0;
        int local_7 = (int) *(ushort*) local_2_3;
        byte* local_2_4 = local_2_3 + 2;
        while (local_7 < 65536)
        {
          byte local_9 = *local_2_4;
          ++local_2_4;
          if ((int) local_9 == 1)
          {
            local_7 = (int) *(ushort*) local_2_4;
            local_2_4 += 2;
          }
          else if ((int) local_9 < 32 && (int) local_9 > 0 && (int) local_9 != 30)
          {
            local_7 += (int) local_9;
          }
          else
          {
            if ((int) local_9 > 0)
              ++local_6;
            ++local_7;
          }
        }
        char[] local_3_1 = new char[local_6 * 2];
        byte* local_2_5 = local_5;
        int local_7_1 = (int) *(ushort*) local_2_5;
        byte* local_2_6 = local_2_5 + 2;
        int local_6_1 = 0;
        while (local_7_1 < 65536)
        {
          byte local_10 = *local_2_6;
          ++local_2_6;
          if ((int) local_10 == 1)
          {
            local_7_1 = (int) *(ushort*) local_2_6;
            local_2_6 += 2;
          }
          else if ((int) local_10 < 32 && (int) local_10 > 0 && (int) local_10 != 30)
          {
            local_7_1 += (int) local_10;
          }
          else
          {
            if ((int) local_10 == 30)
            {
              local_10 = *local_2_6;
              ++local_2_6;
            }
            if ((int) local_10 > 0)
            {
              char[] temp_117 = local_3_1;
              int temp_118 = local_6_1;
              int temp_119 = 1;
              int local_6_2 = temp_118 + temp_119;
              int temp_122 = (int) (ushort) local_7_1;
              temp_117[temp_118] = (char) temp_122;
              char[] temp_123 = local_3_1;
              int temp_124 = local_6_2;
              int temp_125 = 1;
              local_6_1 = temp_124 + temp_125;
              int temp_134 = (int) this.mapBytesToUnicode[local_10];
              temp_123[temp_124] = (char) temp_134;
            }
            ++local_7_1;
          }
        }
        this.arrayUnicodeBestFit = local_3_1;
      }
    }

    [SecurityCritical]
    internal override unsafe int GetByteCount(char* chars, int count, EncoderNLS encoder)
    {
      this.CheckMemorySection();
      char ch1 = char.MinValue;
      EncoderReplacementFallback replacementFallback;
      if (encoder != null)
      {
        ch1 = encoder.charLeftOver;
        replacementFallback = encoder.Fallback as EncoderReplacementFallback;
      }
      else
        replacementFallback = this.EncoderFallback as EncoderReplacementFallback;
      if (replacementFallback != null && replacementFallback.MaxCharCount == 1)
      {
        if ((int) ch1 > 0)
          ++count;
        return count;
      }
      EncoderFallbackBuffer encoderFallbackBuffer = (EncoderFallbackBuffer) null;
      int num = 0;
      char* charEnd = chars + count;
      if ((int) ch1 > 0)
      {
        encoderFallbackBuffer = encoder.FallbackBuffer;
        encoderFallbackBuffer.InternalInitialize(chars, charEnd, encoder, false);
        encoderFallbackBuffer.InternalFallback(ch1, ref chars);
      }
      char ch2;
      while ((int) (ch2 = encoderFallbackBuffer == null ? char.MinValue : encoderFallbackBuffer.InternalGetNextChar()) != 0 || chars < charEnd)
      {
        if ((int) ch2 == 0)
        {
          ch2 = *chars;
          chars += 2;
        }
        if ((int) this.mapUnicodeToBytes[(int) ch2] == 0 && (int) ch2 != 0)
        {
          if (encoderFallbackBuffer == null)
          {
            encoderFallbackBuffer = encoder != null ? encoder.FallbackBuffer : this.encoderFallback.CreateFallbackBuffer();
            encoderFallbackBuffer.InternalInitialize(charEnd - count, charEnd, encoder, false);
          }
          encoderFallbackBuffer.InternalFallback(ch2, ref chars);
        }
        else
          ++num;
      }
      return num;
    }

    [SecurityCritical]
    internal override unsafe int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, EncoderNLS encoder)
    {
      this.CheckMemorySection();
      char ch1 = char.MinValue;
      EncoderReplacementFallback replacementFallback;
      if (encoder != null)
      {
        ch1 = encoder.charLeftOver;
        replacementFallback = encoder.Fallback as EncoderReplacementFallback;
      }
      else
        replacementFallback = this.EncoderFallback as EncoderReplacementFallback;
      char* charEnd = chars + charCount;
      byte* numPtr1 = bytes;
      char* chPtr = chars;
      if (replacementFallback != null && replacementFallback.MaxCharCount == 1)
      {
        byte num1 = this.mapUnicodeToBytes[(int) replacementFallback.DefaultString[0]];
        if ((int) num1 != 0)
        {
          if ((int) ch1 > 0)
          {
            if (byteCount == 0)
              this.ThrowBytesOverflow(encoder, true);
            *bytes++ = num1;
            --byteCount;
          }
          if (byteCount < charCount)
          {
            this.ThrowBytesOverflow(encoder, byteCount < 1);
            charEnd = chars + byteCount;
          }
          while (chars < charEnd)
          {
            char ch2 = *chars;
            chars += 2;
            byte num2 = this.mapUnicodeToBytes[(int) ch2];
            *bytes = (int) num2 != 0 || (int) ch2 == 0 ? num2 : num1;
            ++bytes;
          }
          if (encoder != null)
          {
            encoder.charLeftOver = char.MinValue;
            encoder.m_charsUsed = (int) (chars - chPtr);
          }
          return (int) (bytes - numPtr1);
        }
      }
      EncoderFallbackBuffer encoderFallbackBuffer = (EncoderFallbackBuffer) null;
      byte* numPtr2 = bytes + byteCount;
      if ((int) ch1 > 0)
      {
        encoderFallbackBuffer = encoder.FallbackBuffer;
        encoderFallbackBuffer.InternalInitialize(chars, charEnd, encoder, true);
        encoderFallbackBuffer.InternalFallback(ch1, ref chars);
        if ((long) encoderFallbackBuffer.Remaining > numPtr2 - bytes)
          this.ThrowBytesOverflow(encoder, true);
      }
      char ch3;
      while ((int) (ch3 = encoderFallbackBuffer == null ? char.MinValue : encoderFallbackBuffer.InternalGetNextChar()) != 0 || chars < charEnd)
      {
        if ((int) ch3 == 0)
        {
          ch3 = *chars;
          chars += 2;
        }
        byte num = this.mapUnicodeToBytes[(int) ch3];
        if ((int) num == 0 && (int) ch3 != 0)
        {
          if (encoderFallbackBuffer == null)
          {
            encoderFallbackBuffer = encoder != null ? encoder.FallbackBuffer : this.encoderFallback.CreateFallbackBuffer();
            encoderFallbackBuffer.InternalInitialize(charEnd - charCount, charEnd, encoder, true);
          }
          encoderFallbackBuffer.InternalFallback(ch3, ref chars);
          if ((long) encoderFallbackBuffer.Remaining > numPtr2 - bytes)
          {
            chars -= 2;
            encoderFallbackBuffer.InternalReset();
            this.ThrowBytesOverflow(encoder, chars == chPtr);
            break;
          }
        }
        else
        {
          if (bytes >= numPtr2)
          {
            if (encoderFallbackBuffer == null || !encoderFallbackBuffer.bFallingBack)
              chars -= 2;
            this.ThrowBytesOverflow(encoder, chars == chPtr);
            break;
          }
          *bytes = num;
          ++bytes;
        }
      }
      if (encoder != null)
      {
        if (encoderFallbackBuffer != null && !encoderFallbackBuffer.bUsedEncoder)
          encoder.charLeftOver = char.MinValue;
        encoder.m_charsUsed = (int) (chars - chPtr);
      }
      return (int) (bytes - numPtr1);
    }

    [SecurityCritical]
    internal override unsafe int GetCharCount(byte* bytes, int count, DecoderNLS decoder)
    {
      this.CheckMemorySection();
      DecoderReplacementFallback replacementFallback;
      bool microsoftBestFitFallback;
      if (decoder == null)
      {
        replacementFallback = this.DecoderFallback as DecoderReplacementFallback;
        microsoftBestFitFallback = this.DecoderFallback.IsMicrosoftBestFitFallback;
      }
      else
      {
        replacementFallback = decoder.Fallback as DecoderReplacementFallback;
        microsoftBestFitFallback = decoder.Fallback.IsMicrosoftBestFitFallback;
      }
      if (microsoftBestFitFallback || replacementFallback != null && replacementFallback.MaxCharCount == 1)
        return count;
      DecoderFallbackBuffer decoderFallbackBuffer = (DecoderFallbackBuffer) null;
      int num1 = count;
      byte[] bytes1 = new byte[1];
      byte* numPtr = bytes + count;
      while (bytes < numPtr)
      {
        int num2 = (int) this.mapBytesToUnicode[*bytes];
        ++bytes;
        int num3 = 65533;
        if (num2 == num3)
        {
          if (decoderFallbackBuffer == null)
          {
            decoderFallbackBuffer = decoder != null ? decoder.FallbackBuffer : this.DecoderFallback.CreateFallbackBuffer();
            decoderFallbackBuffer.InternalInitialize(numPtr - count, (char*) null);
          }
          bytes1[0] = *(bytes - 1);
          num1 = num1 - 1 + decoderFallbackBuffer.InternalFallback(bytes1, bytes);
        }
      }
      return num1;
    }

    [SecurityCritical]
    internal override unsafe int GetChars(byte* bytes, int byteCount, char* chars, int charCount, DecoderNLS decoder)
    {
      this.CheckMemorySection();
      byte* numPtr1 = bytes + byteCount;
      byte* numPtr2 = bytes;
      char* chPtr = chars;
      DecoderReplacementFallback replacementFallback;
      bool microsoftBestFitFallback;
      if (decoder == null)
      {
        replacementFallback = this.DecoderFallback as DecoderReplacementFallback;
        microsoftBestFitFallback = this.DecoderFallback.IsMicrosoftBestFitFallback;
      }
      else
      {
        replacementFallback = decoder.Fallback as DecoderReplacementFallback;
        microsoftBestFitFallback = decoder.Fallback.IsMicrosoftBestFitFallback;
      }
      if (microsoftBestFitFallback || replacementFallback != null && replacementFallback.MaxCharCount == 1)
      {
        char ch1 = replacementFallback != null ? replacementFallback.DefaultString[0] : '?';
        if (charCount < byteCount)
        {
          this.ThrowCharsOverflow(decoder, charCount < 1);
          numPtr1 = bytes + charCount;
        }
        while (bytes < numPtr1)
        {
          char ch2;
          if (microsoftBestFitFallback)
          {
            if (this.arrayBytesBestFit == null)
              this.ReadBestFitTable();
            ch2 = this.arrayBytesBestFit[(int) *bytes];
          }
          else
            ch2 = this.mapBytesToUnicode[*bytes];
          ++bytes;
          *chars = (int) ch2 != 65533 ? ch2 : ch1;
          chars += 2;
        }
        if (decoder != null)
          decoder.m_bytesUsed = (int) (bytes - numPtr2);
        return (int) (chars - chPtr);
      }
      DecoderFallbackBuffer decoderFallbackBuffer = (DecoderFallbackBuffer) null;
      byte[] bytes1 = new byte[1];
      char* charEnd = chars + charCount;
      while (bytes < numPtr1)
      {
        char ch = this.mapBytesToUnicode[*bytes];
        ++bytes;
        if ((int) ch == 65533)
        {
          if (decoderFallbackBuffer == null)
          {
            decoderFallbackBuffer = decoder != null ? decoder.FallbackBuffer : this.DecoderFallback.CreateFallbackBuffer();
            decoderFallbackBuffer.InternalInitialize(numPtr1 - byteCount, charEnd);
          }
          bytes1[0] = *(bytes - 1);
          if (!decoderFallbackBuffer.InternalFallback(bytes1, bytes, ref chars))
          {
            --bytes;
            decoderFallbackBuffer.InternalReset();
            this.ThrowCharsOverflow(decoder, bytes == numPtr2);
            break;
          }
        }
        else
        {
          if (chars >= charEnd)
          {
            --bytes;
            this.ThrowCharsOverflow(decoder, bytes == numPtr2);
            break;
          }
          *chars = ch;
          chars += 2;
        }
      }
      if (decoder != null)
        decoder.m_bytesUsed = (int) (bytes - numPtr2);
      return (int) (chars - chPtr);
    }

    public override int GetMaxByteCount(int charCount)
    {
      if (charCount < 0)
        throw new ArgumentOutOfRangeException("charCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      long num = (long) charCount + 1L;
      if (this.EncoderFallback.MaxCharCount > 1)
        num *= (long) this.EncoderFallback.MaxCharCount;
      if (num > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException("charCount", Environment.GetResourceString("ArgumentOutOfRange_GetByteCountOverflow"));
      return (int) num;
    }

    public override int GetMaxCharCount(int byteCount)
    {
      if (byteCount < 0)
        throw new ArgumentOutOfRangeException("byteCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      long num = (long) byteCount;
      if (this.DecoderFallback.MaxCharCount > 1)
        num *= (long) this.DecoderFallback.MaxCharCount;
      if (num > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException("byteCount", Environment.GetResourceString("ArgumentOutOfRange_GetCharCountOverflow"));
      return (int) num;
    }

    [ComVisible(false)]
    public override bool IsAlwaysNormalized(NormalizationForm form)
    {
      if (form == NormalizationForm.FormC)
      {
        switch (this.CodePage)
        {
          case 28603:
          case 28605:
          case 21866:
          case 28591:
          case 28592:
          case 28594:
          case 28595:
          case 28599:
          case 20880:
          case 20924:
          case 21025:
          case 20297:
          case 20866:
          case 20871:
          case 10029:
          case 20273:
          case 20277:
          case 20278:
          case 20280:
          case 20284:
          case 20285:
          case 10007:
          case 10017:
          case 1140:
          case 1141:
          case 1142:
          case 1143:
          case 1144:
          case 1145:
          case 1146:
          case 1147:
          case 1148:
          case 1149:
          case 1250:
          case 1251:
          case 1252:
          case 1254:
          case 1256:
          case 850:
          case 852:
          case 855:
          case 858:
          case 860:
          case 861:
          case 862:
          case 863:
          case 865:
          case 866:
          case 869:
          case 870:
          case 1026:
          case 1047:
          case 720:
          case 737:
          case 775:
          case 37:
          case 437:
          case 500:
            return true;
        }
      }
      return false;
    }
  }
}
