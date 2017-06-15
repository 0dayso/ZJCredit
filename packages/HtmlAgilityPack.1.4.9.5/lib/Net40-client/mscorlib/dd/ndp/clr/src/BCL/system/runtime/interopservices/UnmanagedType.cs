// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.UnmanagedType
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>指定如何将参数或字段封送到非托管代码。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum UnmanagedType
  {
    [__DynamicallyInvokable] Bool = 2,
    [__DynamicallyInvokable] I1 = 3,
    [__DynamicallyInvokable] U1 = 4,
    [__DynamicallyInvokable] I2 = 5,
    [__DynamicallyInvokable] U2 = 6,
    [__DynamicallyInvokable] I4 = 7,
    [__DynamicallyInvokable] U4 = 8,
    [__DynamicallyInvokable] I8 = 9,
    [__DynamicallyInvokable] U8 = 10,
    [__DynamicallyInvokable] R4 = 11,
    [__DynamicallyInvokable] R8 = 12,
    [__DynamicallyInvokable] Currency = 15,
    [__DynamicallyInvokable] BStr = 19,
    [__DynamicallyInvokable] LPStr = 20,
    [__DynamicallyInvokable] LPWStr = 21,
    [__DynamicallyInvokable] LPTStr = 22,
    [__DynamicallyInvokable] ByValTStr = 23,
    [__DynamicallyInvokable] IUnknown = 25,
    [__DynamicallyInvokable] IDispatch = 26,
    [__DynamicallyInvokable] Struct = 27,
    [__DynamicallyInvokable] Interface = 28,
    [__DynamicallyInvokable] SafeArray = 29,
    [__DynamicallyInvokable] ByValArray = 30,
    [__DynamicallyInvokable] SysInt = 31,
    [__DynamicallyInvokable] SysUInt = 32,
    [__DynamicallyInvokable] VBByRefStr = 34,
    [__DynamicallyInvokable] AnsiBStr = 35,
    [__DynamicallyInvokable] TBStr = 36,
    [__DynamicallyInvokable] VariantBool = 37,
    [__DynamicallyInvokable] FunctionPtr = 38,
    [__DynamicallyInvokable] AsAny = 40,
    [__DynamicallyInvokable] LPArray = 42,
    [__DynamicallyInvokable] LPStruct = 43,
    [__DynamicallyInvokable] CustomMarshaler = 44,
    [__DynamicallyInvokable] Error = 45,
    [ComVisible(false), __DynamicallyInvokable] IInspectable = 46,
    [ComVisible(false), __DynamicallyInvokable] HString = 47,
  }
}
