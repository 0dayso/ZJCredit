// Decompiled with JetBrains decompiler
// Type: System.IO.Path
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;

namespace System.IO
{
  /// <summary>对包含文件或目录路径信息的 <see cref="T:System.String" /> 实例执行操作。这些操作是以跨平台的方式执行的。若要浏览此类型的.NET Framework 源代码，请参阅 Reference Source。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public static class Path
  {
    /// <summary>提供平台特定的字符，该字符用于在反映分层文件系统组织的路径字符串中分隔目录级别。</summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static readonly char DirectorySeparatorChar = '\\';
    /// <summary>提供平台特定的替换字符，该替换字符用于在反映分层文件系统组织的路径字符串中分隔目录级别。</summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static readonly char AltDirectorySeparatorChar = '/';
    /// <summary>提供平台特定的卷分隔符。</summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static readonly char VolumeSeparatorChar = ':';
    /// <summary>提供平台特定的字符数组，这些字符不能在传递到 <see cref="T:System.IO.Path" /> 类的成员的路径字符串参数中指定。</summary>
    /// <returns>当前平台的无效路径字符的字符数组。</returns>
    /// <filterpriority>1</filterpriority>
    [Obsolete("Please use GetInvalidPathChars or GetInvalidFileNameChars instead.")]
    public static readonly char[] InvalidPathChars = new char[36]{ '"', '<', '>', '|', char.MinValue, '\x0001', '\x0002', '\x0003', '\x0004', '\x0005', '\x0006', '\a', '\b', '\t', '\n', '\v', '\f', '\r', '\x000E', '\x000F', '\x0010', '\x0011', '\x0012', '\x0013', '\x0014', '\x0015', '\x0016', '\x0017', '\x0018', '\x0019', '\x001A', '\x001B', '\x001C', '\x001D', '\x001E', '\x001F' };
    internal static readonly char[] TrimEndChars = new char[8]{ '\t', '\n', '\v', '\f', '\r', ' ', '\x0085', ' ' };
    private static readonly char[] RealInvalidPathChars = new char[36]{ '"', '<', '>', '|', char.MinValue, '\x0001', '\x0002', '\x0003', '\x0004', '\x0005', '\x0006', '\a', '\b', '\t', '\n', '\v', '\f', '\r', '\x000E', '\x000F', '\x0010', '\x0011', '\x0012', '\x0013', '\x0014', '\x0015', '\x0016', '\x0017', '\x0018', '\x0019', '\x001A', '\x001B', '\x001C', '\x001D', '\x001E', '\x001F' };
    private static readonly char[] InvalidPathCharsWithAdditionalChecks = new char[38]{ '"', '<', '>', '|', char.MinValue, '\x0001', '\x0002', '\x0003', '\x0004', '\x0005', '\x0006', '\a', '\b', '\t', '\n', '\v', '\f', '\r', '\x000E', '\x000F', '\x0010', '\x0011', '\x0012', '\x0013', '\x0014', '\x0015', '\x0016', '\x0017', '\x0018', '\x0019', '\x001A', '\x001B', '\x001C', '\x001D', '\x001E', '\x001F', '*', '?' };
    private static readonly char[] InvalidFileNameChars = new char[41]{ '"', '<', '>', '|', char.MinValue, '\x0001', '\x0002', '\x0003', '\x0004', '\x0005', '\x0006', '\a', '\b', '\t', '\n', '\v', '\f', '\r', '\x000E', '\x000F', '\x0010', '\x0011', '\x0012', '\x0013', '\x0014', '\x0015', '\x0016', '\x0017', '\x0018', '\x0019', '\x001A', '\x001B', '\x001C', '\x001D', '\x001E', '\x001F', ':', '*', '?', '\\', '/' };
    /// <summary>用于在环境变量中分隔路径字符串的平台特定的分隔符。</summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static readonly char PathSeparator = ';';
    internal static readonly int MaxPath = 260;
    private static readonly int MaxDirectoryLength = (int) byte.MaxValue;
    private static readonly char[] s_Base32Char = new char[32]{ 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5' };
    internal const string DirectorySeparatorCharAsString = "\\";
    internal const int MAX_PATH = 260;
    internal const int MAX_DIRECTORY_PATH = 248;
    internal const int MaxLongPath = 32000;
    private const string LongPathPrefix = "\\\\?\\";
    private const string UNCPathPrefix = "\\\\";
    private const string UNCLongPathPrefixToInsert = "?\\UNC\\";
    private const string UNCLongPathPrefix = "\\\\?\\UNC\\";

    /// <summary>更改路径字符串的扩展名。</summary>
    /// <returns>已修改的路径信息。在基于 Windows 的桌面平台上，如果 <paramref name="path" /> 是 null 或空字符串 (“”)，则返回的路径信息是未修改的。如果 <paramref name="extension" /> 为 null，则返回的字符串包含指定的路径（其扩展名已移除）。如果 <paramref name="path" /> 不具有扩展名且 <paramref name="extension" /> 不为 null，则返回的路径字符串包含追加到 <paramref name="path" /> 结尾的 <paramref name="extension" />。</returns>
    /// <param name="path">要修改的路径信息。该路径不能包含在 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 中定义的任何字符。</param>
    /// <param name="extension">新的扩展名（有或没有前导句点）。指定 null 以从 <paramref name="path" /> 移除现有扩展名。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 包含 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 中已定义的一个或多个无效字符。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string ChangeExtension(string path, string extension)
    {
      if (path == null)
        return (string) null;
      Path.CheckInvalidPathChars(path, false);
      string str = path;
      int length = path.Length;
      while (--length >= 0)
      {
        char ch = path[length];
        if ((int) ch == 46)
        {
          str = path.Substring(0, length);
          break;
        }
        if ((int) ch == (int) Path.DirectorySeparatorChar || (int) ch == (int) Path.AltDirectorySeparatorChar || (int) ch == (int) Path.VolumeSeparatorChar)
          break;
      }
      if (extension != null && path.Length != 0)
      {
        if (extension.Length == 0 || (int) extension[0] != 46)
          str += ".";
        str += extension;
      }
      return str;
    }

    /// <summary>返回指定路径字符串的目录信息。</summary>
    /// <returns>
    /// <paramref name="path" /> 的目录信息；如果 <paramref name="path" /> 表示根目录或为 null，则为 null。如果 <paramref name="path" /> 不包含目录信息，则返回 <see cref="F:System.String.Empty" />。</returns>
    /// <param name="path">文件或目录的路径。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 参数包含无效字符、为空、或仅包含空白。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">在 .NET for Windows Store apps 或 可移植类库, ，捕获该基类异常， <see cref="T:System.IO.IOException" />, 、 相反。<paramref name="path" /> 参数的长度超过系统定义的最大长度。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string GetDirectoryName(string path)
    {
      if (path != null)
      {
        Path.CheckInvalidPathChars(path, false);
        string str1 = Path.NormalizePath(path, false);
        if (path.Length > 0)
        {
          try
          {
            string str2 = Path.RemoveLongPathPrefix(path);
            int length = 0;
            while (length < str2.Length && (int) str2[length] != 63 && (int) str2[length] != 42)
              ++length;
            if (length > 0)
              Path.GetFullPath(str2.Substring(0, length));
          }
          catch (SecurityException ex)
          {
            if (path.IndexOf("~", StringComparison.Ordinal) != -1)
              str1 = Path.NormalizePath(path, false, false);
          }
          catch (PathTooLongException ex)
          {
          }
          catch (NotSupportedException ex)
          {
          }
          catch (IOException ex)
          {
          }
          catch (ArgumentException ex)
          {
          }
        }
        path = str1;
        int rootLength = Path.GetRootLength(path);
        if (path.Length > rootLength)
        {
          int length = path.Length;
          if (length == rootLength)
            return (string) null;
          do
            ;
          while (length > rootLength && (int) path[--length] != (int) Path.DirectorySeparatorChar && (int) path[length] != (int) Path.AltDirectorySeparatorChar);
          return path.Substring(0, length);
        }
      }
      return (string) null;
    }

    internal static int GetRootLength(string path)
    {
      Path.CheckInvalidPathChars(path, false);
      int index = 0;
      int length = path.Length;
      if (length >= 1 && Path.IsDirectorySeparator(path[0]))
      {
        index = 1;
        if (length >= 2 && Path.IsDirectorySeparator(path[1]))
        {
          index = 2;
          int num = 2;
          while (index < length && ((int) path[index] != (int) Path.DirectorySeparatorChar && (int) path[index] != (int) Path.AltDirectorySeparatorChar || --num > 0))
            ++index;
        }
      }
      else if (length >= 2 && (int) path[1] == (int) Path.VolumeSeparatorChar)
      {
        index = 2;
        if (length >= 3 && Path.IsDirectorySeparator(path[2]))
          ++index;
      }
      return index;
    }

    internal static bool IsDirectorySeparator(char c)
    {
      if ((int) c != (int) Path.DirectorySeparatorChar)
        return (int) c == (int) Path.AltDirectorySeparatorChar;
      return true;
    }

    /// <summary>获取包含不允许在路径名中使用的字符的数组。</summary>
    /// <returns>包含不允许在路径名中使用的字符的数组。</returns>
    [__DynamicallyInvokable]
    public static char[] GetInvalidPathChars()
    {
      return (char[]) Path.RealInvalidPathChars.Clone();
    }

    /// <summary>获取包含不允许在文件名中使用的字符的数组。</summary>
    /// <returns>包含不允许在文件名中使用的字符的数组。</returns>
    [__DynamicallyInvokable]
    public static char[] GetInvalidFileNameChars()
    {
      return (char[]) Path.InvalidFileNameChars.Clone();
    }

    /// <summary>返回指定的路径字符串的扩展名。</summary>
    /// <returns>指定路径的扩展名（包含句点“.”）、或 null、或 <see cref="F:System.String.Empty" />。如果 <paramref name="path" /> 为 null，则 <see cref="M:System.IO.Path.GetExtension(System.String)" /> 返回 null。如果 <paramref name="path" /> 不具有扩展名信息，则 <see cref="M:System.IO.Path.GetExtension(System.String)" /> 返回 <see cref="F:System.String.Empty" />。</returns>
    /// <param name="path">从中获取扩展名的路径字符串。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 包含 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 中已定义的一个或多个无效字符。 </exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string GetExtension(string path)
    {
      if (path == null)
        return (string) null;
      Path.CheckInvalidPathChars(path, false);
      int length = path.Length;
      int startIndex = length;
      while (--startIndex >= 0)
      {
        char ch = path[startIndex];
        if ((int) ch == 46)
        {
          if (startIndex != length - 1)
            return path.Substring(startIndex, length - startIndex);
          return string.Empty;
        }
        if ((int) ch == (int) Path.DirectorySeparatorChar || (int) ch == (int) Path.AltDirectorySeparatorChar || (int) ch == (int) Path.VolumeSeparatorChar)
          break;
      }
      return string.Empty;
    }

    /// <summary>返回指定路径字符串的绝对路径。</summary>
    /// <returns>
    /// <paramref name="path" /> 的完全限定的位置，例如“C:\MyFile.txt”。</returns>
    /// <param name="path">要获取其绝对路径信息的文件或目录。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 中已定义一个或多个无效字符。- 或 - 系统未能检索绝对路径。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所需的权限。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 包含一个冒号（“:”），此冒号不是卷标识符（如，“c:\”）的一部分。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static string GetFullPath(string path)
    {
      string fullPathInternal = Path.GetFullPathInternal(path);
      FileIOPermission.QuickDemand(FileIOPermissionAccess.PathDiscovery, fullPathInternal, false, false);
      return fullPathInternal;
    }

    [SecurityCritical]
    internal static string UnsafeGetFullPath(string path)
    {
      string fullPathInternal = Path.GetFullPathInternal(path);
      FileIOPermission.QuickDemand(FileIOPermissionAccess.PathDiscovery, fullPathInternal, false, false);
      return fullPathInternal;
    }

    internal static string GetFullPathInternal(string path)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      return Path.NormalizePath(path, true);
    }

    [SecuritySafeCritical]
    internal static string NormalizePath(string path, bool fullCheck)
    {
      return Path.NormalizePath(path, fullCheck, Path.MaxPath);
    }

    [SecuritySafeCritical]
    internal static string NormalizePath(string path, bool fullCheck, bool expandShortPaths)
    {
      return Path.NormalizePath(path, fullCheck, Path.MaxPath, expandShortPaths);
    }

    [SecuritySafeCritical]
    internal static string NormalizePath(string path, bool fullCheck, int maxPathLength)
    {
      return Path.NormalizePath(path, fullCheck, maxPathLength, true);
    }

    [SecurityCritical]
    internal static unsafe string NormalizePath(string path, bool fullCheck, int maxPathLength, bool expandShortPaths)
    {
      if (fullCheck)
      {
        path = path.TrimEnd(Path.TrimEndChars);
        Path.CheckInvalidPathChars(path, false);
      }
      int index1 = 0;
      PathHelper pathHelper;
      if (path.Length + 1 <= Path.MaxPath)
      {
        char* charArrayPtr = stackalloc char[Path.MaxPath];
        pathHelper = new PathHelper(charArrayPtr, Path.MaxPath);
      }
      else
        pathHelper = new PathHelper(path.Length + Path.MaxPath, maxPathLength);
      uint num1 = 0;
      uint num2 = 0;
      bool flag1 = false;
      uint num3 = 0;
      int num4 = -1;
      bool flag2 = false;
      bool flag3 = true;
      int num5 = 0;
      bool flag4 = false;
      if (path.Length > 0 && ((int) path[0] == (int) Path.DirectorySeparatorChar || (int) path[0] == (int) Path.AltDirectorySeparatorChar))
      {
        pathHelper.Append('\\');
        ++index1;
        num4 = 0;
      }
      for (; index1 < path.Length; ++index1)
      {
        char ch1 = path[index1];
        if ((int) ch1 == (int) Path.DirectorySeparatorChar || (int) ch1 == (int) Path.AltDirectorySeparatorChar)
        {
          if ((int) num3 == 0)
          {
            if (num2 > 0U)
            {
              int index2 = num4 + 1;
              if ((int) path[index2] != 46)
                throw new ArgumentException(Environment.GetResourceString("Arg_PathIllegal"));
              if (num2 >= 2U)
              {
                if (flag2 && num2 > 2U)
                  throw new ArgumentException(Environment.GetResourceString("Arg_PathIllegal"));
                if ((int) path[index2 + 1] == 46)
                {
                  for (int index3 = index2 + 2; (long) index3 < (long) index2 + (long) num2; ++index3)
                  {
                    if ((int) path[index3] != 46)
                      throw new ArgumentException(Environment.GetResourceString("Arg_PathIllegal"));
                  }
                  num2 = 2U;
                }
                else
                {
                  if (num2 > 1U)
                    throw new ArgumentException(Environment.GetResourceString("Arg_PathIllegal"));
                  num2 = 1U;
                }
              }
              if ((int) num2 == 2)
                pathHelper.Append('.');
              pathHelper.Append('.');
              flag1 = false;
            }
            if (num1 > 0U & flag3 && index1 + 1 < path.Length && ((int) path[index1 + 1] == (int) Path.DirectorySeparatorChar || (int) path[index1 + 1] == (int) Path.AltDirectorySeparatorChar))
              pathHelper.Append(Path.DirectorySeparatorChar);
          }
          num2 = 0U;
          num1 = 0U;
          if (!flag1)
          {
            flag1 = true;
            pathHelper.Append(Path.DirectorySeparatorChar);
          }
          num3 = 0U;
          num4 = index1;
          flag2 = false;
          flag3 = false;
          if (flag4)
          {
            pathHelper.TryExpandShortFileName();
            flag4 = false;
          }
          int num6 = pathHelper.Length - 1;
          int num7 = num5;
          if (num6 - num7 > Path.MaxDirectoryLength)
            throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
          num5 = num6;
        }
        else if ((int) ch1 == 46)
          ++num2;
        else if ((int) ch1 == 32)
        {
          ++num1;
        }
        else
        {
          if ((int) ch1 == 126 & expandShortPaths)
            flag4 = true;
          flag1 = false;
          if (flag3 && (int) ch1 == (int) Path.VolumeSeparatorChar)
          {
            char ch2 = index1 > 0 ? path[index1 - 1] : ' ';
            if (((int) num2 != 0 || num3 < 1U ? 0 : ((int) ch2 != 32 ? 1 : 0)) == 0)
              throw new ArgumentException(Environment.GetResourceString("Arg_PathIllegal"));
            flag2 = true;
            if (num3 > 1U)
            {
              int index2 = 0;
              while (index2 < pathHelper.Length && (int) pathHelper[index2] == 32)
                ++index2;
              if ((long) num3 - (long) index2 == 1L)
              {
                pathHelper.Length = 0;
                pathHelper.Append(ch2);
              }
            }
            num3 = 0U;
          }
          else
            num3 += 1U + num2 + num1;
          if (num2 > 0U || num1 > 0U)
          {
            int num6 = num4 >= 0 ? index1 - num4 - 1 : index1;
            if (num6 > 0)
            {
              for (int index2 = 0; index2 < num6; ++index2)
                pathHelper.Append(path[num4 + 1 + index2]);
            }
            num2 = 0U;
            num1 = 0U;
          }
          pathHelper.Append(ch1);
          num4 = index1;
        }
      }
      if (pathHelper.Length - 1 - num5 > Path.MaxDirectoryLength)
        throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
      if ((int) num3 == 0 && num2 > 0U)
      {
        int index2 = num4 + 1;
        if ((int) path[index2] != 46)
          throw new ArgumentException(Environment.GetResourceString("Arg_PathIllegal"));
        if (num2 >= 2U)
        {
          if (flag2 && num2 > 2U)
            throw new ArgumentException(Environment.GetResourceString("Arg_PathIllegal"));
          if ((int) path[index2 + 1] == 46)
          {
            for (int index3 = index2 + 2; (long) index3 < (long) index2 + (long) num2; ++index3)
            {
              if ((int) path[index3] != 46)
                throw new ArgumentException(Environment.GetResourceString("Arg_PathIllegal"));
            }
            num2 = 2U;
          }
          else
          {
            if (num2 > 1U)
              throw new ArgumentException(Environment.GetResourceString("Arg_PathIllegal"));
            num2 = 1U;
          }
        }
        if ((int) num2 == 2)
          pathHelper.Append('.');
        pathHelper.Append('.');
      }
      if (pathHelper.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Arg_PathIllegal"));
      if (fullCheck && (pathHelper.OrdinalStartsWith("http:", false) || pathHelper.OrdinalStartsWith("file:", false)))
        throw new ArgumentException(Environment.GetResourceString("Argument_PathUriFormatNotSupported"));
      if (flag4)
        pathHelper.TryExpandShortFileName();
      int num8 = 1;
      if (fullCheck)
      {
        num8 = pathHelper.GetFullPathName();
        bool flag5 = false;
        for (int index2 = 0; index2 < pathHelper.Length && !flag5; ++index2)
        {
          if ((int) pathHelper[index2] == 126 & expandShortPaths)
            flag5 = true;
        }
        if (flag5 && !pathHelper.TryExpandShortFileName())
        {
          int lastSlash = -1;
          for (int index2 = pathHelper.Length - 1; index2 >= 0; --index2)
          {
            if ((int) pathHelper[index2] == (int) Path.DirectorySeparatorChar)
            {
              lastSlash = index2;
              break;
            }
          }
          if (lastSlash >= 0)
          {
            if (pathHelper.Length >= maxPathLength)
              throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
            int lenSavedName = pathHelper.Length - lastSlash - 1;
            pathHelper.Fixup(lenSavedName, lastSlash);
          }
        }
      }
      if (num8 != 0 && pathHelper.Length > 1 && ((int) pathHelper[0] == 92 && (int) pathHelper[1] == 92))
      {
        int index2;
        for (index2 = 2; index2 < num8; ++index2)
        {
          if ((int) pathHelper[index2] == 92)
          {
            ++index2;
            break;
          }
        }
        if (index2 == num8)
          throw new ArgumentException(Environment.GetResourceString("Arg_PathIllegalUNC"));
        if (pathHelper.OrdinalStartsWith("\\\\?\\globalroot", true))
          throw new ArgumentException(Environment.GetResourceString("Arg_PathGlobalRoot"));
      }
      if (pathHelper.Length >= maxPathLength)
        throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
      if (num8 == 0)
      {
        int errorCode = Marshal.GetLastWin32Error();
        if (errorCode == 0)
          errorCode = 161;
        __Error.WinIOError(errorCode, path);
        return (string) null;
      }
      string a = pathHelper.ToString();
      if (string.Equals(a, path, StringComparison.Ordinal))
        a = path;
      return a;
    }

    internal static bool HasLongPathPrefix(string path)
    {
      return path.StartsWith("\\\\?\\", StringComparison.Ordinal);
    }

    internal static string AddLongPathPrefix(string path)
    {
      if (path.StartsWith("\\\\?\\", StringComparison.Ordinal))
        return path;
      if (path.StartsWith("\\\\", StringComparison.Ordinal))
        return path.Insert(2, "?\\UNC\\");
      return "\\\\?\\" + path;
    }

    internal static string RemoveLongPathPrefix(string path)
    {
      if (!path.StartsWith("\\\\?\\", StringComparison.Ordinal))
        return path;
      if (path.StartsWith("\\\\?\\UNC\\", StringComparison.OrdinalIgnoreCase))
        return path.Remove(2, 6);
      return path.Substring(4);
    }

    internal static StringBuilder RemoveLongPathPrefix(StringBuilder pathSB)
    {
      string @string = pathSB.ToString();
      if (!@string.StartsWith("\\\\?\\", StringComparison.Ordinal))
        return pathSB;
      if (@string.StartsWith("\\\\?\\UNC\\", StringComparison.OrdinalIgnoreCase))
        return pathSB.Remove(2, 6);
      return pathSB.Remove(0, 4);
    }

    /// <summary>返回指定路径字符串的文件名和扩展名。</summary>
    /// <returns>
    /// <paramref name="path" /> 中最后一个目录字符后的字符。如果 <paramref name="path" /> 的最后一个字符是目录或卷分隔符，则此方法返回 <see cref="F:System.String.Empty" />。如果 <paramref name="path" /> 为 null，则此方法返回 null。</returns>
    /// <param name="path">从中获取文件名和扩展名的路径字符串。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 包含 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 中已定义的一个或多个无效字符。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string GetFileName(string path)
    {
      if (path != null)
      {
        Path.CheckInvalidPathChars(path, false);
        int length = path.Length;
        int index = length;
        while (--index >= 0)
        {
          char ch = path[index];
          if ((int) ch == (int) Path.DirectorySeparatorChar || (int) ch == (int) Path.AltDirectorySeparatorChar || (int) ch == (int) Path.VolumeSeparatorChar)
            return path.Substring(index + 1, length - index - 1);
        }
      }
      return path;
    }

    /// <summary>返回不具有扩展名的指定路径字符串的文件名。</summary>
    /// <returns>由 <see cref="M:System.IO.Path.GetFileName(System.String)" /> 返回的字符串，但不包括最后的句点 (.) 以及之后的所有字符。</returns>
    /// <param name="path">文件的路径。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 包含 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 中已定义的一个或多个无效字符。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string GetFileNameWithoutExtension(string path)
    {
      path = Path.GetFileName(path);
      if (path == null)
        return (string) null;
      int length;
      if ((length = path.LastIndexOf('.')) == -1)
        return path;
      return path.Substring(0, length);
    }

    /// <summary>获取指定路径的根目录信息。</summary>
    /// <returns>
    /// <paramref name="path" /> 的根目录，例如“C:\”；如果 <paramref name="path" /> 为 null，则为 null；如果 <paramref name="path" /> 不包含根目录信息，则为空字符串。</returns>
    /// <param name="path">从中获取根目录信息的路径。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 包含 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 中已定义的一个或多个无效字符。- 或 - <see cref="F:System.String.Empty" /> 被传递到 <paramref name="path" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string GetPathRoot(string path)
    {
      if (path == null)
        return (string) null;
      path = Path.NormalizePath(path, false);
      return path.Substring(0, Path.GetRootLength(path));
    }

    /// <summary>返回当前用户的临时文件夹的路径。</summary>
    /// <returns>临时文件夹的路径，以反斜杠结尾。</returns>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所需的权限。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static string GetTempPath()
    {
      new EnvironmentPermission(PermissionState.Unrestricted).Demand();
      StringBuilder buffer = new StringBuilder(260);
      int num = (int) Win32Native.GetTempPath(260, buffer);
      string @string = buffer.ToString();
      if (num == 0)
        __Error.WinIOError();
      return Path.GetFullPathInternal(@string);
    }

    internal static bool IsRelative(string path)
    {
      return (path.Length < 3 || (int) path[1] != (int) Path.VolumeSeparatorChar || (int) path[2] != (int) Path.DirectorySeparatorChar || ((int) path[0] < 97 || (int) path[0] > 122) && ((int) path[0] < 65 || (int) path[0] > 90)) && (path.Length < 2 || (int) path[0] != 92 || (int) path[1] != 92);
    }

    /// <summary>返回随机文件夹名或文件名。</summary>
    /// <returns>随机文件夹名或文件名。</returns>
    [__DynamicallyInvokable]
    public static string GetRandomFileName()
    {
      byte[] numArray = new byte[10];
      RNGCryptoServiceProvider cryptoServiceProvider = (RNGCryptoServiceProvider) null;
      try
      {
        cryptoServiceProvider = new RNGCryptoServiceProvider();
        cryptoServiceProvider.GetBytes(numArray);
        char[] charArray = Path.ToBase32StringSuitableForDirName(numArray).ToCharArray();
        int index = 8;
        int num = 46;
        charArray[index] = (char) num;
        int startIndex = 0;
        int length = 12;
        return new string(charArray, startIndex, length);
      }
      finally
      {
        if (cryptoServiceProvider != null)
          cryptoServiceProvider.Dispose();
      }
    }

    /// <summary>在磁盘上创建磁唯一命名的零字节的临时文件并返回该文件的完整路径。</summary>
    /// <returns>临时文件的完整路径。</returns>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误，例如没有提供唯一的临时文件名。- 或 -此方法无法创建临时文件。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static string GetTempFileName()
    {
      return Path.InternalGetTempFileName(true);
    }

    [SecurityCritical]
    internal static string UnsafeGetTempFileName()
    {
      return Path.InternalGetTempFileName(false);
    }

    [SecurityCritical]
    private static string InternalGetTempFileName(bool checkHost)
    {
      string tempPath = Path.GetTempPath();
      new FileIOPermission(FileIOPermissionAccess.Write, tempPath).Demand();
      StringBuilder tmpFileName = new StringBuilder(260);
      if ((int) Win32Native.GetTempFileName(tempPath, "tmp", 0U, tmpFileName) == 0)
        __Error.WinIOError();
      return tmpFileName.ToString();
    }

    /// <summary>确定路径是否包括文件扩展名。</summary>
    /// <returns>如果路径中最后一个目录分隔符（\\ 或 /）或卷分隔符 (:) 之后的字符包括句点 (.)，并且后面跟有一个或多个字符，则为 true；否则为 false。</returns>
    /// <param name="path">用于搜索扩展名的路径。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 包含 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 中已定义的一个或多个无效字符。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool HasExtension(string path)
    {
      if (path != null)
      {
        Path.CheckInvalidPathChars(path, false);
        int length = path.Length;
        while (--length >= 0)
        {
          char ch = path[length];
          if ((int) ch == 46)
            return length != path.Length - 1;
          if ((int) ch == (int) Path.DirectorySeparatorChar || (int) ch == (int) Path.AltDirectorySeparatorChar || (int) ch == (int) Path.VolumeSeparatorChar)
            break;
        }
      }
      return false;
    }

    /// <summary>获取一个值，该值指示指定的路径字符串是否包含根。</summary>
    /// <returns>如果 <paramref name="path" /> 包含一个根，则为 true；否则为 false。</returns>
    /// <param name="path">要测试的路径。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 包含 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 中已定义的一个或多个无效字符。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool IsPathRooted(string path)
    {
      if (path != null)
      {
        Path.CheckInvalidPathChars(path, false);
        int length = path.Length;
        if (length >= 1 && ((int) path[0] == (int) Path.DirectorySeparatorChar || (int) path[0] == (int) Path.AltDirectorySeparatorChar) || length >= 2 && (int) path[1] == (int) Path.VolumeSeparatorChar)
          return true;
      }
      return false;
    }

    /// <summary>将两个字符串组合成一个路径。</summary>
    /// <returns>已组合的路径。如果指定的路径之一是零长度字符串，则该方法返回其他路径。如果 <paramref name="path2" /> 包含绝对路径，则该方法返回 <paramref name="path2" />。</returns>
    /// <param name="path1">要组合的第一个路径。</param>
    /// <param name="path2">要组合的第二个路径。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path1" /> 或 <paramref name="path2" /> 包含 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 中已定义的一个或多个无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path1" /> 或 <paramref name="path2" /> 为 null。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string Combine(string path1, string path2)
    {
      if (path1 == null || path2 == null)
        throw new ArgumentNullException(path1 == null ? "path1" : "path2");
      Path.CheckInvalidPathChars(path1, false);
      Path.CheckInvalidPathChars(path2, false);
      return Path.CombineNoChecks(path1, path2);
    }

    /// <summary>将三个字符串组合成一个路径。</summary>
    /// <returns>已组合的路径。</returns>
    /// <param name="path1">要组合的第一个路径。</param>
    /// <param name="path2">要组合的第二个路径。</param>
    /// <param name="path3">要组合的第三个路径。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path1" />、<paramref name="path2" /> 或  <paramref name="path3" /> 包含 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 中已定义的一个或多个无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path1" />、<paramref name="path2" /> 或 <paramref name="path3" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public static string Combine(string path1, string path2, string path3)
    {
      if (path1 == null || path2 == null || path3 == null)
        throw new ArgumentNullException(path1 == null ? "path1" : (path2 == null ? "path2" : "path3"));
      Path.CheckInvalidPathChars(path1, false);
      Path.CheckInvalidPathChars(path2, false);
      Path.CheckInvalidPathChars(path3, false);
      return Path.CombineNoChecks(Path.CombineNoChecks(path1, path2), path3);
    }

    /// <summary>将四个字符串组合成一个路径。</summary>
    /// <returns>已组合的路径。</returns>
    /// <param name="path1">要组合的第一个路径。</param>
    /// <param name="path2">要组合的第二个路径。</param>
    /// <param name="path3">要组合的第三个路径。</param>
    /// <param name="path4">要组合的第四个路径。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path1" />、<paramref name="path2" />、<paramref name="path3" /> 或 <paramref name="path4" /> 包含 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 中已定义的一个或多个无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path1" />、<paramref name="path2" />、<paramref name="path3" /> 或 <paramref name="path4" /> 为 null。</exception>
    public static string Combine(string path1, string path2, string path3, string path4)
    {
      if (path1 == null || path2 == null || (path3 == null || path4 == null))
        throw new ArgumentNullException(path1 == null ? "path1" : (path2 == null ? "path2" : (path3 == null ? "path3" : "path4")));
      Path.CheckInvalidPathChars(path1, false);
      Path.CheckInvalidPathChars(path2, false);
      Path.CheckInvalidPathChars(path3, false);
      Path.CheckInvalidPathChars(path4, false);
      return Path.CombineNoChecks(Path.CombineNoChecks(Path.CombineNoChecks(path1, path2), path3), path4);
    }

    /// <summary>将字符串数组组合成一个路径。</summary>
    /// <returns>已组合的路径。</returns>
    /// <param name="paths">由路径的各部分构成的数组。</param>
    /// <exception cref="T:System.ArgumentException">数组中的一个字符串包含 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 中定义的一个或多个无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">数组中的一个字符串为 null。</exception>
    [__DynamicallyInvokable]
    public static string Combine(params string[] paths)
    {
      if (paths == null)
        throw new ArgumentNullException("paths");
      int capacity = 0;
      int num = 0;
      for (int index = 0; index < paths.Length; ++index)
      {
        if (paths[index] == null)
          throw new ArgumentNullException("paths");
        if (paths[index].Length != 0)
        {
          Path.CheckInvalidPathChars(paths[index], false);
          if (Path.IsPathRooted(paths[index]))
          {
            num = index;
            capacity = paths[index].Length;
          }
          else
            capacity += paths[index].Length;
          char ch = paths[index][paths[index].Length - 1];
          if ((int) ch != (int) Path.DirectorySeparatorChar && (int) ch != (int) Path.AltDirectorySeparatorChar && (int) ch != (int) Path.VolumeSeparatorChar)
            ++capacity;
        }
      }
      StringBuilder sb = StringBuilderCache.Acquire(capacity);
      for (int index1 = num; index1 < paths.Length; ++index1)
      {
        if (paths[index1].Length != 0)
        {
          if (sb.Length == 0)
          {
            sb.Append(paths[index1]);
          }
          else
          {
            StringBuilder stringBuilder = sb;
            int index2 = stringBuilder.Length - 1;
            char ch = stringBuilder[index2];
            if ((int) ch != (int) Path.DirectorySeparatorChar && (int) ch != (int) Path.AltDirectorySeparatorChar && (int) ch != (int) Path.VolumeSeparatorChar)
              sb.Append(Path.DirectorySeparatorChar);
            sb.Append(paths[index1]);
          }
        }
      }
      return StringBuilderCache.GetStringAndRelease(sb);
    }

    private static string CombineNoChecks(string path1, string path2)
    {
      if (path2.Length == 0)
        return path1;
      if (path1.Length == 0 || Path.IsPathRooted(path2))
        return path2;
      string str = path1;
      int index = str.Length - 1;
      char ch = str[index];
      if ((int) ch != (int) Path.DirectorySeparatorChar && (int) ch != (int) Path.AltDirectorySeparatorChar && (int) ch != (int) Path.VolumeSeparatorChar)
        return path1 + "\\" + path2;
      return path1 + path2;
    }

    internal static string ToBase32StringSuitableForDirName(byte[] buff)
    {
      StringBuilder sb = StringBuilderCache.Acquire(16);
      int length = buff.Length;
      int num1 = 0;
      do
      {
        byte num2 = num1 < length ? buff[num1++] : (byte) 0;
        byte num3 = num1 < length ? buff[num1++] : (byte) 0;
        byte num4 = num1 < length ? buff[num1++] : (byte) 0;
        byte num5 = num1 < length ? buff[num1++] : (byte) 0;
        byte num6 = num1 < length ? buff[num1++] : (byte) 0;
        sb.Append(Path.s_Base32Char[(int) num2 & 31]);
        sb.Append(Path.s_Base32Char[(int) num3 & 31]);
        sb.Append(Path.s_Base32Char[(int) num4 & 31]);
        sb.Append(Path.s_Base32Char[(int) num5 & 31]);
        sb.Append(Path.s_Base32Char[(int) num6 & 31]);
        sb.Append(Path.s_Base32Char[((int) num2 & 224) >> 5 | ((int) num5 & 96) >> 2]);
        sb.Append(Path.s_Base32Char[((int) num3 & 224) >> 5 | ((int) num6 & 96) >> 2]);
        byte num7 = (byte) ((uint) num4 >> 5);
        if (((int) num5 & 128) != 0)
          num7 |= (byte) 8;
        if (((int) num6 & 128) != 0)
          num7 |= (byte) 16;
        sb.Append(Path.s_Base32Char[(int) num7]);
      }
      while (num1 < length);
      return StringBuilderCache.GetStringAndRelease(sb);
    }

    internal static void CheckSearchPattern(string searchPattern)
    {
      int num;
      for (; (num = searchPattern.IndexOf("..", StringComparison.Ordinal)) != -1; searchPattern = searchPattern.Substring(num + 2))
      {
        if (num + 2 == searchPattern.Length)
          throw new ArgumentException(Environment.GetResourceString("Arg_InvalidSearchPattern"));
        if ((int) searchPattern[num + 2] == (int) Path.DirectorySeparatorChar || (int) searchPattern[num + 2] == (int) Path.AltDirectorySeparatorChar)
          throw new ArgumentException(Environment.GetResourceString("Arg_InvalidSearchPattern"));
      }
    }

    internal static bool HasIllegalCharacters(string path, bool checkAdditional)
    {
      if (checkAdditional)
        return path.IndexOfAny(Path.InvalidPathCharsWithAdditionalChecks) >= 0;
      return path.IndexOfAny(Path.RealInvalidPathChars) >= 0;
    }

    internal static void CheckInvalidPathChars(string path, bool checkAdditional = false)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (Path.HasIllegalCharacters(path, checkAdditional))
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPathChars"));
    }

    internal static string InternalCombine(string path1, string path2)
    {
      if (path1 == null || path2 == null)
        throw new ArgumentNullException(path1 == null ? "path1" : "path2");
      Path.CheckInvalidPathChars(path1, false);
      Path.CheckInvalidPathChars(path2, false);
      if (path2.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_PathEmpty"), "path2");
      if (Path.IsPathRooted(path2))
        throw new ArgumentException(Environment.GetResourceString("Arg_Path2IsRooted"), "path2");
      int length = path1.Length;
      if (length == 0)
        return path2;
      char ch = path1[length - 1];
      if ((int) ch != (int) Path.DirectorySeparatorChar && (int) ch != (int) Path.AltDirectorySeparatorChar && (int) ch != (int) Path.VolumeSeparatorChar)
        return path1 + "\\" + path2;
      return path1 + path2;
    }
  }
}
