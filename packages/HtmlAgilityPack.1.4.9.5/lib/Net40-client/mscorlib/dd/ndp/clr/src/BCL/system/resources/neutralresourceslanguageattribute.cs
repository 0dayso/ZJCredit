// Decompiled with JetBrains decompiler
// Type: System.Resources.NeutralResourcesLanguageAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Resources
{
  /// <summary>通知应用程序默认区域性的资源控制器。此类不能被继承。</summary>
  [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class NeutralResourcesLanguageAttribute : Attribute
  {
    private string _culture;
    private UltimateResourceFallbackLocation _fallbackLoc;

    /// <summary>获取区域性名称。</summary>
    /// <returns>主程序集的默认区域性的名称。</returns>
    [__DynamicallyInvokable]
    public string CultureName
    {
      [__DynamicallyInvokable] get
      {
        return this._culture;
      }
    }

    /// <summary>获取 <see cref="T:System.Resources.ResourceManager" /> 类的位置，以用于通过资源后备进程检索非特定语言资源。</summary>
    /// <returns>枚举值之一，指示从中检索非特定资源的位置（主程序集或附属程序集）。</returns>
    public UltimateResourceFallbackLocation Location
    {
      get
      {
        return this._fallbackLoc;
      }
    }

    /// <summary>初始化 <see cref="T:System.Resources.NeutralResourcesLanguageAttribute" /> 类的新实例。</summary>
    /// <param name="cultureName">用其编写的当前程序集的非特定语言资源的区域性的名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="cultureName" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    public NeutralResourcesLanguageAttribute(string cultureName)
    {
      if (cultureName == null)
        throw new ArgumentNullException("cultureName");
      this._culture = cultureName;
      this._fallbackLoc = UltimateResourceFallbackLocation.MainAssembly;
    }

    /// <summary>使用指定的最终资源后备位置初始化 <see cref="T:System.Resources.NeutralResourcesLanguageAttribute" /> 类的新实例。</summary>
    /// <param name="cultureName">用其编写的当前程序集的非特定语言资源的区域性的名称。</param>
    /// <param name="location">枚举值之一，指示检索非特定后备资源的位置。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="cultureName" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="location" /> 不是 <see cref="T:System.Resources.UltimateResourceFallbackLocation" /> 的成员。</exception>
    public NeutralResourcesLanguageAttribute(string cultureName, UltimateResourceFallbackLocation location)
    {
      if (cultureName == null)
        throw new ArgumentNullException("cultureName");
      if (!Enum.IsDefined(typeof (UltimateResourceFallbackLocation), (object) location))
        throw new ArgumentException(Environment.GetResourceString("Arg_InvalidNeutralResourcesLanguage_FallbackLoc", (object) location));
      this._culture = cultureName;
      this._fallbackLoc = location;
    }
  }
}
