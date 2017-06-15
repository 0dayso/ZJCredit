// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.TypeEntry
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Remoting
{
  /// <summary>实现保存用来激活远程类型实例的配置信息的基类。</summary>
  [ComVisible(true)]
  public class TypeEntry
  {
    private string _typeName;
    private string _assemblyName;
    private RemoteAppEntry _cachedRemoteAppEntry;

    /// <summary>获取配置为远程激活类型的对象类型的完整类型名。</summary>
    /// <returns>配置为远程激活类型的对象类型的完整类型名。</returns>
    public string TypeName
    {
      get
      {
        return this._typeName;
      }
      set
      {
        this._typeName = value;
      }
    }

    /// <summary>获取配置为远程激活类型的对象类型的程序集名称。</summary>
    /// <returns>配置为远程激活类型的对象类型的程序集名称。</returns>
    public string AssemblyName
    {
      get
      {
        return this._assemblyName;
      }
      set
      {
        this._assemblyName = value;
      }
    }

    /// <summary>初始化 <see cref="T:System.Runtime.Remoting.TypeEntry" /> 类的新实例。</summary>
    protected TypeEntry()
    {
    }

    internal void CacheRemoteAppEntry(RemoteAppEntry entry)
    {
      this._cachedRemoteAppEntry = entry;
    }

    internal RemoteAppEntry GetRemoteAppEntry()
    {
      return this._cachedRemoteAppEntry;
    }
  }
}
