// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Metadata.SoapTypeAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Metadata
{
  /// <summary>自定义目标类型的 SOAP 生成和处理。此类不能被继承。</summary>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface)]
  [ComVisible(true)]
  public sealed class SoapTypeAttribute : SoapAttribute
  {
    private SoapTypeAttribute.ExplicitlySet _explicitlySet;
    private SoapOption _SoapOptions;
    private string _XmlElementName;
    private string _XmlTypeName;
    private string _XmlTypeNamespace;
    private XmlFieldOrderOption _XmlFieldOrder;

    /// <summary>获取或设置 <see cref="T:System.Runtime.Remoting.Metadata.SoapOption" /> 配置值。</summary>
    /// <returns>
    /// <see cref="T:System.Runtime.Remoting.Metadata.SoapOption" /> 配置值。</returns>
    public SoapOption SoapOptions
    {
      get
      {
        return this._SoapOptions;
      }
      set
      {
        this._SoapOptions = value;
      }
    }

    /// <summary>获取或设置 XML 元素名。</summary>
    /// <returns>XML 元素名称。</returns>
    public string XmlElementName
    {
      get
      {
        if (this._XmlElementName == null && this.ReflectInfo != null)
          this._XmlElementName = SoapTypeAttribute.GetTypeName((Type) this.ReflectInfo);
        return this._XmlElementName;
      }
      set
      {
        this._XmlElementName = value;
        this._explicitlySet = this._explicitlySet | SoapTypeAttribute.ExplicitlySet.XmlElementName;
      }
    }

    /// <summary>获取或设置在序列化目标对象类型期间使用的 XML 命名空间。</summary>
    /// <returns>在序列化目标对象类型期间使用的 XML 命名空间。</returns>
    public override string XmlNamespace
    {
      get
      {
        if (this.ProtXmlNamespace == null && this.ReflectInfo != null)
          this.ProtXmlNamespace = this.XmlTypeNamespace;
        return this.ProtXmlNamespace;
      }
      set
      {
        this.ProtXmlNamespace = value;
        this._explicitlySet = this._explicitlySet | SoapTypeAttribute.ExplicitlySet.XmlNamespace;
      }
    }

    /// <summary>获取或设置目标对象类型的 XML 类型名。</summary>
    /// <returns>目标对象类型的 XML 类型名。</returns>
    public string XmlTypeName
    {
      get
      {
        if (this._XmlTypeName == null && this.ReflectInfo != null)
          this._XmlTypeName = SoapTypeAttribute.GetTypeName((Type) this.ReflectInfo);
        return this._XmlTypeName;
      }
      set
      {
        this._XmlTypeName = value;
        this._explicitlySet = this._explicitlySet | SoapTypeAttribute.ExplicitlySet.XmlTypeName;
      }
    }

    /// <summary>获取或设置当前对象类型的 XML 类型命名空间。</summary>
    /// <returns>当前对象类型的 XML 类型命名空间。</returns>
    public string XmlTypeNamespace
    {
      [SecuritySafeCritical] get
      {
        if (this._XmlTypeNamespace == null && this.ReflectInfo != null)
          this._XmlTypeNamespace = XmlNamespaceEncoder.GetXmlNamespaceForTypeNamespace((RuntimeType) this.ReflectInfo, (string) null);
        return this._XmlTypeNamespace;
      }
      set
      {
        this._XmlTypeNamespace = value;
        this._explicitlySet = this._explicitlySet | SoapTypeAttribute.ExplicitlySet.XmlTypeNamespace;
      }
    }

    /// <summary>获取或设置目标对象类型的 XML 字段顺序。</summary>
    /// <returns>目标对象类型的 XML 字段顺序。</returns>
    public XmlFieldOrderOption XmlFieldOrder
    {
      get
      {
        return this._XmlFieldOrder;
      }
      set
      {
        this._XmlFieldOrder = value;
      }
    }

    /// <summary>获取或设置一个值，该值指示当前特性的目标是否序列化为 XML 特性而非 XML 字段。</summary>
    /// <returns>当前的实现总是返回 false。</returns>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">尝试设置当前属性。</exception>
    public override bool UseAttribute
    {
      get
      {
        return false;
      }
      set
      {
        throw new RemotingException(Environment.GetResourceString("Remoting_Attribute_UseAttributeNotsettable"));
      }
    }

    internal bool IsInteropXmlElement()
    {
      return (uint) (this._explicitlySet & (SoapTypeAttribute.ExplicitlySet.XmlElementName | SoapTypeAttribute.ExplicitlySet.XmlNamespace)) > 0U;
    }

    internal bool IsInteropXmlType()
    {
      return (uint) (this._explicitlySet & (SoapTypeAttribute.ExplicitlySet.XmlTypeName | SoapTypeAttribute.ExplicitlySet.XmlTypeNamespace)) > 0U;
    }

    private static string GetTypeName(Type t)
    {
      if (!t.IsNested)
        return t.Name;
      string fullName = t.FullName;
      string @namespace = t.Namespace;
      if (@namespace == null || @namespace.Length == 0)
        return fullName;
      return fullName.Substring(@namespace.Length + 1);
    }

    [Flags]
    [Serializable]
    private enum ExplicitlySet
    {
      None = 0,
      XmlElementName = 1,
      XmlNamespace = 2,
      XmlTypeName = 4,
      XmlTypeNamespace = 8,
    }
  }
}
