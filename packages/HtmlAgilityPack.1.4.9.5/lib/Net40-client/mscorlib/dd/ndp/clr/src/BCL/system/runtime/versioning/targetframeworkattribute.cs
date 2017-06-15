// Decompiled with JetBrains decompiler
// Type: System.Runtime.Versioning.TargetFrameworkAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.Versioning
{
  /// <summary>标识某个特定程序集编译时针对 .NET Framework 的版本。</summary>
  [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
  [__DynamicallyInvokable]
  public sealed class TargetFrameworkAttribute : Attribute
  {
    private string _frameworkName;
    private string _frameworkDisplayName;

    /// <summary>获取编译某个特定程序集时针对的 .NET Framework 版本的名称。</summary>
    /// <returns>编译该程序集时针对的 .NET Framework 版本的名称。</returns>
    [__DynamicallyInvokable]
    public string FrameworkName
    {
      [__DynamicallyInvokable] get
      {
        return this._frameworkName;
      }
    }

    /// <summary>获取生成某个程序集时针对的 .NET Framework 版本的显示名称。</summary>
    /// <returns>.NET Framework 版本的显示名称。</returns>
    [__DynamicallyInvokable]
    public string FrameworkDisplayName
    {
      [__DynamicallyInvokable] get
      {
        return this._frameworkDisplayName;
      }
      [__DynamicallyInvokable] set
      {
        this._frameworkDisplayName = value;
      }
    }

    /// <summary>通过指定一个程序集在生成时针对的 .NET Framework 版本，初始化 <see cref="T:System.Runtime.Versioning.TargetFrameworkAttribute" /> 类的实例。</summary>
    /// <param name="frameworkName">生成该程序集时针对的 .NET Framework 的版本。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="frameworkName" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public TargetFrameworkAttribute(string frameworkName)
    {
      if (frameworkName == null)
        throw new ArgumentNullException("frameworkName");
      this._frameworkName = frameworkName;
    }
  }
}
