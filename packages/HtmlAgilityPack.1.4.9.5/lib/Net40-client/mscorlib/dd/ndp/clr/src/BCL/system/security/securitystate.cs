// Decompiled with JetBrains decompiler
// Type: System.Security.SecurityState
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Permissions;

namespace System.Security
{
  /// <summary>提供一个基类，用于从 <see cref="T:System.AppDomainManager" /> 对象中请求操作的安全状态。</summary>
  [SecurityCritical]
  [PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
  public abstract class SecurityState
  {
    /// <summary>获取一个值，该值指示 <see cref="T:System.Security.SecurityState" /> 类的此实现的状态是否在当前宿主上可用。</summary>
    /// <returns>如果状态可用，则为 true；否则为 false。</returns>
    [SecurityCritical]
    public bool IsStateAvailable()
    {
      AppDomainManager appDomainManager = AppDomainManager.CurrentAppDomainManager;
      if (appDomainManager == null)
        return false;
      return appDomainManager.CheckSecuritySettings(this);
    }

    /// <summary>在派生类中重写时，确保由 <see cref="T:System.Security.SecurityState" /> 表示的状态在宿主上可用。</summary>
    public abstract void EnsureState();
  }
}
