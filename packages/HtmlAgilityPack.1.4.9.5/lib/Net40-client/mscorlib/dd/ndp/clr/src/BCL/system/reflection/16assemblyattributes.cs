// Decompiled with JetBrains decompiler
// Type: System.Reflection.AssemblyMetadataAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Reflection
{
  /// <summary>定义密钥/值将数据用于装饰的程序集配对。</summary>
  [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
  [__DynamicallyInvokable]
  public sealed class AssemblyMetadataAttribute : Attribute
  {
    private string m_key;
    private string m_value;

    /// <summary>获取元数据密钥。</summary>
    /// <returns>元数据密钥。</returns>
    [__DynamicallyInvokable]
    public string Key
    {
      [__DynamicallyInvokable] get
      {
        return this.m_key;
      }
    }

    /// <summary>获取元数据值。</summary>
    /// <returns>元数据的值。</returns>
    [__DynamicallyInvokable]
    public string Value
    {
      [__DynamicallyInvokable] get
      {
        return this.m_value;
      }
    }

    /// <summary>使用指定的元数据密钥和值初始化 <see cref="T:System.Reflection.AssemblyMetadataAttribute" /> 类的新实例。</summary>
    /// <param name="key">元数据密钥。</param>
    /// <param name="value">元数据的值。</param>
    [__DynamicallyInvokable]
    public AssemblyMetadataAttribute(string key, string value)
    {
      this.m_key = key;
      this.m_value = value;
    }
  }
}
