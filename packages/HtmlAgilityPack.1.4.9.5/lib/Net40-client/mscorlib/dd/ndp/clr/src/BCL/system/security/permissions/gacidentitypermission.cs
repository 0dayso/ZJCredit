// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.GacIdentityPermissionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>允许对使用声明安全性应用到代码中的 <see cref="T:System.Security.Permissions.GacIdentityPermission" /> 进行安全操作。此类不能被继承。</summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
  [ComVisible(true)]
  [Serializable]
  public sealed class GacIdentityPermissionAttribute : CodeAccessSecurityAttribute
  {
    /// <summary>使用指定的 <see cref="T:System.Security.Permissions.SecurityAction" /> 值初始化 <see cref="T:System.Security.Permissions.GacIdentityPermissionAttribute" /> 类的新实例。</summary>
    /// <param name="action">
    /// <see cref="T:System.Security.Permissions.SecurityAction" /> 值之一。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="action" /> 参数不是有效的 <see cref="T:System.Security.Permissions.SecurityAction" /> 值。</exception>
    public GacIdentityPermissionAttribute(SecurityAction action)
      : base(action)
    {
    }

    /// <summary>创建一个新 <see cref="T:System.Security.Permissions.GacIdentityPermission" /> 对象。</summary>
    /// <returns>与此特性对应的 <see cref="T:System.Security.Permissions.GacIdentityPermission" />。</returns>
    public override IPermission CreatePermission()
    {
      return (IPermission) new GacIdentityPermission();
    }
  }
}
