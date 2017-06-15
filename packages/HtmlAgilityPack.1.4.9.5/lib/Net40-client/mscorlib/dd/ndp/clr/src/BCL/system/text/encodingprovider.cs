// Decompiled with JetBrains decompiler
// Type: System.Text.EncodingProvider
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Text
{
  /// <summary>提供编码提供程序的基类，后者提供在特定平台上不可用的编码。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public abstract class EncodingProvider
  {
    private static object s_InternalSyncObject = new object();
    private static volatile EncodingProvider[] s_providers;

    /// <summary>初始化 <see cref="T:System.Text.EncodingProvider" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public EncodingProvider()
    {
    }

    /// <summary>返回具有指定名称的编码。</summary>
    /// <returns>指定的名称，与关联的编码，它或null如果此<see cref="T:System.Text.EncodingProvider" />不能返回一个有效的编码对应于<paramref name="name" />。</returns>
    /// <param name="name">请求的编码的名称。</param>
    [__DynamicallyInvokable]
    public abstract Encoding GetEncoding(string name);

    /// <summary>返回与指定代码页标识符关联的编码。</summary>
    /// <returns>与指定的代码页中，关联的编码，它或null如果此<see cref="T:System.Text.EncodingProvider" />不能返回一个有效的编码对应于<paramref name="codepage" />。</returns>
    /// <param name="codepage">请求的编码的代码页标识符。</param>
    [__DynamicallyInvokable]
    public abstract Encoding GetEncoding(int codepage);

    /// <summary>返回与指定名称关联的编码。参数指定一个错误处理程序，用于处理无法编码的字符和无法解码的字节序列。</summary>
    /// <returns>指定的名称，与关联的编码，它或null如果此<see cref="T:System.Text.EncodingProvider" />不能返回一个有效的编码对应于<paramref name="name" />。</returns>
    /// <param name="name">首选编码的名称。</param>
    /// <param name="encoderFallback">一个字符不能使用此编码进行编码时提供错误处理过程的对象。</param>
    /// <param name="decoderFallback">一个对象，在无法用当前编码对字节序列进行解码时，该对象可用来提供错误处理过程。</param>
    [__DynamicallyInvokable]
    public virtual Encoding GetEncoding(string name, EncoderFallback encoderFallback, DecoderFallback decoderFallback)
    {
      Encoding encoding = this.GetEncoding(name);
      if (encoding != null)
      {
        encoding = (Encoding) this.GetEncoding(name).Clone();
        encoding.EncoderFallback = encoderFallback;
        encoding.DecoderFallback = decoderFallback;
      }
      return encoding;
    }

    /// <summary>返回与指定代码页标识符关联的编码。参数指定一个错误处理程序，用于处理无法编码的字符和无法解码的字节序列。</summary>
    /// <returns>与指定的代码页中，关联的编码，它或null如果此<see cref="T:System.Text.EncodingProvider" />不能返回一个有效的编码对应于<paramref name="codepage" />。</returns>
    /// <param name="codepage">请求的编码的代码页标识符。</param>
    /// <param name="encoderFallback">一个字符不能使用此编码进行编码时提供错误处理过程的对象。</param>
    /// <param name="decoderFallback">一个对象，与该编码字节序列无法解码时提供错误处理过程。</param>
    [__DynamicallyInvokable]
    public virtual Encoding GetEncoding(int codepage, EncoderFallback encoderFallback, DecoderFallback decoderFallback)
    {
      Encoding encoding = this.GetEncoding(codepage);
      if (encoding != null)
      {
        encoding = (Encoding) this.GetEncoding(codepage).Clone();
        encoding.EncoderFallback = encoderFallback;
        encoding.DecoderFallback = decoderFallback;
      }
      return encoding;
    }

    internal static void AddProvider(EncodingProvider provider)
    {
      if (provider == null)
        throw new ArgumentNullException("provider");
      lock (EncodingProvider.s_InternalSyncObject)
      {
        if (EncodingProvider.s_providers == null)
        {
          EncodingProvider.s_providers = new EncodingProvider[1]
          {
            provider
          };
        }
        else
        {
          if (Array.IndexOf<EncodingProvider>(EncodingProvider.s_providers, provider) >= 0)
            return;
          EncodingProvider[] local_2 = new EncodingProvider[EncodingProvider.s_providers.Length + 1];
          Array.Copy((Array) EncodingProvider.s_providers, (Array) local_2, EncodingProvider.s_providers.Length);
          EncodingProvider[] temp_21 = local_2;
          int temp_25 = temp_21.Length - 1;
          EncodingProvider temp_26 = provider;
          temp_21[temp_25] = temp_26;
          EncodingProvider.s_providers = local_2;
        }
      }
    }

    internal static Encoding GetEncodingFromProvider(int codepage)
    {
      if (EncodingProvider.s_providers == null)
        return (Encoding) null;
      foreach (EncodingProvider sProvider in EncodingProvider.s_providers)
      {
        Encoding encoding = sProvider.GetEncoding(codepage);
        if (encoding != null)
          return encoding;
      }
      return (Encoding) null;
    }

    internal static Encoding GetEncodingFromProvider(string encodingName)
    {
      if (EncodingProvider.s_providers == null)
        return (Encoding) null;
      foreach (EncodingProvider sProvider in EncodingProvider.s_providers)
      {
        Encoding encoding = sProvider.GetEncoding(encodingName);
        if (encoding != null)
          return encoding;
      }
      return (Encoding) null;
    }

    internal static Encoding GetEncodingFromProvider(int codepage, EncoderFallback enc, DecoderFallback dec)
    {
      if (EncodingProvider.s_providers == null)
        return (Encoding) null;
      foreach (EncodingProvider sProvider in EncodingProvider.s_providers)
      {
        Encoding encoding = sProvider.GetEncoding(codepage, enc, dec);
        if (encoding != null)
          return encoding;
      }
      return (Encoding) null;
    }

    internal static Encoding GetEncodingFromProvider(string encodingName, EncoderFallback enc, DecoderFallback dec)
    {
      if (EncodingProvider.s_providers == null)
        return (Encoding) null;
      foreach (EncodingProvider sProvider in EncodingProvider.s_providers)
      {
        Encoding encoding = sProvider.GetEncoding(encodingName, enc, dec);
        if (encoding != null)
          return encoding;
      }
      return (Encoding) null;
    }
  }
}
