// Decompiled with JetBrains decompiler
// Type: System.Reflection.AssemblyNameFlags
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>提供有关 <see cref="T:System.Reflection.Assembly" /> 引用的信息。</summary>
  [Flags]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum AssemblyNameFlags
  {
    [__DynamicallyInvokable] None = 0,
    [__DynamicallyInvokable] PublicKey = 1,
    EnableJITcompileOptimizer = 16384,
    EnableJITcompileTracking = 32768,
    [__DynamicallyInvokable] Retargetable = 256,
  }
}
