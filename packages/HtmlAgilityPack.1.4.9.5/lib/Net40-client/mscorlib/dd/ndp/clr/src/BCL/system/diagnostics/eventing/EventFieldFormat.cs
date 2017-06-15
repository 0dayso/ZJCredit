// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.EventFieldFormat
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Tracing
{
  /// <summary>指定如何设置用户定义类型的值的格式，可以用于重写字段的默认格式设置。</summary>
  [__DynamicallyInvokable]
  public enum EventFieldFormat
  {
    [__DynamicallyInvokable] Default = 0,
    [__DynamicallyInvokable] String = 2,
    [__DynamicallyInvokable] Boolean = 3,
    [__DynamicallyInvokable] Hexadecimal = 4,
    [__DynamicallyInvokable] Xml = 11,
    [__DynamicallyInvokable] Json = 12,
    [__DynamicallyInvokable] HResult = 15,
  }
}
