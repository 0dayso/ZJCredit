// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Contexts.IContributeDynamicSink
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
  /// <summary>指示实现属性将在运行时通过 <see cref="M:System.Runtime.Remoting.Contexts.Context.RegisterDynamicProperty(System.Runtime.Remoting.Contexts.IDynamicProperty,System.ContextBoundObject,System.Runtime.Remoting.Contexts.Context)" /> 方法注册。</summary>
  [ComVisible(true)]
  public interface IContributeDynamicSink
  {
    /// <summary>通过 <see cref="T:System.Runtime.Remoting.Contexts.IDynamicMessageSink" /> 接口返回在发生启动调用事件和结束调用事件时会收到通知的消息接收器。</summary>
    /// <returns>公开 <see cref="T:System.Runtime.Remoting.Contexts.IDynamicMessageSink" /> 接口的动态接收器。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    IDynamicMessageSink GetDynamicSink();
  }
}
