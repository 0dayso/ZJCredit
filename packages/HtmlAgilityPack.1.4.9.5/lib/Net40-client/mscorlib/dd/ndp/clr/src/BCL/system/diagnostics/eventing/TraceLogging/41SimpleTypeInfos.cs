// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.DateTimeOffsetTypeInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Tracing
{
  internal sealed class DateTimeOffsetTypeInfo : TraceLoggingTypeInfo<DateTimeOffset>
  {
    public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
    {
      TraceLoggingMetadataCollector metadataCollector = collector.AddGroup(name);
      string name1 = "Ticks";
      int num1 = (int) Statics.MakeDataType(TraceLoggingDataType.FileTime, format);
      metadataCollector.AddScalar(name1, (TraceLoggingDataType) num1);
      string name2 = "Offset";
      int num2 = 9;
      metadataCollector.AddScalar(name2, (TraceLoggingDataType) num2);
    }

    public override void WriteData(TraceLoggingDataCollector collector, ref DateTimeOffset value)
    {
      long ticks = value.Ticks;
      collector.AddScalar(ticks < 504911232000000000L ? 0L : ticks - 504911232000000000L);
      collector.AddScalar(value.Offset.Ticks);
    }
  }
}
