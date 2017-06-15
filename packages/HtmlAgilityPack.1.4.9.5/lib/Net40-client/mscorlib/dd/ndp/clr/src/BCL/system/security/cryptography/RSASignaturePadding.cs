// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.RSASignaturePadding
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.Cryptography
{
  public sealed class RSASignaturePadding : IEquatable<RSASignaturePadding>
  {
    private static readonly RSASignaturePadding s_pkcs1 = new RSASignaturePadding(RSASignaturePaddingMode.Pkcs1);
    private static readonly RSASignaturePadding s_pss = new RSASignaturePadding(RSASignaturePaddingMode.Pss);
    private readonly RSASignaturePaddingMode _mode;

    public static RSASignaturePadding Pkcs1
    {
      get
      {
        return RSASignaturePadding.s_pkcs1;
      }
    }

    public static RSASignaturePadding Pss
    {
      get
      {
        return RSASignaturePadding.s_pss;
      }
    }

    public RSASignaturePaddingMode Mode
    {
      get
      {
        return this._mode;
      }
    }

    private RSASignaturePadding(RSASignaturePaddingMode mode)
    {
      this._mode = mode;
    }

    public static bool operator ==(RSASignaturePadding left, RSASignaturePadding right)
    {
      if (left == null)
        return right == null;
      return left.Equals(right);
    }

    public static bool operator !=(RSASignaturePadding left, RSASignaturePadding right)
    {
      return !(left == right);
    }

    public override int GetHashCode()
    {
      return this._mode.GetHashCode();
    }

    public override bool Equals(object obj)
    {
      return this.Equals(obj as RSASignaturePadding);
    }

    public bool Equals(RSASignaturePadding other)
    {
      if (other != (RSASignaturePadding) null)
        return this._mode == other._mode;
      return false;
    }

    public override string ToString()
    {
      return this._mode.ToString();
    }
  }
}
