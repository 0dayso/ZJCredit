// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Services.EnterpriseServicesHelper
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Security;

namespace System.Runtime.Remoting.Services
{
  /// <summary>提供与 <see cref="T:System.AppDomain" /> 外的非托管类进行通信和操作所需的 API。此类不能被继承。</summary>
  [SecurityCritical]
  [ComVisible(true)]
  public sealed class EnterpriseServicesHelper
  {
    /// <summary>使用 运行时可调用包装 (RCW) 来包装指定的 IUnknown COM 接口。</summary>
    /// <returns>在此包装指定的 IUnknown 的 RCW。</returns>
    /// <param name="punk">指向要包装的 IUnknownCOM 接口的指针。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有 UnmanagedCode 权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public static object WrapIUnknownWithComObject(IntPtr punk)
    {
      return Marshal.InternalWrapIUnknownWithComObject(punk);
    }

    /// <summary>从指定的 <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" /> 构造 <see cref="T:System.Runtime.Remoting.Messaging.ReturnMessage" />。</summary>
    /// <returns>从在 <paramref name="ctorMsg" /> 参数中指定的构造调用返回的 <see cref="T:System.Runtime.Remoting.Activation.IConstructionReturnMessage" />。</returns>
    /// <param name="ctorMsg">对将要从中返回新的 <see cref="T:System.Runtime.Remoting.Messaging.ReturnMessage" /> 实例的对象的构造调用。</param>
    /// <param name="retObj">一个 <see cref="T:System.Runtime.Remoting.ObjRef" />，表示使用 <paramref name="ctorMsg" /> 中的构造调用来构造的对象。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [ComVisible(true)]
    public static IConstructionReturnMessage CreateConstructionReturnMessage(IConstructionCallMessage ctorMsg, MarshalByRefObject retObj)
    {
      return (IConstructionReturnMessage) new ConstructorReturnMessage(retObj, (object[]) null, 0, (LogicalCallContext) null, ctorMsg);
    }

    /// <summary>将 COM 可调用包装(CCW) 从类的一个实例切换到同一个类的另一个实例。</summary>
    /// <param name="oldcp">表示由 CCW 引用的类的旧实例的代理。</param>
    /// <param name="newcp">表示由 CCW 引用的类的新实例的代理。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有 UnmanagedCode 权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public static void SwitchWrappers(RealProxy oldcp, RealProxy newcp)
    {
      object transparentProxy1 = oldcp.GetTransparentProxy();
      object transparentProxy2 = newcp.GetTransparentProxy();
      RemotingServices.GetServerContextForProxy(transparentProxy1);
      RemotingServices.GetServerContextForProxy(transparentProxy2);
      object newtp = transparentProxy2;
      Marshal.InternalSwitchCCW(transparentProxy1, newtp);
    }
  }
}
