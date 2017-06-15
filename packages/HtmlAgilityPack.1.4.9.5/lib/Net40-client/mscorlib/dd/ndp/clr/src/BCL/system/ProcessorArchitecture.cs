// Decompiled with JetBrains decompiler
// Type: System.Reflection.ProcessorArchitecture
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>标识可执行文件的目标平台的处理器和每字位数。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum ProcessorArchitecture
  {
    [__DynamicallyInvokable] None,
    [__DynamicallyInvokable] MSIL,
    [__DynamicallyInvokable] X86,
    [__DynamicallyInvokable] IA64,
    [__DynamicallyInvokable] Amd64,
    [__DynamicallyInvokable] Arm,
  }
}
