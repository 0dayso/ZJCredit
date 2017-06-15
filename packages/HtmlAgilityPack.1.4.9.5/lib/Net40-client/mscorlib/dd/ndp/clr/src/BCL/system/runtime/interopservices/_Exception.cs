// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices._Exception
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.Serialization;
using System.Security;

namespace System.Runtime.InteropServices
{
  /// <summary>向非托管代码公开 <see cref="T:System.Exception" /> 类的公共成员。</summary>
  [Guid("b36b5c63-42ef-38bc-a07e-0b34c98f164a")]
  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  [CLSCompliant(false)]
  [ComVisible(true)]
  public interface _Exception
  {
    /// <summary>为 COM 对象提供对 <see cref="P:System.Exception.Message" /> 属性的版本无关的访问。</summary>
    /// <returns>解释异常原因的错误消息或空字符串 ("")。</returns>
    string Message { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Exception.StackTrace" /> 属性的版本无关的访问。</summary>
    /// <returns>一个字符串，它描述调用堆栈的内容，其中首先显示最近的方法调用。</returns>
    string StackTrace { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Exception.HelpLink" /> 属性的版本无关的访问。</summary>
    /// <returns>帮助文件的统一资源名称 (URN) 或统一资源定位器 (URL)。</returns>
    string HelpLink { get; set; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Exception.Source" /> 属性的版本无关的访问。</summary>
    /// <returns>导致错误的应用程序或对象的名称。</returns>
    string Source { get; set; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Exception.InnerException" /> 属性的版本无关的访问。</summary>
    /// <returns>一个 <see cref="T:System.Exception" /> 的实例，描述导致当前异常的错误。<see cref="P:System.Exception.InnerException" /> 属性返回与传递给构造函数的值相同的值，或者，如果没有向构造函数提供内部异常值，则返回空引用（Visual Basic 中为 Nothing）。此属性为只读。</returns>
    Exception InnerException { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Exception.TargetSite" /> 属性的版本无关的访问。</summary>
    /// <returns>引发当前异常的 <see cref="T:System.Reflection.MethodBase" /> 对象。</returns>
    MethodBase TargetSite { get; }

    /// <summary>为 COM 对象提供对 <see cref="M:System.Exception.ToString" /> 方法的版本无关的访问。</summary>
    /// <returns>表示当前 <see cref="T:System.Exception" /> 对象的字符串。</returns>
    string ToString();

    /// <summary>为 COM 对象提供对 <see cref="M:System.Object.Equals(System.Object)" /> 方法的版本无关的访问。</summary>
    /// <returns>如果指定的 <see cref="T:System.Object" /> 等于当前的 <see cref="T:System.Object" />，则为 true；否则为 false。</returns>
    /// <param name="obj">与当前的 <see cref="T:System.Object" /> 进行比较的 <see cref="T:System.Object" />。</param>
    bool Equals(object obj);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Object.GetHashCode" /> 方法的版本无关的访问。</summary>
    /// <returns>当前实例的哈希代码。</returns>
    int GetHashCode();

    /// <summary>为 COM 对象提供对 <see cref="M:System.Exception.GetType" /> 方法的版本无关的访问。</summary>
    /// <returns>一个 <see cref="T:System.Type" /> 对象，表示当前实例的确切运行时类型。</returns>
    Type GetType();

    /// <summary>为 COM 对象提供对 <see cref="M:System.Exception.GetBaseException" /> 方法的版本无关的访问。</summary>
    /// <returns>异常链中第一个被引发的异常。如果当前异常的 <see cref="P:System.Exception.InnerException" /> 属性是 null 引用（Visual Basic 中为 Nothing），则此属性返回当前异常。</returns>
    Exception GetBaseException();

    /// <summary>为 COM 对象提供对 <see cref="M:System.Exception.GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)" /> 方法的版本无关的访问</summary>
    /// <param name="info">
    /// <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 对象，包含有关所引发异常的序列化对象数据。</param>
    /// <param name="context">
    /// <see cref="T:System.Runtime.Serialization.StreamingContext" /> 结构，它包含有关源或目标的上下文信息。</param>
    [SecurityCritical]
    void GetObjectData(SerializationInfo info, StreamingContext context);
  }
}
