// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.RSAEncryptionPadding
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.Cryptography
{
  public sealed class RSAEncryptionPadding : IEquatable<RSAEncryptionPadding>
  {
    private static readonly RSAEncryptionPadding s_pkcs1 = new RSAEncryptionPadding(RSAEncryptionPaddingMode.Pkcs1, new HashAlgorithmName());
    private static readonly RSAEncryptionPadding s_oaepSHA1 = RSAEncryptionPadding.CreateOaep(HashAlgorithmName.SHA1);
    private static readonly RSAEncryptionPadding s_oaepSHA256 = RSAEncryptionPadding.CreateOaep(HashAlgorithmName.SHA256);
    private static readonly RSAEncryptionPadding s_oaepSHA384 = RSAEncryptionPadding.CreateOaep(HashAlgorithmName.SHA384);
    private static readonly RSAEncryptionPadding s_oaepSHA512 = RSAEncryptionPadding.CreateOaep(HashAlgorithmName.SHA512);
    private RSAEncryptionPaddingMode _mode;
    private HashAlgorithmName _oaepHashAlgorithm;

    public static RSAEncryptionPadding Pkcs1
    {
      get
      {
        return RSAEncryptionPadding.s_pkcs1;
      }
    }

    public static RSAEncryptionPadding OaepSHA1
    {
      get
      {
        return RSAEncryptionPadding.s_oaepSHA1;
      }
    }

    public static RSAEncryptionPadding OaepSHA256
    {
      get
      {
        return RSAEncryptionPadding.s_oaepSHA256;
      }
    }

    public static RSAEncryptionPadding OaepSHA384
    {
      get
      {
        return RSAEncryptionPadding.s_oaepSHA384;
      }
    }

    public static RSAEncryptionPadding OaepSHA512
    {
      get
      {
        return RSAEncryptionPadding.s_oaepSHA512;
      }
    }

    public RSAEncryptionPaddingMode Mode
    {
      get
      {
        return this._mode;
      }
    }

    public HashAlgorithmName OaepHashAlgorithm
    {
      get
      {
        return this._oaepHashAlgorithm;
      }
    }

    private RSAEncryptionPadding(RSAEncryptionPaddingMode mode, HashAlgorithmName oaepHashAlgorithm)
    {
      this._mode = mode;
      this._oaepHashAlgorithm = oaepHashAlgorithm;
    }

    public static bool operator ==(RSAEncryptionPadding left, RSAEncryptionPadding right)
    {
      if (left == null)
        return right == null;
      return left.Equals(right);
    }

    public static bool operator !=(RSAEncryptionPadding left, RSAEncryptionPadding right)
    {
      return !(left == right);
    }

    public static RSAEncryptionPadding CreateOaep(HashAlgorithmName hashAlgorithm)
    {
      if (string.IsNullOrEmpty(hashAlgorithm.Name))
        throw new ArgumentException(Environment.GetResourceString("Cryptography_HashAlgorithmNameNullOrEmpty"), "hashAlgorithm");
      return new RSAEncryptionPadding(RSAEncryptionPaddingMode.Oaep, hashAlgorithm);
    }

    public override int GetHashCode()
    {
      return RSAEncryptionPadding.CombineHashCodes(this._mode.GetHashCode(), this._oaepHashAlgorithm.GetHashCode());
    }

    private static int CombineHashCodes(int h1, int h2)
    {
      return (h1 << 5) + h1 ^ h2;
    }

    public override bool Equals(object obj)
    {
      return this.Equals(obj as RSAEncryptionPadding);
    }

    public bool Equals(RSAEncryptionPadding other)
    {
      if (other != (RSAEncryptionPadding) null && this._mode == other._mode)
        return this._oaepHashAlgorithm == other._oaepHashAlgorithm;
      return false;
    }

    public override string ToString()
    {
      return this._mode.ToString() + this._oaepHashAlgorithm.Name;
    }
  }
}
