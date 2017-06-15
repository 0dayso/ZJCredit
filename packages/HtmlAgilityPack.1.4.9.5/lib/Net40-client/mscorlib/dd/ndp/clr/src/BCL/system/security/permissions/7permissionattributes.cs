// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.ReflectionPermissionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>允许对使用声明安全性应用到代码中的 <see cref="T:System.Security.Permissions.ReflectionPermission" /> 进行安全操作。</summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
  [ComVisible(true)]
  [Serializable]
  public sealed class ReflectionPermissionAttribute : CodeAccessSecurityAttribute
  {
    private ReflectionPermissionFlag m_flag;

    /// <summary>获取或设置当前允许使用的反射。</summary>
    /// <returns>一个或多个使用按位“或”组合在一起的 <see cref="T:System.Security.Permissions.ReflectionPermissionFlag" /> 值。</returns>
    /// <exception cref="T:System.ArgumentException">试图将此属性设置为无效值。要查阅有效值，请参见 <see cref="T:System.Security.Permissions.ReflectionPermissionFlag" />。</exception>
    public ReflectionPermissionFlag Flags
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

    /// <summary>获取或设置一个值，指示是否允许在不可见的成员上反射。</summary>
    /// <returns>如果允许在不可见的成员上反射，则为 true；否则为 false。</returns>
    [Obsolete("This API has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")]
    public bool TypeInformation
    {
      get
      {
        return (uint) (this.m_flag & ReflectionPermissionFlag.TypeInformation) > 0U;
      }
      set
      {
        this.m_flag = value ? this.m_flag | ReflectionPermissionFlag.TypeInformation : this.m_flag & ~ReflectionPermissionFlag.TypeInformation;
      }
    }

    /// <summary>获取或设置一个值，指示是否允许在非公共成员上调用操作。</summary>
    /// <returns>如果允许在非公共成员上调用操作，则为 true；否则为 false。</returns>
    public bool MemberAccess
    {
      get
      {
        return (uint) (this.m_flag & ReflectionPermissionFlag.MemberAccess) > 0U;
      }
      set
      {
        this.m_flag = value ? this.m_flag | ReflectionPermissionFlag.MemberAccess : this.m_flag & ~ReflectionPermissionFlag.MemberAccess;
      }
    }

    /// <summary>获取或设置一个值，指示是否允许在 <see cref="N:System.Reflection.Emit" /> 中使用某些功能（例如发出调试符号）。</summary>
    /// <returns>如果允许使用受影响的功能，则为 true；否则为 false。</returns>
    [Obsolete("This permission is no longer used by the CLR.")]
    public bool ReflectionEmit
    {
      get
      {
        return (uint) (this.m_flag & ReflectionPermissionFlag.ReflectionEmit) > 0U;
      }
      set
      {
        this.m_flag = value ? this.m_flag | ReflectionPermissionFlag.ReflectionEmit : this.m_flag & ~ReflectionPermissionFlag.ReflectionEmit;
      }
    }

    /// <summary>获取或设置一个值，指示是否允许非公共成员的受限制调用。受限制调用意味着程序集的授予集（包含被调用的非公共成员）必须与调用程序集的授予集相同，或者前者是后者的子集。</summary>
    /// <returns>如果允许非公共成员的受限制调用，则为 true；否则为 false。</returns>
    public bool RestrictedMemberAccess
    {
      get
      {
        return (uint) (this.m_flag & ReflectionPermissionFlag.RestrictedMemberAccess) > 0U;
      }
      set
      {
        this.m_flag = value ? this.m_flag | ReflectionPermissionFlag.RestrictedMemberAccess : this.m_flag & ~ReflectionPermissionFlag.RestrictedMemberAccess;
      }
    }

    /// <summary>用指定的 <see cref="T:System.Security.Permissions.SecurityAction" /> 初始化 <see cref="T:System.Security.Permissions.ReflectionPermissionAttribute" /> 类的新实例。</summary>
    /// <param name="action">
    /// <see cref="T:System.Security.Permissions.SecurityAction" /> 值之一。</param>
    public ReflectionPermissionAttribute(SecurityAction action)
      : base(action)
    {
    }

    /// <summary>创建并返回一个新的 <see cref="T:System.Security.Permissions.ReflectionPermission" />。</summary>
    /// <returns>与此特性对应的 <see cref="T:System.Security.Permissions.ReflectionPermission" />。</returns>
    public override IPermission CreatePermission()
    {
      if (this.m_unrestricted)
        return (IPermission) new ReflectionPermission(PermissionState.Unrestricted);
      return (IPermission) new ReflectionPermission(this.m_flag);
    }
  }
}
