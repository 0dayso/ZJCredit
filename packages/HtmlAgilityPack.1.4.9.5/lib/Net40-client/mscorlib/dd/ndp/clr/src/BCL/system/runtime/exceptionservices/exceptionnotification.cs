// Decompiled with JetBrains decompiler
// Type: System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.ConstrainedExecution;

namespace System.Runtime.ExceptionServices
{
  /// <summary>在公共语言运行时开始搜索事件处理程序之前，为托管异常首次出现时引发的通知事件提供数据。</summary>
  public class FirstChanceExceptionEventArgs : EventArgs
  {
    private Exception m_Exception;

    /// <summary>与托管代码中引发的异常对应的托管异常对象。</summary>
    /// <returns>新引发的异常。</returns>
    public Exception Exception
    {
      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)] get
      {
        return this.m_Exception;
      }
    }

    /// <summary>使用指定的异常初始化 <see cref="T:System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs" /> 类的新实例。</summary>
    /// <param name="exception">刚才由托管代码引发的异常，该异常将由 <see cref="E:System.AppDomain.UnhandledException" /> 事件检查。</param>
    public FirstChanceExceptionEventArgs(Exception exception)
    {
      this.m_Exception = exception;
    }
  }
}
