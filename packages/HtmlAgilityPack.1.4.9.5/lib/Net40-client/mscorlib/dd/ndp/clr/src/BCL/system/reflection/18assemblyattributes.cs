// Decompiled with JetBrains decompiler
// Type: System.Reflection.AssemblyKeyNameAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>指定 CSP 内某密钥容器的名称，该密钥容器包含用于生成强名称的密钥对。</summary>
  [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class AssemblyKeyNameAttribute : Attribute
  {
    private string m_keyName;

    /// <summary>获取容器的名称，该容器包含用于为属性化程序集生成强名称的密钥对。</summary>
    /// <returns>一个字符串，它包含带有相关密钥对的容器的名称。</returns>
    [__DynamicallyInvokable]
    public string KeyName
    {
      [__DynamicallyInvokable] get
      {
        return this.m_keyName;
      }
    }

    /// <summary>使用某容器的名称（该容器保存用于为正在属性化的程序集生成强名称的密钥对）来初始化 <see cref="T:System.Reflection.AssemblyKeyNameAttribute" /> 类的新实例。</summary>
    /// <param name="keyName">包含密钥对的容器的名称。</param>
    [__DynamicallyInvokable]
    public AssemblyKeyNameAttribute(string keyName)
    {
      this.m_keyName = keyName;
    }
  }
}
