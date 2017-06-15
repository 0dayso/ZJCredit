// Decompiled with JetBrains decompiler
// Type: System.OrdinalComparer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Globalization;

namespace System
{
  [Serializable]
  internal sealed class OrdinalComparer : StringComparer, IWellKnownStringEqualityComparer
  {
    private bool _ignoreCase;

    internal OrdinalComparer(bool ignoreCase)
    {
      this._ignoreCase = ignoreCase;
    }

    public override int Compare(string x, string y)
    {
      if (x == y)
        return 0;
      if (x == null)
        return -1;
      if (y == null)
        return 1;
      if (this._ignoreCase)
        return string.Compare(x, y, StringComparison.OrdinalIgnoreCase);
      return string.CompareOrdinal(x, y);
    }

    public override bool Equals(string x, string y)
    {
      if (x == y)
        return true;
      if (x == null || y == null)
        return false;
      if (!this._ignoreCase)
        return x.Equals(y);
      if (x.Length != y.Length)
        return false;
      return string.Compare(x, y, StringComparison.OrdinalIgnoreCase) == 0;
    }

    public override int GetHashCode(string obj)
    {
      if (obj == null)
        throw new ArgumentNullException("obj");
      if (this._ignoreCase)
        return TextInfo.GetHashCodeOrdinalIgnoreCase(obj);
      return obj.GetHashCode();
    }

    public override bool Equals(object obj)
    {
      OrdinalComparer ordinalComparer = obj as OrdinalComparer;
      if (ordinalComparer == null)
        return false;
      return this._ignoreCase == ordinalComparer._ignoreCase;
    }

    public override int GetHashCode()
    {
      int hashCode = "OrdinalComparer".GetHashCode();
      if (!this._ignoreCase)
        return hashCode;
      return ~hashCode;
    }

    IEqualityComparer IWellKnownStringEqualityComparer.GetRandomizedEqualityComparer()
    {
      return (IEqualityComparer) new OrdinalRandomizedComparer(this._ignoreCase);
    }

    IEqualityComparer IWellKnownStringEqualityComparer.GetEqualityComparerForSerialization()
    {
      return (IEqualityComparer) this;
    }
  }
}
