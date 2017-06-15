// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.PInvokeMap
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  [Serializable]
  internal enum PInvokeMap
  {
    CharSetNotSpec = 0,
    NoMangle = 1,
    CharSetAnsi = 2,
    CharSetUnicode = 4,
    CharSetAuto = 6,
    CharSetMask = 6,
    BestFitEnabled = 16,
    BestFitDisabled = 32,
    PinvokeOLE = 32,
    BestFitMask = 48,
    BestFitUseAsm = 48,
    SupportsLastError = 64,
    CallConvWinapi = 256,
    CallConvCdecl = 512,
    CallConvStdcall = 768,
    CallConvThiscall = 1024,
    CallConvFastcall = 1280,
    CallConvMask = 1792,
    ThrowOnUnmappableCharEnabled = 4096,
    ThrowOnUnmappableCharDisabled = 8192,
    ThrowOnUnmappableCharMask = 12288,
    ThrowOnUnmappableCharUseAsm = 12288,
  }
}
