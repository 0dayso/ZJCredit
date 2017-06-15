// Decompiled with JetBrains decompiler
// Type: System.Threading.AsyncLocal`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Threading
{
  /// <summary>表示对于给定异步控制流（如异步方法）是本地数据的环境数据。</summary>
  /// <typeparam name="T">环境数据的类型。</typeparam>
  [__DynamicallyInvokable]
  public sealed class AsyncLocal<T> : IAsyncLocal
  {
    [SecurityCritical]
    private readonly Action<AsyncLocalValueChangedArgs<T>> m_valueChangedHandler;

    /// <summary>获取或设置环境数据的值。</summary>
    /// <returns>环境数据的值。</returns>
    [__DynamicallyInvokable]
    public T Value
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        object localValue = ExecutionContext.GetLocalValue((IAsyncLocal) this);
        if (localValue != null)
          return (T) localValue;
        return default (T);
      }
      [SecuritySafeCritical, __DynamicallyInvokable] set
      {
        ExecutionContext.SetLocalValue((IAsyncLocal) this, (object) value, this.m_valueChangedHandler != null);
      }
    }

    /// <summary>实例化不接收更改通知的 <see cref="T:System.Threading.AsyncLocal`1" /> 实例。</summary>
    [__DynamicallyInvokable]
    public AsyncLocal()
    {
    }

    /// <summary>实例化接收更改通知的 <see cref="T:System.Threading.AsyncLocal`1" /> 本地实例。</summary>
    /// <param name="valueChangedHandler">只要当前值在任何线程上发生更改时便会调用的委托。</param>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public AsyncLocal(Action<AsyncLocalValueChangedArgs<T>> valueChangedHandler)
    {
      this.m_valueChangedHandler = valueChangedHandler;
    }

    [SecurityCritical]
    void IAsyncLocal.OnValueChanged(object previousValueObj, object currentValueObj, bool contextChanged)
    {
      this.m_valueChangedHandler(new AsyncLocalValueChangedArgs<T>(previousValueObj == null ? default (T) : (T) previousValueObj, currentValueObj == null ? default (T) : (T) currentValueObj, contextChanged));
    }
  }
}
