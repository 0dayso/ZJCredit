// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComVisibleAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>控制程序集中个别托管类型、成员或所有类型对 COM 的可访问性。</summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class ComVisibleAttribute : Attribute
  {
    internal bool _val;

    /// <summary>获取一个值，该值指示 COM 类型是否可见。</summary>
    /// <returns>如果该类型可见，则为 true；否则为 false。默认值为 true。</returns>
    [__DynamicallyInvokable]
    public bool Value
    {
      [__DynamicallyInvokable] get
      {
        return this._val;
      }
    }

    /// <summary>初始化 ComVisibleAttribute 类的新实例。</summary>
    /// <param name="visibility">true 指示该类型对 COM 可见；否则为 false。默认值为 true。</param>
    [__DynamicallyInvokable]
    public ComVisibleAttribute(bool visibility)
    {
      this._val = visibility;
    }
  }
}
