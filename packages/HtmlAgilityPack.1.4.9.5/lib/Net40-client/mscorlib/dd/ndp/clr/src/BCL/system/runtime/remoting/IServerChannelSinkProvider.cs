// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.IServerChannelSinkProvider
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
  /// <summary>为远程处理消息从其流过的服务器信道创建服务器信道接收器。</summary>
  [ComVisible(true)]
  public interface IServerChannelSinkProvider
  {
    /// <summary>获取或设置信道接收器提供程序链中的下一个接收器提供程序。</summary>
    /// <returns>信道接收器提供程序链中的下一个接收器提供程序。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    IServerChannelSinkProvider Next { [SecurityCritical] get; [SecurityCritical] set; }

    /// <summary>返回与当前接收器关联的信道的信道数据。</summary>
    /// <param name="channelData">将在其中返回信道数据的 <see cref="T:System.Runtime.Remoting.Channels.IChannelDataStore" /> 对象。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    [SecurityCritical]
    void GetChannelData(IChannelDataStore channelData);

    /// <summary>创建接收器链。</summary>
    /// <returns>新生成的信道接收器链中的第一个接收器，或 null（指示此提供程序将不会或不能为此终结点提供连接）。</returns>
    /// <param name="channel">要为其创建信道接收器链的信道。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    [SecurityCritical]
    IServerChannelSink CreateSink(IChannelReceiver channel);
  }
}
