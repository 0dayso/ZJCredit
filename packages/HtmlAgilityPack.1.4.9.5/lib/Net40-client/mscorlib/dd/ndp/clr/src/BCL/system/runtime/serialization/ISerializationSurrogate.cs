// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.ISerializationSurrogate
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Serialization
{
  /// <summary>实现序列化代理项选择器，此选择器允许一个对象对另一个对象执行序列化和反序列化。</summary>
  [ComVisible(true)]
  public interface ISerializationSurrogate
  {
    /// <summary>使用将对象序列化所需的数据填充所提供的 <see cref="T:System.Runtime.Serialization.SerializationInfo" />。</summary>
    /// <param name="obj">要序列化的对象。</param>
    /// <param name="info">要填充数据的 <see cref="T:System.Runtime.Serialization.SerializationInfo" />。</param>
    /// <param name="context">此序列化的目标（请参见 <see cref="T:System.Runtime.Serialization.StreamingContext" />）。</param>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    [SecurityCritical]
    void GetObjectData(object obj, SerializationInfo info, StreamingContext context);

    /// <summary>使用 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 中的信息填充对象。</summary>
    /// <returns>已填充的反序列化对象。</returns>
    /// <param name="obj">要填充的对象。</param>
    /// <param name="info">要填充对象的信息。</param>
    /// <param name="context">对象从其中进行反序列化的源。</param>
    /// <param name="selector">兼容代理项搜索开始处的代理项选择器。</param>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    [SecurityCritical]
    object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector);
  }
}
