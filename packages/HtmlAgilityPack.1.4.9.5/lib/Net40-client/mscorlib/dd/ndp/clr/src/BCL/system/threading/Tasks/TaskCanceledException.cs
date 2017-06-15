// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.TaskCanceledException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Serialization;

namespace System.Threading.Tasks
{
  /// <summary>表示一个用于告知任务取消的异常。</summary>
  [__DynamicallyInvokable]
  [Serializable]
  public class TaskCanceledException : OperationCanceledException
  {
    [NonSerialized]
    private Task m_canceledTask;

    /// <summary>获取与此异常关联的任务。</summary>
    /// <returns>对与此异常关联的 <see cref="T:System.Threading.Tasks.Task" /> 的引用。</returns>
    [__DynamicallyInvokable]
    public Task Task
    {
      [__DynamicallyInvokable] get
      {
        return this.m_canceledTask;
      }
    }

    /// <summary>使用由系统提供的用来描述错误的消息初始化 <see cref="T:System.Threading.Tasks.TaskCanceledException" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public TaskCanceledException()
      : base(Environment.GetResourceString("TaskCanceledException_ctor_DefaultMessage"))
    {
    }

    /// <summary>使用指定的描述错误的消息初始化 <see cref="T:System.Threading.Tasks.TaskCanceledException" /> 类的新实例。</summary>
    /// <param name="message">描述该异常的消息。此构造函数的调用方需要确保此字符串已针对当前系统区域性进行了本地化。</param>
    [__DynamicallyInvokable]
    public TaskCanceledException(string message)
      : base(message)
    {
    }

    /// <summary>使用指定错误消息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.Threading.Tasks.TaskCanceledException" /> 类的新实例。</summary>
    /// <param name="message">描述该异常的消息。此构造函数的调用方需要确保此字符串已针对当前系统区域性进行了本地化。</param>
    /// <param name="innerException">导致当前异常的异常。如果 <paramref name="innerException" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    [__DynamicallyInvokable]
    public TaskCanceledException(string message, Exception innerException)
      : base(message, innerException)
    {
    }

    /// <summary>使用对已取消的 <see cref="T:System.Threading.Tasks.Task" /> 的引用初始化 <see cref="T:System.Threading.Tasks.TaskCanceledException" /> 类的新实例。</summary>
    /// <param name="task">已取消的任务。</param>
    [__DynamicallyInvokable]
    public TaskCanceledException(Task task)
      : base(Environment.GetResourceString("TaskCanceledException_ctor_DefaultMessage"), task != null ? task.CancellationToken : new CancellationToken())
    {
      this.m_canceledTask = task;
    }

    /// <summary>使用序列化数据初始化 <see cref="T:System.Threading.Tasks.TaskCanceledException" /> 类的新实例。</summary>
    /// <param name="info">承载序列化对象数据的对象。</param>
    /// <param name="context">关于来源和目标的上下文信息</param>
    protected TaskCanceledException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
