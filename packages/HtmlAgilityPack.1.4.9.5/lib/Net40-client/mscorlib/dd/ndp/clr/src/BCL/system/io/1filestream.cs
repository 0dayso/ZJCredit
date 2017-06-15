// Decompiled with JetBrains decompiler
// Type: System.IO.FileStream
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System.Diagnostics.Tracing;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
  /// <summary>为文件提供 <see cref="T:System.IO.Stream" />，既支持同步读写操作，也支持异步读写操作。若要浏览此类型的.NET Framework 源代码，请参阅 Reference Source。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  public class FileStream : Stream
  {
    internal const int DefaultBufferSize = 4096;
    private const bool _canUseAsync = true;
    private byte[] _buffer;
    private string _fileName;
    private bool _isAsync;
    private bool _canRead;
    private bool _canWrite;
    private bool _canSeek;
    private bool _exposedHandle;
    private bool _isPipe;
    private int _readPos;
    private int _readLen;
    private int _writePos;
    private int _bufferSize;
    [SecurityCritical]
    private SafeFileHandle _handle;
    private long _pos;
    private long _appendStart;
    private static AsyncCallback s_endReadTask;
    private static AsyncCallback s_endWriteTask;
    private static Action<object> s_cancelReadHandler;
    private static Action<object> s_cancelWriteHandler;
    private const int FILE_ATTRIBUTE_NORMAL = 128;
    private const int FILE_ATTRIBUTE_ENCRYPTED = 16384;
    private const int FILE_FLAG_OVERLAPPED = 1073741824;
    internal const int GENERIC_READ = -2147483648;
    private const int GENERIC_WRITE = 1073741824;
    private const int FILE_BEGIN = 0;
    private const int FILE_CURRENT = 1;
    private const int FILE_END = 2;
    internal const int ERROR_BROKEN_PIPE = 109;
    internal const int ERROR_NO_DATA = 232;
    private const int ERROR_HANDLE_EOF = 38;
    private const int ERROR_INVALID_PARAMETER = 87;
    private const int ERROR_IO_PENDING = 997;

    /// <summary>获取一个值，该值指示当前流是否支持读取。</summary>
    /// <returns>如果流支持读取，则为 true；如果流已关闭或是通过只写访问方式打开的，则为 false。</returns>
    /// <filterpriority>1</filterpriority>
    public override bool CanRead
    {
      get
      {
        return this._canRead;
      }
    }

    /// <summary>获取一个值，该值指示当前流是否支持写入。</summary>
    /// <returns>如果流支持写入，则为 true；如果流已关闭或是通过只读访问方式打开的，则为 false。</returns>
    /// <filterpriority>1</filterpriority>
    public override bool CanWrite
    {
      get
      {
        return this._canWrite;
      }
    }

    /// <summary>获取一个值，该值指示当前流是否支持查找。</summary>
    /// <returns>如果流支持查找，则为 true；如果流已关闭或者如果 FileStream 是从操作系统句柄（如管道或到控制台的输出）构造的，则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    public override bool CanSeek
    {
      get
      {
        return this._canSeek;
      }
    }

    /// <summary>获取一个值，该值指示 FileStream 是异步还是同步打开的。</summary>
    /// <returns>如果 FileStream 是异步打开的，则为 true，否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    public virtual bool IsAsync
    {
      get
      {
        return this._isAsync;
      }
    }

    /// <summary>获取用字节表示的流长度。</summary>
    /// <returns>表示流长度（以字节为单位）的长值。</returns>
    /// <exception cref="T:System.NotSupportedException">该流的 <see cref="P:System.IO.FileStream.CanSeek" /> 为 false。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误，如文件被关闭。</exception>
    /// <filterpriority>1</filterpriority>
    public override long Length
    {
      [SecuritySafeCritical] get
      {
        if (this._handle.IsClosed)
          __Error.FileNotOpen();
        if (!this.CanSeek)
          __Error.SeekNotSupported();
        int highSize = 0;
        int fileSize = Win32Native.GetFileSize(this._handle, out highSize);
        if (fileSize == -1)
        {
          int lastWin32Error = Marshal.GetLastWin32Error();
          if (lastWin32Error != 0)
            __Error.WinIOError(lastWin32Error, string.Empty);
        }
        long num = (long) highSize << 32 | (long) (uint) fileSize;
        if (this._writePos > 0 && this._pos + (long) this._writePos > num)
          num = (long) this._writePos + this._pos;
        return num;
      }
    }

    /// <summary>获取传递给构造函数的 FileStream 的名称。</summary>
    /// <returns>一个字符串，它是 FileStream 的名称。</returns>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public string Name
    {
      [SecuritySafeCritical] get
      {
        if (this._fileName == null)
          return Environment.GetResourceString("IO_UnknownFileName");
        new FileIOPermission(FileIOPermissionAccess.PathDiscovery, new string[1]
        {
          this._fileName
        }, 0 != 0, 0 != 0).Demand();
        return this._fileName;
      }
    }

    internal string NameInternal
    {
      get
      {
        if (this._fileName == null)
          return "<UnknownFileName>";
        return this._fileName;
      }
    }

    /// <summary>获取或设置此流的当前位置。</summary>
    /// <returns>此流的当前位置。</returns>
    /// <exception cref="T:System.NotSupportedException">流不支持查找。</exception>
    /// <exception cref="T:System.IO.IOException">发生了 I/O 错误。- 或 -在 Windows 98 或较早版本中，该位置被设置为超出流的末尾的很大的一个值。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">试图将位置设置为负值。</exception>
    /// <exception cref="T:System.IO.EndOfStreamException">试图在流的末尾之外查找，而流不支持此操作。</exception>
    /// <filterpriority>1</filterpriority>
    public override long Position
    {
      [SecuritySafeCritical] get
      {
        if (this._handle.IsClosed)
          __Error.FileNotOpen();
        if (!this.CanSeek)
          __Error.SeekNotSupported();
        if (this._exposedHandle)
          this.VerifyOSHandlePosition();
        return this._pos + (long) (this._readPos - this._readLen + this._writePos);
      }
      set
      {
        if (value < 0L)
          throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        if (this._writePos > 0)
          this.FlushWrite(false);
        this._readPos = 0;
        this._readLen = 0;
        this.Seek(value, SeekOrigin.Begin);
      }
    }

    /// <summary>获取当前 FileStream 对象所封装文件的操作系统文件句柄。</summary>
    /// <returns>此 FileStream 对象所封装文件的操作系统文件句柄；如果 FileStream 已关闭，则为 -1。</returns>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [Obsolete("This property has been deprecated.  Please use FileStream's SafeFileHandle property instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
    public virtual IntPtr Handle
    {
      [SecurityCritical, SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        this.Flush();
        this._readPos = 0;
        this._readLen = 0;
        this._writePos = 0;
        this._exposedHandle = true;
        return this._handle.DangerousGetHandle();
      }
    }

    /// <summary>获取 <see cref="T:Microsoft.Win32.SafeHandles.SafeFileHandle" /> 对象，它代表当前 <see cref="T:System.IO.FileStream" /> 对象所封装的文件的操作系统文件句柄。</summary>
    /// <returns>一个对象，该对象表示当前 <see cref="T:System.IO.FileStream" /> 对象封装的文件的操作系统文件句柄。</returns>
    /// <filterpriority>1</filterpriority>
    public virtual SafeFileHandle SafeFileHandle
    {
      [SecurityCritical, SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        this.Flush();
        this._readPos = 0;
        this._readLen = 0;
        this._writePos = 0;
        this._exposedHandle = true;
        return this._handle;
      }
    }

    internal FileStream()
    {
    }

    /// <summary>使用指定的路径和创建模式初始化 <see cref="T:System.IO.FileStream" /> 类的新实例。</summary>
    /// <param name="path">当前 FileStream 对象将封装的文件的相对路径或绝对路径。</param>
    /// <param name="mode">一个确定如何打开或创建文件的常数。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 为空字符串 ("")，只包含空格，或者包含一个或多个无效字符。- 或 -<paramref name="path" /> 指非文件设备，如"con："、"com1："，"lpt1:"，等等。在 NTFS 环境中。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 指非文件设备，如"con："、"com1："，"lpt1:"，等等。在非 NTFS 环境中。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">找不到文件，比如当 <paramref name="mode" /> 是 FileMode.Truncate 或 FileMode.Open 而 <paramref name="path" /> 指定的文件不存在时。文件必须已经以这些模式存在。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误，比如指定 FileMode.CreateNew 而 <paramref name="path" /> 指定的文件已存在。- 或 -流已关闭。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效，比如在未映射的驱动器上。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="mode" /> 包含无效值。</exception>
    [SecuritySafeCritical]
    public FileStream(string path, FileMode mode)
    {
      string path1 = path;
      int num1 = (int) mode;
      int num2 = 6;
      int num3 = num1 == num2 ? 2 : 3;
      int num4 = 1;
      int bufferSize = 4096;
      int num5 = 0;
      string fileName = Path.GetFileName(path);
      int num6 = 0;
      // ISSUE: explicit constructor call
      this.\u002Ector(path1, (FileMode) num1, (FileAccess) num3, (FileShare) num4, bufferSize, (FileOptions) num5, fileName, num6 != 0);
    }

    /// <summary>使用指定的路径、创建模式和读/写权限初始化 <see cref="T:System.IO.FileStream" /> 类的新实例。</summary>
    /// <param name="path">当前 FileStream 对象将封装的文件的相对路径或绝对路径。</param>
    /// <param name="mode">一个确定如何打开或创建文件的常数。</param>
    /// <param name="access">一个常数，用于确定 FileStream 对象访问文件的方式。该常数还可以确定由 FileStream 对象的 <see cref="P:System.IO.FileStream.CanRead" /> 和 <see cref="P:System.IO.FileStream.CanWrite" /> 属性返回的值。如果 <paramref name="path" /> 指定磁盘文件，则 <see cref="P:System.IO.FileStream.CanSeek" /> 为 true。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 为空字符串 ("")，只包含空格，或者包含一个或多个无效字符。- 或 -<paramref name="path" /> 指非文件设备，如"con："、"com1："，"lpt1:"，等等。在 NTFS 环境中。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 指非文件设备，如"con："、"com1："，"lpt1:"，等等。在非 NTFS 环境中。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">找不到文件，比如当 <paramref name="mode" /> 是 FileMode.Truncate 或 FileMode.Open 而 <paramref name="path" /> 指定的文件不存在时。文件必须已经以这些模式存在。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误，比如指定 FileMode.CreateNew 而 <paramref name="path" /> 指定的文件已存在。- 或 -流已关闭。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效，比如在未映射的驱动器上。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">对于指定的 <paramref name="access" /> 操作系统不允许所请求的 <paramref name="path" />，比如当 <paramref name="access" /> 是 Write 或 ReadWrite 而文件或目录设置为只读访问时。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="mode" /> 包含无效值。</exception>
    [SecuritySafeCritical]
    public FileStream(string path, FileMode mode, FileAccess access)
      : this(path, mode, access, FileShare.Read, 4096, FileOptions.None, Path.GetFileName(path), false)
    {
    }

    /// <summary>使用指定的路径、创建模式、读/写权限和共享权限创建 <see cref="T:System.IO.FileStream" /> 类的新实例。</summary>
    /// <param name="path">当前 FileStream 对象将封装的文件的相对路径或绝对路径。</param>
    /// <param name="mode">一个确定如何打开或创建文件的常数。</param>
    /// <param name="access">一个常数，用于确定 FileStream 对象访问文件的方式。该常数还可以确定由 FileStream 对象的 <see cref="P:System.IO.FileStream.CanRead" /> 和 <see cref="P:System.IO.FileStream.CanWrite" /> 属性返回的值。如果 <paramref name="path" /> 指定磁盘文件，则 <see cref="P:System.IO.FileStream.CanSeek" /> 为 true。</param>
    /// <param name="share">一个常数，确定文件将如何由进程共享。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 为空字符串 ("")，只包含空格，或者包含一个或多个无效字符。- 或 -<paramref name="path" /> 指非文件设备，如"con："、"com1："，"lpt1:"，等等。在 NTFS 环境中。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 指非文件设备，如"con："、"com1："，"lpt1:"，等等。在非 NTFS 环境中。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">找不到文件，比如当 <paramref name="mode" /> 是 FileMode.Truncate 或 FileMode.Open 而 <paramref name="path" /> 指定的文件不存在时。文件必须已经以这些模式存在。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误，比如指定 FileMode.CreateNew 而 <paramref name="path" /> 指定的文件已存在。- 或 -系统正在运行 Windows 98 或 Windows 98 Second Edition，并且 <paramref name="share" /> 设置为 FileShare.Delete。- 或 -流已关闭。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效，比如在未映射的驱动器上。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">对于指定的 <paramref name="access" /> 操作系统不允许所请求的 <paramref name="path" />，比如当 <paramref name="access" /> 是 Write 或 ReadWrite 而文件或目录设置为只读访问时。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="mode" /> 包含无效值。</exception>
    [SecuritySafeCritical]
    public FileStream(string path, FileMode mode, FileAccess access, FileShare share)
      : this(path, mode, access, share, 4096, FileOptions.None, Path.GetFileName(path), false)
    {
    }

    /// <summary>用指定的路径、创建模式、读/写及共享权限和缓冲区大小初始化 <see cref="T:System.IO.FileStream" /> 类的新实例。</summary>
    /// <param name="path">当前 FileStream 对象将封装的文件的相对路径或绝对路径。</param>
    /// <param name="mode">一个确定如何打开或创建文件的常数。</param>
    /// <param name="access">一个常数，用于确定 FileStream 对象访问文件的方式。该常数还可以确定由 FileStream 对象的 <see cref="P:System.IO.FileStream.CanRead" /> 和 <see cref="P:System.IO.FileStream.CanWrite" /> 属性返回的值。如果 <paramref name="path" /> 指定磁盘文件，则 <see cref="P:System.IO.FileStream.CanSeek" /> 为 true。</param>
    /// <param name="share">一个常数，确定文件将如何由进程共享。</param>
    /// <param name="bufferSize">一个大于零的正 <see cref="T:System.Int32" /> 值，表示缓冲区大小。默认缓冲区大小为 4096。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 为空字符串 ("")，只包含空格，或者包含一个或多个无效字符。- 或 -<paramref name="path" /> 指非文件设备，如"con："、"com1："，"lpt1:"，等等。在 NTFS 环境中。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 指非文件设备，如"con："、"com1："，"lpt1:"，等等。在非 NTFS 环境中。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="bufferSize" /> 为负数或零。- 或 - <paramref name="mode" />、<paramref name="access" /> 或 <paramref name="share" /> 包含无效值。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">找不到文件，比如当 <paramref name="mode" /> 是 FileMode.Truncate 或 FileMode.Open 而 <paramref name="path" /> 指定的文件不存在时。文件必须已经以这些模式存在。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误，比如指定 FileMode.CreateNew 而 <paramref name="path" /> 指定的文件已存在。- 或 -系统正在运行 Windows 98 或 Windows 98 Second Edition，并且 <paramref name="share" /> 设置为 FileShare.Delete。- 或 -流已关闭。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效，比如在未映射的驱动器上。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">对于指定的 <paramref name="access" /> 操作系统不允许所请求的 <paramref name="path" />，比如当 <paramref name="access" /> 是 Write 或 ReadWrite 而文件或目录设置为只读访问时。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    [SecuritySafeCritical]
    public FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize)
      : this(path, mode, access, share, bufferSize, FileOptions.None, Path.GetFileName(path), false)
    {
    }

    /// <summary>使用指定的路径、创建模式、读/写和共享权限、其他 FileStreams 可以具有的对此文件的访问权限、缓冲区大小和附加文件选项初始化 <see cref="T:System.IO.FileStream" /> 类的新实例。</summary>
    /// <param name="path">当前 FileStream 对象将封装的文件的相对路径或绝对路径。</param>
    /// <param name="mode">一个确定如何打开或创建文件的常数。</param>
    /// <param name="access">一个常数，用于确定 FileStream 对象访问文件的方式。该常数还可以确定由 FileStream 对象的 <see cref="P:System.IO.FileStream.CanRead" /> 和 <see cref="P:System.IO.FileStream.CanWrite" /> 属性返回的值。如果 <paramref name="path" /> 指定磁盘文件，则 <see cref="P:System.IO.FileStream.CanSeek" /> 为 true。</param>
    /// <param name="share">一个常数，确定文件将如何由进程共享。</param>
    /// <param name="bufferSize">一个大于零的正 <see cref="T:System.Int32" /> 值，表示缓冲区大小。默认缓冲区大小为 4096。</param>
    /// <param name="options">一个指定附加文件选项的值。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 为空字符串 ("")，只包含空格，或者包含一个或多个无效字符。- 或 -<paramref name="path" /> 指非文件设备，如"con："、"com1："，"lpt1:"，等等。在 NTFS 环境中。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 指非文件设备，如"con："、"com1："，"lpt1:"，等等。在非 NTFS 环境中。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="bufferSize" /> 为负数或零。- 或 - <paramref name="mode" />、<paramref name="access" /> 或 <paramref name="share" /> 包含无效值。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">找不到文件，比如当 <paramref name="mode" /> 是 FileMode.Truncate 或 FileMode.Open 而 <paramref name="path" /> 指定的文件不存在时。文件必须已经以这些模式存在。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误，比如指定 FileMode.CreateNew 而 <paramref name="path" /> 指定的文件已存在。- 或 -流已关闭。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效，比如在未映射的驱动器上。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">对于指定的 <paramref name="access" /> 操作系统不允许所请求的 <paramref name="path" />，比如当 <paramref name="access" /> 是 Write 或 ReadWrite 而文件或目录设置为只读访问时。- 或 -为 <see cref="F:System.IO.FileOptions.Encrypted" /> 指定了 <paramref name="options" />，但是当前平台不支持文件加密。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    [SecuritySafeCritical]
    public FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, FileOptions options)
      : this(path, mode, access, share, bufferSize, options, Path.GetFileName(path), false)
    {
    }

    /// <summary>使用指定的路径、创建模式、读/写和共享权限、缓冲区大小和同步或异步状态初始化 <see cref="T:System.IO.FileStream" /> 类的新实例。</summary>
    /// <param name="path">当前 FileStream 对象将封装的文件的相对路径或绝对路径。</param>
    /// <param name="mode">一个确定如何打开或创建文件的常数。</param>
    /// <param name="access">一个常数，用于确定 FileStream 对象访问文件的方式。该常数还可以确定由 FileStream 对象的 <see cref="P:System.IO.FileStream.CanRead" /> 和 <see cref="P:System.IO.FileStream.CanWrite" /> 属性返回的值。如果 <paramref name="path" /> 指定磁盘文件，则 <see cref="P:System.IO.FileStream.CanSeek" /> 为 true。</param>
    /// <param name="share">一个常数，确定文件将如何由进程共享。</param>
    /// <param name="bufferSize">一个大于零的正 <see cref="T:System.Int32" /> 值，表示缓冲区大小。默认缓冲区大小为 4096。</param>
    /// <param name="useAsync">指定使用异步 I/O 还是同步 I/O。但是，请注意，基础操作系统可能不支持异步 I/O，因此在指定 true 后，根据所用平台，句柄可能同步打开。当异步打开时，<see cref="M:System.IO.FileStream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> 和 <see cref="M:System.IO.FileStream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> 方法在执行大量读或写时效果更好，但对于少量的读/写，这些方法速度可能要慢得多。如果应用程序打算利用异步 I/O，将 <paramref name="useAsync" /> 参数设置为 true。正确使用异步 I/O 可以使应用程序的速度加快 10 倍，但是如果在没有为异步 I/O 重新设计应用程序的情况下使用异步 I/O，则可能使性能降低 10 倍。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 为空字符串 ("")，只包含空格，或者包含一个或多个无效字符。- 或 -<paramref name="path" /> 指非文件设备，如"con："、"com1："，"lpt1:"，等等。在 NTFS 环境中。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 指非文件设备，如"con："、"com1："，"lpt1:"，等等。在非 NTFS 环境中。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="bufferSize" /> 为负数或零。- 或 - <paramref name="mode" />、<paramref name="access" /> 或 <paramref name="share" /> 包含无效值。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">找不到文件，比如当 <paramref name="mode" /> 是 FileMode.Truncate 或 FileMode.Open 而 <paramref name="path" /> 指定的文件不存在时。文件必须已经以这些模式存在。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误，比如指定 FileMode.CreateNew 而 <paramref name="path" /> 指定的文件已存在。- 或 - 系统正在运行 Windows 98 或 Windows 98 Second Edition，并且 <paramref name="share" /> 设置为 FileShare.Delete。- 或 -流已关闭。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效，比如在未映射的驱动器上。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">对于指定的 <paramref name="access" /> 操作系统不允许所请求的 <paramref name="path" />，比如当 <paramref name="access" /> 是 Write 或 ReadWrite 而文件或目录设置为只读访问时。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    [SecuritySafeCritical]
    public FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, bool useAsync)
      : this(path, mode, access, share, bufferSize, useAsync ? FileOptions.Asynchronous : FileOptions.None, Path.GetFileName(path), false)
    {
    }

    /// <summary>使用指定的路径、创建模式、访问权限和共享权限、缓冲区大小、附加文件选项、访问控制和审核安全初始化 <see cref="T:System.IO.FileStream" /> 类的新实例。</summary>
    /// <param name="path">当前 <see cref="T:System.IO.FileStream" /> 对象将封装的文件的相对路径或绝对路径。</param>
    /// <param name="mode">一个确定如何打开或创建文件的常数。</param>
    /// <param name="rights">一个常数，确定为文件创建访问和审核规则时要使用的访问权。</param>
    /// <param name="share">一个常数，确定文件将如何由进程共享。</param>
    /// <param name="bufferSize">一个大于零的正 <see cref="T:System.Int32" /> 值，表示缓冲区大小。默认缓冲区大小为 4096。</param>
    /// <param name="options">一个指定附加文件选项的常数。</param>
    /// <param name="fileSecurity">一个常数，确定文件的访问控制和审核安全。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 为空字符串 ("")，只包含空格，或者包含一个或多个无效字符。- 或 -<paramref name="path" /> 指非文件设备，如"con："、"com1："，"lpt1:"，等等。在 NTFS 环境中。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 指非文件设备，如"con："、"com1："，"lpt1:"，等等。在非 NTFS 环境中。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="bufferSize" /> 为负数或零。- 或 - <paramref name="mode" />、<paramref name="access" /> 或 <paramref name="share" /> 包含无效值。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">找不到文件，比如当 <paramref name="mode" /> 是 FileMode.Truncate 或 FileMode.Open 而 <paramref name="path" /> 指定的文件不存在时。文件必须已经以这些模式存在。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误，比如指定 FileMode.CreateNew 而 <paramref name="path" /> 指定的文件已存在。- 或 -流已关闭。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效，比如在未映射的驱动器上。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">对于指定的 <paramref name="access" /> 操作系统不允许所请求的 <paramref name="path" />，比如当 <paramref name="access" /> 是 Write 或 ReadWrite 而文件或目录设置为只读访问时。- 或 -为 <see cref="F:System.IO.FileOptions.Encrypted" /> 指定了 <paramref name="options" />，但是当前平台不支持文件加密。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的 <paramref name="path" />、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.PlatformNotSupportedException">当前操作系统不是 Windows NT 或更高版本。</exception>
    [SecuritySafeCritical]
    public FileStream(string path, FileMode mode, FileSystemRights rights, FileShare share, int bufferSize, FileOptions options, FileSecurity fileSecurity)
    {
      object pinningHandle;
      Win32Native.SECURITY_ATTRIBUTES secAttrs = FileStream.GetSecAttrs(share, fileSecurity, out pinningHandle);
      try
      {
        this.Init(path, mode, (FileAccess) 0, (int) rights, true, share, bufferSize, options, secAttrs, Path.GetFileName(path), false, false, false);
      }
      finally
      {
        if (pinningHandle != null)
          ((GCHandle) pinningHandle).Free();
      }
    }

    /// <summary>使用指定的路径、创建模式、访问权限和共享权限、缓冲区大小和附加文件选项初始化 <see cref="T:System.IO.FileStream" /> 类的新实例。</summary>
    /// <param name="path">当前 <see cref="T:System.IO.FileStream" /> 对象将封装的文件的相对路径或绝对路径。</param>
    /// <param name="mode">一个确定如何打开或创建文件的常数。</param>
    /// <param name="rights">一个常数，确定为文件创建访问和审核规则时要使用的访问权。</param>
    /// <param name="share">一个常数，确定文件将如何由进程共享。</param>
    /// <param name="bufferSize">一个大于零的正 <see cref="T:System.Int32" /> 值，表示缓冲区大小。默认缓冲区大小为 4096。</param>
    /// <param name="options">一个指定附加文件选项的常数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 为空字符串 ("")，只包含空格，或者包含一个或多个无效字符。- 或 -<paramref name="path" /> 指非文件设备，如"con："、"com1："，"lpt1:"，等等。在 NTFS 环境中。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 指非文件设备，如"con："、"com1："，"lpt1:"，等等。在非 NTFS 环境中。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="bufferSize" /> 为负数或零。- 或 - <paramref name="mode" />、<paramref name="access" /> 或 <paramref name="share" /> 包含无效值。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">找不到文件，比如当 <paramref name="mode" /> 是 FileMode.Truncate 或 FileMode.Open 而 <paramref name="path" /> 指定的文件不存在时。文件必须已经以这些模式存在。</exception>
    /// <exception cref="T:System.PlatformNotSupportedException">当前操作系统不是 Windows NT 或更高版本。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误，比如指定 FileMode.CreateNew 而 <paramref name="path" /> 指定的文件已存在。- 或 -流已关闭。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效，比如在未映射的驱动器上。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">对于指定的 <paramref name="access" /> 操作系统不允许所请求的 <paramref name="path" />，比如当 <paramref name="access" /> 是 Write 或 ReadWrite 而文件或目录设置为只读访问时。- 或 -为 <see cref="F:System.IO.FileOptions.Encrypted" /> 指定了 <paramref name="options" />，但是当前平台不支持文件加密。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的 <paramref name="path" />、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    [SecuritySafeCritical]
    public FileStream(string path, FileMode mode, FileSystemRights rights, FileShare share, int bufferSize, FileOptions options)
    {
      Win32Native.SECURITY_ATTRIBUTES secAttrs = FileStream.GetSecAttrs(share);
      this.Init(path, mode, (FileAccess) 0, (int) rights, true, share, bufferSize, options, secAttrs, Path.GetFileName(path), false, false, false);
    }

    [SecurityCritical]
    internal FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, FileOptions options, string msgPath, bool bFromProxy)
    {
      Win32Native.SECURITY_ATTRIBUTES secAttrs = FileStream.GetSecAttrs(share);
      this.Init(path, mode, access, 0, false, share, bufferSize, options, secAttrs, msgPath, bFromProxy, false, false);
    }

    [SecurityCritical]
    internal FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, FileOptions options, string msgPath, bool bFromProxy, bool useLongPath)
    {
      Win32Native.SECURITY_ATTRIBUTES secAttrs = FileStream.GetSecAttrs(share);
      this.Init(path, mode, access, 0, false, share, bufferSize, options, secAttrs, msgPath, bFromProxy, useLongPath, false);
    }

    [SecurityCritical]
    internal FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, FileOptions options, string msgPath, bool bFromProxy, bool useLongPath, bool checkHost)
    {
      Win32Native.SECURITY_ATTRIBUTES secAttrs = FileStream.GetSecAttrs(share);
      this.Init(path, mode, access, 0, false, share, bufferSize, options, secAttrs, msgPath, bFromProxy, useLongPath, checkHost);
    }

    /// <summary>使用指定的读/写权限为指定的文件句柄初始化 <see cref="T:System.IO.FileStream" /> 类的新实例。</summary>
    /// <param name="handle">当前 FileStream 对象将封装的文件的文件句柄。</param>
    /// <param name="access">一个常数，用于设置 FileStream 对象的 <see cref="P:System.IO.FileStream.CanRead" /> 和 <see cref="P:System.IO.FileStream.CanWrite" /> 属性。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="access" /> 不是 <see cref="T:System.IO.FileAccess" /> 的字段。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误，如磁盘错误。- 或 -流已关闭。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">对于指定的文件句柄，操作系统不允许所请求的 <paramref name="access" />，例如，当 <paramref name="access" /> 为 Write 或 ReadWrite 而文件句柄设置为只读访问时。</exception>
    [Obsolete("This constructor has been deprecated.  Please use new FileStream(SafeFileHandle handle, FileAccess access) instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
    public FileStream(IntPtr handle, FileAccess access)
      : this(handle, access, true, 4096, false)
    {
    }

    /// <summary>使用指定的读/写权限和 FileStream 实例所属权为指定的文件句柄初始化 <see cref="T:System.IO.FileStream" /> 类的新实例。</summary>
    /// <param name="handle">当前 FileStream 对象将封装的文件的文件句柄。</param>
    /// <param name="access">一个常数，用于设置 FileStream 对象的 <see cref="P:System.IO.FileStream.CanRead" /> 和 <see cref="P:System.IO.FileStream.CanWrite" /> 属性。</param>
    /// <param name="ownsHandle">如果文件句柄将由此 FileStream 实例所有，则为 true；否则为 false。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="access" /> 不是 <see cref="T:System.IO.FileAccess" /> 的字段。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误，如磁盘错误。- 或 -流已关闭。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">对于指定的文件句柄，操作系统不允许所请求的 <paramref name="access" />，例如，当 <paramref name="access" /> 为 Write 或 ReadWrite 而文件句柄设置为只读访问时。</exception>
    [Obsolete("This constructor has been deprecated.  Please use new FileStream(SafeFileHandle handle, FileAccess access) instead, and optionally make a new SafeFileHandle with ownsHandle=false if needed.  http://go.microsoft.com/fwlink/?linkid=14202")]
    public FileStream(IntPtr handle, FileAccess access, bool ownsHandle)
      : this(handle, access, ownsHandle, 4096, false)
    {
    }

    /// <summary>使用指定的读/写权限、FileStream 实例所属权和缓冲区大小为指定的文件句柄初始化 <see cref="T:System.IO.FileStream" /> 类的新实例。</summary>
    /// <param name="handle">此 FileStream 对象将封装的文件的文件句柄。</param>
    /// <param name="access">一个常数，用于设置 FileStream 对象的 <see cref="P:System.IO.FileStream.CanRead" /> 和 <see cref="P:System.IO.FileStream.CanWrite" /> 属性。</param>
    /// <param name="ownsHandle">如果文件句柄将由此 FileStream 实例所有，则为 true；否则为 false。</param>
    /// <param name="bufferSize">一个大于零的正 <see cref="T:System.Int32" /> 值，表示缓冲区大小。默认缓冲区大小为 4096。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="bufferSize" /> 为负数。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误，如磁盘错误。- 或 -流已关闭。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">对于指定的文件句柄，操作系统不允许所请求的 <paramref name="access" />，例如，当 <paramref name="access" /> 为 Write 或 ReadWrite 而文件句柄设置为只读访问时。</exception>
    [Obsolete("This constructor has been deprecated.  Please use new FileStream(SafeFileHandle handle, FileAccess access, int bufferSize) instead, and optionally make a new SafeFileHandle with ownsHandle=false if needed.  http://go.microsoft.com/fwlink/?linkid=14202")]
    public FileStream(IntPtr handle, FileAccess access, bool ownsHandle, int bufferSize)
      : this(handle, access, ownsHandle, bufferSize, false)
    {
    }

    /// <summary>使用指定的读/写权限、FileStream 实例所属权、缓冲区大小和同步或异步状态为指定的文件句柄初始化 <see cref="T:System.IO.FileStream" /> 类的新实例。</summary>
    /// <param name="handle">此 FileStream 对象将封装的文件的文件句柄。</param>
    /// <param name="access">一个常数，用于设置 FileStream 对象的 <see cref="P:System.IO.FileStream.CanRead" /> 和 <see cref="P:System.IO.FileStream.CanWrite" /> 属性。</param>
    /// <param name="ownsHandle">如果文件句柄将由此 FileStream 实例所有，则为 true；否则为 false。</param>
    /// <param name="bufferSize">一个大于零的正 <see cref="T:System.Int32" /> 值，表示缓冲区大小。默认缓冲区大小为 4096。</param>
    /// <param name="isAsync">如果异步打开句柄（即以重叠的 I/O 模式），则为 true；否则为 false。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="access" /> 小于 FileAccess.Read 或大于 FileAccess.ReadWrite，或者 <paramref name="bufferSize" /> 小于或等于 0。</exception>
    /// <exception cref="T:System.ArgumentException">该句柄无效。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误，如磁盘错误。- 或 -流已关闭。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">对于指定的文件句柄，操作系统不允许所请求的 <paramref name="access" />，例如，当 <paramref name="access" /> 为 Write 或 ReadWrite 而文件句柄设置为只读访问时。</exception>
    [SecuritySafeCritical]
    [Obsolete("This constructor has been deprecated.  Please use new FileStream(SafeFileHandle handle, FileAccess access, int bufferSize, bool isAsync) instead, and optionally make a new SafeFileHandle with ownsHandle=false if needed.  http://go.microsoft.com/fwlink/?linkid=14202")]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public FileStream(IntPtr handle, FileAccess access, bool ownsHandle, int bufferSize, bool isAsync)
      : this(new SafeFileHandle(handle, ownsHandle), access, bufferSize, isAsync)
    {
    }

    /// <summary>使用指定的读/写权限为指定的文件句柄初始化 <see cref="T:System.IO.FileStream" /> 类的新实例。</summary>
    /// <param name="handle">当前 FileStream 对象将封装的文件的文件句柄。</param>
    /// <param name="access">一个常数，用于设置 FileStream 对象的 <see cref="P:System.IO.FileStream.CanRead" /> 和 <see cref="P:System.IO.FileStream.CanWrite" /> 属性。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="access" /> 不是 <see cref="T:System.IO.FileAccess" /> 的字段。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误，如磁盘错误。- 或 -流已关闭。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">对于指定的文件句柄，操作系统不允许所请求的 <paramref name="access" />，例如，当 <paramref name="access" /> 为 Write 或 ReadWrite 而文件句柄设置为只读访问时。</exception>
    [SecuritySafeCritical]
    public FileStream(SafeFileHandle handle, FileAccess access)
      : this(handle, access, 4096, false)
    {
    }

    /// <summary>使用指定的读/写权限和缓冲区大小为指定的文件句柄初始化 <see cref="T:System.IO.FileStream" /> 类的新实例。</summary>
    /// <param name="handle">当前 FileStream 对象将封装的文件的文件句柄。</param>
    /// <param name="access">一个 <see cref="T:System.IO.FileAccess" /> 常数，它设置 FileStream 对象的 <see cref="P:System.IO.FileStream.CanRead" /> 和 <see cref="P:System.IO.FileStream.CanWrite" /> 属性。</param>
    /// <param name="bufferSize">一个大于零的正 <see cref="T:System.Int32" /> 值，表示缓冲区大小。默认缓冲区大小为 4096。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="handle" /> 参数是无效的句柄。- 或 -<paramref name="handle" /> 参数是同步句柄，但被异步使用。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="bufferSize" /> 参数为负数。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误，如磁盘错误。- 或 -流已关闭。 </exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">对于指定的文件句柄，操作系统不允许所请求的 <paramref name="access" />，例如，当 <paramref name="access" /> 为 Write 或 ReadWrite 而文件句柄设置为只读访问时。</exception>
    [SecuritySafeCritical]
    public FileStream(SafeFileHandle handle, FileAccess access, int bufferSize)
      : this(handle, access, bufferSize, false)
    {
    }

    /// <summary>使用指定的读/写权限、缓冲区大小和同步或异步状态为指定的文件句柄初始化 <see cref="T:System.IO.FileStream" /> 类的新实例。</summary>
    /// <param name="handle">此 FileStream 对象将封装的文件的文件句柄。</param>
    /// <param name="access">一个常数，用于设置 FileStream 对象的 <see cref="P:System.IO.FileStream.CanRead" /> 和 <see cref="P:System.IO.FileStream.CanWrite" /> 属性。</param>
    /// <param name="bufferSize">一个大于零的正 <see cref="T:System.Int32" /> 值，表示缓冲区大小。默认缓冲区大小为 4096。</param>
    /// <param name="isAsync">如果异步打开句柄（即以重叠的 I/O 模式），则为 true；否则为 false。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="handle" /> 参数是无效的句柄。- 或 -<paramref name="handle" /> 参数是同步句柄，但被异步使用。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="bufferSize" /> 参数为负数。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误，如磁盘错误。- 或 -流已关闭。 </exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">对于指定的文件句柄，操作系统不允许所请求的 <paramref name="access" />，例如，当 <paramref name="access" /> 为 Write 或 ReadWrite 而文件句柄设置为只读访问时。</exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public FileStream(SafeFileHandle handle, FileAccess access, int bufferSize, bool isAsync)
    {
      if (handle.IsInvalid)
        throw new ArgumentException(Environment.GetResourceString("Arg_InvalidHandle"), "handle");
      this._handle = handle;
      this._exposedHandle = true;
      if (access < FileAccess.Read || access > FileAccess.ReadWrite)
        throw new ArgumentOutOfRangeException("access", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
      if (bufferSize <= 0)
        throw new ArgumentOutOfRangeException("bufferSize", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
      int fileType = Win32Native.GetFileType(this._handle);
      this._isAsync = isAsync;
      this._canRead = (uint) (access & FileAccess.Read) > 0U;
      this._canWrite = (uint) (access & FileAccess.Write) > 0U;
      this._canSeek = fileType == 1;
      this._bufferSize = bufferSize;
      this._readPos = 0;
      this._readLen = 0;
      this._writePos = 0;
      this._fileName = (string) null;
      this._isPipe = fileType == 3;
      if (this._isAsync)
      {
        bool flag;
        try
        {
          flag = ThreadPool.BindHandle((SafeHandle) this._handle);
        }
        catch (ApplicationException ex)
        {
          throw new ArgumentException(Environment.GetResourceString("Arg_HandleNotAsync"));
        }
        if (!flag)
          throw new IOException(Environment.GetResourceString("IO.IO_BindHandleFailed"));
      }
      else if (fileType != 3)
        this.VerifyHandleIsSync();
      if (this._canSeek)
        this.SeekCore(0L, SeekOrigin.Current);
      else
        this._pos = 0L;
    }

    /// <summary>确保垃圾回收器回收 FileStream 时释放资源并执行其他清理操作。</summary>
    [SecuritySafeCritical]
    ~FileStream()
    {
      if (this._handle == null)
        return;
      this.Dispose(false);
    }

    [SecuritySafeCritical]
    private unsafe void Init(string path, FileMode mode, FileAccess access, int rights, bool useRights, FileShare share, int bufferSize, FileOptions options, Win32Native.SECURITY_ATTRIBUTES secAttrs, string msgPath, bool bFromProxy, bool useLongPath, bool checkHost)
    {
      if (path == null)
        throw new ArgumentNullException("path", Environment.GetResourceString("ArgumentNull_Path"));
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      FileSystemRights fileSystemRights = (FileSystemRights) rights;
      this._fileName = msgPath;
      this._exposedHandle = false;
      FileShare fileShare = share & ~FileShare.Inheritable;
      string paramName = (string) null;
      if (mode < FileMode.CreateNew || mode > FileMode.Append)
        paramName = "mode";
      else if (!useRights && (access < FileAccess.Read || access > FileAccess.ReadWrite))
        paramName = "access";
      else if (useRights && (fileSystemRights < FileSystemRights.ReadData || fileSystemRights > FileSystemRights.FullControl))
        paramName = "rights";
      else if (fileShare < FileShare.None || fileShare > (FileShare.ReadWrite | FileShare.Delete))
        paramName = "share";
      if (paramName != null)
        throw new ArgumentOutOfRangeException(paramName, Environment.GetResourceString("ArgumentOutOfRange_Enum"));
      if (options != FileOptions.None && (options & (FileOptions) 67092479) != FileOptions.None)
        throw new ArgumentOutOfRangeException("options", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
      if (bufferSize <= 0)
        throw new ArgumentOutOfRangeException("bufferSize", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
      if ((!useRights && (access & FileAccess.Write) == (FileAccess) 0 || useRights && (fileSystemRights & FileSystemRights.Write) == (FileSystemRights) 0) && (mode == FileMode.Truncate || mode == FileMode.CreateNew || (mode == FileMode.Create || mode == FileMode.Append)))
      {
        if (!useRights)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFileMode&AccessCombo", (object) mode, (object) access));
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFileMode&RightsCombo", (object) mode, (object) fileSystemRights));
      }
      if (useRights && mode == FileMode.Truncate)
      {
        if (fileSystemRights == FileSystemRights.Write)
        {
          useRights = false;
          access = FileAccess.Write;
        }
        else
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFileModeTruncate&RightsCombo", (object) mode, (object) fileSystemRights));
      }
      int dwDesiredAccess = useRights ? rights : (access == FileAccess.Read ? int.MinValue : (access == FileAccess.Write ? 1073741824 : -1073741824));
      int maxPathLength = useLongPath ? 32000 : Path.MaxPath;
      string path1 = Path.NormalizePath(path, true, maxPathLength);
      this._fileName = path1;
      if (path1.StartsWith("\\\\.\\", StringComparison.Ordinal))
        throw new ArgumentException(Environment.GetResourceString("Arg_DevicesNotSupported"));
      Path.CheckInvalidPathChars(path1, true);
      if (path1.IndexOf(':', 2) != -1)
        throw new NotSupportedException(Environment.GetResourceString("Argument_PathFormatNotSupported"));
      bool flag1 = false;
      if (!useRights && (access & FileAccess.Read) != (FileAccess) 0 || useRights && (fileSystemRights & FileSystemRights.ReadAndExecute) != (FileSystemRights) 0)
      {
        if (mode == FileMode.Append)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidAppendMode"));
        flag1 = true;
      }
      if (!CodeAccessSecurityEngine.QuickCheckForAllDemands())
      {
        FileIOPermissionAccess access1 = FileIOPermissionAccess.NoAccess;
        if (flag1)
          access1 |= FileIOPermissionAccess.Read;
        if (!useRights && (access & FileAccess.Write) != (FileAccess) 0 || useRights && (fileSystemRights & (FileSystemRights.Write | FileSystemRights.DeleteSubdirectoriesAndFiles | FileSystemRights.Delete | FileSystemRights.ChangePermissions | FileSystemRights.TakeOwnership)) != (FileSystemRights) 0 || useRights && (fileSystemRights & FileSystemRights.Synchronize) != (FileSystemRights) 0 && mode == FileMode.OpenOrCreate)
        {
          if (mode == FileMode.Append)
            access1 |= FileIOPermissionAccess.Append;
          else
            access1 |= FileIOPermissionAccess.Write;
        }
        AccessControlActions control = (secAttrs == null ? 0 : ((IntPtr) secAttrs.pSecurityDescriptor != IntPtr.Zero ? 1 : 0)) != 0 ? AccessControlActions.Change : AccessControlActions.None;
        new FileIOPermission(access1, control, new string[1]{ path1 }, 0 != 0, 0 != 0).Demand();
      }
      share &= ~FileShare.Inheritable;
      bool flag2 = mode == FileMode.Append;
      if (mode == FileMode.Append)
        mode = FileMode.OpenOrCreate;
      if ((options & FileOptions.Asynchronous) != FileOptions.None)
        this._isAsync = true;
      else
        options &= ~FileOptions.Asynchronous;
      int dwFlagsAndAttributes = (int) (options | (FileOptions) 1048576);
      int newMode = Win32Native.SetErrorMode(1);
      try
      {
        string str = path1;
        if (useLongPath)
          str = Path.AddLongPathPrefix(str);
        this._handle = Win32Native.SafeCreateFile(str, dwDesiredAccess, share, secAttrs, mode, dwFlagsAndAttributes, IntPtr.Zero);
        if (this._handle.IsInvalid)
        {
          int errorCode = Marshal.GetLastWin32Error();
          if (errorCode == 3 && path1.Equals(Directory.InternalGetDirectoryRoot(path1)))
            errorCode = 5;
          bool flag3 = false;
          if (!bFromProxy)
          {
            try
            {
              new FileIOPermission(FileIOPermissionAccess.PathDiscovery, new string[1]
              {
                this._fileName
              }, 0 != 0, 0 != 0).Demand();
              flag3 = true;
            }
            catch (SecurityException ex)
            {
            }
          }
          if (flag3)
            __Error.WinIOError(errorCode, this._fileName);
          else
            __Error.WinIOError(errorCode, msgPath);
        }
      }
      finally
      {
        Win32Native.SetErrorMode(newMode);
      }
      if (Win32Native.GetFileType(this._handle) != 1)
      {
        this._handle.Close();
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_FileStreamOnNonFiles"));
      }
      if (this._isAsync)
      {
        bool flag3 = false;
        new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
        try
        {
          flag3 = ThreadPool.BindHandle((SafeHandle) this._handle);
        }
        finally
        {
          CodeAccessPermission.RevertAssert();
          if (!flag3)
            this._handle.Close();
        }
        if (!flag3)
          throw new IOException(Environment.GetResourceString("IO.IO_BindHandleFailed"));
      }
      if (!useRights)
      {
        this._canRead = (uint) (access & FileAccess.Read) > 0U;
        this._canWrite = (uint) (access & FileAccess.Write) > 0U;
      }
      else
      {
        this._canRead = (uint) (fileSystemRights & FileSystemRights.ReadData) > 0U;
        this._canWrite = (fileSystemRights & FileSystemRights.WriteData) != (FileSystemRights) 0 || (uint) (fileSystemRights & FileSystemRights.AppendData) > 0U;
      }
      this._canSeek = true;
      this._isPipe = false;
      this._pos = 0L;
      this._bufferSize = bufferSize;
      this._readPos = 0;
      this._readLen = 0;
      this._writePos = 0;
      if (flag2)
        this._appendStart = this.SeekCore(0L, SeekOrigin.End);
      else
        this._appendStart = -1L;
    }

    [SecuritySafeCritical]
    private static Win32Native.SECURITY_ATTRIBUTES GetSecAttrs(FileShare share)
    {
      Win32Native.SECURITY_ATTRIBUTES structure = (Win32Native.SECURITY_ATTRIBUTES) null;
      if ((share & FileShare.Inheritable) != FileShare.None)
      {
        structure = new Win32Native.SECURITY_ATTRIBUTES();
        structure.nLength = Marshal.SizeOf<Win32Native.SECURITY_ATTRIBUTES>(structure);
        structure.bInheritHandle = 1;
      }
      return structure;
    }

    [SecuritySafeCritical]
    private static unsafe Win32Native.SECURITY_ATTRIBUTES GetSecAttrs(FileShare share, FileSecurity fileSecurity, out object pinningHandle)
    {
      pinningHandle = (object) null;
      Win32Native.SECURITY_ATTRIBUTES structure = (Win32Native.SECURITY_ATTRIBUTES) null;
      if ((share & FileShare.Inheritable) != FileShare.None || fileSecurity != null)
      {
        structure = new Win32Native.SECURITY_ATTRIBUTES();
        structure.nLength = Marshal.SizeOf<Win32Native.SECURITY_ATTRIBUTES>(structure);
        if ((share & FileShare.Inheritable) != FileShare.None)
          structure.bInheritHandle = 1;
        if (fileSecurity != null)
        {
          byte[] descriptorBinaryForm = fileSecurity.GetSecurityDescriptorBinaryForm();
          pinningHandle = (object) GCHandle.Alloc((object) descriptorBinaryForm, GCHandleType.Pinned);
          fixed (byte* numPtr = descriptorBinaryForm)
            structure.pSecurityDescriptor = numPtr;
        }
      }
      return structure;
    }

    [SecuritySafeCritical]
    private unsafe void VerifyHandleIsSync()
    {
      byte[] bytes = new byte[1];
      int hr = 0;
      if (this.CanRead)
        this.ReadFileNative(this._handle, bytes, 0, 0, (NativeOverlapped*) null, out hr);
      else if (this.CanWrite)
        this.WriteFileNative(this._handle, bytes, 0, 0, (NativeOverlapped*) null, out hr);
      if (hr == 87)
        throw new ArgumentException(Environment.GetResourceString("Arg_HandleNotSync"));
      if (hr != 6)
        return;
      __Error.WinIOError(hr, "<OS handle>");
    }

    /// <summary>获取 <see cref="T:System.Security.AccessControl.FileSecurity" /> 对象，该对象封装当前 <see cref="T:System.IO.FileStream" /> 对象所描述的文件的访问控制列表 (ACL) 项。</summary>
    /// <returns>一个对象，该对象封装当前 <see cref="T:System.IO.FileStream" /> 对象所描述的文件的访问控制设置。</returns>
    /// <exception cref="T:System.ObjectDisposedException">文件被关闭。</exception>
    /// <exception cref="T:System.IO.IOException">打开文件时发生 I/O 错误。</exception>
    /// <exception cref="T:System.SystemException">找不到文件。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">当前平台不支持此操作。- 或 - 调用方没有所要求的权限。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public FileSecurity GetAccessControl()
    {
      if (this._handle.IsClosed)
        __Error.FileNotOpen();
      return new FileSecurity(this._handle, this._fileName, AccessControlSections.Access | AccessControlSections.Owner | AccessControlSections.Group);
    }

    /// <summary>将 <see cref="T:System.Security.AccessControl.FileSecurity" /> 对象所描述的访问控制列表 (ACL) 项应用于当前 <see cref="T:System.IO.FileStream" /> 对象所描述的文件。</summary>
    /// <param name="fileSecurity">描述要应用于当前文件的 ACL 项的对象。</param>
    /// <exception cref="T:System.ObjectDisposedException">文件被关闭。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="fileSecurity" /> 参数为 null。</exception>
    /// <exception cref="T:System.SystemException">未能找到或修改文件。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">当前进程不具有打开该文件的权限。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public void SetAccessControl(FileSecurity fileSecurity)
    {
      if (fileSecurity == null)
        throw new ArgumentNullException("fileSecurity");
      if (this._handle.IsClosed)
        __Error.FileNotOpen();
      fileSecurity.Persist(this._handle, this._fileName);
    }

    /// <summary>释放由 <see cref="T:System.IO.FileStream" /> 占用的非托管资源，还可以另外再释放托管资源。</summary>
    /// <param name="disposing">若要释放托管资源和非托管资源，则为 true；若仅释放非托管资源，则为 false。</param>
    [SecuritySafeCritical]
    protected override void Dispose(bool disposing)
    {
      try
      {
        if (this._handle == null || this._handle.IsClosed || this._writePos <= 0)
          return;
        this.FlushWrite(!disposing);
      }
      finally
      {
        if (this._handle != null && !this._handle.IsClosed)
          this._handle.Dispose();
        this._canRead = false;
        this._canWrite = false;
        this._canSeek = false;
        base.Dispose(disposing);
      }
    }

    /// <summary>清除此流的缓冲区，使得所有缓冲数据都写入到文件中。</summary>
    /// <exception cref="T:System.IO.IOException">发生了 I/O 错误。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <filterpriority>1</filterpriority>
    public override void Flush()
    {
      this.Flush(false);
    }

    /// <summary>清除此流的缓冲区，将所有缓冲数据都写入到文件中，并且也清除所有中间文件缓冲区。</summary>
    /// <param name="flushToDisk">如果刷新所有中间文件缓冲区，则为 true；否则为 false。</param>
    [SecuritySafeCritical]
    public virtual void Flush(bool flushToDisk)
    {
      if (this._handle.IsClosed)
        __Error.FileNotOpen();
      this.FlushInternalBuffer();
      if (!flushToDisk || !this.CanWrite)
        return;
      this.FlushOSBuffer();
    }

    private void FlushInternalBuffer()
    {
      if (this._writePos > 0)
      {
        this.FlushWrite(false);
      }
      else
      {
        if (this._readPos >= this._readLen || !this.CanSeek)
          return;
        this.FlushRead();
      }
    }

    [SecuritySafeCritical]
    private void FlushOSBuffer()
    {
      if (Win32Native.FlushFileBuffers(this._handle))
        return;
      __Error.WinIOError();
    }

    private void FlushRead()
    {
      if (this._readPos - this._readLen != 0)
        this.SeekCore((long) (this._readPos - this._readLen), SeekOrigin.Current);
      this._readPos = 0;
      this._readLen = 0;
    }

    private void FlushWrite(bool calledFromFinalizer)
    {
      if (this._isAsync)
      {
        IAsyncResult asyncResult = (IAsyncResult) this.BeginWriteCore(this._buffer, 0, this._writePos, (AsyncCallback) null, (object) null);
        if (!calledFromFinalizer)
          this.EndWrite(asyncResult);
      }
      else
        this.WriteCore(this._buffer, 0, this._writePos);
      this._writePos = 0;
    }

    /// <summary>将该流的长度设置为给定值。</summary>
    /// <param name="value">流的新长度。</param>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <exception cref="T:System.NotSupportedException">流不同时支持写入和查找。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">试图将 <paramref name="value" /> 参数设置为小于 0。</exception>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    public override void SetLength(long value)
    {
      if (value < 0L)
        throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (this._handle.IsClosed)
        __Error.FileNotOpen();
      if (!this.CanSeek)
        __Error.SeekNotSupported();
      if (!this.CanWrite)
        __Error.WriteNotSupported();
      if (this._writePos > 0)
        this.FlushWrite(false);
      else if (this._readPos < this._readLen)
        this.FlushRead();
      this._readPos = 0;
      this._readLen = 0;
      if (this._appendStart != -1L && value < this._appendStart)
        throw new IOException(Environment.GetResourceString("IO.IO_SetLengthAppendTruncate"));
      this.SetLengthCore(value);
    }

    [SecuritySafeCritical]
    private void SetLengthCore(long value)
    {
      long offset = this._pos;
      if (this._exposedHandle)
        this.VerifyOSHandlePosition();
      if (this._pos != value)
        this.SeekCore(value, SeekOrigin.Begin);
      if (!Win32Native.SetEndOfFile(this._handle))
      {
        int lastWin32Error = Marshal.GetLastWin32Error();
        int num = 87;
        if (lastWin32Error == num)
          throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_FileLengthTooBig"));
        string maybeFullPath = string.Empty;
        __Error.WinIOError(lastWin32Error, maybeFullPath);
      }
      if (offset == value)
        return;
      if (offset < value)
        this.SeekCore(offset, SeekOrigin.Begin);
      else
        this.SeekCore(0L, SeekOrigin.End);
    }

    /// <summary>从流中读取字节块并将该数据写入给定缓冲区中。</summary>
    /// <returns>读入缓冲区中的总字节数。如果字节数当前不可用，则总字节数可能小于所请求的字节数；如果已到达流结尾，则为零。</returns>
    /// <param name="array">此方法返回时包含指定的字节数组，数组中 <paramref name="offset" /> 和 (<paramref name="offset" /> + <paramref name="count" /> - 1<paramref name=")" /> 之间的值由从当前源中读取的字节替换。</param>
    /// <param name="offset">
    /// <paramref name="array" /> 中的字节偏移量，将在此处放置读取的字节。</param>
    /// <param name="count">最多读取的字节数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="offset" /> 或 <paramref name="count" /> 为负。</exception>
    /// <exception cref="T:System.NotSupportedException">流不支持读取。</exception>
    /// <exception cref="T:System.IO.IOException">发生了 I/O 错误。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="offset" /> 和 <paramref name="count" /> 描述 <paramref name="array" /> 中的无效范围。</exception>
    /// <exception cref="T:System.ObjectDisposedException">在流关闭后调用方法。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    public override int Read([In, Out] byte[] array, int offset, int count)
    {
      if (array == null)
        throw new ArgumentNullException("array", Environment.GetResourceString("ArgumentNull_Buffer"));
      if (offset < 0)
        throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (array.Length - offset < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (this._handle.IsClosed)
        __Error.FileNotOpen();
      bool flag = false;
      int byteCount = this._readLen - this._readPos;
      if (byteCount == 0)
      {
        if (!this.CanRead)
          __Error.ReadNotSupported();
        if (this._writePos > 0)
          this.FlushWrite(false);
        if (!this.CanSeek || count >= this._bufferSize)
        {
          int num = this.ReadCore(array, offset, count);
          this._readPos = 0;
          this._readLen = 0;
          return num;
        }
        if (this._buffer == null)
          this._buffer = new byte[this._bufferSize];
        byteCount = this.ReadCore(this._buffer, 0, this._bufferSize);
        if (byteCount == 0)
          return 0;
        flag = byteCount < this._bufferSize;
        this._readPos = 0;
        this._readLen = byteCount;
      }
      if (byteCount > count)
        byteCount = count;
      Buffer.InternalBlockCopy((Array) this._buffer, this._readPos, (Array) array, offset, byteCount);
      this._readPos = this._readPos + byteCount;
      if (!this._isPipe && byteCount < count && !flag)
      {
        int num = this.ReadCore(array, offset + byteCount, count - byteCount);
        byteCount += num;
        this._readPos = 0;
        this._readLen = 0;
      }
      return byteCount;
    }

    [SecuritySafeCritical]
    private unsafe int ReadCore(byte[] buffer, int offset, int count)
    {
      if (this._isAsync)
        return this.EndRead((IAsyncResult) this.BeginReadCore(buffer, offset, count, (AsyncCallback) null, (object) null, 0));
      if (this._exposedHandle)
        this.VerifyOSHandlePosition();
      int hr = 0;
      int num = this.ReadFileNative(this._handle, buffer, offset, count, (NativeOverlapped*) null, out hr);
      if (num == -1)
      {
        if (hr == 109)
        {
          num = 0;
        }
        else
        {
          if (hr == 87)
            throw new ArgumentException(Environment.GetResourceString("Arg_HandleNotSync"));
          __Error.WinIOError(hr, string.Empty);
        }
      }
      this._pos = this._pos + (long) num;
      return num;
    }

    /// <summary>将该流的当前位置设置为给定值。</summary>
    /// <returns>流中的新位置。</returns>
    /// <param name="offset">相对于 <paramref name="origin" /> 的点，从此处开始查找。</param>
    /// <param name="origin">使用 <see cref="T:System.IO.SeekOrigin" /> 类型的值，将开始位置、结束位置或当前位置指定为 <paramref name="offset" /> 的参考点。</param>
    /// <exception cref="T:System.IO.IOException">发生了 I/O 错误。</exception>
    /// <exception cref="T:System.NotSupportedException">流不支持查找，例如，如果 FileStream 是由管道或控制台输出构造的。</exception>
    /// <exception cref="T:System.ArgumentException">试图在流的开始位置之前查找。</exception>
    /// <exception cref="T:System.ObjectDisposedException">在流关闭后调用方法。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    public override long Seek(long offset, SeekOrigin origin)
    {
      if (origin < SeekOrigin.Begin || origin > SeekOrigin.End)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidSeekOrigin"));
      if (this._handle.IsClosed)
        __Error.FileNotOpen();
      if (!this.CanSeek)
        __Error.SeekNotSupported();
      if (this._writePos > 0)
        this.FlushWrite(false);
      else if (origin == SeekOrigin.Current)
        offset -= (long) (this._readLen - this._readPos);
      if (this._exposedHandle)
        this.VerifyOSHandlePosition();
      long offset1 = this._pos + (long) (this._readPos - this._readLen);
      long num1 = this.SeekCore(offset, origin);
      if (this._appendStart != -1L && num1 < this._appendStart)
      {
        this.SeekCore(offset1, SeekOrigin.Begin);
        throw new IOException(Environment.GetResourceString("IO.IO_SeekAppendOverwrite"));
      }
      if (this._readLen > 0)
      {
        if (offset1 == num1)
        {
          if (this._readPos > 0)
          {
            Buffer.InternalBlockCopy((Array) this._buffer, this._readPos, (Array) this._buffer, 0, this._readLen - this._readPos);
            this._readLen = this._readLen - this._readPos;
            this._readPos = 0;
          }
          if (this._readLen > 0)
            this.SeekCore((long) this._readLen, SeekOrigin.Current);
        }
        else if (offset1 - (long) this._readPos < num1 && num1 < offset1 + (long) this._readLen - (long) this._readPos)
        {
          int num2 = (int) (num1 - offset1);
          Buffer.InternalBlockCopy((Array) this._buffer, this._readPos + num2, (Array) this._buffer, 0, this._readLen - (this._readPos + num2));
          this._readLen = this._readLen - (this._readPos + num2);
          this._readPos = 0;
          if (this._readLen > 0)
            this.SeekCore((long) this._readLen, SeekOrigin.Current);
        }
        else
        {
          this._readPos = 0;
          this._readLen = 0;
        }
      }
      return num1;
    }

    [SecuritySafeCritical]
    private long SeekCore(long offset, SeekOrigin origin)
    {
      int hr = 0;
      long num = Win32Native.SetFilePointer(this._handle, offset, origin, out hr);
      if (num == -1L)
      {
        if (hr == 6)
          this._handle.Dispose();
        __Error.WinIOError(hr, string.Empty);
      }
      this._pos = num;
      return num;
    }

    private void VerifyOSHandlePosition()
    {
      if (!this.CanSeek || this.SeekCore(0L, SeekOrigin.Current) == this._pos)
        return;
      this._readPos = 0;
      this._readLen = 0;
      if (this._writePos > 0)
      {
        this._writePos = 0;
        throw new IOException(Environment.GetResourceString("IO.IO_FileStreamHandlePosition"));
      }
    }

    /// <summary>将字节块写入文件流。</summary>
    /// <param name="array">包含要写入该流的数据的缓冲区。</param>
    /// <param name="offset">
    /// <paramref name="array" /> 中的从零开始的字节偏移量，从此处开始将字节复制到该流。</param>
    /// <param name="count">最多写入的字节数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="offset" /> 和 <paramref name="count" /> 描述 <paramref name="array" /> 中的无效范围。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="offset" /> 或 <paramref name="count" /> 为负。</exception>
    /// <exception cref="T:System.IO.IOException">发生了 I/O 错误。- 或 -另一个线程可能已导致操作系统的文件句柄位置发生意外更改。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <exception cref="T:System.NotSupportedException">当前流实例不支持写入。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    public override void Write(byte[] array, int offset, int count)
    {
      if (array == null)
        throw new ArgumentNullException("array", Environment.GetResourceString("ArgumentNull_Buffer"));
      if (offset < 0)
        throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (array.Length - offset < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (this._handle.IsClosed)
        __Error.FileNotOpen();
      if (this._writePos == 0)
      {
        if (!this.CanWrite)
          __Error.WriteNotSupported();
        if (this._readPos < this._readLen)
          this.FlushRead();
        this._readPos = 0;
        this._readLen = 0;
      }
      if (this._writePos > 0)
      {
        int byteCount = this._bufferSize - this._writePos;
        if (byteCount > 0)
        {
          if (byteCount > count)
            byteCount = count;
          Buffer.InternalBlockCopy((Array) array, offset, (Array) this._buffer, this._writePos, byteCount);
          this._writePos = this._writePos + byteCount;
          if (count == byteCount)
            return;
          offset += byteCount;
          count -= byteCount;
        }
        if (this._isAsync)
          this.EndWrite((IAsyncResult) this.BeginWriteCore(this._buffer, 0, this._writePos, (AsyncCallback) null, (object) null));
        else
          this.WriteCore(this._buffer, 0, this._writePos);
        this._writePos = 0;
      }
      if (count >= this._bufferSize)
      {
        this.WriteCore(array, offset, count);
      }
      else
      {
        if (count == 0)
          return;
        if (this._buffer == null)
          this._buffer = new byte[this._bufferSize];
        Buffer.InternalBlockCopy((Array) array, offset, (Array) this._buffer, this._writePos, count);
        this._writePos = count;
      }
    }

    [SecuritySafeCritical]
    private unsafe void WriteCore(byte[] buffer, int offset, int count)
    {
      if (this._isAsync)
      {
        this.EndWrite((IAsyncResult) this.BeginWriteCore(buffer, offset, count, (AsyncCallback) null, (object) null));
      }
      else
      {
        if (this._exposedHandle)
          this.VerifyOSHandlePosition();
        int hr = 0;
        int num = this.WriteFileNative(this._handle, buffer, offset, count, (NativeOverlapped*) null, out hr);
        if (num == -1)
        {
          if (hr == 232)
          {
            num = 0;
          }
          else
          {
            if (hr == 87)
              throw new IOException(Environment.GetResourceString("IO.IO_FileTooLongOrHandleNotSync"));
            __Error.WinIOError(hr, string.Empty);
          }
        }
        this._pos = this._pos + (long) num;
      }
    }

    /// <summary>开始异步读操作。（考虑使用 <see cref="M:System.IO.FileStream.ReadAsync(System.Byte[],System.Int32,System.Int32,System.Threading.CancellationToken)" /> 进行替换；请参见“备注”部分。）</summary>
    /// <returns>引用异步读取的对象。</returns>
    /// <param name="array">将数据读入的缓冲区。</param>
    /// <param name="offset">
    /// <paramref name="array" /> 中的字节偏移量，从此处开始读取。</param>
    /// <param name="numBytes">最多读取的字节数。</param>
    /// <param name="userCallback">异步读操作完成后调用的方法。</param>
    /// <param name="stateObject">一个用户提供的对象，它将该特定的异步读取请求与其他请求区别开来。</param>
    /// <exception cref="T:System.ArgumentException">数组长度减去 <paramref name="offset" /> 小于 <paramref name="numBytes" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="offset" /> 或 <paramref name="numBytes" /> 为负。</exception>
    /// <exception cref="T:System.IO.IOException">试图在文件的末尾之外进行异步读取。</exception>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override IAsyncResult BeginRead(byte[] array, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      if (offset < 0)
        throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (numBytes < 0)
        throw new ArgumentOutOfRangeException("numBytes", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (array.Length - offset < numBytes)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (this._handle.IsClosed)
        __Error.FileNotOpen();
      if (!this._isAsync)
        return base.BeginRead(array, offset, numBytes, userCallback, stateObject);
      return (IAsyncResult) this.BeginReadAsync(array, offset, numBytes, userCallback, stateObject);
    }

    [SecuritySafeCritical]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    private FileStreamAsyncResult BeginReadAsync(byte[] array, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
    {
      if (!this.CanRead)
        __Error.ReadNotSupported();
      if (this._isPipe)
      {
        if (this._readPos >= this._readLen)
          return this.BeginReadCore(array, offset, numBytes, userCallback, stateObject, 0);
        int num = this._readLen - this._readPos;
        if (num > numBytes)
          num = numBytes;
        Buffer.InternalBlockCopy((Array) this._buffer, this._readPos, (Array) array, offset, num);
        this._readPos = this._readPos + num;
        return FileStreamAsyncResult.CreateBufferedReadResult(num, userCallback, stateObject, false);
      }
      if (this._writePos > 0)
        this.FlushWrite(false);
      if (this._readPos == this._readLen)
      {
        if (numBytes < this._bufferSize)
        {
          if (this._buffer == null)
            this._buffer = new byte[this._bufferSize];
          this._readLen = this.EndRead((IAsyncResult) this.BeginReadCore(this._buffer, 0, this._bufferSize, (AsyncCallback) null, (object) null, 0));
          int num = this._readLen;
          if (num > numBytes)
            num = numBytes;
          Buffer.InternalBlockCopy((Array) this._buffer, 0, (Array) array, offset, num);
          this._readPos = num;
          return FileStreamAsyncResult.CreateBufferedReadResult(num, userCallback, stateObject, false);
        }
        this._readPos = 0;
        this._readLen = 0;
        return this.BeginReadCore(array, offset, numBytes, userCallback, stateObject, 0);
      }
      int num1 = this._readLen - this._readPos;
      if (num1 > numBytes)
        num1 = numBytes;
      Buffer.InternalBlockCopy((Array) this._buffer, this._readPos, (Array) array, offset, num1);
      this._readPos = this._readPos + num1;
      if (num1 >= numBytes)
        return FileStreamAsyncResult.CreateBufferedReadResult(num1, userCallback, stateObject, false);
      this._readPos = 0;
      this._readLen = 0;
      return this.BeginReadCore(array, offset + num1, numBytes - num1, userCallback, stateObject, num1);
    }

    [SecuritySafeCritical]
    private unsafe FileStreamAsyncResult BeginReadCore(byte[] bytes, int offset, int numBytes, AsyncCallback userCallback, object stateObject, int numBufferedBytesRead)
    {
      FileStreamAsyncResult streamAsyncResult = new FileStreamAsyncResult(numBufferedBytesRead, bytes, this._handle, userCallback, stateObject, false);
      NativeOverlapped* overLapped = streamAsyncResult.OverLapped;
      if (this.CanSeek)
      {
        long length = this.Length;
        if (this._exposedHandle)
          this.VerifyOSHandlePosition();
        if (this._pos + (long) numBytes > length)
          numBytes = this._pos > length ? 0 : (int) (length - this._pos);
        overLapped->OffsetLow = (int) this._pos;
        overLapped->OffsetHigh = (int) (this._pos >> 32);
        this.SeekCore((long) numBytes, SeekOrigin.Current);
      }
      if (FrameworkEventSource.IsInitialized && FrameworkEventSource.Log.IsEnabled(EventLevel.Informational, (EventKeywords) 16))
        FrameworkEventSource.Log.ThreadTransferSend((long) streamAsyncResult.OverLapped, 2, string.Empty, false);
      int hr = 0;
      if (this.ReadFileNative(this._handle, bytes, offset, numBytes, overLapped, out hr) == -1 && numBytes != -1)
      {
        if (hr == 109)
        {
          overLapped->InternalLow = IntPtr.Zero;
          streamAsyncResult.CallUserCallback();
        }
        else if (hr != 997)
        {
          if (!this._handle.IsClosed && this.CanSeek)
            this.SeekCore(0L, SeekOrigin.Current);
          if (hr == 38)
            __Error.EndOfFile();
          else
            __Error.WinIOError(hr, string.Empty);
        }
      }
      return streamAsyncResult;
    }

    /// <summary>等待挂起的异步读操作完成。（考虑使用 <see cref="M:System.IO.FileStream.ReadAsync(System.Byte[],System.Int32,System.Int32,System.Threading.CancellationToken)" /> 进行替换；请参见“备注”部分。）</summary>
    /// <returns>从流中读取的字节数，介于 0 和所请求的字节数之间。流仅在流结尾返回 0，否则在至少有 1 个字节可用之前应一直进行阻止。。</returns>
    /// <param name="asyncResult">对所等待的挂起异步请求的引用。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="asyncResult" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">此 <see cref="T:System.IAsyncResult" /> 对象不是通过对该类调用 <see cref="M:System.IO.FileStream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> 来创建的。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <see cref="M:System.IO.FileStream.EndRead(System.IAsyncResult)" /> 被多次调用。</exception>
    /// <exception cref="T:System.IO.IOException">此流关闭或发生内部错误。</exception>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    public override int EndRead(IAsyncResult asyncResult)
    {
      if (asyncResult == null)
        throw new ArgumentNullException("asyncResult");
      if (!this._isAsync)
        return base.EndRead(asyncResult);
      FileStreamAsyncResult streamAsyncResult = asyncResult as FileStreamAsyncResult;
      if (streamAsyncResult == null || streamAsyncResult.IsWrite)
        __Error.WrongAsyncResult();
      if (1 == Interlocked.CompareExchange(ref streamAsyncResult._EndXxxCalled, 1, 0))
        __Error.EndReadCalledTwice();
      streamAsyncResult.Wait();
      streamAsyncResult.ReleaseNativeResource();
      if (streamAsyncResult.ErrorCode != 0)
        __Error.WinIOError(streamAsyncResult.ErrorCode, string.Empty);
      return streamAsyncResult.NumBytesRead;
    }

    /// <summary>从文件中读取一个字节，并将读取位置提升一个字节。</summary>
    /// <returns>强制转换为 <see cref="T:System.Int32" /> 的字节；或者如果已到达流的末尾，则为 -1。</returns>
    /// <exception cref="T:System.NotSupportedException">当前流不支持读取。</exception>
    /// <exception cref="T:System.ObjectDisposedException">当前流已关闭。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    public override int ReadByte()
    {
      if (this._handle.IsClosed)
        __Error.FileNotOpen();
      if (this._readLen == 0 && !this.CanRead)
        __Error.ReadNotSupported();
      if (this._readPos == this._readLen)
      {
        if (this._writePos > 0)
          this.FlushWrite(false);
        if (this._buffer == null)
          this._buffer = new byte[this._bufferSize];
        this._readLen = this.ReadCore(this._buffer, 0, this._bufferSize);
        this._readPos = 0;
      }
      if (this._readPos == this._readLen)
        return -1;
      int num = (int) this._buffer[this._readPos];
      this._readPos = this._readPos + 1;
      return num;
    }

    /// <summary>开始异步写操作。（考虑使用 <see cref="M:System.IO.FileStream.WriteAsync(System.Byte[],System.Int32,System.Int32,System.Threading.CancellationToken)" /> 进行替换；请参见“备注”部分。）</summary>
    /// <returns>引用异步写入的对象。</returns>
    /// <param name="array">包含要写入当前流的数据的缓冲区。</param>
    /// <param name="offset">
    /// <paramref name="array" /> 中的从零开始的字节偏移量，从此处开始将字节复制到当前流。</param>
    /// <param name="numBytes">最多写入的字节数。</param>
    /// <param name="userCallback">异步写操作完成后调用的方法。</param>
    /// <param name="stateObject">一个用户提供的对象，它将该特定的异步写入请求与其他请求区别开来。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="array" /> 长度减去 <paramref name="offset" /> 小于 <paramref name="numBytes" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="offset" /> 或 <paramref name="numBytes" /> 为负。</exception>
    /// <exception cref="T:System.NotSupportedException">流不支持写入。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <exception cref="T:System.IO.IOException">发生了 I/O 错误。</exception>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override IAsyncResult BeginWrite(byte[] array, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      if (offset < 0)
        throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (numBytes < 0)
        throw new ArgumentOutOfRangeException("numBytes", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (array.Length - offset < numBytes)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (this._handle.IsClosed)
        __Error.FileNotOpen();
      if (!this._isAsync)
        return base.BeginWrite(array, offset, numBytes, userCallback, stateObject);
      return (IAsyncResult) this.BeginWriteAsync(array, offset, numBytes, userCallback, stateObject);
    }

    [SecuritySafeCritical]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    private FileStreamAsyncResult BeginWriteAsync(byte[] array, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
    {
      if (!this.CanWrite)
        __Error.WriteNotSupported();
      if (this._isPipe)
      {
        if (this._writePos > 0)
          this.FlushWrite(false);
        return this.BeginWriteCore(array, offset, numBytes, userCallback, stateObject);
      }
      if (this._writePos == 0)
      {
        if (this._readPos < this._readLen)
          this.FlushRead();
        this._readPos = 0;
        this._readLen = 0;
      }
      int num = this._bufferSize - this._writePos;
      if (numBytes <= num)
      {
        if (this._writePos == 0)
          this._buffer = new byte[this._bufferSize];
        Buffer.InternalBlockCopy((Array) array, offset, (Array) this._buffer, this._writePos, numBytes);
        this._writePos = this._writePos + numBytes;
        return FileStreamAsyncResult.CreateBufferedReadResult(numBytes, userCallback, stateObject, true);
      }
      if (this._writePos > 0)
        this.FlushWrite(false);
      return this.BeginWriteCore(array, offset, numBytes, userCallback, stateObject);
    }

    [SecuritySafeCritical]
    private unsafe FileStreamAsyncResult BeginWriteCore(byte[] bytes, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
    {
      FileStreamAsyncResult streamAsyncResult = new FileStreamAsyncResult(0, bytes, this._handle, userCallback, stateObject, true);
      NativeOverlapped* overLapped = streamAsyncResult.OverLapped;
      if (this.CanSeek)
      {
        long length = this.Length;
        if (this._exposedHandle)
          this.VerifyOSHandlePosition();
        if (this._pos + (long) numBytes > length)
          this.SetLengthCore(this._pos + (long) numBytes);
        overLapped->OffsetLow = (int) this._pos;
        overLapped->OffsetHigh = (int) (this._pos >> 32);
        this.SeekCore((long) numBytes, SeekOrigin.Current);
      }
      if (FrameworkEventSource.IsInitialized && FrameworkEventSource.Log.IsEnabled(EventLevel.Informational, (EventKeywords) 16))
        FrameworkEventSource.Log.ThreadTransferSend((long) streamAsyncResult.OverLapped, 2, string.Empty, false);
      int hr = 0;
      if (this.WriteFileNative(this._handle, bytes, offset, numBytes, overLapped, out hr) == -1 && numBytes != -1)
      {
        if (hr == 232)
          streamAsyncResult.CallUserCallback();
        else if (hr != 997)
        {
          if (!this._handle.IsClosed && this.CanSeek)
            this.SeekCore(0L, SeekOrigin.Current);
          if (hr == 38)
            __Error.EndOfFile();
          else
            __Error.WinIOError(hr, string.Empty);
        }
      }
      return streamAsyncResult;
    }

    /// <summary>结束异步写入操作，在 I/O 操作完成之前一直阻止。（考虑使用 <see cref="M:System.IO.FileStream.WriteAsync(System.Byte[],System.Int32,System.Int32,System.Threading.CancellationToken)" /> 进行替换；请参见“备注”部分。）</summary>
    /// <param name="asyncResult">挂起的异步 I/O 请求。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="asyncResult" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">此 <see cref="T:System.IAsyncResult" /> 对象不是通过对该类调用 <see cref="M:System.IO.Stream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> 来创建的。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <see cref="M:System.IO.FileStream.EndWrite(System.IAsyncResult)" /> 被多次调用。</exception>
    /// <exception cref="T:System.IO.IOException">此流关闭或发生内部错误。</exception>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    public override void EndWrite(IAsyncResult asyncResult)
    {
      if (asyncResult == null)
        throw new ArgumentNullException("asyncResult");
      if (!this._isAsync)
      {
        base.EndWrite(asyncResult);
      }
      else
      {
        FileStreamAsyncResult streamAsyncResult = asyncResult as FileStreamAsyncResult;
        if (streamAsyncResult == null || !streamAsyncResult.IsWrite)
          __Error.WrongAsyncResult();
        if (1 == Interlocked.CompareExchange(ref streamAsyncResult._EndXxxCalled, 1, 0))
          __Error.EndWriteCalledTwice();
        streamAsyncResult.Wait();
        streamAsyncResult.ReleaseNativeResource();
        if (streamAsyncResult.ErrorCode == 0)
          return;
        __Error.WinIOError(streamAsyncResult.ErrorCode, string.Empty);
      }
    }

    /// <summary>一个字节写入文件流中的当前位置。</summary>
    /// <param name="value">要写入流的字节。</param>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <exception cref="T:System.NotSupportedException">流不支持写入。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    public override void WriteByte(byte value)
    {
      if (this._handle.IsClosed)
        __Error.FileNotOpen();
      if (this._writePos == 0)
      {
        if (!this.CanWrite)
          __Error.WriteNotSupported();
        if (this._readPos < this._readLen)
          this.FlushRead();
        this._readPos = 0;
        this._readLen = 0;
        if (this._buffer == null)
          this._buffer = new byte[this._bufferSize];
      }
      if (this._writePos == this._bufferSize)
        this.FlushWrite(false);
      this._buffer[this._writePos] = value;
      this._writePos = this._writePos + 1;
    }

    /// <summary>防止其他进程读取或写入 <see cref="T:System.IO.FileStream" />。</summary>
    /// <param name="position">要锁定的范围的起始处。此参数的值必须大于或等于零 (0)。</param>
    /// <param name="length">要锁定的范围。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="position" /> 或 <paramref name="length" /> 为负。</exception>
    /// <exception cref="T:System.ObjectDisposedException">文件被关闭。</exception>
    /// <exception cref="T:System.IO.IOException">由于另一个进程已锁定文件的部分内容，因此该进程无法访问该文件。</exception>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    public virtual void Lock(long position, long length)
    {
      if (position < 0L || length < 0L)
        throw new ArgumentOutOfRangeException(position < 0L ? "position" : "length", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (this._handle.IsClosed)
        __Error.FileNotOpen();
      if (Win32Native.LockFile(this._handle, (int) position, (int) (position >> 32), (int) length, (int) (length >> 32)))
        return;
      __Error.WinIOError();
    }

    /// <summary>允许其他进程访问以前锁定的某个文件的全部或部分。</summary>
    /// <param name="position">要取消锁定的范围的开始处。</param>
    /// <param name="length">要取消锁定的范围。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="position" /> 或 <paramref name="length" /> 为负。</exception>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    public virtual void Unlock(long position, long length)
    {
      if (position < 0L || length < 0L)
        throw new ArgumentOutOfRangeException(position < 0L ? "position" : "length", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (this._handle.IsClosed)
        __Error.FileNotOpen();
      if (Win32Native.UnlockFile(this._handle, (int) position, (int) (position >> 32), (int) length, (int) (length >> 32)))
        return;
      __Error.WinIOError();
    }

    [SecurityCritical]
    private unsafe int ReadFileNative(SafeFileHandle handle, byte[] bytes, int offset, int count, NativeOverlapped* overlapped, out int hr)
    {
      if (bytes.Length - offset < count)
        throw new IndexOutOfRangeException(Environment.GetResourceString("IndexOutOfRange_IORaceCondition"));
      if (bytes.Length == 0)
      {
        hr = 0;
        return 0;
      }
      int numBytesRead = 0;
      int num;
      fixed (byte* numPtr = bytes)
        num = !this._isAsync ? Win32Native.ReadFile(handle, numPtr + offset, count, out numBytesRead, IntPtr.Zero) : Win32Native.ReadFile(handle, numPtr + offset, count, IntPtr.Zero, overlapped);
      if (num == 0)
      {
        hr = Marshal.GetLastWin32Error();
        if (hr == 109 || hr == 233 || hr != 6)
          return -1;
        this._handle.Dispose();
        return -1;
      }
      hr = 0;
      return numBytesRead;
    }

    [SecurityCritical]
    private unsafe int WriteFileNative(SafeFileHandle handle, byte[] bytes, int offset, int count, NativeOverlapped* overlapped, out int hr)
    {
      if (bytes.Length - offset < count)
        throw new IndexOutOfRangeException(Environment.GetResourceString("IndexOutOfRange_IORaceCondition"));
      if (bytes.Length == 0)
      {
        hr = 0;
        return 0;
      }
      int numBytesWritten = 0;
      int num;
      fixed (byte* numPtr = bytes)
        num = !this._isAsync ? Win32Native.WriteFile(handle, numPtr + offset, count, out numBytesWritten, IntPtr.Zero) : Win32Native.WriteFile(handle, numPtr + offset, count, IntPtr.Zero, overlapped);
      if (num == 0)
      {
        hr = Marshal.GetLastWin32Error();
        if (hr == 232 || hr != 6)
          return -1;
        this._handle.Dispose();
        return -1;
      }
      hr = 0;
      return numBytesWritten;
    }

    /// <summary>从当前流异步读取字节的序列，将流中的位置提升读取的字节数，并监视取消请求。</summary>
    /// <returns>表示异步读取操作的任务。<paramref name="TResult" /> 参数的值包含读入缓冲区的总字节数。如果当前可用字节数少于所请求的字节数，则该结果值可小于所请求的字节数；如果已到达流结尾时，则为 0（零）。</returns>
    /// <param name="buffer">数据写入的缓冲区。</param>
    /// <param name="offset">
    /// <paramref name="buffer" /> 中的字节偏移量，从该偏移量开始写入从流中读取的数据。</param>
    /// <param name="count">最多读取的字节数。</param>
    /// <param name="cancellationToken">要监视取消请求的标记。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="offset" /> 或 <paramref name="count" /> 为负。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="offset" /> 与 <paramref name="count" /> 的和大于缓冲区长度。</exception>
    /// <exception cref="T:System.NotSupportedException">流不支持读取。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已被释放。</exception>
    /// <exception cref="T:System.InvalidOperationException">该流正在由其前一次读取操作使用。</exception>
    [ComVisible(false)]
    [SecuritySafeCritical]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
      if (buffer == null)
        throw new ArgumentNullException("buffer");
      if (offset < 0)
        throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - offset < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (this.GetType() != typeof (FileStream))
        return base.ReadAsync(buffer, offset, count, cancellationToken);
      if (cancellationToken.IsCancellationRequested)
        return Task.FromCancellation<int>(cancellationToken);
      if (this._handle.IsClosed)
        __Error.FileNotOpen();
      if (!this._isAsync)
        return base.ReadAsync(buffer, offset, count, cancellationToken);
      FileStream.FileStreamReadWriteTask<int> streamReadWriteTask = new FileStream.FileStreamReadWriteTask<int>(cancellationToken);
      AsyncCallback userCallback = FileStream.s_endReadTask;
      if (userCallback == null)
        FileStream.s_endReadTask = userCallback = new AsyncCallback(FileStream.EndReadTask);
      streamReadWriteTask._asyncResult = this.BeginReadAsync(buffer, offset, count, userCallback, (object) streamReadWriteTask);
      if (streamReadWriteTask._asyncResult.IsAsync && cancellationToken.CanBeCanceled)
      {
        Action<object> callback = FileStream.s_cancelReadHandler;
        if (callback == null)
          FileStream.s_cancelReadHandler = callback = new Action<object>(FileStream.CancelTask<int>);
        streamReadWriteTask._registration = cancellationToken.Register(callback, (object) streamReadWriteTask);
        if (streamReadWriteTask._asyncResult.IsCompleted)
          streamReadWriteTask._registration.Dispose();
      }
      return (Task<int>) streamReadWriteTask;
    }

    /// <summary>将字节的序列异步写入当前流，将该流中的当前位置向前移动写入的字节数，并监视取消请求。</summary>
    /// <returns>表示异步写入操作的任务。</returns>
    /// <param name="buffer">从中写入数据的缓冲区。</param>
    /// <param name="offset">
    /// <paramref name="buffer" /> 中的从零开始的字节偏移量，从此处开始将字节复制到该流。</param>
    /// <param name="count">最多写入的字节数。</param>
    /// <param name="cancellationToken">要监视取消请求的标记。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="offset" /> 或 <paramref name="count" /> 为负。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="offset" /> 与 <paramref name="count" /> 的和大于缓冲区长度。</exception>
    /// <exception cref="T:System.NotSupportedException">流不支持写入。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已被释放。</exception>
    /// <exception cref="T:System.InvalidOperationException">该流正在由其前一次写入操作使用。</exception>
    [ComVisible(false)]
    [SecuritySafeCritical]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
      if (buffer == null)
        throw new ArgumentNullException("buffer");
      if (offset < 0)
        throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - offset < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (this.GetType() != typeof (FileStream))
        return base.WriteAsync(buffer, offset, count, cancellationToken);
      if (cancellationToken.IsCancellationRequested)
        return Task.FromCancellation(cancellationToken);
      if (this._handle.IsClosed)
        __Error.FileNotOpen();
      if (!this._isAsync)
        return base.WriteAsync(buffer, offset, count, cancellationToken);
      FileStream.FileStreamReadWriteTask<VoidTaskResult> streamReadWriteTask = new FileStream.FileStreamReadWriteTask<VoidTaskResult>(cancellationToken);
      AsyncCallback userCallback = FileStream.s_endWriteTask;
      if (userCallback == null)
        FileStream.s_endWriteTask = userCallback = new AsyncCallback(FileStream.EndWriteTask);
      streamReadWriteTask._asyncResult = this.BeginWriteAsync(buffer, offset, count, userCallback, (object) streamReadWriteTask);
      if (streamReadWriteTask._asyncResult.IsAsync && cancellationToken.CanBeCanceled)
      {
        Action<object> callback = FileStream.s_cancelWriteHandler;
        if (callback == null)
          FileStream.s_cancelWriteHandler = callback = new Action<object>(FileStream.CancelTask<VoidTaskResult>);
        streamReadWriteTask._registration = cancellationToken.Register(callback, (object) streamReadWriteTask);
        if (streamReadWriteTask._asyncResult.IsCompleted)
          streamReadWriteTask._registration.Dispose();
      }
      return (Task) streamReadWriteTask;
    }

    [SecuritySafeCritical]
    private static void CancelTask<T>(object state)
    {
      FileStream.FileStreamReadWriteTask<T> streamReadWriteTask = state as FileStream.FileStreamReadWriteTask<T>;
      FileStreamAsyncResult streamAsyncResult = streamReadWriteTask._asyncResult;
      try
      {
        if (streamAsyncResult.IsCompleted)
          return;
        streamAsyncResult.Cancel();
      }
      catch (Exception ex)
      {
        streamReadWriteTask.TrySetException((object) ex);
      }
    }

    [SecuritySafeCritical]
    private static void EndReadTask(IAsyncResult iar)
    {
      FileStreamAsyncResult streamAsyncResult = iar as FileStreamAsyncResult;
      FileStream.FileStreamReadWriteTask<int> streamReadWriteTask = streamAsyncResult.AsyncState as FileStream.FileStreamReadWriteTask<int>;
      try
      {
        if (streamAsyncResult.IsAsync)
        {
          streamAsyncResult.ReleaseNativeResource();
          streamReadWriteTask._registration.Dispose();
        }
        if (streamAsyncResult.ErrorCode == 995)
        {
          CancellationToken tokenToRecord = streamReadWriteTask._cancellationToken;
          streamReadWriteTask.TrySetCanceled(tokenToRecord);
        }
        else
          streamReadWriteTask.TrySetResult(streamAsyncResult.NumBytesRead);
      }
      catch (Exception ex)
      {
        streamReadWriteTask.TrySetException((object) ex);
      }
    }

    [SecuritySafeCritical]
    private static void EndWriteTask(IAsyncResult iar)
    {
      FileStreamAsyncResult streamAsyncResult = iar as FileStreamAsyncResult;
      FileStream.FileStreamReadWriteTask<VoidTaskResult> streamReadWriteTask = iar.AsyncState as FileStream.FileStreamReadWriteTask<VoidTaskResult>;
      try
      {
        if (streamAsyncResult.IsAsync)
        {
          streamAsyncResult.ReleaseNativeResource();
          streamReadWriteTask._registration.Dispose();
        }
        if (streamAsyncResult.ErrorCode == 995)
        {
          CancellationToken tokenToRecord = streamReadWriteTask._cancellationToken;
          streamReadWriteTask.TrySetCanceled(tokenToRecord);
        }
        else
          streamReadWriteTask.TrySetResult(new VoidTaskResult());
      }
      catch (Exception ex)
      {
        streamReadWriteTask.TrySetException((object) ex);
      }
    }

    /// <summary>异步清理这个流的所有缓冲区，并使所有缓冲数据写入基础设备，并且监控取消请求。</summary>
    /// <returns>表示异步刷新操作的任务。</returns>
    /// <param name="cancellationToken">要监视取消请求的标记。</param>
    /// <exception cref="T:System.ObjectDisposedException">流已被释放。</exception>
    [ComVisible(false)]
    [SecuritySafeCritical]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override Task FlushAsync(CancellationToken cancellationToken)
    {
      if (this.GetType() != typeof (FileStream))
        return base.FlushAsync(cancellationToken);
      if (cancellationToken.IsCancellationRequested)
        return Task.FromCancellation(cancellationToken);
      if (this._handle.IsClosed)
        __Error.FileNotOpen();
      try
      {
        this.FlushInternalBuffer();
      }
      catch (Exception ex)
      {
        return Task.FromException(ex);
      }
      if (!this.CanWrite)
        return Task.CompletedTask;
      TaskFactory factory = Task.Factory;
      CancellationToken cancellationToken1 = cancellationToken;
      int num = 8;
      TaskScheduler @default = TaskScheduler.Default;
      return factory.StartNew((Action<object>) (state => ((FileStream) state).FlushOSBuffer()), (object) this, cancellationToken1, (TaskCreationOptions) num, @default);
    }

    private sealed class FileStreamReadWriteTask<T> : Task<T>
    {
      internal CancellationToken _cancellationToken;
      internal CancellationTokenRegistration _registration;
      internal FileStreamAsyncResult _asyncResult;

      internal FileStreamReadWriteTask(CancellationToken cancellationToken)
      {
        this._cancellationToken = cancellationToken;
      }
    }
  }
}
