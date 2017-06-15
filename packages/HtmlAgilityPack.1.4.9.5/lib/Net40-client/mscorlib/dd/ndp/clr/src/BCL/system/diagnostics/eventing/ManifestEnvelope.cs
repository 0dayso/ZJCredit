// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.ManifestEnvelope
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Tracing
{
  internal struct ManifestEnvelope
  {
    public const int MaxChunkSize = 65280;
    public ManifestEnvelope.ManifestFormats Format;
    public byte MajorVersion;
    public byte MinorVersion;
    public byte Magic;
    public ushort TotalChunks;
    public ushort ChunkNumber;

    public enum ManifestFormats : byte
    {
      SimpleXmlFormat = 1,
    }
  }
}
