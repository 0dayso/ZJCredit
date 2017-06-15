// Decompiled with JetBrains decompiler
// Type: System.InvalidTimeZoneException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System
{
  /// <summary>时区信息无效时引发的异常。</summary>
  [TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=b77a5c561934e089")]
  [__DynamicallyInvokable]
  [Serializable]
  [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
  public class InvalidTimeZoneException : Exception
  {
    /// <summary>使用指定的消息字符串初始化 <see cref="T:System.InvalidTimeZoneException" /> 类的新实例。</summary>
    /// <param name="message">描述异常的字符串。</param>
    [__DynamicallyInvokable]
    public InvalidTimeZoneException(string message)
      : base(message)
    {
    }

    /// <summary>使用指定的错误消息和对导致此异常的内部异常的引用来初始化 <see cref="T:System.InvalidTimeZoneException" /> 类的新实例。</summary>
    /// <param name="message">描述异常的字符串。</param>
    /// <param name="innerException">导致当前异常的异常。</param>
    [__DynamicallyInvokable]
    public InvalidTimeZoneException(string message, Exception innerException)
      : base(message, innerException)
    {
    }

    /// <summary>用序列化数据初始化 <see cref="T:System.InvalidTimeZoneException" /> 类的新实例。</summary>
    /// <param name="info">包含序列化数据的对象。</param>
    /// <param name="context">包含序列化数据的流。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="info" /> 参数为 null。- 或 -<paramref name="context" /> 参数为 null。</exception>
    protected InvalidTimeZoneException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    /// <summary>使用系统提供的消息初始化 <see cref="T:System.InvalidTimeZoneException" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public InvalidTimeZoneException()
    {
    }
  }
}
