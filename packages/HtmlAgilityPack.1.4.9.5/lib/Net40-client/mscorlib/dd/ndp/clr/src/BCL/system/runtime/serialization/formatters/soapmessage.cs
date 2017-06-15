// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.SoapMessage
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Serialization.Formatters
{
  /// <summary>包含在 SOAP RPC（远程过程调用）的序列化过程中所需的参数的名称和类型。</summary>
  [ComVisible(true)]
  [Serializable]
  public class SoapMessage : ISoapMessage
  {
    internal string[] paramNames;
    internal object[] paramValues;
    internal Type[] paramTypes;
    internal string methodName;
    internal string xmlNameSpace;
    internal Header[] headers;

    /// <summary>获取或设置所调用方法的参数名称。</summary>
    /// <returns>所调用方法的参数名称。</returns>
    public string[] ParamNames
    {
      get
      {
        return this.paramNames;
      }
      set
      {
        this.paramNames = value;
      }
    }

    /// <summary>获取或设置所调用方法的参数值。</summary>
    /// <returns>所调用方法的参数值。</returns>
    public object[] ParamValues
    {
      get
      {
        return this.paramValues;
      }
      set
      {
        this.paramValues = value;
      }
    }

    /// <summary>此属性被保留。请改用 <see cref="P:System.Runtime.Serialization.Formatters.SoapMessage.ParamNames" /> 和/或 <see cref="P:System.Runtime.Serialization.Formatters.SoapMessage.ParamValues" /> 属性。</summary>
    /// <returns>所调用方法的参数类型。</returns>
    public Type[] ParamTypes
    {
      get
      {
        return this.paramTypes;
      }
      set
      {
        this.paramTypes = value;
      }
    }

    /// <summary>获取或设置调用的方法的名称。</summary>
    /// <returns>调用的方法的名称。</returns>
    public string MethodName
    {
      get
      {
        return this.methodName;
      }
      set
      {
        this.methodName = value;
      }
    }

    /// <summary>获取或设置包含所调用方法的对象所在的 XML 命名空间名称。</summary>
    /// <returns>包含所调用方法的对象所在的 XML 命名空间名称。</returns>
    public string XmlNameSpace
    {
      get
      {
        return this.xmlNameSpace;
      }
      set
      {
        this.xmlNameSpace = value;
      }
    }

    /// <summary>获取或设置所调用方法的带外数据。</summary>
    /// <returns>所调用方法的带外数据。</returns>
    public Header[] Headers
    {
      get
      {
        return this.headers;
      }
      set
      {
        this.headers = value;
      }
    }
  }
}
