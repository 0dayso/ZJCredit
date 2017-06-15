// Decompiled with JetBrains decompiler
// Type: System.Collections.StructuralEqualityComparer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Collections
{
  [Serializable]
  internal class StructuralEqualityComparer : IEqualityComparer
  {
    public bool Equals(object x, object y)
    {
      if (x != null)
      {
        IStructuralEquatable structuralEquatable = x as IStructuralEquatable;
        if (structuralEquatable != null)
          return structuralEquatable.Equals(y, (IEqualityComparer) this);
        if (y != null)
          return x.Equals(y);
        return false;
      }
      return y == null;
    }

    public int GetHashCode(object obj)
    {
      if (obj == null)
        return 0;
      IStructuralEquatable structuralEquatable = obj as IStructuralEquatable;
      if (structuralEquatable != null)
        return structuralEquatable.GetHashCode((IEqualityComparer) this);
      return obj.GetHashCode();
    }
  }
}
