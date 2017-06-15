// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.STATSTG
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>包含有关打开的存储、流或字节数组对象的统计信息。</summary>
  [__DynamicallyInvokable]
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public struct STATSTG
  {
    /// <summary>表示指向以 NULL 结尾的字符串的指针，该字符串包含此结构所描述的对象的名称。</summary>
    [__DynamicallyInvokable]
    public string pwcsName;
    /// <summary>指示存储对象的类型，该类型为 STGTY 枚举值之一。</summary>
    [__DynamicallyInvokable]
    public int type;
    /// <summary>指定流或字节数组的大小（以字节为单位）。</summary>
    [__DynamicallyInvokable]
    public long cbSize;
    /// <summary>指示此存储、流或字节数组的上次修改日期。</summary>
    [__DynamicallyInvokable]
    public FILETIME mtime;
    /// <summary>指示此存储、流或字节数组的创建时间。</summary>
    [__DynamicallyInvokable]
    public FILETIME ctime;
    /// <summary>指定此存储、流或字节数组的上次访问时间。</summary>
    [__DynamicallyInvokable]
    public FILETIME atime;
    /// <summary>指示打开对象时指定的访问模式。</summary>
    [__DynamicallyInvokable]
    public int grfMode;
    /// <summary>指示受该流或字节数组支持的区域锁定的类型。</summary>
    [__DynamicallyInvokable]
    public int grfLocksSupported;
    /// <summary>指示存储对象的类标识符。</summary>
    [__DynamicallyInvokable]
    public Guid clsid;
    /// <summary>指示存储对象的当前状态位（最近由 IStorage::SetStateBits 方法设置的值）。</summary>
    [__DynamicallyInvokable]
    public int grfStateBits;
    /// <summary>保留供将来使用。</summary>
    [__DynamicallyInvokable]
    public int reserved;
  }
}
