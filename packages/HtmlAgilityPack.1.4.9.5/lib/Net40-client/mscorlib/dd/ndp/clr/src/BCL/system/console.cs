// Decompiled with JetBrains decompiler
// Type: System.Console
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;

namespace System
{
  /// <summary>表示控制台应用程序的标准输入流、输出流和错误流。此类不能被继承。若要浏览此类型的.NET Framework 源代码，请参阅 Reference Source。</summary>
  /// <filterpriority>1</filterpriority>
  public static class Console
  {
    private static readonly UnicodeEncoding StdConUnicodeEncoding = new UnicodeEncoding(false, false);
    private static volatile bool _isOutTextWriterRedirected = false;
    private static volatile bool _isErrorTextWriterRedirected = false;
    private static volatile Encoding _inputEncoding = (Encoding) null;
    private static volatile Encoding _outputEncoding = (Encoding) null;
    private static volatile bool _stdInRedirectQueried = false;
    private static volatile bool _stdOutRedirectQueried = false;
    private static volatile bool _stdErrRedirectQueried = false;
    private const int DefaultConsoleBufferSize = 256;
    private const short AltVKCode = 18;
    private const int NumberLockVKCode = 144;
    private const int CapsLockVKCode = 20;
    private const int MinBeepFrequency = 37;
    private const int MaxBeepFrequency = 32767;
    private const int MaxConsoleTitleLength = 24500;
    private static volatile TextReader _in;
    private static volatile TextWriter _out;
    private static volatile TextWriter _error;
    private static volatile ConsoleCancelEventHandler _cancelCallbacks;
    private static volatile Console.ControlCHooker _hooker;
    [SecurityCritical]
    private static Win32Native.InputRecord _cachedInputRecord;
    private static volatile bool _haveReadDefaultColors;
    private static volatile byte _defaultColors;
    private static bool _isStdInRedirected;
    private static bool _isStdOutRedirected;
    private static bool _isStdErrRedirected;
    private static volatile object s_InternalSyncObject;
    private static volatile object s_ReadKeySyncObject;
    private static volatile IntPtr _consoleInputHandle;
    private static volatile IntPtr _consoleOutputHandle;

    private static object InternalSyncObject
    {
      get
      {
        if (Console.s_InternalSyncObject == null)
        {
          object obj = new object();
          Interlocked.CompareExchange<object>(ref Console.s_InternalSyncObject, obj, (object) null);
        }
        return Console.s_InternalSyncObject;
      }
    }

    private static object ReadKeySyncObject
    {
      get
      {
        if (Console.s_ReadKeySyncObject == null)
        {
          object obj = new object();
          Interlocked.CompareExchange<object>(ref Console.s_ReadKeySyncObject, obj, (object) null);
        }
        return Console.s_ReadKeySyncObject;
      }
    }

    private static IntPtr ConsoleInputHandle
    {
      [SecurityCritical] get
      {
        if (Console._consoleInputHandle == IntPtr.Zero)
          Console._consoleInputHandle = Win32Native.GetStdHandle(-10);
        return Console._consoleInputHandle;
      }
    }

    private static IntPtr ConsoleOutputHandle
    {
      [SecurityCritical] get
      {
        if (Console._consoleOutputHandle == IntPtr.Zero)
          Console._consoleOutputHandle = Win32Native.GetStdHandle(-11);
        return Console._consoleOutputHandle;
      }
    }

    /// <summary>获取指示输入是否已从标准输入流中重定向的值。</summary>
    /// <returns>如果重定向输入，则为 true；否则为 false。</returns>
    public static bool IsInputRedirected
    {
      [SecuritySafeCritical] get
      {
        if (Console._stdInRedirectQueried)
          return Console._isStdInRedirected;
        lock (Console.InternalSyncObject)
        {
          if (Console._stdInRedirectQueried)
            return Console._isStdInRedirected;
          Console._isStdInRedirected = Console.IsHandleRedirected(Console.ConsoleInputHandle);
          Console._stdInRedirectQueried = true;
          return Console._isStdInRedirected;
        }
      }
    }

    /// <summary>获取指示输出是否已从标准输入流中重定向的值。</summary>
    /// <returns>如果重定向输出，则为 true；否则为 false。</returns>
    public static bool IsOutputRedirected
    {
      [SecuritySafeCritical] get
      {
        if (Console._stdOutRedirectQueried)
          return Console._isStdOutRedirected;
        lock (Console.InternalSyncObject)
        {
          if (Console._stdOutRedirectQueried)
            return Console._isStdOutRedirected;
          Console._isStdOutRedirected = Console.IsHandleRedirected(Console.ConsoleOutputHandle);
          Console._stdOutRedirectQueried = true;
          return Console._isStdOutRedirected;
        }
      }
    }

    /// <summary>获取指示错误输出流是否已经从标准错误流被再定位的值。</summary>
    /// <returns>如果重定向错误，则为 true；否则为 false。</returns>
    public static bool IsErrorRedirected
    {
      [SecuritySafeCritical] get
      {
        if (Console._stdErrRedirectQueried)
          return Console._isStdErrRedirected;
        lock (Console.InternalSyncObject)
        {
          if (Console._stdErrRedirectQueried)
            return Console._isStdErrRedirected;
          Console._isStdErrRedirected = Console.IsHandleRedirected(Win32Native.GetStdHandle(-12));
          Console._stdErrRedirectQueried = true;
          return Console._isStdErrRedirected;
        }
      }
    }

    /// <summary>获取标准输入流。</summary>
    /// <returns>表示标准输入流的 <see cref="T:System.IO.TextReader" />。</returns>
    /// <filterpriority>1</filterpriority>
    public static TextReader In
    {
      [SecuritySafeCritical, HostProtection(SecurityAction.LinkDemand, UI = true)] get
      {
        if (Console._in == null)
        {
          lock (Console.InternalSyncObject)
          {
            if (Console._in == null)
            {
              Stream local_2 = Console.OpenStandardInput(256);
              TextReader local_3;
              if (local_2 == Stream.Null)
              {
                local_3 = (TextReader) StreamReader.Null;
              }
              else
              {
                Encoding local_4 = Console.InputEncoding;
                local_3 = TextReader.Synchronized((TextReader) new StreamReader(local_2, local_4, false, 256, true));
              }
              Thread.MemoryBarrier();
              Console._in = local_3;
            }
          }
        }
        return Console._in;
      }
    }

    /// <summary>获取标准输出流。</summary>
    /// <returns>表示标准输出流的 <see cref="T:System.IO.TextWriter" />。</returns>
    /// <filterpriority>1</filterpriority>
    public static TextWriter Out
    {
      [HostProtection(SecurityAction.LinkDemand, UI = true)] get
      {
        if (Console._out == null)
          Console.InitializeStdOutError(true);
        return Console._out;
      }
    }

    /// <summary>获取标准错误输出流。</summary>
    /// <returns>表示标准错误输出流的 <see cref="T:System.IO.TextWriter" />。</returns>
    /// <filterpriority>1</filterpriority>
    public static TextWriter Error
    {
      [HostProtection(SecurityAction.LinkDemand, UI = true)] get
      {
        if (Console._error == null)
          Console.InitializeStdOutError(false);
        return Console._error;
      }
    }

    /// <summary>获取或设置控制台用于读取输入的编码。</summary>
    /// <returns>用于读取控制台输入的编码。</returns>
    /// <exception cref="T:System.ArgumentNullException">一个集运算中的属性值是 null。</exception>
    /// <exception cref="T:System.IO.IOException">执行此操作期间出错。</exception>
    /// <exception cref="T:System.Security.SecurityException">您的应用程序没有执行此操作的权限。</exception>
    /// <filterpriority>1</filterpriority>
    public static Encoding InputEncoding
    {
      [SecuritySafeCritical] get
      {
        if (Console._inputEncoding != null)
          return Console._inputEncoding;
        lock (Console.InternalSyncObject)
        {
          if (Console._inputEncoding != null)
            return Console._inputEncoding;
          Console._inputEncoding = Encoding.GetEncoding((int) Win32Native.GetConsoleCP());
          return Console._inputEncoding;
        }
      }
      [SecuritySafeCritical] set
      {
        if (value == null)
          throw new ArgumentNullException("value");
        new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
        lock (Console.InternalSyncObject)
        {
          if (!Console.IsStandardConsoleUnicodeEncoding(value) && !Win32Native.SetConsoleCP((uint) value.CodePage))
            __Error.WinIOError();
          Console._inputEncoding = (Encoding) value.Clone();
          Console._in = (TextReader) null;
        }
      }
    }

    /// <summary>获取或设置控制台用于写入输出的编码。</summary>
    /// <returns>用于写入控制台输出的编码。</returns>
    /// <exception cref="T:System.ArgumentNullException">一个集运算中的属性值是 null。</exception>
    /// <exception cref="T:System.IO.IOException">执行此操作期间出错。</exception>
    /// <exception cref="T:System.Security.SecurityException">您的应用程序没有执行此操作的权限。</exception>
    /// <filterpriority>1</filterpriority>
    public static Encoding OutputEncoding
    {
      [SecuritySafeCritical] get
      {
        if (Console._outputEncoding != null)
          return Console._outputEncoding;
        lock (Console.InternalSyncObject)
        {
          if (Console._outputEncoding != null)
            return Console._outputEncoding;
          Console._outputEncoding = Encoding.GetEncoding((int) Win32Native.GetConsoleOutputCP());
          return Console._outputEncoding;
        }
      }
      [SecuritySafeCritical] set
      {
        if (value == null)
          throw new ArgumentNullException("value");
        new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
        lock (Console.InternalSyncObject)
        {
          if (Console._out != null && !Console._isOutTextWriterRedirected)
          {
            Console._out.Flush();
            Console._out = (TextWriter) null;
          }
          if (Console._error != null && !Console._isErrorTextWriterRedirected)
          {
            Console._error.Flush();
            Console._error = (TextWriter) null;
          }
          if (!Console.IsStandardConsoleUnicodeEncoding(value) && !Win32Native.SetConsoleOutputCP((uint) value.CodePage))
            __Error.WinIOError();
          Console._outputEncoding = (Encoding) value.Clone();
        }
      }
    }

