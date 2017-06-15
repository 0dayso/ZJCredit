// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.IChannelReceiverHook
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
  /// <summary>指示实现信道要挂接到外部侦听器服务。</summary>
  [ComVisible(true)]
  public interface IChannelReceiverHook
  {
    /// <summary>获取要挂接到的侦听器的类型。</summary>
    /// <returns>要挂接到的侦听器的类型（例如“http”）。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    string ChannelScheme { [SecurityCritical] get; }

    /// <summary>获取一个布尔值，该值指示是否需要将 <see cref="T:System.Runtime.Remoting.Channels.IChannelReceiverHook" /> 挂接到外部侦听器服务。</summary>
    /// <returns>一个布尔值，该值指示是否需要将 <see cref="T:System.Runtime.Remoting.Channels.IChannelReceiverHook" /> 挂接到外部侦听器服务。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    bool WantsToListen { [SecurityCritical] get; }

    /// <summary>获取当前信道正在使用的信道接收器链。</summary>
    /// <returns>当前信道正在使用的信道接收器链。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    IServerChannelSink ChannelSinkChain { [SecurityCritical] get; }

    /// <summary>添加信道挂钩将在其上进行侦听的 URI。</summary>
    /// <param name="channelUri">信道挂钩将在其上进行侦听的 URI。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    [SecurityCritical]
    void AddHookChannelUri(string channelUri);
  }
}
