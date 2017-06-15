// Decompiled with JetBrains decompiler
// Type: System.Runtime.ProfileOptimization
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime
{
  /// <summary>提高应用程序在应用范围的启动性能需要通过 just-in-time (JIT) 编辑器根据以前版本中生成的配置文件来执行那么可能要执行的背景编辑方法。</summary>
  public static class ProfileOptimization
  {
    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void InternalSetProfileRoot(string directoryPath);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void InternalStartProfile(string profile, IntPtr ptrNativeAssemblyLoadContext);

    /// <summary>启动当前应用程序域的优化配置，并设置优化配置文件存储的文件夹。在单核计算机上，此方法被忽略。</summary>
    /// <param name="directoryPath">到存储配置文件用于当前应用程序域的的文件夹的完整路径。</param>
    [SecurityCritical]
    public static void SetProfileRoot(string directoryPath)
    {
      ProfileOptimization.InternalSetProfileRoot(directoryPath);
    }

    /// <summary>开始在指定的配置文件以前记录的方法的实时（JIT）编辑，在后台线程。开始记录当前方法使用的过程，之后复盖指定的配置文件。</summary>
    /// <param name="profile">要使用的配置文件的文件名。</param>
    [SecurityCritical]
    public static void StartProfile(string profile)
    {
      ProfileOptimization.InternalStartProfile(profile, IntPtr.Zero);
    }
  }
}
