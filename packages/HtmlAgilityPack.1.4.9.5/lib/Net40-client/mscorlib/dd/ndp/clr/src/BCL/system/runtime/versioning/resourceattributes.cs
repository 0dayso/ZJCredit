// Decompiled with JetBrains decompiler
// Type: System.Runtime.Versioning.ResourceConsumptionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;

namespace System.Runtime.Versioning
{
  /// <summary>指定由类成员使用的资源。此类不能被继承。</summary>
  [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property, Inherited = false)]
  [Conditional("RESOURCE_ANNOTATION_WORK")]
  public sealed class ResourceConsumptionAttribute : Attribute
  {
    private ResourceScope _consumptionScope;
    private ResourceScope _resourceScope;

    /// <summary>获取所用资源的资源范围。</summary>
    /// <returns>一个 <see cref="T:System.Runtime.Versioning.ResourceScope" /> 对象，指定所用成员的资源范围。</returns>
    public ResourceScope ResourceScope
    {
      get
      {
        return this._resourceScope;
      }
    }

    /// <summary>获取此成员的使用范围。</summary>
    /// <returns>一个 <see cref="T:System.Runtime.Versioning.ResourceScope" /> 对象，指定此成员使用的资源范围。</returns>
    public ResourceScope ConsumptionScope
    {
      get
      {
        return this._consumptionScope;
      }
    }

    /// <summary>初始化 <see cref="T:System.Runtime.Versioning.ResourceConsumptionAttribute" /> 类的新实例，并指定所用资源的范围。</summary>
    /// <param name="resourceScope">所用资源的 <see cref="T:System.Runtime.Versioning.ResourceScope" />。</param>
    public ResourceConsumptionAttribute(ResourceScope resourceScope)
    {
      this._resourceScope = resourceScope;
      this._consumptionScope = this._resourceScope;
    }

    /// <summary>初始化 <see cref="T:System.Runtime.Versioning.ResourceConsumptionAttribute" /> 类的新实例，并指定所用资源的范围及其实际使用范围。</summary>
    /// <param name="resourceScope">所用资源的 <see cref="T:System.Runtime.Versioning.ResourceScope" />。</param>
    /// <param name="consumptionScope">此成员使用的 <see cref="T:System.Runtime.Versioning.ResourceScope" />。</param>
    public ResourceConsumptionAttribute(ResourceScope resourceScope, ResourceScope consumptionScope)
    {
      this._resourceScope = resourceScope;
      this._consumptionScope = consumptionScope;
    }
  }
}
