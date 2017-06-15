// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.TYPELIBATTR
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>标识特定类型库并为成员名称提供本地化支持。</summary>
  [__DynamicallyInvokable]
  [Serializable]
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public struct TYPELIBATTR
  {
    /// <summary>表示类型库的全局唯一的库 ID。</summary>
    [__DynamicallyInvokable]
    public Guid guid;
    /// <summary>表示类型库的区域设置 ID。</summary>
    [__DynamicallyInvokable]
    public int lcid;
    /// <summary>表示类型库的目标硬件平台。</summary>
    [__DynamicallyInvokable]
    public SYSKIND syskind;
    /// <summary>表示类型库的主版本号。</summary>
    [__DynamicallyInvokable]
    public short wMajorVerNum;
    /// <summary>表示类型库的次版本号。</summary>
    [__DynamicallyInvokable]
    public short wMinorVerNum;
    /// <summary>表示库标志。</summary>
    [__DynamicallyInvokable]
    public LIBFLAGS wLibFlags;
  }
}
