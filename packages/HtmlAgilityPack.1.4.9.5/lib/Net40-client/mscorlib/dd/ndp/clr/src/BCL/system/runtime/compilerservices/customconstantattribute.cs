// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.CustomConstantAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
  /// <summary>定义一个编译器可以为字段或方法参数永久保存的常数值。</summary>
  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public abstract class CustomConstantAttribute : Attribute
  {
    /// <summary>获取该属性存储的常数值。</summary>
    /// <returns>该属性存储的常数值。</returns>
    [__DynamicallyInvokable]
    public abstract object Value { [__DynamicallyInvokable] get; }

    /// <summary>初始化 <see cref="T:System.Runtime.CompilerServices.CustomConstantAttribute" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    protected CustomConstantAttribute()
    {
    }

    internal static object GetRawConstant(CustomAttributeData attr)
    {
      foreach (CustomAttributeNamedArgument namedArgument in (IEnumerable<CustomAttributeNamedArgument>) attr.NamedArguments)
      {
        if (namedArgument.MemberInfo.Name.Equals("Value"))
          return namedArgument.TypedValue.Value;
      }
      return (object) DBNull.Value;
    }
  }
}
