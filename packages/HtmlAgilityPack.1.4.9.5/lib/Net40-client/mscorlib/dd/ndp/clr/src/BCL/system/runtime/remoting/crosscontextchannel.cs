// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.CrossContextChannel
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Channels
{
  internal class CrossContextChannel : InternalSink, IMessageSink
  {
    private static object staticSyncObject = new object();
    private static InternalCrossContextDelegate s_xctxDel = new InternalCrossContextDelegate(CrossContextChannel.SyncProcessMessageCallback);
    private const string _channelName = "XCTX";
    private const int _channelCapability = 0;
    private const string _channelURI = "XCTX_URI";

    private static CrossContextChannel messageSink
    {
      get
      {
        return Thread.GetDomain().RemotingData.ChannelServicesData.xctxmessageSink;
      }
      set
      {
        Thread.GetDomain().RemotingData.ChannelServicesData.xctxmessageSink = value;
      }
    }

    internal static IMessageSink MessageSink
    {
      get
      {
        if (CrossContextChannel.messageSink == null)
        {
          CrossContextChannel crossContextChannel = new CrossContextChannel();
          lock (CrossContextChannel.staticSyncObject)
          {
            if (CrossContextChannel.messageSink == null)
              CrossContextChannel.messageSink = crossContextChannel;
          }
        }
        return (IMessageSink) CrossContextChannel.messageSink;
      }
    }

    public IMessageSink NextSink
    {
      [SecurityCritical] get
      {
        return (IMessageSink) null;
      }
    }

    [SecuritySafeCritical]
    static CrossContextChannel()
    {
    }

    [SecurityCritical]
    internal static object SyncProcessMessageCallback(object[] args)
    {
      IMessage msg1 = args[0] as IMessage;
      Context context = args[1] as Context;
      if (RemotingServices.CORProfilerTrackRemoting())
      {
        Guid id = Guid.Empty;
        if (RemotingServices.CORProfilerTrackRemotingCookie())
        {
          object obj = msg1.Properties[(object) "CORProfilerCookie"];
          if (obj != null)
            id = (Guid) obj;
        }
        RemotingServices.CORProfilerRemotingServerReceivingMessage(id, false);
      }
      IMessage msg2 = msg1;
      int num1 = 0;
      int num2 = 1;
      int num3 = 0;
      int num4 = 1;
      context.NotifyDynamicSinks(msg2, num1 != 0, num2 != 0, num3 != 0, num4 != 0);
      IMessage message = context.GetServerContextChain().SyncProcessMessage(msg1);
      IMessage msg3 = message;
      int num5 = 0;
      int num6 = 0;
      int num7 = 0;
      int num8 = 1;
      context.NotifyDynamicSinks(msg3, num5 != 0, num6 != 0, num7 != 0, num8 != 0);
      if (RemotingServices.CORProfilerTrackRemoting())
      {
        Guid id;
        RemotingServices.CORProfilerRemotingServerSendingReply(out id, false);
        if (RemotingServices.CORProfilerTrackRemotingCookie())
          message.Properties[(object) "CORProfilerCookie"] = (object) id;
      }
      return (object) message;
    }

    [SecurityCritical]
    public virtual IMessage SyncProcessMessage(IMessage reqMsg)
    {
      object[] args = new object[2];
      IMessage message1;
      try
      {
        IMessage message2 = InternalSink.ValidateMessage(reqMsg);
        if (message2 != null)
          return message2;
        ServerIdentity serverIdentity = InternalSink.GetServerIdentity(reqMsg);
        args[0] = (object) reqMsg;
        args[1] = (object) serverIdentity.ServerContext;
        message1 = (IMessage) Thread.CurrentThread.InternalCrossContextCallback(serverIdentity.ServerContext, CrossContextChannel.s_xctxDel, args);
      }
      catch (Exception ex)
      {
        IMethodCallMessage mcm = (IMethodCallMessage) reqMsg;
        message1 = (IMessage) new ReturnMessage(ex, mcm);
        if (reqMsg != null)
          ((ReturnMessage) message1).SetLogicalCallContext((LogicalCallContext) reqMsg.Properties[(object) Message.CallContextKey]);
      }
      return message1;
    }

    [SecurityCritical]
    internal static object AsyncProcessMessageCallback(object[] args)
    {
      AsyncWorkItem asyncWorkItem = (AsyncWorkItem) null;
      IMessage msg1 = (IMessage) args[0];
      IMessageSink replySink = (IMessageSink) args[1];
      Context oldCtx = (Context) args[2];
      Context context = (Context) args[3];
      if (replySink != null)
        asyncWorkItem = new AsyncWorkItem(replySink, oldCtx);
      IMessage msg2 = msg1;
      int num1 = 0;
      int num2 = 1;
      int num3 = 1;
      int num4 = 1;
      context.NotifyDynamicSinks(msg2, num1 != 0, num2 != 0, num3 != 0, num4 != 0);
      return (object) context.GetServerContextChain().AsyncProcessMessage(msg1, (IMessageSink) asyncWorkItem);
    }

    [SecurityCritical]
    public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
    {
      IMessage msg = InternalSink.ValidateMessage(reqMsg);
      object[] args = new object[4];
      IMessageCtrl messageCtrl = (IMessageCtrl) null;
      if (msg != null)
      {
        if (replySink != null)
          replySink.SyncProcessMessage(msg);
      }
      else
      {
        ServerIdentity serverIdentity = InternalSink.GetServerIdentity(reqMsg);
        if (RemotingServices.CORProfilerTrackRemotingAsync())
        {
          Guid id = Guid.Empty;
          if (RemotingServices.CORProfilerTrackRemotingCookie())
          {
            object obj = reqMsg.Properties[(object) "CORProfilerCookie"];
            if (obj != null)
              id = (Guid) obj;
          }
          RemotingServices.CORProfilerRemotingServerReceivingMessage(id, true);
          if (replySink != null)
            replySink = (IMessageSink) new ServerAsyncReplyTerminatorSink(replySink);
        }
        Context serverContext = serverIdentity.ServerContext;
        if (serverContext.IsThreadPoolAware)
        {
          args[0] = (object) reqMsg;
          args[1] = (object) replySink;
          args[2] = (object) Thread.CurrentContext;
          args[3] = (object) serverContext;
          InternalCrossContextDelegate ftnToCall = new InternalCrossContextDelegate(CrossContextChannel.AsyncProcessMessageCallback);
          messageCtrl = (IMessageCtrl) Thread.CurrentThread.InternalCrossContextCallback(serverContext, ftnToCall, args);
        }
        else
          ThreadPool.QueueUserWorkItem(new WaitCallback(new AsyncWorkItem(reqMsg, replySink, Thread.CurrentContext, serverIdentity).FinishAsyncWork));
      }
      return messageCtrl;
    }

    [SecurityCritical]
    internal static object DoAsyncDispatchCallback(object[] args)
    {
      AsyncWorkItem asyncWorkItem = (AsyncWorkItem) null;
      IMessage msg = (IMessage) args[0];
      IMessageSink replySink = (IMessageSink) args[1];
      Context oldCtx = (Context) args[2];
      Context context = (Context) args[3];
      if (replySink != null)
        asyncWorkItem = new AsyncWorkItem(replySink, oldCtx);
      return (object) context.GetServerContextChain().AsyncProcessMessage(msg, (IMessageSink) asyncWorkItem);
    }

    [SecurityCritical]
    internal static IMessageCtrl DoAsyncDispatch(IMessage reqMsg, IMessageSink replySink)
    {
      object[] args = new object[4];
      ServerIdentity serverIdentity = InternalSink.GetServerIdentity(reqMsg);
      if (RemotingServices.CORProfilerTrackRemotingAsync())
      {
        Guid id = Guid.Empty;
        if (RemotingServices.CORProfilerTrackRemotingCookie())
        {
          object obj = reqMsg.Properties[(object) "CORProfilerCookie"];
          if (obj != null)
            id = (Guid) obj;
        }
        RemotingServices.CORProfilerRemotingServerReceivingMessage(id, true);
        if (replySink != null)
          replySink = (IMessageSink) new ServerAsyncReplyTerminatorSink(replySink);
      }
      Context serverContext = serverIdentity.ServerContext;
      args[0] = (object) reqMsg;
      args[1] = (object) replySink;
      args[2] = (object) Thread.CurrentContext;
      args[3] = (object) serverContext;
      InternalCrossContextDelegate ftnToCall = new InternalCrossContextDelegate(CrossContextChannel.DoAsyncDispatchCallback);
      return (IMessageCtrl) Thread.CurrentThread.InternalCrossContextCallback(serverContext, ftnToCall, args);
    }
  }
}
