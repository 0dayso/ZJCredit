// Decompiled with JetBrains decompiler
// Type: System.Runtime.ConstrainedExecution.CriticalFinalizerObject
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Runtime.ConstrainedExecution
{
  /// <summary>确保派生类中的所有终止代码均标记为关键。</summary>
  [ComVisible(true)]
  [SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
  public abstract class CriticalFinalizerObject
  {
    /// <summary>初始化 <see cref="T:System.Runtime.ConstrainedExecution.CriticalFinalizerObject" /> 类的新实例。</summary>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    protected CriticalFinalizerObject()
    {
    }

    /// <summary>释放由 <see cref="T:System.Runtime.ConstrainedExecution.CriticalFinalizerObject" /> 类使用的所有资源。</summary>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    ~CriticalFinalizerObject()
    {
    }
  }
}
