// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.TypeLibFuncAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>包含最初从 COM 类型库为此方法导入的 <see cref="T:System.Runtime.InteropServices.FUNCFLAGS" />。</summary>
  [AttributeUsage(AttributeTargets.Method, Inherited = false)]
  [ComVisible(true)]
  public sealed class TypeLibFuncAttribute : Attribute
  {
    internal TypeLibFuncFlags _val;

    /// <summary>获取该方法的 <see cref="T:System.Runtime.InteropServices.TypeLibFuncFlags" /> 值。</summary>
    /// <returns>该方法的 <see cref="T:System.Runtime.InteropServices.TypeLibFuncFlags" /> 值。</returns>
    public TypeLibFuncFlags Value
    {
      get
      {
        return this._val;
      }
    }

    /// <summary>使用指定的 <see cref="T:System.Runtime.InteropServices.TypeLibFuncFlags" /> 值初始化 TypeLibFuncAttribute 类的新实例。</summary>
    /// <param name="flags">该特性化方法的 <see cref="T:System.Runtime.InteropServices.TypeLibFuncFlags" /> 值，是在此值从中导入的类型库中找到的。</param>
    public TypeLibFuncAttribute(TypeLibFuncFlags flags)
    {
      this._val = flags;
    }

    /// <summary>使用指定的 <see cref="T:System.Runtime.InteropServices.TypeLibFuncFlags" /> 值初始化 TypeLibFuncAttribute 类的新实例。</summary>
    /// <param name="flags">该特性化方法的 <see cref="T:System.Runtime.InteropServices.TypeLibFuncFlags" /> 值，是在此值从中导入的类型库中找到的。</param>
    public TypeLibFuncAttribute(short flags)
    {
      this._val = (TypeLibFuncFlags) flags;
    }
  }
}
