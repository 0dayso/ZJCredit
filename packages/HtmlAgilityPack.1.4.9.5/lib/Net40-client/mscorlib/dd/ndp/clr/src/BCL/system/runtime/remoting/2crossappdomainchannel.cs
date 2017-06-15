// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.CrossAppDomainSink
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.IO;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Security.Principal;
using System.Threading;

namespace System.Runtime.Remoting.Channels
{
  internal class CrossAppDomainSink : InternalSink, IMessageSink
  {
    private static object staticSyncObject = new object();
    private static InternalCrossContextDelegate s_xctxDel = new InternalCrossContextDelegate(CrossAppDomainSink.DoTransitionDispatchCallback);
    internal const int GROW_BY = 8;
    internal static volatile int[] _sinkKeys;
    internal static volatile CrossAppDomainSink[] _sinks;
    internal const string LCC_DATA_KEY = "__xADCall";
    internal CrossAppDomainData _xadData;

    public IMessageSink NextSink
    {
      [SecurityCritical] get
      {
        return (IMessageSink) null;
      }
    }

    [SecuritySafeCritical]
    static CrossAppDomainSink()
    {
    }

    internal CrossAppDomainSink(CrossAppDomainData xadData)
    {
      this._xadData = xadData;
    }

    internal static void GrowArrays(int oldSize)
    {
      if (CrossAppDomainSink._sinks == null)
      {
        CrossAppDomainSink._sinks = new CrossAppDomainSink[8];
        CrossAppDomainSink._sinkKeys = new int[8];
      }
      else
      {
        CrossAppDomainSink[] crossAppDomainSinkArray = new CrossAppDomainSink[CrossAppDomainSink._sinks.Length + 8];
        int[] numArray = new int[CrossAppDomainSink._sinkKeys.Length + 8];
        Array.Copy((Array) CrossAppDomainSink._sinks, (Array) crossAppDomainSinkArray, CrossAppDomainSink._sinks.Length);
        Array.Copy((Array) CrossAppDomainSink._sinkKeys, (Array) numArray, CrossAppDomainSink._sinkKeys.Length);
        CrossAppDomainSink._sinks = crossAppDomainSinkArray;
        CrossAppDomainSink._sinkKeys = numArray;
      }
    }

    internal static CrossAppDomainSink FindOrCreateSink(CrossAppDomainData xadData)
    {
      lock (CrossAppDomainSink.staticSyncObject)
      {
        int local_2 = xadData.DomainID;
        if (CrossAppDomainSink._sinks == null)
          CrossAppDomainSink.GrowArrays(0);
        int local_3 = 0;
        while (CrossAppDomainSink._sinks[local_3] != null)
        {
          if (CrossAppDomainSink._sinkKeys[local_3] == local_2)
            return CrossAppDomainSink._sinks[local_3];
          ++local_3;
          if (local_3 == CrossAppDomainSink._sinks.Length)
          {
            CrossAppDomainSink.GrowArrays(local_3);
            break;
          }
        }
        CrossAppDomainSink._sinks[local_3] = new CrossAppDomainSink(xadData);
        CrossAppDomainSink._sinkKeys[local_3] = local_2;
        return CrossAppDomainSink._sinks[local_3];
      }
    }

    internal static void DomainUnloaded(int domainID)
    {
      int num = domainID;
      lock (CrossAppDomainSink.staticSyncObject)
      {
        if (CrossAppDomainSink._sinks == null)
          return;
        int local_3 = 0;
        int local_4 = -1;
        while (CrossAppDomainSink._sinks[local_3] != null)
        {
          if (CrossAppDomainSink._sinkKeys[local_3] == num)
            local_4 = local_3;
          ++local_3;
          if (local_3 == CrossAppDomainSink._sinks.Length)
            break;
        }
        if (local_4 == -1)
          return;
        CrossAppDomainSink._sinkKeys[local_4] = CrossAppDomainSink._sinkKeys[local_3 - 1];
        CrossAppDomainSink._sinks[local_4] = CrossAppDomainSink._sinks[local_3 - 1];
        CrossAppDomainSink._sinkKeys[local_3 - 1] = 0;
        CrossAppDomainSink._sinks[local_3 - 1] = (CrossAppDomainSink) null;
      }
    }

    [SecurityCritical]
    internal static byte[] DoDispatch(byte[] reqStmBuff, SmuggledMethodCallMessage smuggledMcm, out SmuggledMethodReturnMessage smuggledMrm)
    {
      IMessage msg1;
      if (smuggledMcm != null)
      {
        ArrayList deserializedArgs = smuggledMcm.FixupForNewAppDomain();
        msg1 = (IMessage) new MethodCall(smuggledMcm, deserializedArgs);
      }
      else
        msg1 = CrossAppDomainSerializer.DeserializeMessage(new MemoryStream(reqStmBuff));
      LogicalCallContext logicalCallContext1 = Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext;
      string name1 = "__xADCall";
      // ISSUE: variable of a boxed type
      __Boxed<bool> local = (ValueType) true;
      logicalCallContext1.SetData(name1, (object) local);
      IMessage msg2 = ChannelServices.SyncDispatchMessage(msg1);
      string name2 = "__xADCall";
      logicalCallContext1.FreeNamedDataSlot(name2);
      smuggledMrm = SmuggledMethodReturnMessage.SmuggleIfPossible(msg2);
      if (smuggledMrm != null)
        return (byte[]) null;
      if (msg2 == null)
        return (byte[]) null;
      LogicalCallContext logicalCallContext2 = (LogicalCallContext) msg2.Properties[(object) Message.CallContextKey];
      if (logicalCallContext2 != null && logicalCallContext2.Principal != null)
        logicalCallContext2.Principal = (IPrincipal) null;
      return CrossAppDomainSerializer.SerializeMessage(msg2).GetBuffer();
    }

