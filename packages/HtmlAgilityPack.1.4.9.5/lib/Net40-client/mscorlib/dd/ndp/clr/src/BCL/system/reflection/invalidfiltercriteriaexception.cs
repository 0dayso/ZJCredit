// Decompiled with JetBrains decompiler
// Type: System.Reflection.InvalidFilterCriteriaException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
  /// <summary>当筛选条件对正使用的筛选器类型无效时，在 <see cref="M:System.Type.FindMembers(System.Reflection.MemberTypes,System.Reflection.BindingFlags,System.Reflection.MemberFilter,System.Object)" /> 中引发的异常。</summary>
  [ComVisible(true)]
  [Serializable]
  public class InvalidFilterCriteriaException : ApplicationException
  {
    /// <summary>用默认属性初始化 <see cref="T:System.Reflection.InvalidFilterCriteriaException" /> 类的新实例。</summary>
    public InvalidFilterCriteriaException()
      : base(Environment.GetResourceString("Arg_InvalidFilterCriteriaException"))
    {
      this.SetErrorCode(-2146232831);
    }

    /// <summary>用给定的 HRESULT 和消息字符串初始化 <see cref="T:System.Reflection.InvalidFilterCriteriaException" /> 类的新实例。</summary>
    /// <param name="message">该异常的消息文本。</param>
    public InvalidFilterCriteriaException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146232831);
    }

    /// <summary>使用指定错误消息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.Reflection.InvalidFilterCriteriaException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="inner">导致当前异常的异常。如果 <paramref name="inner" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    public InvalidFilterCriteriaException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2146232831);
    }

    /// <summary>用指定的序列化和上下文信息初始化 <see cref="T:System.Reflection.InvalidFilterCriteriaException" /> 类的新实例。</summary>
    /// <param name="info">SerializationInfo 对象，包含序列化此实例所需的信息。</param>
    /// <param name="context">StreamingContext 对象，包含与此实例关联的序列化流的源和目标。</param>
    protected InvalidFilterCriteriaException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
