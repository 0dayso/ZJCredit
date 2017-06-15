// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.DebuggerHiddenAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Diagnostics
{
  /// <summary>指定 <see cref="T:System.Diagnostics.DebuggerHiddenAttribute" />。此类不能被继承。</summary>
  /// <filterpriority>1</filterpriority>
  [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class DebuggerHiddenAttribute : Attribute
  {
    /// <summary>初始化 <see cref="T:System.Diagnostics.DebuggerHiddenAttribute" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public DebuggerHiddenAttribute()
    {
    }
  }
}
