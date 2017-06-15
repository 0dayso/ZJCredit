// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.AsyncResult
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
  /// <summary>封装对委托的异步操作的结果。</summary>
  [ComVisible(true)]
  public class AsyncResult : IAsyncResult, IMessageSink
  {
    private IMessageCtrl _mc;
    private AsyncCallback _acbd;
    private IMessage _replyMsg;
    private bool _isCompleted;
    private bool _endInvokeCalled;
    private ManualResetEvent _AsyncWaitHandle;
    private Delegate _asyncDelegate;
    private object _asyncState;

    /// <summary>获取一个值，该值指示服务器是否已完成该调用。</summary>
    /// <returns>服务器完成该调用后，为 true；否则为 false。</returns>
    public virtual bool IsCompleted
    {
      get
      {
        return this._isCompleted;
      }
    }

    /// <summary>获取在其上调用异步调用的委托对象。</summary>
    /// <returns>在其上调用异步调用的委托对象。</returns>
    public virtual object AsyncDelegate
    {
      get
      {
        return (object) this._asyncDelegate;
      }
    }

    /// <summary>获取作为 BeginInvoke 方法调用的最后一个参数而提供的对象。</summary>
    /// <returns>作为 BeginInvoke 方法调用的最后一个参数而提供的对象。</returns>
    public virtual object AsyncState
    {
      get
      {
        return this._asyncState;
      }
    }

    /// <summary>获取一个值，该值指示 BeginInvoke 调用是否同步完成。</summary>
    /// <returns>如果 BeginInvoke 调用同步完成，则为 true；否则为 false。</returns>
    public virtual bool CompletedSynchronously
    {
      get
      {
        return false;
      }
    }

    /// <summary>获取或设置一个值，该值指示是否已在当前 <see cref="T:System.Runtime.Remoting.Messaging.AsyncResult" /> 上调用 EndInvoke。</summary>
    /// <returns>如果已在当前 <see cref="T:System.Runtime.Remoting.Messaging.AsyncResult" /> 上调用 EndInvoke，则为 true；否则为 false。</returns>
    public bool EndInvokeCalled
    {
      get
      {
        return this._endInvokeCalled;
      }
      set
      {
        this._endInvokeCalled = value;
      }
    }

    /// <summary>获取封装 Win32 同步句柄并允许实现各种同步方案的 <see cref="T:System.Threading.WaitHandle" />。</summary>
    /// <returns>封装 Win32 同步句柄并允许实现各种同步方案的 <see cref="T:System.Threading.WaitHandle" />。</returns>
    public virtual WaitHandle AsyncWaitHandle
    {
      get
      {
        this.FaultInWaitHandle();
        return (WaitHandle) this._AsyncWaitHandle;
      }
    }

    /// <summary>获取接收器链中的下一个消息接收器。</summary>
    /// <returns>一个 <see cref="T:System.Runtime.Remoting.Messaging.IMessageSink" /> 接口，表示接收器链上的下一个消息接收器。</returns>
    public IMessageSink NextSink
    {
      [SecurityCritical] get
      {
        return (IMessageSink) null;
      }
    }

    [SecurityCritical]
    internal AsyncResult(Message m)
    {
      m.GetAsyncBeginInfo(out this._acbd, out this._asyncState);
      this._asyncDelegate = (Delegate) m.GetThisPtr();
    }

    private void FaultInWaitHandle()
    {
      lock (this)
      {
        if (this._AsyncWaitHandle != null)
          return;
        this._AsyncWaitHandle = new ManualResetEvent(false);
      }
    }

    /// <summary>为当前远程方法调用设置一个 <see cref="T:System.Runtime.Remoting.Messaging.IMessageCtrl" />，这为在调度异步消息后控制异步消息提供了一种方法。</summary>
    /// <param name="mc">当前远程方法调用的 <see cref="T:System.Runtime.Remoting.Messaging.IMessageCtrl" />。</param>
    public virtual void SetMessageCtrl(IMessageCtrl mc)
    {
      this._mc = mc;
    }

    /// <summary>异步处理远程对象上的方法调用返回的响应消息。</summary>
    /// <returns>返回 null。</returns>
    /// <param name="msg">远程对象上的方法调用的响应消息。</param>
    [SecurityCritical]
    public virtual IMessage SyncProcessMessage(IMessage msg)
    {
      this._replyMsg = msg != null ? (msg is IMethodReturnMessage ? msg : (IMessage) new ReturnMessage((Exception) new RemotingException(Environment.GetResourceString("Remoting_Message_BadType")), (IMethodCallMessage) new ErrorMessage())) : (IMessage) new ReturnMessage((Exception) new RemotingException(Environment.GetResourceString("Remoting_NullMessage")), (IMethodCallMessage) new ErrorMessage());
      this._isCompleted = true;
      this.FaultInWaitHandle();
      this._AsyncWaitHandle.Set();
      if (this._acbd != null)
        this._acbd((IAsyncResult) this);
      return (IMessage) null;
    }

    /// <summary>实现 <see cref="T:System.Runtime.Remoting.Messaging.IMessageSink" /> 接口。</summary>
    /// <returns>不返回任何值。</returns>
    /// <param name="msg">请求的 <see cref="T:System.Runtime.Remoting.Messaging.IMessage" /> 接口。</param>
    /// <param name="replySink">响应的 <see cref="T:System.Runtime.Remoting.Messaging.IMessageSink" /> 接口。</param>
    [SecurityCritical]
    public virtual IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_Method"));
    }

    /// <summary>获取异步调用的响应消息。</summary>
    /// <returns>一条远程处理消息，表示对远程对象上的方法调用的响应。</returns>
    public virtual IMessage GetReplyMessage()
    {
      return this._replyMsg;
    }
  }
}
