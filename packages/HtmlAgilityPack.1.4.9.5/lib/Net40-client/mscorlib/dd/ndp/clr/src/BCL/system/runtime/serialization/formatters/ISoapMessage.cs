// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.ISoapMessage
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Serialization.Formatters
{
  /// <summary>为特定对象提供接口，它包含在 SOAP RPC（远程过程调用）的序列化过程中所需的参数的名称和类型。</summary>
  [ComVisible(true)]
  public interface ISoapMessage
  {
    /// <summary>获取或设置方法调用的参数名称。</summary>
    /// <returns>方法调用的参数名称。</returns>
    string[] ParamNames { get; set; }

    /// <summary>获取或设置方法调用的参数值。</summary>
    /// <returns>方法调用的参数值。</returns>
    object[] ParamValues { get; set; }

    /// <summary>获取或设置方法调用的参数类型。</summary>
    /// <returns>方法调用的参数类型。</returns>
    Type[] ParamTypes { get; set; }

    /// <summary>获取或设置调用的方法的名称。</summary>
    /// <returns>调用的方法的名称。</returns>
    string MethodName { get; set; }

    /// <summary>获取或设置 SOAP RPC（远程过程调用）<see cref="P:System.Runtime.Serialization.Formatters.ISoapMessage.MethodName" /> 元素的 XML 命名空间。</summary>
    /// <returns>包含所调用方法的对象所在的 XML 命名空间名称。</returns>
    string XmlNameSpace { get; set; }

    /// <summary>获取或设置方法调用的带外数据。</summary>
    /// <returns>方法调用的带外数据。</returns>
    Header[] Headers { get; set; }
  }
}
