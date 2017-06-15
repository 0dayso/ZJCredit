// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.IApplicationTrustManager
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Policy
{
  /// <summary>确定是否应执行应用程序以及应授予它哪个权限集。</summary>
  [ComVisible(true)]
  public interface IApplicationTrustManager : ISecurityEncodable
  {
    /// <summary>确定是否应执行应用程序以及应授予它哪个权限集。</summary>
    /// <returns>一个对象，包含有关应用程序的安全决策。</returns>
    /// <param name="activationContext">应用程序的激活上下文。</param>
    /// <param name="context">信任应用程序上下文的管理器。</param>
    ApplicationTrust DetermineApplicationTrust(ActivationContext activationContext, TrustManagerContext context);
  }
}
