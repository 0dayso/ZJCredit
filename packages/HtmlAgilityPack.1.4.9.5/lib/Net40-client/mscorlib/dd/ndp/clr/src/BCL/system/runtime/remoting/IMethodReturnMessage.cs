// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.IMethodReturnMessage
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
  /// <summary>定义方法调用返回消息接口。</summary>
  [ComVisible(true)]
  public interface IMethodReturnMessage : IMethodMessage, IMessage
  {
    /// <summary>获取方法调用中标记为 ref 或 out 参数的参数的数目。</summary>
    /// <returns>方法调用中标记为 ref 或 out 参数的参数的数目。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方通过接口引用进行调用，它没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    int OutArgCount { [SecurityCritical] get; }

    /// <summary>返回标记为 ref 或 out 参数的指定参数。</summary>
    /// <returns>标记为 ref 或 out 参数的指定参数。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方通过接口引用进行调用，它没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    object[] OutArgs { [SecurityCritical] get; }

    /// <summary>获取在方法调用期间引发的异常。</summary>
    /// <returns>方法调用的异常对象；或者如果该方法未引发异常，则为 null。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方通过接口引用进行调用，它没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    Exception Exception { [SecurityCritical] get; }

    /// <summary>获取方法调用的返回值。</summary>
    /// <returns>方法调用的返回值。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方通过接口引用进行调用，它没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    object ReturnValue { [SecurityCritical] get; }

    /// <summary>返回标记为 ref 或 out 参数的指定参数的名称。</summary>
    /// <returns>参数名称；或者如果未实现当前方法，则为 null。</returns>
    /// <param name="index">请求的参数名称的数目。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方通过接口引用进行调用，它没有基础结构权限。</exception>
    [SecurityCritical]
    string GetOutArgName(int index);

    /// <summary>返回标记为 ref 或 out 参数的指定参数。</summary>
    /// <returns>标记为 ref 或 out 参数的指定参数。</returns>
    /// <param name="argNum">请求的参数的数目。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方通过接口引用进行调用，它没有基础结构权限。</exception>
    [SecurityCritical]
    object GetOutArg(int argNum);
  }
}
