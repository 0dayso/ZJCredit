// Decompiled with JetBrains decompiler
// Type: System.IFormattable
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>提供将对象的值格式化为字符串表示形式的功能。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public interface IFormattable
  {
    /// <summary>使用指定的格式格式化当前实例的值。</summary>
    /// <returns>使用指定格式的当前实例的值。</returns>
    /// <param name="format">要使用的格式。- 或 -null 引用（Visual Basic 中为 Nothing）将使用为 <see cref="T:System.IFormattable" /> 实现的类型所定义的默认格式。</param>
    /// <param name="formatProvider">要用于设置值格式的提供程序。- 或 -null 引用（Visual Basic 中为 Nothing）将从操作系统的当前区域设置中获取数字格式信息。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    string ToString(string format, IFormatProvider formatProvider);
  }
}
