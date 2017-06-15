// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.SByteEnumEqualityComparer`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Collections.Generic
{
  [Serializable]
  internal sealed class SByteEnumEqualityComparer<T> : EnumEqualityComparer<T>, ISerializable where T : struct
  {
    public SByteEnumEqualityComparer()
    {
    }

    public SByteEnumEqualityComparer(SerializationInfo information, StreamingContext context)
    {
    }

    public override int GetHashCode(T obj)
    {
      return ((sbyte) JitHelpers.UnsafeEnumCast<T>(obj)).GetHashCode();
    }
  }
}
