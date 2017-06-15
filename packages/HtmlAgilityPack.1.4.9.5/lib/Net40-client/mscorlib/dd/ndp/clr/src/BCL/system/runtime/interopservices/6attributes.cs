// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ClassInterfaceAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>为公开给 COM 的类指定要生成的类接口的类型（如果有接口生成）。</summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class ClassInterfaceAttribute : Attribute
  {
    internal ClassInterfaceType _val;

    /// <summary>获取 <see cref="T:System.Runtime.InteropServices.ClassInterfaceType" /> 值，该值描述应该为该类生成哪种类型的接口。</summary>
    /// <returns>描述应该为该类生成哪种类型的接口的 <see cref="T:System.Runtime.InteropServices.ClassInterfaceType" /> 值。</returns>
    [__DynamicallyInvokable]
    public ClassInterfaceType Value
    {
      [__DynamicallyInvokable] get
      {
        return this._val;
      }
    }

    /// <summary>使用指定的 <see cref="T:System.Runtime.InteropServices.ClassInterfaceType" /> 枚举成员初始化 <see cref="T:System.Runtime.InteropServices.ClassInterfaceAttribute" /> 类的新实例。</summary>
    /// <param name="classInterfaceType">
    /// <see cref="T:System.Runtime.InteropServices.ClassInterfaceType" /> 值之一，描述为类生成的接口的类型。</param>
    [__DynamicallyInvokable]
    public ClassInterfaceAttribute(ClassInterfaceType classInterfaceType)
    {
      this._val = classInterfaceType;
    }

    /// <summary>用指定的 <see cref="T:System.Runtime.InteropServices.ClassInterfaceType" /> 枚举值初始化 <see cref="T:System.Runtime.InteropServices.ClassInterfaceAttribute" /> 类的新实例。</summary>
    /// <param name="classInterfaceType">描述为类生成的接口的类型。</param>
    [__DynamicallyInvokable]
    public ClassInterfaceAttribute(short classInterfaceType)
    {
      this._val = (ClassInterfaceType) classInterfaceType;
    }
  }
}
