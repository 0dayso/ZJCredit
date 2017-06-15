// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.ReturnValueNameAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.WindowsRuntime
{
  /// <summary>指定 Windows 运行时 元素指定方法的返回值的名称。</summary>
  [AttributeUsage(AttributeTargets.Delegate | AttributeTargets.ReturnValue, AllowMultiple = false, Inherited = false)]
  [__DynamicallyInvokable]
  public sealed class ReturnValueNameAttribute : Attribute
  {
    private string m_Name;

    /// <summary>获取 Windows 运行时 组件中为方法返回值指定的名称。</summary>
    /// <returns>方法的返回值的名称。</returns>
    [__DynamicallyInvokable]
    public string Name
    {
      [__DynamicallyInvokable] get
      {
        return this.m_Name;
      }
    }

    /// <summary>初始化 <see cref="T:System.Runtime.InteropServices.WindowsRuntime.ReturnValueNameAttribute" /> 类的新实例，并指定返回值的名称。</summary>
    /// <param name="name">返回值的名称。</param>
    [__DynamicallyInvokable]
    public ReturnValueNameAttribute(string name)
    {
      this.m_Name = name;
    }
  }
}
