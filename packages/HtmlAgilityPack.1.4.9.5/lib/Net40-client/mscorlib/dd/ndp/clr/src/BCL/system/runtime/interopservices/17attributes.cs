// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.TypeLibTypeAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>包含最初为此类型从 COM 类型库导入的 <see cref="T:System.Runtime.InteropServices.TYPEFLAGS" />。</summary>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface, Inherited = false)]
  [ComVisible(true)]
  public sealed class TypeLibTypeAttribute : Attribute
  {
    internal TypeLibTypeFlags _val;

    /// <summary>获取此类型的 <see cref="T:System.Runtime.InteropServices.TypeLibTypeFlags" /> 值。</summary>
    /// <returns>此类型的 <see cref="T:System.Runtime.InteropServices.TypeLibTypeFlags" /> 值。</returns>
    public TypeLibTypeFlags Value
    {
      get
      {
        return this._val;
      }
    }

    /// <summary>使用指定的 <see cref="T:System.Runtime.InteropServices.TypeLibTypeFlags" /> 值初始化 TypeLibTypeAttribute 类的新实例。</summary>
    /// <param name="flags">特性化类型的 <see cref="T:System.Runtime.InteropServices.TypeLibTypeFlags" /> 值，是在此值从中导入的类型库中找到的。</param>
    public TypeLibTypeAttribute(TypeLibTypeFlags flags)
    {
      this._val = flags;
    }

    /// <summary>使用指定的 <see cref="T:System.Runtime.InteropServices.TypeLibTypeFlags" /> 值初始化 TypeLibTypeAttribute 类的新实例。</summary>
    /// <param name="flags">特性化类型的 <see cref="T:System.Runtime.InteropServices.TypeLibTypeFlags" /> 值，是在此值从中导入的类型库中找到的。</param>
    public TypeLibTypeAttribute(short flags)
    {
      this._val = (TypeLibTypeFlags) flags;
    }
  }
}
