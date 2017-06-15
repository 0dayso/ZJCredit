// Decompiled with JetBrains decompiler
// Type: System.Reflection.PInvokeAttributes
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Reflection
{
  [Flags]
  [Serializable]
  internal enum PInvokeAttributes
  {
    NoMangle = 1,
    CharSetMask = 6,
    CharSetNotSpec = 0,
    CharSetAnsi = 2,
    CharSetUnicode = 4,
    CharSetAuto = CharSetUnicode | CharSetAnsi,
    BestFitUseAssem = 0,
    BestFitEnabled = 16,
    BestFitDisabled = 32,
    BestFitMask = BestFitDisabled | BestFitEnabled,
    ThrowOnUnmappableCharUseAssem = 0,
    ThrowOnUnmappableCharEnabled = 4096,
    ThrowOnUnmappableCharDisabled = 8192,
    ThrowOnUnmappableCharMask = ThrowOnUnmappableCharDisabled | ThrowOnUnmappableCharEnabled,
    SupportsLastError = 64,
    CallConvMask = 1792,
    CallConvWinapi = 256,
    CallConvCdecl = 512,
    CallConvStdcall = CallConvCdecl | CallConvWinapi,
    CallConvThiscall = 1024,
    CallConvFastcall = CallConvThiscall | CallConvWinapi,
    MaxValue = 65535,
  }
}
