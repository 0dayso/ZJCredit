// Decompiled with JetBrains decompiler
// Type: System.ResolveEventArgs
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.InteropServices;

namespace System
{
  /// <summary>为加载程序解析事件（如 <see cref="E:System.AppDomain.TypeResolve" />、<see cref="E:System.AppDomain.ResourceResolve" />、<see cref="E:System.AppDomain.ReflectionOnlyAssemblyResolve" /> 和 <see cref="E:System.AppDomain.AssemblyResolve" /> 事件）提供数据。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  public class ResolveEventArgs : EventArgs
  {
    private string _Name;
    private Assembly _RequestingAssembly;

    /// <summary>获取要解析的项的名称。</summary>
    /// <returns>要解析的项的名称。</returns>
    /// <filterpriority>2</filterpriority>
    public string Name
    {
      get
      {
        return this._Name;
      }
    }

    /// <summary>获取正在解析其依赖项的程序集。</summary>
    /// <returns>请求了 <see cref="P:System.ResolveEventArgs.Name" /> 属性指定的项的程序集。</returns>
    public Assembly RequestingAssembly
    {
      get
      {
        return this._RequestingAssembly;
      }
    }

    /// <summary>初始化 <see cref="T:System.ResolveEventArgs" /> 类的新实例，同时指定要解析的项的名称。</summary>
    /// <param name="name">要解析的项的名称。</param>
    public ResolveEventArgs(string name)
    {
      this._Name = name;
    }

    /// <summary>初始化 <see cref="T:System.ResolveEventArgs" /> 类的新实例，同时指定要解析的项的名称以及正在解析其依赖项的程序集。</summary>
    /// <param name="name">要解析的项的名称。</param>
    /// <param name="requestingAssembly">正在解析其依赖项的程序集。</param>
    public ResolveEventArgs(string name, Assembly requestingAssembly)
    {
      this._Name = name;
      this._RequestingAssembly = requestingAssembly;
    }
  }
}
