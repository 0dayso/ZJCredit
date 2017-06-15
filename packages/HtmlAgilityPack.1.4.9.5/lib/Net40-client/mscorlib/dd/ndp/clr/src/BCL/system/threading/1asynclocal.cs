// Decompiled with JetBrains decompiler
// Type: System.Threading.AsyncLocalValueChangedArgs`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Threading
{
  /// <summary>向针对更改通知进行了注册的 <see cref="T:System.Threading.AsyncLocal`1" /> 实例提供数据更改信息的类。</summary>
  /// <typeparam name="T">数据的类型。</typeparam>
  [__DynamicallyInvokable]
  public struct AsyncLocalValueChangedArgs<T>
  {
    /// <summary>获取数据的上一个值。</summary>
    /// <returns>数据的上一个值。</returns>
    [__DynamicallyInvokable]
    public T PreviousValue { [__DynamicallyInvokable] get; private set; }

    /// <summary>获取数据的当前值。</summary>
    /// <returns>数据的当前值。</returns>
    [__DynamicallyInvokable]
    public T CurrentValue { [__DynamicallyInvokable] get; private set; }

    /// <summary>返回一个值，该值指示是否由于执行上下文更改而更改了值。</summary>
    /// <returns>如果由于执行上下文更改而更改了值，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool ThreadContextChanged { [__DynamicallyInvokable] get; private set; }

    internal AsyncLocalValueChangedArgs(T previousValue, T currentValue, bool contextChanged)
    {
      this = new AsyncLocalValueChangedArgs<T>();
      this.PreviousValue = previousValue;
      this.CurrentValue = currentValue;
      this.ThreadContextChanged = contextChanged;
    }
  }
}
