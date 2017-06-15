// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.InternalApplicationIdentityHelper
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal
{
  /// <summary>提供对 <see cref="T:System.ApplicationIdentity" /> 对象的内部属性的访问。</summary>
  [ComVisible(false)]
  public static class InternalApplicationIdentityHelper
  {
    /// <summary>获取一个 IDefinitionAppId 接口，表示 <see cref="T:System.ApplicationIdentity" /> 对象的唯一标识符。</summary>
    /// <returns>由 <see cref="T:System.ApplicationIdentity" /> 对象包含的唯一标识符。</returns>
    /// <param name="id">要从中提取标识符的对象。</param>
    [SecurityCritical]
    public static object GetInternalAppId(ApplicationIdentity id)
    {
      return (object) id.Identity;
    }
  }
}
