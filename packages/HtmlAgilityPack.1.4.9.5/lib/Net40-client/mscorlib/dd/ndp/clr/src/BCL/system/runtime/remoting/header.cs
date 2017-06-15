// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.Header
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
  /// <summary>定义调用的带外数据。</summary>
  [ComVisible(true)]
  [Serializable]
  public class Header
  {
    /// <summary>包含 <see cref="T:System.Runtime.Remoting.Messaging.Header" /> 的名称。</summary>
    public string Name;
    /// <summary>包含 <see cref="T:System.Runtime.Remoting.Messaging.Header" /> 的值。</summary>
    public object Value;
    /// <summary>指示接收端是否必须理解带外数据。</summary>
    public bool MustUnderstand;
    /// <summary>指示当前 <see cref="T:System.Runtime.Remoting.Messaging.Header" /> 所属的 XML 命名空间。</summary>
    public string HeaderNamespace;

    /// <summary>使用给定的名称和值初始化 <see cref="T:System.Runtime.Remoting.Messaging.Header" /> 类的新实例。</summary>
    /// <param name="_Name">
    /// <see cref="T:System.Runtime.Remoting.Messaging.Header" /> 的名称。</param>
    /// <param name="_Value">包含 <see cref="T:System.Runtime.Remoting.Messaging.Header" /> 的值的对象。</param>
    public Header(string _Name, object _Value)
      : this(_Name, _Value, true)
    {
    }

    /// <summary>使用给定的名称、值和其他配置信息初始化 <see cref="T:System.Runtime.Remoting.Messaging.Header" /> 类的新实例。</summary>
    /// <param name="_Name">
    /// <see cref="T:System.Runtime.Remoting.Messaging.Header" /> 的名称。</param>
    /// <param name="_Value">包含 <see cref="T:System.Runtime.Remoting.Messaging.Header" /> 的值的对象。</param>
    /// <param name="_MustUnderstand">指示接收端是否必须理解带外数据。</param>
    public Header(string _Name, object _Value, bool _MustUnderstand)
    {
      this.Name = _Name;
      this.Value = _Value;
      this.MustUnderstand = _MustUnderstand;
    }

    /// <summary>初始化 <see cref="T:System.Runtime.Remoting.Messaging.Header" /> 类的新实例。</summary>
    /// <param name="_Name">
    /// <see cref="T:System.Runtime.Remoting.Messaging.Header" /> 的名称。</param>
    /// <param name="_Value">包含 <see cref="T:System.Runtime.Remoting.Messaging.Header" /> 的值的对象。</param>
    /// <param name="_MustUnderstand">指示接收端是否必须理解带外数据。</param>
    /// <param name="_HeaderNamespace">
    /// <see cref="T:System.Runtime.Remoting.Messaging.Header" />XML 命名空间。</param>
    public Header(string _Name, object _Value, bool _MustUnderstand, string _HeaderNamespace)
    {
      this.Name = _Name;
      this.Value = _Value;
      this.MustUnderstand = _MustUnderstand;
      this.HeaderNamespace = _HeaderNamespace;
    }
  }
}
