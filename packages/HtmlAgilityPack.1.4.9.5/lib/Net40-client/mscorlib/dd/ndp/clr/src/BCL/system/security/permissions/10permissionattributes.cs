// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.UIPermissionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>允许对使用声明安全性应用到代码中的 <see cref="T:System.Security.Permissions.UIPermission" /> 进行安全操作。此类不能被继承。</summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
  [ComVisible(true)]
  [Serializable]
  public sealed class UIPermissionAttribute : CodeAccessSecurityAttribute
  {
    private UIPermissionWindow m_windowFlag;
    private UIPermissionClipboard m_clipboardFlag;

    /// <summary>获取或设置所允许的对窗口资源访问权限的类型。</summary>
    /// <returns>
    /// <see cref="T:System.Security.Permissions.UIPermissionWindow" /> 值之一。</returns>
    public UIPermissionWindow Window
    {
      get
      {
        return this.m_windowFlag;
      }
      set
      {
        this.m_windowFlag = value;
      }
    }

    /// <summary>获取或设置所允许的对剪贴板访问权限的类型。</summary>
    /// <returns>
    /// <see cref="T:System.Security.Permissions.UIPermissionClipboard" /> 值之一。</returns>
    public UIPermissionClipboard Clipboard
    {
      get
      {
        return this.m_clipboardFlag;
      }
      set
      {
        this.m_clipboardFlag = value;
      }
    }

    /// <summary>用指定的 <see cref="T:System.Security.Permissions.SecurityAction" /> 初始化 <see cref="T:System.Security.Permissions.UIPermissionAttribute" /> 类的新实例。</summary>
    /// <param name="action">
    /// <see cref="T:System.Security.Permissions.SecurityAction" /> 值之一。</param>
    public UIPermissionAttribute(SecurityAction action)
      : base(action)
    {
    }

    /// <summary>创建并返回一个新的 <see cref="T:System.Security.Permissions.UIPermission" />。</summary>
    /// <returns>与此特性对应的 <see cref="T:System.Security.Permissions.UIPermission" />。</returns>
    public override IPermission CreatePermission()
    {
      if (this.m_unrestricted)
        return (IPermission) new UIPermission(PermissionState.Unrestricted);
      return (IPermission) new UIPermission(this.m_windowFlag, this.m_clipboardFlag);
    }
  }
}
