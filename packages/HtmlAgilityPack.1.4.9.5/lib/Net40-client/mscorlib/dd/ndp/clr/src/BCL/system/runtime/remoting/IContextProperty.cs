// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Contexts.IContextProperty
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
  /// <summary>从上下文属性收集命名信息，并确定新上下文是否与上下文属性兼容。</summary>
  [ComVisible(true)]
  public interface IContextProperty
  {
    /// <summary>获取将属性添加到上下文中时使用的属性名称。</summary>
    /// <returns>属性的名称。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    string Name { [SecurityCritical] get; }

    /// <summary>返回一个指示上下文属性是否与新上下文兼容的布尔值。</summary>
    /// <returns>如果该上下文属性可以与给定的上下文中的其他上下文属性共存，则为 true；否则为 false。</returns>
    /// <param name="newCtx">已在其中创建 <see cref="T:System.Runtime.Remoting.Contexts.ContextProperty" /> 的新上下文。</param>
    [SecurityCritical]
    bool IsNewContextOK(Context newCtx);

    /// <summary>当上下文冻结时调用。</summary>
    /// <param name="newContext">要冻结的上下文。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    void Freeze(Context newContext);
  }
}
