// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.ICriticalNotifyCompletion
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Runtime.CompilerServices
{
  /// <summary>表示等候程序，其计划等待操作完成时的后续部分。</summary>
  [__DynamicallyInvokable]
  public interface ICriticalNotifyCompletion : INotifyCompletion
  {
    /// <summary>计划实例完成时调用的延续操作。</summary>
    /// <param name="continuation">要在操作完成时调用的操作。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="continuation" /> 参数是 null（在 Visual Basic 中为 Nothing）。</exception>
    [SecurityCritical]
    [__DynamicallyInvokable]
    void UnsafeOnCompleted(Action continuation);
  }
}
