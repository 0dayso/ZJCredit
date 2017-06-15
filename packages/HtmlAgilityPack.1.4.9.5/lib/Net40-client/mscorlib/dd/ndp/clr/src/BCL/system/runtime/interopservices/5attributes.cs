// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComDefaultInterfaceAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>指定要向 COM 公开的默认接口。此类不能被继承。</summary>
  [AttributeUsage(AttributeTargets.Class, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class ComDefaultInterfaceAttribute : Attribute
  {
    internal Type _val;

    /// <summary>获取 <see cref="T:System.Type" /> 对象，该对象指定要向 COM 公开的默认接口。</summary>
    /// <returns>
    /// <see cref="T:System.Type" /> 对象，该对象指定要向 COM 公开的默认接口。</returns>
    [__DynamicallyInvokable]
    public Type Value
    {
      [__DynamicallyInvokable] get
      {
        return this._val;
      }
    }

    /// <summary>以指定的 <see cref="T:System.Type" /> 对象作为向 COM 公开的默认接口初始化 <see cref="T:System.Runtime.InteropServices.ComDefaultInterfaceAttribute" /> 类的新实例。</summary>
    /// <param name="defaultInterface">一个 <see cref="T:System.Type" /> 值，指示要向 COM 公开的默认接口。</param>
    [__DynamicallyInvokable]
    public ComDefaultInterfaceAttribute(Type defaultInterface)
    {
      this._val = defaultInterface;
    }
  }
}
