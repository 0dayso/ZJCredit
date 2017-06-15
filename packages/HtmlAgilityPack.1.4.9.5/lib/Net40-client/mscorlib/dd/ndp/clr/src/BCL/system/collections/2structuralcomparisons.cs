// Decompiled with JetBrains decompiler
// Type: System.Collections.StructuralComparer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Collections
{
  [Serializable]
  internal class StructuralComparer : IComparer
  {
    public int Compare(object x, object y)
    {
      if (x == null)
        return y != null ? -1 : 0;
      if (y == null)
        return 1;
      IStructuralComparable structuralComparable = x as IStructuralComparable;
      if (structuralComparable != null)
        return structuralComparable.CompareTo(y, (IComparer) this);
      return Comparer.Default.Compare(x, y);
    }
  }
}
