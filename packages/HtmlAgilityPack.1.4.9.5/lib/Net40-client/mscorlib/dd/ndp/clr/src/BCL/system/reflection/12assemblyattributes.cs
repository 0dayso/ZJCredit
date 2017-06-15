// Decompiled with JetBrains decompiler
// Type: System.Reflection.AssemblyKeyFileAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>指定包含用于生成强名称的密钥对的文件名称。</summary>
  [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class AssemblyKeyFileAttribute : Attribute
  {
    private string m_keyFile;

    /// <summary>获取文件的名称，该文件包含用于为属性化程序集生成强名称的密钥对。</summary>
    /// <returns>包含密钥对所在文件的名称的字符串。</returns>
    [__DynamicallyInvokable]
    public string KeyFile
    {
      [__DynamicallyInvokable] get
      {
        return this.m_keyFile;
      }
    }

    /// <summary>使用文件的名称初始化 AssemblyKeyFileAttribute 类的新实例，该文件包含为正在属性化的程序集生成强名称的密钥对。</summary>
    /// <param name="keyFile">包含密钥对的文件的名称。</param>
    [__DynamicallyInvokable]
    public AssemblyKeyFileAttribute(string keyFile)
    {
      this.m_keyFile = keyFile;
    }
  }
}
