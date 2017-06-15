// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.UnsafeNativeMethods
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  internal static class UnsafeNativeMethods
  {
    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("api-ms-win-core-winrt-error-l1-1-1.dll", PreserveSig = false)]
    internal static extern IRestrictedErrorInfo GetRestrictedErrorInfo();

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("api-ms-win-core-winrt-error-l1-1-1.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool RoOriginateLanguageException(int error, [MarshalAs((UnmanagedType) 0)] string message, IntPtr languageException);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("api-ms-win-core-winrt-error-l1-1-1.dll", PreserveSig = false)]
    internal static extern void RoReportUnhandledError(IRestrictedErrorInfo error);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("api-ms-win-core-winrt-string-l1-1-0.dll", CallingConvention = CallingConvention.StdCall)]
    internal static extern unsafe int WindowsCreateString([MarshalAs(UnmanagedType.LPWStr)] string sourceString, int length, [Out] IntPtr* hstring);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("api-ms-win-core-winrt-string-l1-1-0.dll", CallingConvention = CallingConvention.StdCall)]
    internal static extern unsafe int WindowsCreateStringReference(char* sourceString, int length, [Out] HSTRING_HEADER* hstringHeader, [Out] IntPtr* hstring);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("api-ms-win-core-winrt-string-l1-1-0.dll", CallingConvention = CallingConvention.StdCall)]
    internal static extern int WindowsDeleteString(IntPtr hstring);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("api-ms-win-core-winrt-string-l1-1-0.dll", CallingConvention = CallingConvention.StdCall)]
    internal static extern unsafe char* WindowsGetStringRawBuffer(IntPtr hstring, [Out] uint* length);
  }
}
