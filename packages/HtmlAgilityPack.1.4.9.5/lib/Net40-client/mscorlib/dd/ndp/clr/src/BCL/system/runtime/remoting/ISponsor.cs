// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Lifetime.ISponsor
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Lifetime
{
  /// <summary>指示该实施者希望成为生存期租约主办方。</summary>
  [ComVisible(true)]
  public interface ISponsor
  {
    /// <summary>请求发起客户端续订指定对象的租约。</summary>
    /// <returns>指定对象的附加租用时间。</returns>
    /// <param name="lease">需要续订租约的对象的生存期租约。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方通过接口引用进行调用，它没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    TimeSpan Renewal(ILease lease);
  }
}
