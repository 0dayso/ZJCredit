// Decompiled with JetBrains decompiler
// Type: System.Runtime.ExceptionServices.ExceptionDispatchInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.ExceptionServices
{
  /// <summary>表示异常，其状态在特定代码点捕获。</summary>
  [__DynamicallyInvokable]
  public sealed class ExceptionDispatchInfo
  {
    private Exception m_Exception;
    private string m_remoteStackTrace;
    private object m_stackTrace;
    private object m_dynamicMethods;
    private UIntPtr m_IPForWatsonBuckets;
    private object m_WatsonBuckets;

    internal UIntPtr IPForWatsonBuckets
    {
      get
      {
        return this.m_IPForWatsonBuckets;
      }
    }

    internal object WatsonBuckets
    {
      get
      {
        return this.m_WatsonBuckets;
      }
    }

    internal object BinaryStackTraceArray
    {
      get
      {
        return this.m_stackTrace;
      }
    }

    internal object DynamicMethodArray
    {
      get
      {
        return this.m_dynamicMethods;
      }
    }

    internal string RemoteStackTrace
    {
      get
      {
        return this.m_remoteStackTrace;
      }
    }

    /// <summary>获取被当前实例表示的异常。</summary>
    /// <returns>被当前实例表示的异常。</returns>
    [__DynamicallyInvokable]
    public Exception SourceException
    {
      [__DynamicallyInvokable] get
      {
        return this.m_Exception;
      }
    }

    private ExceptionDispatchInfo(Exception exception)
    {
      this.m_Exception = exception;
      this.m_remoteStackTrace = exception.RemoteStackTrace;
      object currentStackTrace;
      object dynamicMethodArray;
      this.m_Exception.GetStackTracesDeepCopy(out currentStackTrace, out dynamicMethodArray);
      this.m_stackTrace = currentStackTrace;
      this.m_dynamicMethods = dynamicMethodArray;
      this.m_IPForWatsonBuckets = exception.IPForWatsonBuckets;
      this.m_WatsonBuckets = exception.WatsonBuckets;
    }

    /// <summary>创建 <see cref="T:System.Runtime.ExceptionServices.ExceptionDispatchInfo" /> 对象，此对象在代码当前点表示指定异常。</summary>
    /// <returns>表示代码中当前点的指定异常的对象。</returns>
    /// <param name="source">由一个返回的对象表示且状态被捕获的异常。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="source" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public static ExceptionDispatchInfo Capture(Exception source)
    {
      if (source == null)
        throw new ArgumentNullException("source", Environment.GetResourceString("ArgumentNull_Obj"));
      return new ExceptionDispatchInfo(source);
    }

    /// <summary>恢复捕获异常时保存的状态后，引发由当前 <see cref="T:System.Runtime.ExceptionServices.ExceptionDispatchInfo" /> 对象表示的异常。</summary>
    [__DynamicallyInvokable]
    public void Throw()
    {
      this.m_Exception.RestoreExceptionDispatchInfo(this);
      throw this.m_Exception;
    }
  }
}
