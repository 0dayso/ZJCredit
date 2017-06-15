// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.RSAParameters
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>表示 <see cref="T:System.Security.Cryptography.RSA" /> 算法的标准参数。</summary>
  [ComVisible(true)]
  [Serializable]
  public struct RSAParameters
  {
    /// <summary>表示 <see cref="T:System.Security.Cryptography.RSA" /> 算法的 Exponent 参数。</summary>
    public byte[] Exponent;
    /// <summary>表示 <see cref="T:System.Security.Cryptography.RSA" /> 算法的 Modulus 参数。</summary>
    public byte[] Modulus;
    /// <summary>表示 <see cref="T:System.Security.Cryptography.RSA" /> 算法的 P 参数。</summary>
    [NonSerialized]
    public byte[] P;
    /// <summary>表示 <see cref="T:System.Security.Cryptography.RSA" /> 算法的 Q 参数。</summary>
    [NonSerialized]
    public byte[] Q;
    /// <summary>表示 <see cref="T:System.Security.Cryptography.RSA" /> 算法的 DP 参数。</summary>
    [NonSerialized]
    public byte[] DP;
    /// <summary>表示 <see cref="T:System.Security.Cryptography.RSA" /> 算法的 DQ 参数。</summary>
    [NonSerialized]
    public byte[] DQ;
    /// <summary>表示 <see cref="T:System.Security.Cryptography.RSA" /> 算法的 InverseQ 参数。</summary>
    [NonSerialized]
    public byte[] InverseQ;
    /// <summary>表示 <see cref="T:System.Security.Cryptography.RSA" /> 算法的 D 参数。</summary>
    [NonSerialized]
    public byte[] D;
  }
}
