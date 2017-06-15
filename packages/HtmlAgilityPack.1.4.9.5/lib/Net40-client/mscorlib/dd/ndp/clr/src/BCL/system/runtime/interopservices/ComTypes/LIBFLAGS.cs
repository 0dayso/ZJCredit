// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.LIBFLAGS
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>定义应用于类型库的标志。</summary>
  [Flags]
  [__DynamicallyInvokable]
  [Serializable]
  public enum LIBFLAGS : short
  {
    [__DynamicallyInvokable] LIBFLAG_FRESTRICTED = 1,
    [__DynamicallyInvokable] LIBFLAG_FCONTROL = 2,
    [__DynamicallyInvokable] LIBFLAG_FHIDDEN = 4,
    [__DynamicallyInvokable] LIBFLAG_FHASDISKIMAGE = 8,
  }
}
