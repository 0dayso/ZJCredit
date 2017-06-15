// Decompiled with JetBrains decompiler
// Type: System.ParamArrayAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>指示方法在调用中将允许参数的数目可变。此类不能被继承。</summary>
  /// <filterpriority>1</filterpriority>
  [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class ParamArrayAttribute : Attribute
  {
    /// <summary>使用默认属性初始化 <see cref="T:System.ParamArrayAttribute" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public ParamArrayAttribute()
    {
    }
  }
}
