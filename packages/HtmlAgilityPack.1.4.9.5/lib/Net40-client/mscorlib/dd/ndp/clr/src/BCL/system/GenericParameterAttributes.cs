// Decompiled with JetBrains decompiler
// Type: System.Reflection.GenericParameterAttributes
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Reflection
{
  /// <summary>描述对泛型类型或泛型方法的泛型类型参数的约束。</summary>
  [Flags]
  [__DynamicallyInvokable]
  public enum GenericParameterAttributes
  {
    [__DynamicallyInvokable] None = 0,
    [__DynamicallyInvokable] VarianceMask = 3,
    [__DynamicallyInvokable] Covariant = 1,
    [__DynamicallyInvokable] Contravariant = 2,
    [__DynamicallyInvokable] SpecialConstraintMask = 28,
    [__DynamicallyInvokable] ReferenceTypeConstraint = 4,
    [__DynamicallyInvokable] NotNullableValueTypeConstraint = 8,
    [__DynamicallyInvokable] DefaultConstructorConstraint = 16,
  }
}
