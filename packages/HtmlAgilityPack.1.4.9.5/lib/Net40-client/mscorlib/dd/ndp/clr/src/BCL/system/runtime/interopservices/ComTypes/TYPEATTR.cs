// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.TYPEATTR
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>包含 UCOMITypeInfo 的特性。</summary>
  [__DynamicallyInvokable]
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public struct TYPEATTR
  {
    /// <summary>与 <see cref="F:System.Runtime.InteropServices.TYPEATTR.memidConstructor" /> 和 <see cref="F:System.Runtime.InteropServices.TYPEATTR.memidDestructor" /> 字段一起使用的常数。</summary>
    [__DynamicallyInvokable]
    public const int MEMBER_ID_NIL = -1;
    /// <summary>类型信息的 GUID。</summary>
    [__DynamicallyInvokable]
    public Guid guid;
    /// <summary>成员名称和文档字符串的区域设置。</summary>
    [__DynamicallyInvokable]
    public int lcid;
    /// <summary>保留供将来使用。</summary>
    [__DynamicallyInvokable]
    public int dwReserved;
    /// <summary>构造函数的 ID，如果没有，则为 <see cref="F:System.Runtime.InteropServices.TYPEATTR.MEMBER_ID_NIL" />。</summary>
    [__DynamicallyInvokable]
    public int memidConstructor;
    /// <summary>析构函数的 ID，如果没有，则为 <see cref="F:System.Runtime.InteropServices.TYPEATTR.MEMBER_ID_NIL" />。</summary>
    [__DynamicallyInvokable]
    public int memidDestructor;
    /// <summary>保留供将来使用。</summary>
    public IntPtr lpstrSchema;
    /// <summary>此类型的实例的大小。</summary>
    [__DynamicallyInvokable]
    public int cbSizeInstance;
    /// <summary>
    /// <see cref="T:System.Runtime.InteropServices.TYPEKIND" /> 值，该值描述此信息描述的类型。</summary>
    [__DynamicallyInvokable]
    public TYPEKIND typekind;
    /// <summary>指示此结构描述的接口上的函数数目。</summary>
    [__DynamicallyInvokable]
    public short cFuncs;
    /// <summary>指示此结构所描述的接口上的变量和数据字段的数目。</summary>
    [__DynamicallyInvokable]
    public short cVars;
    /// <summary>指示在此结构描述的接口上实现的接口数目。</summary>
    [__DynamicallyInvokable]
    public short cImplTypes;
    /// <summary>此类型的虚方法表 (VTBL) 的大小。</summary>
    [__DynamicallyInvokable]
    public short cbSizeVft;
    /// <summary>指定此类型实例的字节对齐方式。</summary>
    [__DynamicallyInvokable]
    public short cbAlignment;
    /// <summary>描述此信息的 <see cref="T:System.Runtime.InteropServices.TYPEFLAGS" /> 值。</summary>
    [__DynamicallyInvokable]
    public TYPEFLAGS wTypeFlags;
    /// <summary>主要版本号。</summary>
    [__DynamicallyInvokable]
    public short wMajorVerNum;
    /// <summary>次要版本号。</summary>
    [__DynamicallyInvokable]
    public short wMinorVerNum;
    /// <summary>如果 <see cref="F:System.Runtime.InteropServices.TYPEATTR.typekind" />==<see cref="F:System.Runtime.InteropServices.TYPEKIND.TKIND_ALIAS" />，则指定该类型（此类型为该类型的别名）。</summary>
    [__DynamicallyInvokable]
    public TYPEDESC tdescAlias;
    /// <summary>所描述的类型的 IDL 特性。</summary>
    [__DynamicallyInvokable]
    public IDLDESC idldescType;
  }
}
