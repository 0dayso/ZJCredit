// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.InterfaceTypeAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>指示向 COM 公开时，托管接口是双重的、仅支持调度的、还是仅支持 IUnknown 的。</summary>
  [AttributeUsage(AttributeTargets.Interface, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class InterfaceTypeAttribute : Attribute
  {
    internal ComInterfaceType _val;

    /// <summary>获取 <see cref="T:System.Runtime.InteropServices.ComInterfaceType" /> 值，该值描述应如何向 COM 公开接口。</summary>
    /// <returns>
    /// <see cref="T:System.Runtime.InteropServices.ComInterfaceType" /> 值，该值描述应如何向 COM 公开接口。</returns>
    [__DynamicallyInvokable]
    public ComInterfaceType Value
    {
      [__DynamicallyInvokable] get
      {
        return this._val;
      }
    }

    /// <summary>使用指定的 <see cref="T:System.Runtime.InteropServices.ComInterfaceType" /> 枚举成员初始化 <see cref="T:System.Runtime.InteropServices.InterfaceTypeAttribute" /> 类的新实例。</summary>
    /// <param name="interfaceType">
    /// <see cref="T:System.Runtime.InteropServices.ComInterfaceType" /> 值之一，指定如何向 COM 客户端公开接口。</param>
    [__DynamicallyInvokable]
    public InterfaceTypeAttribute(ComInterfaceType interfaceType)
    {
      this._val = interfaceType;
    }

    /// <summary>使用指定的 <see cref="T:System.Runtime.InteropServices.ComInterfaceType" /> 枚举成员初始化 <see cref="T:System.Runtime.InteropServices.InterfaceTypeAttribute" /> 类的新实例。</summary>
    /// <param name="interfaceType">描述应如何向 COM 客户端公开接口。</param>
    [__DynamicallyInvokable]
    public InterfaceTypeAttribute(short interfaceType)
    {
      this._val = (ComInterfaceType) interfaceType;
    }
  }
}
