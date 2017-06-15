// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Contexts.ContextProperty
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Contexts
{
  /// <summary>保存属性名称的名称/值对和表示上下文属性的对象。</summary>
  [SecurityCritical]
  [ComVisible(true)]
  [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
  public class ContextProperty
  {
    internal string _name;
    internal object _property;

    /// <summary>获取 T:System.Runtime.Remoting.Contexts.ContextProperty 类的名称。</summary>
    /// <returns>
    /// <see cref="T:System.Runtime.Remoting.Contexts.ContextProperty" /> 类的名称。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public virtual string Name
    {
      get
      {
        return this._name;
      }
    }

    /// <summary>获取表示上下文属性的对象。</summary>
    /// <returns>表示上下文属性的对象。</returns>
    public virtual object Property
    {
      get
      {
        return this._property;
      }
    }

    internal ContextProperty(string name, object prop)
    {
      this._name = name;
      this._property = prop;
    }
  }
}