    /// <summary>获取或设置控制台的背景色。</summary>
    /// <returns>一个值，指定控制台的背景色；也就是显示在每个字符后面的颜色。默认为黑色。</returns>
    /// <exception cref="T:System.ArgumentException">设置操作中指定的颜色不是有效的成员 <see cref="T:System.ConsoleColor" />。</exception>
    /// <exception cref="T:System.Security.SecurityException">用户没有执行此操作的权限。</exception>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
    /// </PermissionSet>
    public static ConsoleColor BackgroundColor
    {
      [SecuritySafeCritical] get
      {
        bool succeeded;
        Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo(false, out succeeded);
        if (!succeeded)
          return ConsoleColor.Black;
        return Console.ColorAttributeToConsoleColor((Win32Native.Color) ((int) bufferInfo.wAttributes & 240));
      }
      [SecuritySafeCritical] set
      {
        new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
        Win32Native.Color colorAttribute = Console.ConsoleColorToColorAttribute(value, true);
        bool succeeded;
        Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo(false, out succeeded);
        if (!succeeded)
          return;
        Win32Native.SetConsoleTextAttribute(Console.ConsoleOutputHandle, (short) ((int) (ushort) ((int) bufferInfo.wAttributes & -241) | (int) (ushort) colorAttribute));
      }
    }

    /// <summary>获取或设置控制台的前景色。</summary>
    /// <returns>一个 <see cref="T:System.ConsoleColor" />，指定控制台的前景色；也就是显示的每个字符的颜色。默认为灰色。</returns>
    /// <exception cref="T:System.ArgumentException">设置操作中指定的颜色不是有效的成员 <see cref="T:System.ConsoleColor" />。</exception>
    /// <exception cref="T:System.Security.SecurityException">用户没有执行此操作的权限。</exception>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
    /// </PermissionSet>
    public static ConsoleColor ForegroundColor
    {
      [SecuritySafeCritical] get
      {
        bool succeeded;
        Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo(false, out succeeded);
        if (!succeeded)
          return ConsoleColor.Gray;
        return Console.ColorAttributeToConsoleColor((Win32Native.Color) ((int) bufferInfo.wAttributes & 15));
      }
      [SecuritySafeCritical] set
      {
        new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
        Win32Native.Color colorAttribute = Console.ConsoleColorToColorAttribute(value, false);
        bool succeeded;
        Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo(false, out succeeded);
        if (!succeeded)
          return;
        Win32Native.SetConsoleTextAttribute(Console.ConsoleOutputHandle, (short) ((int) (ushort) ((int) bufferInfo.wAttributes & -16) | (int) (ushort) colorAttribute));
      }
    }

    /// <summary>获取或设置缓冲区的高度。</summary>
    /// <returns>缓冲区的当前高度，以行为单位。</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">一个集运算中的值小于或等于零。- 或 - 一个集运算中的值是大于或等于 <see cref="F:System.Int16.MaxValue" />。- 或 - 一个集运算中的值小于 <see cref="P:System.Console.WindowTop" /> + <see cref="P:System.Console.WindowHeight" />。</exception>
    /// <exception cref="T:System.Security.SecurityException">用户没有执行此操作的权限。</exception>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    public static int BufferHeight
    {
      [SecuritySafeCritical] get
      {
        return (int) Console.GetBufferInfo().dwSize.Y;
      }
      set
      {
        Console.SetBufferSize(Console.BufferWidth, value);
      }
    }

    /// <summary>获取或设置缓冲区的宽度。</summary>
    /// <returns>缓冲区的当前宽度，以列为单位。</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">一个集运算中的值小于或等于零。- 或 - 一个集运算中的值是大于或等于 <see cref="F:System.Int16.MaxValue" />。- 或 - 一个集运算中的值小于 <see cref="P:System.Console.WindowLeft" /> + <see cref="P:System.Console.WindowWidth" />。</exception>
    /// <exception cref="T:System.Security.SecurityException">用户没有执行此操作的权限。</exception>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    public static int BufferWidth
    {
      [SecuritySafeCritical] get
      {
        return (int) Console.GetBufferInfo().dwSize.X;
      }
      set
      {
        Console.SetBufferSize(value, Console.BufferHeight);
      }
    }

    /// <summary>获取或设置控制台窗口区域的高度。</summary>
    /// <returns>控制台窗口的高度，以行为单位。</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">值 <see cref="P:System.Console.WindowWidth" /> 属性或值的 <see cref="P:System.Console.WindowHeight" /> 属性小于或等于 0。- 或 -值 <see cref="P:System.Console.WindowHeight" /> 属性加上的值 <see cref="P:System.Console.WindowTop" /> 属性是大于或等于 <see cref="F:System.Int16.MaxValue" />。- 或 -值 <see cref="P:System.Console.WindowWidth" /> 属性或值的 <see cref="P:System.Console.WindowHeight" /> 属性大于最大窗口宽度或高度的当前屏幕分辨率和控制台字体。</exception>
    /// <exception cref="T:System.IO.IOException">错误读取或写入信息。</exception>
    /// <filterpriority>1</filterpriority>
    public static int WindowHeight
    {
      [SecuritySafeCritical] get
      {
        Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo();
        return (int) bufferInfo.srWindow.Bottom - (int) bufferInfo.srWindow.Top + 1;
      }
      set
      {
        Console.SetWindowSize(Console.WindowWidth, value);
      }
    }

    /// <summary>获取或设置控制台窗口的宽度。</summary>
    /// <returns>控制台窗口的宽度，以列为单位。</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">值 <see cref="P:System.Console.WindowWidth" /> 属性或值的 <see cref="P:System.Console.WindowHeight" /> 属性小于或等于 0。- 或 -值 <see cref="P:System.Console.WindowHeight" /> 属性加上的值 <see cref="P:System.Console.WindowTop" /> 属性是大于或等于 <see cref="F:System.Int16.MaxValue" />。- 或 -值 <see cref="P:System.Console.WindowWidth" /> 属性或值的 <see cref="P:System.Console.WindowHeight" /> 属性大于最大窗口宽度或高度的当前屏幕分辨率和控制台字体。</exception>
    /// <exception cref="T:System.IO.IOException">错误读取或写入信息。</exception>
    /// <filterpriority>1</filterpriority>
    public static int WindowWidth
    {
      [SecuritySafeCritical] get
      {
        Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo();
        return (int) bufferInfo.srWindow.Right - (int) bufferInfo.srWindow.Left + 1;
      }
      set
      {
        Console.SetWindowSize(value, Console.WindowHeight);
      }
    }

    /// <summary>根据当前字体和屏幕分辨率获取控制台窗口可能具有的最大列数。</summary>
    /// <returns>控制台窗口可能具有的最大宽度，以列为单位。</returns>
    /// <filterpriority>1</filterpriority>
    public static int LargestWindowWidth
    {
      [SecuritySafeCritical] get
      {
        return (int) Win32Native.GetLargestConsoleWindowSize(Console.ConsoleOutputHandle).X;
      }
    }

    /// <summary>根据当前字体和屏幕分辨率获取控制台窗口可能具有的最大行数。</summary>
    /// <returns>控制台窗口可能具有的最大高度，以行为单位。</returns>
    /// <filterpriority>1</filterpriority>
    public static int LargestWindowHeight
    {
      [SecuritySafeCritical] get
      {
        return (int) Win32Native.GetLargestConsoleWindowSize(Console.ConsoleOutputHandle).Y;
      }
    }

    /// <summary>获取或设置控制台窗口区域的最左边相对于屏幕缓冲区的位置。</summary>
    /// <returns>控制台窗口的最左边的位置，以列为单位。</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">在设置操作中，要分配的值小于零。- 或 -赋值后， <see cref="P:System.Console.WindowLeft" /> 加上 <see cref="P:System.Console.WindowWidth" /> 将超过 <see cref="P:System.Console.BufferWidth" />。</exception>
    /// <exception cref="T:System.IO.IOException">错误读取或写入信息。</exception>
    /// <filterpriority>1</filterpriority>
    public static int WindowLeft
    {
      [SecuritySafeCritical] get
      {
        return (int) Console.GetBufferInfo().srWindow.Left;
      }
      set
      {
        Console.SetWindowPosition(value, Console.WindowTop);
      }
    }

    /// <summary>获取或设置控制台窗口区域的最顶部相对于屏幕缓冲区的位置。</summary>
    /// <returns>控制台窗口最顶部的位置，以行为单位。</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">在设置操作中，要分配的值小于零。- 或 -赋值后， <see cref="P:System.Console.WindowTop" /> 加上 <see cref="P:System.Console.WindowHeight" /> 将超过 <see cref="P:System.Console.BufferHeight" />。</exception>
    /// <exception cref="T:System.IO.IOException">错误读取或写入信息。</exception>
    /// <filterpriority>1</filterpriority>
    public static int WindowTop
    {
      [SecuritySafeCritical] get
      {
        return (int) Console.GetBufferInfo().srWindow.Top;
      }
      set
      {
        Console.SetWindowPosition(Console.WindowLeft, value);
      }
    }

    /// <summary>获取或设置光标在缓冲区中的列位置。</summary>
    /// <returns>光标的当前位置，以列为单位。</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">一个集运算中的值小于零。- 或 - 一个集运算中的值是大于或等于 <see cref="P:System.Console.BufferWidth" />。</exception>
    /// <exception cref="T:System.Security.SecurityException">用户没有执行此操作的权限。</exception>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    public static int CursorLeft
    {
      [SecuritySafeCritical] get
      {
        return (int) Console.GetBufferInfo().dwCursorPosition.X;
      }
      set
      {
        Console.SetCursorPosition(value, Console.CursorTop);
      }
    }

    /// <summary>获取或设置光标在缓冲区中的行位置。</summary>
    /// <returns>光标的当前位置，以行为单位。</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">一个集运算中的值小于零。- 或 - 一个集运算中的值是大于或等于 <see cref="P:System.Console.BufferHeight" />。</exception>
    /// <exception cref="T:System.Security.SecurityException">用户没有执行此操作的权限。</exception>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    public static int CursorTop
    {
      [SecuritySafeCritical] get
      {
        return (int) Console.GetBufferInfo().dwCursorPosition.Y;
      }
      set
      {
        Console.SetCursorPosition(Console.CursorLeft, value);
      }
    }

