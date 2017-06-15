// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.IRemotingTypeInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting
{
  /// <summary>提供对象的类型信息。</summary>
  [ComVisible(true)]
  public interface IRemotingTypeInfo
  {
    /// <summary>获取或设置 <see cref="T:System.Runtime.Remoting.ObjRef" /> 中的服务器对象的完全限定类型名。</summary>
    /// <returns>
    /// <see cref="T:System.Runtime.Remoting.ObjRef" /> 中的服务器对象的完全限定类型名。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方通过接口引用进行调用，它没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    string TypeName { [SecurityCritical] get; [SecurityCritical] set; }

    /// <summary>检查是否可以将表示指定对象类型的代理强制转换为由 <see cref="T:System.Runtime.Remoting.IRemotingTypeInfo" /> 接口表示的类型。</summary>
    /// <returns>如果强制转换将成功，则为 true；否则为 false。</returns>
    /// <param name="fromType">要强制转换到的类型。</param>
    /// <param name="o">要为其检查强制转换的对象。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方通过接口引用进行调用，它没有基础结构权限。</exception>
    [SecurityCritical]
    bool CanCastTo(Type fromType, object o);
  }
}
