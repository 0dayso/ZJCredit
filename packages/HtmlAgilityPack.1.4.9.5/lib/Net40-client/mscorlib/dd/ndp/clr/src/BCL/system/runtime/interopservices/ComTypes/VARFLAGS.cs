// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.VARFLAGS
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>标识定义变量属性的常数。</summary>
  [Flags]
  [__DynamicallyInvokable]
  [Serializable]
  public enum VARFLAGS : short
  {
    [__DynamicallyInvokable] VARFLAG_FREADONLY = 1,
    [__DynamicallyInvokable] VARFLAG_FSOURCE = 2,
    [__DynamicallyInvokable] VARFLAG_FBINDABLE = 4,
    [__DynamicallyInvokable] VARFLAG_FREQUESTEDIT = 8,
    [__DynamicallyInvokable] VARFLAG_FDISPLAYBIND = 16,
    [__DynamicallyInvokable] VARFLAG_FDEFAULTBIND = 32,
    [__DynamicallyInvokable] VARFLAG_FHIDDEN = 64,
    [__DynamicallyInvokable] VARFLAG_FRESTRICTED = 128,
    [__DynamicallyInvokable] VARFLAG_FDEFAULTCOLLELEM = 256,
    [__DynamicallyInvokable] VARFLAG_FUIDEFAULT = 512,
    [__DynamicallyInvokable] VARFLAG_FNONBROWSABLE = 1024,
    [__DynamicallyInvokable] VARFLAG_FREPLACEABLE = 2048,
    [__DynamicallyInvokable] VARFLAG_FIMMEDIATEBIND = 4096,
  }
}
