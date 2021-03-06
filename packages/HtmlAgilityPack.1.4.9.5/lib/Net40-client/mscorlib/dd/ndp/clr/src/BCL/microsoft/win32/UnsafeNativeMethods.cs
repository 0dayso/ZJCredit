﻿// Decompiled with JetBrains decompiler
// Type: Microsoft.Win32.UnsafeNativeMethods
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System;
using System.Diagnostics.Tracing;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace Microsoft.Win32
{
  [SecurityCritical]
  [SuppressUnmanagedCodeSecurity]
  internal static class UnsafeNativeMethods
  {
    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern int GetTimeZoneInformation(out Win32Native.TimeZoneInformation lpTimeZoneInformation);

    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern int GetDynamicTimeZoneInformation(out Win32Native.DynamicTimeZoneInformation lpDynamicTimeZoneInformation);

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool GetFileMUIPath(int flags, [MarshalAs(UnmanagedType.LPWStr)] string filePath, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder language, ref int languageLength, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder fileMuiPath, ref int fileMuiPathLength, ref long enumerator);

    [DllImport("user32.dll", EntryPoint = "LoadStringW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
    internal static extern int LoadString(SafeLibraryHandle handle, int id, StringBuilder buffer, int bufferLength);

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    internal static extern SafeLibraryHandle LoadLibraryEx(string libFilename, IntPtr reserved, int flags);

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool FreeLibrary(IntPtr hModule);

    [SecurityCritical]
    [DllImport("combase.dll")]
    internal static extern int RoGetActivationFactory([MarshalAs((UnmanagedType) 0)] string activatableClassId, [In] ref Guid iid, [MarshalAs((UnmanagedType) 0)] out object factory);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    internal static class ManifestEtw
    {
      internal const int ERROR_ARITHMETIC_OVERFLOW = 534;
      internal const int ERROR_NOT_ENOUGH_MEMORY = 8;
      internal const int ERROR_MORE_DATA = 234;
      internal const int ERROR_NOT_SUPPORTED = 50;
      internal const int ERROR_INVALID_PARAMETER = 87;
      internal const int EVENT_CONTROL_CODE_DISABLE_PROVIDER = 0;
      internal const int EVENT_CONTROL_CODE_ENABLE_PROVIDER = 1;
      internal const int EVENT_CONTROL_CODE_CAPTURE_STATE = 2;

      [SecurityCritical]
      [DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
      internal static extern unsafe uint EventRegister([In] ref Guid providerId, [In] UnsafeNativeMethods.ManifestEtw.EtwEnableCallback enableCallback, [In] void* callbackContext, [In, Out] ref long registrationHandle);

      [SecurityCritical]
      [DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
      internal static extern uint EventUnregister([In] long registrationHandle);

      [SecurityCritical]
      [DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
      internal static extern unsafe int EventWrite([In] long registrationHandle, [In] ref EventDescriptor eventDescriptor, [In] int userDataCount, [In] EventProvider.EventData* userData);

      [SecurityCritical]
      [DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
      internal static extern int EventWriteString([In] long registrationHandle, [In] byte level, [In] long keyword, [In] string msg);

      internal static unsafe int EventWriteTransferWrapper(long registrationHandle, ref EventDescriptor eventDescriptor, Guid* activityId, Guid* relatedActivityId, int userDataCount, EventProvider.EventData* userData)
      {
        int num = UnsafeNativeMethods.ManifestEtw.EventWriteTransfer(registrationHandle, ref eventDescriptor, activityId, relatedActivityId, userDataCount, userData);
        if (num == 87 && (IntPtr) relatedActivityId == IntPtr.Zero)
        {
          Guid guid = Guid.Empty;
          num = UnsafeNativeMethods.ManifestEtw.EventWriteTransfer(registrationHandle, ref eventDescriptor, activityId, &guid, userDataCount, userData);
        }
        return num;
      }

      [SuppressUnmanagedCodeSecurity]
      [DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
      private static extern unsafe int EventWriteTransfer([In] long registrationHandle, [In] ref EventDescriptor eventDescriptor, [In] Guid* activityId, [In] Guid* relatedActivityId, [In] int userDataCount, [In] EventProvider.EventData* userData);

      [SuppressUnmanagedCodeSecurity]
      [DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
      internal static extern int EventActivityIdControl([In] UnsafeNativeMethods.ManifestEtw.ActivityControl ControlCode, [In, Out] ref Guid ActivityId);

      [SuppressUnmanagedCodeSecurity]
      [DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
      internal static extern unsafe int EventSetInformation([In] long registrationHandle, [In] UnsafeNativeMethods.ManifestEtw.EVENT_INFO_CLASS informationClass, [In] void* eventInformation, [In] int informationLength);

      [SuppressUnmanagedCodeSecurity]
      [DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
      internal static extern unsafe int EnumerateTraceGuidsEx(UnsafeNativeMethods.ManifestEtw.TRACE_QUERY_INFO_CLASS TraceQueryInfoClass, void* InBuffer, int InBufferSize, void* OutBuffer, int OutBufferSize, ref int ReturnLength);

      [SecurityCritical]
      internal unsafe delegate void EtwEnableCallback([In] ref Guid sourceId, [In] int isEnabled, [In] byte level, [In] long matchAnyKeywords, [In] long matchAllKeywords, [In] UnsafeNativeMethods.ManifestEtw.EVENT_FILTER_DESCRIPTOR* filterData, [In] void* callbackContext);

      internal struct EVENT_FILTER_DESCRIPTOR
      {
        public long Ptr;
        public int Size;
        public int Type;
      }

      internal enum ActivityControl : uint
      {
        EVENT_ACTIVITY_CTRL_GET_ID = 1,
        EVENT_ACTIVITY_CTRL_SET_ID = 2,
        EVENT_ACTIVITY_CTRL_CREATE_ID = 3,
        EVENT_ACTIVITY_CTRL_GET_SET_ID = 4,
        EVENT_ACTIVITY_CTRL_CREATE_SET_ID = 5,
      }

      internal enum EVENT_INFO_CLASS
      {
        BinaryTrackInfo,
        SetEnableAllKeywords,
        SetTraits,
      }

      internal enum TRACE_QUERY_INFO_CLASS
      {
        TraceGuidQueryList,
        TraceGuidQueryInfo,
        TraceGuidQueryProcess,
        TraceStackTracingInfo,
        MaxTraceSetInfoClass,
      }

      internal struct TRACE_GUID_INFO
      {
        public int InstanceCount;
        public int Reserved;
      }

      internal struct TRACE_PROVIDER_INSTANCE_INFO
      {
        public int NextOffset;
        public int EnableCount;
        public int Pid;
        public int Flags;
      }

      internal struct TRACE_ENABLE_INFO
      {
        public int IsEnabled;
        public byte Level;
        public byte Reserved1;
        public ushort LoggerId;
        public int EnableProperty;
        public int Reserved2;
        public long MatchAnyKeyword;
        public long MatchAllKeyword;
      }
    }
  }
}
