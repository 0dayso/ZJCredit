// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.TypeIdentifierAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>提供对类型等效性的支持。</summary>
  [AttributeUsage(AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
  [ComVisible(false)]
  [__DynamicallyInvokable]
  public sealed class TypeIdentifierAttribute : Attribute
  {
    internal string Scope_;
    internal string Identifier_;

    /// <summary>获取传递给 <see cref="M:System.Runtime.InteropServices.TypeIdentifierAttribute.#ctor(System.String,System.String)" /> 构造函数的 <paramref name="scope" /> 参数的值。</summary>
    /// <returns>构造函数的 <paramref name="scope" /> 参数的值。</returns>
    [__DynamicallyInvokable]
    public string Scope
    {
      [__DynamicallyInvokable] get
      {
        return this.Scope_;
      }
    }

    /// <summary>获取传递给 <see cref="M:System.Runtime.InteropServices.TypeIdentifierAttribute.#ctor(System.String,System.String)" /> 构造函数的 <paramref name="identifier" /> 参数的值。</summary>
    /// <returns>构造函数的 <paramref name="identifier" /> 参数的值。</returns>
    [__DynamicallyInvokable]
    public string Identifier
    {
      [__DynamicallyInvokable] get
      {
        return this.Identifier_;
      }
    }

    /// <summary>创建 <see cref="T:System.Runtime.InteropServices.TypeIdentifierAttribute" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public TypeIdentifierAttribute()
    {
    }

    /// <summary>用指定的范围和标识符创建 <see cref="T:System.Runtime.InteropServices.TypeIdentifierAttribute" /> 类的新实例。</summary>
    /// <param name="scope">第一个类型等效性字符串。</param>
    /// <param name="identifier">第二个类型等效性字符串。</param>
    [__DynamicallyInvokable]
    public TypeIdentifierAttribute(string scope, string identifier)
    {
      this.Scope_ = scope;
      this.Identifier_ = identifier;
    }
  }
}
