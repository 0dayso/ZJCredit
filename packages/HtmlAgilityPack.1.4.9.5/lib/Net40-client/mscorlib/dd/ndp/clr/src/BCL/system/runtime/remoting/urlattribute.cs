// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Activation.UrlAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Security;

namespace System.Runtime.Remoting.Activation
{
  /// <summary>定义可在调用站点上用于指定将发生激活的 URL 的特性。此类不能被继承。</summary>
  [SecurityCritical]
  [ComVisible(true)]
  [Serializable]
  public sealed class UrlAttribute : ContextAttribute
  {
    private static string propertyName = "UrlAttribute";
    private string url;

    /// <summary>获取 <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> 的 URL 值。</summary>
    /// <returns>
    /// <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> 的 URL 值。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public string UrlValue
    {
      [SecurityCritical] get
      {
        return this.url;
      }
    }

    /// <summary>创建 <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> 类的新实例。</summary>
    /// <param name="callsiteURL">调用站点 URL。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="callsiteURL" /> 参数为 null。</exception>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    [SecurityCritical]
    public UrlAttribute(string callsiteURL)
      : base(UrlAttribute.propertyName)
    {
      if (callsiteURL == null)
        throw new ArgumentNullException("callsiteURL");
      this.url = callsiteURL;
    }

    /// <summary>检查指定对象是否与当前实例引用相同的 URL。</summary>
    /// <returns>如果对象为具有相同值的 <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />，则为 true；否则为 false。</returns>
    /// <param name="o">与当前的 <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> 比较的对象。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public override bool Equals(object o)
    {
      if (o is IContextProperty && o is UrlAttribute)
        return ((UrlAttribute) o).UrlValue.Equals(this.url);
      return false;
    }

    /// <summary>返回当前 <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> 的哈希值。</summary>
    /// <returns>当前 <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> 的哈希值。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public override int GetHashCode()
    {
      return this.url.GetHashCode();
    }

    /// <summary>返回一个布尔值，该值指示指定的 <see cref="T:System.Runtime.Remoting.Contexts.Context" /> 是否满足 <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> 的要求。</summary>
    /// <returns>如果传入的上下文是可接受的，则为 true；否则为 false。</returns>
    /// <param name="ctx">当前上下文特性检查所依据的上下文。</param>
    /// <param name="msg">结构调用，需要依据当前上下文检查其参数。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    [ComVisible(true)]
    public override bool IsContextOK(Context ctx, IConstructionCallMessage msg)
    {
      return false;
    }

    /// <summary>强制在指定 URL 处的上下文内创建上下文和服务器对象。</summary>
    /// <param name="ctorMsg">要创建的服务器对象的 <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" />。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    [ComVisible(true)]
    public override void GetPropertiesForNewContext(IConstructionCallMessage ctorMsg)
    {
    }
  }
}
