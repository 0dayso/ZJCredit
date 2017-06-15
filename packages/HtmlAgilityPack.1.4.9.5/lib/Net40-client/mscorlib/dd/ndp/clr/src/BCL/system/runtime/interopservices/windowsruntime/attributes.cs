// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.DefaultInterfaceAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.WindowsRuntime
{
  /// <summary>指定托管 Windows 运行时 类的默认接口。</summary>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
  [__DynamicallyInvokable]
  public sealed class DefaultInterfaceAttribute : Attribute
  {
    private Type m_defaultInterface;

    /// <summary>获取默认接口的类型。</summary>
    /// <returns>默认接口的类型。</returns>
    [__DynamicallyInvokable]
    public Type DefaultInterface
    {
      [__DynamicallyInvokable] get
      {
        return this.m_defaultInterface;
      }
    }

    /// <summary>初始化 <see cref="T:System.Runtime.InteropServices.WindowsRuntime.DefaultInterfaceAttribute" /> 类的新实例。</summary>
    /// <param name="defaultInterface">用于应用特性的类的指定的默认接口的接口类型。</param>
    [__DynamicallyInvokable]
    public DefaultInterfaceAttribute(Type defaultInterface)
    {
      this.m_defaultInterface = defaultInterface;
    }
  }
}
