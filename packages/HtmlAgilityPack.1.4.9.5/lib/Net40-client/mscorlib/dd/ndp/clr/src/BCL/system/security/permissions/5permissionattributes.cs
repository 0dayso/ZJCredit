// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.KeyContainerPermissionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>允许对使用声明安全性应用到代码中的 <see cref="T:System.Security.Permissions.KeyContainerPermission" /> 进行安全操作。此类不能被继承。</summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
  [ComVisible(true)]
  [Serializable]
  public sealed class KeyContainerPermissionAttribute : CodeAccessSecurityAttribute
  {
    private int m_providerType = -1;
    private int m_keySpec = -1;
    private KeyContainerPermissionFlags m_flags;
    private string m_keyStore;
    private string m_providerName;
    private string m_keyContainerName;

    /// <summary>获取或设置密钥存储区的名称。</summary>
    /// <returns>密钥存储区的名称。默认为“*”。</returns>
    public string KeyStore
    {
      get
      {
        return this.m_keyStore;
      }
      set
      {
        this.m_keyStore = value;
      }
    }

    /// <summary>获取或设置提供程序名称。</summary>
    /// <returns>提供程序的名称。</returns>
    public string ProviderName
    {
      get
      {
        return this.m_providerName;
      }
      set
      {
        this.m_providerName = value;
      }
    }

    /// <summary>获取或设置提供程序类型。</summary>
    /// <returns>Wincrypt.h 头文件中定义的 PROV_ 值之一。</returns>
    public int ProviderType
    {
      get
      {
        return this.m_providerType;
      }
      set
      {
        this.m_providerType = value;
      }
    }

    /// <summary>获取或设置密钥容器的名称。</summary>
    /// <returns>密钥容器的名称。</returns>
    public string KeyContainerName
    {
      get
      {
        return this.m_keyContainerName;
      }
      set
      {
        this.m_keyContainerName = value;
      }
    }

    /// <summary>获取或设置密钥规范。</summary>
    /// <returns>Wincrypt.h 头文件中定义的 AT_ 值之一。</returns>
    public int KeySpec
    {
      get
      {
        return this.m_keySpec;
      }
      set
      {
        this.m_keySpec = value;
      }
    }

    /// <summary>获取或设置密钥容器权限。</summary>
    /// <returns>
    /// <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" /> 值的按位组合。默认值为 <see cref="F:System.Security.Permissions.KeyContainerPermissionFlags.NoFlags" />。</returns>
    public KeyContainerPermissionFlags Flags
    {
      get
      {
        return this.m_flags;
      }
      set
      {
        this.m_flags = value;
      }
    }

    /// <summary>使用指定的安全操作初始化 <see cref="T:System.Security.Permissions.KeyContainerPermissionAttribute" /> 类的新实例。</summary>
    /// <param name="action">
    /// <see cref="T:System.Security.Permissions.SecurityAction" /> 值之一。</param>
    public KeyContainerPermissionAttribute(SecurityAction action)
      : base(action)
    {
    }

    /// <summary>创建并返回一个新的 <see cref="T:System.Security.Permissions.KeyContainerPermission" />。</summary>
    /// <returns>与此特性对应的 <see cref="T:System.Security.Permissions.KeyContainerPermission" />。</returns>
    public override IPermission CreatePermission()
    {
      if (this.m_unrestricted)
        return (IPermission) new KeyContainerPermission(PermissionState.Unrestricted);
      if (KeyContainerPermissionAccessEntry.IsUnrestrictedEntry(this.m_keyStore, this.m_providerName, this.m_providerType, this.m_keyContainerName, this.m_keySpec))
        return (IPermission) new KeyContainerPermission(this.m_flags);
      KeyContainerPermission containerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
      KeyContainerPermissionAccessEntry accessEntry = new KeyContainerPermissionAccessEntry(this.m_keyStore, this.m_providerName, this.m_providerType, this.m_keyContainerName, this.m_keySpec, this.m_flags);
      containerPermission.AccessEntries.Add(accessEntry);
      return (IPermission) containerPermission;
    }
  }
}
