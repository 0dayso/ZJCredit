// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.IChannelInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting
{
  /// <summary>提供与 <see cref="T:System.Runtime.Remoting.ObjRef" /> 一起传送的自定义信道信息。</summary>
  [ComVisible(true)]
  public interface IChannelInfo
  {
    /// <summary>获取和设置每个信道的信道数据。</summary>
    /// <returns>每个信道的信道数据。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方通过接口引用进行调用，它没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    object[] ChannelData { [SecurityCritical] get; [SecurityCritical] set; }
  }
}
