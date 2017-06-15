// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.OnSerializingAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
  /// <summary>如果将对象图中的对象应用于某方法，则应指定在序列化该对象图时调用该方法。相对于图中的其他对象的序列化的顺序是非确定性的。</summary>
  [AttributeUsage(AttributeTargets.Method, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class OnSerializingAttribute : Attribute
  {
    /// <summary>初始化 <see cref="T:System.Runtime.Serialization.OnSerializingAttribute" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public OnSerializingAttribute()
    {
    }
  }
}
