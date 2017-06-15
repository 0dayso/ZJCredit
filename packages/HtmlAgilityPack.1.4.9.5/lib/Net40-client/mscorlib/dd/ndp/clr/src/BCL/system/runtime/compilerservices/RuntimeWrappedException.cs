// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.RuntimeWrappedException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Serialization;
using System.Security;

namespace System.Runtime.CompilerServices
{
  /// <summary>包装不是从 <see cref="T:System.Exception" /> 类派生的异常。此类不能被继承。</summary>
  [Serializable]
  public sealed class RuntimeWrappedException : Exception
  {
    private object m_wrappedException;

    /// <summary>获取由 <see cref="T:System.Runtime.CompilerServices.RuntimeWrappedException" /> 对象包装的对象。</summary>
    /// <returns>由 <see cref="T:System.Runtime.CompilerServices.RuntimeWrappedException" /> 对象包装的对象。</returns>
    public object WrappedException
    {
      get
      {
        return this.m_wrappedException;
      }
    }

    private RuntimeWrappedException(object thrownObject)
      : base(Environment.GetResourceString("RuntimeWrappedException"))
    {
      this.SetErrorCode(-2146233026);
      this.m_wrappedException = thrownObject;
    }

    internal RuntimeWrappedException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      this.m_wrappedException = info.GetValue("WrappedException", typeof (object));
    }

    /// <summary>使用有关异常的信息设置 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 对象。</summary>
    /// <param name="info">
    /// <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 对象，包含有关所引发异常的序列化对象数据。</param>
    /// <param name="context">
    /// <see cref="T:System.Runtime.Serialization.StreamingContext" /> 对象，它包含有关源或目标的上下文信息。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="info" /> 参数为 null。</exception>
    [SecurityCritical]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      base.GetObjectData(info, context);
      info.AddValue("WrappedException", this.m_wrappedException, typeof (object));
    }
  }
}
