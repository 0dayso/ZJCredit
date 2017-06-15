// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.OnDeserializingAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
  /// <summary>当将对象图应用某方法时，指定反序列化对象时调用的方法。相对于图中的其他对象的反序列化的顺序是非确定性的。</summary>
  [AttributeUsage(AttributeTargets.Method, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class OnDeserializingAttribute : Attribute
  {
    /// <summary>初始化 <see cref="T:System.Runtime.Serialization.OnDeserializingAttribute" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public OnDeserializingAttribute()
    {
    }
  }
}
