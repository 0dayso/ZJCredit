// Decompiled with JetBrains decompiler
// Type: System.Text.InternalDecoderBestFitFallback
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Text
{
  [Serializable]
  internal sealed class InternalDecoderBestFitFallback : DecoderFallback
  {
    internal char cReplacement = '?';
    internal Encoding encoding;
    internal char[] arrayBestFit;

    public override int MaxCharCount
    {
      get
      {
        return 1;
      }
    }

    internal InternalDecoderBestFitFallback(Encoding encoding)
    {
      this.encoding = encoding;
      this.bIsMicrosoftBestFitFallback = true;
    }

    public override DecoderFallbackBuffer CreateFallbackBuffer()
    {
      return (DecoderFallbackBuffer) new InternalDecoderBestFitFallbackBuffer(this);
    }

    public override bool Equals(object value)
    {
      InternalDecoderBestFitFallback decoderBestFitFallback = value as InternalDecoderBestFitFallback;
      if (decoderBestFitFallback != null)
        return this.encoding.CodePage == decoderBestFitFallback.encoding.CodePage;
      return false;
    }

    public override int GetHashCode()
    {
      return this.encoding.CodePage;
    }
  }
}
