// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.UnsafeValueTypeAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.CompilerServices
{
  /// <summary>指定某个类型包含可能溢出的非托管数组。此类不能被继承。</summary>
  [AttributeUsage(AttributeTargets.Struct)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class UnsafeValueTypeAttribute : Attribute
  {
    /// <summary>初始化 <see cref="T:System.Runtime.CompilerServices.UnsafeValueTypeAttribute" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public UnsafeValueTypeAttribute()
    {
    }
  }
}
