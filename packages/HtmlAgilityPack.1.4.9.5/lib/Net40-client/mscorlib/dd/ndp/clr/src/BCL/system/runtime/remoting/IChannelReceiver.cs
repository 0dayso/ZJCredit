// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.IChannelReceiver
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
  /// <summary>为接收器信道提供所需的函数和属性。</summary>
  [ComVisible(true)]
  public interface IChannelReceiver : IChannel
  {
    /// <summary>获取信道特定数据。</summary>
    /// <returns>信道数据。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    object ChannelData { [SecurityCritical] get; }

    /// <summary>为 URI 返回所有 URL 的数组。</summary>
    /// <returns>URL 的数组。</returns>
    /// <param name="objectURI">要求 URL 的 URI。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    [SecurityCritical]
    string[] GetUrlsForUri(string objectURI);

    /// <summary>指示当前信道开始侦听请求。</summary>
    /// <param name="data">可选的初始化信息。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    [SecurityCritical]
    void StartListening(object data);

    /// <summary>指示当前信道停止侦听请求。</summary>
    /// <param name="data">该信道的可选状态信息。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    [SecurityCritical]
    void StopListening(object data);
  }
}
