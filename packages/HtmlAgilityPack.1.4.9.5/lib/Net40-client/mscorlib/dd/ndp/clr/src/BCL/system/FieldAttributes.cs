// Decompiled with JetBrains decompiler
// Type: System.Reflection.FieldAttributes
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>指定描述字段特性的标志。</summary>
  [Flags]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum FieldAttributes
  {
    [__DynamicallyInvokable] FieldAccessMask = 7,
    [__DynamicallyInvokable] PrivateScope = 0,
    [__DynamicallyInvokable] Private = 1,
    [__DynamicallyInvokable] FamANDAssem = 2,
    [__DynamicallyInvokable] Assembly = FamANDAssem | Private,
    [__DynamicallyInvokable] Family = 4,
    [__DynamicallyInvokable] FamORAssem = Family | Private,
    [__DynamicallyInvokable] Public = Family | FamANDAssem,
    [__DynamicallyInvokable] Static = 16,
    [__DynamicallyInvokable] InitOnly = 32,
    [__DynamicallyInvokable] Literal = 64,
    [__DynamicallyInvokable] NotSerialized = 128,
    [__DynamicallyInvokable] SpecialName = 512,
    [__DynamicallyInvokable] PinvokeImpl = 8192,
    ReservedMask = 38144,
    [__DynamicallyInvokable] RTSpecialName = 1024,
    [__DynamicallyInvokable] HasFieldMarshal = 4096,
    [__DynamicallyInvokable] HasDefault = 32768,
    [__DynamicallyInvokable] HasFieldRVA = 256,
  }
}
