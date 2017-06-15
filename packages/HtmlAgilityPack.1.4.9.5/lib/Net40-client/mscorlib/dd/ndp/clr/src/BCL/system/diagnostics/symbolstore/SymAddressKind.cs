// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.SymbolStore.SymAddressKind
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
  /// <summary>指定 <see cref="T:System.Diagnostics.SymbolStore.ISymbolWriter" /> 接口的 <see cref="M:System.Diagnostics.SymbolStore.ISymbolWriter.DefineLocalVariable(System.String,System.Reflection.FieldAttributes,System.Byte[],System.Diagnostics.SymbolStore.SymAddressKind,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32)" />、<see cref="M:System.Diagnostics.SymbolStore.ISymbolWriter.DefineParameter(System.String,System.Reflection.ParameterAttributes,System.Int32,System.Diagnostics.SymbolStore.SymAddressKind,System.Int32,System.Int32,System.Int32)" /> 和 <see cref="M:System.Diagnostics.SymbolStore.ISymbolWriter.DefineField(System.Diagnostics.SymbolStore.SymbolToken,System.String,System.Reflection.FieldAttributes,System.Byte[],System.Diagnostics.SymbolStore.SymAddressKind,System.Int32,System.Int32,System.Int32)" /> 方法中的局部变量、参数和字段的地址类型。</summary>
  [ComVisible(true)]
  [Serializable]
  public enum SymAddressKind
  {
    ILOffset = 1,
    NativeRVA = 2,
    NativeRegister = 3,
    NativeRegisterRelative = 4,
    NativeOffset = 5,
    NativeRegisterRegister = 6,
    NativeRegisterStack = 7,
    NativeStackRegister = 8,
    BitField = 9,
    NativeSectionOffset = 10,
  }
}
