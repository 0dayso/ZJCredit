// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.UCOMIStream
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>请改用 <see cref="T:System.Runtime.InteropServices.ComTypes.IStream" />。</summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.IStream instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [Guid("0000000c-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface UCOMIStream
  {
    /// <summary>将指定的字节数从流对象读入从当前查找指针开始的内存。</summary>
    /// <param name="pv">成功返回时包含从流中读取的数据。</param>
    /// <param name="cb">要从流对象中读取的字节数。</param>
    /// <param name="pcbRead">指向 ULONG 变量的指针，该变量接收从流对象中读取的实际字节数。</param>
    void Read([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1), Out] byte[] pv, int cb, IntPtr pcbRead);

    /// <summary>将指定数量的字节写入从当前查找指针开始的流对象。</summary>
    /// <param name="pv">要写入此流的缓冲区。</param>
    /// <param name="cb">要写入此流的字节数。</param>
    /// <param name="pcbWritten">成功返回时包含写入此流对象的实际的字节数。调用方可以将此指针设置为 null，在此情况下该方法并不提供写入的实际字节数。</param>
    void Write([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] pv, int cb, IntPtr pcbWritten);

    /// <summary>将查找指针更改到相对于流的开头、流的结尾或当前查找指针的新位置。</summary>
    /// <param name="dlibMove">要添加到 <paramref name="dwOrigin" /> 的置换。</param>
    /// <param name="dwOrigin">指定查找的起始地址。该起始地址可以是文件的开头、当前查找指针或文件的结尾。</param>
    /// <param name="plibNewPosition">成功返回时包含从流的开头算起的查找指针的偏移量。</param>
    void Seek(long dlibMove, int dwOrigin, IntPtr plibNewPosition);

    /// <summary>更改流对象的大小。</summary>
    /// <param name="libNewSize">将流的新大小指定为字节数。</param>
    void SetSize(long libNewSize);

    /// <summary>将指定数量的字节从该流中的当前查找指针复制到另一个流中的当前查找指针。</summary>
    /// <param name="pstm">对目标流的引用。</param>
    /// <param name="cb">要从源流复制的字节数。</param>
    /// <param name="pcbRead">成功返回时包含从源读取的实际字节数。</param>
    /// <param name="pcbWritten">成功返回时包含写入到目标的实际字节数。</param>
    void CopyTo(UCOMIStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten);

    /// <summary>确保对以事务模式打开的流对象所做的任何更改都能反映在父级存储中。</summary>
    /// <param name="grfCommitFlags">控制提交对流对象的更改的方式。</param>
    void Commit(int grfCommitFlags);

    /// <summary>放弃自从上次 <see cref="M:System.Runtime.InteropServices.UCOMIStream.Commit(System.Int32)" /> 调用以来对事务处理流所做的所有更改。</summary>
    void Revert();

    /// <summary>限制对流中指定字节范围的访问。</summary>
    /// <param name="libOffset">范围开始位置的字节偏移量。</param>
    /// <param name="cb">要限制的范围的长度（以字节为单位）。</param>
    /// <param name="dwLockType">所请求的对访问该范围的限制。</param>
    void LockRegion(long libOffset, long cb, int dwLockType);

    /// <summary>移除对先前使用 <see cref="M:System.Runtime.InteropServices.UCOMIStream.LockRegion(System.Int64,System.Int64,System.Int32)" /> 限制的字节范围的访问限制。</summary>
    /// <param name="libOffset">范围开始位置的字节偏移量。</param>
    /// <param name="cb">要限制的范围的长度（以字节为单位）。</param>
    /// <param name="dwLockType">先前设置在范围上的访问限制。</param>
    void UnlockRegion(long libOffset, long cb, int dwLockType);

    /// <summary>检索此流的 <see cref="T:System.Runtime.InteropServices.STATSTG" /> 结构。</summary>
    /// <param name="pstatstg">成功返回时包含描述此流对象的 STATSTG 结构。</param>
    /// <param name="grfStatFlag">在 STATSTG 结构中指定此方法不返回的某些成员，从而节省一些内存分配操作。</param>
    void Stat(out STATSTG pstatstg, int grfStatFlag);

    /// <summary>创建一个新的流对象，该流对象具有自己的查找指针且该指针与原始流引用相同的字节。</summary>
    /// <param name="ppstm">成功返回时包含新的流对象。</param>
    void Clone(out UCOMIStream ppstm);
  }
}
