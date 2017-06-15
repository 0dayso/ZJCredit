// Decompiled with JetBrains decompiler
// Type: System.Reflection.IntrospectionExtensions
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Reflection
{
  /// <summary>包含转换的 <see cref="T:System.Type" /> 对象的方法。</summary>
  [__DynamicallyInvokable]
  public static class IntrospectionExtensions
  {
    /// <summary>返回指定类型的 <see cref="T:System.Reflection.TypeInfo" /> 表示形式。</summary>
    /// <returns>被转换的对象。</returns>
    /// <param name="type">要转换的类型。</param>
    [__DynamicallyInvokable]
    public static TypeInfo GetTypeInfo(this Type type)
    {
      if (type == (Type) null)
        throw new ArgumentNullException("type");
      IReflectableType reflectableType = (IReflectableType) type;
      if (reflectableType == null)
        return (TypeInfo) null;
      return reflectableType.GetTypeInfo();
    }
  }
}
