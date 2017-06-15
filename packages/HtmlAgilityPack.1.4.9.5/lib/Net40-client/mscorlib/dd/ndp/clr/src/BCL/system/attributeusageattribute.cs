// Decompiled with JetBrains decompiler
// Type: System.AttributeUsageAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>指定另一特性类的用法。此类不能被继承。</summary>
  /// <filterpriority>1</filterpriority>
  [AttributeUsage(AttributeTargets.Class, Inherited = true)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class AttributeUsageAttribute : Attribute
  {
    internal static AttributeUsageAttribute Default = new AttributeUsageAttribute(AttributeTargets.All);
    internal AttributeTargets m_attributeTarget = AttributeTargets.All;
    internal bool m_inherited = true;
    internal bool m_allowMultiple;

    /// <summary>获取一组值，这组值标识指示的属性可应用到的程序元素。</summary>
    /// <returns>一个或多个 <see cref="T:System.AttributeTargets" /> 值。默认值为 All。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public AttributeTargets ValidOn
    {
      [__DynamicallyInvokable] get
      {
        return this.m_attributeTarget;
      }
    }

    /// <summary>获取或设置一个布尔值，该值指示能否为一个程序元素指定多个指示属性实例。</summary>
    /// <returns>如果允许指定多个实例，则为 true；否则为 false。默认值为 false。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public bool AllowMultiple
    {
      [__DynamicallyInvokable] get
      {
        return this.m_allowMultiple;
      }
      [__DynamicallyInvokable] set
      {
        this.m_allowMultiple = value;
      }
    }

    /// <summary>获取或设置一个布尔值，该值指示指示的属性能否由派生类和重写成员继承。</summary>
    /// <returns>如果该属性可由派生类和重写成员继承，则为 true，否则为 false。默认值为 true。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public bool Inherited
    {
      [__DynamicallyInvokable] get
      {
        return this.m_inherited;
      }
      [__DynamicallyInvokable] set
      {
        this.m_inherited = value;
      }
    }

    /// <summary>用指定的 <see cref="T:System.AttributeTargets" />、<see cref="P:System.AttributeUsageAttribute.AllowMultiple" /> 值和 <see cref="P:System.AttributeUsageAttribute.Inherited" /> 值列表初始化 <see cref="T:System.AttributeUsageAttribute" /> 类的新实例。</summary>
    /// <param name="validOn">使用按位"或"运算符组合的一组值，用于指示哪些程序元素是有效的。</param>
    [__DynamicallyInvokable]
    public AttributeUsageAttribute(AttributeTargets validOn)
    {
      this.m_attributeTarget = validOn;
    }

    internal AttributeUsageAttribute(AttributeTargets validOn, bool allowMultiple, bool inherited)
    {
      this.m_attributeTarget = validOn;
      this.m_allowMultiple = allowMultiple;
      this.m_inherited = inherited;
    }
  }
}
