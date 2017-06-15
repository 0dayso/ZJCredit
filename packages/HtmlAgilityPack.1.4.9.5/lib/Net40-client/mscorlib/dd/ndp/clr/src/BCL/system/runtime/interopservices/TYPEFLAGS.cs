// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.TYPEFLAGS
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>请改用 <see cref="T:System.Runtime.InteropServices.ComTypes.TYPEFLAGS" />。</summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.TYPEFLAGS instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [Flags]
  [Serializable]
  public enum TYPEFLAGS : short
  {
    TYPEFLAG_FAPPOBJECT = 1,
    TYPEFLAG_FCANCREATE = 2,
    TYPEFLAG_FLICENSED = 4,
    TYPEFLAG_FPREDECLID = 8,
    TYPEFLAG_FHIDDEN = 16,
    TYPEFLAG_FCONTROL = 32,
    TYPEFLAG_FDUAL = 64,
    TYPEFLAG_FNONEXTENSIBLE = 128,
    TYPEFLAG_FOLEAUTOMATION = 256,
    TYPEFLAG_FRESTRICTED = 512,
    TYPEFLAG_FAGGREGATABLE = 1024,
    TYPEFLAG_FREPLACEABLE = 2048,
    TYPEFLAG_FDISPATCHABLE = 4096,
    TYPEFLAG_FREVERSEBIND = 8192,
    TYPEFLAG_FPROXY = 16384,
  }
}
