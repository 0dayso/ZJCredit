// Decompiled with JetBrains decompiler
// Type: System.Security.HostProtectionException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace System.Security
{
  /// <summary>检测到被拒绝的主机资源时引发的异常。</summary>
  [ComVisible(true)]
  [Serializable]
  public class HostProtectionException : SystemException
  {
    private HostProtectionResource m_protected;
    private HostProtectionResource m_demanded;
    private const string ProtectedResourcesName = "ProtectedResources";
    private const string DemandedResourcesName = "DemandedResources";

    /// <summary>获取或设置部分受信任的代码不可访问的主机保护资源。</summary>
    /// <returns>
    /// <see cref="T:System.Security.Permissions.HostProtectionResource" /> 值的按位组合，用于标识不可访问的主机保护类别。默认值为 <see cref="F:System.Security.Permissions.HostProtectionResource.None" />。</returns>
    public HostProtectionResource ProtectedResources
    {
      get
      {
        return this.m_protected;
      }
    }

    /// <summary>获取或设置导致引发异常的要求的主机保护资源。</summary>
    /// <returns>
    /// <see cref="T:System.Security.Permissions.HostProtectionResource" /> 值的按位组合，用于标识导致引发异常的保护资源。默认值为 <see cref="F:System.Security.Permissions.HostProtectionResource.None" />。</returns>
    public HostProtectionResource DemandedResources
    {
      get
      {
        return this.m_demanded;
      }
    }

    /// <summary>使用默认值初始化 <see cref="T:System.Security.HostProtectionException" /> 类的新实例。</summary>
    public HostProtectionException()
    {
      this.m_protected = HostProtectionResource.None;
      this.m_demanded = HostProtectionResource.None;
    }

    /// <summary>使用指定的错误消息初始化 <see cref="T:System.Security.HostProtectionException" /> 类的新实例。</summary>
    /// <param name="message">描述错误的消息。</param>
    public HostProtectionException(string message)
      : base(message)
    {
      this.m_protected = HostProtectionResource.None;
      this.m_demanded = HostProtectionResource.None;
    }

    /// <summary>使用指定错误消息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.Security.HostProtectionException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="e">导致当前异常的异常。如果 <paramref name="innerException" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    public HostProtectionException(string message, Exception e)
      : base(message, e)
    {
      this.m_protected = HostProtectionResource.None;
      this.m_demanded = HostProtectionResource.None;
    }

    /// <summary>使用提供的序列化信息和流上下文初始化 <see cref="T:System.Security.HostProtectionException" /> 类的新实例。</summary>
    /// <param name="info">承载序列化对象数据的对象。</param>
    /// <param name="context">有关源或目标的上下文信息。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="info" /> 为 null。</exception>
    protected HostProtectionException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      this.m_protected = (HostProtectionResource) info.GetValue("ProtectedResources", typeof (HostProtectionResource));
      this.m_demanded = (HostProtectionResource) info.GetValue("DemandedResources", typeof (HostProtectionResource));
    }

    /// <summary>用指定的错误消息、受保护的主机资源和导致引发异常的主机资源初始化 <see cref="T:System.Security.HostProtectionException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="protectedResources">枚举值的按位组合，这些值指定部分受信任的代码不可访问的主机资源。</param>
    /// <param name="demandedResources">枚举值的按位组合，这些值指定所需的主机资源。</param>
    public HostProtectionException(string message, HostProtectionResource protectedResources, HostProtectionResource demandedResources)
      : base(message)
    {
      this.SetErrorCode(-2146232768);
      this.m_protected = protectedResources;
      this.m_demanded = demandedResources;
    }

    private HostProtectionException(HostProtectionResource protectedResources, HostProtectionResource demandedResources)
      : base(SecurityException.GetResString("HostProtection_HostProtection"))
    {
      this.SetErrorCode(-2146232768);
      this.m_protected = protectedResources;
      this.m_demanded = demandedResources;
    }

    private string ToStringHelper(string resourceString, object attr)
    {
      if (attr == null)
        return string.Empty;
      StringBuilder stringBuilder = new StringBuilder();
      string newLine1 = Environment.NewLine;
      stringBuilder.Append(newLine1);
      string newLine2 = Environment.NewLine;
      stringBuilder.Append(newLine2);
      string resourceString1 = Environment.GetResourceString(resourceString);
      stringBuilder.Append(resourceString1);
      string newLine3 = Environment.NewLine;
      stringBuilder.Append(newLine3);
      object obj = attr;
      stringBuilder.Append(obj);
      return stringBuilder.ToString();
    }

    /// <summary>返回当前主机保护异常的字符串表示形式。</summary>
    /// <returns>当前 <see cref="T:System.Security.HostProtectionException" /> 的字符串表示形式。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
    /// </PermissionSet>
    public override string ToString()
    {
      string stringHelper1 = this.ToStringHelper("HostProtection_ProtectedResources", (object) this.ProtectedResources);
      StringBuilder stringBuilder = new StringBuilder();
      string @string = base.ToString();
      stringBuilder.Append(@string);
      string str = stringHelper1;
      stringBuilder.Append(str);
      string stringHelper2 = this.ToStringHelper("HostProtection_DemandedResources", (object) this.DemandedResources);
      stringBuilder.Append(stringHelper2);
      return stringBuilder.ToString();
    }

    /// <summary>用有关主机保护异常的信息设置指定的 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 对象。</summary>
    /// <param name="info">有关引发的异常的序列化对象数据。</param>
    /// <param name="context">关于来源和目标的上下文信息</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="info" /> 为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    /// </PermissionSet>
    [SecurityCritical]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      base.GetObjectData(info, context);
      info.AddValue("ProtectedResources", (object) this.ProtectedResources, typeof (HostProtectionResource));
      info.AddValue("DemandedResources", (object) this.DemandedResources, typeof (HostProtectionResource));
    }
  }
}
