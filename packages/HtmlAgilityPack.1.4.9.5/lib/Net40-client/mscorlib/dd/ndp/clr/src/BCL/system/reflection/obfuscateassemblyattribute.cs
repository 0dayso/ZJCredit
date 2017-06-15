// Decompiled with JetBrains decompiler
// Type: System.Reflection.ObfuscateAssemblyAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>指示模糊处理工具对适当的程序集类型使用其标准模糊处理规则。</summary>
  [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
  [ComVisible(true)]
  public sealed class ObfuscateAssemblyAttribute : Attribute
  {
    private bool m_strip = true;
    private bool m_assemblyIsPrivate;

    /// <summary>获取一个 <see cref="T:System.Boolean" /> 值，该值指示程序集是否标记为私有。</summary>
    /// <returns>如果程序集标记为私有，则为 true；否则为 false。</returns>
    public bool AssemblyIsPrivate
    {
      get
      {
        return this.m_assemblyIsPrivate;
      }
    }

    /// <summary>获取或设置一个 <see cref="T:System.Boolean" /> 值，该值指示模糊处理工具是否应在处理后移除该特性。</summary>
    /// <returns>如果模糊处理工具应在处理后移除该特性，则为 true；否则为 false。此属性的默认值为 true。</returns>
    public bool StripAfterObfuscation
    {
      get
      {
        return this.m_strip;
      }
      set
      {
        this.m_strip = value;
      }
    }

    /// <summary>初始化 <see cref="T:System.Reflection.ObfuscateAssemblyAttribute" /> 类的新实例，指定要模糊处理的程序集是公共的还是私有的。</summary>
    /// <param name="assemblyIsPrivate">如果程序集在某个应用程序范围内使用，则为 true；否则为 false。</param>
    public ObfuscateAssemblyAttribute(bool assemblyIsPrivate)
    {
      this.m_assemblyIsPrivate = assemblyIsPrivate;
    }
  }
}
