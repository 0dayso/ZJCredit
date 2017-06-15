// Decompiled with JetBrains decompiler
// Type: System.ICustomFormatter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>定义一种方法，它支持自定义设置对象的值的格式。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public interface ICustomFormatter
  {
    /// <summary>使用指定的格式和区域性特定格式设置信息将指定对象的值转换为等效的字符串表示形式。</summary>
    /// <returns>
    /// <paramref name="arg" /> 的值的字符串表示形式，按照 <paramref name="format" /> 和 <paramref name="formatProvider" /> 的指定来进行格式设置。</returns>
    /// <param name="format">包含格式规范的格式字符串。</param>
    /// <param name="arg">要设置格式的对象。</param>
    /// <param name="formatProvider">一个对象，它提供有关当前实例的格式信息。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    string Format(string format, object arg, IFormatProvider formatProvider);
  }
}
