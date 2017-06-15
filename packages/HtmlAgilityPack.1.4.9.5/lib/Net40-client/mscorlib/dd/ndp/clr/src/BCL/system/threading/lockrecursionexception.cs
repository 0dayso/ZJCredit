// Decompiled with JetBrains decompiler
// Type: System.Threading.LockRecursionException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Threading
{
  /// <summary>当进入锁定状态的递归与此锁定的递归策略不兼容时引发的异常。</summary>
  /// <filterpriority>2</filterpriority>
  [TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=b77a5c561934e089")]
  [__DynamicallyInvokable]
  [Serializable]
  [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
  public class LockRecursionException : Exception
  {
    /// <summary>使用由系统提供的用来描述错误的消息初始化 <see cref="T:System.Threading.LockRecursionException" /> 类的新实例。</summary>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public LockRecursionException()
    {
    }

    /// <summary>使用指定的描述错误的消息初始化 <see cref="T:System.Threading.LockRecursionException" /> 类的新实例。</summary>
    /// <param name="message">描述该异常的消息。此构造函数的调用方必须确保此字符串已针对当前系统区域性进行了本地化。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public LockRecursionException(string message)
      : base(message)
    {
    }

    /// <summary>用序列化数据初始化 <see cref="T:System.Threading.LockRecursionException" /> 类的新实例。</summary>
    /// <param name="info">承载序列化对象数据的对象。</param>
    /// <param name="context">关于来源和目标的上下文信息</param>
    protected LockRecursionException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    /// <summary>使用指定错误消息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.Threading.LockRecursionException" /> 类的新实例。</summary>
    /// <param name="message">描述该异常的消息。此构造函数的调用方必须确保此字符串已针对当前系统区域性进行了本地化。</param>
    /// <param name="innerException">引发当前异常的异常。如果 <paramref name="innerException" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public LockRecursionException(string message, Exception innerException)
      : base(message, innerException)
    {
    }
  }
}
