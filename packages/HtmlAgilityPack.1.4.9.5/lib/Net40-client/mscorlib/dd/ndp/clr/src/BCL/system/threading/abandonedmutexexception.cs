// Decompiled with JetBrains decompiler
// Type: System.Threading.AbandonedMutexException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Threading
{
  /// <summary>当某个线程获取由另一个线程放弃（即在未释放的情况下退出）的 <see cref="T:System.Threading.Mutex" /> 对象时引发的异常。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(false)]
  [__DynamicallyInvokable]
  [Serializable]
  public class AbandonedMutexException : SystemException
  {
    private int m_MutexIndex = -1;
    private Mutex m_Mutex;

    /// <summary>获取导致异常的被放弃的互斥体（如果已知的话）。</summary>
    /// <returns>如果未能识别被放弃的互斥体，则为表示该被放弃的互斥体的 <see cref="T:System.Threading.Mutex" /> 对象或 null。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public Mutex Mutex
    {
      [__DynamicallyInvokable] get
      {
        return this.m_Mutex;
      }
    }

    /// <summary>获取导致异常的被放弃的互斥体的索引（如果已知的话）。</summary>
    /// <returns>如果未能确定被放弃的互斥体的索引，则为传递给 <see cref="Overload:System.Threading.WaitHandle.WaitAny" /> 方法的等待句柄数组中的索引、表示该被放弃的互斥体的 <see cref="T:System.Threading.Mutex" /> 对象的索引或 –1。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public int MutexIndex
    {
      [__DynamicallyInvokable] get
      {
        return this.m_MutexIndex;
      }
    }

    /// <summary>使用默认值初始化 <see cref="T:System.Threading.AbandonedMutexException" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public AbandonedMutexException()
      : base(Environment.GetResourceString("Threading.AbandonedMutexException"))
    {
      this.SetErrorCode(-2146233043);
    }

    /// <summary>使用指定的错误消息初始化 <see cref="T:System.Threading.AbandonedMutexException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误消息。</param>
    [__DynamicallyInvokable]
    public AbandonedMutexException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233043);
    }

    /// <summary>用指定的错误信息和内部异常初始化 <see cref="T:System.Threading.AbandonedMutexException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误消息。</param>
    /// <param name="inner">导致当前异常的异常。如果 <paramref name="inner" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    [__DynamicallyInvokable]
    public AbandonedMutexException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2146233043);
    }

    /// <summary>用被放弃的互斥体的指定索引（如果可用）和表示该互斥体的 <see cref="T:System.Threading.Mutex" /> 对象初始化 <see cref="T:System.Threading.AbandonedMutexException" /> 类的新实例。</summary>
    /// <param name="location">如果对 <see cref="Overload:System.Threading.WaitHandle.WaitAny" /> 方法引发异常，则为等待句柄数组中被放弃的互斥体的索引，如果对 <see cref="Overload:System.Threading.WaitHandle.WaitOne" /> 或 <see cref="Overload:System.Threading.WaitHandle.WaitAll" /> 方法引发异常，则为 –1。</param>
    /// <param name="handle">一个 <see cref="T:System.Threading.Mutex" /> 对象，表示被放弃的互斥体。</param>
    [__DynamicallyInvokable]
    public AbandonedMutexException(int location, WaitHandle handle)
      : base(Environment.GetResourceString("Threading.AbandonedMutexException"))
    {
      this.SetErrorCode(-2146233043);
      this.SetupException(location, handle);
    }

    /// <summary>用指定的错误信息、被放弃的互斥体的索引（如果可用）以及被放弃的互斥体初始化 <see cref="T:System.Threading.AbandonedMutexException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误消息。</param>
    /// <param name="location">如果对 <see cref="Overload:System.Threading.WaitHandle.WaitAny" /> 方法引发异常，则为等待句柄数组中被放弃的互斥体的索引，如果对 <see cref="Overload:System.Threading.WaitHandle.WaitOne" /> 或 <see cref="Overload:System.Threading.WaitHandle.WaitAll" /> 方法引发异常，则为 –1。</param>
    /// <param name="handle">一个 <see cref="T:System.Threading.Mutex" /> 对象，表示被放弃的互斥体。</param>
    [__DynamicallyInvokable]
    public AbandonedMutexException(string message, int location, WaitHandle handle)
      : base(message)
    {
      this.SetErrorCode(-2146233043);
      this.SetupException(location, handle);
    }

    /// <summary>用指定的错误信息、内部异常、被放弃的互斥体的索引（如果可用）以及表示该互斥体的 <see cref="T:System.Threading.Mutex" /> 对象初始化 <see cref="T:System.Threading.AbandonedMutexException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误消息。</param>
    /// <param name="inner">导致当前异常的异常。如果 <paramref name="inner" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    /// <param name="location">如果对 <see cref="Overload:System.Threading.WaitHandle.WaitAny" /> 方法引发异常，则为等待句柄数组中被放弃的互斥体的索引，如果对 <see cref="Overload:System.Threading.WaitHandle.WaitOne" /> 或 <see cref="Overload:System.Threading.WaitHandle.WaitAll" /> 方法引发异常，则为 –1。</param>
    /// <param name="handle">一个 <see cref="T:System.Threading.Mutex" /> 对象，表示被放弃的互斥体。</param>
    [__DynamicallyInvokable]
    public AbandonedMutexException(string message, Exception inner, int location, WaitHandle handle)
      : base(message, inner)
    {
      this.SetErrorCode(-2146233043);
      this.SetupException(location, handle);
    }

    /// <summary>用序列化数据初始化 <see cref="T:System.Threading.AbandonedMutexException" /> 类的新实例。</summary>
    /// <param name="info">
    /// <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 对象，包含有关所引发异常的序列化对象数据。</param>
    /// <param name="context">
    /// <see cref="T:System.Runtime.Serialization.StreamingContext" /> 对象，它包含有关源或目标的上下文信息。</param>
    protected AbandonedMutexException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    private void SetupException(int location, WaitHandle handle)
    {
      this.m_MutexIndex = location;
      if (handle == null)
        return;
      this.m_Mutex = handle as Mutex;
    }
  }
}
