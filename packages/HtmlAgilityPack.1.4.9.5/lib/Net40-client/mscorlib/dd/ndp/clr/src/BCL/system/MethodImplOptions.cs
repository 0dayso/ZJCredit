// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.MethodImplOptions
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
  /// <summary>定义如何实现某方法的详细信息。</summary>
  [Flags]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum MethodImplOptions
  {
    Unmanaged = 4,
    ForwardRef = 16,
    [__DynamicallyInvokable] PreserveSig = 128,
    InternalCall = 4096,
    Synchronized = 32,
    [__DynamicallyInvokable] NoInlining = 8,
    [ComVisible(false), __DynamicallyInvokable] AggressiveInlining = 256,
    [__DynamicallyInvokable] NoOptimization = 64,
  }
}
