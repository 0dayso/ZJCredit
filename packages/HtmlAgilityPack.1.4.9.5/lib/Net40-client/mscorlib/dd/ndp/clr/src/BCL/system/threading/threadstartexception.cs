// Decompiled with JetBrains decompiler
// Type: System.Threading.ThreadStartException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Serialization;

namespace System.Threading
{
  /// <summary>当基础操作系统线程已启动但该线程尚未准备好执行用户代码前，托管线程中出现错误，则会引发异常。</summary>
  [Serializable]
  public sealed class ThreadStartException : SystemException
  {
    private ThreadStartException()
      : base(Environment.GetResourceString("Arg_ThreadStartException"))
    {
      this.SetErrorCode(-2146233051);
    }

    private ThreadStartException(Exception reason)
      : base(Environment.GetResourceString("Arg_ThreadStartException"), reason)
    {
      this.SetErrorCode(-2146233051);
    }

    internal ThreadStartException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