    /// <summary>获取或设置光标在字符单元格中的高度。</summary>
    /// <returns>光标的大小，以字符单元格高度的百分比表示。属性值的范围为 1 到 100。</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">在设置操作中指定的值是小于 1 或大于 100。</exception>
    /// <exception cref="T:System.Security.SecurityException">用户没有执行此操作的权限。</exception>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
    /// </PermissionSet>
    public static int CursorSize
    {
      [SecuritySafeCritical] get
      {
        Win32Native.CONSOLE_CURSOR_INFO cci;
        if (!Win32Native.GetConsoleCursorInfo(Console.ConsoleOutputHandle, out cci))
          __Error.WinIOError();
        return cci.dwSize;
      }
      [SecuritySafeCritical] set
      {
        if (value < 1 || value > 100)
          throw new ArgumentOutOfRangeException("value", (object) value, Environment.GetResourceString("ArgumentOutOfRange_CursorSize"));
        new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
        IntPtr consoleOutputHandle = Console.ConsoleOutputHandle;
        Win32Native.CONSOLE_CURSOR_INFO consoleCursorInfo;
        Win32Native.CONSOLE_CURSOR_INFO& cci1 = @consoleCursorInfo;
        if (!Win32Native.GetConsoleCursorInfo(consoleOutputHandle, cci1))
          __Error.WinIOError();
        consoleCursorInfo.dwSize = value;
        Win32Native.CONSOLE_CURSOR_INFO& cci2 = @consoleCursorInfo;
        if (Win32Native.SetConsoleCursorInfo(consoleOutputHandle, cci2))
          return;
        __Error.WinIOError();
      }
    }

    /// <summary>获取或设置一个值，用以指示光标是否可见。</summary>
    /// <returns>如果光标可见，则为 true；否则为 false。</returns>
    /// <exception cref="T:System.Security.SecurityException">用户没有执行此操作的权限。</exception>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
    /// </PermissionSet>
    public static bool CursorVisible
    {
      [SecuritySafeCritical] get
      {
        Win32Native.CONSOLE_CURSOR_INFO cci;
        if (!Win32Native.GetConsoleCursorInfo(Console.ConsoleOutputHandle, out cci))
          __Error.WinIOError();
        return cci.bVisible;
      }
      [SecuritySafeCritical] set
      {
        new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
        IntPtr consoleOutputHandle = Console.ConsoleOutputHandle;
        Win32Native.CONSOLE_CURSOR_INFO consoleCursorInfo;
        Win32Native.CONSOLE_CURSOR_INFO& cci1 = @consoleCursorInfo;
        if (!Win32Native.GetConsoleCursorInfo(consoleOutputHandle, cci1))
          __Error.WinIOError();
        consoleCursorInfo.bVisible = value;
        Win32Native.CONSOLE_CURSOR_INFO& cci2 = @consoleCursorInfo;
        if (Win32Native.SetConsoleCursorInfo(consoleOutputHandle, cci2))
          return;
        __Error.WinIOError();
      }
    }

    /// <summary>获取或设置要显示在控制台标题栏中的标题。</summary>
    /// <returns>要在控制台的标题栏中显示的字符串。标题字符串的最大长度是 24500 个字符。</returns>
    /// <exception cref="T:System.InvalidOperationException">在 get 操作中，检索到的标题的长度超过 24500 个字符。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">在设置操作中，指定的标题的长度超过 24500 个字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">在设置操作中，指定的标题是 null。</exception>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
    /// </PermissionSet>
    public static string Title
    {
      [SecuritySafeCritical] get
      {
        string s = (string) null;
        int outTitleLength = -1;
        int titleNative = Console.GetTitleNative(JitHelpers.GetStringHandleOnStack(ref s), out outTitleLength);
        if (titleNative != 0)
          __Error.WinIOError(titleNative, string.Empty);
        if (outTitleLength > 24500)
          throw new InvalidOperationException(Environment.GetResourceString("ArgumentOutOfRange_ConsoleTitleTooLong"));
        return s;
      }
      [SecuritySafeCritical] set
      {
        if (value == null)
          throw new ArgumentNullException("value");
        if (value.Length > 24500)
          throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_ConsoleTitleTooLong"));
        new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
        if (Win32Native.SetConsoleTitle(value))
          return;
        __Error.WinIOError();
      }
    }

    /// <summary>获取一个值，该值指示按键操作在输入流中是否可用。</summary>
    /// <returns>如果按键操作可用，则为 true；否则为 false。</returns>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <exception cref="T:System.InvalidOperationException">标准输入重定向到一个文件而不是键盘。</exception>
    /// <filterpriority>1</filterpriority>
    public static bool KeyAvailable
    {
      [SecuritySafeCritical, HostProtection(SecurityAction.LinkDemand, UI = true)] get
      {
        if ((int) Console._cachedInputRecord.eventType == 1)
          return true;
        Win32Native.InputRecord buffer = new Win32Native.InputRecord();
        int numEventsRead = 0;
        while (true)
        {
          do
          {
            if (!Win32Native.PeekConsoleInput(Console.ConsoleInputHandle, out buffer, 1, out numEventsRead))
            {
              int lastWin32Error = Marshal.GetLastWin32Error();
              int num = 6;
              if (lastWin32Error == num)
                throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ConsoleKeyAvailableOnFile"));
              string maybeFullPath = "stdin";
              __Error.WinIOError(lastWin32Error, maybeFullPath);
            }
            if (numEventsRead == 0)
              return false;
            if (Console.IsKeyDownEvent(buffer) && !Console.IsModKey(buffer))
              goto label_12;
          }
          while (Win32Native.ReadConsoleInput(Console.ConsoleInputHandle, out buffer, 1, out numEventsRead));
          __Error.WinIOError();
        }
label_12:
        return true;
      }
    }

    /// <summary>获取一个值，该值指示 Num Lock 键盘切换键是打开的还是关闭的。</summary>
    /// <returns>如果 Num Lock 是打开的，则为 true；如果 Num Lock 是关闭的，则为 false。</returns>
    /// <filterpriority>1</filterpriority>
    public static bool NumberLock
    {
      [SecuritySafeCritical] get
      {
        return ((int) Win32Native.GetKeyState(144) & 1) == 1;
      }
    }

    /// <summary>获取一个值，该值指示 Caps Lock 键盘切换键是打开的还是关闭的。</summary>
    /// <returns>如果 Caps Lock 是打开的，则为 true；如果 Caps Lock 是关闭的，则为 false。</returns>
    /// <filterpriority>1</filterpriority>
    public static bool CapsLock
    {
      [SecuritySafeCritical] get
      {
        return ((int) Win32Native.GetKeyState(20) & 1) == 1;
      }
    }

    /// <summary>获取或设置一个值，该值指示是将修改键 <see cref="F:System.ConsoleModifiers.Control" /> 和控制台键 <see cref="F:System.ConsoleKey.C" /> 的组合 (Ctrl+C) 视为普通输入，还是视为由操作系统处理的中断。</summary>
    /// <returns>如果将 Ctrl+C 视为普通输入，则为 true；否则为 false。</returns>
    /// <exception cref="T:System.IO.IOException">无法获取或设置控制台输入缓冲区的输入的模式。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
    /// </PermissionSet>
    public static bool TreatControlCAsInput
    {
      [SecuritySafeCritical] get
      {
        IntPtr consoleInputHandle = Console.ConsoleInputHandle;
        IntPtr num1 = Win32Native.INVALID_HANDLE_VALUE;
        if (consoleInputHandle == num1)
          throw new IOException(Environment.GetResourceString("IO.IO_NoConsole"));
        int num2 = 0;
        int& mode = @num2;
        if (!Win32Native.GetConsoleMode(consoleInputHandle, mode))
          __Error.WinIOError();
        return (num2 & 1) == 0;
      }
      [SecuritySafeCritical] set
      {
        new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
        IntPtr consoleInputHandle = Console.ConsoleInputHandle;
        IntPtr num1 = Win32Native.INVALID_HANDLE_VALUE;
        if (consoleInputHandle == num1)
          throw new IOException(Environment.GetResourceString("IO.IO_NoConsole"));
        int num2 = 0;
        int& mode1 = @num2;
        Win32Native.GetConsoleMode(consoleInputHandle, mode1);
        if (value)
          num2 &= -2;
        else
          num2 |= 1;
        int mode2 = num2;
        if (Win32Native.SetConsoleMode(consoleInputHandle, mode2))
          return;
        __Error.WinIOError();
      }
    }

    /// <summary>当 <see cref="F:System.ConsoleModifiers.Control" /> 修改键 (Ctrl) 和 <see cref="F:System.ConsoleKey.C" /> console 键 (C) 或 Break 键同时按住（Ctrl+C 或 Ctrl+Break）时发生。</summary>
    /// <filterpriority>1</filterpriority>
    public static event ConsoleCancelEventHandler CancelKeyPress
    {
      [SecuritySafeCritical] add
      {
        new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
        lock (Console.InternalSyncObject)
        {
          Console._cancelCallbacks += value;
          if (Console._hooker != null)
            return;
          Console._hooker = new Console.ControlCHooker();
          Console._hooker.Hook();
        }
      }
      [SecuritySafeCritical] remove
      {
        new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
        lock (Console.InternalSyncObject)
        {
          Console._cancelCallbacks -= value;
          if (Console._hooker == null || Console._cancelCallbacks != null)
            return;
          Console._hooker.Unhook();
        }
      }
    }

    [SecuritySafeCritical]
    private static bool IsHandleRedirected(IntPtr ioHandle)
    {
      if ((Win32Native.GetFileType(new SafeFileHandle(ioHandle, false)) & 2) != 2)
        return true;
      int mode;
      return !Win32Native.GetConsoleMode(ioHandle, out mode);
    }

    [SecuritySafeCritical]
    private static void InitializeStdOutError(bool stdout)
    {
      lock (Console.InternalSyncObject)
      {
        if (stdout && Console._out != null || !stdout && Console._error != null)
          return;
        Stream local_3 = !stdout ? Console.OpenStandardError(256) : Console.OpenStandardOutput(256);
        TextWriter local_2_1;
        if (local_3 == Stream.Null)
        {
          local_2_1 = TextWriter.Synchronized((TextWriter) StreamWriter.Null);
        }
        else
        {
          Encoding local_4 = Console.OutputEncoding;
          local_2_1 = TextWriter.Synchronized((TextWriter) new StreamWriter(local_3, local_4, 256, true)
          {
            HaveWrittenPreamble = true,
            AutoFlush = true
          });
        }
        if (stdout)
          Console._out = local_2_1;
        else
          Console._error = local_2_1;
      }
    }

