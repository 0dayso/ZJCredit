// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.IntPtrTypeInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Tracing
{
  internal sealed class IntPtrTypeInfo : TraceLoggingTypeInfo<IntPtr>
  {
    public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
    {
      collector.AddScalar(name, Statics.FormatPtr(format, Statics.IntPtrType));
    }

    public override void WriteData(TraceLoggingDataCollector collector, ref IntPtr value)
    {
      collector.AddScalar(value);
    }
  }
}
