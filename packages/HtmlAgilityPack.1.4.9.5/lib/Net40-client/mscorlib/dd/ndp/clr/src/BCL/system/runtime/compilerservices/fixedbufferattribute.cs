// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.FixedBufferAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.CompilerServices
{
  /// <summary>指示字段应被视为包含指定基元类型的固定数目的元素。此类不能被继承。</summary>
  [AttributeUsage(AttributeTargets.Field, Inherited = false)]
  [__DynamicallyInvokable]
  public sealed class FixedBufferAttribute : Attribute
  {
    private Type elementType;
    private int length;

    /// <summary>获取固定缓冲区中包含的元素的类型。</summary>
    /// <returns>元素的类型。</returns>
    [__DynamicallyInvokable]
    public Type ElementType
    {
      [__DynamicallyInvokable] get
      {
        return this.elementType;
      }
    }

    /// <summary>获取缓冲区中元素的数目。</summary>
    /// <returns>固定缓冲区中元素的数目。</returns>
    [__DynamicallyInvokable]
    public int Length
    {
      [__DynamicallyInvokable] get
      {
        return this.length;
      }
    }

    /// <summary>初始化 <see cref="T:System.Runtime.CompilerServices.FixedBufferAttribute" /> 类的新实例。</summary>
    /// <param name="elementType">缓冲区中包含的元素的类型。</param>
    /// <param name="length">缓冲区中元素的数目。</param>
    [__DynamicallyInvokable]
    public FixedBufferAttribute(Type elementType, int length)
    {
      this.elementType = elementType;
      this.length = length;
    }
  }
}
