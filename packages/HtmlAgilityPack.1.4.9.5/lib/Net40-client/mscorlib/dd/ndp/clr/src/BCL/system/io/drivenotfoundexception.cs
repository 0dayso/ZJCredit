// Decompiled with JetBrains decompiler
// Type: System.IO.DriveNotFoundException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.IO
{
  /// <summary>当尝试访问的驱动器或共享不可用时引发的异常。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [Serializable]
  public class DriveNotFoundException : IOException
  {
    /// <summary>初始化 <see cref="T:System.IO.DriveNotFoundException" /> 类的新实例，使其消息字符串设置为系统提供的消息，其 HRESULT 设置为 COR_E_DIRECTORYNOTFOUND。</summary>
    public DriveNotFoundException()
      : base(Environment.GetResourceString("Arg_DriveNotFoundException"))
    {
      this.SetErrorCode(-2147024893);
    }

    /// <summary>用指定的消息字符串和设置为 COR_E_DIRECTORYNOTFOUND 的 HRESULT 初始化 <see cref="T:System.IO.DriveNotFoundException" /> 类的新实例。</summary>
    /// <param name="message">描述错误的 <see cref="T:System.String" /> 对象。此构造函数的调用方需要确保此字符串已针对当前系统区域性进行了本地化。</param>
    public DriveNotFoundException(string message)
      : base(message)
    {
      this.SetErrorCode(-2147024893);
    }

    /// <summary>使用指定的错误消息和对导致此异常的内部异常的引用来初始化 <see cref="T:System.IO.DriveNotFoundException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="innerException">导致当前异常的异常。如果 <paramref name="innerException" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    public DriveNotFoundException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2147024893);
    }

    /// <summary>用指定的序列化和上下文信息初始化 <see cref="T:System.IO.DriveNotFoundException" /> 类的新实例。</summary>
    /// <param name="info">
    /// <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 对象，包含有关所引发异常的序列化对象数据。</param>
    /// <param name="context">
    /// <see cref="T:System.Runtime.Serialization.StreamingContext" /> 对象，包含有关所引发异常的源或目标的上下文信息。</param>
    protected DriveNotFoundException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
