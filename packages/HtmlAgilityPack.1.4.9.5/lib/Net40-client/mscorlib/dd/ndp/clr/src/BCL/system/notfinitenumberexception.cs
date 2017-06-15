// Decompiled with JetBrains decompiler
// Type: System.NotFiniteNumberException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
  /// <summary>当浮点值为正无穷大、负无穷大或非数字 (NaN) 时引发的异常。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [Serializable]
  public class NotFiniteNumberException : ArithmeticException
  {
    private double _offendingNumber;

    /// <summary>获取无效数字，它可以是正无穷大、负无穷大或非数字 (NaN)。</summary>
    /// <returns>无效数字。</returns>
    /// <filterpriority>2</filterpriority>
    public double OffendingNumber
    {
      get
      {
        return this._offendingNumber;
      }
    }

    /// <summary>初始化 <see cref="T:System.NotFiniteNumberException" /> 类的新实例。</summary>
    public NotFiniteNumberException()
      : base(Environment.GetResourceString("Arg_NotFiniteNumberException"))
    {
      this._offendingNumber = 0.0;
      this.SetErrorCode(-2146233048);
    }

    /// <summary>使用无效数字初始化 <see cref="T:System.NotFiniteNumberException" /> 类的新实例。</summary>
    /// <param name="offendingNumber">引发异常的参数的值。</param>
    public NotFiniteNumberException(double offendingNumber)
    {
      this._offendingNumber = offendingNumber;
      this.SetErrorCode(-2146233048);
    }

    /// <summary>使用指定的错误信息初始化 <see cref="T:System.NotFiniteNumberException" /> 类的新实例。</summary>
    /// <param name="message">描述错误的消息。</param>
    public NotFiniteNumberException(string message)
      : base(message)
    {
      this._offendingNumber = 0.0;
      this.SetErrorCode(-2146233048);
    }

    /// <summary>使用指定的错误消息和无效数字初始化 <see cref="T:System.NotFiniteNumberException" /> 类的新实例。</summary>
    /// <param name="message">描述错误的消息。</param>
    /// <param name="offendingNumber">引发异常的参数的值。</param>
    public NotFiniteNumberException(string message, double offendingNumber)
      : base(message)
    {
      this._offendingNumber = offendingNumber;
      this.SetErrorCode(-2146233048);
    }

    /// <summary>使用指定错误消息和对作为此异常的根本原因的内部异常的引用来初始化 <see cref="T:System.NotFiniteNumberException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="innerException">导致当前异常的异常。如果 <paramref name="innerException" /> 参数不是空引用（在 Visual Basic 中为 Nothing），则在处理内部异常的 catch 块中引发当前异常。</param>
    public NotFiniteNumberException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2146233048);
    }

    /// <summary>使用指定的错误消息、无效数字和对内部异常（为该异常的根源）的引用来初始化 <see cref="T:System.NotFiniteNumberException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="offendingNumber">引发异常的参数的值。</param>
    /// <param name="innerException">导致当前异常的异常。如果 <paramref name="innerException" /> 参数不是空引用（在 Visual Basic 中为 Nothing），则在处理内部异常的 catch 块中引发当前异常。</param>
    public NotFiniteNumberException(string message, double offendingNumber, Exception innerException)
      : base(message, innerException)
    {
      this._offendingNumber = offendingNumber;
      this.SetErrorCode(-2146233048);
    }

    /// <summary>用序列化数据初始化 <see cref="T:System.NotFiniteNumberException" /> 类的新实例。</summary>
    /// <param name="info">承载序列化对象数据的对象。</param>
    /// <param name="context">关于来源和目标的上下文信息</param>
    protected NotFiniteNumberException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      this._offendingNumber = (double) info.GetInt32("OffendingNumber");
    }

    /// <summary>使用无效数字和附加异常信息设置 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 对象。</summary>
    /// <param name="info">承载序列化对象数据的对象。</param>
    /// <param name="context">关于来源和目标的上下文信息</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="info" /> 对象为 null。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    /// </PermissionSet>
    [SecurityCritical]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      base.GetObjectData(info, context);
      info.AddValue("OffendingNumber", (object) this._offendingNumber, typeof (int));
    }
  }
}
