﻿// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.RandomizedObjectEqualityComparer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Collections.Generic
{
  internal sealed class RandomizedObjectEqualityComparer : IEqualityComparer, IWellKnownStringEqualityComparer
  {
    private long _entropy;

    public RandomizedObjectEqualityComparer()
    {
      this._entropy = HashHelpers.GetEntropy();
    }

    public bool Equals(object x, object y)
    {
      if (x != null)
      {
        if (y != null)
          return x.Equals(y);
        return false;
      }
      return y == null;
    }

    [SecuritySafeCritical]
    public int GetHashCode(object obj)
    {
      if (obj == null)
        return 0;
      string str = obj as string;
      if (str == null)
        return obj.GetHashCode();
      string s = str;
      int length = s.Length;
      long additionalEntropy = this._entropy;
      return string.InternalMarvin32HashString(s, length, additionalEntropy);
    }

    public override bool Equals(object obj)
    {
      RandomizedObjectEqualityComparer equalityComparer = obj as RandomizedObjectEqualityComparer;
      if (equalityComparer != null)
        return this._entropy == equalityComparer._entropy;
      return false;
    }

    public override int GetHashCode()
    {
      return this.GetType().Name.GetHashCode() ^ (int) (this._entropy & (long) int.MaxValue);
    }

    IEqualityComparer IWellKnownStringEqualityComparer.GetRandomizedEqualityComparer()
    {
      return (IEqualityComparer) new RandomizedObjectEqualityComparer();
    }

    IEqualityComparer IWellKnownStringEqualityComparer.GetEqualityComparerForSerialization()
    {
      return (IEqualityComparer) null;
    }
  }
}
