// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.SinkProviderData
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
  /// <summary>为接收器提供程序存储接收器提供程序数据。</summary>
  [ComVisible(true)]
  public class SinkProviderData
  {
    private Hashtable _properties = new Hashtable((IEqualityComparer) StringComparer.InvariantCultureIgnoreCase);
    private ArrayList _children = new ArrayList();
    private string _name;

    /// <summary>获取与当前 <see cref="T:System.Runtime.Remoting.Channels.SinkProviderData" /> 对象中的数据关联的接收器提供程序的名称。</summary>
    /// <returns>具有与当前 <see cref="T:System.Runtime.Remoting.Channels.SinkProviderData" /> 对象中的数据关联的 XML 节点名称的 <see cref="T:System.String" />。</returns>
    public string Name
    {
      get
      {
        return this._name;
      }
    }

    /// <summary>获取可以通过其访问接收器提供程序上属性的字典。</summary>
    /// <returns>可以通过其访问接收器提供程序上属性的字典。</returns>
    public IDictionary Properties
    {
      get
      {
        return (IDictionary) this._properties;
      }
    }

    /// <summary>获取一个子 <see cref="T:System.Runtime.Remoting.Channels.SinkProviderData" /> 节点的列表。</summary>
    /// <returns>子 <see cref="T:System.Runtime.Remoting.Channels.SinkProviderData" /> 节点的 <see cref="T:System.Collections.IList" />。</returns>
    public IList Children
    {
      get
      {
        return (IList) this._children;
      }
    }

    /// <summary>初始化 <see cref="T:System.Runtime.Remoting.Channels.SinkProviderData" /> 类的新实例。</summary>
    /// <param name="name">与当前 <see cref="T:System.Runtime.Remoting.Channels.SinkProviderData" /> 对象中的数据关联的接收器提供程序的名称。</param>
    public SinkProviderData(string name)
    {
      this._name = name;
    }
  }
}
