// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.KeyValuePair`2
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Text;

namespace System.Collections.Generic
{
  /// <summary>定义可设置或检索的键/值对。</summary>
  /// <typeparam name="TKey">键的类型。</typeparam>
  /// <typeparam name="TValue">值的类型。</typeparam>
  /// <filterpriority>1</filterpriority>
  [__DynamicallyInvokable]
  [Serializable]
  public struct KeyValuePair<TKey, TValue>
  {
    private TKey key;
    private TValue value;

    /// <summary>获取键/值对中的键。</summary>
    /// <returns>一个 <paramref name="TKey" />，它是 <see cref="T:System.Collections.Generic.KeyValuePair`2" /> 的键。</returns>
    [__DynamicallyInvokable]
    public TKey Key
    {
      [__DynamicallyInvokable] get
      {
        return this.key;
      }
    }

    /// <summary>获取键/值对中的值。</summary>
    /// <returns>一个 <paramref name="TValue" />，它是 <see cref="T:System.Collections.Generic.KeyValuePair`2" /> 的值。</returns>
    [__DynamicallyInvokable]
    public TValue Value
    {
      [__DynamicallyInvokable] get
      {
        return this.value;
      }
    }

    /// <summary>用指定的键和值初始化 <see cref="T:System.Collections.Generic.KeyValuePair`2" /> 结构的新实例。</summary>
    /// <param name="key">每个键/值对中定义的对象。</param>
    /// <param name="value">与 <paramref name="key" /> 相关联的定义。</param>
    [__DynamicallyInvokable]
    public KeyValuePair(TKey key, TValue value)
    {
      this.key = key;
      this.value = value;
    }

    /// <summary>使用键和值的字符串表示形式返回 <see cref="T:System.Collections.Generic.KeyValuePair`2" /> 的字符串表示形式。</summary>
    /// <returns>
    /// <see cref="T:System.Collections.Generic.KeyValuePair`2" /> 的字符串表示形式，它包括键和值的字符串表示形式。</returns>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      StringBuilder sb = StringBuilderCache.Acquire(16);
      sb.Append('[');
      if ((object) this.Key != null)
        sb.Append(this.Key.ToString());
      sb.Append(", ");
      if ((object) this.Value != null)
        sb.Append(this.Value.ToString());
      sb.Append(']');
      return StringBuilderCache.GetStringAndRelease(sb);
    }
  }
}
