// Decompiled with JetBrains decompiler
// Type: System.Reflection.AssemblyProductAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>定义程序集清单的产品名称自定义属性。</summary>
  [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class AssemblyProductAttribute : Attribute
  {
    private string m_product;

    /// <summary>获取产品名称信息。</summary>
    /// <returns>包含产品名称的字符串。</returns>
    [__DynamicallyInvokable]
    public string Product
    {
      [__DynamicallyInvokable] get
      {
        return this.m_product;
      }
    }

    /// <summary>初始化 <see cref="T:System.Reflection.AssemblyProductAttribute" /> 类的新实例。</summary>
    /// <param name="product">产品名称信息。</param>
    [__DynamicallyInvokable]
    public AssemblyProductAttribute(string product)
    {
      this.m_product = product;
    }
  }
}