    private static bool IsStandardConsoleUnicodeEncoding(Encoding encoding)
    {
      UnicodeEncoding unicodeEncoding = encoding as UnicodeEncoding;
      if (unicodeEncoding == null || Console.StdConUnicodeEncoding.CodePage != unicodeEncoding.CodePage)
        return false;
      return Console.StdConUnicodeEncoding.bigEndian == unicodeEncoding.bigEndian;
    }

    private static bool GetUseFileAPIs(int handleType)
    {
      switch (handleType)
      {
        case -12:
          if (Console.IsStandardConsoleUnicodeEncoding(Console.OutputEncoding))
            return Console.IsErrorRedirected;
          return true;
        case -11:
          if (Console.IsStandardConsoleUnicodeEncoding(Console.OutputEncoding))
            return Console.IsOutputRedirected;
          return true;
        case -10:
          if (Console.IsStandardConsoleUnicodeEncoding(Console.InputEncoding))
            return Console.IsInputRedirected;
          return true;
        default:
          return true;
      }
    }

    [SecuritySafeCritical]
    private static Stream GetStandardFile(int stdHandleName, FileAccess access, int bufferSize)
    {
      SafeFileHandle safeFileHandle = new SafeFileHandle(Win32Native.GetStdHandle(stdHandleName), false);
      if (safeFileHandle.IsInvalid)
      {
        safeFileHandle.SetHandleAsInvalid();
        return Stream.Null;
      }
      if (stdHandleName != -10 && !Console.ConsoleHandleIsWritable(safeFileHandle))
        return Stream.Null;
      bool useFileApIs = Console.GetUseFileAPIs(stdHandleName);
      return (Stream) new __ConsoleStream(safeFileHandle, access, useFileApIs);
    }

    [SecuritySafeCritical]
    private static unsafe bool ConsoleHandleIsWritable(SafeFileHandle outErrHandle)
    {
      byte num = 65;
      int numBytesWritten;
      return (uint) Win32Native.WriteFile(outErrHandle, &num, 0, out numBytesWritten, IntPtr.Zero) > 0U;
    }

    /// <summary>通过控制台扬声器播放提示音。</summary>
    /// <exception cref="T:System.Security.HostProtectionException">此方法在执行在服务器上，如 SQL Server，不允许对用户接口的访问。</exception>
    /// <filterpriority>1</filterpriority>
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void Beep()
    {
      Console.Beep(800, 200);
    }

    /// <summary>通过控制台扬声器播放具有指定频率和持续时间的提示音。</summary>
    /// <param name="frequency">提示音的频率，介于 37 到 32767 赫兹之间。</param>
    /// <param name="duration">提示音的持续时间，以毫秒为单位。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="frequency" /> 小于 37 或大于 32767 赫兹。- 或 -<paramref name="duration" /> 小于或等于零。</exception>
    /// <exception cref="T:System.Security.HostProtectionException">此方法在执行在服务器上，如 SQL Server，不允许对控制台的访问。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void Beep(int frequency, int duration)
    {
      if (frequency < 37 || frequency > (int) short.MaxValue)
        throw new ArgumentOutOfRangeException("frequency", (object) frequency, Environment.GetResourceString("ArgumentOutOfRange_BeepFrequency", (object) 37, (object) (int) short.MaxValue));
      if (duration <= 0)
        throw new ArgumentOutOfRangeException("duration", (object) duration, Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
      Win32Native.Beep(frequency, duration);
    }

    /// <summary>清除控制台缓冲区和相应的控制台窗口的显示信息。</summary>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    public static void Clear()
    {
      Win32Native.COORD coord = new Win32Native.COORD();
      IntPtr consoleOutputHandle = Console.ConsoleOutputHandle;
      IntPtr num1 = Win32Native.INVALID_HANDLE_VALUE;
      if (consoleOutputHandle == num1)
        throw new IOException(Environment.GetResourceString("IO.IO_NoConsole"));
      Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo();
      int num2 = (int) bufferInfo.dwSize.X * (int) bufferInfo.dwSize.Y;
      int num3 = 0;
      int num4 = 32;
      int nLength = num2;
      Win32Native.COORD dwWriteCoord = coord;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      int& pNumCharsWritten = @num3;
      if (!Win32Native.FillConsoleOutputCharacter(consoleOutputHandle, (char) num4, nLength, dwWriteCoord, pNumCharsWritten))
        __Error.WinIOError();
      num3 = 0;
      int num5 = (int) bufferInfo.wAttributes;
      int numCells = num2;
      Win32Native.COORD startCoord = coord;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      int& pNumBytesWritten = @num3;
      if (!Win32Native.FillConsoleOutputAttribute(consoleOutputHandle, (short) num5, numCells, startCoord, pNumBytesWritten))
        __Error.WinIOError();
      Win32Native.COORD cursorPosition = coord;
      if (Win32Native.SetConsoleCursorPosition(consoleOutputHandle, cursorPosition))
        return;
      __Error.WinIOError();
    }

    [SecurityCritical]
    private static Win32Native.Color ConsoleColorToColorAttribute(ConsoleColor color, bool isBackground)
    {
      if ((color & ~ConsoleColor.White) != ConsoleColor.Black)
        throw new ArgumentException(Environment.GetResourceString("Arg_InvalidConsoleColor"));
      Win32Native.Color color1 = (Win32Native.Color) color;
      if (isBackground)
        color1 = (Win32Native.Color) ((int) color1 << 4);
      return color1;
    }

    [SecurityCritical]
    private static ConsoleColor ColorAttributeToConsoleColor(Win32Native.Color c)
    {
      if ((c & Win32Native.Color.BackgroundMask) != Win32Native.Color.Black)
        c = (Win32Native.Color) ((int) c >> 4);
      return (ConsoleColor) c;
    }

    /// <summary>将控制台的前景色和背景色设置为默认值。</summary>
    /// <exception cref="T:System.Security.SecurityException">用户没有执行此操作的权限。</exception>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static void ResetColor()
    {
      new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
      bool succeeded;
      Console.GetBufferInfo(false, out succeeded);
      if (!succeeded)
        return;
      Win32Native.SetConsoleTextAttribute(Console.ConsoleOutputHandle, (short) Console._defaultColors);
    }

    /// <summary>将屏幕缓冲区的指定源区域复制到指定的目标区域。</summary>
    /// <param name="sourceLeft">源区域最左边的列。</param>
    /// <param name="sourceTop">源区域最顶部的行。</param>
    /// <param name="sourceWidth">源区域中列的数目。</param>
    /// <param name="sourceHeight">源区域中行的数目。</param>
    /// <param name="targetLeft">目标区域最左边的列。</param>
    /// <param name="targetTop">目标区域最顶部的行。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">一个或多个参数是小于零。- 或 - <paramref name="sourceLeft" /> 或 <paramref name="targetLeft" /> 大于或等于 <see cref="P:System.Console.BufferWidth" />。- 或 - <paramref name="sourceTop" /> 或 <paramref name="targetTop" /> 大于或等于 <see cref="P:System.Console.BufferHeight" />。- 或 - <paramref name="sourceTop" /> + <paramref name="sourceHeight" /> 大于或等于 <see cref="P:System.Console.BufferHeight" />。- 或 - <paramref name="sourceLeft" /> + <paramref name="sourceWidth" /> 大于或等于 <see cref="P:System.Console.BufferWidth" />。</exception>
    /// <exception cref="T:System.Security.SecurityException">用户没有执行此操作的权限。</exception>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
    /// </PermissionSet>
    public static void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop)
    {
      Console.MoveBufferArea(sourceLeft, sourceTop, sourceWidth, sourceHeight, targetLeft, targetTop, ' ', ConsoleColor.Black, Console.BackgroundColor);
    }

