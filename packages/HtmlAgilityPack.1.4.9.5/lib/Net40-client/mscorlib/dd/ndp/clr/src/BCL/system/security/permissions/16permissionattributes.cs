// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.IsolatedStoragePermissionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>允许对使用声明安全性应用到代码中的 <see cref="T:System.Security.Permissions.IsolatedStoragePermission" /> 进行安全操作。</summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
  [ComVisible(true)]
  [Serializable]
  public abstract class IsolatedStoragePermissionAttribute : CodeAccessSecurityAttribute
  {
    internal long m_userQuota;
    internal IsolatedStorageContainment m_allowed;

    /// <summary>获取或设置最大的用户存储配额大小。</summary>
    /// <returns>最大的用户存储配额大小（以字节为单位）。</returns>
    public long UserQuota
    {
      get
      {
        return this.m_userQuota;
      }
      set
      {
        this.m_userQuota = value;
      }
    }

    /// <summary>获取或设置声明的独立存储级别。</summary>
    /// <returns>
    /// <see cref="T:System.Security.Permissions.IsolatedStorageContainment" /> 值之一。</returns>
    public IsolatedStorageContainment UsageAllowed
    {
      get
      {
        return this.m_allowed;
      }
      set
      {
        this.m_allowed = value;
      }
    }

    /// <summary>用指定的 <see cref="T:System.Security.Permissions.SecurityAction" /> 初始化 <see cref="T:System.Security.Permissions.IsolatedStoragePermissionAttribute" /> 类的新实例。</summary>
    /// <param name="action">
    /// <see cref="T:System.Security.Permissions.SecurityAction" /> 值之一。</param>
    protected IsolatedStoragePermissionAttribute(SecurityAction action)
      : base(action)
    {
    }
  }
}
