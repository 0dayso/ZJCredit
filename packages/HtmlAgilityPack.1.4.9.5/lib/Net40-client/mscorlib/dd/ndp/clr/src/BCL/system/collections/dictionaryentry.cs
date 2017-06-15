// Decompiled with JetBrains decompiler
// Type: System.Collections.DictionaryEntry
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Collections
{
  /// <summary>定义可设置或检索的字典键/值对。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public struct DictionaryEntry
  {
    private object _key;
    private object _value;

    /// <summary>获取或设置键/值对中的键。</summary>
    /// <returns>键/值对中的键。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public object Key
    {
      [__DynamicallyInvokable] get
      {
        return this._key;
      }
      [__DynamicallyInvokable] set
      {
        this._key = value;
      }
    }

    /// <summary>获取或设置键/值对中的值。</summary>
    /// <returns>键/值对中的值。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public object Value
    {
      [__DynamicallyInvokable] get
      {
        return this._value;
      }
      [__DynamicallyInvokable] set
      {
        this._value = value;
      }
    }

    /// <summary>使用指定的键和值初始化 <see cref="T:System.Collections.DictionaryEntry" /> 类型的实例。</summary>
    /// <param name="key">每个键/值对中定义的对象。</param>
    /// <param name="value">与 <paramref name="key" /> 相关联的定义。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> 为 null，并且 .NET Framework 版本为 1.0 或 1.1。</exception>
    [__DynamicallyInvokable]
    public DictionaryEntry(object key, object value)
    {
      this._key = key;
      this._value = value;
    }
  }
}
