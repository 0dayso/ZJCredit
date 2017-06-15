// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.CryptographicUnexpectedOperationException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security.Cryptography
{
  /// <summary>当加密操作中出现意外操作时引发的异常。</summary>
  [ComVisible(true)]
  [Serializable]
  public class CryptographicUnexpectedOperationException : CryptographicException
  {
    /// <summary>使用默认属性初始化 <see cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException" /> 类的新实例。</summary>
    public CryptographicUnexpectedOperationException()
    {
      this.SetErrorCode(-2146233295);
    }

    /// <summary>用指定的错误消息初始化 <see cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    public CryptographicUnexpectedOperationException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233295);
    }

    /// <summary>以指定格式使用指定的错误消息来初始化 <see cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException" /> 类的新实例。</summary>
    /// <param name="format">用于输出错误信息的格式。</param>
    /// <param name="insert">解释异常原因的错误信息。</param>
    public CryptographicUnexpectedOperationException(string format, string insert)
      : base(string.Format((IFormatProvider) CultureInfo.CurrentCulture, format, (object) insert))
    {
      this.SetErrorCode(-2146233295);
    }

    /// <summary>使用指定错误消息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="inner">导致当前异常的异常。如果 <paramref name="inner" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    public CryptographicUnexpectedOperationException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2146233295);
    }

    /// <summary>用序列化数据初始化 <see cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException" /> 类的新实例。</summary>
    /// <param name="info">承载序列化对象数据的对象。</param>
    /// <param name="context">关于来源和目标的上下文信息</param>
    protected CryptographicUnexpectedOperationException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
