// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.DebuggerVisualizerAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Diagnostics
{
  /// <summary>指定类型具有可视化工具。此类不能被继承。</summary>
  /// <filterpriority>1</filterpriority>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
  [ComVisible(true)]
  public sealed class DebuggerVisualizerAttribute : Attribute
  {
    private string visualizerObjectSourceName;
    private string visualizerName;
    private string description;
    private string targetName;
    private Type target;

    /// <summary>获取可视化工具对象源的完全限定类型名称。</summary>
    /// <returns>可视化工具对象源的完全限定类型名称。</returns>
    /// <filterpriority>2</filterpriority>
    public string VisualizerObjectSourceTypeName
    {
      get
      {
        return this.visualizerObjectSourceName;
      }
    }

    /// <summary>获取可视化工具的完全限定类型名称。</summary>
    /// <returns>完全限定的可视化工具类型名称。</returns>
    /// <filterpriority>2</filterpriority>
    public string VisualizerTypeName
    {
      get
      {
        return this.visualizerName;
      }
    }

    /// <summary>获取或设置可视化工具的说明。</summary>
    /// <returns>可视化工具的说明。</returns>
    /// <filterpriority>2</filterpriority>
    public string Description
    {
      get
      {
        return this.description;
      }
      set
      {
        this.description = value;
      }
    }

    /// <summary>获取或设置该特性应用于程序集级别时的目标类型。</summary>
    /// <returns>作为可视化工具的目标的类型。</returns>
    /// <exception cref="T:System.ArgumentNullException">该值因为是 null 而无法设置。</exception>
    /// <filterpriority>2</filterpriority>
    public Type Target
    {
      get
      {
        return this.target;
      }
      set
      {
        if (value == (Type) null)
          throw new ArgumentNullException("value");
        this.targetName = value.AssemblyQualifiedName;
        this.target = value;
      }
    }

    /// <summary>获取或设置该特性应用于程序集级别时的完全限定类型名称。</summary>
    /// <returns>目标类型的完全限定类型名称。</returns>
    /// <filterpriority>2</filterpriority>
    public string TargetTypeName
    {
      get
      {
        return this.targetName;
      }
      set
      {
        this.targetName = value;
      }
    }

    /// <summary>通过指定可视化工具的类型名称来初始化 <see cref="T:System.Diagnostics.DebuggerVisualizerAttribute" /> 类的新实例。</summary>
    /// <param name="visualizerTypeName">可视化工具的完全限定类型名称。</param>
    public DebuggerVisualizerAttribute(string visualizerTypeName)
    {
      this.visualizerName = visualizerTypeName;
    }

    /// <summary>通过指定可视化工具的类型名称和可视化工具对象源的类型名称，来初始化 <see cref="T:System.Diagnostics.DebuggerVisualizerAttribute" /> 类的新实例。</summary>
    /// <param name="visualizerTypeName">可视化工具的完全限定类型名称。</param>
    /// <param name="visualizerObjectSourceTypeName">可视化工具对象源的完全限定类型名称。</param>
    public DebuggerVisualizerAttribute(string visualizerTypeName, string visualizerObjectSourceTypeName)
    {
      this.visualizerName = visualizerTypeName;
      this.visualizerObjectSourceName = visualizerObjectSourceTypeName;
    }

    /// <summary>通过指定可视化工具的类型名称和可视化工具对象源的类型，来初始化 <see cref="T:System.Diagnostics.DebuggerVisualizerAttribute" /> 类的新实例。</summary>
    /// <param name="visualizerTypeName">可视化工具的完全限定类型名称。</param>
    /// <param name="visualizerObjectSource">可视化工具对象源的类型。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="visualizerObjectSource" /> 为 null。</exception>
    public DebuggerVisualizerAttribute(string visualizerTypeName, Type visualizerObjectSource)
    {
      if (visualizerObjectSource == (Type) null)
        throw new ArgumentNullException("visualizerObjectSource");
      this.visualizerName = visualizerTypeName;
      this.visualizerObjectSourceName = visualizerObjectSource.AssemblyQualifiedName;
    }

    /// <summary>通过指定可视化工具的类型来初始化 <see cref="T:System.Diagnostics.DebuggerVisualizerAttribute" /> 类的新实例。</summary>
    /// <param name="visualizer">可视化工具的类型。</param>
    /// <exception cref="T:System.ArgumentNullException">v<paramref name="isualizer" /> 为 null。</exception>
    public DebuggerVisualizerAttribute(Type visualizer)
    {
      if (visualizer == (Type) null)
        throw new ArgumentNullException("visualizer");
      this.visualizerName = visualizer.AssemblyQualifiedName;
    }

    /// <summary>通过指定可视化工具的类型和可视化工具对象源的类型来初始化 <see cref="T:System.Diagnostics.DebuggerVisualizerAttribute" /> 类的新实例。</summary>
    /// <param name="visualizer">可视化工具的类型。</param>
    /// <param name="visualizerObjectSource">可视化工具对象源的类型。</param>
    /// <exception cref="T:System.ArgumentNullException">v<paramref name="isualizer" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="visualizerObjectSource" /> 为 null。</exception>
    public DebuggerVisualizerAttribute(Type visualizer, Type visualizerObjectSource)
    {
      if (visualizer == (Type) null)
        throw new ArgumentNullException("visualizer");
      if (visualizerObjectSource == (Type) null)
        throw new ArgumentNullException("visualizerObjectSource");
      this.visualizerName = visualizer.AssemblyQualifiedName;
      this.visualizerObjectSourceName = visualizerObjectSource.AssemblyQualifiedName;
    }

    /// <summary>通过指定可视化工具的类型和可视化工具对象源的类型名称，来初始化 <see cref="T:System.Diagnostics.DebuggerVisualizerAttribute" /> 类的新实例。</summary>
    /// <param name="visualizer">可视化工具的类型。</param>
    /// <param name="visualizerObjectSourceTypeName">可视化工具对象源的完全限定类型名称。</param>
    /// <exception cref="T:System.ArgumentNullException">v<paramref name="isualizer" /> 为 null。</exception>
    public DebuggerVisualizerAttribute(Type visualizer, string visualizerObjectSourceTypeName)
    {
      if (visualizer == (Type) null)
        throw new ArgumentNullException("visualizer");
      this.visualizerName = visualizer.AssemblyQualifiedName;
      this.visualizerObjectSourceName = visualizerObjectSourceTypeName;
    }
  }
}
