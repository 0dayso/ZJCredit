// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.ISafeSerializationData
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.Serialization
{
  /// <summary>启用安全透明的代码中的自定义异常数据的序列化。</summary>
  public interface ISafeSerializationData
  {
    /// <summary>方法在取消序列化实例之前被调用。</summary>
    /// <param name="deserialized">包含实例状态的对象。</param>
    void CompleteDeserialization(object deserialized);
  }
}
