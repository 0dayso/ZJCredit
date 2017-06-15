// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Metadata.SoapFieldAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata
{
  /// <summary>自定义字段的 SOAP 生成和处理。此类不能被继承。</summary>
  [AttributeUsage(AttributeTargets.Field)]
  [ComVisible(true)]
  public sealed class SoapFieldAttribute : SoapAttribute
  {
    private SoapFieldAttribute.ExplicitlySet _explicitlySet;
    private string _xmlElementName;
    private int _order;

    /// <summary>获取或设置包含在 <see cref="T:System.Runtime.Remoting.Metadata.SoapFieldAttribute" /> 特性中的字段的 XML 元素名称。</summary>
    /// <returns>包含在此特性中的字段的 XML 元素名称。</returns>
    public string XmlElementName
    {
      get
      {
        if (this._xmlElementName == null && this.ReflectInfo != null)
          this._xmlElementName = ((MemberInfo) this.ReflectInfo).Name;
        return this._xmlElementName;
      }
      set
      {
        this._xmlElementName = value;
        this._explicitlySet = this._explicitlySet | SoapFieldAttribute.ExplicitlySet.XmlElementName;
      }
    }

    /// <summary>获取或设置当前字段属性的顺序。</summary>
    /// <returns>当前字段特性的顺序。</returns>
    public int Order
    {
      get
      {
        return this._order;
      }
      set
      {
        this._order = value;
      }
    }

    /// <summary>返回一个值，该值指示当前特性是否包含 Interop XML 元素值。</summary>
    /// <returns>如果当前特性包含 Interop XML 元素值，则为 true；否则为 false。</returns>
    public bool IsInteropXmlElement()
    {
      return (uint) (this._explicitlySet & SoapFieldAttribute.ExplicitlySet.XmlElementName) > 0U;
    }

    [Flags]
    [Serializable]
    private enum ExplicitlySet
    {
      None = 0,
      XmlElementName = 1,
    }
  }
}
