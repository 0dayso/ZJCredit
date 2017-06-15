// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.CallingConvention
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>指定调用在非托管代码中实现的方法所需的调用约定。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum CallingConvention
  {
    [__DynamicallyInvokable] Winapi = 1,
    [__DynamicallyInvokable] Cdecl = 2,
    [__DynamicallyInvokable] StdCall = 3,
    [__DynamicallyInvokable] ThisCall = 4,
    FastCall = 5,
  }
}
