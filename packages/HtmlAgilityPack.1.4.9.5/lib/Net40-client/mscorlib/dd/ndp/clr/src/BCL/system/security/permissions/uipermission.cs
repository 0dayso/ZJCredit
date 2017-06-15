// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.UIPermission
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Permissions
{
  /// <summary>控制与用户界面和剪贴板相关的权限。此类不能被继承。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class UIPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
  {
    private UIPermissionWindow m_windowFlag;
    private UIPermissionClipboard m_clipboardFlag;

    /// <summary>获取或设置由该权限表示的窗口访问权限。</summary>
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
        UIPermission.VerifyWindowFlag(value);
        this.m_windowFlag = value;
      }
    }

    /// <summary>获取或设置由该权限表示的剪贴板访问权限。</summary>
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
        UIPermission.VerifyClipboardFlag(value);
        this.m_clipboardFlag = value;
      }
    }

    /// <summary>初始化 <see cref="T:System.Security.Permissions.UIPermission" /> 类的新实例，该实例可根据指定具有完全受限制的访问权限或不受限制的访问权限。</summary>
    /// <param name="state">枚举值之一。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="state" /> 参数不是有效的 <see cref="T:System.Security.Permissions.PermissionState" />。</exception>
    public UIPermission(PermissionState state)
    {
      if (state == PermissionState.Unrestricted)
      {
        this.SetUnrestricted(true);
      }
      else
      {
        if (state != PermissionState.None)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
        this.SetUnrestricted(false);
        this.Reset();
      }
    }

    /// <summary>用指定的窗口和剪贴板权限初始化 <see cref="T:System.Security.Permissions.UIPermission" /> 类的新实例。</summary>
    /// <param name="windowFlag">枚举值之一。</param>
    /// <param name="clipboardFlag">枚举值之一。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="windowFlag" /> 参数不是有效的 <see cref="T:System.Security.Permissions.UIPermissionWindow" /> 值。- 或 -<paramref name="clipboardFlag" /> 参数不是有效的 <see cref="T:System.Security.Permissions.UIPermissionClipboard" /> 值。</exception>
    public UIPermission(UIPermissionWindow windowFlag, UIPermissionClipboard clipboardFlag)
    {
      UIPermission.VerifyWindowFlag(windowFlag);
      UIPermission.VerifyClipboardFlag(clipboardFlag);
      this.m_windowFlag = windowFlag;
      this.m_clipboardFlag = clipboardFlag;
    }

    /// <summary>在具有使用窗口的权限，但没有对剪贴板的访问权限的情况下，初始化 <see cref="T:System.Security.Permissions.UIPermission" /> 类的新实例。</summary>
    /// <param name="windowFlag">枚举值之一。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="windowFlag" /> 参数不是有效的 <see cref="T:System.Security.Permissions.UIPermissionWindow" /> 值。</exception>
    public UIPermission(UIPermissionWindow windowFlag)
    {
      UIPermission.VerifyWindowFlag(windowFlag);
      this.m_windowFlag = windowFlag;
    }

    /// <summary>在具有使用剪贴板的权限，但没有对窗口的访问权限的情况下，初始化 <see cref="T:System.Security.Permissions.UIPermission" /> 类的新实例。</summary>
    /// <param name="clipboardFlag">枚举值之一。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="clipboardFlag" /> 参数不是有效的 <see cref="T:System.Security.Permissions.UIPermissionClipboard" /> 值。</exception>
    public UIPermission(UIPermissionClipboard clipboardFlag)
    {
      UIPermission.VerifyClipboardFlag(clipboardFlag);
      this.m_clipboardFlag = clipboardFlag;
    }

    private static void VerifyWindowFlag(UIPermissionWindow flag)
    {
      if (flag < UIPermissionWindow.NoWindows || flag > UIPermissionWindow.AllWindows)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) flag));
    }

    private static void VerifyClipboardFlag(UIPermissionClipboard flag)
    {
      if (flag < UIPermissionClipboard.NoClipboard || flag > UIPermissionClipboard.AllClipboard)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) flag));
    }

    private void Reset()
    {
      this.m_windowFlag = UIPermissionWindow.NoWindows;
      this.m_clipboardFlag = UIPermissionClipboard.NoClipboard;
    }

    private void SetUnrestricted(bool unrestricted)
    {
      if (!unrestricted)
        return;
      this.m_windowFlag = UIPermissionWindow.AllWindows;
      this.m_clipboardFlag = UIPermissionClipboard.AllClipboard;
    }

    /// <summary>返回一个值，该值指示当前权限是否为无限制的。</summary>
    /// <returns>如果当前权限是无限制的，则为 true；否则为 false。</returns>
    public bool IsUnrestricted()
    {
      if (this.m_windowFlag == UIPermissionWindow.AllWindows)
        return this.m_clipboardFlag == UIPermissionClipboard.AllClipboard;
      return false;
    }

    /// <summary>确定当前权限是否为指定权限的子集。</summary>
    /// <returns>如果当前权限是指定权限的子集，则为 true；否则为 false。</returns>
    /// <param name="target">要测试子集关系的权限。此权限必须与当前权限属于同一类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="target" /> 参数不是 null，而且与当前权限不是同一类型。</exception>
    public override bool IsSubsetOf(IPermission target)
    {
      if (target == null)
      {
        if (this.m_windowFlag == UIPermissionWindow.NoWindows)
          return this.m_clipboardFlag == UIPermissionClipboard.NoClipboard;
        return false;
      }
      try
      {
        UIPermission uiPermission = (UIPermission) target;
        if (uiPermission.IsUnrestricted())
          return true;
        if (this.IsUnrestricted())
          return false;
        return this.m_windowFlag <= uiPermission.m_windowFlag && this.m_clipboardFlag <= uiPermission.m_clipboardFlag;
      }
      catch (InvalidCastException ex)
      {
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      }
    }

    /// <summary>创建并返回一个权限，该权限是当前权限和指定权限的交集。</summary>
    /// <returns>一个新权限，它表示当前权限与指定权限的交集。如果交集为空，则此新权限为 null。</returns>
    /// <param name="target">要与当前权限相交的权限。它必须与当前权限属于同一类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="target" /> 参数不是 null，而且与当前权限不是同一类型。</exception>
    public override IPermission Intersect(IPermission target)
    {
      if (target == null)
        return (IPermission) null;
      if (!this.VerifyType(target))
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      UIPermission uiPermission = (UIPermission) target;
      UIPermissionWindow windowFlag = this.m_windowFlag < uiPermission.m_windowFlag ? this.m_windowFlag : uiPermission.m_windowFlag;
      UIPermissionClipboard clipboardFlag = this.m_clipboardFlag < uiPermission.m_clipboardFlag ? this.m_clipboardFlag : uiPermission.m_clipboardFlag;
      if (windowFlag == UIPermissionWindow.NoWindows && clipboardFlag == UIPermissionClipboard.NoClipboard)
        return (IPermission) null;
      return (IPermission) new UIPermission(windowFlag, clipboardFlag);
    }

    /// <summary>创建一个权限，该权限是当前权限和指定权限的并集。</summary>
    /// <returns>一个新权限，它表示当前权限与指定权限的并集。</returns>
    /// <param name="target">将与当前权限合并的权限。它必须与当前权限属于同一类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="target" /> 参数不是 null，而且与当前权限不是同一类型。</exception>
    public override IPermission Union(IPermission target)
    {
      if (target == null)
        return this.Copy();
      if (!this.VerifyType(target))
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      UIPermission uiPermission = (UIPermission) target;
      UIPermissionWindow windowFlag = this.m_windowFlag > uiPermission.m_windowFlag ? this.m_windowFlag : uiPermission.m_windowFlag;
      UIPermissionClipboard clipboardFlag = this.m_clipboardFlag > uiPermission.m_clipboardFlag ? this.m_clipboardFlag : uiPermission.m_clipboardFlag;
      if (windowFlag == UIPermissionWindow.NoWindows && clipboardFlag == UIPermissionClipboard.NoClipboard)
        return (IPermission) null;
      return (IPermission) new UIPermission(windowFlag, clipboardFlag);
    }

    /// <summary>创建并返回当前权限的相同副本。</summary>
    /// <returns>当前权限的副本。</returns>
    public override IPermission Copy()
    {
      return (IPermission) new UIPermission(this.m_windowFlag, this.m_clipboardFlag);
    }

    /// <summary>创建权限及其当前状态的 XML 编码。</summary>
    /// <returns>权限的 XML 编码，包括任何状态信息。</returns>
    public override SecurityElement ToXml()
    {
      SecurityElement permissionElement = CodeAccessPermission.CreatePermissionElement((IPermission) this, "System.Security.Permissions.UIPermission");
      if (!this.IsUnrestricted())
      {
        if (this.m_windowFlag != UIPermissionWindow.NoWindows)
          permissionElement.AddAttribute("Window", Enum.GetName(typeof (UIPermissionWindow), (object) this.m_windowFlag));
        if (this.m_clipboardFlag != UIPermissionClipboard.NoClipboard)
          permissionElement.AddAttribute("Clipboard", Enum.GetName(typeof (UIPermissionClipboard), (object) this.m_clipboardFlag));
      }
      else
        permissionElement.AddAttribute("Unrestricted", "true");
      return permissionElement;
    }

    /// <summary>从 XML 编码重新构造具有指定状态的权限。</summary>
    /// <param name="esd">用于重新构造权限的 XML 编码。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="esd" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="esd" /> 参数不是有效的权限元素。- 或 -<paramref name="esd" /> 参数的版本号无效。</exception>
    public override void FromXml(SecurityElement esd)
    {
      CodeAccessPermission.ValidateElement(esd, (IPermission) this);
      if (XMLUtil.IsUnrestricted(esd))
      {
        this.SetUnrestricted(true);
      }
      else
      {
        this.m_windowFlag = UIPermissionWindow.NoWindows;
        this.m_clipboardFlag = UIPermissionClipboard.NoClipboard;
        string str1 = esd.Attribute("Window");
        if (str1 != null)
          this.m_windowFlag = (UIPermissionWindow) Enum.Parse(typeof (UIPermissionWindow), str1);
        string str2 = esd.Attribute("Clipboard");
        if (str2 == null)
          return;
        this.m_clipboardFlag = (UIPermissionClipboard) Enum.Parse(typeof (UIPermissionClipboard), str2);
      }
    }

    int IBuiltInPermission.GetTokenIndex()
    {
      return UIPermission.GetTokenIndex();
    }

    internal static int GetTokenIndex()
    {
      return 7;
    }
  }
}
