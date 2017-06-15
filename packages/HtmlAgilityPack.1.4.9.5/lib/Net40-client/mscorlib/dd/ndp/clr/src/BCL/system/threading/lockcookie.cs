// Decompiled with JetBrains decompiler
// Type: System.Threading.LockCookie
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Threading
{
  /// <summary>定义实现单个编写器/多个阅读器语义的锁。这是值类型。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  public struct LockCookie
  {
    private int _dwFlags;
    private int _dwWriterSeqNum;
    private int _wReaderAndWriterLevel;
    private int _dwThreadID;

    /// <summary>指示两个 <see cref="T:System.Threading.LockCookie" /> 结构是否等同。</summary>
    /// <returns>如果 <paramref name="a" /> 等于 <paramref name="b" />，则为 true；否则为 false。</returns>
    /// <param name="a">要与 <paramref name="b" /> 进行比较的 <see cref="T:System.Threading.LockCookie" />。</param>
    /// <param name="b">要与 <paramref name="a" /> 进行比较的 <see cref="T:System.Threading.LockCookie" />。</param>
    public static bool operator ==(LockCookie a, LockCookie b)
    {
      return a.Equals(b);
    }

    /// <summary>指示两个 <see cref="T:System.Threading.LockCookie" /> 结构是否不相等。</summary>
    /// <returns>如果 <paramref name="a" /> 不等于 <paramref name="b" />，则为 true；否则为 false。</returns>
    /// <param name="a">要与 <paramref name="b" /> 进行比较的 <see cref="T:System.Threading.LockCookie" />。</param>
    /// <param name="b">要与 <paramref name="a" /> 进行比较的 <see cref="T:System.Threading.LockCookie" />。</param>
    public static bool operator !=(LockCookie a, LockCookie b)
    {
      return !(a == b);
    }

    /// <summary>返回此实例的哈希代码。</summary>
    /// <returns>32 位有符号整数哈希代码。</returns>
    public override int GetHashCode()
    {
      return this._dwFlags + this._dwWriterSeqNum + this._wReaderAndWriterLevel + this._dwThreadID;
    }

    /// <summary>指示指定的对象是否为 <see cref="T:System.Threading.LockCookie" /> 并且等于当前实例。</summary>
    /// <returns>如果 <paramref name="obj" /> 的值等于当前实例的值，则为 true；否则为 false。</returns>
    /// <param name="obj">要与当前实例进行比较的对象。</param>
    public override bool Equals(object obj)
    {
      if (obj is LockCookie)
        return this.Equals((LockCookie) obj);
      return false;
    }

    /// <summary>指定当前实例是否等于指定的 <see cref="T:System.Threading.LockCookie" />。</summary>
    /// <returns>如果 <paramref name="obj" /> 等于当前实例的值，则为 true；否则为 false。</returns>
    /// <param name="obj">要与当前实例进行比较的 <see cref="T:System.Threading.LockCookie" />。</param>
    public bool Equals(LockCookie obj)
    {
      if (obj._dwFlags == this._dwFlags && obj._dwWriterSeqNum == this._dwWriterSeqNum && obj._wReaderAndWriterLevel == this._wReaderAndWriterLevel)
        return obj._dwThreadID == this._dwThreadID;
      return false;
    }
  }
}
