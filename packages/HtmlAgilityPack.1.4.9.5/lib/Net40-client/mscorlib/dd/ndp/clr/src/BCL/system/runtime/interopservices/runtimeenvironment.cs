// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.RuntimeEnvironment
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace System.Runtime.InteropServices
{
  /// <summary>提供一个返回有关公共语言运行时环境的信息的 static 方法的集合。</summary>
  [ComVisible(true)]
  public class RuntimeEnvironment
  {
    /// <summary>获取系统配置文件的路径。</summary>
    /// <returns>系统配置文件的路径。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public static string SystemConfigurationFile
    {
      [SecuritySafeCritical] get
      {
        StringBuilder stringBuilder = new StringBuilder(260);
        string runtimeDirectory = RuntimeEnvironment.GetRuntimeDirectory();
        stringBuilder.Append(runtimeDirectory);
        string configurationFile = AppDomainSetup.RuntimeConfigurationFile;
        stringBuilder.Append(configurationFile);
        string @string = stringBuilder.ToString();
        new FileIOPermission(FileIOPermissionAccess.PathDiscovery, @string).Demand();
        return @string;
      }
    }

    /// <summary>初始化 <see cref="T:System.Runtime.InteropServices.RuntimeEnvironment" /> 类的新实例。</summary>
    [Obsolete("Do not create instances of the RuntimeEnvironment class.  Call the static methods directly on this type instead", true)]
    public RuntimeEnvironment()
    {
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern string GetModuleFileName();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern string GetDeveloperPath();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern string GetHostBindingFile();

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void _GetSystemVersion(StringHandleOnStack retVer);

    /// <summary>测试是否将指定的程序集加载到全局程序集缓存中。</summary>
    /// <returns>如果程序集已加载到全局程序集缓存中，则为 true；否则为 false。</returns>
    /// <param name="a">要测试的程序集。</param>
    public static bool FromGlobalAccessCache(Assembly a)
    {
      return a.GlobalAssemblyCache;
    }

    /// <summary>获取正在运行当前进程的公共语言运行时的版本号。</summary>
    /// <returns>字符串，包含公共语言运行时的版本号。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static string GetSystemVersion()
    {
      string s = (string) null;
      RuntimeEnvironment._GetSystemVersion(JitHelpers.GetStringHandleOnStack(ref s));
      return s;
    }

    /// <summary>返回公共语言运行时的安装目录。</summary>
    /// <returns>一个字符串，包含公共语言运行时的安装目录的路径。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static string GetRuntimeDirectory()
    {
      string runtimeDirectoryImpl = RuntimeEnvironment.GetRuntimeDirectoryImpl();
      new FileIOPermission(FileIOPermissionAccess.PathDiscovery, runtimeDirectoryImpl).Demand();
      return runtimeDirectoryImpl;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern string GetRuntimeDirectoryImpl();

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern IntPtr GetRuntimeInterfaceImpl([MarshalAs(UnmanagedType.LPStruct), In] Guid clsid, [MarshalAs(UnmanagedType.LPStruct), In] Guid riid);

    /// <summary>返回指定类上的指定接口。</summary>
    /// <returns>指向所请求接口的非托管指针。</returns>
    /// <param name="clsid">所需类的标识符。</param>
    /// <param name="riid">所需接口的标识符。</param>
    /// <exception cref="T:System.Runtime.InteropServices.COMException">IUnknown::QueryInterface 失败。</exception>
    [SecurityCritical]
    [ComVisible(false)]
    public static IntPtr GetRuntimeInterfaceAsIntPtr(Guid clsid, Guid riid)
    {
      return RuntimeEnvironment.GetRuntimeInterfaceImpl(clsid, riid);
    }

    /// <summary>返回通过指向 COM 对象的 IUnknown 接口的指针表示该对象的类型实例。</summary>
    /// <returns>一个对象，表示指定的非托管 COM 对象。</returns>
    /// <param name="clsid">所需类的标识符。</param>
    /// <param name="riid">所需接口的标识符。</param>
    /// <exception cref="T:System.Runtime.InteropServices.COMException">IUnknown::QueryInterface 失败。</exception>
    [SecurityCritical]
    [ComVisible(false)]
    public static object GetRuntimeInterfaceAsObject(Guid clsid, Guid riid)
    {
      IntPtr pUnk = IntPtr.Zero;
      try
      {
        pUnk = RuntimeEnvironment.GetRuntimeInterfaceImpl(clsid, riid);
        return Marshal.GetObjectForIUnknown(pUnk);
      }
      finally
      {
        if (pUnk != IntPtr.Zero)
          Marshal.Release(pUnk);
      }
    }
  }
}
