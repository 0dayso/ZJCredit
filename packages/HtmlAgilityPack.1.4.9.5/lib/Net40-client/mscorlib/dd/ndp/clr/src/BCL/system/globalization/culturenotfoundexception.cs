// Decompiled with JetBrains decompiler
// Type: System.Globalization.CultureNotFoundException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Globalization
{
  /// <summary>当调用的方法尝试构造一个计算机上不可用的区域性时引发的异常。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class CultureNotFoundException : ArgumentException, ISerializable
  {
    private string m_invalidCultureName;
    private int? m_invalidCultureId;

    /// <summary>获取找不到的区域性标识符。</summary>
    /// <returns>无效的区域性标识符。</returns>
    public virtual int? InvalidCultureId
    {
      get
      {
        return this.m_invalidCultureId;
      }
    }

    /// <summary>获取找不到的区域性名称。</summary>
    /// <returns>无效的区域性名称。</returns>
    [__DynamicallyInvokable]
    public virtual string InvalidCultureName
    {
      [__DynamicallyInvokable] get
      {
        return this.m_invalidCultureName;
      }
    }

    private static string DefaultMessage
    {
      get
      {
        return Environment.GetResourceString("Argument_CultureNotSupported");
      }
    }

    private string FormatedInvalidCultureId
    {
      get
      {
        if (this.InvalidCultureId.HasValue)
          return string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0} (0x{0:x4})", (object) this.InvalidCultureId.Value);
        return this.InvalidCultureName;
      }
    }

    /// <summary>获取解释异常原因的错误消息。</summary>
    /// <returns>描述异常的详细信息的文本字符串。</returns>
    [__DynamicallyInvokable]
    public override string Message
    {
      [__DynamicallyInvokable] get
      {
        string message = base.Message;
        if (!this.m_invalidCultureId.HasValue && this.m_invalidCultureName == null)
          return message;
        string resourceString = Environment.GetResourceString("Argument_CultureInvalidIdentifier", (object) this.FormatedInvalidCultureId);
        if (message == null)
          return resourceString;
        return message + Environment.NewLine + resourceString;
      }
    }

    /// <summary>初始化 <see cref="T:System.Globalization.CultureNotFoundException" /> 类的新实例，将其消息字符串设置为系统提供的消息。</summary>
    [__DynamicallyInvokable]
    public CultureNotFoundException()
      : base(CultureNotFoundException.DefaultMessage)
    {
    }

    /// <summary>使用指定的错误消息初始化 <see cref="T:System.Globalization.CultureNotFoundException" /> 类的新实例。</summary>
    /// <param name="message">与此异常一起显示的错误消息。</param>
    [__DynamicallyInvokable]
    public CultureNotFoundException(string message)
      : base(message)
    {
    }

    /// <summary>使用指定的错误消息和导致此异常的参数的名称来初始化 <see cref="T:System.Globalization.CultureNotFoundException" /> 类的新实例。</summary>
    /// <param name="paramName">导致当前异常的参数的名称。</param>
    /// <param name="message">与此异常一起显示的错误消息。</param>
    [__DynamicallyInvokable]
    public CultureNotFoundException(string paramName, string message)
      : base(message, paramName)
    {
    }

    /// <summary>使用指定的错误消息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.Globalization.CultureNotFoundException" /> 类的新实例。</summary>
    /// <param name="message">与此异常一起显示的错误消息。</param>
    /// <param name="innerException">导致当前异常的异常。如果 <paramref name="innerException" /> 参数不为空引用，则在处理内部异常的 catch 块中引发当前异常。</param>
    [__DynamicallyInvokable]
    public CultureNotFoundException(string message, Exception innerException)
      : base(message, innerException)
    {
    }

    /// <summary>使用指定的错误消息、无效的区域性 ID 和导致此异常的参数的名称来初始化 <see cref="T:System.Globalization.CultureNotFoundException" /> 类的新实例。</summary>
    /// <param name="paramName">导致当前异常的参数的名称。</param>
    /// <param name="invalidCultureId">找不到的区域性 ID。</param>
    /// <param name="message">与此异常一起显示的错误消息。</param>
    public CultureNotFoundException(string paramName, int invalidCultureId, string message)
      : base(message, paramName)
    {
      this.m_invalidCultureId = new int?(invalidCultureId);
    }

    /// <summary>使用指定的错误消息、无效的区域性 ID 和对导致此异常的内部异常的引用来初始化 <see cref="T:System.Globalization.CultureNotFoundException" /> 类的新实例。</summary>
    /// <param name="message">与此异常一起显示的错误消息。</param>
    /// <param name="invalidCultureId">找不到的区域性 ID。</param>
    /// <param name="innerException">导致当前异常的异常。如果 <paramref name="innerException" /> 参数不为空引用，则在处理内部异常的 catch 块中引发当前异常。</param>
    public CultureNotFoundException(string message, int invalidCultureId, Exception innerException)
      : base(message, innerException)
    {
      this.m_invalidCultureId = new int?(invalidCultureId);
    }

    /// <summary>使用指定的错误消息、无效的区域性名称和导致此异常的参数的名称来初始化 <see cref="T:System.Globalization.CultureNotFoundException" /> 类的新实例。</summary>
    /// <param name="paramName">导致当前异常的参数的名称。</param>
    /// <param name="invalidCultureName">找不到的区域性名称。</param>
    /// <param name="message">与此异常一起显示的错误消息。</param>
    [__DynamicallyInvokable]
    public CultureNotFoundException(string paramName, string invalidCultureName, string message)
      : base(message, paramName)
    {
      this.m_invalidCultureName = invalidCultureName;
    }

    /// <summary>使用指定的错误消息、无效的区域性名称和对导致此异常的内部异常的引用来初始化 <see cref="T:System.Globalization.CultureNotFoundException" /> 类的新实例。</summary>
    /// <param name="message">与此异常一起显示的错误消息。</param>
    /// <param name="invalidCultureName">找不到的区域性名称。</param>
    /// <param name="innerException">导致当前异常的异常。如果 <paramref name="innerException" /> 参数不为空引用，则在处理内部异常的 catch 块中引发当前异常。</param>
    [__DynamicallyInvokable]
    public CultureNotFoundException(string message, string invalidCultureName, Exception innerException)
      : base(message, innerException)
    {
      this.m_invalidCultureName = invalidCultureName;
    }

    /// <summary>使用指定的序列化数据和上下文初始化 <see cref="T:System.Globalization.CultureNotFoundException" /> 类的新实例。</summary>
    /// <param name="info">承载序列化对象数据的对象。</param>
    /// <param name="context">关于来源和目标的上下文信息</param>
    protected CultureNotFoundException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      this.m_invalidCultureId = (int?) info.GetValue("InvalidCultureId", typeof (int?));
      this.m_invalidCultureName = (string) info.GetValue("InvalidCultureName", typeof (string));
    }

    /// <summary>设置带有参数名和附加异常信息的 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 对象。</summary>
    /// <param name="info">承载序列化对象数据的对象。</param>
    /// <param name="context">关于来源和目标的上下文信息</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="info" /> is null.</exception>
    [SecurityCritical]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      base.GetObjectData(info, context);
      int? nullable1 = new int?();
      int? nullable2 = this.m_invalidCultureId;
      info.AddValue("InvalidCultureId", (object) nullable2, typeof (int?));
      info.AddValue("InvalidCultureName", (object) this.m_invalidCultureName, typeof (string));
    }
  }
}
