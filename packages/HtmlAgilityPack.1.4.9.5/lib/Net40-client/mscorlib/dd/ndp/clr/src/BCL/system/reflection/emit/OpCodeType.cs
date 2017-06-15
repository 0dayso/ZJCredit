// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.OpCodeType
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
  /// <summary>描述 Microsoft 中间语言 (MSIL) 指令的类型。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum OpCodeType
  {
    [Obsolete("This API has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")] Annotation,
    [__DynamicallyInvokable] Macro,
    [__DynamicallyInvokable] Nternal,
    [__DynamicallyInvokable] Objmodel,
    [__DynamicallyInvokable] Prefix,
    [__DynamicallyInvokable] Primitive,
  }
}
