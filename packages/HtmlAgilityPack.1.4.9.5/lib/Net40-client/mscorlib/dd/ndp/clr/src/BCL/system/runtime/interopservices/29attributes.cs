// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.StructLayoutAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Security;

namespace System.Runtime.InteropServices
{
  /// <summary>允许你控制内存中类或结构的数据字段的物理布局。</summary>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class StructLayoutAttribute : Attribute
  {
    private const int DEFAULT_PACKING_SIZE = 8;
    internal LayoutKind _val;
    /// <summary>控制类或结构的数据字段在内存中的对齐方式。</summary>
    [__DynamicallyInvokable]
    public int Pack;
    /// <summary>指示类或结构的绝对大小。</summary>
    [__DynamicallyInvokable]
    public int Size;
    /// <summary>指示在默认情况下是否应将类中的字符串数据字段作为 LPWSTR 或 LPSTR 进行封送处理。</summary>
    [__DynamicallyInvokable]
    public CharSet CharSet;

    /// <summary>获取 <see cref="T:System.Runtime.InteropServices.LayoutKind" /> 值，该值指定如何排列类或结构。</summary>
    /// <returns>枚举值之一，指定如何排列类或结构。</returns>
    [__DynamicallyInvokable]
    public LayoutKind Value
    {
      [__DynamicallyInvokable] get
      {
        return this._val;
      }
    }

    internal StructLayoutAttribute(LayoutKind layoutKind, int pack, int size, CharSet charSet)
    {
      this._val = layoutKind;
      this.Pack = pack;
      this.Size = size;
      this.CharSet = charSet;
    }

    /// <summary>用指定的 <see cref="T:System.Runtime.InteropServices.LayoutKind" /> 枚举成员初始化 <see cref="T:System.Runtime.InteropServices.StructLayoutAttribute" /> 类的新实例。</summary>
    /// <param name="layoutKind">枚举值之一，指定应该如何排列类或结构。</param>
    [__DynamicallyInvokable]
    public StructLayoutAttribute(LayoutKind layoutKind)
    {
      this._val = layoutKind;
    }

    /// <summary>用指定的 <see cref="T:System.Runtime.InteropServices.LayoutKind" /> 枚举成员初始化 <see cref="T:System.Runtime.InteropServices.StructLayoutAttribute" /> 类的新实例。</summary>
    /// <param name="layoutKind">表示 <see cref="T:System.Runtime.InteropServices.LayoutKind" /> 值之一的 16 位整数，指定应该如何排列类或结构。</param>
    public StructLayoutAttribute(short layoutKind)
    {
      this._val = (LayoutKind) layoutKind;
    }

    [SecurityCritical]
    internal static Attribute GetCustomAttribute(RuntimeType type)
    {
      if (!StructLayoutAttribute.IsDefined(type))
        return (Attribute) null;
      int packSize = 0;
      int classSize = 0;
      LayoutKind layoutKind = LayoutKind.Auto;
      switch (type.Attributes & TypeAttributes.LayoutMask)
      {
        case TypeAttributes.NotPublic:
          layoutKind = LayoutKind.Auto;
          break;
        case TypeAttributes.SequentialLayout:
          layoutKind = LayoutKind.Sequential;
          break;
        case TypeAttributes.ExplicitLayout:
          layoutKind = LayoutKind.Explicit;
          break;
      }
      CharSet charSet = CharSet.None;
      switch (type.Attributes & TypeAttributes.StringFormatMask)
      {
        case TypeAttributes.NotPublic:
          charSet = CharSet.Ansi;
          break;
        case TypeAttributes.UnicodeClass:
          charSet = CharSet.Unicode;
          break;
        case TypeAttributes.AutoClass:
          charSet = CharSet.Auto;
          break;
      }
      type.GetRuntimeModule().MetadataImport.GetClassLayout(type.MetadataToken, out packSize, out classSize);
      if (packSize == 0)
        packSize = 8;
      return (Attribute) new StructLayoutAttribute(layoutKind, packSize, classSize, charSet);
    }

    internal static bool IsDefined(RuntimeType type)
    {
      return !type.IsInterface && !type.HasElementType && !type.IsGenericParameter;
    }
  }
}
