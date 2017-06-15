// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.IMessageCtrl
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
  /// <summary>提供一种方法，以在使用 <see cref="M:System.Runtime.Remoting.Messaging.IMessageSink.AsyncProcessMessage(System.Runtime.Remoting.Messaging.IMessage,System.Runtime.Remoting.Messaging.IMessageSink)" /> 调度异步消息后控制这些消息。</summary>
  [ComVisible(true)]
  public interface IMessageCtrl
  {
    /// <summary>取消异步调用。</summary>
    /// <param name="msToCancel">取消消息前经过的毫秒数。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方通过接口引用进行调用，它没有基础结构权限。</exception>
    [SecurityCritical]
    void Cancel(int msToCancel);
  }
}
