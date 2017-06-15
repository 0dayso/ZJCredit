// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.InternalActivationContextHelper
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal
{
  /// <summary>提供对 <see cref="T:System.ActivationContext" /> 对象中数据的访问。</summary>
  [ComVisible(false)]
  public static class InternalActivationContextHelper
  {
    /// <summary>从 <see cref="T:System.ActivationContext" /> 对象获取应用程序清单的内容。</summary>
    /// <returns>由 <see cref="T:System.ActivationContext" /> 对象包含的应用程序清单。</returns>
    /// <param name="appInfo">包含清单的对象。</param>
    [SecuritySafeCritical]
    public static object GetActivationContextData(ActivationContext appInfo)
    {
      return (object) appInfo.ActivationContextData;
    }

    /// <summary>获取 <see cref="T:System.ActivationContext" /> 对象中最后一个部署组件的清单。</summary>
    /// <returns>
    /// <see cref="T:System.ActivationContext" /> 对象中最后一个部署组件的清单。</returns>
    /// <param name="appInfo">包含清单的对象。</param>
    [SecuritySafeCritical]
    public static object GetApplicationComponentManifest(ActivationContext appInfo)
    {
      return (object) appInfo.ApplicationComponentManifest;
    }

    /// <summary>获取 <see cref="T:System.ActivationContext" /> 对象中第一个部署组件的清单。</summary>
    /// <returns>
    /// <see cref="T:System.ActivationContext" /> 对象中第一个部署组件的清单。</returns>
    /// <param name="appInfo">包含清单的对象。</param>
    [SecuritySafeCritical]
    public static object GetDeploymentComponentManifest(ActivationContext appInfo)
    {
      return (object) appInfo.DeploymentComponentManifest;
    }

    /// <summary>通知 <see cref="T:System.ActivationContext" /> 准备运行。</summary>
    /// <param name="appInfo">要通知的对象。</param>
    public static void PrepareForExecution(ActivationContext appInfo)
    {
      appInfo.PrepareForExecution();
    }

    /// <summary>获取一个值，该值指示这是否是第一次运行此 <see cref="T:System.ActivationContext" /> 对象。</summary>
    /// <returns>如果 <see cref="T:System.ActivationContext" /> 指示是第一次运行此对象，则为 true；否则为 false。</returns>
    /// <param name="appInfo">要检查的对象。</param>
    public static bool IsFirstRun(ActivationContext appInfo)
    {
      return appInfo.LastApplicationStateResult == ActivationContext.ApplicationStateDisposition.RunningFirstTime;
    }

    /// <summary>获取一个包含应用程序清单的原始内容的字节数组。</summary>
    /// <returns>一个包含应用程序清单作为原始数据的数组。</returns>
    /// <param name="appInfo">要从中获取字节的对象。</param>
    public static byte[] GetApplicationManifestBytes(ActivationContext appInfo)
    {
      if (appInfo == null)
        throw new ArgumentNullException("appInfo");
      return appInfo.GetApplicationManifestBytes();
    }

    /// <summary>获取一个包含部署清单的原始内容的字节数组。</summary>
    /// <returns>一个包含部署清单作为原始数据的数组。</returns>
    /// <param name="appInfo">要从中获取字节的对象。</param>
    public static byte[] GetDeploymentManifestBytes(ActivationContext appInfo)
    {
      if (appInfo == null)
        throw new ArgumentNullException("appInfo");
      return appInfo.GetDeploymentManifestBytes();
    }
  }
}
