// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.IChannelSinkBase
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
  /// <summary>为信道接收器提供基接口。</summary>
  [ComVisible(true)]
  public interface IChannelSinkBase
  {
    /// <summary>获取可以通过其访问接收器的属性的字典。</summary>
    /// <returns>字典，可以通过它访问接收器的属性；或者如果信道接收器不支持属性，则为 null。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    IDictionary Properties { [SecurityCritical] get; }
  }
}
