// Decompiled with JetBrains decompiler
// Type: System.Reflection.PropertyAttributes
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>定义可能与属性 (Property) 关联的属性 (Attribute)。这些特性值定义在 corhdr.h 中。</summary>
  [Flags]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum PropertyAttributes
  {
    [__DynamicallyInvokable] None = 0,
    [__DynamicallyInvokable] SpecialName = 512,
    ReservedMask = 62464,
    [__DynamicallyInvokable] RTSpecialName = 1024,
    [__DynamicallyInvokable] HasDefault = 4096,
    Reserved2 = 8192,
    Reserved3 = 16384,
    Reserved4 = 32768,
  }
}
