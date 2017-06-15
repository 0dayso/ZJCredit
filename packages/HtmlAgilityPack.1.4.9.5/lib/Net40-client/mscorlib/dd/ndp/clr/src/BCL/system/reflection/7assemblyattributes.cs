// Decompiled with JetBrains decompiler
// Type: System.Reflection.AssemblyDefaultAliasAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>定义程序集清单的友好默认别名。</summary>
  [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class AssemblyDefaultAliasAttribute : Attribute
  {
    private string m_defaultAlias;

    /// <summary>获取默认别名信息。</summary>
    /// <returns>包含默认别名信息的字符串。</returns>
    [__DynamicallyInvokable]
    public string DefaultAlias
    {
      [__DynamicallyInvokable] get
      {
        return this.m_defaultAlias;
      }
    }

    /// <summary>初始化 <see cref="T:System.Reflection.AssemblyDefaultAliasAttribute" /> 类的新实例。</summary>
    /// <param name="defaultAlias">程序集默认别名信息。</param>
    [__DynamicallyInvokable]
    public AssemblyDefaultAliasAttribute(string defaultAlias)
    {
      this.m_defaultAlias = defaultAlias;
    }
  }
}
