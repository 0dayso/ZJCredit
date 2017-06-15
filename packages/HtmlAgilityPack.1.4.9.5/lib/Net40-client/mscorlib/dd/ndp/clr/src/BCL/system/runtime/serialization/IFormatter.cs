// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.IFormatter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.IO;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
  /// <summary>提供将序列化对象格式化的功能。</summary>
  [ComVisible(true)]
  public interface IFormatter
  {
    /// <summary>获取或设置当前格式化程序所使用的 <see cref="T:System.Runtime.Serialization.SurrogateSelector" />。</summary>
    /// <returns>当前格式化程序所使用的 <see cref="T:System.Runtime.Serialization.SurrogateSelector" />。</returns>
    ISurrogateSelector SurrogateSelector { get; set; }

    /// <summary>获取或设置在反序列化过程中执行类型查找的 <see cref="T:System.Runtime.Serialization.SerializationBinder" />。</summary>
    /// <returns>在反序列化过程中执行类型查找的 <see cref="T:System.Runtime.Serialization.SerializationBinder" />。</returns>
    SerializationBinder Binder { get; set; }

    /// <summary>获取或设置用于序列化和反序列化的 <see cref="T:System.Runtime.Serialization.StreamingContext" />。</summary>
    /// <returns>用于序列化和反序列化的 <see cref="T:System.Runtime.Serialization.StreamingContext" />。</returns>
    StreamingContext Context { get; set; }

    /// <summary>反序列化所提供流中的数据并重新组成对象图形。</summary>
    /// <returns>反序列化的图形的顶级对象。</returns>
    /// <param name="serializationStream">包含要反序列化的数据的流。</param>
    object Deserialize(Stream serializationStream);

    /// <summary>将对象或具有给定根的对象图形序列化为所提供的流。</summary>
    /// <param name="serializationStream">格式化程序在其中放置序列化数据的流。此流可以引用多种后备存储区（如文件、网络、内存等）。</param>
    /// <param name="graph">要序列化的对象或对象图形的根。将自动序列化此根对象的所有子对象。</param>
    void Serialize(Stream serializationStream, object graph);
  }
}
