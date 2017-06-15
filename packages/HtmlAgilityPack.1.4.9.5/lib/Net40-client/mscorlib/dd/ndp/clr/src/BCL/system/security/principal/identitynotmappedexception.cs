// Decompiled with JetBrains decompiler
// Type: System.Security.Principal.IdentityNotMappedException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security.Principal
{
  /// <summary>表示其标识未能映射到已知标识的主体的一个异常。</summary>
  [ComVisible(false)]
  [Serializable]
  public sealed class IdentityNotMappedException : SystemException
  {
    private IdentityReferenceCollection unmappedIdentities;

    /// <summary>表示 <see cref="T:System.Security.Principal.IdentityNotMappedException" /> 异常的未映射标识的集合。</summary>
    /// <returns>未映射标识的集合。</returns>
    public IdentityReferenceCollection UnmappedIdentities
    {
      get
      {
        if (this.unmappedIdentities == null)
          this.unmappedIdentities = new IdentityReferenceCollection();
        return this.unmappedIdentities;
      }
    }

    /// <summary>初始化 <see cref="T:System.Security.Principal.IdentityNotMappedException" /> 类的新实例。</summary>
    public IdentityNotMappedException()
      : base(Environment.GetResourceString("IdentityReference_IdentityNotMapped"))
    {
    }

    /// <summary>使用指定的错误消息初始化 <see cref="T:System.Security.Principal.IdentityNotMappedException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    public IdentityNotMappedException(string message)
      : base(message)
    {
    }

    /// <summary>使用指定的错误消息和内部异常初始化 <see cref="T:System.Security.Principal.IdentityNotMappedException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="inner">导致当前异常的异常。如果 <paramref name="inner" /> 不为空，则在处理内部异常的 catch 块中引发当前异常。</param>
    public IdentityNotMappedException(string message, Exception inner)
      : base(message, inner)
    {
    }

    internal IdentityNotMappedException(string message, IdentityReferenceCollection unmappedIdentities)
      : this(message)
    {
      this.unmappedIdentities = unmappedIdentities;
    }

    internal IdentityNotMappedException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    /// <summary>获取序列化信息，其中包含创建此 <see cref="T:System.Security.Principal.IdentityNotMappedException" /> 对象的实例所需的数据。</summary>
    /// <param name="serializationInfo">
    /// <see cref="T:System.Runtime.Serialization." />
    /// <see cref="SerializationInfoobject" />，它存有有关所引发的异常的序列化对象数据。</param>
    /// <param name="streamingContext">
    /// <see cref="T:System.Runtime.SerializationInfo." />
    /// <see cref="StreamingContext" /> 对象包含有关源或目标的上下文信息。</param>
    [SecurityCritical]
    public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
    {
      base.GetObjectData(serializationInfo, streamingContext);
    }
  }
}
