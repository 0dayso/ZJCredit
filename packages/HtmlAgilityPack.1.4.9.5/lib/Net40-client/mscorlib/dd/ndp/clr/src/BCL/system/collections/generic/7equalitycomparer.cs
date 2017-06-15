// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.ShortEnumEqualityComparer`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Collections.Generic
{
  [Serializable]
  internal sealed class ShortEnumEqualityComparer<T> : EnumEqualityComparer<T>, ISerializable where T : struct
  {
    public ShortEnumEqualityComparer()
    {
    }

    public ShortEnumEqualityComparer(SerializationInfo information, StreamingContext context)
    {
    }

    public override int GetHashCode(T obj)
    {
      return ((short) JitHelpers.UnsafeEnumCast<T>(obj)).GetHashCode();
    }
  }
}
