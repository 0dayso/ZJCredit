// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.PARAMFLAG
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>请改用 <see cref="T:System.Runtime.InteropServices.ComTypes.PARAMFLAG" />。</summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.PARAMFLAG instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [Flags]
  [Serializable]
  public enum PARAMFLAG : short
  {
    PARAMFLAG_NONE = 0,
    PARAMFLAG_FIN = 1,
    PARAMFLAG_FOUT = 2,
    PARAMFLAG_FLCID = 4,
    PARAMFLAG_FRETVAL = 8,
    PARAMFLAG_FOPT = 16,
    PARAMFLAG_FHASDEFAULT = 32,
    PARAMFLAG_FHASCUSTDATA = 64,
  }
}
