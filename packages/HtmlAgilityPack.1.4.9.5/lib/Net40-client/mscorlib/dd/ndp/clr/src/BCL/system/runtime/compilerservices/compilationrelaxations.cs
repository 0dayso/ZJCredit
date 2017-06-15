// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.CompilationRelaxationsAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
  /// <summary>控制由公共语言运行时的实时 (JIT) 编译器生成的代码的严格性。</summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module | AttributeTargets.Class | AttributeTargets.Method)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class CompilationRelaxationsAttribute : Attribute
  {
    private int m_relaxations;

    /// <summary>获取构造当前对象时指定的编译松弛法。</summary>
    /// <returns>构造当前对象时指定的编译松弛法。将 <see cref="T:System.Runtime.CompilerServices.CompilationRelaxations" /> 枚举与 <see cref="P:System.Runtime.CompilerServices.CompilationRelaxationsAttribute.CompilationRelaxations" /> 属性一起使用。</returns>
    [__DynamicallyInvokable]
    public int CompilationRelaxations
    {
      [__DynamicallyInvokable] get
      {
        return this.m_relaxations;
      }
    }

    /// <summary>使用指定的编译松弛法初始化 <see cref="T:System.Runtime.CompilerServices.CompilationRelaxationsAttribute" /> 类的新实例。</summary>
    /// <param name="relaxations">编译松弛法。</param>
    [__DynamicallyInvokable]
    public CompilationRelaxationsAttribute(int relaxations)
    {
      this.m_relaxations = relaxations;
    }

    /// <summary>使用指定的 <see cref="T:System.Runtime.CompilerServices.CompilationRelaxations" /> 值初始化 <see cref="T:System.Runtime.CompilerServices.CompilationRelaxationsAttribute" /> 类的新实例。</summary>
    /// <param name="relaxations">
    /// <see cref="T:System.Runtime.CompilerServices.CompilationRelaxations" /> 值之一。</param>
    public CompilationRelaxationsAttribute(System.Runtime.CompilerServices.CompilationRelaxations relaxations)
    {
      this.m_relaxations = (int) relaxations;
    }
  }
}
