// Decompiled with JetBrains decompiler
// Type: System.AttributeTargets
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>指定可以对它们应用特性的应用程序元素。</summary>
  /// <filterpriority>2</filterpriority>
  [Flags]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum AttributeTargets
  {
    [__DynamicallyInvokable] Assembly = 1,
    [__DynamicallyInvokable] Module = 2,
    [__DynamicallyInvokable] Class = 4,
    [__DynamicallyInvokable] Struct = 8,
    [__DynamicallyInvokable] Enum = 16,
    [__DynamicallyInvokable] Constructor = 32,
    [__DynamicallyInvokable] Method = 64,
    [__DynamicallyInvokable] Property = 128,
    [__DynamicallyInvokable] Field = 256,
    [__DynamicallyInvokable] Event = 512,
    [__DynamicallyInvokable] Interface = 1024,
    [__DynamicallyInvokable] Parameter = 2048,
    [__DynamicallyInvokable] Delegate = 4096,
    [__DynamicallyInvokable] ReturnValue = 8192,
    [__DynamicallyInvokable] GenericParameter = 16384,
    [__DynamicallyInvokable] All = GenericParameter | ReturnValue | Delegate | Parameter | Interface | Event | Field | Property | Method | Constructor | Enum | Struct | Class | Module | Assembly,
  }
}
