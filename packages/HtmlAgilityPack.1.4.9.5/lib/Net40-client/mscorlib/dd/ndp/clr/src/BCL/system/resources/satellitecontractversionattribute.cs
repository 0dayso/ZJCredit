// Decompiled with JetBrains decompiler
// Type: System.Resources.SatelliteContractVersionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Resources
{
  /// <summary>指示 <see cref="T:System.Resources.ResourceManager" /> 对象要求附属程序集的特定版本。</summary>
  [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class SatelliteContractVersionAttribute : Attribute
  {
    private string _version;

    /// <summary>获取具有所需资源的附属程序集的版本。</summary>
    /// <returns>一个字符串，它包含具有所需资源的附属程序集的版本。</returns>
    [__DynamicallyInvokable]
    public string Version
    {
      [__DynamicallyInvokable] get
      {
        return this._version;
      }
    }

    /// <summary>初始化 <see cref="T:System.Resources.SatelliteContractVersionAttribute" /> 类的新实例。</summary>
    /// <param name="version">一个字符串，指定要加载的附属程序集的版本。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="version" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    public SatelliteContractVersionAttribute(string version)
    {
      if (version == null)
        throw new ArgumentNullException("version");
      this._version = version;
    }
  }
}
