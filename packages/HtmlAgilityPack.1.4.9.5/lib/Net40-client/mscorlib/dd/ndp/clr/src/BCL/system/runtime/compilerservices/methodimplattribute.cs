// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.MethodImplAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
  /// <summary>指定如何实现某方法的详细信息。此类不能被继承。</summary>
  [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class MethodImplAttribute : Attribute
  {
    internal MethodImplOptions _val;
    /// <summary>一个 <see cref="T:System.Runtime.CompilerServices.MethodCodeType" /> 值，指示为此方法提供了哪种类型的实现。</summary>
    public MethodCodeType MethodCodeType;

    /// <summary>获取描述属性化方法的 <see cref="T:System.Runtime.CompilerServices.MethodImplOptions" /> 值。</summary>
    /// <returns>描述特性化方法的 <see cref="T:System.Runtime.CompilerServices.MethodImplOptions" /> 值。</returns>
    [__DynamicallyInvokable]
    public MethodImplOptions Value
    {
      [__DynamicallyInvokable] get
      {
        return this._val;
      }
    }

    internal MethodImplAttribute(MethodImplAttributes methodImplAttributes)
    {
      MethodImplOptions methodImplOptions = MethodImplOptions.Unmanaged | MethodImplOptions.ForwardRef | MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall | MethodImplOptions.Synchronized | MethodImplOptions.NoInlining | MethodImplOptions.AggressiveInlining | MethodImplOptions.NoOptimization;
      this._val = (MethodImplOptions) (methodImplAttributes & (MethodImplAttributes) methodImplOptions);
    }

    /// <summary>使用指定的 <see cref="T:System.Runtime.CompilerServices.MethodImplOptions" /> 值初始化 <see cref="T:System.Runtime.CompilerServices.MethodImplAttribute" /> 类的新实例。</summary>
    /// <param name="methodImplOptions">一个 <see cref="T:System.Runtime.CompilerServices.MethodImplOptions" /> 值，该值指定属性化方法的属性。</param>
    [__DynamicallyInvokable]
    public MethodImplAttribute(MethodImplOptions methodImplOptions)
    {
      this._val = methodImplOptions;
    }

    /// <summary>使用指定的 <see cref="T:System.Runtime.CompilerServices.MethodImplOptions" /> 值初始化 <see cref="T:System.Runtime.CompilerServices.MethodImplAttribute" /> 类的新实例。</summary>
    /// <param name="value">一个位屏蔽，表示所需的 <see cref="T:System.Runtime.CompilerServices.MethodImplOptions" /> 值，该值指定属性化方法的属性。</param>
    public MethodImplAttribute(short value)
    {
      this._val = (MethodImplOptions) value;
    }

    /// <summary>初始化 <see cref="T:System.Runtime.CompilerServices.MethodImplAttribute" /> 类的新实例。</summary>
    public MethodImplAttribute()
    {
    }
  }
}
