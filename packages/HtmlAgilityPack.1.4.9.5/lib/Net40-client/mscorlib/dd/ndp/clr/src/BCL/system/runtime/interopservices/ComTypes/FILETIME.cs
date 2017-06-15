// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.FILETIME
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>表示从 1601 年 1 月 1 日起的 100 毫微秒间隔数。此结构是一个 64 位值。</summary>
  [__DynamicallyInvokable]
  public struct FILETIME
  {
    /// <summary>指定 FILETIME 的低 32 位。</summary>
    [__DynamicallyInvokable]
    public int dwLowDateTime;
    /// <summary>指定 FILETIME 的高 32 位。</summary>
    [__DynamicallyInvokable]
    public int dwHighDateTime;
  }
}
