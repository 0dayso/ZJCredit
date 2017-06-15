// Decompiled with JetBrains decompiler
// Type: System.Security.Principal.IPrincipal
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Principal
{
  /// <summary>定义用户对象的基本功能。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public interface IPrincipal
  {
    /// <summary>获取当前用户的标识。</summary>
    /// <returns>与当前用户关联的 <see cref="T:System.Security.Principal.IIdentity" /> 对象。</returns>
    [__DynamicallyInvokable]
    IIdentity Identity { [__DynamicallyInvokable] get; }

    /// <summary>确定当前用户是否属于指定的角色。</summary>
    /// <returns>如果当前用户是指定角色的成员，则为 true；否则为 false。</returns>
    /// <param name="role">要检查其成员资格的角色的名称。</param>
    [__DynamicallyInvokable]
    bool IsInRole(string role);
  }
}
