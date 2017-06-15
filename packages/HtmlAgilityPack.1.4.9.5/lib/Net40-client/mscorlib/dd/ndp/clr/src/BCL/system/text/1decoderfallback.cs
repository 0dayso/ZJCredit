// Decompiled with JetBrains decompiler
// Type: System.Text.DecoderFallbackBuffer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Security;

namespace System.Text
{
  /// <summary>提供一个允许回退处理程序在无法解码输入的字节序列时返回备用字符串到解码器的缓冲区。</summary>
  /// <filterpriority>2</filterpriority>
  [__DynamicallyInvokable]
  public abstract class DecoderFallbackBuffer
  {
    [SecurityCritical]
    internal unsafe byte* byteStart;
    [SecurityCritical]
    internal unsafe char* charEnd;

    /// <summary>在派生类中被重写时，获取尚未处理的当前 <see cref="T:System.Text.DecoderFallbackBuffer" /> 对象中的字符数。</summary>
    /// <returns>尚未处理的当前回退缓冲区中的字符数。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public abstract int Remaining { [__DynamicallyInvokable] get; }

    /// <summary>初始化 <see cref="T:System.Text.DecoderFallbackBuffer" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    protected DecoderFallbackBuffer()
    {
    }

    /// <summary>在派生类中被重写时，准备回退缓冲区以便对指定输入字节序列进行处理。</summary>
    /// <returns>如果回退缓冲区可以处理 <paramref name="bytesUnknown" />，则为 true；如果回退缓冲区忽略 <paramref name="bytesUnknown" />，则为 false。</returns>
    /// <param name="bytesUnknown">字节的输入数组。</param>
    /// <param name="index">
    /// <paramref name="bytesUnknown" /> 中字节的索引位置。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public abstract bool Fallback(byte[] bytesUnknown, int index);

    /// <summary>在派生类中重写后，此方法检索回退缓冲区中的下一个字符。</summary>
    /// <returns>回退缓冲区中的下一个字符。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public abstract char GetNextChar();

    /// <summary>在派生类中被重写时，将导致下一次调用 <see cref="M:System.Text.DecoderFallbackBuffer.GetNextChar" /> 方法，以便访问位于当前字符位置之前的数据缓冲区字符位置。</summary>
    /// <returns>如果 <see cref="M:System.Text.DecoderFallbackBuffer.MovePrevious" /> 操作成功，则为 true；否则为 false。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public abstract bool MovePrevious();

    /// <summary>初始化所有与此回退缓冲区相关的数据和状态信息。</summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual void Reset()
    {
      do
        ;
      while ((int) this.GetNextChar() != 0);
    }

    [SecurityCritical]
    internal unsafe void InternalReset()
    {
      this.byteStart = (byte*) null;
      this.Reset();
    }

    [SecurityCritical]
    internal unsafe void InternalInitialize(byte* byteStart, char* charEnd)
    {
      this.byteStart = byteStart;
      this.charEnd = charEnd;
    }

    [SecurityCritical]
    internal virtual unsafe bool InternalFallback(byte[] bytes, byte* pBytes, ref char* chars)
    {
      if (this.Fallback(bytes, (int) (pBytes - this.byteStart - (long) bytes.Length)))
      {
        char* chPtr = chars;
        bool flag = false;
        char nextChar;
        while ((int) (nextChar = this.GetNextChar()) != 0)
        {
          if (char.IsSurrogate(nextChar))
          {
            if (char.IsHighSurrogate(nextChar))
            {
              if (flag)
                throw new ArgumentException(Environment.GetResourceString("Argument_InvalidCharSequenceNoIndex"));
              flag = true;
            }
            else
            {
              if (!flag)
                throw new ArgumentException(Environment.GetResourceString("Argument_InvalidCharSequenceNoIndex"));
              flag = false;
            }
          }
          if (chPtr >= this.charEnd)
            return false;
          *chPtr++ = nextChar;
        }
        if (flag)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidCharSequenceNoIndex"));
        chars = chPtr;
      }
      return true;
    }

    [SecurityCritical]
    internal virtual unsafe int InternalFallback(byte[] bytes, byte* pBytes)
    {
      if (!this.Fallback(bytes, (int) (pBytes - this.byteStart - (long) bytes.Length)))
        return 0;
      int num = 0;
      bool flag = false;
      char nextChar;
      while ((int) (nextChar = this.GetNextChar()) != 0)
      {
        if (char.IsSurrogate(nextChar))
        {
          if (char.IsHighSurrogate(nextChar))
          {
            if (flag)
              throw new ArgumentException(Environment.GetResourceString("Argument_InvalidCharSequenceNoIndex"));
            flag = true;
          }
          else
          {
            if (!flag)
              throw new ArgumentException(Environment.GetResourceString("Argument_InvalidCharSequenceNoIndex"));
            flag = false;
          }
        }
        ++num;
      }
      if (flag)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidCharSequenceNoIndex"));
      return num;
    }

    internal void ThrowLastBytesRecursive(byte[] bytesUnknown)
    {
      StringBuilder stringBuilder = new StringBuilder(bytesUnknown.Length * 3);
      int index;
      for (index = 0; index < bytesUnknown.Length && index < 20; ++index)
      {
        if (stringBuilder.Length > 0)
          stringBuilder.Append(" ");
        stringBuilder.Append(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "\\x{0:X2}", (object) bytesUnknown[index]));
      }
      if (index == 20)
        stringBuilder.Append(" ...");
      throw new ArgumentException(Environment.GetResourceString("Argument_RecursiveFallbackBytes", (object) stringBuilder.ToString()), "bytesUnknown");
    }
  }
}
