// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.FUNCFLAGS
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>标识定义函数属性的常数。</summary>
  [Flags]
  [__DynamicallyInvokable]
  [Serializable]
  public enum FUNCFLAGS : short
  {
    [__DynamicallyInvokable] FUNCFLAG_FRESTRICTED = 1,
    [__DynamicallyInvokable] FUNCFLAG_FSOURCE = 2,
    [__DynamicallyInvokable] FUNCFLAG_FBINDABLE = 4,
    [__DynamicallyInvokable] FUNCFLAG_FREQUESTEDIT = 8,
    [__DynamicallyInvokable] FUNCFLAG_FDISPLAYBIND = 16,
    [__DynamicallyInvokable] FUNCFLAG_FDEFAULTBIND = 32,
    [__DynamicallyInvokable] FUNCFLAG_FHIDDEN = 64,
    [__DynamicallyInvokable] FUNCFLAG_FUSESGETLASTERROR = 128,
    [__DynamicallyInvokable] FUNCFLAG_FDEFAULTCOLLELEM = 256,
    [__DynamicallyInvokable] FUNCFLAG_FUIDEFAULT = 512,
    [__DynamicallyInvokable] FUNCFLAG_FNONBROWSABLE = 1024,
    [__DynamicallyInvokable] FUNCFLAG_FREPLACEABLE = 2048,
    [__DynamicallyInvokable] FUNCFLAG_FIMMEDIATEBIND = 4096,
  }
}
