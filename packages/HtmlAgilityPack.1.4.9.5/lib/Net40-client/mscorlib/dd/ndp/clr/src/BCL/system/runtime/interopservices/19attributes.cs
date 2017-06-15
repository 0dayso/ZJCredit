// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.TypeLibVarAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>包含最初为此字段从 COM 类型库导入的 <see cref="T:System.Runtime.InteropServices.VARFLAGS" />。</summary>
  [AttributeUsage(AttributeTargets.Field, Inherited = false)]
  [ComVisible(true)]
  public sealed class TypeLibVarAttribute : Attribute
  {
    internal TypeLibVarFlags _val;

    /// <summary>获取此字段的 <see cref="T:System.Runtime.InteropServices.TypeLibVarFlags" /> 值。</summary>
    /// <returns>此字段的 <see cref="T:System.Runtime.InteropServices.TypeLibVarFlags" /> 值。</returns>
    public TypeLibVarFlags Value
    {
      get
      {
        return this._val;
      }
    }

    /// <summary>使用指定的 <see cref="T:System.Runtime.InteropServices.TypeLibVarFlags" /> 值初始化 <see cref="T:System.Runtime.InteropServices.TypeLibVarAttribute" /> 类的新实例。</summary>
    /// <param name="flags">特性化字段的 <see cref="T:System.Runtime.InteropServices.TypeLibVarFlags" /> 值，是在此值从中导入的类型库中找到的。</param>
    public TypeLibVarAttribute(TypeLibVarFlags flags)
    {
      this._val = flags;
    }

    /// <summary>使用指定的 <see cref="T:System.Runtime.InteropServices.TypeLibVarFlags" /> 值初始化 <see cref="T:System.Runtime.InteropServices.TypeLibVarAttribute" /> 类的新实例。</summary>
    /// <param name="flags">特性化字段的 <see cref="T:System.Runtime.InteropServices.TypeLibVarFlags" /> 值，是在此值从中导入的类型库中找到的。</param>
    public TypeLibVarAttribute(short flags)
    {
      this._val = (TypeLibVarFlags) flags;
    }
  }
}
