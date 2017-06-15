// Decompiled with JetBrains decompiler
// Type: System.Reflection.AssemblyFlagsAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>为程序集指定 <see cref="T:System.Reflection.AssemblyNameFlags" /> 标志的按位组合，从而描述实时 (JIT) 编译器选项，该程序集是否可重定目标以及是否有完整或已标记化的公钥。此类不能被继承。</summary>
  [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class AssemblyFlagsAttribute : Attribute
  {
    private AssemblyNameFlags m_flags;

    /// <summary>获取一个无符号整数值，该值表示创建此属性实例时指定的 <see cref="T:System.Reflection.AssemblyNameFlags" /> 标志的组合。</summary>
    /// <returns>一个无符号整数值，表示 <see cref="T:System.Reflection.AssemblyNameFlags" /> 标志的按位组合。</returns>
    [Obsolete("This property has been deprecated. Please use AssemblyFlags instead. http://go.microsoft.com/fwlink/?linkid=14202")]
    [CLSCompliant(false)]
    public uint Flags
    {
      get
      {
        return (uint) this.m_flags;
      }
    }

    /// <summary>获取一个整数值，该值表示在创建此属性实例时指定的 <see cref="T:System.Reflection.AssemblyNameFlags" /> 标志的组合。</summary>
    /// <returns>一个整数值，表示 <see cref="T:System.Reflection.AssemblyNameFlags" /> 标志的按位组合。</returns>
    [__DynamicallyInvokable]
    public int AssemblyFlags
    {
      [__DynamicallyInvokable] get
      {
        return (int) this.m_flags;
      }
    }

    /// <summary>使用 <see cref="T:System.Reflection.AssemblyNameFlags" /> 标志（被强制转换为无符号整数值）的指定组合初始化 <see cref="T:System.Reflection.AssemblyFlagsAttribute" /> 类的新实例。</summary>
    /// <param name="flags">
    /// <see cref="T:System.Reflection.AssemblyNameFlags" /> 标志（被强制转换为无符号整数值）的按位组合，表示实时 (JIT) 编译器选项、寿命、程序集是否可重定目标以及是否有完整或已标记化的公钥。</param>
    [Obsolete("This constructor has been deprecated. Please use AssemblyFlagsAttribute(AssemblyNameFlags) instead. http://go.microsoft.com/fwlink/?linkid=14202")]
    [CLSCompliant(false)]
    public AssemblyFlagsAttribute(uint flags)
    {
      this.m_flags = (AssemblyNameFlags) flags;
    }

    /// <summary>使用 <see cref="T:System.Reflection.AssemblyNameFlags" /> 标志（被强制转换为整数值）的指定组合初始化 <see cref="T:System.Reflection.AssemblyFlagsAttribute" /> 类的新实例。</summary>
    /// <param name="assemblyFlags">
    /// <see cref="T:System.Reflection.AssemblyNameFlags" /> 标志（被强制转换为整数值）的按位组合，表示实时 (JIT) 编译器选项、寿命、程序集是否可重定目标以及是否有完整或已标记化的公钥。</param>
    [Obsolete("This constructor has been deprecated. Please use AssemblyFlagsAttribute(AssemblyNameFlags) instead. http://go.microsoft.com/fwlink/?linkid=14202")]
    public AssemblyFlagsAttribute(int assemblyFlags)
    {
      this.m_flags = (AssemblyNameFlags) assemblyFlags;
    }

    /// <summary>使用 <see cref="T:System.Reflection.AssemblyNameFlags" /> 标志的指定组合初始化 <see cref="T:System.Reflection.AssemblyFlagsAttribute" /> 类的新实例。</summary>
    /// <param name="assemblyFlags">
    /// <see cref="T:System.Reflection.AssemblyNameFlags" /> 标志的按位组合，表示实时 (JIT) 编译器选项、寿命、程序集是否可重定目标以及是否有完整或已标记化的公钥。</param>
    [__DynamicallyInvokable]
    public AssemblyFlagsAttribute(AssemblyNameFlags assemblyFlags)
    {
      this.m_flags = assemblyFlags;
    }
  }
}
