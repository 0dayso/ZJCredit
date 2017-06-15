// Decompiled with JetBrains decompiler
// Type: System.Threading.ThreadAbortException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Threading
{
  /// <summary>当对 <see cref="M:System.Threading.Thread.Abort(System.Object)" /> 方法发出调用时引发的异常。此类不能被继承。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [Serializable]
  public sealed class ThreadAbortException : SystemException
  {
    /// <summary>获取一个对象，该对象包含与线程中止相关的应用程序特定的信息。</summary>
    /// <returns>包含应用程序特定的信息的对象。</returns>
    /// <filterpriority>2</filterpriority>
    public object ExceptionState
    {
      [SecuritySafeCritical] get
      {
        return Thread.CurrentThread.AbortReason;
      }
    }

    private ThreadAbortException()
      : base(Exception.GetMessageFromNativeResources(Exception.ExceptionMessageKind.ThreadAbort))
    {
      this.SetErrorCode(-2146233040);
    }

    internal ThreadAbortException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
