// Decompiled with JetBrains decompiler
// Type: System.IO.LongPathFile
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.IO
{
  [ComVisible(false)]
  internal static class LongPathFile
  {
    private const int ERROR_ACCESS_DENIED = 5;

    [SecurityCritical]
    internal static void Copy(string sourceFileName, string destFileName, bool overwrite)
    {
      string fullSourceFileName = LongPath.NormalizePath(sourceFileName);
      new FileIOPermission(FileIOPermissionAccess.Read, new string[1]
      {
        fullSourceFileName
      }, 0 != 0, 0 != 0).Demand();
      string fullDestFileName = LongPath.NormalizePath(destFileName);
      new FileIOPermission(FileIOPermissionAccess.Write, new string[1]
      {
        fullDestFileName
      }, 0 != 0, 0 != 0).Demand();
      LongPathFile.InternalCopy(fullSourceFileName, fullDestFileName, sourceFileName, destFileName, overwrite);
    }

    [SecurityCritical]
    private static string InternalCopy(string fullSourceFileName, string fullDestFileName, string sourceFileName, string destFileName, bool overwrite)
    {
      fullSourceFileName = Path.AddLongPathPrefix(fullSourceFileName);
      fullDestFileName = Path.AddLongPathPrefix(fullDestFileName);
      if (!Win32Native.CopyFile(fullSourceFileName, fullDestFileName, !overwrite))
      {
        int lastWin32Error = Marshal.GetLastWin32Error();
        string maybeFullPath = destFileName;
        if (lastWin32Error != 80)
        {
          using (SafeFileHandle file = Win32Native.UnsafeCreateFile(fullSourceFileName, int.MinValue, FileShare.Read, (Win32Native.SECURITY_ATTRIBUTES) null, FileMode.Open, 0, IntPtr.Zero))
          {
            if (file.IsInvalid)
              maybeFullPath = sourceFileName;
          }
          if (lastWin32Error == 5 && LongPathDirectory.InternalExists(fullDestFileName))
            throw new IOException(Environment.GetResourceString("Arg_FileIsDirectory_Name", (object) destFileName), 5, fullDestFileName);
        }
        __Error.WinIOError(lastWin32Error, maybeFullPath);
      }
      return fullDestFileName;
    }

    [SecurityCritical]
    internal static void Delete(string path)
    {
      string str = LongPath.NormalizePath(path);
      new FileIOPermission(FileIOPermissionAccess.Write, new string[1]{ str }, 0 != 0, 0 != 0).Demand();
      if (Win32Native.DeleteFile(Path.AddLongPathPrefix(str)))
        return;
      int lastWin32Error = Marshal.GetLastWin32Error();
      if (lastWin32Error == 2)
        return;
      __Error.WinIOError(lastWin32Error, str);
    }

    [SecurityCritical]
    internal static bool Exists(string path)
    {
      try
      {
        if (path == null || path.Length == 0)
          return false;
        path = LongPath.NormalizePath(path);
        if (path.Length > 0)
        {
          string str = path;
          int index = str.Length - 1;
          if (Path.IsDirectorySeparator(str[index]))
            return false;
        }
        new FileIOPermission(FileIOPermissionAccess.Read, new string[1]{ path }, 0 != 0, 0 != 0).Demand();
        return LongPathFile.InternalExists(path);
      }
      catch (ArgumentException ex)
      {
      }
      catch (NotSupportedException ex)
      {
      }
      catch (SecurityException ex)
      {
      }
      catch (IOException ex)
      {
      }
      catch (UnauthorizedAccessException ex)
      {
      }
      return false;
    }

    [SecurityCritical]
    internal static bool InternalExists(string path)
    {
      return File.InternalExists(Path.AddLongPathPrefix(path));
    }

    [SecurityCritical]
    internal static DateTimeOffset GetCreationTime(string path)
    {
      string str = LongPath.NormalizePath(path);
      new FileIOPermission(FileIOPermissionAccess.Read, new string[1]{ str }, 0 != 0, 0 != 0).Demand();
      string path1 = Path.AddLongPathPrefix(str);
      Win32Native.WIN32_FILE_ATTRIBUTE_DATA fileAttributeData = new Win32Native.WIN32_FILE_ATTRIBUTE_DATA();
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Win32Native.WIN32_FILE_ATTRIBUTE_DATA& data = @fileAttributeData;
      int num1 = 0;
      int num2 = 0;
      int errorCode = File.FillAttributeInfo(path1, data, num1 != 0, num2 != 0);
      if (errorCode != 0)
        __Error.WinIOError(errorCode, str);
      return new DateTimeOffset(DateTime.FromFileTimeUtc((long) fileAttributeData.ftCreationTimeHigh << 32 | (long) fileAttributeData.ftCreationTimeLow).ToLocalTime()).ToLocalTime();
    }

    [SecurityCritical]
    internal static DateTimeOffset GetLastAccessTime(string path)
    {
      string str = LongPath.NormalizePath(path);
      new FileIOPermission(FileIOPermissionAccess.Read, new string[1]{ str }, 0 != 0, 0 != 0).Demand();
      string path1 = Path.AddLongPathPrefix(str);
      Win32Native.WIN32_FILE_ATTRIBUTE_DATA fileAttributeData = new Win32Native.WIN32_FILE_ATTRIBUTE_DATA();
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Win32Native.WIN32_FILE_ATTRIBUTE_DATA& data = @fileAttributeData;
      int num1 = 0;
      int num2 = 0;
      int errorCode = File.FillAttributeInfo(path1, data, num1 != 0, num2 != 0);
      if (errorCode != 0)
        __Error.WinIOError(errorCode, str);
      return new DateTimeOffset(DateTime.FromFileTimeUtc((long) fileAttributeData.ftLastAccessTimeHigh << 32 | (long) fileAttributeData.ftLastAccessTimeLow).ToLocalTime()).ToLocalTime();
    }

    [SecurityCritical]
    internal static DateTimeOffset GetLastWriteTime(string path)
    {
      string str = LongPath.NormalizePath(path);
      new FileIOPermission(FileIOPermissionAccess.Read, new string[1]{ str }, 0 != 0, 0 != 0).Demand();
      string path1 = Path.AddLongPathPrefix(str);
      Win32Native.WIN32_FILE_ATTRIBUTE_DATA fileAttributeData = new Win32Native.WIN32_FILE_ATTRIBUTE_DATA();
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Win32Native.WIN32_FILE_ATTRIBUTE_DATA& data = @fileAttributeData;
      int num1 = 0;
      int num2 = 0;
      int errorCode = File.FillAttributeInfo(path1, data, num1 != 0, num2 != 0);
      if (errorCode != 0)
        __Error.WinIOError(errorCode, str);
      return new DateTimeOffset(DateTime.FromFileTimeUtc((long) fileAttributeData.ftLastWriteTimeHigh << 32 | (long) fileAttributeData.ftLastWriteTimeLow).ToLocalTime()).ToLocalTime();
    }

    [SecurityCritical]
    internal static void Move(string sourceFileName, string destFileName)
    {
      string str = LongPath.NormalizePath(sourceFileName);
      new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.Write, new string[1]{ str }, 0 != 0, 0 != 0).Demand();
      string path = LongPath.NormalizePath(destFileName);
      new FileIOPermission(FileIOPermissionAccess.Write, new string[1]{ path }, 0 != 0, 0 != 0).Demand();
      if (!LongPathFile.InternalExists(str))
        __Error.WinIOError(2, str);
      if (Win32Native.MoveFile(Path.AddLongPathPrefix(str), Path.AddLongPathPrefix(path)))
        return;
      __Error.WinIOError();
    }

    [SecurityCritical]
    internal static long GetLength(string path)
    {
      string path1 = LongPath.NormalizePath(path);
      new FileIOPermission(FileIOPermissionAccess.Read, new string[1]{ path1 }, 0 != 0, 0 != 0).Demand();
      string path2 = Path.AddLongPathPrefix(path1);
      Win32Native.WIN32_FILE_ATTRIBUTE_DATA fileAttributeData = new Win32Native.WIN32_FILE_ATTRIBUTE_DATA();
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Win32Native.WIN32_FILE_ATTRIBUTE_DATA& data = @fileAttributeData;
      int num1 = 0;
      int num2 = 1;
      int errorCode = File.FillAttributeInfo(path2, data, num1 != 0, num2 != 0);
      if (errorCode != 0)
        __Error.WinIOError(errorCode, path);
      if ((fileAttributeData.fileAttributes & 16) != 0)
        __Error.WinIOError(2, path);
      return (long) fileAttributeData.fileSizeHigh << 32 | (long) fileAttributeData.fileSizeLow & (long) uint.MaxValue;
    }
  }
}