    /// <summary>将屏幕缓冲区的指定源区域复制到指定的目标区域。</summary>
    /// <param name="sourceLeft">源区域最左边的列。</param>
    /// <param name="sourceTop">源区域最顶部的行。</param>
    /// <param name="sourceWidth">源区域中列的数目。</param>
    /// <param name="sourceHeight">源区域中行的数目。</param>
    /// <param name="targetLeft">目标区域最左边的列。</param>
    /// <param name="targetTop">目标区域最顶部的行。</param>
    /// <param name="sourceChar">用于填充源区域的字符。</param>
    /// <param name="sourceForeColor">用于填充源区域的前景色。</param>
    /// <param name="sourceBackColor">用于填充源区域的背景色。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">一个或多个参数是小于零。- 或 - <paramref name="sourceLeft" /> 或 <paramref name="targetLeft" /> 大于或等于 <see cref="P:System.Console.BufferWidth" />。- 或 - <paramref name="sourceTop" /> 或 <paramref name="targetTop" /> 大于或等于 <see cref="P:System.Console.BufferHeight" />。- 或 - <paramref name="sourceTop" /> + <paramref name="sourceHeight" /> 大于或等于 <see cref="P:System.Console.BufferHeight" />。- 或 - <paramref name="sourceLeft" /> + <paramref name="sourceWidth" /> 大于或等于 <see cref="P:System.Console.BufferWidth" />。</exception>
    /// <exception cref="T:System.ArgumentException">一个或两个颜色参数不是成员的 <see cref="T:System.ConsoleColor" /> 枚举。</exception>
    /// <exception cref="T:System.Security.SecurityException">用户没有执行此操作的权限。</exception>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static unsafe void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop, char sourceChar, ConsoleColor sourceForeColor, ConsoleColor sourceBackColor)
    {
      if (sourceForeColor < ConsoleColor.Black || sourceForeColor > ConsoleColor.White)
        throw new ArgumentException(Environment.GetResourceString("Arg_InvalidConsoleColor"), "sourceForeColor");
      if (sourceBackColor < ConsoleColor.Black || sourceBackColor > ConsoleColor.White)
        throw new ArgumentException(Environment.GetResourceString("Arg_InvalidConsoleColor"), "sourceBackColor");
      Win32Native.COORD bufferSize = Console.GetBufferInfo().dwSize;
      if (sourceLeft < 0 || sourceLeft > (int) bufferSize.X)
        throw new ArgumentOutOfRangeException("sourceLeft", (object) sourceLeft, Environment.GetResourceString("ArgumentOutOfRange_ConsoleBufferBoundaries"));
      if (sourceTop < 0 || sourceTop > (int) bufferSize.Y)
        throw new ArgumentOutOfRangeException("sourceTop", (object) sourceTop, Environment.GetResourceString("ArgumentOutOfRange_ConsoleBufferBoundaries"));
      if (sourceWidth < 0 || sourceWidth > (int) bufferSize.X - sourceLeft)
        throw new ArgumentOutOfRangeException("sourceWidth", (object) sourceWidth, Environment.GetResourceString("ArgumentOutOfRange_ConsoleBufferBoundaries"));
      if (sourceHeight < 0 || sourceTop > (int) bufferSize.Y - sourceHeight)
        throw new ArgumentOutOfRangeException("sourceHeight", (object) sourceHeight, Environment.GetResourceString("ArgumentOutOfRange_ConsoleBufferBoundaries"));
      if (targetLeft < 0 || targetLeft > (int) bufferSize.X)
        throw new ArgumentOutOfRangeException("targetLeft", (object) targetLeft, Environment.GetResourceString("ArgumentOutOfRange_ConsoleBufferBoundaries"));
      if (targetTop < 0 || targetTop > (int) bufferSize.Y)
        throw new ArgumentOutOfRangeException("targetTop", (object) targetTop, Environment.GetResourceString("ArgumentOutOfRange_ConsoleBufferBoundaries"));
      if (sourceWidth == 0 || sourceHeight == 0)
        return;
      new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
      Win32Native.CHAR_INFO[] charInfoArray = new Win32Native.CHAR_INFO[sourceWidth * sourceHeight];
      bufferSize.X = (short) sourceWidth;
      bufferSize.Y = (short) sourceHeight;
      Win32Native.COORD bufferCoord = new Win32Native.COORD();
      Win32Native.SMALL_RECT readRegion = new Win32Native.SMALL_RECT();
      readRegion.Left = (short) sourceLeft;
      readRegion.Right = (short) (sourceLeft + sourceWidth - 1);
      readRegion.Top = (short) sourceTop;
      readRegion.Bottom = (short) (sourceTop + sourceHeight - 1);
      int num1;
      fixed (Win32Native.CHAR_INFO* pBuffer = charInfoArray)
        num1 = Win32Native.ReadConsoleOutput(Console.ConsoleOutputHandle, pBuffer, bufferSize, bufferCoord, ref readRegion) ? 1 : 0;
      if (num1 == 0)
        __Error.WinIOError();
      Win32Native.COORD coord = new Win32Native.COORD();
      coord.X = (short) sourceLeft;
      short wColorAttribute = (short) (Console.ConsoleColorToColorAttribute(sourceBackColor, true) | Console.ConsoleColorToColorAttribute(sourceForeColor, false));
      for (int index = sourceTop; index < sourceTop + sourceHeight; ++index)
      {
        coord.Y = (short) index;
        int num2;
        if (!Win32Native.FillConsoleOutputCharacter(Console.ConsoleOutputHandle, sourceChar, sourceWidth, coord, out num2))
          __Error.WinIOError();
        if (!Win32Native.FillConsoleOutputAttribute(Console.ConsoleOutputHandle, wColorAttribute, sourceWidth, coord, out num2))
          __Error.WinIOError();
      }
      Win32Native.SMALL_RECT writeRegion = new Win32Native.SMALL_RECT();
      writeRegion.Left = (short) targetLeft;
      writeRegion.Right = (short) (targetLeft + sourceWidth);
      writeRegion.Top = (short) targetTop;
      writeRegion.Bottom = (short) (targetTop + sourceHeight);
      fixed (Win32Native.CHAR_INFO* buffer = charInfoArray)
        Win32Native.WriteConsoleOutput(Console.ConsoleOutputHandle, buffer, bufferSize, bufferCoord, ref writeRegion);
    }

    [SecurityCritical]
    private static Win32Native.CONSOLE_SCREEN_BUFFER_INFO GetBufferInfo()
    {
      bool succeeded;
      return Console.GetBufferInfo(true, out succeeded);
    }

    [SecuritySafeCritical]
    private static Win32Native.CONSOLE_SCREEN_BUFFER_INFO GetBufferInfo(bool throwOnNoConsole, out bool succeeded)
    {
      succeeded = false;
      IntPtr consoleOutputHandle = Console.ConsoleOutputHandle;
      if (consoleOutputHandle == Win32Native.INVALID_HANDLE_VALUE)
      {
        if (!throwOnNoConsole)
          return new Win32Native.CONSOLE_SCREEN_BUFFER_INFO();
        throw new IOException(Environment.GetResourceString("IO.IO_NoConsole"));
      }
      Win32Native.CONSOLE_SCREEN_BUFFER_INFO lpConsoleScreenBufferInfo;
      if (!Win32Native.GetConsoleScreenBufferInfo(consoleOutputHandle, out lpConsoleScreenBufferInfo))
      {
        bool screenBufferInfo = Win32Native.GetConsoleScreenBufferInfo(Win32Native.GetStdHandle(-12), out lpConsoleScreenBufferInfo);
        if (!screenBufferInfo)
          screenBufferInfo = Win32Native.GetConsoleScreenBufferInfo(Win32Native.GetStdHandle(-10), out lpConsoleScreenBufferInfo);
        if (!screenBufferInfo)
        {
          int lastWin32Error = Marshal.GetLastWin32Error();
          if (lastWin32Error == 6 && !throwOnNoConsole)
            return new Win32Native.CONSOLE_SCREEN_BUFFER_INFO();
          __Error.WinIOError(lastWin32Error, (string) null);
        }
      }
      if (!Console._haveReadDefaultColors)
      {
        Console._defaultColors = (byte) ((uint) lpConsoleScreenBufferInfo.wAttributes & (uint) byte.MaxValue);
        Console._haveReadDefaultColors = true;
      }
      succeeded = true;
      return lpConsoleScreenBufferInfo;
    }

    /// <summary>将屏幕缓冲区的高度和宽度设置为指定值。</summary>
    /// <param name="width">缓冲区的宽度，以列为单位。</param>
    /// <param name="height">缓冲区的高度，以行为单位。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="height" /> 或 <paramref name="width" /> 小于或等于零。- 或 - <paramref name="height" /> 或 <paramref name="width" /> 大于或等于 <see cref="F:System.Int16.MaxValue" />。- 或 - <paramref name="width" /> 是小于 <see cref="P:System.Console.WindowLeft" /> + <see cref="P:System.Console.WindowWidth" />。- 或 - <paramref name="height" /> 是小于 <see cref="P:System.Console.WindowTop" /> + <see cref="P:System.Console.WindowHeight" />。</exception>
    /// <exception cref="T:System.Security.SecurityException">用户没有执行此操作的权限。</exception>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static void SetBufferSize(int width, int height)
    {
      new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
      Win32Native.SMALL_RECT smallRect = Console.GetBufferInfo().srWindow;
      if (width < (int) smallRect.Right + 1 || width >= (int) short.MaxValue)
        throw new ArgumentOutOfRangeException("width", (object) width, Environment.GetResourceString("ArgumentOutOfRange_ConsoleBufferLessThanWindowSize"));
      if (height < (int) smallRect.Bottom + 1 || height >= (int) short.MaxValue)
        throw new ArgumentOutOfRangeException("height", (object) height, Environment.GetResourceString("ArgumentOutOfRange_ConsoleBufferLessThanWindowSize"));
      if (Win32Native.SetConsoleScreenBufferSize(Console.ConsoleOutputHandle, new Win32Native.COORD()
      {
        X = (short) width,
        Y = (short) height
      }))
        return;
      __Error.WinIOError();
    }

