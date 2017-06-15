// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.TimeSpanTypeInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Tracing
{
  internal sealed class TimeSpanTypeInfo : TraceLoggingTypeInfo<TimeSpan>
  {
    public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
    {
      collector.AddScalar(name, Statics.MakeDataType(TraceLoggingDataType.Int64, format));
    }

    public override void WriteData(TraceLoggingDataCollector collector, ref TimeSpan value)
    {
      collector.AddScalar(value.Ticks);
    }
  }
}
