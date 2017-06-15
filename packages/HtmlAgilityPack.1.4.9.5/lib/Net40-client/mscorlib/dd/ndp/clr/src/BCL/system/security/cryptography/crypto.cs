// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.KeySizes
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>确定对称加密算法的有效密钥大小设置。</summary>
  [ComVisible(true)]
  public sealed class KeySizes
  {
    private int m_minSize;
    private int m_maxSize;
    private int m_skipSize;

    /// <summary>指定最小密钥大小（以位为单位）。</summary>
    /// <returns>最小密钥大小（以位为单位）。</returns>
    public int MinSize
    {
      get
      {
        return this.m_minSize;
      }
    }

    /// <summary>指定最大密钥大小（以位为单位）。</summary>
    /// <returns>最大密钥大小（以位为单位）。</returns>
    public int MaxSize
    {
      get
      {
        return this.m_maxSize;
      }
    }

    /// <summary>指定有效密钥大小之间的间隔（以位为单位）。</summary>
    /// <returns>有效密钥大小之间的间隔（以位为单位）。</returns>
    public int SkipSize
    {
      get
      {
        return this.m_skipSize;
      }
    }

    /// <summary>使用指定密钥值初始化 <see cref="T:System.Security.Cryptography.KeySizes" /> 类的新实例。</summary>
    /// <param name="minSize">最小有效密钥大小。</param>
    /// <param name="maxSize">最大有效密钥大小。</param>
    /// <param name="skipSize">有效密钥大小之间的间隔。</param>
    public KeySizes(int minSize, int maxSize, int skipSize)
    {
      this.m_minSize = minSize;
      this.m_maxSize = maxSize;
      this.m_skipSize = skipSize;
    }
  }
}
