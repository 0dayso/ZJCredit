// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Metadata.SoapMethodAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Metadata
{
  /// <summary>自定义方法的 SOAP 生成和处理。此类不能被继承。</summary>
  [AttributeUsage(AttributeTargets.Method)]
  [ComVisible(true)]
  public sealed class SoapMethodAttribute : SoapAttribute
  {
    private string _SoapAction;
    private string _responseXmlElementName;
    private string _responseXmlNamespace;
    private string _returnXmlElementName;
    private bool _bSoapActionExplicitySet;

    internal bool SoapActionExplicitySet
    {
      get
      {
        return this._bSoapActionExplicitySet;
      }
    }

    /// <summary>获取或设置与用此方法发送的 HTTP 请求一起使用的 SOAPAction 标头字段。目前未实现此属性。</summary>
    /// <returns>与用此方法发送的 HTTP 请求一起使用的 SOAPAction 标头字段。</returns>
    public string SoapAction
    {
      [SecuritySafeCritical] get
      {
        if (this._SoapAction == null)
          this._SoapAction = this.XmlTypeNamespaceOfDeclaringType + "#" + ((MemberInfo) this.ReflectInfo).Name;
        return this._SoapAction;
      }
      set
      {
        this._SoapAction = value;
        this._bSoapActionExplicitySet = true;
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

    /// <summary>获取或设置在序列化目标方法的远程方法调用期间使用的 XML 命名空间。</summary>
    /// <returns>在序列化目标方法的远程方法调用期间使用的 XML 命名空间。</returns>
    public override string XmlNamespace
    {
      [SecuritySafeCritical] get
      {
        if (this.ProtXmlNamespace == null)
          this.ProtXmlNamespace = this.XmlTypeNamespaceOfDeclaringType;
        return this.ProtXmlNamespace;
      }
      set
      {
        this.ProtXmlNamespace = value;
      }
    }

    /// <summary>获取或设置用于对目标方法的方法响应的 XML 元素名称。</summary>
    /// <returns>用于对目标方法的方法响应的 XML 元素名称。</returns>
    public string ResponseXmlElementName
    {
      get
      {
        if (this._responseXmlElementName == null && this.ReflectInfo != null)
          this._responseXmlElementName = ((MemberInfo) this.ReflectInfo).Name + "Response";
        return this._responseXmlElementName;
      }
      set
      {
        this._responseXmlElementName = value;
      }
    }

    /// <summary>获取或设置用于对目标方法的方法响应的 XML 元素命名空间。</summary>
    /// <returns>用于对目标方法的方法响应的 XML 元素命名空间。</returns>
    public string ResponseXmlNamespace
    {
      get
      {
        if (this._responseXmlNamespace == null)
          this._responseXmlNamespace = this.XmlNamespace;
        return this._responseXmlNamespace;
      }
      set
      {
        this._responseXmlNamespace = value;
      }
    }

    /// <summary>获取或设置用于来自目标方法的返回值的 XML 元素名称。</summary>
    /// <returns>用于来自目标方法的返回值的 XML 元素名称。</returns>
    public string ReturnXmlElementName
    {
      get
      {
        if (this._returnXmlElementName == null)
          this._returnXmlElementName = "return";
        return this._returnXmlElementName;
      }
      set
      {
        this._returnXmlElementName = value;
      }
    }

    private string XmlTypeNamespaceOfDeclaringType
    {
      [SecurityCritical] get
      {
        if (this.ReflectInfo != null)
          return XmlNamespaceEncoder.GetXmlNamespaceForType((RuntimeType) ((MemberInfo) this.ReflectInfo).DeclaringType, (string) null);
        return (string) null;
      }
    }
  }
}
