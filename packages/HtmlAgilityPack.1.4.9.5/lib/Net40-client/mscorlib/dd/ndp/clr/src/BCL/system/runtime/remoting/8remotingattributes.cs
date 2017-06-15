// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Metadata.SoapAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata
{
  /// <summary>提供所有 SOAP 特性的默认功能。</summary>
  [ComVisible(true)]
  public class SoapAttribute : Attribute
  {
    /// <summary>在其下序列化当前 SOAP 特性的目标的 XML 命名空间的名称。</summary>
    protected string ProtXmlNamespace;
    private bool _bUseAttribute;
    private bool _bEmbedded;
    /// <summary>一个反射对象，从 <see cref="T:System.Runtime.Remoting.Metadata.SoapAttribute" /> 类派生的特性类使用该对象设置 XML 序列化信息。</summary>
    protected object ReflectInfo;

    /// <summary>获取或设置 XML 命名空间的名称。</summary>
    /// <returns>在其下序列化当前特性的目标的 XML 命名空间的名称。</returns>
    public virtual string XmlNamespace
    {
      get
      {
        return this.ProtXmlNamespace;
      }
      set
      {
        this.ProtXmlNamespace = value;
      }
    }

    /// <summary>获取或设置一个值，该值指示当前特性的目标是否序列化为 XML 特性而非 XML 字段。</summary>
    /// <returns>如果当前特性的目标对象必须序列化为 XML 特性，则为 true；如果目标对象必须序列化为子元素，则为 false。</returns>
    public virtual bool UseAttribute
    {
      get
      {
        return this._bUseAttribute;
      }
      set
      {
        this._bUseAttribute = value;
      }
    }

    /// <summary>获取或设置一个值，该值指示是否必须在 SOAP 序列化期间嵌套该类型。</summary>
    /// <returns>如果必须在 SOAP 序列化期间嵌套该目标对象，则为 true；否则为 false。</returns>
    public virtual bool Embedded
    {
      get
      {
        return this._bEmbedded;
      }
      set
      {
        this._bEmbedded = value;
      }
    }

    internal void SetReflectInfo(object info)
    {
      this.ReflectInfo = info;
    }
  }
}
