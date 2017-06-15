// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.IMessage
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
  /// <summary>包含在合作的消息接收器之间发送的通讯数据。</summary>
  [ComVisible(true)]
  public interface IMessage
  {
    /// <summary>获取表示消息属性集合的 <see cref="T:System.Collections.IDictionary" />。</summary>
    /// <returns>表示消息属性集合的字典。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方通过接口引用进行调用，它没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    IDictionary Properties { [SecurityCritical] get; }
  }
}
