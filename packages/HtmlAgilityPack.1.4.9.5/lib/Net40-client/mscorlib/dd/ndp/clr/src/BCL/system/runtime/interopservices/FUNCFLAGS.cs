// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.FUNCFLAGS
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>请改用 <see cref="T:System.Runtime.InteropServices.ComTypes.FUNCFLAGS" />。</summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.FUNCFLAGS instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [Flags]
  [Serializable]
  public enum FUNCFLAGS : short
  {
    FUNCFLAG_FRESTRICTED = 1,
    FUNCFLAG_FSOURCE = 2,
    FUNCFLAG_FBINDABLE = 4,
    FUNCFLAG_FREQUESTEDIT = 8,
    FUNCFLAG_FDISPLAYBIND = 16,
    FUNCFLAG_FDEFAULTBIND = 32,
    FUNCFLAG_FHIDDEN = 64,
    FUNCFLAG_FUSESGETLASTERROR = 128,
    FUNCFLAG_FDEFAULTCOLLELEM = 256,
    FUNCFLAG_FUIDEFAULT = 512,
    FUNCFLAG_FNONBROWSABLE = 1024,
    FUNCFLAG_FREPLACEABLE = 2048,
    FUNCFLAG_FIMMEDIATEBIND = 4096,
  }
}
