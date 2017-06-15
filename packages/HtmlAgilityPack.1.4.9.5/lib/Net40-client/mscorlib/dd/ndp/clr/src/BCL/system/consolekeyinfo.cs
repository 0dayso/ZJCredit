// Decompiled with JetBrains decompiler
// Type: System.ConsoleKeyInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  /// <summary>描述按下的控制台键，包括控制台键表示的字符以及 Shift、Alt 和 Ctrl 修改键的状态。</summary>
  /// <filterpriority>1</filterpriority>
  [Serializable]
  public struct ConsoleKeyInfo
  {
    private char _keyChar;
    private ConsoleKey _key;
    private ConsoleModifiers _mods;

    /// <summary>获取当前 <see cref="T:System.ConsoleKeyInfo" /> 对象表示的 Unicode 字符。</summary>
    /// <returns>与当前 <see cref="T:System.ConsoleKeyInfo" /> 对象表示的控制台键对应的对象。</returns>
    /// <filterpriority>1</filterpriority>
    public char KeyChar
    {
      get
      {
        return this._keyChar;
      }
    }

    /// <summary>获取当前 <see cref="T:System.ConsoleKeyInfo" /> 对象表示的控制台键。</summary>
    /// <returns>标识按下的控制台键的值。</returns>
    /// <filterpriority>1</filterpriority>
    public ConsoleKey Key
    {
      get
      {
        return this._key;
      }
    }

    /// <summary>获取 <see cref="T:System.ConsoleModifiers" /> 值的一个按位组合，指定与控制台键同时按下的一个或多个修改键。</summary>
    /// <returns>枚举值的按位组合。没有默认值。</returns>
    /// <filterpriority>1</filterpriority>
    public ConsoleModifiers Modifiers
    {
      get
      {
        return this._mods;
      }
    }

    /// <summary>用指定的字符、控制台键和修改键初始化 <see cref="T:System.ConsoleKeyInfo" /> 结构的新实例。</summary>
    /// <param name="keyChar">与 <paramref name="key" /> 参数对应的 Unicode 字符。</param>
    /// <param name="key">与 <paramref name="keyChar" /> 参数对应的控制台键。</param>
    /// <param name="shift">true 指示按下了 Shift 键；否则为 false。</param>
    /// <param name="alt">true 指示按下了 Alt 键；否则为 false。</param>
    /// <param name="control">true 指示按下了 Ctrl 键；否则为 false。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="key" /> 参数的数值小于 0 或大于 255。</exception>
    public ConsoleKeyInfo(char keyChar, ConsoleKey key, bool shift, bool alt, bool control)
    {
      if (key < (ConsoleKey) 0 || key > (ConsoleKey.F16 | ConsoleKey.F17))
        throw new ArgumentOutOfRangeException("key", Environment.GetResourceString("ArgumentOutOfRange_ConsoleKey"));
      this._keyChar = keyChar;
      this._key = key;
      this._mods = (ConsoleModifiers) 0;
      if (shift)
        this._mods = this._mods | ConsoleModifiers.Shift;
      if (alt)
        this._mods = this._mods | ConsoleModifiers.Alt;
      if (!control)
        return;
      this._mods = this._mods | ConsoleModifiers.Control;
    }

    /// <summary>指示指定的 <see cref="T:System.ConsoleKeyInfo" /> 对象是否相等。</summary>
    /// <returns>如果 <paramref name="a" /> 等于 <paramref name="b" />，则为 true；否则为 false。</returns>
    /// <param name="a">要比较的第一个对象。</param>
    /// <param name="b">要比较的第二个对象。</param>
    public static bool operator ==(ConsoleKeyInfo a, ConsoleKeyInfo b)
    {
      return a.Equals(b);
    }

    /// <summary>指示指定的 <see cref="T:System.ConsoleKeyInfo" /> 对象是否不等。</summary>
    /// <returns>如果 <paramref name="a" /> 不等于 <paramref name="b" />，则为 true；否则为 false。</returns>
    /// <param name="a">要比较的第一个对象。</param>
    /// <param name="b">要比较的第二个对象。</param>
    public static bool operator !=(ConsoleKeyInfo a, ConsoleKeyInfo b)
    {
      return !(a == b);
    }

    /// <summary>获取一个值，该值指示指定的对象是否等于当前的 <see cref="T:System.ConsoleKeyInfo" /> 对象。</summary>
    /// <returns>如果 <paramref name="value" /> 为等于当前的 <see cref="T:System.ConsoleKeyInfo" /> 对象的 <see cref="T:System.ConsoleKeyInfo" /> 对象，则为 true；否则为 false。</returns>
    /// <param name="value">要与当前的 <see cref="T:System.ConsoleKeyInfo" /> 对象进行比较的对象。</param>
    public override bool Equals(object value)
    {
      if (value is ConsoleKeyInfo)
        return this.Equals((ConsoleKeyInfo) value);
      return false;
    }

    /// <summary>获取一个值，该值指示指定的 <see cref="T:System.ConsoleKeyInfo" /> 对象是否等于当前的 <see cref="T:System.ConsoleKeyInfo" /> 对象。</summary>
    /// <returns>如果 <paramref name="obj" /> 与当前的 <see cref="T:System.ConsoleKeyInfo" /> 对象相同，则为 true；否则为 false。</returns>
    /// <param name="obj">要与当前的 <see cref="T:System.ConsoleKeyInfo" /> 对象进行比较的对象。</param>
    public bool Equals(ConsoleKeyInfo obj)
    {
      if ((int) obj._keyChar == (int) this._keyChar && obj._key == this._key)
        return obj._mods == this._mods;
      return false;
    }

    /// <summary>返回当前 <see cref="T:System.ConsoleKeyInfo" /> 对象的哈希代码。</summary>
    /// <returns>32 位有符号整数哈希代码。</returns>
    public override int GetHashCode()
    {
      return (int) ((ConsoleModifiers) this._keyChar | this._mods);
    }
  }
}
