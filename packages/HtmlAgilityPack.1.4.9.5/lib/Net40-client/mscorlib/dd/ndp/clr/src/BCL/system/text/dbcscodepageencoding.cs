// Decompiled with JetBrains decompiler
// Type: System.Text.DBCSCodePageEncoding
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System.Text
{
  [Serializable]
  internal class DBCSCodePageEncoding : BaseCodePageEncoding, ISerializable
  {
    [SecurityCritical]
    [NonSerialized]
    protected unsafe char* mapBytesToUnicode = (char*) null;
    [SecurityCritical]
    [NonSerialized]
    protected unsafe ushort* mapUnicodeToBytes = (ushort*) null;
    [SecurityCritical]
    [NonSerialized]
    protected unsafe int* mapCodePageCached = (int*) null;
    [NonSerialized]
    protected const char UNKNOWN_CHAR_FLAG = '\0';
    [NonSerialized]
    protected const char UNICODE_REPLACEMENT_CHAR = '�';
    [NonSerialized]
    protected const char LEAD_BYTE_CHAR = '\xFFFE';
    [NonSerialized]
    private ushort bytesUnknown;
    [NonSerialized]
    private int byteCountUnknown;
    [NonSerialized]
    protected char charUnknown;
    private static object s_InternalSyncObject;

    private static object InternalSyncObject
    {
      get
      {
        if (DBCSCodePageEncoding.s_InternalSyncObject == null)
        {
          object obj = new object();
          Interlocked.CompareExchange<object>(ref DBCSCodePageEncoding.s_InternalSyncObject, obj, (object) null);
        }
        return DBCSCodePageEncoding.s_InternalSyncObject;
      }
    }

    [SecurityCritical]
    public DBCSCodePageEncoding(int codePage)
    {
      int num = codePage;
      // ISSUE: explicit constructor call
      this.\u002Ector(num, num);
    }

    [SecurityCritical]
    internal unsafe DBCSCodePageEncoding(int codePage, int dataCodePage)
      : base(codePage, dataCodePage)
    {
    }

    [SecurityCritical]
    internal unsafe DBCSCodePageEncoding(SerializationInfo info, StreamingContext context)
      : base(0)
    {
      throw new ArgumentNullException("this");
    }

    [SecurityCritical]
    protected override unsafe void LoadManagedCodePage()
    {
      if ((int) this.pCodePage->ByteCount != 2)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_NoCodepageData", (object) this.CodePage));
      this.bytesUnknown = this.pCodePage->ByteReplace;
      this.charUnknown = this.pCodePage->UnicodeReplace;
      if (this.DecoderFallback.IsMicrosoftBestFitFallback)
        ((InternalDecoderBestFitFallback) this.DecoderFallback).cReplacement = this.charUnknown;
      this.byteCountUnknown = 1;
      if ((int) this.bytesUnknown > (int) byte.MaxValue)
        this.byteCountUnknown = this.byteCountUnknown + 1;
      byte* sharedMemory = this.GetSharedMemory(262148 + this.iExtraBytes);
      this.mapBytesToUnicode = (char*) sharedMemory;
      this.mapUnicodeToBytes = (ushort*) (sharedMemory + 131072);
      this.mapCodePageCached = (int*) (sharedMemory + 262144 + this.iExtraBytes);
      if (*this.mapCodePageCached != 0)
      {
        if (*this.mapCodePageCached != this.dataTableCodePage && this.bFlagDataTable || *this.mapCodePageCached != this.CodePage && !this.bFlagDataTable)
          throw new OutOfMemoryException(Environment.GetResourceString("Arg_OutOfMemoryException"));
      }
      else
      {
        char* chPtr = (char*) &this.pCodePage->FirstDataWord;
        int num = 0;
        while (num < 65536)
        {
          char index = *chPtr;
          chPtr += 2;
          if ((int) index == 1)
          {
            num = (int) *chPtr;
            chPtr += 2;
          }
          else if ((int) index < 32 && (int) index > 0)
          {
            num += (int) index;
          }
          else
          {
            int bytes;
            if ((int) index == (int) ushort.MaxValue)
            {
              bytes = num;
              index = (char) num;
            }
            else if ((int) index == 65534)
            {
              bytes = num;
            }
            else
            {
              if ((int) index == 65533)
              {
                ++num;
                continue;
              }
              bytes = num;
            }
            if (this.CleanUpBytes(ref bytes))
            {
              if ((int) index != 65534)
                this.mapUnicodeToBytes[index] = (ushort) bytes;
              this.mapBytesToUnicode[bytes] = index;
            }
            ++num;
          }
        }
        this.CleanUpEndBytes(this.mapBytesToUnicode);
        if (!this.bFlagDataTable)
          return;
        *this.mapCodePageCached = this.dataTableCodePage;
      }
    }

    protected virtual bool CleanUpBytes(ref int bytes)
    {
      return true;
    }

    [SecurityCritical]
    protected virtual unsafe void CleanUpEndBytes(char* chars)
    {
    }

    [SecurityCritical]
    protected override unsafe void ReadBestFitTable()
    {
      lock (DBCSCodePageEncoding.InternalSyncObject)
      {
        if (this.arrayUnicodeBestFit != null)
          return;
        char* local_2 = (char*) &this.pCodePage->FirstDataWord;
        int local_3 = 0;
        while (local_3 < 65536)
        {
          char local_10 = *local_2;
          local_2 += 2;
          if ((int) local_10 == 1)
          {
            local_3 = (int) *local_2;
            local_2 += 2;
          }
          else if ((int) local_10 < 32 && (int) local_10 > 0)
            local_3 += (int) local_10;
          else
            ++local_3;
        }
        char* local_4 = local_2;
        int local_5 = 0;
        int local_3_1 = (int) *local_2;
        char* local_2_1 = (char*) ((IntPtr) local_2 + 2);
        while (local_3_1 < 65536)
        {
          char local_11 = *local_2_1;
          local_2_1 += 2;
          if ((int) local_11 == 1)
          {
            local_3_1 = (int) *local_2_1;
            local_2_1 += 2;
          }
          else if ((int) local_11 < 32 && (int) local_11 > 0)
          {
            local_3_1 += (int) local_11;
          }
          else
          {
            if ((int) local_11 != 65533)
            {
              int local_12 = local_3_1;
              if (this.CleanUpBytes(ref local_12) && (int) this.mapBytesToUnicode[local_12] != (int) local_11)
                ++local_5;
            }
            ++local_3_1;
          }
        }
        char[] local_6 = new char[local_5 * 2];
        int local_5_1 = 0;
        char* local_2_2 = local_4;
        int local_3_2 = (int) *local_2_2;
        char* local_2_3 = (char*) ((IntPtr) local_2_2 + 2);
        bool local_7 = false;
        while (local_3_2 < 65536)
        {
          char local_13 = *local_2_3;
          local_2_3 += 2;
          if ((int) local_13 == 1)
          {
            local_3_2 = (int) *local_2_3;
            local_2_3 += 2;
          }
          else if ((int) local_13 < 32 && (int) local_13 > 0)
          {
            local_3_2 += (int) local_13;
          }
          else
          {
            if ((int) local_13 != 65533)
            {
              int local_14 = local_3_2;
              if (this.CleanUpBytes(ref local_14) && (int) this.mapBytesToUnicode[local_14] != (int) local_13)
              {
                if (local_14 != local_3_2)
                  local_7 = true;
                char[] temp_129 = local_6;
                int temp_130 = local_5_1;
                int temp_131 = 1;
                int local_5_2 = temp_130 + temp_131;
                int temp_134 = (int) (ushort) local_14;
                temp_129[temp_130] = (char) temp_134;
                char[] temp_135 = local_6;
                int temp_136 = local_5_2;
                int temp_137 = 1;
                local_5_1 = temp_136 + temp_137;
                int temp_139 = (int) local_13;
                temp_135[temp_136] = (char) temp_139;
              }
            }
            ++local_3_2;
          }
        }
        if (local_7)
        {
          int local_15 = 0;
          while (local_15 < local_6.Length - 2)
          {
            int local_16 = local_15;
            char local_17 = local_6[local_15];
            int local_18 = local_15 + 2;
            while (local_18 < local_6.Length)
            {
              if ((int) local_17 > (int) local_6[local_18])
              {
                local_17 = local_6[local_18];
                local_16 = local_18;
              }
              local_18 += 2;
            }
            if (local_16 != local_15)
            {
              char local_19 = local_6[local_16];
              local_6[local_16] = local_6[local_15];
              local_6[local_15] = local_19;
              char local_19_1 = local_6[local_16 + 1];
              local_6[local_16 + 1] = local_6[local_15 + 1];
              local_6[local_15 + 1] = local_19_1;
            }
            local_15 += 2;
          }
        }
        this.arrayBytesBestFit = local_6;
        char* local_8 = local_2_3;
        char* temp_335 = local_2_3;
        int temp_156 = 2;
        char* local_2_4 = (char*) ((IntPtr) temp_335 + temp_156);
        int local_9 = (int) *temp_335;
        int local_5_3 = 0;
        while (local_9 < 65536)
        {
          char local_20 = *local_2_4;
          local_2_4 += 2;
          if ((int) local_20 == 1)
          {
            local_9 = (int) *local_2_4;
            local_2_4 += 2;
          }
          else if ((int) local_20 < 32 && (int) local_20 > 0)
          {
            local_9 += (int) local_20;
          }
          else
          {
            if ((int) local_20 > 0)
              ++local_5_3;
            ++local_9;
          }
        }
        char[] local_6_1 = new char[local_5_3 * 2];
        char* temp_341 = local_8;
        int temp_195 = 2;
        char* local_2_6 = (char*) ((IntPtr) temp_341 + temp_195);
        int local_9_1 = (int) *temp_341;
        int local_5_4 = 0;
        while (local_9_1 < 65536)
        {
          char local_21 = *local_2_6;
          local_2_6 += 2;
          if ((int) local_21 == 1)
          {
            local_9_1 = (int) *local_2_6;
            local_2_6 += 2;
          }
          else if ((int) local_21 < 32 && (int) local_21 > 0)
          {
            local_9_1 += (int) local_21;
          }
          else
          {
            if ((int) local_21 > 0)
            {
              int local_22 = (int) local_21;
              if (this.CleanUpBytes(ref local_22))
              {
                char[] temp_219 = local_6_1;
                int temp_220 = local_5_4;
                int temp_221 = 1;
                int local_5_5 = temp_220 + temp_221;
                int temp_224 = (int) (ushort) local_9_1;
                temp_219[temp_220] = (char) temp_224;
                char[] temp_225 = local_6_1;
                int temp_226 = local_5_5;
                int temp_227 = 1;
                local_5_4 = temp_226 + temp_227;
                int temp_236 = (int) this.mapBytesToUnicode[local_22];
                temp_225[temp_226] = (char) temp_236;
              }
            }
            ++local_9_1;
          }
        }
        this.arrayUnicodeBestFit = local_6_1;
      }
    }

    [SecurityCritical]
    internal override unsafe int GetByteCount(char* chars, int count, EncoderNLS encoder)
    {
      this.CheckMemorySection();
      char ch1 = char.MinValue;
      if (encoder != null)
      {
        ch1 = encoder.charLeftOver;
        if (encoder.InternalHasFallbackBuffer && encoder.FallbackBuffer.Remaining > 0)
          throw new ArgumentException(Environment.GetResourceString("Argument_EncoderFallbackNotEmpty", (object) this.EncodingName, (object) encoder.Fallback.GetType()));
      }
      int num1 = 0;
      char* charEnd = chars + count;
      EncoderFallbackBuffer encoderFallbackBuffer = (EncoderFallbackBuffer) null;
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
        ushort num2 = this.mapUnicodeToBytes[ch2];
        if ((int) num2 == 0 && (int) ch2 != 0)
        {
          if (encoderFallbackBuffer == null)
          {
            encoderFallbackBuffer = encoder != null ? encoder.FallbackBuffer : this.encoderFallback.CreateFallbackBuffer();
            encoderFallbackBuffer.InternalInitialize(charEnd - count, charEnd, encoder, false);
          }
          encoderFallbackBuffer.InternalFallback(ch2, ref chars);
        }
        else
        {
          ++num1;
          if ((int) num2 >= 256)
            ++num1;
        }
      }
      return num1;
    }

    [SecurityCritical]
    internal override unsafe int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, EncoderNLS encoder)
    {
      this.CheckMemorySection();
      EncoderFallbackBuffer encoderFallbackBuffer = (EncoderFallbackBuffer) null;
      char* charEnd = chars + charCount;
      char* chPtr = chars;
      byte* numPtr1 = bytes;
      byte* numPtr2 = bytes + byteCount;
      if (encoder != null)
      {
        char ch = encoder.charLeftOver;
        encoderFallbackBuffer = encoder.FallbackBuffer;
        encoderFallbackBuffer.InternalInitialize(chars, charEnd, encoder, true);
        if (encoder.m_throwOnOverflow && encoderFallbackBuffer.Remaining > 0)
          throw new ArgumentException(Environment.GetResourceString("Argument_EncoderFallbackNotEmpty", (object) this.EncodingName, (object) encoder.Fallback.GetType()));
        if ((int) ch > 0)
          encoderFallbackBuffer.InternalFallback(ch, ref chars);
      }
      char ch1;
      while ((int) (ch1 = encoderFallbackBuffer == null ? char.MinValue : encoderFallbackBuffer.InternalGetNextChar()) != 0 || chars < charEnd)
      {
        if ((int) ch1 == 0)
        {
          ch1 = *chars;
          chars += 2;
        }
        ushort num = this.mapUnicodeToBytes[ch1];
        if ((int) num == 0 && (int) ch1 != 0)
        {
          if (encoderFallbackBuffer == null)
          {
            encoderFallbackBuffer = this.encoderFallback.CreateFallbackBuffer();
            encoderFallbackBuffer.InternalInitialize(charEnd - charCount, charEnd, encoder, true);
          }
          encoderFallbackBuffer.InternalFallback(ch1, ref chars);
        }
        else
        {
          if ((int) num >= 256)
          {
            if (bytes + 1 >= numPtr2)
            {
              if (encoderFallbackBuffer == null || !encoderFallbackBuffer.bFallingBack)
                chars -= 2;
              else
                encoderFallbackBuffer.MovePrevious();
              this.ThrowBytesOverflow(encoder, chars == chPtr);
              break;
            }
            *bytes = (byte) ((uint) num >> 8);
            ++bytes;
          }
          else if (bytes >= numPtr2)
          {
            if (encoderFallbackBuffer == null || !encoderFallbackBuffer.bFallingBack)
              chars -= 2;
            else
              encoderFallbackBuffer.MovePrevious();
            this.ThrowBytesOverflow(encoder, chars == chPtr);
            break;
          }
          *bytes = (byte) ((uint) num & (uint) byte.MaxValue);
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
    internal override unsafe int GetCharCount(byte* bytes, int count, DecoderNLS baseDecoder)
    {
      this.CheckMemorySection();
      DBCSCodePageEncoding.DBCSDecoder dbcsDecoder = (DBCSCodePageEncoding.DBCSDecoder) baseDecoder;
      DecoderFallbackBuffer decoderFallbackBuffer = (DecoderFallbackBuffer) null;
      byte* numPtr = bytes + count;
      int num1 = count;
      if (dbcsDecoder != null && (int) dbcsDecoder.bLeftOver > 0)
      {
        if (count == 0)
        {
          if (!dbcsDecoder.MustFlush)
            return 0;
          DecoderFallbackBuffer fallbackBuffer = dbcsDecoder.FallbackBuffer;
          fallbackBuffer.InternalInitialize(bytes, (char*) null);
          byte[] bytes1 = new byte[1]{ dbcsDecoder.bLeftOver };
          return fallbackBuffer.InternalFallback(bytes1, bytes);
        }
        int index = (int) dbcsDecoder.bLeftOver << 8 | (int) *bytes;
        ++bytes;
        if ((int) this.mapBytesToUnicode[index] == 0 && index != 0)
        {
          int num2 = num1 - 1;
          decoderFallbackBuffer = dbcsDecoder.FallbackBuffer;
          decoderFallbackBuffer.InternalInitialize(numPtr - count, (char*) null);
          byte[] bytes1 = new byte[2]{ (byte) (index >> 8), (byte) index };
          num1 = num2 + decoderFallbackBuffer.InternalFallback(bytes1, bytes);
        }
      }
      while (bytes < numPtr)
      {
        int index = (int) *bytes;
        ++bytes;
        char ch = this.mapBytesToUnicode[index];
        if ((int) ch == 65534)
        {
          --num1;
          if (bytes < numPtr)
          {
            index = index << 8 | (int) *bytes;
            ++bytes;
            ch = this.mapBytesToUnicode[index];
          }
          else if (dbcsDecoder == null || dbcsDecoder.MustFlush)
          {
            ++num1;
            ch = char.MinValue;
          }
          else
            break;
        }
        if ((int) ch == 0 && index != 0)
        {
          if (decoderFallbackBuffer == null)
          {
            decoderFallbackBuffer = dbcsDecoder != null ? dbcsDecoder.FallbackBuffer : this.DecoderFallback.CreateFallbackBuffer();
            decoderFallbackBuffer.InternalInitialize(numPtr - count, (char*) null);
          }
          int num2 = num1 - 1;
          byte[] bytes1;
          if (index < 256)
            bytes1 = new byte[1]{ (byte) index };
          else
            bytes1 = new byte[2]
            {
              (byte) (index >> 8),
              (byte) index
            };
          num1 = num2 + decoderFallbackBuffer.InternalFallback(bytes1, bytes);
        }
      }
      return num1;
    }

    [SecurityCritical]
    internal override unsafe int GetChars(byte* bytes, int byteCount, char* chars, int charCount, DecoderNLS baseDecoder)
    {
      this.CheckMemorySection();
      DBCSCodePageEncoding.DBCSDecoder dbcsDecoder = (DBCSCodePageEncoding.DBCSDecoder) baseDecoder;
      byte* numPtr1 = bytes;
      byte* numPtr2 = bytes + byteCount;
      char* chPtr = chars;
      char* charEnd = chars + charCount;
      bool flag = false;
      DecoderFallbackBuffer decoderFallbackBuffer = (DecoderFallbackBuffer) null;
      if (dbcsDecoder != null && (int) dbcsDecoder.bLeftOver > 0)
      {
        if (byteCount == 0)
        {
          if (!dbcsDecoder.MustFlush)
            return 0;
          DecoderFallbackBuffer fallbackBuffer = dbcsDecoder.FallbackBuffer;
          fallbackBuffer.InternalInitialize(bytes, charEnd);
          byte[] bytes1 = new byte[1]{ dbcsDecoder.bLeftOver };
          if (!fallbackBuffer.InternalFallback(bytes1, bytes, ref chars))
            this.ThrowCharsOverflow((DecoderNLS) dbcsDecoder, true);
          dbcsDecoder.bLeftOver = (byte) 0;
          return (int) (chars - chPtr);
        }
        int index = (int) dbcsDecoder.bLeftOver << 8 | (int) *bytes;
        ++bytes;
        char ch = this.mapBytesToUnicode[index];
        if ((int) ch == 0 && index != 0)
        {
          decoderFallbackBuffer = dbcsDecoder.FallbackBuffer;
          decoderFallbackBuffer.InternalInitialize(numPtr2 - byteCount, charEnd);
          byte[] bytes1 = new byte[2]{ (byte) (index >> 8), (byte) index };
          if (!decoderFallbackBuffer.InternalFallback(bytes1, bytes, ref chars))
            this.ThrowCharsOverflow((DecoderNLS) dbcsDecoder, true);
        }
        else
        {
          if (chars >= charEnd)
            this.ThrowCharsOverflow((DecoderNLS) dbcsDecoder, true);
          *chars++ = ch;
        }
      }
      while (bytes < numPtr2)
      {
        int index = (int) *bytes;
        ++bytes;
        char ch = this.mapBytesToUnicode[index];
        if ((int) ch == 65534)
        {
          if (bytes < numPtr2)
          {
            index = index << 8 | (int) *bytes;
            ++bytes;
            ch = this.mapBytesToUnicode[index];
          }
          else if (dbcsDecoder == null || dbcsDecoder.MustFlush)
          {
            ch = char.MinValue;
          }
          else
          {
            flag = true;
            dbcsDecoder.bLeftOver = (byte) index;
            break;
          }
        }
        if ((int) ch == 0 && index != 0)
        {
          if (decoderFallbackBuffer == null)
          {
            decoderFallbackBuffer = dbcsDecoder != null ? dbcsDecoder.FallbackBuffer : this.DecoderFallback.CreateFallbackBuffer();
            decoderFallbackBuffer.InternalInitialize(numPtr2 - byteCount, charEnd);
          }
          byte[] bytes1;
          if (index < 256)
            bytes1 = new byte[1]{ (byte) index };
          else
            bytes1 = new byte[2]
            {
              (byte) (index >> 8),
              (byte) index
            };
          if (!decoderFallbackBuffer.InternalFallback(bytes1, bytes, ref chars))
          {
            bytes -= bytes1.Length;
            decoderFallbackBuffer.InternalReset();
            this.ThrowCharsOverflow((DecoderNLS) dbcsDecoder, bytes == numPtr1);
            break;
          }
        }
        else
        {
          if (chars >= charEnd)
          {
            --bytes;
            if (index >= 256)
              --bytes;
            this.ThrowCharsOverflow((DecoderNLS) dbcsDecoder, bytes == numPtr1);
            break;
          }
          *chars++ = ch;
        }
      }
      if (dbcsDecoder != null)
      {
        if (!flag)
          dbcsDecoder.bLeftOver = (byte) 0;
        dbcsDecoder.m_bytesUsed = (int) (bytes - numPtr1);
      }
      return (int) (chars - chPtr);
    }

    public override int GetMaxByteCount(int charCount)
    {
      if (charCount < 0)
        throw new ArgumentOutOfRangeException("charCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      long num1 = (long) charCount + 1L;
      if (this.EncoderFallback.MaxCharCount > 1)
        num1 *= (long) this.EncoderFallback.MaxCharCount;
      long num2 = num1 * 2L;
      if (num2 > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException("charCount", Environment.GetResourceString("ArgumentOutOfRange_GetByteCountOverflow"));
      return (int) num2;
    }

    public override int GetMaxCharCount(int byteCount)
    {
      if (byteCount < 0)
        throw new ArgumentOutOfRangeException("byteCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      long num = (long) byteCount + 1L;
      if (this.DecoderFallback.MaxCharCount > 1)
        num *= (long) this.DecoderFallback.MaxCharCount;
      if (num > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException("byteCount", Environment.GetResourceString("ArgumentOutOfRange_GetCharCountOverflow"));
      return (int) num;
    }

    public override Decoder GetDecoder()
    {
      return (Decoder) new DBCSCodePageEncoding.DBCSDecoder(this);
    }

    [Serializable]
    internal class DBCSDecoder : DecoderNLS
    {
      internal byte bLeftOver;

      internal override bool HasState
      {
        get
        {
          return (uint) this.bLeftOver > 0U;
        }
      }

      public DBCSDecoder(DBCSCodePageEncoding encoding)
        : base((Encoding) encoding)
      {
      }

      public override void Reset()
      {
        this.bLeftOver = (byte) 0;
        if (this.m_fallbackBuffer == null)
          return;
        this.m_fallbackBuffer.Reset();
      }
    }
  }
}
