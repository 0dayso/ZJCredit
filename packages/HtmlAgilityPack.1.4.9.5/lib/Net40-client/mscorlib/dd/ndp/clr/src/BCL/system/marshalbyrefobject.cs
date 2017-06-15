// Decompiled with JetBrains decompiler
// Type: System.MarshalByRefObject
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Lifetime;
using System.Security;
using System.Threading;

namespace System
{
  /// <summary>允许在支持远程处理的应用程序中跨应用程序域边界访问对象。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [Serializable]
  public abstract class MarshalByRefObject
  {
    private object __identity;

    private object Identity
    {
      get
      {
        return this.__identity;
      }
      set
      {
        this.__identity = value;
      }
    }

    [SecuritySafeCritical]
    internal IntPtr GetComIUnknown(bool fIsBeingMarshalled)
    {
      return !RemotingServices.IsTransparentProxy((object) this) ? Marshal.GetIUnknownForObject((object) this) : RemotingServices.GetRealProxy((object) this).GetCOMIUnknown(fIsBeingMarshalled);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern IntPtr GetComIUnknown(MarshalByRefObject o);

    internal bool IsInstanceOfType(Type T)
    {
      return T.IsInstanceOfType((object) this);
    }

    internal object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
    {
      Type type = this.GetType();
      if (!type.IsCOMObject)
        throw new InvalidOperationException(Environment.GetResourceString("Arg_InvokeMember"));
      string name1 = name;
      int num = (int) invokeAttr;
      Binder binder1 = binder;
      object[] args1 = args;
      ParameterModifier[] modifiers1 = modifiers;
      CultureInfo culture1 = culture;
      string[] namedParameters1 = namedParameters;
      return type.InvokeMember(name1, (BindingFlags) num, binder1, (object) this, args1, modifiers1, culture1, namedParameters1);
    }

    /// <summary>创建当前 <see cref="T:System.MarshalByRefObject" /> 对象的浅表副本。</summary>
    /// <returns>当前 <see cref="T:System.MarshalByRefObject" /> 对象的浅表副本。</returns>
    /// <param name="cloneIdentity">如果要删除当前 <see cref="T:System.MarshalByRefObject" /> 对象的标识，则为 false，这使该对象在跨远程边界封送时分配一个新标识。值 false 通常比较合适。如果为 true，则将当前 <see cref="T:System.MarshalByRefObject" /> 对象的标识复制到它的克隆项，这会将远程客户端调用路由到远程服务器对象。</param>
    protected MarshalByRefObject MemberwiseClone(bool cloneIdentity)
    {
      MarshalByRefObject marshalByRefObject = (MarshalByRefObject) this.MemberwiseClone();
      if (!cloneIdentity)
        marshalByRefObject.Identity = (object) null;
      return marshalByRefObject;
    }

    [SecuritySafeCritical]
    internal static System.Runtime.Remoting.Identity GetIdentity(MarshalByRefObject obj, out bool fServer)
    {
      fServer = true;
      System.Runtime.Remoting.Identity identity = (System.Runtime.Remoting.Identity) null;
      if (obj != null)
      {
        if (!RemotingServices.IsTransparentProxy((object) obj))
        {
          identity = (System.Runtime.Remoting.Identity) obj.Identity;
        }
        else
        {
          fServer = false;
          identity = RemotingServices.GetRealProxy((object) obj).IdentityObject;
        }
      }
      return identity;
    }

    internal static System.Runtime.Remoting.Identity GetIdentity(MarshalByRefObject obj)
    {
      bool fServer;
      return MarshalByRefObject.GetIdentity(obj, out fServer);
    }

    internal ServerIdentity __RaceSetServerIdentity(ServerIdentity id)
    {
      if (this.__identity == null)
      {
        if (!id.IsContextBound)
          id.RaceSetTransparentProxy((object) this);
        Interlocked.CompareExchange(ref this.__identity, (object) id, (object) null);
      }
      return (ServerIdentity) this.__identity;
    }

    internal void __ResetServerIdentity()
    {
      this.__identity = (object) null;
    }

    /// <summary>检索控制此实例的生存期策略的当前生存期服务对象。</summary>
    /// <returns>
    /// <see cref="T:System.Runtime.Remoting.Lifetime.ILease" /> 类型的对象，用于控制此实例的生存期策略。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration, Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public object GetLifetimeService()
    {
      return (object) LifetimeServices.GetLease(this);
    }

    /// <summary>获取控制此实例的生存期策略的生存期服务对象。</summary>
    /// <returns>
    /// <see cref="T:System.Runtime.Remoting.Lifetime.ILease" /> 类型的对象，用于控制此实例的生存期策略。这是此实例当前的生存期服务对象（如果存在）；否则为初始化为 <see cref="P:System.Runtime.Remoting.Lifetime.LifetimeServices.LeaseManagerPollTime" /> 属性的值的新生存期服务对象。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration, Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public virtual object InitializeLifetimeService()
    {
      return (object) LifetimeServices.GetLeaseInitial(this);
    }

    /// <summary>创建一个对象，该对象包含生成用于与远程对象进行通信的代理所需的全部相关信息。</summary>
    /// <returns>生成代理所需要的信息。</returns>
    /// <param name="requestedType">新的 <see cref="T:System.Runtime.Remoting.ObjRef" /> 将引用的对象的 <see cref="T:System.Type" />。</param>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">此实例不是有效的远程处理对象。</exception>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public virtual ObjRef CreateObjRef(Type requestedType)
    {
      if (this.__identity == null)
        throw new RemotingException(Environment.GetResourceString("Remoting_NoIdentityEntry"));
      return new ObjRef(this, requestedType);
    }

    [SecuritySafeCritical]
    internal bool CanCastToXmlType(string xmlTypeName, string xmlTypeNamespace)
    {
      Type type = SoapServices.GetInteropTypeFromXmlType(xmlTypeName, xmlTypeNamespace);
      if (type == (Type) null)
      {
        string typeNamespace;
        string assemblyName;
        if (!SoapServices.DecodeXmlNamespaceForClrTypeNamespace(xmlTypeNamespace, out typeNamespace, out assemblyName))
          return false;
        string name = typeNamespace == null || typeNamespace.Length <= 0 ? xmlTypeName : typeNamespace + "." + xmlTypeName;
        try
        {
          type = Assembly.Load(assemblyName).GetType(name, false, false);
        }
        catch
        {
          return false;
        }
      }
      if (type != (Type) null)
        return type.IsAssignableFrom(this.GetType());
      return false;
    }

    [SecuritySafeCritical]
    internal static bool CanCastToXmlTypeHelper(RuntimeType castType, MarshalByRefObject o)
    {
      if (castType == (RuntimeType) null)
        throw new ArgumentNullException("castType");
      if (!castType.IsInterface && !castType.IsMarshalByRef)
        return false;
      string xmlType = (string) null;
      string xmlTypeNamespace = (string) null;
      if (!SoapServices.GetXmlTypeForInteropType((Type) castType, out xmlType, out xmlTypeNamespace))
      {
        xmlType = castType.Name;
        xmlTypeNamespace = SoapServices.CodeXmlNamespaceForClrTypeNamespace(castType.Namespace, castType.GetRuntimeAssembly().GetSimpleName());
      }
      return o.CanCastToXmlType(xmlType, xmlTypeNamespace);
    }
  }
}
