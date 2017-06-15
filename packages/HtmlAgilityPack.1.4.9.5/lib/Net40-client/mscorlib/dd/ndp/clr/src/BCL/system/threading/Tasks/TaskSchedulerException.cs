// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.TaskSchedulerException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Serialization;

namespace System.Threading.Tasks
{
  /// <summary>表示一个用于告知由 <see cref="T:System.Threading.Tasks.TaskScheduler" /> 计划的某个操作无效的异常。</summary>
  [__DynamicallyInvokable]
  [Serializable]
  public class TaskSchedulerException : Exception
  {
    /// <summary>使用由系统提供的用来描述错误的消息初始化 <see cref="T:System.Threading.Tasks.TaskSchedulerException" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public TaskSchedulerException()
      : base(Environment.GetResourceString("TaskSchedulerException_ctor_DefaultMessage"))
    {
    }

    /// <summary>使用指定的描述错误的消息初始化 <see cref="T:System.Threading.Tasks.TaskSchedulerException" /> 类的新实例。</summary>
    /// <param name="message">描述该异常的消息。此构造函数的调用方需要确保此字符串已针对当前系统区域性进行了本地化。</param>
    [__DynamicallyInvokable]
    public TaskSchedulerException(string message)
      : base(message)
    {
    }

    /// <summary>使用默认的错误消息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.Threading.Tasks.TaskSchedulerException" /> 类的新实例。</summary>
    /// <param name="innerException">导致当前异常的异常。</param>
    [__DynamicallyInvokable]
    public TaskSchedulerException(Exception innerException)
      : base(Environment.GetResourceString("TaskSchedulerException_ctor_DefaultMessage"), innerException)
    {
    }

    /// <summary>使用指定错误消息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.Threading.Tasks.TaskSchedulerException" /> 类的新实例。</summary>
    /// <param name="message">描述该异常的消息。此构造函数的调用方需要确保此字符串已针对当前系统区域性进行了本地化。</param>
    /// <param name="innerException">导致当前异常的异常。如果 <paramref name="innerException" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    [__DynamicallyInvokable]
    public TaskSchedulerException(string message, Exception innerException)
      : base(message, innerException)
    {
    }

    /// <summary>使用序列化数据初始化 <see cref="T:System.Threading.Tasks.TaskSchedulerException" /> 类的新实例。</summary>
    /// <param name="info">承载序列化对象数据的对象。</param>
    /// <param name="context">关于来源和目标的上下文信息</param>
    protected TaskSchedulerException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
