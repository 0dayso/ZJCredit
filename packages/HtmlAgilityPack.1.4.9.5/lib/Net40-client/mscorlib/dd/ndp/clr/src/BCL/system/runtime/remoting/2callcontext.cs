// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.LogicalCallContext
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Principal;

namespace System.Runtime.Remoting.Messaging
{
  /// <summary>提供在进行远程方法调用期间用执行代码路径传送的一组属性。</summary>
  [SecurityCritical]
  [ComVisible(true)]
  [Serializable]
  public sealed class LogicalCallContext : ISerializable, ICloneable
  {
    private static Type s_callContextType = typeof (LogicalCallContext);
    private const string s_CorrelationMgrSlotName = "System.Diagnostics.Trace.CorrelationManagerSlot";
    private Hashtable m_Datastore;
    private CallContextRemotingData m_RemotingData;
    private CallContextSecurityData m_SecurityData;
    private object m_HostContext;
    private bool m_IsCorrelationMgr;
    private Header[] _sendHeaders;
    private Header[] _recvHeaders;

    /// <summary>获取一个值，该值指示当前的 <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> 是否包含信息。</summary>
    /// <returns>布尔值，它指示当前的 <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> 是否包含信息。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public bool HasInfo
    {
      [SecurityCritical] get
      {
        bool flag = false;
        if (this.m_RemotingData != null && this.m_RemotingData.HasInfo || this.m_SecurityData != null && this.m_SecurityData.HasInfo || (this.m_HostContext != null || this.HasUserData))
          flag = true;
        return flag;
      }
    }

    private bool HasUserData
    {
      get
      {
        if (this.m_Datastore != null)
          return this.m_Datastore.Count > 0;
        return false;
      }
    }

    internal CallContextRemotingData RemotingData
    {
      get
      {
        if (this.m_RemotingData == null)
          this.m_RemotingData = new CallContextRemotingData();
        return this.m_RemotingData;
      }
    }

    internal CallContextSecurityData SecurityData
    {
      get
      {
        if (this.m_SecurityData == null)
          this.m_SecurityData = new CallContextSecurityData();
        return this.m_SecurityData;
      }
    }

    internal object HostContext
    {
      get
      {
        return this.m_HostContext;
      }
      set
      {
        this.m_HostContext = value;
      }
    }

    private Hashtable Datastore
    {
      get
      {
        if (this.m_Datastore == null)
          this.m_Datastore = new Hashtable();
        return this.m_Datastore;
      }
    }

    internal IPrincipal Principal
    {
      get
      {
        if (this.m_SecurityData != null)
          return this.m_SecurityData.Principal;
        return (IPrincipal) null;
      }
      [SecurityCritical] set
      {
        this.SecurityData.Principal = value;
      }
    }

    internal LogicalCallContext()
    {
    }

    [SecurityCritical]
    internal LogicalCallContext(SerializationInfo info, StreamingContext context)
    {
      SerializationInfoEnumerator enumerator = info.GetEnumerator();
      while (enumerator.MoveNext())
      {
        if (enumerator.Name.Equals("__RemotingData"))
          this.m_RemotingData = (CallContextRemotingData) enumerator.Value;
        else if (enumerator.Name.Equals("__SecurityData"))
        {
          if (context.State == StreamingContextStates.CrossAppDomain)
            this.m_SecurityData = (CallContextSecurityData) enumerator.Value;
        }
        else if (enumerator.Name.Equals("__HostContext"))
          this.m_HostContext = enumerator.Value;
        else if (enumerator.Name.Equals("__CorrelationMgrSlotPresent"))
          this.m_IsCorrelationMgr = (bool) enumerator.Value;
        else
          this.Datastore[(object) enumerator.Name] = enumerator.Value;
      }
    }

    /// <summary>用序列化当前 <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> 所需的数据来填充指定的 <see cref="T:System.Runtime.Serialization.SerializationInfo" />。</summary>
    /// <param name="info">要填充数据的 <see cref="T:System.Runtime.Serialization.SerializationInfo" />。</param>
    /// <param name="context">有关序列化的源或目标的上下文信息。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="info" /> 为 null。</exception>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有 SerializationFormatter 权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter, Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      info.SetType(LogicalCallContext.s_callContextType);
      if (this.m_RemotingData != null)
        info.AddValue("__RemotingData", (object) this.m_RemotingData);
      if (this.m_SecurityData != null && context.State == StreamingContextStates.CrossAppDomain)
        info.AddValue("__SecurityData", (object) this.m_SecurityData);
      if (this.m_HostContext != null)
        info.AddValue("__HostContext", this.m_HostContext);
      if (this.m_IsCorrelationMgr)
        info.AddValue("__CorrelationMgrSlotPresent", this.m_IsCorrelationMgr);
      if (!this.HasUserData)
        return;
      IDictionaryEnumerator enumerator = this.m_Datastore.GetEnumerator();
      while (enumerator.MoveNext())
        info.AddValue((string) enumerator.Key, enumerator.Value);
    }

    /// <summary>创建作为当前实例副本的新对象。</summary>
    /// <returns>作为此实例副本的新对象。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public object Clone()
    {
      LogicalCallContext logicalCallContext = new LogicalCallContext();
      if (this.m_RemotingData != null)
        logicalCallContext.m_RemotingData = (CallContextRemotingData) this.m_RemotingData.Clone();
      if (this.m_SecurityData != null)
        logicalCallContext.m_SecurityData = (CallContextSecurityData) this.m_SecurityData.Clone();
      if (this.m_HostContext != null)
        logicalCallContext.m_HostContext = this.m_HostContext;
      logicalCallContext.m_IsCorrelationMgr = this.m_IsCorrelationMgr;
      if (this.HasUserData)
      {
        IDictionaryEnumerator enumerator = this.m_Datastore.GetEnumerator();
        if (!this.m_IsCorrelationMgr)
        {
          while (enumerator.MoveNext())
            logicalCallContext.Datastore[(object) (string) enumerator.Key] = enumerator.Value;
        }
        else
        {
          while (enumerator.MoveNext())
          {
            string str = (string) enumerator.Key;
            if (str.Equals("System.Diagnostics.Trace.CorrelationManagerSlot"))
              logicalCallContext.Datastore[(object) str] = ((ICloneable) enumerator.Value).Clone();
            else
              logicalCallContext.Datastore[(object) str] = enumerator.Value;
          }
        }
      }
      return (object) logicalCallContext;
    }

