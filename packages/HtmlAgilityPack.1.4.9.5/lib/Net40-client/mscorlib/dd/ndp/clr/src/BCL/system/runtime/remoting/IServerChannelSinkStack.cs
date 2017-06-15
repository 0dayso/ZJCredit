// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.IServerChannelSinkStack
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
  /// <summary>为服务器信道接收器堆栈提供堆栈功能。</summary>
  [ComVisible(true)]
  public interface IServerChannelSinkStack : IServerResponseChannelSinkStack
  {
    /// <summary>将指定的接收器和与之关联的信息推送到接收器堆栈中。</summary>
    /// <param name="sink">要推送到接收器堆栈中的接收器。</param>
    /// <param name="state">在请求端生成的、响应端所需要的信息。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    [SecurityCritical]
    void Push(IServerChannelSink sink, object state);

    /// <summary>弹出与接收器堆栈中指定接收器（含）之下的所有接收器关联的信息。</summary>
    /// <returns>在请求端生成的、与指定接收器关联的信息。</returns>
    /// <param name="sink">要从接收器堆栈中移除和返回的接收器。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    [SecurityCritical]
    object Pop(IServerChannelSink sink);

    /// <summary>存储消息接收器及其关联状态，用于以后的异步处理。</summary>
    /// <param name="sink">服务器信道接收器。</param>
    /// <param name="state">与 <paramref name="sink" /> 关联的状态。</param>
    [SecurityCritical]
    void Store(IServerChannelSink sink, object state);

    /// <summary>存储消息接收器及其关联状态，然后使用刚刚存储的接收器或已存储的任意其他接收器来异步调度消息。</summary>
    /// <param name="sink">服务器信道接收器。</param>
    /// <param name="state">与 <paramref name="sink" /> 关联的状态。</param>
    [SecurityCritical]
    void StoreAndDispatch(IServerChannelSink sink, object state);

    /// <summary>提供一个回调委托，用于在异步调度消息后处理回调。</summary>
    /// <param name="ar">对远程对象执行的异步操作的状态。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    void ServerCallback(IAsyncResult ar);
  }
}
