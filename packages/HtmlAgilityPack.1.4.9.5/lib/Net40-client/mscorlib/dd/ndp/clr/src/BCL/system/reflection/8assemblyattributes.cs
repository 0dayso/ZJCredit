// Decompiled with JetBrains decompiler
// Type: System.Reflection.AssemblyInformationalVersionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>定义程序集清单的其他版本信息。</summary>
  [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class AssemblyInformationalVersionAttribute : Attribute
  {
    private string m_informationalVersion;

    /// <summary>获取版本信息。</summary>
    /// <returns>包含版本信息的字符串。</returns>
    [__DynamicallyInvokable]
    public string InformationalVersion
    {
      [__DynamicallyInvokable] get
      {
        return this.m_informationalVersion;
      }
    }

    /// <summary>初始化 <see cref="T:System.Reflection.AssemblyInformationalVersionAttribute" /> 类的新实例。</summary>
    /// <param name="informationalVersion">程序集版本信息。</param>
    [__DynamicallyInvokable]
    public AssemblyInformationalVersionAttribute(string informationalVersion)
    {
      this.m_informationalVersion = informationalVersion;
    }
  }
}
