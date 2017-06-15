// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.ITransportHeaders
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
  /// <summary>存储在信道接收器中使用的标头的集合。</summary>
  [ComVisible(true)]
  public interface ITransportHeaders
  {
    /// <summary>获取或设置与给定键关联的传输标头。</summary>
    /// <returns>与给定键关联的传输标头。</returns>
    /// <param name="key">与请求的传输标头关联的键。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    object this[object key] { [SecurityCritical] get; [SecurityCritical] set; }

    /// <summary>返回 <see cref="T:System.Collections.IEnumerator" />，它循环访问 <see cref="T:System.Runtime.Remoting.Channels.ITransportHeaders" /> 对象中的所有项。</summary>
    /// <returns>
    /// <see cref="T:System.Collections.IEnumerator" />，它循环访问 <see cref="T:System.Runtime.Remoting.Channels.ITransportHeaders" /> 对象中的所有项。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    IEnumerator GetEnumerator();
  }
}
