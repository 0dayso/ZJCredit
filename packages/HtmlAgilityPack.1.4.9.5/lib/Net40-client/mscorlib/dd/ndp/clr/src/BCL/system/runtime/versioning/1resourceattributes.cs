// Decompiled with JetBrains decompiler
// Type: System.Runtime.Versioning.ResourceExposureAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;

namespace System.Runtime.Versioning
{
  /// <summary>指定类的成员的资源公开范围。此类不能被继承。</summary>
  [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field, Inherited = false)]
  [Conditional("RESOURCE_ANNOTATION_WORK")]
  public sealed class ResourceExposureAttribute : Attribute
  {
    private ResourceScope _resourceExposureLevel;

    /// <summary>获取资源公开范围。</summary>
    /// <returns>一个 <see cref="T:System.Runtime.Versioning.ResourceScope" /> 对象。</returns>
    public ResourceScope ResourceExposureLevel
    {
      get
      {
        return this._resourceExposureLevel;
      }
    }

    /// <summary>用指定的公开级别初始化 <see cref="T:System.Runtime.Versioning.ResourceExposureAttribute" /> 类的新实例。</summary>
    /// <param name="exposureLevel">资源的范围。</param>
    public ResourceExposureAttribute(ResourceScope exposureLevel)
    {
      this._resourceExposureLevel = exposureLevel;
    }
  }
}
