// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.IChannel
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
  /// <summary>为跨远程处理边界的消息提供管道。</summary>
  [ComVisible(true)]
  public interface IChannel
  {
    /// <summary>获取该信道的优先级。</summary>
    /// <returns>指示信道优先级的整数。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    int ChannelPriority { [SecurityCritical] get; }

    /// <summary>获取信道的名称。</summary>
    /// <returns>信道的名称。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    string ChannelName { [SecurityCritical] get; }

    /// <summary>将对象 URI 返回为输出参数，将当前信道的 URI 返回为返回值。</summary>
    /// <returns>当前信道的 URI，或者如果 URI 不属于该信道，则为 null。</returns>
    /// <param name="url">该对象的 URL。</param>
    /// <param name="objectURI">当该方法返回时，包含持有对象 URI 的 <see cref="T:System.String" />。该参数未经初始化即被传递。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    [SecurityCritical]
    string Parse(string url, out string objectURI);
  }
}
