// Decompiled with JetBrains decompiler
// Type: System.Reflection.AssemblyTitleAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>指定程序集的说明。</summary>
  [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class AssemblyTitleAttribute : Attribute
  {
    private string m_title;

    /// <summary>获取程序集标题信息。</summary>
    /// <returns>程序集标题。</returns>
    [__DynamicallyInvokable]
    public string Title
    {
      [__DynamicallyInvokable] get
      {
        return this.m_title;
      }
    }

    /// <summary>初始化 <see cref="T:System.Reflection.AssemblyTitleAttribute" /> 类的新实例。</summary>
    /// <param name="title">程序集标题。</param>
    [__DynamicallyInvokable]
    public AssemblyTitleAttribute(string title)
    {
      this.m_title = title;
    }
  }
}
