// Decompiled with JetBrains decompiler
// Type: System.CLSCompliantAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>指示程序元素是否符合公共语言规范 (CLS)。此类不能被继承。</summary>
  /// <filterpriority>1</filterpriority>
  [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class CLSCompliantAttribute : Attribute
  {
    private bool m_compliant;

    /// <summary>获取指示所指示的程序元素是否符合 CLS 的布尔值。</summary>
    /// <returns>如果程序元素符合 CLS，则为 true；否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public bool IsCompliant
    {
      [__DynamicallyInvokable] get
      {
        return this.m_compliant;
      }
    }

    /// <summary>用布尔值初始化 <see cref="T:System.CLSCompliantAttribute" /> 类的实例，该值指示所指示的程序元素是否符合 CLS。</summary>
    /// <param name="isCompliant">如果程序元素符合 CLS，则为 true；否则为 false。</param>
    [__DynamicallyInvokable]
    public CLSCompliantAttribute(bool isCompliant)
    {
      this.m_compliant = isCompliant;
    }
  }
}
