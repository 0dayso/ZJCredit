// Decompiled with JetBrains decompiler
// Type: System.Reflection.IReflectableType
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Reflection
{
  /// <summary>表示可在其上发射的类型。</summary>
  [__DynamicallyInvokable]
  public interface IReflectableType
  {
    /// <summary>检索表示此类型的对象。</summary>
    /// <returns>一个表示此类型的对象。</returns>
    [__DynamicallyInvokable]
    TypeInfo GetTypeInfo();
  }
}
