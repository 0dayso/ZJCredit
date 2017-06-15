// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.ConcurrentSetItem`2
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Tracing
{
  internal abstract class ConcurrentSetItem<KeyType, ItemType> where ItemType : ConcurrentSetItem<KeyType, ItemType>
  {
    public abstract int Compare(ItemType other);

    public abstract int Compare(KeyType key);
  }
}
