// Decompiled with JetBrains decompiler
// Type: System.IO.IsolatedStorage.IsolatedStorageFileStream
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.IO.IsolatedStorage
{
  /// <summary>公开独立存储中的文件。</summary>
  [ComVisible(true)]
  public class IsolatedStorageFileStream : FileStream
  {
    private const int s_BlockSize = 1024;
    private const string s_BackSlash = "\\";
    private FileStream m_fs;
    private IsolatedStorageFile m_isf;
    private string m_GivenPath;
    private string m_FullPath;
    private bool m_OwnedStore;

    /// <summary>获取一个布尔值，该值指示该文件是否可读。</summary>
    /// <returns>如果 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> 对象可读，则为 true；否则为 false。</returns>
    public override bool CanRead
    {
      get
      {
        return this.m_fs.CanRead;
      }
    }

    /// <summary>获取一个布尔值，该值指示是否可以写入文件。</summary>
    /// <returns>如果可以写入 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> 对象，则为 true；否则为 false。</returns>
    public override bool CanWrite
    {
      get
      {
        return this.m_fs.CanWrite;
      }
    }

    /// <summary>获取一个布尔值，该值指示查找操作是否受支持。</summary>
    /// <returns>如果 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> 对象支持查找操作，则为 true；否则为 false。</returns>
    public override bool CanSeek
    {
      get
      {
        return this.m_fs.CanSeek;
      }
    }

    /// <summary>获取一个布尔值，该值指示 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> 对象是异步打开的还是同步打开的。</summary>
    /// <returns>如果 true 对象支持异步访问，则为 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" />；否则为 false。</returns>
    public override bool IsAsync
    {
      get
      {
        return this.m_fs.IsAsync;
      }
    }

    /// <summary>获取 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> 对象的长度。</summary>
    /// <returns>
    /// <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> 对象的长度（以字节为单位）。</returns>
    public override long Length
    {
      get
      {
        return this.m_fs.Length;
      }
    }

    /// <summary>获取或设置当前 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> 对象的当前位置。</summary>
    /// <returns>此 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> 对象的当前位置。</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">此位置不能设置为负数。</exception>
    public override long Position
    {
      get
      {
        return this.m_fs.Position;
      }
      set
      {
        if (value < 0L)
          throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        this.Seek(value, SeekOrigin.Begin);
      }
    }

    /// <summary>获取当前 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> 对象封装的文件的文件句柄。不允许在 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> 对象上访问此属性，如果访问，将引发 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageException" />。</summary>
    /// <returns>当前 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> 对象封装的文件的文件句柄。</returns>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    /// <see cref="P:System.IO.IsolatedStorage.IsolatedStorageFileStream.Handle" /> 属性总是生成此异常。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [Obsolete("This property has been deprecated.  Please use IsolatedStorageFileStream's SafeFileHandle property instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
    public override IntPtr Handle
    {
      [SecurityCritical, SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        this.NotPermittedError();
        return Win32Native.INVALID_HANDLE_VALUE;
      }
    }

    /// <summary>获取 <see cref="T:Microsoft.Win32.SafeHandles.SafeFileHandle" /> 对象，它代表当前 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> 对象所封装的文件的操作系统文件句柄。</summary>
    /// <returns>
    /// <see cref="T:Microsoft.Win32.SafeHandles.SafeFileHandle" /> 对象，它表示当前 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> 对象所封装的文件的操作系统文件句柄。</returns>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    /// <see cref="P:System.IO.IsolatedStorage.IsolatedStorageFileStream.SafeFileHandle" /> 属性总是生成此异常。</exception>
    public override SafeFileHandle SafeFileHandle
    {
      [SecurityCritical, SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        this.NotPermittedError();
        return (SafeFileHandle) null;
      }
    }

    private IsolatedStorageFileStream()
    {
    }

    /// <summary>初始化 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> 对象的新实例，通过该实例可以访问指定 <paramref name="mode" /> 中的 <paramref name="path" /> 指定的文件。</summary>
    /// <param name="path">独立存储区内文件的相对路径。</param>
    /// <param name="mode">
    /// <see cref="T:System.IO.FileMode" /> 值之一。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 的格式错误。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    /// <paramref name="path" /> 中的目录不存在。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">未找到文件，而 <paramref name="mode" /> 设置为 <see cref="F:System.IO.FileMode.Open" />。</exception>
    public IsolatedStorageFileStream(string path, FileMode mode)
    {
      string path1 = path;
      int num1 = (int) mode;
      int num2 = 6;
      int num3 = num1 == num2 ? 2 : 3;
      int num4 = 0;
      // ISSUE: variable of the null type
      __Null local = null;
      // ISSUE: explicit constructor call
      this.\u002Ector(path1, (FileMode) num1, (FileAccess) num3, (FileShare) num4, (IsolatedStorageFile) local);
    }

    /// <summary>初始化 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> 类的一个新实例，以便可以在 <paramref name="isf" /> 指定的 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFile" /> 的上下文中，以指定的 <paramref name="mode" /> 来访问 <paramref name="path" /> 所指定的文件。</summary>
    /// <param name="path">独立存储区内文件的相对路径。</param>
    /// <param name="mode">
    /// <see cref="T:System.IO.FileMode" /> 值之一。</param>
    /// <param name="isf">要在其中打开 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> 的 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFile" />。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 的格式错误。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">未找到文件，而 <paramref name="mode" /> 设置为 <see cref="F:System.IO.FileMode.Open" />。</exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    /// <paramref name="isf" /> 没有配额。</exception>
    public IsolatedStorageFileStream(string path, FileMode mode, IsolatedStorageFile isf)
    {
      string path1 = path;
      int num1 = (int) mode;
      int num2 = 6;
      int num3 = num1 == num2 ? 2 : 3;
      int num4 = 0;
      IsolatedStorageFile isf1 = isf;
      // ISSUE: explicit constructor call
      this.\u002Ector(path1, (FileMode) num1, (FileAccess) num3, (FileShare) num4, isf1);
    }

    /// <summary>初始化 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> 类的一个新实例，以便可以指定的 <paramref name="mode" />、用请求类型的 <paramref name="access" /> 访问 <paramref name="path" /> 所指定的文件。</summary>
    /// <param name="path">独立存储区内文件的相对路径。</param>
    /// <param name="mode">
    /// <see cref="T:System.IO.FileMode" /> 值之一。</param>
    /// <param name="access">
    /// <see cref="T:System.IO.FileAccess" /> 值的按位组合。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 的格式错误。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">未找到文件，而 <paramref name="mode" /> 设置为 <see cref="F:System.IO.FileMode.Open" />。</exception>
    public IsolatedStorageFileStream(string path, FileMode mode, FileAccess access)
    {
      string path1 = path;
      int num1 = (int) mode;
      int num2 = (int) access;
      int num3 = 1;
      int num4 = num2 == num3 ? 1 : 0;
      int bufferSize = 4096;
      // ISSUE: variable of the null type
      __Null local = null;
      // ISSUE: explicit constructor call
      this.\u002Ector(path1, (FileMode) num1, (FileAccess) num2, (FileShare) num4, bufferSize, (IsolatedStorageFile) local);
    }

    /// <summary>初始化 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> 类的一个新实例，以便可以在 <paramref name="isf" /> 所指定的 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFile" /> 的上下文中，以指定的 <paramref name="mode" />、用指定的文件 <paramref name="access" /> 来访问 <paramref name="path" /> 所指定的文件。</summary>
    /// <param name="path">独立存储区内文件的相对路径。</param>
    /// <param name="mode">
    /// <see cref="T:System.IO.FileMode" /> 值之一。</param>
    /// <param name="access">
    /// <see cref="T:System.IO.FileAccess" /> 值的按位组合。</param>
    /// <param name="isf">要在其中打开 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> 的 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFile" />。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 的格式错误。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.ObjectDisposedException">独立存储区已关闭。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">未找到文件，而 <paramref name="mode" /> 设置为 <see cref="F:System.IO.FileMode.Open" />。</exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    /// <paramref name="isf" /> 没有配额。</exception>
    public IsolatedStorageFileStream(string path, FileMode mode, FileAccess access, IsolatedStorageFile isf)
    {
      string path1 = path;
      int num1 = (int) mode;
      int num2 = (int) access;
      int num3 = 1;
      int num4 = num2 == num3 ? 1 : 0;
      int bufferSize = 4096;
      IsolatedStorageFile isf1 = isf;
      // ISSUE: explicit constructor call
      this.\u002Ector(path1, (FileMode) num1, (FileAccess) num2, (FileShare) num4, bufferSize, isf1);
    }

    /// <summary>初始化 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> 类的一个新实例，以便可以使用 <paramref name="share" /> 指定的文件共享模式，以指定的 <paramref name="mode" />、用指定的文件 <paramref name="access" /> 访问 <paramref name="path" /> 所指定的文件。</summary>
    /// <param name="path">独立存储区内文件的相对路径。</param>
    /// <param name="mode">
    /// <see cref="T:System.IO.FileMode" /> 值之一。</param>
    /// <param name="access">
    /// <see cref="T:System.IO.FileAccess" /> 值的按位组合。</param>
    /// <param name="share">
    /// <see cref="T:System.IO.FileShare" /> 值的按位组合。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 的格式错误。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">未找到文件，而 <paramref name="mode" /> 设置为 <see cref="F:System.IO.FileMode.Open" />。</exception>
    public IsolatedStorageFileStream(string path, FileMode mode, FileAccess access, FileShare share)
      : this(path, mode, access, share, 4096, (IsolatedStorageFile) null)
    {
    }

    /// <summary>初始化 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> 类的一个新实例，以便可以在 <paramref name="isf" /> 指定的 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFile" /> 的上下文中，使用 <paramref name="share" /> 指定的文件共享模式，以指定的 <paramref name="mode" />、用指定的文件 <paramref name="access" /> 来访问 <paramref name="path" /> 所指定的文件。</summary>
    /// <param name="path">独立存储区内文件的相对路径。</param>
    /// <param name="mode">
    /// <see cref="T:System.IO.FileMode" /> 值之一。</param>
    /// <param name="access">
    /// <see cref="T:System.IO.FileAccess" /> 值的按位组合。</param>
    /// <param name="share">
    /// <see cref="T:System.IO.FileShare" /> 值的按位组合。</param>
    /// <param name="isf">要在其中打开 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> 的 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFile" />。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 的格式错误。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">未找到文件，而 <paramref name="mode" /> 设置为 <see cref="F:System.IO.FileMode.Open" />。</exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    /// <paramref name="isf" /> 没有配额。</exception>
    public IsolatedStorageFileStream(string path, FileMode mode, FileAccess access, FileShare share, IsolatedStorageFile isf)
      : this(path, mode, access, share, 4096, isf)
    {
    }

    /// <summary>初始化 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> 类的一个新实例，以便可以使用 <paramref name="share" /> 指定的文件共享模式（指定了 <paramref name="buffersize" />），以指定的 <paramref name="mode" />、用指定的文件 <paramref name="access" /> 访问 <paramref name="path" /> 所指定的文件。</summary>
    /// <param name="path">独立存储区内文件的相对路径。</param>
    /// <param name="mode">
    /// <see cref="T:System.IO.FileMode" /> 值之一。</param>
    /// <param name="access">
    /// <see cref="T:System.IO.FileAccess" /> 值的按位组合。</param>
    /// <param name="share">
    /// <see cref="T:System.IO.FileShare" /> 值的按位组合。</param>
    /// <param name="bufferSize">
    /// <see cref="T:System.IO.FileStream" /> 缓冲区的大小。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 的格式错误。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">未找到文件，而 <paramref name="mode" /> 设置为 <see cref="F:System.IO.FileMode.Open" />。</exception>
    public IsolatedStorageFileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize)
      : this(path, mode, access, share, bufferSize, (IsolatedStorageFile) null)
    {
    }

    /// <summary>初始化 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> 类的一个新实例，以便可以在 <paramref name="isf" /> 指定的 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFile" /> 的上下文中，使用 <paramref name="share" /> 指定的文件享模式（指定了 <paramref name="buffersize" />），以指定的 <paramref name="mode" />、用指定的文件 <paramref name="access" /> 来访问 <paramref name="path" /> 所指定的文件。</summary>
    /// <param name="path">独立存储区内文件的相对路径。</param>
    /// <param name="mode">
    /// <see cref="T:System.IO.FileMode" /> 值之一。</param>
    /// <param name="access">
    /// <see cref="T:System.IO.FileAccess" /> 值的按位组合。</param>
    /// <param name="share">
    /// <see cref="T:System.IO.FileShare" /> 值的按位组合 </param>
    /// <param name="bufferSize">
    /// <see cref="T:System.IO.FileStream" /> 缓冲区的大小。</param>
    /// <param name="isf">要在其中打开 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> 的 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFile" />。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 的格式错误。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">未找到文件，而 <paramref name="mode" /> 设置为 <see cref="F:System.IO.FileMode.Open" />。</exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
    /// <paramref name="isf" /> 没有配额。</exception>
    [SecuritySafeCritical]
    public IsolatedStorageFileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, IsolatedStorageFile isf)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (path.Length == 0 || path.Equals("\\"))
        throw new ArgumentException(Environment.GetResourceString("IsolatedStorage_Path"));
      if (isf == null)
      {
        this.m_OwnedStore = true;
        isf = IsolatedStorageFile.GetUserStoreForDomain();
      }
      if (isf.Disposed)
        throw new ObjectDisposedException((string) null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
      switch (mode)
      {
        case FileMode.CreateNew:
        case FileMode.Create:
        case FileMode.Open:
        case FileMode.OpenOrCreate:
        case FileMode.Truncate:
        case FileMode.Append:
          this.m_isf = isf;
          FileIOPermission fileIoPermission = new FileIOPermission(FileIOPermissionAccess.AllAccess, this.m_isf.RootDirectory);
          fileIoPermission.Assert();
          fileIoPermission.PermitOnly();
          this.m_GivenPath = path;
          this.m_FullPath = this.m_isf.GetFullPath(this.m_GivenPath);
          ulong num = 0;
          bool flag = false;
          bool locked = false;
          RuntimeHelpers.PrepareConstrainedRegions();
          try
          {
            switch (mode)
            {
              case FileMode.CreateNew:
                flag = true;
                break;
              case FileMode.Create:
              case FileMode.OpenOrCreate:
              case FileMode.Truncate:
              case FileMode.Append:
                this.m_isf.Lock(ref locked);
                try
                {
                  num = IsolatedStorageFile.RoundToBlockSize((ulong) LongPathFile.GetLength(this.m_FullPath));
                  break;
                }
                catch (FileNotFoundException ex)
                {
                  flag = true;
                  break;
                }
                catch
                {
                  break;
                }
            }
            if (flag)
              this.m_isf.ReserveOneBlock();
            try
            {
              this.m_fs = new FileStream(this.m_FullPath, mode, access, share, bufferSize, FileOptions.None, this.m_GivenPath, true, true);
            }
            catch
            {
              if (flag)
                this.m_isf.UnreserveOneBlock();
              throw;
            }
            if (!flag)
            {
              if (mode != FileMode.Truncate)
              {
                if (mode != FileMode.Create)
                  goto label_34;
              }
              ulong blockSize = IsolatedStorageFile.RoundToBlockSize((ulong) this.m_fs.Length);
              if (num > blockSize)
                this.m_isf.Unreserve(num - blockSize);
              else if (blockSize > num)
                this.m_isf.Reserve(blockSize - num);
            }
          }
          finally
          {
            if (locked)
              this.m_isf.Unlock();
          }
label_34:
          CodeAccessPermission.RevertAll();
          break;
        default:
          throw new ArgumentException(Environment.GetResourceString("IsolatedStorage_FileOpenMode"));
      }
    }

    /// <summary>释放由 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> 占用的非托管资源，还可以释放托管资源。</summary>
    /// <param name="disposing">若要释放托管资源和非托管资源，则为 true；若仅释放非托管资源，则为 false。 </param>
    protected override void Dispose(bool disposing)
    {
      try
      {
        if (!disposing)
          return;
        try
        {
          if (this.m_fs == null)
            return;
          this.m_fs.Close();
        }
        finally
        {
          if (this.m_OwnedStore && this.m_isf != null)
            this.m_isf.Close();
        }
      }
      finally
      {
        base.Dispose(disposing);
      }
    }

    /// <summary>清除此流的缓冲区，使得所有缓冲数据都写入到文件中。</summary>
    public override void Flush()
    {
      this.m_fs.Flush();
    }

    /// <summary>清除此流的缓冲区，将所有缓冲数据都写入到文件中，并且也清除所有中间文件缓冲区。</summary>
    /// <param name="flushToDisk">如果刷新所有中间文件缓冲区，则为 true；否则为 false。</param>
    public override void Flush(bool flushToDisk)
    {
      this.m_fs.Flush(flushToDisk);
    }

    /// <summary>将此 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> 对象的长度设置为指定的 <paramref name="value" />。</summary>
    /// <param name="value">
    /// <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> 对象的新长度。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="value" /> 是一个负数。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public override void SetLength(long value)
    {
      if (value < 0L)
        throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      bool locked = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this.m_isf.Lock(ref locked);
        ulong num1 = (ulong) this.m_fs.Length;
        ulong num2 = (ulong) value;
        this.m_isf.Reserve(num1, num2);
        try
        {
          this.ZeroInit(num1, num2);
          this.m_fs.SetLength(value);
        }
        catch
        {
          this.m_isf.UndoReserveOperation(num1, num2);
          throw;
        }
        if (num1 <= num2)
          return;
        this.m_isf.UndoReserveOperation(num2, num1);
      }
      finally
      {
        if (locked)
          this.m_isf.Unlock();
      }
    }

    /// <summary>防止其他进程读取或写入流。</summary>
    /// <param name="position">锁定范围的起始位置。此参数的值必须大于或等于0 (零)。</param>
    /// <param name="length">用于锁定的字节数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="position" /> 或 <paramref name="length" /> 为负。</exception>
    /// <exception cref="T:System.ObjectDisposedException">文件被关闭。</exception>
    /// <exception cref="T:System.IO.IOException">由于另一个进程已锁定文件的部分内容，因此该进程无法访问该文件。</exception>
    public override void Lock(long position, long length)
    {
      if (position < 0L || length < 0L)
        throw new ArgumentOutOfRangeException(position < 0L ? "position" : "length", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      this.m_fs.Lock(position, length);
    }

    /// <summary>允许其他进程访问以前锁定的某个文件的全部或部分内容。</summary>
    /// <param name="position">解锁范围的起始位置。此参数的值必须大于或等于0 (零)。</param>
    /// <param name="length">要解锁的字节数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="position" /> 或 <paramref name="length" /> 为负。</exception>
    public override void Unlock(long position, long length)
    {
      if (position < 0L || length < 0L)
        throw new ArgumentOutOfRangeException(position < 0L ? "position" : "length", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      this.m_fs.Unlock(position, length);
    }

    private void ZeroInit(ulong oldLen, ulong newLen)
    {
      if (oldLen >= newLen)
        return;
      ulong num1 = newLen - oldLen;
      byte[] buffer = new byte[1024];
      long position = this.m_fs.Position;
      this.m_fs.Seek((long) oldLen, SeekOrigin.Begin);
      if (num1 <= 1024UL)
      {
        this.m_fs.Write(buffer, 0, (int) num1);
        this.m_fs.Position = position;
      }
      else
      {
        int count = 1024 - (int) ((long) oldLen & 1023L);
        this.m_fs.Write(buffer, 0, count);
        ulong num2 = num1 - (ulong) count;
        int num3 = (int) (num2 / 1024UL);
        for (int index = 0; index < num3; ++index)
          this.m_fs.Write(buffer, 0, 1024);
        this.m_fs.Write(buffer, 0, (int) ((long) num2 & 1023L));
        this.m_fs.Position = position;
      }
    }

    /// <summary>将字节从当前缓冲的 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> 对象复制到数组。</summary>
    /// <returns>读入 <paramref name="buffer" /> 中的总字节数。如果当前可用的字节数没有请求的字节数那么多，则总字节数可能小于请求的字节数；如果已到达流的末尾，则为零。</returns>
    /// <param name="buffer">要读取的缓冲区。</param>
    /// <param name="offset">缓冲区中开始写入的偏移量。</param>
    /// <param name="count">最多读取的字节数。</param>
    public override int Read(byte[] buffer, int offset, int count)
    {
      return this.m_fs.Read(buffer, offset, count);
    }

    /// <summary>从独立存储中的 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> 对象读取一个字节。</summary>
    /// <returns>从独立存储文件中读取的 8 位无符号整数值。</returns>
    public override int ReadByte()
    {
      return this.m_fs.ReadByte();
    }

    /// <summary>将此 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> 对象的当前位置设置为指定值。</summary>
    /// <returns>
    /// <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> 对象中的新位置。</returns>
    /// <param name="offset">
    /// <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> 对象的新位置。</param>
    /// <param name="origin">
    /// <see cref="T:System.IO.SeekOrigin" /> 值之一。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="origin" /> 必须是 <see cref="T:System.IO.SeekOrigin" /> 值之一。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public override long Seek(long offset, SeekOrigin origin)
    {
      bool locked = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this.m_isf.Lock(ref locked);
        ulong oldLen = (ulong) this.m_fs.Length;
        ulong newLen;
        switch (origin)
        {
          case SeekOrigin.Begin:
            newLen = offset < 0L ? 0UL : (ulong) offset;
            break;
          case SeekOrigin.Current:
            newLen = this.m_fs.Position + offset < 0L ? 0UL : (ulong) (this.m_fs.Position + offset);
            break;
          case SeekOrigin.End:
            newLen = this.m_fs.Length + offset < 0L ? 0UL : (ulong) (this.m_fs.Length + offset);
            break;
          default:
            throw new ArgumentException(Environment.GetResourceString("IsolatedStorage_SeekOrigin"));
        }
        this.m_isf.Reserve(oldLen, newLen);
        try
        {
          this.ZeroInit(oldLen, newLen);
          return this.m_fs.Seek(offset, origin);
        }
        catch
        {
          this.m_isf.UndoReserveOperation(oldLen, newLen);
          throw;
        }
      }
      finally
      {
        if (locked)
          this.m_isf.Unlock();
      }
    }

    /// <summary>使用从字节数组中读取的数据将字节块写入 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> 对象。</summary>
    /// <param name="buffer">要写入的缓冲区。</param>
    /// <param name="offset">缓冲区中开始写入的字节偏移量。</param>
    /// <param name="count">最多写入的字节数。</param>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">写入尝试超过了 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> 对象的配额。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public override void Write(byte[] buffer, int offset, int count)
    {
      bool locked = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this.m_isf.Lock(ref locked);
        ulong oldLen = (ulong) this.m_fs.Length;
        ulong newLen = (ulong) this.m_fs.Position + (ulong) count;
        this.m_isf.Reserve(oldLen, newLen);
        try
        {
          this.m_fs.Write(buffer, offset, count);
        }
        catch
        {
          this.m_isf.UndoReserveOperation(oldLen, newLen);
          throw;
        }
      }
      finally
      {
        if (locked)
          this.m_isf.Unlock();
      }
    }

    /// <summary>将一个字节写入 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> 对象。</summary>
    /// <param name="value">写入独立存储文件的字节值。</param>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">写入尝试超过了 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> 对象的配额。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public override void WriteByte(byte value)
    {
      bool locked = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this.m_isf.Lock(ref locked);
        ulong oldLen = (ulong) this.m_fs.Length;
        ulong newLen = (ulong) this.m_fs.Position + 1UL;
        this.m_isf.Reserve(oldLen, newLen);
        try
        {
          this.m_fs.WriteByte(value);
        }
        catch
        {
          this.m_isf.UndoReserveOperation(oldLen, newLen);
          throw;
        }
      }
      finally
      {
        if (locked)
          this.m_isf.Unlock();
      }
    }

    /// <summary>开始异步读。</summary>
    /// <returns>表示可能仍处于挂起状态的异步读取的 <see cref="T:System.IAsyncResult" /> 对象。此 <see cref="T:System.IAsyncResult" /> 必须传递到该流的 <see cref="M:System.IO.IsolatedStorage.IsolatedStorageFileStream.EndRead(System.IAsyncResult)" /> 方法以确定读取的字节数。这可以通过调用 <see cref="M:System.IO.IsolatedStorage.IsolatedStorageFileStream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> 的相同代码来完成，或在传递给 <see cref="M:System.IO.IsolatedStorage.IsolatedStorageFileStream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> 的回调中完成。</returns>
    /// <param name="buffer">将数据读入的缓冲区。</param>
    /// <param name="offset">
    /// <paramref name="buffer" /> 中的字节偏移量，从此处开始读取。</param>
    /// <param name="numBytes">最多读取的字节数。</param>
    /// <param name="userCallback">异步读操作完成后调用的方法。此参数可选。</param>
    /// <param name="stateObject">异步读的状态。</param>
    /// <exception cref="T:System.IO.IOException">试图在文件的末尾之外进行异步读取。</exception>
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override IAsyncResult BeginRead(byte[] buffer, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
    {
      return this.m_fs.BeginRead(buffer, offset, numBytes, userCallback, stateObject);
    }

    /// <summary>结束挂起的异步读取请求。</summary>
    /// <returns>从流中读取的字节数，介于零和所请求的字节数之间。流仅在到达流的结尾处时才返回零。否则，它们将一直被阻止到至少有一个字节可用时。</returns>
    /// <param name="asyncResult">挂起的异步请求。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="asyncResult" /> 为 null。</exception>
    public override int EndRead(IAsyncResult asyncResult)
    {
      if (asyncResult == null)
        throw new ArgumentNullException("asyncResult");
      return this.m_fs.EndRead(asyncResult);
    }

    /// <summary>开始异步写。</summary>
    /// <returns>表示可能仍处于挂起状态的异步写入的 <see cref="T:System.IAsyncResult" />。此 <see cref="T:System.IAsyncResult" /> 必须传递到该流的 <see cref="M:System.IO.Stream.EndWrite(System.IAsyncResult)" /> 方法以确保写入完成，然后相应地释放资源。这可以通过调用 <see cref="M:System.IO.Stream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> 的相同代码来完成，或在传递给 <see cref="M:System.IO.Stream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> 的回调中完成。</returns>
    /// <param name="buffer">数据写入的缓冲区。</param>
    /// <param name="offset">
    /// <paramref name="buffer" /> 中的字节偏移量，从此处开始写入。</param>
    /// <param name="numBytes">最多写入的字节数。</param>
    /// <param name="userCallback">异步写操作完成后调用的方法。此参数可选。</param>
    /// <param name="stateObject">异步写的状态。</param>
    /// <exception cref="T:System.IO.IOException">尝试超出文件尾进行异步写入。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override IAsyncResult BeginWrite(byte[] buffer, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
    {
      bool locked = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this.m_isf.Lock(ref locked);
        ulong oldLen = (ulong) this.m_fs.Length;
        ulong newLen = (ulong) this.m_fs.Position + (ulong) numBytes;
        this.m_isf.Reserve(oldLen, newLen);
        try
        {
          return this.m_fs.BeginWrite(buffer, offset, numBytes, userCallback, stateObject);
        }
        catch
        {
          this.m_isf.UndoReserveOperation(oldLen, newLen);
          throw;
        }
      }
      finally
      {
        if (locked)
          this.m_isf.Unlock();
      }
    }

    /// <summary>结束异步写入。</summary>
    /// <param name="asyncResult">要结束的挂起的异步 I/O 请求。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="asyncResult" /> 参数为 null。</exception>
    public override void EndWrite(IAsyncResult asyncResult)
    {
      if (asyncResult == null)
        throw new ArgumentNullException("asyncResult");
      this.m_fs.EndWrite(asyncResult);
    }

    internal void NotPermittedError(string str)
    {
      throw new IsolatedStorageException(str);
    }

    internal void NotPermittedError()
    {
      this.NotPermittedError(Environment.GetResourceString("IsolatedStorage_Operation_ISFS"));
    }
  }
}
