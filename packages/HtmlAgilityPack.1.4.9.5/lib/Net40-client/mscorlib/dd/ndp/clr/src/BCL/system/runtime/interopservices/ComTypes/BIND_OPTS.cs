// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.BIND_OPTS
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>存储在名字对象绑定操作期间使用的参数。</summary>
  [__DynamicallyInvokable]
  public struct BIND_OPTS
  {
    /// <summary>指定 BIND_OPTS 结构的大小（以字节为单位）。</summary>
    [__DynamicallyInvokable]
    public int cbStruct;
    /// <summary>控制名字对象绑定操作的各个方面。</summary>
    [__DynamicallyInvokable]
    public int grfFlags;
    /// <summary>表示当打开包含由该名字对象标识的对象的文件时应使用的标志。</summary>
    [__DynamicallyInvokable]
    public int grfMode;
    /// <summary>指示调用方指定的用于完成绑定操作的时间（由 GetTickCount 函数返回的以毫秒为单位的时钟时间）。</summary>
    [__DynamicallyInvokable]
    public int dwTickCountDeadline;
  }
}
