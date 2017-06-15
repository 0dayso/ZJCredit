// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.TypeForwardedFromAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.CompilerServices
{
  /// <summary>指定另一个程序集中的源 <see cref="T:System.Type" />。</summary>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
  [__DynamicallyInvokable]
  public sealed class TypeForwardedFromAttribute : Attribute
  {
    private string assemblyFullName;

    /// <summary>获取源类型的程序集限定名称。</summary>
    /// <returns>源类型的程序集限定名称。</returns>
    [__DynamicallyInvokable]
    public string AssemblyFullName
    {
      [__DynamicallyInvokable] get
      {
        return this.assemblyFullName;
      }
    }

    private TypeForwardedFromAttribute()
    {
    }

    /// <summary>初始化 <see cref="T:System.Runtime.CompilerServices.TypeForwardedFromAttribute" /> 类的新实例。</summary>
    /// <param name="assemblyFullName">另一个程序集中的源 <see cref="T:System.Type" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assemblyFullName" /> 为 null 或为空。</exception>
    [__DynamicallyInvokable]
    public TypeForwardedFromAttribute(string assemblyFullName)
    {
      if (string.IsNullOrEmpty(assemblyFullName))
        throw new ArgumentNullException("assemblyFullName");
      this.assemblyFullName = assemblyFullName;
    }
  }
}
