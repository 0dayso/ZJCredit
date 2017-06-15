// Decompiled with JetBrains decompiler
// Type: System.Text.EncoderFallbackBuffer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Text
{
  /// <summary>提供一个允许回退处理程序在无法编码输入的字符时返回备用字符串到编码器的缓冲区。</summary>
  /// <filterpriority>2</filterpriority>
  [__DynamicallyInvokable]
  public abstract class EncoderFallbackBuffer
  {
    [SecurityCritical]
    internal unsafe char* charStart;
    [SecurityCritical]
    internal unsafe char* charEnd;
    internal EncoderNLS encoder;
    internal bool setEncoder;
    internal bool bUsedEncoder;
    internal bool bFallingBack;
    internal int iRecursionCount;
    private const int iMaxRecursion = 250;

    /// <summary>在派生类中重写后，此属性获取当前 <see cref="T:System.Text.EncoderFallbackBuffer" /> 对象中要处理的剩余字符数。</summary>
    /// <returns>尚未处理的当前回退缓冲区中的字符数。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public abstract int Remaining { [__DynamicallyInvokable] get; }

    /// <summary>初始化 <see cref="T:System.Text.EncoderFallbackBuffer" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    protected EncoderFallbackBuffer()
    {
    }

    /// <summary>在派生类中重写后，此方法对回退缓冲区进行准备，以处理指定的输入字符。</summary>
    /// <returns>如果回退缓冲区能处理 <paramref name="charUnknown" /> 则为 true；如果回退缓冲区忽略 <paramref name="charUnknown" />，则为 false。</returns>
    /// <param name="charUnknown">一个输入字符。</param>
    /// <param name="index">该字符在输入缓冲区中的索引位置。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public abstract bool Fallback(char charUnknown, int index);

    /// <summary>在派生类中重写后，此方法对回退缓冲区进行准备，以处理指定的代理项对。</summary>
    /// <returns>如果回退缓冲区可以处理 <paramref name="charUnknownHigh" /> 和 <paramref name="charUnknownLow" />，则为 true；如果回退缓冲区忽略代理项对，则为 false。</returns>
    /// <param name="charUnknownHigh">输入对的高代理项。</param>
    /// <param name="charUnknownLow">输入对的低代理项。</param>
    /// <param name="index">该代理项对在输入缓冲区中的索引位置。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public abstract bool Fallback(char charUnknownHigh, char charUnknownLow, int index);

    /// <summary>在派生类中重写后，此方法检索回退缓冲区中的下一个字符。</summary>
    /// <returns>回退缓冲区中的下一个字符。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public abstract char GetNextChar();

    /// <summary>在派生类中重写后，此方法将使对 <see cref="M:System.Text.EncoderFallbackBuffer.GetNextChar" /> 方法的下一次调用访问当前字符位置之前的数据缓冲区字符位置。</summary>
    /// <returns>如果 <see cref="M:System.Text.EncoderFallbackBuffer.MovePrevious" /> 操作成功，则为 true，否则为 false。</returns>
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
      this.charStart = (char*) null;
      this.bFallingBack = false;
      this.iRecursionCount = 0;
      this.Reset();
    }

    [SecurityCritical]
    internal unsafe void InternalInitialize(char* charStart, char* charEnd, EncoderNLS encoder, bool setEncoder)
    {
      this.charStart = charStart;
      this.charEnd = charEnd;
      this.encoder = encoder;
      this.setEncoder = setEncoder;
      this.bUsedEncoder = false;
      this.bFallingBack = false;
      this.iRecursionCount = 0;
    }

    internal char InternalGetNextChar()
    {
      char nextChar = this.GetNextChar();
      this.bFallingBack = (uint) nextChar > 0U;
      if ((int) nextChar == 0)
        this.iRecursionCount = 0;
      return nextChar;
    }

    [SecurityCritical]
    internal virtual unsafe bool InternalFallback(char ch, ref char* chars)
    {
      int index = (int) (chars - this.charStart) - 1;
      if (char.IsHighSurrogate(ch))
      {
        if (chars >= this.charEnd)
        {
          if (this.encoder != null && !this.encoder.MustFlush)
          {
            if (this.setEncoder)
            {
              this.bUsedEncoder = true;
              this.encoder.charLeftOver = ch;
            }
            this.bFallingBack = false;
            return false;
          }
        }
        else
        {
          char ch1 = *chars;
          if (char.IsLowSurrogate(ch1))
          {
            if (this.bFallingBack)
            {
              int num = this.iRecursionCount;
              this.iRecursionCount = num + 1;
              if (num > 250)
                this.ThrowLastCharRecursive(char.ConvertToUtf32(ch, ch1));
            }
            chars += 2;
            this.bFallingBack = this.Fallback(ch, ch1, index);
            return this.bFallingBack;
          }
        }
      }
      if (this.bFallingBack)
      {
        int num = this.iRecursionCount;
        this.iRecursionCount = num + 1;
        if (num > 250)
          this.ThrowLastCharRecursive((int) ch);
      }
      this.bFallingBack = this.Fallback(ch, index);
      return this.bFallingBack;
    }

    internal void ThrowLastCharRecursive(int charRecursive)
    {
      throw new ArgumentException(Environment.GetResourceString("Argument_RecursiveFallback", (object) charRecursive), "chars");
    }
  }
}
