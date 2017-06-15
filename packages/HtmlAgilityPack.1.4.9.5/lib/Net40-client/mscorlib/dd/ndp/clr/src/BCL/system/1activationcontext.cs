// Decompiled with JetBrains decompiler
// Type: System.ApplicationIdentity
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Deployment.Internal.Isolation;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
  /// <summary>提供唯一标识清单激活的应用程序的能力。此类不能被继承。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(false)]
  [Serializable]
  public sealed class ApplicationIdentity : ISerializable
  {
    private IDefinitionAppId _appId;

    /// <summary>获取应用程序的全名。</summary>
    /// <returns>应用程序的全名，也称为显示名称。</returns>
    /// <filterpriority>1</filterpriority>
    public string FullName
    {
      [SecuritySafeCritical] get
      {
        return IsolationInterop.AppIdAuthority.DefinitionToText(0U, this._appId);
      }
    }

    /// <summary>获取作为 URL 的部署清单的位置。</summary>
    /// <returns>部署清单的 URL。</returns>
    /// <filterpriority>1</filterpriority>
    public string CodeBase
    {
      [SecuritySafeCritical] get
      {
        return this._appId.get_Codebase();
      }
    }

    internal IDefinitionAppId Identity
    {
      [SecurityCritical] get
      {
        return this._appId;
      }
    }

    private ApplicationIdentity()
    {
    }

    [SecurityCritical]
    private ApplicationIdentity(SerializationInfo info, StreamingContext context)
    {
      string Identity = (string) info.GetValue("FullName", typeof (string));
      if (Identity == null)
        throw new ArgumentNullException("fullName");
      this._appId = IsolationInterop.AppIdAuthority.TextToDefinition(0U, Identity);
    }

    /// <summary>初始化 <see cref="T:System.ApplicationIdentity" /> 类的新实例。</summary>
    /// <param name="applicationIdentityFullName">应用程序的全名。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="applicationIdentityFullName" /> 为 null。</exception>
    [SecuritySafeCritical]
    public ApplicationIdentity(string applicationIdentityFullName)
    {
      if (applicationIdentityFullName == null)
        throw new ArgumentNullException("applicationIdentityFullName");
      this._appId = IsolationInterop.AppIdAuthority.TextToDefinition(0U, applicationIdentityFullName);
    }

    [SecurityCritical]
    internal ApplicationIdentity(IDefinitionAppId applicationIdentity)
    {
      this._appId = applicationIdentity;
    }

    /// <summary>返回清单激活的应用程序的全名。</summary>
    /// <returns>清单激活的应用程序的全名。</returns>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public override string ToString()
    {
      return this.FullName;
    }

    [SecurityCritical]
    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
    {
      info.AddValue("FullName", (object) this.FullName, typeof (string));
    }
  }
}
