// Decompiled with JetBrains decompiler
// Type: System.Reflection.AssemblyAlgorithmIdAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Configuration.Assemblies;
using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>指定对程序集中的所有文件进行哈希计算的算法。此类不能被继承。</summary>
  [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
  [ComVisible(true)]
  public sealed class AssemblyAlgorithmIdAttribute : Attribute
  {
    private uint m_algId;

    /// <summary>获取程序集清单内容的哈希算法。</summary>
    /// <returns>表示程序集哈希算法的无符号整数。</returns>
    [CLSCompliant(false)]
    public uint AlgorithmId
    {
      get
      {
        return this.m_algId;
      }
    }

    /// <summary>用指定的哈希算法初始化 <see cref="T:System.Reflection.AssemblyAlgorithmIdAttribute" /> 类的新实例，使用 <see cref="T:System.Configuration.Assemblies.AssemblyHashAlgorithm" /> 的成员之一来表示哈希算法。</summary>
    /// <param name="algorithmId">表示哈希算法的 AssemblyHashAlgorithm 的成员。</param>
    public AssemblyAlgorithmIdAttribute(AssemblyHashAlgorithm algorithmId)
    {
      this.m_algId = (uint) algorithmId;
    }

    /// <summary>用指定的哈希算法初始化 <see cref="T:System.Reflection.AssemblyAlgorithmIdAttribute" /> 类的新实例，使用无符号整数来表示哈希算法。</summary>
    /// <param name="algorithmId">表示哈希算法的无符号整数。</param>
    [CLSCompliant(false)]
    public AssemblyAlgorithmIdAttribute(uint algorithmId)
    {
      this.m_algId = algorithmId;
    }
  }
}
