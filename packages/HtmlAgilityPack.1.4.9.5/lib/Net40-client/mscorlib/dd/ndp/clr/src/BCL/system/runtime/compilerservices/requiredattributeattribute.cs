// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.RequiredAttributeAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
  /// <summary>指定导入编译器必须完全理解类型定义的语义，或拒绝使用它。此类不能被继承。</summary>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
  [ComVisible(true)]
  [Serializable]
  public sealed class RequiredAttributeAttribute : Attribute
  {
    private Type requiredContract;

    /// <summary>获取导入编译器必须完全理解的类型。</summary>
    /// <returns>导入编译器必须完全理解的类型。</returns>
    public Type RequiredContract
    {
      get
      {
        return this.requiredContract;
      }
    }

    /// <summary>初始化 <see cref="T:System.Runtime.CompilerServices.RequiredAttributeAttribute" /> 类的新实例。</summary>
    /// <param name="requiredContract">导入编译器必须完全理解的类型。.NET Framework 版本 2.0 及更高版本不支持此参数。</param>
    public RequiredAttributeAttribute(Type requiredContract)
    {
      this.requiredContract = requiredContract;
    }
  }
}
