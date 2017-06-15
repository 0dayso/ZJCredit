// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComInterfaceType
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>标识如何向 COM 公开接口。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum ComInterfaceType
  {
    [__DynamicallyInvokable] InterfaceIsDual,
    [__DynamicallyInvokable] InterfaceIsIUnknown,
    [__DynamicallyInvokable] InterfaceIsIDispatch,
    [ComVisible(false), __DynamicallyInvokable] InterfaceIsIInspectable,
  }
}
