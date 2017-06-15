// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
  /// <summary>包装 XSD QName 类型。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class SoapQName : ISoapXsd
  {
    private string _name;
    private string _namespace;
    private string _key;

    /// <summary>获取当前 SOAP 类型的 XML 架构定义语言 (XSD)。</summary>
    /// <returns>一个 <see cref="T:System.String" /> 指示当前 SOAP 类型的 XSD。</returns>
    public static string XsdType
    {
      get
      {
        return "QName";
      }
    }

    /// <summary>获取或设置限定名的名称部分。</summary>
    /// <returns>一个 <see cref="T:System.String" /> 包含限定名的名称部分。</returns>
    public string Name
    {
      get
      {
        return this._name;
      }
      set
      {
        this._name = value;
      }
    }

    /// <summary>获取或设置由 <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName.Key" /> 引用的命名空间。</summary>
    /// <returns>
    /// <see cref="T:System.String" />，包含 <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName.Key" /> 引用的命名空间。</returns>
    public string Namespace
    {
      get
      {
        return this._namespace;
      }
      set
      {
        this._namespace = value;
      }
    }

    /// <summary>获取或设置限定名的命名空间别名。</summary>
    /// <returns>一个 <see cref="T:System.String" /> 包含限定名的命名空间别名。</returns>
    public string Key
    {
      get
      {
        return this._key;
      }
      set
      {
        this._key = value;
      }
    }

    /// <summary>初始化 <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName" /> 类的新实例。</summary>
    public SoapQName()
    {
    }

    /// <summary>用限定名的本地部分初始化 <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName" /> 类的新实例。</summary>
    /// <param name="value">一个 <see cref="T:System.String" /> 包含限定名的本地部分。</param>
    public SoapQName(string value)
    {
      this._name = value;
    }

    /// <summary>用限定名的命名空间别名和本地部分初始化 <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName" /> 类的新实例。</summary>
    /// <param name="key">一个 <see cref="T:System.String" /> 包含限定名的命名空间别名。</param>
    /// <param name="name">一个 <see cref="T:System.String" /> 包含限定名的本地部分。</param>
    public SoapQName(string key, string name)
    {
      this._name = name;
      this._key = key;
    }

    /// <summary>用限定名的命名空间别名、本地部分以及由该别名引用的命名空间来初始化 <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName" /> 类的新实例。</summary>
    /// <param name="key">一个 <see cref="T:System.String" /> 包含限定名的命名空间别名。</param>
    /// <param name="name">一个 <see cref="T:System.String" /> 包含限定名的本地部分。</param>
    /// <param name="namespaceValue">
    /// <see cref="T:System.String" />，包含 <paramref name="key" /> 引用的命名空间。</param>
    public SoapQName(string key, string name, string namespaceValue)
    {
      this._name = name;
      this._namespace = namespaceValue;
      this._key = key;
    }

    /// <summary>返回当前 SOAP 类型的 XML 架构定义语言 (XSD)。</summary>
    /// <returns>
    /// <see cref="T:System.String" />，指示当前 SOAP 类型的 XSD。</returns>
    public string GetXsdType()
    {
      return SoapQName.XsdType;
    }

    /// <summary>以 <see cref="T:System.String" /> 的形式返回限定名。</summary>
    /// <returns>“<see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName.Key" /> : <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName.Name" />”格式的 <see cref="T:System.String" />。如果未指定 <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName.Key" />，此方法将返回 <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName.Name" />。</returns>
    public override string ToString()
    {
      if (this._key == null || this._key.Length == 0)
        return this._name;
      return this._key + ":" + this._name;
    }

    /// <summary>将指定的 <see cref="T:System.String" /> 转换为 <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName" /> 对象。</summary>
    /// <returns>从 <paramref name="value" /> 获取的 <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName" /> 对象。</returns>
    /// <param name="value">要转换的 <see cref="T:System.String" />。</param>
    public static SoapQName Parse(string value)
    {
      if (value == null)
        return new SoapQName();
      string key = "";
      string name = value;
      int length = value.IndexOf(':');
      if (length > 0)
      {
        key = value.Substring(0, length);
        name = value.Substring(length + 1);
      }
      return new SoapQName(key, name);
    }
  }
}
