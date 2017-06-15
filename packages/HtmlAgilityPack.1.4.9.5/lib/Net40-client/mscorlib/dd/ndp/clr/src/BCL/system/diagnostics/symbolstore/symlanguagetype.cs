// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.SymbolStore.SymLanguageType
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
  /// <summary>保存要与符号存储区一起使用的语言类型的公用 GUID。</summary>
  [ComVisible(true)]
  public class SymLanguageType
  {
    /// <summary>指定要与符号存储区一起使用的 C 语言类型的 GUID。</summary>
    public static readonly Guid C = new Guid(1671464724, (short) -969, (short) 4562, (byte) 144, (byte) 76, (byte) 0, (byte) 192, (byte) 79, (byte) 163, (byte) 2, (byte) 161);
    /// <summary>指定要与符号存储区一起使用的 C++ 语言类型的 GUID。</summary>
    public static readonly Guid CPlusPlus = new Guid(974311607, (short) -15764, (short) 4560, (byte) 180, (byte) 66, (byte) 0, (byte) 160, (byte) 36, (byte) 74, (byte) 29, (byte) 210);
    /// <summary>指定要与符号存储区一起使用的 C# 语言类型的 GUID。</summary>
    public static readonly Guid CSharp = new Guid(1062298360, (short) 1990, (short) 4563, (byte) 144, (byte) 83, (byte) 0, (byte) 192, (byte) 79, (byte) 163, (byte) 2, (byte) 161);
    /// <summary>指定要与符号存储区一起使用的 Basic 语言类型的 GUID。</summary>
    public static readonly Guid Basic = new Guid(974311608, (short) -15764, (short) 4560, (byte) 180, (byte) 66, (byte) 0, (byte) 160, (byte) 36, (byte) 74, (byte) 29, (byte) 210);
    /// <summary>指定要与符号存储区一起使用的 Java 语言类型的 GUID。</summary>
    public static readonly Guid Java = new Guid(974311604, (short) -15764, (short) 4560, (byte) 180, (byte) 66, (byte) 0, (byte) 160, (byte) 36, (byte) 74, (byte) 29, (byte) 210);
    /// <summary>指定要与符号存储区一起使用的 Cobol 语言类型的 GUID。</summary>
    public static readonly Guid Cobol = new Guid(-1358664495, (short) -12063, (short) 4562, (byte) 151, (byte) 124, (byte) 0, (byte) 160, (byte) 201, (byte) 180, (byte) 213, (byte) 12);
    /// <summary>指定要与符号存储区一起使用的 Pascal 语言类型的 GUID。</summary>
    public static readonly Guid Pascal = new Guid(-1358664494, (short) -12063, (short) 4562, (byte) 151, (byte) 124, (byte) 0, (byte) 160, (byte) 201, (byte) 180, (byte) 213, (byte) 12);
    /// <summary>指定要与符号存储区一起使用的 ILAssembly 语言类型的 GUID。</summary>
    public static readonly Guid ILAssembly = new Guid(-1358664493, (short) -12063, (short) 4562, (byte) 151, (byte) 124, (byte) 0, (byte) 160, (byte) 201, (byte) 180, (byte) 213, (byte) 12);
    /// <summary>指定要与符号存储区一起使用的 JScript 语言类型的 GUID。</summary>
    public static readonly Guid JScript = new Guid(974311606, (short) -15764, (short) 4560, (byte) 180, (byte) 66, (byte) 0, (byte) 160, (byte) 36, (byte) 74, (byte) 29, (byte) 210);
    /// <summary>指定要与符号存储区一起使用的 SMC 语言类型的 GUID。</summary>
    public static readonly Guid SMC = new Guid(228302715, (short) 26129, (short) 4563, (byte) 189, (byte) 42, (byte) 0, (byte) 0, (byte) 248, (byte) 8, (byte) 73, (byte) 189);
    /// <summary>指定要与符号存储区一起使用的 C++ 语言类型的 GUID。</summary>
    public static readonly Guid MCPlusPlus = new Guid(1261829608, (short) 1990, (short) 4563, (byte) 144, (byte) 83, (byte) 0, (byte) 192, (byte) 79, (byte) 163, (byte) 2, (byte) 161);
  }
}
