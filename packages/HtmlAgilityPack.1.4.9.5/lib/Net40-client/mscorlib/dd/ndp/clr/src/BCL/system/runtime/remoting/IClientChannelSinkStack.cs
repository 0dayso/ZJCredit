// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.IClientChannelSinkStack
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
  /// <summary>为客户端信道接收器的堆栈提供功能，在异步消息响应解码过程中必须调用这些客户端信道接收器。</summary>
  [ComVisible(true)]
  public interface IClientChannelSinkStack : IClientResponseChannelSinkStack
  {
    /// <summary>将指定的接收器和与之关联的信息推送到接收器堆栈中。</summary>
    /// <param name="sink">要推送到接收器堆栈中的接收器。</param>
    /// <param name="state">在请求端生成的、响应端所需要的信息。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    [SecurityCritical]
    void Push(IClientChannelSink sink, object state);

    /// <summary>弹出与接收器堆栈中指定接收器（含）之下的所有接收器关联的信息。</summary>
    /// <returns>在请求端生成的、与指定接收器关联的信息。</returns>
    /// <param name="sink">要从接收器堆栈中移除和返回的接收器。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    [SecurityCritical]
    object Pop(IClientChannelSink sink);
  }
}
