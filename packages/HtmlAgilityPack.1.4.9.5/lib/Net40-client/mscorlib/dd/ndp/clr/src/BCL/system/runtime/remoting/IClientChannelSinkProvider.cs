// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.IClientChannelSinkProvider
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
  /// <summary>为远程处理消息从其流过的客户端信道创建客户端信道接收器。</summary>
  [ComVisible(true)]
  public interface IClientChannelSinkProvider
  {
    /// <summary>获取或设置信道接收器提供程序链中的下一个接收器提供程序。</summary>
    /// <returns>信道接收器提供程序链中的下一个接收器提供程序。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    IClientChannelSinkProvider Next { [SecurityCritical] get; [SecurityCritical] set; }

    /// <summary>创建接收器链。</summary>
    /// <returns>新生成的信道接收器链中的第一个接收器，或 null（指示此提供程序将不会或不能为此终结点提供连接）。</returns>
    /// <param name="channel">信道，为其构造当前接收器链。</param>
    /// <param name="url">要连接到的对象的 URL。如果连接完全基于 <paramref name="remoteChannelData" /> 参数中包含的信息，则该参数可以为 null。</param>
    /// <param name="remoteChannelData">描述远程服务器上的信道的信道数据对象。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    [SecurityCritical]
    IClientChannelSink CreateSink(IChannelSender channel, string url, object remoteChannelData);
  }
}
