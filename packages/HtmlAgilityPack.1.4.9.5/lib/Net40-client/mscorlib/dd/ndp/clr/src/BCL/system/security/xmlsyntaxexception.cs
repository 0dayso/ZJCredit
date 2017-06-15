// Decompiled with JetBrains decompiler
// Type: System.Security.XmlSyntaxException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security
{
  /// <summary>在 XML 语法分析中出现语法错误时引发的异常。此类不能被继承。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class XmlSyntaxException : SystemException
  {
    /// <summary>使用默认属性初始化 <see cref="T:System.Security.XmlSyntaxException" /> 类的新实例。</summary>
    public XmlSyntaxException()
      : base(Environment.GetResourceString("XMLSyntax_InvalidSyntax"))
    {
      this.SetErrorCode(-2146233320);
    }

    /// <summary>使用指定的错误消息初始化 <see cref="T:System.Security.XmlSyntaxException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    public XmlSyntaxException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233320);
    }

    /// <summary>使用指定错误消息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.Security.XmlSyntaxException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="inner">导致当前异常的异常。如果 <paramref name="inner" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    public XmlSyntaxException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2146233320);
    }

    /// <summary>用检测到异常时的行号初始化 <see cref="T:System.Security.XmlSyntaxException" /> 类的新实例。</summary>
    /// <param name="lineNumber">XML 流的行号，在此行中检测到 XML 语法错误。</param>
    public XmlSyntaxException(int lineNumber)
      : base(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("XMLSyntax_SyntaxError"), (object) lineNumber))
    {
      this.SetErrorCode(-2146233320);
    }

    /// <summary>用指定的错误信息和检测到异常时的行号初始化 <see cref="T:System.Security.XmlSyntaxException" /> 类的新实例。</summary>
    /// <param name="lineNumber">XML 流的行号，在此行中检测到 XML 语法错误。</param>
    /// <param name="message">解释异常原因的错误信息。</param>
    public XmlSyntaxException(int lineNumber, string message)
      : base(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("XMLSyntax_SyntaxErrorEx"), (object) lineNumber, (object) message))
    {
      this.SetErrorCode(-2146233320);
    }

    internal XmlSyntaxException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
