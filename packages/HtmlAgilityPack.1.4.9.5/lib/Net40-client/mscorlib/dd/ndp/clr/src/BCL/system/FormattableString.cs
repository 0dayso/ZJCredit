// Decompiled with JetBrains decompiler
// Type: System.FormattableString
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;

namespace System
{
  /// <summary>表示一个复合格式字符串，以及使用要设置格式的参数中。</summary>
  [__DynamicallyInvokable]
  public abstract class FormattableString : IFormattable
  {
    /// <summary>返回的复合格式字符串。</summary>
    /// <returns>复合格式字符串中。</returns>
    [__DynamicallyInvokable]
    public abstract string Format { [__DynamicallyInvokable] get; }

    /// <summary>获取要设置格式的参数的数目。</summary>
    /// <returns>要设置格式的参数的数目。</returns>
    [__DynamicallyInvokable]
    public abstract int ArgumentCount { [__DynamicallyInvokable] get; }

    /// <summary>实例化 <see cref="T:System.FormattableString" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    protected FormattableString()
    {
    }

    /// <summary>返回一个对象数组，包含一个或多个要设置格式的对象。</summary>
    /// <returns>一个对象数组，包含一个或多个要设置格式的对象。</returns>
    [__DynamicallyInvokable]
    public abstract object[] GetArguments();

    /// <summary>返回指定的索引位置处的参数。</summary>
    /// <returns>参数。</returns>
    /// <param name="index">参数的索引。其值可以介于 0 到减 1 所得的值<see cref="P:System.FormattableString.ArgumentCount" />。</param>
    [__DynamicallyInvokable]
    public abstract object GetArgument(int index);

    /// <summary>返回使用指定区域性格式设置约定设置复合格式字符串格式以及其参数而生成的字符串。</summary>
    /// <returns>结果字符串设置格式的使用的约定<paramref name="formatProvider" />。</returns>
    /// <param name="formatProvider">一个对象，提供区域性特定的格式设置信息。</param>
    [__DynamicallyInvokable]
    public abstract string ToString(IFormatProvider formatProvider);

    [__DynamicallyInvokable]
    string IFormattable.ToString(string ignored, IFormatProvider formatProvider)
    {
      return this.ToString(formatProvider);
    }

    /// <summary>返回参数使用固定区域性的约定的格式的结果字符串。</summary>
    /// <returns>使用固定区域性的约定设置格式的当前实例字符串。</returns>
    /// <param name="formattable">要转换到结果字符串的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="formattable" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public static string Invariant(FormattableString formattable)
    {
      if (formattable == null)
        throw new ArgumentNullException("formattable");
      return formattable.ToString((IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>返回使用当前区域性的格式设置约定设置格式沿其参数的复合格式字符串而得出的字符串。</summary>
    /// <returns>使用当前区域性的约定设置格式的一个结果字符串。</returns>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return this.ToString((IFormatProvider) CultureInfo.CurrentCulture);
    }
  }
}
