// Decompiled with JetBrains decompiler
// Type: System.Configuration.Assemblies.AssemblyHash
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Configuration.Assemblies
{
  /// <summary>代表程序集清单内容的哈希。</summary>
  [ComVisible(true)]
  [Obsolete("The AssemblyHash class has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")]
  [Serializable]
  public struct AssemblyHash : ICloneable
  {
    /// <summary>一个空 <see cref="T:System.Configuration.Assemblies.AssemblyHash" /> 对象。</summary>
    [Obsolete("The AssemblyHash class has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")]
    public static readonly AssemblyHash Empty = new AssemblyHash(AssemblyHashAlgorithm.None, (byte[]) null);
    private AssemblyHashAlgorithm _Algorithm;
    private byte[] _Value;

    /// <summary>获取或设置哈希算法。</summary>
    /// <returns>程序集哈希算法。</returns>
    [Obsolete("The AssemblyHash class has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")]
    public AssemblyHashAlgorithm Algorithm
    {
      get
      {
        return this._Algorithm;
      }
      set
      {
        this._Algorithm = value;
      }
    }

    /// <summary>用指定的哈希值初始化 <see cref="T:System.Configuration.Assemblies.AssemblyHash" /> 结构的新实例。哈希算法默认为 <see cref="F:System.Configuration.Assemblies.AssemblyHashAlgorithm.SHA1" />。</summary>
    /// <param name="value">哈希值。</param>
    [Obsolete("The AssemblyHash class has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")]
    public AssemblyHash(byte[] value)
    {
      this._Algorithm = AssemblyHashAlgorithm.SHA1;
      this._Value = (byte[]) null;
      if (value == null)
        return;
      int length = value.Length;
      this._Value = new byte[length];
      Array.Copy((Array) value, (Array) this._Value, length);
    }

    /// <summary>用指定的哈希算法和哈希值初始化 <see cref="T:System.Configuration.Assemblies.AssemblyHash" /> 结构的新实例。</summary>
    /// <param name="algorithm">用于生成哈希的算法。该参数的值来自 <see cref="T:System.Configuration.Assemblies.AssemblyHashAlgorithm" /> 枚举。</param>
    /// <param name="value">哈希值。</param>
    [Obsolete("The AssemblyHash class has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")]
    public AssemblyHash(AssemblyHashAlgorithm algorithm, byte[] value)
    {
      this._Algorithm = algorithm;
      this._Value = (byte[]) null;
      if (value == null)
        return;
      int length = value.Length;
      this._Value = new byte[length];
      Array.Copy((Array) value, (Array) this._Value, length);
    }

    /// <summary>获取哈希值。</summary>
    /// <returns>哈希值。</returns>
    [Obsolete("The AssemblyHash class has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")]
    public byte[] GetValue()
    {
      return this._Value;
    }

    /// <summary>设置哈希值。</summary>
    /// <param name="value">哈希值。</param>
    [Obsolete("The AssemblyHash class has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")]
    public void SetValue(byte[] value)
    {
      this._Value = value;
    }

    /// <summary>克隆该对象。</summary>
    /// <returns>该对象的精确副本。</returns>
    [Obsolete("The AssemblyHash class has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")]
    public object Clone()
    {
      return (object) new AssemblyHash(this._Algorithm, this._Value);
    }
  }
}
