// Decompiled with JetBrains decompiler
// Type: System.OperationCanceledException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Threading;

namespace System
{
  /// <summary>取消线程正在执行的操作时在线程中引发的异常。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class OperationCanceledException : SystemException
  {
    [NonSerialized]
    private CancellationToken _cancellationToken;

    /// <summary>获取与已取消的操作关联的令牌。</summary>
    /// <returns>与已取消的操作关联的令牌，或默认令牌。</returns>
    [__DynamicallyInvokable]
    public CancellationToken CancellationToken
    {
      [__DynamicallyInvokable] get
      {
        return this._cancellationToken;
      }
      private set
      {
        this._cancellationToken = value;
      }
    }

    /// <summary>使用系统提供的错误信息初始化 <see cref="T:System.OperationCanceledException" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public OperationCanceledException()
      : base(Environment.GetResourceString("OperationCanceled"))
    {
      this.SetErrorCode(-2146233029);
    }

    /// <summary>使用指定的错误信息初始化 <see cref="T:System.OperationCanceledException" /> 类的新实例。</summary>
    /// <param name="message">描述该错误的 <see cref="T:System.String" />。</param>
    [__DynamicallyInvokable]
    public OperationCanceledException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233029);
    }

    /// <summary>使用指定错误消息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.OperationCanceledException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="innerException">导致当前异常的异常。如果 <paramref name="innerException" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    [__DynamicallyInvokable]
    public OperationCanceledException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2146233029);
    }

    /// <summary>用取消令牌初始化 <see cref="T:System.OperationCanceledException" /> 类的新实例。</summary>
    /// <param name="token">一个与已取消的操作关联的取消标记。</param>
    [__DynamicallyInvokable]
    public OperationCanceledException(CancellationToken token)
      : this()
    {
      this.CancellationToken = token;
    }

    /// <summary>使用指定的错误信息和取消令牌初始化 <see cref="T:System.OperationCanceledException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="token">一个与已取消的操作关联的取消标记。</param>
    [__DynamicallyInvokable]
    public OperationCanceledException(string message, CancellationToken token)
      : this(message)
    {
      this.CancellationToken = token;
    }

    /// <summary>用指定的错误消息、对作为此异常原因的内部异常的引用以及取消令牌初始化 <see cref="T:System.OperationCanceledException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="innerException">导致当前异常的异常。如果 <paramref name="innerException" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    /// <param name="token">一个与已取消的操作关联的取消标记。</param>
    [__DynamicallyInvokable]
    public OperationCanceledException(string message, Exception innerException, CancellationToken token)
      : this(message, innerException)
    {
      this.CancellationToken = token;
    }

    /// <summary>用序列化数据初始化 <see cref="T:System.OperationCanceledException" /> 类的新实例。</summary>
    /// <param name="info">承载序列化对象数据的对象。</param>
    /// <param name="context">关于来源和目标的上下文信息</param>
    protected OperationCanceledException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
