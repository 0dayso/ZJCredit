// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.StrongNameIdentityPermissionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>允许对使用声明安全性应用到代码中的 <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" /> 进行安全操作。此类不能被继承。</summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
  [ComVisible(true)]
  [Serializable]
  public sealed class StrongNameIdentityPermissionAttribute : CodeAccessSecurityAttribute
  {
    private string m_name;
    private string m_version;
    private string m_blob;

    /// <summary>获取或设置强名称标识的名称。</summary>
    /// <returns>要与安全提供程序所指定名称相比较的名称。</returns>
    public string Name
    {
      get
      {
        return this.m_name;
      }
      set
      {
        this.m_name = value;
      }
    }

    /// <summary>获取或设置强名称标识的版本。</summary>
    /// <returns>强名称标识的版本号。</returns>
    public string Version
    {
      get
      {
        return this.m_version;
      }
      set
      {
        this.m_version = value;
      }
    }

    /// <summary>获取或设置用十六进制字符串表示的强名称标识的公钥值。</summary>
    /// <returns>用十六进制字符串表示的强名称标识的公钥值。</returns>
    public string PublicKey
    {
      get
      {
        return this.m_blob;
      }
      set
      {
        this.m_blob = value;
      }
    }

    /// <summary>用指定的 <see cref="T:System.Security.Permissions.SecurityAction" /> 初始化 <see cref="T:System.Security.Permissions.StrongNameIdentityPermissionAttribute" /> 类的新实例。</summary>
    /// <param name="action">
    /// <see cref="T:System.Security.Permissions.SecurityAction" /> 值之一。</param>
    public StrongNameIdentityPermissionAttribute(SecurityAction action)
      : base(action)
    {
    }

    /// <summary>创建并返回一个新的 <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" />。</summary>
    /// <returns>对应于此特性的 <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" />。</returns>
    /// <exception cref="T:System.ArgumentException">该方法失败，因为关键字为 null。</exception>
    public override IPermission CreatePermission()
    {
      if (this.m_unrestricted)
        return (IPermission) new StrongNameIdentityPermission(PermissionState.Unrestricted);
      if (this.m_blob == null && this.m_name == null && this.m_version == null)
        return (IPermission) new StrongNameIdentityPermission(PermissionState.None);
      if (this.m_blob == null)
        throw new ArgumentException(Environment.GetResourceString("ArgumentNull_Key"));
      StrongNamePublicKeyBlob blob = new StrongNamePublicKeyBlob(this.m_blob);
      if (this.m_version == null || this.m_version.Equals(string.Empty))
        return (IPermission) new StrongNameIdentityPermission(blob, this.m_name, (System.Version) null);
      return (IPermission) new StrongNameIdentityPermission(blob, this.m_name, new System.Version(this.m_version));
    }
  }
}
