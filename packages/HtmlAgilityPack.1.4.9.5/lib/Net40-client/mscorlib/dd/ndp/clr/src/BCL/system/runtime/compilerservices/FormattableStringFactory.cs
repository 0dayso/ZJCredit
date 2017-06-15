// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.FormattableStringFactory
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.CompilerServices
{
  /// <summary>提供一个静态方法，用于从复合格式字符串及其参数创建 <see cref="T:System.FormattableString" /> 对象。</summary>
  [__DynamicallyInvokable]
  public static class FormattableStringFactory
  {
    /// <summary>从复合格式字符串及其参数创建 <see cref="T:System.FormattableString" /> 实例。</summary>
    /// <returns>表示复合格式字符串及其参数的对象。</returns>
    /// <param name="format">复合格式字符串。</param>
    /// <param name="arguments">要在结果字符串中插入其字符串表示形式的参数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="format" /> 为 null。- 或 -<paramref name="arguments" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public static FormattableString Create(string format, params object[] arguments)
    {
      if (format == null)
        throw new ArgumentNullException("format");
      if (arguments == null)
        throw new ArgumentNullException("arguments");
      return (FormattableString) new FormattableStringFactory.ConcreteFormattableString(format, arguments);
    }

    private sealed class ConcreteFormattableString : FormattableString
    {
      private readonly string _format;
      private readonly object[] _arguments;

      public override string Format
      {
        get
        {
          return this._format;
        }
      }

      public override int ArgumentCount
      {
        get
        {
          return this._arguments.Length;
        }
      }

      internal ConcreteFormattableString(string format, object[] arguments)
      {
        this._format = format;
        this._arguments = arguments;
      }

      public override object[] GetArguments()
      {
        return this._arguments;
      }

      public override object GetArgument(int index)
      {
        return this._arguments[index];
      }

      public override string ToString(IFormatProvider formatProvider)
      {
        return string.Format(formatProvider, this._format, this._arguments);
      }
    }
  }
}