    /// <summary>将控制台窗口的高度和宽度设置为指定值。</summary>
    /// <param name="width">控制台窗口的宽度，以列为单位。</param>
    /// <param name="height">控制台窗口的高度，以行为单位。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="width" /> 或 <paramref name="height" /> 小于或等于零。- 或 - <paramref name="width" /> 加上 <see cref="P:System.Console.WindowLeft" /> 或 <paramref name="height" /> 加上 <see cref="P:System.Console.WindowTop" /> 大于或等于 <see cref="F:System.Int16.MaxValue" />。- 或 -<paramref name="width" /> 或 <paramref name="height" /> 大于最大窗口宽度或高度的当前屏幕分辨率和控制台字体。</exception>
    /// <exception cref="T:System.Security.SecurityException">用户没有执行此操作的权限。</exception>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static unsafe void SetWindowSize(int width, int height)
    {
      if (width <= 0)
        throw new ArgumentOutOfRangeException("width", (object) width, Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
      if (height <= 0)
        throw new ArgumentOutOfRangeException("height", (object) height, Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
      new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
      Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo();
      bool flag = false;
      Win32Native.COORD size = new Win32Native.COORD();
      size.X = bufferInfo.dwSize.X;
      size.Y = bufferInfo.dwSize.Y;
      if ((int) bufferInfo.dwSize.X < (int) bufferInfo.srWindow.Left + width)
      {
        if ((int) bufferInfo.srWindow.Left >= (int) short.MaxValue - width)
          throw new ArgumentOutOfRangeException("width", Environment.GetResourceString("ArgumentOutOfRange_ConsoleWindowBufferSize"));
        size.X = (short) ((int) bufferInfo.srWindow.Left + width);
        flag = true;
      }
      if ((int) bufferInfo.dwSize.Y < (int) bufferInfo.srWindow.Top + height)
      {
        if ((int) bufferInfo.srWindow.Top >= (int) short.MaxValue - height)
          throw new ArgumentOutOfRangeException("height", Environment.GetResourceString("ArgumentOutOfRange_ConsoleWindowBufferSize"));
        size.Y = (short) ((int) bufferInfo.srWindow.Top + height);
        flag = true;
      }
      if (flag && !Win32Native.SetConsoleScreenBufferSize(Console.ConsoleOutputHandle, size))
        __Error.WinIOError();
      Win32Native.SMALL_RECT smallRect = bufferInfo.srWindow;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Win32Native.SMALL_RECT& local1 = @smallRect;
      // ISSUE: explicit reference operation
      int num1 = (int) (short) ((int) (^local1).Top + height - 1);
      // ISSUE: explicit reference operation
      (^local1).Bottom = (short) num1;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Win32Native.SMALL_RECT& local2 = @smallRect;
      // ISSUE: explicit reference operation
      int num2 = (int) (short) ((int) (^local2).Left + width - 1);
      // ISSUE: explicit reference operation
      (^local2).Right = (short) num2;
      if (Win32Native.SetConsoleWindowInfo(Console.ConsoleOutputHandle, true, &smallRect))
        return;
      int lastWin32Error = Marshal.GetLastWin32Error();
      if (flag)
        Win32Native.SetConsoleScreenBufferSize(Console.ConsoleOutputHandle, bufferInfo.dwSize);
      Win32Native.COORD consoleWindowSize = Win32Native.GetLargestConsoleWindowSize(Console.ConsoleOutputHandle);
      if (width > (int) consoleWindowSize.X)
        throw new ArgumentOutOfRangeException("width", (object) width, Environment.GetResourceString("ArgumentOutOfRange_ConsoleWindowSize_Size", (object) consoleWindowSize.X));
      if (height > (int) consoleWindowSize.Y)
        throw new ArgumentOutOfRangeException("height", (object) height, Environment.GetResourceString("ArgumentOutOfRange_ConsoleWindowSize_Size", (object) consoleWindowSize.Y));
      __Error.WinIOError(lastWin32Error, string.Empty);
    }

    /// <summary>设置控制台窗口相对于屏幕缓冲区的位置。</summary>
    /// <param name="left">控制台窗口左上角的列位置。</param>
    /// <param name="top">控制台窗口左上角的行位置。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="left" /> 或 <paramref name="top" /> 小于零。- 或 - <paramref name="left" /> + <see cref="P:System.Console.WindowWidth" /> 大于 <see cref="P:System.Console.BufferWidth" />。- 或 - <paramref name="top" /> + <see cref="P:System.Console.WindowHeight" /> 大于 <see cref="P:System.Console.BufferHeight" />。</exception>
    /// <exception cref="T:System.Security.SecurityException">用户没有执行此操作的权限。</exception>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static unsafe void SetWindowPosition(int left, int top)
    {
      new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
      Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo();
      Win32Native.SMALL_RECT smallRect = bufferInfo.srWindow;
      int num1 = left + (int) smallRect.Right - (int) smallRect.Left + 1;
      if (left < 0 || num1 > (int) bufferInfo.dwSize.X || num1 < 0)
        throw new ArgumentOutOfRangeException("left", (object) left, Environment.GetResourceString("ArgumentOutOfRange_ConsoleWindowPos"));
      int num2 = top + (int) smallRect.Bottom - (int) smallRect.Top + 1;
      if (top < 0 || num2 > (int) bufferInfo.dwSize.Y || num2 < 0)
        throw new ArgumentOutOfRangeException("top", (object) top, Environment.GetResourceString("ArgumentOutOfRange_ConsoleWindowPos"));
      smallRect.Bottom -= (short) ((int) smallRect.Top - top);
      smallRect.Right -= (short) ((int) smallRect.Left - left);
      smallRect.Left = (short) left;
      smallRect.Top = (short) top;
      if (Win32Native.SetConsoleWindowInfo(Console.ConsoleOutputHandle, true, &smallRect))
        return;
      __Error.WinIOError();
    }

    /// <summary>设置光标位置。</summary>
    /// <param name="left">光标的列位置。将从 0 开始从左到右对列进行编号。</param>
    /// <param name="top">光标的行位置。从上到下，从 0 开始为行编号。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="left" /> 或 <paramref name="top" /> 小于零。- 或 - <paramref name="left" /> 大于或等于 <see cref="P:System.Console.BufferWidth" />。- 或 - <paramref name="top" /> 大于或等于 <see cref="P:System.Console.BufferHeight" />。</exception>
    /// <exception cref="T:System.Security.SecurityException">用户没有执行此操作的权限。</exception>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static void SetCursorPosition(int left, int top)
    {
      if (left < 0 || left >= (int) short.MaxValue)
        throw new ArgumentOutOfRangeException("left", (object) left, Environment.GetResourceString("ArgumentOutOfRange_ConsoleBufferBoundaries"));
      if (top < 0 || top >= (int) short.MaxValue)
        throw new ArgumentOutOfRangeException("top", (object) top, Environment.GetResourceString("ArgumentOutOfRange_ConsoleBufferBoundaries"));
      new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
      if (Win32Native.SetConsoleCursorPosition(Console.ConsoleOutputHandle, new Win32Native.COORD()
      {
        X = (short) left,
        Y = (short) top
      }))
        return;
      int lastWin32Error = Marshal.GetLastWin32Error();
      Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo();
      if (left < 0 || left >= (int) bufferInfo.dwSize.X)
        throw new ArgumentOutOfRangeException("left", (object) left, Environment.GetResourceString("ArgumentOutOfRange_ConsoleBufferBoundaries"));
      if (top < 0 || top >= (int) bufferInfo.dwSize.Y)
        throw new ArgumentOutOfRangeException("top", (object) top, Environment.GetResourceString("ArgumentOutOfRange_ConsoleBufferBoundaries"));
      string maybeFullPath = string.Empty;
      __Error.WinIOError(lastWin32Error, maybeFullPath);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Ansi)]
    private static extern int GetTitleNative(StringHandleOnStack outTitle, out int outTitleLength);

    /// <summary>获取用户按下的下一个字符或功能键。按下的键显示在控制台窗口中。</summary>
    /// <returns>一个 <see cref="T:System.ConsoleKeyInfo" /> 对象，描述 <see cref="T:System.ConsoleKey" /> 常数和对应于按下的控制台键的 Unicode 字符（如果存在这样的字符）。<see cref="T:System.ConsoleKeyInfo" /> 对象还在 <see cref="T:System.ConsoleModifiers" /> 值的按位组合中描述是否在按下控制台键的同时按下了一个或多个 Shift、Alt 和 Ctrl 修改键。</returns>
    /// <exception cref="T:System.InvalidOperationException">
    /// <see cref="P:System.Console.In" /> 从控制台以外的某个流属性将被重定向。</exception>
    /// <filterpriority>1</filterpriority>
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static ConsoleKeyInfo ReadKey()
    {
      return Console.ReadKey(false);
    }

    [SecurityCritical]
    private static bool IsAltKeyDown(Win32Native.InputRecord ir)
    {
      return (uint) (ir.keyEvent.controlKeyState & 3) > 0U;
    }

    [SecurityCritical]
    private static bool IsKeyDownEvent(Win32Native.InputRecord ir)
    {
      if ((int) ir.eventType == 1)
        return ir.keyEvent.keyDown;
      return false;
    }

    [SecurityCritical]
    private static bool IsModKey(Win32Native.InputRecord ir)
    {
      short num = ir.keyEvent.virtualKeyCode;
      if (((int) num < 16 || (int) num > 18) && ((int) num != 20 && (int) num != 144))
        return (int) num == 145;
      return true;
    }

    /// <summary>获取用户按下的下一个字符或功能键。按下的键可以选择显示在控制台窗口中。</summary>
    /// <returns>一个 <see cref="T:System.ConsoleKeyInfo" /> 对象，描述 <see cref="T:System.ConsoleKey" /> 常数和对应于按下的控制台键的 Unicode 字符（如果存在这样的字符）。<see cref="T:System.ConsoleKeyInfo" /> 对象还在 <see cref="T:System.ConsoleModifiers" /> 值的按位组合中描述是否在按下控制台键的同时按下了一个或多个 Shift、Alt 和 Ctrl 修改键。</returns>
    /// <param name="intercept">确定是否在控制台窗口中显示按下的键。如果为 true，则不显示按下的键；否则为 false。</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// <see cref="P:System.Console.In" /> 从控制台以外的某个流属性将被重定向。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static ConsoleKeyInfo ReadKey(bool intercept)
    {
      int numEventsRead = -1;
      Win32Native.InputRecord buffer;
      lock (Console.ReadKeySyncObject)
      {
        if ((int) Console._cachedInputRecord.eventType == 1)
        {
          buffer = Console._cachedInputRecord;
          if ((int) Console._cachedInputRecord.keyEvent.repeatCount == 0)
            Console._cachedInputRecord.eventType = (short) -1;
          else
            --Console._cachedInputRecord.keyEvent.repeatCount;
        }
        else
        {
          while (Win32Native.ReadConsoleInput(Console.ConsoleInputHandle, out buffer, 1, out numEventsRead) && numEventsRead != 0)
          {
            short local_7 = buffer.keyEvent.virtualKeyCode;
            if ((Console.IsKeyDownEvent(buffer) || (int) local_7 == 18) && ((int) buffer.keyEvent.uChar != 0 || !Console.IsModKey(buffer)))
            {
              ConsoleKey local_8 = (ConsoleKey) local_7;
              if (!Console.IsAltKeyDown(buffer) || (local_8 < ConsoleKey.NumPad0 || local_8 > ConsoleKey.NumPad9) && (local_8 != ConsoleKey.Clear && local_8 != ConsoleKey.Insert) && (local_8 < ConsoleKey.PageUp || local_8 > ConsoleKey.DownArrow))
              {
                if ((int) buffer.keyEvent.repeatCount > 1)
                {
                  --buffer.keyEvent.repeatCount;
                  Console._cachedInputRecord = buffer;
                  goto label_15;
                }
                else
                  goto label_15;
              }
            }
          }
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ConsoleReadKeyOnFile"));
        }
      }
label_15:
      int num1 = buffer.keyEvent.controlKeyState;
      int num2 = 16;
      bool shift = (uint) (num1 & num2) > 0U;
      int num3 = 3;
      bool alt = (uint) (num1 & num3) > 0U;
      int num4 = 12;
      bool control = (uint) (num1 & num4) > 0U;
      ConsoleKeyInfo consoleKeyInfo = new ConsoleKeyInfo(buffer.keyEvent.uChar, (ConsoleKey) buffer.keyEvent.virtualKeyCode, shift, alt, control);
      if (intercept)
        return consoleKeyInfo;
      Console.Write(buffer.keyEvent.uChar);
      return consoleKeyInfo;
    }

    private static bool BreakEvent(int controlType)
    {
      if (controlType != 0 && controlType != 1)
        return false;
      ConsoleCancelEventHandler cancelCallbacks = Console._cancelCallbacks;
      if (cancelCallbacks == null)
        return false;
      Console.ControlCDelegateData controlCdelegateData = new Console.ControlCDelegateData(controlType == 0 ? ConsoleSpecialKey.ControlC : ConsoleSpecialKey.ControlBreak, cancelCallbacks);
      if (!ThreadPool.QueueUserWorkItem(new WaitCallback(Console.ControlCDelegate), (object) controlCdelegateData))
        return false;
      TimeSpan timeout = new TimeSpan(0, 0, 30);
      controlCdelegateData.CompletionEvent.WaitOne(timeout, false);
      if (!controlCdelegateData.DelegateStarted)
        return false;
      controlCdelegateData.CompletionEvent.WaitOne();
      controlCdelegateData.CompletionEvent.Close();
      return controlCdelegateData.Cancel;
    }

    private static void ControlCDelegate(object data)
    {
      Console.ControlCDelegateData controlCdelegateData = (Console.ControlCDelegateData) data;
      try
      {
        controlCdelegateData.DelegateStarted = true;
        ConsoleCancelEventArgs e = new ConsoleCancelEventArgs(controlCdelegateData.ControlKey);
        controlCdelegateData.CancelCallbacks((object) null, e);
        controlCdelegateData.Cancel = e.Cancel;
      }
      finally
      {
        controlCdelegateData.CompletionEvent.Set();
      }
    }

    /// <summary>获取标准错误流。</summary>
    /// <returns>标准错误流。</returns>
    /// <filterpriority>1</filterpriority>
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static Stream OpenStandardError()
    {
      return Console.OpenStandardError(256);
    }

    /// <summary>获取设置为指定缓冲区大小的标准错误流。</summary>
    /// <returns>标准错误流。</returns>
    /// <param name="bufferSize">内部流缓冲区大小。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="bufferSize" /> 小于或等于零。</exception>
    /// <filterpriority>1</filterpriority>
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static Stream OpenStandardError(int bufferSize)
    {
      if (bufferSize < 0)
        throw new ArgumentOutOfRangeException("bufferSize", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      return Console.GetStandardFile(-12, FileAccess.Write, bufferSize);
    }

    /// <summary>获取标准输入流。</summary>
    /// <returns>标准输入流。</returns>
    /// <filterpriority>1</filterpriority>
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static Stream OpenStandardInput()
    {
      return Console.OpenStandardInput(256);
    }

    /// <summary>获取设置为指定缓冲区大小的标准输入流。</summary>
    /// <returns>标准输入流。</returns>
    /// <param name="bufferSize">内部流缓冲区大小。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="bufferSize" /> 小于或等于零。</exception>
    /// <filterpriority>1</filterpriority>
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static Stream OpenStandardInput(int bufferSize)
    {
      if (bufferSize < 0)
        throw new ArgumentOutOfRangeException("bufferSize", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      return Console.GetStandardFile(-10, FileAccess.Read, bufferSize);
    }

    /// <summary>获取标准输出流。</summary>
    /// <returns>标准输出流。</returns>
    /// <filterpriority>1</filterpriority>
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static Stream OpenStandardOutput()
    {
      return Console.OpenStandardOutput(256);
    }

    /// <summary>获取设置为指定缓冲区大小的标准输出流。</summary>
    /// <returns>标准输出流。</returns>
    /// <param name="bufferSize">内部流缓冲区大小。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="bufferSize" /> 小于或等于零。</exception>
    /// <filterpriority>1</filterpriority>
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static Stream OpenStandardOutput(int bufferSize)
    {
      if (bufferSize < 0)
        throw new ArgumentOutOfRangeException("bufferSize", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      return Console.GetStandardFile(-11, FileAccess.Write, bufferSize);
    }

    /// <summary>将 <see cref="P:System.Console.In" /> 属性设置为指定的 <see cref="T:System.IO.TextReader" /> 对象。</summary>
    /// <param name="newIn">一个流，它是新的标准输入。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="newIn" /> 为 null。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void SetIn(TextReader newIn)
    {
      if (newIn == null)
        throw new ArgumentNullException("newIn");
      new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
      newIn = TextReader.Synchronized(newIn);
      lock (Console.InternalSyncObject)
        Console._in = newIn;
    }

    /// <summary>将 <see cref="P:System.Console.Out" /> 属性设置为指定的 <see cref="T:System.IO.TextWriter" /> 对象。</summary>
    /// <param name="newOut">一个流，它是新的标准输出。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="newOut" /> 为 null。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void SetOut(TextWriter newOut)
    {
      if (newOut == null)
        throw new ArgumentNullException("newOut");
      new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
      Console._isOutTextWriterRedirected = true;
      newOut = TextWriter.Synchronized(newOut);
      lock (Console.InternalSyncObject)
        Console._out = newOut;
    }

    /// <summary>将 <see cref="P:System.Console.Error" /> 属性设置为指定的 <see cref="T:System.IO.TextWriter" /> 对象。</summary>
    /// <param name="newError">一个流，它是新的标准错误输出。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="newError" /> 为 null。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void SetError(TextWriter newError)
    {
      if (newError == null)
        throw new ArgumentNullException("newError");
      new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
      Console._isErrorTextWriterRedirected = true;
      newError = TextWriter.Synchronized(newError);
      lock (Console.InternalSyncObject)
        Console._error = newError;
    }

    /// <summary>从标准输入流读取下一个字符。</summary>
    /// <returns>输入流中的下一个字符；如果当前没有更多的字符可供读取，则为负一 (-1)。</returns>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static int Read()
    {
      return Console.In.Read();
    }

    /// <summary>从标准输入流读取下一行字符。</summary>
    /// <returns>输入流中的下一行字符；如果没有更多的可用行，则为 null。</returns>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <exception cref="T:System.OutOfMemoryException">没有足够的内存来为返回的字符串分配缓冲区。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">在下一行中的字符的字符数是否大于 <see cref="F:System.Int32.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static string ReadLine()
    {
      return Console.In.ReadLine();
    }

    /// <summary>将当前行终止符写入标准输出流。</summary>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void WriteLine()
    {
      Console.Out.WriteLine();
    }

    /// <summary>将指定布尔值的文本表示形式（后跟当前行终止符）写入标准输出流。</summary>
    /// <param name="value">要写入的值。</param>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void WriteLine(bool value)
    {
      Console.Out.WriteLine(value);
    }

    /// <summary>将指定的 Unicode 字符值（后跟当前行终止符）写入标准输出流。</summary>
    /// <param name="value">要写入的值。</param>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void WriteLine(char value)
    {
      Console.Out.WriteLine(value);
    }

    /// <summary>将指定的 Unicode 字符数组（后跟当前行终止符）写入标准输出流。</summary>
    /// <param name="buffer">Unicode 字符数组。</param>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void WriteLine(char[] buffer)
    {
      Console.Out.WriteLine(buffer);
    }

    /// <summary>将指定的 Unicode 字符子数组（后跟当前行终止符）写入标准输出流。</summary>
    /// <param name="buffer">Unicode 字符的数组。</param>
    /// <param name="index">
    /// <paramref name="buffer" /> 中的起始位置。</param>
    /// <param name="count">要写入的字符数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 或 <paramref name="count" /> 小于零。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="index" /> 加上 <paramref name="count" /> 指定的位置，则不在 <paramref name="buffer" />。</exception>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void WriteLine(char[] buffer, int index, int count)
    {
      Console.Out.WriteLine(buffer, index, count);
    }

    /// <summary>将指定的 <see cref="T:System.Decimal" /> 值的文本表示形式（后跟当前行终止符）写入标准输出流。</summary>
    /// <param name="value">要写入的值。</param>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void WriteLine(Decimal value)
    {
      Console.Out.WriteLine(value);
    }

    /// <summary>将指定的双精度浮点值的文本表示形式（后跟当前行终止符）写入标准输出流。</summary>
    /// <param name="value">要写入的值。</param>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void WriteLine(double value)
    {
      Console.Out.WriteLine(value);
    }

    /// <summary>将指定的单精度浮点值的文本表示形式（后跟当前行终止符）写入标准输出流。</summary>
    /// <param name="value">要写入的值。</param>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void WriteLine(float value)
    {
      Console.Out.WriteLine(value);
    }

    /// <summary>将指定的 32 位有符号整数值的文本表示（后跟当前行的结束符）写入标准输出流。</summary>
    /// <param name="value">要写入的值。</param>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void WriteLine(int value)
    {
      Console.Out.WriteLine(value);
    }

    /// <summary>将指定的 32 位无符号的整数值的文本表示（后跟当前行的结束符）写入标准输出流。</summary>
    /// <param name="value">要写入的值。</param>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void WriteLine(uint value)
    {
      Console.Out.WriteLine(value);
    }

    /// <summary>将指定的 64 位有符号整数值的文本表示（后跟当前行的结束符）写入标准输出流。</summary>
    /// <param name="value">要写入的值。</param>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void WriteLine(long value)
    {
      Console.Out.WriteLine(value);
    }

    /// <summary>将指定的 64 位无符号的整数值的文本表示（后跟当前行的结束符）写入标准输出流。</summary>
    /// <param name="value">要写入的值。</param>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void WriteLine(ulong value)
    {
      Console.Out.WriteLine(value);
    }

    /// <summary>将指定对象的文本表示形式（后跟当前行终止符）写入标准输出流。</summary>
    /// <param name="value">要写入的值。</param>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void WriteLine(object value)
    {
      Console.Out.WriteLine(value);
    }

    /// <summary>将指定的字符串值（后跟当前行终止符）写入标准输出流。</summary>
    /// <param name="value">要写入的值。</param>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void WriteLine(string value)
    {
      Console.Out.WriteLine(value);
    }

    /// <summary>使用指定的格式信息，将指定对象（后跟当前行终止符）的文本表示形式写入标准输出流。</summary>
    /// <param name="format">复合格式字符串（请参见“备注”）。</param>
    /// <param name="arg0">要使用 <paramref name="format" /> 写入的对象。</param>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="format" /> 为 null。</exception>
    /// <exception cref="T:System.FormatException">中的格式规范 <paramref name="format" /> 无效。</exception>
    /// <filterpriority>1</filterpriority>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void WriteLine(string format, object arg0)
    {
      Console.Out.WriteLine(format, arg0);
    }

    /// <summary>使用指定的格式信息，将指定对象的文本表示形式（后跟当前行终止符）写入标准输出流。</summary>
    /// <param name="format">复合格式字符串（请参见“备注”）。</param>
    /// <param name="arg0">要使用 <paramref name="format" /> 写入的第一个对象。</param>
    /// <param name="arg1">要使用 <paramref name="format" /> 写入的第二个对象。</param>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="format" /> 为 null。</exception>
    /// <exception cref="T:System.FormatException">中的格式规范 <paramref name="format" /> 无效。</exception>
    /// <filterpriority>1</filterpriority>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void WriteLine(string format, object arg0, object arg1)
    {
      Console.Out.WriteLine(format, arg0, arg1);
    }

    /// <summary>使用指定的格式信息，将指定对象的文本表示形式（后跟当前行终止符）写入标准输出流。</summary>
    /// <param name="format">复合格式字符串（请参见“备注”）。</param>
    /// <param name="arg0">要使用 <paramref name="format" /> 写入的第一个对象。</param>
    /// <param name="arg1">要使用 <paramref name="format" /> 写入的第二个对象。</param>
    /// <param name="arg2">要使用 <paramref name="format" /> 写入的第三个对象。</param>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="format" /> 为 null。</exception>
    /// <exception cref="T:System.FormatException">中的格式规范 <paramref name="format" /> 无效。</exception>
    /// <filterpriority>1</filterpriority>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void WriteLine(string format, object arg0, object arg1, object arg2)
    {
      Console.Out.WriteLine(format, arg0, arg1, arg2);
    }

    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void WriteLine(string format, object arg0, object arg1, object arg2, object arg3, __arglist)
    {
      ArgIterator argIterator = new ArgIterator(__arglist);
      int length = argIterator.GetRemainingCount() + 4;
      object[] objArray = new object[length];
      objArray[0] = arg0;
      objArray[1] = arg1;
      objArray[2] = arg2;
      objArray[3] = arg3;
      for (int index = 4; index < length; ++index)
        objArray[index] = TypedReference.ToObject(argIterator.GetNextArg());
      Console.Out.WriteLine(format, objArray);
    }

    /// <summary>使用指定的格式信息，将指定的对象数组（后跟当前行终止符）的文本表示形式写入标准输出流。</summary>
    /// <param name="format">复合格式字符串（请参见“备注”）。</param>
    /// <param name="arg">要使用 <paramref name="format" /> 写入的对象的数组。</param>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="format" /> 或 <paramref name="arg" /> 为 null。</exception>
    /// <exception cref="T:System.FormatException">中的格式规范 <paramref name="format" /> 无效。</exception>
    /// <filterpriority>1</filterpriority>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void WriteLine(string format, params object[] arg)
    {
      if (arg == null)
        Console.Out.WriteLine(format, (object) null, (object) null);
      else
        Console.Out.WriteLine(format, arg);
    }

    /// <summary>使用指定的格式信息将指定对象的文本表示形式写入标准输出流。</summary>
    /// <param name="format">复合格式字符串（请参见“备注”）。</param>
    /// <param name="arg0">要使用 <paramref name="format" /> 写入的对象。</param>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="format" /> 为 null。</exception>
    /// <exception cref="T:System.FormatException">中的格式规范 <paramref name="format" /> 无效。</exception>
    /// <filterpriority>1</filterpriority>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void Write(string format, object arg0)
    {
      Console.Out.Write(format, arg0);
    }

    /// <summary>使用指定的格式信息将指定对象的文本表示形式写入标准输出流。</summary>
    /// <param name="format">复合格式字符串（请参见“备注”）。</param>
    /// <param name="arg0">要使用 <paramref name="format" /> 写入的第一个对象。</param>
    /// <param name="arg1">要使用 <paramref name="format" /> 写入的第二个对象。</param>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="format" /> 为 null。</exception>
    /// <exception cref="T:System.FormatException">中的格式规范 <paramref name="format" /> 无效。</exception>
    /// <filterpriority>1</filterpriority>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void Write(string format, object arg0, object arg1)
    {
      Console.Out.Write(format, arg0, arg1);
    }

    /// <summary>使用指定的格式信息将指定对象的文本表示形式写入标准输出流。</summary>
    /// <param name="format">复合格式字符串（请参见“备注”）。</param>
    /// <param name="arg0">要使用 <paramref name="format" /> 写入的第一个对象。</param>
    /// <param name="arg1">要使用 <paramref name="format" /> 写入的第二个对象。</param>
    /// <param name="arg2">要使用 <paramref name="format" /> 写入的第三个对象。</param>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="format" /> 为 null。</exception>
    /// <exception cref="T:System.FormatException">中的格式规范 <paramref name="format" /> 无效。</exception>
    /// <filterpriority>1</filterpriority>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void Write(string format, object arg0, object arg1, object arg2)
    {
      Console.Out.Write(format, arg0, arg1, arg2);
    }

    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void Write(string format, object arg0, object arg1, object arg2, object arg3, __arglist)
    {
      ArgIterator argIterator = new ArgIterator(__arglist);
      int length = argIterator.GetRemainingCount() + 4;
      object[] objArray = new object[length];
      objArray[0] = arg0;
      objArray[1] = arg1;
      objArray[2] = arg2;
      objArray[3] = arg3;
      for (int index = 4; index < length; ++index)
        objArray[index] = TypedReference.ToObject(argIterator.GetNextArg());
      Console.Out.Write(format, objArray);
    }

    /// <summary>使用指定的格式信息将指定的对象数组的文本表示形式写入标准输出流。</summary>
    /// <param name="format">复合格式字符串（请参见“备注”）。</param>
    /// <param name="arg">要使用 <paramref name="format" /> 写入的对象的数组。</param>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="format" /> 或 <paramref name="arg" /> 为 null。</exception>
    /// <exception cref="T:System.FormatException">中的格式规范 <paramref name="format" /> 无效。</exception>
    /// <filterpriority>1</filterpriority>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void Write(string format, params object[] arg)
    {
      if (arg == null)
        Console.Out.Write(format, (object) null, (object) null);
      else
        Console.Out.Write(format, arg);
    }

    /// <summary>将指定的布尔值的文本表示形式写入标准输出流。</summary>
    /// <param name="value">要写入的值。</param>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void Write(bool value)
    {
      Console.Out.Write(value);
    }

    /// <summary>将指定的 Unicode 字符值写入标准输出流。</summary>
    /// <param name="value">要写入的值。</param>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void Write(char value)
    {
      Console.Out.Write(value);
    }

    /// <summary>将指定的 Unicode 字符数组写入标准输出流。</summary>
    /// <param name="buffer">Unicode 字符数组。</param>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void Write(char[] buffer)
    {
      Console.Out.Write(buffer);
    }

    /// <summary>将指定的 Unicode 字符子数组写入标准输出流。</summary>
    /// <param name="buffer">Unicode 字符的数组。</param>
    /// <param name="index">
    /// <paramref name="buffer" /> 中的起始位置。</param>
    /// <param name="count">要写入的字符数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 或 <paramref name="count" /> 小于零。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="index" /> 加上 <paramref name="count" /> 指定的位置，则不在 <paramref name="buffer" />。</exception>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void Write(char[] buffer, int index, int count)
    {
      Console.Out.Write(buffer, index, count);
    }

    /// <summary>将指定的双精度浮点值的文本表示形式写入标准输出流。</summary>
    /// <param name="value">要写入的值。</param>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void Write(double value)
    {
      Console.Out.Write(value);
    }

    /// <summary>将指定的 <see cref="T:System.Decimal" /> 值的文本表示形式写入标准输出流。</summary>
    /// <param name="value">要写入的值。</param>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void Write(Decimal value)
    {
      Console.Out.Write(value);
    }

    /// <summary>将指定的单精度浮点值的文本表示形式写入标准输出流。</summary>
    /// <param name="value">要写入的值。</param>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void Write(float value)
    {
      Console.Out.Write(value);
    }

    /// <summary>将指定的 32 位有符号整数值的文本表示写入标准输出流。</summary>
    /// <param name="value">要写入的值。</param>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void Write(int value)
    {
      Console.Out.Write(value);
    }

    /// <summary>将指定的 32 位无符号整数值的文本表示写入标准输出流。</summary>
    /// <param name="value">要写入的值。</param>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void Write(uint value)
    {
      Console.Out.Write(value);
    }

    /// <summary>将指定的 64 位有符号整数值的文本表示写入标准输出流。</summary>
    /// <param name="value">要写入的值。</param>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void Write(long value)
    {
      Console.Out.Write(value);
    }

    /// <summary>将指定的 64 位无符号整数值的文本表示写入标准输出流。</summary>
    /// <param name="value">要写入的值。</param>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void Write(ulong value)
    {
      Console.Out.Write(value);
    }

    /// <summary>将指定对象的文本表示形式写入标准输出流。</summary>
    /// <param name="value">要写入的值，或者为 null。</param>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void Write(object value)
    {
      Console.Out.Write(value);
    }

    /// <summary>将指定的字符串值写入标准输出流。</summary>
    /// <param name="value">要写入的值。</param>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, UI = true)]
    public static void Write(string value)
    {
      Console.Out.Write(value);
    }

    [Flags]
    internal enum ControlKeyState
    {
      RightAltPressed = 1,
      LeftAltPressed = 2,
      RightCtrlPressed = 4,
      LeftCtrlPressed = 8,
      ShiftPressed = 16,
      NumLockOn = 32,
      ScrollLockOn = 64,
      CapsLockOn = 128,
      EnhancedKey = 256,
    }

    internal sealed class ControlCHooker : CriticalFinalizerObject
    {
      private bool _hooked;
      [SecurityCritical]
      private Win32Native.ConsoleCtrlHandlerRoutine _handler;

      [SecurityCritical]
      internal ControlCHooker()
      {
        this._handler = new Win32Native.ConsoleCtrlHandlerRoutine(Console.BreakEvent);
      }

      ~ControlCHooker()
      {
        this.Unhook();
      }

      [SecuritySafeCritical]
      internal void Hook()
      {
        if (this._hooked)
          return;
        if (!Win32Native.SetConsoleCtrlHandler(this._handler, true))
          __Error.WinIOError();
        this._hooked = true;
      }

      [SecuritySafeCritical]
      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
      internal void Unhook()
      {
        if (!this._hooked)
          return;
        if (!Win32Native.SetConsoleCtrlHandler(this._handler, false))
          __Error.WinIOError();
        this._hooked = false;
      }
    }

    private sealed class ControlCDelegateData
    {
      internal ConsoleSpecialKey ControlKey;
      internal bool Cancel;
      internal bool DelegateStarted;
      internal ManualResetEvent CompletionEvent;
      internal ConsoleCancelEventHandler CancelCallbacks;

      internal ControlCDelegateData(ConsoleSpecialKey controlKey, ConsoleCancelEventHandler cancelCallbacks)
      {
        this.ControlKey = controlKey;
        this.CancelCallbacks = cancelCallbacks;
        this.CompletionEvent = new ManualResetEvent(false);
      }
    }
  }
}