    [SecurityCritical]
    internal void Merge(LogicalCallContext lc)
    {
      if (lc == null || this == lc || !lc.HasUserData)
        return;
      IDictionaryEnumerator enumerator = lc.Datastore.GetEnumerator();
      while (enumerator.MoveNext())
        this.Datastore[(object) (string) enumerator.Key] = enumerator.Value;
    }

    /// <summary>清空具有指定名称的数据槽。</summary>
    /// <param name="name">要清空的数据槽的名称。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public void FreeNamedDataSlot(string name)
    {
      this.Datastore.Remove((object) name);
    }

    /// <summary>检索与当前实例中的指定名称关联的对象。</summary>
    /// <returns>逻辑调用上下文中与指定名称关联的对象。</returns>
    /// <param name="name">调用上下文中的项的名称。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public object GetData(string name)
    {
      return this.Datastore[(object) name];
    }

    /// <summary>存储当前实例中的指定对象，并将其与指定名称关联。</summary>
    /// <param name="name">要与新项关联的调用上下文中的名称。</param>
    /// <param name="data">要存储在调用上下文中的对象。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public void SetData(string name, object data)
    {
      this.Datastore[(object) name] = data;
      if (!name.Equals("System.Diagnostics.Trace.CorrelationManagerSlot"))
        return;
      this.m_IsCorrelationMgr = true;
    }

    private Header[] InternalGetOutgoingHeaders()
    {
      Header[] headerArray = this._sendHeaders;
      this._sendHeaders = (Header[]) null;
      this._recvHeaders = (Header[]) null;
      return headerArray;
    }

    internal void InternalSetHeaders(Header[] headers)
    {
      this._sendHeaders = headers;
      this._recvHeaders = (Header[]) null;
    }

    internal Header[] InternalGetHeaders()
    {
      if (this._sendHeaders != null)
        return this._sendHeaders;
      return this._recvHeaders;
    }

    [SecurityCritical]
    internal IPrincipal RemovePrincipalIfNotSerializable()
    {
      IPrincipal principal = this.Principal;
      if (principal != null && !principal.GetType().IsSerializable)
        this.Principal = (IPrincipal) null;
      return principal;
    }

    [SecurityCritical]
    internal void PropagateOutgoingHeadersToMessage(IMessage msg)
    {
      Header[] outgoingHeaders = this.InternalGetOutgoingHeaders();
      if (outgoingHeaders == null)
        return;
      IDictionary properties = msg.Properties;
      foreach (Header header in outgoingHeaders)
      {
        if (header != null)
        {
          string propertyKeyForHeader = LogicalCallContext.GetPropertyKeyForHeader(header);
          properties[(object) propertyKeyForHeader] = (object) header;
        }
      }
    }

    internal static string GetPropertyKeyForHeader(Header header)
    {
      if (header == null)
        return (string) null;
      if (header.HeaderNamespace != null)
        return header.Name + ", " + header.HeaderNamespace;
      return header.Name;
    }

    [SecurityCritical]
    internal void PropagateIncomingHeadersToCallContext(IMessage msg)
    {
      IInternalMessage internalMessage = msg as IInternalMessage;
      if (internalMessage != null && !internalMessage.HasProperties())
        return;
      IDictionaryEnumerator enumerator = msg.Properties.GetEnumerator();
      int length = 0;
      while (enumerator.MoveNext())
      {
        if (!((string) enumerator.Key).StartsWith("__", StringComparison.Ordinal) && enumerator.Value is Header)
          ++length;
      }
      Header[] headerArray = (Header[]) null;
      if (length > 0)
      {
        headerArray = new Header[length];
        int num = 0;
        enumerator.Reset();
        while (enumerator.MoveNext())
        {
          if (!((string) enumerator.Key).StartsWith("__", StringComparison.Ordinal))
          {
            Header header = enumerator.Value as Header;
            if (header != null)
              headerArray[num++] = header;
          }
        }
      }
      this._recvHeaders = headerArray;
      this._sendHeaders = (Header[]) null;
    }

    internal struct Reader
    {
      private LogicalCallContext m_ctx;

      public bool IsNull
      {
        get
        {
          return this.m_ctx == null;
        }
      }

      public bool HasInfo
      {
        get
        {
          if (!this.IsNull)
            return this.m_ctx.HasInfo;
          return false;
        }
      }

      public IPrincipal Principal
      {
        get
        {
          if (!this.IsNull)
            return this.m_ctx.Principal;
          return (IPrincipal) null;
        }
      }

      public object HostContext
      {
        get
        {
          if (!this.IsNull)
            return this.m_ctx.HostContext;
          return (object) null;
        }
      }

      public Reader(LogicalCallContext ctx)
      {
        this.m_ctx = ctx;
      }

      public LogicalCallContext Clone()
      {
        return (LogicalCallContext) this.m_ctx.Clone();
      }

      [SecurityCritical]
      public object GetData(string name)
      {
        if (!this.IsNull)
          return this.m_ctx.GetData(name);
        return (object) null;
      }
    }
  }
}
