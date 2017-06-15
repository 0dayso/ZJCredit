// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.EnvironmentPermissionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>允许对使用声明安全性应用到代码中的 <see cref="T:System.Security.Permissions.EnvironmentPermission" /> 进行安全操作。此类不能被继承。</summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
  [ComVisible(true)]
  [Serializable]
  public sealed class EnvironmentPermissionAttribute : CodeAccessSecurityAttribute
  {
    private string m_read;
    private string m_write;

    /// <summary>获取或设置该字符串值所指定环境变量的读访问权限。</summary>
    /// <returns>可以读访问的环境变量列表。</returns>
    public string Read
    {
      get
      {
        return this.m_read;
      }
      set
      {
        this.m_read = value;
      }
    }

    /// <summary>获取或设置该字符串值所指定环境变量的写访问权限。</summary>
    /// <returns>可以写访问的环境变量列表。</returns>
    public string Write
    {
      get
      {
        return this.m_write;
      }
      set
      {
        this.m_write = value;
      }
    }

    /// <summary>为该字符串值所指定的环境变量设置完全访问权限。</summary>
    /// <returns>可以完全访问的环境变量列表。</returns>
    /// <exception cref="T:System.NotSupportedException">此属性不支持 get 方法。</exception>
    public string All
    {
      get
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_GetMethod"));
      }
      set
      {
        this.m_write = value;
        this.m_read = value;
      }
    }

    /// <summary>用指定的 <see cref="T:System.Security.Permissions.SecurityAction" /> 初始化 <see cref="T:System.Security.Permissions.EnvironmentPermissionAttribute" /> 类的新实例。</summary>
    /// <param name="action">
    /// <see cref="T:System.Security.Permissions.SecurityAction" /> 值之一。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="action" /> 参数不是有效的 <see cref="T:System.Security.Permissions.SecurityAction" /> 值。</exception>
    public EnvironmentPermissionAttribute(SecurityAction action)
      : base(action)
    {
    }

    /// <summary>创建并返回一个新的 <see cref="T:System.Security.Permissions.EnvironmentPermission" />。</summary>
    /// <returns>与此特性对应的 <see cref="T:System.Security.Permissions.EnvironmentPermission" />。</returns>
    public override IPermission CreatePermission()
    {
      if (this.m_unrestricted)
        return (IPermission) new EnvironmentPermission(PermissionState.Unrestricted);
      EnvironmentPermission environmentPermission = new EnvironmentPermission(PermissionState.None);
      if (this.m_read != null)
        environmentPermission.SetPathList(EnvironmentPermissionAccess.Read, this.m_read);
      if (this.m_write != null)
        environmentPermission.SetPathList(EnvironmentPermissionAccess.Write, this.m_write);
      return (IPermission) environmentPermission;
    }
  }
}