    [SecurityCritical]
    internal static object DoTransitionDispatchCallback(object[] args)
    {
      byte[] reqStmBuff = (byte[]) args[0];
      SmuggledMethodCallMessage smuggledMcm = (SmuggledMethodCallMessage) args[1];
      SmuggledMethodReturnMessage smuggledMrm = (SmuggledMethodReturnMessage) null;
      byte[] numArray;
      try
      {
        numArray = CrossAppDomainSink.DoDispatch(reqStmBuff, smuggledMcm, out smuggledMrm);
      }
      catch (Exception ex)
      {
        ErrorMessage errorMessage = new ErrorMessage();
        numArray = CrossAppDomainSerializer.SerializeMessage((IMessage) new ReturnMessage(ex, (IMethodCallMessage) errorMessage)).GetBuffer();
      }
      args[2] = (object) smuggledMrm;
      return (object) numArray;
    }

    [SecurityCritical]
    internal byte[] DoTransitionDispatch(byte[] reqStmBuff, SmuggledMethodCallMessage smuggledMcm, out SmuggledMethodReturnMessage smuggledMrm)
    {
      object[] args = new object[3]{ (object) reqStmBuff, (object) smuggledMcm, null };
      byte[] numArray = (byte[]) Thread.CurrentThread.InternalCrossContextCallback((Context) null, this._xadData.ContextID, this._xadData.DomainID, CrossAppDomainSink.s_xctxDel, args);
      smuggledMrm = (SmuggledMethodReturnMessage) args[2];
      return numArray;
    }

    [SecurityCritical]
    public virtual IMessage SyncProcessMessage(IMessage reqMsg)
    {
      IMessage message1 = InternalSink.ValidateMessage(reqMsg);
      if (message1 != null)
        return message1;
      IPrincipal principal = (IPrincipal) null;
      IMessage message2 = (IMessage) null;
      try
      {
        IMethodCallMessage methodCallMessage = reqMsg as IMethodCallMessage;
        if (methodCallMessage != null)
        {
          LogicalCallContext logicalCallContext = methodCallMessage.LogicalCallContext;
          if (logicalCallContext != null)
            principal = logicalCallContext.RemovePrincipalIfNotSerializable();
        }
        MemoryStream memoryStream = (MemoryStream) null;
        SmuggledMethodCallMessage smuggledMcm = SmuggledMethodCallMessage.SmuggleIfPossible(reqMsg);
        if (smuggledMcm == null)
          memoryStream = CrossAppDomainSerializer.SerializeMessage(reqMsg);
        LogicalCallContext callCtx = CallContext.SetLogicalCallContext((LogicalCallContext) null);
        byte[] buffer = (byte[]) null;
        SmuggledMethodReturnMessage smuggledMrm;
        try
        {
          buffer = smuggledMcm == null ? this.DoTransitionDispatch(memoryStream.GetBuffer(), (SmuggledMethodCallMessage) null, out smuggledMrm) : this.DoTransitionDispatch((byte[]) null, smuggledMcm, out smuggledMrm);
        }
        finally
        {
          CallContext.SetLogicalCallContext(callCtx);
        }
        if (smuggledMrm != null)
        {
          ArrayList deserializedArgs = smuggledMrm.FixupForNewAppDomain();
          message2 = (IMessage) new MethodResponse((IMethodCallMessage) reqMsg, smuggledMrm, deserializedArgs);
        }
        else if (buffer != null)
          message2 = CrossAppDomainSerializer.DeserializeMessage(new MemoryStream(buffer), reqMsg as IMethodCallMessage);
      }
      catch (Exception ex1)
      {
        try
        {
          message2 = (IMessage) new ReturnMessage(ex1, reqMsg as IMethodCallMessage);
        }
        catch (Exception ex2)
        {
        }
      }
      if (principal != null)
      {
        IMethodReturnMessage methodReturnMessage = message2 as IMethodReturnMessage;
        if (methodReturnMessage != null)
          methodReturnMessage.LogicalCallContext.Principal = principal;
      }
      return message2;
    }

    [SecurityCritical]
    public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
    {
      ThreadPool.QueueUserWorkItem(new WaitCallback(new ADAsyncWorkItem(reqMsg, (IMessageSink) this, replySink).FinishAsyncWork));
      return (IMessageCtrl) null;
    }
  }
}
