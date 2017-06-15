// Decompiled with JetBrains decompiler
// Type: System.StringComparison
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>指定 <see cref="M:System.String.Compare(System.String,System.String)" /> 和 <see cref="M:System.String.Equals(System.Object)" /> 方法的某些重载要使用的区域、大小写和排序规则。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum StringComparison
  {
    [__DynamicallyInvokable] CurrentCulture,
    [__DynamicallyInvokable] CurrentCultureIgnoreCase,
    InvariantCulture,
    InvariantCultureIgnoreCase,
    [__DynamicallyInvokable] Ordinal,
    [__DynamicallyInvokable] OrdinalIgnoreCase,
  }
}
