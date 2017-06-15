// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Activation.IActivator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Activation
{
  /// <summary>提供远程处理激活器类的基本功能。</summary>
  [ComVisible(true)]
  public interface IActivator
  {
    /// <summary>获取或设置链中的下一个激活器。</summary>
    /// <returns>链中的下一个激活器。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    IActivator NextActivator { [SecurityCritical] get; [SecurityCritical] set; }

    /// <summary>获取该激活器为活动状态的 <see cref="T:System.Runtime.Remoting.Activation.ActivatorLevel" />。</summary>
    /// <returns>该激活器为活动状态的 <see cref="T:System.Runtime.Remoting.Activation.ActivatorLevel" />。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    ActivatorLevel Level { [SecurityCritical] get; }

    /// <summary>创建在提供的 <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" /> 中指定的对象的实例。</summary>
    /// <returns>包含在 <see cref="T:System.Runtime.Remoting.Activation.IConstructionReturnMessage" /> 中的对象激活的状态。</returns>
    /// <param name="msg">激活对象所需的对象信息，存储在 <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" /> 中。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    [SecurityCritical]
    IConstructionReturnMessage Activate(IConstructionCallMessage msg);
  }
}
