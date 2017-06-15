// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.DebuggerStepThroughAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Diagnostics
{
  /// <summary>指示调试器逐句通过代码，而不是单步执行代码。此类不能被继承。</summary>
  /// <filterpriority>1</filterpriority>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class DebuggerStepThroughAttribute : Attribute
  {
    /// <summary>初始化 <see cref="T:System.Diagnostics.DebuggerStepThroughAttribute" /> 类的新实例。 </summary>
    [__DynamicallyInvokable]
    public DebuggerStepThroughAttribute()
    {
    }
  }
}
