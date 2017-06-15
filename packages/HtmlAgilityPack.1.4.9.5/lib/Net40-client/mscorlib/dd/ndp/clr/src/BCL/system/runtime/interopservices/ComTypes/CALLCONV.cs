// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.CALLCONV
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>标识 METHODDATA 结构中描述的方法所使用的调用约定。</summary>
  [__DynamicallyInvokable]
  [Serializable]
  public enum CALLCONV
  {
    [__DynamicallyInvokable] CC_CDECL = 1,
    [__DynamicallyInvokable] CC_MSCPASCAL = 2,
    [__DynamicallyInvokable] CC_PASCAL = 2,
    [__DynamicallyInvokable] CC_MACPASCAL = 3,
    [__DynamicallyInvokable] CC_STDCALL = 4,
    [__DynamicallyInvokable] CC_RESERVED = 5,
    [__DynamicallyInvokable] CC_SYSCALL = 6,
    [__DynamicallyInvokable] CC_MPWCDECL = 7,
    [__DynamicallyInvokable] CC_MPWPASCAL = 8,
    [__DynamicallyInvokable] CC_MAX = 9,
  }
}
