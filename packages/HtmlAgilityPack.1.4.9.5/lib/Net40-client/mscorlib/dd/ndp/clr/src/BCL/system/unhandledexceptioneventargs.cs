// Decompiled with JetBrains decompiler
// Type: System.UnhandledExceptionEventArgs
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace System
{
  /// <summary>为以下情况下引发的事件提供数据：存在一个不是在任何应用程序域中处理的异常。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [Serializable]
  public class UnhandledExceptionEventArgs : EventArgs
  {
    private object _Exception;
    private bool _IsTerminating;

    /// <summary>获取未处理的异常对象。</summary>
    /// <returns>未处理的异常对象。</returns>
    /// <filterpriority>2</filterpriority>
    public object ExceptionObject
    {
      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)] get
      {
        return this._Exception;
      }
    }

    /// <summary>指示公共语言运行时是否即将终止。</summary>
    /// <returns>如果运行库即将终止，则为 true；否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    public bool IsTerminating
    {
      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)] get
      {
        return this._IsTerminating;
      }
    }

    /// <summary>使用异常对象和公共语言运行时终止标记初始化 <see cref="T:System.UnhandledExceptionEventArgs" /> 类的新实例。</summary>
    /// <param name="exception">未处理的异常。 </param>
    /// <param name="isTerminating">如果运行库即将终止，则为 true；否则为 false。</param>
    public UnhandledExceptionEventArgs(object exception, bool isTerminating)
    {
      this._Exception = exception;
      this._IsTerminating = isTerminating;
    }
  }
}
