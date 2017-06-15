// Decompiled with JetBrains decompiler
// Type: System.Resources.MissingSatelliteAssemblyException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Resources
{
  /// <summary>当默认区域性资源的附属程序集丢失时引发的异常。</summary>
  [ComVisible(true)]
  [Serializable]
  public class MissingSatelliteAssemblyException : SystemException
  {
    private string _cultureName;

    /// <summary>获取默认区域性的名称。</summary>
    /// <returns>默认区域性的名称。</returns>
    public string CultureName
    {
      get
      {
        return this._cultureName;
      }
    }

    /// <summary>使用默认属性初始化 <see cref="T:System.Resources.MissingSatelliteAssemblyException" /> 类的新实例。</summary>
    public MissingSatelliteAssemblyException()
      : base(Environment.GetResourceString("MissingSatelliteAssembly_Default"))
    {
      this.SetErrorCode(-2146233034);
    }

    /// <summary>使用指定的错误消息初始化 <see cref="T:System.Resources.MissingSatelliteAssemblyException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    public MissingSatelliteAssemblyException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233034);
    }

    /// <summary>用指定的错误信息和非特定区域性的名称初始化 <see cref="T:System.Resources.MissingSatelliteAssemblyException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="cultureName">非特定区域性的名称。</param>
    public MissingSatelliteAssemblyException(string message, string cultureName)
      : base(message)
    {
      this.SetErrorCode(-2146233034);
      this._cultureName = cultureName;
    }

    /// <summary>使用指定错误消息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.Resources.MissingSatelliteAssemblyException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="inner">导致当前异常的异常。如果 <paramref name="inner" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    public MissingSatelliteAssemblyException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2146233034);
    }

    /// <summary>用序列化数据初始化 <see cref="T:System.Resources.MissingSatelliteAssemblyException" /> 类的新实例。</summary>
    /// <param name="info">承载序列化对象数据的对象。</param>
    /// <param name="context">有关异常的源或目标的上下文信息。</param>
    protected MissingSatelliteAssemblyException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
