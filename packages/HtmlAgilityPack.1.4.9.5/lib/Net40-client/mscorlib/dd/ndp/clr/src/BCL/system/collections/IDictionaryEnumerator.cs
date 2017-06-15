// Decompiled with JetBrains decompiler
// Type: System.Collections.IDictionaryEnumerator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Collections
{
  /// <summary>枚举非泛型字典的元素。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public interface IDictionaryEnumerator : IEnumerator
  {
    /// <summary>获取当前字典项的键。</summary>
    /// <returns>当前枚举元素的键。</returns>
    /// <exception cref="T:System.InvalidOperationException">将 <see cref="T:System.Collections.IDictionaryEnumerator" /> 定位于字典的第一项之前或最后一项之后。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    object Key { [__DynamicallyInvokable] get; }

    /// <summary>获取当前字典项的值。</summary>
    /// <returns>当前枚举元素的值。</returns>
    /// <exception cref="T:System.InvalidOperationException">将 <see cref="T:System.Collections.IDictionaryEnumerator" /> 定位于字典的第一项之前或最后一项之后。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    object Value { [__DynamicallyInvokable] get; }

    /// <summary>同时获取当前字典项的键和值。</summary>
    /// <returns>同时包含当前字典项的键和值的 <see cref="T:System.Collections.DictionaryEntry" />。</returns>
    /// <exception cref="T:System.InvalidOperationException">将 <see cref="T:System.Collections.IDictionaryEnumerator" /> 定位于字典的第一项之前或最后一项之后。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    DictionaryEntry Entry { [__DynamicallyInvokable] get; }
  }
}
