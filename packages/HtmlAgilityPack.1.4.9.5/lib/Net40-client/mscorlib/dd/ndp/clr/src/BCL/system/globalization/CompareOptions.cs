// Decompiled with JetBrains decompiler
// Type: System.Globalization.CompareOptions
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Globalization
{
  /// <summary>定义要用于 <see cref="T:System.Globalization.CompareInfo" /> 的字符串比较选项。</summary>
  [Flags]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum CompareOptions
  {
    [__DynamicallyInvokable] None = 0,
    [__DynamicallyInvokable] IgnoreCase = 1,
    [__DynamicallyInvokable] IgnoreNonSpace = 2,
    [__DynamicallyInvokable] IgnoreSymbols = 4,
    [__DynamicallyInvokable] IgnoreKanaType = 8,
    [__DynamicallyInvokable] IgnoreWidth = 16,
    [__DynamicallyInvokable] OrdinalIgnoreCase = 268435456,
    [__DynamicallyInvokable] StringSort = 536870912,
    [__DynamicallyInvokable] Ordinal = 1073741824,
  }
}
