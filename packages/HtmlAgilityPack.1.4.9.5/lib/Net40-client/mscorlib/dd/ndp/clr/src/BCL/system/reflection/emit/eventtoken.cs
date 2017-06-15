// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.EventToken
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
  /// <summary>表示由元数据返回以表示某事件的 Token。</summary>
  [ComVisible(true)]
  [Serializable]
  public struct EventToken
  {
    /// <summary>
    /// <see cref="P:System.Reflection.Emit.EventToken.Token" /> 值为 0 的默认 EventToken。</summary>
    public static readonly EventToken Empty;
    internal int m_event;

    /// <summary>检索此事件的元数据标记。</summary>
    /// <returns>只读。检索此事件的元数据标记。</returns>
    public int Token
    {
      get
      {
        return this.m_event;
      }
    }

    internal EventToken(int str)
    {
      this.m_event = str;
    }

    /// <summary>指示两个 <see cref="T:System.Reflection.Emit.EventToken" /> 结构是否等同。</summary>
    /// <returns>如果 <paramref name="a" /> 等于 <paramref name="b" />，则为 true；否则为 false。</returns>
    /// <param name="a">要与 <paramref name="b" /> 进行比较的 <see cref="T:System.Reflection.Emit.EventToken" />。</param>
    /// <param name="b">要与 <paramref name="a" /> 进行比较的 <see cref="T:System.Reflection.Emit.EventToken" />。</param>
    public static bool operator ==(EventToken a, EventToken b)
    {
      return a.Equals(b);
    }

    /// <summary>指示两个 <see cref="T:System.Reflection.Emit.EventToken" /> 结构是否不相等。</summary>
    /// <returns>如果 <paramref name="a" /> 不等于 <paramref name="b" />，则为 true；否则为 false。</returns>
    /// <param name="a">要与 <paramref name="b" /> 进行比较的 <see cref="T:System.Reflection.Emit.EventToken" />。</param>
    /// <param name="b">要与 <paramref name="a" /> 进行比较的 <see cref="T:System.Reflection.Emit.EventToken" />。</param>
    public static bool operator !=(EventToken a, EventToken b)
    {
      return !(a == b);
    }

    /// <summary>生成此事件的哈希代码。</summary>
    /// <returns>返回此实例的哈希代码。</returns>
    public override int GetHashCode()
    {
      return this.m_event;
    }

    /// <summary>检查给定对象是否为 EventToken 的实例并检查它是否等于此实例。</summary>
    /// <returns>如果 <paramref name="obj" /> 为 EventToken 的实例并等于当前实例，则返回 true；否则返回 false。</returns>
    /// <param name="obj">与该实例进行比较的对象。</param>
    public override bool Equals(object obj)
    {
      if (obj is EventToken)
        return this.Equals((EventToken) obj);
      return false;
    }

    /// <summary>指示当前实例是否等于指定的 <see cref="T:System.Reflection.Emit.EventToken" />。</summary>
    /// <returns>如果 <paramref name="obj" /> 的值等于当前实例的值，则为 true；否则为 false。</returns>
    /// <param name="obj">要与当前实例进行比较的 <see cref="T:System.Reflection.Emit.EventToken" />。</param>
    public bool Equals(EventToken obj)
    {
      return obj.m_event == this.m_event;
    }
  }
}
