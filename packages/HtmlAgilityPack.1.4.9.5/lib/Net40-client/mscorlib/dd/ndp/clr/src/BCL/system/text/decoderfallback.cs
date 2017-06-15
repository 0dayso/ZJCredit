// Decompiled with JetBrains decompiler
// Type: System.Text.DecoderFallback
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Threading;

namespace System.Text
{
  /// <summary>为不能转换为输出字符的已编码输入字节序列提供失败处理机制（称为“回退”）。</summary>
  /// <filterpriority>2</filterpriority>
  [__DynamicallyInvokable]
  [Serializable]
  public abstract class DecoderFallback
  {
    internal bool bIsMicrosoftBestFitFallback;
    private static volatile DecoderFallback replacementFallback;
    private static volatile DecoderFallback exceptionFallback;
    private static object s_InternalSyncObject;

    private static object InternalSyncObject
    {
      get
      {
        if (DecoderFallback.s_InternalSyncObject == null)
        {
          object obj = new object();
          Interlocked.CompareExchange<object>(ref DecoderFallback.s_InternalSyncObject, obj, (object) null);
        }
        return DecoderFallback.s_InternalSyncObject;
      }
    }

    /// <summary>获取输出替代字符串的对象，以替代无法解码的输入字节序列。</summary>
    /// <returns>从 <see cref="T:System.Text.DecoderFallback" /> 类派生的类型。默认值是发出问号字符（“?”和 U+003F）来替代未知字节序列的 <see cref="T:System.Text.DecoderReplacementFallback" /> 对象。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static DecoderFallback ReplacementFallback
    {
      [__DynamicallyInvokable] get
      {
        if (DecoderFallback.replacementFallback == null)
        {
          lock (DecoderFallback.InternalSyncObject)
          {
            if (DecoderFallback.replacementFallback == null)
              DecoderFallback.replacementFallback = (DecoderFallback) new DecoderReplacementFallback();
          }
        }
        return DecoderFallback.replacementFallback;
      }
    }

    /// <summary>获取无法解码输入字节序列时引发异常的对象。</summary>
    /// <returns>从 <see cref="T:System.Text.DecoderFallback" /> 类派生的类型。默认值为 <see cref="T:System.Text.DecoderExceptionFallback" /> 对象。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static DecoderFallback ExceptionFallback
    {
      [__DynamicallyInvokable] get
      {
        if (DecoderFallback.exceptionFallback == null)
        {
          lock (DecoderFallback.InternalSyncObject)
          {
            if (DecoderFallback.exceptionFallback == null)
              DecoderFallback.exceptionFallback = (DecoderFallback) new DecoderExceptionFallback();
          }
        }
        return DecoderFallback.exceptionFallback;
      }
    }

    /// <summary>当用派生类重写时，获取当前 <see cref="T:System.Text.DecoderFallback" /> 对象能返回的最大字符数。</summary>
    /// <returns>当前 <see cref="T:System.Text.DecoderFallback" /> 对象能返回的最大字符数。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public abstract int MaxCharCount { [__DynamicallyInvokable] get; }

    internal bool IsMicrosoftBestFitFallback
    {
      get
      {
        return this.bIsMicrosoftBestFitFallback;
      }
    }

    /// <summary>初始化 <see cref="T:System.Text.DecoderFallback" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    protected DecoderFallback()
    {
    }

    /// <summary>在派生类中重写时，将初始化 <see cref="T:System.Text.DecoderFallbackBuffer" /> 类的新实例。</summary>
    /// <returns>提供解码器回退缓冲区的对象。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public abstract DecoderFallbackBuffer CreateFallbackBuffer();
  }
}
