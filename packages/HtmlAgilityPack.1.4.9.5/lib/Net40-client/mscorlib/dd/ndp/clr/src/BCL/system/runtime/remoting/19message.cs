// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.InternalMessageWrapper
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
  /// <summary>包装在消息接收器之间传递的远程处理数据，或者用于从客户端到服务器的请求，或者用于后续的响应。</summary>
  [SecurityCritical]
  [ComVisible(true)]
  public class InternalMessageWrapper
  {
    /// <summary>表示由消息包装所包装的请求或响应 <see cref="T:System.Runtime.Remoting.Messaging.IMethodMessage" /> 接口。</summary>
    protected IMessage WrappedMessage;

    /// <summary>初始化 <see cref="T:System.Runtime.Remoting.Messaging.InternalMessageWrapper" /> 类的新实例。</summary>
    /// <param name="msg">一条消息，或者作为远程对象上的输出方法调用，或者作为后续的响应。</param>
    public InternalMessageWrapper(IMessage msg)
    {
      this.WrappedMessage = msg;
    }

    [SecurityCritical]
    internal object GetIdentityObject()
    {
      IInternalMessage internalMessage = this.WrappedMessage as IInternalMessage;
      if (internalMessage != null)
        return (object) internalMessage.IdentityObject;
      InternalMessageWrapper internalMessageWrapper = this.WrappedMessage as InternalMessageWrapper;
      if (internalMessageWrapper != null)
        return internalMessageWrapper.GetIdentityObject();
      return (object) null;
    }

    [SecurityCritical]
    internal object GetServerIdentityObject()
    {
      IInternalMessage internalMessage = this.WrappedMessage as IInternalMessage;
      if (internalMessage != null)
        return (object) internalMessage.ServerIdentityObject;
      InternalMessageWrapper internalMessageWrapper = this.WrappedMessage as InternalMessageWrapper;
      if (internalMessageWrapper != null)
        return internalMessageWrapper.GetServerIdentityObject();
      return (object) null;
    }
  }
}
