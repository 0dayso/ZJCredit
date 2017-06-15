// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.IDLFLAG
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>请改用 <see cref="T:System.Runtime.InteropServices.ComTypes.IDLFLAG" />。</summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.IDLFLAG instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [Flags]
  [Serializable]
  public enum IDLFLAG : short
  {
    IDLFLAG_NONE = 0,
    IDLFLAG_FIN = 1,
    IDLFLAG_FOUT = 2,
    IDLFLAG_FLCID = 4,
    IDLFLAG_FRETVAL = 8,
  }
}
