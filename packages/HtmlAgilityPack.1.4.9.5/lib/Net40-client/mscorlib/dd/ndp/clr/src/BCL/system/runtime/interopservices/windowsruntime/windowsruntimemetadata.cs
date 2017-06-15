// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.WindowsRuntimeMetadata
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  /// <summary>提供事件，用于解析仅反射类型请求，用于 Windows 元数据文件提供的类型，以及执行解析的方法。</summary>
  public static class WindowsRuntimeMetadata
  {
    /// <summary>当 Windows 元数据文件的解析在仅限反射的上下文中失败时发生。</summary>
    public static event EventHandler<NamespaceResolveEventArgs> ReflectionOnlyNamespaceResolve;

    /// <summary>当 Windows Metadata 文件解析在设计环境中失败时发生。</summary>
    public static event EventHandler<DesignerNamespaceResolveEventArgs> DesignerNamespaceResolve;

    /// <summary>找到此指定命名空间的 Windows Metadata 文件，给定指定位置搜索。</summary>
    /// <returns>表示定义 <paramref name="namespaceName" /> 的 Windows 元数据文件字符串的一个可枚举的列表。</returns>
    /// <param name="namespaceName">要解析的命名空间。</param>
    /// <param name="packageGraphFilePaths">要搜索 Windows 元数据文件的应用程序路径，或仅要搜索操作系统安装中的 null Windows 元数据文件。</param>
    /// <exception cref="T:System.PlatformNotSupportedException">该操作系统版本不支持 Windows 运行时。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="namespaceName" /> 为 null。</exception>
    [SecurityCritical]
    public static IEnumerable<string> ResolveNamespace(string namespaceName, IEnumerable<string> packageGraphFilePaths)
    {
      return WindowsRuntimeMetadata.ResolveNamespace(namespaceName, (string) null, packageGraphFilePaths);
    }

    /// <summary>找到此指定命名空间的 Windows Metadata 文件，给定指定位置搜索。</summary>
    /// <returns>表示定义 <paramref name="namespaceName" /> 的 Windows 元数据文件字符串的一个可枚举的列表。</returns>
    /// <param name="namespaceName">要解析的命名空间。</param>
    /// <param name="windowsSdkFilePath">用来搜索由 SDK 提供的 Windows 元数据文件的路径，或用来搜索来自操作系统安装的 Windows 元数据文件，则为 null。</param>
    /// <param name="packageGraphFilePaths">要搜索 Windows 元数据文件的应用程序路径。</param>
    /// <exception cref="T:System.PlatformNotSupportedException">该操作系统版本不支持 Windows 运行时。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="namespaceName" /> 为 null。</exception>
    [SecurityCritical]
    public static IEnumerable<string> ResolveNamespace(string namespaceName, string windowsSdkFilePath, IEnumerable<string> packageGraphFilePaths)
    {
      if (namespaceName == null)
        throw new ArgumentNullException("namespaceName");
      string[] strArray = (string[]) null;
      if (packageGraphFilePaths != null)
      {
        List<string> stringList = new List<string>(packageGraphFilePaths);
        strArray = new string[stringList.Count];
        int index = 0;
        foreach (string str in stringList)
        {
          strArray[index] = str;
          ++index;
        }
      }
      string[] o = (string[]) null;
      string namespaceName1 = namespaceName;
      string windowsSdkFilePath1 = windowsSdkFilePath;
      string[] packageGraphFilePaths1 = strArray;
      int cPackageGraphFilePaths = packageGraphFilePaths1 == null ? 0 : strArray.Length;
      ObjectHandleOnStack objectHandleOnStack = JitHelpers.GetObjectHandleOnStack<string[]>(ref o);
      WindowsRuntimeMetadata.nResolveNamespace(namespaceName1, windowsSdkFilePath1, packageGraphFilePaths1, cPackageGraphFilePaths, objectHandleOnStack);
      return (IEnumerable<string>) o;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void nResolveNamespace(string namespaceName, string windowsSdkFilePath, string[] packageGraphFilePaths, int cPackageGraphFilePaths, ObjectHandleOnStack retFileNames);

    internal static RuntimeAssembly[] OnReflectionOnlyNamespaceResolveEvent(AppDomain appDomain, RuntimeAssembly assembly, string namespaceName)
    {
      // ISSUE: reference to a compiler-generated field
      EventHandler<NamespaceResolveEventArgs> eventHandler = WindowsRuntimeMetadata.ReflectionOnlyNamespaceResolve;
      if (eventHandler != null)
      {
        Delegate[] invocationList = eventHandler.GetInvocationList();
        int length = invocationList.Length;
        for (int index1 = 0; index1 < length; ++index1)
        {
          NamespaceResolveEventArgs e = new NamespaceResolveEventArgs(namespaceName, (Assembly) assembly);
          ((EventHandler<NamespaceResolveEventArgs>) invocationList[index1])((object) appDomain, e);
          Collection<Assembly> resolvedAssemblies = e.ResolvedAssemblies;
          if (resolvedAssemblies.Count > 0)
          {
            RuntimeAssembly[] runtimeAssemblyArray = new RuntimeAssembly[resolvedAssemblies.Count];
            int index2 = 0;
            foreach (Assembly asm in resolvedAssemblies)
            {
              runtimeAssemblyArray[index2] = AppDomain.GetRuntimeAssembly(asm);
              ++index2;
            }
            return runtimeAssemblyArray;
          }
        }
      }
      return (RuntimeAssembly[]) null;
    }

    internal static string[] OnDesignerNamespaceResolveEvent(AppDomain appDomain, string namespaceName)
    {
      // ISSUE: reference to a compiler-generated field
      EventHandler<DesignerNamespaceResolveEventArgs> eventHandler = WindowsRuntimeMetadata.DesignerNamespaceResolve;
      if (eventHandler != null)
      {
        Delegate[] invocationList = eventHandler.GetInvocationList();
        int length = invocationList.Length;
        for (int index1 = 0; index1 < length; ++index1)
        {
          DesignerNamespaceResolveEventArgs e = new DesignerNamespaceResolveEventArgs(namespaceName);
          ((EventHandler<DesignerNamespaceResolveEventArgs>) invocationList[index1])((object) appDomain, e);
          Collection<string> resolvedAssemblyFiles = e.ResolvedAssemblyFiles;
          if (resolvedAssemblyFiles.Count > 0)
          {
            string[] strArray = new string[resolvedAssemblyFiles.Count];
            int index2 = 0;
            foreach (string str in resolvedAssemblyFiles)
            {
              if (string.IsNullOrEmpty(str))
                throw new ArgumentException(Environment.GetResourceString("Arg_EmptyOrNullString"), "DesignerNamespaceResolveEventArgs.ResolvedAssemblyFiles");
              strArray[index2] = str;
              ++index2;
            }
            return strArray;
          }
        }
      }
      return (string[]) null;
    }
  }
}
