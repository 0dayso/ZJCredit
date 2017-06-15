// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.DSAParameters
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>包含 <see cref="T:System.Security.Cryptography.DSA" /> 算法的典型参数。</summary>
  [ComVisible(true)]
  [Serializable]
  public struct DSAParameters
  {
    /// <summary>指定 <see cref="T:System.Security.Cryptography.DSA" /> 算法的 P 参数。</summary>
    public byte[] P;
    /// <summary>指定 <see cref="T:System.Security.Cryptography.DSA" /> 算法的 Q 参数。</summary>
    public byte[] Q;
    /// <summary>指定 <see cref="T:System.Security.Cryptography.DSA" /> 算法的 G 参数。</summary>
    public byte[] G;
    /// <summary>指定 <see cref="T:System.Security.Cryptography.DSA" /> 算法的 Y 参数。</summary>
    public byte[] Y;
    /// <summary>指定 <see cref="T:System.Security.Cryptography.DSA" /> 算法的 J 参数。</summary>
    public byte[] J;
    /// <summary>指定 <see cref="T:System.Security.Cryptography.DSA" /> 算法的 X 参数。</summary>
    [NonSerialized]
    public byte[] X;
    /// <summary>指定 <see cref="T:System.Security.Cryptography.DSA" /> 算法的种子。</summary>
    public byte[] Seed;
    /// <summary>指定 <see cref="T:System.Security.Cryptography.DSA" /> 算法的计数器。</summary>
    public int Counter;
  }
}
