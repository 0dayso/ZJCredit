// Decompiled with JetBrains decompiler
// Type: System.Reflection.AssemblyCopyrightAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>定义程序集清单的版权自定义属性。</summary>
  [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class AssemblyCopyrightAttribute : Attribute
  {
    private string m_copyright;

    /// <summary>获取版权信息。</summary>
    /// <returns>包含版权信息的字符串。</returns>
    [__DynamicallyInvokable]
    public string Copyright
    {
      [__DynamicallyInvokable] get
      {
        return this.m_copyright;
      }
    }

    /// <summary>初始化 <see cref="T:System.Reflection.AssemblyCopyrightAttribute" /> 类的新实例。</summary>
    /// <param name="copyright">版权信息。</param>
    [__DynamicallyInvokable]
    public AssemblyCopyrightAttribute(string copyright)
    {
      this.m_copyright = copyright;
    }
  }
}
