// Decompiled with JetBrains decompiler
// Type: System.Reflection.MethodAttributes
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>指定方法属性的标志。这些标志在 corhdr.h 文件中定义。</summary>
  [Flags]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum MethodAttributes
  {
    [__DynamicallyInvokable] MemberAccessMask = 7,
    [__DynamicallyInvokable] PrivateScope = 0,
    [__DynamicallyInvokable] Private = 1,
    [__DynamicallyInvokable] FamANDAssem = 2,
    [__DynamicallyInvokable] Assembly = FamANDAssem | Private,
    [__DynamicallyInvokable] Family = 4,
    [__DynamicallyInvokable] FamORAssem = Family | Private,
    [__DynamicallyInvokable] Public = Family | FamANDAssem,
    [__DynamicallyInvokable] Static = 16,
    [__DynamicallyInvokable] Final = 32,
    [__DynamicallyInvokable] Virtual = 64,
    [__DynamicallyInvokable] HideBySig = 128,
    [__DynamicallyInvokable] CheckAccessOnOverride = 512,
    [__DynamicallyInvokable] VtableLayoutMask = 256,
    [__DynamicallyInvokable] ReuseSlot = 0,
    [__DynamicallyInvokable] NewSlot = VtableLayoutMask,
    [__DynamicallyInvokable] Abstract = 1024,
    [__DynamicallyInvokable] SpecialName = 2048,
    [__DynamicallyInvokable] PinvokeImpl = 8192,
    [__DynamicallyInvokable] UnmanagedExport = 8,
    [__DynamicallyInvokable] RTSpecialName = 4096,
    ReservedMask = 53248,
    [__DynamicallyInvokable] HasSecurity = 16384,
    [__DynamicallyInvokable] RequireSecObject = 32768,
  }
}
