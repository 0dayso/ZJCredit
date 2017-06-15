// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Contexts.ContextAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Contexts
{
  /// <summary>提供 <see cref="T:System.Runtime.Remoting.Contexts.IContextAttribute" /> 和 <see cref="T:System.Runtime.Remoting.Contexts.IContextProperty" /> 接口的默认实现。</summary>
  [SecurityCritical]
  [AttributeUsage(AttributeTargets.Class)]
  [ComVisible(true)]
  [Serializable]
  [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
  public class ContextAttribute : Attribute, IContextAttribute, IContextProperty
  {
    /// <summary>指示上下文特性的名称。</summary>
    protected string AttributeName;

    /// <summary>获取上下文特性的名称。</summary>
    /// <returns>上下文特性的名称。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public virtual string Name
    {
      [SecurityCritical] get
      {
        return this.AttributeName;
      }
    }

    /// <summary>使用指定的名称创建 <see cref="T:System.Runtime.Remoting.Contexts.ContextAttribute" /> 类的实例。</summary>
    /// <param name="name">上下文特性的名称。</param>
    public ContextAttribute(string name)
    {
      this.AttributeName = name;
    }

    /// <summary>返回一个指示上下文属性是否与新上下文兼容的布尔值。</summary>
    /// <returns>如果上下文属性与新上下文兼容，则为 true；否则为 false。</returns>
    /// <param name="newCtx">已在其中创建属性的新上下文。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public virtual bool IsNewContextOK(Context newCtx)
    {
      return true;
    }

    /// <summary>当上下文冻结时调用。</summary>
    /// <param name="newContext">要冻结的上下文。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public virtual void Freeze(Context newContext)
    {
    }

    /// <summary>返回一个布尔值，该值指示此实例是否与指定的对象相等。</summary>
    /// <returns>如果 <paramref name="o" /> 不为 null 且对象名称等效，则为 true；否则为 false。</returns>
    /// <param name="o">与该实例进行比较的对象。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public override bool Equals(object o)
    {
      IContextProperty contextProperty = o as IContextProperty;
      if (contextProperty != null)
        return this.AttributeName.Equals(contextProperty.Name);
      return false;
    }

    /// <summary>返回此 <see cref="T:System.Runtime.Remoting.Contexts.ContextAttribute" /> 实例的哈希代码。</summary>
    /// <returns>此 <see cref="T:System.Runtime.Remoting.Contexts.ContextAttribute" /> 实例的哈希代码。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public override int GetHashCode()
    {
      return this.AttributeName.GetHashCode();
    }

    /// <summary>返回一个指示该上下文参数是否满足上下文特性要求的布尔值。</summary>
    /// <returns>如果传入的上下文一切正常，则为 true；否则为 false。</returns>
    /// <param name="ctx">要在其中检查的上下文。</param>
    /// <param name="ctorMsg">将上下文属性添加到的 <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="ctx" /> 或 <paramref name="ctorMsg" /> 为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public virtual bool IsContextOK(Context ctx, IConstructionCallMessage ctorMsg)
    {
      if (ctx == null)
        throw new ArgumentNullException("ctx");
      if (ctorMsg == null)
        throw new ArgumentNullException("ctorMsg");
      if (!ctorMsg.ActivationType.IsContextful)
        return true;
      object obj = (object) ctx.GetProperty(this.AttributeName);
      return obj != null && this.Equals(obj);
    }

    /// <summary>将当前上下文属性添加到给定的消息。</summary>
    /// <param name="ctorMsg">将上下文属性添加到的 <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="ctorMsg" /> 参数为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public virtual void GetPropertiesForNewContext(IConstructionCallMessage ctorMsg)
    {
      if (ctorMsg == null)
        throw new ArgumentNullException("ctorMsg");
      ctorMsg.ContextProperties.Add((object) this);
    }
  }
}
