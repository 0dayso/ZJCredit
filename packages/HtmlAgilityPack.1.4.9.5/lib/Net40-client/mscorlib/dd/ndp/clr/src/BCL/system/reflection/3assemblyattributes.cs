// Decompiled with JetBrains decompiler
// Type: System.Reflection.AssemblyCompanyAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>定义程序集清单的公司名称自定义属性。</summary>
  [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class AssemblyCompanyAttribute : Attribute
  {
    private string m_company;

    /// <summary>获取公司名称信息。</summary>
    /// <returns>包含公司名称的字符串。</returns>
    [__DynamicallyInvokable]
    public string Company
    {
      [__DynamicallyInvokable] get
      {
        return this.m_company;
      }
    }

    /// <summary>初始化 <see cref="T:System.Reflection.AssemblyCompanyAttribute" /> 类的新实例。</summary>
    /// <param name="company">公司名称信息。</param>
    [__DynamicallyInvokable]
    public AssemblyCompanyAttribute(string company)
    {
      this.m_company = company;
    }
  }
}
