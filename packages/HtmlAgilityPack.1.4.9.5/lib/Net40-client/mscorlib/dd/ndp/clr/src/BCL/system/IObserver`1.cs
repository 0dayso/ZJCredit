// Decompiled with JetBrains decompiler
// Type: System.IObserver`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  /// <summary>提供用于接收基于推送的通知的机制。</summary>
  /// <typeparam name="T">提供通知信息的对象。此类型参数是逆变。即可以使用指定的类型或派生程度更低的类型。有关协变和逆变的详细信息，请参阅 泛型中的协变和逆变。</typeparam>
  [__DynamicallyInvokable]
  public interface IObserver<in T>
  {
    /// <summary>向观察者提供新数据。</summary>
    /// <param name="value">当前的通知信息。</param>
    [__DynamicallyInvokable]
    void OnNext(T value);

    /// <summary>通知观察者，提供程序遇到错误情况。</summary>
    /// <param name="error">一个提供有关错误的附加信息的对象。</param>
    [__DynamicallyInvokable]
    void OnError(Exception error);

    /// <summary>通知观察者，提供程序已完成发送基于推送的通知。</summary>
    [__DynamicallyInvokable]
    void OnCompleted();
  }
}
