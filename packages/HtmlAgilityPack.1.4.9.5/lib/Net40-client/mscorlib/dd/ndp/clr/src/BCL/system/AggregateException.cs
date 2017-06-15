// Decompiled with JetBrains decompiler
// Type: System.AggregateException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.ExceptionServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
  /// <summary>表示在应用程序执行期间发生的一个或多个错误。</summary>
  [DebuggerDisplay("Count = {InnerExceptionCount}")]
  [__DynamicallyInvokable]
  [Serializable]
  public class AggregateException : Exception
  {
    private ReadOnlyCollection<Exception> m_innerExceptions;

    /// <summary>获取导致当前异常的 <see cref="T:System.Exception" /> 实例的只读集合。</summary>
    /// <returns>返回导致当前异常的 <see cref="T:System.Exception" /> 实例的只读集合。</returns>
    [__DynamicallyInvokable]
    public ReadOnlyCollection<Exception> InnerExceptions
    {
      [__DynamicallyInvokable] get
      {
        return this.m_innerExceptions;
      }
    }

    private int InnerExceptionCount
    {
      get
      {
        return this.InnerExceptions.Count;
      }
    }

    /// <summary>使用由系统提供的用来描述错误的消息初始化 <see cref="T:System.AggregateException" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public AggregateException()
      : base(Environment.GetResourceString("AggregateException_ctor_DefaultMessage"))
    {
      this.m_innerExceptions = new ReadOnlyCollection<Exception>((IList<Exception>) new Exception[0]);
    }

    /// <summary>使用指定的描述错误的消息初始化 <see cref="T:System.AggregateException" /> 类的新实例。</summary>
    /// <param name="message">描述该异常的消息。此构造函数的调用方需要确保此字符串已针对当前系统区域性进行了本地化。</param>
    [__DynamicallyInvokable]
    public AggregateException(string message)
      : base(message)
    {
      this.m_innerExceptions = new ReadOnlyCollection<Exception>((IList<Exception>) new Exception[0]);
    }

    /// <summary>使用指定错误消息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.AggregateException" /> 类的新实例。</summary>
    /// <param name="message">描述该异常的消息。此构造函数的调用方需要确保此字符串已针对当前系统区域性进行了本地化。</param>
    /// <param name="innerException">导致当前异常的异常。如果 <paramref name="innerException" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="innerException" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    public AggregateException(string message, Exception innerException)
      : base(message, innerException)
    {
      if (innerException == null)
        throw new ArgumentNullException("innerException");
      this.m_innerExceptions = new ReadOnlyCollection<Exception>((IList<Exception>) new Exception[1]
      {
        innerException
      });
    }

    /// <summary>用对作为此异常原因的内部异常的引用初始化 <see cref="T:System.AggregateException" /> 类的新实例。</summary>
    /// <param name="innerExceptions">导致当前异常的异常。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="innerExceptions" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="innerExceptions" /> 的元素为 Null。</exception>
    [__DynamicallyInvokable]
    public AggregateException(IEnumerable<Exception> innerExceptions)
      : this(Environment.GetResourceString("AggregateException_ctor_DefaultMessage"), innerExceptions)
    {
    }

    /// <summary>用对作为此异常原因的内部异常的引用初始化 <see cref="T:System.AggregateException" /> 类的新实例。</summary>
    /// <param name="innerExceptions">导致当前异常的异常。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="innerExceptions" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="innerExceptions" /> 的元素为 Null。</exception>
    [__DynamicallyInvokable]
    public AggregateException(params Exception[] innerExceptions)
      : this(Environment.GetResourceString("AggregateException_ctor_DefaultMessage"), innerExceptions)
    {
    }

    /// <summary>使用指定错误信息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.AggregateException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="innerExceptions">导致当前异常的异常。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="innerExceptions" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="innerExceptions" /> 的元素为 Null。</exception>
    [__DynamicallyInvokable]
    public AggregateException(string message, IEnumerable<Exception> innerExceptions)
      : this(message, innerExceptions as IList<Exception> ?? (innerExceptions == null ? (IList<Exception>) null : (IList<Exception>) new List<Exception>(innerExceptions)))
    {
    }

    /// <summary>使用指定错误信息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.AggregateException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="innerExceptions">导致当前异常的异常。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="innerExceptions" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="innerExceptions" /> 的元素为 Null。</exception>
    [__DynamicallyInvokable]
    public AggregateException(string message, params Exception[] innerExceptions)
      : this(message, (IList<Exception>) innerExceptions)
    {
    }

    private AggregateException(string message, IList<Exception> innerExceptions)
      : base(message, innerExceptions == null || innerExceptions.Count <= 0 ? (Exception) null : innerExceptions[0])
    {
      if (innerExceptions == null)
        throw new ArgumentNullException("innerExceptions");
      Exception[] exceptionArray = new Exception[innerExceptions.Count];
      for (int index = 0; index < exceptionArray.Length; ++index)
      {
        exceptionArray[index] = innerExceptions[index];
        if (exceptionArray[index] == null)
          throw new ArgumentException(Environment.GetResourceString("AggregateException_ctor_InnerExceptionNull"));
      }
      this.m_innerExceptions = new ReadOnlyCollection<Exception>((IList<Exception>) exceptionArray);
    }

    internal AggregateException(IEnumerable<ExceptionDispatchInfo> innerExceptionInfos)
      : this(Environment.GetResourceString("AggregateException_ctor_DefaultMessage"), innerExceptionInfos)
    {
    }

    internal AggregateException(string message, IEnumerable<ExceptionDispatchInfo> innerExceptionInfos)
      : this(message, innerExceptionInfos as IList<ExceptionDispatchInfo> ?? (innerExceptionInfos == null ? (IList<ExceptionDispatchInfo>) null : (IList<ExceptionDispatchInfo>) new List<ExceptionDispatchInfo>(innerExceptionInfos)))
    {
    }

    private AggregateException(string message, IList<ExceptionDispatchInfo> innerExceptionInfos)
      : base(message, innerExceptionInfos == null || innerExceptionInfos.Count <= 0 || innerExceptionInfos[0] == null ? (Exception) null : innerExceptionInfos[0].SourceException)
    {
      if (innerExceptionInfos == null)
        throw new ArgumentNullException("innerExceptionInfos");
      Exception[] exceptionArray = new Exception[innerExceptionInfos.Count];
      for (int index = 0; index < exceptionArray.Length; ++index)
      {
        ExceptionDispatchInfo exceptionDispatchInfo = innerExceptionInfos[index];
        if (exceptionDispatchInfo != null)
          exceptionArray[index] = exceptionDispatchInfo.SourceException;
        if (exceptionArray[index] == null)
          throw new ArgumentException(Environment.GetResourceString("AggregateException_ctor_InnerExceptionNull"));
      }
      this.m_innerExceptions = new ReadOnlyCollection<Exception>((IList<Exception>) exceptionArray);
    }

    /// <summary>用序列化数据初始化 <see cref="T:System.AggregateException" /> 类的新实例。</summary>
    /// <param name="info">承载序列化对象数据的对象。</param>
    /// <param name="context">关于来源和目标的上下文信息</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="info" /> 参数为 null。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">异常未能正确进行反序列化。</exception>
    [SecurityCritical]
    protected AggregateException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      Exception[] exceptionArray = info.GetValue("InnerExceptions", typeof (Exception[])) as Exception[];
      if (exceptionArray == null)
        throw new SerializationException(Environment.GetResourceString("AggregateException_DeserializationFailure"));
      this.m_innerExceptions = new ReadOnlyCollection<Exception>((IList<Exception>) exceptionArray);
    }

    /// <summary>用序列化数据初始化 <see cref="T:System.AggregateException" /> 类的新实例。</summary>
    /// <param name="info">承载序列化对象数据的对象。</param>
    /// <param name="context">关于来源和目标的上下文信息</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="info" /> 参数为 null。</exception>
    [SecurityCritical]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      base.GetObjectData(info, context);
      Exception[] array = new Exception[this.m_innerExceptions.Count];
      this.m_innerExceptions.CopyTo(array, 0);
      info.AddValue("InnerExceptions", (object) array, typeof (Exception[]));
    }

    /// <summary>返回 <see cref="T:System.AggregateException" />，它是此异常的根本原因。</summary>
    /// <returns>返回 <see cref="T:System.AggregateException" />，它是此异常的根本原因。</returns>
    [__DynamicallyInvokable]
    public override Exception GetBaseException()
    {
      Exception exception = (Exception) this;
      for (AggregateException aggregateException = this; aggregateException != null && aggregateException.InnerExceptions.Count == 1; aggregateException = exception as AggregateException)
        exception = exception.InnerException;
      return exception;
    }

    /// <summary>对此 <see cref="T:System.AggregateException" /> 所包含的每个 <see cref="T:System.Exception" /> 调用处理程序。</summary>
    /// <param name="predicate">要对每个异常执行的谓词。该谓词接受作为参数来处理 <see cref="T:System.Exception" />，并返回指示异常是否已处理的布尔值。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="predicate" /> 参数为 null。</exception>
    /// <exception cref="T:System.AggregateException">未处理此 <see cref="T:System.AggregateException" /> 包含的异常。</exception>
    [__DynamicallyInvokable]
    public void Handle(Func<Exception, bool> predicate)
    {
      if (predicate == null)
        throw new ArgumentNullException("predicate");
      List<Exception> exceptionList = (List<Exception>) null;
      for (int index = 0; index < this.m_innerExceptions.Count; ++index)
      {
        if (!predicate(this.m_innerExceptions[index]))
        {
          if (exceptionList == null)
            exceptionList = new List<Exception>();
          exceptionList.Add(this.m_innerExceptions[index]);
        }
      }
      if (exceptionList != null)
        throw new AggregateException(this.Message, (IList<Exception>) exceptionList);
    }

    /// <summary>将 <see cref="T:System.AggregateException" /> 实例平展入单个新实例。</summary>
    /// <returns>一个新的平展 <see cref="T:System.AggregateException" />。</returns>
    [__DynamicallyInvokable]
    public AggregateException Flatten()
    {
      List<Exception> exceptionList1 = new List<Exception>();
      List<AggregateException> aggregateExceptionList = new List<AggregateException>();
      aggregateExceptionList.Add(this);
      int num = 0;
      while (aggregateExceptionList.Count > num)
      {
        IList<Exception> exceptionList2 = (IList<Exception>) aggregateExceptionList[num++].InnerExceptions;
        for (int index = 0; index < exceptionList2.Count; ++index)
        {
          Exception exception = exceptionList2[index];
          if (exception != null)
          {
            AggregateException aggregateException = exception as AggregateException;
            if (aggregateException != null)
              aggregateExceptionList.Add(aggregateException);
            else
              exceptionList1.Add(exception);
          }
        }
      }
      return new AggregateException(this.Message, (IList<Exception>) exceptionList1);
    }

    /// <summary>创建并返回当前 <see cref="T:System.AggregateException" /> 的字符串表示形式。</summary>
    /// <returns>当前异常的字符串表示形式。</returns>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      string str = base.ToString();
      for (int index = 0; index < this.m_innerExceptions.Count; ++index)
        str = string.Format((IFormatProvider) CultureInfo.InvariantCulture, Environment.GetResourceString("AggregateException_ToString"), (object) str, (object) Environment.NewLine, (object) index, (object) this.m_innerExceptions[index].ToString(), (object) "<---", (object) Environment.NewLine);
      return str;
    }
  }
}
