// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.DependencyAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.CompilerServices
{
  /// <summary>指示引用程序集将在何时加载依赖项。此类不能被继承。</summary>
  [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
  [Serializable]
  public sealed class DependencyAttribute : Attribute
  {
    private string dependentAssembly;
    private LoadHint loadHint;

    /// <summary>获取依赖程序集的值。</summary>
    /// <returns>依赖程序集的名称。</returns>
    public string DependentAssembly
    {
      get
      {
        return this.dependentAssembly;
      }
    }

    /// <summary>获取 <see cref="T:System.Runtime.CompilerServices.LoadHint" /> 值，该值指示程序集将在何时加载依赖项。</summary>
    /// <returns>
    /// <see cref="T:System.Runtime.CompilerServices.LoadHint" /> 值之一。</returns>
    public LoadHint LoadHint
    {
      get
      {
        return this.loadHint;
      }
    }

    /// <summary>使用指定的 <see cref="T:System.Runtime.CompilerServices.LoadHint" /> 值初始化 <see cref="T:System.Runtime.CompilerServices.DependencyAttribute" /> 类的新实例。</summary>
    /// <param name="dependentAssemblyArgument">要绑定到的依赖程序集。</param>
    /// <param name="loadHintArgument">
    /// <see cref="T:System.Runtime.CompilerServices.LoadHint" /> 值之一。</param>
    public DependencyAttribute(string dependentAssemblyArgument, LoadHint loadHintArgument)
    {
      this.dependentAssembly = dependentAssemblyArgument;
      this.loadHint = loadHintArgument;
    }
  }
}
