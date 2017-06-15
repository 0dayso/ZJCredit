// Decompiled with JetBrains decompiler
// Type: System.IAsyncResult
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Threading;

namespace System
{
  /// <summary>表示异步操作的状态。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public interface IAsyncResult
  {
    /// <summary>获取一个值，该值指示异步操作是否已完成。</summary>
    /// <returns>如果操作完成则为 true，否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    bool IsCompleted { [__DynamicallyInvokable] get; }

    /// <summary>获取用于等待异步操作完成的 <see cref="T:System.Threading.WaitHandle" />。</summary>
    /// <returns>用于等待异步操作完成的 <see cref="T:System.Threading.WaitHandle" />。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    WaitHandle AsyncWaitHandle { [__DynamicallyInvokable] get; }

    /// <summary>获取用户定义的对象，它限定或包含关于异步操作的信息。</summary>
    /// <returns>用户定义的对象，它限定或包含关于异步操作的信息。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    object AsyncState { [__DynamicallyInvokable] get; }

    /// <summary>获取一个值，该值指示异步操作是否同步完成。</summary>
    /// <returns>如果异步操作同步完成，则为 true；否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    bool CompletedSynchronously { [__DynamicallyInvokable] get; }
  }
}
