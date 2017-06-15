// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.DebuggerBrowsableAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Diagnostics
{
  /// <summary>确定是否在调试器变量窗口中显示成员以及如何显示成员。此类不能被继承。</summary>
  /// <filterpriority>1</filterpriority>
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class DebuggerBrowsableAttribute : Attribute
  {
    private DebuggerBrowsableState state;

    /// <summary>获取属性的显示状态。</summary>
    /// <returns>
    /// <see cref="T:System.Diagnostics.DebuggerBrowsableState" /> 值之一。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public DebuggerBrowsableState State
    {
      [__DynamicallyInvokable] get
      {
        return this.state;
      }
    }

    /// <summary>初始化 <see cref="T:System.Diagnostics.DebuggerBrowsableAttribute" /> 类的新实例。</summary>
    /// <param name="state">
    /// <see cref="T:System.Diagnostics.DebuggerBrowsableState" /> 值之一，指定成员的显示方式。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="state" /> 不是 <see cref="T:System.Diagnostics.DebuggerBrowsableState" /> 值之一。</exception>
    [__DynamicallyInvokable]
    public DebuggerBrowsableAttribute(DebuggerBrowsableState state)
    {
      if (state < DebuggerBrowsableState.Never || state > DebuggerBrowsableState.RootHidden)
        throw new ArgumentOutOfRangeException("state");
      this.state = state;
    }
  }
}
