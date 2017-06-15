// Decompiled with JetBrains decompiler
// Type: System.Text.EncoderReplacementFallback
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Text
{
  /// <summary>为不能转换为输出字节序列的输入字符提供一个称为“回退”的失败处理机制。此回退使用由用户指定的替换字符串来代替原始的输入字符。此类不能被继承。</summary>
  /// <filterpriority>2</filterpriority>
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class EncoderReplacementFallback : EncoderFallback
  {
    private string strDefault;

    /// <summary>获取作为此 <see cref="T:System.Text.EncoderReplacementFallback" /> 对象的值的替换字符串。</summary>
    /// <returns>一个用于替换无法编码的输入字符的替代字符串。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public string DefaultString
    {
      [__DynamicallyInvokable] get
      {
        return this.strDefault;
      }
    }

    /// <summary>获取此 <see cref="T:System.Text.EncoderReplacementFallback" /> 对象的替换字符串中的字符数。</summary>
    /// <returns>用于替换无法编码的输入字符的字符串中的字符数。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override int MaxCharCount
    {
      [__DynamicallyInvokable] get
      {
        return this.strDefault.Length;
      }
    }

    /// <summary>初始化 <see cref="T:System.Text.EncoderReplacementFallback" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public EncoderReplacementFallback()
      : this("?")
    {
    }

    /// <summary>使用指定的替换字符串初始化 <see cref="T:System.Text.EncoderReplacementFallback" /> 类的新实例。</summary>
    /// <param name="replacement">编码操作中转换的、用以替代无法编码的输入字符的字符串。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="replacement" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="replacement" /> 包含无效的代理项对。也就是说，代理项不是由一个高代理项组件后面跟着一个低代理项组件组成。</exception>
    [__DynamicallyInvokable]
    public EncoderReplacementFallback(string replacement)
    {
      if (replacement == null)
        throw new ArgumentNullException("replacement");
      bool flag = false;
      for (int index = 0; index < replacement.Length; ++index)
      {
        if (char.IsSurrogate(replacement, index))
        {
          if (char.IsHighSurrogate(replacement, index))
          {
            if (!flag)
              flag = true;
            else
              break;
          }
          else
          {
            if (!flag)
            {
              flag = true;
              break;
            }
            flag = false;
          }
        }
        else if (flag)
          break;
      }
      if (flag)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidCharSequenceNoIndex", (object) "replacement"));
      this.strDefault = replacement;
    }

    /// <summary>创建一个 <see cref="T:System.Text.EncoderFallbackBuffer" /> 对象，该对象是用此 <see cref="T:System.Text.EncoderReplacementFallback" /> 对象的替换字符串初始化的。</summary>
    /// <returns>一个 <see cref="T:System.Text.EncoderFallbackBuffer" /> 对象，它与此 <see cref="T:System.Text.EncoderReplacementFallback" /> 对象相等。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override EncoderFallbackBuffer CreateFallbackBuffer()
    {
      return (EncoderFallbackBuffer) new EncoderReplacementFallbackBuffer(this);
    }

    /// <summary>指示指定对象的值是否与等于此 <see cref="T:System.Text.EncoderReplacementFallback" /> 对象的值。</summary>
    /// <returns>true if the <paramref name="value" /> parameter specifies an <see cref="T:System.Text.EncoderReplacementFallback" /> object and the replacement string of that object is equal to the replacement string of this <see cref="T:System.Text.EncoderReplacementFallback" /> object; otherwise, false.</returns>
    /// <param name="value">
    /// <see cref="T:System.Text.EncoderReplacementFallback" /> 对象。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override bool Equals(object value)
    {
      EncoderReplacementFallback replacementFallback = value as EncoderReplacementFallback;
      if (replacementFallback != null)
        return this.strDefault == replacementFallback.strDefault;
      return false;
    }

    /// <summary>检索此 <see cref="T:System.Text.EncoderReplacementFallback" /> 对象的值的哈希代码。</summary>
    /// <returns>此对象的值的哈希代码。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return this.strDefault.GetHashCode();
    }
  }
}
