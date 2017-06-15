// Decompiled with JetBrains decompiler
// Type: System.Reflection.MethodImplAttributes
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>指定方法实现属性的标志。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum MethodImplAttributes
  {
    [__DynamicallyInvokable] IL = 0,
    [__DynamicallyInvokable] Managed = 0,
    [__DynamicallyInvokable] Native = 1,
    [__DynamicallyInvokable] OPTIL = 2,
    [__DynamicallyInvokable] CodeTypeMask = 3,
    [__DynamicallyInvokable] Runtime = 3,
    [__DynamicallyInvokable] ManagedMask = 4,
    [__DynamicallyInvokable] Unmanaged = 4,
    [__DynamicallyInvokable] NoInlining = 8,
    [__DynamicallyInvokable] ForwardRef = 16,
    [__DynamicallyInvokable] Synchronized = 32,
    [__DynamicallyInvokable] NoOptimization = 64,
    [__DynamicallyInvokable] PreserveSig = 128,
    [ComVisible(false), __DynamicallyInvokable] AggressiveInlining = 256,
    [__DynamicallyInvokable] InternalCall = 4096,
    MaxMethodImplVal = 65535,
  }
}
