// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.KeyValuePairTypeInfo`2
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;

namespace System.Diagnostics.Tracing
{
  internal sealed class KeyValuePairTypeInfo<K, V> : TraceLoggingTypeInfo<KeyValuePair<K, V>>
  {
    private readonly TraceLoggingTypeInfo<K> keyInfo;
    private readonly TraceLoggingTypeInfo<V> valueInfo;

    public KeyValuePairTypeInfo(List<Type> recursionCheck)
    {
      this.keyInfo = TraceLoggingTypeInfo<K>.GetInstance(recursionCheck);
      this.valueInfo = TraceLoggingTypeInfo<V>.GetInstance(recursionCheck);
    }

    public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
    {
      TraceLoggingMetadataCollector collector1 = collector.AddGroup(name);
      this.keyInfo.WriteMetadata(collector1, "Key", EventFieldFormat.Default);
      this.valueInfo.WriteMetadata(collector1, "Value", format);
    }

    public override void WriteData(TraceLoggingDataCollector collector, ref KeyValuePair<K, V> value)
    {
      K key = value.Key;
      V v = value.Value;
      this.keyInfo.WriteData(collector, ref key);
      this.valueInfo.WriteData(collector, ref v);
    }

    public override object GetData(object value)
    {
      Dictionary<string, object> dictionary = new Dictionary<string, object>();
      KeyValuePair<K, V> keyValuePair = (KeyValuePair<K, V>) value;
      string key1 = "Key";
      object data1 = this.keyInfo.GetData((object) keyValuePair.Key);
      dictionary.Add(key1, data1);
      string key2 = "Value";
      object data2 = this.valueInfo.GetData((object) keyValuePair.Value);
      dictionary.Add(key2, data2);
      return (object) dictionary;
    }
  }
}
