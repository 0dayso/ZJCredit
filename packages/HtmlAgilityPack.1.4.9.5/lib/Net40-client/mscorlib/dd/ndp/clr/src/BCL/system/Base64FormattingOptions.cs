// Decompiled with JetBrains decompiler
// Type: System.Base64FormattingOptions
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  /// <summary>指定相关的 <see cref="Overload:System.Convert.ToBase64CharArray" /> 和 <see cref="Overload:System.Convert.ToBase64String" /> 方法是否在其输出中插入分行符。</summary>
  /// <filterpriority>1</filterpriority>
  [Flags]
  public enum Base64FormattingOptions
  {
    None = 0,
    InsertLineBreaks = 1,
  }
}
