// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.DefaultDependencyAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.CompilerServices
{
  /// <summary>向公共语言运行时 (CLR) 提供提示，指示加载依赖项的可能性。此类用在依赖程序集中，用于指示当父级未指定 <see cref="T:System.Runtime.CompilerServices.DependencyAttribute" /> 特性时应使用的提示。此类不能被继承。</summary>
  [AttributeUsage(AttributeTargets.Assembly)]
  [Serializable]
  public sealed class DefaultDependencyAttribute : Attribute
  {
    private LoadHint loadHint;

    /// <summary>获取 <see cref="T:System.Runtime.CompilerServices.LoadHint" /> 值，该值指示程序集何时加载依赖项。</summary>
    /// <returns>
    /// <see cref="T:System.Runtime.CompilerServices.LoadHint" /> 值之一。</returns>
    public LoadHint LoadHint
    {
      get
      {
        return this.loadHint;
      }
    }

    /// <summary>用指定的 <see cref="T:System.Runtime.CompilerServices.LoadHint" /> 绑定初始化 <see cref="T:System.Runtime.CompilerServices.DefaultDependencyAttribute" /> 类的新实例。</summary>
    /// <param name="loadHintArgument">
    /// <see cref="T:System.Runtime.CompilerServices.LoadHint" /> 值之一，它指示默认绑定首选项。</param>
    public DefaultDependencyAttribute(LoadHint loadHintArgument)
    {
      this.loadHint = loadHintArgument;
    }
  }
}
