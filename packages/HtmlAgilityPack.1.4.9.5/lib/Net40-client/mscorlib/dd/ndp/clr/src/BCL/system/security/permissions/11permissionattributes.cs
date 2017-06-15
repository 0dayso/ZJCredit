// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.ZoneIdentityPermissionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>允许对使用声明安全性应用到代码中的 <see cref="T:System.Security.Permissions.ZoneIdentityPermission" /> 进行安全操作。此类不能被继承。</summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
  [ComVisible(true)]
  [Serializable]
  public sealed class ZoneIdentityPermissionAttribute : CodeAccessSecurityAttribute
  {
    private SecurityZone m_flag = SecurityZone.NoZone;

    /// <summary>获取或设置属性值指定的内容区域中的成员身份。</summary>
    /// <returns>
    /// <see cref="T:System.Security.SecurityZone" /> 值之一。</returns>
    public SecurityZone Zone
    {
      get
      {
        return this.m_flag;
      }
      set
      {
        this.m_flag = value;
      }
    }

    /// <summary>用指定的 <see cref="T:System.Security.Permissions.SecurityAction" /> 初始化 <see cref="T:System.Security.Permissions.ZoneIdentityPermissionAttribute" /> 类的新实例。</summary>
    /// <param name="action">
    /// <see cref="T:System.Security.Permissions.SecurityAction" /> 值之一。</param>
    public ZoneIdentityPermissionAttribute(SecurityAction action)
      : base(action)
    {
    }

    /// <summary>创建并返回一个新的 <see cref="T:System.Security.Permissions.ZoneIdentityPermission" />。</summary>
    /// <returns>与此特性对应的 <see cref="T:System.Security.Permissions.ZoneIdentityPermission" />。</returns>
    public override IPermission CreatePermission()
    {
      if (this.m_unrestricted)
        return (IPermission) new ZoneIdentityPermission(PermissionState.Unrestricted);
      return (IPermission) new ZoneIdentityPermission(this.m_flag);
    }
  }
}
