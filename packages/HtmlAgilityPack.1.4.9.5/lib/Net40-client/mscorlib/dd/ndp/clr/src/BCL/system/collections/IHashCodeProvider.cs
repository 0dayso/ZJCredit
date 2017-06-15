// Decompiled with JetBrains decompiler
// Type: System.Collections.IHashCodeProvider
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Collections
{
  /// <summary>使用自定义哈希函数为对象提供哈希代码。</summary>
  /// <filterpriority>2</filterpriority>
  [Obsolete("Please use IEqualityComparer instead.")]
  [ComVisible(true)]
  public interface IHashCodeProvider
  {
    /// <summary>返回指定对象的哈希代码。</summary>
    /// <returns>指定对象的哈希代码。</returns>
    /// <param name="obj">
    /// <see cref="T:System.Object" />，将为其返回哈希代码。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="obj" /> 的类型为引用类型，<paramref name="obj" /> 为 null。</exception>
    /// <filterpriority>2</filterpriority>
    int GetHashCode(object obj);
  }
}
