// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.ServerChannelSinkStack
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Metadata;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Channels
{
  /// <summary>持有服务器信道接收器的堆栈。</summary>
  [SecurityCritical]
  [ComVisible(true)]
  public class ServerChannelSinkStack : IServerChannelSinkStack, IServerResponseChannelSinkStack
  {
    private ServerChannelSinkStack.SinkStack _stack;
    private ServerChannelSinkStack.SinkStack _rememberedStack;
    private IMessage _asyncMsg;
    private MethodInfo _asyncEnd;
    private object _serverObject;
    private IMethodCallMessage _msg;

    internal object ServerObject
    {
      set
      {
        this._serverObject = value;
      }
    }

    /// <summary>将指定的接收器和与之关联的信息推送到接收器堆栈中。</summary>
    /// <param name="sink">要推送到接收器堆栈中的接收器。</param>
    /// <param name="state">在请求端生成的、响应端所需要的信息。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public void Push(IServerChannelSink sink, object state)
    {
      this._stack = new ServerChannelSinkStack.SinkStack()
      {
        PrevStack = this._stack,
        Sink = sink,
        State = state
      };
    }

    /// <summary>弹出与接收器堆栈中指定接收器（含）之下的所有接收器关联的信息。</summary>
    /// <returns>在请求端生成的、与指定接收器关联的信息。</returns>
    /// <param name="sink">要从接收器堆栈中移除和返回的接收器。</param>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">当前接收器堆栈为空，或者从未将指定接收器推送到当前堆栈中。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public object Pop(IServerChannelSink sink)
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

    /// <summary>存储消息接收器及其关联状态，用于以后的异步处理。</summary>
    /// <param name="sink">服务器信道接收器。</param>
    /// <param name="state">与 <paramref name="sink" /> 关联的状态。</param>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">当前接收器堆栈为空。- 或 -指定的接收器从未推入当前堆栈。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public void Store(IServerChannelSink sink, object state)
    {
      if (this._stack == null)
        throw new RemotingException(Environment.GetResourceString("Remoting_Channel_StoreOnEmptySinkStack"));
      while (this._stack.Sink != sink)
      {
        this._stack = this._stack.PrevStack;
        if (this._stack == null)
          break;
      }
      if (this._stack.Sink == null)
        throw new RemotingException(Environment.GetResourceString("Remoting_Channel_StoreOnSinkStackWithoutPush"));
      this._rememberedStack = new ServerChannelSinkStack.SinkStack()
      {
        PrevStack = this._rememberedStack,
        Sink = sink,
        State = state
      };
      this.Pop(sink);
    }

    /// <summary>存储消息接收器及其关联状态，然后使用刚刚存储的接收器或已存储的任意其他接收器来异步调度消息。</summary>
    /// <param name="sink">服务器信道接收器。</param>
    /// <param name="state">与 <paramref name="sink" /> 关联的状态。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public void StoreAndDispatch(IServerChannelSink sink, object state)
    {
      this.Store(sink, state);
      this.FlipRememberedStack();
      CrossContextChannel.DoAsyncDispatch(this._asyncMsg, (IMessageSink) null);
    }

    private void FlipRememberedStack()
    {
      if (this._stack != null)
        throw new RemotingException(Environment.GetResourceString("Remoting_Channel_CantCallFRSWhenStackEmtpy"));
      for (; this._rememberedStack != null; this._rememberedStack = this._rememberedStack.PrevStack)
        this._stack = new ServerChannelSinkStack.SinkStack()
        {
          PrevStack = this._stack,
          Sink = this._rememberedStack.Sink,
          State = this._rememberedStack.State
        };
    }

    /// <summary>请求对当前接收器堆栈中接收器的方法调用进行异步处理。</summary>
    /// <param name="msg">要序列化到请求流上的消息。</param>
    /// <param name="headers">从服务器响应流中检索到的标头。</param>
    /// <param name="stream">从传输接收器返回的流。</param>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">当前接收器堆栈为空。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public void AsyncProcessResponse(IMessage msg, ITransportHeaders headers, Stream stream)
    {
      if (this._stack == null)
        throw new RemotingException(Environment.GetResourceString("Remoting_Channel_CantCallAPRWhenStackEmpty"));
      IServerChannelSink serverChannelSink = this._stack.Sink;
      object obj = this._stack.State;
      this._stack = this._stack.PrevStack;
      object state = obj;
      IMessage msg1 = msg;
      ITransportHeaders headers1 = headers;
      Stream stream1 = stream;
      serverChannelSink.AsyncProcessResponse((IServerResponseChannelSinkStack) this, state, msg1, headers1, stream1);
    }

    /// <summary>返回指定的消息要序列化到其上的 <see cref="T:System.IO.Stream" />。</summary>
    /// <returns>指定的消息要序列化到其上的 <see cref="T:System.IO.Stream" />。</returns>
    /// <param name="msg">要序列化到请求流上的消息。</param>
    /// <param name="headers">从服务器响应流中检索到的标头。</param>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">接收器堆栈为空。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public Stream GetResponseStream(IMessage msg, ITransportHeaders headers)
    {
      if (this._stack == null)
        throw new RemotingException(Environment.GetResourceString("Remoting_Channel_CantCallGetResponseStreamWhenStackEmpty"));
      IServerChannelSink sink = this._stack.Sink;
      object state = this._stack.State;
      this._stack = this._stack.PrevStack;
      Stream responseStream = sink.GetResponseStream((IServerResponseChannelSinkStack) this, state, msg, headers);
      this.Push(sink, state);
      return responseStream;
    }

    /// <summary>提供一个 <see cref="T:System.AsyncCallback" /> 委托，用于在异步调度消息后处理回调。</summary>
    /// <param name="ar">对远程对象执行的异步操作的状态。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public void ServerCallback(IAsyncResult ar)
    {
      if (!(this._asyncEnd != (MethodInfo) null))
        return;
      RemotingMethodCachedData reflectionCachedData1 = InternalRemotingServices.GetReflectionCachedData((MethodBase) this._asyncEnd);
      RemotingMethodCachedData reflectionCachedData2 = InternalRemotingServices.GetReflectionCachedData(this._msg.MethodBase);
      ParameterInfo[] parameters = reflectionCachedData1.Parameters;
      object[] objArray = new object[parameters.Length];
      objArray[parameters.Length - 1] = (object) ar;
      object[] args = this._msg.Args;
      AsyncMessageHelper.GetOutArgs(reflectionCachedData2.Parameters, args, objArray);
      StackBuilderSink stackBuilderSink = new StackBuilderSink(this._serverObject);
      object[] outArgs1;
      object ret = stackBuilderSink.PrivateProcessMessage(this._asyncEnd.MethodHandle, Message.CoerceArgs((MethodBase) this._asyncEnd, objArray, parameters), this._serverObject, out outArgs1);
      if (outArgs1 != null)
        outArgs1 = ArgMapper.ExpandAsyncEndArgsToSyncArgs(reflectionCachedData2, outArgs1);
      stackBuilderSink.CopyNonByrefOutArgsFromOriginalArgs(reflectionCachedData2, args, ref outArgs1);
      object[] outArgs2 = outArgs1;
      int argCount = this._msg.ArgCount;
      LogicalCallContext logicalCallContext = Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext;
      IMethodCallMessage mcm = this._msg;
      this.AsyncProcessResponse((IMessage) new ReturnMessage(ret, outArgs2, argCount, logicalCallContext, mcm), (ITransportHeaders) null, (Stream) null);
    }

    private class SinkStack
    {
      public ServerChannelSinkStack.SinkStack PrevStack;
      public IServerChannelSink Sink;
      public object State;
    }
  }
}
