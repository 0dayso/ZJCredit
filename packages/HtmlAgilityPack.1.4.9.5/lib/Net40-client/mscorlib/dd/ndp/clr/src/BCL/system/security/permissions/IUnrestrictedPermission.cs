// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.IUnrestrictedPermission
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>允许一种权限公开其为无限制状态。</summary>
  [ComVisible(true)]
  public interface IUnrestrictedPermission
  {
    /// <summary>获取一个值，该值指示是否允许对受权限保护的资源进行不受限制的访问。</summary>
    /// <returns>如果允许不受限制地使用受此权限所保护的资源，则为 true；否则为 false。</returns>
    bool IsUnrestricted();
  }
}
