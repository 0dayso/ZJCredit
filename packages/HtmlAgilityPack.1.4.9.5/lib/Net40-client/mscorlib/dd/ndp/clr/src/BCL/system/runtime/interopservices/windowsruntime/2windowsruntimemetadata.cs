// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.DesignerNamespaceResolveEventArgs
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.ObjectModel;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  /// <summary>为 <see cref="E:System.Runtime.InteropServices.WindowsRuntime.WindowsRuntimeMetadata.DesignerNamespaceResolve" /> 事件提供数据。</summary>
  [ComVisible(false)]
  public class DesignerNamespaceResolveEventArgs : EventArgs
  {
    private string _NamespaceName;
    private Collection<string> _ResolvedAssemblyFiles;

    /// <summary>获取要解析的命名空间的名称。</summary>
    /// <returns>要解析的命名空间的名称。</returns>
    public string NamespaceName
    {
      get
      {
        return this._NamespaceName;
      }
    }

    /// <summary>获取程序集文件的结合， 当 <see cref="E:System.Runtime.InteropServices.WindowsRuntime.WindowsRuntimeMetadata.DesignerNamespaceResolve" /> 事件的事件处理程序被调用时，该集合为空，即事件处理程序负责添加需要的程序集。</summary>
    /// <returns>定义请求的命名空间程序集文件的集合。</returns>
    public Collection<string> ResolvedAssemblyFiles
    {
      get
      {
        return this._ResolvedAssemblyFiles;
      }
    }

    /// <summary>初始化 <see cref="T:System.Runtime.InteropServices.WindowsRuntime.DesignerNamespaceResolveEventArgs" /> 类的新实例。</summary>
    /// <param name="namespaceName">要解析的命名空间的名称。</param>
    public DesignerNamespaceResolveEventArgs(string namespaceName)
    {
      this._NamespaceName = namespaceName;
      this._ResolvedAssemblyFiles = new Collection<string>();
    }
  }
}
