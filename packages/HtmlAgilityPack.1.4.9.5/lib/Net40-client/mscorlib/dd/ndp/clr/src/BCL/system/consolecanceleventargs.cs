// Decompiled with JetBrains decompiler
// Type: System.ConsoleCancelEventArgs
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  /// <summary>为 <see cref="E:System.Console.CancelKeyPress" /> 事件提供数据。此类不能被继承。</summary>
  /// <filterpriority>2</filterpriority>
  [Serializable]
  public sealed class ConsoleCancelEventArgs : EventArgs
  {
    private ConsoleSpecialKey _type;
    private bool _cancel;

    /// <summary>获取或设置一个值，该值指示同时按下 <see cref="F:System.ConsoleModifiers.Control" /> 修改键和 <see cref="F:System.ConsoleKey.C" /> 控制台键 (Ctrl+C) 或 Ctrl+Break 键是否会终止当前进程。默认值为 false，这将终止当前进程。</summary>
    /// <returns>如果当前进程在事件处理程序结束时应继续，则为 true；如果当前进程应终止，则为 false。默认值为 false；当前进程将在事件处理程序返回时终止。如果为 true，当前进程将继续。</returns>
    /// <filterpriority>2</filterpriority>
    public bool Cancel
    {
      get
      {
        return this._cancel;
      }
      set
      {
        this._cancel = value;
      }
    }

    /// <summary>获取中断当前进程的修改键和控制台键的组合。</summary>
    /// <returns>一个枚举值指定中断当前进程的组合键。没有默认值。</returns>
    /// <filterpriority>1</filterpriority>
    public ConsoleSpecialKey SpecialKey
    {
      get
      {
        return this._type;
      }
    }

    internal ConsoleCancelEventArgs(ConsoleSpecialKey type)
    {
      this._type = type;
      this._cancel = false;
    }
  }
}
