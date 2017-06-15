// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.IEnvoyInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting
{
  /// <summary>提供代表信息。</summary>
  [ComVisible(true)]
  public interface IEnvoyInfo
  {
    /// <summary>获取或设置代表列表，这些代表是在封送该对象时由服务器上下文和对象链提供的。</summary>
    /// <returns>代表接收器链。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    IMessageSink EnvoySinks { [SecurityCritical] get; [SecurityCritical] set; }
  }
}
