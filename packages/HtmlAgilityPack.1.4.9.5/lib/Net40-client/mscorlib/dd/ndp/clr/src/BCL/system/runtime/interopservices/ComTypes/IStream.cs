// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.IStream
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>提供具有 ISequentialStream 功能的 IStream 接口的托管定义。</summary>
  [Guid("0000000c-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [__DynamicallyInvokable]
  [ComImport]
  public interface IStream
  {
    /// <summary>将指定的字节数从流对象读入从当前查找指针开始的内存。</summary>
    /// <param name="pv">此方法返回时，包含从流中读取的数据。该参数未经初始化即被传递。</param>
    /// <param name="cb">要从流对象中读取的字节数。</param>
    /// <param name="pcbRead">指向 ULONG 变量的指针，该变量接收从流对象中读取的实际字节数。</param>
    void Read([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1), Out] byte[] pv, int cb, IntPtr pcbRead);

    /// <summary>将指定数量的字节写入从当前查找指针开始的流对象。</summary>
    /// <param name="pv">要将此流写入的缓冲区。</param>
    /// <param name="cb">要写入此流的字节数。</param>
    /// <param name="pcbWritten">成功返回时包含写入此流对象的实际的字节数。如果调用方将此指针设置为 <see cref="F:System.IntPtr.Zero" />，则此方法不提供写入的实际字节数。</param>
    void Write([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] pv, int cb, IntPtr pcbWritten);

    /// <summary>将查找指针更改到相对于流的开头、流的结尾或当前查找指针的新位置。</summary>
    /// <param name="dlibMove">要添加到 <paramref name="dwOrigin" /> 的置换。</param>
    /// <param name="dwOrigin">查找的起始地址。该起始地址可以是文件的开头、当前查找指针或文件的结尾。</param>
    /// <param name="plibNewPosition">成功返回时包含从流的开头算起的查找指针的偏移量。</param>
    void Seek(long dlibMove, int dwOrigin, IntPtr plibNewPosition);

    /// <summary>更改流对象的大小。</summary>
    /// <param name="libNewSize">流的新大小以字节数表示。</param>
    [__DynamicallyInvokable]
    void SetSize(long libNewSize);

    /// <summary>将指定数量的字节从该流中的当前查找指针复制到另一个流中的当前查找指针。</summary>
    /// <param name="pstm">对目标流的引用。</param>
    /// <param name="cb">要从源流复制的字节数。</param>
    /// <param name="pcbRead">成功返回时包含从源读取的实际字节数。</param>
    /// <param name="pcbWritten">成功返回时包含写入到目标的实际字节数。</param>
    void CopyTo(IStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten);

    /// <summary>确保对在事务处理模式下打开的流对象所做的任何更改都能反映在父级存储中。</summary>
    /// <param name="grfCommitFlags">控制流对象更改的提交方式的值。</param>
    [__DynamicallyInvokable]
    void Commit(int grfCommitFlags);

    /// <summary>放弃自从上次 <see cref="M:System.Runtime.InteropServices.ComTypes.IStream.Commit(System.Int32)" /> 调用以来对事务处理流所做的所有更改。</summary>
    [__DynamicallyInvokable]
    void Revert();

    /// <summary>限制对流中指定字节范围的访问。</summary>
    /// <param name="libOffset">范围开始位置的字节偏移量。</param>
    /// <param name="cb">要限制的范围的长度（以字节为单位）。</param>
    /// <param name="dwLockType">所请求的对访问该范围的限制。</param>
    [__DynamicallyInvokable]
    void LockRegion(long libOffset, long cb, int dwLockType);

    /// <summary>移除对先前使用 <see cref="M:System.Runtime.InteropServices.ComTypes.IStream.LockRegion(System.Int64,System.Int64,System.Int32)" /> 方法限制的字节范围的访问限制。</summary>
    /// <param name="libOffset">范围开始位置的字节偏移量。</param>
    /// <param name="cb">要限制的范围的长度（以字节为单位）。</param>
    /// <param name="dwLockType">先前设置在范围上的访问限制。</param>
    [__DynamicallyInvokable]
    void UnlockRegion(long libOffset, long cb, int dwLockType);

    /// <summary>检索此流的 <see cref="T:System.Runtime.InteropServices.STATSTG" /> 结构。</summary>
    /// <param name="pstatstg">此方法返回时，包含描述此流对象的 STATSTG 结构。该参数未经初始化即被传递。</param>
    /// <param name="grfStatFlag">在 STATSTG 结构中指定此方法不返回的成员，这样就省去了一些内存分配操作。</param>
    [__DynamicallyInvokable]
    void Stat(out STATSTG pstatstg, int grfStatFlag);

    /// <summary>创建一个新的流对象，该流对象具有自己的查找指针且该指针与原始流引用相同的字节。</summary>
    /// <param name="ppstm">此方法返回时，包含新的流对象。该参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    void Clone(out IStream ppstm);
  }
}
