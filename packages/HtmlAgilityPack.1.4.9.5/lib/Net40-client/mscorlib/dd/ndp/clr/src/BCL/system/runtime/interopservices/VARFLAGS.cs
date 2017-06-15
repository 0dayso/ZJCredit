// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.VARFLAGS
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>请改用 <see cref="T:System.Runtime.InteropServices.ComTypes.VARFLAGS" />。</summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.VARFLAGS instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [Flags]
  [Serializable]
  public enum VARFLAGS : short
  {
    VARFLAG_FREADONLY = 1,
    VARFLAG_FSOURCE = 2,
    VARFLAG_FBINDABLE = 4,
    VARFLAG_FREQUESTEDIT = 8,
    VARFLAG_FDISPLAYBIND = 16,
    VARFLAG_FDEFAULTBIND = 32,
    VARFLAG_FHIDDEN = 64,
    VARFLAG_FRESTRICTED = 128,
    VARFLAG_FDEFAULTCOLLELEM = 256,
    VARFLAG_FUIDEFAULT = 512,
    VARFLAG_FNONBROWSABLE = 1024,
    VARFLAG_FREPLACEABLE = 2048,
    VARFLAG_FIMMEDIATEBIND = 4096,
  }
}
