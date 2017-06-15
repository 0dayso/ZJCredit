// Decompiled with JetBrains decompiler
// Type: System.Environment
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Collections;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;

namespace System
{
  /// <summary>提供有关当前环境和平台的信息以及操作它们的方法。此类不能被继承。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public static class Environment
  {
    private const int MaxEnvVariableValueLength = 32767;
    private const int MaxSystemEnvVariableLength = 1024;
    private const int MaxUserEnvVariableLength = 255;
    private static volatile Environment.ResourceHelper m_resHelper;
    private const int MaxMachineNameLength = 256;
    private static object s_InternalSyncObject;
    private static volatile OperatingSystem m_os;
    private static volatile bool s_IsWindows8OrAbove;
    private static volatile bool s_CheckedOSWin8OrAbove;
    private static volatile bool s_WinRTSupported;
    private static volatile bool s_CheckedWinRT;
    private static volatile IntPtr processWinStation;
    private static volatile bool isUserNonInteractive;

    private static object InternalSyncObject
    {
      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)] get
      {
        if (Environment.s_InternalSyncObject == null)
        {
          object obj = new object();
          Interlocked.CompareExchange<object>(ref Environment.s_InternalSyncObject, obj, (object) null);
        }
        return Environment.s_InternalSyncObject;
      }
    }

    /// <summary>获取系统启动后经过的毫秒数。</summary>
    /// <returns>一个 32 位带符号整数，它包含自上次启动计算机以来所经过的时间（以毫秒为单位）。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static extern int TickCount { [SecuritySafeCritical, __DynamicallyInvokable, MethodImpl(MethodImplOptions.InternalCall)] get; }

    /// <summary>获取或设置进程的退出代码。</summary>
    /// <returns>包含退出代码的 32 位有符号整数。默认值为 0（零），这指示已成功完成处理。</returns>
    /// <filterpriority>1</filterpriority>
    public static extern int ExitCode { [SecuritySafeCritical, MethodImpl(MethodImplOptions.InternalCall)] get; [SecuritySafeCritical, MethodImpl(MethodImplOptions.InternalCall)] set; }

    internal static bool IsCLRHosted
    {
      [SecuritySafeCritical] get
      {
        return Environment.GetIsCLRHosted();
      }
    }

    /// <summary>获取该进程的命令行。</summary>
    /// <returns>包含命令行参数的字符串。</returns>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="Path" />
    /// </PermissionSet>
    public static string CommandLine
    {
      [SecuritySafeCritical] get
      {
        new EnvironmentPermission(EnvironmentPermissionAccess.Read, "Path").Demand();
        string s = (string) null;
        Environment.GetCommandLine(JitHelpers.GetStringHandleOnStack(ref s));
        return s;
      }
    }

    /// <summary>获取或设置当前工作目录的完全限定路径。</summary>
    /// <returns>包含目录路径的字符串。</returns>
    /// <exception cref="T:System.ArgumentException">试图将设置为空字符串 ("")。</exception>
    /// <exception cref="T:System.ArgumentNullException">试图将设置为 null.</exception>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">尝试设置找不到一个本地路径。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有适当的权限。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public static string CurrentDirectory
    {
      get
      {
        return Directory.GetCurrentDirectory();
      }
      set
      {
        Directory.SetCurrentDirectory(value);
      }
    }

    /// <summary>获取系统目录的完全限定路径。</summary>
    /// <returns>包含目录路径的字符串。</returns>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public static string SystemDirectory
    {
      [SecuritySafeCritical] get
      {
        StringBuilder sb = new StringBuilder(260);
        int length = 260;
        if (Win32Native.GetSystemDirectory(sb, length) == 0)
          __Error.WinIOError();
        string @string = sb.ToString();
        new FileIOPermission(FileIOPermissionAccess.PathDiscovery, @string).Demand();
        return @string;
      }
    }

    internal static string InternalWindowsDirectory
    {
      [SecurityCritical] get
      {
        StringBuilder sb = new StringBuilder(260);
        int length = 260;
        if (Win32Native.GetWindowsDirectory(sb, length) == 0)
          __Error.WinIOError();
        return sb.ToString();
      }
    }

    /// <summary>获取此本地计算机的 NetBIOS 名称。</summary>
    /// <returns>包含此计算机的名称的字符串。</returns>
    /// <exception cref="T:System.InvalidOperationException">无法获取此计算机的名称。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="COMPUTERNAME" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public static string MachineName
    {
      [SecuritySafeCritical] get
      {
        new EnvironmentPermission(EnvironmentPermissionAccess.Read, "COMPUTERNAME").Demand();
        StringBuilder nameBuffer = new StringBuilder(256);
        int num = 256;
        int& bufferSize = @num;
        if (Win32Native.GetComputerName(nameBuffer, bufferSize) == 0)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ComputerName"));
        return nameBuffer.ToString();
      }
    }

    /// <summary>获取当前计算机上的处理器数。</summary>
    /// <returns>指定当前计算机上处理器个数的 32 位有符号整数。没有默认值。如果当前计算机包含多个处理器组，则此属性返回可用的逻辑处理器数以供公共语言运行时 (CLR) 使用。</returns>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="NUMBER_OF_PROCESSORS" />
    /// </PermissionSet>
    [__DynamicallyInvokable]
    public static int ProcessorCount
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        return Environment.GetProcessorCount();
      }
    }

    /// <summary>获取操作系统的内存页的字节数。</summary>
    /// <returns>系统内存页中的字节数。</returns>
    public static int SystemPageSize
    {
      [SecuritySafeCritical] get
      {
        new EnvironmentPermission(PermissionState.Unrestricted).Demand();
        Win32Native.SYSTEM_INFO lpSystemInfo = new Win32Native.SYSTEM_INFO();
        Win32Native.GetSystemInfo(ref lpSystemInfo);
        return lpSystemInfo.dwPageSize;
      }
    }

    /// <summary>获取为此环境定义的换行字符串。</summary>
    /// <returns>对于非 Unix 平台为包含“\r\n”的字符串，对于 Unix 平台则为包含“\n”的字符串。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string NewLine
    {
      [__DynamicallyInvokable] get
      {
        return "\r\n";
      }
    }

    /// <summary>获取一个 <see cref="T:System.Version" /> 对象，该对象描述公共语言运行时的主版本、次版本、内部版本和修订号。</summary>
    /// <returns>用于显示公共语言运行时版本的对象。</returns>
    /// <filterpriority>1</filterpriority>
    public static Version Version
    {
      get
      {
        return new Version(4, 0, 30319, 42000);
      }
    }

    /// <summary>获取映射到进程上下文的物理内存量。</summary>
    /// <returns>一个 64 位带符号整数，包含映射到进程上下文的物理内存字节的数目。</returns>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public static long WorkingSet
    {
      [SecuritySafeCritical] get
      {
        new EnvironmentPermission(PermissionState.Unrestricted).Demand();
        return Environment.GetWorkingSet();
      }
    }

    /// <summary>获取包含当前平台标识符和版本号的 <see cref="T:System.OperatingSystem" /> 对象。</summary>
    /// <returns>一个包含平台标识符和版本号的对象。</returns>
    /// <exception cref="T:System.InvalidOperationException">此属性不能获得的系统版本。- 或 - 不是成员的获得的平台标识符。 <see cref="T:System.PlatformID" /></exception>
    /// <filterpriority>1</filterpriority>
    public static OperatingSystem OSVersion
    {
      [SecuritySafeCritical] get
      {
        if (Environment.m_os == null)
        {
          Win32Native.OSVERSIONINFO osVer1 = new Win32Native.OSVERSIONINFO();
          if (!Environment.GetVersion(osVer1))
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_GetVersion"));
          Win32Native.OSVERSIONINFOEX osVer2 = new Win32Native.OSVERSIONINFOEX();
          if (!Environment.GetVersionEx(osVer2))
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_GetVersion"));
          Environment.m_os = new OperatingSystem(PlatformID.Win32NT, new Version(osVer1.MajorVersion, osVer1.MinorVersion, osVer1.BuildNumber, (int) osVer2.ServicePackMajor << 16 | (int) osVer2.ServicePackMinor), osVer1.CSDVersion);
        }
        return Environment.m_os;
      }
    }

    internal static bool IsWindows8OrAbove
    {
      get
      {
        if (!Environment.s_CheckedOSWin8OrAbove)
        {
          OperatingSystem osVersion = Environment.OSVersion;
          Environment.s_IsWindows8OrAbove = osVersion.Platform == PlatformID.Win32NT && (osVersion.Version.Major == 6 && osVersion.Version.Minor >= 2 || osVersion.Version.Major > 6);
          Environment.s_CheckedOSWin8OrAbove = true;
        }
        return Environment.s_IsWindows8OrAbove;
      }
    }

    internal static bool IsWinRTSupported
    {
      [SecuritySafeCritical] get
      {
        if (!Environment.s_CheckedWinRT)
        {
          Environment.s_WinRTSupported = Environment.WinRTSupported();
          Environment.s_CheckedWinRT = true;
        }
        return Environment.s_WinRTSupported;
      }
    }

    /// <summary>获取当前的堆栈跟踪信息。</summary>
    /// <returns>包含堆栈跟踪信息的字符串。此值可为 <see cref="F:System.String.Empty" />。</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">请求的堆栈跟踪信息超出了范围。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
    /// </PermissionSet>
    [__DynamicallyInvokable]
    public static string StackTrace
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        new EnvironmentPermission(PermissionState.Unrestricted).Demand();
        return Environment.GetStackTrace((Exception) null, true);
      }
    }

    /// <summary>确定当前进程是否为 64 位进程。</summary>
    /// <returns>如果进程为 64 位进程，则为 true；否则为 false。</returns>
    public static bool Is64BitProcess
    {
      get
      {
        return false;
      }
    }

    /// <summary>确定当前操作系统是否为 64 位操作系统。</summary>
    /// <returns>如果操作系统为 64 位操作系统，则为 true；否则为 false。</returns>
    public static bool Is64BitOperatingSystem
    {
      [SecuritySafeCritical] get
      {
        bool isWow64;
        return ((!Win32Native.DoesWin32MethodExist("kernel32.dll", "IsWow64Process") ? 0 : (Win32Native.IsWow64Process(Win32Native.GetCurrentProcess(), out isWow64) ? 1 : 0)) & (isWow64 ? 1 : 0)) != 0;
      }
    }

    /// <summary>获取一个值，该值指示当前的应用程序域是否正在卸载或者公共语言运行时 (CLR) 是否正在关闭。</summary>
    /// <returns>如果当前的应用程序域正在卸载或者 CLR 正在关闭，为 true；否则，为 false.</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static extern bool HasShutdownStarted { [SecuritySafeCritical, __DynamicallyInvokable, MethodImpl(MethodImplOptions.InternalCall)] get; }

    /// <summary>获取当前已登录到 Windows 操作系统的人员的用户名。</summary>
    /// <returns>已登录到 Windows 的人员的用户名。</returns>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="UserName" />
    /// </PermissionSet>
    public static string UserName
    {
      [SecuritySafeCritical] get
      {
        new EnvironmentPermission(EnvironmentPermissionAccess.Read, "UserName").Demand();
        StringBuilder lpBuffer = new StringBuilder(256);
        int capacity = lpBuffer.Capacity;
        if (Win32Native.GetUserName(lpBuffer, ref capacity))
          return lpBuffer.ToString();
        return string.Empty;
      }
    }

    /// <summary>获取一个值，用以指示当前进程是否在用户交互模式中运行。</summary>
    /// <returns>如果当前进程在用户交互模式中运行，则为 true；否则为 false。</returns>
    /// <filterpriority>1</filterpriority>
    public static bool UserInteractive
    {
      [SecuritySafeCritical] get
      {
        IntPtr processWindowStation = Win32Native.GetProcessWindowStation();
        if (processWindowStation != IntPtr.Zero && Environment.processWinStation != processWindowStation)
        {
          int lpnLengthNeeded = 0;
          Win32Native.USEROBJECTFLAGS userobjectflags = new Win32Native.USEROBJECTFLAGS();
          if (Win32Native.GetUserObjectInformation(processWindowStation, 1, userobjectflags, Marshal.SizeOf<Win32Native.USEROBJECTFLAGS>(userobjectflags), ref lpnLengthNeeded) && (userobjectflags.dwFlags & 1) == 0)
            Environment.isUserNonInteractive = true;
          Environment.processWinStation = processWindowStation;
        }
        return !Environment.isUserNonInteractive;
      }
    }

    /// <summary>获取与当前用户关联的网络域名。</summary>
    /// <returns>与当前用户关联的网络域名。</returns>
    /// <exception cref="T:System.PlatformNotSupportedException">操作系统不支持检索网络域的名称。</exception>
    /// <exception cref="T:System.InvalidOperationException">无法检索的网络域的名称。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="UserName;UserDomainName" />
    /// </PermissionSet>
    public static string UserDomainName
    {
      [SecuritySafeCritical] get
      {
        new EnvironmentPermission(EnvironmentPermissionAccess.Read, "UserDomain").Demand();
        byte[] sid = new byte[1024];
        int length1 = sid.Length;
        StringBuilder domainName = new StringBuilder(1024);
        uint domainNameLen1 = (uint) domainName.Capacity;
        if ((int) Win32Native.GetUserNameEx(2, domainName, ref domainNameLen1) == 1)
        {
          string @string = domainName.ToString();
          int length2 = @string.IndexOf('\\');
          if (length2 != -1)
            return @string.Substring(0, length2);
        }
        uint domainNameLen2 = (uint) domainName.Capacity;
        int peUse;
        if (!Win32Native.LookupAccountName((string) null, Environment.UserName, sid, ref length1, domainName, ref domainNameLen2, out peUse))
          throw new InvalidOperationException(Win32Native.GetMessage(Marshal.GetLastWin32Error()));
        return domainName.ToString();
      }
    }

    /// <summary>获取当前托管线程的唯一标识符。</summary>
    /// <returns>一个整数，表示此托管线程的唯一标识符。</returns>
    [__DynamicallyInvokable]
    public static int CurrentManagedThreadId
    {
      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success), __DynamicallyInvokable] get
      {
        return Thread.CurrentThread.ManagedThreadId;
      }
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void _Exit(int exitCode);

    /// <summary>终止此进程并为基础操作系统提供指定的退出代码。</summary>
    /// <param name="exitCode">提供给操作系统的退出代码。使用 0（零）指示处理已成功完成。</param>
    /// <exception cref="T:System.Security.SecurityException">调用方没有足够的安全权限来执行此函数。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public static void Exit(int exitCode)
    {
      Environment._Exit(exitCode);
    }

    /// <summary>向 Windows 的应用程序事件日志写入消息后立即终止进程，然后在发往 Microsoft 的错误报告中加入该消息。</summary>
    /// <param name="message">一条解释进程终止原因的消息；如果未提供解释，则为 null。</param>
    [SecurityCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern void FailFast(string message);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void FailFast(string message, uint exitCode);

    /// <summary>向 Windows 的应用程序事件日志写入消息后立即终止进程，然后在发往 Microsoft 的错误报告中加入该消息和异常信息。</summary>
    /// <param name="message">一条解释进程终止原因的消息；如果未提供解释，则为 null。</param>
    /// <param name="exception">一个异常，表示导致终止的错误。通常这是 catch 块中的异常。</param>
    [SecurityCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern void FailFast(string message, Exception exception);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void TriggerCodeContractFailure(ContractFailureKind failureKind, string message, string condition, string exceptionAsString);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetIsCLRHosted();

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetCommandLine(StringHandleOnStack retString);

    /// <summary>将嵌入到指定字符串中的每个环境变量的名称替换为该变量的值的等效字符串，然后返回结果字符串。</summary>
    /// <returns>一个字符串，其中的每个环境变量均被替换为该变量的值。</returns>
    /// <param name="name">包含零个或多个环境变量名的字符串。每个环境变量都用百分号 (%) 引起来。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static string ExpandEnvironmentVariables(string name)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      if (name.Length == 0 || AppDomain.IsAppXModel() && !AppDomain.IsAppXDesignMode())
        return name;
      int num1 = 100;
      StringBuilder lpDst = new StringBuilder(num1);
      bool flag1 = CodeAccessSecurityEngine.QuickCheckForAllDemands();
      string[] strArray = name.Split('%');
      StringBuilder stringBuilder = flag1 ? (StringBuilder) null : new StringBuilder();
      bool flag2 = false;
      for (int index = 1; index < strArray.Length - 1; ++index)
      {
        if (strArray[index].Length == 0 | flag2)
        {
          flag2 = false;
        }
        else
        {
          lpDst.Length = 0;
          string lpSrc = "%" + strArray[index] + "%";
          int num2 = Win32Native.ExpandEnvironmentStrings(lpSrc, lpDst, num1);
          if (num2 == 0)
            Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
          while (num2 > num1)
          {
            num1 = num2;
            lpDst.Capacity = num1;
            lpDst.Length = 0;
            num2 = Win32Native.ExpandEnvironmentStrings(lpSrc, lpDst, num1);
            if (num2 == 0)
              Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
          }
          if (!flag1)
          {
            flag2 = lpDst.ToString() != lpSrc;
            if (flag2)
            {
              stringBuilder.Append(strArray[index]);
              stringBuilder.Append(';');
            }
          }
        }
      }
      if (!flag1)
        new EnvironmentPermission(EnvironmentPermissionAccess.Read, stringBuilder.ToString()).Demand();
      lpDst.Length = 0;
      int num3 = Win32Native.ExpandEnvironmentStrings(name, lpDst, num1);
      if (num3 == 0)
        Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
      while (num3 > num1)
      {
        num1 = num3;
        lpDst.Capacity = num1;
        lpDst.Length = 0;
        num3 = Win32Native.ExpandEnvironmentStrings(name, lpDst, num1);
        if (num3 == 0)
          Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
      }
      return lpDst.ToString();
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern int GetProcessorCount();

    /// <summary>返回包含当前进程的命令行参数的字符串数组。</summary>
    /// <returns>字符串数组，其中的每个元素都包含一个命令行参数。第一个元素是可执行文件名，后面的零个或多个元素包含其余的命令行参数。</returns>
    /// <exception cref="T:System.NotSupportedException">系统不支持命令行参数。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="Path" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static string[] GetCommandLineArgs()
    {
      new EnvironmentPermission(EnvironmentPermissionAccess.Read, "Path").Demand();
      return Environment.GetCommandLineArgsNative();
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern string[] GetCommandLineArgsNative();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern string nativeGetEnvironmentVariable(string variable);

    /// <summary>从当前进程检索环境变量的值。</summary>
    /// <returns>
    /// <paramref name="variable" /> 指定的环境变量的值；或者如果找不到环境变量，则返回 null。</returns>
    /// <param name="variable">环境变量名。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="variable" /> 为 null。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所需的权限，才能执行此操作。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static string GetEnvironmentVariable(string variable)
    {
      if (variable == null)
        throw new ArgumentNullException("variable");
      if (AppDomain.IsAppXModel() && !AppDomain.IsAppXDesignMode())
        return (string) null;
      new EnvironmentPermission(EnvironmentPermissionAccess.Read, variable).Demand();
      StringBuilder sb = StringBuilderCache.Acquire(128);
      string lpName1 = variable;
      StringBuilder lpValue1 = sb;
      int capacity1 = lpValue1.Capacity;
      int environmentVariable = Win32Native.GetEnvironmentVariable(lpName1, lpValue1, capacity1);
      if (environmentVariable == 0 && Marshal.GetLastWin32Error() == 203)
      {
        StringBuilderCache.Release(sb);
        return (string) null;
      }
      string lpName2;
      StringBuilder lpValue2;
      int capacity2;
      for (; environmentVariable > sb.Capacity; environmentVariable = Win32Native.GetEnvironmentVariable(lpName2, lpValue2, capacity2))
      {
        sb.Capacity = environmentVariable;
        sb.Length = 0;
        lpName2 = variable;
        lpValue2 = sb;
        capacity2 = lpValue2.Capacity;
      }
      return StringBuilderCache.GetStringAndRelease(sb);
    }

    /// <summary>从当前进程或者从当前用户或本地计算机的 Windows 操作系统注册表项检索环境变量的值。</summary>
    /// <returns>
    /// <paramref name="variable" /> 和 <paramref name="target" /> 参数指定的环境变量的值；或者如果找不到环境变量，则返回 null。</returns>
    /// <param name="variable">环境变量名。</param>
    /// <param name="target">
    /// <see cref="T:System.EnvironmentVariableTarget" /> 值之一。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="variable" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="target" /> 不是有效 <see cref="T:System.EnvironmentVariableTarget" /> 值。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所需的权限，才能执行此操作。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static string GetEnvironmentVariable(string variable, EnvironmentVariableTarget target)
    {
      if (variable == null)
        throw new ArgumentNullException("variable");
      if (target == EnvironmentVariableTarget.Process)
        return Environment.GetEnvironmentVariable(variable);
      new EnvironmentPermission(PermissionState.Unrestricted).Demand();
      if (target == EnvironmentVariableTarget.Machine)
      {
        using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Control\\Session Manager\\Environment", false))
        {
          if (registryKey == null)
            return (string) null;
          return registryKey.GetValue(variable) as string;
        }
      }
      else if (target == EnvironmentVariableTarget.User)
      {
        using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Environment", false))
        {
          if (registryKey == null)
            return (string) null;
          return registryKey.GetValue(variable) as string;
        }
      }
      else
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) target));
    }

    [SecurityCritical]
    private static unsafe char[] GetEnvironmentCharArray()
    {
      char[] chArray = (char[]) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
      }
      finally
      {
        char* chPtr1 = (char*) null;
        try
        {
          chPtr1 = Win32Native.GetEnvironmentStrings();
          if ((IntPtr) chPtr1 == IntPtr.Zero)
            throw new OutOfMemoryException();
          char* chPtr2 = chPtr1;
          while ((int) *chPtr2 != 0 || (int) *(ushort*) ((IntPtr) chPtr2 + 2) != 0)
            chPtr2 += 2;
          int charCount = (int) (chPtr2 - chPtr1 + 1L);
          chArray = new char[charCount];
          fixed (char* dmem = chArray)
            string.wstrcpy(dmem, chPtr1, charCount);
        }
        finally
        {
          if ((IntPtr) chPtr1 != IntPtr.Zero)
            Win32Native.FreeEnvironmentStrings(chPtr1);
        }
      }
      return chArray;
    }

    /// <summary>从当前进程检索所有环境变量名及其值。</summary>
    /// <returns>包含所有环境变量名及其值的字典；如果找不到任何环境变量，则返回空字典。</returns>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所需的权限，才能执行此操作。</exception>
    /// <exception cref="T:System.OutOfMemoryException">缓冲区是内存不足。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static IDictionary GetEnvironmentVariables()
    {
      if (AppDomain.IsAppXModel() && !AppDomain.IsAppXDesignMode())
        return (IDictionary) new Hashtable(0);
      bool flag1 = CodeAccessSecurityEngine.QuickCheckForAllDemands();
      StringBuilder stringBuilder = flag1 ? (StringBuilder) null : new StringBuilder();
      bool flag2 = true;
      char[] environmentCharArray = Environment.GetEnvironmentCharArray();
      Hashtable hashtable = new Hashtable(20);
      for (int index = 0; index < environmentCharArray.Length; ++index)
      {
        int startIndex1 = index;
        while ((int) environmentCharArray[index] != 61 && (int) environmentCharArray[index] != 0)
          ++index;
        if ((int) environmentCharArray[index] != 0)
        {
          if (index - startIndex1 == 0)
          {
            while ((int) environmentCharArray[index] != 0)
              ++index;
          }
          else
          {
            string str1 = new string(environmentCharArray, startIndex1, index - startIndex1);
            ++index;
            int startIndex2 = index;
            while ((int) environmentCharArray[index] != 0)
              ++index;
            string str2 = new string(environmentCharArray, startIndex2, index - startIndex2);
            hashtable[(object) str1] = (object) str2;
            if (!flag1)
            {
              if (flag2)
                flag2 = false;
              else
                stringBuilder.Append(';');
              stringBuilder.Append(str1);
            }
          }
        }
      }
      if (!flag1)
        new EnvironmentPermission(EnvironmentPermissionAccess.Read, stringBuilder.ToString()).Demand();
      return (IDictionary) hashtable;
    }

    internal static IDictionary GetRegistryKeyNameValuePairs(RegistryKey registryKey)
    {
      Hashtable hashtable = new Hashtable(20);
      if (registryKey != null)
      {
        foreach (string valueName in registryKey.GetValueNames())
        {
          string @string = registryKey.GetValue(valueName, (object) "").ToString();
          hashtable.Add((object) valueName, (object) @string);
        }
      }
      return (IDictionary) hashtable;
    }

    /// <summary>从当前进程或者从当前用户或本地计算机的 Windows 操作系统注册表项检索所有环境变量名及其值。</summary>
    /// <returns>包含 <paramref name="target" /> 参数所指定的源中所有环境变量名及其值的字典；否则，如果找不到任何环境变量，则返回空字典。</returns>
    /// <param name="target">
    /// <see cref="T:System.EnvironmentVariableTarget" /> 值之一。</param>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所需的权限，才能执行此操作的指定值的 <paramref name="target" />。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="target" /> 包含一个非法值。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static IDictionary GetEnvironmentVariables(EnvironmentVariableTarget target)
    {
      if (target == EnvironmentVariableTarget.Process)
        return Environment.GetEnvironmentVariables();
      new EnvironmentPermission(PermissionState.Unrestricted).Demand();
      if (target == EnvironmentVariableTarget.Machine)
      {
        using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Control\\Session Manager\\Environment", false))
          return Environment.GetRegistryKeyNameValuePairs(registryKey);
      }
      else if (target == EnvironmentVariableTarget.User)
      {
        using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Environment", false))
          return Environment.GetRegistryKeyNameValuePairs(registryKey);
      }
      else
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) target));
    }

    /// <summary>创建、修改或删除当前进程中存储的环境变量。</summary>
    /// <param name="variable">环境变量名。</param>
    /// <param name="value">要分配给 <paramref name="variable" /> 的值。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="variable" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="variable" /> 包含零个字符的长度为零的字符串，初始十六进制 (0x00) 或等号 （"="）。- 或 -长度 <paramref name="variable" /> 或 <paramref name="value" /> 大于或等于 32767 个字符。- 或 -执行此操作期间出错。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所需的权限，才能执行此操作。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static void SetEnvironmentVariable(string variable, string value)
    {
      Environment.CheckEnvironmentVariableName(variable);
      new EnvironmentPermission(PermissionState.Unrestricted).Demand();
      if (string.IsNullOrEmpty(value) || (int) value[0] == 0)
        value = (string) null;
      else if (value.Length >= (int) short.MaxValue)
        throw new ArgumentException(Environment.GetResourceString("Argument_LongEnvVarValue"));
      if (AppDomain.IsAppXModel() && !AppDomain.IsAppXDesignMode())
        throw new PlatformNotSupportedException();
      if (Win32Native.SetEnvironmentVariable(variable, value))
        return;
      int lastWin32Error = Marshal.GetLastWin32Error();
      switch (lastWin32Error)
      {
        case 203:
          break;
        case 206:
          throw new ArgumentException(Environment.GetResourceString("Argument_LongEnvVarValue"));
        default:
          throw new ArgumentException(Win32Native.GetMessage(lastWin32Error));
      }
    }

    private static void CheckEnvironmentVariableName(string variable)
    {
      if (variable == null)
        throw new ArgumentNullException("variable");
      if (variable.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_StringZeroLength"), "variable");
      if ((int) variable[0] == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_StringFirstCharIsZero"), "variable");
      if (variable.Length >= (int) short.MaxValue)
        throw new ArgumentException(Environment.GetResourceString("Argument_LongEnvVarValue"));
      if (variable.IndexOf('=') != -1)
        throw new ArgumentException(Environment.GetResourceString("Argument_IllegalEnvVarName"));
    }

    /// <summary>创建、修改或删除当前进程中或者为当前用户或本地计算机保留的 Windows 操作系统注册表项中存储的环境变量。</summary>
    /// <param name="variable">环境变量名。</param>
    /// <param name="value">要分配给 <paramref name="variable" /> 的值。</param>
    /// <param name="target">一个用于指定环境变量的位置的枚举值。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="variable" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="variable" /> 包含零个字符的长度为零的字符串，初始十六进制 (0x00) 或等号 （"="）。- 或 -长度 <paramref name="variable" /> 大于或等于 32767 个字符。- 或 -<paramref name="target" /> 不是成员的 <see cref="T:System.EnvironmentVariableTarget" /> 枚举。- 或 -<paramref name="target" /> 是 <see cref="F:System.EnvironmentVariableTarget.Machine" /> 或 <see cref="F:System.EnvironmentVariableTarget.User" />, ，和长度 <paramref name="variable" /> 大于或等于 255。- 或 -<paramref name="target" /> 是 <see cref="F:System.EnvironmentVariableTarget.Process" /> 和长度 <paramref name="value" /> 大于或等于 32767 个字符。- 或 -执行此操作期间出错。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所需的权限，才能执行此操作。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static void SetEnvironmentVariable(string variable, string value, EnvironmentVariableTarget target)
    {
      if (target == EnvironmentVariableTarget.Process)
      {
        Environment.SetEnvironmentVariable(variable, value);
      }
      else
      {
        Environment.CheckEnvironmentVariableName(variable);
        if (variable.Length >= 1024)
          throw new ArgumentException(Environment.GetResourceString("Argument_LongEnvVarName"));
        new EnvironmentPermission(PermissionState.Unrestricted).Demand();
        if (string.IsNullOrEmpty(value) || (int) value[0] == 0)
          value = (string) null;
        if (target == EnvironmentVariableTarget.Machine)
        {
          using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Control\\Session Manager\\Environment", true))
          {
            if (registryKey != null)
            {
              if (value == null)
                registryKey.DeleteValue(variable, false);
              else
                registryKey.SetValue(variable, (object) value);
            }
          }
        }
        else if (target == EnvironmentVariableTarget.User)
        {
          if (variable.Length >= (int) byte.MaxValue)
            throw new ArgumentException(Environment.GetResourceString("Argument_LongEnvVarValue"));
          using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Environment", true))
          {
            if (registryKey != null)
            {
              if (value == null)
                registryKey.DeleteValue(variable, false);
              else
                registryKey.SetValue(variable, (object) value);
            }
          }
        }
        else
          throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) target));
        int num = Win32Native.SendMessageTimeout(new IntPtr((int) ushort.MaxValue), 26, IntPtr.Zero, "Environment", 0U, 1000U, IntPtr.Zero) == IntPtr.Zero ? 1 : 0;
      }
    }

    /// <summary>返回包含当前计算机中的逻辑驱动器名称的字符串数组。</summary>
    /// <returns>字符串数组，其中的每个元素都包含逻辑驱动器名称。例如，如果计算机的硬盘是第一个逻辑驱动器，则返回的第一个元素是“C:\”。</returns>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所需的权限。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static string[] GetLogicalDrives()
    {
      new EnvironmentPermission(PermissionState.Unrestricted).Demand();
      int logicalDrives = Win32Native.GetLogicalDrives();
      if (logicalDrives == 0)
        __Error.WinIOError();
      uint num1 = (uint) logicalDrives;
      int length = 0;
      while ((int) num1 != 0)
      {
        if (((int) num1 & 1) != 0)
          ++length;
        num1 >>= 1;
      }
      string[] strArray = new string[length];
      char[] chArray = new char[3]{ 'A', ':', '\\' };
      uint num2 = (uint) logicalDrives;
      int num3 = 0;
      while ((int) num2 != 0)
      {
        if (((int) num2 & 1) != 0)
          strArray[num3++] = new string(chArray);
        num2 >>= 1;
        ++chArray[0];
      }
      return strArray;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern long GetWorkingSet();

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool WinRTSupported();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool GetVersion(Win32Native.OSVERSIONINFO osVer);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool GetVersionEx(Win32Native.OSVERSIONINFOEX osVer);

    internal static string GetStackTrace(Exception e, bool needFileInfo)
    {
      return (e != null ? new System.Diagnostics.StackTrace(e, needFileInfo) : new System.Diagnostics.StackTrace(needFileInfo)).ToString(System.Diagnostics.StackTrace.TraceFormat.Normal);
    }

    [SecuritySafeCritical]
    private static void InitResourceHelper()
    {
      bool lockTaken = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        Monitor.Enter(Environment.InternalSyncObject, ref lockTaken);
        if (Environment.m_resHelper != null)
          return;
        Environment.ResourceHelper resourceHelper = new Environment.ResourceHelper("mscorlib");
        Thread.MemoryBarrier();
        Environment.m_resHelper = resourceHelper;
      }
      finally
      {
        if (lockTaken)
          Monitor.Exit(Environment.InternalSyncObject);
      }
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern string GetResourceFromDefault(string key);

    internal static string GetResourceStringLocal(string key)
    {
      if (Environment.m_resHelper == null)
        Environment.InitResourceHelper();
      return Environment.m_resHelper.GetResourceString(key);
    }

    [SecuritySafeCritical]
    internal static string GetResourceString(string key)
    {
      return Environment.GetResourceFromDefault(key);
    }

    [SecuritySafeCritical]
    internal static string GetResourceString(string key, params object[] values)
    {
      return string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString(key), values);
    }

    internal static string GetRuntimeResourceString(string key)
    {
      return Environment.GetResourceString(key);
    }

    internal static string GetRuntimeResourceString(string key, params object[] values)
    {
      return Environment.GetResourceString(key, values);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool GetCompatibilityFlag(CompatibilityFlag flag);

    /// <summary>获取由指定枚举标识的系统特殊文件夹的路径。</summary>
    /// <returns>如果指定的系统特殊文件夹实际存在于您的计算机上，则为到该文件夹的路径；否则为空字符串 ("")。如果系统未创建文件夹、已删除现有文件夹，或者文件夹是不对应物理路径的虚拟目录（例如"我的电脑"），则该文件夹不会实际存在。</returns>
    /// <param name="folder">标识系统特殊文件夹的枚举常数。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="folder" /> 不是成员的 <see cref="T:System.Environment.SpecialFolder" />。</exception>
    /// <exception cref="T:System.PlatformNotSupportedException">不支持当前的平台。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static string GetFolderPath(Environment.SpecialFolder folder)
    {
      if (!Enum.IsDefined(typeof (Environment.SpecialFolder), (object) folder))
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) folder));
      return Environment.InternalGetFolderPath(folder, Environment.SpecialFolderOption.None, false);
    }

    /// <summary>获取由指定枚举标识的系统特殊文件夹的路径，并使用用于访问特殊文件夹的指定选项。</summary>
    /// <returns>如果指定的系统特殊文件夹实际存在于您的计算机上，则为到该文件夹的路径；否则为空字符串 ("")。如果系统未创建文件夹、已删除现有文件夹，或者文件夹是不对应物理路径的虚拟目录（例如"我的电脑"），则该文件夹不会实际存在。</returns>
    /// <param name="folder">标识系统特殊文件夹的枚举常数。</param>
    /// <param name="option">指定用于访问特殊文件夹的选项。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="folder" /> 不是成员 <see cref="T:System.Environment.SpecialFolder" /></exception>
    /// <exception cref="T:System.PlatformNotSupportedException">
    ///   <see cref="T:System.PlatformNotSupportedException" />
    /// </exception>
    [SecuritySafeCritical]
    public static string GetFolderPath(Environment.SpecialFolder folder, Environment.SpecialFolderOption option)
    {
      if (!Enum.IsDefined(typeof (Environment.SpecialFolder), (object) folder))
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) folder));
      if (!Enum.IsDefined(typeof (Environment.SpecialFolderOption), (object) option))
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) option));
      return Environment.InternalGetFolderPath(folder, option, false);
    }

    [SecurityCritical]
    internal static string UnsafeGetFolderPath(Environment.SpecialFolder folder)
    {
      return Environment.InternalGetFolderPath(folder, Environment.SpecialFolderOption.None, true);
    }

    [SecurityCritical]
    private static string InternalGetFolderPath(Environment.SpecialFolder folder, Environment.SpecialFolderOption option, bool suppressSecurityChecks = false)
    {
      if (option == Environment.SpecialFolderOption.Create && !suppressSecurityChecks)
        new FileIOPermission(PermissionState.None)
        {
          AllFiles = FileIOPermissionAccess.Write
        }.Demand();
      StringBuilder lpszPath = new StringBuilder(260);
      int folderPath = Win32Native.SHGetFolderPath(IntPtr.Zero, (int) (folder | (Environment.SpecialFolder) option), IntPtr.Zero, 0, lpszPath);
      string path;
      if (folderPath < 0)
      {
        if (folderPath == -2146233031)
          throw new PlatformNotSupportedException();
        path = string.Empty;
      }
      else
        path = lpszPath.ToString();
      if (!suppressSecurityChecks)
        new FileIOPermission(FileIOPermissionAccess.PathDiscovery, path).Demand();
      return path;
    }

    internal sealed class ResourceHelper
    {
      private string m_name;
      private ResourceManager SystemResMgr;
      private Stack currentlyLoading;
      internal bool resourceManagerInited;
      private int infinitelyRecursingCount;

      internal ResourceHelper(string name)
      {
        this.m_name = name;
      }

      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
      internal string GetResourceString(string key)
      {
        if (key == null || key.Length == 0)
          return "[Resource lookup failed - null or empty resource name]";
        return this.GetResourceString(key, (CultureInfo) null);
      }

      [SecuritySafeCritical]
      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
      internal string GetResourceString(string key, CultureInfo culture)
      {
        if (key == null || key.Length == 0)
          return "[Resource lookup failed - null or empty resource name]";
        Environment.ResourceHelper.GetResourceStringUserData resourceStringUserData = new Environment.ResourceHelper.GetResourceStringUserData(this, key, culture);
        RuntimeHelpers.ExecuteCodeWithGuaranteedCleanup(new RuntimeHelpers.TryCode(this.GetResourceStringCode), new RuntimeHelpers.CleanupCode(this.GetResourceStringBackoutCode), (object) resourceStringUserData);
        return resourceStringUserData.m_retVal;
      }

      [SecuritySafeCritical]
      private void GetResourceStringCode(object userDataIn)
      {
        Environment.ResourceHelper.GetResourceStringUserData resourceStringUserData = (Environment.ResourceHelper.GetResourceStringUserData) userDataIn;
        Environment.ResourceHelper resourceHelper = resourceStringUserData.m_resourceHelper;
        string name = resourceStringUserData.m_key;
        CultureInfo cultureInfo = resourceStringUserData.m_culture;
        Monitor.Enter((object) resourceHelper, ref resourceStringUserData.m_lockWasTaken);
        if (resourceHelper.currentlyLoading != null && resourceHelper.currentlyLoading.Count > 0 && resourceHelper.currentlyLoading.Contains((object) name))
        {
          if (resourceHelper.infinitelyRecursingCount > 0)
          {
            resourceStringUserData.m_retVal = "[Resource lookup failed - infinite recursion or critical failure detected.]";
            return;
          }
          ++resourceHelper.infinitelyRecursingCount;
          string message = "Infinite recursion during resource lookup within mscorlib.  This may be a bug in mscorlib, or potentially in certain extensibility points such as assembly resolve events or CultureInfo names.  Resource name: " + name;
          Assert.Fail("[mscorlib recursive resource lookup bug]", message, -2146232797, System.Diagnostics.StackTrace.TraceFormat.NoResourceLookup);
          Environment.FailFast(message);
        }
        if (resourceHelper.currentlyLoading == null)
          resourceHelper.currentlyLoading = new Stack(4);
        if (!resourceHelper.resourceManagerInited)
        {
          RuntimeHelpers.PrepareConstrainedRegions();
          try
          {
          }
          finally
          {
            RuntimeHelpers.RunClassConstructor(typeof (ResourceManager).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof (ResourceReader).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof (RuntimeResourceSet).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof (BinaryReader).TypeHandle);
            resourceHelper.resourceManagerInited = true;
          }
        }
        resourceHelper.currentlyLoading.Push((object) name);
        if (resourceHelper.SystemResMgr == null)
          resourceHelper.SystemResMgr = new ResourceManager(this.m_name, typeof (object).Assembly);
        string @string = resourceHelper.SystemResMgr.GetString(name, (CultureInfo) null);
        resourceHelper.currentlyLoading.Pop();
        resourceStringUserData.m_retVal = @string;
      }

      [PrePrepareMethod]
      private void GetResourceStringBackoutCode(object userDataIn, bool exceptionThrown)
      {
        Environment.ResourceHelper.GetResourceStringUserData resourceStringUserData = (Environment.ResourceHelper.GetResourceStringUserData) userDataIn;
        Environment.ResourceHelper resourceHelper = resourceStringUserData.m_resourceHelper;
        if (exceptionThrown && resourceStringUserData.m_lockWasTaken)
        {
          resourceHelper.SystemResMgr = (ResourceManager) null;
          resourceHelper.currentlyLoading = (Stack) null;
        }
        if (!resourceStringUserData.m_lockWasTaken)
          return;
        Monitor.Exit((object) resourceHelper);
      }

      internal class GetResourceStringUserData
      {
        public Environment.ResourceHelper m_resourceHelper;
        public string m_key;
        public CultureInfo m_culture;
        public string m_retVal;
        public bool m_lockWasTaken;

        public GetResourceStringUserData(Environment.ResourceHelper resourceHelper, string key, CultureInfo culture)
        {
          this.m_resourceHelper = resourceHelper;
          this.m_key = key;
          this.m_culture = culture;
        }
      }
    }

    /// <summary>指定用于获取特殊文件夹路径的选项。</summary>
    public enum SpecialFolderOption
    {
      None = 0,
      DoNotVerify = 16384,
      Create = 32768,
    }

    /// <summary>指定用于检索系统特殊文件夹的目录路径的枚举常数。</summary>
    [ComVisible(true)]
    public enum SpecialFolder
    {
      Desktop = 0,
      Programs = 2,
      MyDocuments = 5,
      Personal = 5,
      Favorites = 6,
      Startup = 7,
      Recent = 8,
      SendTo = 9,
      StartMenu = 11,
      MyMusic = 13,
      MyVideos = 14,
      DesktopDirectory = 16,
      MyComputer = 17,
      NetworkShortcuts = 19,
      Fonts = 20,
      Templates = 21,
      CommonStartMenu = 22,
      CommonPrograms = 23,
      CommonStartup = 24,
      CommonDesktopDirectory = 25,
      ApplicationData = 26,
      PrinterShortcuts = 27,
      LocalApplicationData = 28,
      InternetCache = 32,
      Cookies = 33,
      History = 34,
      CommonApplicationData = 35,
      Windows = 36,
      System = 37,
      ProgramFiles = 38,
      MyPictures = 39,
      UserProfile = 40,
      SystemX86 = 41,
      ProgramFilesX86 = 42,
      CommonProgramFiles = 43,
      CommonProgramFilesX86 = 44,
      CommonTemplates = 45,
      CommonDocuments = 46,
      CommonAdminTools = 47,
      AdminTools = 48,
      CommonMusic = 53,
      CommonPictures = 54,
      CommonVideos = 55,
      Resources = 56,
      LocalizedResources = 57,
      CommonOemLinks = 58,
      CDBurning = 59,
    }
  }
}
