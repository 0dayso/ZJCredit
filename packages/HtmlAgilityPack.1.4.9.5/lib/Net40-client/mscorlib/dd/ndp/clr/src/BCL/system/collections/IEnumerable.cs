// Decompiled with JetBrains decompiler
// Type: System.Collections.IEnumerable
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Collections
{
  /// <summary>公开枚举数，该枚举数支持在非泛型集合上进行简单迭代。若要浏览此类型的 .NET Framework 源代码，请参阅引用源。</summary>
  /// <filterpriority>1</filterpriority>
  [Guid("496B0ABE-CDEE-11d3-88E8-00902754C43A")]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public interface IEnumerable
  {
    /// <summary>返回循环访问集合的枚举数。</summary>
    /// <returns>可用于循环访问集合的 <see cref="T:System.Collections.IEnumerator" /> 对象。</returns>
    /// <filterpriority>2</filterpriority>
    [DispId(-4)]
    [__DynamicallyInvokable]
    IEnumerator GetEnumerator();
  }
}
