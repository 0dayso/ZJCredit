// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.DebuggableAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Diagnostics
{
  /// <summary>修改运行时实时 (JIT) 调试的代码生成。此类不能被继承。</summary>
  /// <filterpriority>1</filterpriority>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module, AllowMultiple = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class DebuggableAttribute : Attribute
  {
    private DebuggableAttribute.DebuggingModes m_debuggingModes;

    /// <summary>获取指示运行时是否将在代码生成过程中跟踪调试器信息的值。</summary>
    /// <returns>如果运行时将在代码生成过程中跟踪调试器的信息，则为 true，否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    public bool IsJITTrackingEnabled
    {
      get
      {
        return (uint) (this.m_debuggingModes & DebuggableAttribute.DebuggingModes.Default) > 0U;
      }
    }

    /// <summary>获取指示运行时优化程序是否已禁用的值。</summary>
    /// <returns>如果运行时优化程序已禁用，则为 true，否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    public bool IsJITOptimizerDisabled
    {
      get
      {
        return (uint) (this.m_debuggingModes & DebuggableAttribute.DebuggingModes.DisableOptimizations) > 0U;
      }
    }

    /// <summary>获取属性的调试模式。</summary>
    /// <returns>
    /// <see cref="T:System.Diagnostics.DebuggableAttribute.DebuggingModes" /> 值的按位组合，它描述实时 (JIT) 编译器的调试模式。默认值为 <see cref="F:System.Diagnostics.DebuggableAttribute.DebuggingModes.Default" />。</returns>
    /// <filterpriority>2</filterpriority>
    public DebuggableAttribute.DebuggingModes DebuggingFlags
    {
      get
      {
        return this.m_debuggingModes;
      }
    }

    /// <summary>使用为实时 (JIT) 编译器指定的跟踪和优化选项来初始化 <see cref="T:System.Diagnostics.DebuggableAttribute" /> 类的新实例。</summary>
    /// <param name="isJITTrackingEnabled">如果启用调试，则为 true；否则为 false。</param>
    /// <param name="isJITOptimizerDisabled">如果禁用执行的优化程序，则为 true否则为 false。</param>
    public DebuggableAttribute(bool isJITTrackingEnabled, bool isJITOptimizerDisabled)
    {
      this.m_debuggingModes = DebuggableAttribute.DebuggingModes.None;
      if (isJITTrackingEnabled)
        this.m_debuggingModes = this.m_debuggingModes | DebuggableAttribute.DebuggingModes.Default;
      if (!isJITOptimizerDisabled)
        return;
      this.m_debuggingModes = this.m_debuggingModes | DebuggableAttribute.DebuggingModes.DisableOptimizations;
    }

    /// <summary>使用为实时 (JIT) 编译器指定的调试模式来初始化 <see cref="T:System.Diagnostics.DebuggableAttribute" /> 类的新实例。</summary>
    /// <param name="modes">
    /// <see cref="T:System.Diagnostics.DebuggableAttribute.DebuggingModes" /> 值的按位组合，它指定 JIT 编译器的调试模式。</param>
    [__DynamicallyInvokable]
    public DebuggableAttribute(DebuggableAttribute.DebuggingModes modes)
    {
      this.m_debuggingModes = modes;
    }

    /// <summary>指定实时 (JIT) 编译器的调试模式。</summary>
    [Flags]
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public enum DebuggingModes
    {
      [__DynamicallyInvokable] None = 0,
      [__DynamicallyInvokable] Default = 1,
      [__DynamicallyInvokable] DisableOptimizations = 256,
      [__DynamicallyInvokable] IgnoreSymbolStoreSequencePoints = 2,
      [__DynamicallyInvokable] EnableEditAndContinue = 4,
    }
  }
}
