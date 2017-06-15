// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.DebuggerStepperBoundaryAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Diagnostics
{
  /// <summary>指示特性后面的代码将以运行模式而非单步执行模式执行。</summary>
  [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
  [ComVisible(true)]
  [Serializable]
  public sealed class DebuggerStepperBoundaryAttribute : Attribute
  {
  }
}
