// Decompiled with JetBrains decompiler
// Type: System.Reflection.AssemblyTrademarkAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>定义程序集清单的商标自定义属性。</summary>
  [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class AssemblyTrademarkAttribute : Attribute
  {
    private string m_trademark;

    /// <summary>获取商标信息。</summary>
    /// <returns>包含商标信息的 String。</returns>
    [__DynamicallyInvokable]
    public string Trademark
    {
      [__DynamicallyInvokable] get
      {
        return this.m_trademark;
      }
    }

    /// <summary>初始化 <see cref="T:System.Reflection.AssemblyTrademarkAttribute" /> 类的新实例。</summary>
    /// <param name="trademark">商标信息。</param>
    [__DynamicallyInvokable]
    public AssemblyTrademarkAttribute(string trademark)
    {
      this.m_trademark = trademark;
    }
  }
}
