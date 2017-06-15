// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.SecurityPermissionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>允许对使用声明安全性应用到代码中的 <see cref="T:System.Security.Permissions.SecurityPermission" /> 进行安全操作。此类不能被继承。</summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
  [ComVisible(true)]
  [Serializable]
  public sealed class SecurityPermissionAttribute : CodeAccessSecurityAttribute
  {
    private SecurityPermissionFlag m_flag;

    /// <summary>获取或设置组成 <see cref="T:System.Security.Permissions.SecurityPermission" /> 权限的所有权限标志。</summary>
    /// <returns>一个或多个使用按位“或”组合在一起的 <see cref="T:System.Security.Permissions.SecurityPermissionFlag" /> 值。</returns>
    /// <exception cref="T:System.ArgumentException">试图将此属性设置为无效值。要查阅有效值，请参见 <see cref="T:System.Security.Permissions.SecurityPermissionFlag" />。</exception>
    public SecurityPermissionFlag Flags
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

    /// <summary>获取或设置一个值，该值指示是否声明了断言的权限，断言此代码的所有调用方都具有该操作必需的权限。</summary>
    /// <returns>如果声明了断言的权限，则为 true；否则为 false。</returns>
    public bool Assertion
    {
      get
      {
        return (uint) (this.m_flag & SecurityPermissionFlag.Assertion) > 0U;
      }
      set
      {
        this.m_flag = value ? this.m_flag | SecurityPermissionFlag.Assertion : this.m_flag & ~SecurityPermissionFlag.Assertion;
      }
    }

    /// <summary>获取或设置一个值，该值指示是否声明了调用非托管代码的权限。</summary>
    /// <returns>如果声明了调用非托管代码的权限，则为 true；否则为 false。</returns>
    public bool UnmanagedCode
    {
      get
      {
        return (uint) (this.m_flag & SecurityPermissionFlag.UnmanagedCode) > 0U;
      }
      set
      {
        this.m_flag = value ? this.m_flag | SecurityPermissionFlag.UnmanagedCode : this.m_flag & ~SecurityPermissionFlag.UnmanagedCode;
      }
    }

    /// <summary>获取或设置一个值，该值指示是否声明了跳过代码验证的权限。</summary>
    /// <returns>如果声明了跳过代码验证的权限，则为 true；否则为 false。</returns>
    public bool SkipVerification
    {
      get
      {
        return (uint) (this.m_flag & SecurityPermissionFlag.SkipVerification) > 0U;
      }
      set
      {
        this.m_flag = value ? this.m_flag | SecurityPermissionFlag.SkipVerification : this.m_flag & ~SecurityPermissionFlag.SkipVerification;
      }
    }

    /// <summary>获取或设置一个值，该值指示是否声明了执行代码的权限。</summary>
    /// <returns>如果声明了执行代码的权限，则为 true；否则为 false。</returns>
    public bool Execution
    {
      get
      {
        return (uint) (this.m_flag & SecurityPermissionFlag.Execution) > 0U;
      }
      set
      {
        this.m_flag = value ? this.m_flag | SecurityPermissionFlag.Execution : this.m_flag & ~SecurityPermissionFlag.Execution;
      }
    }

    /// <summary>获取或设置一个值，该值指示是否声明了操作线程的权限。</summary>
    /// <returns>如果声明了操作线程的权限，则为 true；否则为 false。</returns>
    public bool ControlThread
    {
      get
      {
        return (uint) (this.m_flag & SecurityPermissionFlag.ControlThread) > 0U;
      }
      set
      {
        this.m_flag = value ? this.m_flag | SecurityPermissionFlag.ControlThread : this.m_flag & ~SecurityPermissionFlag.ControlThread;
      }
    }

    /// <summary>获取或设置一个值，该值指示是否声明了改变或操作证据的权限。</summary>
    /// <returns>如果声明了改变或操作证据的能力，则为 true；否则为 false。</returns>
    public bool ControlEvidence
    {
      get
      {
        return (uint) (this.m_flag & SecurityPermissionFlag.ControlEvidence) > 0U;
      }
      set
      {
        this.m_flag = value ? this.m_flag | SecurityPermissionFlag.ControlEvidence : this.m_flag & ~SecurityPermissionFlag.ControlEvidence;
      }
    }

    /// <summary>获取或设置一个值，该值指示是否声明了查看并操作安全策略的权限。</summary>
    /// <returns>如果声明了操作安全策略的权限，则为 true；否则为 false。</returns>
    public bool ControlPolicy
    {
      get
      {
        return (uint) (this.m_flag & SecurityPermissionFlag.ControlPolicy) > 0U;
      }
      set
      {
        this.m_flag = value ? this.m_flag | SecurityPermissionFlag.ControlPolicy : this.m_flag & ~SecurityPermissionFlag.ControlPolicy;
      }
    }

    /// <summary>获取或设置一个值，该值指示代码是否可以使用序列化格式化程序来序列化或反序列化对象。</summary>
    /// <returns>如果代码可以使用序列化格式化程序来序列化或反序列化对象，则为 true，否则为 false。</returns>
    public bool SerializationFormatter
    {
      get
      {
        return (uint) (this.m_flag & SecurityPermissionFlag.SerializationFormatter) > 0U;
      }
      set
      {
        this.m_flag = value ? this.m_flag | SecurityPermissionFlag.SerializationFormatter : this.m_flag & ~SecurityPermissionFlag.SerializationFormatter;
      }
    }

    /// <summary>获取或设置一个值，该值指示是否声明了改变或操作域安全策略的权限。</summary>
    /// <returns>如果声明了在应用程序域中改变或操作安全策略的权限，则为 true；否则为 false。</returns>
    public bool ControlDomainPolicy
    {
      get
      {
        return (uint) (this.m_flag & SecurityPermissionFlag.ControlDomainPolicy) > 0U;
      }
      set
      {
        this.m_flag = value ? this.m_flag | SecurityPermissionFlag.ControlDomainPolicy : this.m_flag & ~SecurityPermissionFlag.ControlDomainPolicy;
      }
    }

    /// <summary>获取或设置一个值，该值指示是否声明了操作当前用户的权限。</summary>
    /// <returns>如果声明了操作当前用户的权限，则为 true；否则为 false。</returns>
    public bool ControlPrincipal
    {
      get
      {
        return (uint) (this.m_flag & SecurityPermissionFlag.ControlPrincipal) > 0U;
      }
      set
      {
        this.m_flag = value ? this.m_flag | SecurityPermissionFlag.ControlPrincipal : this.m_flag & ~SecurityPermissionFlag.ControlPrincipal;
      }
    }

    /// <summary>获取或设置一个值，该值指示是否声明了操作 <see cref="T:System.AppDomain" /> 的权限。</summary>
    /// <returns>如果声明了操作 <see cref="T:System.AppDomain" /> 的权限，则为 true；否则为 false。</returns>
    public bool ControlAppDomain
    {
      get
      {
        return (uint) (this.m_flag & SecurityPermissionFlag.ControlAppDomain) > 0U;
      }
      set
      {
        this.m_flag = value ? this.m_flag | SecurityPermissionFlag.ControlAppDomain : this.m_flag & ~SecurityPermissionFlag.ControlAppDomain;
      }
    }

    /// <summary>获取或设置一个值，该值指示代码是否可以配置远程处理类型和信道。</summary>
    /// <returns>如果代码可以配置远程处理类型和信道，则为 true；否则为 false。</returns>
    public bool RemotingConfiguration
    {
      get
      {
        return (uint) (this.m_flag & SecurityPermissionFlag.RemotingConfiguration) > 0U;
      }
      set
      {
        this.m_flag = value ? this.m_flag | SecurityPermissionFlag.RemotingConfiguration : this.m_flag & ~SecurityPermissionFlag.RemotingConfiguration;
      }
    }

    /// <summary>获取或设置一个值，该值指示代码是否可以插入公共语言运行时结构，例如添加远程处理上下文接收器 (Remoting Context Sink)、Envoy 接收器 (Envoy Sink) 和动态接收器 (Dynamic Sink)。</summary>
    /// <returns>如果代码可以插入公共语言运行时结构，则为 true；否则为 false。</returns>
    [ComVisible(true)]
    public bool Infrastructure
    {
      get
      {
        return (uint) (this.m_flag & SecurityPermissionFlag.Infrastructure) > 0U;
      }
      set
      {
        this.m_flag = value ? this.m_flag | SecurityPermissionFlag.Infrastructure : this.m_flag & ~SecurityPermissionFlag.Infrastructure;
      }
    }

    /// <summary>获取或设置一个值，该值指示代码是否具有在应用程序配置文件中执行绑定重定向的权限。</summary>
    /// <returns>如果代码可以执行绑定重定向，则为 true；否则为 false。</returns>
    public bool BindingRedirects
    {
      get
      {
        return (uint) (this.m_flag & SecurityPermissionFlag.BindingRedirects) > 0U;
      }
      set
      {
        this.m_flag = value ? this.m_flag | SecurityPermissionFlag.BindingRedirects : this.m_flag & ~SecurityPermissionFlag.BindingRedirects;
      }
    }

    /// <summary>用指定的 <see cref="T:System.Security.Permissions.SecurityAction" /> 初始化 <see cref="T:System.Security.Permissions.SecurityPermissionAttribute" /> 类的新实例。</summary>
    /// <param name="action">
    /// <see cref="T:System.Security.Permissions.SecurityAction" /> 值之一。</param>
    public SecurityPermissionAttribute(SecurityAction action)
      : base(action)
    {
    }

    /// <summary>创建并返回一个新的 <see cref="T:System.Security.Permissions.SecurityPermission" />。</summary>
    /// <returns>与此特性对应的 <see cref="T:System.Security.Permissions.SecurityPermission" />。</returns>
    public override IPermission CreatePermission()
    {
      if (this.m_unrestricted)
        return (IPermission) new SecurityPermission(PermissionState.Unrestricted);
      return (IPermission) new SecurityPermission(this.m_flag);
    }
  }
}
