// Decompiled with JetBrains decompiler
// Type: System.IObservable`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  /// <summary>定义基于推送的通知的提供程序。</summary>
  /// <typeparam name="T">提供通知信息的对象。此类型参数是协变。即可以使用指定的类型或派生程度更高的类型。有关协变和逆变的详细信息，请参阅 泛型中的协变和逆变。</typeparam>
  [__DynamicallyInvokable]
  public interface IObservable<out T>
  {
    /// <summary>通知提供程序：某观察程序将要接收通知。</summary>
    /// <returns>对允许观察者在提供程序发送完通知前停止接收这些通知的接口的引用。</returns>
    /// <param name="observer">要接收通知的对象。</param>
    [__DynamicallyInvokable]
    IDisposable Subscribe(IObserver<T> observer);
  }
}
