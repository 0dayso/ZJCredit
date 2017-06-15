// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.DISPPARAMS
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>包含通过 IDispatch::Invoke 传递给方法或属性的参数。</summary>
  [__DynamicallyInvokable]
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public struct DISPPARAMS
  {
    /// <summary>表示对参数数组的引用。</summary>
    [__DynamicallyInvokable]
    public IntPtr rgvarg;
    /// <summary>表示命名参数的调度 ID。</summary>
    [__DynamicallyInvokable]
    public IntPtr rgdispidNamedArgs;
    /// <summary>表示参数的计数。</summary>
    [__DynamicallyInvokable]
    public int cArgs;
    /// <summary>表示命名参数的计数。</summary>
    [__DynamicallyInvokable]
    public int cNamedArgs;
  }
}
