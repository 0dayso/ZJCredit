// Decompiled with JetBrains decompiler
// Type: System.Globalization.NumberStyles
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Globalization
{
  /// <summary>确定数字字符串参数中允许的样式，这些参数已传递到整数和浮点数类型的 Parse 和 TryParse 方法。</summary>
  [Flags]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum NumberStyles
  {
    [__DynamicallyInvokable] None = 0,
    [__DynamicallyInvokable] AllowLeadingWhite = 1,
    [__DynamicallyInvokable] AllowTrailingWhite = 2,
    [__DynamicallyInvokable] AllowLeadingSign = 4,
    [__DynamicallyInvokable] AllowTrailingSign = 8,
    [__DynamicallyInvokable] AllowParentheses = 16,
    [__DynamicallyInvokable] AllowDecimalPoint = 32,
    [__DynamicallyInvokable] AllowThousands = 64,
    [__DynamicallyInvokable] AllowExponent = 128,
    [__DynamicallyInvokable] AllowCurrencySymbol = 256,
    [__DynamicallyInvokable] AllowHexSpecifier = 512,
    [__DynamicallyInvokable] Integer = AllowLeadingSign | AllowTrailingWhite | AllowLeadingWhite,
    [__DynamicallyInvokable] HexNumber = AllowHexSpecifier | AllowTrailingWhite | AllowLeadingWhite,
    [__DynamicallyInvokable] Number = Integer | AllowThousands | AllowDecimalPoint | AllowTrailingSign,
    [__DynamicallyInvokable] Float = Integer | AllowExponent | AllowDecimalPoint,
    [__DynamicallyInvokable] Currency = Number | AllowCurrencySymbol | AllowParentheses,
    [__DynamicallyInvokable] Any = Currency | AllowExponent,
  }
}
