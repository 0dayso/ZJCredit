// Decompiled with JetBrains decompiler
// Type: System.Reflection.AssemblyCultureAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>指定程序集支持哪个区域性。</summary>
  [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class AssemblyCultureAttribute : Attribute
  {
    private string m_culture;

    /// <summary>获取属性化程序集支持的区域性。</summary>
    /// <returns>包含受支持区域性的名称的字符串。</returns>
    [__DynamicallyInvokable]
    public string Culture
    {
      [__DynamicallyInvokable] get
      {
        return this.m_culture;
      }
    }

    /// <summary>用正在属性化的程序集支持的区域性初始化 <see cref="T:System.Reflection.AssemblyCultureAttribute" /> 类的新实例。</summary>
    /// <param name="culture">属性化程序集支持的区域性。</param>
    [__DynamicallyInvokable]
    public AssemblyCultureAttribute(string culture)
    {
      this.m_culture = culture;
    }
  }
}
