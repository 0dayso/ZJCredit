﻿// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.NameInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Threading;

namespace System.Diagnostics.Tracing
{
  internal sealed class NameInfo : ConcurrentSetItem<KeyValuePair<string, EventTags>, NameInfo>
  {
    private static int lastIdentity = 184549376;
    internal readonly string name;
    internal readonly EventTags tags;
    internal readonly int identity;
    internal readonly byte[] nameMetadata;

    public NameInfo(string name, EventTags tags, int typeMetadataSize)
    {
      this.name = name;
      this.tags = tags & (EventTags) 268435455;
      this.identity = Interlocked.Increment(ref NameInfo.lastIdentity);
      int pos1 = 0;
      Statics.EncodeTags((int) this.tags, ref pos1, (byte[]) null);
      this.nameMetadata = Statics.MetadataForString(name, pos1, 0, typeMetadataSize);
      int pos2 = 2;
      Statics.EncodeTags((int) this.tags, ref pos2, this.nameMetadata);
    }

    internal static void ReserveEventIDsBelow(int eventId)
    {
      int num1;
      int num2;
      do
      {
        num1 = NameInfo.lastIdentity;
        num2 = Math.Max((NameInfo.lastIdentity & -16777216) + eventId, num1);
      }
      while (Interlocked.CompareExchange(ref NameInfo.lastIdentity, num2, num1) != num1);
    }

    public override int Compare(NameInfo other)
    {
      return this.Compare(other.name, other.tags);
    }

    public override int Compare(KeyValuePair<string, EventTags> key)
    {
      return this.Compare(key.Key, key.Value & (EventTags) 268435455);
    }

    private int Compare(string otherName, EventTags otherTags)
    {
      int num = StringComparer.Ordinal.Compare(this.name, otherName);
      if (num == 0 && this.tags != otherTags)
        num = this.tags < otherTags ? -1 : 1;
      return num;
    }
  }
}
