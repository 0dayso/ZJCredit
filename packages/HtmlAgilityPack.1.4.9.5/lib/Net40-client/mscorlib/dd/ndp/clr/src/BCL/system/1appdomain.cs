// Decompiled with JetBrains decompiler
// Type: System.AssemblyLoadEventArgs
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.InteropServices;

namespace System
{
  /// <summary>为 <see cref="E:System.AppDomain.AssemblyLoad" /> 事件提供数据。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  public class AssemblyLoadEventArgs : EventArgs
  {
    private Assembly _LoadedAssembly;

    /// <summary>获取一个表示当前加载的程序集的 <see cref="T:System.Reflection.Assembly" />。</summary>
    /// <returns>表示当前加载的程序集的 <see cref="T:System.Reflection.Assembly" /> 实例。</returns>
    /// <filterpriority>2</filterpriority>
    public Assembly LoadedAssembly
    {
      get
      {
        return this._LoadedAssembly;
      }
    }

    /// <summary>使用指定 <see cref="T:System.Reflection.Assembly" /> 初始化 <see cref="T:System.AssemblyLoadEventArgs" /> 类的新实例。</summary>
    /// <param name="loadedAssembly">表示当前加载的程序集的实例。</param>
    public AssemblyLoadEventArgs(Assembly loadedAssembly)
    {
      this._LoadedAssembly = loadedAssembly;
    }
  }
}
