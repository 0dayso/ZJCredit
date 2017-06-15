// Decompiled with JetBrains decompiler
// Type: System.Reflection.AssemblyVersionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>指定正在属性化的程序集的版本。</summary>
  [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class AssemblyVersionAttribute : Attribute
  {
    private string m_version;

    /// <summary>获取属性化程序集的版本号。</summary>
    /// <returns>包含程序集的版本号的字符串。</returns>
    [__DynamicallyInvokable]
    public string Version
    {
      [__DynamicallyInvokable] get
      {
        return this.m_version;
      }
    }

    /// <summary>使用正在属性化的程序集的版本号来初始化 AssemblyVersionAttribute 类的新实例。</summary>
    /// <param name="version">属性化程序集的版本号。</param>
    [__DynamicallyInvokable]
    public AssemblyVersionAttribute(string version)
    {
      this.m_version = version;
    }
  }
}
