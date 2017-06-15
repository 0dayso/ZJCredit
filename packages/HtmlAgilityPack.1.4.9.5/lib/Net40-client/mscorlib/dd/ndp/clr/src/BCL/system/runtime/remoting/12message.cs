// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.ConstructionCall
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Serialization;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
  /// <summary>实现 <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" /> 接口以创建一条请求消息，该消息构成远程对象上的构造函数调用。</summary>
  [SecurityCritical]
  [CLSCompliant(false)]
  [ComVisible(true)]
  [Serializable]
  public class ConstructionCall : MethodCall, IConstructionCallMessage, IMethodCallMessage, IMethodMessage, IMessage
  {
    internal Type _activationType;
    internal string _activationTypeName;
    internal IList _contextProperties;
    internal object[] _callSiteActivationAttributes;
    internal IActivator _activator;

    /// <summary>获取远程对象的调用方激活特性。</summary>
    /// <returns>类型 <see cref="T:System.Object" /> 的数组，包含远程对象的调用方激活特性。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public object[] CallSiteActivationAttributes
    {
      [SecurityCritical] get
      {
        return this._callSiteActivationAttributes;
      }
    }

    /// <summary>获取要激活的远程对象的类型。</summary>
    /// <returns>要激活的远程对象的 <see cref="T:System.Type" />。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public Type ActivationType
    {
      [SecurityCritical] get
      {
        if (this._activationType == (Type) null && this._activationTypeName != null)
          this._activationType = (Type) RemotingServices.InternalGetTypeFromQualifiedTypeName(this._activationTypeName, false);
        return this._activationType;
      }
    }

    /// <summary>获取要激活的远程对象的完整类型名称。</summary>
    /// <returns>一个 <see cref="T:System.String" />，包含要激活的远程对象的完整类型名称。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public string ActivationTypeName
    {
      [SecurityCritical] get
      {
        return this._activationTypeName;
      }
    }

    /// <summary>获取一个属性列表，这些属性定义要在其中创建远程对象的上下文。</summary>
    /// <returns>一个包含属性列表的 <see cref="T:System.Collections.IList" />，这些属性定义要在其中创建远程对象的上下文。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public IList ContextProperties
    {
      [SecurityCritical] get
      {
        if (this._contextProperties == null)
          this._contextProperties = (IList) new ArrayList();
        return this._contextProperties;
      }
    }

    /// <summary>获取表示远程处理消息属性集合的 <see cref="T:System.Collections.IDictionary" /> 接口。</summary>
    /// <returns>表示远程处理消息属性集合的 <see cref="T:System.Collections.IDictionary" /> 接口。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public override IDictionary Properties
    {
      [SecurityCritical] get
      {
        lock (this)
        {
          if (this.InternalProperties == null)
            this.InternalProperties = (IDictionary) new Hashtable();
          if (this.ExternalProperties == null)
            this.ExternalProperties = (IDictionary) new CCMDictionary((IConstructionCallMessage) this, this.InternalProperties);
          return this.ExternalProperties;
        }
      }
    }

    /// <summary>获取或设置激活远程对象的激活器。</summary>
    /// <returns>激活远程对象的 <see cref="T:System.Runtime.Remoting.Activation.IActivator" />。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public IActivator Activator
    {
      [SecurityCritical] get
      {
        return this._activator;
      }
      [SecurityCritical] set
      {
        this._activator = value;
      }
    }

    /// <summary>从一个远程处理标头数组初始化 <see cref="T:System.Runtime.Remoting.Messaging.ConstructionCall" /> 类的一个新实例。</summary>
    /// <param name="headers">包含键/值对的远程处理标头数组。该数组用于初始化属于“http://schemas.microsoft.com/clr/soap/messageProperties”命名空间的标头的 <see cref="T:System.Runtime.Remoting.Messaging.ConstructionCall" /> 字段。</param>
    public ConstructionCall(Header[] headers)
      : base(headers)
    {
    }

    /// <summary>通过复制现有消息来初始化 <see cref="T:System.Runtime.Remoting.Messaging.ConstructionCall" /> 类的一个新实例。</summary>
    /// <param name="m">远程处理消息。</param>
    public ConstructionCall(IMessage m)
      : base(m)
    {
    }

    internal ConstructionCall(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    [SecurityCritical]
    internal override bool FillSpecialHeader(string key, object value)
    {
      if (key != null)
      {
        if (key.Equals("__ActivationType"))
          this._activationType = (Type) null;
        else if (key.Equals("__ContextProperties"))
          this._contextProperties = (IList) value;
        else if (key.Equals("__CallSiteActivationAttributes"))
          this._callSiteActivationAttributes = (object[]) value;
        else if (key.Equals("__Activator"))
        {
          this._activator = (IActivator) value;
        }
        else
        {
          if (!key.Equals("__ActivationTypeName"))
            return base.FillSpecialHeader(key, value);
          this._activationTypeName = (string) value;
        }
      }
      return true;
    }
  }
}
