// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.CharTypeInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Tracing
{
  internal sealed class CharTypeInfo : TraceLoggingTypeInfo<char>
  {
    public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
    {
      collector.AddScalar(name, Statics.Format16(format, TraceLoggingDataType.Char16));
    }

    public override void WriteData(TraceLoggingDataCollector collector, ref char value)
    {
      collector.AddScalar(value);
    }
  }
}
