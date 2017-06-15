// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.SoapFault
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata;
using System.Security;

namespace System.Runtime.Serialization.Formatters
{
  /// <summary>在 SOAP 消息中传送错误和状态信息。此类不能被继承。</summary>
  [SoapType(Embedded = true)]
  [ComVisible(true)]
  [Serializable]
  public sealed class SoapFault : ISerializable
  {
    private string faultCode;
    private string faultString;
    private string faultActor;
    [SoapField(Embedded = true)]
    private object detail;

    /// <summary>获取或设置 <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" /> 的错误代码。</summary>
    /// <returns>此 <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" /> 的错误代码。</returns>
    public string FaultCode
    {
      get
      {
        return this.faultCode;
      }
      set
      {
        this.faultCode = value;
      }
    }

    /// <summary>获取或设置 <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" /> 的错误信息。</summary>
    /// <returns>
    /// <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" /> 的错误信息。</returns>
    public string FaultString
    {
      get
      {
        return this.faultString;
      }
      set
      {
        this.faultString = value;
      }
    }

    /// <summary>获取或设置 <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" /> 的错误根源。</summary>
    /// <returns>
    /// <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" /> 的错误根源。</returns>
    public string FaultActor
    {
      get
      {
        return this.faultActor;
      }
      set
      {
        this.faultActor = value;
      }
    }

    /// <summary>获取或设置 <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" /> 所需的附加信息。</summary>
    /// <returns>
    /// <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" /> 所需的附加信息。</returns>
    public object Detail
    {
      get
      {
        return this.detail;
      }
      set
      {
        this.detail = value;
      }
    }

    /// <summary>使用默认值初始化 <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" /> 类的新实例。</summary>
    public SoapFault()
    {
    }

    /// <summary>在将属性设置为指定值的情况下，初始化 <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" /> 类的新实例。</summary>
    /// <param name="faultCode">
    /// <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" /> 的新实例的错误代码。错误代码标识所发生错误的类型。</param>
    /// <param name="faultString">
    /// <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" /> 的新实例的错误字符串。错误字符串提供可读的错误解释。</param>
    /// <param name="faultActor">生成错误的对象的 URI。</param>
    /// <param name="serverFault">公共语言运行时异常的说明。此信息还会在 <see cref="P:System.Runtime.Serialization.Formatters.SoapFault.Detail" /> 属性中提供。</param>
    public SoapFault(string faultCode, string faultString, string faultActor, ServerFault serverFault)
    {
      this.faultCode = faultCode;
      this.faultString = faultString;
      this.faultActor = faultActor;
      this.detail = (object) serverFault;
    }

    internal SoapFault(SerializationInfo info, StreamingContext context)
    {
      SerializationInfoEnumerator enumerator = info.GetEnumerator();
      while (enumerator.MoveNext())
      {
        string name = enumerator.Name;
        object obj = enumerator.Value;
        if (string.Compare(name, "faultCode", true, CultureInfo.InvariantCulture) == 0)
        {
          int num1 = ((string) obj).IndexOf(':');
          int num2;
          this.faultCode = num1 <= -1 ? (string) obj : ((string) obj).Substring(num2 = num1 + 1);
        }
        else if (string.Compare(name, "faultString", true, CultureInfo.InvariantCulture) == 0)
          this.faultString = (string) obj;
        else if (string.Compare(name, "faultActor", true, CultureInfo.InvariantCulture) == 0)
          this.faultActor = (string) obj;
        else if (string.Compare(name, "detail", true, CultureInfo.InvariantCulture) == 0)
          this.detail = obj;
      }
    }

    /// <summary>用数据填充指定的 <see cref="T:System.Runtime.Serialization.SerializationInfo" />，以序列化 <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" /> 对象。</summary>
    /// <param name="info">要填充数据的 <see cref="T:System.Runtime.Serialization.SerializationInfo" />。</param>
    /// <param name="context">当前序列化的目标（请参见 <see cref="T:System.Runtime.Serialization.StreamingContext" />）。</param>
    [SecurityCritical]
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      info.AddValue("faultcode", (object) ("SOAP-ENV:" + this.faultCode));
      info.AddValue("faultstring", (object) this.faultString);
      if (this.faultActor != null)
        info.AddValue("faultactor", (object) this.faultActor);
      info.AddValue("detail", this.detail, typeof (object));
    }
  }
}
