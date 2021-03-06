﻿// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtokens
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
  /// <summary>包装 XML NMTOKENS 属性。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class SoapNmtokens : ISoapXsd
  {
    private string _value;

    /// <summary>获取当前 SOAP 类型的 XML 架构定义语言 (XSD)。</summary>
    /// <returns>一个 <see cref="T:System.String" /> 指示当前 SOAP 类型的 XSD。</returns>
    public static string XsdType
    {
      get
      {
        return "NMTOKENS";
      }
    }

    /// <summary>获取或设置 XML NMTOKENS 属性。</summary>
    /// <returns>
    /// <see cref="T:System.String" />，包含 XML NMTOKENS 属性。</returns>
    public string Value
    {
      get
      {
        return this._value;
      }
      set
      {
        this._value = value;
      }
    }

    /// <summary>初始化 <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtokens" /> 类的新实例。</summary>
    public SoapNmtokens()
    {
    }

    /// <summary>用 XML NMTOKENS 属性初始化 <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtokens" /> 类的新实例。</summary>
    /// <param name="value">
    /// <see cref="T:System.String" />，包含 XML NMTOKENS 属性。</param>
    public SoapNmtokens(string value)
    {
      this._value = value;
    }

    /// <summary>返回当前 SOAP 类型的 XML 架构定义语言 (XSD)。</summary>
    /// <returns>一个 <see cref="T:System.String" /> 指示当前 SOAP 类型的 XSD。</returns>
    public string GetXsdType()
    {
      return SoapNmtokens.XsdType;
    }

    /// <summary>以 <see cref="T:System.String" /> 的形式返回 <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtokens.Value" />。</summary>
    /// <returns>从 <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtokens.Value" /> 中获取 <see cref="T:System.String" />。</returns>
    public override string ToString()
    {
      return SoapType.Escape(this._value);
    }

    /// <summary>将指定的 <see cref="T:System.String" /> 转换为 <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtokens" /> 对象。</summary>
    /// <returns>从 <paramref name="value" /> 获取的 <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtokens" /> 对象。</returns>
    /// <param name="value">要转换的 String。</param>
    public static SoapNmtokens Parse(string value)
    {
      return new SoapNmtokens(value);
    }
  }
}
