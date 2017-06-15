// Decompiled with JetBrains decompiler
// Type: System.NonSerializedAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.InteropServices;

namespace System
{
  /// <summary>指示可序列化类的某个字段不应被序列化。此类不能被继承。</summary>
  /// <filterpriority>1</filterpriority>
  [AttributeUsage(AttributeTargets.Field, Inherited = false)]
  [ComVisible(true)]
  public sealed class NonSerializedAttribute : Attribute
  {
    internal static Attribute GetCustomAttribute(RuntimeFieldInfo field)
    {
      if ((field.Attributes & FieldAttributes.NotSerialized) == FieldAttributes.PrivateScope)
        return (Attribute) null;
      return (Attribute) new NonSerializedAttribute();
    }

    internal static bool IsDefined(RuntimeFieldInfo field)
    {
      return (uint) (field.Attributes & FieldAttributes.NotSerialized) > 0U;
    }
  }
}
