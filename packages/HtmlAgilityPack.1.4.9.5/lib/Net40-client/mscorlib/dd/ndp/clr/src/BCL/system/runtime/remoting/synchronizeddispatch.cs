// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Contexts.SynchronizationAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Runtime.Remoting.Contexts
{
  /// <summary>为当前上下文和所有共享同一实例的上下文强制一个同步域。</summary>
  [SecurityCritical]
  [AttributeUsage(AttributeTargets.Class)]
  [ComVisible(true)]
  [Serializable]
  [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
  public class SynchronizationAttribute : ContextAttribute, IContributeServerContextSink, IContributeClientContextSink
  {
    private static readonly int _timeOut = -1;
    /// <summary>指示不能在具有同步的上下文中创建该特性所应用于的类。此字段为常数。</summary>
    public const int NOT_SUPPORTED = 1;
    /// <summary>指示应用此特性的类与该上下文是否有同步无关。此字段为常数。</summary>
    public const int SUPPORTED = 2;
    /// <summary>指示必须在具有同步的上下文中创建该特性所应用于的类。此字段为常数。</summary>
    public const int REQUIRED = 4;
    /// <summary>指示每次都必须在具有该同步属性的新实例的上下文中创建应用此特性的类。此字段为常数。</summary>
    public const int REQUIRES_NEW = 8;
    private const string PROPERTY_NAME = "Synchronization";
    [NonSerialized]
    internal AutoResetEvent _asyncWorkEvent;
    [NonSerialized]
    private RegisteredWaitHandle _waitHandle;
    [NonSerialized]
    internal Queue _workItemQueue;
    [NonSerialized]
    internal bool _locked;
    internal bool _bReEntrant;
    internal int _flavor;
    [NonSerialized]
    private SynchronizationAttribute _cliCtxAttr;
    [NonSerialized]
    private string _syncLcid;
    [NonSerialized]
    private ArrayList _asyncLcidList;

    /// <summary>获取或设置指示实现此 <see cref="T:System.Runtime.Remoting.Contexts.SynchronizationAttribute" /> 实例的 <see cref="T:System.Runtime.Remoting.Contexts.Context" /> 是否被锁定的布尔值。</summary>
    /// <returns>指示实现此 <see cref="T:System.Runtime.Remoting.Contexts.SynchronizationAttribute" /> 实例的 <see cref="T:System.Runtime.Remoting.Contexts.Context" /> 是否被锁定的布尔值。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public virtual bool Locked
    {
      get
      {
        return this._locked;
      }
      set
      {
        this._locked = value;
      }
    }

    /// <summary>获取或设置指示是否需要重入的布尔值。</summary>
    /// <returns>一个布尔值，指示是否需要重入。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public virtual bool IsReEntrant
    {
      get
      {
        return this._bReEntrant;
      }
    }

    internal string SyncCallOutLCID
    {
      get
      {
        return this._syncLcid;
      }
      set
      {
        this._syncLcid = value;
      }
    }

    internal ArrayList AsyncCallOutLCIDList
    {
      get
      {
        return this._asyncLcidList;
      }
    }

    /// <summary>使用默认值初始化 <see cref="T:System.Runtime.Remoting.Contexts.SynchronizationAttribute" /> 类的新实例。</summary>
    public SynchronizationAttribute()
      : this(4, false)
    {
    }

    /// <summary>用指示是否需要重入的布尔值来初始化 <see cref="T:System.Runtime.Remoting.Contexts.SynchronizationAttribute" /> 类的新实例。</summary>
    /// <param name="reEntrant">一个布尔值，指示是否需要重入。</param>
    public SynchronizationAttribute(bool reEntrant)
      : this(4, reEntrant)
    {
    }

    /// <summary>用指示该特性所应用于的对象的行为的标志来初始化 <see cref="T:System.Runtime.Remoting.Contexts.SynchronizationAttribute" /> 类的新实例。</summary>
    /// <param name="flag">一个整数值，指示该特性所应用于的对象的行为。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="flag" /> 参数不是定义的标志之一。</exception>
    public SynchronizationAttribute(int flag)
      : this(flag, false)
    {
    }

    /// <summary>用指示该特性所应用于的对象的行为的标志和指示是否需要重入的布尔值来初始化 <see cref="T:System.Runtime.Remoting.Contexts.SynchronizationAttribute" /> 类的新实例。</summary>
    /// <param name="flag">一个整数值，指示该特性所应用于的对象的行为。</param>
    /// <param name="reEntrant">如果需要重入且必须截获和序列化标注，则为 true；否则为 false。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="flag" /> 参数不是定义的标志之一。</exception>
    public SynchronizationAttribute(int flag, bool reEntrant)
      : base("Synchronization")
    {
      this._bReEntrant = reEntrant;
      switch (flag)
      {
        case 1:
        case 2:
        case 4:
        case 8:
          this._flavor = flag;
          break;
        default:
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"), "flag");
      }
    }

    internal bool IsKnownLCID(IMessage reqMsg)
    {
      string logicalCallId = ((LogicalCallContext) reqMsg.Properties[(object) Message.CallContextKey]).RemotingData.LogicalCallID;
      if (!logicalCallId.Equals(this._syncLcid))
        return this._asyncLcidList.Contains((object) logicalCallId);
      return true;
    }

    internal void Dispose()
    {
      if (this._waitHandle == null)
        return;
      this._waitHandle.Unregister((WaitHandle) null);
    }

    /// <summary>返回一个指示该上下文参数是否满足上下文特性要求的布尔值。</summary>
    /// <returns>如果传入的上下文一切正常，则为 true；否则为 false。</returns>
    /// <param name="ctx">要检查的上下文。</param>
    /// <param name="msg">在构造由该特性标记的上下文绑定对象时收集的信息。在确定是否可以接受该上下文时，<see cref="T:System.Runtime.Remoting.Contexts.SynchronizationAttribute" /> 可以在该上下文中检查、添加和移除属性。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="ctx" /> 或 <paramref name="msg" /> 参数为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    [ComVisible(true)]
    public override bool IsContextOK(Context ctx, IConstructionCallMessage msg)
    {
      if (ctx == null)
        throw new ArgumentNullException("ctx");
      if (msg == null)
        throw new ArgumentNullException("msg");
      bool flag = true;
      if (this._flavor == 8)
      {
        flag = false;
      }
      else
      {
        SynchronizationAttribute synchronizationAttribute = (SynchronizationAttribute) ctx.GetProperty("Synchronization");
        if (this._flavor == 1 && synchronizationAttribute != null || this._flavor == 4 && synchronizationAttribute == null)
          flag = false;
        if (this._flavor == 4)
          this._cliCtxAttr = synchronizationAttribute;
      }
      return flag;
    }

    /// <summary>将 Synchronized 上下文属性添加到指定的 <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" />。</summary>
    /// <param name="ctorMsg">将该属性添加到的 <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" />。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    [ComVisible(true)]
    public override void GetPropertiesForNewContext(IConstructionCallMessage ctorMsg)
    {
      if (this._flavor == 1 || this._flavor == 2 || ctorMsg == null)
        return;
      if (this._cliCtxAttr != null)
      {
        ctorMsg.ContextProperties.Add((object) this._cliCtxAttr);
        this._cliCtxAttr = (SynchronizationAttribute) null;
      }
      else
        ctorMsg.ContextProperties.Add((object) this);
    }

    internal virtual void InitIfNecessary()
    {
      lock (this)
      {
        if (this._asyncWorkEvent != null)
          return;
        this._asyncWorkEvent = new AutoResetEvent(false);
        this._workItemQueue = new Queue();
        this._asyncLcidList = new ArrayList();
        this._waitHandle = ThreadPool.RegisterWaitForSingleObject((WaitHandle) this._asyncWorkEvent, new WaitOrTimerCallback(this.DispatcherCallBack), (object) null, SynchronizationAttribute._timeOut, false);
      }
    }

    private void DispatcherCallBack(object stateIgnored, bool ignored)
    {
      WorkItem work;
      lock (this._workItemQueue)
        work = (WorkItem) this._workItemQueue.Dequeue();
      this.ExecuteWorkItem(work);
      this.HandleWorkCompletion();
    }

    internal virtual void HandleThreadExit()
    {
      this.HandleWorkCompletion();
    }

    internal virtual void HandleThreadReEntry()
    {
      WorkItem work = new WorkItem((IMessage) null, (IMessageSink) null, (IMessageSink) null);
      work.SetDummy();
      this.HandleWorkRequest(work);
    }

    internal virtual void HandleWorkCompletion()
    {
      WorkItem workItem = (WorkItem) null;
      bool flag = false;
      lock (this._workItemQueue)
      {
        if (this._workItemQueue.Count >= 1)
        {
          workItem = (WorkItem) this._workItemQueue.Peek();
          flag = true;
          workItem.SetSignaled();
        }
        else
          this._locked = false;
      }
      if (!flag)
        return;
      if (workItem.IsAsync())
      {
        this._asyncWorkEvent.Set();
      }
      else
      {
        lock (workItem)
          Monitor.Pulse((object) workItem);
      }
    }

    internal virtual void HandleWorkRequest(WorkItem work)
    {
      if (!this.IsNestedCall(work._reqMsg))
      {
        if (work.IsAsync())
        {
          lock (this._workItemQueue)
          {
            work.SetWaiting();
            this._workItemQueue.Enqueue((object) work);
            if (this._locked || this._workItemQueue.Count != 1)
              return;
            work.SetSignaled();
            this._locked = true;
            this._asyncWorkEvent.Set();
          }
        }
        else
        {
          lock (work)
          {
            bool local_0_1;
            lock (this._workItemQueue)
            {
              if (!this._locked && this._workItemQueue.Count == 0)
              {
                this._locked = true;
                local_0_1 = false;
              }
              else
              {
                local_0_1 = true;
                work.SetWaiting();
                this._workItemQueue.Enqueue((object) work);
              }
            }
            if (local_0_1)
            {
              Monitor.Wait((object) work);
              if (!work.IsDummy())
              {
                this.DispatcherCallBack((object) null, true);
              }
              else
              {
                lock (this._workItemQueue)
                  this._workItemQueue.Dequeue();
              }
            }
            else
            {
              if (work.IsDummy())
                return;
              work.SetSignaled();
              this.ExecuteWorkItem(work);
              this.HandleWorkCompletion();
            }
          }
        }
      }
      else
      {
        work.SetSignaled();
        work.Execute();
      }
    }

    internal void ExecuteWorkItem(WorkItem work)
    {
      work.Execute();
    }

    internal bool IsNestedCall(IMessage reqMsg)
    {
      bool flag = false;
      if (!this.IsReEntrant)
      {
        string syncCallOutLcid = this.SyncCallOutLCID;
        if (syncCallOutLcid != null)
        {
          LogicalCallContext logicalCallContext = (LogicalCallContext) reqMsg.Properties[(object) Message.CallContextKey];
          if (logicalCallContext != null && syncCallOutLcid.Equals(logicalCallContext.RemotingData.LogicalCallID))
            flag = true;
        }
        if (!flag && this.AsyncCallOutLCIDList.Count > 0 && this.AsyncCallOutLCIDList.Contains((object) ((LogicalCallContext) reqMsg.Properties[(object) Message.CallContextKey]).RemotingData.LogicalCallID))
          flag = true;
      }
      return flag;
    }

    /// <summary>创建一个同步调度接收器，并将其链接到所提供的位于远程处理调用的服务器端的上下文边界处的接收器链的前面。</summary>
    /// <returns>具有新的同步调度接收器的复合接收器链。</returns>
    /// <param name="nextSink">到目前为止组成的接收链。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public virtual IMessageSink GetServerContextSink(IMessageSink nextSink)
    {
      this.InitIfNecessary();
      return (IMessageSink) new SynchronizedServerContextSink(this, nextSink);
    }

    /// <summary>创建一个标注接收器，并将其链接到所提供的位于远程处理调用客户端的上下文边界处的接收器链的前面。</summary>
    /// <returns>具有新的标注接收器的复合接收器链。</returns>
    /// <param name="nextSink">到目前为止组成的接收链。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public virtual IMessageSink GetClientContextSink(IMessageSink nextSink)
    {
      this.InitIfNecessary();
      return (IMessageSink) new SynchronizedClientContextSink(this, nextSink);
    }
  }
}
