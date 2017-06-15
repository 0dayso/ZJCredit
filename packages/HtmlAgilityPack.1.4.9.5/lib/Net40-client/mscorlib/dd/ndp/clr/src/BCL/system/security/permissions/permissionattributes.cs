// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.SecurityAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>为 <see cref="T:System.Security.Permissions.CodeAccessSecurityAttribute" /> 派生自的声明安全性指定基特性类。</summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
  [ComVisible(true)]
  [Serializable]
  public abstract class SecurityAttribute : Attribute
  {
    internal SecurityAction m_action;
    internal bool m_unrestricted;

    /// <summary>获取或设置安全性操作。</summary>
    /// <returns>
    /// <see cref="T:System.Security.Permissions.SecurityAction" /> 值之一。</returns>
    public SecurityAction Action
    {
      get
      {
        return this.m_action;
      }
      set
      {
        this.m_action = value;
      }
    }

    /// <summary>获取或设置一个值，该值指示是否声明了对受该特性保护的资源有完全（无限制的）权限。</summary>
    /// <returns>如果声明了对受保护资源的完全权限，则为 true；否则为 false。</returns>
    public bool Unrestricted
    {
      get
      {
        return this.m_unrestricted;
      }
      set
      {
        this.m_unrestricted = value;
      }
    }

    /// <summary>用指定的 <see cref="T:System.Security.Permissions.SecurityAction" /> 初始化 <see cref="T:System.Security.Permissions.SecurityAttribute" /> 的新实例。</summary>
    /// <param name="action">
    /// <see cref="T:System.Security.Permissions.SecurityAction" /> 值之一。</param>
    protected SecurityAttribute(SecurityAction action)
    {
      this.m_action = action;
    }

    /// <summary>在派生类中重写时，将创建一个权限对象，该对象随后可序列化为二进制格式并与 <see cref="T:System.Security.Permissions.SecurityAction" /> 一起永久地存储在程序集的元数据中。</summary>
    /// <returns>可序列化的权限对象。</returns>
    public abstract IPermission CreatePermission();

    [SecurityCritical]
    internal static IntPtr FindSecurityAttributeTypeHandle(string typeName)
    {
      PermissionSet.s_fullTrust.Assert();
      Type type = Type.GetType(typeName, false, false);
      if (type == (Type) null)
        return IntPtr.Zero;
      return type.TypeHandle.Value;
    }
  }
}
