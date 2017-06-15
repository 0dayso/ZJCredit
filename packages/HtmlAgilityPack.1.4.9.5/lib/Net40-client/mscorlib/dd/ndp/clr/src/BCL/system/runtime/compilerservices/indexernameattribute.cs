// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.IndexerNameAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
  /// <summary>指示使索引器在不直接支持索引器的编程语言中已知的名称。</summary>
  [AttributeUsage(AttributeTargets.Property, Inherited = true)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class IndexerNameAttribute : Attribute
  {
    /// <summary>初始化 <see cref="T:System.Runtime.CompilerServices.IndexerNameAttribute" /> 类的新实例。</summary>
    /// <param name="indexerName">显示给其他语言的索引器名称。</param>
    [__DynamicallyInvokable]
    public IndexerNameAttribute(string indexerName)
    {
    }
  }
}
