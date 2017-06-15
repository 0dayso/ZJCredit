// Decompiled with JetBrains decompiler
// Type: System.Text.EncoderFallback
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Threading;

namespace System.Text
{
  /// <summary>为不能转换为已编码输出字节序列的输入字符提供称为“回退”的失败处理机制。</summary>
  /// <filterpriority>2</filterpriority>
  [__DynamicallyInvokable]
  [Serializable]
  public abstract class EncoderFallback
  {
    internal bool bIsMicrosoftBestFitFallback;
    private static volatile EncoderFallback replacementFallback;
    private static volatile EncoderFallback exceptionFallback;
    private static object s_InternalSyncObject;

    private static object InternalSyncObject
    {
      get
      {
        if (EncoderFallback.s_InternalSyncObject == null)
        {
          object obj = new object();
          Interlocked.CompareExchange<object>(ref EncoderFallback.s_InternalSyncObject, obj, (object) null);
        }
        return EncoderFallback.s_InternalSyncObject;
      }
    }

    /// <summary>获取一个对象，该对象会输出一个替代字符串来代替无法编码的输入字符。</summary>
    /// <returns>从 <see cref="T:System.Text.EncoderFallback" /> 类派生的类型。默认值是 <see cref="T:System.Text.EncoderReplacementFallback" /> 对象，该对象将未知输入字符替换为问号字符（“?”，U+003F）。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static EncoderFallback ReplacementFallback
    {
      [__DynamicallyInvokable] get
      {
        if (EncoderFallback.replacementFallback == null)
        {
          lock (EncoderFallback.InternalSyncObject)
          {
            if (EncoderFallback.replacementFallback == null)
              EncoderFallback.replacementFallback = (EncoderFallback) new EncoderReplacementFallback();
          }
        }
        return EncoderFallback.replacementFallback;
      }
    }

    /// <summary>获取一个对象，在无法对输入字符进行编码时，该对象将引发异常。</summary>
    /// <returns>从 <see cref="T:System.Text.EncoderFallback" /> 类派生的类型。默认值为 <see cref="T:System.Text.EncoderExceptionFallback" /> 对象。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static EncoderFallback ExceptionFallback
    {
      [__DynamicallyInvokable] get
      {
        if (EncoderFallback.exceptionFallback == null)
        {
          lock (EncoderFallback.InternalSyncObject)
          {
            if (EncoderFallback.exceptionFallback == null)
              EncoderFallback.exceptionFallback = (EncoderFallback) new EncoderExceptionFallback();
          }
        }
        return EncoderFallback.exceptionFallback;
      }
    }

    /// <summary>在派生类中重写时，获取当前 <see cref="T:System.Text.EncoderFallback" /> 对象可以返回的最大字符数。</summary>
    /// <returns>当前 <see cref="T:System.Text.EncoderFallback" /> 对象可以返回的最大字符数。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public abstract int MaxCharCount { [__DynamicallyInvokable] get; }

    /// <summary>初始化 <see cref="T:System.Text.EncoderFallback" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    protected EncoderFallback()
    {
    }

    /// <summary>在派生类中重写时，将初始化 <see cref="T:System.Text.EncoderFallbackBuffer" /> 类的新实例。</summary>
    /// <returns>提供编码器回退缓冲区的对象。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public abstract EncoderFallbackBuffer CreateFallbackBuffer();
  }
}
