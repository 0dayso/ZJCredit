// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.FieldOffsetAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Security;

namespace System.Runtime.InteropServices
{
  /// <summary>指示字段在类或结构的非托管表示形式内的物理位置。</summary>
  [AttributeUsage(AttributeTargets.Field, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class FieldOffsetAttribute : Attribute
  {
    internal int _val;

    /// <summary>获取从结构开始到字段开始的偏移量。</summary>
    /// <returns>从结构开始到字段开始的偏移量。</returns>
    [__DynamicallyInvokable]
    public int Value
    {
      [__DynamicallyInvokable] get
      {
        return this._val;
      }
    }

    /// <summary>使用结构内到字段开始的偏移量初始化 <see cref="T:System.Runtime.InteropServices.FieldOffsetAttribute" /> 类的新实例。</summary>
    /// <param name="offset">从结构开始处到字段开始处的偏移量（以字节为单位）。</param>
    [__DynamicallyInvokable]
    public FieldOffsetAttribute(int offset)
    {
      this._val = offset;
    }

    [SecurityCritical]
    internal static Attribute GetCustomAttribute(RuntimeFieldInfo field)
    {
      int offset;
      if (field.DeclaringType != (Type) null && field.GetRuntimeModule().MetadataImport.GetFieldOffset(field.DeclaringType.MetadataToken, field.MetadataToken, out offset))
        return (Attribute) new FieldOffsetAttribute(offset);
      return (Attribute) null;
    }

    [SecurityCritical]
    internal static bool IsDefined(RuntimeFieldInfo field)
    {
      return FieldOffsetAttribute.GetCustomAttribute(field) != null;
    }
  }
}
