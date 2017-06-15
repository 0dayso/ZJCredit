// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.RemotingSurrogateSelector
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Messaging
{
  /// <summary>选择可用于序列化从 <see cref="T:System.MarshalByRefObject" /> 派生的对象的远程处理代理项。</summary>
  [SecurityCritical]
  [ComVisible(true)]
  [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
  public class RemotingSurrogateSelector : ISurrogateSelector
  {
    private static Type s_IMethodCallMessageType = typeof (IMethodCallMessage);
    private static Type s_IMethodReturnMessageType = typeof (IMethodReturnMessage);
    private static Type s_ObjRefType = typeof (ObjRef);
    private RemotingSurrogate _remotingSurrogate = new RemotingSurrogate();
    private ObjRefSurrogate _objRefSurrogate = new ObjRefSurrogate();
    private object _rootObj;
    private ISurrogateSelector _next;
    private ISerializationSurrogate _messageSurrogate;
    private MessageSurrogateFilter _filter;

    /// <summary>获取或设置 <see cref="T:System.Runtime.Remoting.Messaging.RemotingSurrogateSelector" /> 的当前实例的 <see cref="T:System.Runtime.Remoting.Messaging.MessageSurrogateFilter" /> 委托。</summary>
    /// <returns>
    /// <see cref="T:System.Runtime.Remoting.Messaging.RemotingSurrogateSelector" /> 的当前实例的 <see cref="T:System.Runtime.Remoting.Messaging.MessageSurrogateFilter" /> 委托。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public MessageSurrogateFilter Filter
    {
      get
      {
        return this._filter;
      }
      set
      {
        this._filter = value;
      }
    }

    /// <summary>初始化 <see cref="T:System.Runtime.Remoting.Messaging.RemotingSurrogateSelector" /> 类的新实例。</summary>
    public RemotingSurrogateSelector()
    {
      this._messageSurrogate = (ISerializationSurrogate) new MessageSurrogate(this);
    }

    /// <summary>设置位于对象图的根处的对象。</summary>
    /// <param name="obj">位于对象图的根处的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="obj" /> 参数为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public void SetRootObject(object obj)
    {
      if (obj == null)
        throw new ArgumentNullException("obj");
      this._rootObj = obj;
      SoapMessageSurrogate messageSurrogate = this._messageSurrogate as SoapMessageSurrogate;
      if (messageSurrogate == null)
        return;
      messageSurrogate.SetRootObject(this._rootObj);
    }

    /// <summary>返回位于对象图的根处的对象。</summary>
    /// <returns>位于对象图的根处的对象。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public object GetRootObject()
    {
      return this._rootObj;
    }

    /// <summary>将指定的 <see cref="T:System.Runtime.Serialization.ISurrogateSelector" /> 添加到代理项选择器链。</summary>
    /// <param name="selector">要检查的下一个 <see cref="T:System.Runtime.Serialization.ISurrogateSelector" />。</param>
    [SecurityCritical]
    public virtual void ChainSelector(ISurrogateSelector selector)
    {
      this._next = selector;
    }

    /// <summary>返回给定上下文中给定类型的适当代理项。</summary>
    /// <returns>给定上下文中给定类型的适当代理项。</returns>
    /// <param name="type">为其请求代理项的 <see cref="T:System.Type" />。</param>
    /// <param name="context">序列化的源或目标。</param>
    /// <param name="ssout">当该方法返回时，包含适合于指定对象类型的 <see cref="T:System.Runtime.Serialization.ISurrogateSelector" />。该参数未经初始化即被传递。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter, Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public virtual ISerializationSurrogate GetSurrogate(Type type, StreamingContext context, out ISurrogateSelector ssout)
    {
      if (type == (Type) null)
        throw new ArgumentNullException("type");
      if (type.IsMarshalByRef)
      {
        ssout = (ISurrogateSelector) this;
        return (ISerializationSurrogate) this._remotingSurrogate;
      }
      if (RemotingSurrogateSelector.s_IMethodCallMessageType.IsAssignableFrom(type) || RemotingSurrogateSelector.s_IMethodReturnMessageType.IsAssignableFrom(type))
      {
        ssout = (ISurrogateSelector) this;
        return this._messageSurrogate;
      }
      if (RemotingSurrogateSelector.s_ObjRefType.IsAssignableFrom(type))
      {
        ssout = (ISurrogateSelector) this;
        return (ISerializationSurrogate) this._objRefSurrogate;
      }
      if (this._next != null)
        return this._next.GetSurrogate(type, context, out ssout);
      ssout = (ISurrogateSelector) null;
      return (ISerializationSurrogate) null;
    }

    /// <summary>返回代理项选择器链中的下一个 <see cref="T:System.Runtime.Serialization.ISurrogateSelector" />。</summary>
    /// <returns>代理项选择器链中的下一个 <see cref="T:System.Runtime.Serialization.ISurrogateSelector" />。</returns>
    [SecurityCritical]
    public virtual ISurrogateSelector GetNextSelector()
    {
      return this._next;
    }

    /// <summary>设置当前代理项选择器以使用 SOAP 格式。</summary>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public virtual void UseSoapFormat()
    {
      this._messageSurrogate = (ISerializationSurrogate) new SoapMessageSurrogate(this);
      ((SoapMessageSurrogate) this._messageSurrogate).SetRootObject(this._rootObj);
    }
  }
}
