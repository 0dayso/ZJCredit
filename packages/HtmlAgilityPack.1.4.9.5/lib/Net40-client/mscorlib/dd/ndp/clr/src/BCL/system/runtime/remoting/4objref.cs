// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.ObjRef
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting
{
  /// <summary>存储生成代理以与远程对象通信所需的所有相关信息。</summary>
  [SecurityCritical]
  [ComVisible(true)]
  [Serializable]
  [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
  public class ObjRef : IObjectReference, ISerializable
  {
    private static Type orType = typeof (ObjRef);
    internal const int FLG_MARSHALED_OBJECT = 1;
    internal const int FLG_WELLKNOWN_OBJREF = 2;
    internal const int FLG_LITE_OBJREF = 4;
    internal const int FLG_PROXY_ATTRIBUTE = 8;
    internal string uri;
    internal IRemotingTypeInfo typeInfo;
    internal IEnvoyInfo envoyInfo;
    internal IChannelInfo channelInfo;
    internal int objrefFlags;
    internal GCHandle srvIdentity;
    internal int domainID;

    /// <summary>获取或设置特定对象实例的 URI。</summary>
    /// <returns>特定对象实例的 URI。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public virtual string URI
    {
      get
      {
        return this.uri;
      }
      set
      {
        this.uri = value;
      }
    }

    /// <summary>获取或设置 <see cref="T:System.Runtime.Remoting.ObjRef" /> 描述的对象的 <see cref="T:System.Runtime.Remoting.IRemotingTypeInfo" />。</summary>
    /// <returns>
    /// <see cref="T:System.Runtime.Remoting.ObjRef" /> 描述的对象的 <see cref="T:System.Runtime.Remoting.IRemotingTypeInfo" />。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public virtual IRemotingTypeInfo TypeInfo
    {
      get
      {
        return this.typeInfo;
      }
      set
      {
        this.typeInfo = value;
      }
    }

    /// <summary>获取或设置 <see cref="T:System.Runtime.Remoting.ObjRef" /> 的 <see cref="T:System.Runtime.Remoting.IEnvoyInfo" />。</summary>
    /// <returns>
    /// <see cref="T:System.Runtime.Remoting.ObjRef" /> 的 <see cref="T:System.Runtime.Remoting.IEnvoyInfo" /> 接口。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public virtual IEnvoyInfo EnvoyInfo
    {
      get
      {
        return this.envoyInfo;
      }
      set
      {
        this.envoyInfo = value;
      }
    }

    /// <summary>获取或设置 <see cref="T:System.Runtime.Remoting.ObjRef" /> 的 <see cref="T:System.Runtime.Remoting.IChannelInfo" />。</summary>
    /// <returns>
    /// <see cref="T:System.Runtime.Remoting.ObjRef" /> 的 <see cref="T:System.Runtime.Remoting.IChannelInfo" /> 接口。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public virtual IChannelInfo ChannelInfo
    {
      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)] get
      {
        return this.channelInfo;
      }
      set
      {
        this.channelInfo = value;
      }
    }

    [SecurityCritical]
    private ObjRef(ObjRef o)
    {
      this.uri = o.uri;
      this.typeInfo = o.typeInfo;
      this.envoyInfo = o.envoyInfo;
      this.channelInfo = o.channelInfo;
      this.objrefFlags = o.objrefFlags;
      this.SetServerIdentity(o.GetServerIdentity());
      this.SetDomainID(o.GetDomainID());
    }

    /// <summary>初始化 <see cref="T:System.Runtime.Remoting.ObjRef" /> 类的新实例以引用指定 <see cref="T:System.Type" /> 的指定 <see cref="T:System.MarshalByRefObject" />。</summary>
    /// <param name="o">新的 <see cref="T:System.Runtime.Remoting.ObjRef" /> 实例将引用的对象。</param>
    /// <param name="requestedType">新的 <see cref="T:System.Runtime.Remoting.ObjRef" /> 实例将引用的对象的 <see cref="T:System.Type" />。</param>
    [SecurityCritical]
    public ObjRef(MarshalByRefObject o, Type requestedType)
    {
      if (o == null)
        throw new ArgumentNullException("o");
      RuntimeType requestedType1 = requestedType as RuntimeType;
      if (requestedType != (Type) null && requestedType1 == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
      bool fServer;
      Identity identity = MarshalByRefObject.GetIdentity(o, out fServer);
      this.Init((object) o, identity, requestedType1);
    }

    /// <summary>用序列化数据初始化 <see cref="T:System.Runtime.Remoting.ObjRef" /> 类的新实例。</summary>
    /// <param name="info">承载序列化对象数据的对象。</param>
    /// <param name="context">有关异常的源或目标的上下文信息。</param>
    [SecurityCritical]
    protected ObjRef(SerializationInfo info, StreamingContext context)
    {
      string str = (string) null;
      bool flag = false;
      SerializationInfoEnumerator enumerator = info.GetEnumerator();
      while (enumerator.MoveNext())
      {
        if (enumerator.Name.Equals("uri"))
          this.uri = (string) enumerator.Value;
        else if (enumerator.Name.Equals("typeInfo"))
          this.typeInfo = (IRemotingTypeInfo) enumerator.Value;
        else if (enumerator.Name.Equals("envoyInfo"))
          this.envoyInfo = (IEnvoyInfo) enumerator.Value;
        else if (enumerator.Name.Equals("channelInfo"))
          this.channelInfo = (IChannelInfo) enumerator.Value;
        else if (enumerator.Name.Equals("objrefFlags"))
        {
          object obj = enumerator.Value;
          this.objrefFlags = !(obj.GetType() == typeof (string)) ? (int) obj : ((IConvertible) obj).ToInt32((IFormatProvider) null);
        }
        else if (enumerator.Name.Equals("fIsMarshalled"))
        {
          object obj = enumerator.Value;
          if ((!(obj.GetType() == typeof (string)) ? (int) obj : ((IConvertible) obj).ToInt32((IFormatProvider) null)) == 0)
            flag = true;
        }
        else if (enumerator.Name.Equals("url"))
          str = (string) enumerator.Value;
        else if (enumerator.Name.Equals("SrvIdentity"))
          this.SetServerIdentity((GCHandle) enumerator.Value);
        else if (enumerator.Name.Equals("DomainId"))
          this.SetDomainID((int) enumerator.Value);
      }
      this.objrefFlags = flag ? this.objrefFlags & -2 : this.objrefFlags | 1;
      if (str == null)
        return;
      this.uri = str;
      this.objrefFlags = this.objrefFlags | 4;
    }

    /// <summary>使用默认值初始化 <see cref="T:System.Runtime.Remoting.ObjRef" /> 类的新实例。</summary>
    public ObjRef()
    {
      this.objrefFlags = 0;
    }

    internal void SetServerIdentity(GCHandle hndSrvIdentity)
    {
      this.srvIdentity = hndSrvIdentity;
    }

    internal GCHandle GetServerIdentity()
    {
      return this.srvIdentity;
    }

    internal void SetDomainID(int id)
    {
      this.domainID = id;
    }

    internal int GetDomainID()
    {
      return this.domainID;
    }

    [SecurityCritical]
    internal bool CanSmuggle()
    {
      if (this.GetType() != typeof (ObjRef) || this.IsObjRefLite())
        return false;
      Type type1 = (Type) null;
      if (this.typeInfo != null)
        type1 = this.typeInfo.GetType();
      Type type2 = (Type) null;
      if (this.channelInfo != null)
        type2 = this.channelInfo.GetType();
      if (!(type1 == (Type) null) && !(type1 == typeof (System.Runtime.Remoting.TypeInfo)) && !(type1 == typeof (DynamicTypeInfo)) || (this.envoyInfo != null || !(type2 == (Type) null) && !(type2 == typeof (System.Runtime.Remoting.ChannelInfo))))
        return false;
      if (this.channelInfo != null)
      {
        foreach (object obj in this.channelInfo.ChannelData)
        {
          if (!(obj is CrossAppDomainData))
            return false;
        }
      }
      return true;
    }

    [SecurityCritical]
    internal ObjRef CreateSmuggleableCopy()
    {
      return new ObjRef(this);
    }

    /// <summary>用序列化当前 <see cref="T:System.Runtime.Remoting.ObjRef" /> 实例所需的数据来填充指定的 <see cref="T:System.Runtime.Serialization.SerializationInfo" />。</summary>
    /// <param name="info">要填充数据的 <see cref="T:System.Runtime.Serialization.SerializationInfo" />。</param>
    /// <param name="context">有关序列化的源或目标的上下文信息。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="info" /> 参数为 null。</exception>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有序列化格式化程序权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter, Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      info.SetType(ObjRef.orType);
      if (!this.IsObjRefLite())
      {
        info.AddValue("uri", (object) this.uri, typeof (string));
        info.AddValue("objrefFlags", this.objrefFlags);
        info.AddValue("typeInfo", (object) this.typeInfo, typeof (IRemotingTypeInfo));
        info.AddValue("envoyInfo", (object) this.envoyInfo, typeof (IEnvoyInfo));
        info.AddValue("channelInfo", (object) this.GetChannelInfoHelper(), typeof (IChannelInfo));
      }
      else
        info.AddValue("url", (object) this.uri, typeof (string));
    }

    [SecurityCritical]
    private IChannelInfo GetChannelInfoHelper()
    {
      System.Runtime.Remoting.ChannelInfo channelInfo1 = this.channelInfo as System.Runtime.Remoting.ChannelInfo;
      if (channelInfo1 == null)
        return this.channelInfo;
      object[] channelData = channelInfo1.ChannelData;
      if (channelData == null)
        return (IChannelInfo) channelInfo1;
      string[] strArray = (string[]) CallContext.GetData("__bashChannelUrl");
      if (strArray == null)
        return (IChannelInfo) channelInfo1;
      string str1 = strArray[0];
      string str2 = strArray[1];
      System.Runtime.Remoting.ChannelInfo channelInfo2 = new System.Runtime.Remoting.ChannelInfo();
      channelInfo2.ChannelData = new object[channelData.Length];
      for (int index = 0; index < channelData.Length; ++index)
      {
        channelInfo2.ChannelData[index] = channelData[index];
        ChannelDataStore channelDataStore1 = channelInfo2.ChannelData[index] as ChannelDataStore;
        if (channelDataStore1 != null)
        {
          string[] channelUris = channelDataStore1.ChannelUris;
          if (channelUris != null && channelUris.Length == 1 && channelUris[0].Equals(str1))
          {
            ChannelDataStore channelDataStore2 = channelDataStore1.InternalShallowCopy();
            channelDataStore2.ChannelUris = new string[1];
            channelDataStore2.ChannelUris[0] = str2;
            channelInfo2.ChannelData[index] = (object) channelDataStore2;
          }
        }
      }
      return (IChannelInfo) channelInfo2;
    }

    /// <summary>返回对 <see cref="T:System.Runtime.Remoting.ObjRef" /> 描述的远程对象的引用。</summary>
    /// <returns>对 <see cref="T:System.Runtime.Remoting.ObjRef" /> 描述的远程对象的引用。</returns>
    /// <param name="context">当前对象所驻留的上下文。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有序列化格式化程序权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter, RemotingConfiguration, Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public virtual object GetRealObject(StreamingContext context)
    {
      return this.GetRealObjectHelper();
    }

    [SecurityCritical]
    internal object GetRealObjectHelper()
    {
      if (!this.IsMarshaledObject())
        return (object) this;
      if (this.IsObjRefLite())
      {
        int num = this.uri.IndexOf(RemotingConfiguration.ApplicationId);
        if (num > 0)
          this.uri = this.uri.Substring(num - 1);
      }
      return this.GetCustomMarshaledCOMObject(RemotingServices.Unmarshal(this, !(this.GetType() == typeof (ObjRef))));
    }

    [SecurityCritical]
    private object GetCustomMarshaledCOMObject(object ret)
    {
      if (this.TypeInfo is DynamicTypeInfo)
      {
        IntPtr pUnk = IntPtr.Zero;
        if (this.IsFromThisProcess())
        {
          if (!this.IsFromThisAppDomain())
          {
            try
            {
              bool fIsURTAggregated;
              pUnk = ((__ComObject) ret).GetIUnknown(out fIsURTAggregated);
              if (pUnk != IntPtr.Zero)
              {
                if (!fIsURTAggregated)
                {
                  string typeName1 = this.TypeInfo.TypeName;
                  string name = (string) null;
                  string assemblyName = (string) null;
                  // ISSUE: explicit reference operation
                  // ISSUE: variable of a reference type
                  string& typeName2 = @name;
                  // ISSUE: explicit reference operation
                  // ISSUE: variable of a reference type
                  string& assemName = @assemblyName;
                  System.Runtime.Remoting.TypeInfo.ParseTypeAndAssembly(typeName1, typeName2, assemName);
                  Assembly assembly = FormatterServices.LoadAssemblyFromStringNoThrow(assemblyName);
                  if (assembly == (Assembly) null)
                    throw new RemotingException(Environment.GetResourceString("Serialization_AssemblyNotFound", (object) assemblyName));
                  Type t = assembly.GetType(name, false, false);
                  if (t != (Type) null && !t.IsVisible)
                    t = (Type) null;
                  object objectForIunknown = Marshal.GetTypedObjectForIUnknown(pUnk, t);
                  if (objectForIunknown != null)
                    ret = objectForIunknown;
                }
              }
            }
            finally
            {
              if (pUnk != IntPtr.Zero)
                Marshal.Release(pUnk);
            }
          }
        }
      }
      return ret;
    }

    internal bool IsMarshaledObject()
    {
      return (this.objrefFlags & 1) == 1;
    }

    internal void SetMarshaledObject()
    {
      this.objrefFlags = this.objrefFlags | 1;
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal bool IsWellKnown()
    {
      return (this.objrefFlags & 2) == 2;
    }

    internal void SetWellKnown()
    {
      this.objrefFlags = this.objrefFlags | 2;
    }

    internal bool HasProxyAttribute()
    {
      return (this.objrefFlags & 8) == 8;
    }

    internal void SetHasProxyAttribute()
    {
      this.objrefFlags = this.objrefFlags | 8;
    }

    internal bool IsObjRefLite()
    {
      return (this.objrefFlags & 4) == 4;
    }

    internal void SetObjRefLite()
    {
      this.objrefFlags = this.objrefFlags | 4;
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    private CrossAppDomainData GetAppDomainChannelData()
    {
      int index = 0;
      for (; index < this.ChannelInfo.ChannelData.Length; ++index)
      {
        CrossAppDomainData crossAppDomainData = this.ChannelInfo.ChannelData[index] as CrossAppDomainData;
        if (crossAppDomainData != null)
          return crossAppDomainData;
      }
      return (CrossAppDomainData) null;
    }

    /// <summary>返回一个布尔值，该值指示当前的 <see cref="T:System.Runtime.Remoting.ObjRef" /> 实例是否引用位于当前进程中的对象。</summary>
    /// <returns>一个布尔值，指示当前的 <see cref="T:System.Runtime.Remoting.ObjRef" /> 实例是否引用位于当前进程中的对象。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    public bool IsFromThisProcess()
    {
      if (this.IsWellKnown())
        return false;
      CrossAppDomainData domainChannelData = this.GetAppDomainChannelData();
      if (domainChannelData != null)
        return domainChannelData.IsFromThisProcess();
      return false;
    }

    /// <summary>返回一个布尔值，该值指示当前的 <see cref="T:System.Runtime.Remoting.ObjRef" /> 实例是否引用位于当前 <see cref="T:System.AppDomain" /> 中的对象。</summary>
    /// <returns>一个布尔值，指示当前的 <see cref="T:System.Runtime.Remoting.ObjRef" /> 实例是否引用位于当前 <see cref="T:System.AppDomain" /> 中的对象。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public bool IsFromThisAppDomain()
    {
      CrossAppDomainData domainChannelData = this.GetAppDomainChannelData();
      if (domainChannelData != null)
        return domainChannelData.IsFromThisAppDomain();
      return false;
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal int GetServerDomainId()
    {
      if (!this.IsFromThisProcess())
        return 0;
      return this.GetAppDomainChannelData().DomainID;
    }

    [SecurityCritical]
    internal IntPtr GetServerContext(out int domainId)
    {
      IntPtr num = IntPtr.Zero;
      domainId = 0;
      if (this.IsFromThisProcess())
      {
        CrossAppDomainData domainChannelData = this.GetAppDomainChannelData();
        domainId = domainChannelData.DomainID;
        if (AppDomain.IsDomainIdValid(domainChannelData.DomainID))
          num = domainChannelData.ContextID;
      }
      return num;
    }

    [SecurityCritical]
    internal void Init(object o, Identity idObj, RuntimeType requestedType)
    {
      this.uri = idObj.URI;
      MarshalByRefObject tpOrObject = idObj.TPOrObject;
      RuntimeType runtimeType1 = RemotingServices.IsTransparentProxy((object) tpOrObject) ? (RuntimeType) RemotingServices.GetRealProxy((object) tpOrObject).GetProxiedType() : (RuntimeType) tpOrObject.GetType();
      RuntimeType runtimeType2 = (RuntimeType) null == requestedType ? runtimeType1 : requestedType;
      if ((RuntimeType) null != requestedType && !requestedType.IsAssignableFrom((System.Reflection.TypeInfo) runtimeType1) && !typeof (IMessageSink).IsAssignableFrom((Type) runtimeType1))
        throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_InvalidRequestedType"), (object) requestedType.ToString()));
      this.TypeInfo = !runtimeType1.IsCOMObject ? (IRemotingTypeInfo) InternalRemotingServices.GetReflectionCachedData(runtimeType2).TypeInfo : (IRemotingTypeInfo) new DynamicTypeInfo(runtimeType2);
      if (!idObj.IsWellKnown())
      {
        this.EnvoyInfo = System.Runtime.Remoting.EnvoyInfo.CreateEnvoyInfo(idObj as ServerIdentity);
        IChannelInfo channelInfo = (IChannelInfo) new System.Runtime.Remoting.ChannelInfo();
        if (o is AppDomain)
        {
          object[] channelData = channelInfo.ChannelData;
          int length1 = channelData.Length;
          object[] objArray1 = new object[length1];
          object[] objArray2 = objArray1;
          int length2 = length1;
          Array.Copy((Array) channelData, (Array) objArray2, length2);
          for (int index = 0; index < length1; ++index)
          {
            if (!(objArray1[index] is CrossAppDomainData))
              objArray1[index] = (object) null;
          }
          channelInfo.ChannelData = objArray1;
        }
        this.ChannelInfo = channelInfo;
        if (runtimeType1.HasProxyAttribute)
          this.SetHasProxyAttribute();
      }
      else
        this.SetWellKnown();
      if (!ObjRef.ShouldUseUrlObjRef())
        return;
      if (this.IsWellKnown())
      {
        this.SetObjRefLite();
      }
      else
      {
        string httpUrlForObject = ChannelServices.FindFirstHttpUrlForObject(this.URI);
        if (httpUrlForObject == null)
          return;
        this.URI = httpUrlForObject;
        this.SetObjRefLite();
      }
    }

    internal static bool ShouldUseUrlObjRef()
    {
      return RemotingConfigHandler.UrlObjRefMode;
    }

    [SecurityCritical]
    internal static bool IsWellFormed(ObjRef objectRef)
    {
      bool flag = true;
      if (objectRef == null || objectRef.URI == null || !objectRef.IsWellKnown() && !objectRef.IsObjRefLite() && (!(objectRef.GetType() != ObjRef.orType) && objectRef.ChannelInfo == null))
        flag = false;
      return flag;
    }
  }
}
