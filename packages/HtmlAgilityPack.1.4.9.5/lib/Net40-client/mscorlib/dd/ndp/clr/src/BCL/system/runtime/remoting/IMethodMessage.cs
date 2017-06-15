// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.IMethodMessage
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
  /// <summary>定义方法消息接口。</summary>
  [ComVisible(true)]
  public interface IMethodMessage : IMessage
  {
    /// <summary>获取要将该调用发送到的特定对象的 URI。</summary>
    /// <returns>包含被调用方法的远程对象的 URI。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方通过接口引用进行调用，它没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    string Uri { [SecurityCritical] get; }

    /// <summary>获取被调用方法的名称。</summary>
    /// <returns>被调用方法的名称。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方通过接口引用进行调用，它没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    string MethodName { [SecurityCritical] get; }

    /// <summary>获取要将该调用发送到的特定对象的完整 <see cref="T:System.Type" /> 名称。</summary>
    /// <returns>要将该调用发送到的特定对象的完整 <see cref="T:System.Type" /> 名称。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方通过接口引用进行调用，它没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    string TypeName { [SecurityCritical] get; }

    /// <summary>获取包含方法签名的对象。</summary>
    /// <returns>包含方法签名的对象。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方通过接口引用进行调用，它没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    object MethodSignature { [SecurityCritical] get; }

    /// <summary>获取传递给该方法的参数的数目。</summary>
    /// <returns>传递给该方法的参数的数目。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方通过接口引用进行调用，它没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    int ArgCount { [SecurityCritical] get; }

    /// <summary>获取传递给该方法的参数数组。</summary>
    /// <returns>
    /// <see cref="T:System.Object" /> 数组，它包含传递给该方法的参数。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方通过接口引用进行调用，它没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    object[] Args { [SecurityCritical] get; }

    /// <summary>获取一个值，该值指示消息是否具有变量参数。</summary>
    /// <returns>如果该方法可以接受数目可变的参数，则为 true；否则为 false。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方通过接口引用进行调用，它没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    bool HasVarArgs { [SecurityCritical] get; }

    /// <summary>获取当前方法调用的 <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" />。</summary>
    /// <returns>获取当前方法调用的 <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" />。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方通过接口引用进行调用，它没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    LogicalCallContext LogicalCallContext { [SecurityCritical] get; }

    /// <summary>获取被调用方法的 <see cref="T:System.Reflection.MethodBase" />。</summary>
    /// <returns>被调用方法的 <see cref="T:System.Reflection.MethodBase" />。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方通过接口引用进行调用，它没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    MethodBase MethodBase { [SecurityCritical] get; }

    /// <summary>获取传递给该方法的参数的名称。</summary>
    /// <returns>传递给该方法的指定参数的名称；如果未实现当前方法，则为 null。</returns>
    /// <param name="index">请求的参数的数目。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方通过接口引用进行调用，它没有基础结构权限。</exception>
    [SecurityCritical]
    string GetArgName(int index);

    /// <summary>获取作为 <see cref="T:System.Object" /> 的特定参数。</summary>
    /// <returns>传递给该方法的参数。</returns>
    /// <param name="argNum">请求的参数的数目。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方通过接口引用进行调用，它没有基础结构权限。</exception>
    [SecurityCritical]
    object GetArg(int argNum);
  }
}
