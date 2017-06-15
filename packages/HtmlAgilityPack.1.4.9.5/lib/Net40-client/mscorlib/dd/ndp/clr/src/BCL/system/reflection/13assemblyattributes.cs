// Decompiled with JetBrains decompiler
// Type: System.Reflection.AssemblyDelaySignAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>指定程序集在创建时未完全签名。</summary>
  [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class AssemblyDelaySignAttribute : Attribute
  {
    private bool m_delaySign;

    /// <summary>获取指示该属性状态的值。</summary>
    /// <returns>如果此程序集已采用延迟签名方式生成，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool DelaySign
    {
      [__DynamicallyInvokable] get
      {
        return this.m_delaySign;
      }
    }

    /// <summary>初始化 <see cref="T:System.Reflection.AssemblyDelaySignAttribute" /> 类的新实例。</summary>
    /// <param name="delaySign">如果此属性表示的功能被激活，则为 true；否则为 false。</param>
    [__DynamicallyInvokable]
    public AssemblyDelaySignAttribute(bool delaySign)
    {
      this.m_delaySign = delaySign;
    }
  }
}
