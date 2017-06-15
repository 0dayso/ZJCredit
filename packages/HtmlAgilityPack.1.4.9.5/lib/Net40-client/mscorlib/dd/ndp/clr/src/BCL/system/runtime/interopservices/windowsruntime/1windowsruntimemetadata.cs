// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.NamespaceResolveEventArgs
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.ObjectModel;
using System.Reflection;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  /// <summary>为 <see cref="E:System.Runtime.InteropServices.WindowsRuntime.WindowsRuntimeMetadata.ReflectionOnlyNamespaceResolve" /> 事件提供数据。</summary>
  [ComVisible(false)]
  public class NamespaceResolveEventArgs : EventArgs
  {
    private string _NamespaceName;
    private Assembly _RequestingAssembly;
    private Collection<Assembly> _ResolvedAssemblies;

    /// <summary>获取要解析的命名空间的名称。</summary>
    /// <returns>要解析的命名空间的名称。</returns>
    public string NamespaceName
    {
      get
      {
        return this._NamespaceName;
      }
    }

    /// <summary>获取正在解析其依赖项的程序集名称。</summary>
    /// <returns>正在解析其依赖项的程序集名称。</returns>
    public Assembly RequestingAssembly
    {
      get
      {
        return this._RequestingAssembly;
      }
    }

    /// <summary>获取程序集的集合，当 <see cref="E:System.Runtime.InteropServices.WindowsRuntime.WindowsRuntimeMetadata.ReflectionOnlyNamespaceResolve" /> 事件的事件处理程序被调用时，该集合为空，即事件处理程序负责添加需要的程序集。</summary>
    /// <returns>定义请求的命名空间程序集的集合。</returns>
    public Collection<Assembly> ResolvedAssemblies
    {
      get
      {
        return this._ResolvedAssemblies;
      }
    }

    /// <summary>初始化 <see cref="T:System.Runtime.InteropServices.WindowsRuntime.NamespaceResolveEventArgs" /> 类的新实例，同时指定要解析的项的命名空间以及正在解析其依赖项的程序集。</summary>
    /// <param name="namespaceName">要解析的命名空间。</param>
    /// <param name="requestingAssembly">正在解析其依赖项的程序集。</param>
    public NamespaceResolveEventArgs(string namespaceName, Assembly requestingAssembly)
    {
      this._NamespaceName = namespaceName;
      this._RequestingAssembly = requestingAssembly;
      this._ResolvedAssemblies = new Collection<Assembly>();
    }
  }
}
