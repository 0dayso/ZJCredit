// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.BestFitMappingAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>控制是否将 Unicode 字符转换为最接近的匹配 ANSI 字符。</summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class BestFitMappingAttribute : Attribute
  {
    internal bool _bestFitMapping;
    /// <summary>启用或禁用在遇到已被转换为 ANSI“?”字符的无法映射的 Unicode 字符时引发异常。</summary>
    [__DynamicallyInvokable]
    public bool ThrowOnUnmappableChar;

    /// <summary>获取将 Unicode 字符转换为 ANSI 字符时的最佳映射行为。</summary>
    /// <returns>如果启用最佳映射则为 true；否则为 false。默认值为 true。</returns>
    [__DynamicallyInvokable]
    public bool BestFitMapping
    {
      [__DynamicallyInvokable] get
      {
        return this._bestFitMapping;
      }
    }

    /// <summary>初始化 <see cref="T:System.Runtime.InteropServices.BestFitMappingAttribute" /> 类的新实例，并将其设置为 <see cref="P:System.Runtime.InteropServices.BestFitMappingAttribute.BestFitMapping" /> 属性的值。</summary>
    /// <param name="BestFitMapping">true 指示启用最佳映射；否则为 false。默认值为 true。</param>
    [__DynamicallyInvokable]
    public BestFitMappingAttribute(bool BestFitMapping)
    {
      this._bestFitMapping = BestFitMapping;
    }
  }
}
