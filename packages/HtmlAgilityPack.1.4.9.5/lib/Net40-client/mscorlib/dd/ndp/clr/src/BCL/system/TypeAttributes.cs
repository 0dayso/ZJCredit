// Decompiled with JetBrains decompiler
// Type: System.Reflection.TypeAttributes
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>指定类型属性。</summary>
  [Flags]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [System.Serializable]
  public enum TypeAttributes
  {
    [__DynamicallyInvokable] VisibilityMask = 7,
    [__DynamicallyInvokable] NotPublic = 0,
    [__DynamicallyInvokable] Public = 1,
    [__DynamicallyInvokable] NestedPublic = 2,
    [__DynamicallyInvokable] NestedPrivate = NestedPublic | Public,
    [__DynamicallyInvokable] NestedFamily = 4,
    [__DynamicallyInvokable] NestedAssembly = NestedFamily | Public,
    [__DynamicallyInvokable] NestedFamANDAssem = NestedFamily | NestedPublic,
    [__DynamicallyInvokable] NestedFamORAssem = NestedFamANDAssem | Public,
    [__DynamicallyInvokable] LayoutMask = 24,
    [__DynamicallyInvokable] AutoLayout = 0,
    [__DynamicallyInvokable] SequentialLayout = 8,
    [__DynamicallyInvokable] ExplicitLayout = 16,
    [__DynamicallyInvokable] ClassSemanticsMask = 32,
    [__DynamicallyInvokable] Class = 0,
    [__DynamicallyInvokable] Interface = ClassSemanticsMask,
    [__DynamicallyInvokable] Abstract = 128,
    [__DynamicallyInvokable] Sealed = 256,
    [__DynamicallyInvokable] SpecialName = 1024,
    [__DynamicallyInvokable] Import = 4096,
    [__DynamicallyInvokable] Serializable = 8192,
    [ComVisible(false), __DynamicallyInvokable] WindowsRuntime = 16384,
    [__DynamicallyInvokable] StringFormatMask = 196608,
    [__DynamicallyInvokable] AnsiClass = 0,
    [__DynamicallyInvokable] UnicodeClass = 65536,
    [__DynamicallyInvokable] AutoClass = 131072,
    [__DynamicallyInvokable] CustomFormatClass = AutoClass | UnicodeClass,
    [__DynamicallyInvokable] CustomFormatMask = 12582912,
    [__DynamicallyInvokable] BeforeFieldInit = 1048576,
    ReservedMask = 264192,
    [__DynamicallyInvokable] RTSpecialName = 2048,
    [__DynamicallyInvokable] HasSecurity = 262144,
  }
}
