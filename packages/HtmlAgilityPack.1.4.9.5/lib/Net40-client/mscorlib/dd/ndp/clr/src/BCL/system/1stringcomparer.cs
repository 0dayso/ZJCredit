// Decompiled with JetBrains decompiler
// Type: System.CultureAwareComparer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Globalization;

namespace System
{
  [Serializable]
  internal sealed class CultureAwareComparer : StringComparer, IWellKnownStringEqualityComparer
  {
    private CompareInfo _compareInfo;
    private bool _ignoreCase;

    internal CultureAwareComparer(CultureInfo culture, bool ignoreCase)
    {
      this._compareInfo = culture.CompareInfo;
      this._ignoreCase = ignoreCase;
    }

    internal CultureAwareComparer(CompareInfo compareInfo, bool ignoreCase)
    {
      this._compareInfo = compareInfo;
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
      return this._compareInfo.Compare(x, y, this._ignoreCase ? CompareOptions.IgnoreCase : CompareOptions.None);
    }

    public override bool Equals(string x, string y)
    {
      if (x == y)
        return true;
      if (x == null || y == null)
        return false;
      return this._compareInfo.Compare(x, y, this._ignoreCase ? CompareOptions.IgnoreCase : CompareOptions.None) == 0;
    }

    public override int GetHashCode(string obj)
    {
      if (obj == null)
        throw new ArgumentNullException("obj");
      CompareOptions options = CompareOptions.None;
      if (this._ignoreCase)
        options |= CompareOptions.IgnoreCase;
      return this._compareInfo.GetHashCodeOfString(obj, options);
    }

    public override bool Equals(object obj)
    {
      CultureAwareComparer cultureAwareComparer = obj as CultureAwareComparer;
      if (cultureAwareComparer == null || this._ignoreCase != cultureAwareComparer._ignoreCase)
        return false;
      return this._compareInfo.Equals((object) cultureAwareComparer._compareInfo);
    }

    public override int GetHashCode()
    {
      int hashCode = this._compareInfo.GetHashCode();
      if (!this._ignoreCase)
        return hashCode;
      return ~hashCode;
    }

    IEqualityComparer IWellKnownStringEqualityComparer.GetRandomizedEqualityComparer()
    {
      return (IEqualityComparer) new CultureAwareRandomizedComparer(this._compareInfo, this._ignoreCase);
    }

    IEqualityComparer IWellKnownStringEqualityComparer.GetEqualityComparerForSerialization()
    {
      return (IEqualityComparer) this;
    }
  }
}
