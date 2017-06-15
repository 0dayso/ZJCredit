// Decompiled with JetBrains decompiler
// Type: System.Reflection.AssemblyDescriptionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>提供程序集的文本说明。</summary>
  [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class AssemblyDescriptionAttribute : Attribute
  {
    private string m_description;

    /// <summary>获取程序集说明信息。</summary>
    /// <returns>包含程序集说明的字符串。</returns>
    [__DynamicallyInvokable]
    public string Description
    {
      [__DynamicallyInvokable] get
      {
        return this.m_description;
      }
    }

    /// <summary>初始化 <see cref="T:System.Reflection.AssemblyDescriptionAttribute" /> 类的新实例。</summary>
    /// <param name="description">程序集说明。</param>
    [__DynamicallyInvokable]
    public AssemblyDescriptionAttribute(string description)
    {
      this.m_description = description;
    }
  }
}
