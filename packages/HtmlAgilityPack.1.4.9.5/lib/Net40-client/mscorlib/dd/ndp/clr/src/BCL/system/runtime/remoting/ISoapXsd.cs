// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Metadata.W3cXsd2001.ISoapXsd
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
  /// <summary>提供对 SOAP 类型的 XML 架构定义语言 (XSD) 的访问。</summary>
  [ComVisible(true)]
  public interface ISoapXsd
  {
    /// <summary>返回当前 SOAP 类型的 XML 架构定义语言 (XSD)。</summary>
    /// <returns>一个 <see cref="T:System.String" /> 指示当前 SOAP 类型的 XSD。</returns>
    string GetXsdType();
  }
}
