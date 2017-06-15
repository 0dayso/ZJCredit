// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.ISerializable
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Serialization
{
  /// <summary>允许对象控制其自己的序列化和反序列化过程。</summary>
  [ComVisible(true)]
  public interface ISerializable
  {
    /// <summary>使用将目标对象序列化所需的数据填充 <see cref="T:System.Runtime.Serialization.SerializationInfo" />。</summary>
    /// <param name="info">要填充数据的 <see cref="T:System.Runtime.Serialization.SerializationInfo" />。</param>
    /// <param name="context">此序列化的目标（请参见 <see cref="T:System.Runtime.Serialization.StreamingContext" />）。</param>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    [SecurityCritical]
    void GetObjectData(SerializationInfo info, StreamingContext context);
  }
}
