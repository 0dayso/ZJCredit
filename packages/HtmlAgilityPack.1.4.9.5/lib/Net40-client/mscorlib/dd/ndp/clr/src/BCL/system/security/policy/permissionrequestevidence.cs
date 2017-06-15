// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.PermissionRequestEvidence
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Policy
{
  /// <summary>定义表示权限请求的证据。此类不能被继承。</summary>
  [ComVisible(true)]
  [Obsolete("Assembly level declarative security is obsolete and is no longer enforced by the CLR by default. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
  [Serializable]
  public sealed class PermissionRequestEvidence : EvidenceBase
  {
    private PermissionSet m_request;
    private PermissionSet m_optional;
    private PermissionSet m_denied;
    private string m_strRequest;
    private string m_strOptional;
    private string m_strDenied;

    /// <summary>获取代码运行所需的最小权限。</summary>
    /// <returns>代码运行所需的最小权限。</returns>
    public PermissionSet RequestedPermissions
    {
      get
      {
        return this.m_request;
      }
    }

    /// <summary>获取（如果授予）代码可以使用的（但不是必需的）权限。</summary>
    /// <returns>（如果授予）代码可以使用的（但不是必需的）权限。</returns>
    public PermissionSet OptionalPermissions
    {
      get
      {
        return this.m_optional;
      }
    }

    /// <summary>获取代码明确请求不要授予的权限。</summary>
    /// <returns>代码明确请求不要授予的权限。</returns>
    public PermissionSet DeniedPermissions
    {
      get
      {
        return this.m_denied;
      }
    }

    /// <summary>用代码程序集的权限请求初始化 <see cref="T:System.Security.Policy.PermissionRequestEvidence" /> 类的新实例。</summary>
    /// <param name="request">代码运行所需的最小权限。</param>
    /// <param name="optional">（如果授予）代码可以使用的（但不是必需的）权限。</param>
    /// <param name="denied">代码明确请求不要授予的权限。</param>
    public PermissionRequestEvidence(PermissionSet request, PermissionSet optional, PermissionSet denied)
    {
      this.m_request = request != null ? request.Copy() : (PermissionSet) null;
      this.m_optional = optional != null ? optional.Copy() : (PermissionSet) null;
      if (denied == null)
        this.m_denied = (PermissionSet) null;
      else
        this.m_denied = denied.Copy();
    }

    /// <summary>创建作为当前实例副本的新对象。</summary>
    /// <returns>作为此实例副本的新对象。</returns>
    public override EvidenceBase Clone()
    {
      return (EvidenceBase) this.Copy();
    }

    /// <summary>创建当前 <see cref="T:System.Security.Policy.PermissionRequestEvidence" /> 的等效副本。</summary>
    /// <returns>当前 <see cref="T:System.Security.Policy.PermissionRequestEvidence" /> 的等效副本。</returns>
    public PermissionRequestEvidence Copy()
    {
      return new PermissionRequestEvidence(this.m_request, this.m_optional, this.m_denied);
    }

    internal SecurityElement ToXml()
    {
      SecurityElement securityElement = new SecurityElement("System.Security.Policy.PermissionRequestEvidence");
      securityElement.AddAttribute("version", "1");
      if (this.m_request != null)
      {
        SecurityElement child = new SecurityElement("Request");
        child.AddChild(this.m_request.ToXml());
        securityElement.AddChild(child);
      }
      if (this.m_optional != null)
      {
        SecurityElement child = new SecurityElement("Optional");
        child.AddChild(this.m_optional.ToXml());
        securityElement.AddChild(child);
      }
      if (this.m_denied != null)
      {
        SecurityElement child = new SecurityElement("Denied");
        child.AddChild(this.m_denied.ToXml());
        securityElement.AddChild(child);
      }
      return securityElement;
    }

    /// <summary>获取 <see cref="T:System.Security.Policy.PermissionRequestEvidence" /> 状态的字符串表示形式。</summary>
    /// <returns>
    /// <see cref="T:System.Security.Policy.PermissionRequestEvidence" /> 状态的字符串表示形式。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public override string ToString()
    {
      return this.ToXml().ToString();
    }
  }
}
