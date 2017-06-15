// Decompiled with JetBrains decompiler
// Type: System.Resources.IResourceWriter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Resources
{
  /// <summary>提供将资源写到输出文件或输出流的基本功能。</summary>
  [ComVisible(true)]
  public interface IResourceWriter : IDisposable
  {
    /// <summary>将 <see cref="T:System.String" /> 类型的已命名资源添加到要编写的资源列表中。</summary>
    /// <param name="name">资源的名称。</param>
    /// <param name="value">资源的值。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 参数为 null。</exception>
    void AddResource(string name, string value);

    /// <summary>将 <see cref="T:System.Object" /> 类型的已命名资源添加到要编写的资源列表中。</summary>
    /// <param name="name">资源的名称。</param>
    /// <param name="value">资源的值。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数为 null。</exception>
    void AddResource(string name, object value);

    /// <summary>将 8 位无符号整数数组作为命名资源添加到要写的资源列表中。</summary>
    /// <param name="name">资源的名称。</param>
    /// <param name="value">8 位无符号整数数组形式的资源值。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数为 null。</exception>
    void AddResource(string name, byte[] value);

    /// <summary>关闭基础资源文件或流，并确保所有数据已写入该文件。</summary>
    void Close();

    /// <summary>将所有由 <see cref="M:System.Resources.IResourceWriter.AddResource(System.String,System.String)" /> 方法添加的资源写到输出文件或输出流中。</summary>
    void Generate();
  }
}
