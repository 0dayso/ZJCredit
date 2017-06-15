// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.IMethodCallMessage
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
  /// <summary>定义方法调用消息接口。</summary>
  [ComVisible(true)]
  public interface IMethodCallMessage : IMethodMessage, IMessage
  {
    /// <summary>获取调用中未标记为 out 参数的参数数目。</summary>
    /// <returns>调用中未标记为 out 参数的参数数目。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方通过接口引用进行调用，它没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    int InArgCount { [SecurityCritical] get; }

    /// <summary>获取未标记为 out 参数的参数数组。</summary>
    /// <returns>未标记为 out 参数的参数数组。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方通过接口引用进行调用，它没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    object[] InArgs { [SecurityCritical] get; }

    /// <summary>返回未标记为 out 参数的指定参数的名称。</summary>
    /// <returns>未标记为 out 参数的特定参数的名称。</returns>
    /// <param name="index">请求的 in 参数的数目。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方通过接口引用进行调用，它没有基础结构权限。</exception>
    [SecurityCritical]
    string GetInArgName(int index);

    /// <summary>返回未标记为 out 参数的指定参数。</summary>
    /// <returns>请求的未标记为 out 参数的参数。</returns>
    /// <param name="argNum">请求的 in 参数的数目。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方通过接口引用进行调用，它没有基础结构权限。</exception>
    [SecurityCritical]
    object GetInArg(int argNum);
  }
}
