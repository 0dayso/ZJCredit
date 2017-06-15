// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.FieldMetadata
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Text;

namespace System.Diagnostics.Tracing
{
  internal class FieldMetadata
  {
    private readonly string name;
    private readonly int nameSize;
    private readonly EventFieldTags tags;
    private readonly byte[] custom;
    private readonly ushort fixedCount;
    private byte inType;
    private byte outType;

    public FieldMetadata(string name, TraceLoggingDataType type, EventFieldTags tags, bool variableCount)
      : this(name, type, tags, variableCount ? (byte) 64 : (byte) 0, (ushort) 0, (byte[]) null)
    {
    }

    public FieldMetadata(string name, TraceLoggingDataType type, EventFieldTags tags, ushort fixedCount)
      : this(name, type, tags, (byte) 32, fixedCount, (byte[]) null)
    {
    }

    public FieldMetadata(string name, TraceLoggingDataType type, EventFieldTags tags, byte[] custom)
      : this(name, type, tags, (byte) 96, custom == null ? (ushort) 0 : checked ((ushort) custom.Length), custom)
    {
    }

    private FieldMetadata(string name, TraceLoggingDataType dataType, EventFieldTags tags, byte countFlags, ushort fixedCount = 0, byte[] custom = null)
    {
      if (name == null)
        throw new ArgumentNullException("name", "This usually means that the object passed to Write is of a type that does not support being used as the top-level object in an event, e.g. a primitive or built-in type.");
      Statics.CheckName(name);
      int num = (int) (dataType & (TraceLoggingDataType) 31);
      this.name = name;
      this.nameSize = Encoding.UTF8.GetByteCount(this.name) + 1;
      this.inType = (byte) ((uint) num | (uint) countFlags);
      this.outType = (byte) ((int) dataType >> 8 & (int) sbyte.MaxValue);
      this.tags = tags;
      this.fixedCount = fixedCount;
      this.custom = custom;
      if ((int) countFlags != 0)
      {
        if (num == 0)
          throw new NotSupportedException(Environment.GetResourceString("EventSource_NotSupportedArrayOfNil"));
        if (num == 14)
          throw new NotSupportedException(Environment.GetResourceString("EventSource_NotSupportedArrayOfBinary"));
        if (num == 1 || num == 2)
          throw new NotSupportedException(Environment.GetResourceString("EventSource_NotSupportedArrayOfNullTerminatedString"));
      }
      if ((this.tags & (EventFieldTags) 268435455) != EventFieldTags.None)
        this.outType = (byte) ((uint) this.outType | 128U);
      if ((int) this.outType == 0)
        return;
      this.inType = (byte) ((uint) this.inType | 128U);
    }

    public void IncrementStructFieldCount()
    {
      this.inType = (byte) ((uint) this.inType | 128U);
      this.outType = (byte) ((uint) this.outType + 1U);
      if (((int) this.outType & (int) sbyte.MaxValue) == 0)
        throw new NotSupportedException(Environment.GetResourceString("EventSource_TooManyFields"));
    }

    public void Encode(ref int pos, byte[] metadata)
    {
      if (metadata != null)
        Encoding.UTF8.GetBytes(this.name, 0, this.name.Length, metadata, pos);
      pos += this.nameSize;
      if (metadata != null)
        metadata[pos] = this.inType;
      ++pos;
      if (((int) this.inType & 128) != 0)
      {
        if (metadata != null)
          metadata[pos] = this.outType;
        ++pos;
        if (((int) this.outType & 128) != 0)
          Statics.EncodeTags((int) this.tags, ref pos, metadata);
      }
      if (((int) this.inType & 32) == 0)
        return;
      if (metadata != null)
      {
        metadata[pos + 0] = (byte) this.fixedCount;
        metadata[pos + 1] = (byte) ((uint) this.fixedCount >> 8);
      }
      pos += 2;
      if (96 != ((int) this.inType & 96) || (int) this.fixedCount == 0)
        return;
      if (metadata != null)
        Buffer.BlockCopy((Array) this.custom, 0, (Array) metadata, pos, (int) this.fixedCount);
      pos += (int) this.fixedCount;
    }
  }
}
