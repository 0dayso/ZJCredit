// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.IChannelDataStore
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
  /// <summary>存储远程处理信道的信道数据。</summary>
  [ComVisible(true)]
  public interface IChannelDataStore
  {
    /// <summary>获取当前信道映射到的信道 URI 的数组。</summary>
    /// <returns>当前信道映射到的信道 URI 的数组。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    string[] ChannelUris { [SecurityCritical] get; }

    /// <summary>获取或设置与实现信道的指定键关联的数据对象。</summary>
    /// <returns>实现信道的指定数据对象。</returns>
    /// <param name="key">数据对象所关联的键。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    object this[object key] { [SecurityCritical] get; [SecurityCritical] set; }
  }
}
