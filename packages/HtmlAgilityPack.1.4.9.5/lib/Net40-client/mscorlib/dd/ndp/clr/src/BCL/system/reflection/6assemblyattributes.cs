// Decompiled with JetBrains decompiler
// Type: System.Reflection.AssemblyConfigurationAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>为程序集指定生成配置，例如发布或调试。</summary>
  [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class AssemblyConfigurationAttribute : Attribute
  {
    private string m_configuration;

    /// <summary>获取程序集配置信息。</summary>
    /// <returns>包含程序集配置信息的字符串。</returns>
    [__DynamicallyInvokable]
    public string Configuration
    {
      [__DynamicallyInvokable] get
      {
        return this.m_configuration;
      }
    }

    /// <summary>初始化 <see cref="T:System.Reflection.AssemblyConfigurationAttribute" /> 类的新实例。</summary>
    /// <param name="configuration">程序集配置。</param>
    [__DynamicallyInvokable]
    public AssemblyConfigurationAttribute(string configuration)
    {
      this.m_configuration = configuration;
    }
  }
}
