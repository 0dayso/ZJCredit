// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.IIdentityPermissionFactory
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Policy
{
  /// <summary>定义创建新标识权限的方法。</summary>
  [ComVisible(true)]
  public interface IIdentityPermissionFactory
  {
    /// <summary>创建指定证据的新标识权限。</summary>
    /// <returns>新标识权限。</returns>
    /// <param name="evidence">创建新标识权限所依据的证据。</param>
    IPermission CreateIdentityPermission(Evidence evidence);
  }
}
