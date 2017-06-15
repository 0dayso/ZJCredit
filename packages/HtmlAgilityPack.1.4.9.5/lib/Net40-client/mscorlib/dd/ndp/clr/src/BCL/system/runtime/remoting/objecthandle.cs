// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.ObjectHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Remoting.Lifetime;
using System.Security;

namespace System.Runtime.Remoting
{
  /// <summary>包装按值封送对象引用，从而使它们可以通过间接寻址返回。</summary>
  [ClassInterface(ClassInterfaceType.AutoDual)]
  [ComVisible(true)]
  public class ObjectHandle : MarshalByRefObject, IObjectHandle
  {
    private object WrappedObject;

    private ObjectHandle()
    {
    }

    /// <summary>初始化 <see cref="T:System.Runtime.Remoting.ObjectHandle" /> 类的实例，包装给定对象 <paramref name="o" />。</summary>
    /// <param name="o">由新的 <see cref="T:System.Runtime.Remoting.ObjectHandle" /> 包装的对象。</param>
    public ObjectHandle(object o)
    {
      this.WrappedObject = o;
    }

    /// <summary>返回被包装的对象。</summary>
    /// <returns>被包装的对象。</returns>
    public object Unwrap()
    {
      return this.WrappedObject;
    }

    /// <summary>初始化被包装的对象的生存期租约。</summary>
    /// <returns>初始化的 <see cref="T:System.Runtime.Remoting.Lifetime.ILease" />，它使您可以控制被包装对象的生存期。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration, Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public override object InitializeLifetimeService()
    {
      MarshalByRefObject marshalByRefObject = this.WrappedObject as MarshalByRefObject;
      if (marshalByRefObject != null && marshalByRefObject.InitializeLifetimeService() == null)
        return (object) null;
      return (object) (ILease) base.InitializeLifetimeService();
    }
  }
}
