// Decompiled with JetBrains decompiler
// Type: System.SerializableAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.InteropServices;

namespace System
{
  /// <summary>指示一个类可以序列化。此类不能被继承。</summary>
  /// <filterpriority>1</filterpriority>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Delegate, Inherited = false)]
  [ComVisible(true)]
  public sealed class SerializableAttribute : Attribute
  {
    internal static Attribute GetCustomAttribute(RuntimeType type)
    {
      if ((type.Attributes & TypeAttributes.Serializable) != TypeAttributes.Serializable)
        return (Attribute) null;
      return (Attribute) new SerializableAttribute();
    }

    internal static bool IsDefined(RuntimeType type)
    {
      return type.IsSerializable;
    }
  }
}
