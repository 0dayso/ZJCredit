// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.ClientChannelSinkStack
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
  /// <summary>持有异步消息响应解码过程中必须调用的客户端信道接收器堆栈。</summary>
  [SecurityCritical]
  [ComVisible(true)]
  public class ClientChannelSinkStack : IClientChannelSinkStack, IClientResponseChannelSinkStack
  {
    private ClientChannelSinkStack.SinkStack _stack;
    private IMessageSink _replySink;

    /// <summary>使用默认值初始化 <see cref="T:System.Runtime.Remoting.Channels.ClientChannelSinkStack" /> 类的新实例。</summary>
    public ClientChannelSinkStack()
    {
    }

    /// <summary>使用指定的答复接收器初始化 <see cref="T:System.Runtime.Remoting.Channels.ClientChannelSinkStack" /> 类的新实例。</summary>
    /// <param name="replySink">当前堆栈可用于答复消息的 <see cref="T:System.Runtime.Remoting.Messaging.IMessageSink" />。</param>
    public ClientChannelSinkStack(IMessageSink replySink)
    {
      this._replySink = replySink;
    }

    /// <summary>将指定的接收器和与之关联的信息推送到接收器堆栈中。</summary>
    /// <param name="sink">要推送到接收器堆栈中的接收器。</param>
    /// <param name="state">在请求端生成的、响应端所需要的信息。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public void Push(IClientChannelSink sink, object state)
    {
      this._stack = new ClientChannelSinkStack.SinkStack()
      {
        PrevStack = this._stack,
        Sink = sink,
        State = state
      };
    }

    /// <summary>弹出与接收器堆栈中指定接收器之下（含指定接收器）的所有接收器关联的信息。</summary>
    /// <returns>在请求端生成的、与指定接收器关联的信息。</returns>
    /// <param name="sink">要从接收器堆栈中移除和返回的接收器。</param>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">当前接收器堆栈为空，或者从未将指定接收器推送到当前堆栈中。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public object Pop(IClientChannelSink sink)
    {
      if (this._stack == null)
        throw new RemotingException(Environment.GetResourceString("Remoting_Channel_PopOnEmptySinkStack"));
      while (this._stack.Sink != sink)
      {
        this._stack = this._stack.PrevStack;
        if (this._stack == null)
          break;
      }
      if (this._stack.Sink == null)
        throw new RemotingException(Environment.GetResourceString("Remoting_Channel_PopFromSinkStackWithoutPush"));
      object obj = this._stack.State;
      this._stack = this._stack.PrevStack;
      return obj;
    }

    /// <summary>请求对当前接收器堆栈中接收器的方法调用进行异步处理。</summary>
    /// <param name="headers">从服务器响应流中检索到的标头。</param>
    /// <param name="stream">从传输接收器返回的流。</param>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">当前接收器堆栈为空。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public void AsyncProcessResponse(ITransportHeaders headers, Stream stream)
    {
      if (this._replySink == null)
        return;
      if (this._stack == null)
        throw new RemotingException(Environment.GetResourceString("Remoting_Channel_CantCallAPRWhenStackEmpty"));
      IClientChannelSink clientChannelSink = this._stack.Sink;
      object obj = this._stack.State;
      this._stack = this._stack.PrevStack;
      object state = obj;
      ITransportHeaders headers1 = headers;
      Stream stream1 = stream;
      clientChannelSink.AsyncProcessResponse((IClientResponseChannelSinkStack) this, state, headers1, stream1);
    }

    /// <summary>调度答复接收器上的指定答复消息。</summary>
    /// <param name="msg">要调度的 <see cref="T:System.Runtime.Remoting.Messaging.IMessage" />。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public void DispatchReplyMessage(IMessage msg)
    {
      if (this._replySink == null)
        return;
      this._replySink.SyncProcessMessage(msg);
    }

    /// <summary>调度答复接收器上的指定异常。</summary>
    /// <param name="e">要调度到服务器的异常。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public void DispatchException(Exception e)
    {
      this.DispatchReplyMessage((IMessage) new ReturnMessage(e, (IMethodCallMessage) null));
    }

    private class SinkStack
    {
      public ClientChannelSinkStack.SinkStack PrevStack;
      public IClientChannelSink Sink;
      public object State;
    }
  }
}
