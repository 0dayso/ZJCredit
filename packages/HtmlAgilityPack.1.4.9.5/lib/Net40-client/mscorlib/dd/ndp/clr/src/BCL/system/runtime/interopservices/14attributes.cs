// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.IDispatchImplAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>指示当向 COM 公开双重接口和调度接口时公共语言运行时使用何种 IDispatch 实现。</summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, Inherited = false)]
  [Obsolete("This attribute is deprecated and will be removed in a future version.", false)]
  [ComVisible(true)]
  public sealed class IDispatchImplAttribute : Attribute
  {
    internal IDispatchImplType _val;

    /// <summary>获取由类使用的 <see cref="T:System.Runtime.InteropServices.IDispatchImplType" /> 值。</summary>
    /// <returns>由类使用的 <see cref="T:System.Runtime.InteropServices.IDispatchImplType" /> 值。</returns>
    public IDispatchImplType Value
    {
      get
      {
        return this._val;
      }
    }

    /// <summary>使用指定的 <see cref="T:System.Runtime.InteropServices.IDispatchImplType" /> 值初始化 IDispatchImplAttribute 类的新实例。</summary>
    /// <param name="implType">指示将使用的 <see cref="T:System.Runtime.InteropServices.IDispatchImplType" /> 枚举。</param>
    public IDispatchImplAttribute(IDispatchImplType implType)
    {
      this._val = implType;
    }

    /// <summary>使用指定的 <see cref="T:System.Runtime.InteropServices.IDispatchImplType" /> 值初始化 IDispatchImplAttribute 类的新实例。</summary>
    /// <param name="implType">指示将使用的 <see cref="T:System.Runtime.InteropServices.IDispatchImplType" /> 枚举。</param>
    public IDispatchImplAttribute(short implType)
    {
      this._val = (IDispatchImplType) implType;
    }
  }
}
