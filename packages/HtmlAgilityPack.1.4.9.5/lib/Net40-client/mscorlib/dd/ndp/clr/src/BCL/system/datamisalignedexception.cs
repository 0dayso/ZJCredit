// Decompiled with JetBrains decompiler
// Type: System.DataMisalignedException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
  /// <summary>当在某个地址读取或写入一个单元的数据，但该地址的数据大小不是该数据单元的整数倍时引发的异常。此类不能被继承。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class DataMisalignedException : SystemException
  {
    /// <summary>初始化 <see cref="T:System.DataMisalignedException" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public DataMisalignedException()
      : base(Environment.GetResourceString("Arg_DataMisalignedException"))
    {
      this.SetErrorCode(-2146233023);
    }

    /// <summary>使用指定的错误信息初始化 <see cref="T:System.DataMisalignedException" /> 类的新实例。</summary>
    /// <param name="message">描述错误的 <see cref="T:System.String" /> 对象。<paramref name="message" /> 的内容被设计为人可理解的形式。此构造函数的调用方需要确保此字符串已针对当前系统区域性进行了本地化。</param>
    [__DynamicallyInvokable]
    public DataMisalignedException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233023);
    }

    /// <summary>用指定的错误信息和基础异常初始化 <see cref="T:System.DataMisalignedException" /> 类的新实例。</summary>
    /// <param name="message">描述错误的 <see cref="T:System.String" /> 对象。<paramref name="message" /> 的内容被设计为人可理解的形式。此构造函数的调用方需要确保此字符串已针对当前系统区域性进行了本地化。</param>
    /// <param name="innerException">导致当前 <see cref="T:System.DataMisalignedException" /> 的异常。如果 <paramref name="innerException" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    [__DynamicallyInvokable]
    public DataMisalignedException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2146233023);
    }

    internal DataMisalignedException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
