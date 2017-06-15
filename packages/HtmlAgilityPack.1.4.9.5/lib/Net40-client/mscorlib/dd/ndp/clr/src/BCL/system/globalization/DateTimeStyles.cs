// Decompiled with JetBrains decompiler
// Type: System.Globalization.DateTimeStyles
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Globalization
{
  /// <summary>定义一些格式设置选项，这些选项可自定义许多日期和时间分析方法的字符串分析方法。</summary>
  [Flags]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum DateTimeStyles
  {
    [__DynamicallyInvokable] None = 0,
    [__DynamicallyInvokable] AllowLeadingWhite = 1,
    [__DynamicallyInvokable] AllowTrailingWhite = 2,
    [__DynamicallyInvokable] AllowInnerWhite = 4,
    [__DynamicallyInvokable] AllowWhiteSpaces = AllowInnerWhite | AllowTrailingWhite | AllowLeadingWhite,
    [__DynamicallyInvokable] NoCurrentDateDefault = 8,
    [__DynamicallyInvokable] AdjustToUniversal = 16,
    [__DynamicallyInvokable] AssumeLocal = 32,
    [__DynamicallyInvokable] AssumeUniversal = 64,
    [__DynamicallyInvokable] RoundtripKind = 128,
  }
}
