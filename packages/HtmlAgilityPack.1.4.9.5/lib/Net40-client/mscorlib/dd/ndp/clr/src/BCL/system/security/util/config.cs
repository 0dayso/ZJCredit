// Decompiled with JetBrains decompiler
// Type: System.Security.Util.Config
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Policy;

namespace System.Security.Util
{
  internal static class Config
  {
    private static volatile string m_machineConfig;
    private static volatile string m_userConfig;

    internal static string MachineDirectory
    {
      [SecurityCritical] get
      {
        Config.GetFileLocales();
        return Config.m_machineConfig;
      }
    }

    internal static string UserDirectory
    {
      [SecurityCritical] get
      {
        Config.GetFileLocales();
        return Config.m_userConfig;
      }
    }

    [SecurityCritical]
    private static void GetFileLocales()
    {
      if (Config.m_machineConfig == null)
      {
        string s = (string) null;
        Config.GetMachineDirectory(JitHelpers.GetStringHandleOnStack(ref s));
        Config.m_machineConfig = s;
      }
      if (Config.m_userConfig != null)
        return;
      string s1 = (string) null;
      Config.GetUserDirectory(JitHelpers.GetStringHandleOnStack(ref s1));
      Config.m_userConfig = s1;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern int SaveDataByte(string path, [In] byte[] data, int length);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern bool RecoverData(ConfigId id);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void SetQuickCache(ConfigId id, QuickCacheEntryType quickCacheFlags);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern bool GetCacheEntry(ConfigId id, int numKey, [In] byte[] key, int keyLength, ObjectHandleOnStack retData);

    [SecurityCritical]
    internal static bool GetCacheEntry(ConfigId id, int numKey, byte[] key, out byte[] data)
    {
      byte[] o = (byte[]) null;
      int num1 = (int) id;
      int numKey1 = numKey;
      byte[] key1 = key;
      int length = key1.Length;
      ObjectHandleOnStack objectHandleOnStack = JitHelpers.GetObjectHandleOnStack<byte[]>(ref o);
      int num2 = Config.GetCacheEntry((ConfigId) num1, numKey1, key1, length, objectHandleOnStack) ? 1 : 0;
      data = o;
      return num2 != 0;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void AddCacheEntry(ConfigId id, int numKey, [In] byte[] key, int keyLength, byte[] data, int dataLength);

    [SecurityCritical]
    internal static void AddCacheEntry(ConfigId id, int numKey, byte[] key, byte[] data)
    {
      int num = (int) id;
      int numKey1 = numKey;
      byte[] key1 = key;
      int length1 = key1.Length;
      byte[] data1 = data;
      int length2 = data1.Length;
      Config.AddCacheEntry((ConfigId) num, numKey1, key1, length1, data1, length2);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void ResetCacheData(ConfigId id);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetMachineDirectory(StringHandleOnStack retDirectory);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetUserDirectory(StringHandleOnStack retDirectory);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern bool WriteToEventLog(string message);
  }
}
