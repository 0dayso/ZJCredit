// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.TYPELIBATTR
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>请改用 <see cref="T:System.Runtime.InteropServices.ComTypes.TYPELIBATTR" />。</summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.TYPELIBATTR instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [Serializable]
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public struct TYPELIBATTR
  {
    /// <summary>表示类型库的全局唯一的库 ID。</summary>
    public Guid guid;
    /// <summary>表示类型库的区域设置 ID。</summary>
    public int lcid;
    /// <summary>表示类型库的目标硬件平台。</summary>
    public SYSKIND syskind;
    /// <summary>表示类型库的主版本号。</summary>
    public short wMajorVerNum;
    /// <summary>表示类型库的次版本号。</summary>
    public short wMinorVerNum;
    /// <summary>表示库标志。</summary>
    public LIBFLAGS wLibFlags;
  }
}
