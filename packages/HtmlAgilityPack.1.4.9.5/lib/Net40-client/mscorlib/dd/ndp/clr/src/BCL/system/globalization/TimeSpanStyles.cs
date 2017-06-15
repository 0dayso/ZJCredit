// Decompiled with JetBrains decompiler
// Type: System.Globalization.TimeSpanStyles
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Globalization
{
  /// <summary>定义一些格式设置选项，这些选项可自定义 <see cref="Overload:System.TimeSpan.ParseExact" /> 和 <see cref="Overload:System.TimeSpan.TryParseExact" /> 方法的字符串分析方法。</summary>
  [Flags]
  [__DynamicallyInvokable]
  public enum TimeSpanStyles
  {
    [__DynamicallyInvokable] None = 0,
    [__DynamicallyInvokable] AssumeNegative = 1,
  }
}
